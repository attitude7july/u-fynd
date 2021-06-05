using Fynd.Services.Contract;
using Fynd.Services.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using ClosedXML.Excel;

namespace Fynd.Services.Implementation
{
    public class HotelService : IHotelService
    {
        private readonly ILogger<IHotelService> _logger;
        private readonly IFileService _fileService;
        public HotelService(ILogger<IHotelService> logger, IFileService fileService, IEmailService emailService)
        {
            _logger = logger;
            _fileService = fileService;
        }

        public Task<HotelRateResponse> GetFilteredInformation(GetRequestModel request)
        {
            try
            {
                if (request == null)
                {
                    throw new ArgumentException("Input fields cannot be null.");
                }

                var jsonString = _fileService.ReadJsonFile(_fileService.Task3FileName);

                if (string.IsNullOrEmpty(jsonString))
                {
                    throw new ArgumentException("No json data found.");
                }

                var hotelLists = _fileService.GetObject<List<HotelRateResponse>>(jsonString);

                var hotel = (from h in hotelLists
                             where h.Hotel.HotelId == request.HotelId
                             select new HotelRateResponse
                             {
                                 Hotel = h?.Hotel,
                                 HotelRates = h?.HotelRates?
                                                .Where(x => x.TargetDay.ToString("yyyy-MM-dd") == request.ArrivalDate.ToString("yyyy-MM-dd"))
                                                .ToList()
                             }
                            );

                return Task.FromResult(hotel?.FirstOrDefault());

            }
            catch (Exception exc)
            {
                _logger.LogError(exc, nameof(GetFilteredInformation), request);

                throw;
            }
        }

        public Task<byte[]> GetExcelReport()
        {
            try
            {
                byte[] content = null;

                var jsonString = _fileService.ReadJsonFile(_fileService.Task2FileName);

                if (string.IsNullOrEmpty(jsonString))
                {
                    throw new ArgumentException("No json data found.");
                }

                var data = _fileService.GetObject<HotelRateResponse>(jsonString);

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("HotelReport");

                    var currentRow = 1;
                    worksheet.Cell(currentRow, 1).Value = "ARRIVAL_DATE";
                    worksheet.Cell(currentRow, 2).Value = "DEPARTURE_DATE";
                    worksheet.Cell(currentRow, 3).Value = "PRICE";
                    worksheet.Cell(currentRow, 4).Value = "CURRENCY";
                    worksheet.Cell(currentRow, 5).Value = "RATENAME";
                    worksheet.Cell(currentRow, 6).Value = "ADULTS";
                    worksheet.Cell(currentRow, 7).Value = "BREAKFAST_INCLUDED";

                    foreach (var hotel in data.HotelRates)
                    {
                        currentRow++;
                        worksheet.Cell(currentRow, 1).Value = hotel.TargetDay;
                        worksheet.Cell(currentRow, 1).Style.NumberFormat.Format = "dd.MM.yy";

                        worksheet.Cell(currentRow, 2).Value = hotel.TargetDay.AddDays(1);
                        worksheet.Cell(currentRow, 2).Style.NumberFormat.Format = "dd.MM.yy";

                        worksheet.Cell(currentRow, 3).Value = hotel.Price.NumericFloat;
                        worksheet.Cell(currentRow, 3).Style.Fill.SetBackgroundColor(XLColor.AliceBlue);

                        worksheet.Cell(currentRow, 4).Value = hotel.Price.Currency;
                        worksheet.Cell(currentRow, 5).Value = hotel.RateName;
                        worksheet.Cell(currentRow, 6).Value = hotel.Adults;
                        worksheet.Cell(currentRow, 7).Value = hotel.RateTags?.FirstOrDefault()?.Shape == true ? 1 : 0;

                        if (currentRow % 2 == 0)
                        {
                            worksheet.Rows(currentRow.ToString()).Style.Fill.BackgroundColor = XLColor.BabyBlueEyes;
                        }
                    }
                    worksheet.RangeUsed().SetAutoFilter();

                    using var stream = new MemoryStream();
                    workbook.SaveAs(stream);
                    content = stream.ToArray();
                }

                return Task.FromResult(content);
            }
            catch (Exception exc)
            {

                _logger.LogError(exc, nameof(GetExcelReport));

                throw;
            }
        }
    }
}
