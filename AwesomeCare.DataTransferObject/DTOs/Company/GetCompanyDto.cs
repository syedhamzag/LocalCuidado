using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.Company
{
  public  class GetCompanyDto
    {
       
        public string Company { get; set; }        
        public string LogoUrl { get; set; }       
        public string Address { get; set; }       
        public string Email { get; set; }       
        public string Website { get; set; }
        public string Language { get; set; }
    }
}
