using System.Net.Mail;
using Microsoft.AspNetCore.Identity;
using TodoXpress.Api.Identity.Entities;

namespace TodoXpress.Api.Identity.Services;

internal sealed class EmailService(string fromAddress) : SmtpClient, IEmailSender<User>
{
    async Task IEmailSender<User>.SendConfirmationLinkAsync(User user, string email, string confirmationLink)
    {
        var message = new MailMessage(fromAddress, email)
        {
            Subject = "Bestätigen Sie Ihre E-Mail-Adresse",
            Body = $"Bitte bestätigen Sie Ihre E-Mail-Adresse, indem Sie auf den folgenden Link klicken: {confirmationLink}"
        };
        await Task.Delay(0);
        // await this.SendMailAsync(message);
    }

    async Task IEmailSender<User>.SendPasswordResetCodeAsync(User user, string email, string resetCode)
    {
         var message = new MailMessage(fromAddress, email)
        {
            Subject = "Passwort-Reset-Code",
            Body = $"Ihr Code zum Zurücksetzen des Passworts lautet: {resetCode}"
        };
        await Task.Delay(0);
        // await this.SendMailAsync(message);
    }

    async Task IEmailSender<User>.SendPasswordResetLinkAsync(User user, string email, string resetLink)
    {
        var message = new MailMessage(fromAddress, email)
        {
            Subject = "Link zum Zurücksetzen des Passworts",
            Body = $"Bitte klicken Sie auf den folgenden Link, um Ihr Passwort zurückzusetzen: {resetLink}"
        };
        await Task.Delay(0);
        // await this.SendMailAsync(message);
    }
}
