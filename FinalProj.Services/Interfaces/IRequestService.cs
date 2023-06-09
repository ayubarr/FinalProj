﻿using FinalProj.ApiModels.DTOs.EntitiesDTOs.RequestsDTO;
using FinalProj.ApiModels.Response.Interfaces;
using FinalProj.Domain.Models.Entities.Requests.RequestsInfo;
using FinalProj.Domain.Models.Enums;

namespace FinalProj.Services.Interfaces
{
    /// <summary>
    /// Interface for the request service that handles operations related to requests.
    /// </summary>
    public interface IRequestService
    {
        /// <summary>
        /// Retrieves a list of unassigned requests.
        /// </summary>
        /// <returns>An asynchronous operation that returns the list of unassigned requests.</returns>S
        public Task<IBaseResponse<IEnumerable<RequestDTO>>> GetUnassignedRequests();

        /// <summary>
        /// Retrieves a list of closed requests assigned to the specified operator.
        /// </summary>
        /// <param name="operatorId">The ID of the operator.</param>
        /// <returns>An asynchronous operation that returns the list of closed requests.</returns>
        public Task<IBaseResponse<IEnumerable<RequestDTO>>> GetClosedRequestsByOperatorId(string operatorId);

        /// <summary>
        /// Retrieves a list of active requests assigned to the specified operator.
        /// </summary>
        /// <param name="operatorId">The ID of the operator.</param>
        /// <returns>An asynchronous operation that returns the list of active requests.</returns>
        public Task<IBaseResponse<IEnumerable<RequestDTO>>> GetActiveRequestsByOperatorId(string operatorId);

        /// <summary>
        /// Assigns a request to a team.
        /// </summary>
        /// <param name="requestId">The ID of the request to assign.</param>
        /// <param name="teamId">The ID of the team to assign the request to.</param>
        /// <returns>An asynchronous operation that returns a response indicating the success or failure of the assignment.</returns>
        public Task<IBaseResponse<bool>> AssignRequestToTeam(Guid requestId, string teamId);

        /// <summary>
        /// Assigns a request to an operator.
        /// </summary>
        /// <param name="requestId">The ID of the request to assign.</param>
        /// <param name="operatorId">The ID of the operator to assign the request to.</param>
        /// <returns>An asynchronous operation that returns a response indicating the success or failure of the assignment.</returns>
        public Task<IBaseResponse<bool>> AssignRequestToOperator(Guid requestId, string operatorId);

        /// <summary>
        /// Marks a request as completed.
        /// </summary>
        /// <param name="requestId">The ID of the request to mark as completed.</param>
        /// <returns>An asynchronous operation that returns a response indicating the success or failure of marking the request as completed.</returns>
        public Task<IBaseResponse<bool>> MarkRequestAsCompleted(Guid requestId);

        /// <summary>
        /// Assigns a location to a request.
        /// </summary>
        /// <param name="requestId">The ID of the request to assign the location to.</param>
        /// <param name="locationId">The ID of the location to assign to the request.</param>
        /// <returns>An asynchronous operation that returns a response indicating the success or failure of assigning the location to the request.</returns>

        public Task<IBaseResponse<bool>> AssignLocationToRequest(Guid requestId, Guid locationId);
        /// <summary>
        /// Sets the number of ecoboxes and the template for the specified request.
        /// </summary>
        /// <param name="RequestId">Request ID.</param>
        /// <param name="quantity">The number of eco-boxes.</param>
        /// </// <param name="templates">Template ID.</param>
        /// <returns>An object representing the response to an operation with a Boolean value inside.</returns>
        
        public Task<IBaseResponse<bool>> SetEcoBoxQuantityAndTemplate(Guid requestId, int quantity, Guid templateId);
        /// <summary>
        /// Creates a new request.
        /// </summary>
        /// <param name="request">The request data transfer object (DTO) containing the details of the request to be created.</param>
        /// <returns>A task that represents the asynchronous operation and contains an <see cref="IBaseResponse{T}"/> where T is a boolean indicating the success or failure of the creation process.</returns>
        public Task<IBaseResponse<bool>> CreateRequest(RequestDTO request);
        /// <summary>
        /// Changes the status of a specific request.
        /// </summary>
        /// <param name="requestId">The unique identifier of the request.</param>
        /// <param name="newStatus">The new status to be assigned to the request.</param>
        /// <returns>A task that represents the asynchronous operation and contains an <see cref="IBaseResponse{T}"/> where T is a boolean indicating the success or failure of the status change.</returns>
       
        public Task<IBaseResponse<bool>> ChangeRequestStatus(Guid requestId, Status newStatus);
    }
}
