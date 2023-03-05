using Mandrill;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeCare.Services.Services
{
    public class EmailService : IEmailService
    {
        private string apiKey { get; set; }
        private string emailSender { get; set; }
        public string senderEmail { get; set; }

        private readonly ILogger<EmailService> logger;

        public EmailService(string key, string senderEmail, string emailSender, ILogger<EmailService> logger)
        {
            apiKey = key;
            this.senderEmail = senderEmail;
            this.logger = logger;
            this.emailSender = emailSender;
        }
        public async Task SendAsync(List<string> recipients, string subject, string htmlContent, bool showAllRecipients = false)
        {
            try
            {
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress(senderEmail, emailSender);
                List<EmailAddress> tos = recipients.Select(e => new EmailAddress(e)).ToList();

                var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, tos, subject, "", htmlContent, showAllRecipients);
                var response = await client.SendEmailAsync(msg);
                var content = await response.Body.ReadAsStringAsync();

                logger.LogInformation(content);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "EmailService", null);
            }
        }

        public async Task SendAsync(List<string> recipients, string subject, string htmlContent, byte[] attachment, string filename, string contentType, bool showAllRecipients = false)
        {
            try
            {
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress(senderEmail, emailSender);
                List<EmailAddress> tos = recipients.Select(e => new EmailAddress(e)).ToList();

                var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, tos, subject, "", htmlContent, showAllRecipients);

                msg.Attachments = new List<SendGrid.Helpers.Mail.Attachment>
                {
                    new SendGrid.Helpers.Mail.Attachment()
                    {
                         Content =Convert.ToBase64String(attachment),
                         ContentId = filename,
                         Disposition ="attachment",
                         Filename = filename,
                         Type = contentType
                    }
                };
                var response = await client.SendEmailAsync(msg);
                var content = await response.Body.ReadAsStringAsync();

                logger.LogInformation(content);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "EmailService", null);
            }
        }
        public async Task SendEmail(System.Net.Mail.Attachment attachment, string subject, string body, string sender, string password, string recipient, string Smtp)
        {
            try
            {
                SmtpClient smtp = new SmtpClient();
                smtp.EnableSsl = true;
                smtp.Host = "smtp.mycuidado.co.uk";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = new NetworkCredential(sender, password);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                MailMessage msg = new MailMessage(sender, recipient);
                msg.Subject = subject;
                msg.Body = body;
                if (attachment != null)
                    msg.Attachments.Add(attachment);
                msg.IsBodyHtml = true;
                smtp.Send(msg);

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, "Email Not Sent", null);
            }

        }

    
    }
}
