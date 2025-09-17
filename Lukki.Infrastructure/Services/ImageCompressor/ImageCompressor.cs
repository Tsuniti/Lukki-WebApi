using Lukki.Application.Common.Interfaces.Services.ImageCompressor;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;

namespace Lukki.Infrastructure.Services.ImageCompressor;

public class ImageCompressor : IImageCompressor
{
    public async Task<Stream> CompressAsync(Stream imageStream)
    {
        using var image = await Image.LoadAsync(imageStream);
        bool hasTransparency = image.HasTransparency(); // Extension method
        
        if (image.Width > 1280 || image.Height > 1280)
        {
            image.Mutate(x => x.Resize(new ResizeOptions
            {
                Size = new Size(1280, 1280),
                Mode = ResizeMode.Max,
                Sampler = KnownResamplers.Lanczos3,
                Compand = true
            }));
        }

        // Blur only for JPEG (without transparency)
        if (!hasTransparency)
        {
            image.Mutate(x => x.GaussianBlur(0.5f));
        }

        // Saving in PNG or JPEG
        var outputStream = new MemoryStream();
        
        if (hasTransparency)
        {
            await image.SaveAsPngAsync(outputStream, new PngEncoder
            {
                CompressionLevel = PngCompressionLevel.BestCompression,
                SkipMetadata = true,
                ColorType = PngColorType.RgbWithAlpha
            });
        }
        else
        {
            await image.SaveAsJpegAsync(outputStream, new JpegEncoder
            {
                Quality = 90,
                ColorType = JpegEncodingColor.YCbCrRatio420,
                Interleaved = true,
                SkipMetadata = true
            });
        }
        
        outputStream.Position = 0;
        return outputStream;
    }
}