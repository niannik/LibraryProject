using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApi.ExceptionExtensions;
using Services;
using Services.Models.BookModels;
using Services.Models.UserModels;
using Services.ResponseModels;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserBooksController
    {
        private readonly UserBooksServices _userBooksServices;
        public UserBooksController(UserBooksServices services)
        {
            _userBooksServices = services;
        }

        [HttpGet]
        public async Task<ActionResult<GetListOfBook>> GetList()
        {
            var books =await _userBooksServices.GetBooksByUser();
            return books.ToHttpResponse();
        }
        [HttpGet("Status")]
        public async Task<ActionResult<BookInfoResponseModel>> GetById(int id)
        {
            var status = await _userBooksServices.GetBookInfo(id);
            return status.ToHttpResponse();
        }
    }
}
