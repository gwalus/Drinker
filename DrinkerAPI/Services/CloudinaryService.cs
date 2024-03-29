﻿using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DrinkerAPI.Helpers;
using DrinkerAPI.Interfaces;
using Microsoft.Extensions.Options;
using System.IO;
using System.Net;
using System.Threading.Tasks;
namespace DrinkerAPI.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        public readonly Account _account;
        public readonly Cloudinary _cloudinary;
        public CloudinaryService(IOptions<CloudinarySettings> config)
        {
            _account = new Account
            (
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );
            _cloudinary = new Cloudinary(_account);
        }
        public async Task<string> UploadFile(byte[] destinationData, string filename)
        {
            ImageUploadResult uploadResult = null;
            using (var ms = new MemoryStream(destinationData))
            {
                ImageUploadParams uploadParams = new ImageUploadParams
                {
                    Folder = "coctails",
                    File = new FileDescription(filename, ms),
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
