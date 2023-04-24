namespace Property_Management.BLL.DTOs.Requests
{
    public class EmailRequests
    {
        public string? EmailPassword { get; set; }
        public string EmailBody { get; set; }
        public int EmailPort { get; set; } = 465;
        public string Subject { get; set; }
        public string To { get; set; }
        public string HostEmail { get; set; }
        public string From { get; set; }
    }

    public class SendBulkEmailRequest : EmailRequests
    {
       public IEnumerable<string> Recievers { get; set; }
    }
}
