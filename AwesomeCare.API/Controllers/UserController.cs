using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using AwesomeCare.DataAccess.Repositories;
using AwesomeCare.DataTransferObject.DTOs.User;
using AwesomeCare.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AwesomeCare.API.Controllers
{
   
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IGenericRepository<StaffPersonalInfo> staffPersonalInfoRepository;
        private readonly ILogger<UserController> logger;

        public UserController(UserManager<ApplicationUser> userManager,
            IGenericRepository<StaffPersonalInfo> staffPersonalInfoRepository,
            ILogger<UserController> logger)
        {
            this.userManager = userManager;
            this.staffPersonalInfoRepository = staffPersonalInfoRepository;
            this.logger = logger;
        }



        [HttpGet]
        [ProducesResponseType(typeof(List<GetUser>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var users = await userManager.Users.ProjectTo<GetUser>().ToListAsync();
            return Ok(users);
        }


        [HttpGet("{userId}")]
        [ProducesResponseType(typeof(GetUser), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(string userId)
        {
            var user = await userManager.Users.ProjectTo<GetUser>().FirstOrDefaultAsync(u => u.UserId == userId);
            return Ok(user);
        }

        [HttpPut]
        [ProducesResponseType(typeof(GetUser), StatusCodes.Status200OK)]
        public async Task<IActionResult> Put([FromBody] PutUser model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await userManager.FindByIdAsync(model.UserId);

            if (user == null) return NotFound();

            user.EmailConfirmed = model.EmailConfirmed;
            user.LockoutEnabled = model.LockedOutEnabled;
            if (!model.LockedOutEnabled)
            {
                user.AccessFailedCount = 0;
                user.LockoutEnd = null;
            }
            var result = await userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                logger.LogInformation($"Update User Failed: {string.Join(',', result.Errors)}");
                return BadRequest(ModelState);
            }

            if (!string.IsNullOrEmpty(model.Password))
            {
                var resetPasswordResult = await ResetPassword(user, model.Password);
                logger.LogInformation($"Update User Password Reset returned: {resetPasswordResult.Succeeded}");
            }
            return Ok();
        }



        [HttpGet("ChangeEmail/{userId}")]
        [ProducesResponseType(typeof(GetChangeEmail), StatusCodes.Status200OK)]
        public async Task<IActionResult> ChangeEmail(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);
            var changeEmail = new GetChangeEmail
            {
                Email = user.Email,
                UserId = user.Id
            };

            return Ok(changeEmail);
        }

        /// <summary>
        /// Changes user Email as well as log on Username/Email
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("ChangeEmail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ChangeEmail([FromBody] PostChangeEmail model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await userManager.FindByIdAsync(model.UserId);
            var profile = await staffPersonalInfoRepository.Table.FirstOrDefaultAsync(p => p.ApplicationUserId == model.UserId);

            user.Email = model.EmailAddress;
            user.UserName = model.EmailAddress;

            var result = await userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                if (profile != null)
                {
                    profile.Email = model.EmailAddress;
                    await staffPersonalInfoRepository.UpdateEntity(profile);
                }
                return Ok();
            }
            else
                return BadRequest(result.Errors.FirstOrDefault());
        }

        [HttpPost("Admin/ResetPassord")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ResetUserPassword([FromBody] PostResetPassord model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return NotFound();

            //var resetToken = await userManager.GeneratePasswordResetTokenAsync(user);
            var result = await ResetPassword(user, model.Password);// await userManager.ResetPasswordAsync(user, resetToken, model.Password);
            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }
        }

        async Task<IdentityResult> ResetPassword(ApplicationUser user, string password)
        {
            var resetToken = await userManager.GeneratePasswordResetTokenAsync(user);
            var result = await userManager.ResetPasswordAsync(user, resetToken, password);

            return result;
        }
    }
}
