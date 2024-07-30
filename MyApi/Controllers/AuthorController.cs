using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Models.AuthorModels;

namespace MyApi.Controllers
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
        public async Task<List<ShowAuthorModel>> Get()
        {
            var authors =await authorServices.ShowAuthors();
            return authors;
        }
        [HttpPost]
        public async Task<ActionResult> Create(CreateAuthorModel author)
        {
            await authorServices.AddAsync(author);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Update(UpdateAuthorModel model)
        {
            await authorServices.UpdateAsync(model);
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            await authorServices.DeleteAsync(id);
            return Ok();
        }
    }
}
