using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
//using Oracle.DataAccess.Client;
using Oracle.ManagedDataAccess.Client;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public partial class Pages_AddRecipes : System.Web.UI.Page
{
    public int ingredientId, quantityId, unitId,categoryId;
    public string category;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["username"] == null)
            Response.Redirect("Login.aspx");
        if (!IsPostBack)
        {
            subby.Text = Session["username"].ToString();
            OracleConnection conn;
            OracleCommand comm;
            OracleDataReader reader;
            string connectionString =
            ConfigurationManager.ConnectionStrings[
            "RecipieConnection"].ConnectionString;
            conn = new OracleConnection(connectionString);
            comm = new OracleCommand("SELECT * FROM RECIPIECATEGORY", conn);
            conn.Open();
            reader = comm.ExecuteReader();
            categoryText.DataSource = reader;
            categoryText.DataValueField = "CATEGORY";
            categoryText.DataBind();
            conn.Close();
            categoryText.Items.Add(new ListItem("Add New..", "-1"));
            categoryText.Items.FindByValue("-1").Attributes.Add("style", "font-weight:bolder");
                
        }
    }


    protected void Page_PreInit(object sender, EventArgs e)
    {
        HttpCookie myCookies = Request.Cookies["theme"];
        if (myCookies != null)
            Page.Theme = myCookies.Value;
    }

//    private void button1_Click(object sender, System.EventArgs e)
//    {
//        SerializeAndSave();
//    }
//    private void button2_Click(object sender, System.EventArgs e)
//    {
//        RetrieveAndDeserialize();
//    }
//    public void SerializeAndSave()
//    {
//        try
//        {
//            // instantiate a MemoryStream and a new instance of our class          
//            MemoryStream ms = new MemoryStream();
//            ClassToSerialize c = new ClassToSerialize(txtName.Text);
//            // create a new BinaryFormatter instance 
//            BinaryFormatter b = new BinaryFormatter();
//            // serialize the class into the MemoryStream 
//            b.Serialize(ms, c);
//            ms.Seek(0, 0);
//            // Show the information 
//            textBox1.Text = "Ms Length: " + ms.Length.ToString();
//            int res = SaveToDB(txtName.Text, ms.ToArray());
//            textBox1.Text += "\nDB RetVal: " + res.ToString() + "\n";
//            //Clean up 
//            ms.Close();
//        }
//        catch (Exception ex)
//        {
//            textBox1.Text = ex.Message;
//        }
//    }
//    public void RetrieveAndDeserialize()
//    {
//        MemoryStream ms2 = new MemoryStream();
//        byte[] buf = RetrieveFromDB(txtName.Text);
//        ms2.Write(buf, 0, buf.Length);
//        ms2.Seek(0, 0);
//        BinaryFormatter b = new BinaryFormatter();
//        ClassToSerialize c = (ClassToSerialize)b.Deserialize(ms2);
//        textBox1.Text += "Deserialized Name: " + c.name + "\n";
//        textBox1.Text += "Portion of Deserialized float array: \n";
//        for (int j = 0; j < 100; j++)
//        {
//            textBox1.Text += c.fltArray[j].ToString() + "\n";
//        }
//        ms2.Close();
//    }
//    private int SaveToDB(string imgName, byte[] imgbin)
//     {
//        SqlConnection connection = new SqlConnection("Server=(local);DataBase=Northwind;User Id=sa;Password=;");

//        SqlCommand command = new SqlCommand("INSERT INTO Employees (firstname,lastname,photo) VALUES(@img_name, @img_name, @img_data)", connection ); 
//        // (need to write something to first and lastname columns 
//        // because of constraints) 
//       SqlParameter param0 = new SqlParameter("@img_name", SqlDbType.VarChar, 50);
//        param0.Value = imgName;
//        command.Parameters.Add(param0);
//        SqlParameter param1 = new SqlParameter("@img_data", SqlDbType.Image);
//        param1.Value = imgbin;
//        command.Parameters.Add(param1);
//        connection.Open();
//        int numRowsAffected = command.ExecuteNonQuery();
//        connection.Close();
//        return numRowsAffected;
//    }
//    private byte[] RetrieveFromDB(string lastname)
//    {
//        SqlConnection connection = new SqlConnection("Server=(local);DataBase=Northwind; User Id=sa;Password=;");
//        SqlCommand command = new SqlCommand("select top 1 Photo from Employees where lastname = '"+lastname +"'", connection ); 
  
//        connection.Open();
//        SqlDataReader dr = command.ExecuteReader();
//        dr.Read();
//        byte[] imgData = (byte[])dr["Photo"];
//        connection.Close();
//        return imgData;
//    }
//}// end class 
//[Serializable]
//public class ClassToSerialize
//{
//    public string name;
//    public float[] fltArray;
//    // constructor initializes name and creates the sample array of floats 
//    public ClassToSerialize(string theName)
//    {
//        this.name = theName;
//        float[] theArray = new float[1000];
//        Random rnd = new System.Random();
//        for (int i = 0; i < 1000; i++)
//            theArray[i] = (float)rnd.NextDouble() * 1000;
//        fltArray = theArray;
//    }
//} 
//}

    protected void Savebtn_Click(object sender, EventArgs e)

    {
        
        try
        {
        OracleConnection conn;
        OracleCommand comm;
        OracleDataReader reader;
        string connectionString = ConfigurationManager.ConnectionStrings["RecipieConnection"].ConnectionString;
        conn = new OracleConnection(connectionString);


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
                    categoryId = Convert.ToInt32(reader["categoryId"]);
                }
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
                    categoryId = Convert.ToInt32(reader["categoryId"]);
                }
          }

            string query = "insert into RECIPIEDETAILS (RECIPIENAME, SUBMITTEDBY, CATEGORYID, PREPERATIONTIME, NUMBEROFSERVINGS, DESCRIPTION) values ('" + rname.Text + "','" + subby.Text + "','"+ categoryId + "','" + cookinTime.Text + "','" + Convert.ToInt32(numserv.Text) + "','" + recipedesc.Text + "') RETURNING RECIPIEID INTO :recipieid ";

            comm = new OracleCommand(query, conn);
            OracleParameter outputParameter = new OracleParameter("recipieid", OracleDbType.Int32);
            outputParameter.Direction = ParameterDirection.Output;
            comm.Parameters.Add(outputParameter);
            comm.ExecuteNonQuery();
            //Label1.Text = outputParameter.Value.ToString();
            string output = outputParameter.Value.ToString();
            int recipieId = Convert.ToInt32(output);

            foreach (Ingridient ingredient in IngredientRepository.ingredients)
            {
                comm = new OracleCommand("select * from INGREDIENT where INGREDIENTNAME= :ingredientName", conn);
                comm.Parameters.Add("ingredientName", OracleDbType.Varchar2);
                comm.Parameters["ingredientName"].Value = ingredient.IngredientName;
                reader = comm.ExecuteReader();
                if (!reader.Read())
                {
                    reader.Close();
                    comm = new OracleCommand("insert into INGREDIENT (INGREDIENTNAME) values ('"+ingredient.IngredientName+ "') RETURNING INGREDIENTID INTO :ingredid ", conn);
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
                    comm = new OracleCommand("insert into RECIPIEINGREDIENT (RECIPIEID,INGREDIENTID,QUANTITY,UNIT) values ('"+recipieId+"','"+ingredientId+"','"+ingredient.Quantity+"','"+ingredient.UnitofMeasures+"')", conn);
                    comm.ExecuteNonQuery();
                }
                else
                {
                    reader.Close();
                }
                lblResult.Text = "Recipie Added Sucessfully..";
            }

            //    List<Ingridient> ingredientList = new List<Ingridient>();
            //if (Session["ingredient"] != null)
            //{
            //    ingredientList = (List<Ingridient>)Session["ingredient"];
            //    foreach (Ingridient ingredients in ingredientList)
            //    {
            //        string queryIngredient = "insert into INGREDIENT(INGREDIENTNAME) value ('ingred') RETURNING INGREDIENTID INTO :ingredientid ";
            //        comm = new OracleCommand(queryIngredient, conn);
            //        OracleParameter outputParameterIngredient = new OracleParameter("ingredientid", OracleDbType.Int32);
            //        outputParameterIngredient.Direction = ParameterDirection.Output;
            //        comm.Parameters.Add(outputParameterIngredient);
            //        comm.ExecuteNonQuery();
            //        string outputter = outputParameterIngredient.Value.ToString();
            //        ingredientId = Convert.ToInt32(outputter);
            //        Label1.Text = ingredientId.ToString();
            //        string query2 = "insert into RECIPIEINGREDIENT(RECIPIEID,INGREDIENTID,QUANTITY,UNIT) value ('" + recipieId + "','" + ingredientId + "','" + Convert.ToInt32(ingredients.Quantity) + "','" + ingredients.UnitofMeasures + "') ";
            //        comm = new OracleCommand(query2, conn);
            //        comm.ExecuteNonQuery();

            //    }
            //}
            //else
            //{
            //    Label1.Text = "Null";
            //}
            //conn.Close();

        }
        catch (Exception ex)
        {
            lblResult.Text =ex.ToString();
        }
    }

   

    //protected void SaveAll_Click(object sender, EventArgs e)
    //{
    //    SqlConnection conn;
    //    SqlCommand comm;
    //    string connectionString = ConfigurationManager.ConnectionStrings["Recipie"].ConnectionString;
    //    conn = new SqlConnection(connectionString);

    //    //conn.Open();
    //    ////string query2 = "update RecipeTable set Status='0'";
    //    //comm = new SqlCommand(query2, conn);
    //    //comm.ExecuteNonQuery();
    //    //conn.Close();

    //    Response.Redirect("RecipieDetail.aspx");
    //}
    protected void CancelBtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddRecipes.aspx");
    }

    //protected void btnAddIngredient_Click(object sender, EventArgs e)
    //{
    //    SqlConnection conn;
    //    SqlCommand comm, comm1;
    //    SqlDataReader reader1, reader;
    //    string connectionString =
    //            ConfigurationManager.ConnectionStrings[
    //            "Recipie"].ConnectionString;
    //    conn = new SqlConnection(connectionString);
    //    conn.Open();

    //    comm = new SqlCommand("select * from IngredientTable where (ingredientsName= '" + IngredientControl.TextIngredint + "')", conn);
    //    reader1 = comm.ExecuteReader();

    //    if (reader1.HasRows)
    //    {
    //        reader1.Read();
    //        ingredientId = Convert.ToInt32(reader1["ingredientId"]);
    //        conn.Close();
    //    }
    //    else
    //    {
    //        comm = new SqlCommand("insert into IngredientTable (ingredientsName) values ('" + IngredientControl.TextIngredint + "')", conn);
    //        comm.ExecuteNonQuery();
    //        conn.Close();
    //        conn.Open();
    //        comm1 = new SqlCommand("select ingredientId from IngredientTable where ingredientsName = :ingredient", conn);
    //        comm1.Parameters.Add("ingredient", SqlDbType.VarChar);
    //        comm1.Parameters["ingredient"].Value = IngredientControl.TextIngredint.ToString();
    //        reader = comm1.ExecuteReader();
    //        reader.Read();
    //        ingredientId = Convert.ToInt32(reader["ingredientId"]);
    //        conn.Close();
    //    }
    //    conn.Open();

    //    comm1 = new SqlCommand("select * from recipedetails where (" +
    //    "recipename = '" + recipenametxt.Text + "' and preparationtime = '" + preptimetxt.Text + "' and numberofservings = '" + nostxt.Text + "' and description = '" + desctxt.Text + "' and submittedby ='" + submittedbytxt.Text + "')", conn);

    //    reader = comm1.ExecuteReader();
    //    if (reader.HasRows)
    //    {
    //        reader.Read();
    //        recipeID = Convert.ToInt32(reader["recipeid"]);
    //    }

    //    comm = new OracleCommand("insert into recipeingredient (recipeid,ingredientid,quantity,unit) values (" + recipeID + "," + ingredientID + ",'" + WebUserControl.Text2 + "','" + WebUserControl.Text3 + "')", conn);
    //    comm.ExecuteNonQuery();

    //    comm = new OracleCommand("SELECT * FROM COMP214F16_001_P_33.RECIPEINGREDIENT INNER JOIN COMP214F16_001_P_33.INGREDIENT ON COMP214F16_001_P_33.RECIPEINGREDIENT.INGREDIENTID = COMP214F16_001_P_33.INGREDIENT.INGREDIENTID WHERE (COMP214F16_001_P_33.RECIPEINGREDIENT.RECIPEID = " + recipeID + ")", conn);
    //    reader1 = comm.ExecuteReader();
    //    GridView1.DataSource = reader1;
    //    GridView1.DataBind();
    //    conn.Close();
    //    editingredient.Enabled = true;
    //}

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
            try
            {
                OracleConnection conn;
                OracleCommand comm;
                OracleDataReader reader;
                string connectionString = ConfigurationManager.ConnectionStrings["RecipieConnection"].ConnectionString;
                conn = new OracleConnection(connectionString);
                comm = new OracleCommand("select * from RECIPIECATEGORY where CATEGORY = :category", conn);
                comm.Parameters.Add("category", OracleDbType.Varchar2);
                comm.Parameters["category"].Value = addCategoryText.Text;
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
        }
        else
        {
            errorlbl.Visible = false;
            addCategorylbl.Visible = false;
            addCategoryText.Visible = false;
        }
    }


    //protected void Add_Click(object sender, EventArgs e)
    //{
    //    OracleConnection conn;
    //    OracleCommand comm;
    //    OracleDataReader reader;
    //    string connectionString = ConfigurationManager.ConnectionStrings["RecipieConnection"].ConnectionString;
    //    conn = new OracleConnection(connectionString);
    //    //errorlbl.Visible =false;
    //    comm = new OracleCommand("insert into RECIPIECATEGORY(CATEGORY) values ('" + addCategoryText.Text + "')", conn);
    //    errorlbl.Text = "Solved";
    //    //categoryText.Items.FindByText(addCategoryText.Text).Selected = true;
    //    //addCategoryText.Text = string.Empty;
    //}
}
