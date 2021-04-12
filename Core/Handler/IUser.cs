using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Entity;

namespace Core.Handler
{
    public interface IUser
    {
        Task<int> CheckUser(string username, string hash);
        Task<string> GetUserSalt(string username);
        Task<bool> CheckUsernameExists(string username);
        Task CreateUser(string username, string salt, string hash);
        Task<int> GetUserId(string username);
    }
}
