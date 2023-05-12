using TravelManagement.Models;

namespace TravelManagement.Repository
{
    public interface IAirlineRepository:IRepository<Airline>
    {
        Task<Airline> UpdateAsync(Airline Entity);
    }
}
