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
using System.Xml.Linq;

namespace TimeZone_Assign.back_end.record.staff
{
    public partial class EditStaff : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        watchEntities2 db = new watchEntities2();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                string empId = Request.QueryString["EMPLOYEE_ID"].ToString();

                string sql = "SELECT * FROM EMPLOYEE WHERE EMPLOYEE_ID = @empId";

                SqlCommand cmd = new SqlCommand(sql, conn);

                conn.Open();

                cmd.Parameters.AddWithValue("@empId", empId);
                SqlDataReader reader = cmd.ExecuteReader();

                reader.Read();

                lblEmpID.Text = empId;
                lblRoleID.Text = reader["ROLE_ID"].ToString();
                txtName.Text = reader["NAME"].ToString();
                string ddlPositionValue = reader["POSITION"].ToString();

                foreach (ListItem item in ddlPosition.Items)
                {
                    if (item.Value.ToLower() == ddlPositionValue.ToLower())
                    {
                        item.Selected = true;
                    }
                }

                txtEmail.Text = reader["EMAIL"].ToString();
                txtPhone.Text = reader["PHONE"].ToString();
                txtUsername.Text = reader["USERNAME"].ToString();
                //change text mode to single line
                txtPassword.TextMode = TextBoxMode.SingleLine;
                //set the text property to its original value to show the entered text
                txtPassword.Text = reader["PASSWORD"].ToString();

                lblDate.Text = reader["DATE_REGISTERED"].ToString();

                string roleId = reader["ROLE_ID"].ToString();
                reader.Close();

                string sql2 = "SELECT * FROM ROLE WHERE ROLE_ID = @roleId";
                SqlCommand cmd2 = new SqlCommand(sql2, conn);

                cmd2.Parameters.AddWithValue("@roleId", roleId);
                SqlDataReader r1 = cmd2.ExecuteReader();
                r1.Read();

                //button selected automatic compare to database
                string create = r1["CREATE_ACCESS"].ToString();
                if (create.ToString().ToUpper() == "Y")
                {
                    rblCreate.Items.FindByValue("Y").Selected = true;
                }
                else
                {
                    rblCreate.Items.FindByValue("N").Selected = true;
                }

                string delete = r1["DELETE_ACCESS"].ToString();
                if (delete.ToString().ToUpper() == "Y")
                {
                    rblDelete.Items.FindByValue("Y").Selected = true;
                }
                else
                {
                    rblDelete.Items.FindByValue("N").Selected = true;
                }

                string edit = r1["EDIT_ACCESS"].ToString();
                if (edit.ToString().ToUpper() == "Y")
                {
                    rblEdit.Items.FindByValue("Y").Selected = true;
                }
                else
                {
                    rblEdit.Items.FindByValue("N").Selected = true;
                }

                String read = r1["READ_ACCESS"].ToString();
                if (read.ToString().ToUpper() == "Y")
                {
                    rblRead.Items.FindByValue("Y").Selected = true;
                }
                else
                {
                    rblRead.Items.FindByValue("N").Selected = true;
                }

                r1.Close();
                conn.Close();
            }
        }

        protected void btnEditStaff_Click(object sender, EventArgs e)
        {
                if (Page.IsValid)
                {
                try
                {
                    conn.Open();

                    //UPDATE EMPLOYEE TABLE
                    string sql = "UPDATE EMPLOYEE SET NAME =@name, POSITION =@position, EMAIL =@email, PHONE =@phone, USERNAME =@username, PASSWORD =@password WHERE EMPLOYEE_ID = @empId";
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    string empId = Request.QueryString["EMPLOYEE_ID"].ToString();

                    cmd.Parameters.AddWithValue("@empId", empId);

                    if (!validateEditStaff(txtName.Text, txtEmail.Text, txtPhone.Text, txtUsername.Text, txtPassword.Text))
                    {
                        return;
                    }
                    cmd.Parameters.AddWithValue("@name", txtName.Text);
                    cmd.Parameters.AddWithValue("@position", ddlPosition.SelectedValue);
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@phone", txtPhone.Text);
                    cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@password", txtPassword.Text);

                    cmd.ExecuteNonQuery();

                    //UPDATE ROLE TABLE
                    string sql2 = "UPDATE ROLE SET CREATE_ACCESS = @createAccess, READ_ACCESS = @readAccess, EDIT_ACCESS = @editAccess, DELETE_ACCESS = @deleteAccess FROM ROLE INNER JOIN EMPLOYEE ON ROLE.ROLE_ID = EMPLOYEE.ROLE_ID";
                    SqlCommand cmd2 = new SqlCommand(sql2, conn);

                    cmd2.Parameters.AddWithValue("@createAccess", rblCreate.SelectedValue);
                    cmd2.Parameters.AddWithValue("@readAccess", rblRead.SelectedValue);
                    cmd2.Parameters.AddWithValue("@editAccess", rblEdit.SelectedValue);
                    cmd2.Parameters.AddWithValue("@deleteAccess", rblDelete.SelectedValue);

                    cmd2.ExecuteNonQuery();

                    conn.Close();

                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Staff successfully updated!'); window.location.href = 'StaffRecord.aspx';", true);

                }
                catch (Exception ex)
                {
                    Response.Redirect("~/err/page-not-found.html");
                }
            }
        }

            protected void btnCancelEditStaff_Click(object sender, EventArgs e)
            {
                Response.Redirect("StaffRecord.aspx");
            }

            protected bool validateEditStaff(string name, string email, string phoneNumber, string username, string password)
            {
                string nameFormat = "^[A-Z][a-zA-Z]*( [A-Z][a-zA-Z]*)*$";
                string emailFormat = "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$";
                string phoneFormat = "^01[0-46-9]-\\d{3}-?\\d{4,6}$";
                string usernameFormat = "^[a-zA-Z][a-zA-Z0-9]*(?:_[a-zA-Z0-9]+)*$";
                string passwordFormat = "^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[^a-zA-Z0-9]).{8,}$";

                if (Regex.IsMatch(name, nameFormat) &&
                    Regex.IsMatch(email, emailFormat) &&
                    Regex.IsMatch(phoneNumber, phoneFormat) &&
                    Regex.IsMatch(username, usernameFormat) &&
                    Regex.IsMatch(password, passwordFormat))
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        

        protected bool usernameExistenceCheck(string username)
        {
            EMPLOYEE cust = db.EMPLOYEEs.SingleOrDefault(c => c.USERNAME == username);
            ////retrieve employee
            if (cust != null)
            {
                //custom validator
                cvUsernameDuplicate.IsValid = false;
                return false;
            }
            return true;
        }


    }
}