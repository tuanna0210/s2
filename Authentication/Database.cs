using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication
{
    public class Database
    {
        public string DataSoure { get; set; }

        public string Name { get; set; }

        public string UserId { get; set; }

        public string Password { get; set; }

        public Database Clone()
        {
            return (Database)this.MemberwiseClone();
        }
    }
}
