using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Oracle.ManagedDataAccess.Client;

public partial class Pages_Recipes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["username"] == null)
            Response.Redirect("Login.aspx");

        if (!IsPostBack)
        {
            OracleConnection conn;
            OracleCommand comm;
            OracleDataReader reader;
            string connectionString =
            ConfigurationManager.ConnectionStrings[
            "RecipieConnection"].ConnectionString;
            conn = new OracleConnection(connectionString);
            comm = new OracleCommand(
              "SELECT RECIPIEID, RECIPIENAME, SUBMITTEDBY, PREPERATIONTIME FROM RECIPIEDETAILS order by RECIPIEID ASC ",
              conn);
            try
            {
                conn.Open();
                reader = comm.ExecuteReader();
                grid.DataSource = reader;
                grid.DataBind();
            }
            catch(Exception ex)
            {
                Label1.Text = ex.ToString();
            }
            conn.Close();
           
            //comm = new OracleCommand(
            //  "SELECT * FROM recipedetails ",
            //  conn);
            //reader = comm.ExecuteReader();
            //GridView2.DataSource = reader;
            //GridView2.DataBind();
        }
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        HttpCookie myCookies = Request.Cookies["theme"];
        if (myCookies != null)
            Page.Theme = myCookies.Value;
    }
    
    protected void grid_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = grid.SelectedRow;
        string s = row.Cells[0].Text;
        Session["selectedRowIndex"] = s;
        Response.Redirect("RecipieDetail.aspx");

    }
   

}