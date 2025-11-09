using GeekGarden.API.Models;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace GeekGarden.API.Services
{
    public class EmailService
    {
        private readonly SmtpSettings _smtpSettings;

        public EmailService(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }

        public async Task<bool> SendEmailAsync(string to, string subject, string body, string? from = null)
        {
            try
            {
                using (var smtp = new SmtpClient())
                {
                    smtp.Host = _smtpSettings.Host;
                    smtp.Port = _smtpSettings.Port;
                    smtp.EnableSsl = _smtpSettings.EnableSsl;
                    smtp.Credentials = new NetworkCredential(_smtpSettings.User, _smtpSettings.Password);

                    var mail = new MailMessage
                    {
                        From = new MailAddress(from ?? _smtpSettings.User),
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = true
                    };

                    mail.To.Add(to);

                    await smtp.SendMailAsync(mail);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Email send failed: {ex.Message}");
                return false;
            }
        }
    }
}
