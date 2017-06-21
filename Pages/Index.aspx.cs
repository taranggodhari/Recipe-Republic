using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["username"] == null)
            Response.Redirect("Login.aspx");

        Label2.Text = Session["username"].ToString();
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        HttpCookie myCookies = Request.Cookies["theme"];
        if (myCookies != null)
            Page.Theme = myCookies.Value;

        //string theme = (string)Session["theme"];

        //if (theme != null)
        //{
        //    Page.Theme = theme;
        //}
        //else
        //{
        //    Page.Theme = "Light";
        //}
    }
}