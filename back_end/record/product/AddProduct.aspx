<%@ Page Title="" Language="C#" MasterPageFile="~/back_end/BackEnd.Master" AutoEventWireup="true" CodeBehind="AddProduct.aspx.cs" Inherits="TimeZone_Assign.back_end.record.product.AddProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 class="record-title">Add Product</h2>
    <!-- Content Area -->
    <table class="content-area">
        <!-- Information -->
        <tr>
            <td colspan="2" style="text-align: center; font-weight: bold; height: 43px;">Information</td>
        </tr>
        <tr>
            <td>Watch ID </td>
            <td>
                <asp:TextBox ID="tbWatchID" runat="server" Enabled="False"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Reference No. </td>
            <td>
                <asp:TextBox ID="tbReferenceNo" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" Display="Dynamic" ErrorMessage="*Required" ForeColor="Red" SetFocusOnError="True" ControlToValidate="tbReferenceNo"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="tbReferenceNo" Display="Dynamic" ErrorMessage="Please enter a valid reference no. (only letters and numbers)." ForeColor="Red" ValidationExpression="^[a-zA-Z0-9]+$" SetFocusOnError="True"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td>Price (RM) </td>
            <td>
                <asp:TextBox ID="tbPrice" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" Display="Dynamic" ErrorMessage="*Required" ForeColor="Red" SetFocusOnError="True" ControlToValidate="tbPrice"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="tbPrice" Display="Dynamic" ErrorMessage="Please enter a valid price using only numbers and a maximum of 2 decimal places." ForeColor="Red" ValidationExpression="^\d+(\.\d{2})?$" SetFocusOnError="True"></asp:RegularExpressionValidator>
                <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="tbPrice" Display="Dynamic" ErrorMessage="Please enter a valid price between 0.00 and 10000000.00." ForeColor="Red" Type="Double" MaximumValue="10000000.00" MinimumValue="0.00" SetFocusOnError="True"></asp:RangeValidator>
            </td>
        </tr>
        <tr>
            <td>Qty Left </td>
            <td>
                <asp:TextBox ID="tbQtyLeft" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" Display="Dynamic" ErrorMessage="*Required" ForeColor="Red" SetFocusOnError="True" ControlToValidate="tbQtyLeft"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="tbQtyLeft" Display="Dynamic" ErrorMessage="Please enter a valid quantity (only numbers). " ForeColor="Red" ValidationExpression="^\d+$" SetFocusOnError="True"></asp:RegularExpressionValidator>
                <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="tbQtyLeft" Display="Dynamic" ErrorMessage="Please enter a valid quantity between 0 and 100." ForeColor="Red" Type="Integer" MinimumValue="0" MaximumValue="100" SetFocusOnError="True"></asp:RangeValidator>
            </td>
        </tr>
        <tr>
            <td>Status </td>
            <td>
                <asp:DropDownList ID="ddlStatus" runat="server">
                    <asp:ListItem Value="1" Text="Active"></asp:ListItem>
                    <asp:ListItem Value="0" Text="Inactive"></asp:ListItem>
                </asp:DropDownList></td>
        </tr>
        <!-- Category -->
        <tr>
            <td colspan="2" style="text-align: center; font-weight: bold;">Category</td>
        </tr>
        <tr>
            <td>Name </td>
            <td>
                <asp:DropDownList ID="ddlCategory" runat="server"></asp:DropDownList></td>
        </tr>
        <!-- Specifications -->
        <tr>
            <td colspan="2" style="text-align: center; font-weight: bold;">Specifications</td>
        </tr>
        <tr>
            <td>Model Case</td>
            <td>
                <asp:TextBox ID="tbModelCase" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ErrorMessage="*Required" ForeColor="Red" SetFocusOnError="True" ControlToValidate="tbModelCase"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Bezel</td>
            <td>
                <asp:TextBox ID="tbBezel" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ErrorMessage="*Required" ForeColor="Red" SetFocusOnError="True" ControlToValidate="tbBezel"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Water Resistance</td>
            <td>
                <asp:TextBox ID="tbWaterResistance" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" ErrorMessage="*Required" ForeColor="Red" SetFocusOnError="True" ControlToValidate="tbWaterResistance"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Movement</td>
            <td>
                <asp:TextBox ID="tbMovement" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic" ErrorMessage="*Required" ForeColor="Red" SetFocusOnError="True" ControlToValidate="tbMovement"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Calibre</td>
            <td>
                <asp:TextBox ID="tbCalibre" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="Dynamic" ErrorMessage="*Required" ForeColor="Red" SetFocusOnError="True" ControlToValidate="tbCalibre"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Power Reserve</td>
            <td>
                <asp:TextBox ID="tbPowerReserve" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Display="Dynamic" ErrorMessage="*Required" ForeColor="Red" SetFocusOnError="True" ControlToValidate="tbPowerReserve"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Bracelet</td>
            <td>
                <asp:TextBox ID="tbBracelet" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" Display="Dynamic" ErrorMessage="*Required" ForeColor="Red" SetFocusOnError="True" ControlToValidate="tbBracelet"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Dial</td>
            <td>
                <asp:TextBox ID="tbDial" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" Display="Dynamic" ErrorMessage="*Required" ForeColor="Red" SetFocusOnError="True" ControlToValidate="tbDial"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Certification</td>
            <td>
                <asp:TextBox ID="tbCertification" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" Display="Dynamic" ErrorMessage="*Required" ForeColor="Red" SetFocusOnError="True" ControlToValidate="tbCertification"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Winding Crown</td>
            <td>
                <asp:TextBox ID="tbWindingCrown" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" Display="Dynamic" ErrorMessage="*Required" ForeColor="Red" SetFocusOnError="True" ControlToValidate="tbWindingCrown"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <!-- Gallery -->
        <tr>
            <td colspan="2" style="text-align: center; font-weight: bold;">Gallery</td>
        </tr>
        <tr>
            <td>Product</td>
            <td>
                <asp:FileUpload ID="fileImgProduct" runat="server" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="fileImgProduct" Display="Dynamic" ErrorMessage="*Required" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Slideshow 1</td>
            <td>
                <asp:FileUpload ID="fileImgSlideshow1" runat="server" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="fileImgSlideshow1" Display="Dynamic" ErrorMessage="*Required" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Slideshow 2</td>
            <td>
                <asp:FileUpload ID="fileImgSlideshow2" runat="server" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="fileImgSlideshow2" ErrorMessage="*Required" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Slideshow 3</td>
            <td>
                <asp:FileUpload ID="fileImgSlideshow3" runat="server" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="fileImgSlideshow3" Display="Dynamic" ErrorMessage="*Required" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Slideshow 4</td>
            <td>
                <asp:FileUpload ID="fileImgSlideshow4" runat="server" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="fileImgSlideshow4" Display="Dynamic" ErrorMessage="*Required" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="button-area">
                <asp:Button ID="confirmBtn" runat="server" Text="Confirm" CssClass="button" OnClick="confirmBtn_Click" />
                <asp:Button ID="cancelBtn" runat="server" Text="Cancel" CssClass="button" PostBackUrl="~/back_end/record/product/ProductRecord.aspx" CausesValidation="False" />
            </td>
        </tr>
    </table>
    <div style="margin-bottom: 50px;"></div>
</asp:Content>
