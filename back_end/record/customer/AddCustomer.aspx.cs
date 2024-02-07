using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using TimeZone_Assign;

namespace timezone.back_end.record.customer
{
    public partial class AddCustomer : System.Web.UI.Page
    {
        watchEntities2 db = new watchEntities2();

        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

        //step 2: retreive CS from global.asax
        //string cs = Global.CS;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    string sql = "SELECT COUNT(*) FROM CUSTOMER";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();
                    count++;
                    tbCustID.Text = "CU" + count++.ToString("D4");
                    conn.Close();

                    string sql2 = "SELECT COUNT(*) FROM ADDRESS";
                    cmd = new SqlCommand(sql2, conn);
                    conn.Open();
                    int count2 = (int)cmd.ExecuteScalar();
                    count2++;
                    tbAddressId.Text = "A" + count2++.ToString("D4");
                    conn.Close();

                    string sql3 = "SELECT COUNT(*) FROM CARD";
                    cmd = new SqlCommand(sql3, conn);
                    conn.Open();
                    int count3 = (int)cmd.ExecuteScalar();
                    count3++;
                    tbCardId.Text = "C" + count3++.ToString("D4");
                    conn.Close();
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/err/page-not-found.html");
                }
                

            }
            //retrive customer details from aspx to backend and save in db
        }

        protected void confirmBtn_Click(object sender, EventArgs e)
        {
            //step 3: retreive all user input
            if (Page.IsValid)
            {
                string id = tbCustID.Text;
                string name = tbName.Text;
                string gender = rblGender.SelectedValue;
                string email = tbEmail.Text;
                string phone = tbPhone.Text;
                string username = tbUsername.Text;
                string password = tbPassword.Text;
                DateTime date = DateTime.Now;

                string addressId = tbAddressId.Text;
                string address1 = tbAddress1.Text;
                string address2 = tbAddress2.Text;
                string postcode = tbPostcode.Text;
                string city = ddlCity.SelectedValue;
                string state = ddlState.SelectedValue;

                string cardId = tbCardId.Text;
                string cardNo = tbCardNo.Text;
                string goodThru = tbGoodThru.Text;
                string cvv = tbCvv.Text;

                //server - side validations
                if (!validationCustomer(name, username, email, phone, password) &&
                   !validationAddress(address1, address2, city, state, postcode) &&
                   !validationCard(cardNo, goodThru, cvv))
                {
                    return;
                }
                //account existence check 
                if (!accountExistenceCheck(email))
                {
                    return;
                }
                //username existence check 
                if (!usernameExistenceCheck(username))
                {
                    return;
                }


                //step 4: insert sql statemetn
                string sql = "INSERT INTO CUSTOMER (CUSTOMER_ID, NAME, GENDER, EMAIL, PHONE, USERNAME, PASSWORD, DATE_REGISTERED, STATUS) VALUES (@Id,@name,@gender,@email,@phone,@username,@password,@date, @status)";

                string sql2 = "INSERT INTO ADDRESS (ADDRESS_ID, CUSTOMER_ID, ADDRESS_LINE_1, ADDRESS_LINE_2, POSTCODE, CITY, STATE) VALUES        (@addressId,@Id,@address1,@address2,@postcode,@city,@state)";

                string sql3 = "INSERT INTO CARD (CARD_ID, CUSTOMER_ID, CARD_NUMBER, GOODTHRU, CVV) VALUES (@cardId,@Id,@cardNo,@goodThru,@Cvv)";

                //step 5: sql connection
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

                //step 6: execute cmd
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlCommand cmd2 = new SqlCommand(sql2, conn);
                SqlCommand cmd3 = new SqlCommand(sql3, conn);

                string hashedPassword = Encryption.HashPassword(password);


                //step 6.1: supply parameter
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@gender", gender);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", hashedPassword);
                cmd.Parameters.AddWithValue("@date", date);
                cmd.Parameters.AddWithValue("@status", 1);

                cmd2.Parameters.AddWithValue("@addressId", addressId);
                cmd2.Parameters.AddWithValue("@Id", id);
                cmd2.Parameters.AddWithValue("@address1", address1);
                cmd2.Parameters.AddWithValue("@address2", address2);
                cmd2.Parameters.AddWithValue("@postcode", postcode);
                cmd2.Parameters.AddWithValue("@city", city);
                cmd2.Parameters.AddWithValue("@state", state);

                cmd3.Parameters.AddWithValue("@cardId", cardId);
                cmd3.Parameters.AddWithValue("@Id", id);
                cmd3.Parameters.AddWithValue("@cardNo", cardNo);
                cmd3.Parameters.AddWithValue("@goodThru", goodThru);
                cmd3.Parameters.AddWithValue("@cvv", cvv);

                try
                {
                    //step 7: open connection
                    conn.Open();

                    //step 8: execute cmd, reader, scalar, insert = nonquery
                    cmd.ExecuteNonQuery();
                    cmd2.ExecuteNonQuery();
                    cmd3.ExecuteNonQuery();

                    //setp 9:
                    conn.Close();

                    //redirect user back to select2.aspx
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('New Customer Record Added!'); window.location.href = 'CustomerRecord.aspx';", true);

                }
                catch (Exception ex)
                {
                    Response.Redirect("~/err/page-not-found-admin.html");
                }



            }
        }

        protected void cancelBtn_Click(object sender, EventArgs e)
        {
            // Call the JavaScript function to disable client-side validations
            Page.ClientScript.RegisterStartupScript(this.GetType(), "disableValidations", "disableClientValidations();", true);

            // Redirect user back to CustomerRecord.aspx
            Response.Redirect("~/back_end/record/customer/CustomerRecord.aspx");
        }

        ////VALIDATIONS==============================================================================

        protected bool validationCustomer(string name, string username, string email, string phoneNo, string password)
        {
            //name, username, email, phoneNO,password
            string regexName = "^[A-Z][a-zA-Z]*( [A-Z][a-zA-Z]*)*$";
            string regexUsername = "^[a-zA-Z][a-zA-Z0-9]*(?:_[a-zA-Z0-9]+)*$";
            string regexEmail = "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$";
            string regexPhoneNo = "^01[0-46-9]-\\d{3}-?\\d{4,6}$";
            string regexPassword = "^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[^a-zA-Z0-9]).{8,}$";


            if (Regex.IsMatch(name, regexName) &&
                Regex.IsMatch(username, regexUsername) &&
                Regex.IsMatch(email, regexEmail) &&
                Regex.IsMatch(phoneNo, regexPhoneNo) &&
                Regex.IsMatch(password, regexPassword))
            {
                //if all matches
                return true;
            }
            else
            {
                return false;
            }


        }

        protected bool validationAddress(string add1, string add2, string city, string state, string poscode)
        {
            string regexAdd = "^[a-zA-Z0-9\\s#.,/-]+$";
            string regexPos = "^\\d{5}$";
            string regexCity = "^[A-Za-z]+(?:[\\s-][A-Za-z]+)*";
            string regexState = "^[A-Za-z]+(?:[\\s-][A-Za-z]+)*";
            

            if (Regex.IsMatch(add1, regexAdd) &&
                Regex.IsMatch(add2, regexAdd) &&
                Regex.IsMatch(city, regexCity) &&
                Regex.IsMatch(state, regexState) &&
                Regex.IsMatch(poscode, regexPos) )
            {
                //if all matches
                return true;
            }
            else
            {
                return false;
            }


        }

        protected bool validationCard(string cardNo, string goodThru, string cvv)
        {
            string regexCardNo = "^\\d{4}\\s\\d{4}\\s\\d{4}\\s\\d{4}$";
            string regexGoodThru = "^(0[1-9]|1[0-2])\\/(0[1-9]|[12][0-9]|3[01])$";
            string regexCvv = "^^[0-9]{3}$";

            if (Regex.IsMatch(cardNo, regexCardNo) &&
                Regex.IsMatch(goodThru, regexGoodThru) &&
                Regex.IsMatch(cvv, regexCvv))
            {
                //if all matches
                return true;
            }
            else
            {
                return false;
            }
        }

        protected bool accountExistenceCheck(string email)
        {
            CUSTOMER cust = db.CUSTOMERs.SingleOrDefault(c => c.EMAIL == email);

            ////retrieve user
            if (cust != null)
            {
                //custom validator
                cvAccountDuplicate.IsValid = false;
                
                return false;
            }
            return true;
        }

        protected bool usernameExistenceCheck(string username)
        {
            CUSTOMER cust = db.CUSTOMERs.SingleOrDefault(c => c.USERNAME == username);
            ////retrieve user
            if (cust != null)
            {
                //custom validator
                cvUsernameDuplicate.IsValid = false;
                return false;
            }
            return true;
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