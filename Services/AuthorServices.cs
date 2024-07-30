using Common;
using Data;
using Entities;
using Microsoft.EntityFrameworkCore;
using Services.Errors;
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
        public async Task<Result<List<ShowAuthorModel>>> ShowAuthors()
        {
            var authors =await context.Authors
                .Select(x => new ShowAuthorModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    BooksCount = x.Books!.Count(),

                }).ToListAsync();
            if (authors.Count == 0)
            {
                return AuthorErrors.EmptyAuthorTable;
            }
            return authors;
        }

        public async Task<Author?> FindById(int id)
        {
            var author = await context.Authors.Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            return author;
        }

        public async Task<Result> AddAsync(CreateAuthorModel model)
        {
            var author = new Author()
            {
                Name = model.Name
            };
            context.Authors.Add(author);
            await context.SaveChangesAsync();
            return Result.Success();
        }
        public async Task<Result> DeleteAsync(int id)
        {
            Author? author = await FindById(id);
            if(author == null)
            {
                return AuthorErrors.AuthorNotFound;
            }
            context.Authors.Remove(author);
            await context.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> UpdateAsync(UpdateAuthorModel model)
        {
            Author? oldAuthor = await FindById(model.Id);
            if(oldAuthor == null)
            {
                return AuthorErrors.AuthorNotFound;
            }
            oldAuthor.Name = model.Name;
            await context.SaveChangesAsync();
            return Result.Success();
        }  

    }
}
