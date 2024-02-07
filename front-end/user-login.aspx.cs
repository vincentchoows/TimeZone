using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;

namespace TimeZone_Assign.front_end
{
    public partial class user_login : System.Web.UI.Page
    {
        watchEntities2 db = new watchEntities2();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Customer"))
                {
                    Response.Redirect("~/front-end/home.aspx");
                }
                else  //role = "Employee"
                {
                    //Response.Redirect("~/back_end/BackEndHomePage.aspx");
                    Response.Redirect("~/front-end/home.aspx");
                }
            }
        }

    }
}