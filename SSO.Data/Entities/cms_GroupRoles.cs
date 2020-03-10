using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSO.Data.Entities
{
    public class cms_GroupRoles
    {
        public cms_GroupRoles()
        {
            this.cms_Roles = new HashSet<cms_Roles>();
            this.cms_Membership = new HashSet<cms_Membership>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ParentID { get; set; }
        public bool Active { get; set; }
        public Nullable<bool> IsDepartment { get; set; }
        public int Order { get; set; }
        public string AllChildrent { get; set; }
        public int CreatedUser { get; set; }
        public int LastUpdatedUser { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime LastUpdatedDate { get; set; }

        public virtual ICollection<cms_Roles> cms_Roles { get; set; }
        public virtual ICollection<cms_Membership> cms_Membership { get; set; }
    }
}
