using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using timezone.back_end.record.customer;

namespace TimeZone_Assign.front_end
{
    public partial class edit_profile : System.Web.UI.Page
    {
        watchEntities2 db = new watchEntities2();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CUSTOMER custSession = (CUSTOMER)Session["customer"];
                string sessionCustID = "";

                try
                {
                    sessionCustID = custSession.CUSTOMER_ID;
                }
                catch (Exception ex)
                {
                    sessionCustID = "";
                }

                // Retrieve from db
                CUSTOMER customer = db.CUSTOMERs.FirstOrDefault(c => c.CUSTOMER_ID == sessionCustID);
                ADDRESS address = db.ADDRESSes.FirstOrDefault(a => a.CUSTOMER_ID == sessionCustID);
                CARD card = db.CARDs.FirstOrDefault(ca => ca.CUSTOMER_ID == sessionCustID);

                if (customer != null && address != null && card != null)
                {
                    //preload info
                    txtName.Text = customer.NAME;
                    txtUsername.Text = customer.USERNAME;
                    rblGender.Items.FindByValue(customer.GENDER).Selected = true;
                    txtEmail.Text = customer.EMAIL;
                    txtPhoneNo.Text = customer.PHONE;

                    txtAddress1.Text = address.ADDRESS_LINE_1;
                    txtAddress2.Text = address.ADDRESS_LINE_2;
                    txtCity.Text = address.CITY;
                    txtState.Text = address.STATE;
                    txtPostcode.Text = address.POSTCODE.ToString();
                    
                    txtCardNo.Text = card.CARD_NUMBER;
                    txtGoodThru.Text = card.GOODTHRU;
                    txtCvv.Text = card.CVV;
                }
                else
                {
                    return;
                }
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            CUSTOMER sessionCust = (CUSTOMER)Session["customer"];
            CUSTOMER customer = new CUSTOMER();
            ADDRESS address = new ADDRESS();
            CARD card = new CARD();

            try
            {
                customer = db.CUSTOMERs.FirstOrDefault(c => c.CUSTOMER_ID == sessionCust.CUSTOMER_ID);
                address = db.ADDRESSes.FirstOrDefault(ad => ad.CUSTOMER_ID == sessionCust.CUSTOMER_ID);
                card = db.CARDs.FirstOrDefault(ca => ca.CUSTOMER_ID == sessionCust.CUSTOMER_ID);
            }
            catch (Exception ex)
            {
                Response.Redirect("~/err/page-not-found.html");
            }

            //retrieve info ---
            string name = txtName.Text;
            string username = txtUsername.Text;
            string phoneNo = txtPhoneNo.Text;
            string email = txtEmail.Text;
            string gender = rblGender.SelectedValue;

            string cardNo = txtCardNo.Text;
            string goodThru = txtGoodThru.Text;
            string cvv = txtCvv.Text;

            string address1 = txtAddress1.Text;
            string address2 = txtAddress2.Text;
            string postcode = txtPostcode.Text;
            string city = ddlCity.SelectedValue;
            string state = ddlState.SelectedValue;

            //server-side validations
            if (!validationCustomer(name, username, email, phoneNo) &&
               !validationAddress(address1, address2, city, state, postcode) &&
               !validationCard(cardNo, goodThru, cvv))
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


            // Modify the record as needed
            customer.NAME = name;
            customer.USERNAME = username;
            customer.PHONE = phoneNo;
            customer.EMAIL = email;
            customer.GENDER = gender;

            address.ADDRESS_LINE_1 = address1;
            address.ADDRESS_LINE_2 = address2;
            address.POSTCODE = int.Parse(postcode);
            address.CITY = city;
            address.STATE = state;

            card.CARD_NUMBER = cardNo;
            card.GOODTHRU = goodThru;
            card.CVV = cvv;

            db.SaveChanges();
            Response.Redirect("edit-profile.aspx");

            try
            {
                db.SaveChanges();
                Response.Redirect("edit-profile.aspx");
            }
            catch (Exception ex)
            {
                //dont save
                Response.Redirect("~/err/page-not-found.html");
            }

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("edit-profile.aspx");
        }

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