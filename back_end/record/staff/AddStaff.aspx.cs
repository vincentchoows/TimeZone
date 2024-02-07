using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TimeZone_Assign.front_end;

namespace TimeZone_Assign.back_end.record.staff
{
    public partial class AddStaff : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

        watchEntities2 db = new watchEntities2();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DateTime currentDate = DateTime.Now;
                lblDate.Text = currentDate.ToString("dd-MM-yyyy");
                lblEmpID.Text = GenerateNewEmployeeId();
                lblRoleID.Text = GenerateNewRoleId();
            }
        }

        protected void btnAddStaff_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try {
                //insert data into database
                string sql = "INSERT INTO EMPLOYEE VALUES(@EMPLOYEE_ID, @ROLE_ID, @NAME, @POSITION, @EMAIL, @PHONE, @USERNAME, @PASSWORD, @DATE_REGISTERED, @STATUS)";
                string sql2 = "INSERT INTO ROLE VALUES(@ROLE_ID, @CREATE_ACCESS, @READ_ACCESS, @EDIT_ACCESS, @DELETE_ACCESS)";

                string empID = lblEmpID.Text;
                string roleID = lblRoleID.Text;
                string name = txtName.Text;
                string position = ddlPosition.Text;
                string email = txtEmail.Text;
                string phoneNo = txtPhone.Text;
                string username = txtUsername.Text;
                string password = txtPassword.Text;
                string confirmPassword = txtConfirmPassword.Text;
                DateTime date = DateTime.Now;

                if (!validateAddStaff(name, email, phoneNo, username, password, confirmPassword)) {
                    return;
                } else if (!usernameExistenceCheck(username))
                {
                    return;
                }

                /*
                EMPLOYEE newEmp = new EMPLOYEE
                {
                    EMPLOYEE_ID = empID,
                    ROLE_ID = roleID,
                    NAME = name,
                    POSITION = position,
                    EMAIL = email,
                    PHONE = phoneNo,
                    USERNAME = username,
                    PASSWORD = Security.GetHash(password),
                    //PASSWORD = password,
                    DATE_REGISTERED = date,
                    STATUS = true
                };

                ROLE newRole = new ROLE
                {
                    ROLE_ID = roleID,
                    CREATE_ACCESS = rblCreate.SelectedValue,
                    READ_ACCESS = rblRead.SelectedValue,
                    EDIT_ACCESS = rblEdit.SelectedValue,
                    DELETE_ACCESS = rblDelete.SelectedValue
                };*/

                
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@EMPLOYEE_ID", empID);
                cmd.Parameters.AddWithValue("@ROLE_ID", roleID);
                cmd.Parameters.AddWithValue("@NAME", name);
                cmd.Parameters.AddWithValue("@POSITION", position);
                cmd.Parameters.AddWithValue("@EMAIL", email);
                cmd.Parameters.AddWithValue("@PHONE", phoneNo);
                cmd.Parameters.AddWithValue("@USERNAME", username);
                cmd.Parameters.AddWithValue("@PASSWORD", Security.GetHash(password));
                cmd.Parameters.AddWithValue("@DATE_REGISTERED", date);
                cmd.Parameters.AddWithValue("@STATUS", true);
                
                string create = rblCreate.Text;
                string read = rblRead.Text;
                string edit = rblEdit.Text;
                string delete = rblDelete.Text;

                SqlCommand cmd2 = new SqlCommand(sql2, conn);
                cmd2.Parameters.AddWithValue("@ROLE_ID", roleID);
                cmd2.Parameters.AddWithValue("@CREATE_ACCESS", create);
                cmd2.Parameters.AddWithValue("@READ_ACCESS", read);
                cmd2.Parameters.AddWithValue("@EDIT_ACCESS", edit);
                cmd2.Parameters.AddWithValue("@DELETE_ACCESS", delete);

                conn.Open();
                cmd2.ExecuteNonQuery();
                cmd.ExecuteNonQuery();
                conn.Close();
                
                /*newRole.EMPLOYEEs.Add(newEmp);
                //db.ROLEs.Add(newRole);

                db.ROLEs.Add(newRole);

                db.SaveChanges();*/

                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Staff successfully added!'); window.location.href = 'StaffRecord.aspx';", true);
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/err/page-not-found.html");
                }
            }
        }


        private string GenerateNewEmployeeId()
        {
            // Retrieve the current user ID from the database
            string currentEmployeeId = "";
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                connection.Open();

                // Query the database to get the current user ID
                string sql = "SELECT MAX(EMPLOYEE_ID) FROM EMPLOYEE";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        currentEmployeeId = result.ToString();
                    }
                }

                connection.Close();
            }

            // Increment the current user ID and return the new employee ID
            int currentId = 0;
            if (Int32.TryParse(currentEmployeeId.Replace("E", ""), out currentId))
            {
                currentId++; // Increment the current ID
                return "E" + currentId.ToString().PadLeft(4, '0'); // Generate the new user ID with leading zeros
            }
            else
            {
                // If the current user ID is not in the expected format, return a default value
                return null;
            }

        }

        private string GenerateNewRoleId()
        {
            // Retrieve the current user ID from the database
            string currentRoleId = "";
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                connection.Open();

                // Query the database to get the current user ID
                string sql = "SELECT MAX(ROLE_ID) FROM ROLE";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        currentRoleId = result.ToString();
                    }
                }

                connection.Close();
            }

            // Increment the current user ID and return the new employee ID
            int currentId = 0;
            if (Int32.TryParse(currentRoleId.Replace("RL", ""), out currentId))
            {
                currentId++; // Increment the current ID
                return "RL" + currentId.ToString().PadLeft(4, '0'); // Generate the new user ID with leading zeros
            }
            else
            {
                return null;
            }
        }

        protected void btnCancelAddStaff_Click(object sender, EventArgs e)
        {
            Response.Redirect("StaffRecord.aspx");
        }

        protected bool validateAddStaff(string name, string email, string phoneNumber, string username, string password, string confirmPassword)
        {
            string nameFormat = "^[A-Z][a-zA-Z]*( [A-Z][a-zA-Z]*)*$";
            string emailFormat = "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$";
            string phoneFormat = "^01[0-46-9]-\\d{3}-?\\d{4,6}$";
            string usernameFormat = "^[a-zA-Z][a-zA-Z0-9]*(?:_[a-zA-Z0-9]+)*$";
            string passwordFormat = "^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[^a-zA-Z0-9]).{8,}$";

            if(Regex.IsMatch(name, nameFormat) &&
                Regex.IsMatch(email, emailFormat) &&
                Regex.IsMatch(phoneNumber, phoneFormat) &&
                Regex.IsMatch(username, usernameFormat) &&
                Regex.IsMatch(password, passwordFormat) && password.Equals(confirmPassword))
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        protected bool accountExistenceCheck(string email)
        {
            EMPLOYEE emp = db.EMPLOYEEs.SingleOrDefault(c => c.EMAIL == email);

            ////retrieve user
            if (emp != null)
            {
                //custom validator
                cvAccountDuplicate.IsValid = false;

                return false;
            }
            return true;
        }

        protected bool usernameExistenceCheck(string username)
        {
            EMPLOYEE emp = db.EMPLOYEEs.SingleOrDefault(c => c.USERNAME == username);
            ////retrieve employee
            if (emp != null)
            {
                //custom validator
                cvUsernameDuplicate.IsValid = false;
                return false;
            }
            return true;
        }
    }
}