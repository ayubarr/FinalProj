using FinalApp.ApiModels.DTOs.EntitiesDTOs.UsersDTOs;
using FinalApp.Domain.Models.Entities.Persons.Users;
using FinalApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinalApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TechTeamController : ControllerBase
    {
        private readonly IBaseUserService<TechTeam> _userService;

        public TechTeamController(IBaseUserService<TechTeam> userService)
        {
            _userService = userService;
        }

        [HttpGet("ActiveRequest")]
        public async Task<IActionResult> GetActive(int techTeamId)
        {
            var response = await _userService.GetActiveRequests(techTeamId);
            return Ok(response.Data);
        }

        [HttpGet("ClosedRequest")]
        public async Task<IActionResult> GetClosed(int techTeamId)
        {
            var response = await _userService.GetClosedRequests(techTeamId);
            return Ok(response.Data);
        }
        [HttpPost("AcceptRequest/{requestId}/{Id}")]
        public async Task<IActionResult> AcceptRequest(int requestId, int Id)
        {
            var response = await _userService.AcceptRequest(requestId, Id);
            return Ok(response.Data);
        }

        [HttpPost("MarkRequestAsCompleted/{requestId}")]
        public async Task<IActionResult> MarkRequestAsCompleted(int requestId)
        {
            var response = await _userService.MarkRequestAsCompleted(requestId);
            return Ok(response.Data);
        }

        [HttpPost("CloseRequestByUser/{requestId}/{Id}")]
        public async Task<IActionResult> CloseRequestByUser(int requestId, int Id)
        {
            var response = await _userService.CloseRequestByUser(requestId, Id);
            return Ok(response.Data);
        }       
    }
}
