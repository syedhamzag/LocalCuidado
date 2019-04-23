using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.CompanyContact
{
   public class GetCompanyContactDto
    {
        public int CompanyContactId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public int CompanyId { get; set; }
    }
}
