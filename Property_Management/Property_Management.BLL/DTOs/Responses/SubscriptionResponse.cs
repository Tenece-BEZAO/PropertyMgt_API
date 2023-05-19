namespace Property_Management.BLL.DTOs.Responses
{
    public class SubscriptionResponse
    {
        public bool UserExist { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
    }
}
