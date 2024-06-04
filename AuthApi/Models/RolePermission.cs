using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApi.Models
{
    public class RolePermission
    {
        public int Id { get; set; }
        public string UserRole { get; set; }
        [ForeignKey("Menu")]
        public int MenuCode { get; set; }
        public Menu Menu { get; set; }
        public bool HaveView { get; set; }
        public bool HaveRead { get; set; }
        public bool HaveEdit { get; set; }
        public bool HaveDelete { get; set; }
    }
}
