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

        [HttpGet("ActiveRequest")]
        public async Task<IActionResult> GetActive(string clientId)
        {
            var response = await _clientService.GetActiveRequests(clientId);
            return Ok(response.Data);
        }

        [HttpGet("ClosedRequest")]
        public async Task<IActionResult> GetClosed(string clientId)
        {
            var response = await _clientService.GetClosedRequests(clientId);
            return Ok(response.Data);
        }
        [HttpGet]
        public IActionResult Get()
        {
            var response = _clientService.
            return Ok(response.Data);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var response = _service.ReadById(id);
            return Ok(response.Data);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> PostClient(ClientDTO model)
        {
            await _clientService.RegisterClient(model);
            return Ok();
        }


        [HttpPut]
        public async Task<IActionResult> Put(ClientDTO model)
        {
            await _service.UpdateAsync(model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteByIdAsync(id);
            return Ok();
        }
    }
}
