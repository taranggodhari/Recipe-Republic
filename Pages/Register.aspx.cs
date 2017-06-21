using Oracle.ManagedDataAccess.Client;
using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

public partial class Pages_Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["username"] == null)
        //    Response.Redirect("Pages/Login.aspx");
    }


    protected void CreateUser_Click(object sender, EventArgs e)
    {
        string username = UserName.Text;
        string firstname = FirstName.Text;
        string lastname = LastName.Text;
        string email = Email.Text;
        if (Password.Text.Equals(ConfirmPassword.Text))
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            Byte[] hashedBytes;
            UTF8Encoding encoder = new UTF8Encoding();
            hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(Password.Text));
            string storePass = BitConverter.ToString(hashedBytes);

           
            OracleConnection conn;
            OracleCommand comm;
            OracleDataReader reader;
            string connectionString =
            ConfigurationManager.ConnectionStrings[
            "RecipieConnection"].ConnectionString;
            conn = new OracleConnection(connectionString);
            comm = new OracleCommand("select * from recipieusers where username = '"+username+"'", conn);
            conn.Open();
            reader = comm.ExecuteReader();
            if (!reader.Read())
            {
                comm = new OracleCommand("select * from recipieusers where EMAILADDRESS = '" + email + "'", conn);
                
                reader = comm.ExecuteReader();
                if (!reader.Read())
                {
                    comm = new OracleCommand("Insert into recipieusers (USERNAME, FIRSTNAME, LASTNAME, EMAILADDRESS, PASSWORD) values ('" + username + "','" + firstname + "','" + lastname + "','" + email + "','" + storePass + "')", conn);
                    try
                    {

                        comm.ExecuteNonQuery();
                        ErrorMessage.Text = "registered Sucessfully";
                        Session["username"] = username;
                        System.Threading.Thread.Sleep(500);
                        Response.Redirect("Index.aspx");
                    }
                    catch (Exception ex)
                    {
                        ErrorMessage.Text = ex.ToString();
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                else
                {
                    ErrorMessage.Text = "Email Already Taken";
                }
            }
            else
            {
                ErrorMessage.Text = "Username Already Taken";
            }
        }
        else
            ErrorMessage.Text = "pass Incorredct";
    }

    

    
}