using System.Threading.Tasks;

namespace B3_C1.Models
{
    public interface ISendMailService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
