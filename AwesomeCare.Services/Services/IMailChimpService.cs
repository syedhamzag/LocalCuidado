using Mandrill.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeCare.Services.Services
{
    public interface IMailChimpService
    {
        Task<List<EmailResult>> SendAsync(string subject, string content, bool ishtml, List<AwesomeCare.DataTransferObject.Models.MailChimp.Recipient> recipients);
        Task<List<EmailResult>> SendAsync(string subject, string content, bool ishtml, List<AwesomeCare.DataTransferObject.Models.MailChimp.Recipient> recipients,List<IFormFile> attachments);
    }
}
