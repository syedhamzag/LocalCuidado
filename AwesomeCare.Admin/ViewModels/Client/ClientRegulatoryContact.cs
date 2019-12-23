
using AwesomeCare.DataTransferObject.Validations;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.ViewModels.Client
{
    public class ClientRegulatoryContact
    {
        public int ClientId { get; set; }
        public int BaseRecordItemId { get; set; }
        public string RegulatoryContact { get; set; }
        public bool IsSelected { get; set; }
        public DateTime DatePerformed { get; set; }
        public DateTime DueDate { get; set; }

        [DataType(DataType.Upload)]
        [MaxFileSize(Lenght = 1)]
       // [AllowedExtensions(new string[] { ".pdf" })]
        public IFormFile EvidenceFile { get; set; }
        public string Evidence { get; set; }
    }
}
