using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TimeZone_Assign.front_end
{
    public partial class profile : System.Web.UI.Page
    {
        watchEntities2 db = new watchEntities2();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            CUSTOMER custSession = (CUSTOMER)Session["customer"];
            string sessionCustID = custSession.CUSTOMER_ID;

            using (var db = new watchEntities2())
            {

                // Retrieve from db
                CUSTOMER customer = db.CUSTOMERs.FirstOrDefault(c => c.CUSTOMER_ID == sessionCustID);
                ADDRESS address = db.ADDRESSes.FirstOrDefault(c => c.CUSTOMER_ID == sessionCustID);
                CARD card = db.CARDs.FirstOrDefault(c => c.CUSTOMER_ID == sessionCustID);

                if (customer != null)
                {
                    lblName.Text = customer.NAME;

                    txtName.Text = customer.NAME;
                    txtUsername.Text = customer.USERNAME;
                    txtGender.Text = customer.GENDER == "M" ? "Male" : "Female";
                    txtEmail.Text = customer.EMAIL;
                    txtPhoneNo.Text = customer.PHONE;

                    txtAddress.Text = address.ADDRESS_LINE_1 + ", " + address.ADDRESS_LINE_2;

                    string cardNo = card.CARD_NUMBER;
                    string lastFourDigits = cardNo.Substring(cardNo.Length - 4);
                    string cardNoDisplay = "**** **** **** " + lastFourDigits;
                    txtCardNo.Text = cardNoDisplay; 

                }
                else
                {
                    return;
                }
            }
        }
    }
}