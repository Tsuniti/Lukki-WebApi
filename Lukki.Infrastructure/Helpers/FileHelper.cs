using Microsoft.AspNetCore.Http;

namespace Lukki.Infrastructure.Helpers;

public static class FileHelpers
{
    public static async Task<List<Stream>> ConvertToStreamsAsync(List<IFormFile> files)
    {
        var streams = new List<Stream>();
        foreach (var file in files)
        {
            var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            ms.Position = 0;
            streams.Add(ms);
        }
        return streams;
    }
    
    public static async Task<Stream> ConvertToStreamAsync(IFormFile file)
    {
        var stream = new MemoryStream();
        await file.CopyToAsync(stream);
        stream.Position = 0;
        return stream;
    }
    
}