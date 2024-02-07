using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace TimeZone_Assign.front_end
{
    public partial class checkout_invoice : System.Web.UI.Page
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

                lblTotal.Text = total.ToString("#,##0.00");
                lblSubtotal.Text = subtotal.ToString("#,##0.00");
                lblTax.Text = tax.ToString("#,##0.00");

                

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
                        lblName.Text = (string)dr["Name"];
                        lblEmail.Text = (string)dr["Email"];
                        lblAdd1.Text = (string)dr["ADDRESS_LINE_1"];
                        lblAdd2.Text = (string)dr["ADDRESS_LINE_2"];
                        lblPostCode.Text = dr["Postcode"].ToString();
                        lblState.Text = (string)dr["State"];
                        lblCity.Text = (string)dr["City"];
                    }
                    dr.Close();

                    //payment method pass the order id ez
                    string paymentId = Request.QueryString["paymentId"];

                    string sql2 = "SELECT PAYMENT_METHOD, PAYMENT_DATE FROM PAYMENT WHERE PAYMENT_ID = @id";

                    SqlCommand cmd2 = new SqlCommand(sql2, conn);

                    cmd2.Parameters.AddWithValue("@id", paymentId);

                    SqlDataReader dr2 = cmd2.ExecuteReader();
                    if (dr2.Read())
                    {
                        lblPayMethod.Text = (string)dr2["PAYMENT_METHOD"];
                        lblPaymentDate.Text = ((DateTime)dr2["PAYMENT_DATE"]).ToString("yyyy-MM-dd");
                    }

                    dr2.Close();

                    string sql1a = "SELECT COUNT(*) FROM ORDERS";
                    SqlCommand cmd1a = new SqlCommand(sql1a, conn);
                    int orderCount = (int)cmd1a.ExecuteScalar();
                    string OrderID = "OR" + orderCount.ToString("D4");

                    lblInvoice.Text = OrderID;
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/err/page-not-found.html");
                }
                conn.Close();
                Session.Remove(sessionKey);
            }
        }

        protected void btnOrder_Click(object sender, EventArgs e)
        {
            Response.Redirect("order-pending.aspx");
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