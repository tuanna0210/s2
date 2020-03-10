using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSO.Data.Entities
{
    public class cms_Membership
    {
        public cms_Membership()
        {
            this.cms_GroupRoles = new HashSet<cms_GroupRoles>();
        }

        public System.Guid ApplicationId { get; set; }
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Displayname { get; set; }
        public string Gender { get; set; }
        public string Link { get; set; }
        public string Provider { get; set; }
        public string LoweredUserName { get; set; }
        public string MobileAlias { get; set; }
        public bool IsAnonymous { get; set; }
        public string Password { get; set; }
        public string HashCode { get; set; }
        public string Email { get; set; }
        public string PasswordQuestion { get; set; }
        public string PasswordAnswer { get; set; }
        public bool IsApproved { get; set; }
        public bool IsLockedOut { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.DateTime LastLoginDate { get; set; }
        public System.DateTime LastPasswordChangedDate { get; set; }
        public System.DateTime LastLockoutDate { get; set; }
        public int FailedPasswordAttemptCount { get; set; }
        public string ActivateCode { get; set; }
        public string Comment { get; set; }

        public virtual ICollection<cms_GroupRoles> cms_GroupRoles { get; set; }
    }
}
