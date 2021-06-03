using Fynd.Services.Contract;
using Fynd.Services.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Fynd.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FyndController : ControllerBase
    {

        private readonly IHotelService _hotelService;

        public FyndController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }


        [HttpGet(Name = nameof(GetFilteredListForTask3))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(HotelRateResponse))]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetFilteredListForTask3([FromQuery] GetRequestModel requestModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(requestModel);
            }

            var hotel = await _hotelService.GetFilteredInformation(requestModel);

            if (hotel == null)
            {
                return NotFound();
            }

            return Ok(hotel);
        }

    }
}
