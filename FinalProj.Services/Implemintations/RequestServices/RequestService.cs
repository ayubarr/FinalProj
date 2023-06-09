﻿using FinalProj.ApiModels.DTOs.EntitiesDTOs.RequestsDTO;
using FinalProj.ApiModels.Response.Helpers;
using FinalProj.ApiModels.Response.Interfaces;
using FinalProj.DAL.Repository.Interfaces;
using FinalProj.Domain.Models.Entities.Persons.Users;
using FinalProj.Domain.Models.Entities.Requests.EcoBoxInfo;
using FinalProj.Domain.Models.Entities.Requests.RequestsInfo;
using FinalProj.Domain.Models.Enums;
using FinalProj.Services.Interfaces;
using FinalProj.Services.Mapping.Helpers;
using FinallApp.ValidationHelper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FinalProj.Services.Implemintations.RequestServices
{
    public class RequestService : IRequestService
    {
        private readonly IBaseAsyncRepository<Request> _repository;
        private readonly IBaseAsyncRepository<Location> _locationRepository;
        private readonly IBaseAsyncRepository<EcoBoxTemplate> _templateRepository;
        private readonly UserManager<Client> _userManager;

        public RequestService(IBaseAsyncRepository<Request> repository,
            IBaseAsyncRepository<Location> locationRepository,
            IBaseAsyncRepository<EcoBoxTemplate> templateRepository,
            UserManager<Client> userManager)
        {
            _repository = repository;
            _locationRepository = locationRepository;
            _templateRepository = templateRepository;
            _userManager = userManager;
        }

        public async Task<IBaseResponse<IEnumerable<RequestDTO>>> GetUnassignedRequests()
        {
            try
            {
                var unassignedRequests = await _repository
                    .ReadAllAsync().Result
                    .Where(request => request.RequestStatus == Status.Active && request.OperatorId == null && request.TechTeamId == null)
                    .ToListAsync();

                IEnumerable<RequestDTO> unassignedRequestsDTO = MapperHelperForDto<Request, RequestDTO>.Map(unassignedRequests);

                return ResponseFactory<IEnumerable<RequestDTO>>.CreateSuccessResponse(unassignedRequestsDTO);
            }
            catch (ArgumentNullException argNullException)
            {
                return ResponseFactory<IEnumerable<RequestDTO>>.CreateNotFoundResponse(argNullException);
            }
            catch (Exception exception)
            {
                return ResponseFactory<IEnumerable<RequestDTO>>.CreateErrorResponse(exception);
            }
        }

        public async Task<IBaseResponse<IEnumerable<RequestDTO>>> GetClosedRequestsByOperatorId(string operatorId)
        {
            try
            {
                StringValidator.CheckIsNotNull(operatorId);

                var closedRequests = await _repository
                    .ReadAllAsync().Result
                    .Where(r => r.OperatorId == operatorId && r.RequestStatus == Status.Closed)
                    .ToListAsync();

                IEnumerable<RequestDTO> closedRequestsDTO = MapperHelperForDto<Request, RequestDTO>.Map(closedRequests);

                return ResponseFactory<IEnumerable<RequestDTO>>.CreateSuccessResponse(closedRequestsDTO);
            }
            catch (ArgumentNullException argNullException)
            {
                return ResponseFactory<IEnumerable<RequestDTO>>.CreateNotFoundResponse(argNullException);
            }
            catch (Exception exception)
            {
                return ResponseFactory<IEnumerable<RequestDTO>>.CreateErrorResponse(exception);
            }
        }

        public async Task<IBaseResponse<IEnumerable<RequestDTO>>> GetActiveRequestsByOperatorId(string operatorId)
        {
            try
            {
                StringValidator.CheckIsNotNull(operatorId);

                var activeRequests = await _repository
                    .ReadAllAsync().Result
                    .Where(r => r.OperatorId == operatorId && r.RequestStatus == Status.Active)
                    .ToListAsync();

                IEnumerable<RequestDTO> activeRequestsDTO = MapperHelperForDto<Request, RequestDTO>.Map(activeRequests);

                return ResponseFactory<IEnumerable<RequestDTO>>.CreateSuccessResponse(activeRequestsDTO);
            }
            catch (ArgumentNullException argNullException)
            {
                return ResponseFactory<IEnumerable<RequestDTO>>.CreateNotFoundResponse(argNullException);
            }
            catch (Exception exception)
            {
                return ResponseFactory<IEnumerable<RequestDTO>>.CreateErrorResponse(exception);
            }
        }

        public async Task<IBaseResponse<bool>> AssignRequestToTeam(Guid requestId, string teamId)
        {
            try
            {
                ObjectValidator<Guid>.CheckIsNotNullObject(requestId);
                StringValidator.CheckIsNotNull(teamId);

                var request = await _repository.ReadByIdAsync(requestId);

                request.TechTeamId = teamId;
                await _repository.UpdateAsync(request);

                return ResponseFactory<bool>.CreateSuccessResponse(true);
            }
            catch (ArgumentNullException argNullException)
            {
                return ResponseFactory<bool>.CreateNotFoundResponse(argNullException);
            }
            catch (Exception exception)
            {
                return ResponseFactory<bool>.CreateErrorResponse(exception);
            }
        }

        public async Task<IBaseResponse<bool>> AssignRequestToOperator(Guid requestId, string operatorId)
        {
            try
            {
                ObjectValidator<Guid>.CheckIsNotNullObject(requestId);
                StringValidator.CheckIsNotNull(operatorId);

                var request = await _repository.ReadByIdAsync(requestId);

                request.OperatorId = operatorId;
                await _repository.UpdateAsync(request);

                return ResponseFactory<bool>.CreateSuccessResponse(true);
            }
            catch (ArgumentNullException argNullException)
            {
                return ResponseFactory<bool>.CreateNotFoundResponse(argNullException);
            }
            catch (Exception exception)
            {
                return ResponseFactory<bool>.CreateErrorResponse(exception);
            }
        }

        public async Task<IBaseResponse<bool>> MarkRequestAsCompleted(Guid requestId)
        {
            try
            {
                ObjectValidator<Guid>.CheckIsNotNullObject(requestId);

                var request = await _repository.ReadByIdAsync(requestId);

                request.RequestStatus = Status.Completed;
                await _repository.UpdateAsync(request);

                return ResponseFactory<bool>.CreateSuccessResponse(true);
            }
            catch (ArgumentNullException argNullException)
            {
                return ResponseFactory<bool>.CreateNotFoundResponse(argNullException);
            }
            catch (Exception exception)
            {
                return ResponseFactory<bool>.CreateErrorResponse(exception);
            }
        }

        public async Task<IBaseResponse<bool>> AssignLocationToRequest(Guid requestId, Guid locationId)
        {
            try
            {
                ObjectValidator<Guid>.CheckIsNotNullObject(requestId);
                ObjectValidator<Guid>.CheckIsNotNullObject(locationId);

                var request = await _repository.ReadByIdAsync(requestId);
                var location = await _locationRepository.ReadByIdAsync(locationId);

                request.Location = location;
                await _repository.UpdateAsync(request);

                return ResponseFactory<bool>.CreateSuccessResponse(true);
            }
            catch (ArgumentNullException argNullException)
            {
                return ResponseFactory<bool>.CreateNotFoundResponse(argNullException);
            }
            catch (Exception exception)
            {
                return ResponseFactory<bool>.CreateErrorResponse(exception);
            }
        }

        public async Task<IBaseResponse<bool>> SetEcoBoxQuantityAndTemplate(Guid requestId, int quantity, Guid templateId)
        {
            try
            {
                ObjectValidator<Guid>.CheckIsNotNullObject(requestId);
                ObjectValidator<Guid>.CheckIsNotNullObject(templateId);

                var request = await _repository.ReadAllAsync().Result
                    .Include(r => r.Location)
                    .ThenInclude(location => location.EcoBoxes)
                    .FirstOrDefaultAsync(request => request.Id == requestId);

                if (request.Location != null && request.Location.EcoBoxes != null && request.Location.EcoBoxes.Any())
                {
                    var template = await _templateRepository.ReadByIdAsync(templateId);
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
            catch (ArgumentNullException argNullException)
            {
                return ResponseFactory<bool>.CreateNotFoundResponse(argNullException);
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

                var client = await _userManager.FindByIdAsync(newRequest.ClientId);
                newRequest.Client = client;

                newRequest.Id = Guid.NewGuid();
                
                await _repository.Create(newRequest);
                return ResponseFactory<bool>.CreateSuccessResponseWithId(true, newRequest.Id);
            }
            catch (ArgumentNullException argException)
            {
                return ResponseFactory<bool>.CreateNotFoundResponse(argException);
            }
            catch (Exception exception)
            {
                return ResponseFactory<bool>.CreateErrorResponse(exception);
            }
        }

        public async Task<IBaseResponse<bool>> ChangeRequestStatus(Guid requestId, Status newStatus)
        {
            try
            {
                ObjectValidator<Guid>.CheckIsNotNullObject(requestId);
                ObjectValidator<Status>.CheckIsNotNullObject(newStatus);

                var request = await _repository.ReadByIdAsync(requestId);

                request.RequestStatus = newStatus;
                await _repository.UpdateAsync(request);

                return ResponseFactory<bool>.CreateSuccessResponse(true);
            }
            catch (ArgumentNullException argNullException)
            {
                return ResponseFactory<bool>.CreateNotFoundResponse(argNullException);
            }
            catch (Exception exception)
            {
                return ResponseFactory<bool>.CreateErrorResponse(exception);
            }
        }
    }
}
