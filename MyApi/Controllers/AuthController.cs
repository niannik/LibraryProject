using Microsoft.AspNetCore.Mvc;
using MyApi.ExceptionExtensions;
using Services;
using Services.ResponseModels;

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
        public async Task<ActionResult<LoginResponse>> SignIn(string userName, string password)
        {
            var token = await _authServices.LogInWithPassword(userName, password);
            return token.ToHttpResponse();
        }
    }
}
