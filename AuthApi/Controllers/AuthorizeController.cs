using AuthApi.Dtos;
using AuthApi.Helper;
using AuthApi.Service;
using AutoMapper;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using System.IdentityModel.Tokens.Jwt;

using System.Threading.Tasks;

namespace AuthApi.Controllers
{
   
    public class AuthorizeController : BaseController
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IJwtService _Jwt;
        public AuthorizeController(IUnitOfWork uof, IMapper mapper, IConfiguration configuration, IJwtService Jwt)
        {
            _uof = uof;
            _mapper = mapper;
            _configuration = configuration;
            _Jwt = Jwt;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDto model)
        {
            ApiResponse api = new ApiResponse();
            if (!ModelState.IsValid)
            {
                api.ResponseCode = 400;
                api.ErrorMessage = "Please enter the password or name";
                return BadRequest(api);
            }

            var user = await _uof._user.GetUser(model.Name, model.Password);
            if(user == null)
            {
                api.ResponseCode = 401;
                api.ErrorMessage = "the password or name is not correct";
                return Unauthorized(api);
            }

            var token = _Jwt.generateToken(user);
            UserLoginResDto res = new UserLoginResDto()
            {
                Name = user.Name,
                Expired = token.ValidTo,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = await _Jwt.generateRefreshTokenAsync(user.Id)
            };
            return Ok(res);


        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken(UserLoginResDto model)
        {
            ApiResponse api = new ApiResponse();
            if (!ModelState.IsValid)
            {
                api.ResponseCode = 400;
                api.ErrorMessage = "Please enter the password or name";
                return BadRequest(api);
            }

            var tokenRef = await _uof._refreshToken.getToken(model.RefreshToken);
            if (tokenRef == null)
            {
                api.ResponseCode = 401;
                api.ErrorMessage = "You are not authorized";
                return Unauthorized(api);
            }
            
            var token = await _Jwt.generateNewTokenAsync(model);
            if(token == null)
            {
                api.ResponseCode = 401;
                api.ErrorMessage = "You are not authorized";
                return Unauthorized(api);
            }
            
            return Ok(model);

        }


    }
}
