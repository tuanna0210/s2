using MvcAuthenication;
using SSO.Data;
using SSO.Data.Entities;
using SSO.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSO.BLL
{
    public class SystemUserBLL : IUserService
    {
        private MembershipDA userDA = new MembershipDA();
        private UserRoleDA userRoleDA = new UserRoleDA();
        public string GetUserNameByID(int userid)
        {
            return userDA.GetById(userid).DisplayName;
        }

        public void Register(IUserRegister item, ref SystemCommon.Error error, ref string activatecode)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Check allow activate
        /// </summary>
        /// <param name="activatecode"></param>
        /// <param name="error"></param>
        public void Activate(string activatecode, ref SystemCommon.Error error)
        {
            throw new NotImplementedException();
        }

        public void ActivateCompleted(string activatecode, string password, ref SystemCommon.Error error)
        {
            throw new NotImplementedException();
        }

        public Membership GetUser(string account, string password, ref SystemCommon.Error error)
        {
            var user = userDA.GetListBySecurity(account, password).FirstOrDefault();

            if (user == null)
            {
                error = SystemCommon.Error.InfoIncorrect;
                return null;
            }
            if (user.HashCode != Common.GetMd5x2(user.Username + user.Password))
            {
                error = SystemCommon.Error.InfoIncorrect;
                return null;
            }
            if (user != null && !user.IsApproved)
            {
                error = SystemCommon.Error.NotActivated;
            }

            if (user != null && !user.IsLockedOut)
            {
                error = SystemCommon.Error.LoginSuccess;
            }

            return user;
        }

        public Membership GetUser(string account, ref SystemCommon.Error error)
        {
            var user = userDA.GetListByUsername(account).FirstOrDefault();

            if (user == null)
            {
                error = SystemCommon.Error.InfoIncorrect;
                return null;
            }

            if (user != null && !user.IsApproved)
            {
                error = SystemCommon.Error.NotActivated;
            }

            if (user != null && !user.IsLockedOut)
            {
                error = SystemCommon.Error.LoginSuccess;
            }

            return user;
        }

        public UserPrincipal GetUserPrincipal(UserPrincipal userPrincipal)
        {
            return null;
        }
        public UserPrincipal GetUserPrincipal(IUserLogin userLogin, ref SystemCommon.Error error)
        {
            Membership user;
            if (string.IsNullOrEmpty(userLogin.Provider))
            {
                user = GetUser(userLogin.UserName, Common.GetMd5x2(userLogin.Password), ref error);
            }
            else
            {
                //user = GetUser(userLogin.UserName, ref error);
                user = GetUser(userLogin.UserName, Common.GetMd5x2(userLogin.Password), ref error);
            }
            var userPrincipal = new UserPrincipal();

            if (user != null && !user.IsLockedOut)
            {
                var listRoleByUser = userRoleDA.GetListByUserId(user.Id);

                userPrincipal.ID = user.Id;
                userPrincipal.FullName = user.DisplayName;
                userPrincipal.Email = user.Email;
                userPrincipal.Account = userLogin.UserName;
                userPrincipal.Password = userLogin.Password;
                userPrincipal.Remember = userLogin.Remember;
                userPrincipal.Avatar = string.IsNullOrEmpty(user.LoweredUserName) ? Notify.NoImage : user.LoweredUserName;
                userPrincipal.RoleIDs = listRoleByUser.Select(x=>x.Role).Distinct().ToList();
            }
            return userPrincipal;
        }

        public List<int> GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public List<string> GetPermissions(string moduleCode, List<int> roleIds)
        {
            throw new NotImplementedException();
        }

        public List<int> GetParentGroupIds(List<int> groupIds)
        {
            throw new NotImplementedException();
        }
        public List<Membership> GetUsers(int pageIndex, int pageSize)
        {
            return userDA.Search(pageIndex, pageSize);
        }
        public int SearchGetTotalRow()
        {
            return userDA.GetTotalRowFromSearch();
        }
        public Message Create(Membership model)
        {
            Message result = new Message();
            var userInfo = userDA.GetListByUsernameOrEmail(model.Username, model.Email);
            if(userInfo != null && userInfo.Count > 0)
            {
                result.Error = true;
                result.Title = "Username/email đã tồn tại";
            }
            else
            {
                model.Username = model.Username.ToLower();
                model.Email = model.Email.ToLower();
                model.Password = Common.GetMd5x2("123456");
                model.LoweredUserName = string.Empty;
                model.CreatedDate = DateTime.Now;
                model.LastLoginDate = model.CreatedDate;
                model.LastPasswordChangedDate = model.CreatedDate;
                model.LastLockoutDate = model.CreatedDate;
                model.FailedPasswordAttemptCount = 0;
                model.HashCode = Common.GetMd5x2(model.Username + model.Password);
                userDA.Insert(model);
                result.Error = false;
                result.Title = Notify.AddSuccess + model.Username;
            }
            return result;
        }
    }
}