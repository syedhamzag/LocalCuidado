using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs
{
    public class PostReportStaff
    {
        [Display(Name = "Reporting Staff")]
        public int? ReportingStaffId { get; set; }
        [Required]
        [Display(Name = "Client Involved")]
        public int ClientId { get; set; }
        [Required]
        [Display(Name = "Staff Involved")]
        public int StaffInvolvedId { get; set; }
        /// <summary>
        /// From Base Record
        /// </summary>
        [Required]
        [Display(Name = "Incident Type")]
        public int IncidentType { get; set; }
        [Required]
        [Display(Name = "Incident Details")]
        public string IncidentDetails { get; set; }
        [Display(Name = "Action Taken")]
        public string ActionTaken { get; set; }
        public string Witness { get; set; }
       
        public string Attachment { get; set; }

    }
}
