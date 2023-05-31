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

namespace FinalProj.Services.Implemintations.RequestServices
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
                NumberValidator<int>.IsRange(evaluation, MinEvaluation, MaxEvaluation);

                var reviews = await _repository.ReadAllAsync().Result
                    .Where(r => r.Evaluation == evaluation)
                    .ToListAsync();

                IEnumerable<ReviewDTO> reviewsDTO = MapperHelperForDto<Review, ReviewDTO>.Map(reviews);

                return ResponseFactory<IEnumerable<ReviewDTO>>.CreateSuccessResponse(reviewsDTO);
            }
            catch (ArgumentNullException argNullException)
            {
                return ResponseFactory<IEnumerable<ReviewDTO>>.CreateNotFoundResponse(argNullException);
            }
            catch (ArgumentException argException)
            {
                return ResponseFactory<IEnumerable<ReviewDTO>>.CreateNotFoundResponse(argException);
            }
            catch (Exception excpetion)
            {
                return ResponseFactory<IEnumerable<ReviewDTO>>.CreateErrorResponse(excpetion);
            }
        }

        public async Task<IBaseResponse<IEnumerable<ReviewDTO>>> GetReviewsByRequestId(Guid requestId)
        {
            try
            {
                ObjectValidator<Guid>.CheckIsNotNullObject(requestId);

                var reviews = await _repository.ReadAllAsync().Result
                    .Where(request => request.Request.Id == requestId)
                    .ToListAsync();

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

        public async Task<IBaseResponse<bool>> CanCreateReview(Guid requestId)
        {
            try
            {
                ObjectValidator<Guid>.CheckIsNotNullObject(requestId);
                var request = await _requestRepository.ReadAllAsync().Result
                    .FirstOrDefaultAsync(r => r.Id == requestId);

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

        public async Task<IBaseResponse<bool>> CreateReview(ReviewDTO reviewDto)
        {
            try
            {
                ObjectValidator<ReviewDTO>.CheckIsNotNullObject(reviewDto);
                var newReview = MapperHelperForEntity<ReviewDTO, Review>.Map(reviewDto);

                var request = await _requestRepository.ReadAllAsync().Result
                    .FirstOrDefaultAsync(request => request.Id == reviewDto.requestId);

                if (request == null || request.RequestStatus != Status.Completed || request.Review != null)
                    throw new InvalidOperationException("Unable to create a review for the specified request.");

                newReview.Id = Guid.NewGuid();

                request.ReviewId = newReview.Id;
                request.Review = newReview;

                await _repository.Create(newReview);
        
                await _requestRepository.UpdateAsync(request);


                return ResponseFactory<bool>.CreateSuccessResponseWithId(true, newReview.Id);
            }
            catch (InvalidOperationException invException)
            {
                return ResponseFactory<bool>.CreateInvalidOperationResponse(invException);
            }
            catch (ArgumentNullException argNullException)
            {
                return ResponseFactory<bool>.CreateNotFoundResponse(argNullException);
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

        public async Task<IBaseResponse<bool>> CanUpdateReview(Guid reviewId)
        {
            try
            {
                ObjectValidator<Guid>.CheckIsNotNullObject(reviewId);
                var review = await _repository.ReadByIdAsync(reviewId);

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

        public async Task<IBaseResponse<bool>> UpdateReview(ReviewDTO reviewDto)
        {
            try
            {
                ObjectValidator<ReviewDTO>.CheckIsNotNullObject(reviewDto);
                var newReview = MapperHelperForEntity<ReviewDTO, Review>.Map(reviewDto);

                var request = await _requestRepository.ReadAllAsync().Result
                    .FirstOrDefaultAsync(request => request.Id == reviewDto.requestId);

                if (request == null || request.RequestStatus != Status.Completed || request.Review != null)
                    throw new InvalidOperationException("Unable to create a review for the specified request.");

                newReview.Id = Guid.NewGuid();

                request.ReviewId = newReview.Id;
                request.Review = newReview;

                await _repository.UpdateAsync(newReview);

                await _requestRepository.UpdateAsync(request);

                return ResponseFactory<bool>.CreateSuccessResponseWithId(true, newReview.Id);
            }
            catch (InvalidOperationException invException)
            {
                return ResponseFactory<bool>.CreateInvalidOperationResponse(invException);
            }
            catch (ArgumentNullException argNullException)
            {
                return ResponseFactory<bool>.CreateNotFoundResponse(argNullException);
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
