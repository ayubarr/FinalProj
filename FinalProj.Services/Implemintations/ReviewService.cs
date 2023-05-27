using FinalApp.ApiModels.DTOs.EntitiesDTOs.RequestsDTO;
using FinalApp.ApiModels.Response.Helpers;
using FinalApp.ApiModels.Response.Interfaces;
using FinalApp.DAL.Repository.Interfaces;
using FinalApp.Domain.Models.Entities.Requests.RequestsInfo;
using FinalApp.Domain.Models.Enums;
using FinalApp.Services.Interfaces;
using FinalApp.Services.Mapping.Helpers;
using FinallApp.ValidationHelper;
using Microsoft.EntityFrameworkCore;

namespace FinalApp.Services.Implemintations
{
    public class ReviewService : IReviewService
    {
        private const int MinEvaluation = 1;
        private const int MaxEvaluation = 10;
        private readonly IBaseAsyncRepository<Review> _repository;
        private readonly IBaseAsyncRepository<Request> _requestRepository;

        public ReviewService(IBaseAsyncRepository<Review> repository, IBaseAsyncRepository<Request> requestRepository)
        {
            _repository = repository;
            _requestRepository = requestRepository;
        }

        public async Task<IBaseResponse<IEnumerable<ReviewDTO>>> GetReviewsByEvaluation(int evaluation)
        {
            try
            {
                var reviews = await _repository.ReadAllAsync().Result
                    .Where(r => r.Evaluation == evaluation)
                    .ToListAsync();

                ObjectValidator<List<Review>>.CheckIsNotNullObject(reviews);
                IEnumerable<ReviewDTO> reviewsDTO = MapperHelperForDto<Review, ReviewDTO>.Map(reviews);

                return ResponseFactory<IEnumerable<ReviewDTO>>.CreateSuccessResponse(reviewsDTO);
            }
            catch (ArgumentNullException argNullException)
            {
                return ResponseFactory<IEnumerable<ReviewDTO>>.CreateNotFoundResponse(argNullException);
            }
            catch (Exception excpetion)
            {
                return ResponseFactory<IEnumerable<ReviewDTO>>.CreateErrorResponse(excpetion);
            }
        }

        public async Task<IBaseResponse<IEnumerable<ReviewDTO>>> GetReviewsByRequestId(int requestId)
        {
            try
            {
                var reviews = await _repository.ReadAllAsync().Result
                    .Where(request => request.Request.Id == requestId)
                    .ToListAsync();

                ObjectValidator<List<Review>>.CheckIsNotNullObject(reviews);
                IEnumerable<ReviewDTO> reviewsDTO = MapperHelperForDto<Review, ReviewDTO>.Map(reviews);

                return ResponseFactory<IEnumerable<ReviewDTO>>.CreateSuccessResponse(reviewsDTO);
            }
            catch (ArgumentNullException argNullException)
            {
                return ResponseFactory<IEnumerable<ReviewDTO>>.CreateNotFoundResponse(argNullException);
            }
            catch (Exception exception)
            {
                return ResponseFactory<IEnumerable<ReviewDTO>>.CreateErrorResponse(exception);
            }
        }

        public async Task<IBaseResponse<bool>> CanCreateReview(int requestId)
        {
            try
            {
                var request = await _requestRepository.ReadAllAsync().Result
                    .FirstOrDefaultAsync(r => r.Id == requestId);

                ObjectValidator<Request>.CheckIsNotNullObject(request);

                if (request.RequestStatus != Status.Completed || request.Review != null)
                    throw new InvalidOperationException("Unable to create a review for the specified request.");

                var canCreateReview = true;

                return ResponseFactory<bool>.CreateSuccessResponse(canCreateReview);
            }
            catch (InvalidOperationException invException)
            {
                return ResponseFactory<bool>.CreateInvalidOperationResponse(invException);
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

        public async Task<IBaseResponse<ReviewDTO>> CreateReview(int requestId, string reviewText, int evaluation)
        {
            try
            {
                StringValidator.CheckIsNotNull(reviewText);
                NumberValidator<int>.IsRange(evaluation, MinEvaluation, MaxEvaluation);

                Request request = await _requestRepository.ReadAllAsync().Result
                    .FirstOrDefaultAsync(r => r.Id == requestId);


                if (request == null || request.RequestStatus != Status.Completed || request.Review != null)
                    throw new InvalidOperationException("Unable to create review for the specified request.");


                var review = new Review
                {
                    ReviewText = reviewText,
                    Evaluation = evaluation
                };

                await _repository.Create(review);
                request.Review = review;


                return ResponseFactory<ReviewDTO>.CreateSuccessResponse(MapperHelperForDto<Review, ReviewDTO>.Map(review));
            }
            catch (InvalidOperationException invException)
            {
                return ResponseFactory<ReviewDTO>.CreateInvalidOperationResponse(invException);
            }
            catch (ArgumentNullException argNullException)
            {
                return ResponseFactory<ReviewDTO>.CreateNotFoundResponse(argNullException);
            }
            catch (Exception exception)
            {
                return ResponseFactory<ReviewDTO>.CreateErrorResponse(exception);
            }
        }

        public async Task<IBaseResponse<bool>> CanUpdateReview(int reviewId)
        {
            try
            {
                var review = await _repository.ReadByIdAsync(reviewId);

                ObjectValidator<Review>.CheckIsNotNullObject(review);

                var canUpdateReview = review != null && review.Request.RequestStatus == Status.Completed;

                return ResponseFactory<bool>.CreateSuccessResponse(canUpdateReview);
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

        public async Task<IBaseResponse<ReviewDTO>> UpdateReview(int reviewId, string reviewText, int evaluation)
        {
            try
            {
                StringValidator.CheckIsNotNull(reviewText);
                NumberValidator<int>.IsRange(evaluation, MinEvaluation, MaxEvaluation);

                var review = await _repository.ReadByIdAsync(reviewId);

                if (review == null || review.Request.RequestStatus != Status.Completed)
                    throw new InvalidOperationException("Unable to update the specified review.");

                review.ReviewText = reviewText;
                review.Evaluation = evaluation;

                await _repository.UpdateAsync(review);

                ObjectValidator<Review>.CheckIsNotNullObject(review);
                ReviewDTO reviewsDTO = MapperHelperForDto<Review, ReviewDTO>.Map(review);

                return ResponseFactory<ReviewDTO>.CreateSuccessResponse(reviewsDTO);
            }
            catch (InvalidOperationException invException)
            {
                return ResponseFactory<ReviewDTO>.CreateInvalidOperationResponse(invException);
            }
            catch (ArgumentNullException argNullException)
            {
                return ResponseFactory<ReviewDTO>.CreateNotFoundResponse(argNullException);
            }
            catch (Exception exception)
            {
                return ResponseFactory<ReviewDTO>.CreateErrorResponse(exception);
            }
        }
    }
}
