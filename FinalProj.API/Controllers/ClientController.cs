using FinalProj.ApiModels.Auth.Models;
using FinalProj.Domain.Models.Entities.Persons.Users;
using FinalProj.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProj.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly IBaseUserService<Client> _userService;
        private readonly IAuthManager<Client> _authService;
        private readonly ILogger<ClientController> _logger;
        public ClientController(IClientService clientService,
            IBaseUserService<Client> userService,
            IAuthManager<Client> authService,
            ILogger<ClientController> logger
            )
        {
            _logger = logger;
            _clientService = clientService;
            _userService = userService;
            _authService = authService;
        }

        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator")]
        [HttpPut("SetClientRoleById")]
        public async Task<IActionResult> PutRoleById(string usertId, int roleId)
        {
            await _userService.SetUserAsRoleById(usertId, roleId);
            return Ok();
        }

        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator")]
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

        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Client")]
        [HttpPost("CloseRequestByClient/{requestId}/{Id}")]
        public async Task<IActionResult> CloseRequestByUser(Guid requestId, string clientId)
        {
            _logger.Log(LogLevel.Information, "test");
            var response = await _userService.CloseRequestByUser(requestId, clientId);

            return Ok(response.Data);
        }

        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator, Moderator")]
        [HttpGet("ClientsWithRequest")]
        public async Task<IActionResult> GetClients()
        {
            _logger.Log(LogLevel.Information, "test");
            var response = await _clientService.GetClientsWithRequests();
            return Ok(response.Data);
        }

        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator")]
        [HttpPut("SetClientAsAdmin")]
        public async Task<IActionResult> Put(string clientId)
        {
            await _userService.SetUserAsAdminAsync(clientId);
            return Ok();
        }

        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator, Moderator, Client")]
        [HttpGet("ActiveRequest")]
        public async Task<IActionResult> GetActive(string clientId)
        {
            var response = await _userService.GetActiveRequests(clientId);
            return Ok(response.Data);
        }

        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator, Moderator, Client")]
        [HttpGet("ClosedRequest")]
        public async Task<IActionResult> GetClosed(string clientId)
        {
            var response = await _userService.GetClosedRequests(clientId);
            return Ok(response.Data);
        }

        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator, Client")]
        [HttpPost("MarkRequestAsCompleted/{requestId}")]
        public async Task<IActionResult> MarkRequestAsCompleted(Guid requestId)
        {
            var response = await _userService.MarkRequestAsCompleted(requestId);
            return Ok(response.Data);
        }

        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator, Moderator")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _userService.ReadAllAsync();
            return Ok(response.Data);
        }

        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator, Moderator")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var response = await _userService.ReadByIdAsync(id);
            return Ok(response.Data);
        }

        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator, Moderator, Client")]
        [HttpPut]
        public async Task<IActionResult> Put(Client model)
        {
            await _userService.UpdateAsync(model);
            return Ok();
        }

        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator, Moderator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _userService.DeleteByIdAsync(id);
            return Ok();
        }




        [HttpPost]
        [Route("login")]
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
        [Route("register")]
        public async Task<IActionResult> RegisterClient([FromBody] RegisterModel model)
        {
            var result = await _authService.Register(model);
            await _userService.SetUserAsRoleById(result.userId, 0);

            return Ok(result);
        }

        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator")]
        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken(TokenModel tokenModel)
        {
            var result = await _authService.RefreshToken(tokenModel);
            return Ok(result);
        }

        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator")]
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

        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator")]
        [HttpPost]
        [Route("revoke-all")]
        public async Task<IActionResult> RevokeAll()
        {
            await _authService.RevokeAllRefreshTokensAsync();
            return NoContent();
        }
    }
}
