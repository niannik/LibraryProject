using Api.Authorization;
using Api.Extensions;
using Application.Authors.Queries.UserGetAuthors;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.UserArea;

[Route("api/[Controller]")]
[Authorize(Policy = AppAuthorizationPolicies.User)]
public class AuthorsController : ApiController
{
    private readonly IMediator _mediator;
    public AuthorsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<UserGetAuthorsResponse>> GetAll()
    {
        var query = new UserGetAuthorsQuery();

        var result = await _mediator.Send(query, CancellationToken);

        return result.ToHttpResponse();
    }
}
