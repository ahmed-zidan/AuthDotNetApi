﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApi.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CreditLimit { get; set; }
        public bool IsActive { get; set; }
        public string TextCode { get; set; }
    }
}
