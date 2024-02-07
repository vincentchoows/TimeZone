<%@ Page Title="" Language="C#" MasterPageFile="~/front-end/frontend.Master" AutoEventWireup="true" CodeBehind="change-password.aspx.cs" Inherits="TimeZone_Assign.front_end.change_password" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Banner -->
    <div class="edit-banner top-banner">
        <div class="txt">CHANGE PASSWORD</div>
    </div>
    <div class="modal-bg-1 change-pswd-part">
        <div class="modal-content">
            <div class="modal-bg-2">
                <div class="modal-content" style="width: 70%; height: 75%; margin: 100px 0;">
                    <div class="text-center">
                        <h2>Change Password</h2>
                        <br />
                    </div>
                    <span class="warn-msg" style="border-bottom: 1px solid lightgrey;">For your account's security, do not share your password with anyone else</span>
                    <br />
                    <div class="form-row">
                        <div class="col-md-12 mb-3">
                            <table>
                                <tr>
                                    <td style="width: 150px;">
                                        <label for="name">Current Password:</label></td>
                                    <td style="padding-left: 20px;">
                                        <%--input current password--%>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Password is required** " ControlToValidate="txtCurrentPswd" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtCurrentPswd" Display="Dynamic" ErrorMessage="Must contain one uppercase and lowercase letter, one digit, one special character, at least 8 characters" ForeColor="Red" ValidationExpression="^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[^a-zA-Z0-9]).{8,}$"></asp:RegularExpressionValidator>

                                        <asp:CustomValidator ID="cvNotMatched" runat="server" CssClass="error" Display="Dynamic" ErrorMessage="Current password does not match" ForeColor="Red"></asp:CustomValidator>

                                        <asp:TextBox ID="txtCurrentPswd" runat="server" CssClass="form-control" placeholder="Password" TextMode="Password"></asp:TextBox>
                                    </td>
                                    <%--<span class="current-pwd-unmatch" style="color: #fa2020"></span>--%>
                                </tr>
                                <tr>
                                    <td>
                                        <label class="mb-40" for="name">New Password:</label></td>
                                    <td style="padding-left: 20px;">
                                        <br />
                                        <br />
                                        <%--input password--%>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Password is required** " ControlToValidate="txtNewPswd" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtNewPswd" Display="Dynamic" ErrorMessage="Must contain one uppercase and lowercase letter, one digit, one special character, at least 8 characters" ForeColor="Red" ValidationExpression="^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[^a-zA-Z0-9]).{8,}$"></asp:RegularExpressionValidator>

                                        <asp:TextBox ID="txtNewPswd" runat="server" CssClass="form-control" placeholder="Password" TextMode="Password"></asp:TextBox>



                                        <small id="password_strength passwordHelpBlock" class="form-text text-muted">Your password must be at least 8 characters long, contain at least one uppercase and lowercase letter, numbers, and special character.
                                        </small>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                        <label for="name">Confirm Password:</label></td>
                                    <td style="padding-left: 20px;">
                                        <br />
                                        <%--input password--%>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ErrorMessage="Re-entering your password is required" ControlToValidate="txtNewPswd" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtNewPswd" ControlToValidate="txtConfirmPswd" Display="Dynamic" ErrorMessage="Password does not match." ForeColor="Red"></asp:CompareValidator>

                                        <asp:TextBox ID="txtConfirmPswd" runat="server" CssClass="form-control" placeholder="Password" TextMode="Password"></asp:TextBox>


                                        <small class="form-text text-muted">
                                            <span id="message" style="color: #fa2020"></span>
                                        </small>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>



                    <div class="mt-4">
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" OnClick="btnSave_Click" />
                        <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="button" OnClick="btnReset_Click" />


                        <asp:HyperLink ID="backToEditProfile" runat="server" NavigateUrl="~/front-end/edit-profile.aspx" CssClass="float-right back-to-edit">Back to edit user profile</asp:HyperLink>

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
