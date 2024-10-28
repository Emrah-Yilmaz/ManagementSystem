namespace ManagementSystem.Notification.Services
{
    using System.Net;
    using System.Net.Mail;
    using CommonLibrary.Options.Email;
    using Microsoft.Extensions.Options;

    public class EmailService : IEmailService
    {
        private readonly EmailOptions _emailOptions;

        public EmailService(IOptions<EmailOptions> emailSettings)
        {
            _emailOptions = emailSettings.Value;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var smtpClient = new SmtpClient(_emailOptions.SmtpServer)
            {
                Port = _emailOptions.SmtpPort,
                Credentials = new NetworkCredential(_emailOptions.SenderEmail, _emailOptions.SenderPassword),
                EnableSsl = _emailOptions.UseSsl
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailOptions.SenderEmail, _emailOptions.SenderName),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            mailMessage.To.Add(toEmail);
            try
            {
                await smtpClient.SendMailAsync(mailMessage);

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }

}
