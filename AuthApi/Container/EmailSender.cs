using AuthApi.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApi.Container
{
    public class EmailSender : IEmailSender
    {
        public bool sendEmail(string name, string message)
        {
            
            return true;
        }
    }
}
