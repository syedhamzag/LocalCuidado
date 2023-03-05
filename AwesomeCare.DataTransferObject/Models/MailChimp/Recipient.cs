using AwesomeCare.DataTransferObject.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeCare.DataTransferObject.Models.MailChimp
{
    public class Recipient
    {
        /// <summary>
        /// the email address of the recipient
        /// </summary>       
        public string Email { get; set; }

        /// <summary>
        /// the optional display name to use for the recipient
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// the header type to use for the recipient, defaults to "to" if not provided Possible values: "to", "cc", or "bcc".
        /// </summary>
        public EmailTypeEnum Type { get; set; }

    }
}
