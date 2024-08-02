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
using Microsoft.AspNetCore.Authorization;
using Services.ResponseModels;
namespace MyApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = Permission.Admin)]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookServices _bookServices;
        public BookController(BookServices bookServices)
        {
            this._bookServices = bookServices;
        }

        [HttpGet]
        public async Task<ActionResult<GetListOfBook>> Get()
        {
            var books = await _bookServices.GetBooks();
            return books.ToHttpResponse()!;
        }
        [HttpGet("Filter")]
        public async Task<ActionResult<GetListOfBook>> Filter([FromQuery]GetFilteredBook showFilteredBook)
        {
            var books = await _bookServices.FilterBook(showFilteredBook);
            return books.ToHttpResponse()!;
        }

        [Route("Info")]
        [HttpGet]
        public async Task<ActionResult<InfoBookModel>> GetInfo([FromQuery] int id)
        {
            var book = await _bookServices.GetBookInfo(id);
            return book.ToHttpResponse();
        }
        [HttpPost]
        public async Task<ActionResult> Create(CreateBookModel book)
        {
            Result result = await _bookServices.AddAsync(book);
            return result.ToHttpResponse();
            
        }

        [HttpPut]
        public async Task<ActionResult> Update(UpdateBookModel book)
        {
            Result result = await _bookServices.UpdateAsync(book);
            return result.ToHttpResponse();
        }
         
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            Result result = await _bookServices.DeleteAsync(id);
            return result.ToHttpResponse();
        }
    }
}
