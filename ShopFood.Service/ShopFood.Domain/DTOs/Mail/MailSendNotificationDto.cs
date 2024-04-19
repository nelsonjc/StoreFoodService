using System.Net.Mail;

namespace ShopFood.Domain.DTOs.Mail
{
    public class MailSendNotificationDto
    {
        public MailConfigDto Configuration { get; set; }
        public string HTMLBody { get; set; }
        public string[] Destinatary { get; set; }
        public string Subject { get; set; }
        public List<Attachment> Attachments { get; set; }
    }

    public class MailConfigDto
    {
        public string Remittance { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool DefaultCredentials { get; set; }
        public bool EnableSSL { get; set; }
        public bool IsBodyHtml { get; set; }
    }
}
