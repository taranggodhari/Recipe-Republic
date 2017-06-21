using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Setup : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["username"] == null)
            Response.Redirect("Login.aspx");
    }
    protected void btnSet_Click(object sender, EventArgs e)
    {
        if (rdbDark.Checked)
        {
            HttpCookie myCookies = new HttpCookie("theme", "Dark");
            myCookies.Expires = DateTime.Now.AddMonths(1);
            Response.Cookies.Add(myCookies);
//            Session["theme"] = "Dark";
        }
        else if (rdbLight.Checked)
        {

            HttpCookie myCookies = new HttpCookie("theme", "Light");
            myCookies.Expires = DateTime.Now.AddMonths(1);
            Response.Cookies.Add(myCookies);
            //            Session["theme"] = "Light";
        }
        Response.Redirect("Setup.aspx");
       

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