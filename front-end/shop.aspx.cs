using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TimeZone_Assign.front_end
{
    public partial class shop : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void cosmographColl_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("collection.aspx?categoryId=" + "C0001");
        }

        protected void submarinerColl_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("collection.aspx?categoryId=" + "C0002");
        }

        protected void datejustColl_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("collection.aspx?categoryId=" + "C0003");
        }

        protected void explorerColl_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("collection.aspx?categoryId=" + "C0004");
        }
    }
}