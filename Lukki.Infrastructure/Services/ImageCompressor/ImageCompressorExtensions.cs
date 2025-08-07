using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.PixelFormats;

namespace Lukki.Infrastructure.Services.ImageCompressor;

public static class ImageCompressorExtensions
{
    public static bool HasTransparency(this Image image)
    {
        return image switch
        {
            Image<Rgba32> rgbaImg => rgbaImg.HasTransparentPixels(),
            Image<Argb32> argbImg => argbImg.HasTransparentPixels(),
            _ => false
        };
    }

    private static bool HasTransparentPixels<TPixel>(this Image<TPixel> image)
        where TPixel : unmanaged, IPixel<TPixel>
    {
        var memoryGroup = image.GetPixelMemoryGroup();
        
        foreach (var memory in memoryGroup)
        {
            var span = memory.Span;
            for (int i = 0; i < span.Length; i += 10)
            {
                Rgba32 pixel = default;
                span[i].ToRgba32(ref pixel);
                
                if (pixel.A < 255)
                    return true;
            }
        }

        return false;
    }
}