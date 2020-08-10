using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeCare.Services.Services
{
   public class EmailService: IEmailService
    {
        private string apiKey { get; set; }
        private string emailSender { get; set; }
        public string senderEmail { get; set; }

        private readonly ILogger<EmailService> logger;

        public EmailService(string key,string senderEmail,string emailSender,ILogger<EmailService> logger)
        {
            apiKey = key;
            this.senderEmail = senderEmail;
            this.logger = logger;
            this.emailSender = emailSender;
        }
        public async Task SendAsync(List<string> recipients,string subject,string htmlContent, bool showAllRecipients = false)
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
    }
}
