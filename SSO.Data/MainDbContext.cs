using FluentData;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSO.Data
{
    public class MainDbContext
    {
        public static IDbContext SSODB()
        {
            return new DbContext().ConnectionString(ConfigurationManager.AppSettings["SSO_V1"], new PostgreSqlProvider());
        }
    }
}
