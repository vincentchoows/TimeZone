<%@ Page Title="" Language="C#" MasterPageFile="~/back_end/BackEnd.Master" AutoEventWireup="true" CodeBehind="ProductRecord.aspx.cs" Inherits="TimeZone_Assign.back_end.record.product.ProductRecord" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content-container">
        <div class="top-section">
            <span style="font-weight: bold; font-size: 30px;">Product Record</span> &nbsp&nbsp
            <asp:Button ID="addBtn" runat="server" Text="+ Add New" PostBackUrl="~/back_end/record/product/AddProduct.aspx" />
        </div>
        <asp:GridView ID="productGv" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="WATCH_ID" DataSourceID="SqlDataSource1" ForeColor="Black" GridLines="Horizontal" CssClass="content-table">
            <Columns>
                <asp:BoundField DataField="WATCH_ID" HeaderText="ID" ReadOnly="True" SortExpression="WATCH_ID" />
                <asp:TemplateField HeaderText="Name" SortExpression="NAME">
                    <ItemTemplate>
                        <%# Eval("name") + " " + Eval("reference_no") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="PRICE" HeaderText="Price" SortExpression="PRICE" DataFormatString="RM {0:#,##0.00}" />
                <asp:BoundField DataField="STOCK_QTY" HeaderText="Qty" SortExpression="STOCK_QTY" />
                <asp:TemplateField HeaderText="Status" SortExpression="Status">
                    <ItemTemplate>
                        <%# Convert.ToBoolean(Eval("status")) ? "Active" : "Inactive" %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:Button ID="viewBtn" runat="server" Text="View" OnClick="view_Click" CommandArgument='<%# Eval("watch_id") %>' />
                        <asp:Button ID="editBtn" runat="server" Text="Edit" OnClick="edit_Click" CommandArgument='<%# Eval("watch_id") %>' />
                        <asp:Button ID="hideBtn" runat="server" Text='<%# Convert.ToInt32(Eval("STATUS")) == 1 ? "Deactivate" : "Activate" %>' OnClick="btn_Click" CommandArgument='<%# Eval("watch_id") %>' />
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
    </div>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT CATEGORY.*, GALLERY.*, WATCH.* FROM CATEGORY INNER JOIN WATCH ON CATEGORY.CATEGORY_ID = WATCH.CATEGORY_ID INNER JOIN GALLERY ON WATCH.GALLERY_ID = GALLERY.GALLERY_ID"></asp:SqlDataSource>
</asp:Content>
