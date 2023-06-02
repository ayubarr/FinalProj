using FinalApp.ApiModels.Auth.Models;
using FinalApp.ApiModels.DTOs.EntitiesDTOs.UsersDTOs;
using FinalApp.Domain.Models.Entities.Persons.Users;
using FinalApp.Domain.Models.Enums;
using FinalApp.Services.Interfaces;
using FinalProj.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalApp.Api.Controllers
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

        [HttpPost("CloseRequestByClient/{requestId}/{Id}")]
        public async Task<IActionResult> CloseRequestByUser(Guid requestId, string clientId)
        {
            _logger.Log(LogLevel.Information, "test");
            var response = await _userService.CloseRequestByUser(requestId, clientId);
            
            return Ok(response.Data);
        }

        [HttpGet("ClientsWithRequest")]
        public async Task<IActionResult> GetClients()
        {
            _logger.Log(LogLevel.Information, "test");
            var response = await _clientService.GetClientsWithRequests();
            return Ok(response.Data);
        }

        [HttpPut("SetClientRoleId)")]
        public async Task<IActionResult> PutRoleById(string clientId, int roleId)
        {
            await _userService.SetUserAsRoleById(clientId, roleId);
            return Ok();
        }


        [HttpPut("SetClientAsAdmin")]
        public async Task<IActionResult> Put(string clientId)
        {
            await _userService.SetUserAsAdminAsync(clientId);
            return Ok();
        }

        [HttpGet("ActiveRequest")]
        public async Task<IActionResult> GetActive(string clientId)
        {
            var response = await _userService.GetActiveRequests(clientId);
            return Ok(response.Data);
        }

        [HttpGet("ClosedRequest")]
        public async Task<IActionResult> GetClosed(string clientId)
        {
            var response = await _userService.GetClosedRequests(clientId);
            return Ok(response.Data);
        }

        [HttpPost("MarkRequestAsCompleted/{requestId}")]
        public async Task<IActionResult> MarkRequestAsCompleted(Guid requestId)
        {
            var response = await _userService.MarkRequestAsCompleted(requestId);
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
        public async Task<IActionResult> Put(Client model)
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
