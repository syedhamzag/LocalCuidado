using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeCare.DataTransferObject.DTOs.StaffReference
{
    public class PostStaffReference
    {
        public int StaffReferenceId { get; set; }
        public string Reference { get; set; }
        public int StaffId { get; set; }
        public DateTime Date { get; set; }
        public int ApplicantRole { get; set; }
        public int DateofEmployement { get; set; }
        public string DateofExit { get; set; }
        public string RehireStaff { get; set; }
        public string Relationship { get; set; }
        public int TeamWork { get; set; }
        public int Integrity { get; set; }
        public int Knowledgeable { get; set; }
        public int WorkUnderPressure { get; set; }
        public int Caring { get; set; }
        public int PreviousExperience { get; set; }
        public string RefreeName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public string Attachment { get; set; }
        public int ConfirmedBy { get; set; }
        public int Status { get; set; }
    }
}
