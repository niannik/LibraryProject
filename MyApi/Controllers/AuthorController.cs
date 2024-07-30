using Microsoft.AspNetCore.Mvc;
using Common;
using Services;
using Services.Models.AuthorModels;
using MyApi.ExceptionExtensions;

namespace MyApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly AuthorServices authorServices;
        public AuthorController(AuthorServices authorServices)
        {
            this.authorServices = authorServices;
        }
        [HttpGet]
        public async Task<ActionResult<List<ShowAuthorModel>>> Get()
        {
            var authors = await authorServices.ShowAuthors();
            return authors.ToHttpResponse();
        }
        [HttpPost]
        public async Task<ActionResult> Create(CreateAuthorModel author)
        {
            Result result = await authorServices.AddAsync(author);
            return result.ToHttpResponse();
        }

        [HttpPut]
        public async Task<ActionResult> Update(UpdateAuthorModel model)
        {
            Result result = await authorServices.UpdateAsync(model);
            return result.ToHttpResponse();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            Result result = await authorServices.DeleteAsync(id);
            return result.ToHttpResponse();
        }
    }
}
