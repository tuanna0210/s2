using SSO.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSO.Data
{
    public class ClientDA : BaseSystemDA<cms_Client, int>, IBaseSystemDA<cms_Client>
    {
        public ClientDA(bool syn = false) : base(syn) { }
    }
}
