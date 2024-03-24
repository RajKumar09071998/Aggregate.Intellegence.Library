using Aggregate.Intellegence.Library.Web.UI.Interface;
using Aggregate.Intellegence.Library.Web.UI.Model;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Aggregate.Intellegence.Library.Web.UI.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService bookServices;
        private readonly INotyfService notyfService;
        public BookController(IBookService bookServices,
                              INotyfService notyfService)
        {
            this.bookServices = bookServices;
            this.notyfService = notyfService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> FetchAllBooks()
        
        {
            try
            {
                var response = await bookServices.GetAllABooks();
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public async Task<IActionResult> InsertOrUpdateBook(Book book)
        {
            try
            {
                if(book != null)
                {
                    var response = await bookServices.InsertOrUpdateABook(book);
                    if(response)
                    {
                        if (book.BookId > 0)
                        {
                            notyfService.Success("Book Updated Successfully");
                        }
                        notyfService.Success("Book Insertred Successfully");
                        return Json(true);
                    }
                    notyfService.Warning("Something went wrong");
                    return Json(false);
                }
                notyfService.Warning("Something went wrong");
                return Json(false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public async Task<IActionResult>GetABook(long bookId)
        {
            try
            {
                var response = await bookServices.GetABook(bookId);
                return response != null ? Json(true) : Json(false);
            }
            catch(Exception ex)
            {
                throw ex;
            }
           
        }
        [HttpDelete]
        public async Task<IActionResult>DeleteABook(long bookId)
        {
            try
            {
                var response = await bookServices.DeleteABook(bookId);
                return response != null ? Json(true) : Json(false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
