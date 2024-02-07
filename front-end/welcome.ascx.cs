using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TimeZone_Assign.front_end
{
    public partial class welcome : System.Web.UI.UserControl
    {
        HttpContext ctx = HttpContext.Current;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ctx.User.Identity.IsAuthenticated)
            {
                try
                {
                    USERROLE userrole = (USERROLE)Session["userrole"];
                    txtCurrentUser.Text = userrole.USERNAME;
                }
                catch(Exception ex)
                {
                    txtCurrentUser.Text = ex.ToString();
                }
            }
        }
    }
}