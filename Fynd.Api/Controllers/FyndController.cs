using Fynd.Services.Contract;
using Fynd.Services.Models;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Fynd.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FyndController : ControllerBase
    {

        private readonly IHotelService _hotelService;
        private readonly IEmailService _emailService;
        public FyndController(IHotelService hotelService, IEmailService emailService)
        {
            _hotelService = hotelService;
            _emailService = emailService;
        }
        [Route(nameof(GetFilteredListForTask3))]
        [HttpGet]
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
        [Route(nameof(GetExcelReportTask2))]
        [HttpGet]
        public async Task<IActionResult> GetExcelReportTask2()
        {
            var content = await _hotelService.GetExcelReport();

            if (content == null)
            {
                return NotFound();
            }

            BackgroundJob.Schedule(() => _emailService.SendEmail(content), TimeSpan.FromMinutes(1));

            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "excelReport.xlsx");
        }

    }
}
