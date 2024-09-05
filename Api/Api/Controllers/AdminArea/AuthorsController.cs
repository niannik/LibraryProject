using Api.Authorization;
using Api.Extensions;
using Application.Authors.Commands.AdminCreateAuthor;
using Application.Authors.Commands.AdminDeleteAuthor;
using Application.Authors.Commands.AdminUpdateAuthor;
using Application.Authors.Queries.AdminGetAuthors;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.AdminArea;

[Route("api/admin/[Controller]")]
[Authorize(Policy = AppAuthorizationPolicies.Admin)]
public class AuthorsController : ApiController
{
    private readonly IMediator _mediator;
    public AuthorsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<AdminGetAuthorsResponse>> GetAll()
    {
        var query = new AdminGetAuthorsQuery();

        var result = await _mediator.Send(query, CancellationToken);

        return result.ToHttpResponse();
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] AdminCreateAuthorCommand command)
    {
        var result = await _mediator.Send(command, CancellationToken);

        return result.ToHttpResponse();
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update([FromRoute] int id,[FromBody] AdminUpdateAuthorDto dto)
    {
        var command = new AdminUpdateAuthorCommand
        {
            Id = id,
            Email = dto.Email,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Address = dto.Address,
            PhoneNumber = dto.PhoneNumber,
        };

        var result = await _mediator.Send(command, CancellationToken);

        return result.ToHttpResponse();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete([FromRoute] int id)
    {
        var command = new AdminDeleteAuthorCommand
        {
            Id = id
        };

        var result = await _mediator.Send(command, CancellationToken);

        return result.ToHttpResponse();
    }
}
