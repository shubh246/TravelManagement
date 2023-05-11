using TravelManagement.Models;

namespace TravelManagement.Repository
{
    public interface IUserRepository:IRepository<User>
    {

        Task<User> UpdateAsync(User Entity);

    
    }
}
