using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TimeZone_Assign.front_end;
using System.IO;
using Twilio.TwiML.Voice;

namespace TimeZone_Assign.back_end.record.review
{
    public partial class EditReview : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    //Retrieve Review ID
                    string reviewId = Request.QueryString["reviewId"];
                    string sql = @"
                    SELECT CUSTOMER.*, ORDER_ITEM.*, ORDERS.*, REPLY.*, REVIEW.*, WATCH.*
                    FROM CUSTOMER
                    INNER JOIN ORDERS ON CUSTOMER.CUSTOMER_ID = ORDERS.CUSTOMER_ID
                    INNER JOIN ORDER_ITEM ON ORDERS.ORDER_ID = ORDER_ITEM.ORDER_ID
                    INNER JOIN REVIEW ON ORDER_ITEM.REVIEW_ID = REVIEW.REVIEW_ID
                    LEFT JOIN REPLY ON REVIEW.REPLY_ID = REPLY.REPLY_ID
                    INNER JOIN WATCH ON ORDER_ITEM.WATCH_ID = WATCH.WATCH_ID
                    WHERE REVIEW.REVIEW_ID = @reviewId
                    ";

                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@reviewId", reviewId);

                    conn.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        lblReviewId.Text = (string)dr["review_id"];
                        lblDate.Text = ((DateTime)dr["review_date"]).ToString();
                        lblCustomer.Text = (string)dr["name"];
                        lblProduct.Text = (string)dr["reference_no"];
                        lblRating.Text = dr["rating"].ToString() + " out of 5";
                        lblComment.Text = (string)dr["comment"];

                        string replyId = dr["reply_id"] == DBNull.Value ? "" : (string)dr["reply_id"];

                        if (replyId != "")
                        {
                            lblReplyId.Text = replyId;
                            tbReply.Text = dr["reply"].ToString();
                            dr.Close();
                        }
                        else
                        {
                            dr.Close();
                            string sql1 = "SELECT MAX(REPLY_ID) FROM REPLY";
                            SqlCommand cmd1 = new SqlCommand(sql1, conn);
                            string lastId = cmd1.ExecuteScalar() == DBNull.Value ? "0" : (string)cmd1.ExecuteScalar();
                            int count = int.Parse(lastId[lastId.Length - 1].ToString());
                            count++;
                            lblReplyId.Text = "RR" + count.ToString("D4");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/err/page-not-found.html");
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        protected void confirmBtn_Click(object sender, EventArgs e)
        {
            try
            {
                //Retrieve Reply ID
                string replyId = "";
                string reviewId = Request.QueryString["reviewId"];
                string sql1 = @"
                    SELECT REVIEW.*, CUSTOMER.* 
                    FROM CUSTOMER 
                    CROSS JOIN REVIEW
                    WHERE REVIEW.REVIEW_ID = @reviewId
                    ";

                SqlCommand cmd1 = new SqlCommand(sql1, conn);
                cmd1.Parameters.AddWithValue("@reviewId", reviewId);

                conn.Open();
                SqlDataReader dr = cmd1.ExecuteReader();

                string custName = "";

                if (dr.Read())
                {
                    replyId = dr["reply_id"] == DBNull.Value ? "" : (string)dr["reply_id"];
                    custName = (string)dr["name"];
                }
                conn.Close();

                //SAVE RECORD INTO DB
                //ADD NEW REPLY RECORD (IF REPLY ID = "")
                if (replyId == "")
                {
                    conn.Open();

                    replyId = lblReplyId.Text;

                    string sql = "INSERT INTO REPLY(REPLY_ID, EMPLOYEE_ID, REPLY, REPLY_DATE) VALUES (@replyId,@empId,@reply,@replyDate)";
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    string empId = (string)Session["UserID"];
                    

                    cmd.Parameters.AddWithValue("@replyId", replyId);
                    cmd.Parameters.AddWithValue("@empId", empId);
                    cmd.Parameters.AddWithValue("@reply", tbReply.Text);
                    cmd.Parameters.AddWithValue("@replyDate", DateTime.Today);

                    cmd.ExecuteNonQuery();

                    //UPDATE REPLY_ID (FOREIGN KEY) OF REVIEW TABLE
                    string sql2 = "UPDATE REVIEW SET REPLY_ID = @replyId WHERE REVIEW_ID = @reviewId";
                    SqlCommand cmd2 = new SqlCommand(sql2, conn);

                    cmd2.Parameters.AddWithValue("@replyId", replyId);
                    cmd2.Parameters.AddWithValue("@reviewId", lblReviewId.Text);

                    cmd2.ExecuteNonQuery();

                    conn.Close();


                    string sql3 = @"
                    SELECT CUSTOMER.EMAIL
                    FROM     CUSTOMER INNER JOIN
                    ORDERS ON CUSTOMER.CUSTOMER_ID = ORDERS.CUSTOMER_ID INNER JOIN
                    ORDER_ITEM ON ORDERS.ORDER_ID = ORDER_ITEM.ORDER_ID INNER JOIN
                    REVIEW ON ORDER_ITEM.REVIEW_ID = REVIEW.REVIEW_ID
                    WHERE REVIEW.REVIEW_ID = @id
                    ";

                    SqlCommand cmd3 = new SqlCommand(sql3, conn);
                    cmd3.Parameters.AddWithValue("@id", reviewId);

                    conn.Open();
                    SqlDataReader dr3 = cmd3.ExecuteReader();

                    string email = "";

                    if (dr3.Read())
                    {
                        email = (string)dr3["email"];
                    }
                    conn.Close();


                    string recipientEmail = email;
                    string subject = "Your review has been replied - TimeZone";
                    string body = $@"
                                Dear {custName},
                                <br><br>
                                We would like to inform you that your review has been replied to. 
                                Please login to your account and navigate to the product page to view the reply.
                                <br>
                                If you have any further questions or concerns, please do not hesitate to contact us.
                                <br><br>
                                Best regards,
                                <br>
                                TimeZone
                                ";

                    SendEmail(recipientEmail, subject, body);

                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Reply successfully added!'); window.location.href = 'ReviewRecord.aspx';", true);
                }
                //UPDATE EXISTING REPLY RECORD
                else
                {
                    conn.Open();



                    string sql = "UPDATE REPLY SET EMPLOYEE_ID =@empId, REPLY =@reply, REPLY_DATE =@replyDate WHERE REPLY_ID = @replyId";
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    //HARD CODED (WAIT SESSION TEACH)
                    string empId = (string)Session["UserID"];

                    cmd.Parameters.AddWithValue("@empId", empId);
                    cmd.Parameters.AddWithValue("@reply", tbReply.Text);
                    cmd.Parameters.AddWithValue("@replyDate", DateTime.Today);
                    cmd.Parameters.AddWithValue("@replyId", lblReplyId.Text);

                    cmd.ExecuteNonQuery();
                    conn.Close();

                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Reply successfully updated!'); window.location.href = 'ReviewRecord.aspx';", true);
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/err/page-not-found.html");
            }
        }

        private void SendEmail(string recipientEmail, string subject, string body)
        {
            try
            {
                string mailSender = "timezoneeeeee@gmail.com";

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(mailSender);
                    mail.To.Add(recipientEmail);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new System.Net.NetworkCredential(mailSender, "pwmgkwmmwgghawjv");
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }
            }
            catch (Exception ex)
            {
                string errorMessage = string.Format("An error occurred while sending the email: {0}", ex.ToString());
                System.Diagnostics.Debug.WriteLine(errorMessage);
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