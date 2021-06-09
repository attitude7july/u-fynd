using Fynd.Services.Implementation;
using Fynd.Services.Models;
using Xunit;

namespace Fynd.Services.Test.Services
{
    public class FileServiceTests
    {

        [Fact]
        public void ShouldReadJsonFile()
        {
            var _fileService = new FileService();

            var json = _fileService.ReadJsonFile("task3_hotelsrates.json");

            Assert.NotNull(json);

        }

        [Fact]
        public void ShouldReadObjectFromJsonFile()
        {
            var _fileService = new FileService();

            var json = _fileService.ReadJsonFile("task2_ hotelrates.json");

            var response = _fileService.GetObject<HotelRateResponse>(json);
            Assert.NotNull(json);
            Assert.NotNull(response);

            Assert.Equal(typeof(HotelRateResponse), response.GetType());
        }
    }
}
