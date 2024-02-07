using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TimeZone_Assign.back_end.record.sales
{
    public partial class SalesRecord : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // Check if the current row is a data row
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Get the Payment ID of the current row
                string paymentId = DataBinder.Eval(e.Row.DataItem, "PAYMENT_ID").ToString();

                // Check if this Payment ID has already been displayed
                if (ViewState["DisplayedPaymentIds"] != null && ((List<string>)ViewState["DisplayedPaymentIds"]).Contains(paymentId))
                {
                    // Hide the row
                    e.Row.Visible = false;
                }
                else
                {
                    // Add the Payment ID to the list of displayed Payment IDs
                    if (ViewState["DisplayedPaymentIds"] == null)
                    {
                        ViewState["DisplayedPaymentIds"] = new List<string>();
                    }
                    ((List<string>)ViewState["DisplayedPaymentIds"]).Add(paymentId);
                }
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                // Get the index of the row that contains the clicked button
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                // Get the values of the fields in the selected row
                GridViewRow row = GridView1.Rows[rowIndex];

                // Get the button control
                Button btn = (Button)row.FindControl("btnEdit");

                string paymentID = Convert.ToString(row.Cells[0].Text); // the ID is in the first cell

                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

                if (btn.Text == "Activate")
                {
                    string query = "UPDATE PAYMENT SET STATUS = 1 WHERE PAYMENT_ID = @paymentID";

                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@paymentID", paymentID);

                    conn.Open();

                    cmd.ExecuteNonQuery();

                    conn.Close();
                }
                else if (btn.Text == "Deactivate")
                {
                    string query = "UPDATE PAYMENT SET STATUS = 0 WHERE PAYMENT_ID = @paymentID";

                    SqlCommand cmd = new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@paymentID", paymentID);

                    conn.Open();

                    cmd.ExecuteNonQuery();

                    conn.Close();
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Payment status successfully updated!'); window.location.href = 'SalesRecord.aspx';", true);
            }

        }
        protected List<string> getWatch(string paymentID)
        {
            List<string> watch = new List<string>();

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

            string query = "SELECT STRING_AGG(WATCH.WATCH_ID + ' ' + WATCH.REFERENCE_NO + ' X' + CAST(ORDER_ITEM.QTY AS VARCHAR), ',') AS REFERENCE_NO " +
                "FROM     PAYMENT INNER JOIN " +
                "ORDERS ON PAYMENT.PAYMENT_ID = ORDERS.PAYMENT_ID INNER JOIN " +
                "ORDER_ITEM ON ORDERS.ORDER_ID = ORDER_ITEM.ORDER_ID INNER JOIN " +
                "WATCH ON ORDER_ITEM.WATCH_ID = WATCH.WATCH_ID INNER JOIN " +
                "CUSTOMER ON ORDERS.CUSTOMER_ID = CUSTOMER.CUSTOMER_ID WHERE PAYMENT.PAYMENT_ID = @paymentID";

            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@paymentID", paymentID);

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
            conn.Close();

            return watch;
        }
    }
}