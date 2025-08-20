using Bookworm.Models;
using Bookworm.Services;
using MailKit.Net.Smtp;
using MimeKit;

namespace Bookworm.ServicesImpl
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfig;

        public EmailSender(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }

        public async Task SendEmailWithAttachmentAsync(Message message, byte[] pdfAttachment, string attachmentFileName)
        {
            var emailMessage = CreateEmailMessage(message, pdfAttachment, attachmentFileName);
            await SendAsync(emailMessage);
        }

        private MimeMessage CreateEmailMessage(Message message, byte[] pdfAttachment, string attachmentFileName)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Bookworm Store", _emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;

            var bodyBuilder = new BodyBuilder { HtmlBody = $"<h3 style='color:black;'>{message.Content}</h3>" };
            bodyBuilder.Attachments.Add(attachmentFileName, pdfAttachment, ContentType.Parse("application/pdf"));
            emailMessage.Body = bodyBuilder.ToMessageBody();

            return emailMessage;
        }

        private async Task SendAsync(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(_emailConfig.Username, _emailConfig.Password);
                    await client.SendAsync(mailMessage);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error sending email: {ex.Message}");
                    throw;
                }
                finally
                {
                    await client.DisconnectAsync(true);
                }
            }
        }
    }
}