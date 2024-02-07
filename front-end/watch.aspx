<%@ Page Title="" Language="C#" MasterPageFile="~/front-end/frontend.Master" AutoEventWireup="true" CodeBehind="watch.aspx.cs" Inherits="TimeZone_Assign.front_end.watch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Banner -->
    <div class="watch-banner top-banner">
        <div class="txt uppercase-text">
            <asp:Label ID="lblBannerName" runat="server" Text="Label"></asp:Label>
        </div>
    </div>

    <!-- Product section -->
    <div class="product-section">
        <div class="container">
            <div class="row">
                <div class="col-6">
                    <!-- Slideshow (Bootstrap) -->
                    <div class="product-slideshow-area">
                        <div id="carouselExampleDark" class="carousel carousel-dark slide" data-bs-ride="carousel">
                            <div class="carousel-inner">
                                <div class="carousel-item active">
                                    <asp:Image ID="imgCarousel1" runat="server" CssClass="d-block w-100" />
                                </div>
                                <div class="carousel-item">
                                    <asp:Image ID="imgCarousel2" runat="server" CssClass="d-block w-100" />
                                </div>
                                <div class="carousel-item">
                                    <asp:Image ID="imgCarousel3" runat="server" CssClass="d-block w-100" />
                                </div>
                                <div class="carousel-item">
                                    <asp:Image ID="imgCarousel4" runat="server" CssClass="d-block w-100" />
                                </div>
                            </div>
                            <button class="carousel-control-prev" type="button" data-bs-target="#carouselExampleDark" data-bs-slide="prev">
                                <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                                <span class="visually-hidden">Previous</span>
                            </button>
                            <button class="carousel-control-next" type="button" data-bs-target="#carouselExampleDark" data-bs-slide="next">
                                <span class="carousel-control-next-icon" aria-hidden="true"></span>
                                <span class="visually-hidden">Next</span>
                            </button>
                        </div>
                    </div>
                </div>
                <!-- Product Details Area -->
                <div class="col-6">
                    <div class="product-details-area">
                        <div class="top-area">
                            <span class="product-name">
                                <asp:Label ID="lblProdName" runat="server" Text="Label" CssClass="uppercase-text"></asp:Label>
                            </span>
                            <br />
                            Price: 
                            <span class="product-price">
                                <asp:Label ID="lblPrice" runat="server" Text="Label"></asp:Label>
                            </span>
                        </div>
                        <br />
                        <h6>MODEL CASE</h6>
                        <asp:Label ID="lblModelCase" runat="server" Text="Label"></asp:Label>
                        <br />
                        <br />
                        <h6>BEZEL</h6>
                        <asp:Label ID="lblBezel" runat="server" Text="Label"></asp:Label>
                        <br />
                        <br />
                        <h6>WATER RESISTANCE</h6>
                        <asp:Label ID="lblWaterResistance" runat="server" Text="Label"></asp:Label>
                        <br />
                        <br />
                        <h6>MOVEMENT</h6>
                        <asp:Label ID="lblMovement" runat="server" Text="Label"></asp:Label>
                        <br />
                        <br />
                        <h6>CALIBRE</h6>
                        <asp:Label ID="lblCalibre" runat="server" Text="Label"></asp:Label>
                        <br />
                        <br />
                        <h6>POWER RESERVE</h6>
                        <asp:Label ID="lblPowerReserve" runat="server" Text="Label"></asp:Label>
                        <br />
                        <br />
                        <h6>BRACELET</h6>
                        <asp:Label ID="lblBracelet" runat="server" Text="Label"></asp:Label>
                        <br />
                        <br />
                        <h6>DIAL</h6>
                        <asp:Label ID="lblDial" runat="server" Text="Label"></asp:Label>
                        <br />
                        <br />
                        <h6>CERTIFICATION</h6>
                        <asp:Label ID="lblCertification" runat="server" Text="Label"></asp:Label>
                        <br />
                        <br />
                        <h6>WINDING CROWN</h6>
                        <asp:Label ID="lblWindingCrown" runat="server" Text="Label"></asp:Label>
                        <br />
                        <br />
                        <br />
                        <!-- Add To Cart Button -->
                        <asp:Button ID="addToCart" runat="server" Text="Add To Cart" CssClass="button" OnClick="addToCart_Click"/>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Review section -->
    <div class="product-review">
        <div class="container">
            <!-- Overall Rating Section -->
            <div class="rating-section">
                <h4>Product Ratings</h4>
                <span class="rating-text">
                    <asp:Label ID="lblProdRating" runat="server" Text="Label"></asp:Label>
                </span>
                out of 5
                <asp:Label runat="server" ID="lblStarRating" CssClass="rating-star"></asp:Label>
            </div>
            <!-- Comment Section -->
            <div class="comment-section">
                <!-- One Person's Comment -->
                <asp:Repeater ID="RepeaterCustomerComment" runat="server">
                    <ItemTemplate>
                        <div class="person-comment">
                            <div class="row">
                                <div class="col-1">
                                    <i class="fa-solid fa-circle-user"></i>
                                </div>
                                <div class="col-11">
                                    <b><%# Eval("custName") %></b><br />
                                    <span class="comment-date"><%# Eval("date") %></span><br />
                                    <span class="rating-star"><%# GetRatingStars(Convert.ToDouble(Eval("rating"))) %></span><br />
                                    <div>
                                        <i class="fa-solid fa-quote-left fa-2xs"></i>&nbsp;<%# Eval("comment") %>&nbsp;<i class="fa-solid fa-quote-right fa-2xs"></i>
                                    </div>
                                    <br />
                                    <!-- Seller Response -->
                                    <div class="seller-response box arrow-top">
                                        Seller's Response:<br />
                                        <%# Eval("reply") %>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
</asp:Content>
