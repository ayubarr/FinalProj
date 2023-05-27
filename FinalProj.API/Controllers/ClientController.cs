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

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
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
    }
}
