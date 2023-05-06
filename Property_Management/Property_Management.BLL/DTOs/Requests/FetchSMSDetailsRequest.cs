namespace Property_Management.BLL.DTOs.Requests
{
    public class FetchSMSDetailsRequest
    {
        public string SMSAccountIdentification { get; set; }
        public string SMSAccountPassword { get; set; }
        public string SMSAccountFrom { get; set; }
    }
}
