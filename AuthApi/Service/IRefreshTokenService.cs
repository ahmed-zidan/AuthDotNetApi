using AuthApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApi.Service
{
    public interface IRefreshTokenService
    {
        Task<RefreshToken> getToken(string refreshToken);
        
    }
}
