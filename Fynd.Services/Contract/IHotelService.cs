using Fynd.Services.Models;
using System.Threading.Tasks;

namespace Fynd.Services.Contract
{
    public interface IHotelService
    {
        Task<HotelRateResponse> GetFilteredInformation(GetRequestModel request);
    }
}
