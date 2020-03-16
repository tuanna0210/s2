using MvcAuthenication;
using SSO.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DVG.SSO.App_Start
{
    public class SystemConfig : IConfig
    {
        private SystemConfig()
        {
        }

        private static SystemConfig instance;

        public static SystemConfig Config
        {
            get
            {
                if (instance == null)
                {
                    instance = new SystemConfig();
                }
                return instance;
            }
        }

        public Type UserService
        {
            get
            {
                return typeof(SystemUserBLL);
            }
        }

        public Database Database
        {
            get
            {
                Database database = new Database();
                return database;
            }
        }

        public string Culture
        {
            get
            {
                return "vi-VN";
            }
        }

        public static void Register()
        {
            ManagerConfig.Initialize(Config);
        }
    }
}