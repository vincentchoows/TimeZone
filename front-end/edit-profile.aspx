<%@ Page Title="" Language="C#" MasterPageFile="~/front-end/frontend.Master" AutoEventWireup="true" CodeBehind="edit-profile.aspx.cs" Inherits="TimeZone_Assign.front_end.edit_profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Banner -->
    <div class="edit-banner top-banner">
        <div class="txt">EDIT PROFILE</div>
    </div>

    <div style="margin: 100px 0">
        <div class="modal-bg-1 edit-profile-part">
            <div class="modal-content">
                <div class="container">
                    <div class="row">
                        <div class="col-xl-12 mt-4 ">
                            <div class="text-center">
                                <h2>Edit Profile </h2>
                                <br />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="container">
                    <%--start row--%>
                    <div class="row edit-row">

                        <%--back link to profile page--%>
                        <asp:HyperLink ID="goBackLink" runat="server" NavigateUrl="~/front-end/profile.aspx" CssClass="back-to-home btn-icon fa fa-arrow-left"> Back to profile
                        </asp:HyperLink>

                        <div class="col-md-4">
                            <table>
                                <tr>
                                    <td class="text-mute">Basic Info
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label for="validationDefault01">Name:</label></td>
                                    <td>
                                        <%--input name--%>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Name is required** " ControlToValidate="txtName" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>

                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtName" Display="Dynamic" ErrorMessage="Invalid name entered. (E.g. John Doe)" ForeColor="Red" ValidationExpression="^[A-Z][a-zA-Z]*( [A-Z][a-zA-Z]*)*$"></asp:RegularExpressionValidator>

                                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label for="validationDefault01">Username:</label></td>
                                    <td>
                                        <%--input username--%>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Username is required** " ControlToValidate="txtUsername" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtUsername" Display="Dynamic" ErrorMessage="Invalid username entered. (E.g. john_doe123)" ForeColor="Red" ValidationExpression="^[a-zA-Z][a-zA-Z0-9]*(?:_[a-zA-Z0-9]+)*$"></asp:RegularExpressionValidator>

                                        <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label for="validationDefault02">Email:</label></td>
                                    <td>
                                        <%--input email--%>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Email address is required** " ControlToValidate="txtEmail" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtEmail" Display="Dynamic" ErrorMessage="Invalid email address. (E.g. johndoe@gmail.com)" ForeColor="Red" ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"></asp:RegularExpressionValidator>
                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label for="validationDefault03">Phone Number:</label></td>
                                    <td>
                                        <%--input phoneNo--%>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Phone No. is required** " ControlToValidate="txtPhoneNo" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtPhoneNo" Display="Dynamic" ErrorMessage="Invalid phone no. entered. (E.g. 011-2221111)" ForeColor="Red" ValidationExpression="^01[0-46-9]-\d{3}-?\d{4,6}$"></asp:RegularExpressionValidator>

                                        <asp:TextBox ID="txtPhoneNo" runat="server" CssClass="form-control"></asp:TextBox>
                                    </td>
                                </tr>


                                <tr>
                                    <td>
                                        <label for="validationDefault01">Gender:</label>
                                    </td>
                                    <td>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="Gender is required** " ControlToValidate="rblGender" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>

                                        <asp:RadioButtonList ID="rblGender" runat="server" CssClass="rbl">
                                            <asp:ListItem Value="F">Female</asp:ListItem>
                                            <asp:ListItem Value="M">Male</asp:ListItem>
                                        </asp:RadioButtonList>



                                    </td>
                                </tr>
                            </table>
                        </div>

                        <div class="col-md-4">
                            <table>
                                <tr>
                                    <td class="text-mute">Address Info
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label for="name">Address Line 1:</label></td>
                                    <td>
                                        <%--input Address--%>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Address 1 is required** " ControlToValidate="txtAddress1" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtAddress1" Display="Dynamic" ErrorMessage="Invalid Address 1 format." ForeColor="Red" ValidationExpression="^[a-zA-Z0-9\s#.,/-]+$"></asp:RegularExpressionValidator>

                                        <asp:TextBox ID="txtAddress1" runat="server" CssClass="form-control"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label for="name">Address Line 2:</label></td>
                                    <td>
                                        <%--input Address--%>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Address 2 is required** " ControlToValidate="txtAddress2" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server" ControlToValidate="txtAddress2" Display="Dynamic" ErrorMessage="Invalid Address 2 format." ForeColor="Red" ValidationExpression="^[a-zA-Z0-9\s#.,/-]+$"></asp:RegularExpressionValidator>

                                        <asp:TextBox ID="txtAddress2" runat="server" CssClass="form-control"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label for="name">City:</label></td>
                                    <td>
                                        <%--input city--%>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="City is required** " ControlToValidate="ddlCity" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>

                                        <asp:XmlDataSource ID="CitiesXmlDataSource" runat="server" DataFile="~/App_Data/MalaysianCities.xml" XPath="cities/city" />

                                        <asp:DropDownList ID="ddlCity" runat="server" DataSourceID="CitiesXmlDataSource" DataTextField="value" DataValueField="value" CssClass="city-list">
                                            <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtCity" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        
                                    </td>
                                </tr>


                                <tr>
                                    <td>
                                        <label for="name">State:</label></td>
                                    <td>
                                        <%--input state--%>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="State is required** " ControlToValidate="txtState" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>

                                         <asp:XmlDataSource ID="MalaysianStatesXmlDataSource" runat="server" DataFile="~/App_Data/MalaysianStates.xml" XPath="states/state" />
                                    <asp:DropDownList ID="ddlState" runat="server" DataSourceID="MalaysianStatesXmlDataSource" DataTextField="value" DataValueField="value" CssClass="state-list">
                                    </asp:DropDownList>

                                        <asp:TextBox ID="txtState" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label for="name">Postcode:</label></td>
                                    <td>
                                        <%--input postcode--%>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="Poscode is required** " ControlToValidate="txtPostcode" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server" ControlToValidate="txtPostcode" Display="Dynamic" ErrorMessage="Invalid poscode (E.g. 11500)" ForeColor="Red" ValidationExpression="^\d{5}$"></asp:RegularExpressionValidator>

                                        <asp:TextBox ID="txtPostcode" runat="server" CssClass="form-control"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>

                        </div>
                        <div class="col-md-4">
                            <table>
                                <tr>
                                    <td class="text-mute">Card Info
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <label for="name">Card Number:</label></td>
                                    <td>
                                        <%--input cardNo--%>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Card No.is required** " ControlToValidate="txtCardNo" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtCardNo" Display="Dynamic" ErrorMessage="Invalid card No. (E.g. 1111 2222 3333 4444)" ForeColor="Red" ValidationExpression="^\d{4}\s\d{4}\s\d{4}\s\d{4}$"></asp:RegularExpressionValidator>

                                        <asp:TextBox ID="txtCardNo" runat="server" CssClass="form-control"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label for="name">Good Thru:</label></td>
                                    <td>
                                        <%--input goodthru--%>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="GoodThru is required**. " ControlToValidate="txtGoodThru" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtGoodThru" Display="Dynamic" ErrorMessage="Invalid goodthru (E.g. 01/01)" ForeColor="Red" ValidationExpression="^(0[1-9]|1[0-2])\/(0[1-9]|[12][0-9]|3[01])$"></asp:RegularExpressionValidator>

                                        <asp:TextBox ID="txtGoodThru" runat="server" CssClass="form-control"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label for="name">CVV:</label></td>
                                    <td>
                                        <%--input cvv--%>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="CVV is required** " ControlToValidate="txtCvv" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtCvv" Display="Dynamic" ErrorMessage="Invalid Cvv (E.g. 012)" ForeColor="Red" ValidationExpression="^^[0-9]{3}$"></asp:RegularExpressionValidator>

                                        <asp:TextBox ID="txtCvv" runat="server" CssClass="form-control"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>

                        </div>
                    </div>
                    <%--end row--%>
                    <div class="mt-5 mb-3">
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="button" OnClick="btnSave_Click" />
                        <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="button" OnClick="btnReset_Click" />

                        <asp:HyperLink ID="changePswdLink" runat="server" NavigateUrl="~/front-end/change-password.aspx" CssClass="float-right back-edit-password">Click here to change password</asp:HyperLink>

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
