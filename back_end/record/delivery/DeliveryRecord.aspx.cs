using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TimeZone_Assign.back_end.record.delivery
{
    public partial class DeliveryRecord : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void EditDeliverybtn_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string deliveryId = button.CommandArgument;

            Response.Redirect("./EditDelivery.aspx?DELIVERY_ID=" + deliveryId);
        }

        protected void DeleteDeliverybtn_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string deliveryId = button.CommandArgument;

            Response.Redirect("./EditDelivery.aspx?DELIVERY_ID=" + deliveryId);
        }

        protected List<string> getWatch(string deliveryId)
        {
            List<string> watch = new List<string>();

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

            //exchange << WATCH.NAME >> TO WATCH.REFERENCE_NO
            string query = "SELECT STRING_AGG(WATCH.WATCH_ID + ' ' + WATCH.REFERENCE_NO + ' X' + CAST(ORDER_ITEM.QTY AS VARCHAR), ',') AS REFERENCE_NO " +
                "FROM DELIVERY INNER JOIN " +
                "ORDERS ON DELIVERY.DELIVERY_ID = ORDERS.DELIVERY_ID INNER JOIN " +
                "ORDER_ITEM ON ORDERS.ORDER_ID = ORDER_ITEM.ORDER_ID INNER JOIN " +
                "WATCH ON ORDER_ITEM.WATCH_ID = WATCH.WATCH_ID INNER JOIN " +
                "CUSTOMER ON ORDERS.CUSTOMER_ID = CUSTOMER.CUSTOMER_ID WHERE DELIVERY.DELIVERY_ID = @deliveryId";


            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@deliveryId", deliveryId);

            conn.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string referenceNos = reader["REFERENCE_NO"].ToString(); // retrieve the comma-separated string
                string[] referenceNosArray = referenceNos.Split(','); // split the string into an array of strings
                foreach (string referenceNo in referenceNosArray)
                {
                    watch.Add(referenceNo.Trim()); // add each reference number to the list (trim to remove any leading/trailing spaces)
                }
            }

            reader.Close();

            return watch;
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // Check if the current row is a data row
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Get the Payment ID of the current row
                string deliveryId = DataBinder.Eval(e.Row.DataItem, "DELIVERY_ID").ToString();

                // Check if this Payment ID has already been displayed
                if (ViewState["DisplayedDeliveryIds"] != null && ((List<string>)ViewState["DisplayedDeliveryIds"]).Contains(deliveryId))
                {
                    // Hide the row
                    e.Row.Visible = false;
                }
                else
                {
                    // Add the Payment ID to the list of displayed Payment IDs
                    if (ViewState["DisplayedDeliveryIds"] == null)
                    {
                        ViewState["DisplayedDeliveryIds"] = new List<string>();
                    }
                    ((List<string>)ViewState["DisplayedDeliveryIds"]).Add(deliveryId);
                }
            }
        }


    }
}