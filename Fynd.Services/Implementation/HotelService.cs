using Fynd.Services.Contract;
using Fynd.Services.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;

namespace Fynd.Services.Implementation
{
    public class HotelService : IHotelService
    {
        private readonly ILogger<IHotelService> _logger;
        private static string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "task3_hotelsrates.json");
        public HotelService(ILogger<IHotelService> logger)
        {
            _logger = logger;

        }

        public Task<HotelRateResponse> GetFilteredInformation(GetRequestModel request)
        {
            try
            {
                if (request == null)
                {

                    throw new ArgumentException("Input fields cannot be null.");
                }

                var hotelLists = JsonConvert.DeserializeObject<List<HotelRateResponse>>(File.ReadAllText(_filePath));

                var hotel = (from h in hotelLists
                             where h.Hotel.HotelId == request.HotelId
                             select new HotelRateResponse
                             {
                                 Hotel = h?.Hotel,
                                 HotelRates = h?.HotelRates?.Where(x => x.TargetDay == request.ArrivalDate).ToList()
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
    }
}
