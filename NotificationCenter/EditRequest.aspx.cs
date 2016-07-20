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
    public partial class EditRequest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // if not posting back to get number by Parameter from address bar
                Request req = RequestDAL.GetSingleRequest(Convert.ToInt32(Request.QueryString["id"]));
                tb_type.Text = req.Type;
                tb_Date.Text = req.Date.ToShortDateString();
                tb_Status.Text = req.Status;
            }
        }

        protected void btn_Update_Click(object sender, EventArgs e)
        {
            
            Request req = RequestDAL.GetSingleRequest(Convert.ToInt32(Request.QueryString["id"]));
            string oldStatus = req.Status;
            req.Status = tb_Status.Text;
            RequestDAL.UpdateRequest(req);
            StatusHelper.AddStatus(req.Type, oldStatus, req.Status);
            Messages.Instance.IsModifiedStatus = true;
            Response.Redirect("Home.aspx");
        }

        protected void btn_BackToMainScreen_Click(object sender, EventArgs e)
        {
            StatusHelper.Clear();
            Messages.Instance.NotificationMessages = String.Empty;
            Messages.Instance.IsModifiedStatus = false ;
            Response.Redirect("Home.aspx");
        }
    }
}