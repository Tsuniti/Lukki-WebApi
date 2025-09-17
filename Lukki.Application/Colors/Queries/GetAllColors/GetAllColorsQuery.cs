using Lukki.Domain.ColorAggregate;
using ErrorOr;
using MediatR;

namespace Lukki.Application.Colors.Queries.GetAllColors;

public class GetAllColorsQuery : IRequest<ErrorOr<List<Color>>>;