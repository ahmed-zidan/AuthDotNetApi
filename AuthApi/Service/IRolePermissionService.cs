using AuthApi.Helper;
using AuthApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApi.Service
{
    public interface IRolePermissionService
    {
        Task<ApiResponse> assignRoles(List<RolePermission> permissions);
        Task<IEnumerable<RolePermission>> allRoles(string role);
    }
}
