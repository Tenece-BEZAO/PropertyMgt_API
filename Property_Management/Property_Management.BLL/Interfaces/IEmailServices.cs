﻿using Property_Management.BLL.DTOs.Requests;
using Property_Management.BLL.DTOs.Responses;

namespace Property_Management.BLL.Interfaces
{
    public interface IEmailServices
    {
        Task<EmailResponse> SendMailAsync(EmailRequests mailRequest);
        Task<EmailResponse> SendBulkMessageAsync(SendBulkEmailRequest bulkMessageRequest);
    }
}
