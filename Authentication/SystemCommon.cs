using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication
{
    public class SystemCommon
    {
        public const string ConnectionString = "metadata=res://*/;provider=System.Data.SqlClient;provider connection string=\"data source={0};initial catalog={1};user id={2};password={3};MultipleActiveResultSets=True;App=EntityFramework\"";

        public enum Status : byte
        {
            Obsolete,
            Using,
            New,
        }

        public enum Error
        {
            LoginSuccess,
            LoginFail,
            RegisterSuccess,
            RegisterFail,
            ActivateFail,
            ActivateSuccess,
            NotSupportCookie,
            NotActivated,
            InfoIncorrect,
            CodeIncorrect,
            AccountExist,
            ConnectFailed,
            EmailIncorrect,
        }
    }
}
