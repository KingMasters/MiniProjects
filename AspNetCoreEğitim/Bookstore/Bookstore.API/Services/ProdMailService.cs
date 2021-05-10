using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.API.Services
{
    public class ProdMailService : IMailService
    {
        private string _mailFrom = Startup.Configuration["mailSettings:mailFrom"];
        private string _mailTo = Startup.Configuration["mailSettings:mailTo"];

        public void Send(string subject, string message)
        {
            Debug.WriteLine($"From:{_mailFrom}, To:{_mailTo}");
            Debug.WriteLine($"Subject:{subject}");
            Debug.WriteLine($"Message:{message}");
        }
    }
}
