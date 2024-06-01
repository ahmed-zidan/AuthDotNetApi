using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApi.Dtos
{
    public class UserLoginResDto
    {
       
        public string Name { get; set; }
        public DateTime Expired { get; set; }
        public string Token { get; set; }
    }
}
