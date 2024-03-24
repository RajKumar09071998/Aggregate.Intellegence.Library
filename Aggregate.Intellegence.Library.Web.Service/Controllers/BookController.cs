using Aggregate.Intellegence.Library.Web.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Aggregate.Intellegence.Library.Web.Service.Models;
namespace Aggregate.Intellegence.Library.Web.Service.Controllers
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
        [HttpPost]
        [Route("InsertOrUpdateBook")]
        public async Task<IActionResult> InsertOrUpdateBook(Book book)
        {

            try
            {
                var response = await bookService.InsertOrUpdateABook(book);
                return Ok(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        [Route("{bookId:long}")]
        public async Task<IActionResult>GetBook(long bookId)
        {
            try
            {
                var response = await bookService.GetABook(bookId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            
        }
        [HttpDelete]
        [Route("{bookId:long}")]
        public async Task<IActionResult>DeleteBook(long bookId)
        {
            try
            {
                var response = await bookService.DeleteABook(bookId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        [Route("GetAllBooks")]
        public async Task<IActionResult>GetAllBooks()
        {
            try
            {
                var response= await bookService.GetAllABooks();
                return Ok(response);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

    }
}
