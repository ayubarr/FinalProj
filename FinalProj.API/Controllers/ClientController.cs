using FinalApp.ApiModels.Auth.Models;
using FinalApp.ApiModels.DTOs.EntitiesDTOs.UsersDTOs;
using FinalApp.Domain.Models.Entities.Persons.Users;
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

        public ClientController(IClientService clientService,
            IBaseUserService<Client> userService,
            IAuthManager<Client> authService)
        {
            _clientService = clientService;
            _userService = userService;
            _authService = authService;
        }

        [HttpGet("ClientsWithRequest")]
        public async Task<IActionResult> GetClients()
        {
            var response = await _clientService.GetClientsWithRequests();
            return Ok(response.Data);
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
                return Ok(new
                {
                    Token = response.Data.Token,
                    RefreshToken = response.Data.RefreshToken,
                    Expiration = response.Data.Expiration
                });
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
