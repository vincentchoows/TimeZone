<%@ Page Title="" Language="C#" MasterPageFile="~/front-end/frontend.Master" AutoEventWireup="true" CodeBehind="rate-product.aspx.cs" Inherits="TimeZone_Assign.front_end.rate_product" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Banner -->
    <div class="rate-banner top-banner">
        <div class="txt">RATING</div>
    </div>

    <!-- User Rating Section -->
    <div class="container">
        <div class="user-rating-section">
            <div class="row">
                <span class="rate-purchase-txt">Rate your purchase!</span>
            </div>
            <div class="row">
                <div class="col-6 image-area">
                    <asp:ImageButton ID="imgBtnProd" runat="server" CssClass="img" OnClick="imgBtnProd_Click" />
                </div>
                <div class="col-6 rating-area d-flex align-items-center">
                    <div>
                        <b>
                            <asp:Label ID="lblProdName" runat="server" Text="Label"></asp:Label></b>
                        <br />
                        <span style="color: grey; font-size: 0.9em;">Purchased on:
                            <asp:Label ID="lblPurchasedOn" runat="server" Text="Label"></asp:Label></span>
                        <br />
                        <asp:RadioButtonList ID="ratingBtnGroup" runat="server" RepeatDirection="Horizontal" CssClass="rating-stars-group">
                            <asp:ListItem Value="1" Text="<i class='fa-solid fa-star fa-xl'></i>"></asp:ListItem>
                            <asp:ListItem Value="2" Text="<i class='fa-solid fa-star fa-xl'></i>"></asp:ListItem>
                            <asp:ListItem Value="3" Text="<i class='fa-solid fa-star fa-xl'></i>"></asp:ListItem>
                            <asp:ListItem Value="4" Text="<i class='fa-solid fa-star fa-xl'></i>"></asp:ListItem>
                            <asp:ListItem Value="5" Text="<i class='fa-solid fa-star fa-xl'></i>"></asp:ListItem>
                        </asp:RadioButtonList>
                        <div style="display: block;">
                            <asp:RequiredFieldValidator ID="rfvRatingBtnGroup" runat="server" ErrorMessage="Please select a rating." ControlToValidate="ratingBtnGroup" ForeColor="red"></asp:RequiredFieldValidator>
                        </div>
                        <!-- Comment Section -->
                        <asp:TextBox ID="commentTextBox" runat="server" Height="100px" Width="300px" TextMode="MultiLine" placeholder="Enter your comment here..."></asp:TextBox>
                        <div style="display: block;">
                            <asp:RequiredFieldValidator ID="rfvCommentTb" runat="server" ErrorMessage="Please enter your comment." ControlToValidate="commentTextBox" ForeColor="red" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                        <br />
                        <!-- Button -->
                        <asp:Button ID="backBtn" runat="server" Text="Back" CssClass="back-btn button" OnClick="backBtn_Click" CausesValidation="false" />
                        <asp:Button ID="confirmBtn" runat="server" Text="Post" CssClass="confirm-btn button" OnClick="confirmBtn_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>


</asp:Content>
