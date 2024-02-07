<%@ Page Title="" Language="C#" MasterPageFile="~/front-end/frontend.Master" AutoEventWireup="true" CodeBehind="order-completed.aspx.cs" Inherits="TimeZone_Assign.front_end.order_completed" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Banner -->
    <div class="order-banner top-banner">
        <div class="txt">MY ORDER</div>
    </div>

    <div class="wrap-content">
        <div class="container">
            <div class="row">
                <!-- Navigation -->
                <div class="col-3">
                    <!-- Profile Icon and name -->
                    <table>
                        <tr>
                            <td><i class="fa-solid fa-circle-user" style="font-size: 80px"></i></td>
                            <td>
                                
                                <b><asp:Label ID="lblName" runat="server" Text="Label"></asp:Label></b>
                                <br />
                                <span style="color: grey">Member</span>
                            </td>
                        </tr>
                    </table>
                    <!-- Navigation Menu -->
                    <div class="nav-list">
                        <a href="profile.aspx"><i class="fa-solid fa-id-card"></i>&nbsp&nbsp My Profile</a>
                        <br />
                        <br />
                        <a href="order-pending.aspx" class="active"><i class="fa-solid fa-clipboard-list"></i>&nbsp&nbsp&nbsp My Order</a>
                        <br />
                        <br />
                        <a href="order-pending.aspx">&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<i class="fa-solid fa-truck"></i>&nbsp Pending</a>
                        <br />
                        <br />
                        <a href="order-completed.aspx" class="active">&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<i class="fa-solid fa-circle-check"></i>&nbsp&nbsp Completed</a>
                    </div>
                </div>
                <!-- Order Info Section -->
                <div class="col-9 order-info-section bg-light">
                    <!-- Title -->
                    <div class="title">
                        Completed
                    </div>
                    <!-- Order Info -->
                    <asp:Repeater ID="RepeaterOrderRecord" runat="server">
                        <ItemTemplate>
                            <div class="order-info">
                                Order ID :
                                <asp:Label ID="lblOrderId" runat="server" Text='<%# Eval("id") %>' />
                                <div style="clear: both">
                                    Date &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp: <%# Eval("paymentDate") %>
                                    <span class="est-arrival">Date Arrival: <%# Eval("arrivalDate") %></span>
                                </div>
                                <!-- Purchased Product Area -->
                                <div class="purchased-product-area">
                                    <asp:Repeater ID="RepeaterProdPurchased" runat="server">
                                        <ItemTemplate>
                                            <div class="purchased-item">
                                                <table>
                                                    <tr>
                                                        <td rowspan="2">
                                                            <asp:ImageButton ID="imgBtnProd" runat="server" ImageUrl='<%# Eval("imagePath") %>' Height="150" Width="150" CssClass="img" CommandArgument='<%# Eval("watchId") %>' OnClick="imgBtnProd_Click" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td><%# Eval("name") %><br />
                                                            Qty: <%# Eval("qty") %><br />
                                                            Total: <%# Eval("price") %>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <!-- Rate Button -->
                                                <asp:Button ID="rateBtn" runat="server" Text='<%# Eval("isRatedStr") %>' CssClass='<%# Eval("isRatedCss") %>' OnClick="rateBtn_Click" CommandArgument='<%# Eval("orderId") + "," + Eval("watchId") %>' Enabled='<%# Eval("isRated") %>' />
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
