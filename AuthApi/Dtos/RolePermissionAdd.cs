using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApi.Dtos
{
    public class RolePermissionAdd
    {
        [Required]
        public string UserRole { get; set; }
        [Required]
        public int MenuCode { get; set; }
        public bool HaveView { get; set; }
        public bool HaveRead { get; set; }
        public bool HaveEdit { get; set; }
        public bool HaveDelete { get; set; }
    }
}
