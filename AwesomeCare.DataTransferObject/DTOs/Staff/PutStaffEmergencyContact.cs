﻿using System.ComponentModel.DataAnnotations;

namespace AwesomeCare.DataTransferObject.DTOs.Staff
{
   public class PutStaffEmergencyContact
    {
       
        [Required]
        public int StaffEmergencyContactId { get; set; }
        [Required]
        public int StaffPersonalInfoId { get; set; }
        [Required]
        public string ContactName { get; set; }
        [Required]
        public string Telephone { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Relationship { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
