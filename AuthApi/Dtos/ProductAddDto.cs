using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApi.Dtos
{
    public class ProductAddDto
    {
        [Required]
        public string Name { get; set; }
        [Required]

        public decimal Price { get; set; }
    }
}
