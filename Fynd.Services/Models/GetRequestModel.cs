using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

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
