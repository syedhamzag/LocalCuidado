using AwesomeCare.DataTransferObject.DTOs.CompanyContact;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.Company
{
  public  class GetCompanyDto
    {
        public GetCompanyDto()
        {
           // Contacts = new HashSet<GetCompanyContactDto>();
        }
       
        public string Company { get; set; }        
        public string LogoUrl { get; set; }       
        public string Address { get; set; }       
        public string Email { get; set; }       
        public string Website { get; set; }
        public string Language { get; set; }

        public   IQueryable<GetCompanyContactDto> Contacts { get; set; }
    }
}
