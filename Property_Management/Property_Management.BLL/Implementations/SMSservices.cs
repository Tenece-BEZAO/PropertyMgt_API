using Microsoft.Extensions.Configuration;
using Property_Management.BLL.Interfaces;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Property_Management.BLL.Implementations
{

    // This class is used by the application to send Email and SMS
    // when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link https://go.microsoft.com/fwlink/?LinkID=532713
    public class SMSservices : ISMSService
    {
        private readonly IConfiguration _configuration;
        public SMSservices(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public async Task<(bool, string)> SendSmsAsync(string phone, string message)
        {
            try
            {
                string? accountSid = _configuration["FetchSMSDetailsRequest:SMSAccountIdentification"];
                string? authToken = _configuration["FetchSMSDetailsRequest:SMSAccountPassword"];
                string? SMSAccountFrom = _configuration["FetchSMSDetailsRequest:SMSAccountFrom"];

                TwilioClient.Init(accountSid, authToken);
                await MessageResource.CreateAsync(
                 to: new PhoneNumber($"+234{phone}"),
                 from: new PhoneNumber(SMSAccountFrom),
                 body: message);
                return (true, $"SMS sent to {phone}");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
    }
}
