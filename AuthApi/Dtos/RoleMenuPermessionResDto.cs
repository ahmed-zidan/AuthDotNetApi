using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApi.Dtos
{
    public class RoleMenuPermessionResDto
    {
        public bool HaveView { get; set; }
        public bool HaveAdd { get; set; }
        public bool HaveEdit { get; set; }
        public bool HaveDelete { get; set; }
    }
}
