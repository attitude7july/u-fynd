using System.Collections.Generic;

namespace Fynd.Services.Models
{
    public class HotelRateResponse
    {
        public Hotel Hotel { get; set; }
        public List<HotelRate> HotelRates { get; set; }
    }


}
