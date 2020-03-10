using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Authentication
{
    public class UserPrincipal : IPrincipal
    {
        private IIdentity _Identity;
        private int _ID;
        private string _Fullname;
        private string _Account;
        private string _Password;
        private string _Email;
        private string _Code;
        private string _Logo;
        private string _Avatar;
        private string _Languague;
        private bool _Remember;
        private string _Provider;

        public UserPrincipal()
        {
        }

        public UserPrincipal(IIdentity identity)
        {
            this._Identity = identity;
        }

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

        public string FullName
        {
            get
            {
                if (string.IsNullOrEmpty(this._Fullname))
                    this._Fullname = string.Empty;
                return this._Fullname;
            }
            set
            {
                this._Fullname = value;
            }
        }

        public string Account
        {
            get
            {
                if (string.IsNullOrEmpty(this._Account))
                    this._Account = string.Empty;
                return this._Account;
            }
            set
            {
                this._Account = value;
            }
        }

        public string Password
        {
            get
            {
                if (string.IsNullOrEmpty(this._Password))
                    this._Password = string.Empty;
                return this._Password;
            }
            set
            {
                this._Password = value;
            }
        }

        public string Email
        {
            get
            {
                if (string.IsNullOrEmpty(this._Email))
                    this._Email = string.Empty;
                return this._Email;
            }
            set
            {
                this._Email = value;
            }
        }

        public string Code
        {
            get
            {
                if (string.IsNullOrEmpty(this._Code))
                    this._Code = string.Empty;
                return this._Code;
            }
            set
            {
                this._Code = value;
            }
        }

        public string Logo
        {
            get
            {
                if (string.IsNullOrEmpty(this._Logo))
                    this._Logo = string.Empty;
                return this._Logo;
            }
            set
            {
                this._Logo = value;
            }
        }

        public string Avatar
        {
            get
            {
                if (string.IsNullOrEmpty(this._Avatar))
                    this._Avatar = string.Empty;
                return this._Avatar;
            }
            set
            {
                this._Avatar = value;
            }
        }

        public string Languague
        {
            get
            {
                if (string.IsNullOrEmpty(this._Languague))
                    this._Languague = string.Empty;
                return this._Languague;
            }
            set
            {
                this._Languague = value;
            }
        }

        public string Provider
        {
            get
            {
                if (string.IsNullOrEmpty(this._Provider))
                    this._Provider = string.Empty;
                return this._Provider;
            }
            set
            {
                this._Provider = value;
            }
        }

        public bool Remember
        {
            get
            {
                return this._Remember;
            }
            set
            {
                this._Remember = value;
            }
        }

        public List<int> RoleIDs { get; set; }

        public List<int> GroupIDs { get; set; }

        public void InitIdentity()
        {
            this._Identity = (IIdentity)new GenericIdentity(this.FullName);
        }

        public IIdentity Identity
        {
            get
            {
                return this._Identity;
            }
        }

        public bool IsInRole(string role)
        {
            return false;
        }

        public bool IsInRole(int role)
        {
            return this.RoleIDs.Contains(role);
        }

        public bool IsAdministrator()
        {
            return this.Account == "Administrator";
        }
    }
}
