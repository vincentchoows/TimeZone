using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace TimeZone_Assign.front_end
{
    public partial class loginpage : System.Web.UI.UserControl
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
                USERROLE u = new USERROLE();
                CUSTOMER cust = new CUSTOMER();

                try
                {
                    //retrieve from db
                    u = db.USERROLEs.SingleOrDefault(
                        user => user.USERNAME == username
                        );

                    cust = db.CUSTOMERs.SingleOrDefault(user => user.USERNAME == username);


                }
                catch (Exception ex)
                {
                    Response.Redirect("~/err/page-not-found.html");
                }

                //compare hashede password
                bool passwordMatches = Encryption.VerifyPassword(password, u.PASSWORD);
                if (passwordMatches)
                {
                    string role = u.ROLE;
                    if (role == "Customer")
                    {
                        

                        //check acc status
                        if (!(bool)cust.STATUS)
                        {
                            cvAccountDeactivated.IsValid = false;
                        }
                        else
                        {
                            //set session
                            cust.PASSWORD = null;
                            Session["customer"] = cust;
                            Session["userrole"] = u;
                            Session["loginSession"] = true;
                            Session["UserID"] = cust.CUSTOMER_ID;

                            Security.LoginUser(u.USERNAME, u.ROLE, false);
                        }
                    }
                    else if (role == "Employee")
                    {
                        EMPLOYEE emp = db.EMPLOYEEs.SingleOrDefault(userEmp => userEmp.USERNAME == username);
                        emp.PASSWORD = null;
                        Session["employee"] = emp;
                        Session["userrole"] = u;
                        Session["loginSession"] = true;
                        Session["UserID"] = emp.EMPLOYEE_ID;

                        Security.LoginUser(u.USERNAME, u.ROLE, false);
                        Response.Redirect("~/back_end/BackEndHomePage.aspx");

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