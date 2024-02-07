using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TimeZone_Assign.back_end.record.product
{
    public partial class EditProduct : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    conn.Open();

                    //Append Category DDL
                    string sql = "SELECT * FROM CATEGORY";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        string category = dr["name"].ToString();
                        ddlCategory.Items.Add(new ListItem(category, category));
                    }

                    dr.Close();

                    //FILL UP THE FORM WITH DEFAULT/ORIGINAL DATA
                    //Retrieve Watch ID
                    string watchId = Request.QueryString["watchId"];
                    string sql2 = "SELECT GALLERY.*, CATEGORY.*, WATCH.* FROM CATEGORY INNER JOIN WATCH ON CATEGORY.CATEGORY_ID = WATCH.CATEGORY_ID INNER JOIN GALLERY ON WATCH.GALLERY_ID = GALLERY.GALLERY_ID WHERE WATCH.WATCH_ID = @id";
                    SqlCommand cmd2 = new SqlCommand(sql2, conn);
                    cmd2.Parameters.AddWithValue("@id", watchId);

                    SqlDataReader dr2 = cmd2.ExecuteReader();

                    if (dr2.Read())
                    {
                        tbWatchID.Text = (string)dr2["watch_id"];
                        tbReferenceNo.Text = (string)dr2["reference_no"];
                        tbQtyLeft.Text = dr2["stock_qty"].ToString();
                        tbPrice.Text = dr2["price"].ToString();
                        ddlStatus.SelectedValue = Convert.ToBoolean(dr2["status"]) ? "1" : "0";
                        ddlCategory.SelectedValue = (string)dr2["name"];
                        tbModelCase.Text = (string)dr2["model_case"];
                        tbBezel.Text = (string)dr2["bezel"];
                        tbWaterResistance.Text = (string)dr2["water_resistance"];
                        tbMovement.Text = (string)dr2["movement"];
                        tbCalibre.Text = (string)dr2["calibre"];
                        tbPowerReserve.Text = (string)dr2["power_reserve"];
                        tbBracelet.Text = (string)dr2["bracelet"];
                        tbDial.Text = (string)dr2["dial"];
                        tbCertification.Text = (string)dr2["certification"];
                        tbWindingCrown.Text = (string)dr2["winding_crown"];
                        imgProd.ImageUrl = (string)dr2["product_image"];
                        imgSs1.ImageUrl = (string)dr2["slideshow_img_1"];
                        imgSs2.ImageUrl = (string)dr2["slideshow_img_2"];
                        imgSs3.ImageUrl = (string)dr2["slideshow_img_3"];
                        imgSs4.ImageUrl = (string)dr2["slideshow_img_4"];
                    }

                    dr2.Close();
                    conn.Close();
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/err/page-not-found.html");
                }
            }
        }

        protected void confirmBtn_Click(object sender, EventArgs e)
        {
            try
            {
                //UPDATE INTO DATABASE
                conn.Open();

                //GET CATEGORY ID
                string sql3 = "SELECT CATEGORY_ID FROM CATEGORY WHERE NAME = @name";
                SqlCommand cmd3 = new SqlCommand(sql3, conn);
                cmd3.Parameters.AddWithValue("@name", ddlCategory.SelectedItem.Value);
                string categoryID = (string)cmd3.ExecuteScalar();

                //UPDATE WATCH TABLE
                string editProdTableSql = "UPDATE WATCH SET CATEGORY_ID =@categoryId, REFERENCE_NO =@referenceNo, MODEL_CASE =@modelCase, BEZEL =@bezel, WATER_RESISTANCE =@waterResistance, MOVEMENT =@movement, CALIBRE =@calibre, POWER_RESERVE =@powerReserve, BRACELET =@bracelet, DIAL =@dial, CERTIFICATION =@certification, WINDING_CROWN =@windingCrown, STOCK_QTY =@stockQty, PRICE =@price, STATUS =@status WHERE WATCH_ID = @watchId";
                SqlCommand editProdTableCmd = new SqlCommand(editProdTableSql, conn);

                editProdTableCmd.Parameters.AddWithValue("@watchId", tbWatchID.Text);
                editProdTableCmd.Parameters.AddWithValue("@categoryId", categoryID);
                editProdTableCmd.Parameters.AddWithValue("@referenceNo", tbReferenceNo.Text);
                editProdTableCmd.Parameters.AddWithValue("@modelCase", tbModelCase.Text);
                editProdTableCmd.Parameters.AddWithValue("@bezel", tbBezel.Text);
                editProdTableCmd.Parameters.AddWithValue("@waterResistance", tbWaterResistance.Text);
                editProdTableCmd.Parameters.AddWithValue("@movement", tbMovement.Text);
                editProdTableCmd.Parameters.AddWithValue("@calibre", tbCalibre.Text);
                editProdTableCmd.Parameters.AddWithValue("@powerReserve", tbPowerReserve.Text);
                editProdTableCmd.Parameters.AddWithValue("@bracelet", tbBracelet.Text);
                editProdTableCmd.Parameters.AddWithValue("@dial", tbDial.Text);
                editProdTableCmd.Parameters.AddWithValue("@certification", tbCertification.Text);
                editProdTableCmd.Parameters.AddWithValue("@windingCrown", tbWindingCrown.Text);
                editProdTableCmd.Parameters.AddWithValue("@stockQty", int.Parse(tbQtyLeft.Text));
                editProdTableCmd.Parameters.AddWithValue("@price", float.Parse(tbPrice.Text));
                editProdTableCmd.Parameters.AddWithValue("@status", ddlStatus.SelectedItem.Value);

                editProdTableCmd.ExecuteNonQuery();

                //UPDATE GALLERY TABLE
                string editGalTableSql = "UPDATE GALLERY SET PRODUCT_IMAGE =@prodImg, SLIDESHOW_IMG_1 =@ss1Img, SLIDESHOW_IMG_2 =@ss2Img, SLIDESHOW_IMG_3 =@ss3Img, SLIDESHOW_IMG_4 =@ss4Img FROM GALLERY INNER JOIN WATCH ON GALLERY.GALLERY_ID = WATCH.GALLERY_ID WHERE WATCH_ID = @watchId";
                SqlCommand editGalTableCmd = new SqlCommand(editGalTableSql, conn);

                editGalTableCmd.Parameters.AddWithValue("@watchId", tbWatchID.Text);

                //
                if (fileUploadProd.HasFile)
                {
                    editGalTableCmd.Parameters.AddWithValue("@prodImg", saveImageToDB(fileUploadProd));
                }
                else
                {
                    editGalTableCmd.Parameters.AddWithValue("@prodImg", imgProd.ImageUrl);
                }

                //
                if (fileUploadSs1.HasFile)
                {
                    editGalTableCmd.Parameters.AddWithValue("@ss1Img", saveImageToDB(fileUploadSs1));
                }
                else
                {
                    editGalTableCmd.Parameters.AddWithValue("@ss1Img", imgSs1.ImageUrl);
                }

                //
                if (fileUploadSs2.HasFile)
                {
                    editGalTableCmd.Parameters.AddWithValue("@ss2Img", saveImageToDB(fileUploadSs2));
                }
                else
                {
                    editGalTableCmd.Parameters.AddWithValue("@ss2Img", imgSs2.ImageUrl);
                }

                //
                if (fileUploadSs3.HasFile)
                {
                    editGalTableCmd.Parameters.AddWithValue("@ss3Img", saveImageToDB(fileUploadSs3));
                }
                else
                {
                    editGalTableCmd.Parameters.AddWithValue("@ss3Img", imgSs3.ImageUrl);
                }

                //
                if (fileUploadSs4.HasFile)
                {
                    editGalTableCmd.Parameters.AddWithValue("@ss4Img", saveImageToDB(fileUploadSs4));
                }
                else
                {
                    editGalTableCmd.Parameters.AddWithValue("@ss4Img", imgSs4.ImageUrl);
                }

                editGalTableCmd.ExecuteNonQuery();

                //Successful pop up msg and redirect user to record page
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Watch successfully updated!'); window.location.href = 'ProductRecord.aspx';", true);

                conn.Close();
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
                    //Generate folder path
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