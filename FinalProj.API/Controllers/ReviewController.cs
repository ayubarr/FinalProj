using FinalApp.ApiModels.DTOs.EntitiesDTOs.RequestsDTO;
using FinalApp.Domain.Models.Entities.Requests.RequestsInfo;
using FinalApp.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IBaseRequestService<Review, ReviewDTO> _service;
        private readonly IReviewService _reviewService;
        public ReviewController(IBaseRequestService<Review, ReviewDTO> service, IReviewService reviewService)
        {
            _service = service;
            _reviewService = reviewService;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var response = _service.ReadAll();
            return Ok(response.Data);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var response = _service.ReadById(id);
            return Ok(response.Data);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteByIdAsync(id);
            return Ok();
        }

        [HttpGet("GetReviewsByEvaluation/{evaluation}")]
        public async Task<IActionResult> GetReviewsByEvaluation(int evaluation)
        {
            var response = await _reviewService.GetReviewsByEvaluation(evaluation);
            return Ok(response);
        }

        [HttpGet("GetReviewsByRequestId/{requestid}")]
        public async Task<IActionResult> GetReviewsByRequestId(Guid requestId)
        {
            var response = await _reviewService.GetReviewsByRequestId(requestId);
            return Ok(response);
        }

        [HttpPost("CreateReview")]
        public async Task<IActionResult> CreateReview(ReviewDTO review)
        {
            var response = await _reviewService.CreateReview(review);
            return Ok(response);
        }

        [HttpPost("CanCreateReview/{requestId}")]
        public async Task<IActionResult> CanCreateReview(Guid requestId)
        {
            var response = await _reviewService.CanCreateReview(requestId);
            return Ok(response);
        }

        [HttpPost("CanUpdateReview/{reviewId}")]
        public async Task<IActionResult> CanUpdateReview(Guid reviewId)
        {
            var response = await _reviewService.CanUpdateReview(reviewId);
            return Ok(response);
        }

        [HttpPut("UpdateReview")]
        public async Task<IActionResult> UpdateReview(ReviewDTO review)
        {
            var response = _reviewService.UpdateReview(review);
            return Ok(response);
        }

    }
}
