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
                new SelectListItem("SPA.Swagger", "SPA (Swagger)"),
                new SelectListItem("Blazor", "Blazor App")
            };

            GrantTypes = new List<SelectListItem>()
            {
                new SelectListItem("password", "password") ,
                new SelectListItem("authorization_code", "authorization_code") ,
                new SelectListItem("client_credentials", "client_credentials") ,
                new SelectListItem("implicit", "implicit") ,
                new SelectListItem("urn:ietf:params:oauth:grant-type:saml2-bearer", "urn:ietf:params:oauth:grant-type:saml2-bearer") ,
                new SelectListItem("urn:ietf:params:oauth:grant-type:saml2-bearer", "urn:ietf:params:oauth:grant-type:saml2-bearer") ,
                new SelectListItem("urn:ietf:params:oauth:grant-type:device_code", "urn:ietf:params:oauth:grant-type:device_code") ,
                new SelectListItem("urn:ietf:params:oauth:grant-type:token-exchange", "urn:ietf:params:oauth:grant-type:token-exchange")
            };

        }
        [Display(Name = "Grant Type")]
        [Required]
        public string GrantType { get; set; }
        [Display(Name = "Client Id")]
        [Required]
        public string ClientId { get; set; } = Guid.NewGuid().ToString("N");
        [Display(Name = "Display Name")]
        [Required]
        public string DisplayName { get; set; }
        [Display(Name = "Url")]
        [Required]
        // [DataType(DataType.Url)]
        public string Url { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Post Logout Url")]
        // [DataType(DataType.Url)]
        public string PostlogoutUrl { get; set; }
        [Display(Name = "Require Consent")]
        public bool RequireConsent { get; set; }
        //  [Display(Name = "Callback Url")]
        //[Required]
        //[DataType(DataType.Url)]
        //public string CallBackUrl { get; set; }
        [Display(Name = "Shared Secret")]
        [Required]
        public string SharedSecret { get; set; }
        //[Display(Name = "Identity Resources")]
        //public List<string> IdentityResources { get; set; }
        //[Display(Name = "Protected Resources")]
        [Required]
        public List<string> AllowedScopes { get; set; }

        [Display(Name = "Is Public Client")]
        public bool IsPublicClient { get; set; }

        [Display(Name = "Allow Offline Access")]
        public bool AllowOfflineAccess { get; set; }

        [Display(Name = "Require PKCE")]
        public bool RequirePkce { get; set; }

        [Display(Name = "CORS Origin Uris (comma separated)")]
        public string CorsOrigins { get; set; }

        [Display(Name = "Allow AccessTokens Via Browser")]
        public bool AllowAccessTokensViaBrowser { get; set; }

        [Required]
        public string CallbackPath { get; set; }

        public List<SelectListItem> IdentityResourceListItems { get; set; }
        public List<SelectListItem> GrantTypes { get; set; }
        public List<SelectListItem> ProtectedResourcesListItems { get; set; }
        public List<SelectListItem> ClientTypes { get; set; }

    }
}
