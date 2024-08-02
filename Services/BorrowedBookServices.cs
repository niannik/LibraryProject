using Common;
using Data;
using Entities;
using Microsoft.EntityFrameworkCore;
using Services.Errors;
using Services.Models;
using Services.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BorrowedBookServices
    {
        private readonly ApplicationDbContext context;
        public BorrowedBookServices(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<Result> AddBorrowedBook(int userId, int bookId)
        {
            var model = new AddUpdateBrrowedBookModel
            {
                BookId = bookId,
                UserId = userId,
                StartDate = DateTime.Now,
            };
            var bookIsAlreadyInBorrowed = await context.BorrowedBooks.AnyAsync(x => x.BookId == model.BookId && x.EndDate == null);
            if (bookIsAlreadyInBorrowed)
            {
                return BorrowedBooksErrors.InvalidBorrow;
            }

            if(await context.Books.AnyAsync(x => x.Id == model.BookId) == false)
            {
                return BookErrors.BookNotFound;
            }

            if(await context.BorrowedBooks.AnyAsync(x => x.UserId == userId && x.EndDate == null))
            {
                return BorrowedBooksErrors.AlreadyHaveABook;
            }
            var borrow = new BorrowedBook()
            {
                StartDate = model.StartDate,
                EndDate = null,
                BookId = model.BookId,
                UserId = model.UserId,
                Book = await context.Books.FindAsync(model.BookId),
                User = await context.Users.FindAsync(model.UserId)

            };
            context.BorrowedBooks.Add(borrow);
            await context.SaveChangesAsync();
            return Result.Success();
        }
        public async Task<BorrowedBook?> FindById(int id)
        {

            var borrowedBook = await context.BorrowedBooks.Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            return borrowedBook;
        }
        public async Task<Result> UpdateStatusToRefunded(int borrowId)
        {
            var borrowedBook =await FindById(borrowId);
            if(borrowedBook == null)
            {
                return BookErrors.BookNotFound;
            }
            if(borrowedBook!.EndDate == null)
            {
                borrowedBook.EndDate = DateTime.Now;
                await context.SaveChangesAsync();
                return Result.Success();
            }

            else
            {
                return BorrowedBooksErrors.InvalidBorrow;
            }
        }
       
        public async Task<GetListOfBorrowedBook> CurrentBorrowedBooks(int id)
        {
            GetListOfBorrowedBook getListOfBorrowedBook = new()
            {
                BorrowedBookList = await context.BorrowedBooks
                .Where(x => x.UserId == id)
                .Include(x => x.User)
                .Include(x => x.Book)!
                .ThenInclude(x => x.Author)
                .Include(x => x.Book)!
                .ThenInclude(x => x.BookCategories)!
                .ThenInclude(x => x.Category)
                .Where(x => x.EndDate == null)
                .Select(x => new BorrowedBookModel
                {
                    Id = x.Id,
                    Username = x.User!.UserName!,
                    BookInfo = new BorrowedBookModel.BookDto()
                    {
                        Title = x.Book!.Title,
                        Author = x.Book.Author!.Name,
                        Categories = x.Book.BookCategories!
                        .Select(x => x.Category!.Name)
                        .ToList()
                    }
                })
                .ToListAsync()
            };
            return getListOfBorrowedBook;
        }
        public async Task<GetListOfBorrowedBook> PreviousBorrowedBook(int id)
        {
            GetListOfBorrowedBook getListOfBorrowedBook = new()
            {
                BorrowedBookList = await context.BorrowedBooks
                .Where(x => x.UserId == id)
                .Include(x => x.User)
                .Include(x => x.Book)!
                .ThenInclude(x => x.Author)
                .Include(x => x.Book)!
                .ThenInclude(x => x.BookCategories)!
                .ThenInclude(x => x.Category)
                .Where(x => x.EndDate != null)
                .Select(x => new BorrowedBookModel
                {
                    Id = x.Id,
                    Username = x.User!.UserName!,
                    BookInfo = new BorrowedBookModel.BookDto()
                    {
                        Title = x.Book!.Title,
                        Author = x.Book.Author!.Name,
                        Categories = x.Book.BookCategories!
                        .Select(x => x.Category!.Name)
                        .ToList()
                    }
                })
                .ToListAsync()
            };

            return getListOfBorrowedBook;
        }
    }
}
