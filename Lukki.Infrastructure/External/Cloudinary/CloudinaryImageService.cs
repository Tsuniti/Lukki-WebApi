using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Lukki.Application.Common.Interfaces.Services.ImageStorage;
using Microsoft.Extensions.Options;

namespace Lukki.Infrastructure.External.Cloudinary;

public class CloudinaryImageService : IImageStorageService
{
    private readonly CloudinaryDotNet.Cloudinary _cloudinary;

    public CloudinaryImageService(IOptions<CloudinarySettings> cloudinaryOptions)
    {
        var settings = cloudinaryOptions.Value;
        var account = new Account(settings.CloudName, settings.ApiKey, settings.ApiSecret);
        _cloudinary = new CloudinaryDotNet.Cloudinary(account);
    }

    public async Task<string> UploadImageAsync(Stream fileStream, string fileName)
    {
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(fileName, fileStream),
            Folder = "lukki-images"
        };

        var result = await _cloudinary.UploadAsync(uploadParams);
        return result.SecureUrl.ToString();
    }

    public async Task DeleteImageAsync(string publicId)
    {
        await _cloudinary.DestroyAsync(new DeletionParams(publicId));
    }
}