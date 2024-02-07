using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TimeZone_Assign.front_end
{
    public partial class rate_product : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    string orderId = Request.QueryString["orderId"];
                    string watchId = Request.QueryString["watchId"];

                    using (conn)
                    {
                        conn.Open();

                        string sql = @"
                                SELECT CATEGORY.NAME, GALLERY.PRODUCT_IMAGE, PAYMENT.PAYMENT_DATE, WATCH.REFERENCE_NO , WATCH.WATCH_ID
                                FROM GALLERY 
                                INNER JOIN CATEGORY 
                                INNER JOIN ORDERS 
                                INNER JOIN ORDER_ITEM ON ORDERS.ORDER_ID = ORDER_ITEM.ORDER_ID 
                                INNER JOIN PAYMENT ON ORDERS.PAYMENT_ID = PAYMENT.PAYMENT_ID 
                                INNER JOIN WATCH ON ORDER_ITEM.WATCH_ID = WATCH.WATCH_ID 
                                ON CATEGORY.CATEGORY_ID = WATCH.CATEGORY_ID 
                                ON GALLERY.GALLERY_ID = WATCH.GALLERY_ID
                                WHERE ORDERS.ORDER_ID = @orderId AND WATCH.WATCH_ID = @watchId
                            ";


                        SqlCommand cmd = new SqlCommand(sql, conn);

                        cmd.Parameters.AddWithValue("@orderId", orderId);
                        cmd.Parameters.AddWithValue("@watchId", watchId);

                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.Read())
                        {
                            imgBtnProd.ImageUrl = (string)dr["product_image"];
                            lblProdName.Text = "Rolex " + (string)dr["name"] + " " + (string)dr["reference_no"];
                            lblPurchasedOn.Text = ((DateTime)dr["payment_date"]).ToString("yyyy-MM-dd");
                        }

                        dr.Close();
                        conn.Close();
                    }
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/err/page-not-found.html");
                }
            }
        }

        protected void backBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("order-completed.aspx");
        }

        protected void imgBtnProd_Click(object sender, ImageClickEventArgs e)
        {
            string watchId = Request.QueryString["watchId"];
            Response.Redirect("watch.aspx?watchId=" + watchId);
        }

        protected void confirmBtn_Click(object sender, EventArgs e)
        {
            string ratingValue = ratingBtnGroup.SelectedValue;
            int rating = int.Parse(ratingValue);
            string comment = commentTextBox.Text;

            try
            {
                //SAVE INTO DATABASE
                //ORDER ITEM TABLE & REVIEW TABLE
                conn.Open();

                //INSERT INTO REVIEW TABLE
                //Auto Generate Review ID
                string sql = "SELECT COUNT(*) FROM REVIEW";
                SqlCommand cmd = new SqlCommand(sql, conn);

                int count = (int)cmd.ExecuteScalar();
                count++;
                string reviewId = "R" + count.ToString("D4");

                //INSERT STATEMENT
                string sql2 = "INSERT INTO REVIEW(REVIEW_ID, REPLY_ID, RATING, COMMENT, REVIEW_DATE, STATUS) VALUES(@reviewId, @replyId, @rating, @comment, @reviewDate, @status)";
                SqlCommand cmd2 = new SqlCommand(sql2, conn);

                cmd2.Parameters.AddWithValue("@reviewId", reviewId);
                cmd2.Parameters.AddWithValue("@replyId", DBNull.Value);
                cmd2.Parameters.AddWithValue("@rating", rating);
                cmd2.Parameters.AddWithValue("@comment", comment);
                cmd2.Parameters.AddWithValue("@reviewDate", DateTime.Now);
                cmd2.Parameters.AddWithValue("@status", true);

                cmd2.ExecuteNonQuery();

                //UPDATE ORDER ITEM TABLE
                string sql3 = "UPDATE ORDER_ITEM SET REVIEW_ID = @reviewId WHERE ORDER_ID = @orderId AND WATCH_ID = @watchId";
                SqlCommand cmd3 = new SqlCommand(sql3, conn);

                string orderId = Request.QueryString["orderId"];
                string watchId = Request.QueryString["watchId"];

                cmd3.Parameters.AddWithValue("@reviewId", reviewId);
                cmd3.Parameters.AddWithValue("@orderId", orderId);
                cmd3.Parameters.AddWithValue("@watchId", watchId);

                cmd3.ExecuteNonQuery();

                conn.Close();

                //Get current user id FROM SESSION
                //Get current user's name
                string custId = (string)Session["UserID"];

                conn.Open();

                string sql4 = "SELECT CUSTOMER.NAME FROM CUSTOMER WHERE CUSTOMER_ID = @custId";
                SqlCommand cmd4 = new SqlCommand(sql4, conn);

                cmd4.Parameters.AddWithValue("@custId", custId);

                string custName = (string)cmd4.ExecuteScalar();

                conn.Close();

                //Send email to notify user
                string recipientEmail = "lauzj12351@gmail.com";
                string subject = "Thank you for your product review - TimeZone";
                string body = $@"
                            Dear {custName},
                            <br><br>
                            We wanted to take a moment to thank you for your recent product review. Your feedback is important to us as it helps us understand how we can better serve our customers.
                            We're so glad to hear that you are enjoying your purchase and appreciate your positive comments about our product. We work hard to provide quality products and excellent customer service, and it's great to know that we're on the right track.
                            <br><br>
                            Thank you again for taking the time to share your thoughts with us. If you have any questions or concerns, please don't hesitate to reach out to us.
                            <br><br>
                            Best regards,
                            <br>
                            TimeZone
                            ";

                SendEmail(recipientEmail, subject, body);

                //Successful pop up msg and redirect user to record page
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Review successfully added!'); window.location.href = 'order-completed.aspx';", true);
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