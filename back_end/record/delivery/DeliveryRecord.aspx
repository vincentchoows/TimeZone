<%@ Page Title="" Language="C#" MasterPageFile="~/back_end/BackEnd.Master" AutoEventWireup="true" CodeBehind="DeliveryRecord.aspx.cs" Inherits="TimeZone_Assign.back_end.record.delivery.DeliveryRecord" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content-container">
        <h2 class="record-title">Delivery Record</h2>

        <asp:GridView ID="GridView1" runat="server" CssClass="content-table" OnRowDataBound="GridView1_RowDataBound" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="DELIVERY_ID" DataSourceID="SqlDataSource1" ForeColor="Black" GridLines="Horizontal" AllowPaging="True" AllowSorting="True">
            <Columns>
                <asp:BoundField DataField="DELIVERY_ID" HeaderText="Delivery ID" SortExpression="DELIVERY_ID" ReadOnly="True" />
                <asp:TemplateField HeaderText="Customer">
                    <ItemTemplate>
                        <%# Eval("NAME") + "<br />" + Eval("PHONE") + "<br />" + Eval("SHIPPING_ADDRESS")%>
                    </ItemTemplate>
                    <ItemStyle Width="200" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Order Items">
                    <ItemTemplate>
                        <asp:Repeater ID="rptWatch" runat="server" DataSource='<%# getWatch(Convert.ToString(Eval("DELIVERY_ID"))) %>'>
                            <ItemTemplate>
                                <%# Container.DataItem %>
                                <br />
                            </ItemTemplate>
                        </asp:Repeater>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="EST_EARLIEST_DATE" HeaderText="Est Earliest Date" SortExpression="EST_EARLIEST_DATE" DataFormatString="{0:dd-MM-yyyy}" />
                <asp:BoundField DataField="EST_LATEST_DATE" HeaderText="Est Latest Date" SortExpression="EST_LATEST_DATE" DataFormatString="{0:dd-MM-yyyy}" />
                <asp:BoundField DataField="ARRIVAL_DATE" HeaderText="Arrival Date" SortExpression="ARRIVAL_DATE" DataFormatString="{0:dd-MM-yyyy}" />
                <asp:BoundField DataField="STATUS" HeaderText="Status" SortExpression="STATUS" />
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" OnClick="EditDeliverybtn_Click" CommandArgument='<%# Eval("DELIVERY_ID") %>' CssClass="action-btn" />
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

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT DELIVERY.*, CUSTOMER.*, ORDERS.*, ORDER_ITEM.*, WATCH.* FROM DELIVERY INNER JOIN ORDERS ON DELIVERY.DELIVERY_ID = ORDERS.DELIVERY_ID INNER JOIN CUSTOMER ON ORDERS.CUSTOMER_ID = CUSTOMER.CUSTOMER_ID INNER JOIN ORDER_ITEM ON ORDERS.ORDER_ID = ORDER_ITEM.ORDER_ID INNER JOIN WATCH ON WATCH.WATCH_ID = ORDER_ITEM.WATCH_ID"></asp:SqlDataSource>

    </div>
</asp:Content>
