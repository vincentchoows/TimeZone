using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TimeZone_Assign.front_end
{
    public partial class watch : System.Web.UI.Page
    {
        double overallRating = 0.00;
        int reviewCount = 0;
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (!Page.IsPostBack)
            {
                try
                {
                    //Retrieve Watch ID
                    string watchId = Request.QueryString["watchId"];

                    addToCart.CommandArgument = watchId;

                    string sql = "SELECT GALLERY.*, CATEGORY.*, WATCH.* FROM CATEGORY INNER JOIN WATCH ON CATEGORY.CATEGORY_ID = WATCH.CATEGORY_ID INNER JOIN GALLERY ON WATCH.GALLERY_ID = GALLERY.GALLERY_ID WHERE WATCH.WATCH_ID = @id";

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@id", watchId);

                    conn.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        lblBannerName.Text = "rolex " + dr["name"] + " " + dr["reference_no"];
                        imgCarousel1.ImageUrl = dr["slideshow_img_1"].ToString();
                        imgCarousel2.ImageUrl = dr["slideshow_img_2"].ToString();
                        imgCarousel3.ImageUrl = dr["slideshow_img_3"].ToString();
                        imgCarousel4.ImageUrl = dr["slideshow_img_4"].ToString();
                        lblProdName.Text = "rolex " + dr["name"] + " " + dr["reference_no"];

                        double price = Convert.ToDouble(dr["price"]);
                        string formattedPrice = price.ToString("C", CultureInfo.GetCultureInfo("ms-MY"));
                        lblPrice.Text = formattedPrice;

                        lblModelCase.Text = dr["model_case"].ToString();
                        lblBezel.Text = dr["bezel"].ToString();
                        lblWaterResistance.Text = dr["water_resistance"].ToString();
                        lblMovement.Text = dr["movement"].ToString();
                        lblCalibre.Text = dr["calibre"].ToString();
                        lblPowerReserve.Text = dr["power_reserve"].ToString();
                        lblBracelet.Text = dr["bracelet"].ToString();
                        lblDial.Text = dr["dial"].ToString();
                        lblCertification.Text = dr["certification"].ToString();
                        lblWindingCrown.Text = dr["winding_crown"].ToString();
                    }

                    dr.Close();
                    conn.Close();

                    string sql2 = @"
                            SELECT REVIEW.RATING, REVIEW.COMMENT, REPLY.REPLY, CUSTOMER.NAME, REVIEW.REVIEW_DATE 
                            FROM CUSTOMER 
                            INNER JOIN ORDERS ON CUSTOMER.CUSTOMER_ID = ORDERS.CUSTOMER_ID 
                            INNER JOIN ORDER_ITEM ON ORDERS.ORDER_ID = ORDER_ITEM.ORDER_ID 
                            INNER JOIN REVIEW ON ORDER_ITEM.REVIEW_ID = REVIEW.REVIEW_ID 
                            LEFT JOIN REPLY ON REVIEW.REPLY_ID = REPLY.REPLY_ID 
                            INNER JOIN WATCH ON ORDER_ITEM.WATCH_ID = WATCH.WATCH_ID
                            WHERE WATCH.WATCH_ID = @id AND
                            REVIEW.STATUS = @status
                            ";

                    SqlCommand cmd2 = new SqlCommand(sql2, conn);
                    cmd2.Parameters.AddWithValue("@id", watchId);
                    cmd2.Parameters.AddWithValue("@status", true);
                    conn.Open();
                    SqlDataReader dr2 = cmd2.ExecuteReader();
                    List<Review> reviewList = new List<Review>();

                    while (dr2.Read())
                    {
                        Review review = new Review();
                        review.custName = (string)dr2["name"];
                        review.date = ((DateTime)dr2["review_date"]).ToString("yyyy-MM-dd");
                        review.comment = (string)dr2["comment"];
                        review.rating = (int)dr2["rating"];

                        overallRating += review.rating;
                        reviewCount++;

                        object replyObj = dr2["reply"];
                        string reply = replyObj != DBNull.Value ? (string)replyObj : null;

                        if (reply == null)
                            review.reply = "";
                        else
                            review.reply = reply;

                        reviewList.Add(review);
                    }

                    dr2.Close();
                    conn.Close();

                    RepeaterCustomerComment.DataSource = reviewList;
                    RepeaterCustomerComment.DataBind();

                    overallRating = overallRating / reviewCount;
                    overallRating = Math.Round(overallRating, 2);

                    lblProdRating.Text = overallRating.ToString("N2");
                    lblStarRating.Text = GetRatingStars(overallRating);
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/err/page-not-found.html");
                }
            }
        }

        protected string GetRatingStars(double rating)
        {
            StringBuilder sb = new StringBuilder();
            int fullStars = (int)Math.Floor(rating);
            int halfStars = (int)Math.Round(rating - fullStars, MidpointRounding.AwayFromZero);
            int emptyStars = 5 - fullStars - halfStars;

            for (int i = 1; i <= fullStars; i++)
            {
                sb.Append("<i class=\"fa-solid fa-star fa-lg gold\"></i>");
            }
            for (int i = 1; i <= halfStars; i++)
            {
                sb.Append("<i class=\"fa-solid fa-star-half-alt fa-lg gold\"></i>");
            }
            for (int i = 1; i <= emptyStars; i++)
            {
                sb.Append("<i class=\"fa-solid fa-star fa-lg\"></i>");
            }

            return sb.ToString();
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

        protected void addToCart_Click(object sender, EventArgs e)
        {
            // get the product details 
            Button btnAddToCart = (Button)sender;
            string watchId = btnAddToCart.CommandArgument;

            int qty = 1;
            try
            {
                //===============
                //this is for the session that for specific user 
                //===============
                string userID = (string)Session["UserID"]; 

                string sessionKey = "CartItems_" + userID; // create the session key based on the user ID
                List<order_item> orderItems = (List<order_item>)Session[sessionKey] ?? new List<order_item>(); // retrieve the cart items from the session

                string sql = "SELECT GALLERY.*, CATEGORY.*, WATCH.* " +
                    "FROM CATEGORY INNER JOIN WATCH ON CATEGORY.CATEGORY_ID = WATCH.CATEGORY_ID " +
                    "INNER JOIN GALLERY ON WATCH.GALLERY_ID = GALLERY.GALLERY_ID " +
                    "WHERE WATCH.WATCH_ID = @id";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@id", watchId);

                conn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                int stockQty = 0;

                if (dr.Read())
                {
                    stockQty = (int)dr["stock_qty"];

                    order_item existingItem = orderItems.FirstOrDefault(i => i.watch_id == watchId);

                    if (existingItem != null)
                    {
                        if (existingItem.qty < stockQty)
                        {
                            // Item already exists in the cart, increase its quantity
                            existingItem.qty += 1;
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Watch Added Successfully');", true);
                        }
                        else
                        {
                            string script = "alert('The maximum quantity for this item has been reached.');";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", script, true);
                        }
                    }
                    else
                    {
                        if (stockQty == 0)
                        {
                            string script = "alert('This product has been sold out.');";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", script, true);
                        }
                        else
                        {
                            order_item newItem = new order_item
                            {
                                watch_id = watchId,
                                qty = qty,
                                productName = (string)dr["reference_no"],
                                imagePath = (string)dr["product_image"],
                                price = Convert.ToDouble(dr["price"])
                            };

                            orderItems.Add(newItem);
                            Session[sessionKey] = orderItems;
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Watch Added Successfully');", true);
                        }
                        
                    }

                }


                conn.Close();

            }
            catch (Exception ex)
            {
                Response.Redirect("~/err/page-not-found.html");
            }

        }

    }
}