using FluentData;
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
        public void Insert(Membership membership)
        {
            using (var context = MainDbContext.SSODB())
            {
                var a = context.Insert("membership")
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
                    .Column("createddate", membership.CreatedDate)
                    .Column("lastlogindate", membership.LastLoginDate)
                    .Column("lastlockoutdate", membership.LastLockoutDate)
                    .Column("failedpasswordattemptcount", membership.FailedPasswordAttemptCount)
                    .Column("comment", membership.Comment)
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
        public List<Membership> Search(int pageIndex, int pageSize)
        {
            using (var context = MainDbContext.SSODB())
            {
                return context.StoredProcedure("membership_search")
                    .Parameter("_pageindex", pageIndex )
                    .Parameter("_pagesize", pageSize)
                    .Parameter("_filterkeyword", "temp")                  
                    .QueryMany<Membership>();
            }
        }
        public int GetTotalRowFromSearch()
        {
            using (var context = MainDbContext.SSODB())
            {
                var command = context.StoredProcedure("membership_search_getcount")
                    .Parameter("_filterkeyword", "temp")
                    .ParameterOut("_count", DataTypes.Int64, 100);
                command.Execute();
                int totlaRow = command.ParameterValue<int>("_count");
                return totlaRow;
            }
        }
    }
}
