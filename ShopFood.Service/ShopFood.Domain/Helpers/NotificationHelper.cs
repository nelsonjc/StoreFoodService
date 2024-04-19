using ShopFood.Domain.DTOs.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ShopFood.Domain.Helpers
{
    public static class NotificationHelper
    {
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
                throw new Exception("No hay destinatarios para enviar el e-mail");

            mailSend.Destinatary.ToList().ForEach(x => mail.To.Add(x));

            if (mailSend.Attachments != null && mailSend.Attachments.Count() > 0)
            {
                mailSend.Attachments.ToList().ForEach(x => mail.Attachments.Add(x));
            }

            SmtpServer.Port = mailSend.Configuration.Port;
            SmtpServer.UseDefaultCredentials = mailSend.Configuration.DefaultCredentials;
            SmtpServer.Credentials = new System.Net.NetworkCredential(mailSend.Configuration.Remittance, mailSend.Configuration.Password);
            SmtpServer.EnableSsl = mailSend.Configuration.EnableSSL;

            SmtpServer.Send(mail);
            SmtpServer.Dispose();
            await Task.CompletedTask;
        }
    }
}
