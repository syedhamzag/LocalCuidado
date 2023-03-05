using System.ComponentModel.DataAnnotations;

namespace AwesomeCare.IdentityServer.ViewModels
{
    public class IdentityResourceViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string DisplayName { get; set; }


        public string Description { get; set; }
    }
}
