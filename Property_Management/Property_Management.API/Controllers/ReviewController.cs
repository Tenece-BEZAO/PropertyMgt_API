using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Property_Management.BLL.DTOs.Requests;
using Property_Management.BLL.DTOs.Responses;
using Property_Management.BLL.Infrastructure;
using Property_Management.BLL.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace Property_Management.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewServices _reviewServices;
        public ReviewController(IReviewServices reviewServices)
        {
            _reviewServices = reviewServices;
        }


        [HttpPost("new-review")]
        [SwaggerOperation(Summary = "save new review", Description = "save new review")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Your review has been saved.", Type = typeof(Response))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "Object value cannot be empty.", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> SaveReview(SaveReviewRequest saveReviewRequest)
        {
            Response response = await _reviewServices.SaveReview(saveReviewRequest);
            return Ok(response);
        }

        [HttpGet("reviews")]
        [SwaggerOperation(Summary = "get all review", Description = "get all review")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Fetched reviews.", Type = typeof(Response))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "No review found.", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetAllReviews()
        {
            IEnumerable<ReviewResponse> response = await _reviewServices.GetAllReviews();
            return Ok(response);
        }

        [HttpGet("review")]
        [SwaggerOperation(Summary = "get single review", Description = "get single review")]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Fetched reviews", Type = typeof(Response))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, 
            Description = "This user has not submitted any review or review rate might be less than '3'...", Type = typeof(ErrorResponse))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "It's not you, it's us", Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetReview(string userId)
        {
            ReviewResponse response = await _reviewServices.GetReview(userId);
            return Ok(response);
        }
    }
}
