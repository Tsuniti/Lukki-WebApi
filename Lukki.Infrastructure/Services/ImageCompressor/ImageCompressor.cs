using Lukki.Application.Common.Interfaces.Services.ImageCompressor;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;

namespace Lukki.Infrastructure.Services.ImageCompressor;

public class ImageCompressor : IImageCompressor
{
    public async Task<Stream> CompressAsync(Stream imageStream)
    {
        using var image = await Image.LoadAsync(imageStream);

        if (image.Width > 1280 || image.Height > 1280)
        {
            image.Mutate(
                x => x.Resize(
                    new ResizeOptions
                    {
                        Size = new Size(1280, 1280),
                        Mode = ResizeMode.Max,
                        Sampler = KnownResamplers.Lanczos3,
                        Compand = true
                    }));
        }
        
        image.Mutate(x => x.GaussianBlur(0.5f));


        var encoder = new JpegEncoder
        {
            Quality = 90, // not less than 80
            ColorType = JpegEncodingColor.YCbCrRatio420,
            Interleaved = true,
            SkipMetadata = true,
        };
        

        var outputStream = new MemoryStream();
        await image.SaveAsJpegAsync(outputStream, encoder);
        outputStream.Position = 0;
        return outputStream;
    }
}