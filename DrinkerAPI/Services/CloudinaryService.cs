using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DrinkerAPI.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DrinkerAPI.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        public readonly Account _account;
        public readonly Cloudinary _cloudinary;
        public CloudinaryService()
        {
            _account = new Account("drinker-api", "865982832527545", "BwX7JL1xoRb51FOflzW0WZV9CDs");
            _cloudinary = new Cloudinary(_account);
        }
        public async Task<string> UploadFile(IFormFile file)
        {
            byte[] destinationData;
            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                destinationData = ms.ToArray();
            }
            ImageUploadResult uploadResult = null;
            using (var ms = new MemoryStream(destinationData))
            {
                ImageUploadParams uploadParams = new ImageUploadParams
                {
                    Folder = "coctails",
                    File = new FileDescription(file.Name, ms),
                    Transformation = new Transformation().Height(700).Width(700),
                };
                uploadResult = _cloudinary.Upload(uploadParams);
                if (uploadResult.StatusCode == HttpStatusCode.OK)
                {
                    return uploadResult.Url.ToString();
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
