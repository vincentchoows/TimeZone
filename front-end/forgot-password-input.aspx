<%@ Page Title="" Language="C#" MasterPageFile="~/front-end/frontend.Master" AutoEventWireup="true" CodeBehind="forgot-password-input.aspx.cs" Inherits="TimeZone_Assign.front_end.forgot_password_input" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Banner -->
    <div class="edit-banner top-banner">
        <div class="txt">FORGET PASSWORD</div>
    </div>

    <div class=" forgot-pswd">
        <div class="modal-bg-1 justify-content-center">
            <div class="modal-content container m-5">
                <div class="row justify-content-center">
                    <div class="col-md-10 ">
                        <%--back link--%>
                        <asp:HyperLink ID="goBackLink" runat="server" NavigateUrl="~/front-end/user-login.aspx" CssClass="back-to-home btn-icon fa fa-arrow-left">
                        </asp:HyperLink>
                        <%--homepage link--%>
                        <asp:HyperLink ID="homeLink" runat="server" NavigateUrl="~/front-end/homepage.aspx" CssClass="company-name">TimeZone
                                <input type="hidden" name="from" value="backToHome">
                        </asp:HyperLink>
                        <div class=" p-5">
                            <h4 class="p-4">Forgot Password</h4>

                            <span style="color: grey; font-size: smaller;">Please enter your username or email to reset your password.</span>
                            <br />
                            <div class="row contact_form">
                                <div class="col-md-12 form-group p_star">
                                    <%--email--%>
                                    <asp:CustomValidator ID="cvAccount" runat="server" CssClass="error" Display="Dynamic" ErrorMessage="No account detected" ForeColor="Red"></asp:CustomValidator>

                                    <asp:RequiredFieldValidator ID="cvEmailRequired" runat="server" ErrorMessage="<br/>Password is required** " ControlToValidate="txtEmail" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>

                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtEmail" Display="Dynamic" ErrorMessage="Invalid email address. (E.g. johndoe@gmail.com)" ForeColor="Red" ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"></asp:RegularExpressionValidator>

                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Username/Email"></asp:TextBox>
                                    <br />
                                    <span style="color: grey; font-size: smaller;">Security Code</span>
                                    <div class="row contact_form">
                                        <div>


                                            <%--security question--%>
                                            <asp:CustomValidator ID="cvMatch" runat="server" CssClass="error" Display="Dynamic" ErrorMessage="Security code did not match" ForeColor="Red"></asp:CustomValidator>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="<br/>Security code is required** " ControlToValidate="txtSecurityQuestion" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>

                                            <asp:TextBox ID="txtSecurityQuestion" runat="server" CssClass="form-control" placeholder="Security code"></asp:TextBox>
                                            <br />
                                        </div>
                                    </div>
                                    <span style="color: grey; font-size: smaller;">New Password </span>
                                    <br />
                                    <div class="row contact_form">
                                        <div class="col-md-12 form-group p_star">
                                            <%--new password--%>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="<br/>Password is required** " ControlToValidate="txtNewPassword" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtNewPassword" Display="Dynamic" ErrorMessage="<br/>Must contain one uppercase and lowercase letter, one digit, one special character, at least 8 characters" ForeColor="Red" ValidationExpression="^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[^a-zA-Z0-9]).{8,}$"></asp:RegularExpressionValidator>

                                            <asp:TextBox ID="txtNewPassword" TextMode="Password" runat="server" CssClass="form-control" placeholder="New password"></asp:TextBox>
                                        </div>
                                    </div>
                                    <br />
                                    <span style="color: grey; font-size: smaller;">Reenter Password </span>
                                    <br />
                                    <div class="row contact_form">
                                        <div class="col-md-12 form-group p_star">


                                            <%--reenter password--%>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="<br/>Password is required** " ControlToValidate="txtReenterPassword" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtNewPassword" ControlToValidate="txtReenterPassword" Display="Dynamic" ErrorMessage="Password does not match." ForeColor="Red"></asp:CompareValidator>

                                            <asp:TextBox TextMode="Password" ID="txtReenterPassword" runat="server" CssClass="form-control" placeholder="Reenter password"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12 form-group justify-content-center text-center">
                                    <br />
                                    <%--continue button--%>
                                    <asp:Label ID="lblTest" runat="server" Text=""></asp:Label>
                                    <asp:Button ID="btnContinue" runat="server" Text="Get Code" CssClass="button justify-content-center" OnClick="btnContinue_Click" CausesValidation="false" style="width:100%"/>
                                    <br />
                                    <br />
                                     <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="button justify-content-center" style="width:100%;" OnClick="btnReset_Click" />
                                    <br />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </div>



    <script src="./assets/js/vendor/modernizr-3.5.0.min.js"></script>
    <!-- Jquery, Popper, Bootstrap -->
    <script src="./assets/js/vendor/jquery-1.12.4.min.js"></script>
    <script src="./assets/js/popper.min.js"></script>
    <script src="./assets/js/bootstrap.min.js"></script>
    <!-- Jquery Mobile Menu -->
    <script src="./assets/js/jquery.slicknav.min.js"></script>

    <!-- Jquery Slick , Owl-Carousel Plugins -->
    <script src="./assets/js/owl.carousel.min.js"></script>
    <script src="./assets/js/slick.min.js"></script>

    <!-- One Page, Animated-HeadLin -->
    <script src="./assets/js/wow.min.js"></script>
    <script src="./assets/js/animated.headline.js"></script>
    <script src="./assets/js/jquery.magnific-popup.js"></script>

    <!-- Scrollup, nice-select, sticky -->
    <script src="./assets/js/jquery.scrollUp.min.js"></script>
    <script src="./assets/js/jquery.nice-select.min.js"></script>
    <script src="./assets/js/jquery.sticky.js"></script>

    <!-- Jquery Plugins, main Jquery -->
    <script src="./assets/js/plugins.js"></script>
    <script src="./assets/js/main.js"></script>
</asp:Content>
