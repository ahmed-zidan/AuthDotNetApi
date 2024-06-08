using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApi.Dtos
{
    public class UserLoginResDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Expired { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string UserRole { get; set; }
    }
}
