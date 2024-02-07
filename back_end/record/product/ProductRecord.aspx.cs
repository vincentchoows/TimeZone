using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TimeZone_Assign.back_end.record.product
{
    public partial class ProductRecord : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void view_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string watchId = button.CommandArgument;

            Response.Redirect("./ProductDetails.aspx?watchId=" + watchId);
        }

        protected void edit_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string watchId = button.CommandArgument;

            Response.Redirect("./EditProduct.aspx?watchId=" + watchId);
        }

        protected void btn_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string watchId = button.CommandArgument;
            Boolean status;
            string statusMsg;

            if (button.Text == "Deactivate")
            {
                //Set to Hidden
                status = false;
                statusMsg = "deactivated";
            }
            else
            {
                //Set to Visible
                status = true;
                statusMsg = "activated";
            }

            try
            {
                //UPDATE STATUS OF WATCH TABLE
                conn.Open();

                string sql = "UPDATE WATCH SET STATUS = @status WHERE WATCH_ID = @watchId";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@status", status);
                cmd.Parameters.AddWithValue("@watchId", watchId);
                cmd.ExecuteNonQuery();

                conn.Close();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Product successfully " + statusMsg + "!'); window.location.href = 'ProductRecord.aspx';", true);
            }
            catch (Exception ex)
            {
                Response.Redirect("~/err/page-not-found.html");
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