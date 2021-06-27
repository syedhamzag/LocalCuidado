using AwesomeCare.DataTransferObject.Validations;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using AwesomeCare.DataTransferObject.DTOs.ClientComplain;

namespace AwesomeCare.DataTransferObject.DTOs.ClientComplainRegister
{
    public class PostComplainRegister
    {
        public PostComplainRegister()
        {
            OfficerToAct = new List<PostComplainOfficerToAct>();
            StaffName = new List<PostComplainStaffName>();
        }
        public string Reference { get; set; }
        public int ComplainId { get; set; }
        public int ClientId { get; set; }
        public string LINK { get; set; }
        public string IRFNUMBER { get; set; }
        public DateTime INCIDENTDATE { get; set; }
        public DateTime DATERECIEVED { get; set; }
        public DateTime DATEOFACKNOWLEDGEMENT { get; set; }
        public string SOURCEOFCOMPLAINTS { get; set; }
        public string COMPLAINANTCONTACT { get; set; }
        public string CONCERNSRAISED { get; set; }
        public DateTime DUEDATE { get; set; }
        public string LETTERTOSTAFF { get; set; }
        public string INVESTIGATIONOUTCOME { get; set; }
        public string ACTIONTAKEN { get; set; }
        public string FINALRESPONSETOFAMILY { get; set; }
        public string ROOTCAUSE { get; set; }
        public string REMARK { get; set; }
        public int StatusId { get; set; }
        public string EvidenceFilePath { get; set; }

        public List<PostComplainOfficerToAct> OfficerToAct { get; set; }
        public List<PostComplainStaffName> StaffName { get; set; }
    }
}
