using Common;
using Data;
using Entities;
using Microsoft.EntityFrameworkCore;
using Services.Errors;
using Services.Models;
using Services.Models.AuthorModels;
using Services.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserServices
    {
        private readonly ApplicationDbContext context;
        private readonly BorrowedBookServices borrowedBookServices;

        public UserServices(ApplicationDbContext context, BorrowedBookServices borrowedBookServices)
        {
            this.context = context;
            this.borrowedBookServices = borrowedBookServices;
        }
        public async Task<Result<List<User>>> ShowUsers()
        {
            var users =await context.Users.AsNoTracking().ToListAsync();
            if(users.Count == 0)
            {
                return UserErrors.EmptyUserTable;
            }
            return users;
        }

        public async Task<User?> FindById(int id)
        {
            var user = await context.Users.Where(x => x.Id == id)
                .Where(x => !x.IsDeleted)
                .FirstOrDefaultAsync();
            return user;
        }


        public async Task<Result> AddAsync(UserModel model)
        {
            User user = new User()
            {
                Name = model.Name,
                Address = model.Address,
                Phone = model.Phone
            };
            context.Users.Add(user);
            await context.SaveChangesAsync();
            return Result.Success();
        }
        public async Task<Result> DeleteAsync(int id)
        {
            User? user = await FindById(id);
            if(user == null)
            {
                return UserErrors.UserNotFound;
            }
            user.IsDeleted = true;
            await context.SaveChangesAsync();
            return Result.Success();
        }

        public async Task<Result> UpdateAsync(int id, UserModel model)
        {
            User? user = await FindById(id);
            if (user == null)
            {
                return UserErrors.UserNotFound;
            }
            user.Name = model.Name;
            user.Address = model.Address;
            user.Phone = model.Phone;
            return Result.Success();
        }

        public async Task<Result> UserBorrowABook(AddUpdateBrrowedBookModel model)
        {
            Result result = await borrowedBookServices.AddBorrowedBook(model);
            if (result.IsSucceeded)
            {
                return Result.Success();
            }
            else
            {
                return BorrowedBooksErrors.InvalidBorrow;
            }
        }

        public async Task<Result> UserRefundABook(int id)
        {
            Result result = await borrowedBookServices.UpdateStatusToRefunded(id);
            if (result.IsSucceeded)
            {
                return Result.Success();
            }
            else
            {
                return BorrowedBooksErrors.InvalidBorrow;
            }
        }
    }
}
