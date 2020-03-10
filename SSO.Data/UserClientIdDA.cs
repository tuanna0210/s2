using SSO.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSO.Data
{
    public class UserClientIdDA
    {
        public List<UserClientId> GetListByUsernameAndDomain(string username, string domain)
        {
            using (var context = MainDbContext.SSODB())
            {
                return context.StoredProcedure("userclientid_getbyusernameanddomain")
                    .Parameter("_username", username)
                    .Parameter("_domain", domain)
                    .QueryMany<UserClientId>();
            }
        }
    }
}
