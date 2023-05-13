using TravelManagement.Models;

namespace TravelManagement.Repository
{
    public interface IFlightRepository:IRepository<Flight>
    {
        Task<Flight> UpdateAsync(Flight Entity);
    }
}
