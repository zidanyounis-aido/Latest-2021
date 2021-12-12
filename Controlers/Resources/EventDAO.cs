using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;

namespace DMS.Resources
{
    public class EventDAO
    {
        //change the connection string as per your database connection.

        private static string connectionString;

        //this method retrieves all events within range start-end
        public static List<CalendarEvent> getEvents(DateTime start, DateTime end, int userId)
        {
            CommonFunction.clsCommon c = new CommonFunction.clsCommon();
            connectionString = c.decrypt(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
            List<CalendarEvent> events = new List<CalendarEvent>();
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT event_id, description, title, event_start, event_end, all_day,Color,CreatedBy FROM Event where event_start>=@start AND event_end<=@end", con);
            cmd.Parameters.Add("@start", SqlDbType.DateTime).Value = start;
            cmd.Parameters.Add("@end", SqlDbType.DateTime).Value = end;
            using (con)
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    events.Add(new CalendarEvent()
                    {
                        id = Convert.ToInt32(reader["event_id"]),
                        title = Convert.ToString(reader["title"]),
                        description = Convert.ToString(reader["description"]),
                        start = Convert.ToDateTime(reader["event_start"]),
                        end = Convert.ToDateTime(reader["event_end"]),
                        allDay = Convert.ToBoolean(reader["all_day"]),
                        backgroundColor = Convert.ToString(reader["Color"]) != null ? Convert.ToString(reader["Color"]) : "#f68b1e",
                        CreatedBy = Convert.ToString(reader["CreatedBy"])
                    });
                }
            }
            //get all non complete tasks
            //CommonFunction.clsCommon c = new CommonFunction.clsCommon();
            Hashtable parameters = new Hashtable();
            parameters.Add("@userId", userId);
            parameters.Add("@start", start);
            parameters.Add("@end", end);
            DataTable dt = c.GetDataAsDataTableFromSP("getToDoListCalender", parameters);
            foreach (var item in dt.AsEnumerable())
            {
                //DateTime dat
                events.Add(new CalendarEvent()
                {
                    id = Convert.ToInt32(item.Field<int>("Id")),
                    title = Convert.ToString(item.Field<string>("TaskName")),
                    description = Convert.ToString(item.Field<string>("Description")),
                    start = item.Field<DateTime>("TaskDate"),
                    end = item.Field<DateTime>("TaskDate").AddDays(1),
                    allDay = true,
                    backgroundColor = "#808e9b"
                    ,
                    color = "#808e9b"
                });
            }
            return events;
            //side note: if you want to show events only related to particular users,
            //if user id of that user is stored in session as Session["userid"]
            //the event table also contains an extra field named 'user_id' to mark the event for that particular user
            //then you can modify the SQL as:
            //SELECT event_id, description, title, event_start, event_end FROM event where user_id=@user_id AND event_start>=@start AND event_end<=@end
            //then add paramter as:cmd.Parameters.AddWithValue("@user_id", HttpContext.Current.Session["userid"]);
        }
        public static List<CalendarEvent> getEventsById(int event_id)
        {
            CommonFunction.clsCommon c = new CommonFunction.clsCommon();
            connectionString = c.decrypt(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
            List<CalendarEvent> events = new List<CalendarEvent>();
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT event_id, description, title, event_start, event_end, all_day,Color,CreatedBy FROM Event where event_id=" + event_id, con);
            //cmd.Parameters.Add("@start", SqlDbType.DateTime).Value = start;
            //cmd.Parameters.Add("@end", SqlDbType.DateTime).Value = end;
            using (con)
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    events.Add(new CalendarEvent()
                    {
                        id = Convert.ToInt32(reader["event_id"]),
                        title = Convert.ToString(reader["title"]),
                        description = Convert.ToString(reader["description"]),
                        start = Convert.ToDateTime(reader["event_start"]),
                        end = Convert.ToDateTime(reader["event_end"]),
                        allDay = Convert.ToBoolean(reader["all_day"]),
                        backgroundColor = Convert.ToString(reader["Color"]) != null ? Convert.ToString(reader["Color"]) : "#f68b1e",
                        CreatedBy = Convert.ToString(reader["CreatedBy"])
                    });
                }
            }
            return events;
        }
        //this method updates the event title and description
        public static void updateEvent(int id, String title, String description, string backgroundColor)
        {
            CommonFunction.clsCommon c = new CommonFunction.clsCommon();
            connectionString = c.decrypt(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("UPDATE Event SET title=@title, description=@description,Color=@Color WHERE event_id=@event_id", con);
            cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = title;
            cmd.Parameters.Add("@description", SqlDbType.NVarChar).Value = description;
            cmd.Parameters.Add("@Color", SqlDbType.NVarChar).Value = backgroundColor;
            cmd.Parameters.Add("@event_id", SqlDbType.Int).Value = id;
            using (con)
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        //this method updates the event start and end time ... allDay parameter added for FullCalendar 2.x
        public static void updateEventTime(int id, DateTime start, DateTime end, bool allDay)
        {
            CommonFunction.clsCommon c = new CommonFunction.clsCommon();
            connectionString = c.decrypt(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("UPDATE Event SET event_start=@event_start, event_end=@event_end, all_day=@all_day WHERE event_id=@event_id", con);
            cmd.Parameters.Add("@event_start", SqlDbType.DateTime).Value = start;
            cmd.Parameters.Add("@event_end", SqlDbType.DateTime).Value = end;
            cmd.Parameters.Add("@event_id", SqlDbType.Int).Value = id;
            cmd.Parameters.Add("@all_day", SqlDbType.Bit).Value = allDay;

            using (con)
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        //this mehtod deletes event with the id passed in.
        public static void deleteEvent(int id)
        {
            CommonFunction.clsCommon c = new CommonFunction.clsCommon();
            connectionString = c.decrypt(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("DELETE FROM Event WHERE (event_id = @event_id)", con);
            cmd.Parameters.Add("@event_id", SqlDbType.Int).Value = id;

            using (con)
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        //this method adds events to the database
        public static int addEvent(CalendarEvent cevent)
        {
            //add event to the database and return the primary key of the added event row
            CommonFunction.clsCommon c = new CommonFunction.clsCommon();
            connectionString = c.decrypt(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString);
            //insert
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("INSERT INTO Event(title, description, event_start, event_end, all_day,CreatedBy,Color) VALUES(@title, @description, @event_start, @event_end, @all_day, @CreatedBy,@Color)", con);
            cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = cevent.title;
            cmd.Parameters.Add("@description", SqlDbType.NVarChar).Value = cevent.description;
            cmd.Parameters.Add("@event_start", SqlDbType.DateTime).Value = cevent.start;
            cmd.Parameters.Add("@event_end", SqlDbType.DateTime).Value = cevent.end;
            cmd.Parameters.Add("@all_day", SqlDbType.Bit).Value = cevent.allDay;
            cmd.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = cevent.CreatedBy;
            cmd.Parameters.Add("@Color", SqlDbType.NVarChar).Value = cevent.backgroundColor ?? "808e9b";
            int key = 0;
            using (con)
            {
                con.Open();
                cmd.ExecuteNonQuery();

                //get primary key of inserted row
                cmd = new SqlCommand("SELECT max(event_id) FROM Event where title=@title AND description=@description AND event_start=@event_start AND event_end=@event_end AND all_day=@all_day AND CreatedBy=@CreatedBy", con);
                cmd.Parameters.Add("@title", SqlDbType.NVarChar).Value = cevent.title;
                cmd.Parameters.Add("@description", SqlDbType.NVarChar).Value = cevent.description;
                cmd.Parameters.Add("@event_start", SqlDbType.DateTime).Value = cevent.start;
                cmd.Parameters.Add("@event_end", SqlDbType.DateTime).Value = cevent.end;
                cmd.Parameters.Add("@all_day", SqlDbType.Bit).Value = cevent.allDay;
                cmd.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = cevent.CreatedBy;
                key = (int)cmd.ExecuteScalar();
            }

            return key;
        }
    }
}