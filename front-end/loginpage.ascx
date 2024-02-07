<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="loginpage.ascx.cs" Inherits="TimeZone_Assign.front_end.loginpage" %>


<!--================ login_part Area =================-->
    <section class="login_part section_padding">
        <div class="container">
            <div class="row align-items-center" style="margin: 100px 0;">

                <%--left side--%>
                <div class="col-lg-6 col-md-6 ">
                    <div class="login_part_text text-center ">
                        <div class="login_part_subcontainer pt-5">
                            <br />
                            <br />
                            <br />
                            <h2>New to our Shop?</h2>
                            <p>
                                There are advances being made in science and technology everyday, and a good example of this is us, TimeZone
                            </p>
                            <br />
                            <br />
                            <%--register button--%>
                            <asp:HyperLink ID="createAccLink" runat="server" NavigateUrl="~/front-end/register.aspx" CssClass="btn_3">
                Create An Account
            <i class="bi bi-box-arrow-left fa-lg"></i>
                            </asp:HyperLink>
                        </div>
                    </div>
                </div>


                <%--right side--%>
                <div class="col-lg-6 col-md-6">
                    <div class="login_part_form">
                        <div class="login_part_form_iner">
                            <h3>Welcome Back !
                                    <br />
                                Please Sign in now</h3>

                            <div class="row contact_form">

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Username is required** " ControlToValidate="txtUsername" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>

                                <asp:CustomValidator ID="cvNotMatched" runat="server" CssClass="error" Display="Dynamic" ErrorMessage="Username and password did not matched." ForeColor="Red"></asp:CustomValidator>

                                <asp:CustomValidator ID="cvInvalidLogin" runat="server" CssClass="error" Display="Dynamic" ErrorMessage="Invalid login. Please use the Staff login page. " ForeColor="Red"></asp:CustomValidator>

                                <asp:CustomValidator ID="cvAccountDeactivated" runat="server" CssClass="error" Display="Dynamic" ErrorMessage="Account deactivated. Please use another account." ForeColor="Red"></asp:CustomValidator>

                                <div class="col-md-12 form-group p_star">

                                    <%--input username--%>
                                    <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Username"></asp:TextBox>
                                </div>

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Password is required** " ControlToValidate="txtPassword" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>

                                <br />
                                <div class="col-md-12 form-group p_star">
                                    <%--input password--%>
                                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" Style="padding-top: 15px;" placeholder="Password" TextMode="Password"></asp:TextBox>

                                </div>
                                <div class="col-md-12 form-group">

                                    <br />
                                    <br />
                                    <%--log in button--%>



                                    <asp:Button ID="btnLogIn" runat="server" Text="Log In" CssClass="button" Style="width: 100%" OnClick="btnLogIn_Click" />
                                    <br />
                                    <br />
                                    <%--<%--forgot password page--%>
                                    <asp:HyperLink ID="forgotPswdLink" runat="server" NavigateUrl="~/front-end/forgot-password-input.aspx" CssClass="lost_pass">
                Forgot Password? 
            <i class="bi bi-box-arrow-left fa-lg"></i>
                                    </asp:HyperLink>



                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <!--================login_part end =================-->