using TravelManagement.Data;
using TravelManagement.Models;

namespace TravelManagement.Repository
{
    public class FlightRepository : Repository<Flight>, IFlightRepository
    {
        private readonly ApplicationDbContext db;
        public FlightRepository(ApplicationDbContext _db) : base(_db)
        {
            db = _db;


        }


        public async Task<Flight> UpdateAsync(Flight Entity)
        {
            //Entity.UpdatedDate = DateTime.Now;
            db.Flights.Update(Entity);
            await db.SaveChangesAsync();
            return Entity;
        }
    }
}
