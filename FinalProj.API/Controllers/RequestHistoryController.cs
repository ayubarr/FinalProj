using FinalApp.Domain.Models.Enums;
using FinalApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestHistoryController : ControllerBase
    {
        private readonly IRequestHistoryService _historyService;

        public RequestHistoryController(IRequestHistoryService historyService)
        {
            _historyService = historyService;
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator, Moderator, TechnicalSupportOperator")]
        [HttpGet("GetStatus/{requestId}")]
        public async Task<IActionResult> GetStatus(Guid requestId)
        {
            var response = await _historyService.GetRequestHistoryStatus(requestId);
            return Ok(response.Data);
        }
    }
}
