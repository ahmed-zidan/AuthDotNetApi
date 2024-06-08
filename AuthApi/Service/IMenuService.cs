using AuthApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApi.Service
{
    public interface IMenuService
    {
        Task addMenu(Menu menu);
        Task<IEnumerable<Menu>> allMenus();
        Task<IEnumerable<Menu>> allMenusByRole(string role);
       

    }
}
