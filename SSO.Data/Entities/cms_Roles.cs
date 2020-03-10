using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSO.Data.Entities
{
    public class cms_Roles
    {
        public cms_Roles()
        {
            this.cms_GroupRoles = new HashSet<cms_GroupRoles>();
        }

        public System.Guid ApplicationId { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string LoweredRoleName { get; set; }
        public string Group { get; set; }
        public string Description { get; set; }
        public Nullable<int> ParentID { get; set; }
        public int Order { get; set; }
        public Nullable<int> Parent { get; set; }

        public virtual ICollection<cms_GroupRoles> cms_GroupRoles { get; set; }
    }
}
