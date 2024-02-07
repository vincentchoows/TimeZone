<%@ Page Title="" Language="C#" MasterPageFile="~/back_end/backend.Master" AutoEventWireup="true" CodeBehind="ViewCustomer.aspx.cs" Inherits="timezone.back_end.record.customer.ViewCustomer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="top-section mb-3" style="display: flex; justify-content: center; margin-top: 3em">
        <span style="font-weight: bold; font-size: 30px;">View Customer Record</span> &nbsp&nbsp
               
    </div>

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT GALLERY.*, CATEGORY.*, WATCH.* FROM CATEGORY INNER JOIN WATCH ON CATEGORY.CATEGORY_ID = WATCH.CATEGORY_ID INNER JOIN GALLERY ON WATCH.GALLERY_ID = GALLERY.GALLERY_ID"></asp:SqlDataSource>
    <!-- Content Area -->
    <table class="content-area mb-4 customer-table" style="margin-left: 20em; width: 60%;">
        <!-- Information -->
        <tr>
            <td colspan="2" style="text-align: center; font-weight: bold; height: 43px;">Information</td>
        </tr>
        <tr>
            <td>Customer ID </td>
            <td>
                <asp:TextBox ID="tbCustID" runat="server" Enabled="False"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Name</td>
            <td>
                <asp:TextBox ID="tbName" runat="server" Enabled="False"></asp:TextBox></td>
        </tr>

        <tr class="input-field">
            <td>Gender </td>
            <td class="pl-5 justify-content-center p">
                <asp:TextBox ID="tbGender" runat="server" Enabled="False"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td>Email </td>
            <td>
                <asp:TextBox ID="tbEmail" runat="server" Enabled="False"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Phone</td>
            <td>
                <asp:TextBox ID="tbPhone" runat="server" Enabled="False"></asp:TextBox></td>
        </tr>

        <tr>
            <td>Username</td>
            <td>
                <asp:TextBox ID="tbUsername" runat="server" Enabled="False"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Date Registered</td>
            <td>
                <asp:TextBox ID="tbDateReg" runat="server" Enabled="False"></asp:TextBox></td>
        </tr>

        <!-- --------------------------------- Address ----------------------------- -->
        <tr>
            <td colspan="2" style="text-align: center; font-weight: bold;">Address</td>
        </tr>
        <tr>
            <td>Address ID </td>
            <td>
                <asp:TextBox ID="tbAddressId" runat="server" Enabled="False"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Address Line 1</td>
            <td>
                <asp:TextBox ID="tbAddress1" runat="server" Enabled="False"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Address Line 2 </td>
            <td>
                <asp:TextBox ID="tbAddress2" runat="server" Enabled="False"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Postcode</td>
            <td>
                <asp:TextBox ID="tbPostcode" runat="server" Enabled="False"></asp:TextBox></td>
        </tr>
        <tr>
            <td>City</td>
            <td>
                <asp:TextBox ID="tbCity" runat="server" Enabled="False"></asp:TextBox></td>
        </tr>
        <tr>
            <td>State</td>
            <td>
                <asp:TextBox ID="tbState" runat="server" Enabled="False"></asp:TextBox></td>
        </tr>



        <!-- Card Details-->
        <tr>
            <td>Card ID </td>
            <td>
                <asp:TextBox ID="tbCardId" runat="server" Enabled="False"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center; font-weight: bold;">Card Details</td>
        </tr>
        <tr>
            <td>Card No.</td>
            <td>
                <asp:TextBox ID="tbCardNo" runat="server" Enabled="False"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Good Thru</td>
            <td>
                <asp:TextBox ID="tbGoodThru" runat="server" Enabled="False"></asp:TextBox></td>
        </tr>
        <tr>
            <td>CVV</td>
            <td>
                <asp:TextBox ID="tbCvv" runat="server" Enabled="False"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="2" class="button-area">
                <asp:Button ID="backBtn" runat="server" Text="Go Back" CssClass="button" PostBackUrl="~/back_end/record/customer/CustomerRecord.aspx" />

            </td>
        </tr>

    </table>
</asp:Content>
