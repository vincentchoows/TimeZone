<%@ Page Title="" Language="C#" MasterPageFile="~/front-end/frontend.Master" AutoEventWireup="true" CodeBehind="cart.aspx.cs" Inherits="TimeZone_Assign.front_end.cart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .card{
            border: none !important;
        }
    </style>
    <!-- Banner -->
    <div class="cart-banner top-banner">
        <div class="txt">MY CART</div>
    </div>
    <div class="container">
        
        <%
            bool login = (bool)ViewState["login"];
            bool empty = (bool)ViewState["empty"];
            if (login == false)
            {




        %>
        <div class="container-fluid mt-100">
            <div class="row">
                <div class="col-md-12">
                    <div class="card">
                        <div class="card-body cart p-5">
                            <div class="col-sm-12 empty-cart-cls text-center"> <img src="https://i.imgur.com/dCdflKN.png" width="130" height="130" class="img-fluid mb-4 mr-3">
                                <h3><strong>Uh oh</strong></h3>
                                <h4>Please login first :)</h4><asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="button cart-submit" OnClick="btnLogin_Click"  />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%
            }
            if (login == true && empty == true)
            {


        %>
        <div class="container-fluid mt-100">
            <div class="row">
                <div class="col-md-12">
                    <div class="card">
                        <div class="card-body cart p-5">
                            <div class="col-sm-12 empty-cart-cls text-center"> <img src="https://i.imgur.com/dCdflKN.png" width="130" height="130" class="img-fluid mb-4 mr-3">
                                <h3><strong>Your Cart is Empty</strong></h3>
                                <h4>Add something to make me happy :)</h4> <asp:Button ID="btnShop" runat="server" Text="Continue Shopping" CssClass="button cart-submit" OnClick="btnShop_Click"  />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%
            }
            else if(login == true && empty == false)
            {
        %>
        <div class="cart-page">
            <table id="cartRow">
                <tr>
                    <th>Product</th>
                    <th>Quantity</th>
                    <th>Subtotal</th>
                </tr>
                <asp:Repeater ID="cartItemsRepeater" runat="server">
                    <ItemTemplate>
                    <tr>
                        <td>
                        <div class="cart-itemInfo">
                            <asp:Image ID="imgProduct" runat="server" Width="80px" Height="80px" ImageUrl='<%# Eval("imagePath") %>' />
                            <div>
                            <p><%# Eval("productName") %></p>
                            <small>Price: <%# Eval("price", "{0:#,##0.00}") %></small>
                            <br />
                            <asp:Button CssClass="btn-remove" runat="server" Text="Remove" id="btnRemove" OnClick="btnRemove_Click" CommandArgument='<%# Eval("watch_id") %>'/>
                            </div>
                        </div>
                        </td>
                        <td>
                        <div class="qty">
                            <asp:Button ID="btnMinus" runat="server" CssClass="minus" Text="-" OnClick="btnMinus_Click" CommandArgument='<%# Eval("watch_id") %>'/>
                            <asp:TextBox ID="txtQuantity" runat="server" CssClass="qty"  Width="30px" Text='<%# Eval("qty") %>' Enabled="false"></asp:TextBox>
                            <asp:Button ID="btnPlus" runat="server" CssClass="plus" Text="+" OnClick="btnPlus_Click" CommandArgument='<%# Eval("watch_id") %>'/>
                        </div>
                        </td>
                        <td><%# Eval("subtotal", "{0:#,##0.00}") %></td>
                    </tr>
                    </ItemTemplate>
                </asp:Repeater>
                
            </table>

            <div class="total-price">
                <table>
                    <tr>
                        <td>Subtotal</td>
                        <td>
                            <asp:Label ID="lblSubtotal" runat="server" Text=""></asp:Label></td>
                    </tr>
                    <tr>
                        <td>Tax(10%)</td>
                        <td><asp:Label ID="lblTax" runat="server" Text=""></asp:Label></td>
                    </tr>
                    <tr>
                        <td>Total</td>
                        <td><asp:Label ID="lblTotal" runat="server" Text=""></asp:Label></td>
                    </tr>
                </table>
            </div>
            <div class="btn-container">
                <asp:HyperLink CssClass="button cart-shopping" ID="HyperLink1" NavigateUrl="shop.aspx" runat="server">Continue Shopping</asp:HyperLink>
                <asp:Button ID="checkout" runat="server" Text="Proceed To Checkout" CssClass="button cart-submit" OnClick="checkout_Click" />
            </div>
        </div>
        <%

            }
        %>
    </div>
</asp:Content>
