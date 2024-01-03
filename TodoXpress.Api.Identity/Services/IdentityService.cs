using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.WebUtilities;
using TodoXpress.Api.Identity.Entities;
using TodoXpress.Api.Identity.Services.Interfaces;

namespace TodoXpress.Api.Identity.Services;

internal sealed class IdentityService(
    UserManager<User> userManager, 
    RoleManager<Role> roleManager, 
    IUserStore<User> userStore,
    IEmailSender<User> emailSender,
    LinkGenerator linkGenerator,
    SignInManager<User> signInManager) : IIdentityService
{
    /// <inheritdoc/>
    public bool IsEmailValid(string email)
    {
        EmailAddressAttribute _emailAddressAttribute = new();
        if (string.IsNullOrEmpty(email) || !_emailAddressAttribute.IsValid(email))
            return false;

        return true;
    }

    /// <inheritdoc/>
    public bool SupportsUserEmail() => userManager.SupportsUserEmail;

    /// <inheritdoc/>
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

    /// <inheritdoc/>
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

    /// <inheritdoc/>
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

    /// <inheritdoc/>
    public async Task<IdentityResult> ResetPasswordAsync(string email, string code, string newPassword)
    {
        var user = await userManager.FindByEmailAsync(email);

        if (user is null || !await userManager.IsEmailConfirmedAsync(user))
            return IdentityResult.Failed(userManager.ErrorDescriber.InvalidEmail(email));

        IdentityResult result;
        try
        {
            var decodedCode = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            result = await userManager.ResetPasswordAsync(user, decodedCode, newPassword);
        }
        catch (FormatException)
        {
            result = IdentityResult.Failed(userManager.ErrorDescriber.InvalidToken());
        }

        return result;
    }

    /// <inheritdoc/>
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

    /// <inheritdoc/>
    public async Task<IdentityResult> SendPasswordForgotEmailAsync(string email)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user is null)
            return IdentityResult.Failed(userManager.ErrorDescriber.InvalidEmail(email));

        if (!await userManager.IsEmailConfirmedAsync(user))
            return IdentityResult.Failed(userManager.ErrorDescriber.InvalidEmail(email));

        var resetCode = await userManager.GeneratePasswordResetTokenAsync(user);
        resetCode = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(resetCode));

        await emailSender.SendPasswordResetCodeAsync(user, email, HtmlEncoder.Default.Encode(resetCode));

        return IdentityResult.Success;
    }
}
