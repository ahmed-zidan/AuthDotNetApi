using AuthApi.Data;
using AuthApi.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApi.Container
{
    public class Product : IProduct
    {
        private readonly MyDbContext _context;
        public Product(MyDbContext context)
        {
            _context = context;
        }
        public async Task AddProduct(Models.Product product)
        {
            await _context.Products.AddAsync(product);
        }

        public async Task<Models.Product> GetProduct(int id)
        {
            return await _context.Products.Where(x => x.Id == id).Include(x => x.productImgs).FirstOrDefaultAsync();
        }
    }
}
