using FinalApp.ApiModels.DTOs.EntitiesDTOs.RequestsDTO;
using FinalApp.Domain.Models.Entities.Requests.RequestsInfo;
using FinalApp.Domain.Models.Enums;
using FinalApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinalApp.Api.Controllers
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

        [HttpGet]
        public IActionResult Get()
        {
            var response = _service.ReadAll();
            return Ok(response.Data);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var response = _service.ReadById(id);
            return Ok(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Post(RequestDTO model)
        {
            await _service.CreateAsync(model);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(RequestDTO model)
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

        [HttpGet("UnassignedRequests")]
        public async Task<IActionResult> GetUnassignedRequests()
        {
            var response = await _requestService.GetUnassignedRequests();
            return Ok(response.Data);
        }

        [HttpGet("ClosedRequestsByOperator/{operatorId}")]
        public async Task<IActionResult> GetClosedRequestsByOperatorId(int operatorId)
        {
            var response = await _requestService.GetClosedRequestsByOperatorId(operatorId);
            return Ok(response.Data);
        }

        [HttpGet("ActiveRequestsByOperator/{operatorId}")]
        public async Task<IActionResult> GetActiveRequestsByOperatorId(int operatorId)
        {
            var response = await _requestService.GetActiveRequestsByOperatorId(operatorId);
            return Ok(response.Data);
        }

        [HttpPost("AssignRequestToTechTeam/{requestId}/{teamId}")]
        public async Task<IActionResult> AssignRequestToTeam(int requestId, int teamId)
        {
            var response = await _requestService.AssignRequestToTeam(requestId, teamId);
            return Ok(response.Data);
        }

        [HttpPost("AssignRequestToOperator/{requestId}/{operatorId}")]
        public async Task<IActionResult> AssignRequestToOperator(int requestId, int operatorId)
        {
            var response = await _requestService.AssignRequestToOperator(requestId, operatorId);
            return Ok(response.Data);
        }

        [HttpPost("MarkRequestAsCompleted/{requestId}")]
        public async Task<IActionResult> MarkRequestAsCompleted(int requestId)
        {
            var response = await _requestService.MarkRequestAsCompleted(requestId);
            return Ok(response.Data);
        }

        [HttpPost("AssignLocationToRequest/{requestId}/{locationId}")]
        public async Task<IActionResult> AssignLocationToRequest(int requestId, int locationId)
        {
            var response = await _requestService.AssignLocationToRequest(requestId, locationId);
            return Ok(response.Data);
        }

        [HttpPost("SetEcoBoxQuantityAndTemplate/{requestId}/{quantity}/{templateId}")]
        public async Task<IActionResult> SetEcoBoxQuantityAndTemplate(int requestId, int quantity, int templateId)
        {
            var response = await _requestService.SetEcoBoxQuantityAndTemplate(requestId, quantity, templateId);
            return Ok(response.Data);
        }

        [HttpPost("CreateRequest")]
        public async Task<IActionResult> CreateRequest(RequestDTO request)
        {
            var response = await _requestService.CreateRequest(request);
            return Ok(response.Data);
        }

        [HttpPost("ChangeRequestStatus/{requestId}/{newStatus}")]
        public async Task<IActionResult> ChangeRequestStatus(int requestId, Status newStatus)
        {
            var response = await _requestService.ChangeRequestStatus(requestId, newStatus);
            return Ok(response.Data);
        }
    }
}
