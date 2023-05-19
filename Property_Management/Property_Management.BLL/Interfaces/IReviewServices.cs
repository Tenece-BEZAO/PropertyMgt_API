using Property_Management.BLL.DTOs.Requests;
using Property_Management.BLL.DTOs.Responses;

namespace Property_Management.BLL.Interfaces
{
    public interface IReviewServices
    {
        Task<Response> SaveReview(SaveReviewRequest saveReviewRequest);
        Task<IEnumerable<ReviewResponse>> GetAllReviews();
        Task<ReviewResponse> GetReview(string userId);
    }
}
