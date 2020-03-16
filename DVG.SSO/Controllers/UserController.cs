using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DVG.SSO.Controllers
{
    public class UserController : BaseController
    {
        // GET: User
        public ActionResult List()
        {
            return View();
        }
    }
}