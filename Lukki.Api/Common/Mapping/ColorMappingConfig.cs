using Lukki.Application.Colors.Commands.CreateColor;
using Lukki.Contracts.Colors;
using Lukki.Domain.ColorAggregate;
using Lukki.Domain.ColorAggregate.ValueObjects;
using Mapster;
using Lukki.Domain.Common.ValueObjects;

namespace Lukki.Api.Common.Mapping;

public class ColorMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateColorRequest, CreateColorCommand>();

        config.NewConfig<Color, ColorResponse>();
        
        TypeAdapterConfig<ColorId, string>.NewConfig().MapWith(id => id.Value.ToString());
        TypeAdapterConfig<Image, string>.NewConfig().MapWith(image => image.Url);
    }
}