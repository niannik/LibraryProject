using Data;
using Entities;
using Microsoft.EntityFrameworkCore;
using Services.Models;
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
        public async Task<Category> FindById(int id)
        {

            var category = await context.Categories.Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            if (category == null)
            {
                //
            }
            return category;
        }
        public async Task AddAsync(CategoryModel model)
        {
            Category category = new Category()
            {
                Name = model.Name

            };
            context.Categories.Add(category);
            await context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            Category category = await FindById(id);
            context.Categories.Remove(category);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateCategoryModel model)
        {
            Category oldCategory = await FindById(model.Id);
            oldCategory.Name = model.Name;
            await context.SaveChangesAsync();
        }
    }
}
