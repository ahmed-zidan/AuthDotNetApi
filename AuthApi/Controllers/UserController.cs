using AuthApi.Dtos;
using AuthApi.Helper;
using AuthApi.Models;
using AuthApi.Service;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApi.Controllers
{
   
    public class UserController : BaseController
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public UserController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> register(UserRegisterDto model)
        {
            ApiResponse api = new ApiResponse();
            if (!ModelState.IsValid)
            {
                api.ResponseCode = 400;
                api.ErrorMessage = "model is not valid";
                return BadRequest(api);
            }
            var user = _mapper.Map<User>(model);
            await _uow._user.addUserAsync(user);
            await _uow.saveChangesAsync();
            return Ok(user.Id);
        }

      
        [HttpPost("confirmUser")]
        public async Task<IActionResult> confirmUser(ConfirmUserDto model)
        {
            ApiResponse api = new ApiResponse();
            if (!ModelState.IsValid)
            {
                api.ResponseCode = 400;
                api.ErrorMessage = "model is not valid";
                return BadRequest(api);
            }
           var res =  await _uow._user.confirmUserAsync(model.UserId , model.OtpText);
            return Ok(res);
        }
        
        [HttpPost("updatePwd")]
        public async Task<IActionResult> updatePwd(UpdatePasswordDto model)
        {
            var res = await _uow._user.updatePassword(model.Id , model.Password , model.OldPassword);
            return Ok(res);
        }

        [HttpGet("forgetPassword/{userId}")]
        public async Task<IActionResult> forgetPassword(int userId)
        {
            var res = await _uow._user.forgetPassword(userId);
            return Ok(res);
        }

        [HttpPost("addNewPass")]
        public async Task<IActionResult> addNewPass(UserAddNewPassDto model)
        {
            var res = await _uow._user.addNewPassword(model.Id,model.Password,model.Otp);
            return Ok(res);
        }
        [HttpPost("updateStatus")]
        public async Task<IActionResult> updateStatus(UserUpdateStutus model)
        {
            
            var res = await _uow._user.updateStutus(model.userId, model.IsActive);
            return Ok(res);
        }
        [HttpPost("updateRole")]
        public async Task<IActionResult> updateRole(UpdateUserRoleDto model)
        {
           
            var res = await _uow._user.updateRole(model.Id, model.Role);
            return Ok(res);
        }
    }
}
