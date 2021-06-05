using Fynd.Services.Contract;
using Fynd.Services.Implementation;
using Fynd.Services.Models;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace Fynd.Services.Test.Services
{
    public class HoteServiceTests
    {
        private readonly Mock<ILogger<IHotelService>> _logger;

        public HoteServiceTests()
        {
            _logger = new Mock<ILogger<IHotelService>>();
        }

        [Theory]
        [ClassData(typeof(CalculatorTestData))]
        public async void ShoudlGetFilteredList(GetRequestModel request, int expectedRanges)
        {
            var service = new HotelService(_logger.Object, new FileService());

            var response = await service.GetFilteredInformation(request);

            Assert.NotNull(response);

            Assert.Equal(response.HotelRates.Count, expectedRanges);
        }

        [Fact]
        public void ShouldThrowArguementException()
        {
            var service = new HotelService(_logger.Object, new FileService());

            Assert.ThrowsAsync<ArgumentException>(() => service.GetFilteredInformation(null));
        }

        [Fact]
        public void ShouldThrowExceptionException()
        {
            var fileService = new Mock<IFileService>();

            var service = new HotelService(_logger.Object, fileService.Object);

            Assert.ThrowsAsync<ArgumentException>(() => service.GetFilteredInformation(new GetRequestModel()));
        }

        [Fact]
        public void ShouldGetJsonData()
        {
            var service = new FileService();

            var request = service.ReadJsonFile("task3_hotelsrates.json");

            Assert.NotNull(request);
        }

        [Fact]
        public void ShouldThrowException()
        {
            var service = new FileService();

            Assert.Throws<FileNotFoundException>(() => service.ReadJsonFile("task3_hotelsrates1.json"));
        }

        [Fact]
        public async void ShouldGetExcelReport()
        {
            var service = new HotelService(_logger.Object, new FileService());

            var response = await service.GetExcelReport();

            Assert.NotNull(response);

            Assert.Equal(typeof(byte[]), response.GetType());
        }


        [Fact]
        public void ShouldThrowExceptionGetExcelReport()
        {
            var fileService = new Mock<IFileService>();

            var service = new HotelService(_logger.Object, fileService.Object);

            Assert.ThrowsAsync<ArgumentException>(() => service.GetExcelReport());
        }
    }

    public class CalculatorTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new GetRequestModel {
                  HotelId=  7294,
                  ArrivalDate= new DateTime(2016, 03, 15)
                }, 26};
            yield return new object[] { new GetRequestModel {
                  HotelId=  7294,
                  ArrivalDate= new DateTime(2016, 03, 16)
                }, 26 };
            yield return new object[] { new GetRequestModel {
                  HotelId=  8759,
                  ArrivalDate= new DateTime(2016, 03, 17)
                }, 26 };
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}