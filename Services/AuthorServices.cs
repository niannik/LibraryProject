using Data;
using Entities;
using Microsoft.EntityFrameworkCore;
using Services.Models.AuthorModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AuthorServices
    {
        private readonly ApplicationDbContext context;

        public AuthorServices(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<List<ShowAuthorModel>> ShowAuthors()
        {
            var authors =await context.Authors
                .Select(x => new ShowAuthorModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    BooksCount = x.Books!.Count(),

                }).ToListAsync();
            return authors;
        }

        public async Task<Author?> FindById(int id)
        {
            var author = await context.Authors.Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            return author;
        }

        public async Task AddAsync(CreateAuthorModel model)
        {
            var author = new Author()
            {
                Name = model.Name
            };
            context.Authors.Add(author);
            await context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            Author? author = await FindById(id);
            if(author != null)
            {
                context.Authors.Remove(author);
            }
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateAuthorModel model)
        {
            Author? oldAuthor = await FindById(model.Id);
            if(oldAuthor != null)
            {
                oldAuthor.Name = model.Name;
            }
            await context.SaveChangesAsync();
        }  

    }
}
