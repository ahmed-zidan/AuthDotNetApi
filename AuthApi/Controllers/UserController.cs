using AuthApi.Dtos;
using AuthApi.Helper;
using AuthApi.Models;
using AuthApi.Service;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public async Task<IActionResult> updatePwd(UpdatePasswordDto model)
        {
            var res = await _uow._user.updatePassword(model.Id , model.Password , model.OldPassword);
            if(res.ResponseCode == 404)
            {
                return NotFound(res);
            }
            return Ok(res);
        }

        [HttpGet("forgetPassword/{email}")]
        public async Task<IActionResult> forgetPassword(string email)
        {
            var res = await _uow._user.forgetPassword(email);
            if(res.ResponseCode == 200)
            {
                return Ok(res);
            }
            return BadRequest(res);
            
        }

        [HttpPost("addNewPass")]
        public async Task<IActionResult> addNewPass(UserAddNewPassDto model)
        {
            var res = await _uow._user.addNewPassword(model.Id,model.Password,model.Otp);
            if(res.ResponseCode == 404)
            {
                return NotFound(res);
            }
            return Ok(res);
        }
        [HttpDelete("deleteUser/{id}")]
        public async Task<IActionResult> deleteUser(int id)
        {
            var res = await _uow._user.deleteUser(id);
            if (res.ResponseCode == 404)
            {
                return NotFound(res);
            }
            else
            {
                await _uow.saveChangesAsync();
                return Ok(res);
            }
            
        }
        [HttpGet("getUsers")]
        public async Task<IActionResult> getUsers()
        {
            var res = await _uow._user.getUsers();
            var users = _mapper.Map<List<UserListDto>>(res);
            return Ok(users);
        }

        [HttpGet("getUser/{id}")]
        public async Task<IActionResult> getUser(int id)
        {
            var res = await _uow._user.GetUser(id);
            var user = _mapper.Map<UserListDto>(res);
            return Ok(user);
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
