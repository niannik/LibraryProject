using Common;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyApi.ExceptionExtensions;
using Services;
using Services.Models;
using Services.Models.UserModels;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = Permission.Admin)]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserServices _userServices;
        public UserController(UserServices userServices)
        {
            this._userServices = userServices;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> Get()
        {
            var users =await _userServices.ShowUsers();
            return users.ToHttpResponse();
        }

        [HttpPost]
        public async Task<ActionResult> Create(UserModel model)
        {
            Result result = await _userServices.AddAsync(model);
            return result.ToHttpResponse();
        }
        
        [HttpPut]
        public async Task<ActionResult> Update(int id , UserModel model)
        {
            Result result =await _userServices.UpdateAsync(id, model);
            return result.ToHttpResponse();
        }
        [Route("Borrow")]
        [HttpPost]
        public async Task<ActionResult> Borrow(AddUpdateBrrowedBookModel model)
        {
            
            Result result = await _userServices.UserBorrowABook(model);
            return result.ToHttpResponse();
        }

        [Route("Refund")]
        [HttpPut]
        public async Task<ActionResult> Refund(int Id)
        {
            Result result = await _userServices.UserRefundABook(Id);
            return result.ToHttpResponse();
        }
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            Result result =  await _userServices.DeleteAsync(id);
            return result.ToHttpResponse();
        }
    }
}
