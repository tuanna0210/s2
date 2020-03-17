using SSO.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DVG.SSO.Filters
{
    public class SSORoleProvider : System.Web.Security.RoleProvider
    {
        public SSORoleProvider()
        { }
        public override bool IsUserInRole(string username, string roleName)
        {
            string[] roles = GetRolesForUser(username);
            foreach (string role in roles)
            {
                if (roleName.Equals(role, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }
        public override string[] GetRolesForUser(string username)
        {
            //Implement the logic to resolve the user's name
            //using (var db = ContextFactory.CreateDefault())
            //{
            //    var rolePermissionManager = new RolePermissionManager(db);
            //    return rolePermissionManager.ResolveRoleName(username);
            //}
            var userRoleDA = new UserRoleDA();
            userRoleDA.GetListByUsername(username);
            string[] roles = new string[] { "deer", "moose", "boars" };
            return roles;
        }

        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }        
        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }      
        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}