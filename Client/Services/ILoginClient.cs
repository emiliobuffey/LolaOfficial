using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LolaOfficial.Shared;

namespace LolaOfficial.Client.Services
{
    public interface ILoginClient
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public Task<User> LoginUser();
    }
}