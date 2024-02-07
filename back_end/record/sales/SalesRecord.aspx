<%@ Page Title="" Language="C#" MasterPageFile="~/back_end/BackEnd.Master" AutoEventWireup="true" CodeBehind="SalesRecord.aspx.cs" Inherits="TimeZone_Assign.back_end.record.sales.SalesRecord" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content-container">
        <div class="top-section">
            <span style="font-weight: bold; font-size: 30px;">Sales Record</span> &nbsp&nbsp
        </div>

        <asp:GridView ID="GridView1" runat="server" CssClass="content-table" OnRowDataBound="GridView1_RowDataBound" OnRowCommand="GridView1_RowCommand" AutoGenerateColumns="False" DataKeyNames="CUSTOMER_ID,WATCH_ID,ORDER_ID,WATCH_ID1,REVIEW_ID,PAYMENT_ID,ORDER_ID1" DataSourceID="SqlDataSource1" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" AllowPaging="True" AllowSorting="True">
            <Columns>
                <asp:BoundField DataField="PAYMENT_ID" HeaderText="Payment ID" ReadOnly="True" SortExpression="PAYMENT_ID" />
                <asp:TemplateField HeaderText="Customer" SortExpression="name">
                    <ItemTemplate>
                        <%# Eval("NAME") + "<br />" + Eval("EMAIL") + "<br />" + Eval("PHONE") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Order Details" ControlStyle-Width="100px">
                    <ItemTemplate>
                        <%# Eval("ORDER_ID") + "<br />"%>
                        <asp:Repeater ID="rptWatch" runat="server" DataSource='<%# getWatch(Convert.ToString(Eval("PAYMENT_ID"))) %>'>
                            <ItemTemplate>
                                <%# Container.DataItem %><br />
                            </ItemTemplate>
                        </asp:Repeater>
                    </ItemTemplate>

                    <ControlStyle Width="100px"></ControlStyle>

                </asp:TemplateField>
                <asp:BoundField DataField="AMOUNT" HeaderText="Amount" SortExpression="AMOUNT" />
                <asp:BoundField DataField="PAYMENT_DATE" HeaderText="Payment Date" SortExpression="PAYMENT_DATE" />
                <asp:BoundField DataField="PAYMENT_METHOD" HeaderText="Payment Method" SortExpression="PAYMENT_METHOD" />
                <asp:TemplateField ConvertEmptyStringToNull="False" HeaderText="Status" SortExpression="status">
                    <ItemTemplate>
                        <%# Convert.ToInt32(Eval("STATUS2")) == 1 ? "Active" : "Inactive" %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:Button ID="btnEdit" runat="server" Text='<%# Convert.ToInt32(Eval("STATUS2")) == 0 ? "Activate" : "Deactivate" %>' CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CommandName="Edit" CssClass="action-btn" />


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
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT CUSTOMER.*, WATCH.*, ORDER_ITEM.*, PAYMENT.*, ORDERS.* FROM PAYMENT INNER JOIN ORDERS ON PAYMENT.PAYMENT_ID = ORDERS.PAYMENT_ID INNER JOIN ORDER_ITEM ON ORDERS.ORDER_ID = ORDER_ITEM.ORDER_ID INNER JOIN WATCH ON ORDER_ITEM.WATCH_ID = WATCH.WATCH_ID INNER JOIN CUSTOMER ON ORDERS.CUSTOMER_ID = CUSTOMER.CUSTOMER_ID "></asp:SqlDataSource>


    </div>
</asp:Content>
