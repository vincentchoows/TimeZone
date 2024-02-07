<%@ Page Title="" Language="C#" MasterPageFile="~/front-end/frontend.Master" AutoEventWireup="true" CodeBehind="profile.aspx.cs" Inherits="TimeZone_Assign.front_end.profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--header title--%>
    <div class="order-banner top-banner">
        <div class="txt">PROFILE</div>
    </div>
    <%--header title end----------------------------------------------------------------------------------------------------------------%>

    <%--profile table------------------------------------------------------------------------------------------------------------------------%>

    <div class="wrap-content">
        <div class="container">
            <div class="row">
                <!-- Navigation -->
                <div class="col-3">
                    <!-- Profile Icon and name -->
                    <table>
                        <tr>
                            <td><i class="fa-solid fa-circle-user" style="font-size: 80px"></i></td>
                            <td>
                                
                                <b><asp:Label ID="lblName" runat="server" Text="Label"></asp:Label></b>
                                <br />
                                <span style="color: grey">Member</span>
                            </td>
                        </tr>
                    </table>
                    <!-- Navigation Menu -->
                    <div class="nav-list">
                        <a href="profile.aspx" class="active"><i class="fa-solid fa-id-card"></i>&nbsp&nbsp My Profile</a>
                        <br />
                        <br />
                        <a href="order-pending.aspx"><i class="fa-solid fa-clipboard-list"></i>&nbsp&nbsp&nbsp My Order</a>
                        <br />
                        <br />
                        <a href="order-pending.aspx">&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<i class="fa-solid fa-truck"></i>&nbsp Pending</a>
                        <br />
                        <br />
                        <a href="order-completed.aspx">&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<i class="fa-solid fa-circle-check"></i>&nbsp&nbsp Completed</a>
                    </div>
                </div>
                <!-- Left Div End-->
                <!-- Right Div Start-->
                <div class="right-div col-sm-9 col-xs-8 bg-light">
                    <!-- Title -->
                    <div class="title p-4" style="border-bottom: 2px solid lightgrey;">
                        My profile
                            <br />
                        <span style="font-size: 90%;">Manage your account</span>
                    </div>
                    <!-- Information -->


                    <div class="profile-page p-3">
                        <table class="table table-hover">
                            <tr>
                                <td class="display-field">Username</td>
                                <%--username textfield--%>
                                <td>
                                    <asp:TextBox ID="txtUsername" runat="server" CssClass="profile-input" ReadOnly="true" ></asp:TextBox></td>

                            </tr>
                            <tr>
                                <td style="width: 200px;">Name</td>
                                <td>
                                    <asp:TextBox ID="txtName" runat="server" CssClass="profile-input" ReadOnly="true" ></asp:TextBox></td>
                            </tr>
                             <tr>
                                <td style="width: 200px;">Gender</td>
                                <td>
                                    <asp:TextBox ID="txtGender" runat="server" CssClass="profile-input" ReadOnly="true" ></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Email</td>
                                <td>
                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="profile-input" ReadOnly="true"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Phone Number</td>
                                <td>
                                    <asp:TextBox ID="txtPhoneNo" runat="server" CssClass="profile-input" ReadOnly="true" ></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Address</td>
                                <td>
                                    <asp:TextBox ID="txtAddress" runat="server" CssClass="profile-input" ReadOnly="true" Text="2A, Lorong Teluk Tempoyak 2, 11960 Bayan Lepas, Pulau Pinang"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Card Number</td>
                                <td>
                                    <asp:TextBox ID="txtCardNo" runat="server" CssClass="profile-input" ReadOnly="true" Text="**** **** **** 8765"></asp:TextBox></td>
                            </tr>
                        </table>
                        <br />
                        <%--goToEditPage--%>
                        <asp:HyperLink ID="goEditPage" runat="server" NavigateUrl="~/front-end/edit-profile.aspx" CssClass="profile-btnEdit button" Style="text-decoration: none;"> Edit
                        </asp:HyperLink>

                        <br />
                    </div>

                </div>
                <!-- Right Div End-->
            </div>
        </div>
    </div>
    <%--profile table end--%>
</asp:Content>
