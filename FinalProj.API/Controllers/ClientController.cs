using FinalApp.ApiModels.DTOs.EntitiesDTOs.UsersDTOs;
using FinalApp.Domain.Models.Entities.Persons.Users;
using FinalApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinalApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly IBaseUserService<Client> _userService;

        public ClientController(IClientService clientService, IBaseUserService<Client> userService)
        {
            _clientService = clientService;
            _userService = userService;
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
    }
}
