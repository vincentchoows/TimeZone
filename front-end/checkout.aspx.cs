using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TimeZone_Assign.front_end
{
    public partial class checkout : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


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

                double changeToMYR = total * 0.23838;
                string formattedValue = changeToMYR.ToString("F2");
                totalHiddenField.Value = formattedValue.ToString();



                // Set the values in the UI
                lblSubtotal.Text = subtotal.ToString("#,##0.00");
                lblTax.Text = tax.ToString("#,##0.00");
                lblTotal.Text = total.ToString("#,##0.00");

                cartItemsRepeater.DataSource = orderItems;
                cartItemsRepeater.DataBind();
                try
                {
                    string sql = "SELECT ADDRESS.*, CARD.*, CUSTOMER.* " +
                        "FROM ADDRESS INNER JOIN CUSTOMER ON ADDRESS.CUSTOMER_ID = CUSTOMER.CUSTOMER_ID " +
                        "INNER JOIN CARD ON CUSTOMER.CUSTOMER_ID = CARD.CUSTOMER_ID " +
                        "WHERE CUSTOMER.CUSTOMER_ID = @id";

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@id", userID);

                    conn.Open();

                    SqlDataReader dr = cmd.ExecuteReader();


                    if (dr.Read())
                    {
                        txtName.Text = (string)dr["Name"];
                        txtName.Enabled = false;
                        txtContact.Text = (string)dr["Phone"];
                        txtContact.Enabled = false;
                        txtEmail.Text = (string)dr["Email"];
                        txtEmail.Enabled = false;
                        txtCardName.Text = (string)dr["Name"];
                        txtCardName.Enabled = false;
                        txtCardNumber.Text = (string)dr["Card_number"];
                        txtCardNumber.Enabled = false;
                        txtExpiration.Text = (string)dr["Goodthru"];
                        txtExpiration.Enabled = false;
                        txtCvv.Text = (string)dr["CVV"];
                        txtCvv.Enabled = false;
                        txtAdd1.Text = (string)dr["ADDRESS_LINE_1"];
                        txtAdd2.Text = (string)dr["ADDRESS_LINE_2"];
                        txtPostcode.Text = dr["Postcode"].ToString();
                        txtState.Text = (string)dr["State"];
                        txtCity.Text = (string)dr["City"];
                    }

                    dr.Close();

                    //Auto Generate payment ID
                    string sql2a = "SELECT COUNT(*) FROM PAYMENT";
                    SqlCommand cmd2a = new SqlCommand(sql2a, conn);
                    int paymentCount = (int)cmd2a.ExecuteScalar();
                    paymentCount++;
                    string newPaymentID = "P" + paymentCount.ToString("D4");

                    paymentHiddenField1.Value = newPaymentID;

                    conn.Close();
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/err/page-not-found.html");
                }
            }
            
        }

        protected void payment_Click(object sender, EventArgs e)
        {


            if (paymentMethod.SelectedValue == "PayPal")
            {
                lblError.Text = "Please Select For Card Payment";
            }
            else
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
                    cmd2.Parameters.AddWithValue("@paymentMethod", paymentMethod.SelectedValue);
                    cmd2.Parameters.AddWithValue("@status", true);
                    cmd2.ExecuteNonQuery();

                    //Auto Generate delivery ID
                    string sql3a = "SELECT COUNT(*) FROM DELIVERY";
                    SqlCommand cmd3a = new SqlCommand(sql3a, conn);
                    int deliveryCount = (int)cmd3a.ExecuteScalar();
                    deliveryCount++;
                    string newdeliveryID = "D" + deliveryCount.ToString("D4");

                    //insert delivery 
                    // Get the current date and time
                    DateTime currentDate = DateTime.Now;

                    // Add 7 days to the current date
                    DateTime estEarlier = currentDate.AddDays(7);

                    // Add 14 days to the current date
                    DateTime estLatest = currentDate.AddDays(14);

                    

                    string shippingAdd = txtAdd1.Text + ", " + txtAdd2.Text + ", " + txtPostcode.Text + ", " + txtCity.Text + ", " + txtState.Text;
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



        }

        protected void paymentMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (paymentMethod.SelectedValue == "PayPal")
            {
                cardDetailsDiv.Visible = false;
            }
            else
            {
                cardDetailsDiv.Visible = true;
            }
            UpdatePanel1.Update();
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