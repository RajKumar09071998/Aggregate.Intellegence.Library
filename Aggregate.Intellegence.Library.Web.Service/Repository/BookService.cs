using Aggregate.Intellegence.Library.Web.Service.AppDBContext;
using Aggregate.Intellegence.Library.Web.Service.Interfaces;
using Aggregate.Intellegence.Library.Web.Service.Models;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Aggregate.Intellegence.Library.Web.Service.Services
{

    public class BookService : IBookService
    {
        private readonly ApplicationDBContext dBContext;
        public BookService(ApplicationDBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        public async Task<bool> DeleteABook(long bookId)
        {
            var book = await dBContext.books.FindAsync(bookId);
            if (book != null)
            {
                dBContext.books.Remove(book);
            }
            var response = await dBContext.SaveChangesAsync();
            return response == 1 ? true : false;
        }

        public async Task<Book> GetABook(long bookId)
        {
            var book = await dBContext.books.FindAsync(bookId);
            if (book != null)
            {
                return book;
            }
            return null;
        }

        public async Task<List<Book>> GetAllABooks()
        {
            return await dBContext.books.Where(x => x.IsActive).ToListAsync();
        }

        public async Task<bool> InsertOrUpdateABook(Book book)
        {
            if (book != null)
            {
                if (book.BookId > 0)
                {
                    var dbBook = await dBContext.books.FindAsync(book.BookId);
                    if (dbBook != null)
                    {
                        dbBook.Title = book.Title;
                        dbBook.Author = book.Author;
                        dbBook.Publisher = book.Publisher;
                        dbBook.PublishYear = book.PublishYear;
                        dbBook.Genre = book.Genre;
                        dbBook.Language = book.Language;
                        dbBook.PageCount = book.PageCount;
                        dbBook.Description = book.Description;
                        dbBook.ModifiedOn = DateTime.Now;
                        dbBook.ModifiedBy = book.ModifiedBy;
                        dbBook.IsActive = book.IsActive;
                    }
                }
                else if (await dBContext.books.AnyAsync(x => x.Title.ToLower().Trim() == book.Title.ToLower().Trim()))
                {
                    var dbBook = await dBContext.books.FirstOrDefaultAsync(x => x.Title.ToLower().Trim() == book.Title.ToLower().Trim());
                    if (dbBook != null)
                    {
                        dbBook.Title = book.Title;
                        dbBook.Author = book.Author;
                        dbBook.Publisher = book.Publisher;
                        dbBook.PublishYear = book.PublishYear;
                        dbBook.Genre = book.Genre;
                        dbBook.Language = book.Language;
                        dbBook.PageCount = book.PageCount;
                        dbBook.Description = book.Description;
                        dbBook.ModifiedOn = DateTime.Now;
                        dbBook.ModifiedBy = book.ModifiedBy;
                        dbBook.IsActive = book.IsActive;
                    }
                }
                else
                {
                    await dBContext.books.AddAsync(book);

                }
                var response = await dBContext.SaveChangesAsync();
                return response == 1 ? true : false;
            }
            return false;

        }


    }
}
