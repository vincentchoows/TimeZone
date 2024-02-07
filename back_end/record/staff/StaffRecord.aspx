<%@ Page Title="" Language="C#" MasterPageFile="~/back_end/BackEnd.Master" AutoEventWireup="true" CodeBehind="StaffRecord.aspx.cs" Inherits="TimeZone_Assign.back_end.record.staff.StaffRecord" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content-container">
        <div class="top-section">
            <span style="font-weight: bold; font-size: 30px;">Staff Record</span>
            <asp:Button ID="addStaffBtn" runat="server" Text="+ Add New" PostBackUrl="AddStaff.aspx" />
        </div>

        <asp:GridView ID="GridView1" runat="server" CssClass="content-table" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="EMPLOYEE_ID,ROLE_ID1" DataSourceID="SqlDataSource1" ForeColor="Black" GridLines="Horizontal" AllowPaging="True" AllowSorting="True">
            <Columns>
                <asp:BoundField DataField="NAME" HeaderText="Name" SortExpression="NAME" />
                <asp:BoundField DataField="POSITION" HeaderText="Position" SortExpression="POSITION" />
                <asp:BoundField DataField="EMAIL" HeaderText="Email" SortExpression="EMAIL">
                    <ItemStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="PHONE" HeaderText="Phone" SortExpression="PHONE" />
                <asp:BoundField DataField="DATE_REGISTERED" HeaderText="Date Registered" SortExpression="DATE_REGISTERED">
                    <ItemStyle Width="150px" />
                </asp:BoundField>
                <asp:BoundField DataField="CREATE_ACCESS" HeaderText="Create Access" SortExpression="CREATE_ACCESS">
                    <ItemStyle Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="READ_ACCESS" HeaderText="Read Access" SortExpression="READ_ACCESS" />
                <asp:BoundField DataField="EDIT_ACCESS" HeaderText="Edit Access" SortExpression="EDIT_ACCESS" />
                <asp:BoundField DataField="DELETE_ACCESS" HeaderText="Delete Access" SortExpression="DELETE_ACCESS">

                    <ItemStyle Height="100px" />
                </asp:BoundField>

                <asp:TemplateField HeaderText="Status" SortExpression="STATUS">
                    <ItemTemplate>
                        <%# Convert.ToBoolean(Eval("STATUS")) ? "Active" : "Inactive" %>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="editStaffBtn" OnClick="EditStaffbtn_Click" CommandArgument='<%# Eval("EMPLOYEE_ID") %>' CssClass="action-btn" />
                        <asp:Button ID="btnHidden" runat="server" Text='<%# Convert.ToBoolean(Eval("STATUS")) ? "Deactivate" : "Activate" %>' CommandArgument='<%# Eval("EMPLOYEE_ID") %>' OnClick="Hiddenbtn_Click" CssClass="action-btn" />
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
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT EMPLOYEE.*, ROLE.* FROM EMPLOYEE INNER JOIN ROLE ON EMPLOYEE.ROLE_ID = ROLE.ROLE_ID"></asp:SqlDataSource>
    </div>
</asp:Content>
