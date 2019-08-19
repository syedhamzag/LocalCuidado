using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.Model.Models
{
   public class Client
    {
        public int ClientId { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string About { get; set; }
        public string Hobbies { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Keyworker { get; set; }
        public string IdNumber { get; set; }
        public int GenderId { get; set; }
        public int NumberOfCalls { get; set; }
        public int AreaCodeId { get; set; }
        public int TeritoryId { get; set; }
        public int ServiceId { get; set; }
        public string ProvisionVenue { get; set; }
        public string PostCode { get; set; }
        public decimal Rate { get; set; }
        public string TeamLeader { get; set; }
        public string DateOfBirth { get; set; }
        public string Telephone { get; set; }
        public int LanguageId { get; set; }
        public string KeySafe { get; set; }
        public int ChoiceOfStaffId { get; set; }
        public int StatusId { get; set; }
        public int CapacityId { get; set; }
        public string ProviderReference { get; set; }
        public int NumberOfStaff { get; set; }
    }
}
