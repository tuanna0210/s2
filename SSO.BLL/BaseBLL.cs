using SSO.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSO.BLL
{
    public class BaseBLL
    {
        public BaseBLL()
        {
            objMsg = new Message() { Error = false };
        }

        public Message Msg
        {
            get { return objMsg; }
            set { objMsg = value; }
        }

        private Message objMsg;
    }
}
