using MvcAuthenication;
using SSO.BLL;
using SSO.Data;
using SSO.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DVG.SSO.Controllers
{
    public class MyAccountController : BaseController
    {
        [SystemAuthorize]
        public ActionResult Dashboard()
        {
            var item = objMembershipDA.GetById(User.ID);
            return View(item);
        }
        [SystemAuthorize]
        public ActionResult ChangePassword()
        {
            return View(objSystemBLL.GetUserChangePassword(User.ID));
        }
        [AcceptVerbs(HttpVerbs.Post)]
        [SystemAuthorize]
        public JsonResult ChangePassword(SystemUserChangePassword user)
        {
            try
            {
                Msg = objSystemBLL.ChangePassword(user);
                if (!Msg.Error)
                    SystemAuthenticate.Logout();
            }
            catch (Exception ex)
            {
                Msg.Title = Notify.ExistsErorr;
                Msg.Error = true;
                LogMan.Instance.WriteErrorToLog(ex, ControlerName);
            }
            return Json(Msg, JsonRequestBehavior.AllowGet);
        }
        #region Khởi tạo đối tượng
        private MembershipDA objMembershipDA = new MembershipDA();
        private MyAccountBLL objSystemBLL = new MyAccountBLL();

        #endregion Khởi tạo đối tượng
    }
}