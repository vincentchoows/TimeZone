<%@ Page Title="" Language="C#" MasterPageFile="~/front-end/frontend.Master" AutoEventWireup="true" CodeBehind="staff-login.aspx.cs" Inherits="TimeZone_Assign.front_end.staff_login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Banner -->
    <div class="login-banner top-banner">
        <div class="txt">STAFF LOGIN</div>
    </div>

    <!--================ login_part Area =================-->
    <section class="login_part section_padding">
        <div class="container">
            <div class="row align-items-center" style="margin: 100px 0;">

                <%--left side--%>
                <div class="col-lg-6 col-md-6">
                    <div class="login_part_form">
                        <div class="login_part_form_iner">
                            <h3>Welcome Back !
                                    <br />
                                Please Sign in now</h3>

                            <div class="row contact_form">
                                <div class="col-md-12 form-group p_star">

                                    <%--input username--%>
                                    <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Username"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Username is required" ControlToValidate="txtUsername" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>

                                </div>
                                <br />
                                <div class="col-md-12 form-group p_star">
                                    <%--input password--%>
                                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" Style="padding-top: 15px;" placeholder="Password" TextMode="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Password is required" ControlToValidate="txtPassword" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                    
                                    <asp:CustomValidator ID="cvNotMatched" runat="server" Display="Dynamic" ErrorMessage="Password and Username not matched" ForeColor="Red"></asp:CustomValidator>
                                </div>
                                <div class="col-md-12 form-group">

                                    <br />
                                    <br />
                                    <%--log in button--%>
                                    <%--PostBackUrl="~/back_end/BackEndHomePage.aspx"--%>
                                    <asp:Button ID="btnLogIn" runat="server" Text="Log In" CssClass="button" Style="width: 100%" OnClick="btnLogIn_Click" />
                                    <br />
                                    <br />

                                </div>
                            </div>

                        </div>
                    </div>
                </div>


                <%--right side--%>
                <div class="col-lg-6 col-md-6 ">
                    <div class="login_part_text text-center ">
                        <div class="login_part_subcontainer pt-5">
                            <br />
                            <br />
                            <br />
                            <h2>Welcome</h2>
                            <p>
                                There are advances being made in science and technology everyday, and a good example of this is us, TimeZone
                            </p>
                            <br />
                            <br />

                        </div>
                    </div>
                </div>

            </div>
        </div>
    </section>
    <!--================login_part end =================-->
</asp:Content>
