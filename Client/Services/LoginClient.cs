using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using LolaOfficial.Client;
using LolaOfficial.Shared;

namespace LolaOfficial.Client.Services
{
    public class LoginClient : ILoginClient
    {
        public string Email { get; set; }
        public string Password { get; set; }

        private HttpClient _httpClient;
        public LoginClient()
        {

        }

        public LoginClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

//calling the web api and returning user
        public async Task<User> LoginUser()
        {
            var response = await _httpClient.PostAsJsonAsync("user/loginuser", this);
            return  await response.Content.ReadFromJsonAsync<User>();
        }

        public static implicit operator LoginClient(User user)
        {
            return new LoginClient
            {
                Email = user.Email,
                Password = user.Password
            };
        }

        public static implicit operator User(LoginClient loginClient)
        {
            return new User
            {
                Email = loginClient.Email,
                Password = loginClient.Password
            };
        }
    }
}