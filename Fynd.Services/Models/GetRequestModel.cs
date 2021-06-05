using System;
using System.ComponentModel.DataAnnotations;

namespace Fynd.Services.Models
{
    public class GetRequestModel
    {
        [Required]
        public int HotelId { get; set; }

        [Required]
        public DateTime ArrivalDate { get; set; }
    }
}
