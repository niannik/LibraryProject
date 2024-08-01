using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApi.ExceptionExtensions;
using Services;
using Services.Models.BookModels;
using Services.Models.UserModels;

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
        public async Task<ActionResult<List<GetBookModel>>> GetList()
        {
            var books =await _userBooksServices.ShowBooks();
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
