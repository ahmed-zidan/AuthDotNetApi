using AuthApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApi.Service
{
    public interface IProduct
    {
        Task<Product> GetProduct(int id);
        Task AddProduct(Product product);
    }
}
