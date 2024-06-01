using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApi.Helper
{
    public class ApiResponse
    {
        public int ResponseCode { get; set; }
        public string Result { get; set; }
        public string ErrorMessage { get; set; }
    }
}
