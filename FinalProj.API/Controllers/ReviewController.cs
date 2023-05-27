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
        public IActionResult Get(int id)
        {
            var response = _service.ReadById(id);
            return Ok(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ReviewDTO model)
        {
            await _service.CreateAsync(model);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(ReviewDTO model)
        {
            await _service.UpdateAsync(model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
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
        public async Task<IActionResult> GetReviewsByRequestId(int requestId)
        {
            var response = await _reviewService.GetReviewsByRequestId(requestId);
            return Ok(response);
        }

        [HttpPost("CreateReview/{requestId}/{reviewText}/{evaluation}")]
        public async Task<IActionResult> CreateReview(int requestId, string reviewText, int evaluation)
        {
            var response = await _reviewService.CreateReview(requestId, reviewText, evaluation);
            return Ok(response);
        }

        [HttpPost("CanCreateReview/{requestId}")]
        public async Task<IActionResult> CanCreateReview(int requestId)
        {
            var response = await _reviewService.CanCreateReview(requestId);
            return Ok(response);
        }

        [HttpPost("CanUpdateReview/{reviewId}")]
        public async Task<IActionResult> CanUpdateReview(int reviewId)
        {
            var response = await _reviewService.CanUpdateReview(reviewId);
            return Ok(response);
        }

        [HttpPut("UpdateReview/{requestId}/{reviewText}/{evaluation}")]
        public async Task<IActionResult> UpdateReview(int requestId, string reviewText, int evaluation)
        {
            var response = _reviewService.UpdateReview(requestId, reviewText, evaluation);
            return Ok(response);
        }

    }
}
