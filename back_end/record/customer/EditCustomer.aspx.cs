using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.EnterpriseServices;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TimeZone_Assign;
using TimeZone_Assign.front_end;

namespace timezone.back_end.record.customer
{
    public partial class EditCustomer : System.Web.UI.Page
    {
        watchEntities2 db = new watchEntities2();
        
        
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    string id = Request.QueryString["custId"] ?? "";
                    //string id = "CU0001";

                    string sql = "SELECT * FROM CUSTOMER WHERE CUSTOMER_ID = @id";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Id", id);

                    string sql2 = "SELECT * FROM ADDRESS WHERE CUSTOMER_ID = @id2";
                    SqlCommand cmd2 = new SqlCommand(sql2, conn);
                    cmd2.Parameters.AddWithValue("@Id2", id);

                    string sql3 = "SELECT * FROM CARD WHERE CUSTOMER_ID = @id3";
                    SqlCommand cmd3 = new SqlCommand(sql3, conn);
                    cmd3.Parameters.AddWithValue("@Id3", id);


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

                        ListItem item = rblGender.Items.FindByValue(gender);
                        if (item != null)
                        {
                            item.Selected = true;
                        }

                        tbEmail.Text = email;
                        tbPhone.Text = phone;
                        tbUsername.Text = username;
                        tbPassword.Text = password;
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


        protected void confirmBtn_Click(object sender, EventArgs e)
        {
            //step 3: retreive all user input
            if (Page.IsValid)
            {
                string requestID = Request.QueryString["custId"] ?? "";

                ADDRESS address = db.ADDRESSes.SingleOrDefault(a => a.CUSTOMER_ID == requestID);

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
                if (!validationCustomer(name, username, email, phone) &&
                   !validationAddress(address1, address2, city, state, postcode) &&
                   !validationCard(cardNo, goodThru, cvv))
                {
                    return;
                }
                //account existence check 
                if (!accountExistenceCheck(id, email))
                {
                    return;
                }
                //username existence check 
                if (!usernameExistenceCheck(id, username))
                {
                    return;
                }

                //if city is unchanged
                if (city.Contains("Select a city"))
                {
                    city = address.CITY;
                }

                //if state is unchanged
                if (state.Contains("Select a state"))
                {
                    state = address.STATE;
                }



                //step 4: insert sql statemetn
                string sql = "UPDATE ADDRESS SET CUSTOMER_ID = @custId, ADDRESS_LINE_1 =@address1, ADDRESS_LINE_2 =@address2, POSTCODE =@postcode, CITY = @city, STATE =@state WHERE ADDRESS_ID = @addressId";

                string sql2 = "UPDATE CARD SET CUSTOMER_ID =@custId2, CARD_NUMBER =@cardNo, GOODTHRU =@goodThru, CVV =@cvv WHERE CARD_ID = @cardId";

                string sql3 = "";

                //if password was unchanged
                if(password == "")
                {
                    sql3 = "UPDATE CUSTOMER SET CUSTOMER_ID =@custId3, NAME =@name, GENDER =@gender, EMAIL =@email, PHONE =@phone, USERNAME =@username, PASSWORD = PASSWORD WHERE CUSTOMER_ID = @custId3";
                }
                else
                {
                    sql3 = "UPDATE CUSTOMER SET CUSTOMER_ID =@custId3, NAME =@name, GENDER =@gender, EMAIL =@email, PHONE =@phone, USERNAME =@username, PASSWORD =@password WHERE CUSTOMER_ID = @custId3";
                }


                //step 5: sql connection
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

                //step 6: execute cmd
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlCommand cmd2 = new SqlCommand(sql2, conn);
                SqlCommand cmd3 = new SqlCommand(sql3, conn);

                //step 6.1: supply parameter

                //string sql = "UPDATE ADDRESS SET CUSTOMER_ID = @custId, ADDRESS_LINE_1 =@address1, ADDRESS_LINE_2 =@address2, POSTCODE =@postcode, CITY = @city, STATE =@state WHERE ADDRESS_ID = @addressId";

                cmd.Parameters.AddWithValue("@custId", id);
                cmd.Parameters.AddWithValue("@address1", address1);
                cmd.Parameters.AddWithValue("@address2", address2);
                cmd.Parameters.AddWithValue("@postcode", postcode);
                cmd.Parameters.AddWithValue("@city", city);
                cmd.Parameters.AddWithValue("@state", state);
                cmd.Parameters.AddWithValue("@addressId", addressId);

                //string sql2 = "UPDATE CARD SET CUSTOMER_ID =@custId2, CARD_NUMBER =@cardNo, GOODTHRU =@goodThru, CVV =@cvv WHERE CARD_ID = @cardId";

                cmd2.Parameters.AddWithValue("@custId2", id);
                cmd2.Parameters.AddWithValue("@cardNo", cardNo);
                cmd2.Parameters.AddWithValue("@goodThru", goodThru);
                cmd2.Parameters.AddWithValue("@cvv", cvv);
                cmd2.Parameters.AddWithValue("@cardId", cardId);

                //string sql3 = "UPDATE CUSTOMER SET NAME =@name, GENDER =@gender, EMAIL =@email, PHONE =@phone, USERNAME =@username, PASSWORD =@password WHERE CUSTOMER_ID = @custId3";
                string hashedPassword = Encryption.HashPassword(password);

                cmd3.Parameters.AddWithValue("@name", name);
                cmd3.Parameters.AddWithValue("@gender", gender);
                cmd3.Parameters.AddWithValue("@email", email);
                cmd3.Parameters.AddWithValue("@phone", phone);
                cmd3.Parameters.AddWithValue("@username", username);
                cmd3.Parameters.AddWithValue("@password", hashedPassword);
                cmd3.Parameters.AddWithValue("@custId3", id);

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
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Customer successfully updated!'); window.location.href = 'CustomerRecord.aspx';", true);
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/err/page-not-found-admin.html");
                }

            }
        }




        protected void cancelBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/back_end/record/customer/CustomerRecord.aspx");
        }

        ////VALIDATIONS==============================================================================

        protected bool validationCustomer(string name, string username, string email, string phoneNo)
        {
            //name, username, email, phoneNO,password
            string regexName = "^[A-Z][a-zA-Z]*( [A-Z][a-zA-Z]*)*$";
            string regexUsername = "^[a-zA-Z][a-zA-Z0-9]*(?:_[a-zA-Z0-9]+)*$";
            string regexEmail = "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$";
            string regexPhoneNo = "^01[0-46-9]-\\d{3}-?\\d{4,6}$";


            if (Regex.IsMatch(name, regexName) &&
                Regex.IsMatch(username, regexUsername) &&
                Regex.IsMatch(email, regexEmail) &&
                Regex.IsMatch(phoneNo, regexPhoneNo))
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
                Regex.IsMatch(poscode, regexPos))
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

        protected bool accountExistenceCheck(string id, string email)
        {
            var customers = db.CUSTOMERs.Where(c => c.CUSTOMER_ID != id).ToList();

            bool isEmailExists = customers.Any(c => c.EMAIL == email);
            if (isEmailExists)
            {
                // email already exists in the list
                cvAccountDuplicate.IsValid = false;
                return false;
            }
            return true;

        }

        protected bool usernameExistenceCheck(string id, string username)
        {
            var customers = db.CUSTOMERs.Where(c => c.CUSTOMER_ID != id).ToList();

            bool isUsernameExists = customers.Any(c => c.USERNAME == username);
            if (isUsernameExists)
            {
                // email already exists in the list
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