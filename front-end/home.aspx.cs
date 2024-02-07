using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TimeZone_Assign.front_end
{
    public partial class home : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            EMPLOYEE empSession = (EMPLOYEE)Session["employee"];
            if(empSession != null)
            {
                Response.Redirect("~/back_end/BackEndHomePage.aspx");
            }

            

            string sessionInfo = "";
            foreach (string key in Session.Keys)
            {
                sessionInfo += $"{key}: {Session[key]?.ToString()}<br>";
                //sessionInfo += $"{key}: {Session[key]?.ToString()}<br>";
            }

            

        }

        protected void GoToShopBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("shop.aspx");
        }
    }
}