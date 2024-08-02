using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApi.ExceptionExtensions;
using Services;
using Services.Models;
using System.Security.Claims;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class BorrowedBookController : ControllerBase
    {
        private readonly BorrowedBookServices _borrowedBookServices;
        public BorrowedBookController(BorrowedBookServices borrowedBookServices)
        {
            this._borrowedBookServices = borrowedBookServices;
        }

        [Route("Current")]

        [HttpGet]
        public async Task<ActionResult<List<BorrowedBookModel>>> GetCurrent()
        {
            var idStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var id = int.Parse(idStr!);
            var books =await _borrowedBookServices.CurrentBorrowedBooks(id);
            return Ok(books);
        }
        [Route("Previous")]
        [HttpGet]
        public async Task<ActionResult<List<BorrowedBookModel>>> GetPrevious()
        {
            var str = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var id = int.Parse(str!);
            var books = await _borrowedBookServices.PreviousBorrowedBook(id);
            return Ok(books);
        }
        [Route("Borrow")]
        [HttpPost]
        public async Task<ActionResult> Borrow(int bookId)
        {
            var str = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var id = int.Parse(str!);
            Result result = await _borrowedBookServices.AddBorrowedBook(id , bookId);
            return result.ToHttpResponse();
        }

        [Route("Refund")]
        [HttpPut]
        public async Task<ActionResult> Refund(int id)
        {
            Result result =  await _borrowedBookServices.UpdateStatusToRefunded(id);
            return result.ToHttpResponse();
        }

    }
}
