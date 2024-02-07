<%@ Page Title="" Language="C#" MasterPageFile="~/front-end/frontend.Master" AutoEventWireup="true" CodeBehind="user-login.aspx.cs" Inherits="TimeZone_Assign.front_end.user_login" %>
<%@ Register TagPrefix="LoginPage" TagName="Login" Src="~/front-end/loginpage.ascx"%>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Banner -->
    <div class="login-banner top-banner">
        <div class="txt">USER LOGIN</div>
    </div>

    
    <LoginPage:Login runat="server"></LoginPage:Login>

</asp:Content>
