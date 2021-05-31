using AwesomeCare.Admin.Services.User;
using AwesomeCare.DataTransferObject.DTOs.User;
using AwesomeCare.Services.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AwesomeCare.Admin.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService userService;

        public UserController(IFileUpload fileUpload,
            IUserService userService) : base(fileUpload)
        {
            this.userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await userService.GetUsers();
            return View(result);
        }

        public async Task<IActionResult> EditUser(string userId)
        {
            var result = await userService.GetUser(userId);
            var model = new PutUser
            {
                EmailConfirmed = result.EmailConfirmed,
                Email = result.Email,
                LockedOutEnabled = result.LockedOutEnabled,
                UserId = result.UserId,
                UserName = result.UserName
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(PutUser model)
        {
            if (!ModelState.IsValid)
                return View(model);

            model.LockedOutEnabled = model.CanLockOut.Equals("yes", StringComparison.InvariantCultureIgnoreCase);
            model.EmailConfirmed = model.ConfirmEmail.Equals("yes", StringComparison.InvariantCultureIgnoreCase);

            var result = await userService.UpdateUser(model);
            var content = await result.Content.ReadAsStringAsync();

            SetOperationStatus(new Models.OperationStatus { IsSuccessful = result.IsSuccessStatusCode, Message = result.IsSuccessStatusCode ? "User updated successfully" : content });

            if (!result.IsSuccessStatusCode)
                return View(model);


            return RedirectToAction("Index");
        }
    }
}
