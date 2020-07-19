using CleanArchitecture.Core.Interfaces;
using System.Threading.Tasks;

namespace BjBygg.Application.Common.Interfaces
{
    public interface IMailService
    {
        Task SendEmailAsync(string toEmail, string subject, string content);
        Task SendTemplateEmailAsync<T>(string toEmail, IMailTemplate<T> template);
    }
}
