using FinalApp.ApiModels.DTOs.EntitiesDTOs.RequestsDTO;
using FinalApp.ApiModels.Response.Helpers;
using FinalApp.ApiModels.Response.Interfaces;
using FinalApp.DAL.Repository.Interfaces;
using FinalApp.Domain.Models.Entities.Requests.EcoBoxInfo;
using FinalApp.Domain.Models.Entities.Requests.RequestsInfo;
using FinalApp.Domain.Models.Enums;
using FinalApp.Services.Interfaces;
using FinalApp.Services.Mapping.Helpers;
using FinallApp.ValidationHelper;
using Microsoft.EntityFrameworkCore;

namespace FinalApp.Services.Implemintations
{
    public class RequestService : IRequestService
    {
        private readonly IBaseAsyncRepository<Request> _repository;
        private readonly IBaseAsyncRepository<Location> _locationRepository;
        private readonly IBaseAsyncRepository<EcoBoxTemplate> _templateRepository;



        public RequestService(IBaseAsyncRepository<Request> repository,
            IBaseAsyncRepository<Location> locationRepository,
            IBaseAsyncRepository<EcoBoxTemplate> templateRepository)
        {
            _repository = repository;
            _locationRepository = locationRepository;
            _templateRepository = templateRepository;
        }
        public async Task<IBaseResponse<IEnumerable<RequestDTO>>> GetUnassignedRequests()
        {
            try
            {
                var unassignedRequests = await _repository
                    .ReadAllAsync().Result
                    .Where(request => request.RequestStatus == Status.Active && request.OperatorId == null && request.TechTeamId == null)
                    .ToListAsync();


                ObjectValidator<IEnumerable<Request>>.CheckIsNotNullObject(unassignedRequests);

                IEnumerable<RequestDTO> unassignedRequestsDTO = MapperHelperForDto<Request, RequestDTO>.Map(unassignedRequests);

                return ResponseFactory<IEnumerable<RequestDTO>>.CreateSuccessResponse(unassignedRequestsDTO);
            }           
            catch (ArgumentException argException)
            {
                return ResponseFactory<IEnumerable<RequestDTO>>.CreateNotFoundResponse(argException);
            }
            catch (Exception exception)
            {
                return ResponseFactory<IEnumerable<RequestDTO>>.CreateErrorResponse(exception);
            }
        }
        public async Task<IBaseResponse<IEnumerable<RequestDTO>>> GetClosedRequestsByOperatorId(int operatorId)
        {
            try
            {
                NumberValidator<int>.IsPositive(operatorId);

                var closedRequests = await _repository
                    .ReadAllAsync().Result
                    .Where(r => r.OperatorId == operatorId && r.RequestStatus == Status.Closed)
                    .ToListAsync();

                ObjectValidator<IEnumerable<Request>>.CheckIsNotNullObject(closedRequests);

                IEnumerable<RequestDTO> closedRequestsDTO = MapperHelperForDto<Request, RequestDTO>.Map(closedRequests);

                return ResponseFactory<IEnumerable<RequestDTO>>.CreateSuccessResponse(closedRequestsDTO);
            }
            catch (ArgumentException argException)
            {
                return ResponseFactory<IEnumerable<RequestDTO>>.CreateNotFoundResponse(argException);
            }
            catch (Exception exception)
            {
                return ResponseFactory<IEnumerable<RequestDTO>>.CreateErrorResponse(exception);
            }
        }

        public async Task<IBaseResponse<IEnumerable<RequestDTO>>> GetActiveRequestsByOperatorId(int operatorId)
        {
            try
            {
                NumberValidator<int>.IsPositive(operatorId);

                var activeRequests = await _repository
                    .ReadAllAsync().Result
                    .Where(r => r.OperatorId == operatorId && r.RequestStatus == Status.Active)
                    .ToListAsync();

                ObjectValidator<IEnumerable<Request>>.CheckIsNotNullObject(activeRequests);

                IEnumerable<RequestDTO> activeRequestsDTO = MapperHelperForDto<Request, RequestDTO>.Map(activeRequests);

                return ResponseFactory<IEnumerable<RequestDTO>>.CreateSuccessResponse(activeRequestsDTO);
            }
            catch (ArgumentException argException)
            {
                return ResponseFactory<IEnumerable<RequestDTO>>.CreateNotFoundResponse(argException);
            }
            catch (Exception exception)
            {
                return ResponseFactory<IEnumerable<RequestDTO>>.CreateErrorResponse(exception);
            }
        }

        public async Task<IBaseResponse<bool>> AssignRequestToTeam(int requestId, int teamId)
        {
            try
            {
                NumberValidator<int>.IsPositive(requestId);
                NumberValidator<int>.IsPositive(teamId);

                var request = await _repository.ReadByIdAsync(requestId);
                ObjectValidator<Request>.CheckIsNotNullObject(request);

                request.TechTeamId = teamId;
                await _repository.UpdateAsync(request);

                return ResponseFactory<bool>.CreateSuccessResponse(true);
            }
            catch (ArgumentException argException)
            {
                return ResponseFactory<bool>.CreateNotFoundResponse(argException);
            }
            catch (Exception exception)
            {
                return ResponseFactory<bool>.CreateErrorResponse(exception);
            }
        }
        public async Task<IBaseResponse<bool>> AssignRequestToOperator(int requestId, int operatorId)
        {
            try
            {
                NumberValidator<int>.IsPositive(requestId);
                NumberValidator<int>.IsPositive(operatorId);

                var request = await _repository.ReadByIdAsync(requestId);

                ObjectValidator<Request>.CheckIsNotNullObject(request);

                request.OperatorId = operatorId;
                await _repository.UpdateAsync(request);

                return ResponseFactory<bool>.CreateSuccessResponse(true);
            }
            catch (ArgumentException argException)
            {
                return ResponseFactory<bool>.CreateNotFoundResponse(argException);
            }
            catch (Exception exception)
            {
                return ResponseFactory<bool>.CreateErrorResponse(exception);
            }
        }

        public async Task<IBaseResponse<bool>> MarkRequestAsCompleted(int requestId)
        {
            try
            {
                NumberValidator<int>.IsPositive(requestId);
 
                var request = await _repository.ReadByIdAsync(requestId);

                ObjectValidator<Request>.CheckIsNotNullObject(request);

                request.RequestStatus = Status.Completed;
                await _repository.UpdateAsync(request);

                return ResponseFactory<bool>.CreateSuccessResponse(true);
            }
            catch (ArgumentException argException)
            {
                return ResponseFactory<bool>.CreateNotFoundResponse(argException);
            }
            catch (Exception exception)
            {
                return ResponseFactory<bool>.CreateErrorResponse(exception);
            }
        }

        public async Task<IBaseResponse<bool>> AssignLocationToRequest(int requestId, int locationId)
        {
            try
            {
                NumberValidator<int>.IsPositive(requestId);
                NumberValidator<int>.IsPositive(locationId);

                var request = await _repository.ReadByIdAsync(requestId);
                var location = await _locationRepository.ReadByIdAsync(locationId);

                ObjectValidator<Request>.CheckIsNotNullObject(request);
                ObjectValidator<Location>.CheckIsNotNullObject(location);

                request.Location = location;
                await _repository.UpdateAsync(request);

                return ResponseFactory<bool>.CreateSuccessResponse(true);
            }
            catch (ArgumentException argException)
            {
                return ResponseFactory<bool>.CreateNotFoundResponse(argException);
            }
            catch (Exception exception)
            {
                return ResponseFactory<bool>.CreateErrorResponse(exception);
            }
        }

        public async Task<IBaseResponse<bool>> SetEcoBoxQuantityAndTemplate(int requestId, int quantity, int templateId)
        {
            try
            {
                NumberValidator<int>.IsPositive(requestId);
                NumberValidator<int>.IsPositive(templateId);

                var request = await _repository.ReadAllAsync().Result
                    .Include(r => r.Location)
                  .ThenInclude(location => location.EcoBoxes)
                  .FirstOrDefaultAsync(request => request.Id == requestId);

                ObjectValidator<Request>.CheckIsNotNullObject(request);
    
                    if (request.Location != null && request.Location.EcoBoxes != null && request.Location.EcoBoxes.Any())
                    {
                        var template = await _templateRepository.ReadByIdAsync(templateId);
                        ObjectValidator<EcoBoxTemplate>.CheckIsNotNullObject(template);
                        var ecoBoxesToUpdate = request.Location.EcoBoxes.Take(quantity);
                        foreach (var ecoBox in ecoBoxesToUpdate)
                        {
                            ecoBox.Template = template;
                        }                                            
                    }
                    else
                    {
                        throw new InvalidOperationException("The Location or EcoBoxes associated with the Request are not available.");
                    }

                    await _repository.UpdateAsync(request);

                return ResponseFactory<bool>.CreateSuccessResponse(true);
            }
            catch (ArgumentException argException)
            {
                return ResponseFactory<bool>.CreateNotFoundResponse(argException);
            }
            catch (InvalidOperationException invOperationException)
            {
                return ResponseFactory<bool>.CreateNotFoundResponse(invOperationException);
            }
            catch (Exception exception)
            {
                return ResponseFactory<bool>.CreateErrorResponse(exception);
            }
        }

        public async Task<IBaseResponse<bool>> CreateRequest(RequestDTO request)
        {
            try
            {
                ObjectValidator<RequestDTO>.CheckIsNotNullObject(request);

                var newRequest = MapperHelperForEntity<RequestDTO, Request>.Map(request);

                await _repository.Create(newRequest);

                return ResponseFactory<bool>.CreateSuccessResponse(true);
            }
            catch (ArgumentException argException)
            {
                return ResponseFactory<bool>
                    .CreateNotFoundResponse(argException);
            }
            catch (Exception exception)
            {
                return ResponseFactory<bool>.CreateErrorResponse(exception);
            }
        }

        public async Task<IBaseResponse<bool>> ChangeRequestStatus(int requestId, Status newStatus)
        {
            try
            {
                NumberValidator<int>.IsPositive(requestId);

                var request = await _repository.ReadByIdAsync(requestId);
                ObjectValidator<Request>.CheckIsNotNullObject(request);

                request.RequestStatus = newStatus;
                await _repository.UpdateAsync(request);

                return ResponseFactory<bool>.CreateSuccessResponse(true);
            }
            catch (ArgumentException argException)
            {
                return ResponseFactory<bool>.CreateNotFoundResponse(argException);
            }
            catch (Exception exception)
            {
                return ResponseFactory<bool>.CreateErrorResponse(exception);
            }
        }
   


    }
}
