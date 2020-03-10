using MvcAuthenication;
using SSO.BLL;
using SSO.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DVG.SSO.Areas.MyAccount.Controllers
{
    public class HomeController : DVG.SSO.Controllers.BaseController
    {
        // GET: MyAccount/Home
        [SystemAuthorize]
        public ActionResult Index()
        {
            return View();
        }
        [SystemAuthorize]
        public ActionResult mView()
        {
            //var item = objMembershipDA.GetById(User.ID);
            //return View(item);
            return View();
        }
        #region Khởi tạo đối tượng

        private MembershipDA objMembershipDA = new MembershipDA();
        private SystemBLL objSystemBLL = new SystemBLL();

        #endregion Khởi tạo đối tượng
    }
}