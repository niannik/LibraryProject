using Common;
using Data;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.Errors;
using Services.Models;
using Services.Models.AuthorModels;
using Services.Models.UserModels;
using Services.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services
{
    public class UserServices
    {
        private readonly ApplicationDbContext context;
        private readonly BorrowedBookServices borrowedBookServices;
        private readonly JwtServices jwtServices;

        public UserServices(ApplicationDbContext context, BorrowedBookServices borrowedBookServices , JwtServices jwtServices)
        {
            this.context = context;
            this.borrowedBookServices = borrowedBookServices;
            this.jwtServices = jwtServices;
        }
        public async Task<Result<GetListOfUsers>> GetUsers()
        {
            GetListOfUsers listOfUsers = new()
            {
                ListOfUsers = await context.Users.AsNoTracking().ToListAsync()
            };
            if(listOfUsers.ListOfUsers.Count == 0)
            {
                return UserErrors.EmptyUserTable;
            }
            return listOfUsers;
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
            var hashedPassword = PasswordHasher.GetSha256Hash(model.Password);
            User user = new User()
            {
                UserName = model.UserName,
                PasswordHash = hashedPassword,
                SecurityStamp = Guid.NewGuid().ToString(),
                Address = model.Address,
                Phone = model.Phone,
                LastLoginDate = DateTime.Now
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
            user.UserName = model.UserName;
            user.Address = model.Address;
            user.Phone = model.Phone;
            return Result.Success();
        }

        public async Task UpdateLastLoginDateAsync(User user)
        {
            user.LastLoginDate = DateTime.Now;
            context.Users.Update(user);
            await context.SaveChangesAsync();
        }
        public async Task<Result> UserBorrowABook(AddUpdateBrrowedBookModel model)
        {
            Result result = await borrowedBookServices.AddBorrowedBook(model.UserId , model.BookId);
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
