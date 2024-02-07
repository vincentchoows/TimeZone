using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TimeZone_Assign.front_end
{
    public partial class collection : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

            if (!Page.IsPostBack)
            {
                try
                {
                    //Retrieve Category ID
                    string categoryId = Request.QueryString["categoryId"];

                    string sql = "SELECT GALLERY.*, CATEGORY.*, WATCH.* FROM CATEGORY INNER JOIN WATCH ON CATEGORY.CATEGORY_ID = WATCH.CATEGORY_ID INNER JOIN GALLERY ON WATCH.GALLERY_ID = GALLERY.GALLERY_ID WHERE CATEGORY.CATEGORY_ID = @id AND WATCH.STATUS = @status";

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@id", categoryId);
                    cmd.Parameters.AddWithValue("@status", true);

                    conn.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    List<Watch> watchList = new List<Watch>();

                    while (dr.Read())
                    {
                        Watch watch = new Watch();
                        watch.id = (string)dr["watch_id"];
                        watch.referenceNo = (string)dr["reference_no"];
                        watch.modelCase = (string)dr["model_case"];
                        watch.imagePath = (string)dr["product_image"];
                        watchList.Add(watch);
                    }

                    dr.Close();

                    RepeaterWatch.DataSource = watchList;
                    RepeaterWatch.DataBind();

                    SqlDataReader dr2 = cmd.ExecuteReader();
                    if (dr2.Read())
                    {
                        imgBanner.ImageUrl = dr2["banner_image"].ToString();
                        lblName.Text = "Rolex " + (string)dr2["name"];
                        string description = (string)dr2["descr"];
                        string[] sentences = description.Split('.');
                        string formattedText = string.Join(".<br/>", sentences);
                        lblDesc.Text = formattedText;
                    }
                }
                catch
                {
                    Response.Redirect("~/err/page-not-found.html");
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        protected void watchBtnClick(object sender, ImageClickEventArgs e)
        {
            ImageButton btn = (ImageButton)sender;
            string watchId = btn.CommandArgument;
            Response.Redirect("watch.aspx?watchId=" + watchId);
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