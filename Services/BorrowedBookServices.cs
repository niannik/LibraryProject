﻿using Data;
using Entities;
using Microsoft.EntityFrameworkCore;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<bool> AddBorrowedBook(AddUpdateBrrowedBookModel model)
        {
            var bookIsAlreadyInBorrowed = await context.BorrowedBooks.AnyAsync(x => x.BookId == model.BookId && x.EndDate == null);
            if (bookIsAlreadyInBorrowed)
            {
                // book already in use
                return false;
            }

            if(await context.Books.AnyAsync(x => x.Id == model.BookId) == false)
            {
                // book not found
                return false;
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
            return true;
        }
        public async Task<BorrowedBook?> FindById(int id)
        {

            var borrowedBook = await context.BorrowedBooks.Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            if (borrowedBook == null)
            {
                //
            }
            return borrowedBook;
        }
        public async Task UpdateStatusToRefunded(int id)
        {
            var borrowedBook =await FindById(id);
            if(borrowedBook!.EndDate == null)
            {
                borrowedBook.EndDate = DateTime.Now;
                await context.SaveChangesAsync();
            }

            else
            {
                //Exception
            }
        }
       
        public async Task<List<BorrowedBookModel>> CurrentBorrowedBooks()
        {
            
            var books = await context.BorrowedBooks
                .Include(x => x.User)
                .Include(x => x.Book)!
                .ThenInclude(x => x.Author)
                .Include(x => x.Book)!
                .ThenInclude(x => x.BookCategories)!
                .ThenInclude(x => x.Category)
                .Where(x => x.EndDate == null)
                .Select(x => new BorrowedBookModel
                {
                    Username = x.User!.Name,
                    BookInfo = new BorrowedBookModel.BookDto()
                    {
                        Title = x.Book!.Title,
                        Author = x.Book.Author!.Name,
                        Categories = x.Book.BookCategories!
                        .Select(x => x.Category!.Name)
                        .ToList()
                    }
                })
                .ToListAsync();
            return books;
        }
        public async Task<List<BorrowedBookModel>> PreviousBorrowedBook()
        {
            var books = await context.BorrowedBooks
                .Include(x => x.User)
                .Include(x => x.Book)!
                .ThenInclude(x => x.Author)
                .Include(x => x.Book)!
                .ThenInclude(x => x.BookCategories)!
                .ThenInclude(x => x.Category)
                .Where(x => x.EndDate != null)
                .Select(x => new BorrowedBookModel
                {
                    Username = x.User!.Name,
                    BookInfo = new BorrowedBookModel.BookDto()
                    {
                        Title = x.Book!.Title,
                        Author = x.Book.Author!.Name,
                        Categories = x.Book.BookCategories!
                        .Select(x => x.Category!.Name)
                        .ToList()
                    }
                })
                .ToListAsync();
            return books;
        }
    }
}