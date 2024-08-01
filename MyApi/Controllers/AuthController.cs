using Microsoft.AspNetCore.Mvc;
using MyApi.ExceptionExtensions;
using Services;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthServices _authServices;
        public AuthController(AuthServices authServices)
        {
            _authServices = authServices;
        }

        [HttpGet]
        public async Task<ActionResult<string>> SignIn(string userName, string password)
        {
            var token = await _authServices.GenerateToken(userName, password);
            return token.ToHttpResponse();
        }
    }
}
