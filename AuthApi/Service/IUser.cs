﻿using AuthApi.Helper;
using AuthApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApi.Service
{
    public interface IUser
    {
        Task<User> GetUser(string name , string password);
        Task<User> GetUser(int id);
        Task addUserAsync(User user);
        Task<ApiResponse> confirmUserAsync(int userId , string otpText);
        Task<ApiResponse> updatePassword(int userId , string password , string oldPassword);
        Task<ApiResponse> forgetPassword(string email);
        Task<ApiResponse> addNewPassword(int userId, string password, string otp);
        Task<ApiResponse> updateStutus(int userId,bool status);
        Task<ApiResponse> updateRole(int userId, string role);
        Task<IEnumerable<User>> getUsers();
        Task<ApiResponse> deleteUser(int id);


    }
}
