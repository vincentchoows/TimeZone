using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TimeZone_Assign.back_end.record.product
{
    public partial class ProductDetails : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    //Retrieve Watch ID
                    string watchId = Request.QueryString["watchId"];

                    string sql = "SELECT GALLERY.*, CATEGORY.*, WATCH.* FROM CATEGORY INNER JOIN WATCH ON CATEGORY.CATEGORY_ID = WATCH.CATEGORY_ID INNER JOIN GALLERY ON WATCH.GALLERY_ID = GALLERY.GALLERY_ID WHERE WATCH.WATCH_ID = @id";

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@id", watchId);

                    conn.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        lblWatchID.Text = (string)dr["watch_id"];
                        lblCategoryName.Text = (string)dr["name"];
                        lblReferenceNo.Text = (string)dr["reference_no"];
                        lblQtyLeft.Text = dr["stock_qty"].ToString();
                        lblPrice.Text = string.Format("RM {0:N2}", dr["price"]);
                        lblStatus.Text = Convert.ToBoolean(dr["status"]) ? "Active" : "Inactive";
                        lblDesc.Text = (string)dr["descr"];
                        lblModelCase.Text = (string)dr["model_case"];
                        lblBezel.Text = (string)dr["bezel"];
                        lblWaterResistance.Text = (string)dr["water_resistance"];
                        lblMovement.Text = (string)dr["movement"];
                        lblCalibre.Text = (string)dr["calibre"];
                        lblPowerReserve.Text = (string)dr["power_reserve"];
                        lblBracelet.Text = (string)dr["bracelet"];
                        lblDial.Text = (string)dr["dial"];
                        lblCertification.Text = (string)dr["certification"];
                        lblWindingCrown.Text = (string)dr["winding_crown"];
                        imgWallpaper.ImageUrl = (string)dr["category_image"];
                        imgBanner.ImageUrl = (string)dr["banner_image"];
                        imgProduct.ImageUrl = (string)dr["product_image"];
                        imgSlideshow1.ImageUrl = (string)dr["slideshow_img_1"];
                        imgSlideshow2.ImageUrl = (string)dr["slideshow_img_2"];
                        imgSlideshow3.ImageUrl = (string)dr["slideshow_img_3"];
                        imgSlideshow4.ImageUrl = (string)dr["slideshow_img_4"];
                    }

                    conn.Close();
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/err/page-not-found.html");
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