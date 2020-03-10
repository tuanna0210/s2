using SSO.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSO.Data
{
    public class UserClientDA
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
        public UserClient GetListByUsernameAndClientId(string username, int clientid)
        {
            using (var context = MainDbContext.SSODB())
            {
                return context.StoredProcedure("userclient_getbyusernameandclientid")
                    .Parameter("_username", username)
                    .Parameter("_clientid", clientid)
                    .QuerySingle<UserClient>();
            }
        }
        public void Update(UserClient userClient)
        {
            using (var context = MainDbContext.SSODB())
            {
                context.Update("userclient")
                    .Column("username", userClient.Username)
                    .Column("useridclient", userClient.UserIdClient)
                    .Column("clientid", userClient.ClientId)
                    .Column("islogin", userClient.IsLogin)
                    .Where("id", userClient.Id)
                    .Execute();
            }
        }
    }
}
