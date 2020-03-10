using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication
{
    public interface IUserService
    {
        UserPrincipal GetUserPrincipal(UserPrincipal user);

        UserPrincipal GetUserPrincipal(IUserLogin user, ref SystemCommon.Error error);

        //void Register(IUserRegister user, ref SystemCommon.Error error, ref string activatecode); //temp

        void Activate(string activatecode, ref SystemCommon.Error error);

        void ActivateCompleted(string activatecode, string password, ref SystemCommon.Error error);

        List<string> GetPermissions(string moduleCode, List<int> roleIds);

        List<int> GetAllRoles();

        List<int> GetParentGroupIds(List<int> groupIds);
    }
}
