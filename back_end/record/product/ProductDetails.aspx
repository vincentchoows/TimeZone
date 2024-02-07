<%@ Page Title="" Language="C#" MasterPageFile="~/back_end/BackEnd.Master" AutoEventWireup="true" CodeBehind="ProductDetails.aspx.cs" Inherits="TimeZone_Assign.back_end.record.product.ProductDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 class="record-title">Product Details</h2>
    <!-- Content Area -->
    <table class="content-area">
        <!-- Information -->
        <tr>
            <td colspan="2" style="text-align: center; font-weight: bold;">Information</td>
        </tr>
        <tr>
            <td>Watch ID </td>
            <td>
                <asp:Label ID="lblWatchID" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td>Reference No. </td>
            <td>
                <asp:Label ID="lblReferenceNo" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td>Price </td>
            <td>
                <asp:Label ID="lblPrice" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td>Qty Left </td>
            <td>
                <asp:Label ID="lblQtyLeft" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td>Status </td>
            <td>
                <asp:Label ID="lblStatus" runat="server"></asp:Label></td>
        </tr>
        <!-- Category -->
        <tr>
            <td colspan="2" style="text-align: center; font-weight: bold;">Category</td>
        </tr>
        <tr>
            <td>Name </td>
            <td>
                <asp:Label ID="lblCategoryName" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td>Description </td>
            <td style="max-width: 800px;">
                <asp:Label ID="lblDesc" runat="server"></asp:Label></td>
        </tr>
        <!-- Specifications -->
        <tr>
            <td colspan="2" style="text-align: center; font-weight: bold;">Specifications</td>
        </tr>
        <tr>
            <td>Model Case</td>
            <td>
                <asp:Label ID="lblModelCase" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td>Bezel</td>
            <td>
                <asp:Label ID="lblBezel" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td>Water Resistance</td>
            <td>
                <asp:Label ID="lblWaterResistance" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td>Movement</td>
            <td>
                <asp:Label ID="lblMovement" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td>Calibre</td>
            <td>
                <asp:Label ID="lblCalibre" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td>Power Reserve</td>
            <td>
                <asp:Label ID="lblPowerReserve" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td>Bracelet</td>
            <td>
                <asp:Label ID="lblBracelet" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td>Dial</td>
            <td>
                <asp:Label ID="lblDial" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td>Certification</td>
            <td>
                <asp:Label ID="lblCertification" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td>Winding Crown</td>
            <td>
                <asp:Label ID="lblWindingCrown" runat="server"></asp:Label></td>
        </tr>
        <!-- Gallery -->
        <tr>
            <td colspan="2" style="text-align: center; font-weight: bold;">Gallery</td>
        </tr>
        <tr>
            <td>Wallpaper</td>
            <td>
                <asp:Image ID="imgWallpaper" runat="server" CssClass="img-resize" /></td>
        </tr>
        <tr>
            <td>Banner</td>
            <td>
                <asp:Image ID="imgBanner" runat="server" CssClass="img-resize" /></td>
        </tr>
        <tr>
            <td>Product</td>
            <td>
                <asp:Image ID="imgProduct" runat="server" CssClass="img-resize" /></td>
        </tr>
        <tr>
            <td>Slideshow 1</td>
            <td>
                <asp:Image ID="imgSlideshow1" runat="server" CssClass="img-resize" /></td>
        </tr>
        <tr>
            <td>Slideshow 2</td>
            <td>
                <asp:Image ID="imgSlideshow2" runat="server" CssClass="img-resize" /></td>
        </tr>
        <tr>
            <td>Slideshow 3</td>
            <td>
                <asp:Image ID="imgSlideshow3" runat="server" CssClass="img-resize" /></td>
        </tr>
        <tr>
            <td>Slideshow 4</td>
            <td>
                <asp:Image ID="imgSlideshow4" runat="server" CssClass="img-resize" /></td>
        </tr>
        <tr>
            <td colspan="2" class="button-area">
                <asp:Button ID="backBtn" runat="server" Text="Back" PostBackUrl="~/back_end/record/product/ProductRecord.aspx" CssClass="button" /></td>
        </tr>
    </table>
    <div style="margin-bottom: 50px;"></div>
</asp:Content>
