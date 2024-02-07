using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Net.NetworkInformation;
using System.IO;

namespace TimeZone_Assign.front_end
{
    public partial class paypalUpdate : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                //Auto Generate Order ID
                string sql1a = "SELECT COUNT(*) FROM ORDERS";
                SqlCommand cmd1a = new SqlCommand(sql1a, conn);
                int orderCount = (int)cmd1a.ExecuteScalar();
                orderCount++;
                string newOrderID = "OR" + orderCount.ToString("D4");

                string userID = (string)Session["UserID"];
                string sessionKey = "CartItems_" + userID; // create the session key based on the user ID

                List<order_item> orderItems = (List<order_item>)Session[sessionKey] ?? new List<order_item>();




                double subtotal = 0;
                foreach (var item in orderItems)
                {
                    subtotal += item.subtotal;

                }



                double tax = subtotal * 10 / 100;
                double total = subtotal + tax;


                //Auto Generate payment ID
                string sql2a = "SELECT COUNT(*) FROM PAYMENT";
                SqlCommand cmd2a = new SqlCommand(sql2a, conn);
                int paymentCount = (int)cmd2a.ExecuteScalar();
                paymentCount++;
                string newPaymentID = "P" + paymentCount.ToString("D4");

                //insert payment
                string sql2 = "INSERT INTO PAYMENT(PAYMENT_ID, AMOUNT, PAYMENT_DATE, PAYMENT_METHOD, STATUS) " +
                            "VALUES (@paymentId, @total, @paymentDate, @paymentMethod, @status)";
                SqlCommand cmd2 = new SqlCommand(sql2, conn);
                cmd2.Parameters.AddWithValue("@paymentId", newPaymentID);
                cmd2.Parameters.AddWithValue("@total", total);
                cmd2.Parameters.AddWithValue("@paymentDate", DateTime.Today.ToString("d/M/yyyy"));
                cmd2.Parameters.AddWithValue("@paymentMethod", "PayPal");
                cmd2.Parameters.AddWithValue("@status", true);
                cmd2.ExecuteNonQuery();

                //Auto Generate delivery ID
                string sql3a = "SELECT COUNT(*) FROM DELIVERY";
                string shippingAdd = "2A, Jalan Bahagia 30, Taman Bahagia, 11900 Georgetown, Pulau Penang";
                SqlCommand cmd3a = new SqlCommand(sql3a, conn);
                int deliveryCount = (int)cmd3a.ExecuteScalar();
                deliveryCount++;
                string newdeliveryID = "D" + deliveryCount.ToString("D4");

                
                
                // Get the current date and time
                DateTime currentDate = DateTime.Now;

                // Add 7 days to the current date
                DateTime estEarlier = currentDate.AddDays(7);

                // Add 14 days to the current date
                DateTime estLatest = currentDate.AddDays(14);

                string sql3 = "INSERT INTO DELIVERY(DELIVERY_ID, SHIPPING_ADDRESS, EST_EARLIEST_DATE, EST_LATEST_DATE, STATUS) " +
                                "VALUES (@deliveryId, @shippingAdd, CONVERT(date, @estEarlier), CONVERT(date, @estLatest), @status)";
                SqlCommand cmd3 = new SqlCommand(sql3, conn);
                cmd3.Parameters.AddWithValue("@deliveryId", newdeliveryID);
                cmd3.Parameters.AddWithValue("@shippingAdd", shippingAdd);
                cmd3.Parameters.AddWithValue("@estEarlier", estEarlier);
                cmd3.Parameters.AddWithValue("@estLatest", estLatest);
                cmd3.Parameters.AddWithValue("@status", "Packaging");
                cmd3.ExecuteNonQuery();

                //insert orders
                string sql4 = "INSERT INTO ORDERS (ORDER_ID, CUSTOMER_ID, PAYMENT_ID, DELIVERY_ID) " +
                            "VALUES(@orderId, @customerId, @paymentId, @deliveryId)";
                SqlCommand cmd4 = new SqlCommand(sql4, conn);
                cmd4.Parameters.AddWithValue("@orderId", newOrderID);
                cmd4.Parameters.AddWithValue("@customerId", userID);
                cmd4.Parameters.AddWithValue("@paymentId", newPaymentID);
                cmd4.Parameters.AddWithValue("@deliveryId", newdeliveryID);
                cmd4.ExecuteNonQuery();





                foreach (var item in orderItems)
                {
                    //insert order items
                    string sql5 = "INSERT INTO ORDER_ITEM(ORDER_ID, WATCH_ID, QTY) VALUES(@order_ID, @watchId, @qty)";
                    SqlCommand cmd5 = new SqlCommand(sql5, conn);
                    cmd5.Parameters.AddWithValue("@order_ID", newOrderID);
                    cmd5.Parameters.AddWithValue("@watchId", item.watch_id);
                    cmd5.Parameters.AddWithValue("@qty", item.qty);
                    cmd5.ExecuteNonQuery();

                    string sql6 = "SELECT GALLERY.*, CATEGORY.*, WATCH.* FROM CATEGORY INNER JOIN WATCH ON CATEGORY.CATEGORY_ID = WATCH.CATEGORY_ID INNER JOIN GALLERY ON WATCH.GALLERY_ID = GALLERY.GALLERY_ID WHERE WATCH.WATCH_ID = @id";

                    SqlCommand cmd6 = new SqlCommand(sql6, conn);

                    cmd6.Parameters.AddWithValue("@id", item.watch_id);

                    SqlDataReader dr = cmd6.ExecuteReader();

                    int stockQty = 0;

                    if (dr.Read())
                    {
                        stockQty = (int)dr["stock_qty"];
                        dr.Close();
                        stockQty -= item.qty;
                        if (stockQty == 0)
                        {

                            string sql7 = "UPDATE WATCH SET STOCK_QTY =@stockQty WHERE WATCH_ID = @watchId";
                            SqlCommand cmd7 = new SqlCommand(sql7, conn);
                            cmd7.Parameters.AddWithValue("@stockQty", stockQty);
                            cmd7.Parameters.AddWithValue("@watchId", item.watch_id);
                            cmd7.ExecuteNonQuery();
                        }
                        else
                        {
                            string sql7 = "UPDATE WATCH SET STOCK_QTY =@stockQty WHERE WATCH_ID = @watchId";
                            SqlCommand cmd7 = new SqlCommand(sql7, conn);
                            cmd7.Parameters.AddWithValue("@stockQty", stockQty);
                            cmd7.Parameters.AddWithValue("@watchId", item.watch_id);
                            cmd7.ExecuteNonQuery();
                        }





                    }
                    dr.Close();

                }




                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Payment Make Sucessfully'); window.location.href = 'checkout-invoice.aspx?paymentId=" + newPaymentID + "';", true);
                conn.Close();
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