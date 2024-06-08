using AuthApi.Data;
using AuthApi.Helper;
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
        private readonly IOtpService _otpService;
        private readonly IEmailSender _emailSender;
        public UserService(MyDbContext context, IOtpService otpService, IEmailSender sender)
        {
            _context = context;
            _otpService = otpService;
            _emailSender = sender;
        }

        public async Task addUserAsync(User user)
        {
            OtpManager otp = new OtpManager()
            {
                CreateDate = DateTime.Now,
                Expired = DateTime.Now.AddMinutes(30),
                OtpText = _otpService.generateOtp(),
                UserId = user.Id,
                OtpType = "register",
            };
            user.Role = "user";
            user.OtpManager = otp;
            _emailSender.sendEmail(user.Name, otp.OtpText);
            await _context.Users.AddAsync(user);
        }

        public async Task<ApiResponse> confirmUserAsync(int userId, string otpText)
        {
            var user = await _context.Users.Include(x=>x.OtpManager).Where(x=>x.Id == userId).FirstOrDefaultAsync();
            ApiResponse api = new ApiResponse();
            if(user == null)
            {
                api.ResponseCode = 404;
                api.ErrorMessage = "User not Correct";
                return api;
            }
            if(user.OtpManager != null)
            {
                if(user.OtpManager.OtpText == otpText && user.OtpManager.Expired <= DateTime.Now)
                {
                    user.IsActive = true;
                    await _context.SaveChangesAsync();
                    api.ResponseCode = 200;
                    api.ErrorMessage = "Passes";
                    return api;
                }
                else
                {
                    api.ResponseCode = 400;
                    api.ErrorMessage = "Expired Otp";
                    return api;
                }
            }
            else
            {
                api.ResponseCode = 400;
                api.ErrorMessage = "ask for new Otp";
                return api;
            }


        }

        public async Task<ApiResponse> forgetPassword(string email)
        {
            ApiResponse api = new ApiResponse();
            var user = await _context.Users.Include(x=>x.OtpManager).Where(x => x.Email == email && x.IsActive == true).FirstOrDefaultAsync();
            if(user != null)
            {
                string otp = _otpService.generateOtp();
                user.OtpManager.OtpText = otp;
                user.OtpManager.OtpType = "forgetPassword";
                user.OtpManager.Expired = DateTime.Now.AddMinutes(30);
                user.OtpManager.CreateDate = DateTime.Now;
                _emailSender.sendEmail(user.Name, otp);
                await _context.SaveChangesAsync();
                api.Result = user.Id.ToString();
                api.ResponseCode = 200;
            }
            else
            {
                api.Result = "Not Found";
                api.ResponseCode = 404;
            }
            return api;
        }
        public async Task<ApiResponse> addNewPassword(int userId, string password, string otp)
        {
            ApiResponse api = new ApiResponse();
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId && x.OtpManager.OtpText == otp
              && x.IsActive == true
            && x.OtpManager.Expired >= DateTime.Now);
            if(user != null)
            {
                user.Password = password;
                await _context.SaveChangesAsync();
                api.Result = "Passed";
                api.ResponseCode = 200;
            }
            else
            {
                api.ErrorMessage = "Not Found";
                api.ResponseCode = 404;
            }
            return api;
        }

        public async Task<User> GetUser(string name, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Name == name && x.Password == password);
        }
      
        public async Task<ApiResponse> updatePassword(int userId, string password, string oldPassword)
        {
            ApiResponse api = new ApiResponse();
            var user = await _context.Users.Where(x=>x.Id == userId && x.Password == oldPassword&&x.IsActive == true).FirstOrDefaultAsync();
            if(user == null)
            {
                api.ResponseCode = 404;
                api.ErrorMessage = "user not found";
                return api;
            }
            user.Password = password;
            await _context.SaveChangesAsync();
            api.ResponseCode = 200;
            api.Result = "passed";
            return api;
        }

        public async Task<ApiResponse> updateStutus(int userId, bool status)
        {
            ApiResponse api = new ApiResponse();
            var user =await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if(user == null)
            {
                api.ResponseCode = 404;
                api.ErrorMessage = "user not found";
                return api;
            }
            user.IsActive = status;
            await _context.SaveChangesAsync();
            api.ResponseCode = 200;
            api.Result = "passed";
            return api;
        }

        public async Task<ApiResponse> updateRole(int userId, string role)
        {
            ApiResponse api = new ApiResponse();
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                api.ResponseCode = 404;
                api.ErrorMessage = "user not found";
                return api;
            }
            user.Role = role;
            await _context.SaveChangesAsync();
            api.ResponseCode = 200;
            api.Result = "passed";
            return api;
        }

        public async Task<IEnumerable<User>> getUsers()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task<ApiResponse> deleteUser(int id)
        {
            ApiResponse api = new ApiResponse();
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if(user == null)
            {
                api.ResponseCode = 404;
                api.ErrorMessage = "not found";
                api.Result = "Fail";
                return api;
            }

            _context.Users.Remove(user);
            api.ResponseCode = 200;
            api.Result = "Passed";
            return api;
        }

        public async Task<User> GetUser(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
