using ErrorOr;
using Lukki.Api.ApiModels.Footer;
using Lukki.Application.Footers.Commands.CreateFooter;
using Lukki.Application.Footers.Queries.GetAllFooterNames;
using Lukki.Application.Footers.Queries.GetFooterByName;
using Lukki.Contracts.Footers;
using Lukki.Domain.FooterAggregate;
using Lukki.Domain.Common.Errors;
using Lukki.Infrastructure.Authentication;
using Lukki.Infrastructure.Helpers;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lukki.Api.Controllers;

[Route("footer")]
public class FooterController : ApiController
{
    private readonly IMapper _mapper;
    private readonly ISender _mediator;

    public FooterController(IMapper mapper, ISender mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize(Roles = AccessRoles.Customer)] // hack: should be ADMIN
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(FooterResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateFooter([FromForm] CreateFooterFormModel form)
    {
        const int maxFileSizeBytes = 20 * 1024; // 20 KB

        foreach (var section in form.Sections)
        {
            foreach (var link in section.Links)
            {
                if (link.Icon?.Length > maxFileSizeBytes)
                {
                    return Problem(
                        new List<Error>
                        {
                            Errors.Footer.ImageTooLarge(
                                yourImageSize: link.Icon.Length,
                                maxImageSize: maxFileSizeBytes)
                        });
                }
            }
        }

        var sections = new List<FooterSectionCommand>();
        foreach (var section in form.Sections)
        {
            var links = new List<FooterLinkCommand>();
            foreach (var link in section.Links)
            {
                Stream? iconStream = null;
                if (link.Icon is not null)
                {
                    iconStream = await FileHelpers.ConvertToStreamAsync(link.Icon);
                }

                var mappedLink = _mapper.Map<(FooterLinkFormModel, Stream?), FooterLinkCommand>((link, iconStream));
                links.Add(mappedLink);
            }

            var mappedSection =
                _mapper.Map<(FooterSectionFormModel, List<FooterLinkCommand>), FooterSectionCommand>((section, links));

            sections.Add(mappedSection);
        }

        var command =
            _mapper.Map<(CreateFooterFormModel, List<FooterSectionCommand>), CreateFooterCommand>((form, sections));


        var createFooterResult = await _mediator.Send(command);

        return createFooterResult.Match(
            footer => Ok(_mapper.Map<FooterResponse>(footer)),
            errors => Problem(errors)
        );
    }


    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(Footer), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetFooterByName([FromQuery] GetFooterRequest request)
    {
        var query = _mapper.Map<GetFooterByNameQuery>(request);

        var getFooterByNameResult = await _mediator.Send(query);

        return getFooterByNameResult.Match(
            footerResult => Ok(_mapper.Map<FooterResponse>(footerResult)),
            errors => Problem(errors));
    }

    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(FooterNamesResponse), StatusCodes.Status200OK)]
    [Route("names")]
    public async Task<IActionResult> GetAllFooterNames()
    {
        var getAllFooterNamesResult = await _mediator.Send(new GetAllFooterNamesQuery());

        return getAllFooterNamesResult.Match(
            footerResult => Ok(_mapper.Map<FooterNamesResponse>(footerResult)),
            errors => Problem(errors));
    }
}