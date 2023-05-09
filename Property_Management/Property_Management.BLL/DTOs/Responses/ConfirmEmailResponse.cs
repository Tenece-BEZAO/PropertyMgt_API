namespace Property_Management.BLL.DTOs.Responses
{
    public class ConfirmEmailResponse
    {
        public string Email { get; set; }
        public string UserId { get; set; }
        public string Token { get; set; }
        public string UserName { get; set; }
        public bool TwoFactorAuth { get; set; }
    }
}
