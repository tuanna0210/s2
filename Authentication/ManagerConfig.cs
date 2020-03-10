using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Authentication
{
    public class ManagerConfig
    {
        internal static Assembly MySystem;
        internal static string Culture;
        internal static Type UserService;

        public static void Initialize(IConfig config)
        {
            ManagerConfig.MySystem = config.GetType().Assembly;
            ManagerConfig.Culture = config.Culture;
            ManagerConfig.UserService = config.UserService;
        }

        public static string GetAppConfig(string pstrKey)
        {
            try
            {
                return new AppSettingsReader().GetValue(pstrKey, typeof(string)).ToString();
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}
