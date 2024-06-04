using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApi.Dtos
{
    public class UserUpdateStutus
    {
        [Required]
        public int userId { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
