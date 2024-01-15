﻿using System.Net.Mail;
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
            Body = $"Bitte bestätigen Sie Ihre E-Mail-Adresse, indem Sie auf den folgenden Link klicken: {confirmationLink}"
        };
        // await Task.Delay(0);
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
            Body = $"Ihr Code zum Zurücksetzen des Passworts lautet: {resetCode}"
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
            Body = $"Bitte klicken Sie auf den folgenden Link, um Ihr Passwort zurückzusetzen: {resetLink}"
        };
        // await Task.Delay(0);
        await this.SendMailAsync(message);
    }
}