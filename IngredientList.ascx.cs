using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class IngredientList : System.Web.UI.UserControl
{
    int ingredientId;
    private int count = 0;
    private string[] myIngredients = new String[15];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            IngredientRepository.ingredients.Clear();
            grd.DataSource = GetTableWithInitialData(); // get first initial data
            grd.DataBind();
        }
    }

    private DataTable GetTableWithInitialData()
    {
        DataTable table = new DataTable();
        table.Columns.Add("Ingredient", typeof(string));
        table.Columns.Add("Unit", typeof(string));
        table.Columns.Add("Quantity", typeof(string));

        return table;

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

            if(count<15 && Array.IndexOf(myIngredients, txtIngredient.Text)<0)
            {
                try
                {
                    row[0] = txtIngredient.Text;
                    row[1] = txtUnit.Text;
                    row[2] = txtQuantity.Text;
                    IngredientRepository.ingredients.Add(new Ingridient(txtIngredient.Text, Convert.ToInt32(txtQuantity.Text), txtUnit.Text));

                    myIngredients[count++] = txtIngredient.Text;
                    table.Rows.Add(row);
                }catch(Exception ex)
                {
                    Label1.Text = ex.ToString();
                }
            }
        }
        if(count<15)
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
        table.Columns.Add("Ingredient", typeof(string));
        table.Columns.Add("Unit", typeof(string));
        table.Columns.Add("Quantity", typeof(string));
        return table;
    }

    //public int a = 0;
    //protected void AddIngredients()
    //{
    //    ListItem items = IngredientTarget.Items.FindByText(IngName.Text);
    //    if (!string.IsNullOrEmpty(IngName.Text))
    //        if (items == null)
    //        {
    //            IngredientTarget.Items.Add(IngName.Text);
    //            QuantityTarget.Items.Add(quant.Text);
    //            UnitTarget.Items.Add(unitMesure.Text);
    //        }
    //}



    //protected void Save_Click(object sender, EventArgs e)
    //{
    //    try
    //     { 
    //    a++;
    //        if (a <= 15)
    //        {
    //            AddIngredients();


    //            //List<Ingridient> ingred = new List<Ingridient>();
    //            //ingred.Add(new Ingridient
    //            //{
    //            //    Quantity = Convert.ToInt32(quant.Text),
    //            //    IngredientName = Convert.ToString(IngName.Text),
    //            //    UnitofMeasures = Convert.ToString(quant.Text)

    //            //});
    //            //Ingridient ingredients = (Ingridient)Application["ingridient"];
    //            //for (int i = 0; i < ingred.Count; i++)
    //            //{
    //            //    if (ingred != null)
    //            //    {
    //            //        TextBox1.Text = 

    //            //    }
    //            //}
    //            //foreach(Ingridient ingrede in ingred)
    //            //{
    //            //    System.Console.WriteLine( Convert.ToString(ingrede));
    //            //}

    //            List<Ingridient> ingred = new List<Ingridient>();

    //            if (Session["ingredient"] != null)
    //            {
    //                ingred = (List<Ingridient>)Session["ingredient"];
    //            }
    //            else
    //            {
    //                ingred = new List<Ingridient>();
    //            }
    //            string ingredName = Convert.ToString(IngName.Text);
    //            int quantity = Convert.ToInt32(quant.Text);
    //            string unit = Convert.ToString(unitMesure.Text);

    //            Ingridient addingredient = new Ingridient(ingredName, quantity, unit);
    //            if (!ingred.Contains(addingredient))
    //            {
    //                ingred.Add(addingredient);
    //                Session["ingredient"] = ingred;
    //            }
    //            DisplayListOfIngredient();

    //            //    SqlConnection conn;
    //            //    SqlCommand comm;
    //            //    string connectionString =
    //            //        ConfigurationManager.ConnectionStrings[
    //            //        "Recipie"].ConnectionString;
    //            //    conn = new SqlConnection(connectionString);
    //            //    conn.Open();

    //            //    comm = new SqlCommand("Select RecipieId from RecipeTable Where Status = 1", conn);
    //            //    comm.CommandType = System.Data.CommandType.Text;

    //            //    SqlDataReader reader = comm.ExecuteReader();
    //            //    SqlCommand commu;


    //            //    string Query;
    //            //    //if (reader.HasRows)
    //            //    {
    //            //        reader.Read();
    //            //        ingredientId = reader.GetInt32(0);
    //            //    }
    //            //    //Label1.Text = Convert.ToString(idSet);
    //            //    conn.Close();
    //            //    conn.Open();

    //            //    Query = "insert into IngredientTable(ingredientId,ingredientsName,quantityName,unit) values (' " + ingredientId + "','" + IngName.Text + "','" + quant.Text + "','" + unitMesure.Text + "')";
    //            //    commu = new SqlCommand(Query, conn);
    //            //    commu.ExecuteNonQuery();
    //            //    conn.Close();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //       Convert.ToString(ex);
    //    }

    //}
    //protected void DisplayListOfIngredient()
    //{
    //    TextBox1.Text = "";
    //    List<Ingridient> ingred = new List<Ingridient>();
    //    if (ViewState["ingredient"] != null)
    //    {
    //        ingred = (List<Ingridient>)Session["ingredient"];
    //        //for (int i =0; i<ingredientlist.Count;i++)
    //        //{
    //        //    TextBox1.Text = ingredientlist[i].ToString();
    //        //}
    //        foreach (Ingridient currentperson in ingred)
    //        {
    //            //IngredientTarget.DataSource = currentperson;
    //            //IngredientTarget.DataBind();
    //            TextBox1.Text += currentperson.IngredientName + " " + currentperson.Quantity + " " + currentperson.UnitofMeasures + " ";
    //        }
    //    }


    //}



   
}