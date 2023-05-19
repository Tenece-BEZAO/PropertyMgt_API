using Property_Management.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace Property_Management.BLL.DTOs.Requests
{
    public class SaveReviewRequest
    {
        [Required]
        public string UserId { get; set; }
        public DateTime SubmitedDate { get; set; } = DateTime.UtcNow;
        [Required]
        public string ReviewMsg { get; set; }
        public double Rating { get; set; }
    }
}
