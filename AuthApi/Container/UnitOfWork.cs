﻿using AuthApi.Data;
using AuthApi.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApi.Container
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyDbContext _context;
        public UnitOfWork(MyDbContext context)
        {
            _context = context;
        }
        public ICustomerService _customer => new CustomerService(_context);

        public IUser _user => new UserService(_context,new OtpService(),new EmailSender());

        public IRefreshTokenService _refreshToken => new RefreshService(_context);

        public IProduct _prodcut => new Product(_context);

        public IRolePermissionService _rolePermission => new RolePermisionService(_context);

        public MenuService _menueService => new MenuService(_context);

        public async Task<bool> saveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
