using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSO.Data.Entities
{
    public class UserClientId
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserIDClient { get; set; }
        public string Domain { get; set; }
        public string Token { get; set; }
    }
}
