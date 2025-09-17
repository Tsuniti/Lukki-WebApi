using ErrorOr;
using Lukki.Domain.ColorAggregate;
using MediatR;

namespace Lukki.Application.Colors.Commands.CreateColor;

public record CreateColorCommand( 
    string Name,
    string? HexColorCode,
    Stream? Image
    ) : IRequest<ErrorOr<Color>>;