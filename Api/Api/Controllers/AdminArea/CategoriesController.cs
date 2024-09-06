using Api.Authorization;
using Api.Extensions;
using Application.Categories.Commands.AdminCreateCategory;
using Application.Categories.Commands.AdminUpdateCategory;
using Application.Categories.Queries.AdminGetCategories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.AdminArea;

[Route("api/admin/[Controller]")]
[Authorize(Policy = AppAuthorizationPolicies.Admin)]

public class CategoriesController : ApiController
{
    private readonly IMediator _mediator;
    public CategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<AdminGetCategoriesResponse>> GetAll()
    {
        var query = new AdminGetCategoriesQuery();

        var result = await _mediator.Send(query, CancellationToken);

        return result.ToHttpResponse();
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody]AdminCreateCategoryCommand command)
    {
        var result = await _mediator.Send(command, CancellationToken);

        return result.ToHttpResponse();
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update([FromRoute]int id ,[FromBody]AdminUpdateCategoryDto dto)
    {
        var command = new AdminUpdateCategoryCommand
        {
            Id = id,
            Title = dto.Title,
        };

        var result = await _mediator.Send(command, CancellationToken);

        return result.ToHttpResponse();
    }
}
