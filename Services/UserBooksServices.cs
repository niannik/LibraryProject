using Common;
using Data;
using Microsoft.EntityFrameworkCore;
using Services.Errors;
using Services.Models.BookModels;
using Services.Models.UserModels;
using Services.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserBooksServices
    {
        private readonly ApplicationDbContext _context;
        private readonly BookServices _bookServices;
        public UserBooksServices(ApplicationDbContext context, BookServices bookServices)
        {
            _context = context;
            _bookServices = bookServices;
        }

        public async Task<Result<GetListOfBook>> GetBooksByUser()
        {
            var books =await _bookServices.GetBooks();
            return books;

        }

        public async Task<Result<BookInfoResponseModel?>> GetBookInfo(int id)
        {
            var query = _context.BorrowedBooks.Where(x => x.BookId == id);
            if(query.Count() == 0)
            {
                return BookErrors.BookNotFound;
            }
            if(await query.AnyAsync(x => x.EndDate == null))
            {
                var newQery = query.Select(x => new BookInfoResponseModel
                {
                    Id = x.Id,
                    Title = x.Book!.Title,
                    Author = x.Book.Author!.Name,
                    Categories  = x.Book.BookCategories!.Select(x => x.Category!.Name).ToList(),
                    Accessibility = "Borrowed"
                }).FirstOrDefaultAsync();
                return await newQery;
            }
            else
            {
                var newQery = await query.Select(x => new BookInfoResponseModel
                {
                    Id = x.Id,
                    Title = x.Book!.Title,
                    Author = x.Book.Author!.Name,
                    Categories = x.Book.BookCategories!.Select(x => x.Category!.Name).ToList(),
                    Accessibility = "Accessible"
                }).FirstOrDefaultAsync();
                return newQery;
            }
            
        }
    }
}
