using eDocCore.Application.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eDocCore.Infrastructure.Persistence.Services
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;

        public EmailService(ILogger<EmailService> logger)
        {
            _logger = logger;
        }

        public Task SendEmailAsync(string to, string subject, string body)
        {
            // Logic gửi email thực tế sẽ ở đây.
            // Ví dụ: sử dụng SMTP client, SendGrid, Mailgun,...
            _logger.LogInformation("Sending email to {To} with subject {Subject}", to, subject);
            return Task.CompletedTask;
        }
    }

}
