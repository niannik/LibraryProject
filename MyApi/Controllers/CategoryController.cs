using Common;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApi.ExceptionExtensions;
using Services;
using Services.Models;
using Services.Models.CategoryModels;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = Permission.Admin)]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryServices _categoryServices;
        public CategoryController(CategoryServices categoryServices)
        {
            this._categoryServices = categoryServices;
        }
        [HttpGet]
        public async Task<ActionResult<List<GetCategoriesModel>>> Get()
        {
            var categories = await _categoryServices.ShowCategories();
            return categories.ToHttpResponse();
        }
        [HttpPost]
        public async Task<ActionResult> Create(CategoryModel category)
        {
            Result result =await _categoryServices.AddAsync(category);
            return result.ToHttpResponse();
        }

        [HttpPut]
        public async Task<ActionResult> Update(UpdateCategoryModel category)
        {
            Result result = await _categoryServices.UpdateAsync(category);
            return result.ToHttpResponse();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            Result result = await _categoryServices.DeleteAsync(id);
            return result.ToHttpResponse();
        }
    }
}
