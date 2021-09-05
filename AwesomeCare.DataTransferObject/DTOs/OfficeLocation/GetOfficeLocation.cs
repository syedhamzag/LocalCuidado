using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.OfficeLocation
{
   public class GetOfficeLocation
    {
        public int OfficeLocationId { get; set; }
        public string UniqueId { get; set; }
        public string Address { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        [DisplayName("Contact Person")]
        public string ContactPersonFullName { get; set; }
        [DisplayName("Contact Person Email")]
        public string ContactPersonEmail { get; set; }
        [DisplayName("Contact Person Telephone")]
        public string ContactPersonPhoneNumber { get; set; }
    }
}
