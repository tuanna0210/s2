using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication
{
    public interface IConfig
    {
        Database Database { get; }

        string Culture { get; }

        Type UserService { get; }
    }
}
