using Microsoft.AspNetCore.Mvc;
using Common;
using Services;
using Services.Models.AuthorModels;
using MyApi.ExceptionExtensions;
using Microsoft.AspNetCore.Authorization;
using Services.ResponseModels;

namespace MyApi
{
    [Route("api/[controller]")]
    [Authorize(Roles = Permission.Admin)]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly AuthorServices _authorServices;
        public AuthorController(AuthorServices authorServices)
        {
            _authorServices = authorServices;
        }
        [HttpGet]
        public async Task<ActionResult<GetListOfAuthors>> Get()
        {
            var authors = await _authorServices.GetAuthors();
            return authors.ToHttpResponse();
        }
        [HttpPost]
        public async Task<ActionResult> Create(CreateAuthorModel author)
        {
            Result result = await _authorServices.AddAsync(author);
            return result.ToHttpResponse();
        }

        [HttpPut]
        public async Task<ActionResult> Update(UpdateAuthorModel model)
        {
            Result result = await _authorServices.UpdateAsync(model);
            return result.ToHttpResponse();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            Result result = await _authorServices.DeleteAsync(id);
            return result.ToHttpResponse();
        }
    }
}
