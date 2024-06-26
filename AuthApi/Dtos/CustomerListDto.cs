﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApi.Dtos
{
    public class CustomerListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CreditLimit { get; set; }
        public bool IsActive { get; set; }
        public string TextCode { get; set; }
    }
}
