<%@ Page Title="" Language="C#" MasterPageFile="~/back_end/BackEnd.Master" AutoEventWireup="true" CodeBehind="EditDelivery.aspx.cs" Inherits="TimeZone_Assign.back_end.record.delivery.EditDelivery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <h2 class="record-title">Edit Delivery</h2>

    <table class="content-area">
            <tr>
            <td>
                Delivery ID :
            </td>
            <td>
                <asp:Label ID="lblDeliveryID" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Shipping Address :
            </td>
            <td>
                <asp:TextBox ID="txtShippingAddress" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red" ControlToValidate="txtShippingAddress" Display="Dynamic" ErrorMessage="Please enter shipping address"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                Estimate Earliest Date : 
            </td>
            <td>
                <asp:TextBox ID="txtEarliestDate" runat="server" TextMode="Date"></asp:TextBox>
                <asp:CompareValidator ID="cvEarliestDate" runat="server" ControlToValidate="txtEarliestDate"
Operator="GreaterThanEqual" forecolor="Red" ErrorMessage="Delivery date must be on or after today's date"
Type="Date" ValueToCompare="<%# DateTime.Today.ToShortDateString() %>"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td>
                Estimate Latest Date :
            </td>
            <td>
                <asp:TextBox ID="txtLatestDate" runat="server" TextMode="Date"></asp:TextBox>
                <asp:CompareValidator ID="cvLatestDate" runat="server" ControlToValidate="txtLatestDate"
Operator="GreaterThanEqual" forecolor="Red" ErrorMessage="Delivery date must be on or after today's date"
Type="Date" ValueToCompare="<%# DateTime.Today.ToShortDateString() %>"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td>
                Arrival Date : 
            </td>
            <td>
                <asp:TextBox ID="txtArrivalDate" runat="server" TextMode="Date"></asp:TextBox>
           <asp:CompareValidator ID="cvArrivalDate" runat="server" ControlToValidate="txtArrivalDate"
Operator="GreaterThanEqual" forecolor="Red" ErrorMessage="Delivery date must be on or after today's date"
Type="Date" ValueToCompare="<%# DateTime.Today.ToShortDateString() %>"></asp:CompareValidator>
                </td>
        </tr>
        <tr>
            <td>
                Status :
            </td>
            <td>
                <asp:DropDownList ID="ddlStatus" runat="server" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged" AutoPostBack="true">
                    <asp:ListItem Value="Packaging" Text="Packaging"></asp:ListItem>
                    <asp:ListItem Value="Shipping" Text="Shipping"></asp:ListItem>
                    <asp:ListItem Value="Delivered" Text="Delivered"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="button-area">
            <asp:Button ID="btnEditDelivery" runat="server" Text="Confirm" OnClick="btnEditDelivery_Click" CssClass="button" />
                <asp:Button ID="btnCancelEdit" runat="server" Text="Cancel" OnClick="btnCancelEdit_Click" CssClass="button" CausesValidation="False"/>
            </td>
             </tr>
    </table>
    <div style="margin-bottom: 50px;"></div>

</asp:Content>
