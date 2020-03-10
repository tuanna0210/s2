using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSO.Data.Entities
{
    public class UserClient
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string UserIdClient { get; set; }
        public int ClientId { get; set; }
        public bool IsLogin { get; set; }
    }
}
