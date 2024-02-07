<%@ Page Title="" Language="C#" MasterPageFile="~/back_end/backend.Master" AutoEventWireup="true" CodeBehind="BackEndHomePage.aspx.cs" Inherits="TimeZone_Assign.back_end.BackEnd" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="content">

            <div class="cards">
                <div class="card">
                    <div class="box">
                        <asp:Label ID="Label1" runat="server" CssClass="dashboardlabel"></asp:Label>
                        <h3>Customer</h3>
                    </div>
                    <div class="icon-design">
                        <i class="bi bi-person-fill fa-3x"></i>
                    </div>
                </div>

                <div class="card">
                    <div class="box">
                        <asp:Label ID="Label2" runat="server" CssClass="dashboardlabel"></asp:Label>
                        <h3>Employee</h3>
                    </div>
                    <div class="icon-design">
                        <i class="bi bi-gift-fill fa-3x"></i>
                    </div>
                </div>

                <div class="card">
                    <div class="box">
                        <asp:Label ID="Label3" runat="server" CssClass="dashboardlabel"></asp:Label>
                        <h3>Orders</h3>
                    </div>
                    <div class="icon-design">
                        <i class="bi bi-bag-fill fa-3x"></i>
                    </div>
                </div>

                <div class="card">
                    <div class="box">
                        <asp:Label ID="Label4" runat="server" CssClass="dashboardlabel"></asp:Label>
                        <h3>Product</h3>
                    </div>
                    <div class="icon-design">
                        <i class="bi bi-cart-fill fa-3x"></i>
                    </div>
                </div>
            </div>

            <div class="content-ii">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ORDER_ID" DataSourceID="SqlDataSource1" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" CssClass="BEHPgridview" Height="181px" Width="1237px">
                    <Columns>
                        <asp:BoundField DataField="CUSTOMER_ID" HeaderText="CUSTOMER_ID" SortExpression="CUSTOMER_ID" />
                        <asp:BoundField DataField="ORDER_ID" HeaderText="ORDER_ID" SortExpression="ORDER_ID" />
                        <asp:BoundField DataField="PAYMENT_ID" HeaderText="PAYMENT_ID" SortExpression="PAYMENT_ID" />
                        <asp:BoundField DataField="DELIVERY_ID" HeaderText="DELIVERY_ID" SortExpression="DELIVERY_ID" />
                    </Columns>
                    <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                    <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                    <RowStyle BackColor="White" ForeColor="#330099" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                    <SortedAscendingCellStyle BackColor="#FEFCEB" />
                    <SortedAscendingHeaderStyle BackColor="#AF0101" />
                    <SortedDescendingCellStyle BackColor="#F6F0C0" />
                    <SortedDescendingHeaderStyle BackColor="#7E0000" />
                </asp:GridView>

                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [ORDERS]"></asp:SqlDataSource>

            </div>

        </div>
    </div>
</asp:Content>
