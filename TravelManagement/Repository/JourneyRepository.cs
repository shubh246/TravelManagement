using TravelManagement.Data;
using TravelManagement.Models;

namespace TravelManagement.Repository
{
    public class JourneyRepository : Repository<Journey>, IJourneyRepository
    {
        private readonly ApplicationDbContext db;
        public JourneyRepository(ApplicationDbContext _db) : base(_db)
        {
            db = _db;


        }


        public async Task<Journey> UpdateAsync(Journey Entity)
        {
            Entity.TravelDate = DateTime.Now;
            db.Journeys.Update(Entity);
            await db.SaveChangesAsync();
            return Entity;
        }
    }
}
