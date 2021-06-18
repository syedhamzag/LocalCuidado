using AwesomeCare.DataTransferObject.Validations;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.ClientComplainRegister
{
    public class PostComplainRegister
    {
        public PostComplainRegister()
        {

        }
        public string Reference { get; set; }
        public int ComplainId { get; set; }
        public int ClientId { get; set; }
        [Required]
        [MaxLength(255)]
        public string LINK { get; set; }
        [Required]
        [MaxLength(50)]
        public string IRFNUMBER { get; set; }
        [Required]
        public DateTime INCIDENTDATE { get; set; }
        [Required]
        public DateTime DATERECIEVED { get; set; }
        public DateTime DATEOFACKNOWLEDGEMENT { get; set; }
        [Required]
        [Display(Name = "OFFICERTOACT")]
        public int OFFICERTOACTId { get; set; }
        [Required]
        [MaxLength(255)]
        public string SOURCEOFCOMPLAINTS { get; set; }

        [Required]
        [MaxLength(50)]
        public string COMPLAINANTCONTACT { get; set; }
        [Required]
        [Display(Name = "STAFFINVOLVED")]
        public int STAFFId { get; set; }
        [Required]
        [MaxLength(255)]
        public string CONCERNSRAISED { get; set; }
        [Required]
        public DateTime DUEDATE { get; set; }
        [Required]
        [MaxLength(255)]
        public string LETTERTOSTAFF { get; set; }
        [Required]
        [MaxLength(255)]
        public string INVESTIGATIONOUTCOME { get; set; }
        [Required]
        [MaxLength(50)]
        public string ACTIONTAKEN { get; set; }
        [Required]
        [MaxLength(255)]
        public string FINALRESPONSETOFAMILY { get; set; }
        [Required]
        [MaxLength(50)]
        public string ROOTCAUSE { get; set; }
        [MaxLength(255)]
        public string REMARK { get; set; }
        [Required]
        [Display(Name = "Status")]
        public int StatusId { get; set; }
        [Required]
        public string EvidenceFilePath { get; set; }
    }
}
