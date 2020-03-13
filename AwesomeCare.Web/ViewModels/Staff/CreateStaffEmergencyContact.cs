using System.ComponentModel.DataAnnotations;

namespace AwesomeCare.Web.ViewModels.Staff
{
    public class CreateStaffEmergencyContact
    {
        
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
