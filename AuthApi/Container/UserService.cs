using AuthApi.Data;
using AuthApi.Models;
using AuthApi.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApi.Container
{
    public class UserService : IUser
    {
        private readonly MyDbContext _context;
        public UserService(MyDbContext context)
        {
            _context = context;
        }
        public async Task<User> GetUser(string name, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Name == name && x.Password == password);
        }
    }
}
