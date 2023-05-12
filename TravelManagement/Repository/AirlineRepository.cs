using TravelManagement.Data;
using TravelManagement.Models;

namespace TravelManagement.Repository
{
    public class AirlineRepository:Repository<Airline>,IAirlineRepository
    {
        private readonly ApplicationDbContext db;
        public AirlineRepository(ApplicationDbContext _db) : base(_db)
        {
            db = _db;


        }


        public async Task<Airline> UpdateAsync(Airline Entity)
        {
            //Entity.UpdatedDate = DateTime.Now;
            db.Airlines.Update(Entity);
            await db.SaveChangesAsync();
            return Entity;
        }
    }
}
