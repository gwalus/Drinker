using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace DrinkerAPI.Interfaces
{
    public interface ICloudinaryService
    {
        Task<string> UploadFile(IFormFile file);
    }
}
