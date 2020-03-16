using SSO.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSO.Data
{
    public class ClientDA
    {
        public Client GetByDomain(string domain)
        {
            using (var context = MainDbContext.SSODB())
            {
                return context.StoredProcedure("client_getbydomain")
                    .Parameter("_domain", domain)
                    .QuerySingle<Client>();
            }
        }
    }
}
