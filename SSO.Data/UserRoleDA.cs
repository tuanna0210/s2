using SSO.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSO.Data
{
    public class UserRoleDA
    {
        public List<UserRole> GetListByUserId(int userId)
        {
            using (var context = MainDbContext.SSODB())
            {
                return context.StoredProcedure("userrole_getbyuserid")
                    .Parameter("_userid", userId)
                    .QueryMany<UserRole>();
            }
        }
        public List<UserRole> GetListByUsername(string username)
        {
            using (var context = MainDbContext.SSODB())
            {
                return context.StoredProcedure("userrole_getbyusername")
                    .Parameter("_username", username)
                    .QueryMany<UserRole>();
            }
        }
    }
}
