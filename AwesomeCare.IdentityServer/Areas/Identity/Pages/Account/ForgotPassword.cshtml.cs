using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using AwesomeCare.Services.Services;
using Microsoft.Extensions.Logging;
using Mandrill.Models;
using Newtonsoft.Json;

namespace AwesomeCare.IdentityServer.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ForgotPasswordModel> logger;
        private readonly IMailChimpService mailChimpService;

        public ForgotPasswordModel(UserManager<ApplicationUser> userManager,
            ILogger<ForgotPasswordModel> logger,
            IMailChimpService mailChimpService)
        {
            _userManager = userManager;
            this.logger = logger;
            this.mailChimpService = mailChimpService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToPage("./ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please 
                // visit https://go.microsoft.com/fwlink/?LinkID=532713

                //  var tt = await _userManager.GenerateChangePhoneNumberTokenAsync(user, user.PhoneNumber);
                //   var kk = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/Account/ResetPassword",
                    pageHandler: null,
                    values: new { area = "Identity", code },
                    protocol: Request.Scheme);

                string content = $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.";
                logger.LogInformation("Reset Password content: {0}", content);
                var emailResult = await mailChimpService.SendAsync("Reset Password", content, true, new List<DataTransferObject.Models.MailChimp.Recipient>
               {
                   new DataTransferObject.Models.MailChimp.Recipient
                   {
                        Email = Input.Email,
                         Name = "",
                          Type = DataTransferObject.Enums.EmailTypeEnum.to
                   }
               });
                var emailResultJson = JsonConvert.SerializeObject(emailResult);
                logger.LogInformation($"Reset password email result {emailResultJson}");

                return RedirectToPage("./ForgotPasswordConfirmation");
            }

            return Page();
        }
    }
}
