using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace TimeZone_Assign.back_end
{
    public partial class BackEnd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //connect to the database
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            conn.Open();

            //create a command to count the rows
            SqlCommand cmd1 = new SqlCommand("SELECT COUNT(*) FROM CUSTOMER", conn);
            SqlCommand cmd2 = new SqlCommand("SELECT COUNT(*) FROM EMPLOYEE", conn);
            SqlCommand cmd3 = new SqlCommand("SELECT COUNT(*) FROM ORDERS", conn);
            SqlCommand cmd4 = new SqlCommand("SELECT COUNT(*) FROM WATCH", conn);

            //Execute the command and get the result
            int count1 = (int)cmd1.ExecuteScalar();
            int count2 = (int)cmd2.ExecuteScalar();
            int count3 = (int)cmd3.ExecuteScalar();
            int count4 = (int)cmd4.ExecuteScalar();

            //update the label with the count
            Label1.Text = count1.ToString();
            Label2.Text = count2.ToString();
            Label3.Text = count3.ToString();
            Label4.Text = count4.ToString();

            //close the connection
            conn.Close();
        }
    }
}