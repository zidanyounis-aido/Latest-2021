
using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Web.SessionState;
using DMS.Resources;

namespace dms.Screen
{
    /// <summary>
    /// Summary description for JsonResponse
    /// </summary>
    public class JsonResponse : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            DateTime start = Convert.ToDateTime(context.Request.QueryString["start"]);
            DateTime end = Convert.ToDateTime(context.Request.QueryString["end"]);
            var userId = Convert.ToInt32(HttpContext.Current.Session["userID"].ToString());
            List<int> idList = new List<int>();
            List<ImproperCalendarEvent> tasksList = new List<ImproperCalendarEvent>();

            //Generate JSON serializable events
            foreach (CalendarEvent cevent in EventDAO.getEvents(start, end,userId))
            {
                if (cevent.id ==4)
                {
                    var x = 6;
                }
                    tasksList.Add(new ImproperCalendarEvent
                    {
                        id = cevent.id,
                        title = cevent.title,
                        start = String.Format("{0:s}", cevent.start),
                        end = String.Format("{0:s}", cevent.end),
                        description = cevent.description,
                        desc="zidatest",
                        allDay = cevent.allDay,
                        backgroundColor = cevent.backgroundColor,
                        borderColor = cevent.backgroundColor,
                        color= cevent.backgroundColor,
                        CreatedBy = cevent.CreatedBy,
                    });
                    idList.Add(cevent.id);
            }

            context.Session["idList"] = idList;

            //Serialize events to string
            System.Web.Script.Serialization.JavaScriptSerializer oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            string sJSON = oSerializer.Serialize(tasksList);

            //Write JSON to response object
            context.Response.Write(sJSON);
        }

        public bool IsReusable
        {
            get { return false; }
        }
    }
}