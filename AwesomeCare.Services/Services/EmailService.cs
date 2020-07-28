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
        public EmailService(string key)
        {
            apiKey = key;
        }
        public async Task SendAsync(string senderEmail,List<string> recipients,string subject,string htmlContent, bool showAllRecipients = false,string senderName = "MyCuidado")
        {
          
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(senderEmail, senderName);
            List<EmailAddress> tos = recipients.Select(e => new EmailAddress(e)).ToList();
           
            var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, tos, subject, "", htmlContent, showAllRecipients);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
