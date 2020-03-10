using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSO.Data.Entities
{
    public class cms_Client
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Domain { get; set; }
        public string Token { get; set; }
        public bool Active { get; set; }
        public int Order { get; set; }
        public int ParentID { get; set; }
        public int CreatedUser { get; set; }
        public int LastUpdatedUser { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime LastUpdatedDate { get; set; }
    }
}
