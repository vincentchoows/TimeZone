<%@ Page Title="" Language="C#" MasterPageFile="~/back_end/BackEnd.Master" AutoEventWireup="true" CodeBehind="ReviewRecord.aspx.cs" Inherits="TimeZone_Assign.back_end.record.review.ReviewRecord" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content-container">
        <h2 class="record-title">Product Review Record</h2>
        <asp:GridView CssClass="content-table" ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="CUSTOMER_ID,ORDER_ID,WATCH_ID,REVIEW_ID,ORDER_ID1,REPLY_ID,REVIEW_ID1,WATCH_ID1" DataSourceID="SqlDataSource1" ForeColor="Black" GridLines="Horizontal" AllowPaging="True" AllowSorting="True" PageSize="5">
            <Columns>
                <asp:BoundField DataField="REVIEW_ID" HeaderText="Review ID" ReadOnly="True" SortExpression="REVIEW_ID" />
                <asp:TemplateField HeaderText="Customer" ItemStyle-Width="100px" SortExpression="name">
                    <ItemTemplate>
                        <%# Eval("NAME") + "<br />" + Eval("EMAIL") + "<br />" + Eval("PHONE") %>
                    </ItemTemplate>

                    <ItemStyle Width="100px"></ItemStyle>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Product" ControlStyle-Width="100px" SortExpression="REFERENCE_NO">
                    <ItemTemplate>
                        <%--<%# Eval("NAME1") + " " + Eval("REFERENCE_NO")%>--%>
                        <%# Eval("REFERENCE_NO") %>
                    </ItemTemplate>
                    <ControlStyle Width="100px"></ControlStyle>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Rating/Comment" ControlStyle-BorderWidth="100px" SortExpression="RATING">
                    <ItemTemplate>
                        <%# Eval("RATING") + " out of 5 <br />" + Eval("COMMENT") %>
                    </ItemTemplate>
                    <ControlStyle BorderWidth="100px"></ControlStyle>
                    <ItemStyle Width="250px" />
                </asp:TemplateField>

                <asp:BoundField DataField="REPLY" HeaderText="Staff Reply">
                    <ItemStyle Width="200px" />
                </asp:BoundField>

                <asp:TemplateField HeaderText="Status" SortExpression="Status">
                    <ItemTemplate>
                        <%# Convert.ToBoolean(Eval("STATUS1")) ? "Visible" : "Hidden" %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:Button ID="btnEdit" runat="server" Text='<%# (Eval("REPLY") != DBNull.Value && Eval("REPLY").ToString() != "") ? "Edit reply" : "Add reply" %>' CommandArgument='<%# Eval("REVIEW_ID") %>' OnClick="btnEdit_Click" CssClass="action-btn" /><br />
                        <asp:Button ID="btnDelete" runat="server" Text="Delete reply" CommandArgument='<%# Eval("REVIEW_ID") %>' CssClass="action-btn" OnClick="btnDelete_Click" Enabled='<%# Eval("REPLY").ToString() != "" ? true : false %>' /><br />
                        <asp:Button ID="btnHide" runat="server" Text='<%# Convert.ToBoolean(Eval("Status1")) ? "Hide Review" : "Show Review" %>' CommandArgument='<%# Eval("REVIEW_ID") %>' OnClick="btnHide_Click" CssClass="action-btn" />
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
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT CUSTOMER.*, ORDER_ITEM.*, ORDERS.*, REPLY.*, REVIEW.*, WATCH.* FROM CUSTOMER INNER JOIN ORDERS ON CUSTOMER.CUSTOMER_ID = ORDERS.CUSTOMER_ID INNER JOIN ORDER_ITEM ON ORDERS.ORDER_ID = ORDER_ITEM.ORDER_ID
                    INNER JOIN REVIEW ON ORDER_ITEM.REVIEW_ID = REVIEW.REVIEW_ID
                    LEFT JOIN REPLY ON REVIEW.REPLY_ID = REPLY.REPLY_ID
                    INNER JOIN WATCH ON ORDER_ITEM.WATCH_ID = WATCH.WATCH_ID"></asp:SqlDataSource>
    </div>
</asp:Content>
