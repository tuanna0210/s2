using SSO.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DVG.SSO.Controllers
{
    public class UserController : BaseController
    {
        private readonly SystemUserBLL userBLL = new SystemUserBLL();
        // GET: User
        public ActionResult List()
        {
            var listUser = userBLL.GetUsers();
            return View(listUser);
        }
    }
}