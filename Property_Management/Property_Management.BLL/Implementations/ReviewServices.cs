using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Property_Management.BLL.DTOs.Requests;
using Property_Management.BLL.DTOs.Responses;
using Property_Management.BLL.Interfaces;
using Property_Management.DAL.Entities;
using Property_Management.DAL.Interfaces;

namespace Property_Management.BLL.Implementations
{
    public class ReviewServices : IReviewServices
    {
        private readonly IRepository<Review> _reviewRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ReviewServices(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _reviewRepo = _unitOfWork.GetRepository<Review>();
        }

        public async Task<Response> SaveReview(SaveReviewRequest saveReviewRequest)
        {
            if (saveReviewRequest == null)
                throw new InvalidOperationException("Object cannot be empty.");

            Review newReview = _mapper.Map<Review>(saveReviewRequest);
            await _reviewRepo.AddAsync(newReview);

            return new Response
            {
                Action = "Review Creation",
                StatusCode = 201,
                Message = $"Thanks for review.",
            };
        }

        public async Task<IEnumerable<ReviewResponse>> GetAllReviews()
        {
            double minimumAllowedReview = 3d;
            IEnumerable<Review> fetchedReview = await _reviewRepo.GetByAsync
                (predicate: rev => rev.Rating > minimumAllowedReview, include: re => re.Include(r => r.User));

            if (fetchedReview == null)
                throw new InvalidOperationException("No review found...");

          return fetchedReview.Select(review => new ReviewResponse
            {
                UserName = review.User.UserName,
                ReviewMsg = review.ReviewMsg,
                Rating = review.Rating,
                SubmitedDate = review.SubmitedDate
            });
        }

        public async Task<ReviewResponse> GetReview(string userId)
        {
            double minimumAllowedReview = 3d;
            Review fetchedReview = await _reviewRepo.GetSingleByAsync
                (predicate: rev => rev.UserId == userId && rev.Rating > minimumAllowedReview, 
                include: re => re.Include(r => r.User));

            if (fetchedReview == null)
                throw new InvalidOperationException("This user has not submitted any review or review rate might be less than '3'...");

            return new ReviewResponse
            {
                UserName = fetchedReview.User.UserName,
                ReviewMsg = fetchedReview.ReviewMsg,
                Rating = fetchedReview.Rating,
                SubmitedDate = fetchedReview.SubmitedDate
            };
        }
    }
}
