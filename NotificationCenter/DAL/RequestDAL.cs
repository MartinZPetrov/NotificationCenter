using NotificationCenter.Hubs;
using NotificationCenter.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace NotificationCenter.DAL
{
    public class RequestDAL
    {
        public static List<Request> GetRequestPerClient(int clientID, string orderField = null, bool asc = false)
        {
            List<Request> requests = new List<Request>();

            // use ConfigurationManager class to read connection string from Web.config file
            string CS = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

            // create new SQLConnection Object
            using (SqlConnection con = new SqlConnection(CS))
            {
                //Create new SqlCommand object.
                SqlCommand cmd = null;
                if (!string.IsNullOrEmpty(orderField))
                {
                    cmd = new SqlCommand(string.Format("SELECT * FROM Request WHERE ClientID = {0} ORDER BY " + orderField + (asc ? " ASC" : " DESC"), clientID), con);
                }
                else
                {
                    cmd = new SqlCommand(string.Format("SELECT * FROM Request WHERE ClientID = {0}", clientID), con);
                }

                if (con.State == ConnectionState.Closed)
                    con.Open();
                
                // Create new SqlDataReader object by invoking ExecuteReader method.
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    // as long as there are more records to read Read == true
                    while (rdr.Read())
                    {
                        Request r = new Request();
                        r.RequestID = Convert.ToInt32(rdr["RequestID"]);
                        r.ClientID = Convert.ToInt32(rdr["ClientID"]);
                        r.Date = DateTime.Parse(rdr["Date"].ToString());
                        r.Type = rdr["Type"].ToString();
                        r.Status = rdr["Status"].ToString();
                        // populate list
                        requests.Add(r);
                    }
                }
            }
            return requests;
        }

        public static Request GetSingleRequest(int rID)
        {
            string CS = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM REQUEST WHERE RequestID=@RequestID", con);
                cmd.Parameters.Add(new SqlParameter("@RequestID", rID));
                con.Open();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    rdr.Read();
                    Request r = new Request();
                    r.RequestID = Convert.ToInt32(rdr["RequestID"]);
                    r.ClientID = Convert.ToInt32(rdr["ClientID"]);
                    r.Status = rdr["Status"].ToString();
                    r.Date = DateTime.Parse(rdr["Date"].ToString());
                    r.Type = rdr["Type"].ToString();

                    return r;
                }
            }
        }

        public static void UpdateRequest(Request r)
        {
            string CS = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("Update Request set Type=@Type, Date=@Date, Status=@Status where RequestID=@RequestID", con);
                cmd.Parameters.Add(new SqlParameter("@Type", r.Type));
                cmd.Parameters.Add(new SqlParameter("@Date", r.Date));
                cmd.Parameters.Add(new SqlParameter("@Status", r.Status));
                cmd.Parameters.Add(new SqlParameter("@RequestID", r.RequestID));
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public static List<Request> GetRequestSearch(string searchText)
        {
            List<Request> requst = new List<Request>();

            // use ConfigurationManager class to read connection string from Web.config file
            string CS = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

            // create new SQLConnection Object
            using (SqlConnection con = new SqlConnection(CS))
            {
                //Create new SqlCommand object.
                //Full outer joins is used in order to retrieve all records
                //which match the search criteria
                string query = @"SELECT DISTINCT Request.*
                                FROM Request
                                WHERE Type LIKE '%" + searchText + "%' or Status LIKE '%" + searchText + "%'";
                SqlCommand cmd = new SqlCommand(query, con);

                con.Open();

                // Create new SqlDataReader object by invoking ExecuteReader method.
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    // as long as there are more records to read Read == true
                    while (rdr.Read())
                    {
                        Request r = new Request();
                        r.RequestID = Convert.ToInt32(rdr["RequestID"]);
                        r.ClientID = Convert.ToInt32(rdr["ClientID"]);
                        r.Status = rdr["Status"].ToString();
                        r.Date = DateTime.Parse(rdr["Date"].ToString());
                        r.Type = rdr["Type"].ToString();
                        // populate list
                        requst.Add(r);
                    }
                }
            }
            return requst;
        }

        public static void DeleteRequst(int rID)
        {
            string CS = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM REQUEST WHERE RequestID=@RequestID", con);
                cmd.Parameters.Add(new SqlParameter("@RequestID", rID));
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }


        // Insert Request method
        public static void InsertRequest(Request r)
        {
            string CS = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("Insert INTO Request (ClientID, Type, Date, Status) values (@ClientID, @Type, @Date, @Status)", con);
                cmd.Parameters.Add(new SqlParameter("@ClientID", r.ClientID));
                cmd.Parameters.Add(new SqlParameter("@Type", r.Type));
                cmd.Parameters.Add(new SqlParameter("@Date", r.Date));
                cmd.Parameters.Add(new SqlParameter("@Status", r.Status));

                con.Open();
                var n = cmd.ExecuteNonQuery();
            }
        }
    }
}