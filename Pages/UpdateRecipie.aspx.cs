using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_UpdateRecipie : System.Web.UI.Page
{
  public  int categoryid,ingredientId;
  private int count = 0;
  private string[] myIngredients = new String[15];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["username"] == null)
            Response.Redirect("Login.aspx");

        if (!IsPostBack)
        {
            try
        {
            
            OracleConnection connection;
            OracleCommand command;
            OracleDataReader rdr;
            string connString =
            ConfigurationManager.ConnectionStrings[
            "RecipieConnection"].ConnectionString;
            connection = new OracleConnection(connString);
            command = new OracleCommand("SELECT * FROM RECIPIECATEGORY", connection);
            connection.Open();
            rdr = command.ExecuteReader();
            categoryText.DataSource = rdr;
            categoryText.DataValueField = "CATEGORY";
            categoryText.DataBind();
            connection.Close();
            categoryText.Items.Add(new ListItem("Add New..", "-1"));
            categoryText.Items.FindByValue("-1").Attributes.Add("style", "font-weight:bolder");
            


            }
        catch (Exception ex)
        {
            Label1.Text = ex.ToString();
        }

        int recipieId = Convert.ToInt32((String)Session["selectedRowIndex"]);

        OracleConnection conn;
        OracleCommand comm;
        OracleDataReader reader;
        string connectionString =
            ConfigurationManager.ConnectionStrings["RecipieConnection"].ConnectionString;
        conn = new OracleConnection(connectionString);
        try
        {
            conn.Open();
            comm = new OracleCommand("SELECT RECIPIENAME, SUBMITTEDBY, CATEGORYID, PREPERATIONTIME, NUMBEROFSERVINGS, DESCRIPTION FROM RECIPIEDETAILS WHERE RECIPIEID =: RecipieId", conn);
            comm.Parameters.Add("RecipieId", OracleDbType.Int32);
            comm.Parameters["RecipieId"].Value = recipieId;
            reader = comm.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                rname.Text = reader["RECIPIENAME"].ToString();
                subby.Text = reader["SUBMITTEDBY"].ToString();
                cookinTime.Text = reader["PREPERATIONTIME"].ToString();
                numserv.Text = reader["NUMBEROFSERVINGS"].ToString();
                recipedesc.Text = reader["DESCRIPTION"].ToString();
                categoryid = Convert.ToInt32(reader["CATEGORYID"]);
            }

            comm = new OracleCommand("select CATEGORY from RECIPIECATEGORY where CATEGORYID = :categoryid", conn);
            comm.Parameters.Add("categoryid", OracleDbType.Int32);
            comm.Parameters["categoryid"].Value = categoryid;
            reader = comm.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    categoryText.Items.FindByValue(reader["CATEGORY"].ToString()).Selected = true;
                    //categoryText.DataBind();
                }
                    conn.Close();
                //Label1.Text = reader["CATEGORY"].ToString();
            }
                string ingredientquery = "SELECT INGREDIENT.INGREDIENTNAME, RECIPIEINGREDIENT.QUANTITY, RECIPIEINGREDIENT.UNIT FROM RECIPIEINGREDIENT INNER JOIN INGREDIENT ON INGREDIENT.INGREDIENTID = RECIPIEINGREDIENT.INGREDIENTID WHERE RECIPIEINGREDIENT.RECIPIEID =: recipieid";
                comm = new OracleCommand(ingredientquery, conn);
                comm.Parameters.Add("recipieid", OracleDbType.Int32);
                comm.Parameters["recipieid"].Value = recipieId;
                conn.Open();
                reader = comm.ExecuteReader();
            
                if (reader.HasRows)
                {
                        grd.DataSource = reader;
                        grd.DataBind();
                }
                conn.Close();
            }
        catch (Exception ex)
        {
                Label1.Text = ex.ToString();
            
        }
            finally
            {
                conn.Close();
            }
    }
    }

    protected void updateButton_Click(object sender, EventArgs e)
    {
        int recipieId = Convert.ToInt32((String)Session["selectedRowIndex"]);

        OracleConnection conn;
        OracleCommand comm;
        OracleDataReader reader;
        string connectionString =
            ConfigurationManager.ConnectionStrings["RecipieConnection"].ConnectionString;
        conn = new OracleConnection(connectionString);

      
        try
        {
            if (!categoryText.SelectedItem.ToString().Equals("Add New.."))
            {
                errorlbl.Visible = false;
                comm = new OracleCommand("select CATEGORYID from RECIPIECATEGORY where CATEGORY = :category", conn);
                comm.Parameters.Add("category", OracleDbType.Varchar2);
                comm.Parameters["category"].Value = categoryText.SelectedItem.ToString();
                conn.Open();
                reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    categoryid = Convert.ToInt32(reader["categoryId"]);
                }
                conn.Close();
            }
            else
            {
                comm = new OracleCommand("select CATEGORYID from RECIPIECATEGORY where CATEGORY = :category", conn);
                comm.Parameters.Add("category", OracleDbType.Varchar2);
                comm.Parameters["category"].Value = addCategoryText.Text;
                conn.Open();
                reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    categoryid = Convert.ToInt32(reader["categoryId"]);
                }
                conn.Close();
            }

            conn.Open();
            comm = new OracleCommand("UPDATE RECIPIEDETAILS SET RECIPIENAME ='" + rname.Text + "', CATEGORYID ='" + categoryid + "', PREPERATIONTIME ='" + cookinTime.Text.ToString() + "', NUMBEROFSERVINGS='" + numserv.Text.ToString() + "', SUBMITTEDBY ='" + subby.Text + "', DESCRIPTION='" + recipedesc.Text + "' WHERE RECIPIEID ='" + recipieId + "'", conn);
            comm.ExecuteNonQuery();

            comm = new OracleCommand("Delete from RECIPIEINGREDIENT  where RECIPIEID = '" + recipieId + "'", conn);
            comm.ExecuteNonQuery();
            btnAddRow_Click(null, null);
            //update ingredient
            foreach (Ingridient ingredient in IngredientRepository.ingredients)
            {
                comm = new OracleCommand("select * from INGREDIENT where INGREDIENTNAME= :ingredientName", conn);
                comm.Parameters.Add("ingredientName", OracleDbType.Varchar2);
                comm.Parameters["ingredientName"].Value = ingredient.IngredientName;
              
                reader = comm.ExecuteReader();
                if (!reader.Read())
                {
                    reader.Close();
                    comm = new OracleCommand("insert into INGREDIENT (INGREDIENTNAME) values ('" + ingredient.IngredientName + "') RETURNING INGREDIENTID INTO :ingredid ", conn);
                    OracleParameter outputerParameter = new OracleParameter("ingredid", OracleDbType.Int32);
                    outputerParameter.Direction = ParameterDirection.Output;
                    comm.Parameters.Add(outputerParameter);
                    comm.ExecuteNonQuery();
                    //Label1.Text = outputParameter.Value.ToString();
                    string outputer = outputerParameter.Value.ToString();
                    ingredientId = Convert.ToInt32(outputer);

                }
                else
                {
                    comm = new OracleCommand("select INGREDIENTID from INGREDIENT where INGREDIENTNAME=:ingredientName", conn);
                    comm.Parameters.Add("ingredientName", OracleDbType.Varchar2);
                    comm.Parameters["ingredientName"].Value = ingredient.IngredientName;
                    reader = comm.ExecuteReader();
                    reader.Read();
                    ingredientId = Convert.ToInt32(reader["ingredientId"]);
                    reader.Close();
                }


                comm = new OracleCommand("select * from RECIPIEINGREDIENT where RECIPIEID=:recipeId and INGREDIENTID=:ingredientId ", conn);
                comm.Parameters.Add("recipeId", OracleDbType.Varchar2);
                comm.Parameters["recipeId"].Value = recipieId;
                comm.Parameters.Add("ingredientId", OracleDbType.Varchar2);
                comm.Parameters["ingredientId"].Value = ingredientId;
                reader = comm.ExecuteReader();

                if (!reader.Read())
                {
                    
                    reader.Close();
                    comm = new OracleCommand("insert into RECIPIEINGREDIENT (RECIPIEID,INGREDIENTID,QUANTITY,UNIT) values ('" + recipieId + "','" + ingredientId + "','" + ingredient.Quantity + "','" + ingredient.UnitofMeasures + "')", conn);
                    comm.ExecuteNonQuery();

                }
                else
                {
                    reader.Close();
                 

                    comm = new OracleCommand("update RECIPIEINGREDIENT set QUANTITY = '" + ingredient.Quantity + "',UNIT = '" + ingredient.UnitofMeasures + "' where RECIPIEID = '" + recipieId + "' and INGREDIENTID = '" + ingredientId + "' ", conn);
                    comm.ExecuteNonQuery();
                }
              
            }
        }
        catch (Exception ex)
        {
            //Label1.Text = categoryid.ToString();
            Label1.Text = ex.ToString();
        }
        finally
        {
            conn.Close();
            Response.Redirect("Recipes.aspx");
        }
    }
    protected void categoryTxt_TextChanged(object sender, EventArgs e)
    {
        categoryText_SelectedIndexChanged(null, null);
    }

    protected void categoryText_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (categoryText.SelectedItem.ToString().Equals("Add New.."))
        {
            // errorlbl.Text = "Clicked";
            //errorlbl.Visible = false;
            addCategoryText.Visible = true;
            addCategorylbl.Visible = true;
           
                OracleConnection conn;
                OracleCommand comm;
                OracleDataReader reader;
                string connectionString = ConfigurationManager.ConnectionStrings["RecipieConnection"].ConnectionString;
                conn = new OracleConnection(connectionString);
                comm = new OracleCommand("select * from RECIPIECATEGORY where CATEGORY = :category", conn);
                comm.Parameters.Add("category", OracleDbType.Varchar2);
                comm.Parameters["category"].Value = addCategoryText.Text;
              try
               {
                conn.Open();
                reader = comm.ExecuteReader();
                if (!reader.Read())
                {
                    //AddBtn.Visible = true;
                    errorlbl.Visible = false;
                    comm = new OracleCommand("insert into RECIPIECATEGORY(CATEGORY) values ('" + addCategoryText.Text + "')", conn);
                    comm.ExecuteNonQuery();
                    //errorlbl.Text = "Solved";
                    categoryText.DataBind();
                }
                else
                {

                    errorlbl.Visible = true;
                    errorlbl.Text = "Category Already Exist..";
                }

            }
            catch (Exception ex)
            {
                errorlbl.Text = ex.ToString();
            }
            finally
            {
                conn.Close();
            }
        }
        else
        {
            errorlbl.Visible = false;
            addCategorylbl.Visible = false;
            addCategoryText.Visible = false;
        }
    }

    protected void btnAddRow_Click(object sender, EventArgs e)
    {
        DataTable table = GetTabelWithNoData();
        DataRow row;
        IngredientRepository.ingredients.Clear();
        foreach (GridViewRow viewRow in grd.Rows)
        {
            row = table.NewRow();
            TextBox txtIngredient = viewRow.FindControl("txtIngredient") as TextBox;
            TextBox txtUnit = viewRow.FindControl("txtUnit") as TextBox;
            TextBox txtQuantity = viewRow.FindControl("txtQuantity") as TextBox;

            if (count < 15 && Array.IndexOf(myIngredients, txtIngredient.Text) < 0)
            {
                try
                {
                    row[0] = txtIngredient.Text;
                    row[1] = txtUnit.Text;
                    row[2] = txtQuantity.Text;
                    IngredientRepository.ingredients.Add(new Ingridient(txtIngredient.Text, Convert.ToInt32(txtQuantity.Text), txtUnit.Text));

                    myIngredients[count++] = txtIngredient.Text;
                    table.Rows.Add(row);
                }
                catch (Exception ex)
                {
                    Label1.Text = ex.ToString();
                }
            }
        }
        if (count < 15)
        {
            row = table.NewRow();
            table.Rows.Add(row);
        }
        grd.DataSource = table;
        grd.DataBind();
    }

    public DataTable GetTabelWithNoData()
    {
        DataTable table = new DataTable();
        table.Columns.Add("IngredientName", typeof(string));
        table.Columns.Add("Unit", typeof(string));
        table.Columns.Add("Quantity", typeof(string));
        return table;
    }
    protected void grid_DeletingButtonPressed(object sender, GridViewDeleteEventArgs e)
    {
        int recipieId = Convert.ToInt32((String)Session["selectedRowIndex"]);

        TextBox tempLabel = (TextBox)grd.Rows[e.RowIndex].FindControl("txtIngredient");
        string ingredName = tempLabel.Text;
        OracleConnection conn;
        OracleCommand comm;
        OracleDataReader reader;
        string connectionString = ConfigurationManager.ConnectionStrings["RecipieConnection"].ConnectionString;
        conn = new OracleConnection(connectionString);
        try
        {
            comm = new OracleCommand("select * from Ingredient where INGREDIENTNAME = :ingredName", conn);
            comm.Parameters.Add("ingredName", OracleDbType.Varchar2);
            comm.Parameters["ingredName"].Value = ingredName;
            conn.Open();
            reader = comm.ExecuteReader();
            if (reader.Read())
                ingredientId = Convert.ToInt32(reader["INGREDIENTID"]);
            conn.Close();
            //Label2.Text = Convert.ToString(ingredientId);
            //Label1.Text = e.RowIndex.ToString();
           
            comm = new OracleCommand("select * from RecipieIngredient where INGREDIENTID = '" + ingredientId + "' and RECIPIEID = '" + recipieId + "' ", conn);
            conn.Open();
            reader = comm.ExecuteReader();
            if (reader.Read())
            {


                comm = new OracleCommand("DELETE from RECIPIEINGREDIENT where INGREDIENTID = '" + ingredientId + "' and RECIPIEID = '" + recipieId + "' ", conn);
                try
                {
                    conn.Open();
                    // Execute the command
                    comm.ExecuteNonQuery();
                    string ingredientquery = "SELECT INGREDIENT.INGREDIENTNAME, RECIPIEINGREDIENT.QUANTITY, RECIPIEINGREDIENT.UNIT FROM RECIPIEINGREDIENT INNER JOIN INGREDIENT ON INGREDIENT.INGREDIENTID = RECIPIEINGREDIENT.INGREDIENTID WHERE RECIPIEINGREDIENT.RECIPIEID =: recipieid";
                    comm = new OracleCommand(ingredientquery, conn);
                    comm.Parameters.Add("recipieid", OracleDbType.Int32);
                    comm.Parameters["recipieid"].Value = recipieId;
                    reader = comm.ExecuteReader();

                    if (reader.HasRows)
                    {
                        grd.DataSource = reader;
                        grd.DataBind();
                    }
                    else
                    {
                        grd.DataBind();
                    }
                    btnAddRow_Click(null, null);
                    conn.Close();
                    //grd.DataBind();


                }
                catch (Exception ex)
                {
                    Label1.Text = ex.ToString();
                }
                finally
                {
                    conn.Close();
                }
            }
            else
            {
                conn.Close();
                Label1.Text = "Please remove The Name Of Ingredient, Quantity and Unit To delete it";
            }
                //grd.DeleteRow(e.RowIndex);
            }
        catch (Exception ex)
        {
            Label1.Text = ex.ToString();
        }
        finally
        {
            //conn.Close();
        }
        //comm = new OracleCommand("DELETE from RECIPIEINGREDIENT where INGREDIENTID = '"+ ingredName + "' and RECIPIEID '"+ recipieId + "' ", conn);
        //grd.DeleteRow(grd.SelectedIndex);
       
    }
}