using Fynd.Services.Contract;

namespace Fynd.Services.Implementation
{
    public class EmailConfig : IEmailConfig
    {
        public string ApiKey { get; set; }
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public string Subject { get; set; }
    }
}
