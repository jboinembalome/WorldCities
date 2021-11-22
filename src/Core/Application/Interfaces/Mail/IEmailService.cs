using WorldCities.Application.Models.Mail;
using System.Threading.Tasks;

namespace WorldCities.Application.Interfaces.Mail
{
    public interface IEmailService
    {
        Task<bool> SendEmail(Email email);
    }
}
