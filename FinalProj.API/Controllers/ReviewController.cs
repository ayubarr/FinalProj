using FinalApp.ApiModels.DTOs.EntitiesDTOs.RequestsDTO;
using FinalApp.Domain.Models.Entities.Requests.RequestsInfo;
using FinalApp.Domain.Models.Enums;
using FinalApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

        //[Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        public IActionResult Get()
        {
            var response = _service.ReadAll();
            return Ok(response.Data);
        }

        //[Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var response = _service.ReadById(id);
            return Ok(response.Data);
        }

        //[Authorize(AuthenticationSchemes = "Bearer")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteByIdAsync(id);
            return Ok();
        }

        //[Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("GetReviewsByEvaluation/{evaluation}")]
        public async Task<IActionResult> GetReviewsByEvaluation(int evaluation)
        {
            var response = await _reviewService.GetReviewsByEvaluation(evaluation);
            return Ok(response);
        }

        //[Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("GetReviewsByRequestId/{requestid}")]
        public async Task<IActionResult> GetReviewsByRequestId(Guid requestId)
        {
            var response = await _reviewService.GetReviewsByRequestId(requestId);
            return Ok(response);
        }

        //[Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("CreateReview")]
        public async Task<IActionResult> CreateReview(ReviewDTO review)
        {
            var response = await _reviewService.CreateReview(review);
            return Ok(response);
        }

        //[Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("CanCreateReview/{requestId}")]
        public async Task<IActionResult> CanCreateReview(Guid requestId)
        {
            var response = await _reviewService.CanCreateReview(requestId);
            return Ok(response);
        }

        //[Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("CanUpdateReview/{reviewId}")]
        public async Task<IActionResult> CanUpdateReview(Guid reviewId)
        {
            var response = await _reviewService.CanUpdateReview(reviewId);
            return Ok(response);
        }

        //[Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut("UpdateReview")]
        public async Task<IActionResult> UpdateReview(ReviewDTO review)
        {
            var response = _reviewService.UpdateReview(review);
            return Ok(response);
        }

    }
}
