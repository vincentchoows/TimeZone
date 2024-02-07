<%@ Page Title="" Language="C#" MasterPageFile="~/back_end/BackEnd.Master" AutoEventWireup="true" CodeBehind="EditReview.aspx.cs" Inherits="TimeZone_Assign.back_end.record.review.EditReview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h5 class="record-title">Add/Edit Reply</h5>
    <!-- Content Area -->
    <table class="content-area">
        <tr>
            <td colspan="2" style="text-align: center; font-weight: bold">Review</td>
        </tr>
        <tr>
            <td>ID</td>
            <td>
                <asp:Label ID="lblReviewId" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td>Date</td>
            <td>
                <asp:Label ID="lblDate" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td>Customer </td>
            <td>
                <asp:Label ID="lblCustomer" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td>Product </td>
            <td>
                <asp:Label ID="lblProduct" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td>Rating </td>
            <td>
                <asp:Label ID="lblRating" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td>Comment </td>
            <td>
                <asp:Label ID="lblComment" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center; font-weight: bold">Reply</td>
        </tr>
        <tr>
            <td>ID </td>
            <td>
                <asp:Label ID="lblReplyId" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td>Reply </td>
            <td>
                <asp:TextBox ID="tbReply" runat="server" TextMode="MultiLine"></asp:TextBox>
                <div style="display: block">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbReply" Display="Dynamic" ErrorMessage="*Required" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="button-area">
                <asp:Button ID="confirmBtn" runat="server" Text="Confirm" CssClass="button" OnClick="confirmBtn_Click" />
                <asp:Button ID="cancelBtn" runat="server" Text="Cancel" CssClass="button" PostBackUrl="~/back_end/record/review/ReviewRecord.aspx" CausesValidation="False" />
            </td>
        </tr>
    </table>
    <div style="margin-bottom: 50px;"></div>
</asp:Content>
