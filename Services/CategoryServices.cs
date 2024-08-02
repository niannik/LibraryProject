using Common;
using Data;
using Entities;
using Microsoft.EntityFrameworkCore;
using Services.Errors;
using Services.Models;
using Services.Models.AuthorModels;
using Services.Models.CategoryModels;
using Services.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Services.Models.CategoryModel;

namespace Services
{
    public class CategoryServices
    {
        private readonly ApplicationDbContext context;
        public CategoryServices(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<Result<GetListOfCategories>> GetCategories()
        {
            GetListOfCategories listOfCategories = new()
            {
                CategoriesList = await context.Categories
                .Select(x => new GetCategoriesModel
                {
                    Id = x.Id,
                    Name = x.Name,

                }).ToListAsync()
            };

            if (listOfCategories.CategoriesList.Count == 0)
            {
                return CategoryErrors.EmptyCategoryTable;
            }
            return listOfCategories;
        }
        public async Task<Category?> FindById(int id)
        {

            var category = await context.Categories.Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            return category;
        }
        public async Task<Result> AddAsync(CategoryModel model)
        {
            Category category = new Category()
            {
                Name = model.Name
            };
            context.Categories.Add(category);
            await context.SaveChangesAsync();
            return Result.Success();
        }
        public async Task<Result> DeleteAsync(int id)
        {
            Category? category = await FindById(id);
            if (category == null)
            {
                return CategoryErrors.CategoryNotFound;
            }
            context.Categories.Remove(category);
            await context.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> UpdateAsync(UpdateCategoryModel model)
        {
            Category? category = await FindById(model.Id);
            if (category == null)
            {
                return CategoryErrors.CategoryNotFound;
            }
            category.Name = model.Name;
            await context.SaveChangesAsync();
            return Result.Success();
        }
    }
}
