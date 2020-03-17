using DVG.SSO.Controllers;
using DVG.SSO.Filters;
using DVG.SSO.Models;
using MvcAuthenication;
using SSO.BLL;
using SSO.Data.Entities;
using SSO.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DVG.SSO.Areas.System.Controllers
{
    public class UserController : BaseController
    {
        private readonly SystemUserBLL userBLL = new SystemUserBLL();
        // GET: System/User
        [SystemAuthorize(ID = 1)]
        public new ActionResult List(int p = 1)
        {
            if( p<= 0)
            {
                p = 1;
            }
            var pageSize = 2;
            int totalPage = 0;
            var listUser = userBLL.GetUsers(p, pageSize);
            var totalRow = userBLL.SearchGetTotalRow();
            var remainder = totalRow % pageSize;
            var quotient = totalRow / pageSize;
            if (remainder == 0)
            {
                totalPage = (quotient > 0) ? quotient : 1;
            }
            else
            {
                totalPage = (quotient > 0) ? quotient + 1 : 1;
            }
            ViewBag.TotalPage = totalPage;
            return View(listUser);
        }
        [SystemAuthorize(ID = 1)]
        public ActionResult Create()
        {
            return View(new MembershipCreateModel());
        }
        [AcceptVerbs(HttpVerbs.Post)]
        [SystemAuthorize(ID = 1)]
        public JsonResult Create(FormCollection f)
        {
            try
            {
                Membership objMembership;

                objMembership = new Membership();
                UpdateModel(objMembership, f);
                Msg = userBLL.Create(objMembership);
            }
            catch (Exception ex)
            {
                Msg.Title = Notify.ExistsErorr;
                Msg.Error = true;
                Msg.Obj = ex;
                LogMan.Instance.WriteErrorToLog(ex, ControlerName);
            }
            return Json(Msg);
        }
    }
}