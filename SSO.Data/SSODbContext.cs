using SSO.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSO.Data
{
    public class SSODbContext : DbContext
    {
        public SSODbContext()
            : base("name=SSO_V2")
        {
        }


        public virtual DbSet<cms_Membership> cms_Membership { get; set; }
        public virtual DbSet<cms_GroupRoles> cms_GroupRoles { get; set; }
        public virtual DbSet<cms_Roles> cms_Roles { get; set; }
        public virtual DbSet<UserClient> UserClient { get; set; }
    }
}
