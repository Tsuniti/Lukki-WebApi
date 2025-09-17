using Lukki.Domain.ColorAggregate;
using ErrorOr;
using Lukki.Application.Common.Interfaces.Persistence;
using Lukki.Domain.Common.Errors;
using MediatR;

namespace Lukki.Application.Colors.Queries.GetAllColors;

public class GetAllColorsQueryHandler :
    IRequestHandler<GetAllColorsQuery, ErrorOr<List<Color>>>
{
    private readonly IColorRepository _colorRepository;

    public GetAllColorsQueryHandler(IColorRepository colorRepository)
    {
        _colorRepository = colorRepository;
    }
    
    public async Task<ErrorOr<List<Color>>> Handle(GetAllColorsQuery request, CancellationToken cancellationToken)
    {
        var colors = await _colorRepository.GetAllAsync();

        if (colors is null || colors.Count == 0)
        {
            return Errors.Color.NoColorsFound();
        }

        return colors;
    }
}