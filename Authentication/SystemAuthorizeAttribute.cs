using MvcAuthenication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Authentication
{
    public class SystemAuthorizeAttribute : AuthorizeAttribute
    {
        private bool allowAnonymous = false;
        private bool checkAuthorize = true;
        private bool checkAuthorizeSuperAdmin = false;
        private string _Role;
        private int _ID;
        private string _Group;
        private string _Name;

        public int ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                this._ID = value;
            }
        }

        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(this._Name))
                    this._Name = this.Role;
                return this._Name;
            }
            set
            {
                this._Name = value;
            }
        }

        public string Role
        {
            get
            {
                if (string.IsNullOrEmpty(this._Role))
                    this._Role = string.Empty;
                return this._Role;
            }
            set
            {
                this._Role = value;
            }
        }

        public string Group
        {
            get
            {
                if (string.IsNullOrEmpty(this._Group))
                    this._Group = string.Empty;
                return this._Group;
            }
            set
            {
                this._Group = value;
            }
        }

        public bool AllowAnonymous
        {
            get
            {
                return this.allowAnonymous;
            }
            set
            {
                this.allowAnonymous = value;
            }
        }

        public bool CheckAuthorize
        {
            get
            {
                return this.checkAuthorize;
            }
            set
            {
                this.checkAuthorize = value;
            }
        }

        public bool CheckAuthorizeSuperAdmin
        {
            get
            {
                return this.checkAuthorizeSuperAdmin;
            }
            set
            {
                this.checkAuthorizeSuperAdmin = value;
            }
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
                throw new ArgumentNullException(nameof(filterContext));
            if (this.AllowAnonymous)
                return;
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = (ActionResult)new RedirectResult(MvcAuthenication.SystemAuthenticate.LoginUrl + filterContext.HttpContext.Request.Url.AbsolutePath);
            }
            else
            {
                bool flag1 = true;
                if (filterContext.Controller is BaseSystemController controller && this.CheckAuthorize)
                {
                    bool flag2 = false;
                    object[] customAttributes = filterContext.ActionDescriptor.GetCustomAttributes(typeof(SystemAuthorizeAttribute), false);
                    if (((IEnumerable<object>)customAttributes).Count<object>() == 0)
                        flag2 = false;
                    else
                        controller.ID = ((SystemAuthorizeAttribute)customAttributes[0]).ID;
                    flag1 = controller.CheckPermission(controller.ID);
                }
                if (!flag1)
                    filterContext.Result = (ActionResult)new RedirectResult(MvcAuthenication.SystemAuthenticate.AccessDeniedUrl);
            }
        }
    }
}