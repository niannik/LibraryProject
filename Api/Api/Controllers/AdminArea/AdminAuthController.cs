using Api.Extensions;
using Application.Auth.Commands.AdminSignIn;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.AdminArea;

[Route("api/seller/auth")]
public class AdminAuthController : ApiController
{
    private readonly IMediator _mediator;

    public AdminAuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<AdminSignInResponse>> SignIn([FromBody] AdminSignInCommand command)
    {
        var result = await _mediator.Send(command, CancellationToken);

        return result.ToHttpResponse();
    }
}
