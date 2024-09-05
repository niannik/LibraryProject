using Api.Extensions;
using Application.Auth.Commands.UserSignIn;
using Application.Auth.Commands.UserSignUp;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.UserArea;

[Route("api/auth")]
public class UserAuthController : ApiController
{
    private readonly IMediator _mediator;

    public UserAuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("signUp")]
    public async Task<ActionResult> SignUp([FromBody] UserSignUpCommand command)
    {
        var result = await _mediator.Send(command, CancellationToken);

        return result.ToHttpResponse();
    }

    [HttpPost("signIn")]
    public async Task<ActionResult<UserSignInResponse>> SignIn([FromBody] UserSignInCommand command)
    {
        var result = await _mediator.Send(command, CancellationToken);

        return result.ToHttpResponse();
    }
}

