using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Oracle.Web.Security;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

public partial class Pages_Login : System.Web.UI.Page
{
  public string originalPassword;
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void LoginButton_Click(object sender, EventArgs e)
    {
        string username = UserName.Text;
 
        MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
        Byte[] hashedBytes;
        UTF8Encoding encoder = new UTF8Encoding();
        hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(Password.Text));
        string userpassword = BitConverter.ToString(hashedBytes);

        OracleConnection conn;
        OracleCommand comm;
        OracleDataReader reader;
        string connectionString =
        ConfigurationManager.ConnectionStrings[
        "RecipieConnection"].ConnectionString;
        conn = new OracleConnection(connectionString);
        comm = new OracleCommand("select * from recipieusers where username = '" + username + "'", conn);
        conn.Open();
        reader = comm.ExecuteReader();
        if (reader.Read())
        {
          string storedpassword = Convert.ToString(reader["PASSWORD"]);
            if(userpassword == storedpassword)
            {
                FailureText.Text = "You are logged in";
                Session["username"] = username;
                Response.Redirect("Index.aspx");
                
            }
            else
            {
                FailureText.Text = "Incorrect Password";
            }
            
        }
        else
        {
            FailureText.Text = "Username Not Found";
        }
    }
}