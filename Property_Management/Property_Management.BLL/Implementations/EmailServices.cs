using AutoMapper;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using Property_Management.BLL.DTOs.Requests;
using Property_Management.BLL.DTOs.Responses;
using Property_Management.BLL.Interfaces;
using Property_Management.DAL.Entities;
using Property_Management.DAL.Interfaces;

namespace Property_Management.BLL.Implementations
{
    public class EmailServices : IEmailServices
    {
        private readonly IRepository<Email> _mailRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmailServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mailRepo = _unitOfWork.GetRepository<Email>();
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

        public async Task<EmailResponse> SendBulkMessageAsync(SendBulkEmailRequest bulkMessageRequest)
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
