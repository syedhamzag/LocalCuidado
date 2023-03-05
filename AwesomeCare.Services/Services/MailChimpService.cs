using Mandrill;
using Mandrill.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeCare.Services.Services
{
    public class MailChimpService: IMailChimpService
    {
        private readonly IMandrillApi mandrill;
        private readonly IConfiguration configuration;
        private Mandrill.Models.EmailMessage EmailMessage;
        public MailChimpService(IMandrillApi mandrill, IConfiguration configuration)
        {
            this.mandrill = mandrill;
            this.configuration = configuration;

            EmailMessage = new Mandrill.Models.EmailMessage();
            EmailMessage.FromEmail = configuration["MailChimpSettings:FromEmail"];
            EmailMessage.FromName = configuration["MailChimpSettings:FromName"];
            EmailMessage.SubAccount = configuration["MailChimpSettings:SubAccount"];
        }
        public async Task<List<EmailResult>> SendAsync(string subject,string content,bool ishtml,List<AwesomeCare.DataTransferObject.Models.MailChimp.Recipient> recipients)
        {

            EmailMessage.Subject = subject;
            EmailMessage.To = recipients.Select(r=> new EmailAddress(r.Email,r.Name,r.Type.ToString())).ToList();
            if (ishtml)
            {
                EmailMessage.Html = content;
            }
            else
            {
                EmailMessage.Text = content;
            }
           EmailMessage.PreserveRecipients = true;

            var sendMessage = new Mandrill.Requests.Messages.SendMessageRequest(EmailMessage);
            var result = await mandrill.SendMessage(sendMessage);

            return result;
        }

        public async Task<List<EmailResult>> SendAsync(string subject, string content, bool ishtml, List<DataTransferObject.Models.MailChimp.Recipient> recipients, List<IFormFile> attachments)
        {
            EmailMessage.Subject = subject;
            EmailMessage.To = recipients.Select(r => new EmailAddress(r.Email, r.Name, r.Type.ToString())).ToList();
            if (ishtml)
            {
                EmailMessage.Html = content;
            }
            else
            {
                EmailMessage.Text = content;
            }
            EmailMessage.PreserveRecipients = true;

            EmailMessage.Attachments = attachments.Select(a => new EmailAttachment
            {
                Base64 = true,
                Content = a.ToBase64(),
                Name = a.FileName,
                Type = a.ContentType

            }).ToList();


            var sendMessage = new Mandrill.Requests.Messages.SendMessageRequest(EmailMessage);
            var result = await mandrill.SendMessage(sendMessage);

            return result;
        }
    }
}
