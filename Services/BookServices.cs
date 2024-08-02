using Common;
using Data;
using Entities;
using Microsoft.EntityFrameworkCore;
using Services.Errors;
using Services.Models.BookModels;
using Services.ResponseModels;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BookServices
    {
        private readonly ApplicationDbContext context;
        public BookServices(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Result<GetListOfBook>> GetBooks()
        {
            GetListOfBook getListOfBook = new()
            {
                ListOfBooks = await context.Books
                .Where(x => !x.IsDeleted).AsQueryable()
                .Select(x => new GetBookModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Author = x.Author!.Name,
                    Categories = x.BookCategories!
                    .Select(x => x.Category!.Name)
                    .ToArray()
                }).ToListAsync()
            };

            if(getListOfBook.ListOfBooks.Count == 0)
            {
                return BookErrors.EmptyBookTable;
            }
            return getListOfBook;

        }
        public async Task<Result<GetListOfBook>> FilterBook(GetFilteredBook filter)
        {
            var query = context.Books
                .Where(x => !x.IsDeleted);
              
            if(filter.Title != null)
            {
                query =query.Where(x => x.Title.Contains(filter.Title));
            }
            if(filter.AuthorId != null)
            {
                query = query.Where(x => x.AuthorId == filter.AuthorId);
            }
            if(filter.CategoryId != null)
            {
                query =query.Where(x => x.BookCategories!
                    .Any(x => x.CategoryId == filter.CategoryId.Value));
            }
            GetListOfBook getListOfBook = new()
            {
                ListOfBooks = await query.Select(x => new GetBookModel()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Author = x.Author!.Name,
                    Categories = x.BookCategories!
                    .Select(x => x.Category!.Name)
                    .ToArray()
                }).ToListAsync()
            };
            return getListOfBook;

        }
        public async Task<Result<InfoBookModel>> GetBookInfo(int id)
        {
            var book =context.Books
                .Where(x => !x.IsDeleted && x.Id == id)
                .Select(x => new GetBookModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Author = x.Author!.Name,
                    Categories = x.BookCategories!
                        .Select(x => x.Category!.Name)
                        .ToArray()
                });
            var borrowBooks = context.BorrowedBooks;

            if (book.Count() == 0)
            {
                return BookErrors.BookNotFound;
            }

            InfoBookModel model = new InfoBookModel()
            {
                BookModel =await book!.FirstOrDefaultAsync(),
                BorrowedCount = await borrowBooks.Where(x => x.BookId == id).CountAsync(),
            };
            if (borrowBooks.All(x => x.BookId == id && x.EndDate != null))
            {
                model.Status = Status.Refunded;
            }
            else if (borrowBooks.All(x => x.BookId != id))
            {
                model.Status = Status.Refunded;
            }
            else
            {
                model.Status = Status.Borrowed;
            }
            return model;
        }
        public async Task<Result> AddAsync(CreateBookModel bookModel)
        {
            var author =await context.Authors.FindAsync(bookModel.AuthorId);
            
            if(author == null)
            {
                return AuthorErrors.AuthorNotFound;
            }
            Book book = new Book()
            {
                Title = bookModel.Title,
                PublicationYear = bookModel.PublicationDate,
                Author = author,
                IsDeleted = false,
            };
            foreach(var id in bookModel.CategoriesId)
            {
                var category = await context.Categories.FindAsync(id);
                if (author == null)
                {
                    return CategoryErrors.CategoryNotFound;
                }
                BookCategory bookCategory = new BookCategory()
                {
                    Book = book,
                    CategoryId = id
                };
                context.BookCategories.Add(bookCategory);
            }
            context.Books.Add(book);
            await context.SaveChangesAsync();
            return Result.Success();
        }
        public async Task<Result> DeleteAsync(int id)
        {

            var book = await context.Books.Where(x => x.Id == id)
                .Where(x => !x.IsDeleted)
                .FirstOrDefaultAsync();
            if (book == null)
            {
                return BookErrors.BookNotFound;
            }
            
            book.IsDeleted = true;
            await context.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> UpdateAsync(UpdateBookModel model)
        {
            Book? book = await context.Books
                .FirstOrDefaultAsync(x => x.Id == model.Id);
            if(book == null)
            {
                return BookErrors.BookNotFound;
            }
            book!.Title = model.Title;
            book.PublicationYear = model.PublicationDate;
            var oldCategories = context.Books
                .Where(x => x.Id == model.Id)
                .SelectMany(x =>x.BookCategories!
                .Select(x => x.Category!.Id));

            Author? author = await context.Authors
                .FirstOrDefaultAsync(x => x.Id == model.AuthorId);
            if (author == null)
            {
                return AuthorErrors.AuthorNotFound;
            }

            var addedCategoriesId = model.CategoriesId.Except(oldCategories).ToList();

            var bookCategoriesQuery = context.BookCategories;
            foreach (var id in addedCategoriesId)
            {
                var category = await context.Categories.FindAsync(id);
                if(category == null)
                {
                    return CategoryErrors.CategoryNotFound;
                }
                bookCategoriesQuery.Add(new BookCategory() { BookId = book.Id , CategoryId = id }); 
            }
            
            var deletedCategoriesId =await oldCategories.Except(model.CategoriesId)
                .ToListAsync();

            foreach(var id in deletedCategoriesId)
            {
                var bookCategory = bookCategoriesQuery
                    .Where(x => x.CategoryId == id && x.BookId == book.Id)
                    .FirstOrDefault();
                bookCategoriesQuery.Remove(bookCategory!);
            }
            await context.SaveChangesAsync();
            return Result.Success();
        }
    }
}
