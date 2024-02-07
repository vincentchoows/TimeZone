<%@ Page Title="" Language="C#" MasterPageFile="~/front-end/frontend.Master" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="TimeZone_Assign.front_end.home" %>
<%@ Register TagPrefix="WelcomePage" TagName="Welcome" Src="~/front-end/welcome.ascx"%>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="home-banner">
        <div class="home-banner-content">


            <% if (Session["customer"] != null)
                { %>
            <h1><WelcomePage:Welcome runat="server"></WelcomePage:Welcome></h1>
            <h3>Select Your New Perfect Style</h3>
            <%  
                }
                else
                {  
            
            %>
            <h1>Select Your New Perfect Style</h1>

            <% 
                } 
            %>
            
            <p>
                Join us in our pursuit of excellence. Explore our iconic <span class="txt-red">timepieces</span> that blend cutting-edge technology with stunning aesthetics.
            </p>
            <asp:Button ID="GoToShopBtn" runat="server" Text="SHOP NOW" CssClass="shop-now-btn" OnClick="GoToShopBtn_Click" />
        </div>
    </div>
</asp:Content>
