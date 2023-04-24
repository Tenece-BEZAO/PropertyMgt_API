namespace Property_Management.DAL.Entities
{
    public class Email
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string SenderEmail { get; set; }
        public string ReceiverEmail { get; set; }
        public string EmailBody { get; set; }
        public string Subject { get; set; }
        public DateTime SendDate { get; set; }
        public bool IsEmailSent { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
    }
}
