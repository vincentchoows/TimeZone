using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.EnterpriseServices;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace timezone.back_end.record.customer
{
    public partial class CustomerRecord : System.Web.UI.Page
    {

        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {


        }

        protected void userGv_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void viewBtn_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string custId = button.CommandArgument;

            Response.Redirect("./ViewCustomer.aspx?custId=" + custId);
        }

        protected void editBtn_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string custId = button.CommandArgument;

            Response.Redirect("./EditCustomer.aspx?custId=" + custId);

        }

        protected void changeStatusBtn_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string[] arguments = button.CommandArgument.Split('|');
            string custId = arguments[0];
            string status = arguments[1];
            bool newStatus = false;

            //step 3: retreive all user input
            if (Page.IsValid)
            {
                try
                {
                    if (status.Equals("False"))
                    {
                        newStatus = true;
                    }
                    else
                    {
                        newStatus = false;
                    }

                    string sql = "UPDATE CUSTOMER SET STATUS = @status WHERE CUSTOMER_ID = @custId";

                    //step 5: sql connection
                    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

                    //step 6: execute cmd
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    //step 6.1: supply parameter
                    cmd.Parameters.Add("@status", SqlDbType.Bit).Value = newStatus;
                    cmd.Parameters.AddWithValue("@custId", custId);

                    //step 7: open connection
                    conn.Open();

                    //step 8: execute cmd, reader, scalar, insert = nonquery
                    cmd.ExecuteNonQuery();

                    //setp 9:
                    conn.Close();

                    //extra: popup message
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Customer Status Updated!'); window.location.href = 'CustomerRecord.aspx';", true);
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/err/page-not-found.html");
                }
                
            }
        }

        protected void Page_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();

            // Log the error
            string logFilePath = Server.MapPath("~/App_Data/ErrorLog.txt");
            using (StreamWriter sw = new StreamWriter(logFilePath, true))
            {
                sw.WriteLine(DateTime.Now.ToString() + ": " + ex.Message);
                sw.WriteLine(ex.StackTrace);
                sw.WriteLine();
            }

            Server.ClearError();

            Response.Redirect("~/err/page-not-found.html");
        }
    }
}