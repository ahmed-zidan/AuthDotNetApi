using AuthApi.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApi.Container
{
    public class OtpService : IOtpService
    {
        public string generateOtp()
        {
            return new Random().Next(0, 100000).ToString("D6");
        }
       
    }
}
