using ETicaretAPI.Application.Abstractions.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace ETicaretAPI.Infrastructure.Services
{
    public class MailService : IMailService
    {
        readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            await SendMailAsync(new[] { to }, subject, body, isBodyHtml);
        }

        public async Task SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
        {
            MailMessage mail = new();
            mail.IsBodyHtml = isBodyHtml;
            foreach (var to in tos)
                mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            mail.From = new(_configuration["Mail:Username"], "Emirhan E-Ticaret", System.Text.Encoding.UTF8);

            SmtpClient smtp = new();
            smtp.Credentials = new NetworkCredential(_configuration["Mail:Username"], _configuration["Mail:Password"]);
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Host = _configuration["Mail:Host"];
            await smtp.SendMailAsync(mail);
        }

        public async Task SendPasswordResetMailAsync(string to, string userId, string resetToken)
        {
            StringBuilder mail = new();
            string resetLink = $"{_configuration["Url:AngularClientUrl"]}/update-password/{userId}/{resetToken}";

            mail.AppendLine("<div style=\"font-family: Arial, sans-serif; font-size: 16px; color: #333; line-height: 1.5;\">");
            mail.AppendLine("<p>Merhaba,</p>");
            mail.AppendLine("<p>Hesabınız için bir <strong>şifre sıfırlama</strong> talebi aldık.</p>");
            mail.AppendLine("<p>Aşağıdaki butona tıklayarak yeni bir şifre belirleyebilirsiniz:</p>");
            mail.AppendLine($"<p style=\"text-align: center; margin: 20px 0;\"><a href=\"{resetLink}\" target=\"_blank\" style=\"background-color: #007bff; color: white; padding: 10px 20px; text-decoration: none; border-radius: 5px; display: inline-block;\">Şifreyi Sıfırla</a></p>");
            mail.AppendLine("<p>Eğer bu talebi siz yapmadıysanız, bu e-postayı görmezden gelebilirsiniz. Güvenliğiniz için şifrenizi kimseyle paylaşmayın.</p>");
            mail.AppendLine("<hr style=\"margin: 30px 0;\">");
            mail.AppendLine("<p style=\"font-size: 12px; color: #777;\">Bu e-posta Emirhan E-Ticaret sistemi tarafından otomatik olarak gönderilmiştir.</p>");
            mail.AppendLine("</div>");

            await SendMailAsync(to, "Şifre Yenileme Talebi", mail.ToString());
        }

        public async Task SendCompletedOrderMailAsync(string to, string orderCode, DateTime orderDate, string userName)
        {
            string mail = $"Sayın {userName} <br>" +
                $"{orderDate:dd.MM.yyyy} tarihinde vermiş olduğunuz {orderCode} sipariş numaralı siparişiniz hazırlanmıştır ve kargoya verilmiştir.";

            await SendMailAsync(to, $"{orderCode} Numaralı Siparişiniz Hakkında", mail);
        }

    }
}
