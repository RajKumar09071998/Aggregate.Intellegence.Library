using Aggregate.Intellegence.Library.Web.UI.Interface;
using Aggregate.Intellegence.Library.Web.UI.Model;
using Newtonsoft.Json;
using System.Text;

namespace Aggregate.Intellegence.Library.Web.UI.BusinessLogic
{
    public class RoleService : IRoleService
    {
        private readonly HttpClient httpClient;
        public RoleService()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:5110/");
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.Timeout = TimeSpan.FromSeconds(60);
        }
        public async Task<bool> DeleteRole(long roleId)
        {
            var uri = Path.Combine("Role", roleId.ToString());
            var response = await httpClient.DeleteAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var responceContent = JsonConvert.DeserializeObject<bool>(content);

                return responceContent ? responceContent : false;
            }
            return false;
        }

        public async Task<List<Role>> FetchAllRoles()
        {
            var rolelist = new List<Role>();
            var response = await httpClient.GetAsync("Role");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var responseContent = JsonConvert.DeserializeObject<List<Role>>(content);
                return response != null ? responseContent : rolelist;
            }
            return rolelist;
        }

        public async Task<Role> GetRole(long roleId)
        {
            var uri = Path.Combine("Role", roleId.ToString());
            var response = await httpClient.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var responseContent = JsonConvert.DeserializeObject<Role>(content);
                return response != null ? responseContent : new Role();
            }
            return new Role();
        }

        public async Task<bool> InsertOrUpdateRole(Role role)
        {
            var _book = JsonConvert.SerializeObject(role);
            var requestContent = new StringContent(_book, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("Role/InsertOrUpdateRole", requestContent);
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
