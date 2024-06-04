using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApi.Service
{
    public interface IEmailSender
    {
        bool sendEmail(string name, string message);
    }
}
