using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Models;
using Services.Models.UserModels;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserServices userServices;
        public UserController(UserServices userServices)
        {
            this.userServices = userServices;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> Get()
        {
            var users = userServices.ShowUsers();
            return Ok(users);
        }
        
        [HttpPost]
        public async Task<ActionResult> Create(UserModel model)
        {
            await userServices.AddAsync(model);
            return Ok();
        }
        
        [HttpPut]
        public async Task<ActionResult> Update(int id , UserModel model)
        {
            await userServices.UpdateAsync(id, model);
            return Ok();
        }
        [Route("Borrow")]
        [HttpPost]
        public async Task<ActionResult> Borrow(AddUpdateBrrowedBookModel model)
        {
            if(await userServices.UserBorrowABook(model))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
            
        }

        [Route("Refund")]
        [HttpPut]
        public async Task<ActionResult> Refund(int Id)
        {
            await userServices.UserRefundABook(Id);
            return Ok();
        }
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            await userServices.DeleteAsync(id);
            return Ok();
        }
    }
}
