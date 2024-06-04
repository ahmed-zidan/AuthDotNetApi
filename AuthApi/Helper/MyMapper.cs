using AuthApi.Dtos;
using AuthApi.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApi.Helper
{
    public class MyMapper:Profile
    {
        public MyMapper()
        {
            CreateMap<Customer, CustomerListDto>();
            CreateMap<CustomerAddDto, Customer>();
            CreateMap<UserLoginDto, User>();
            CreateMap<ProductAddDto, Product>();
            CreateMap<UserRegisterDto, User>();
            CreateMap<RolePermissionAdd, RolePermission>();
            CreateMap<MenuAddDto, Menu>();
            CreateMap<Menu, MenuListDto>();
        }
    }
}
