using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace timezone.back_end.record.customer
{
    public partial class ViewCustomer : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string id = Request.QueryString["custId"] ?? "";
                string sql = "SELECT * FROM CUSTOMER WHERE CUSTOMER_ID = @id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", id);

                string sql2 = "SELECT * FROM ADDRESS WHERE CUSTOMER_ID = @id2";
                SqlCommand cmd2 = new SqlCommand(sql2, conn);
                cmd2.Parameters.AddWithValue("@Id2", id);

                string sql3 = "SELECT * FROM CARD WHERE CUSTOMER_ID = @id3";
                SqlCommand cmd3 = new SqlCommand(sql3, conn);
                cmd3.Parameters.AddWithValue("@Id3", id);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string custId = reader.GetString(0);
                        string name = reader.GetString(1);
                        string gender = reader.GetString(2);
                        string email = reader.GetString(3);
                        string phone = reader.GetString(4);
                        string username = reader.GetString(5);
                        string password = reader.GetString(6);
                        DateTime date = reader.GetDateTime(7);
                        // Do something with the field values

                        tbCustID.Text = custId;
                        tbName.Text = name;
                        tbGender.Text = gender == "M" ? "Male" : "Female";
                        tbEmail.Text = email;
                        tbPhone.Text = phone;
                        tbUsername.Text = username;

                        tbDateReg.Text = date.ToString();
                    }
                    reader.Close();

                    SqlDataReader reader2 = cmd2.ExecuteReader();
                    while (reader2.Read())
                    {
                        string addressId = reader2.GetString(0);
                        string address1 = reader2.GetString(2);
                        string address2 = reader2.GetString(3);
                        int postcode = reader2.GetInt32(4);
                        string city = reader2.GetString(5);
                        string state = reader2.GetString(6);

                        // Do something with the field values
                        tbAddressId.Text = addressId;
                        tbAddress1.Text = address1;
                        tbAddress2.Text = address2;
                        tbPostcode.Text = postcode.ToString();
                        tbCity.Text = city;
                        tbState.Text = state;
                    }
                    reader2.Close();

                    SqlDataReader reader3 = cmd3.ExecuteReader();

                    while (reader3.Read())
                    {
                        string cardId = reader3.GetString(0);
                        string cardNo = reader3.GetString(2);
                        string goodThru = reader3.GetString(3);
                        string cvv = reader3.GetString(4);
                        // Do something with the field values

                        tbCardId.Text = cardId;
                        tbCardNo.Text = cardNo;
                        tbGoodThru.Text = goodThru;
                        tbCvv.Text = cvv;
                    }
                    reader3.Close();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/err/page-not-found.html");
                }
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