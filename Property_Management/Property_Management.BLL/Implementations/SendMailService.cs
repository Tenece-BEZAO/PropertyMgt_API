﻿using Microsoft.Extensions.Configuration;
using Property_Management.BLL.DTOs.Requests;
using Property_Management.BLL.DTOs.Responses;
using Property_Management.BLL.Interfaces;
using Property_Management.DAL.Entities;
using Property_Management.DAL.Interfaces;
using Twilio.Jwt.AccessToken;

namespace Property_Management.BLL.Utilities
{
    public class SendMailService : ISendMailService
    {
        private readonly IEmailServices _emailServices;
        private readonly IConfiguration _configuration;
        private readonly string? _mailPWD;


        public SendMailService(IEmailServices emailServices, IConfiguration configuration)
        {
            _emailServices = emailServices;
            _configuration = configuration;
            _mailPWD = _configuration.GetSection("MPWD").Value;
        }

        public async Task<EmailResponse> SendTwoFactorAuthenticationEmailAsync(ConfirmEmailResponse response)
        {
            string message = @$"Hey! {response.UserName} <hr /> <h3> Use this code <strong>{response.Token}</strong> to login to your account </h3>";
            EmailRequests email = new EmailRequests
            {
                EmailPassword = _mailPWD,
                EmailBody = message,
                EmailPort = 465,
                Subject = "2FA Authentication.",
                To = response.Email,
                HostEmail = "smtp.mail.yahoo.com",
                From = "kellyncode@yahoo.com"
            };

            EmailResponse emailResponse = await _emailServices.SendMailAsync(email);
            return emailResponse;
        }

        public async Task<EmailResponse> SendEmailPasswordResetMailAsync(ApplicationUser user, string link)
        {
            string styles = @"height: 3rem; width: 6rem; text-align: center; padding: 0.8rem; text-decoration: none; background-color: teal; border-radius: 5px; color: white;";
            string message = @$"Hey! {user.UserName} <hr /> <h3>We got a request to recet your password; click on this button </h3> <a style='{styles}' href='{link}' >Reset Password </a> to recet your password. <hr /> <h4> If this is not done by you please ignore this message. </h4>";

            EmailRequests email = new EmailRequests
            {
                EmailPassword = _mailPWD,
                EmailBody = message,
                EmailPort = 465,
                Subject = "Password Reset.",
                To = user.Email,
                HostEmail = "smtp.mail.yahoo.com",
                From = "kellyncode@yahoo.com"
            };

            EmailResponse response = await _emailServices.SendMailAsync(email);
            return response;
        }

        public async Task<EmailResponse> LeaseAcceptanceMailAsync(LandLord landlord, Tenant tenant, Property property, string message)
        {
            string propertyImage = property.Image;
            string serverUrl = "https://localhost:7258/swagger/index.html";
            EmailRequests email = new EmailRequests 
            {
                EmailPassword = _mailPWD,
                EmailBody = @$"<h2>Hi {tenant.FirstName}!</h2> <hr /> <br /> <h5>The lease with the property {property.Name} was {message}.</h5> <br /> <hr /> <a href={serverUrl}> <img src={propertyImage} /></a>",
                EmailPort = 465,
                Subject = "Lease acceptance.",
                To = tenant.Email,
                HostEmail = "smtp.mail.yahoo.com",
                From = "kellyncode@yahoo.com"
            };
            
            EmailRequests notifyLandlord = new EmailRequests 
            {
                EmailPassword = _mailPWD,
                EmailBody = @$"<h2>Hey! {landlord.FirstName}!</h2> <hr /> <br /> <h5> Your lease was {message} by this user {tenant.FirstName} with email {tenant.Email} on {DateTime.UtcNow.ToLongDateString()}.</h5> <br /> <hr /> <a href={serverUrl}> <img src={propertyImage} /></a>",
                EmailPort = 465,
                Subject = "Lease acceptance.",
                To = landlord.Email,
                HostEmail = "smtp.mail.yahoo.com",
                From = "kellyncode@yahoo.com"
            };
            EmailResponse response = await _emailServices.SendMailAsync(email);
            await _emailServices.SendMailAsync(notifyLandlord);
            return response;
        }
        
        
        public async Task<EmailResponse> RentExpireMailAsync(Tenant tenant, string message, bool expiredRent)
        {
            string expiredImgUrl = "https://th.bing.com/th/id/OIP.aCm1MyQtBVGip1CRX-IBIwHaDb?pid=ImgDet&rs=1";
            string ImgUrl = "https://th.bing.com/th/id/OIP.aCm1MyQtBVGip1CRX-IBIwHaDb?pid=ImgDet&rs=1";
            string serverUrl = "https://localhost:7258/swagger/index.html";
            EmailRequests email = new EmailRequests
            {
                EmailPassword = _mailPWD,
                EmailBody = expiredRent ? @$"<h2>Hi {tenant.FirstName}!</h2> <hr /> <br /><h5> {message}</h5> <br /> <hr /> <a href={serverUrl}> <img src={expiredImgUrl} /></a>"
                : @$"<h5> <h2>Hi {tenant.FirstName}!</h2> <hr /> <br /> {message}</h5> <br /> <hr /> <a href={serverUrl}> <img src={ImgUrl} /></a>", 
                EmailPort = 465,
                Subject = expiredRent ? "Rent Expired" : "Rent is still active.",
                To = tenant.Email,
                HostEmail = "smtp.mail.yahoo.com",
                From = "kellyncode@yahoo.com"
            };
            EmailResponse response  = await _emailServices.SendMailAsync(email);
            return response;
        }
        
        public async Task<EmailResponse> UserCreatedMailAsync(ConfirmEmailResponse response)
        {
            string imgUrl = "https://codeconvey.com/wp-content/uploads/2020/06/registration-successful-message-html.png";
            string callbackURL = $"https://localhost:7258/api/auth/confirm-email?id={response.UserId}&token={response.Token}";
            EmailRequests email = new EmailRequests
            {
                EmailPassword = _mailPWD,
                EmailBody = @$"<h2>Hi {response.UserName}!</h2> <hr /> <br /> <h5>Thanks for creating you account with us. Click Continue to Verify your email address.</h5> <br /> <hr /> <a href={callbackURL}> <img src={imgUrl} /></a> ",
                EmailPort = 465,
                Subject = $"Welcome {response.UserName}",
                To = response.Email,
                HostEmail = "smtp.mail.yahoo.com",
                From = "kellyncode@yahoo.com"
            };
            EmailResponse emailResponse  = await _emailServices.SendMailAsync(email);
            return emailResponse;
        }
        
        public async Task<EmailResponse> RecetPasswordSuccessMailAsync(ApplicationUser user, string message)
        {
            string imgUrl = "companylogo.png";
            string logo = "company-logo.png";
            string serverUrl = "https://localhost:7258/swagger/index.html";
            EmailRequests email = new EmailRequests
            {
                EmailPassword = _mailPWD,
                EmailBody = @$"<h2>Hi {user.NormalizedUserName}!</h2> <hr /> <br /><h5>{message}</h5> <br /> <hr /> <a href={serverUrl}>
                     <img src={imgUrl} alt={logo} /></a> ",
                EmailPort = 465,
                Subject = message,
                To = user.Email,
                HostEmail = "smtp.mail.yahoo.com",
                From = "kellyncode@yahoo.com"
            };
            EmailResponse response  = await _emailServices.SendMailAsync(email);
            return response;
        }

        public async Task<EmailResponse> PaymentVerifiedMailAsync(ApplicationUser user, string message)
        {
            string imgUrl = "https://msmeafricaonline.com/wp-content/uploads/2020/10/Paystack-696x392.jpg";
            string serverUrl = "https://localhost:7258/swagger/index.html";
            EmailRequests email = new EmailRequests
            {
                EmailPassword = _mailPWD,
                EmailBody = @$"<h2>Hi {user.NormalizedUserName}!</h2> <hr /> <br /> <h5>Your payment was successfull. Thanks! for using our services and we hope to serve you better. {message}</h5> <br /> <hr /> <a href={serverUrl}> <img src={imgUrl} /></a> ",
                EmailPort = 465,
                Subject = $"Payment verified!",
                To = user.Email,
                HostEmail = "smtp.mail.yahoo.com",
                From = "kellyncode@yahoo.com"
            };
            EmailResponse response = await _emailServices.SendMailAsync(email);
            return response;
        }
    }
}
