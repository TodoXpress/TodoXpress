using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.WebUtilities;
using TodoXpress.Api.Identity.Entities;

namespace TodoXpress.Api.Identity.Services;

internal sealed class IdentityService(
    UserManager<User> userManager, 
    RoleManager<Role> roleManager, 
    IUserStore<User> userStore,
    IEmailSender<User> emailSender,
    LinkGenerator linkGenerator,
    SignInManager<User> signInManager)
{

    /// <summary>
    /// Checks if the given email is valid.
    /// </summary>
    /// <param name="email">The email to check.</param>
    /// <returns><see langword="true"/> if the email is valid, otherwise <see langword="false"/>.</returns>
    public static bool IsEmailValid(string email)
    {
        EmailAddressAttribute _emailAddressAttribute = new();
        if (string.IsNullOrEmpty(email) || !_emailAddressAttribute.IsValid(email))
            return false;

        return true;
    }

    /// <summary>
    /// Used to login a User.
    /// </summary>
    /// <param name="email">The email of the user.</param>
    /// <param name="password">The password of the user.</param>
    /// <returns>A bool which indicates the success of the operration an the user.</returns>
    public async Task<(bool, User?)> LoginAsync(string email, string password)
    {
        signInManager.AuthenticationScheme = IdentityConstants.BearerScheme;
        var result = await signInManager.PasswordSignInAsync(email, password, isPersistent: true, lockoutOnFailure: true);

        if (!result.Succeeded)
        {
            return (false, null);
        }

        var user = await userManager.FindByEmailAsync(email);
        if (user is null)
        {
            return (false, null);
        }

        return (true, user);
    }

    /// <summary>
    /// Gets a flag indicating whether the backing user store supports user emails.
    /// </summary>
    /// <returns>true if the backing user store supports user emails, otherwise false.</returns>
    public bool SupportsUserEmail() => userManager.SupportsUserEmail;

    /// <summary>
    /// Creates and returns the user. If an error occures, the returned user is <see langword="null"/>.
    /// </summary>
    /// <param name="request">The reuqest from which the user should be created.</param>
    /// <returns>The Task that represents the asynchronous operation, containing the IdentityResult of the operation.</returns>
    public async Task<(IdentityResult, User?)> CreateUserAsync(RegisterRequest request)
    {
        var emailStore = (IUserEmailStore<User>)userStore;
        var email = request.Email;

        if (!IsEmailValid(request.Email))
        {
            return (IdentityResult.Failed(userManager.ErrorDescriber.InvalidEmail(email)), null);
        }

        var user = new User();
        await userStore.SetUserNameAsync(user, email, CancellationToken.None);
        await emailStore.SetEmailAsync(user, email, CancellationToken.None);
        var result = await userManager.CreateAsync(user, request.Password);

        return (result, user);
    }

    /// <summary>
    /// Adds the user to the default role.
    /// </summary>
    /// <param name="user">The user to add to the role.</param>
    /// <returns>The Task that represents the asynchronous operation, containing the IdentityResult of the operation.</returns>
    public async Task<IdentityResult> AddUserToDefaultRoleAsync(User user)
    {
        if (!await roleManager.RoleExistsAsync(Role.DefaultUserRole))
        {
            await roleManager.CreateAsync(new Role()
            {
                Name = Role.DefaultUserRole
            });
        }

        var result = await userManager.AddToRoleAsync(user, Role.DefaultUserRole);

        return result;
    }

    /// <summary>
    /// Send a mail to confirm an email adress.
    /// </summary>
    /// <param name="user">The user for which the email should be send.</param>
    /// <param name="context">The http context of the request.</param>
    /// <param name="confirmEmailEndpointName">The name of the endpoint to confirm an email adress.</param>
    /// <param name="email">The email adress to confirm.</param>
    /// <param name="isChange"><see langword="true"/> if the is changed, otherwise <see langword="false"/>.</param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException">Is raised, when the confirm endpoint doesnt exists or the confirm link cant be generated.</exception>
    public async Task SendConfirmationEmailAsync(User user, HttpContext context, string confirmEmailEndpointName, string email, bool isChange = false)
    {
        
        if (confirmEmailEndpointName is null)
        {
            throw new NotSupportedException("No email confirmation endpoint was registered!");
        }

        var code = isChange
            ? await userManager.GenerateChangeEmailTokenAsync(user, email)
            : await userManager.GenerateEmailConfirmationTokenAsync(user);
        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

        var userId = await userManager.GetUserIdAsync(user);
        var routeValues = new RouteValueDictionary()
        {
            ["userId"] = userId,
            ["code"] = code,
        };

        if (isChange)
        {
            routeValues.Add("changedEmail", email);
        }

        var confirmEmailUrl = linkGenerator.GetUriByName(context, confirmEmailEndpointName, routeValues)
            ?? throw new NotSupportedException($"Could not find endpoint named '{confirmEmailEndpointName}'.");
        await emailSender.SendConfirmationLinkAsync(user, email, HtmlEncoder.Default.Encode(confirmEmailUrl));
    }
}
