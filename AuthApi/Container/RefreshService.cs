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
    public class RefreshService : IRefreshTokenService
    {
        private readonly MyDbContext _context;
        public RefreshService(MyDbContext context)
        {
            _context = context;
        }
        public async Task<RefreshToken> getToken(string refreshToken)
        {
            return await _context.RefreshTokens.FirstOrDefaultAsync(x => x.RefreshTokenId == refreshToken);
        }

        
    }
}
