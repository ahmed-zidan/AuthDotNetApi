using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApi.Models
{
    public class Menu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        //public ICollection<RolePermission> RolePermissions { get; set; }
    }
}
