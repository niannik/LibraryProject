using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using Services;

using Microsoft.AspNetCore.Mvc;
using Services.Models.BookModels;
using MyApi.ExceptionExtensions;
using Common;
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
        public async Task<ActionResult<List<ShowBookModel?>>> Get()
        {
            var books = await bookServices.ShowBooks();
            return books.ToHttpResponse()!;
        }
        [HttpGet("Filter")]
        public async Task<ActionResult<List<ShowBookModel?>>> Filter([FromQuery]ShowFilteredBook showFilteredBook)
        {
            var books = await bookServices.FilterBook(showFilteredBook);
            return books.ToHttpResponse()!;
        }

        [Route("Info")]
        [HttpGet]
        public async Task<ActionResult<InfoBookModel>> GetInfo([FromQuery] int id)
        {
            var book = await bookServices.ShowBookInfo(id);
            return book.ToHttpResponse();
        }
        [HttpPost]
        public async Task<ActionResult> Create(CreateBookModel book)
        {
            Result result = await bookServices.AddAsync(book);
            return result.ToHttpResponse();
            
        }

        [HttpPut]
        public async Task<ActionResult> Update(UpdateBookModel book)
        {
            Result result = await bookServices.UpdateAsync(book);
            return result.ToHttpResponse();
        }
         
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            Result result = await bookServices.DeleteAsync(id);
            return result.ToHttpResponse();
        }
    }
}
