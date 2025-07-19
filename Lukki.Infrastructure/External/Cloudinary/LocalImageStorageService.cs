using Lukki.Application.Common.Interfaces.Services.ImageStorage;

namespace Lukki.Infrastructure.External.Cloudinary;


// todo: temp file, only for debugging
public class LocalImageStorageService : IImageStorageService
{
    private readonly string _tempFolderPath;

    public LocalImageStorageService()
    {
        _tempFolderPath = Path.Combine(Path.GetTempPath(), "LukkiTempImages");
        Directory.CreateDirectory(_tempFolderPath);
    }

    public async Task<string> UploadImageAsync(Stream imageStream, string fileName)
    {
        var filePath = Path.Combine(_tempFolderPath, $"{fileName}.jpg");
        
        await using var fileStream = File.Create(filePath);
        await imageStream.CopyToAsync(fileStream);
        
        return filePath;
    }

    public Task DeleteImageAsync(string publicId)
    {
        throw new NotImplementedException();
    }

    public void CleanupTempFiles()
    {
        if (Directory.Exists(_tempFolderPath))
        {
            foreach (var file in Directory.GetFiles(_tempFolderPath))
            {
                File.Delete(file);
            }
        }
    }
}