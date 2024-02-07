using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.Remoting.Channels;
using System.Security.Policy;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace TimeZone_Assign.front_end
{
    public partial class change_password : System.Web.UI.Page
    {
        watchEntities2 db = new watchEntities2();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CUSTOMER custSession = (CUSTOMER)Session["customer"];
                string sessionCustID = custSession.CUSTOMER_ID;

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            CUSTOMER custSession = new CUSTOMER();
            CUSTOMER cust = new CUSTOMER();
            string sessionCustID = "";

            try
            {
                custSession = (CUSTOMER)Session["customer"];
                sessionCustID = custSession.CUSTOMER_ID;
                cust = db.CUSTOMERs.SingleOrDefault(c => c.CUSTOMER_ID == sessionCustID);
            }
            catch (Exception ex)
            {
                sessionCustID = "";
            }

            //retrieve info
            string currentPswd = txtCurrentPswd.Text;
            string newPswd = txtNewPswd.Text;
            string confirmPswd = txtConfirmPswd.Text;

            //server validation
            if (!validationPassword(currentPswd, newPswd, confirmPswd))
            {
                return;
            }

            //compare hashed password
            bool passwordMatches = Encryption.VerifyPassword(currentPswd, cust.PASSWORD);
            if (passwordMatches)
            {
                string hash = Encryption.HashPassword(confirmPswd);
                cust.PASSWORD = hash;

                try
                {
                    db.SaveChanges();
                    Response.Redirect("edit-profile.aspx");
                }
                catch (Exception ex)
                {
                    //Response.Redirect("~/err/page-not-found.html");
                    Response.Redirect("edit-profile.aspx");
                }

                
            }
            else
            {
                cvNotMatched.IsValid = false;
            }

        }

        protected bool validationPassword(string currentPswd, string newPswd, string confirmPswd)
        {
            //name, username, email, phoneNO,password
            string regexPswd = "^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[^a-zA-Z0-9]).{8,}$";

            if (Regex.IsMatch(currentPswd, regexPswd) &&
                Regex.IsMatch(newPswd, regexPswd) &&
                Regex.IsMatch(confirmPswd, regexPswd))
            {
                //if all matches
                return true;
            }
            else
            {
                return false;
            }


        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("change-password.aspx");
        }
    }
}