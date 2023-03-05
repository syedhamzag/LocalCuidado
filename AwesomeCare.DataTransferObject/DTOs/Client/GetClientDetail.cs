using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.Client
{
   public class GetClientDetail
    {
        public int ClientId { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Surname { get; set; }
        public string FullName { get; set; }
        public string IdNumber { get; set; }
        public string DateOfBirth { get; set; }
        // public string Gender { get; set; }
        public string Email { get; set; }
    }
}
