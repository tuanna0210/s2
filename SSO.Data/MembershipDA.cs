using SSO.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSO.Data
{
    public class MembershipDA
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
        public Membership GetById(int userID)
        {
            using (var context = MainDbContext.SSODB())
            {
                return context.StoredProcedure("membership_getbyid")
                    .Parameter("_userid", userID)
                    .QuerySingle<Membership>();
            }
        }
        public void Update(Membership membership)
        {
            using (var context = MainDbContext.SSODB()) 
            {
                context.Update("membership")
                    .Column("username", membership.Username)
                    .Column("displayname", membership.DisplayName)
                    .Column("password", membership.Password)
                    .Column("loweredusername", membership.LoweredUserName)
                    .Column("hashcode", membership.HashCode)
                    .Column("email", membership.Email)
                    .Column("location", membership.Location)
                    .Column("isapproved", membership.IsApproved)
                    .Column("islockedout", membership.IsLockedOut)
                    .Column("lastpasswordchangeddate", membership.LastPasswordChangedDate)
                    .Where("id", membership.Id)
                    .Execute();
            }
        }
        public void UpdateOTPPrivateKey(int userId, string key)
        {
            using (var context = MainDbContext.SSODB())
            {
                context.StoredProcedure("membership_updateotprrivatekey")
                    .Parameter("_userid", userId)
                    .Parameter("_otpprivatekey", key)
                    .Execute();
            }
        }
    }
}
