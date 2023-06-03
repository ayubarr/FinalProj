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


        public OperatorController(IBaseUserService<SupportOperator> userService,
            IAuthManager<SupportOperator> authService,
            IAuthManager<TechTeam> authforTechTeamService)
        {
            _userService = userService;
            _authService = authService;
            _authforTechTeamService = authforTechTeamService;
        }

    

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator, Moderator, Administrator")]
        [HttpGet("ActiveRequest")]
        public async Task<IActionResult> GetActive(string techTeamId)
        {
            var response = await _userService.GetActiveRequests(techTeamId);
            return Ok(response.Data);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator, Moderator, Administrator")]
        [HttpGet("ClosedRequest")]
        public async Task<IActionResult> GetClosed(string techTeamId)
        {
            var response = await _userService.GetClosedRequests(techTeamId);
            return Ok(response.Data);
        }


        [HttpPost("AcceptRequest/{requestId}/{Id}")]
        public async Task<IActionResult> AcceptRequest(Guid requestId, string techTeamId)
        {
            var response = await _userService.AcceptRequest(requestId, techTeamId);
            return Ok(response.Data);
        }

        [HttpPost("MarkRequestAsCompleted/{requestId}")]
        public async Task<IActionResult> MarkRequestAsCompleted(Guid requestId)
        {
            var response = await _userService.MarkRequestAsCompleted(requestId);
            return Ok(response.Data);
        }

        [HttpPost("CloseRequestByOperator/{requestId}/{Id}")]
        public async Task<IActionResult> CloseRequestByUser(Guid requestId, string operatorId)
        {
            var response = await _userService.CloseRequestByUser(requestId, operatorId);
            return Ok(response.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _userService.ReadAllAsync();
            return Ok(response.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var response = await _userService.ReadByIdAsync(id);
            return Ok(response.Data);
        }

        [HttpPut]
        public async Task<IActionResult> Put(SupportOperator model)
        {
            await _userService.UpdateAsync(model);
            return Ok();
        }

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

        [HttpPost]
        [Route("register-operator")]
        public async Task<IActionResult> RegisterOperatorAccount([FromBody] RegisterModel model)
        {
            var result = await _authService.Register(model);
            await _userService.SetUserAsRoleById(result.userId, 2);
            return Ok(result);
        }

        [HttpPost]
        [Route("register-techTeam")]
        public async Task<IActionResult> RegisterTechnicalTeamAccount([FromBody] RegisterModel model)
        {
            var result = await _authforTechTeamService.Register(model);
            await _userService.SetUserAsRoleById(result.userId, 1);

            return Ok(result);
        }


        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            var result = await _authService.RegisterAdmin(model);
            return Ok(result);
        }

        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken(TokenModel tokenModel)
        {
            var result = await _authService.RefreshToken(tokenModel);
            return Ok(result);
        }

        [Authorize]
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

        [Authorize]
        [HttpPost]
        [Route("revoke-all")]
        public async Task<IActionResult> RevokeAll()
        {
            await _authService.RevokeAllRefreshTokensAsync();
            return NoContent();
        }

    }
}
