using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication
{
    public interface IUserLogin
    {
        string UserName { get; set; }

        string Password { get; set; }

        bool Remember { get; set; }

        string Languague { get; set; }

        string Provider { get; set; }
    }
}
