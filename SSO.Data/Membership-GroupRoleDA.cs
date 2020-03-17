using SSO.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSO.Data
{
    public class Membership_GroupRoleDA
    {
        public List<Membership_GroupRole> GetListByUserId(int userId)
        {
            using (var context = MainDbContext.SSODB())
            {
                return context.StoredProcedure("membership_grouprole_getbyuserid")
                    .Parameter("_userid", userId)
                    .QueryMany<Membership_GroupRole>();
            }
        }
    }
}
