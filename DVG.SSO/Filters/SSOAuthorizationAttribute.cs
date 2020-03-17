using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DVG.SSO.Filters
{
    public class SSOAuthorizationAttribute : AuthorizeAttribute
    {
        private string _myKeys;
        public string MyKeys
        {
            get { return _myKeys; }
            set
            {
                Roles = value;
            }
        }
        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            return base.AuthorizeCore(httpContext);
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (filterContext.Result != null)
            {
                if (filterContext.Result.GetType() == typeof(HttpUnauthorizedResult))
                {
                    //Do the logic for the unathorized requests
                }
            }
        }
    }
}