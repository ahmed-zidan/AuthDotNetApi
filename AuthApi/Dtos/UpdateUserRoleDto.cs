using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApi.Dtos
{
    public class UpdateUserRoleDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
