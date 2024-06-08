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
    public class RolePermisionService : IRolePermissionService
    {
        private readonly MyDbContext _context;
        public RolePermisionService(MyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RolePermission>> allRoles(string role)
        {
            var menus = await _context.Menus.ToListAsync();
            var roles = new List<RolePermission>(); 
            foreach (var i in menus)
            {
                var myRole = await getMenuByRole(role, i.Name);
                if(myRole == null)
                {
                    roles.Add(new RolePermission()
                    {
                        HaveAdd = false,
                        HaveDelete = false,
                        HaveEdit = false,
                        HaveView = false,
                        Menu = new Menu()
                        {
                            Name = i.Name,
                            Id = i.Id
                        },
                        UserRole = role

                    });
                }
                else
                {
                    roles.Add(new RolePermission()
                    {
                        HaveAdd = myRole.HaveAdd,
                        HaveDelete = myRole.HaveDelete,
                        HaveEdit = myRole.HaveEdit,
                        HaveView = myRole.HaveView,
                        Menu = new Menu()
                        {
                            Name = i.Name,
                            Id = i.Id
                        },
                        UserRole = role
                    });
                }
            }
            return roles;
        }

        public async Task<ApiResponse> assignRoles(List<RolePermission> permissions)
        {
            ApiResponse api = new ApiResponse();
            foreach(var item in permissions)
            {
                var data = _context.RolePermissions.FirstOrDefault(x=>x.UserRole == item.UserRole && x.MenuCode == item.MenuCode);
                if(data != null)
                {
                    data.HaveDelete = item.HaveDelete;
                    data.HaveEdit = item.HaveEdit;
                    data.HaveView = item.HaveView;
                    data.HaveAdd = item.HaveAdd;
                    _context.RolePermissions.Update(data);
                }
                else if(_context.Menus.Any(x => x.Id == item.MenuCode))
                {
                    await _context.RolePermissions.AddAsync(item);
                }
                else
                {
                    api.ResponseCode = 404;
                    api.ErrorMessage = $"menu {item.MenuCode} not found";
                    return api;
                }
            }
            api.ResponseCode = 200;
            api.ErrorMessage = $"Passed";
            return api;
        }

        public async Task<IEnumerable<string>> getAlRoles()
        {
            return await _context.RolePermissions.Select(x =>x.UserRole).Distinct().ToListAsync();
        }

        public async Task<RolePermission> getMenuByRole(string role, string menuName)
        {
            return await _context.RolePermissions.Where(x => x.UserRole == role && x.Menu.Name == menuName)
               .FirstOrDefaultAsync();
        }
    }
}
