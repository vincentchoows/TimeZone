<%@ Page Title="" Language="C#" MasterPageFile="~/back_end/BackEnd.Master" AutoEventWireup="true" CodeBehind="EditStaff.aspx.cs" Inherits="TimeZone_Assign.back_end.record.staff.EditStaff" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h2 class="record-title">Edit New Employee</h2>

    <table class="content-area">
        <tr>
            <td>Employee ID :
            </td>
            <td>
                <asp:Label ID="lblEmpID" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>Role ID :
            </td>
            <td>
                <asp:Label ID="lblRoleID" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>Name : 
            </td>
            <td>
                <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvName" runat="server" ForeColor="Red" ErrorMessage="Please enter name" ControlToValidate="txtName" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Position :
            </td>
            <td>
                <asp:DropDownList ID="ddlPosition" runat="server">
                    <asp:ListItem Value="Super Admin" Text="Super Admin"></asp:ListItem>
                    <asp:ListItem Value="Admin" Text="Admin"></asp:ListItem>
                    <asp:ListItem Value="Staff" Text="Staff"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>Email : 
            </td>
            <td>
                <asp:TextBox ID="txtEmail" runat="server" TextMode="Email"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="Please enter email" ControlToValidate="txtEmail" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Phone Number :
            </td>
            <td>
                <asp:TextBox ID="txtPhone" runat="server" TextMode="Phone" ></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvPhone" runat="server" ErrorMessage="Please enter phone number" ControlToValidate="txtPhone" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Username : 
            </td>
            <td>
                <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ErrorMessage="Please enter username" ControlToValidate="txtUsername" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtUsername" Display="Dynamic" ErrorMessage="Invalid username entered. (E.g. john_doe123)" ForeColor="Red" ValidationExpression="^[a-zA-Z][a-zA-Z0-9]*(?:_[a-zA-Z0-9]+)*$"></asp:RegularExpressionValidator>
            <asp:CustomValidator ID="cvUsernameDuplicate" runat="server" ControlToValidate="txtUsername" ForeColor="Red" Display="Dynamic" ErrorMessage="This username has been used" ></asp:CustomValidator>

            </td>
        </tr>
        <tr>
            <td>Password :
            </td>
            <td>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ErrorMessage="Please enter password" ControlToValidate="txtPassword" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Confirm Password :

            </td>
            <td>
                <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="rfvConfirmPassword" runat="server" ErrorMessage="Please enter confirm password" ControlToValidate="txtConfirmPassword" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                 <asp:CompareValidator ID="cvPassword" runat="server" ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword" ForeColor="Red" Display="Dynamic" ErrorMessage="Confirm password and password does not matched"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <td>Date Register : 
            </td>
            <td>
                <asp:Label ID="lblDate" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>Create Access:
            </td>
            <td>
                <asp:RadioButtonList ID="rblCreate" runat="server">
                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                    <asp:ListItem Value="N">No</asp:ListItem>
                </asp:RadioButtonList>
                <asp:RequiredFieldValidator ID="rfvCreate" runat="server" ForeColor="Red" ControlToValidate="rblCreate"
    ErrorMessage="Please select yes or no"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Read Access:
            </td>
            <td>
                <asp:RadioButtonList ID="rblRead" runat="server">
                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                    <asp:ListItem Value="N">No</asp:ListItem>
                </asp:RadioButtonList>
                <asp:RequiredFieldValidator ID="rfvRead" runat="server" ForeColor="Red" ControlToValidate="rblRead"
    ErrorMessage="Please select yes or no"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Edit Access:
            </td>
            <td>
                <asp:RadioButtonList ID="rblEdit" runat="server">
                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                    <asp:ListItem Value="N">No</asp:ListItem>
                </asp:RadioButtonList>
                <asp:RequiredFieldValidator ID="rfvEdit" runat="server" ForeColor="Red" ControlToValidate="rblEdit"
    ErrorMessage="Please select yes or no"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Delete Access:
            </td>
            <td>
                <asp:RadioButtonList ID="rblDelete" runat="server">
                    <asp:ListItem Value="Y">Yes</asp:ListItem>
                    <asp:ListItem Value="N">No</asp:ListItem>
                </asp:RadioButtonList>
                <asp:RequiredFieldValidator ID="rfvDelete" runat="server" ForeColor="Red" ControlToValidate="rblDelete"
    ErrorMessage="Please select yes or no"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="button-area">
                <asp:Button ID="btnEditStaff" runat="server" Text="Confirm" OnClick="btnEditStaff_Click" CssClass="button" />
                <asp:Button ID="btnCancelEditStaff" runat="server" Text="Cancel" PostBackUrl="StaffRecord.aspx" CssClass="button" OnClick="btnCancelEditStaff_Click" CausesValidation="False"/>
            </td>
        </tr>
    </table>
    <div style="margin-bottom: 50px;"></div>
</asp:Content>
