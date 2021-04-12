using Core.Handler;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Infrastructure.Repository
{
    public class User : IUser
    {
        private readonly reddit_cloneContext _context;
        public User(reddit_cloneContext context)
        {
            _context = context;
        }

        public Task<int> CheckUser(string username, string hash)
        {
            var q = from user in _context.User
                    where (user.Name == username && user.Hash == hash)
                    select user.Id;

            return Task.FromResult(q.FirstOrDefault());
        }

        public async Task CreateUser(string username, string salt, string hash)
        {
            _context.User.Add(new Models.User
            {
                Name = username,
                Hash = hash,
                Salt = salt,
                CreatedAt = DateTime.UtcNow
            });
            await _context.SaveChangesAsync();

            return;
        }

        public async Task<bool> CheckUsernameExists(string username)
        {
            var q = from user in _context.User
                    where (user.Name == username)
                    select user.Id;
            return q.FirstOrDefault() != 0;
        }

        public Core.Entity.User GetUser()
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUserSalt(string username)
        {
            var q = from user in _context.User
                    where (user.Name == username)
                    select user.Salt;

            return Task.FromResult(q.FirstOrDefault());
        }

        public Task<int> GetUserId(string username)
        {
            var q = from user in _context.User
                    where (user.Name == username)
                    select user.Id;

            return Task.FromResult(q.FirstOrDefault());
        }
    }
}
