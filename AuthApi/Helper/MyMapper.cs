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
            CreateMap<RolePermissionAdd, RolePermission>()
                .ForMember(x=>x.MenuCode , y=>y.MapFrom(src=>src.menuId));
            CreateMap<MenuAddDto, Menu>();
            CreateMap<Menu, MenuListDto>();
            CreateMap<RolePermission, RoleMenuPermessionResDto>();
            CreateMap<User, UserListDto>();
            CreateMap<RolePermission, RoleMenuDto>()
                .ForMember(x=>x.MenuName , y=>y.MapFrom(src=>src.Menu.Name))
                .ForMember(x=>x.menuId , y=>y.MapFrom(src=>src.Menu.Id));

           

        }
    }
}
