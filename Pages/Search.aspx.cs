
using Oracle.ManagedDataAccess.Client;
using System;
using System.Configuration;
using System.Web;
using System.Web.UI;

public partial class Pages_Search : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["username"] == null)
            Response.Redirect("Login.aspx");

        if (!IsPostBack) {
            OracleConnection conn;
            OracleCommand comm1, comm2, comm3;
    OracleDataReader reader1, reader2, reader3;
    string connectionString =
    ConfigurationManager.ConnectionStrings[
    "RecipieConnection"].ConnectionString;
    conn = new OracleConnection(connectionString);
    comm1 = new OracleCommand("SELECT SUBMITTEDBY FROM RECIPIEDETAILS ", conn);
    conn.Open();
            reader1 = comm1.ExecuteReader();
            submitDropDown.DataSource = reader1;
            //submitDropDown.DataTextField = "SUBMITTEDBY";
            //submitDropDown.DataValueField = "SUBMITTEDBY";
            
            submitDropDown.DataBind();
            conn.Close();
            conn.Open();

            comm2 = new OracleCommand("SELECT CATEGORY FROM RECIPIECATEGORY", conn);

    reader2 = comm2.ExecuteReader();
            categoryDropDown.DataSource = reader2;
            categoryDropDown.DataBind();
            conn.Close();
            conn.Open();
            comm3 = new OracleCommand("SELECT INGREDIENTNAME FROM INGREDIENT ", conn);

    reader3 = comm3.ExecuteReader();
            ingridentDropDown.DataSource = reader3;
            ingridentDropDown.DataBind();
            reader1.Close();
            reader2.Close();
            reader3.Close();
            conn.Close();
        }
    }
    protected void Page_PreInit(object sender, EventArgs e)
    {
        HttpCookie myCookies = Request.Cookies["theme"];
        if (myCookies != null)
            Page.Theme = myCookies.Value;
    }

    protected void submit_Click(object sender, EventArgs e)
    {
        try
        {
            String ingName = ingridentDropDown.SelectedValue;
            String subby = submitDropDown.SelectedValue;
            String cat = categoryDropDown.SelectedValue;
        
            OracleConnection conn;
            OracleCommand comm = new OracleCommand();
            string connectionString = ConfigurationManager.ConnectionStrings["RecipieConnection"].ConnectionString;
            conn = new OracleConnection(connectionString);
            OracleDataReader reader;

            if (ingName.Equals("Any") && cat.Equals("All") && subby.Equals("All"))
            {
                comm = new OracleCommand("SELECT RECIPIEID, RECIPIENAME, SUBMITTEDBY, RECIPIECATEGORY.CATEGORY, PREPERATIONTIME, NUMBEROFSERVINGS, DESCRIPTION FROM RECIPIEDETAILS INNER JOIN RECIPIECATEGORY ON RECIPIEDETAILS.CATEGORYID = RECIPIECATEGORY.CATEGORYID ", conn);
                conn.Open();
            }
            else if (ingName.Equals("Any") && cat.Equals("All") && !subby.Equals("All"))
            {
                comm = new OracleCommand("SELECT RECIPIEID, RECIPIENAME, SUBMITTEDBY, RECIPIECATEGORY.CATEGORY, PREPERATIONTIME, NUMBEROFSERVINGS, DESCRIPTION FROM RECIPIEDETAILS INNER JOIN RECIPIECATEGORY ON RECIPIEDETAILS.CATEGORYID = RECIPIECATEGORY.CATEGORYID where RECIPIEDETAILS.SUBMITTEDBY=:SubmittedBy", conn);
                comm.Parameters.Add("SubmittedBy", OracleDbType.Varchar2);
                comm.Parameters["SubmittedBy"].Value = subby;
                conn.Open();
            }
            else if (ingName.Equals("Any") && !cat.Equals("All") && subby.Equals("All"))
            {
                comm = new OracleCommand("SELECT RECIPIEID, RECIPIENAME, SUBMITTEDBY, RECIPIECATEGORY.CATEGORY, PREPERATIONTIME, NUMBEROFSERVINGS, DESCRIPTION FROM RECIPIEDETAILS INNER JOIN RECIPIECATEGORY ON RECIPIEDETAILS.CATEGORYID = RECIPIECATEGORY.CATEGORYID where RECIPIECATEGORY.CATEGORY=:Category", conn);
                comm.Parameters.Add("Category", OracleDbType.Varchar2);
                comm.Parameters["Category"].Value = cat;

                conn.Open();
            }
            else if (!ingName.Equals("Any") && cat.Equals("All") && subby.Equals("All"))
            {
                comm = new OracleCommand("SELECT RECIPIEDETAILS.RECIPIEID, RECIPIENAME, SUBMITTEDBY, RECIPIECATEGORY.CATEGORY, PREPERATIONTIME,NUMBEROFSERVINGS, DESCRIPTION FROM RECIPIEDETAILS INNER JOIN RECIPIECATEGORY ON RECIPIECATEGORY.CATEGORYID=RECIPIEDETAILS.CATEGORYID INNER JOIN RECIPIEINGREDIENT ON RECIPIEINGREDIENT.RECIPIEID=RECIPIEDETAILS.RECIPIEID INNER JOIN INGREDIENT ON  RECIPIEINGREDIENT.INGREDIENTID = INGREDIENT.INGREDIENTID where INGREDIENT.INGREDIENTNAME=:ingName", conn);
                comm.Parameters.Add("ingName", OracleDbType.Varchar2);
                comm.Parameters["ingName"].Value = ingName;
                conn.Open();
            }
            else if (subby.Equals("All"))
            {
                comm = new OracleCommand("SELECT RECIPIEDETAILS.RECIPIEID, RECIPIEDETAILS.RECIPIENAME, RECIPIEDETAILS.SUBMITTEDBY, RECIPIECATEGORY.CATEGORY, RECIPIEDETAILS.PREPERATIONTIME, RECIPIEDETAILS.NUMBEROFSERVINGS as Servings, RECIPIEDETAILS.DESCRIPTION,INGREDIENT.INGREDIENTNAME,RECIPIEINGREDIENT.QUANTITY,RECIPIEINGREDIENT.UNIT FROM RECIPIEDETAILS INNER JOIN RECIPIECATEGORY ON RECIPIEDETAILS.CATEGORYID = RECIPIECATEGORY.CATEGORYID INNER JOIN RECIPIEINGREDIENT ON RECIPIEDETAILS.RECIPIEID = RECIPIEINGREDIENT.RECIPIEID INNER JOIN INGREDIENT ON RECIPIEINGREDIENT.INGREDIENTID = INGREDIENT.INGREDIENTID WHERE INGREDIENT.INGREDIENTNAME =:ingName and RECIPIECATEGORY.CATEGORY=:Category", conn);
                comm.Parameters.Add("ingName", OracleDbType.Varchar2);
                comm.Parameters["ingName"].Value = ingName;
                comm.Parameters.Add("Category", OracleDbType.Varchar2);
                comm.Parameters["Category"].Value = cat;
                conn.Open();
            }
            else if (cat.Equals("All"))
            {
                comm = new OracleCommand("SELECT RECIPIEDETAILS.RECIPIEID, RECIPIENAME, SUBMITTEDBY, RECIPIECATEGORY.CATEGORY, PREPERATIONTIME,NUMBEROFSERVINGS, DESCRIPTION FROM RECIPIEDETAILS INNER JOIN RECIPIECATEGORY ON RECIPIECATEGORY.CATEGORYID=RECIPIEDETAILS.CATEGORYID INNER JOIN RECIPIEINGREDIENT ON RECIPIEINGREDIENT.RECIPIEID=RECIPIEDETAILS.RECIPIEID INNER JOIN INGREDIENT ON  RECIPIEINGREDIENT.INGREDIENTID = INGREDIENT.INGREDIENTID where INGREDIENT.INGREDIENTNAME=:ingName and RECIPIEDETAILS.SUBMITTEDBY=:SubmittedBy", conn);
                comm.Parameters.Add("ingName", OracleDbType.Varchar2);
                comm.Parameters["ingName"].Value = ingName;
                comm.Parameters.Add("SubmittedBy", OracleDbType.Varchar2);
                comm.Parameters["SubmittedBy"].Value = subby;
                conn.Open();
            }
            else if (ingName.Equals("Any"))
            {
                comm = new OracleCommand("SELECT RECIPIEDETAILS.RECIPIEID, RECIPIEDETAILS.RECIPIENAME, RECIPIEDETAILS.SUBMITTEDBY, RECIPIECATEGORY.CATEGORY, RECIPIEDETAILS.PREPERATIONTIME, RECIPIEDETAILS.NUMBEROFSERVINGS as Servings, RECIPIEDETAILS.DESCRIPTION,INGREDIENT.INGREDIENTNAME,RECIPIEINGREDIENT.QUANTITY,RECIPIEINGREDIENT.UNIT FROM RECIPIEDETAILS INNER JOIN RECIPIECATEGORY ON RECIPIEDETAILS.CATEGORYID = RECIPIECATEGORY.CATEGORYID INNER JOIN RECIPIEINGREDIENT ON RECIPIEDETAILS.RECIPIEID = RECIPIEINGREDIENT.RECIPIEID INNER JOIN INGREDIENT ON RECIPIEINGREDIENT.INGREDIENTID = INGREDIENT.INGREDIENTID WHERE RECIPIEDETAILS.SUBMITTEDBY=:SubmittedBy and RECIPIECATEGORY.CATEGORY=:Category", conn);
                comm.Parameters.Add("SubmittedBy", OracleDbType.Varchar2);
                comm.Parameters["SubmittedBy"].Value = subby;
                comm.Parameters.Add("Category", OracleDbType.Varchar2);
                comm.Parameters["Category"].Value = cat;
                conn.Open();
            }
            else if (!ingName.Equals("Any") && !cat.Equals("All") && !subby.Equals("All"))
            {
                comm = new OracleCommand("SELECT RECIPIEDETAILS.RECIPIEID, RECIPIENAME,SUBMITTEDBY, RECIPIECATEGORY.CATEGORY, PREPERATIONTIME, NUMBEROFSERVINGS as Servings, DESCRIPTION,INGREDIENT.INGREDIENTNAME,RECIPIEINGREDIENT.QUANTITY,RECIPIEINGREDIENT.UNIT FROM RECIPIEDETAILS INNER JOIN RECIPIECATEGORY ON RECIPIEDETAILS.CATEGORYID = RECIPIECATEGORY.CATEGORYID INNER JOIN RECIPIEINGREDIENT ON RECIPIEDETAILS.RECIPIEID = RECIPIEINGREDIENT.RECIPIEID INNER JOIN INGREDIENT ON RECIPIEINGREDIENT.INGREDIENTID = INGREDIENT.INGREDIENTID WHERE INGREDIENT.INGREDIENTNAME = '"+ingName+ "' and RECIPIECATEGORY.CATEGORY= '"+cat+"' and RECIPIEDETAILS.SUBMITTEDBY='"+subby+"'", conn);
                conn.Open();
            }
            else
            {
                Label1.Text = "Recipie Not Found...";
            }



            reader = comm.ExecuteReader();
            //Label1.Text = comm.ExecuteReader().HasRows.ToString();
            if (reader != null)
            {
                
                GridView1.DataSource = reader;
                
                GridView1.DataBind();
                reader.Close();
                conn.Close();
            }
           
        } catch (Exception ex)
        {
            Label1.Text = Convert.ToString(ex);
        }
        }
        //string query = "SELECT RecipeTable.Name as Name,RecipeTable.Category as Category,RecipeTable.Cookingtime as Cookingtime,RecipeTable.Numberofservings as Numberofservings,RecipeTable.Description as Description,IngredientTable.ingredientsName as ingredientsName FROM RecipeTable INNER JOIN IngredientTable ON RecipeTable.RecipieId=IngredientTable.ingredientId WHERE RecipeTable.SubmittedBy like '" + subby + "' AND RecipeTable.Category like '" + cat + "' AND IngredientTable.ingredientsName like '" + ingName + "'";
        //conn.Open();

        //comm = new OracleCommand(query, conn);
        //comm.CommandType = CommandType.Text;




    }
