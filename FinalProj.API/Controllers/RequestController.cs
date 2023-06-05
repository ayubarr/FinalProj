using FinalProj.ApiModels.DTOs.EntitiesDTOs.RequestsDTO;
using FinalProj.Domain.Models.Entities.Requests.RequestsInfo;
using FinalProj.Domain.Models.Enums;
using FinalProj.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;

namespace FinalProj.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IBaseRequestService<Request, RequestDTO> _service;
        private readonly IRequestService _requestService;

        public RequestController(IBaseRequestService<Request, RequestDTO> service, IRequestService requestService)
        {
            _service = service;
            _requestService = requestService;
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator, Moderator, TechnicalSupportOperator")]
        [HttpGet]
        public IActionResult Get()
        {
            var response = _service.ReadAll();
            return Ok(response);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator, Moderator, TechnicalSupportOperator")]
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var response = _service.ReadById(id);
            return Ok(response.Data);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator, Moderator, Client")]
        [HttpPost("CreateRequest")]
        public async Task<IActionResult> CreateRequest(RequestDTO request)
        {
            var response = await _requestService.CreateRequest(request);
            return Ok(response);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator, Moderator")]
        [HttpPut]
        public async Task<IActionResult> Put(RequestDTO model)
        {
            await _service.UpdateAsync(model);
            return Ok();
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator, Moderator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteByIdAsync(id);
            return Ok();
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator, Moderator, TechnicalSupportOperator")]
        [HttpGet("UnassignedRequests")]
        public async Task<IActionResult> GetUnassignedRequests()
        {
            var response = await _requestService.GetUnassignedRequests();
            return Ok(response.Data);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator, Moderator, TechnicalSupportOperator")]
        [HttpGet("ClosedRequestsByOperator/{operatorId}")]
        public async Task<IActionResult> GetClosedRequestsByOperatorId(string operatorId)
        {
            var response = await _requestService.GetClosedRequestsByOperatorId(operatorId);
            return Ok(response.Data);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator, Moderator, TechnicalSupportOperator")]
        [HttpGet("ActiveRequestsByOperator/{operatorId}")]
        public async Task<IActionResult> GetActiveRequestsByOperatorId(string operatorId)
        {
            var response = await _requestService.GetActiveRequestsByOperatorId(operatorId);
            return Ok(response.Data);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator, Moderator, TechnicalSupportOperator")]
        [HttpPost("AssignRequestToTechTeam/{requestId}/{teamId}")]
        public async Task<IActionResult> AssignRequestToTeam(Guid requestId, string teamId)
        {
            var response = await _requestService.AssignRequestToTeam(requestId, teamId);
            return Ok(response.Data);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator, Moderator, TechnicalSupportOperator")]
        [HttpPost("AssignRequestToOperator/{requestId}/{operatorId}")]
        public async Task<IActionResult> AssignRequestToOperator(Guid requestId, string operatorId)
        {
            var response = await _requestService.AssignRequestToOperator(requestId, operatorId);
            return Ok(response.Data);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator, Moderator, TechnicalSupportOperator")]
        [HttpPost("MarkRequestAsCompleted/{requestId}")]
        public async Task<IActionResult> MarkRequestAsCompleted(Guid requestId)
        {
            var response = await _requestService.MarkRequestAsCompleted(requestId);
            return Ok(response.Data);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator, Moderator, TechnicalSupportOperator")]
        [HttpPost("AssignLocationToRequest/{requestId}/{locationId}")]
        public async Task<IActionResult> AssignLocationToRequest(Guid requestId, Guid locationId)
        {
            var response = await _requestService.AssignLocationToRequest(requestId, locationId);
            return Ok(response.Data);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator, Moderator, TechnicalSupportOperator")]
        [HttpPost("SetEcoBoxQuantityAndTemplate/{requestId}/{quantity}/{templateId}")]
        public async Task<IActionResult> SetEcoBoxQuantityAndTemplate(Guid requestId, int quantity, Guid templateId)
        {
            var response = await _requestService.SetEcoBoxQuantityAndTemplate(requestId, quantity, templateId);
            return Ok(response.Data);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Administrator, Moderator, TechnicalSupportOperator")]
        [HttpPost("ChangeRequestStatus/{requestId}/{newStatus}")]
        public async Task<IActionResult> ChangeRequestStatus(Guid requestId, Status newStatus)
        {
            var response = await _requestService.ChangeRequestStatus(requestId, newStatus);
            return Ok(response.Data);
        }
    }
}
