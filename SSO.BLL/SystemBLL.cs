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
    public class SystemBLL : BaseBLL
    {
        public Message AddOrUpdate(cms_Membership item, List<int> lstGroupRoleID)
        {
            try
            {
                //temp
                //objMembershipDA = new MembershipDA(true);
                //objGroupRolesDA = new GroupRolesDA(true);
                //if (item.ID > 0)
                //{
                //    //CheckExits code

                //    item.cms_GroupRoles.Clear();
                //    var lstGroupRole = objGroupRolesDA.GetAll(c => lstGroupRoleID.Contains(c.ID));
                //    foreach (var GroupRole in lstGroupRole)
                //    {
                //        item.cms_GroupRoles.Add(GroupRole);
                //    }
                //    objMembershipDA.UpdateAndSubmit(item);
                //    Msg.Title = Notify.UpdateSuccess + item.Displayname;
                //}
                //else
                //{
                //    //CheckExits code

                //    var lstGroupRole = objGroupRolesDA.GetAll(c => lstGroupRoleID.Contains(c.ID));
                //    foreach (var GroupRole in lstGroupRole)
                //    {
                //        item.cms_GroupRoles.Add(GroupRole);
                //    }
                //    objMembershipDA.InsertAndSubmit(item);
                //    Msg.Title = Notify.AddSuccess + item.Displayname;
                //}
                return Msg;
            }
            catch (Exception ex)
            {
                LogMan.Instance.WriteErrorToLog(ex);
                Msg.Title = Notify.UpdateError;
                Msg.Error = true;
                return Msg;
            }
        }

        //temp
        //public SystemUserChangeQuestion GetUserChangeQuestion(int userid)
        //{
        //    SystemUserChangeQuestion user = new SystemUserChangeQuestion();
        //    try
        //    {
        //        cms_Membership item = objMembershipDA.GetById(userid);
        //        user.ID = item.ID;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogMan.Instance.WriteErrorToLog(ex);
        //    }
        //    return user;
        //}

        //temp
        //public Message ChangeQuestion(SystemUserChangeQuestion user)
        //{
        //    try
        //    {
        //        cms_Membership item = objMembershipDA.GetById(user.ID);
        //        item.PasswordQuestion = user.Question;
        //        item.PasswordAnswer = Common.GetMd5x2(user.Answer.ToLower());
        //        objMembershipDA.UpdateAndSubmit(item);
        //        Msg.Title = Notify.UpdateSuccess;
        //    }
        //    catch (Exception ex)
        //    {
        //        Msg.Title = Notify.ExistsErorr;
        //        Msg.Error = true;
        //        Msg.Obj = ex;
        //        LogMan.Instance.WriteErrorToLog(ex);
        //    }
        //    return Msg;
        //}

        public SystemUserChangePassword GetUserChangePassword(int userid)
        {
            SystemUserChangePassword user = new SystemUserChangePassword();
            try
            {
                cms_Membership item = objMembershipDA.GetById(userid);
                user.ID = item.ID;
            }
            catch (Exception ex)
            {
                LogMan.Instance.WriteErrorToLog(ex);
            }
            return user;
        }

        public Message ChangePassword(SystemUserChangePassword user)
        {
            try
            {
                cms_Membership item = objMembershipDA.GetById(user.ID);
                if (item.Password != Common.GetMd5x2(user.PasswordOld))
                {
                    Msg.Title = Notify.IncorrectPassword;
                    Msg.Error = true;
                    return Msg;
                }
                item.Password = Common.GetMd5x2(user.Password);
                item.HashCode = Common.GetMd5x2(item.UserName + item.Password + "nguyễn văn duy");
                item.LastPasswordChangedDate = DateTime.Now;
                objMembershipDA.UpdateAndSubmit(item);
                Msg.Title = Notify.UpdateSuccess;
            }
            catch (Exception ex)
            {
                Msg.Title = Notify.ExistsErorr;
                Msg.Error = true;
                Msg.Obj = ex;
                LogMan.Instance.WriteErrorToLog(ex);
            }
            return Msg;
        }

        private MembershipDA objMembershipDA = new MembershipDA();
        //private GroupRolesDA objGroupRolesDA = new GroupRolesDA(); //temp
    }
}
