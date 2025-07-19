namespace Lukki.Application.Common.Interfaces.Services.ImageStorage;

public interface IImageStorageService
{
    Task<string> UploadImageAsync(Stream fileStream, string fileName);
    Task DeleteImageAsync(string publicId);
}