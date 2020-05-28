using System.Threading.Tasks;

namespace CleanArchitecture.Core.Interfaces
{
    public interface IMailService
    {
        Task SendEmailAsync(string toEmail, string subject, string content);
        Task SendTemplateEmailAsync(string toEmail, string templateId, ITemplateData data);
    }
}
