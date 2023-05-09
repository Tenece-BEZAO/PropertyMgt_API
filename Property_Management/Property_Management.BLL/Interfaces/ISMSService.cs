namespace Property_Management.BLL.Interfaces
{
    public interface ISMSService
    {
        Task<(bool, string)> SendSmsAsync(string number, string message);
    }
}
