using Fynd.Services.Contract;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fynd.Services.Implementation
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<IEmailService> _logger;
        private readonly IEmailConfig _emailConfig;
        public EmailService(ILogger<IEmailService> logger, IEmailConfig emailConfig)
        {
            _logger = logger;
            _emailConfig = emailConfig;
        }
        public async Task<Response> SendEmail(byte[] attachment)
        {
            try
            {
                var client = new SendGridClient(_emailConfig.ApiKey);
                var from = new EmailAddress(_emailConfig.FromAddress);
                var to = new EmailAddress(_emailConfig.ToAddress);
                var plainTextContent = "Assingment for u:fynd";
                var msg = MailHelper.CreateSingleEmail(from, to, _emailConfig.Subject, plainTextContent, null);
                msg.Attachments = new List<Attachment>
                {
                    new Attachment
                    {
                        Content = Convert.ToBase64String(attachment),
                        Filename = "report.xlsx",
                        Type = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        Disposition = "attachment"
                    }
                };
                return await client.SendEmailAsync(msg);
            }
            catch (Exception exc)
            {
                _logger.LogError(exc, nameof(SendEmail), attachment);

                throw;
            }
        }
    }
}
