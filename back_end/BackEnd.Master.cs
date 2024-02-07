using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TimeZone_Assign.back_end
{
    public partial class BackEnd1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {


            Session.Remove("employee");
            Session.Remove("userrole");
            Session.Remove("loginSession");
            Session.Remove("UserID");

            FormsAuthentication.SignOut();

            Response.Redirect("https://localhost:44397/front-end/staff-login.aspx");
        }

    }
}