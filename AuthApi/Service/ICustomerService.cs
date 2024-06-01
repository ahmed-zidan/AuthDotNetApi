using AuthApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApi.Service
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> getAll();
        Task<Customer> getById(int id);
        void Remove(Customer customer);
        Task CreateRecord(Customer model);
        void Update(Customer model);
        
    }
}
