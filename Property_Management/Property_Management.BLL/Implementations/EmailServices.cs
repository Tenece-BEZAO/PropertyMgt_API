using AutoMapper;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using Property_Management.BLL.DTOs.Requests;
using Microsoft.AspNetCore.Identity;
using Property_Management.BLL.DTOs.Responses;
using Property_Management.BLL.Interfaces;
using Property_Management.DAL.Entities;
using Property_Management.DAL.Interfaces;

namespace Property_Management.BLL.Implementations
{
    public class EmailServices : IEmailServices
    {
        private readonly IRepository<Email> _mailRepo;
        private readonly IRepository<ApplicationUser> _userRepo;
        private readonly IRepository<NewsLetter> _newsLetterRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        public EmailServices(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mailRepo = _unitOfWork.GetRepository<Email>();
            _userRepo = _unitOfWork.GetRepository<ApplicationUser>();
            _newsLetterRepo = _unitOfWork.GetRepository<NewsLetter>();
        }

        public async Task<SubscriptionResponse> SubscribeNewsletterEmailAsync(string email)
        {
            bool emailExist = await _newsLetterRepo.AnyAsync(n => n.Email == email);
            if (emailExist) throw new InvalidOperationException("This email already subscribed.");

            NewsLetter newNewsLeter = new NewsLetter { Email = email };
            ApplicationUser? user = await _userManager.FindByEmailAsync(email);
            await _newsLetterRepo.AddAsync(newNewsLeter);
            if (user == null)
            {
                return new SubscriptionResponse {UserExist = false, StatusCode = 200, Message = "Subscription successfull" };
            }

            user.IsSubscribed = true;
            await _userRepo.UpdateAsync(user);
            return new SubscriptionResponse { UserExist = true, UserName = user.UserName, Message = "Subscription successful.", StatusCode = 201 };
        }

        public async Task<IEnumerable<FetchSubcribedUserEmailResponse>> GetAllSubscribedEmailAsync()
        {
            IEnumerable<NewsLetter> subscribedUsers = await _newsLetterRepo.GetAllAsync();
            if (subscribedUsers == null)
                throw new InvalidOperationException("No user found. list is empty.");

           return subscribedUsers.Select(le => new FetchSubcribedUserEmailResponse
            {
             Email = le.Email,
            });
        }

        public async Task<FetchSubcribedUserEmailResponse> GetSubscribedEmailAsync(string email)
        {
            NewsLetter subscribedUser = await _newsLetterRepo.GetSingleByAsync(predicate: ne => ne.Email == email);
            if(subscribedUser == null) throw new InvalidOperationException("This user has not submitted any review.");

            return new FetchSubcribedUserEmailResponse
            {
                Email = subscribedUser.Email,
            };
        }

        public async Task<EmailResponse> SendMailAsync(EmailRequests mailRequest)
        {
            if (mailRequest == null) throw new InvalidOperationException("Object values cannot be empty.");
            MimeMessage email = new();
            email.From.Add(new MailboxAddress(name: "Property Management System Enugu", address: mailRequest.From));
            email.To.Add(MailboxAddress.Parse(mailRequest.To));
            email.Subject = mailRequest.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = mailRequest.EmailBody };

            using SmtpClient smtpClient = new();
            await smtpClient.ConnectAsync(mailRequest.HostEmail, mailRequest.EmailPort, useSsl: true);
            await smtpClient.AuthenticateAsync(userName: mailRequest.From, password: mailRequest.EmailPassword);
            await smtpClient.SendAsync(email);
            await smtpClient.DisconnectAsync(true);
            smtpClient.Dispose();

            Email newEmail = _mapper.Map<Email>(mailRequest);

            await _mailRepo.AddAsync(newEmail);

            return new EmailResponse { Sent = true, SenderEmail = mailRequest.From, ReceiverEmail = mailRequest.To, Subject = mailRequest.Subject, Date = DateTime.UtcNow.ToLongDateString() };
        }

        public async Task<EmailResponse> SendBulkMailAsync(SendBulkEmailRequest bulkMessageRequest)
        {

            if (bulkMessageRequest == null) throw new InvalidOperationException("Object values cannot be empty.");
            foreach (var reciever in bulkMessageRequest.Recievers)
            {
                MimeMessage email = new();
                email.From.Add(new MailboxAddress(name: "Property Management System Enugu", address: bulkMessageRequest.From));
                email.To.Add(MailboxAddress.Parse(reciever));
                email.Subject = bulkMessageRequest.Subject;
                email.Body = new TextPart(TextFormat.Html) { Text = bulkMessageRequest.EmailBody };

                using SmtpClient smtpClient = new();
                await smtpClient.ConnectAsync(bulkMessageRequest.HostEmail, bulkMessageRequest.EmailPort, useSsl: true);
                await smtpClient.AuthenticateAsync(userName: bulkMessageRequest.From, password: bulkMessageRequest.EmailPassword);
                await smtpClient.SendAsync(email);
                await smtpClient.DisconnectAsync(true);
                smtpClient.Dispose();

                Email newEmail = _mapper.Map<Email>(bulkMessageRequest);
                newEmail.SenderEmail = bulkMessageRequest.From;
                newEmail.ReceiverEmail = reciever;
                newEmail.IsEmailSent = true;

                await _mailRepo.AddAsync(newEmail);
            }

            return new EmailResponse { Sent = true, SenderEmail = bulkMessageRequest.From, Receivers = bulkMessageRequest.Recievers, Subject = bulkMessageRequest.Subject, Date = DateTime.UtcNow.ToLongDateString() };
        }
    }
}
