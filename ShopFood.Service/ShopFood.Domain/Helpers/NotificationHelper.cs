using ShopFood.Domain.DTOs.Mail;
using ShopFood.Domain.Variables;
using System.Net;
using System.Net.Mail;

namespace ShopFood.Domain.Helpers
{
    /// <summary>
    /// Class to notifications
    /// </summary>
    public static class NotificationHelper
    {
        /// <summary>
        /// Method to notification with mail
        /// </summary>
        /// <param name="mailSend">Parameter to send data mail type of MailSendNotificationDto</param>
        public static async Task SendMail(MailSendNotificationDto mailSend)
        {
            SmtpClient SmtpServer = new(mailSend.Configuration.Host);
            var mail = new MailMessage
            {
                From = new MailAddress(mailSend.Configuration.Remittance, mailSend.Configuration.DisplayName),
                Subject = mailSend.Subject,
                Body = mailSend.HTMLBody,
                IsBodyHtml = mailSend.Configuration.IsBodyHtml
            };

            if (mailSend.Destinatary is null || mailSend.Destinatary.Length == 0)
                throw new Exception(ServiceMessages.ERROR_MAIL_DESTINY);

            mailSend.Destinatary.ToList().ForEach(x => mail.To.Add(x));

            if (mailSend.Attachments != null && mailSend.Attachments.Count > 0)
                mailSend.Attachments.ToList().ForEach(x => mail.Attachments.Add(x));

            SmtpServer.Port = mailSend.Configuration.Port;
            SmtpServer.UseDefaultCredentials = mailSend.Configuration.DefaultCredentials;
            SmtpServer.Credentials = new NetworkCredential(mailSend.Configuration.Remittance, mailSend.Configuration.Password);
            SmtpServer.EnableSsl = mailSend.Configuration.EnableSSL;

            SmtpServer.Send(mail);
            SmtpServer.Dispose();
            await Task.CompletedTask;
        }
    }
}
