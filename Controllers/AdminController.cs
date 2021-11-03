using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using addroles.Data;
using addroles.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace addroles.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        // private object _context;
        public AdminController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        // POST: http://localhost:5000/api/auth/createrole?role=role
        [HttpPost("createrole")]
        public async Task<ActionResult<ApplicationUser>> CreateRole(String role)
        {
            if (!await _roleManager.RoleExistsAsync(role))
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }
            return CreatedAtAction("CreateRole", new { Role = role });
        }

        [HttpPost("assignrole")]
        public async Task<ActionResult<Cars>> AssignRole(string userName, String role)
        {
            var user = await _userManager.FindByNameAsync(userName);
            await _userManager.AddToRoleAsync(user, role);
            return CreatedAtAction("AssignRole", new { UserId = userName, Role = role });
        }

        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetUserBtId(string id)
        {
            {
                var user = await _userManager.FindByIdAsync(id);
                var roles = await _userManager.GetRolesAsync(user);

                return Ok(roles);
            }
        }

        [HttpGet("getcurrentuser/{email}")]
        [Authorize]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            {
                var user = await _userManager.FindByEmailAsync(email);
                var roles = await _userManager.GetRolesAsync(user);
                return Ok(roles);
            }
        }



        [HttpDelete("{id}")]
        public async Task<ActionResult<ApplicationUser>> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            await _userManager.DeleteAsync(user);

            return user;
        }

        [HttpDelete("userrole/{id}/username/{name}")]
        public async Task<ActionResult<ApplicationUser>> DeleteUserRole(string id, string name)
        {
            var user = await _userManager.FindByNameAsync(name);
            await _userManager.RemoveFromRoleAsync(user, id);

            return user;
        }


    }
}