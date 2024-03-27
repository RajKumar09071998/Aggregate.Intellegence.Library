using Aggregate.Intellegence.Library.Web.UI.Interface;
using Aggregate.Intellegence.Library.Web.UI.Model;
using Newtonsoft.Json;
using System.Text;

namespace Aggregate.Intellegence.Library.Web.UI.BusinessLogic
{
    public class AuthneticatioinService : IAuthneticatioinService
    {
        private readonly HttpClient httpClient;
        public AuthneticatioinService()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:5110/api/");
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.Timeout = TimeSpan.FromSeconds(60);
        }
        public async Task<bool> RegisterUser(RegisterUser registerUser)
        {
            var inputContent = JsonConvert.SerializeObject(registerUser);

            var requestContent = new StringContent(inputContent, Encoding.UTF8, "application/json");

            var responce = await httpClient.PostAsync("Account/RegisterUser", requestContent);

            if (responce.IsSuccessStatusCode)
            {
                var content = await responce.Content.ReadAsStringAsync();

                var responceContent = JsonConvert.DeserializeObject<bool>(content);

                return responceContent ? responceContent : false;
            }
            return false;
        }
        public async Task<AuthenticationResponce> AuthenticateUser(UserLoginVM loginVM)
        {
            var usermodel = JsonConvert.SerializeObject(loginVM);
            var requestContent = new StringContent(usermodel, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("Account/AuthenticateUser", requestContent);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var responceContent = JsonConvert.DeserializeObject<AuthenticationResponce>(content);
                return responceContent != null ? responceContent : null;
            }
            return null;
        }

        public async Task<MVCApplicationUser> GenarateUserClaims(AuthenticationResponce userresponce)
        {
            var usermodel = JsonConvert.SerializeObject(userresponce);

            var requestContent = new StringContent(usermodel, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("Account/GenarateUserClaims", requestContent);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var responceContent = JsonConvert.DeserializeObject<MVCApplicationUser>(content);
                return responceContent != null ? responceContent : null;
            }
            return null;
        }
        public async Task<List<User>> FetchAllUsers()
        {
            var userlist = new List<User>();
            var response = await httpClient.GetAsync("Account/FetchAllUsers");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var responseContent = JsonConvert.DeserializeObject<List<User>>(content);
                return response != null ? responseContent : userlist;
            }
            return userlist;
        }
    }
}
