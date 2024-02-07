<%@ Page Title="" Language="C#" MasterPageFile="~/back_end/backend.Master" AutoEventWireup="true" CodeBehind="CustomerRecord.aspx.cs" Inherits="timezone.back_end.record.customer.CustomerRecord" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="content-container">

        <div class="top-section mb-3">
            <span style="font-weight: bold; font-size: 30px;">Customer Record</span> &nbsp&nbsp
            <asp:Button ID="Button1" runat="server" Text="+ Add New" PostBackUrl="~/back_end/record/customer/AddCustomer.aspx" />

        </div>

        <asp:GridView ID="userGv" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="ADDRESS_ID,CUSTOMER_ID1,CARD_ID" DataSourceID="SqlDataSource1" ForeColor="Black" GridLines="Horizontal" CssClass="content-table" OnSelectedIndexChanged="userGv_SelectedIndexChanged" AllowPaging="True" AllowSorting="True">
            <Columns>
                <asp:BoundField DataField="CUSTOMER_ID" HeaderText="Cust ID" SortExpression="CUSTOMER_ID" />
                <asp:BoundField DataField="NAME" HeaderText="Name" SortExpression="NAME" />
                <asp:BoundField DataField="GENDER" HeaderText="Gender" SortExpression="GENDER" />
                <asp:BoundField DataField="EMAIL" HeaderText="Email" SortExpression="EMAIL" />
                <asp:BoundField DataField="PHONE" HeaderText="Phone" SortExpression="PHONE" />
                <asp:BoundField DataField="USERNAME" HeaderText="Username" SortExpression="USERNAME" />
                <asp:TemplateField HeaderText="Status" SortExpression="Status">
                    <ItemTemplate>
                        <%# Convert.ToBoolean(Eval("status")) ? "Active" : "Inactive" %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:Button ID="viewBtn" runat="server" Text="View" OnClick="viewBtn_Click" CommandArgument='<%# Eval("CUSTOMER_ID") %>' />
                        <asp:Button ID="editBtn" runat="server" Text="Edit" OnClick="editBtn_Click" CommandArgument='<%# Eval("CUSTOMER_ID") %>' />

                        <asp:Button ID="changeStatusBtn" runat="server" Text="Change Status" OnClick="changeStatusBtn_Click" CommandArgument='<%# Eval("CUSTOMER_ID") + "|" + Eval("STATUS") %>' />


                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
            <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F7F7F7" />
            <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
            <SortedDescendingCellStyle BackColor="#E5E5E5" />
            <SortedDescendingHeaderStyle BackColor="#242121" />
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT ADDRESS.ADDRESS_ID AS Expr3, ADDRESS.CUSTOMER_ID AS Expr4, ADDRESS.ADDRESS_LINE_1 AS Expr5, ADDRESS.ADDRESS_LINE_2 AS Expr6, ADDRESS.POSTCODE AS Expr7, ADDRESS.CITY AS Expr8, ADDRESS.STATE AS Expr9, CUSTOMER.CUSTOMER_ID AS Expr1, CUSTOMER.NAME AS Expr10, CUSTOMER.GENDER AS Expr11, CUSTOMER.EMAIL AS Expr12, CUSTOMER.PHONE AS Expr13, CUSTOMER.USERNAME AS Expr14, CUSTOMER.PASSWORD AS Expr15, CUSTOMER.DATE_REGISTERED AS Expr16, CARD.CARD_ID AS Expr17, CARD.CUSTOMER_ID AS Expr2, CARD.CARD_NUMBER AS Expr18, CARD.GOODTHRU AS Expr19, CARD.CVV AS Expr20, ADDRESS.*, CUSTOMER.*, CARD.* FROM ADDRESS INNER JOIN CUSTOMER ON ADDRESS.CUSTOMER_ID = CUSTOMER.CUSTOMER_ID INNER JOIN CARD ON CUSTOMER.CUSTOMER_ID = CARD.CUSTOMER_ID"></asp:SqlDataSource>
    </div>
</asp:Content>

