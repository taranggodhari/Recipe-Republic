using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

public partial class Pages_RecipieDetail : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (Session["username"] == null)
            Response.Redirect("Login.aspx");
        // Obtain the index of the selected row
        int recipieId = Convert.ToInt32((String)Session["selectedRowIndex"]);

        // Define data objects
        OracleConnection conn;
        OracleCommand comm;
        OracleDataReader reader;
        // Read the connection string from Web.config
        string connectionString =
            ConfigurationManager.ConnectionStrings[
            "RecipieConnection"].ConnectionString;
        // Initialize connection
        conn = new OracleConnection(connectionString);
        // Create command
        try
        {
            conn.Open();
            comm = new OracleCommand(
                "SELECT RECIPIEID, RECIPIENAME, SUBMITTEDBY, RECIPIECATEGORY.CATEGORY, PREPERATIONTIME, NUMBEROFSERVINGS, DESCRIPTION FROM RECIPIEDETAILS INNER JOIN RECIPIECATEGORY ON RECIPIEDETAILS.CATEGORYID = RECIPIECATEGORY.CATEGORYID WHERE RECIPIEID =: RecipieId", conn);
            // Add the EmployeeID parameter
            comm.Parameters.Add("RecipieId", OracleDbType.Int32);
            comm.Parameters["RecipieId"].Value = recipieId;
            // Enclose database code in Try-Catch-Finally

            // Open the connection

            // Execute the command
            reader = comm.ExecuteReader();
            // Fill the grid with data
            recipieDetails.DataSourceID = null;
            recipieDetails.DataSource = reader;
            recipieDetails.DataKeyNames = new string[] { "RecipieID" };
            recipieDetails.DataBind();
            // Close the reader
            reader.Close();
        }
        finally
        {
            // Close the connection
            conn.Close();
        }

        comm = new OracleCommand(
               "SELECT INGREDIENT.INGREDIENTID, INGREDIENT.INGREDIENTNAME, QUANTITY, UNIT FROM RECIPIEINGREDIENT INNER JOIN INGREDIENT ON RECIPIEINGREDIENT.INGREDIENTID = INGREDIENT.INGREDIENTID WHERE RECIPIEINGREDIENT.RECIPIEID =: RecipieId", conn);
        // Add the EmployeeID parameter
        comm.Parameters.Add("RecipieId", OracleDbType.Int32);
        comm.Parameters["RecipieId"].Value = recipieId;
        // Enclose database code in Try-Catch-Finally
        try
        {
            // Open the connection
            conn.Open();
            // Execute the command
            reader = comm.ExecuteReader();
            // Fill the grid with data
            ingredientGrid.DataSourceID = null;
            ingredientGrid.DataSource = reader;
            ingredientGrid.DataKeyNames = new string[] { "IngredientID" };
            ingredientGrid.DataBind();
            // Close the reader
            reader.Close();
        }
        finally
        {
            // Close the connection
            conn.Close();
        }

        comm = new OracleCommand(
         "SELECT USERNAME, COMMENTS from RECIPIECOMMENT where RECIPIEID =: RecipieId", conn);
        // Add the EmployeeID parameter
        comm.Parameters.Add("RecipieId", OracleDbType.Int32);
        comm.Parameters["RecipieId"].Value = recipieId;
        // Enclose database code in Try-Catch-Finally
        try
        {
            // Open the connection
            conn.Open();
            // Execute the command
            reader = comm.ExecuteReader();
            // Fill the grid with data
            if (reader.HasRows)
            {
                
                userComments.DataSourceID = null;
                userComments.DataSource = reader;
                //userComments.DataKeyNames = new string[] { "IngredientID" };
                userComments.DataBind();
                // Close the reader
                reader.Close();
            }
            else
            {
              
                reader.Close();
                conn.Close();
            }
        }catch(Exception ex)
        {
            Label2.Text = ex.ToString();
        }
        finally
        {
            // Close the connection
            conn.Close();
        }

    }
    //protected void OnRatingChanged(object sender, RatingEventArgs e)
    //{
    //    int rating = Rating1.CurrentRating;
    //    Label1.Text = rating.ToString();
    //    //    int recipieId = Convert.ToInt32((String)Session["selectedRowIndex"]);
    //    //    string connectionString =
    //    //        ConfigurationManager.ConnectionStrings[
    //    //        "RecipieConnection"].ConnectionString;
    //    //    using (OracleConnection con = new OracleConnection(connectionString))
    //    //    {
    //    //        using (OracleCommand cmd = new OracleCommand("INSERT INTO RECIPIERATING(RecipieID,Rating,Comments) VALUES ('" + recipieId + "','" + e.Value + "','Comments are showwn Here')"))
    //    //        {
    //    //            using (OracleDataAdapter sda = new OracleDataAdapter())
    //    //            {
    //    //                con.Open();
    //    //                cmd.ExecuteNonQuery();
    //    //                con.Close();
    //    //            }
    //    //        }
    //    //    }
    //    //  //  Response.Redirect(Request.Url.AbsoluteUri);
    //}
    protected void grid_RowDeleting(object sender, EventArgs e)
    {
        string currentuser = Session["username"].ToString();
        int recipieId = Convert.ToInt32((String)Session["selectedRowIndex"]);
        OracleConnection conn;
        OracleCommand comm;
        OracleDataReader reader;
        string connectionString =
           ConfigurationManager.ConnectionStrings["RecipieConnection"].ConnectionString;
        conn = new OracleConnection(connectionString);

        comm = new OracleCommand(
           "Select SUBMITTEDBY from RECIPIEDETAILS where RECIPIEID =: recipieid", conn);
        comm.Parameters.Add("usrname", OracleDbType.Int32);
        comm.Parameters["usrname"].Value = recipieId;
        conn.Open();
        reader = comm.ExecuteReader();
        if (reader.Read())
        {
            string subby = reader["SUBMITTEDBY"].ToString();
            if (subby == currentuser)
            {
                comm = new OracleCommand(
                  "Delete FROM RECIPIECOMMENT WHERE RECIPIEID=:RecipieId", conn);
                comm.Parameters.Add("RecipieId", OracleDbType.Int32);
                comm.Parameters["RecipieId"].Value = recipieId;
                try
                {
                    
                    // Execute the command
                    reader = comm.ExecuteReader();
                    reader.Close();
                }
                catch (Exception ex)
                {

                    Label2.Text = ex.ToString();
                }
                finally
                {
                    conn.Close();
                }

                comm = new OracleCommand(
                   "Delete FROM RECIPIEINGREDIENT WHERE RECIPIEID=:RecipieId", conn);
                comm.Parameters.Add("RecipieId", OracleDbType.Int32);
                comm.Parameters["RecipieId"].Value = recipieId;
                try
                {
                    conn.Open();
                    // Execute the command
                    reader = comm.ExecuteReader();
                    reader.Close();
                }
                catch (Exception ex)
                {

                    Label2.Text = ex.ToString();
                }
                finally
                {
                    conn.Close();
                }
                comm = new OracleCommand(
                    "Delete FROM RECIPIEDETAILS WHERE RECIPIEID =:Id ", conn);
                comm.Parameters.Add("Id", OracleDbType.Int32);
                comm.Parameters["Id"].Value = recipieId;
                //Open the connection
                //Enclose database code in Try - Catch - Finally
                try
                {
                    conn.Open();
                    // Open the connection
                    // Execute the command
                    reader = comm.ExecuteReader();
                    // Fill the grid with data
                    recipieDetails.DataSourceID = null;
                    recipieDetails.DataSource = reader;
                    recipieDetails.DataKeyNames = new string[] { "RecipieID" };
                    recipieDetails.DataBind();
                    // Close the reader
                    reader.Close();
                }
                catch (Exception exe)
                {
                    Label2.Text = exe.ToString();
                }
                finally
                {
                    // Close the connection
                    conn.Close();
                    Response.Redirect("Recipes.aspx");
                }
            }
            else
            {
                Label1.ForeColor = System.Drawing.Color.Red;
                Label1.Text = "You Cannot Delete Others Recipie";
            }
        }
        else
        {
          
            conn.Close();
        }
    }


    protected void grid_RowChanging(object sender, EventArgs e)
    {
        
        string currentuser = Session["username"].ToString();
        int recipieId = Convert.ToInt32((String)Session["selectedRowIndex"]);
        OracleConnection conn;
        OracleCommand comm;
        OracleDataReader reader;
        string connectionString =
           ConfigurationManager.ConnectionStrings["RecipieConnection"].ConnectionString;
        conn = new OracleConnection(connectionString);
        try
        {
            comm = new OracleCommand(
               "Select SUBMITTEDBY from RECIPIEDETAILS where RECIPIEID =: recipieid", conn);
            comm.Parameters.Add("usrname", OracleDbType.Int32);
            comm.Parameters["usrname"].Value = recipieId;
            conn.Open();
            reader = comm.ExecuteReader();
            if (reader.Read())
            {
                string subby = reader["SUBMITTEDBY"].ToString();
                if (subby == currentuser)
                {
                    Response.Redirect("UpdateRecipie.aspx");
                }
                else
                {
                    Label1.ForeColor = System.Drawing.Color.Red;
                    Label1.Text = "You Cannot Edit Others Recipie";
                }
            }
        }
        catch(Exception ex)
        {
            Label2.Text = ex.ToString();
        }
        conn.Close();
    }


    protected void subCommentBtn_Click(object sender, EventArgs e)
    {
        string currentuser = Session["username"].ToString();
        string commentuser = commentusername.Text;
        int recipieId = Convert.ToInt32((String)Session["selectedRowIndex"]);
      
        OracleConnection conn;
        OracleCommand comm;
        OracleDataReader reader;
        string connectionString =
           ConfigurationManager.ConnectionStrings["RecipieConnection"].ConnectionString;
        conn = new OracleConnection(connectionString);

        comm = new OracleCommand(
              "Select SUBMITTEDBY from RECIPIEDETAILS where RECIPIEID =: recipieid", conn);
        comm.Parameters.Add("usrname", OracleDbType.Int32);
        comm.Parameters["usrname"].Value = recipieId;
        conn.Open();
        reader = comm.ExecuteReader();
        if (reader.Read())
        {
            string subby = reader["SUBMITTEDBY"].ToString();


            comm = new OracleCommand(
               "Select * from RECIPIEUSERS where username =: usrname", conn);
            comm.Parameters.Add("usrname", OracleDbType.Varchar2);
            comm.Parameters["usrname"].Value = commentuser;
   
            reader = comm.ExecuteReader();
            try
            {
                if (commentuser == subby)
                {
                    ErrorMessage.Text = "You Cannot Comment Your Own Recipie";
                }
                else
                {
                    if (recipiecomment.Text == null)
                    {
                        ErrorMessage.Text = "Please Insert Comment";
                    }
                    else
                    {
                        comm = new OracleCommand("Insert Into RECIPIECOMMENT(RECIPIEID, COMMENTS, USERNAME) Values ('" + recipieId + "','" + recipiecomment.Text + "', '" + commentuser.ToString() + "')", conn);
                        comm.ExecuteNonQuery();
                        ErrorMessage.Text = "Commented";
                        Response.Redirect(Request.RawUrl);

                    }
                }
            }
            catch (Exception ex)
            {
                Label2.Text = ex.ToString();
            }
            finally
            {
                // Close the connection
                conn.Close();
            }
        }     
       }
               

}