﻿using BjBygg.Application.Common.Exceptions;
using BjBygg.Application.Common.Interfaces;
using BjBygg.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace BjBygg.Infrastructure.Api.SendGridMailService
{
    public class SendGridMailService : IMailService
    {
        private IConfiguration _configuration;

        public SendGridMailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendEmailAsync(string toEmail, string subject, string content)
        {
            var apiKey = _configuration.GetValue<string>("SendGridApiKey");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("noreply@bjbygg.no", "Karl Brede Tvete");
            var to = new EmailAddress(toEmail);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, content, content);
            var response = await client.SendEmailAsync(msg);
        }
        public async Task SendTemplateEmailAsync<T>(string toEmail, IMailTemplate<T> template)
        {
            var apiKey = _configuration.GetValue<string>("SendGridApiKey");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("noreply@bjbygg.no", "BjBygg");
            var to = new EmailAddress(toEmail);
            var msg = MailHelper.CreateSingleTemplateEmail(from, to, template.Key, template.Data);

            Response response;
            if (template.Attachment == null)
                response = await client.SendEmailAsync(msg);
            else
            {
                await msg.AddAttachmentAsync(template.AttachmentName, template.Attachment);
                response = await client.SendEmailAsync(msg);
            }

            if (response.StatusCode != System.Net.HttpStatusCode.Accepted)
            {
                throw new BadRequestException();
            }
        }
    }
}
