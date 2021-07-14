using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.PersonalDetail.Personal
{
    public class PostPersonal
    {
        public int PersonalId { get; set; }
        public int PersonalDetailId { get; set; }
        public int FullName { get; set; }
        public int PreferName { get; set; }
        public int PreferLanguage { get; set; }
        public int Gender { get; set; }
        public int DateOfBirth { get; set; }
        public int Religion { get; set; }
        public int Address { get; set; }
        public int PostCode { get; set; }
        public int Nationality { get; set; }
        public int Telephone { get; set; }
        public int Smoking { get; set; }
        public int AccessCode { get; set; }
        public int PreferGender { get; set; }
        public int DNR { get; set; }
    }
}
