using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSO.Data.Entities
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Domain { get; set; }    
        public string Token { get; set; }
        public bool Active { get; set; }
        public int Order { get; set; }
        public int ParentId { get; set; }
        public int CreatedUser { get; set; }
        public int LastUpdatedUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }

    }
}
