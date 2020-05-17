using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.IdentityServer.ViewModels
{
    public class ApiResourceViewModel
    {
        public ApiResourceViewModel()
        {
            ApiScopeClaims = new List<string>();
           // ApiScopeClaimSelectList = new List<SelectListItem>();
        }
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Display Name")]
        public string DisplayName { get; set; }
        [Required]
        public string Description { get; set; }
        public List<string> ApiScopeClaims { get; set; }

       // public List<SelectListItem> ApiScopeClaimSelectList { get; set; }
    }
}
