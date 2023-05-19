namespace Property_Management.DAL.Entities
{
    public class Review
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public DateTime SubmitedDate { get; set; } = DateTime.UtcNow;
        public string ReviewMsg { get; set; }
        public double Rating { get; set; }
    }
}
