using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication
{
    public class ManagerUser
    {
        private static IUserService getUserService()
        {
            return ReflectionHelper.CreateInstance<IUserService>(ManagerConfig.UserService);
        }

        public static List<int> GetParentGroupIDs(List<int> groupIds)
        {
            IUserService userService = ManagerUser.getUserService();
            return userService != null ? userService.GetParentGroupIds(groupIds) : new List<int>();
        }

        public static UserPrincipal GetUserPrincipal(
          IUserLogin user,
          ref SystemCommon.Error error)
        {
            return ManagerUser.getUserService()?.GetUserPrincipal(user, ref error);
        }

        public static UserPrincipal GetUserPrincipal(UserPrincipal user)
        {
            return ManagerUser.getUserService()?.GetUserPrincipal(user);
        }

        //public static void Register(
        //  IUserRegister user,
        //  ref SystemCommon.Error error,
        //  ref string activatecode)
        //{
        //    ManagerUser.getUserService()?.Register(user, ref error, ref activatecode);
        //}

        public static void Activate(string activatecode, ref SystemCommon.Error error)
        {
            ManagerUser.getUserService()?.Activate(activatecode, ref error);
        }

        public static void ActivateCompleted(
          string activatecode,
          string password,
          ref SystemCommon.Error error)
        {
            ManagerUser.getUserService()?.ActivateCompleted(activatecode, password, ref error);
        }

        public static List<string> GetPermissions(string moduleCode, List<int> roleIds)
        {
            List<string> stringList = (List<string>)null;
            IUserService userService = ManagerUser.getUserService();
            if (userService != null)
                stringList = userService.GetPermissions(moduleCode, roleIds);
            return stringList;
        }

        public static List<int> GetAllRoles()
        {
            List<int> intList = (List<int>)null;
            IUserService userService = ManagerUser.getUserService();
            if (userService != null)
                intList = userService.GetAllRoles();
            return intList;
        }
    }
}
