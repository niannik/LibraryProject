using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Models;

namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowedBookController : ControllerBase
    {
        private readonly BorrowedBookServices borrowedBookServices;
        public BorrowedBookController(BorrowedBookServices borrowedBookServices)
        {
            this.borrowedBookServices = borrowedBookServices;
        }

        [Route("Current")]

        [HttpGet]
        public async Task<ActionResult<List<BorrowedBookModel>>> GetCurrent()
        {
            var books =await borrowedBookServices.CurrentBorrowedBooks();
            return Ok(books);
        }
        [Route("Previous")]
        [HttpGet]
        public async Task<ActionResult<List<BorrowedBookModel>>> GetPrevious()
        {
            var books = await borrowedBookServices.PreviousBorrowedBook();
            return Ok(books);
        }
        [Route("Add")]
        [HttpPost]
        public async Task<ActionResult> Add(AddUpdateBrrowedBookModel model)
        {
            await borrowedBookServices.AddBorrowedBook(model);
            return Ok();
        }

        [Route("Refund")]
        [HttpPut]
        public async Task<ActionResult> Refund(int id)
        {
            await borrowedBookServices.UpdateStatusToRefunded(id);
            return Ok();
        }

    }
}
