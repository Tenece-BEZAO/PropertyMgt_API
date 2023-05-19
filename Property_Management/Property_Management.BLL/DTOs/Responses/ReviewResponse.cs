namespace Property_Management.BLL.DTOs.Responses
{
    public class ReviewResponse
    {
        public string UserName { get; set; }
        public DateTime SubmitedDate { get; set; }
        public string ReviewMsg { get; set; }
        public double Rating { get; set; }
    }
}
