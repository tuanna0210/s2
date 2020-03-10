using SSO.Data.Entities;
using SSO.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSO.Data
{
    public class MembershipDA : BaseSystemDA<cms_Membership, int>, IBaseSystemDA<cms_Membership>
    {
        public MembershipDA(bool syn = false) : base(syn) { }

        /// <summary>
        /// Thay đổi trạng thái bản ghi
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Message ShowHide(int id)
        {
            objMsg = new Message() { ID = id, Error = false };
            try
            {
                var item = GetById(id);
                if (item.IsLockedOut)
                {
                    item.IsLockedOut = false;
                    item.LastLockoutDate = DateTime.Now;
                }
                else item.IsLockedOut = true;
                Save();
                objMsg.Title = Notify.UpdateSuccess + item.UserName;
                return objMsg;
            }
            catch (Exception ex)
            {
                objMsg.Title = Notify.ExistsErorr;
                objMsg.Error = true;
                objMsg.Obj = ex;
                return objMsg;
            }
        }

        public Message ReSetPass(int id)
        {
            objMsg = new Message() { ID = id, Error = false };
            try
            {
                var item = GetById(id);
                string strPassWord = Common.RandomString(8);
                item.Password = Common.GetMd5x2(strPassWord);
                item.HashCode = Common.GetMd5x2(item.UserName + item.Password + "nguyễn văn duy");
                Save();
                objMsg.Title = "Cập nhật mật khẩu tài khoản <b>" + item.UserName + "</b> thành công, mật khẩu mới là:" + strPassWord;
                return objMsg;
            }
            catch (Exception ex)
            {
                objMsg.Title = Notify.ExistsErorr;
                objMsg.Error = true;
                objMsg.Obj = ex;
                return objMsg;
            }
        }
        public Message ReSetPass(string username, string password)
        {
            objMsg = new Message() { ID = 0, Title = username, Error = false };
            try
            {
                var item = GetByQuery(p => p.UserName == username);
                if (item.LastPasswordChangedDate != item.CreateDate)
                {
                    objMsg.Title = Notify.IncorrectPassword;
                    objMsg.Error = true;
                }
                string strPassWord = password;
                item.Password = Common.GetMd5x2(strPassWord);
                item.HashCode = Common.GetMd5x2(item.UserName + item.Password + "nguyễn văn duy");
                Save();
                objMsg.Title = "Cập nhật mật khẩu tài khoản <b>" + item.UserName + "</b> thành công, mật khẩu mới là:" + strPassWord;
                return objMsg;
            }
            catch (Exception ex)
            {
                objMsg.Title = Notify.ExistsErorr;
                objMsg.Error = true;
                objMsg.Obj = ex;
                return objMsg;
            }
        }
        //temp
        //public Message AddGroupRole2Membership(int id, int GroupRoleID)
        //{
        //    var item = GetById(id);
        //    Message objMsg = new Message() { ID = id, Error = false };
        //    try
        //    {
        //        var GroupRole = CmsContext.cms_GroupRoles.FirstOrDefault(p => p.ID == GroupRoleID);
        //        if (item.cms_GroupRoles.Contains(GroupRole))
        //        {
        //            item.cms_GroupRoles.Remove(GroupRole);
        //        }
        //        else
        //        {
        //            item.cms_GroupRoles.Add(GroupRole);
        //        }
        //        Save();
        //        objMsg.Title = Notify.UpdateSuccess + item.Displayname;
        //        return objMsg;
        //    }
        //    catch (Exception ex)
        //    {
        //        objMsg.Title = Notify.ExistsErorr;
        //        objMsg.Error = true;
        //        objMsg.Obj = ex;
        //        return objMsg;
        //    }
        //}

        public bool CheckAccount(string username, string password)
        {
            try
            {
                var acc = GetByQuery(p => p.UserName == username);
                var strPassword = Common.GetMd5x2(password);
                var hashCode = Common.GetMd5x2(acc.UserName + strPassword + "nguyễn văn duy");
                var item = GetByQuery(p => p.UserName == username && p.Password == strPassword && p.HashCode == hashCode);
                if (item == null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //public Message Login(string username, string password)
        //{
        //    var MembershipItem = (from c in CmsContext.cms_Membership where c.UserName == username && c.Password == password select c).FirstOrDefault();

        //    if (MembershipItem != null)
        //    {
        //        if (Common.GetMd5x2(MembershipItem.UserName + MembershipItem.Password + "nguyễn văn duy") != MembershipItem.HashCode)
        //        {
        //            objMsg.Title = Notify.IncorrectPassword;
        //            objMsg.Error = true;
        //            return objMsg;
        //        }
        //        if (!MembershipItem.IsApproved)
        //        {
        //            objMsg.Title = Notify.LoginNotActivated;
        //            objMsg.Error = true;
        //            return objMsg;
        //        }
        //        if (!MembershipItem.IsLockedOut)
        //        {
        //            // Đăng nhập thành công
        //            objMsg.Title = "Đăng nhập thành công!";
        //            objMsg.Error = false;
        //            MembershipItem.FailedPasswordAttemptCount = 0;
        //            Save();
        //            // Sử dụng form Authenticate
        //            //ID#UserName#Displayname#ApplicationId#lstDANHSACHQUYEN

        //            #region Danh sách quyền

        //            List<int> lstDANHSACHQUYEN = new List<int>();
        //            var lstGroupRole = MembershipItem.cms_GroupRoles.Select(p => p.cms_Roles).ToList();
        //            foreach (var lstRole in lstGroupRole)
        //            {
        //                foreach (var _Role in lstRole)
        //                {
        //                    lstDANHSACHQUYEN.Add(_Role.ID);
        //                }
        //            }

        //            #endregion Danh sách quyền

        //            au.SignIn(MembershipItem.ID + "#" + MembershipItem.UserName + "#" + MembershipItem.Displayname + "#" + MembershipItem.ApplicationId + "#," + string.Join(",", lstDANHSACHQUYEN) + ",", 1000, "LOGINFOLDIO2013");
        //            return objMsg;
        //        }
        //        else
        //        {
        //            objMsg.Title = "Tài khoản này đang bị khóa!";
        //            objMsg.Error = true;
        //            return objMsg;
        //        }
        //    }
        //    objMsg.Title = "Username hoặc mật khẩu không đúng!";
        //    objMsg.Error = true;
        //    return objMsg;
        //}

        ///// <summary>
        ///// Kiểm tra người dùng có quyền hay không
        ///// </summary>
        ///// <param name="idQuyen">ID quyền</param>
        ///// <returns>bool</returns>
        //public bool CheckRole(int idQuyen)
        //{
        //    if (au.GetListRole().Contains("," + idQuyen + ","))
        //        return true;
        //    return false;
        //}

        //private Authentication.Client au = new Authentication.Client();
    }
}
