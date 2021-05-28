using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkerAPI.Interfaces
{
    public interface ICloudinaryService
    {
        Task<string> UploadFile(IFormFile file);
    }
}
