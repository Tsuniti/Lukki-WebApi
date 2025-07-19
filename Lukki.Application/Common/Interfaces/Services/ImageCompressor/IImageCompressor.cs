namespace Lukki.Application.Common.Interfaces.Services.ImageCompressor;

public interface IImageCompressor
{
    Task<Stream> CompressAsync(Stream imageStream);
}