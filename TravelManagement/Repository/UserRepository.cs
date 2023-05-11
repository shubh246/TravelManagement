using TravelManagement.Data;
using TravelManagement.Models;

namespace TravelManagement.Repository
{
    public class UserRepository:Repository<User>,IUserRepository
    {
        private readonly ApplicationDbContext db;
        public UserRepository(ApplicationDbContext _db) : base(_db)
        {
            db = _db;


        }


        public async Task<User> UpdateAsync(User Entity)
        {
            //Entity.UpdatedDate = DateTime.Now;
            db.Users.Update(Entity);
            await db.SaveChangesAsync();
            return Entity;
        }
    }
}
