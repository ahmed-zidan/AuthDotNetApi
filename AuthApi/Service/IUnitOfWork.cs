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
       IUser _user { get; }
       Task<bool> saveChangesAsync();
       
    }
}
