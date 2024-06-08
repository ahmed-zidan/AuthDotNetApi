using AuthApi.Data;
using AuthApi.Models;
using AuthApi.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApi.Container
{
    public class MenuService : IMenuService
    {
        private readonly MyDbContext _contex;
        public MenuService(MyDbContext context)
        {
            _contex = context;
        }
        public async Task addMenu(Menu menu)
        {
            await _contex.Menus.AddAsync(menu);
        }

        public async Task<IEnumerable<Menu>> allMenus()
        {
           return await _contex.Menus.ToListAsync();
        }
        public async Task<IEnumerable<Menu>> allMenusByRole(string role)
        {
            return await _contex.RolePermissions.Include(x => x.Menu).Where(x => x.UserRole == role &&x.HaveView == true)
                .Select(x => new Menu { Id = x.MenuCode, Name = x.Menu.Name, Status = x.Menu.Status }).ToListAsync();
        }

       
    }
}
