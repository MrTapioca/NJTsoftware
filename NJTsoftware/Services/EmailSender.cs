using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using NJTsoftware.Interfaces;
using NJTsoftware.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NJTsoftware.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;

        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new
                MailboxAddress(_emailSettings.SenderName, _emailSettings.Sender));
            mimeMessage.To.Add(new MailboxAddress(email));
            mimeMessage.Subject = subject;
            mimeMessage.Body = new TextPart()
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                await client.ConnectAsync(_emailSettings.MailServer,
                    _emailSettings.MailPort, MailKit.Security.SecureSocketOptions.Auto);
                await client.AuthenticateAsync(_emailSettings.Sender, _emailSettings.Password);

                await client.SendAsync(mimeMessage);
                await client.DisconnectAsync(true);
            }
        }
    }
}