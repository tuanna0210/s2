using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Test
{
    public class UserClientDA1
    {
        public List<UserClient> GetListByUsername(string username)
        {
            using (var context = MainDbContext.SSODB())
            {
                return context.StoredProcedure("userclient_getbyusername")
                    .Parameter("_username", username)
                    .QueryMany<UserClient>();
            }
        }
        public void Update(UserClient userClient)
        {
            using (var context = MainDbContext.SSODB())
            {
                context.Update("cms_UserClient")//temp
                    .Column("UserName", userClient.username)
                    .Column("UserIDClient", userClient.useridclient)
                    .Column("ClientID", userClient.clientid)
                    .Column("IsLogin", userClient.islogin)
                    .Where("ID", userClient.Id)
                    .Execute();
            }
        }
    }
}