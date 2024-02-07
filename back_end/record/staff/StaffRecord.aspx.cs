using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TimeZone_Assign.back_end.record.staff
{
    public partial class StaffRecord : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                EMPLOYEE empSession = (EMPLOYEE)Session["employee"];
                string emp = empSession.POSITION;
                string empId = empSession.EMPLOYEE_ID;
                string roleId = empSession.ROLE.ROLE_ID;

                string empSql = "SELECT * FROM EMPLOYEE WHERE EMPLOYEE_ID = @empId";
                string sql = "SELECT * FROM ROLE WHERE ROLE_ID = @roleId";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlCommand cmd2 = new SqlCommand(empSql, conn);
                conn.Open();
                cmd2.Parameters.AddWithValue("@empId", empId);
                cmd.Parameters.AddWithValue("@roleId", roleId);

                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                string role = reader["ROLE_ID"].ToString();
                string access = reader["CREATE_ACCESS"].ToString();
                string read = reader["READ_ACCESS"].ToString();
                string edit = reader["EDIT_ACCESS"].ToString();
                string delete = reader["DELETE_ACCESS"].ToString();
                reader.Close();

                SqlDataReader reader2 = cmd2.ExecuteReader();
                reader2.Read();
                string empRole = reader2["ROLE_ID"].ToString();
                reader2.Close();

                string createAccess = "";
                string readAccess = "";
                string editAccess = "";
                string deleteAccess = "";

                if (role == empRole)
                {
                    createAccess = access;
                    readAccess = read;
                    editAccess = edit;
                    deleteAccess = delete;
                }
                //string emp = "Super Admin";
                //new version for merge
                if (emp == "Staff")
                {
                    if (createAccess == "N")
                    {
                        foreach (GridViewRow row in GridView1.Rows)
                        {
                            string position = row.Cells[1].Text;
                            Button btnEdit = (Button)row.FindControl("btnEdit");
                            btnEdit.Enabled = false;
                        }
                    }
                    if (editAccess == "N")
                    {
                        foreach (GridViewRow row in GridView1.Rows)
                        {
                            Button btnEdit = (Button)row.FindControl("btnEdit");
                            btnEdit.Enabled = false;
                        }
                    }
                    if (deleteAccess == "N")
                    {
                        foreach (GridViewRow row in GridView1.Rows)
                        {
                            Button btnHidden = (Button)row.FindControl("btnHidden");
                            btnHidden.Enabled = false;
                        }
                    }
                }
                else if (emp == "Admin")
                {
                    if (createAccess == "N")
                    {
                        addStaffBtn.Enabled = false;
                    }
                    if (editAccess == "N")
                    {
                        foreach (GridViewRow row in GridView1.Rows)
                        {
                            Button btnEdit = (Button)row.FindControl("btnEdit");
                            btnEdit.Enabled = false;
                        }
                    }
                    if (deleteAccess == "N")
                    {
                        foreach (GridViewRow row in GridView1.Rows)
                        {
                            Button btnHidden = (Button)row.FindControl("btnHidden");
                            btnHidden.Enabled = false;
                        }
                    }
                }
                else if (emp == "Super Admin")
                {
                    if (createAccess == "N")
                    {
                        addStaffBtn.Enabled = false;
                    }
                    if (editAccess == "N")
                    {
                        foreach (GridViewRow row in GridView1.Rows)
                        {
                            Button btnEdit = (Button)row.FindControl("btnEdit");
                            btnEdit.Enabled = false;
                        }
                    }
                    if (deleteAccess == "N")
                    {
                        
                        foreach (GridViewRow row in GridView1.Rows)
                        {
                            Button btnHidden = (Button)row.FindControl("btnHidden");
                            btnHidden.Enabled = false;
                        }
                    }
                }
                conn.Close();
            }

        }


        protected void EditStaffbtn_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string empId = button.CommandArgument;

            Response.Redirect("./EditStaff.aspx?EMPLOYEE_ID=" + empId);
        }

        protected void Hiddenbtn_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string staffRecord = button.CommandArgument;
            Boolean status;
            string statusMsg;

            if (button.Text == "Deactivate")
            {
                //Set to Hidden
                status = false;
                statusMsg = "deactivated";
            }
            else
            {
                //Set to Visible
                status = true;
                statusMsg = "activated";
            }

            //UPDATE STATUS OF REVIEW TABLE
            conn.Open();

            string sql = "UPDATE EMPLOYEE SET STATUS = @status WHERE EMPLOYEE_ID = @staffRecord";
            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@staffRecord", staffRecord);
            cmd.ExecuteNonQuery();

            conn.Close();
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Staff successfully " + statusMsg + "!'); window.location.href = 'StaffRecord.aspx';", true);

        }
    }
}