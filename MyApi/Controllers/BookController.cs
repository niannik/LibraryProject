using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Models.BookModels;
namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookServices bookServices;
        public BookController(BookServices bookServices)
        {
            this.bookServices = bookServices;
        }

        [HttpGet]
        public async Task<List<ShowBookModel?>> Get()
        {
            var books = await bookServices.ShowBooks();
            return books;
        }
        [Route("Filter")]
        [HttpGet]
        public async Task<ActionResult<List<ShowBookModel?>>> Filter([FromQuery]ShowFilteredBook showFilteredBook)
        {
            var books = await bookServices.ShowBook(showFilteredBook);
            if (books == null)
            {
                return NotFound();
            }
            return Ok(books);
        }

        [Route("Info")]
        public async Task<InfoBookModel> GetInfo([FromQuery] int id)
        {
            var book = await bookServices.ShowBookInfo(id);
            return book;
        }
        [HttpPost]
        public async Task<ActionResult> Create(CreateBookModel book)
        {
            await bookServices.AddAsync(book);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Update(UpdateBookModel book)
        {
            await bookServices.UpdateAsync(book);
            return Ok();
        }
         
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            await bookServices.DeleteAsync(id);
            return Ok();
        }
    }
}
