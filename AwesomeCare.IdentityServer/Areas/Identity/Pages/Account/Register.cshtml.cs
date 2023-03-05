using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using AwesomeCare.Services.Services;
using Newtonsoft.Json;

namespace AwesomeCare.IdentityServer.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IMailChimpService mailChimpService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration configuration;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IMailChimpService mailChimpService,
            RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            this.mailChimpService = mailChimpService;
            _roleManager = roleManager;
            this.configuration = configuration;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        //   public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            //  ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {

            returnUrl = returnUrl ?? Url.Content("~/");
            // ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    //Add user to role
                    var staffRole = await _roleManager.FindByNameAsync("staff");
                    if (staffRole != null)
                    {
                        await _userManager.AddToRoleAsync(user, staffRole.Name);
                    }

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    string content = $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.";
                    var emailResult = await mailChimpService.SendAsync("Confirm your email", content, true, new List<DataTransferObject.Models.MailChimp.Recipient>
                    {
                        new DataTransferObject.Models.MailChimp.Recipient
                        {
                            Email = Input.Email,
                            Name = "",
                             Type = DataTransferObject.Enums.EmailTypeEnum.to
                        }
                    });

                    string emailResultJson = JsonConvert.SerializeObject(emailResult);
                    _logger.LogInformation($"Register email result {emailResultJson}");


                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        if (string.IsNullOrEmpty(returnUrl))
                        {
                            string staffWebSiteUrl = configuration["staffwebsite"];
                            return RedirectPermanent(staffWebSiteUrl);
                        }
                        else
                        {
                            return RedirectToAction("Login", "Account", new { returnUrl = returnUrl });
                        }
                        //return LocalRedirect(returnUrl);

                    }

                    // //Redirect to Staff Web Site
                    //if (string.IsNullOrEmpty(returnUrl))
                    //{
                    //    string staffWebSiteUrl = configuration["staffwebsite"];
                    //    return RedirectPermanent(staffWebSiteUrl);
                    //}
                    //else
                    //{
                    //    return RedirectToAction("Login", "Account", new { returnUrl = returnUrl });
                    //}

                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
