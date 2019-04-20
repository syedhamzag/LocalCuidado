using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
   public class CompanyModel
    {
        public CompanyModel()
        {
            CompanyContacts = new HashSet<CompanyContactModel>();
        }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string LogoUrl { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Language { get; set; }

        public virtual ICollection<CompanyContactModel> CompanyContacts { get; set; }
    }
}
