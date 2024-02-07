<%@ Page Title="" Language="C#" MasterPageFile="~/back_end/BackEnd.Master" AutoEventWireup="true" CodeBehind="AddStaff.aspx.cs" Inherits="TimeZone_Assign.back_end.record.staff.AddStaff" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 class="record-title">Add New Employee</h2>

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
                <asp:RegularExpressionValidator ID="revName" runat="server" ControlToValidate="txtName" Display="Dynamic" ErrorMessage="Invalid name entered. (E.g. John Doe)" ForeColor="Red" ValidationExpression="^[A-Z][a-zA-Z]*( [A-Z][a-zA-Z]*)*$"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td>Position :
            </td>
            <td>
                <asp:DropDownList ID="ddlPosition" runat="server">
                    <asp:ListItem Value="Super Admin">Super Admin</asp:ListItem>
                    <asp:ListItem Value="Admin">Admin</asp:ListItem>
                    <asp:ListItem Value="Staff">Staff</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>Email : 
            </td>
            <td>
                <asp:TextBox ID="txtEmail" runat="server" TextMode="Email"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="Please enter email" ControlToValidate="txtEmail" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" Display="Dynamic" ErrorMessage="Invalid email address. (E.g. johndoe@gmail.com)" ForeColor="Red" ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"></asp:RegularExpressionValidator>
            <asp:CustomValidator ID="cvAccountDuplicate" runat="server" CssClass="error" Display="Dynamic" ErrorMessage="Account duplicate detected" ForeColor="Red"></asp:CustomValidator>
            </td>
        </tr>
        <tr>
            <td>Phone Number :
            </td>
            <td>
                <asp:TextBox ID="txtPhone" runat="server" TextMode="Phone"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvPhone" runat="server" ErrorMessage="Please enter phone number" ControlToValidate="txtPhone" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtPhone" Display="Dynamic" ErrorMessage="Invalid phone no. entered. (E.g. 011-2221111)" ForeColor="Red" ValidationExpression="^01[0-46-9]-\d{3}-?\d{4,6}$"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td>Username : 
            </td>
            <td>
                <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
              <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ErrorMessage="Please enter username" ControlToValidate="txtUsername" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtUsername" Display="Dynamic" ErrorMessage="Invalid username entered. (E.g. john_doe123)" ForeColor="Red" ValidationExpression="^[a-zA-Z][a-zA-Z0-9]*(?:_[a-zA-Z0-9]+)*$"></asp:RegularExpressionValidator>
                <asp:CustomValidator ID="cvUsernameDuplicate" runat="server" Display="Dynamic" ErrorMessage="Username duplicate detected" ForeColor="Red"></asp:CustomValidator>
            </td>
        </tr>
        <tr>
            <td>Password :
            </td>
            <td>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ErrorMessage="Please enter password" ControlToValidate="txtPassword" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtPassword" Display="Dynamic" ErrorMessage="Must contain one uppercase and lowercase letter, one digit, one special character, at least 8 characters <br/>" ForeColor="Red" ValidationExpression="^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[^a-zA-Z0-9]).{8,}$"></asp:RegularExpressionValidator>

                </td>
        </tr>
        <tr>
            <td>Confirm Password :

            </td>
            <td>
                <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvConfirmPassword" runat="server" ErrorMessage="Please enter confirm password" ControlToValidate="txtConfirmPassword" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                 <asp:CompareValidator ID="cvPassword" runat="server" ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword" ForeColor="Red" Display="Dynamic" ErrorMessage="Password does not matched"></asp:CompareValidator>
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
    ErrorMessage="Please select [Yes] or [No]"></asp:RequiredFieldValidator>
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
    ErrorMessage="Please select [Yes] or [No]"></asp:RequiredFieldValidator>
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
    ErrorMessage="Please select [Yes] or [No]"></asp:RequiredFieldValidator>
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
    ErrorMessage="Please select [Yes] or [No]"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="button-area">
                <asp:Button ID="btnAddStaff" runat="server" Text="Confirm" OnClick="btnAddStaff_Click" CssClass="button" />
                <asp:Button ID="btnCancelAddStaff" runat="server" Text="Cancel" CssClass="button" PostBackUrl="~/back_end/record/staff/StaffRecord.aspx" OnClick="btnCancelAddStaff_Click" CausesValidation="False"/>
                </td>
        </tr>
    </table>
    <div style="margin-bottom: 50px;"></div>
</asp:Content>
