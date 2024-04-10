using LostAnimals.Services.Settings;
using Microsoft.AspNetCore.Identity.UI.Services;
using LostAnimals.Services.Logger;
using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using MimeKit;

namespace LostAnimals.Services.EmailSender;

public class EmailSenderService : IEmailSenderService
{
    private readonly IAppLogger logger;

    public EmailSenderSettings Settings { get; private set; }

    public EmailSenderService(IAppLogger logger, EmailSenderSettings settings)
    {
        this.logger = logger;
        this.Settings = settings;
    }

    public async Task SendEmail(Message message)
    {
        var emailMessage = CreateEmailMessage(message);
        
        await SendAsync(emailMessage);
    }

    private MimeMessage CreateEmailMessage(Message message)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress("admin", Settings.From));
        emailMessage.To.AddRange(message.To);
        emailMessage.Subject = message.Subject;
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };

        return emailMessage;
    }

    private async Task SendAsync(MimeMessage mailMessage)
    {
        using (var client = new SmtpClient())
        {
            try
            {
                await client.ConnectAsync(Settings.SmtpServer, Settings.Port, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                await client.AuthenticateAsync(Settings.UserName, Settings.Password);

                await client.SendAsync(mailMessage);
            }
            catch (Exception e)
            {
                logger.Error("The error occures while sending the message. ", e);
            }
            finally
            {
                await client.DisconnectAsync(true);
                client.Dispose();
            }
        }
    }
}
