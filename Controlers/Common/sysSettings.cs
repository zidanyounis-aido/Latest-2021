using dms.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dms
{
    public static class sysSettings
    {
        private static UserData _userData = new UserData();

        public static string getSettingValue(string settingName)
        {
            CommonFunction.clsCommon c = new CommonFunction.clsCommon();
            string value = c.GetDataAsScalar($"select value from sysSettings where clientId= {_userData.ClientId} and setting='" + settingName + "'").ToString();
            return value;
        }
    }
}