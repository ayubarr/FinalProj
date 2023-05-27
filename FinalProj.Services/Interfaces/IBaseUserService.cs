using FinalApp.ApiModels.DTOs.EntitiesDTOs.RequestsDTO;
using FinalApp.ApiModels.Response.Interfaces;
using FinalApp.Domain.Models.Abstractions.BaseUsers;

namespace FinalApp.Services.Interfaces
{
    /// <summary>
    /// Interface for a base user service that provides CRUD operations and additional functionality for a specific user type.
    /// </summary>
    /// <typeparam name="T">The type of the user, derived from ApplicationUser.</typeparam>
    public interface IBaseUserService<T>
        where T : ApplicationUser
    {
        /// <summary>
        /// Sets the specified user as an admin.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the response indicating the success or failure of the operation.</returns>
        Task<IBaseResponse<bool>> SetUserAsAdminAsync(string userId);

        /// <summary>
        /// Gets a list of active requests for the specified user.
        /// </summary>
        /// <param name="Id">The ID of the user.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the response with the list of active requests or an error if the operation fails.</returns>
        Task<IBaseResponse<IEnumerable<RequestDTO>>> GetActiveRequests(string Id);

        /// <summary>
        /// Gets a list of closed requests for the specified user.
        /// </summary>
        /// <param name="Id">The ID of the user.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the response with the list of closed requests or an error if the operation fails.</returns>
        Task<IBaseResponse<IEnumerable<RequestDTO>>> GetClosedRequests(string Id);

        /// <summary>
        /// Accepts the specified request.
        /// </summary>
        /// <param name="requestId">The ID of the request.</param>
        /// <param name="Id">The ID of the user.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the response indicating the success or failure of the operation.</returns>
        Task<IBaseResponse<bool>> AcceptRequest(Guid requestId, string Id);

        /// <summary>
        /// Marks the specified request as completed.
        /// </summary>
        /// <param name="requestId">The ID of the request.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the response indicating the success or failure of the operation.</returns>
        Task<IBaseResponse<bool>> MarkRequestAsCompleted(Guid requestId);

        /// <summary>
        /// Closes the specified request by the user.
        /// </summary>
        /// <param name="requestId">The ID of the request.</param>
        /// <param name="Id">The ID of the user.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the response indicating the success or failure of the operation.</returns>
        Task<IBaseResponse<bool>> CloseRequestByUser(Guid requestId, string Id);

        /// <summary>
        /// Creates a new user with the specified details and password.
        /// </summary>
        /// <param name="user">The user object containing the details.</param>
        /// <param name="password">The password for the user.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the response indicating the success or failure of the operation.</returns>
        Task<IBaseResponse<bool>> CreateAsync(T user, string password);

        /// <summary>
        /// Retrieves all users of the specified type.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. The task result contains the response with the list of users or an error if the operation fails.</returns>
        Task<IBaseResponse<IEnumerable<T>>> ReadAllAsync();

        /// <summary>
        /// Retrieves a user by their ID.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the response with the user object or an error if the operation fails.</returns>
        Task<IBaseResponse<T>> ReadByIdAsync(string userId);

        /// <summary>
        /// Updates the details of the specified user.
        /// </summary>
        /// <param name="user">The user object containing the updated details.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the response indicating the success or failure of the operation.</returns>
        Task<IBaseResponse<bool>> UpdateAsync(T user);

        /// <summary>
        /// Deletes the specified user.
        /// </summary>
        /// <param name="user">The user object to delete.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the response indicating the success or failure of the operation.</returns>
        Task<IBaseResponse<bool>> DeleteAsync(T user);
    }

}
