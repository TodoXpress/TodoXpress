using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using TodoXpress.Api.Identity.Entities;

namespace TodoXpress.Api.Identity.Services.Interfaces;

public interface IIdentityService
{
    /// <summary>
    /// Checks if the given email is valid.
    /// </summary>
    /// <param name="email">The email to check.</param>
    /// <returns><see langword="true"/> if the email is valid, otherwise <see langword="false"/>.</returns>
    bool IsEmailValid(string email);

    /// <summary>
    /// Gets a flag indicating whether the backing user store supports user emails.
    /// </summary>
    /// <returns>true if the backing user store supports user emails, otherwise false.</returns>
    bool SupportsUserEmail();

    /// <summary>
    /// Used to login a User.
    /// </summary>
    /// <param name="email">The email of the user.</param>
    /// <param name="password">The password of the user.</param>
    /// <returns>A bool which indicates the success of the operration an the user.</returns>
    Task<(bool, User?)> LoginAsync(string email, string password);

    /// <summary>
    /// Creates and returns the user. If an error occures, the returned user is <see langword="null"/>.
    /// </summary>
    /// <param name="request">The reuqest from which the user should be created.</param>
    /// <returns>The Task that represents the asynchronous operation, containing the IdentityResult of the operation.</returns>
    Task<(IdentityResult, User?)> CreateUserAsync(RegisterRequest request);

    /// <summary>
    /// Deletes an user account.
    /// </summary>
    /// <param name="userId">The id of the user.</param>
    /// <returns>The Task that represents the asynchronous operation, containing the IdentityResult of the operation.</returns>
    Task<IdentityResult> DeleteUserAsync(Guid userId);

    /// <summary>
    /// Adds the user to the default role.
    /// </summary>
    /// <param name="user">The user to add to the role.</param>
    /// <returns>The Task that represents the asynchronous operation, containing the IdentityResult of the operation.</returns>
    Task<IdentityResult> AddUserToDefaultRoleAsync(User user);

    /// <summary>
    /// Resets the password for a user.
    /// </summary>
    /// <param name="email">The email address of the user.</param>
    /// <param name="code">The reset code.</param>
    /// <param name="newPassword">The new password to set.</param>
    /// <returns>An <see cref="IdentityResult"/> with information about the operation.</returns>
    Task<IdentityResult> ResetPasswordAsync(string email, string code, string newPassword);

    /// <summary>
    /// Confirms the email address of an user.
    /// </summary>
    /// <param name="userId">The id of an user that needs to confirm the email address.</param>
    /// <param name="confirmationCode">The confirmation code.</param>
    /// <returns></returns>
    Task<IdentityResult> ConfirmEmailAsync(string userId, string confirmationCode);

    /// <summary>
    /// Changes the email of an user.
    /// </summary>
    /// <param name="userId">The id of an user that needs to confirm the email address.</param>
    /// <param name="email">The new email address.</param>
    /// <param name="confirmationCode">The confirmation code.</param>
    /// <returns></returns>
    Task<IdentityResult> ChangeEmailAsync(string userId, string email, string confirmationCode);

    /// <summary>
    /// Send a mail to confirm an email address.
    /// </summary>
    /// <param name="user">The user for which the email should be send.</param>
    /// <param name="context">The http context of the request.</param>
    /// <param name="confirmEmailEndpointName">The name of the endpoint to confirm an email adress.</param>
    /// <param name="email">The email adress to confirm.</param>
    /// <param name="isChange"><see langword="true"/> if the is changed, otherwise <see langword="false"/>.</param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException">Is raised, when the confirm endpoint doesnt exists or the confirm link cant be generated.</exception>
    Task SendConfirmationEmailAsync(User user, HttpContext context, string confirmEmailEndpointName, string email, bool isChange = false);

    /// <summary>
    /// Sends a mail with a reset code to reset the password.
    /// </summary>
    /// <param name="email">The email to send the mail to.</param>
    /// <returns>An <see cref="IdentityResult"/> with information about the operation.</returns>
    Task<IdentityResult> SendPasswordForgotEmailAsync(string email);
}
