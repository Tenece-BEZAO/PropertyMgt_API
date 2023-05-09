namespace Property_Management.BLL.DTOs.Responses
{
    public class EmailResponse
    {
        public string SenderEmail { get; set; }
        public string ReceiverEmail { get; set; }
        public IEnumerable<string> Receivers { get; set; }
        public string Subject { get; set; }
        public string Date { get; set; }
        public bool Sent { get; set; }
    }
}
