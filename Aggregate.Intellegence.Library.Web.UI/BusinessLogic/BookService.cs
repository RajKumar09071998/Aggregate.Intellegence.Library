using Aggregate.Intellegence.Library.Web.UI.Interface;
using Aggregate.Intellegence.Library.Web.UI.Model;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace Aggregate.Intellegence.Library.Web.UI.BusinessLogic
{
    public class BookService : IBookService
    {
        private readonly HttpClient httpClient;
        public BookService()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:5110/api/");
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.Timeout = TimeSpan.FromSeconds(60);
        }
        public async Task<bool> DeleteABook(long bookId)
        {
            var uri = Path.Combine("Book", bookId.ToString());
            var response = await httpClient.DeleteAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var responceContent = JsonConvert.DeserializeObject<bool>(content);

                return responceContent ? responceContent : false;
            }
            return false;
        }

        public async Task<Book> GetABook(long bookId)
        {
            var uri = Path.Combine("Book", bookId.ToString());
            var response = await httpClient.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var responseContent = JsonConvert.DeserializeObject<Book>(content);
                return response != null ? responseContent : new Book();
            }
            return new Book();
        }

        public async Task<List<Book>> GetAllABooks()
        {
            var booklist = new List<Book>();
            var response = await httpClient.GetAsync("Book/GetAllBooks");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var responseContent = JsonConvert.DeserializeObject<List<Book>>(content);
                return responseContent != null ? responseContent : booklist;
            }
            return booklist;

        }

        public async Task<bool> InsertOrUpdateABook(Book book)
        {
            var _book = JsonConvert.SerializeObject(book);
            var requestContent = new StringContent(_book, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("Book/InsertOrUpdateBook", requestContent);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var responseContent = JsonConvert.DeserializeObject<bool>(content);
                return responseContent != null ? responseContent : false;

            }
            return false;
        }
    }
}
