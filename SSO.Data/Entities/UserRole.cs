using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSO.Data.Entities
{
    public class UserRole
    {
        public int Id { get; set; }
        public int MembershipId { get; set; }
        public int Role { get; set; }
    }
}
