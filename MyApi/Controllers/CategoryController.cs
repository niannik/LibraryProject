using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Models;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryServices categoryServices;
        public CategoryController(CategoryServices categoryServices)
        {
            this.categoryServices = categoryServices;
        }
        [HttpGet]
        public async Task<ActionResult<Category>> Get(int id)
        {
            var category = await categoryServices.FindById(id);
            return Ok(category);
        }
        [HttpPost]
        public async Task<ActionResult> Create(CategoryModel category)
        {
            await categoryServices.AddAsync(category);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Update(UpdateCategoryModel category)
        {
            await categoryServices.UpdateAsync(category);
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            await categoryServices.DeleteAsync(id);
            return Ok();
        }
    }
}
