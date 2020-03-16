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
    //SystemBLL
    public class MyAccountBLL : BaseBLL
    {
        private MembershipDA objMembershipDA = new MembershipDA();
        public SystemUserChangePassword GetUserChangePassword(int userid)
        {
            SystemUserChangePassword user = new SystemUserChangePassword();
            try
            {
                Membership item = objMembershipDA.GetById(userid);
                user.ID = item.Id;
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
                Membership item = objMembershipDA.GetById(user.ID);
                if (item.Password != Common.GetMd5x2(user.PasswordOld))
                {
                    Msg.Title = Notify.IncorrectPassword;
                    Msg.Error = true;
                    return Msg;
                }
                item.Password = Common.GetMd5x2(user.Password);
                item.HashCode = Common.GetMd5x2(item.Username + item.Password);
                item.LastPasswordChangedDate = DateTime.Now;
                objMembershipDA.Update(item);
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
    }
}
