using FinalApp.ApiModels.Auth.Models;
using FinalApp.ApiModels.DTOs.EntitiesDTOs.UsersDTOs;
using FinalApp.Domain.Models.Entities.Persons.Users;
using FinalApp.Services.Interfaces;
using FinalProj.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TechTeamController : ControllerBase
    {
        private readonly IBaseUserService<TechTeam> _userService;
        private readonly IAuthManager<TechTeam> _authService;

        public TechTeamController(IBaseUserService<TechTeam> userService, IAuthManager<TechTeam> authService)
        {
            _userService = userService;
            _authService = authService;
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator, Moderator, TechnicalSpecialist")]
        [HttpGet("ActiveRequest")]
        public async Task<IActionResult> GetActive(string techTeamId)
        {
            var response = await _userService.GetActiveRequests(techTeamId);
            return Ok(response.Data);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator, Moderator, TechnicalSpecialist")]
        [HttpGet("ClosedRequest")]
        public async Task<IActionResult> GetClosed(string techTeamId)
        {
            var response = await _userService.GetClosedRequests(techTeamId);
            return Ok(response.Data);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "TechnicalSpecialist")]
        [HttpPost("AcceptRequest/{requestId}/{Id}")]
        public async Task<IActionResult> AcceptRequest(Guid requestId, string Id)
        {
            var response = await _userService.AcceptRequest(requestId, Id);
            return Ok(response.Data);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator, Moderator, TechnicalSpecialist")]
        [HttpPost("MarkRequestAsCompleted/{requestId}")]
        public async Task<IActionResult> MarkRequestAsCompleted(Guid requestId)
        {
            var response = await _userService.MarkRequestAsCompleted(requestId);
            return Ok(response.Data);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator, Moderator")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _userService.ReadAllAsync();
            return Ok(response.Data);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator, Moderator")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var response = await _userService.ReadByIdAsync(id);
            return Ok(response.Data);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator, Moderator, TechnicalSpecialist")]
        [HttpPut]
        public async Task<IActionResult> Put(TechTeam model)
        {
            await _userService.UpdateAsync(model);
            return Ok();
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator, Moderator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _userService.DeleteByIdAsync(id);
            return Ok();
        }



        [HttpPost]
        [Route("login-techTeam")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var response = await _authService.Login(model);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return Unauthorized(response.Message);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator")]
        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken(TokenModel tokenModel)
        {
            var result = await _authService.RefreshToken(tokenModel);
            return Ok(result);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator")]
        [HttpPost]
        [Route("revoke/{username}")]
        public async Task<IActionResult> Revoke(string username)
        {
            try
            {
                await _authService.RevokeRefreshTokenByUsernameAsync(username);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator")]
        [HttpPost]
        [Route("revoke-all")]
        public async Task<IActionResult> RevokeAll()
        {
            await _authService.RevokeAllRefreshTokensAsync();
            return NoContent();
        }

    }
}
