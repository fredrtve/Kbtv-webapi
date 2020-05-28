using CleanArchitecture.Core.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using CleanArchitecture.Core.Exceptions;

namespace CleanArchitecture.Infrastructure.Api.SendGridMailService
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
        public async Task SendTemplateEmailAsync(string toEmail, string templateId, ITemplateData data)
        {
            var apiKey = _configuration.GetValue<string>("SendGridApiKey");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("noreply@bjbygg.no", "BjBygg");
            var to = new EmailAddress(toEmail);
            var msg = MailHelper.CreateSingleTemplateEmail(from, to, templateId, data);
            var response = await client.SendEmailAsync(msg);

            if (response.StatusCode != System.Net.HttpStatusCode.Accepted)
            {
                throw new BadRequestException("Email not sent");
            }
        }
    }
}
