<%@ Page Title="" Language="C#" MasterPageFile="~/back_end/BackEnd.Master" AutoEventWireup="true" CodeBehind="EditProduct.aspx.cs" Inherits="TimeZone_Assign.back_end.record.product.EditProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 class="record-title">Edit Product</h2>
    <!-- Content Area -->
    <table class="content-area">
        <!-- Information -->
        <tr>
            <td colspan="2" style="text-align: center; font-weight: bold; height: 43px;">Information</td>
        </tr>
        <tr>
            <td>Watch ID </td>
            <td>
                <asp:TextBox ID="tbWatchID" runat="server" Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Reference No. </td>
            <td>
                <asp:TextBox ID="tbReferenceNo" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*Required" ControlToValidate="tbReferenceNo" Display="Dynamic" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="tbReferenceNo" Display="Dynamic" ErrorMessage="Please enter a valid reference no. (only letters and numbers)." ForeColor="Red" ValidationExpression="^[a-zA-Z0-9]+$" SetFocusOnError="True"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td>Price (RM) </td>
            <td>
                <asp:TextBox ID="tbPrice" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*Required" ControlToValidate="tbPrice" Display="Dynamic" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="tbPrice" Display="Dynamic" ErrorMessage="Please enter a valid price using only numbers and a maximum of 2 decimal places." ForeColor="Red" ValidationExpression="^\d+(\.\d{2})?$" SetFocusOnError="True"></asp:RegularExpressionValidator>
                <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="tbPrice" Display="Dynamic" ErrorMessage="Please enter a valid price between 0.00 and 10000000.00." ForeColor="Red" Type="Double" MaximumValue="10000000.00" MinimumValue="0.00" SetFocusOnError="True"></asp:RangeValidator>
            </td>
        </tr>
        <tr>
            <td style="height: 75px">Qty Left </td>
            <td style="height: 75px">
                <asp:TextBox ID="tbQtyLeft" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*Required" ControlToValidate="tbQtyLeft" Display="Dynamic" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
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
                </asp:DropDownList>
            </td>
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
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="tbModelCase" Display="Dynamic" ErrorMessage="*Required" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Bezel</td>
            <td>
                <asp:TextBox ID="tbBezel" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="tbBezel" Display="Dynamic" ErrorMessage="*Required" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Water Resistance</td>
            <td>
                <asp:TextBox ID="tbWaterResistance" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="tbWaterResistance" Display="Dynamic" ErrorMessage="*Required" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Movement</td>
            <td>
                <asp:TextBox ID="tbMovement" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="tbMovement" Display="Dynamic" ErrorMessage="*Required" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Calibre</td>
            <td>
                <asp:TextBox ID="tbCalibre" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="tbCalibre" Display="Dynamic" ErrorMessage="*Required" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Power Reserve</td>
            <td>
                <asp:TextBox ID="tbPowerReserve" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="tbPowerReserve" Display="Dynamic" ErrorMessage="*Required" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Bracelet</td>
            <td>
                <asp:TextBox ID="tbBracelet" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="tbBracelet" Display="Dynamic" ErrorMessage="*Required" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Dial</td>
            <td>
                <asp:TextBox ID="tbDial" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="tbDial" Display="Dynamic" ErrorMessage="*Required" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Certification</td>
            <td>
                <asp:TextBox ID="tbCertification" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="tbCertification" Display="Dynamic" ErrorMessage="*Required" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>Winding Crown</td>
            <td>
                <asp:TextBox ID="tbWindingCrown" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="tbWindingCrown" Display="Dynamic" ErrorMessage="*Required" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <!-- Gallery -->
        <tr>
            <td colspan="2" style="text-align: center; font-weight: bold;">Gallery</td>
        </tr>
        <tr>
            <td>Product</td>
            <td id="product">
                <asp:Image ID="imgProd" runat="server" CssClass="img-resize" />
                <asp:FileUpload ID="fileUploadProd" runat="server" CssClass="edit-img-btn" />
            </td>
        </tr>
        <tr>
            <td>Slideshow 1</td>
            <td id="ss1">
                <asp:Image ID="imgSs1" runat="server" CssClass="img-resize" />
                <asp:FileUpload ID="fileUploadSs1" runat="server" CssClass="edit-img-btn" />
            </td>
        </tr>
        <tr>
            <td>Slideshow 2</td>
            <td id="ss2">
                <asp:Image ID="imgSs2" runat="server" CssClass="img-resize" />
                <asp:FileUpload ID="fileUploadSs2" runat="server" CssClass="edit-img-btn" />
            </td>
        </tr>
        <tr>
            <td>Slideshow 3</td>
            <td id="ss3">
                <asp:Image ID="imgSs3" runat="server" CssClass="img-resize" />
                <asp:FileUpload ID="fileUploadSs3" runat="server" CssClass="edit-img-btn" />
            </td>
        </tr>
        <tr>
            <td>Slideshow 4</td>
            <td id="ss4">
                <asp:Image ID="imgSs4" runat="server" CssClass="img-resize" />
                <asp:FileUpload ID="fileUploadSs4" runat="server" CssClass="edit-img-btn" />
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
