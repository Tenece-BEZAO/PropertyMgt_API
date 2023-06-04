using Microsoft.Extensions.Configuration;
using Property_Management.BLL.DTOs.Requests;
using Property_Management.BLL.DTOs.Responses;
using Property_Management.BLL.Interfaces;
using Property_Management.DAL.Entities;
using Property_Management.DAL.Interfaces;

namespace Property_Management.BLL.Implementations
{
    public class SendMailService : ISendMailService
    {
        private readonly IEmailServices _emailServices;
        private readonly IConfiguration _configuration;
        private readonly string? _mailPWD;
        private readonly string? _hostEmail;
        private readonly string? _senderEmail;
        private readonly string? _clientUrl;
        private readonly string? _serverUrl;


        public SendMailService(IEmailServices emailServices, IConfiguration configuration)
        {
            _emailServices = emailServices;
            _configuration = configuration;
            _mailPWD = _configuration["Email:MPWD"];
            _hostEmail = _configuration["Email:HostEmail"];
            _senderEmail = _configuration["Email:From"];
            _clientUrl = _configuration["App:ClientUrl"];
            _serverUrl = _configuration["App:ServerUrl"];
        }
        public async Task<EmailResponse> SendTwoFactorAuthenticationEmailAsync(ConfirmEmailResponse response)
        {
            string emailBody = @$"Hey! {response.UserName} <hr /> <h3> Use this code <strong>{response.Token}</strong> to login to your account </h3>";
            EmailRequests email = new EmailRequests
            {
                EmailPassword = _mailPWD,
                EmailBody = emailBody,
                EmailPort = 465,
                Subject = "2FA Authentication.",
                To = response.Email,
                HostEmail = _hostEmail,
                From = _senderEmail,
            };

            EmailResponse emailResponse = await _emailServices.SendMailAsync(email);
            return emailResponse;
        }

        public async Task<EmailResponse> SendEmailPasswordResetMailAsync(ApplicationUser user, string link)
        {
            string styles = @"height: 3rem; width: 6rem; text-align: center; padding: 0.8rem; text-decoration: none; background-color: teal; border-radius: 5px; color: white;";
            string emailBody = @$"Hey! {user.UserName} <hr /> <h3>We got a request to recet your password; click on this button </h3> <a style='{styles}' href='{link}' >Reset Password </a> to recet your password. <hr /> <h4> If this is not done by you please ignore this email. </h4>";

            EmailRequests email = new EmailRequests
            {
                EmailPassword = _mailPWD,
                EmailBody = emailBody,
                EmailPort = 465,
                Subject = "Password Recet",
                To = user.Email,
                HostEmail = _hostEmail,
                From = _senderEmail,
            };

            EmailResponse response = await _emailServices.SendMailAsync(email);
            return response;
        }

        public async Task<EmailResponse> LeaseAcceptanceMailAsync(LandLord landlord, Tenant tenant, Property property, string message)
        {
            string propertyImage = property.Image;
            string tenantEmailBody = @$"<h2>Hi {tenant.FirstName}!</h2> <hr /> <br /> <h5>The lease with the property {property.Name} was {message}.</h5> <br /> <hr /> <a href={_clientUrl}> <img src={propertyImage} /></a>";
            EmailRequests email = new EmailRequests
            {
                EmailPassword = _mailPWD,
                EmailBody = tenantEmailBody,
                EmailPort = 465,
                Subject = "Lease Acceptance.",
                To = tenant.Email,
                HostEmail = _hostEmail,
                From = _senderEmail,
            };

            string landlordEmailBody = @$"<h2>Hey! {landlord.FirstName}!</h2> <hr /> <br /> <h5> Your lease was {message} by this user {tenant.FirstName} with email {tenant.Email} on {DateTime.UtcNow.ToLongDateString()}.</h5> <br /> <hr /> <a href={_clientUrl}> <img src={propertyImage} /></a>";
            EmailRequests notifyLandlord = new EmailRequests
            {
                EmailPassword = _mailPWD,
                EmailBody = landlordEmailBody,
                EmailPort = 465,
                Subject = "2FA Authentication.",
                To = landlord.Email,
                HostEmail = _hostEmail,
                From = _senderEmail,
            };
            EmailResponse response = await _emailServices.SendMailAsync(email);
            await _emailServices.SendMailAsync(notifyLandlord);
            return response;
        }


        public async Task<EmailResponse> RentExpireMailAsync(Tenant tenant, string message, bool expiredRent)
        {
            string expiredImgUrl = "https://th.bing.com/th/id/OIP.aCm1MyQtBVGip1CRX-IBIwHaDb?pid=ImgDet&rs=1";
            string ImgUrl = "https://th.bing.com/th/id/OIP.aCm1MyQtBVGip1CRX-IBIwHaDb?pid=ImgDet&rs=1";
            string emailBody = expiredRent ? @$"<h2>Hi {tenant.FirstName}!</h2> <hr /> <br /><h5> {message}</h5> <br /> <hr /> <a href={_clientUrl}> <img src={expiredImgUrl} /></a>"
                : @$"<h5> <h2>Hi {tenant.FirstName}!</h2> <hr /> <br /> {message}</h5> <br /> <hr /> <a href={_clientUrl}> <img src={ImgUrl} /></a>";
            EmailRequests email = new EmailRequests
            {
                EmailPassword = _mailPWD,
                Subject = expiredRent ? "Rent Expired" : "Rent is still active.",
                To = tenant.Email,
                EmailBody = emailBody,
                EmailPort = 465,
                HostEmail = _hostEmail,
                From = _senderEmail,
            };
            EmailResponse response = await _emailServices.SendMailAsync(email);
            return response;
        }

        public async Task<EmailResponse> UserCreatedMailAsync(ConfirmEmailResponse response)
        {
            string imgUrl = "https://codeconvey.com/wp-content/uploads/2020/06/registration-successful-emailBody-html.png";
            string callbackURL = $"{_serverUrl}/api/auth/confirm-email?id={response.UserId}&token={response.Token}";
            string emailBody = @$"<h2>Hi {response.UserName}!</h2> <hr /> <br /> <h5>Thanks for creating you account with us. Click Continue to Verify your email address.</h5> <br /> <hr /> <a href={callbackURL}> <img src={imgUrl} /></a> ";
            EmailRequests email = new EmailRequests
            {
                EmailPassword = _mailPWD,
                EmailBody = emailBody,
                EmailPort = 465,
                Subject = $"Welcome {response.UserName}",
                To = response.Email,
                HostEmail = _hostEmail,
                From = _senderEmail,
            };
            EmailResponse emailResponse = await _emailServices.SendMailAsync(email);
            return emailResponse;
        }

        public async Task<EmailResponse> RecetPasswordSuccessMailAsync(ApplicationUser user, string message)
        {
            string imgUrl = "companylogo.png";
            string logo = "company-logo.png";
            string emailBody = @$"<h2>Hi {user.NormalizedUserName}!</h2> <hr /> <br /><h5>{message}</h5> <br /> <hr /> <a href={_serverUrl}>
                     <img src={imgUrl} alt={logo} /></a> ";
            EmailRequests email = new EmailRequests
            {
                EmailPassword = _mailPWD,
                EmailBody = emailBody,
                EmailPort = 465,
                Subject = "Password Recet Succesful.",
                To = user.Email,
                HostEmail = _hostEmail,
                From = _senderEmail,
            };
            EmailResponse response = await _emailServices.SendMailAsync(email);
            return response;
        }

        public async Task<EmailResponse> PaymentVerifiedMailAsync(ApplicationUser user, string message)
        {
            string imgUrl = "https://msmeafricaonline.com/wp-content/uploads/2020/10/Paystack-696x392.jpg";
            string emailBody = @$"<h2>Hi {user.NormalizedUserName}!</h2> <hr /> <br /> <h5>Your payment was successfull. Thanks! for using our services and we hope to serve you better. {message}</h5> <br /> <hr /> <a href={_serverUrl}> <img src={imgUrl} /></a> ";
            EmailRequests email = new EmailRequests
            {
                EmailPassword = _mailPWD,
                EmailBody = emailBody,
                EmailPort = 465,
                Subject = "Payment Verified",
                To = user.Email,
                HostEmail = _hostEmail,
                From = _senderEmail,
            };
            EmailResponse response = await _emailServices.SendMailAsync(email);
            return response;
        }

        public async Task<EmailResponse> SendRentedPropEmailAsync(Tenant tenant, Property property, Unit unit)
        {
            string emailBody = @$"<h2>Hey! {tenant.FirstName}!</h2> <hr /> <br /> <h5>Thanks for purchasing our property. You have successfully rented <br /> Name: {property.Name} <br /> Price: {property.Price} <br /> Unit: {unit.Name}</h5> <br /> <hr /> <a href={_clientUrl}>Vicit Site.</a> ";
            EmailRequests email = new EmailRequests
            {
                EmailPassword = _mailPWD,
                EmailBody = emailBody,
                EmailPort = 465,
                Subject = "Payment Verified",
                To = tenant.Email,
                HostEmail = _hostEmail,
                From = _senderEmail,
            };
            EmailResponse response = await _emailServices.SendMailAsync(email);
            return response;
        }
    }
}
