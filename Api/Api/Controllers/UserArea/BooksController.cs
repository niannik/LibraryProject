using Api.Authorization;
using Api.Extensions;
using Application.Books.Queries.UserGetBooks;
using Application.Books.Queries.UserGetFilteredBooks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.UserArea;

[Route("api/[Controller]")]
[Authorize(Policy = AppAuthorizationPolicies.User)]
public class BooksController : ApiController
{
    private readonly IMediator _mediator;
    public BooksController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet]
    public async Task<ActionResult<UserGetBooksResponse>> GetAll([FromQuery] UserGetBooksQuery query)
    {
        var result = await _mediator.Send(query, CancellationToken);

        return result.ToHttpResponse();
    }

    [HttpGet("filter")]
    public async Task<ActionResult<UserGetFilteredBooksResponse>> GetFiltered([FromQuery] UserGetFilteredBooksQuery query)
    {
        var result = await _mediator.Send(query, CancellationToken);

        return result.ToHttpResponse();
    }
}
