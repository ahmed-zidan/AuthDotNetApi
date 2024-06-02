using AuthApi.Dtos;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApi.Service
{
    public interface IJwtService
    {
        SecurityToken generateToken(Models.User user);
        Task<string> generateRefreshTokenAsync(int userId);
        Task<string> generateNewTokenAsync(UserLoginResDto model);
    }
}
