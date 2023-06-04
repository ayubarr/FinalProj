using FinalApp.ApiModels.Auth.Models;
using FinalApp.Domain.Models.Entities.Persons.Users;
using FinalApp.Services.Interfaces;
using FinalProj.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperatorController : ControllerBase
    {
        private readonly IBaseUserService<SupportOperator> _userService;
        private readonly IAuthManager<SupportOperator> _authService;
        private readonly IAuthManager<TechTeam> _authforTechTeamService;
        private readonly IBaseUserService<TechTeam> _techTeamUserService;

        public OperatorController(IBaseUserService<SupportOperator> userService,
            IAuthManager<SupportOperator> authService,
            IAuthManager<TechTeam> authforTechTeamService,
            IBaseUserService<TechTeam> techTeamuserService)
        {
            _userService = userService;
            _authService = authService;
            _authforTechTeamService = authforTechTeamService;
            _techTeamUserService = techTeamuserService;
        }





        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator")]
        [HttpPut("SetOperatorRoleById")]
        public async Task<IActionResult> PutRoleById(string usertId, int roleId)
        {
            await _userService.SetUserAsRoleById(usertId, roleId);
            return Ok();
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator")]
        [HttpGet("checkOperatorRole/{userId}/{roleId}")]
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





        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator, Moderator, TechnicalSupportOperator")]
        [HttpGet("ActiveRequest")]
        public async Task<IActionResult> GetActive(string techTeamId)
        {
            var response = await _userService.GetActiveRequests(techTeamId);
            return Ok(response.Data);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator, Moderator, TechnicalSupportOperator")]
        [HttpGet("ClosedRequest")]
        public async Task<IActionResult> GetClosed(string techTeamId)
        {
            var response = await _userService.GetClosedRequests(techTeamId);
            return Ok(response.Data);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "TechnicalSupportOperator")]
        [HttpPost("AcceptRequest/{requestId}/{Id}")]
        public async Task<IActionResult> AcceptRequest(Guid requestId, string techTeamId)
        {
            var response = await _userService.AcceptRequest(requestId, techTeamId);
            return Ok(response.Data);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator, Moderator, TechnicalSupportOperator")]
        [HttpPost("MarkRequestAsCompleted/{requestId}")]
        public async Task<IActionResult> MarkRequestAsCompleted(Guid requestId)
        {
            var response = await _userService.MarkRequestAsCompleted(requestId);
            return Ok(response.Data);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator, Moderator, TechnicalSupportOperator")]
        [HttpPost("CloseRequestByOperator/{requestId}/{Id}")]
        public async Task<IActionResult> CloseRequestByUser(Guid requestId, string operatorId)
        {
            var response = await _userService.CloseRequestByUser(requestId, operatorId);
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

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator, Moderator, TechnicalSupportOperator")]
        [HttpPut]
        public async Task<IActionResult> Put(SupportOperator model)
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
        [Route("login-Operator")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var response = await _authService.Login(model);

            if (response.IsSuccess)
            {
                return Ok(response);

            }
            return Unauthorized(response.Message);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator, Moderator, TechnicalSupportOperator")]
        [HttpPost]
        [Route("register-techTeam")]
        public async Task<IActionResult> RegisterTechnicalTeamAccount([FromBody] RegisterModel model)
        {
            var result = await _authforTechTeamService.Register(model);
            await _techTeamUserService.SetUserAsRoleById(result.userId, 1);

            return Ok(result);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator, Moderator, TechnicalSupportOperator")]
        [HttpPost]
        [Route("register-operator")]
        public async Task<IActionResult> RegisterOperatorAccount([FromBody] RegisterModel model)
        {
            var result = await _authService.Register(model);
            await _userService.SetUserAsRoleById(result.userId, 2);
            return Ok(result);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator")]
        [HttpPost]
        [Route("register-moderator")]
        public async Task<IActionResult> RegisterModer([FromBody] RegisterModel model)
        {
            var result = await _authService.Register(model);
            await _userService.SetUserAsRoleById(result.userId, 3);
            return Ok(result);
        }

        // [Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator")]
        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            var result = await _authService.Register(model);
            await _userService.SetUserAsRoleById(result.userId, 4);
            return Ok(result);
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
