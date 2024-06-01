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
    public class CustomerService:ICustomerService
    {
        private readonly MyDbContext _context;
        public CustomerService(MyDbContext context)
        {
            _context = context;
        }

        public async Task CreateRecord(Customer model)
        {
            await _context.Customers.AddAsync(model);
        }

        public async Task<IEnumerable<Customer>> getAll()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> getById(int id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public void Remove(Customer customer)
        {

             _context.Customers.Remove(customer);
        }

        public void Update(Customer model)
        {
            _context.Customers.Update(model);
        }
    }
}
