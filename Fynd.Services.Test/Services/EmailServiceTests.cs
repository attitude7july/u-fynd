using Fynd.Services.Contract;
using Fynd.Services.Implementation;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace Fynd.Services.Test.Services
{
    public class EmailServiceTests
    {
        private readonly Mock<ILogger<IEmailService>> _logger;
        public EmailServiceTests()
        {
            _logger = new Mock<ILogger<IEmailService>>();
        }

        [Fact]
        public async void ShouldSendEmail()
        {
            var eService = new EmailService(_logger.Object, new EmailConfig
            {
                ToAddress = "junkaddress@gmail.com",
                FromAddress = "junkaddress@gmail.com",
                ApiKey = "SG.rbSTXktFSrqUsHvyrh8rVg",
                Subject = "test email"
            });
            var service = new HotelService((new Mock<ILogger<IHotelService>>()).Object, new FileService());

            var docResponse = await service.GetExcelReport();

            Assert.NotNull(docResponse);

            var response = await eService.SendEmail(docResponse);

            Assert.False(response.IsSuccessStatusCode);
        }

        [Fact]
        public async void ShouldThrowExceptionSendEmail()
        {
            var eService = new EmailService(_logger.Object, new EmailConfig
            {
                ToAddress = "junkaddress@gmail.com",
                FromAddress = "junkaddress@gmail.com",
                ApiKey = "SG.rbSTXktFSrqUsHvyrh8rVg",
                Subject = "test email"
            });

            await Assert.ThrowsAsync<ArgumentNullException>(() => eService.SendEmail(null));

        }
    }
}
