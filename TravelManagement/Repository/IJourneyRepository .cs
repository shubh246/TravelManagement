using TravelManagement.Models;

namespace TravelManagement.Repository
{
    public interface IJourneyRepository:IRepository<Journey>
    {
        Task<Journey> UpdateAsync(Journey Entity);
    }
}
