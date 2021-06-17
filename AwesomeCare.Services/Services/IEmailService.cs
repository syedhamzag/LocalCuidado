using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeCare.Services.Services
{
  public interface IEmailService
    {
        Task SendAsync(List<string> recipients, string subject, string htmlContent, bool showAllRecipients = false);
        Task SendAsync(List<string> recipients, string subject, string htmlContent, byte[] attachment, string filename, string contentType, bool showAllRecipients = false);
        Task SendEmail(System.Net.Mail.Attachment attachment, string subject, string body, string sender, string password, string recipient, string Smtp);
    }
}
