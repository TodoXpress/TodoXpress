using System.Net.Mail;
using Microsoft.AspNetCore.Identity;
using TodoXpress.Api.Identity.Entities;

namespace TodoXpress.Api.Identity.Services;

internal sealed class EmailService : SmtpClient, IEmailSender<User>
{
    /// <summary>
    /// Gets or sets the email from wich the mail should be send.
    /// </summary>
    public required string FromAddress { get; init; }

    /// <summary>
    /// Sends an email with a confirmation link.
    /// </summary>
    /// <param name="user">The user for which the mail should be send.</param>
    /// <param name="email">The email address to send the mail to.</param>
    /// <param name="confirmationLink">the link for the confirmation.</param>
    /// <returns>A Task that represents the asynchronus operation.</returns>
    public async Task SendConfirmationLinkAsync(User user, string email, string confirmationLink)
    {
        var message = new MailMessage(FromAddress, email)
        {
            Subject = "Bestätigen Sie Ihre E-Mail-Adresse",
            Body = $"Bitte klicken Sie <a href=\"{confirmationLink}\">hier</a> um Ihre E-Mail-Adresse zu bestätigen",
            IsBodyHtml = true
        };
        await this.SendMailAsync(message);
    }

    /// <summary>
    /// Sends an email with a code to reset the password.
    /// </summary>
    /// <param name="user">The user for which the mail should be send.</param>
    /// <param name="email">The email address to send the mail to.</param>
    /// <param name="resetCode">The code to reset the password.</param>
    /// <returns>A Task that represents the asynchronus operation.</returns>
    public async Task SendPasswordResetCodeAsync(User user, string email, string resetCode)
    {
         var message = new MailMessage(FromAddress, email)
        {
            Subject = "Passwort-Reset-Code",
            Body = $"Ihr Code zum Zurücksetzen des Passworts lautet:\r\n{resetCode}",
            IsBodyHtml = true
        };
        // await Task.Delay(0);
        await this.SendMailAsync(message);
    }

    /// <summary>
    /// Sends an email with a link to reset the password.
    /// </summary>
    /// <param name="user">The user for which the mail should be send.</param>
    /// <param name="email">The email address to send the mail to.</param>
    /// <param name="resetLink">The link to reset the password.</param>
    /// <returns>A Task that represents the asynchronus operation.</returns>
    public async Task SendPasswordResetLinkAsync(User user, string email, string resetLink)
    {
        var message = new MailMessage(FromAddress, email)
        {
            Subject = "Link zum Zurücksetzen des Passworts",
            Body = $"Bitte klicken Sie <a href=\"{resetLink}\">hier</a>, um Ihr Passwort zurückzusetzen.",
            IsBodyHtml = true
        };
        // await Task.Delay(0);
        await this.SendMailAsync(message);
    }
}
