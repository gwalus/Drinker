using System.Threading.Tasks;

namespace DrinkerAPI.Interfaces
{
    public interface ICloudinaryService
    {
        Task<string> UploadFile(byte[] destinationData, string filename);
    }
}
