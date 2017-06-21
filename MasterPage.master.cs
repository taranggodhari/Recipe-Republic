using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["username"] != null)
        { 
        Label2.Visible = true;
        Label1.Text = Session["username"].ToString();
            LogoutButton.Visible = true;
          }
        else
        {
            LogoutButton.Visible = false;
            Label2.Visible = false;
            Label1.Text = "";
        }
     }


    protected void LogoutButton_Click(object sender, EventArgs e)
    {
        Session["username"] = null;
        LogoutButton.Visible = false;
        Response.Redirect("Login.aspx");
    }
}
