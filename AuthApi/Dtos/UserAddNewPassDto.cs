using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApi.Dtos
{
    public class UserAddNewPassDto
    {
        public int Id { get; set; }
        public string Otp { get; set; }
        public string Password { get; set; }
    }
}
