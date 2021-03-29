using DataDownloader.Models;
using System.Threading.Tasks;

namespace DataDownloader.Interfaces
{
    public interface IDataService
    {
        Task<CoctailDbResponseModel> GetDrink(int id);
        Task<DrinkBsonItem> SaveDrink();
    }
}
