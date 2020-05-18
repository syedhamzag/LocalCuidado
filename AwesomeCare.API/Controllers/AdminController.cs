using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeCare.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AdminController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public AdminController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpPost("CreateRole",Name ="CreateRole")]
        public async Task<IActionResult> CreateRole([FromQuery]string role)
        {
            var identityRole = new IdentityRole(role);
            var result = await _roleManager.CreateAsync(identityRole);
            if (result.Succeeded)
                return Ok();
            else
                return BadRequest(result.Errors.FirstOrDefault());
        }

        [HttpGet("GetRoleById/{roleId}", Name = "GetRoleById")]
        public async Task<IActionResult> GetRoleById(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
                return NotFound();

            return Ok(role);
        }

        [HttpGet("GetRoleByName/{roleName}", Name = "GetRoleByName")]
        public async Task<IActionResult> GetRoleByName(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
                return NotFound();

            return Ok(role);
        }

        [HttpGet("GetRoles", Name = "GetRoles")]
        public IActionResult GetRoles()
        {
            var role =  _roleManager.Roles.ToList();
            
            return Ok(role);
        }

        [HttpDelete("DeleteRole/{roleName}", Name = "DeleteRole")]
        public async Task<IActionResult> DeleteRole(string roleName)
        {
            var role = new IdentityRole(roleName);
            var result =await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
                return Ok();
            else
                return BadRequest(result.Errors.FirstOrDefault());
        }
    }
}