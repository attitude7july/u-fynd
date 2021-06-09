using SendGrid;
using System.Threading.Tasks;

namespace Fynd.Services.Contract
{
    public interface IEmailService
    {
        Task<Response> SendEmail(byte[] attachment);
    }
}
