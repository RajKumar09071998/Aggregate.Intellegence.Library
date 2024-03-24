using Aggregate.Intellegence.Library.Web.Service.Models;

namespace Aggregate.Intellegence.Library.Web.Service.Interfaces
{
    public interface IBookService
    {
        Task<bool> InsertOrUpdateABook(Book book);
        Task<bool> DeleteABook(long bookId);
        Task<Book> GetABook(long bookId);
        Task<List<Book>> GetAllABooks();
       
    }
}
