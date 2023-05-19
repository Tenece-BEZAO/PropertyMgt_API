namespace Property_Management.DAL.Entities
{
    public class NewsLetter
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Email { get; set; }
        public DateTime SubcribedDate { get; set; } = DateTime.UtcNow;
    }
}
