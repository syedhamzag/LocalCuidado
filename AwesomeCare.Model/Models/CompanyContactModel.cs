using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
   public class CompanyContactModel
    {
        public int CompanyContactId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public int CompanyId { get; set; }
        public virtual CompanyModel Company { get; set; }
    }
}
