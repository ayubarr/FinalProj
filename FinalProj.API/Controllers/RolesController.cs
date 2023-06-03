using FinalApp.Domain.Models.Abstractions.BaseUsers;
using FinalApp.Domain.Models.Entities.Persons.Users;
using FinalApp.Domain.Models.Enums;
using FinalApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalProj.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IBaseUserService<ApplicationUser> _userService;

        public RolesController(IBaseUserService<ApplicationUser> userService)
        {
            _userService = userService;
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator")]
        [HttpPut("SetClientRoleById")]
        public async Task<IActionResult> PutRoleById(string usertId, int roleId)
        {
            await _userService.SetUserAsRoleById(usertId, roleId);
            return Ok();
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator")]
        [HttpGet("checkUserRole/{userId}/{roleId}")]
        public async Task<IActionResult> CheckUserRole(string userId, int roleId)
        {
            var response = await _userService.CheckUserRole(userId, roleId);

            if (response.IsSuccess)
            {
                return Ok(response.Data);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }
    }
}
