using Data;
using Entities;
using Microsoft.EntityFrameworkCore;
using Services.Models;
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
        public async Task<List<User>> ShowUsers()
        {
            var users =await context.Users.AsNoTracking().ToListAsync();
            return users;
        }

        public async Task<User> FindById(int id)
        {
            var user = await context.Users.Where(x => x.Id == id)
                .Where(x => !x.IsDeleted)
                .FirstOrDefaultAsync();
            return user;
        }

        public async Task AddAsync(UserModel model)
        {
            User user = new User()
            {
                Name = model.Name,
                Address = model.Address,
                Phone = model.Phone
            };
            context.Users.Add(user);
            await context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            User user = await FindById(id);
            user.IsDeleted = true;
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, UserModel model)
        {
            User oldUser = await FindById(id);
            oldUser.Name = model.Name;
            oldUser.Address = model.Address;
            oldUser.Phone = model.Phone;
        }

        public async Task<bool> UserBorrowABook(AddUpdateBrrowedBookModel model)
        {
            if (await borrowedBookServices.AddBorrowedBook(model))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task UserRefundABook(int id)
        {
            await borrowedBookServices.UpdateStatusToRefunded(id);
        }
    }
}
