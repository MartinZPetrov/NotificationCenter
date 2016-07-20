using NotificationCenter.DAL;
using NotificationCenter.Hubs;
using NotificationCenter.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Services;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NotificationCenter
{
    public partial class Home : System.Web.UI.Page
    {
        static int count;
        protected int UserID
        {
            get
            {
                if (ViewState["userID"] != null)
                    return Convert.ToInt32(ViewState["userID"]);
                else
                    return -1;
            }
            set
            {
                ViewState["userID"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.User.Identity.IsAuthenticated)
            {
                FormsAuthentication.RedirectToLoginPage();
            }

            if (Session["UserID"] != null)
            {
                UserID = int.Parse(Session["UserID"].ToString());
                clientID.Value = UserID.ToString();
            }

            //when you want to execute the code only the FIRST time the page is loaded
            if (!IsPostBack)
            {
                ViewState["count"] = 1;
                FellRequestList(UserID);
            }
        }
        private void FellRequestList(int userID)
        {
            if(userID == 0)
            {
                return;
            }
            rptRequestTable.DataSource = RequestDAL.GetRequestPerClient(userID);
            rptRequestTable.DataBind();
        }

        [WebMethod]
        public static string IsValidCertificate(string clientID)
        {
            if(string.IsNullOrEmpty(clientID))
            {
                return string.Empty;
            }
            bool isValid = false;
            string msg = string.Empty;
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            DateTime validTo;
            DateTime ValidFrom;
            string certificateID; 
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = null;
                con.Open();
                cmd = new SqlCommand(string.Format("Select * FROM Certificate WHERE ClientID = {0}", clientID), con);
                
                // Create new SqlDataReader object by invoking ExecuteReader method.
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    rdr.Read();
                    validTo = DateTime.Parse(rdr["ValidTo"].ToString());
                    ValidFrom = DateTime.Parse(rdr["ValidFrom"].ToString());
                    certificateID = rdr["CertificateID"].ToString();

                    if (validTo < DateTime.Today)
                    {
                        isValid = false;
                    }
                    else
                    {
                        isValid = true;
                    }
                }
            }
            if(!isValid)
            {
                Messages.Instance.NotificationMessages = string.Format("Certificate Expiration Notification!Valid From: {0} Valid To:{1} SerialNumber:{2}", ValidFrom, validTo, certificateID);
                NotificationDAL.InsertNotification(new Notification { Name = "EmailExpirationNotification", TypeGroup = "All Cients",  Channel = "Web", CriteriaGroup = "ValidateCertificateDate" });
                Messages.Instance.IsCheckCertificateExpiration = true;
            }
            else
            {
                Messages.Instance.NotificationMessages = string.Empty;
                Messages.Instance.IsCheckCertificateExpiration = false;
            }

            return Messages.Instance.NotificationMessages;
        }


        [WebMethod]
        public static List<Notification> GetNotifications(string clientID)
        {
            List<Notification> lst = null;
            lst = NotificationDAL.GetNotifications();
            return lst;
        }

        protected void lbtn_Delete_Command(object sender, CommandEventArgs e)
        {
            RequestDAL.DeleteRequst(Convert.ToInt32(e.CommandArgument));
            FellRequestList(UserID);
        }

        protected void btn_InsetNewRequest_Click(object sender, EventArgs e)
        {
            // when we click the button we will be redirected to new location
            string redirect = "InsertRequest.aspx?id={0}";
            Response.Redirect(string.Format(redirect, UserID));
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            rptRequestTable.DataSource = RequestDAL.GetRequestSearch(tb_SearchText.Text.Trim());
            rptRequestTable.DataBind();
        }

        protected void loadRepeater(string reqname, int count)
        {
            if (count == 0)
            {
                rptRequestTable.DataSource = RequestDAL.GetRequestPerClient(UserID, reqname, true); // ASC order
                ViewState["count"] = 1;
            }
            else if (count == 1)
            {
                rptRequestTable.DataSource = RequestDAL.GetRequestPerClient(UserID, reqname, false);// DESC Order
                ViewState["count"] = 0;
            }
            rptRequestTable.DataBind();
        }

        protected void linreq_type_Click(object sender, EventArgs e)
        {
            count = Convert.ToInt32(ViewState["count"].ToString());
            ViewState["count"] = count;
            loadRepeater("type", count);
        }

        protected void lnkreq_date_Click(object sender, EventArgs e)
        {
            count = Convert.ToInt32(ViewState["count"].ToString());
            ViewState["count"] = count;
            loadRepeater("date", count);
        }

        protected void linreq_status_Click(object sender, EventArgs e)
        {
            count = Convert.ToInt32(ViewState["count"].ToString());
            ViewState["count"] = count;
            loadRepeater("status", count);
        }

        [WebMethod]
        public static void CheckStatusNotification()
        {
            if(!StatusHelper.ContainsStatues()  && !Messages.Instance.IsModifiedStatus)
            {
                return;
            }
            Messages.Instance.NotificationMessages = string.Format("Notification Alert!Request Type: {0} Old Status: {1} New Status {2}", StatusHelper.StatusType, StatusHelper.OldStatus, StatusHelper.NewStatus);
            NotificationDAL.InsertNotification(new Notification { Name = "StatusUpdateNotification", TypeGroup = "All Cients", Channel = "Web", CriteriaGroup = "Modify of Status" });
            StatusHelper.Clear();
            Messages.Instance.IsModifiedStatus = false;
        }
    }
}