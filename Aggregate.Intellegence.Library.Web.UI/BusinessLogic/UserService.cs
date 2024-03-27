using Aggregate.Intellegence.Library.Web.UI.Interface;
using Aggregate.Intellegence.Library.Web.UI.Model;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace Aggregate.Intellegence.Library.Web.UI.BusinessLogic
{
    public class UserService : IUserService
    {
        private readonly HttpClient httpClient;
        public UserService()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:5110/api/");
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.Timeout = TimeSpan.FromSeconds(60);
        }
        public async Task<bool> DeleteUser(long userId)
        {
            var uri = Path.Combine("User", userId.ToString());
            var response = await httpClient.DeleteAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var responceContent = JsonConvert.DeserializeObject<bool>(content);

                return responceContent ? responceContent : false;
            }
            return false;
        }

        public async Task<List<User>> FetchAllUsers()
        {
            var userlist = new List<User>();
            var response = await httpClient.GetAsync("User/FetchAllUsers");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var responseContent = JsonConvert.DeserializeObject<List<User>>(content);
                return response != null ? responseContent : userlist;
            }
            return userlist;
        }

        public async Task<User> FetchUser(long userId)
        {
            var uri = Path.Combine("User", userId.ToString());
            var response = await httpClient.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var responseContent = JsonConvert.DeserializeObject<User>(content);
                return response != null ? responseContent : new User();
            }
            return new User();
        }

        public async Task<bool> RegisterUser(RegisterUser registerUser)
        {
            var inputContent = JsonConvert.SerializeObject(registerUser);

            var requestContent = new StringContent(inputContent, Encoding.UTF8, "application/json");

            var responce = await httpClient.PostAsync("User/RegisterUser", requestContent);

            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();

                var responceContent = JsonConvert.DeserializeObject<bool>(content);

                return responceContent ? responceContent : false;
            }
            return false;
        }
    }
}
