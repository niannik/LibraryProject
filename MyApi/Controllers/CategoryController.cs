using Common;
using Entities;
using Microsoft.AspNetCore.Mvc;
using MyApi.ExceptionExtensions;
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
        public async Task<ActionResult<List<ShowCategoriesModel>>> Get()
        {
            var categories = await categoryServices.ShowCategories();
            return categories.ToHttpResponse();
        }
        [HttpPost]
        public async Task<ActionResult> Create(CategoryModel category)
        {
            Result result =await categoryServices.AddAsync(category);
            return result.ToHttpResponse();
        }

        [HttpPut]
        public async Task<ActionResult> Update(UpdateCategoryModel category)
        {
            Result result = await categoryServices.UpdateAsync(category);
            return result.ToHttpResponse();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            Result result = await categoryServices.DeleteAsync(id);
            return result.ToHttpResponse();
        }
    }
}
