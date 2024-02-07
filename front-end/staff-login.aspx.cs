using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TimeZone_Assign.front_end
{
    public partial class staff_login : System.Web.UI.Page
    {
        watchEntities2 db = new watchEntities2();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogIn_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {

                string username = txtUsername.Text;
                string password = txtPassword.Text;

                string hash = Security.GetHash(password);
                USERROLE u = new USERROLE();
                EMPLOYEE emp = new EMPLOYEE();


                try
                {
                    u = db.USERROLEs.SingleOrDefault(
                    user => user.USERNAME == username && user.PASSWORD == hash);
                    emp = db.EMPLOYEEs.SingleOrDefault(userEmp => userEmp.USERNAME == username);
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/err/page-not-found.html");
                }

                if (u != null)
                    {
                        string role = u.ROLE;
                        if (role == "Employee")
                        {
                            emp.PASSWORD = null;
                            Session["employee"] = emp;
                            Session["userrole"] = u;
                            Session["loginSession"] = true;
                            Session["UserID"] = emp.EMPLOYEE_ID;

                            Security.LoginUser(u.USERNAME, u.ROLE, false);

                        }
                        else
                        {
                            cvInvalidLogin.IsValid = false;
                        }
                    }
                    else
                    {
                        cvNotMatched.IsValid = false;
                    }
            }
        }
    }
}