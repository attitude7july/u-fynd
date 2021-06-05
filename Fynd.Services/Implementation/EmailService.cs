using Fynd.Services.Contract;
using System.Threading.Tasks;

namespace Fynd.Services.Implementation
{
    public class EmailService : IEmailService
    {
        public Task<string> SendEmail(byte[] attachment)
        {
            return Task.FromResult("email send");
        }
    }
}
