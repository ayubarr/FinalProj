using FinalProj.ApiModels.DTOs.EntitiesDTOs.RequestsDTO;
using FinalProj.ApiModels.Response.Interfaces;

namespace FinalProj.Services.Interfaces
{
    /// <summary>
    /// Represents a service that handles the history of request statuses.
    /// </summary>
    public interface IRequestHistoryService
    {
        /// <summary>
        /// Retrieves the status history of a specific request.
        /// </summary>
        /// <param name="requestId">The unique identifier of the request.</param>
        /// <returns>A task that represents the asynchronous operation and contains an <see cref="IBaseResponse{T}"/> where T is an enumerable collection of <see cref="RequestStatusHistoryDTO"/>.</returns>
        Task<IBaseResponse<IEnumerable<RequestStatusHistoryDTO>>> GetRequestHistoryStatus(Guid requestId);
    }
}
