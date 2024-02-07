using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TimeZone_Assign.back_end.record.review
{
    public partial class ReviewRecord : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string reviewId = button.CommandArgument;

            Response.Redirect("./EditReview.aspx?reviewId=" + reviewId);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string reviewId = button.CommandArgument;

            try
            {
                //Retrieve Reply ID
                string replyId = "";
                string sql = @"
                    SELECT REVIEW.*
                    FROM REVIEW
                    WHERE REVIEW.REVIEW_ID = @reviewId
                    ";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@reviewId", reviewId);

                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    replyId = dr["reply_id"] == DBNull.Value ? "" : (string)dr["reply_id"];
                }

                conn.Close();

                //ONLY DELETE WHEN THERE'S A EXISTING REPLY RECORD
                if (replyId != "")
                {
                    conn.Open();

                    //UPDATE REPLY_ID (FOREIGN KEY) OF REVIEW TABLE TO NULL
                    string sql1 = "UPDATE REVIEW SET REPLY_ID = NULL WHERE REVIEW_ID = @reviewId";
                    SqlCommand cmd1 = new SqlCommand(sql1, conn);

                    cmd1.Parameters.AddWithValue("@reviewId", reviewId);
                    cmd1.ExecuteNonQuery();

                    //DELETE RECORD IN REPLY TABLE
                    string sql2 = "DELETE FROM REPLY WHERE REPLY.REPLY_ID = @replyId";
                    SqlCommand cmd2 = new SqlCommand(sql2, conn);

                    cmd2.Parameters.AddWithValue("@replyId", replyId);
                    cmd2.ExecuteNonQuery();

                    conn.Close();

                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Reply successfully deleted!'); window.location.href = 'ReviewRecord.aspx';", true);
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/err/page-not-found.html");
            }
        }

        protected void btnHide_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string reviewId = button.CommandArgument;
            Boolean status;
            string statusMsg;

            if (button.Text == "Hide Review")
            {
                //Set to Hidden
                status = false;
                statusMsg = "hidden";
            }
            else
            {
                //Set to Visible
                status = true;
                statusMsg = "visibled";
            }

            try
            {
                //UPDATE STATUS OF REVIEW TABLE
                conn.Open();

                
                string sql1 = "UPDATE REVIEW SET STATUS = @status WHERE REVIEW_ID = @reviewId";
                SqlCommand cmd1 = new SqlCommand(sql1, conn);

                cmd1.Parameters.AddWithValue("@status", status);
                cmd1.Parameters.AddWithValue("@reviewId", reviewId);
                cmd1.ExecuteNonQuery();

                conn.Close();
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Review successfully " + statusMsg + "!'); window.location.href = 'ReviewRecord.aspx';", true);
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