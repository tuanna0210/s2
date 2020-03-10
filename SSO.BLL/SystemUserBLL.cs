//using Authentication; //temp
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
        private MembershipDA1 userDA1 = new MembershipDA1();
        private RolesDA objRolesDA = new RolesDA();
        //private UserClientIdDA objUserClientIdDA = new UserClientIdDA();
        public string GetUserNameByID(int userid)
        {
            return userDA.GetById(userid).Displayname;
        }

        public void Register(IUserRegister item, ref SystemCommon.Error error, ref string activatecode)//temp
        {
        }

        /// <summary>
        /// Check allow activate
        /// </summary>
        /// <param name="activatecode"></param>
        /// <param name="error"></param>
        public void Activate(string activatecode, ref SystemCommon.Error error)
        {
            var check = userDA.GetByQuery(p => p.ActivateCode == activatecode && p.IsApproved == true);
            if (check != null && check.LastPasswordChangedDate.AddDays(1) > DateTime.Now)
            {
                error = SystemCommon.Error.ActivateSuccess;
                return;
            }
            error = SystemCommon.Error.ActivateFail;
        }

        public void ActivateCompleted(string activatecode, string password, ref SystemCommon.Error error)
        {
            var check = userDA.GetByQuery(p => p.ActivateCode == activatecode && p.IsApproved == true);
            if (check != null)
            {
                check.IsApproved = true;
                check.Password = Common.GetMd5x2(password);
                check.HashCode = Common.GetMd5x2(check.UserName + check.Password + "nguyễn văn duy");//temp
                userDA.UpdateAndSubmit(check);
                error = SystemCommon.Error.ActivateSuccess;
                return;
            }
            error = SystemCommon.Error.ActivateFail;
        }

        //public void Delete(int id)
        //{
        //    userDA.Delete(id);
        //    userDA.Save();
        //}

        //public SystemUser GetUser(string account, string password)
        //{
        //    var user = userDA.GetQuery(filter: u => u.Account.Contains(account) && u.Password.Contains(password) && u.IsActive).FirstOrDefault();
        //    return user;
        //}

        public Membership GetUser(string account, string password, ref SystemCommon.Error error)
        {
            var user = userDA1.GetListBySecurity(account,password).FirstOrDefault();

            if (user == null)
            {
                error = SystemCommon.Error.InfoIncorrect;
                return null;
            }
            if (user.HashCode != Common.GetMd5x2(user.Username + user.Password + "nguyễn văn duy"))//temp
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
            if (!userDA.CheckConnected())
            {
                error = SystemCommon.Error.ConnectFailed;
                return null;
            }
            var user = userDA1.GetListByUsername(account).FirstOrDefault();//temp

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
            //var user = GetUser(userPrincipal.Account.Trim(), Encryptor.Encrypt(userPrincipal.Password));
            //var newUserPrincipal = new UserPrincipal();

            //if (user != null)
            //{
            //    var roleIds = new SystemOrganizationService().GetRoleIds(user.ID);
            //    var groupIds = new SystemRoleService().GetGroupIds(roleIds);

            //    newUserPrincipal.ID = user.ID;
            //    newUserPrincipal.FullName = user.FullName;
            //    newUserPrincipal.Email = user.Email;
            //    newUserPrincipal.Account = userPrincipal.Account;
            //    newUserPrincipal.Password = userPrincipal.Password;
            //    newUserPrincipal.Code = userPrincipal.Code;
            //    newUserPrincipal.Remember = userPrincipal.Remember;
            //    newUserPrincipal.RoleIDs = roleIds;
            //    newUserPrincipal.GroupIDs = groupIds;
            //}
            //return newUserPrincipal;
            return null;
        }
        public UserPrincipal GetUserPrincipal(IUserLogin userLogin, ref SystemCommon.Error error)
        {
            //cms_Membership user;//temp
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
                //List<int> lstRole = new List<int>();
                //foreach (var item in user.cms_GroupRoles)
                //{
                //    if (item.cms_Roles.Count > 0)
                //    {
                //        lstRole.AddRange(item.cms_Roles.Select(p => p.ID));
                //    }
                //}
                userPrincipal.ID = user.Id;
                userPrincipal.FullName = user.DisplayName;
                userPrincipal.Email = user.Email;
                userPrincipal.Account = userLogin.UserName;
                userPrincipal.Password = userLogin.Password;
                userPrincipal.Remember = userLogin.Remember;
                userPrincipal.Avatar = string.IsNullOrEmpty(user.LoweredUserName) ? Notify.NoImage : user.LoweredUserName;
                //userPrincipal.RoleIDs = lstRole.Distinct().ToList();
            }
            return userPrincipal;
        }

        public List<int> GetAllRoles()
        {
            return objRolesDA.GetAll().Select(p => p.ID).ToList();
        }

        public List<string> GetPermissions(string moduleCode, List<int> roleIds)
        {
            var permissions = objRolesDA.GetAll(p => roleIds.Contains(p.ID) && p.Group == moduleCode).Select(p => p.Code).ToList();
            return permissions;
        }

        public List<int> GetParentGroupIds(List<int> groupIds)
        {
            //var parentIds = new SystemGroupService().GetParentGroupIDs(groupIds);
            //return parentIds;
            return null;
        }
    }
}