using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NotificationCenter.Model;
using NotificationCenter.DAL;

namespace NotificationCenter
{
    public partial class InsertRequest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                tbDate.Text = DateTime.Now.ToShortDateString();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Request r = new Request();
            r.ClientID = Convert.ToInt32(Request.QueryString["id"]);
            r.Type = tbType.Text.Trim();
            r.Date = DateTime.Parse(tbDate.Text.Trim());
            r.Status = tbStatus.Text;
            RequestDAL.InsertRequest(r);

            Response.Redirect("Home.aspx");
        }
        
        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }
    }
}