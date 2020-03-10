using SSO.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSO.Data
{
    public class MembershipDA1
    {
        public List<Membership> GetListBySecurity(string username, string password)
        {
            using (var context = MainDbContext.SSODB())
            {
                return context.StoredProcedure("membership_getlistbysecurity")
                    .Parameter("_username", username)
                    .Parameter("_password", password)
                    .QueryMany<Membership>();
            }
        }
        public List<Membership> GetListByUsername(string username)
        {
            using (var context = MainDbContext.SSODB())
            {
                return context.StoredProcedure("membership_getlistbyusername")
                    .Parameter("_username", username)
                    .QueryMany<Membership>();
            }
        }
    }
}
