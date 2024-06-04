using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApi.Dtos
{
    public class UpdatePasswordDto
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string OldPassword { get; set; }
       
    }
}
