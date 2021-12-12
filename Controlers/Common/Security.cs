using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMS
{
    public static class Security
    {
        public static bool isNotAllowedCharacters(string _text)
        {
            
            string[] SQLIn = { ";", "'", "--", "/*", "xp_" };
            bool flag = true;
            try
            {
                for (Int32 i = 0; i < SQLIn.Length; i++)
                {
                    if (_text.Contains(SQLIn[i]))
                    {
                        flag = false;
                    }
                }
            }
            catch { }
            return flag;
        }

        public static Int32 userID = 0;

        public static Int32 getUserID()
        {
            if (HttpContext.Current.Session["userID"] != null)
            {
                return Convert.ToInt32(HttpContext.Current.Session["userID"]);
            }
            else
            {
                return 0;
            }
        }

        public static Int32 getProgramID(string URL)
        {
            Int32 startIndex = URL.LastIndexOf("/") + 1;
            Int32 endIndex = URL.LastIndexOf(".") - startIndex;
            URL = URL.Substring(startIndex, endIndex);
      
            DMS.DAL.operations op = new DAL.operations();
            tables.dbo.programs prg = op.dboGetAllPrograms("programURL like '%" + URL + "'");
            if (prg.hasRows)
            {
                return prg.fieldProgramID;
            }
            else
            {
                return -1;
            }
        }

        public static bool checkAllowedPage(Int32 userID, string URL)
        {
           // string[] allowedPages = { "dashboard.aspx","default.aspx", "subicons.aspx", "showdocument.aspx", "newDocument.aspx", "advancedSearch.aspx", "changePassword.aspx", "documentInfo.aspx", "documentsList.aspx", "innerPage.aspx", "searchResult.aspx", "subPages.aspx", "wfForm.aspx", "AddToDoList.aspx","EventsDocument.aspx", "manageProfile.aspx", "search.aspx" };
            string[] allowedPages = { "dashboard.aspx", "default.aspx", "showdocument.aspx",
                "newDocument.aspx", "advancedSearch.aspx", "changePassword.aspx", "documentInfo.aspx", "documentsList.aspx",
                "innerPage.aspx", "searchResult.aspx", "subPages.aspx", "wfForm.aspx",  "EventsDocument.aspx",
                "manageProfile.aspx", "search.aspx" };

            for (Int32 i = 0; i < allowedPages.Length; i++)
            {
                if (URL.ToLower().Contains("/" + allowedPages[i].ToLower()))
                    return true;
            }
            URL = URL.Replace("AddToDoList", "ToDoList");
 
             DMS.DAL.operations op = new DAL.operations();
            Int32 prgID = getProgramID(URL);
            if (prgID == -1)
            { return false; }
            else
            {
                op = new DAL.operations();
                tables.dbo.userPrograms userPrg = op.dboGetUserProgramsByPrimaryKey(prgID, userID);
                return userPrg.hasRows;

            }


        }
    }
}