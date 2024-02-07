
using System;
using System.Configuration;
using System.Data.SqlClient;

using System.Web.UI;
using System.Web.UI.WebControls;

using System.Collections.Generic;
using System.Linq;
using System.Web;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using System.Collections.Specialized;
using Twilio.TwiML.Voice;

namespace TimeZone_Assign.back_end.record.delivery
{
    public partial class EditDelivery : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.DataBind();

                string deliveryId = Request.QueryString["DELIVERY_ID"].ToString();
                string sql = "SELECT * FROM DELIVERY WHERE DELIVERY_ID = @deliveryId";

                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();

                cmd.Parameters.AddWithValue("@deliveryId", deliveryId);
                SqlDataReader r = cmd.ExecuteReader();
                r.Read();

                lblDeliveryID.Text = deliveryId;

                txtShippingAddress.Text = r["SHIPPING_ADDRESS"].ToString();

                DateTime date;
                string dateEarlistDate = r["EST_EARLIEST_DATE"].ToString();
                if (DateTime.TryParse(dateEarlistDate, out date))
                {
                    //set the txtEarliestDate.Text to the desired date format
                    txtEarliestDate.Text = date.ToString("yyyy-MM-dd");
                }

                string dateLatestDate = r["EST_LATEST_DATE"].ToString();
                if (DateTime.TryParse(dateLatestDate, out date))
                {
                    txtLatestDate.Text = date.ToString("yyyy-MM-dd");
                }

                string arrivalDate = r["ARRIVAL_DATE"].ToString();
                if (DateTime.TryParse(arrivalDate, out date))
                {
                    txtArrivalDate.Text = date.ToString("yyyy-MM-dd");
                }

                string ddlStatusValue = r["STATUS"].ToString();
                foreach (ListItem item in ddlStatus.Items)
                {
                    if (item.Value == ddlStatusValue)
                    {
                        item.Selected = true;
                    }
                }

                r.Close();

                string sql1 = "SELECT CUSTOMER.PHONE FROM CUSTOMER INNER JOIN ORDERS ON CUSTOMER.CUSTOMER_ID = ORDERS.CUSTOMER_ID INNER JOIN DELIVERY ON ORDERS.DELIVERY_ID = DELIVERY.DELIVERY_ID WHERE DELIVERY.DELIVERY_ID = @id";

                SqlCommand cmd1 = new SqlCommand(sql1, conn);

                cmd1.Parameters.AddWithValue("@id", deliveryId);

                string phoneNo = "";

                SqlDataReader dr = cmd1.ExecuteReader();



                if (dr.Read())
                {
                    phoneNo = (string)dr["PHONE"];
                }
                string cleanedPhoneNumber = phoneNo.Replace("-", "");
                
                btnEditDelivery.CommandArgument = cleanedPhoneNumber;

                conn.Close();
            }

        }

        protected void btnEditDelivery_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    conn.Open();

                    string deliveryId = Request.QueryString["DELIVERY_ID"].ToString();

                    


                    string sql = "UPDATE DELIVERY SET SHIPPING_ADDRESS =@shippingAddress, EST_EARLIEST_DATE =@estEarlierDate, EST_LATEST_DATE =@estLatestDate, ARRIVAL_DATE =@arrivalDate, STATUS =@status WHERE DELIVERY_ID = @deliveryId";

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@deliveryId", deliveryId);
                    cmd.Parameters.AddWithValue("@shippingAddress", txtShippingAddress.Text);
                    cmd.Parameters.AddWithValue("@estEarlierDate", txtEarliestDate.Text);
                    cmd.Parameters.AddWithValue("@estLatestDate", txtLatestDate.Text);
                    if (txtArrivalDate.Enabled)
                    {
                        cmd.Parameters.AddWithValue("@arrivalDate", txtArrivalDate.Text);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@arrivalDate", DBNull.Value);
                    }
                    cmd.Parameters.AddWithValue("@status", ddlStatus.SelectedValue);
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    //Retrieve orders details from database

                    string sql2 = "SELECT PAYMENT.PAYMENT_DATE, DELIVERY.EST_EARLIEST_DATE, DELIVERY.EST_LATEST_DATE, DELIVERY.ARRIVAL_DATE, ORDERS.ORDER_ID FROM DELIVERY INNER JOIN ORDERS ON DELIVERY.DELIVERY_ID = ORDERS.DELIVERY_ID INNER JOIN PAYMENT ON ORDERS.PAYMENT_ID = PAYMENT.PAYMENT_ID WHERE DELIVERY.DELIVERY_ID = @deliveryId";

                    SqlCommand cmd2 = new SqlCommand(sql2, conn);

                    cmd2.Parameters.AddWithValue("@deliveryId", deliveryId);

                    conn.Open();

                    SqlDataReader dr = cmd2.ExecuteReader();

                    DateTime? paymentDate = null;
                    DateTime? estEarliestDate = null;
                    DateTime? estLatestDate = null;
                    DateTime? arrivalDate = null;
                    string orderId = null;

                    if (dr.Read())
                    {
                        //Retrieve the values from the DataReader and assign them to the variables
                        paymentDate = Convert.ToDateTime(dr["PAYMENT_DATE"]);
                        estEarliestDate = Convert.ToDateTime(dr["EST_EARLIEST_DATE"]);
                        estLatestDate = Convert.ToDateTime(dr["EST_LATEST_DATE"]);

                        if (dr["ARRIVAL_DATE"] != DBNull.Value)
                        {
                            arrivalDate = Convert.ToDateTime(dr["ARRIVAL_DATE"]);
                        }

                        orderId = (string)dr["ORDER_ID"];
                    }


                    

                    //Send a Whatsapp msg to notify the user
                    string msg;

                    string phoneNo = btnEditDelivery.CommandArgument;


                    if (ddlStatus.SelectedValue.ToLower() == "shipping")
                    {
                        msg = $"Your order {orderId} on {paymentDate:d} has been shipped and is estimated to arrive between {estEarliestDate:d} - {estLatestDate:d}.";
                        sendWhatsapp(msg, phoneNo);
                    }
                    else if (ddlStatus.SelectedValue.ToLower() == "delivered")
                    {
                        msg = $"Your order {orderId} on {paymentDate:d} has been delivered on {arrivalDate:d}. Thank you for choosing us!";
                        sendWhatsapp(msg, phoneNo);
                    }

                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Delivery successfully updated!'); window.location.href = 'DeliveryRecord.aspx';", true);
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/err/page-not-found.html");
                }
            }
        }

        protected void btnCancelEdit_Click(object sender, EventArgs e)
        {
            Response.Redirect("DeliveryRecord.aspx");
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlStatus.SelectedItem.Text == "Delivered")
            {
                txtArrivalDate.Enabled = true;
                DateTime today = DateTime.Today;
                string tdyDate = today.ToString("yyyy-MM-dd");
                txtArrivalDate.Text = tdyDate;
            }
            else
            {
                txtArrivalDate.Enabled = false;
                txtArrivalDate.Text = "";
            }
        }

        private void sendWhatsapp(string body, string phoneNo)
        {

            phoneNo = "+6" + phoneNo;
            // Account SID from twilio.com/console
            string accountSid = "AC56418fc0e1fc241ad40aacd79e91a0ce";

            // Auth Token from twilio.com/console
            string authToken = "01ee8ff19ab4bc47502f5b8954dd2da9";

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                from: new PhoneNumber("whatsapp:+14155238886"),
                /*to: new PhoneNumber("whatsapp:+60124301471"),*/
                to: new PhoneNumber("whatsapp:" + phoneNo),
                body: body);

        }
    }
}