using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using WebApplication1.Services.BookService;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService bookService;

        public BookController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetBookDto>>> GetAllBooks()
        {
            var result = await bookService.GetAllBooks();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetBookDto>> GetBook(int id)
        {
            var result = await bookService.GetBook(id);

            if (result is null)
                return NotFound("Book doesn't exist");

            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<GetBookDto>>> SearchBooks([FromQuery] QueryParams query)
        {
            var result = await bookService.GetAllBooks();
            
            if (query.CategoryId is not null) {
               
            }
            if (query.Brand is not null) 
            {
                
            }
            

            if (result is null)
                return NotFound("Book doesn't exist");

            return Ok(result);
        }

        /*[HttpPost]
        public async Task<ActionResult<List<Book>>> AddBook(Book book)
        {
            var result = await bookService.AddBook(book);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<List<Book>>> UpdateBook(Book book)
        {
            var result = await bookService.UpdateBook(book);
            if (result is null)
                return NotFound("Book doesn't exist");

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Book>>> DeleteBook(int id)
        {
            var result = await bookService.DeleteBook(id);
            if (result is null)
                return NotFound("Book doesn't exist");

            return Ok(result);
        }*/
    }
}
