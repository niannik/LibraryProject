using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[Controller]")]
public abstract class ApiController : ControllerBase
{
    protected CancellationToken CancellationToken => HttpContext.RequestAborted;
}