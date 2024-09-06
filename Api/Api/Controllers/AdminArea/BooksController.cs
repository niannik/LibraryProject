using Api.Authorization;
using Api.Extensions;
using Application.Books.Commands.AdminCreateBook;
using Application.Books.Commands.AdminDeleteBook;
using Application.Books.Commands.AdminUpdateBook;
using Application.Books.Queries.AdminGetBooks;
using Application.Books.Queries.AdminGetFilteredBooks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.AdminArea;

[Route("api/admin/[Controller]")]
[Authorize(Policy = AppAuthorizationPolicies.Admin)]
public class BooksController : ApiController
{
    private readonly IMediator _mediator;
    public BooksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<AdminGetBooksResponse>> GetAll([FromQuery] AdminGetBooksQuery query)
    {
        var result = await _mediator.Send(query, CancellationToken);

        return result.ToHttpResponse();
    }
    
    [HttpGet("filter")]
    public async Task<ActionResult<AdminGetFilteredBooksResponse>> GetFiltered([FromQuery] AdminGetFilteredBooksQuery query)
    {
        var result = await _mediator.Send(query, CancellationToken);

        return result.ToHttpResponse();
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] AdminCreateBookCommand command)
    {
        var result = await _mediator.Send(command, CancellationToken);

        return result.ToHttpResponse();
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update([FromRoute] int id,[FromBody] AdminUpdateBookDto dto)
    {
        var command = new AdminUpdateBookCommand
        {
            Id = id,
            AuthorId = dto.AuthorId,
            Title = dto.Title,
            PublicationYear = dto.PublicationYear,
            CategoryIds = dto.CategoryIds,
        };

        var result = await _mediator.Send(command, CancellationToken);

        return result.ToHttpResponse();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        var command = new AdminDeleteBookCommand
        {
            Id = id,
        };

        var result = await _mediator.Send(command, CancellationToken);

        return result.ToHttpResponse();
    }

}
