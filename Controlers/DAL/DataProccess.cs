using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
namespace DMS.DAL
{
    public class DataProccess
    {
        const string passPhrase = "Pas5pr@se";
        const string saltValue = "s@1tValue";
        const string hashAlgorithm = "SHA1";
        const int passwordIterations=2;
        const string initVector = "@1B2c3D4e5F6g7H8";
        const int keySize=256;

        public Hashtable parameters = new Hashtable();

        private SqlConnection conn = new SqlConnection();
        private SqlCommand com = new SqlCommand();

        private string varConnectionString;
       
        public void setConnectionString(String connectionString)
        {
            varConnectionString = connectionString;
        }

        public void setConnectionString(String server, String dataBase, String userName="", String password="")
        {
            if (userName == "")
            {
                varConnectionString = "Data Source=" + server + ";Initial Catalog=" + dataBase + ";Integrated Security=True";
            }
            else
            {
                varConnectionString = "Data Source=" + server + ";Initial Catalog=" + dataBase + ";Persist Security Info=True;User ID=" + userName + ";Password=" + password;
            }
        }

        public void initializeVoid()
        {
            conn = new SqlConnection(varConnectionString);
            com = new SqlCommand();
            com.Connection = conn;
            parameters.Clear();
        }        


        public DataProccess()
        {
            SecurityNB.TextEncryption sec = new SecurityNB.TextEncryption();

            /*varConnectionString = sec.Decrypt(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString,
                passPhrase, saltValue, hashAlgorithm, passwordIterations, initVector, keySize);*/
            varConnectionString = sec.Decrypt(ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString, passPhrase, saltValue, hashAlgorithm, passwordIterations, initVector, keySize);
            initializeVoid();
        }


        public DataProccess(String connectionString)
        {
            setConnectionString(connectionString);
            initializeVoid();
        }


        public DataProccess(string server, string dataBase, string userName = "", string password = "")
        {
            setConnectionString(server, dataBase, userName, password);
            initializeVoid();
        }
		
        public object executeScalar(string storedProcedure)
        {
            string strParms = "";

            object i = "";
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = storedProcedure;
            com.Parameters.Clear();
            if (parameters.Count > 0)
            {
                foreach (string key in parameters.Keys)
                {
                    string _key = key;
                    if (parameters[_key] != null)
                    {
                        if (_key.StartsWith(@"@") == false)
                            _key = @"@" + _key;
                        if (parameters[_key].ToString() == DateTime.MaxValue.AddDays(-1).ToString())
                            com.Parameters.AddWithValue(_key, DBNull.Value);
                        else if (parameters[_key].ToString() == Int16.MinValue.ToString())
                            com.Parameters.AddWithValue(_key, DBNull.Value);
                        else
                            com.Parameters.AddWithValue(_key, parameters[_key]);
                    }
                    else
                    {
                        com.Parameters.AddWithValue(_key, DBNull.Value);
                    }

                    try
                    {
                        strParms += _key + "=" + parameters[_key].ToString() + ", ";
                    }
                    catch { }
                }
            }
            //try
            //{
                conn.Open();
                i = com.ExecuteScalar();
            //}
            //catch (Exception e)
            //{
            //    i = -1;
            //    catchingError("executeScalar", e);
            //}
            //finally
            //{
                //conn.Dispose();
                conn.Close();
            //}

                try
                {
                    Int32 userID;
                    if (HttpContext.Current.Session["userID"] != null)
                    {
                        userID = Convert.ToInt32(HttpContext.Current.Session["userID"]);

                        Int32 actionType;
                        string tableName;
                        string sp = storedProcedure;
                        sp = sp.Replace("dbo.", "");
                        if (sp.ToLower().StartsWith("get"))
                        {
                            actionType = 1;
                            tableName = sp.Replace("getAll", "").Replace("get", "");
                            tableName = tableName.Replace("ByPrimaryKey", "");
                        }
                        else if (sp.ToLower().StartsWith("update"))
                        {
                            actionType = 2;
                            tableName = sp.Replace("update", "");
                            tableName = tableName.Replace("ByPrimaryKey", "");
                        }
                        else if (sp.ToLower().StartsWith("delete"))
                        {
                            actionType = 3;
                            tableName = sp.Replace("delete", "");
                            tableName = tableName.Replace("ByPrimaryKey", "");
                        }
                        else if (sp.ToLower().StartsWith("add"))
                        {
                            actionType = 4;
                            tableName = sp.Replace("add", "");
                        }
                        else
                        {
                            actionType = 5;
                            tableName = sp;
                        }

                        if (actionType > 1)
                        {
                            com.CommandType = CommandType.StoredProcedure;
                            com.CommandText = "dbo.AddSysEvents";
                            com.Parameters.Clear();
                            com.Parameters.AddWithValue("@userID", userID);
                            com.Parameters.AddWithValue("@eventTypeID", 3);
                            com.Parameters.AddWithValue("@eventDateTime", DateTime.Now);
                            com.Parameters.AddWithValue("@URL", HttpContext.Current.Request.Url.AbsoluteUri);

                            conn.Open();
                            Int32 eventID = Convert.ToInt32(com.ExecuteScalar());
                            conn.Close();

                            com.CommandType = CommandType.StoredProcedure;
                            com.CommandText = "dbo.AddDataBaseEvents";
                            com.Parameters.Clear();
                            com.Parameters.AddWithValue("@sysEventID", eventID);
                            com.Parameters.AddWithValue("@DBActionTypeID", actionType);
                            com.Parameters.AddWithValue("@tableName", tableName);
                            com.Parameters.AddWithValue("@parameters", strParms);

                            conn.Open();
                            com.ExecuteNonQuery();
                            conn.Close();
                        }
                    }

                }
                catch (Exception ex)
                { }

            return i;
        }

        public Int32 excuteNonQuery(string storedProcedure)
        {
            string strParms="";
            Int32 i = 0;
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = storedProcedure;
            com.Parameters.Clear();
            if (parameters.Count > 0)
            {
                foreach (string key in parameters.Keys)
                {
                    string _key = key;
                    if (parameters[_key] != null)
                    {
                        if (_key.StartsWith(@"@") == false)
                            _key = @"@" + _key;
                        if (parameters[_key].ToString() == DateTime.MaxValue.AddDays(-1).ToString())
                            com.Parameters.AddWithValue(_key, DBNull.Value);
                        else if (parameters[_key].ToString() == Int16.MinValue.ToString())
                            com.Parameters.AddWithValue(_key, DBNull.Value);
                        else
                            com.Parameters.AddWithValue(_key, parameters[_key]);
                    }
                    else
                    {
                        com.Parameters.AddWithValue(_key, DBNull.Value);
                    }
                    try
                    {
                        strParms += _key + "=" + parameters[_key].ToString() + ", ";
                    }
                    catch { }
                }
            }
            //try
            //{
                conn.Open();
               i= com.ExecuteNonQuery();

               
            //}
            //catch (Exception e)
            //{
            //    i = -1;
            //    catchingError("excuteNonQuery", e);
            //}
            //finally
            //{
                //conn.Dispose();
                conn.Close();
            //}

                try
                {
                    Int32 userID;
                    if (HttpContext.Current.Session["userID"] != null)
                    {
                        

                        Int32 actionType;
                        string tableName;
                        string sp = storedProcedure;
                        sp = sp.Replace("dbo.", "");
                        if (sp.ToLower().StartsWith("get"))
                        {
                            actionType = 1;
                            tableName = sp.Replace("getAll", "").Replace("get", "");
                            tableName = tableName.Replace("ByPrimaryKey", "");
                        }
                        else if (sp.ToLower().StartsWith("update"))
                        {
                            actionType = 2;
                            tableName = sp.Replace("update", "");
                            tableName = tableName.Replace("ByPrimaryKey", "");
                        }
                        else if (sp.ToLower().StartsWith("delete"))
                        {
                            actionType = 3;
                            tableName = sp.Replace("delete", "");
                            tableName = tableName.Replace("ByPrimaryKey", "");
                        }
                        else if (sp.ToLower().StartsWith("add"))
                        {
                            actionType = 4;
                            tableName = sp.Replace("add", "");
                        }
                        else
                        {
                            actionType = 5;
                            tableName = sp;
                        }

                        if (actionType > 1)
                        {
                            userID = Convert.ToInt32(HttpContext.Current.Session["userID"]);

                            com.CommandType = CommandType.StoredProcedure;
                            com.CommandText = "dbo.AddSysEvents";
                            com.Parameters.Clear();
                            com.Parameters.AddWithValue("@userID", userID);
                            com.Parameters.AddWithValue("@eventTypeID", 3);
                            com.Parameters.AddWithValue("@eventDateTime", DateTime.Now);
                            com.Parameters.AddWithValue("@URL", HttpContext.Current.Request.Url.AbsoluteUri);

                            conn.Open();
                            Int32 eventID = Convert.ToInt32(com.ExecuteScalar());
                            conn.Close();

                            com.CommandType = CommandType.StoredProcedure;
                            com.CommandText = "dbo.AddDataBaseEvents";
                            com.Parameters.Clear();
                            com.Parameters.AddWithValue("@sysEventID", eventID);
                            com.Parameters.AddWithValue("@DBActionTypeID", actionType);
                            com.Parameters.AddWithValue("@tableName", tableName);
                            com.Parameters.AddWithValue("@parameters", strParms);

                            conn.Open();
                            com.ExecuteNonQuery();
                            conn.Close();
                        }
                    }

                }
                catch (Exception ex)
                { }

            return i;
        }


        public DataTable excuteQuery(string storedProcedure)
        {
            string strParms = "";

            DataTable dt = new DataTable();
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = storedProcedure;
            SqlDataAdapter da = new SqlDataAdapter();
            com.Parameters.Clear();
            da.SelectCommand = com;
            if (parameters.Count > 0)
            {
                foreach (string key in parameters.Keys)
                {
                    string _key = key;
                    if (parameters[_key] != null)
                    {
                        if (_key.StartsWith(@"@") == false)
                            _key = @"@" + _key;
                        if (parameters[_key].ToString() == DateTime.MaxValue.AddDays(-1).ToString())
                            com.Parameters.AddWithValue(_key, DBNull.Value);
                        else if (parameters[_key].ToString() == Int16.MinValue.ToString())
                            com.Parameters.AddWithValue(_key, DBNull.Value);
                        else
                            com.Parameters.AddWithValue(_key, parameters[_key]);
                    }
                    else
                    {
                        com.Parameters.AddWithValue(_key, DBNull.Value);
                    }

                    try
                    {
                        strParms += _key + "=" + parameters[_key].ToString() + ", ";
                    }
                    catch { }
                }
            }
            //try
            //{
                da.Fill(dt);

            //}
            //catch (Exception e)
            //{
            //    // catchingError("excuteQuery", e);
            //}

                //try
                //{
                //    Int32 userID;
                //    if (HttpContext.Current.Session["userID"] != null)
                //    {
                //        userID = Convert.ToInt32(HttpContext.Current.Session["userID"]);

                //        com.CommandType = CommandType.StoredProcedure;
                //        com.CommandText = "dbo.AddSysEvents";
                //        com.Parameters.Clear();
                //        com.Parameters.AddWithValue("@userID", userID);
                //        com.Parameters.AddWithValue("@eventTypeID", 3);
                //        com.Parameters.AddWithValue("@eventDateTime", DateTime.Now);
                //        com.Parameters.AddWithValue("@URL", HttpContext.Current.Request.Url.AbsoluteUri);

                //        conn.Open();
                //        Int32 eventID = Convert.ToInt32(com.ExecuteScalar());
                //        conn.Close();

                //        Int32 actionType;
                //        string tableName;
                //        string sp = storedProcedure;
                //        sp = sp.Replace("dbo.", "");
                //        if (sp.ToLower().StartsWith("get"))
                //        {
                //            actionType = 1;
                //            tableName = sp.Replace("getAll", "").Replace("get", "");
                //            tableName = tableName.Replace("ByPrimaryKey", "");
                //        }
                //        else if (sp.ToLower().StartsWith("update"))
                //        {
                //            actionType = 2;
                //            tableName = sp.Replace("update", "");
                //            tableName = tableName.Replace("ByPrimaryKey", "");
                //        }
                //        else if (sp.ToLower().StartsWith("delete"))
                //        {
                //            actionType = 3;
                //            tableName = sp.Replace("delete", "");
                //            tableName = tableName.Replace("ByPrimaryKey", "");
                //        }
                //        else if (sp.ToLower().StartsWith("add"))
                //        {
                //            actionType = 4;
                //            tableName = sp.Replace("add", "");
                //        }
                //        else
                //        {
                //            actionType = 5;
                //            tableName = sp;
                //        }

                //        com.CommandType = CommandType.StoredProcedure;
                //        com.CommandText = "dbo.AddDataBaseEvents";
                //        com.Parameters.Clear();
                //        com.Parameters.AddWithValue("@sysEventID", eventID);
                //        com.Parameters.AddWithValue("@DBActionTypeID", actionType);
                //        com.Parameters.AddWithValue("@tableName", tableName);
                //        com.Parameters.AddWithValue("@parameters", strParms);

                //        conn.Open();
                //        com.ExecuteNonQuery();
                //        conn.Close();

                //    }

                //}
                //catch (Exception ex)
                //{ }

            return dt;


        }

        public delegate void catchingErrorHandle(String Method, Exception e);
        public event catchingErrorHandle catchingError;
    }
}
