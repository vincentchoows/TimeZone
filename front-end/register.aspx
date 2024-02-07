<%@ Page Title="" Language="C#" MasterPageFile="~/front-end/frontend.Master" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="TimeZone_Assign.front_end.register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Banner -->
    <div class="login-banner top-banner">
        <div class="txt">REGISTER</div>
    </div>

    <%--header title end--%>
    <div class="container register-part">
        <div class="form_wrapper ">
            <div class="form_container">
                <div class="title_container">
                    <h2>Welcome! Please fill in the form</h2>
                </div>
                <div class="row clearfix">
                    <div class="">
                        <%--input name--%>

                        <div>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Name is required** " ControlToValidate="txtName" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>

                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtName" Display="Dynamic" ErrorMessage="Invalid name entered. (E.g. John Doe)" ForeColor="Red" ValidationExpression="^[A-Z][a-zA-Z]*( [A-Z][a-zA-Z]*)*$"></asp:RegularExpressionValidator>
                        </div>
                        <div class="input_field">
                            <span><i aria-hidden="true" class="fas fa-user"></i></span>
                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Full name (E.g. John Doe)"></asp:TextBox>
                        </div>

                        <%--input username--%>
                        <div>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Username is required** " ControlToValidate="txtUsername" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtUsername" Display="Dynamic" ErrorMessage="Invalid username entered. (E.g. john_doe123)" ForeColor="Red" ValidationExpression="^[a-zA-Z][a-zA-Z0-9]*(?:_[a-zA-Z0-9]+)*$"></asp:RegularExpressionValidator>

                            <asp:CustomValidator ID="cvUsernameDuplicate" runat="server" CssClass="error" Display="Dynamic" ErrorMessage="Username duplicate detected" ForeColor="Red" ></asp:CustomValidator>

                        </div>
                        <div class="input_field">
                            <span><i aria-hidden="true" class="fas fa-user"></i></span>
                            <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Username (E.g. john_doe123)"></asp:TextBox>
                        </div>

                        <%--input password--%>
                        <div class="mb-3">
                            <small id="password_strength passwordHelpBlock" class="mb-1 form-text text-muted">Your password must be at least 8 characters long, contain at least one uppercase and lowercase letter, numbers, and special character.
                        </small>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="<br/>Password is required** " ControlToValidate="txtPassword" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtPassword" Display="Dynamic" ErrorMessage="<br/>Must contain one uppercase and lowercase letter, one digit, one special character, at least 8 characters" ForeColor="Red" ValidationExpression="^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[^a-zA-Z0-9]).{8,}$"></asp:RegularExpressionValidator>
                        </div>
                        <div class="input_field">
                            <span><i aria-hidden="true" class="fas fa-lock"></i></span>
                            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" placeholder="Password (E.g. Password123!)" TextMode="Password"></asp:TextBox>
                        </div>

                        <%--input confirm password--%>
                        <div>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ErrorMessage="Re-entering your password is required" ControlToValidate="txtPassword" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtPassword" ControlToValidate="txtReenterPassword" Display="Dynamic" ErrorMessage="Password does not match." ForeColor="Red"></asp:CompareValidator>
                        </div>
                        <div class="input_field">
                            <span><i aria-hidden="true" class="fas fa-lock"></i></span>
                            <asp:TextBox ID="txtReenterPassword" runat="server" CssClass="form-control" placeholder="Re-enter your Password" TextMode="Password"></asp:TextBox>
                        </div>

                        <%--input phoneNo--%>
                        <div>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Phone No. is required** " ControlToValidate="txtPhoneNo" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtPhoneNo" Display="Dynamic" ErrorMessage="Invalid phone no. entered. (E.g. 011-2221111)" ForeColor="Red" ValidationExpression="^01[0-46-9]-\d{3}-?\d{4,6}$"></asp:RegularExpressionValidator>

                        </div>
                        <div class="input_field">
                            <span><i aria-hidden="true" class="fas fa-phone"></i></span>
                            <asp:TextBox ID="txtPhoneNo" runat="server" CssClass="form-control" placeholder="Phone No. (E.g. 011-2223333)"></asp:TextBox>
                        </div>

                        <%--input email--%>
                        <div>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Email address is required** " ControlToValidate="txtEmail" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtEmail" Display="Dynamic" ErrorMessage="Invalid email address. (E.g. johndoe@gmail.com)" ForeColor="Red" ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"></asp:RegularExpressionValidator>

                            <asp:CustomValidator ID="cvAccountDuplicate" runat="server" CssClass="error" Display="Dynamic" ErrorMessage="Account duplicate detected" ForeColor="Red"></asp:CustomValidator>

                        </div>
                        <div class="input_field">
                            <span><i aria-hidden="true" class="fas fa-envelope "></i></span>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Email Address (E.g. johndoe@gmail.com)" TextMode="Email"></asp:TextBox>
                        </div>

                        <%--card number--%>
                        <div>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Card No.is required** " ControlToValidate="txtCardNo" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtCardNo" Display="Dynamic" ErrorMessage="Invalid card No. (E.g. 1111 2222 3333 4444)" ForeColor="Red" ValidationExpression="^\d{4}\s\d{4}\s\d{4}\s\d{4}$"></asp:RegularExpressionValidator>

                        </div>
                        <div class="input_field">
                            <span><i aria-hidden="true" class="fa fa-credit-card "></i></span>
                            <asp:TextBox ID="txtCardNo" runat="server" CssClass="form-control" placeholder="Card No.(E.g. 1111 2222 3333 4444)"></asp:TextBox>
                        </div>

                        <div class="row clearfix">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="GoodThru is required**. " ControlToValidate="txtGoodThru" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtGoodThru" Display="Dynamic" ErrorMessage="Invalid goodthru (E.g. 01/01)" ForeColor="Red" ValidationExpression="^(0[1-9]|1[0-2])\/(0[1-9]|[12][0-9]|3[01])$"></asp:RegularExpressionValidator>

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="CVV is required** " ControlToValidate="txtCvv" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtCvv" Display="Dynamic" ErrorMessage="Invalid Cvv (E.g. 012)" ForeColor="Red" ValidationExpression="^^[0-9]{3}$"></asp:RegularExpressionValidator>


                            <div class="col_half">
                                <%--good thru--%>
                                <div class="input_field">
                                    <span><i aria-hidden="true" class="fa fa-credit-card"></i></span>
                                    <asp:TextBox ID="txtGoodThru" runat="server" CssClass="form-control" placeholder="Good Thru"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col_half">
                                <%--cvv--%>
                                <div class="input_field">
                                    <span><i aria-hidden="true" class="fa fa-credit-card"></i></span>
                                    <asp:TextBox ID="txtCvv" runat="server" CssClass="form-control" placeholder="CVV"></asp:TextBox>
                                </div>
                            </div>
                        </div>


                        <%--address line 1--%>
                        <div>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Address 1 is required** " ControlToValidate="txtAddress1" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtAddress1" Display="Dynamic" ErrorMessage="Invalid Address 1 format." ForeColor="Red" ValidationExpression="^[a-zA-Z0-9\s#.,/-]+$"></asp:RegularExpressionValidator>
                        </div>
                        <div class="input_field">
                            <span><i aria-hidden="true" class="fa fa-map-marker "></i></span>
                            <asp:TextBox ID="txtAddress1" runat="server" CssClass="form-control" placeholder="Address Line 1"></asp:TextBox>
                        </div>

                        <%--address line 2--%>
                        <div>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Address 2 is required** " ControlToValidate="txtAddress2" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server" ControlToValidate="txtAddress2" Display="Dynamic" ErrorMessage="Invalid Address 2 format." ForeColor="Red" ValidationExpression="^[a-zA-Z0-9\s#.,/-]+$"></asp:RegularExpressionValidator>

                        </div>
                        <div class="input_field">
                            <span><i aria-hidden="true" class="fa fa-map-marker "></i></span>
                            <asp:TextBox ID="txtAddress2" runat="server" CssClass="form-control" placeholder="Address Line 2"></asp:TextBox>
                        </div>

                        <%--city--%>
                        <div>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="City is required** " ControlToValidate="ddlCity" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                        <div class="input_field">
                            <span><i aria-hidden="true" class="fa fa-map-marker "></i></span>

                            <asp:XmlDataSource ID="CitiesXmlDataSource" runat="server" DataFile="~/App_Data/MalaysianCities.xml" XPath="cities/city" />
                                    <asp:DropDownList ID="ddlCity" runat="server" DataSourceID="CitiesXmlDataSource" DataTextField="value" DataValueField="value" CssClass="city-list">
                                    </asp:DropDownList>
                        </div>

                        <div class="row clearfix">

                             <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ErrorMessage="State is required** " ControlToValidate="ddlState" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ErrorMessage="Poscode is required** " ControlToValidate="txtPoscode" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server" ControlToValidate="txtPoscode" Display="Dynamic" ErrorMessage="Invalid poscode (E.g. 11500)" ForeColor="Red" ValidationExpression="^\d{5}$"></asp:RegularExpressionValidator>

                            <%--state--%>
                            <div class="col_half">
                                <div>
                                </div>
                                <div class="input_field">
                                    <span><i aria-hidden="true" class="fa fa-map-marker "></i></span>
                                    <div>
                                        <asp:XmlDataSource ID="MalaysianStatesXmlDataSource" runat="server" DataFile="~/App_Data/MalaysianStates.xml" XPath="states/state" />
                                    <asp:DropDownList ID="ddlState" runat="server" DataSourceID="MalaysianStatesXmlDataSource" DataTextField="value" DataValueField="value" CssClass="state-list">
                                    </asp:DropDownList>

                                    </div>
                                    
                                </div>
                            </div>

                            <%--poscode--%>
                            <div class="col_half">
                                <div>
                                </div>
                                <div class="input_field">
                                    <span><i aria-hidden="true" class="fa fa-map-marker "></i></span>
                                    <asp:TextBox ID="txtPoscode" runat="server" CssClass="form-control" placeholder="Poscode"></asp:TextBox>
                                </div>
                            </div>
                        </div>






                        <%--gender--%>
                        <div>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="Gender is required** " ControlToValidate="rblGender" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                        <div class="input_field radio_option">
                            <asp:RadioButtonList ID="rblGender" runat="server">
                                <asp:ListItem Value="M">Male</asp:ListItem>
                                <asp:ListItem Value="F">Female</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>

                        <%--register button--%>
                        <asp:Label ID="lblMsg" Text="" runat="server" style="color:red;"/>

                        <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="button" OnClick="btnRegister_Click" />

                        <%--log in link--%>
                        <div class="text-center">

                            <asp:HyperLink ID="logInLink" runat="server" NavigateUrl="~/front-end/user-login.aspx" CssClass="lost_pass justify-content-center">
                Already have an account?
            <i class="bi bi-box-arrow-left fa-lg"></i>
                            </asp:HyperLink>
                        </div>

                    </div>
                </div>
            </div>
        </div>

    </div>
</asp:Content>
