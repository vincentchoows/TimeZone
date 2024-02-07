using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//step 1: import
using System.Data.SqlClient;
using System.Configuration;
using System.Net.NetworkInformation;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Security.Policy;
using System.Reflection.Emit;
using BCrypt.Net;
using System.Data.Entity.Validation;

namespace TimeZone_Assign.front_end
{
    public partial class register : System.Web.UI.Page
    {

        watchEntities2 db = new watchEntities2();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

            }
        }
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            //step 3: retreive all user input
            if (Page.IsValid)
            {
                //generate 3 IDs ---
                string sqlCust = "SELECT COUNT(*) FROM CUSTOMER";
                SqlCommand cmdID = new SqlCommand(sqlCust, conn);
                conn.Open();
                int count = (int)cmdID.ExecuteScalar();
                count++;
                string newCustID = "CU" + count++.ToString("D4");
                conn.Close();

                string sqlAddress = "SELECT COUNT(*) FROM ADDRESS";
                cmdID = new SqlCommand(sqlAddress, conn);
                conn.Open();
                int count2 = (int)cmdID.ExecuteScalar();
                count2++;
                string newAddressID = "A" + count2++.ToString("D4");
                conn.Close();

                string sqlCard = "SELECT COUNT(*) FROM CARD";
                cmdID = new SqlCommand(sqlCard, conn);
                conn.Open();
                int count3 = (int)cmdID.ExecuteScalar();
                count3++;
                string newCardID = "C" + count3++.ToString("D4");
                conn.Close();

                //retrieve info ---
                string name = txtName.Text;
                string username = txtUsername.Text;
                string password = txtPassword.Text;
                string phoneNo = txtPhoneNo.Text;
                string email = txtEmail.Text;
                string gender = rblGender.SelectedValue;
                DateTime date = DateTime.Now;

                string cardNo = txtCardNo.Text;
                string goodThru = txtGoodThru.Text;
                string cvv = txtCvv.Text;

                string address1 = txtAddress1.Text;
                string address2 = txtAddress2.Text;
                string postcode = txtPoscode.Text;
                string city = ddlCity.SelectedValue;
                string state = ddlState.SelectedValue;

                //server-side validations
                if (!validationCustomer(name, username, email, phoneNo, password) &&
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

                //encrypt the password bcrypt
                string hashedPassword = Encryption.HashPassword(password);

                //creat class
                CUSTOMER newCust = new CUSTOMER
                {
                    CUSTOMER_ID = newCustID,
                    NAME = name,
                    GENDER = gender,
                    EMAIL = email,
                    PHONE = phoneNo,
                    USERNAME = username,
                    PASSWORD = hashedPassword,
                    DATE_REGISTERED = date,
                    STATUS = true

                };

                Session["hashedPassword"] = hashedPassword;

                ADDRESS newAddress = new ADDRESS
                {
                    ADDRESS_ID = newAddressID,
                    ADDRESS_LINE_1 = address1,
                    ADDRESS_LINE_2 = address2,
                    POSTCODE = int.Parse(postcode),
                    CITY = city,
                    STATE = state
                };

                CARD newCard = new CARD
                {
                    CARD_ID = newCardID,
                    CARD_NUMBER = cardNo,
                    GOODTHRU = goodThru,
                    CVV = cvv
                };

                newCust.ADDRESSes.Add(newAddress);
                newCust.CARDs.Add(newCard);

                db.CUSTOMERs.Add(newCust);
                db.ADDRESSes.Add(newAddress);
                db.CARDs.Add(newCard);

                try
                {
                    db.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var result in ex.EntityValidationErrors)
                    {
                        foreach (var error in result.ValidationErrors)
                        {
                            //Console.WriteLine("{0}: {1}", error.PropertyName, error.ErrorMessage);
                            lblMsg.Text = String.Format("{0}: {1}", error.PropertyName, error.ErrorMessage);

                        }
                    }
                }

                Response.Redirect("user-login.aspx");

            }

        }

        protected bool validationCustomer(string name, string username, string email, string phoneNo, string password )
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
                Regex.IsMatch(phoneNo , regexPhoneNo) && 
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
                Regex.IsMatch(cvv, regexCvv) )
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



    }
}