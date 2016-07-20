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
    public class NotificationDAL
    {

        public static List<Notification> GetNotifications(string orderField = null, bool asc = false)
        {
            // create empty list which we'll populate width requests
            List<Notification> notifications = new List<Notification>();

            // use ConfigurationManager class to read connection string from Web.config file
            string CS = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

            // create new SQLConnection Object
            using (SqlConnection con = new SqlConnection(CS))
            {
                //Create new SqlCommand object.
                SqlCommand cmd = null;
                if (!string.IsNullOrEmpty(orderField))
                {
                    cmd = new SqlCommand("SELECT [NotificationID], [Name], [CriteriaGroup], [TypeGroup], [Channel] FROM [dbo].[Notification] ORDER BY " + orderField + (asc ? " ASC" : " DESC"), con);
                }
                else
                {
                    cmd = new SqlCommand(@"SELECT [NotificationID], [Name], [CriteriaGroup], [TypeGroup], [Channel] FROM [dbo].[Notification]", con);
                }
                cmd.Notification = null;
                var dependency = new SqlDependency(cmd);
                dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

                if (con.State == ConnectionState.Closed)
                    con.Open();


                // Create new SqlDataReader object by invoking ExecuteReader method.
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    // as long as there are more records to read Read == true
                    while (rdr.Read())
                    {
                        Notification n = new Notification();
                        n.NotificationID = Convert.ToInt32(rdr["NotificationID"]);
                        n.Name = rdr["Name"].ToString();
                        n.CriteriaGroup = rdr["CriteriaGroup"].ToString();
                        n.TypeGroup = rdr["TypeGroup"].ToString();
                        n.Channel = rdr["Channel"].ToString();
                        // populate list
                        notifications.Add(n);
                    }
                }
            }
            return notifications;
        }

        private static void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                MessagesHub.SendMessages();
            }
        }

        // Insert Notification method
        public static void InsertNotification(Notification n)
        {
            string CS = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;

            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Notification (Name, CriteriaGroup, TypeGroup, Channel) values (@Name, @CriteriaGroup, @TypeGroup, @Channel)", con);
                cmd.Parameters.Add(new SqlParameter("@Name", n.Name));
                cmd.Parameters.Add(new SqlParameter("@CriteriaGroup", n.CriteriaGroup));
                cmd.Parameters.Add(new SqlParameter("@TypeGroup", n.TypeGroup));
                cmd.Parameters.Add(new SqlParameter("@Channel", n.Channel));
                con.Open();
                var res = cmd.ExecuteNonQuery();
            }
        }
    }
}