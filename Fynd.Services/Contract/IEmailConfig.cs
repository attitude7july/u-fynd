namespace Fynd.Services.Contract
{
    public interface IEmailConfig
    {
        public string ApiKey { get; set; }
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public string Subject { get; set; }
    }
}
