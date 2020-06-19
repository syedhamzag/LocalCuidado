using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.IdentityServer.ViewModels
{
    public class IdentityServer4ClientViewModel
    {
        public IdentityServer4ClientViewModel()
        {
            AllowedScopes = new List<string>();
            // IdentityResources = new List<string>();
            IdentityResourceListItems = new List<SelectListItem>();
            ProtectedResourcesListItems = new List<SelectListItem>();
            ClientTypes = new List<SelectListItem>() 
            { 
                new SelectListItem("Web App", "WebApp") ,
                new SelectListItem("SPA", "SPA") ,
                new SelectListItem("SPA.Swagger", "SPA (Swagger)") 
            };
        }
        [Display(Name = "Client Type")]
        [Required]
        public string ClientType { get; set; }
        [Display(Name = "Client Id")]
        [Required]
        public string ClientId { get; set; } = Guid.NewGuid().ToString("N");
        [Display(Name = "Display Name")]
        [Required]
        public string DisplayName { get; set; }
        [Display(Name = "Display Url")]
        [Required]
        [DataType(DataType.Url)]
        public string DisplayUrl { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Logo Url")]
        public string LogoUrl { get; set; }
        [Display(Name = "Require Consent")]
        public bool RequireConsent { get; set; }
        [Display(Name = "Callback Url")]
        [Required]
        [DataType(DataType.Url)]
        public string CallBackUrl { get; set; }
        [Display(Name = "Shared Secret")]
        [Required]
        public string SharedSecret { get; set; }
        //[Display(Name = "Identity Resources")]
        //public List<string> IdentityResources { get; set; }
        //[Display(Name = "Protected Resources")]
        [Required]
        public List<string> AllowedScopes { get; set; }


        public List<SelectListItem> IdentityResourceListItems { get; set; }
        public List<SelectListItem> ProtectedResourcesListItems { get; set; }
        public List<SelectListItem> ClientTypes { get; set; }

    }
}
