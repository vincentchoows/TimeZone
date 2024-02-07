<%@ Page Title="" Language="C#" MasterPageFile="~/front-end/frontend.Master" AutoEventWireup="true" CodeBehind="collection.aspx.cs" Inherits="TimeZone_Assign.front_end.collection" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Banner -->
    <div class="collection-banner">
        <asp:Image ID="imgBanner" runat="server" />
    </div>

    <!-- Collection Info -->
    <div class="collection-info">
        <span class="emphasize">
            <asp:Label ID="lblName" runat="server" Text="Label"></asp:Label></span>
        <br />
        <p class="collection-intro">
            <asp:Label ID="lblDesc" runat="server" Text="Label"></asp:Label>
        </p>
    </div>

    <!-- Watch Section -->
    <div class="watch-section">
        <div class="container">
            <div class="row">
                <asp:Repeater ID="RepeaterWatch" runat="server">
                    <ItemTemplate>
                        <div class="col col-3">
                            <asp:ImageButton ID="btnWatch" runat="server" ImageUrl='<%# Eval("imagePath") %>' CssClass="img" OnClick="watchBtnClick" CommandArgument='<%# Eval("id") %>' />
                            <br />
                            <b>Rolex</b>
                            <br />
                            <span class="txt-collection"><%# Eval("referenceNo") %></span>
                            <br />
                            <span class="txt-model-case"><%# Eval("modelCase") %></span>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>

</asp:Content>
