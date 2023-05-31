using FinalApp.ApiModels.DTOs.EntitiesDTOs.RequestsDTO;
using FinalApp.ApiModels.Response.Interfaces;
using FinalApp.Domain.Models.Entities.Requests.RequestsInfo;

namespace FinalApp.Services.Interfaces
{
    /// <summary>
    /// Represents a service for managing reviews.
    /// </summary>
    public interface IReviewService
    {
        /// <summary>
        /// Retrieves reviews based on the specified evaluation.
        /// </summary>
        /// <param name="evaluation">The evaluation value to filter reviews.</param>
        /// <returns>An asynchronous operation that returns the collection of reviews.</returns>
        public Task<IBaseResponse<IEnumerable<ReviewDTO>>> GetReviewsByEvaluation(int evaluation);

        /// <summary>
        /// Retrieves reviews based on the specified request ID.
        /// </summary>
        /// <param name="requestId">The ID of the request to filter reviews.</param>
        /// <returns>An asynchronous operation that returns the collection of reviews.</returns>
        public Task<IBaseResponse<IEnumerable<ReviewDTO>>> GetReviewsByRequestId(Guid requestId);

        /// <summary>
        /// Checks if a review can be created for the specified request.
        /// </summary>
        /// <param name="requestId">The ID of the request to check.</param>
        /// <returns>An asynchronous operation that returns a boolean indicating if a review can be created.</returns>
        public Task<IBaseResponse<bool>> CanCreateReview(Guid requestId);

        /// <summary>
        /// Creates a new review for the specified request.S
        /// </summary>
        /// <param name="requestId">The ID of the request to create a review for.</param>
        /// <param name="reviewText">The review text.</param>
        /// <param name="evaluation">The evaluation value for the review.</param>S
        /// <returns>An asynchronous operation that returns the created review.</returns>
        public Task<IBaseResponse<bool>> CreateReview(ReviewDTO review);

        /// <summary>
        /// Checks if a review can be updated.
        /// </summary>
        /// <param name="reviewId">The ID of the review to check.</param>
        /// <returns>An asynchronous operation that returns a boolean indicating if a review can be updated.</returns>
        public Task<IBaseResponse<bool>> CanUpdateReview(Guid reviewId);

        /// <summary>
        /// Updates an existing review.
        /// </summary>
        /// <param name="reviewId">The ID of the review to update.</param>
        /// <param name="reviewText">The updated review text.</param>
        /// <param name="evaluation">The updated evaluation value for the review.</param>
        /// <returns>An asynchronous operation that returns the updated review.</returns>
        public Task<IBaseResponse<bool>> UpdateReview(ReviewDTO review);
    }

}
