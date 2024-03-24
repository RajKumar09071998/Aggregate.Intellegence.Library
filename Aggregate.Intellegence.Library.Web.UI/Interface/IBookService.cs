using Aggregate.Intellegence.Library.Web.UI.Model;

namespace Aggregate.Intellegence.Library.Web.UI.Interface
{
    public interface IBookService
    {
        Task<bool> InsertOrUpdateABook(Book book);
        Task<bool> DeleteABook(long bookId);
        Task<Book> GetABook(long bookId);
        Task<List<Book>> GetAllABooks();
    }
}
