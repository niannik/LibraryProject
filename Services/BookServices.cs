using Data;
using Entities;
using Microsoft.EntityFrameworkCore;
using Services.Models.BookModels;
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

        public async Task<ShowBookModel?> ShowABook(int id)
        {
            var book = await context.Books
                .Where(x => !x.IsDeleted &&  x.Id == id)
                .Select(x => new ShowBookModel
                {
                    Title = x.Title,
                    Author = x.Author!.Name,
                    Categories = x.BookCategories!
                    .Select(x => x.Category!.Name)
                    .ToArray()
                }).FirstOrDefaultAsync();
          
            return book;
        }
        public async Task<List<ShowBookModel>>? ShowBooks()
        {
            var book = await context.Books
                .Where(x => !x.IsDeleted).AsQueryable()
                .Select(x => new ShowBookModel
                {
                    Title = x.Title,
                    Author = x.Author!.Name,
                    Categories = x.BookCategories!
                    .Select(x => x.Category!.Name)
                    .ToArray()
                }).ToListAsync();

            return book;

        }
        public async Task<List<ShowBookModel>> ShowBook(ShowFilteredBook filter)
        {
            var query = context.Books
                .Where(x => !x.IsDeleted);
              
            if(filter.Title != null)
            {
                query = query.Where(x => x.Title == filter.Title);
            }
            if(filter.AuthorId != null)
            {
                query = query.Where(x => x.AuthorId == filter.AuthorId);
            }
            if(filter.CategoryId != null)
            {
                query = query.Where(x => x.BookCategories!
                .Any(x => x.CategoryId == filter.CategoryId.Value));
            }

            return query.Select(x => new ShowBookModel() {
                Title = x.Title,
                Author = x.Author!.Name,
                Categories = x.BookCategories!.Select(x => x.Category!.Name).ToArray()
            }).ToList();

        }
        public async Task<InfoBookModel> ShowBookInfo(int id)
        {
            InfoBookModel model = new InfoBookModel()
            {
                BookModel = await ShowABook(id),
                BorrowedCount = await context.BorrowedBooks
                .Where(x => x.BookId == id)
                .CountAsync(),
            };
            if(context.BorrowedBooks.All(x=> x.BookId == id && x.EndDate != null))
            {
                model.Status = Status.Refunded;
            }
            else if(context.BorrowedBooks.All(x => x.BookId != id))
            {
                model.Status = Status.Refunded;
            }
            else
            {
                model.Status = Status.Borrowed;
            }
            return model;
        }
        public virtual async Task<Book?> FindById(int id)
        {

            var book = await context.Books.Where(x => x.Id == id)
                .Where(x => !x.IsDeleted)
                .FirstOrDefaultAsync();
            if (book == null)
            {
                //
            }
            return book;
        }

        public async Task AddAsync(CreateBookModel bookModel)
        {
            Author? author =await context.Authors.FindAsync(bookModel.AuthorId);
            //Category? category = await context.Categories.FindAsync(bookCategoryId);
            if(author == null)
            {
                ArgumentException.ThrowIfNullOrEmpty(nameof(author));
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
                BookCategory bookCategory = new BookCategory()
                {
                    Book = book,
                    CategoryId = id
                };
                context.BookCategories.Add(bookCategory);
            }
            context.Books.Add(book);
            await context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            Book? book = await FindById(id);
            if(book != null)
            {
                book.IsDeleted = true;
            }
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateBookModel model)
        {
            Book? book = await context.Books
                .FirstOrDefaultAsync(x => x.Id == model.Id);

            book!.Title = model.Title;
            book.PublicationYear = model.PublicationDate;
            List <BookCategory> oldCategories = new List<BookCategory>();
            oldCategories =  context.Books
                .Where(x => x.Id == model.Id)
                .SelectMany(x =>x.BookCategories!)
                .ToList();
            foreach(var category in oldCategories)
            {
                context.BookCategories.Remove(category);
            }
            List<BookCategory> newBookCategories = new List<BookCategory>();
            foreach (int id in model.CategoriesId)
            {
                Category? category = await context.Categories.FindAsync(id);
                BookCategory bookCategory = new BookCategory()
                {
                    Category = category,
                    Book = book
                };
                newBookCategories.Add(bookCategory);

            }
            book.BookCategories = newBookCategories;

            await context.SaveChangesAsync();

        }
    }
}
