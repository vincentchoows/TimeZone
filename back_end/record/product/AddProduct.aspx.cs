using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TimeZone_Assign.front_end;

namespace TimeZone_Assign.back_end.record.product
{
    public partial class AddProduct : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    conn.Open();

                    //Auto Generate WATCH ID
                    string sql1 = "SELECT COUNT(*) FROM WATCH";
                    SqlCommand cmd1 = new SqlCommand(sql1, conn);

                    int count = (int)cmd1.ExecuteScalar();
                    count++;
                    tbWatchID.Text = "WA" + count.ToString("D4");

                    //Append Category DDL
                    string sql2 = "SELECT * FROM CATEGORY";
                    SqlCommand cmd2 = new SqlCommand(sql2, conn);
                    SqlDataReader dr = cmd2.ExecuteReader();

                    while (dr.Read())
                    {
                        string category = dr["name"].ToString();
                        ddlCategory.Items.Add(new ListItem(category, category));
                    }
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/err/page-not-found.html");
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        protected void confirmBtn_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                //INSERT INTO GALLERY TABLE
                //Auto Generate Gallery ID
                string sql1 = "SELECT COUNT(*) FROM GALLERY";
                SqlCommand cmd1 = new SqlCommand(sql1, conn);
                int galleryCount = (int)cmd1.ExecuteScalar();
                galleryCount++;
                string newGalleryID = "GA" + galleryCount.ToString("D4");

                //INSERT STATEMENT
                string sql2 = "INSERT INTO GALLERY(GALLERY_ID, PRODUCT_IMAGE, SLIDESHOW_IMG_1, SLIDESHOW_IMG_2, SLIDESHOW_IMG_3, SLIDESHOW_IMG_4) VALUES(@id, @prodImg, @ssImg1, @ssImg2, @ssImg3, @ssImg4)";
                SqlCommand cmd2 = new SqlCommand(sql2, conn);

                cmd2.Parameters.AddWithValue("@id", newGalleryID);
                cmd2.Parameters.AddWithValue("@prodImg", saveImageToDB(fileImgProduct));
                cmd2.Parameters.AddWithValue("@ssImg1", saveImageToDB(fileImgSlideshow1));
                cmd2.Parameters.AddWithValue("@ssImg2", saveImageToDB(fileImgSlideshow2));
                cmd2.Parameters.AddWithValue("@ssImg3", saveImageToDB(fileImgSlideshow3));
                cmd2.Parameters.AddWithValue("@ssImg4", saveImageToDB(fileImgSlideshow4));

                cmd2.ExecuteNonQuery();

                //GET CATEGORY ID
                string sql3 = "SELECT CATEGORY_ID FROM CATEGORY WHERE NAME = @name";
                SqlCommand cmd3 = new SqlCommand(sql3, conn);
                cmd3.Parameters.AddWithValue("@name", ddlCategory.SelectedItem.Value);
                string categoryID = (string)cmd3.ExecuteScalar();

                //INSERT STATEMENT
                string sql4 = "INSERT INTO WATCH(WATCH_ID, CATEGORY_ID, GALLERY_ID, REFERENCE_NO, MODEL_CASE, BEZEL, WATER_RESISTANCE, MOVEMENT, CALIBRE, POWER_RESERVE, BRACELET, DIAL, CERTIFICATION, WINDING_CROWN, STOCK_QTY, PRICE, STATUS) VALUES (@watchID, @categoryID, @galleryID, @referenceNo, @modelCase, @bezel, @waterResistance, @movement, @calibre, @powerReserve, @bracelet, @dial, @certification, @windingCrown, @stockQty, @price, @status)";
                SqlCommand cmd4 = new SqlCommand(sql4, conn);

                cmd4.Parameters.AddWithValue("@watchID", tbWatchID.Text);
                cmd4.Parameters.AddWithValue("@categoryID", categoryID);
                cmd4.Parameters.AddWithValue("@galleryID", newGalleryID);
                cmd4.Parameters.AddWithValue("@referenceNo", tbReferenceNo.Text);
                cmd4.Parameters.AddWithValue("@modelCase", tbModelCase.Text);
                cmd4.Parameters.AddWithValue("@bezel", tbBezel.Text);
                cmd4.Parameters.AddWithValue("@waterResistance", tbWaterResistance.Text);
                cmd4.Parameters.AddWithValue("@movement", tbMovement.Text);
                cmd4.Parameters.AddWithValue("@calibre", tbCalibre.Text);
                cmd4.Parameters.AddWithValue("@powerReserve", tbPowerReserve.Text);
                cmd4.Parameters.AddWithValue("@bracelet", tbBracelet.Text);
                cmd4.Parameters.AddWithValue("@dial", tbDial.Text);
                cmd4.Parameters.AddWithValue("@certification", tbCertification.Text);
                cmd4.Parameters.AddWithValue("@windingCrown", tbWindingCrown.Text);
                cmd4.Parameters.AddWithValue("@stockQty", int.Parse(tbQtyLeft.Text));
                cmd4.Parameters.AddWithValue("@price", float.Parse(tbPrice.Text));
                cmd4.Parameters.AddWithValue("@status", ddlStatus.SelectedItem.Value);

                cmd4.ExecuteNonQuery();

                conn.Close();

                //Successful pop up msg and redirect user to record page
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Watch successfully added!'); window.location.href = 'ProductRecord.aspx';", true);
            }
            catch (Exception ex)
            {
                Response.Redirect("~/err/page-not-found.html");
            }
        }

        private string saveImageToDB(FileUpload fileUpload)
        {
            if (fileUpload.HasFile)
            {
                //Check if the file is .jpg/.jpeg/.png
                string fileExtension = Path.GetExtension(fileUpload.FileName);
                string filePath = "";

                if (fileExtension.ToLower() == ".jpg" || fileExtension.ToLower() == ".jpeg" || fileExtension.ToLower() == ".png")
                {
                    //Generate file name
                    string category = ddlCategory.SelectedItem.ToString().ToLower();
                    string referenceNo = tbReferenceNo.Text.ToUpper();

                    string fileName = fileUpload.FileName;
                    string folderPath = "~/front-end/assets/img/product/" + category + "/" + referenceNo;

                    //Create new folder
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(Server.MapPath(folderPath));
                    }

                    //Save the file to the folder
                    filePath = folderPath + "/" + fileName;
                    fileUpload.SaveAs(Server.MapPath(filePath));

                    return filePath;
                }
                return "";
            }
            return "";
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