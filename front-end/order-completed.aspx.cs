using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TimeZone_Assign.front_end
{
    public partial class order_completed : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {


                //HARDCODED NOW
                string custId = (string)Session["UserID"];

                try
                {
                    CUSTOMER custSession = (CUSTOMER)Session["customer"];
                    string sessionCustID = custSession.CUSTOMER_ID;
                    using (var db = new watchEntities2())
                    {

                        // Retrieve from db
                        CUSTOMER customer = db.CUSTOMERs.FirstOrDefault(c => c.CUSTOMER_ID == sessionCustID);

                        if (customer != null)
                        {
                            lblName.Text = customer.NAME;


                        }
                        else
                        {
                            return;
                        }
                    }

                    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                    {
                        conn.Open();

                        string sql = @"
                        SELECT ORDERS.ORDER_ID, PAYMENT.PAYMENT_DATE, DELIVERY.EST_EARLIEST_DATE, DELIVERY.EST_LATEST_DATE, DELIVERY.STATUS, PAYMENT.AMOUNT, DELIVERY.ARRIVAL_DATE 
                        FROM CUSTOMER 
                        INNER JOIN ORDERS ON CUSTOMER.CUSTOMER_ID = ORDERS.CUSTOMER_ID 
                        INNER JOIN DELIVERY ON ORDERS.DELIVERY_ID = DELIVERY.DELIVERY_ID 
                        INNER JOIN PAYMENT ON ORDERS.PAYMENT_ID = PAYMENT.PAYMENT_ID
                        WHERE CUSTOMER.CUSTOMER_ID = @id
                        AND DELIVERY.STATUS = 'Delivered'
                        ";

                        SqlCommand cmd = new SqlCommand(sql, conn);

                        cmd.Parameters.AddWithValue("@id", custId);

                        SqlDataReader dr = cmd.ExecuteReader();

                        List<Order> orderList = new List<Order>();

                        while (dr.Read())
                        {
                            Order order = new Order();

                            order.id = (string)dr["order_id"];
                            order.paymentDate = ((DateTime)dr["payment_date"]).ToString("yyyy-MM-dd");
                            order.estEarliestDate = ((DateTime)dr["est_earliest_date"]).ToString("yyyy-MM-dd");
                            order.estLatestDate = ((DateTime)dr["est_latest_date"]).ToString("yyyy-MM-dd");
                            order.status = (string)dr["status"];
                            order.arrivalDate = ((DateTime)dr["arrival_date"]).ToString("yyyy-MM-dd");

                            double paymentAmount = Convert.ToDouble(dr["amount"]);
                            string formattedPrice = paymentAmount.ToString("C", CultureInfo.GetCultureInfo("ms-MY"));

                            order.paymentAmount = formattedPrice;

                            orderList.Add(order);
                        }

                        dr.Close();

                        RepeaterOrderRecord.DataSource = orderList;
                        RepeaterOrderRecord.DataBind();

                        foreach (RepeaterItem item in RepeaterOrderRecord.Items)
                        {
                            string orderId = ((Label)item.FindControl("lblOrderId")).Text;

                            string sql2 = @"
                            SELECT CATEGORY.NAME, GALLERY.PRODUCT_IMAGE, ORDER_ITEM.QTY, ORDER_ITEM.REVIEW_ID, WATCH.REFERENCE_NO, WATCH.PRICE, WATCH.WATCH_ID
                            FROM ORDERS 
                            INNER JOIN CUSTOMER ON ORDERS.CUSTOMER_ID = CUSTOMER.CUSTOMER_ID 
                            INNER JOIN ORDER_ITEM ON ORDERS.ORDER_ID = ORDER_ITEM.ORDER_ID 
                            INNER JOIN WATCH ON ORDER_ITEM.WATCH_ID = WATCH.WATCH_ID 
                            INNER JOIN GALLERY ON WATCH.GALLERY_ID = GALLERY.GALLERY_ID 
                            INNER JOIN CATEGORY ON WATCH.CATEGORY_ID = CATEGORY.CATEGORY_ID
                            WHERE ORDERS.ORDER_ID = @orderId
                            ";

                            SqlCommand cmd2 = new SqlCommand(sql2, conn);

                            cmd2.Parameters.AddWithValue("@orderId", orderId);

                            SqlDataReader dr2 = cmd2.ExecuteReader();

                            List<OrderItem> orderItemList = new List<OrderItem>();

                            while (dr2.Read())
                            {
                                OrderItem orderItem = new OrderItem();

                                orderItem.imagePath = (string)dr2["product_image"];
                                orderItem.name = "Rolex " + (string)dr2["name"] + " " + (string)dr2["reference_no"];
                                orderItem.qty = (int)dr2["qty"];
                                orderItem.watchId = (string)dr2["watch_id"];
                                orderItem.orderId = orderId;

                                object reviewIdObj = dr2["review_id"];
                                string reviewId = Convert.IsDBNull(reviewIdObj) ? null : (string)reviewIdObj;

                                if (reviewId == null)
                                {
                                    //Able to rate
                                    orderItem.isRated = !false;
                                    orderItem.isRatedStr = "Rate";
                                    orderItem.isRatedCss = "rate-button button";
                                }
                                else
                                {
                                    //Unable to rate
                                    orderItem.isRated = !true;
                                    orderItem.isRatedStr = "Rated";
                                    orderItem.isRatedCss = "rate-button rate-button-disabled button";
                                }

                                double price = Convert.ToDouble(dr2["price"]);
                                string formattedPrice = price.ToString("C", CultureInfo.GetCultureInfo("ms-MY"));
                                orderItem.price = formattedPrice;

                                orderItemList.Add(orderItem);
                            }

                            dr2.Close();

                            Repeater repeaterProdPurchased = (Repeater)item.FindControl("RepeaterProdPurchased");
                            repeaterProdPurchased.DataSource = orderItemList;
                            repeaterProdPurchased.DataBind();
                        }

                        conn.Close();
                    }
                }
                catch
                {
                    Response.Redirect("~/err/page-not-found.html");
                }
            }
        }

        protected void rateBtn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string[] args = btn.CommandArgument.Split(',');
            string orderId = args[0];
            string watchId = args[1];
            Response.Redirect("rate-product.aspx?orderId=" + orderId + "&watchId=" + watchId);

        }
        protected void imgBtnProd_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btn = (ImageButton)sender;
            string watchId = btn.CommandArgument;
            Response.Redirect("watch.aspx?watchId=" + watchId);
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