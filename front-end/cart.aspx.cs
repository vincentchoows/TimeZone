using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;


namespace TimeZone_Assign.front_end
{
    public partial class cart : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string userID = (string)Session["UserID"];
                    string sessionKey = "CartItems_" + userID; // create the session key based on the user ID

                    List<order_item> orderItems = (List<order_item>)Session[sessionKey] ?? new List<order_item>();

                    double subtotal = 0;
                    foreach (var item in orderItems)
                    {
                        subtotal += item.subtotal;
                    }

                    double tax = subtotal * 10 / 100;
                    double total = subtotal + tax;

                    // Set the values in the UI
                    lblSubtotal.Text = subtotal.ToString("#,##0.00");
                    lblTax.Text = tax.ToString("#,##0.00");
                    lblTotal.Text = total.ToString("#,##0.00");

                    cartItemsRepeater.DataSource = orderItems;
                    cartItemsRepeater.DataBind();

                    bool user_login = true;
                   
                    if (user_login == true)
                    {
                        ViewState["login"] = true;

                        
                    }
                    else
                    {
                        ViewState["login"] = false;
                    }

                    if (orderItems.Count == 0)
                    {
                        ViewState["empty"] = true;
                    }
                    else
                    {
                        ViewState["empty"] = false;
                    }
                }
                catch (Exception ex)
                {
                    Response.Redirect("~/err/page-not-found.html");
                }

            }
            


        }
       
        protected void checkout_Click(object sender, EventArgs e)
        {

            Response.Redirect("checkout.aspx");
        }

        protected void btnPlus_Click(object sender, EventArgs e)
        {
            Button btnPlus = (Button)sender;
            RepeaterItem updateItem = (RepeaterItem)btnPlus.NamingContainer;
            string watchId = btnPlus.CommandArgument;


            string userID = (string)Session["UserID"];
            string sessionKey = "CartItems_" + userID; // create the session key based on the user ID

            // Get the list of cart items from the session
            List<order_item> cartItems = (List<order_item>)Session[sessionKey];

            order_item itemToPlusQty = cartItems.Single(item => item.watch_id == watchId);

            //validation
            string sql = "SELECT GALLERY.*, CATEGORY.*, WATCH.* FROM CATEGORY INNER JOIN WATCH ON CATEGORY.CATEGORY_ID = WATCH.CATEGORY_ID INNER JOIN GALLERY ON WATCH.GALLERY_ID = GALLERY.GALLERY_ID WHERE WATCH.WATCH_ID = @id";

            
            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@id", watchId);

            conn.Open();

            SqlDataReader dr = cmd.ExecuteReader();

            int stockQty = 0;

            if (dr.Read())
            {
                stockQty = (int)dr["stock_qty"];

                if (itemToPlusQty.qty < stockQty)
                {
                    // Increase the quantity of the item by 1
                    itemToPlusQty.qty++;

                    Response.Redirect(Request.RawUrl);
                }
                else
                {
                    string script = "alert('The maximum quantity for this item has been reached.');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", script, true);
                }

            }

            dr.Close();
            


        }

        protected void btnMinus_Click(object sender, EventArgs e)
        {
            Button btnPlus = (Button)sender;
            RepeaterItem updateItem = (RepeaterItem)btnPlus.NamingContainer;
            string watchId = btnPlus.CommandArgument;

            string userID = (string)Session["UserID"];
            string sessionKey = "CartItems_" + userID; // create the session key based on the user ID

            // Get the list of cart items from the session
            List<order_item> cartItems = (List<order_item>)Session[sessionKey];

            
            order_item itemToMinusQty = cartItems.Single(item => item.watch_id == watchId);

            
            itemToMinusQty.qty--;

            if(itemToMinusQty.qty == 0)
            {
                
                cartItems.Remove(itemToMinusQty);
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('The item has been removed.'); window.location.href = 'cart.aspx';", true);

            }
            else
            {
                // Reload the page to reflect the updated cart
                Response.Redirect(Request.RawUrl);
            }

            
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {

            Button btnRemove = (Button)sender;

            string watchId = btnRemove.CommandArgument;

            RepeaterItem removeItem = (RepeaterItem)btnRemove.NamingContainer;


            string userID = (string)Session["UserID"];
            string sessionKey = "CartItems_" + userID; // create the session key based on the user ID

            // Remove the item from the session
            List<order_item> cartItems = (List<order_item>)Session[sessionKey];

            order_item itemToRemove = cartItems.Single(item => item.watch_id == watchId);

            cartItems.Remove(itemToRemove);

            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('The item has been removed.'); window.location.href = 'cart.aspx';", true);

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("user-login.aspx");
        }

        protected void btnShop_Click(object sender, EventArgs e)
        {
            Response.Redirect("shop.aspx");
        }

    }
}