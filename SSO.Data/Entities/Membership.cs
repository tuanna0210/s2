using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSO.Data.Entities
{
    public class Membership
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string LoweredUserName { get; set; }
        public string Password { get; set; }
        public string HashCode { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
        public bool IsApproved { get; set; }
        public bool IsLockedOut { get; set; }
    }
}
