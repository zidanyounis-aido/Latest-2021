using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace dms.App_Code.Common
{
    public class Examples
    {
        void sampes()
        {
            //Example 1
            CommonFunction.clsCommon c = new CommonFunction.clsCommon();
            DataTable dt = c.GetDataAsDataTable("select * from users ");

            //Example 2
            Hashtable parameters = new Hashtable();
            parameters.Add("@userID", 1);
            DataTable dt2 = c.GetDataAsDataTableFromSP("GetAllUsers", parameters);

            //Example 3
            c.NonQuery("inserrt into users ...... ");

            //Example 4
            Hashtable parameters2 = new Hashtable();
            parameters.Add("@userID", 1);
            parameters.Add("@userName", "Sameer");
            c.NonQueryFromSP("AddUsers", parameters2);

            //Example 5
            string username = c.GetDataAsScalar("select top 1 username where userid=1").ToString();

            //Example 6
            Hashtable parameters3 = new Hashtable();
            parameters.Add("@userID", 1);
            string username2 = c.GetDataAsScalarFromSP("getUsernameByID", parameters).ToString();

        }
    }
}