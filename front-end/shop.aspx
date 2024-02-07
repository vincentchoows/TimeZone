<%@ Page Title="" Language="C#" MasterPageFile="~/front-end/frontend.Master" AutoEventWireup="true" CodeBehind="shop.aspx.cs" Inherits="TimeZone_Assign.front_end.shop" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Banner -->
    <div class="shop-banner">
        <img src="assets/img/wallpaper/shop-banner.jpg" />
    </div>

    <!-- Shop Info -->
    <div class="shop-info">
        <span class="timezone"><span style="color: black;">Time</span>Zone</span>
        <br />
        <span class="emphasize">THE OFFICIAL ROLEX RETAILER</span>
        <br />
        <p class="rolex-intro">
            Rolex is more than just a watch - it's a symbol of prestige and refinement.
            <br />
            With a reputation for precision and innovation, a Rolex timepiece is an investment in quality that will stand the test of time.
        </p>
    </div>

    <!-- Collections -->
    <div class="collections">
        <div class="container txt">OUR COLLECTIONS</div>
        <div class="container p-0">
            <div class="row first-row">
                <!-- Cosmograph Daytona -->
                <div class="col-6">
                    <asp:ImageButton ID="cosmographColl" runat="server" ImageUrl="assets/img/wallpaper/cosmograph-daytona.jpg" CssClass="img" OnClick="cosmographColl_Click" />
                </div>
                <!-- Submariner -->
                <div class="col-6">
                    <asp:ImageButton ID="submarinerColl" runat="server" ImageUrl="assets/img/wallpaper/submariner.jpg" CssClass="img" OnClick="submarinerColl_Click" />
                </div>
            </div>
            <div class="row">
                <!-- Date Just Daytona -->
                <div class="col-6">
                    <asp:ImageButton ID="datejustColl" runat="server" ImageUrl="assets/img/wallpaper/datejust.jpg" CssClass="img" OnClick="datejustColl_Click" />
                </div>
                <!-- Explorer -->
                <div class="col-6">
                    <asp:ImageButton ID="explorerColl" runat="server" ImageUrl="assets/img/wallpaper/explorer.jpg" CssClass="img" OnClick="explorerColl_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
