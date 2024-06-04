using AuthApi.Container;
using AuthApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApi.Service
{
    public interface IUnitOfWork
    {
       ICustomerService _customer { get; }
       IProduct _prodcut { get; }
       IRefreshTokenService _refreshToken { get; }
       IRolePermissionService _rolePermission { get; }
       MenuService _menueService { get; }
       IUser _user { get; }
       Task<bool> saveChangesAsync();
       
    }
}
