<%@ Page Title="" Language="C#" MasterPageFile="~/back_end/backend.Master" AutoEventWireup="true" CodeBehind="EditCustomer.aspx.cs" Inherits="timezone.back_end.record.customer.EditCustomer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 class="record-title">Edit Customer Details</h2>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT GALLERY.*, CATEGORY.*, WATCH.* FROM CATEGORY INNER JOIN WATCH ON CATEGORY.CATEGORY_ID = WATCH.CATEGORY_ID INNER JOIN GALLERY ON WATCH.GALLERY_ID = GALLERY.GALLERY_ID"></asp:SqlDataSource>
    <!-- Content Area -->
        <table class="content-area mb-4 customer-table" style="margin-left: 20em; width: 60%;">
            <!-- Information -->
            <tr>
                <td colspan="2" style="text-align: center; font-weight: bold; height: 43px;">Information</td>
            </tr>
            <tr>
                <td style="width: 20%">Customer ID </td>
                <td>
                    <asp:TextBox ID="tbCustID" runat="server" Enabled="False"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Name</td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Name is required** " ControlToValidate="tbName" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>

                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="tbName" Display="Dynamic" ErrorMessage="Invalid name entered. (E.g. John Doe)" ForeColor="Red" ValidationExpression="^[A-Z][a-zA-Z]*( [A-Z][a-zA-Z]*)*$"></asp:RegularExpressionValidator>

                    <asp:TextBox ID="tbName" runat="server"></asp:TextBox></td>
            </tr>
            <tr class="input-field">
                <td>Gender </td>

                <td class="pl-5 justify-content-center p">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="Gender is required** " ControlToValidate="rblGender" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>

                    <asp:RadioButtonList ID="rblGender" runat="server" CssClass="">
                        <asp:ListItem Value="F">Female</asp:ListItem>
                        <asp:ListItem Value="M">Male</asp:ListItem>

                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>Email </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Email address is required** " ControlToValidate="tbEmail" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="tbEmail" Display="Dynamic" ErrorMessage="Invalid email address. (E.g. johndoe@gmail.com)" ForeColor="Red" ValidationExpression="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"></asp:RegularExpressionValidator>

                    <asp:CustomValidator ID="cvAccountDuplicate" runat="server" CssClass="error" Display="Dynamic" ErrorMessage="Account duplicate detected" ForeColor="Red"></asp:CustomValidator>


                    <asp:TextBox ID="tbEmail" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Phone</td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Phone No. is required** " ControlToValidate="tbPhone" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="tbPhone" Display="Dynamic" ErrorMessage="Invalid phone no. entered. (E.g. 011-2221111)" ForeColor="Red" ValidationExpression="^01[0-46-9]-\d{3}-?\d{4,6}$"></asp:RegularExpressionValidator>

                    <asp:TextBox ID="tbPhone" runat="server"></asp:TextBox></td>
            </tr>

            <tr>
                <td>Username</td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Username is required** " ControlToValidate="tbUsername" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="tbUsername" Display="Dynamic" ErrorMessage="Invalid username entered. (E.g. john_doe123)" ForeColor="Red" ValidationExpression="^[a-zA-Z][a-zA-Z0-9]*(?:_[a-zA-Z0-9]+)*$"></asp:RegularExpressionValidator>

                    <asp:CustomValidator ID="cvUsernameDuplicate" runat="server" CssClass="error" Display="Dynamic" ErrorMessage="Username duplicate detected" ForeColor="Red"></asp:CustomValidator>


                    <asp:TextBox ID="tbUsername" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>New Password</td>
                <td>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="tbPassword" Display="Dynamic" ErrorMessage="Must contain one uppercase and lowercase letter, one digit, one special character, at least 8 characters <br/>" ForeColor="Red" ValidationExpression="^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[^a-zA-Z0-9]).{8,}$"></asp:RegularExpressionValidator>

                    <asp:TextBox ID="tbPassword" runat="server" TextMode="Password" CssClass="col-12"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Confirm Password</td>
                <td>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="tbPassword" ControlToValidate="tbReenterPassword" Display="Dynamic" ErrorMessage="Password does not match.<br/>" ForeColor="Red"></asp:CompareValidator>

                    <asp:TextBox ID="tbReenterPassword" runat="server" TextMode="Password" CssClass="col-12"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Date Registered</td>
                <td>
                    <asp:TextBox ID="tbDateReg" runat="server" Enabled="false"></asp:TextBox></td>
            </tr>


            <!-- --------------------------------- Address ----------------------------- -->
            <tr>
                <td colspan="2" style="text-align: center; font-weight: bold;">Address</td>
            </tr>
            <tr>
                <td>Address ID</td>
                <td>
                    <asp:TextBox ID="tbAddressId" runat="server" Enabled="false"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Address Line 1</td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Address 1 is required** " ControlToValidate="tbAddress1" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="tbAddress1" Display="Dynamic" ErrorMessage="Invalid Address 1 format." ForeColor="Red" ValidationExpression="^[a-zA-Z0-9\s#.,/-]+$"></asp:RegularExpressionValidator>

                    <asp:TextBox ID="tbAddress1" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Address Line 2 </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Address 2 is required** " ControlToValidate="tbAddress2" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server" ControlToValidate="tbAddress2" Display="Dynamic" ErrorMessage="Invalid Address 2 format." ForeColor="Red" ValidationExpression="^[a-zA-Z0-9\s#.,/-]+$"></asp:RegularExpressionValidator>

                    <asp:TextBox ID="tbAddress2" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Postcode</td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="Poscode is required** " ControlToValidate="tbPostcode" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server" ControlToValidate="tbPostcode" Display="Dynamic" ErrorMessage="Invalid poscode (E.g. 11500)" ForeColor="Red" ValidationExpression="^\d{5}$"></asp:RegularExpressionValidator>

                    <asp:TextBox ID="tbPostcode" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>City</td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="City is required** " ControlToValidate="ddlCity" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                    
                    <asp:XmlDataSource ID="CitiesXmlDataSource" runat="server" DataFile="~/App_Data/MalaysianCities.xml" XPath="cities/city" />
                    <asp:DropDownList ID="ddlCity" runat="server" DataSourceID="CitiesXmlDataSource" DataTextField="value" DataValueField="value" CssClass="city-list">
                        <asp:ListItem></asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="tbCity" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
            </tr>
            <tr>
                <td>State</td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="State is required** " ControlToValidate="ddlState" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>

                    <asp:XmlDataSource ID="MalaysianStatesXmlDataSource" runat="server" DataFile="~/App_Data/MalaysianStates.xml" XPath="states/state" />
                    <asp:DropDownList ID="ddlState" runat="server" DataSourceID="MalaysianStatesXmlDataSource" DataTextField="value" DataValueField="value" CssClass="state-list">
                    </asp:DropDownList>

                    <asp:TextBox ID="tbState" runat="server" Enabled="false"></asp:TextBox></td>
            </tr>

            <!-- Card Details-->
            <tr>
                <td colspan="2" style="text-align: center; font-weight: bold;">Card Details</td>
            </tr>
            <tr>
                <td>Card ID</td>
                <td>
                    <asp:TextBox ID="tbCardId" runat="server" Enabled="false"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Card No.</td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Card No.is required** " ControlToValidate="tbCardNo" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="tbCardNo" Display="Dynamic" ErrorMessage="Invalid card No. (E.g. 1111 2222 3333 4444)" ForeColor="Red" ValidationExpression="^\d{4}\s\d{4}\s\d{4}\s\d{4}$"></asp:RegularExpressionValidator>

                    <asp:TextBox ID="tbCardNo" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Good Thru</td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="GoodThru is required**. " ControlToValidate="tbGoodThru" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="tbGoodThru" Display="Dynamic" ErrorMessage="Invalid goodthru (E.g. 01/01)" ForeColor="Red" ValidationExpression="^(0[1-9]|1[0-2])\/(0[1-9]|[12][0-9]|3[01])$"></asp:RegularExpressionValidator>

                    <asp:TextBox ID="tbGoodThru" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>CVV</td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="CVV is required** " ControlToValidate="tbCvv" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="tbCvv" Display="Dynamic" ErrorMessage="Invalid Cvv (E.g. 012)" ForeColor="Red" ValidationExpression="^^[0-9]{3}$"></asp:RegularExpressionValidator>

                    <asp:TextBox ID="tbCvv" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2" class="button-area">


                    <asp:Button ID="confirmBtn" runat="server" Text="Confirm" CssClass="button" OnClick="confirmBtn_Click" />

                    <asp:Button ID="cancelBtn" runat="server" Text="Cancel" CssClass="button" NavigateUrl="~/back_end/record/customer/CustomerRecord.aspx" OnClick="cancelBtn_Click" OnClientClick="return true;" />



                </td>
            </tr>

        </table>
</asp:Content>
