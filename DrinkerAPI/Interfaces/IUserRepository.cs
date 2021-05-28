using DrinkerAPI.Models;
using System.Threading.Tasks;

namespace DrinkerAPI.Interfaces
{
    public interface IUserRepository
    {
        Task<AppUser> GetUserById(int id);
    }
}
