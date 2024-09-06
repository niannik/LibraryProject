using Api.Authorization;
using Api.Extensions;
using Application.Categories.Queries.AdminGetCategories;
using Application.Categories.Queries.UserGetCategories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.UserArea;

[Route("api/[Controller]")]
[Authorize(Policy = AppAuthorizationPolicies.User)]

public class CategoriesController : ApiController
{
    private readonly IMediator _mediator;
    public CategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<UserGetCategoriesResponse>> GetAll()
    {
        var query = new UserGetCategoriesQuery();

        var result = await _mediator.Send(query, CancellationToken);

        return result.ToHttpResponse();
    }
}
