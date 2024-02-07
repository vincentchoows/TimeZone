 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TimeZone_Assign.front_end
{
    public partial class logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            
                Session.Remove("customer");
                Session.Remove("userrole");
                Session.Remove("loginSession");
                Session.Remove("UserID");

                FormsAuthentication.SignOut();
                Response.Redirect("~/front-end/home.aspx");
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                      
        }
    }
}