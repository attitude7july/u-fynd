using System.Threading.Tasks;

namespace Fynd.Services.Contract
{
    public interface IEmailService
    {
        Task<string> SendEmail(byte[] attachment);
    }
}
