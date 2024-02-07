<%@ Page Title="" Language="C#" MasterPageFile="~/front-end/frontend.Master" AutoEventWireup="true" CodeBehind="checkout-invoice.aspx.cs" Inherits="TimeZone_Assign.front_end.checkout_invoice" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--print purpose-->
    <style>
        @media print {
            body * {
                visibility: hidden;
            }

            .print-container, .print-container * {
                visibility: visible;
            }

            .print-container {
                position: absolute;
                top: 0;
                left: 0;
                width: 210mm; /* A4 width */
                height: 297mm; /* A4 height */
                margin: 0;
            }

            .noprint {
                display: none;
            }
        }
    </style>
    <!-- Banner -->
    <div class="cart-banner top-banner">
        <div class="txt">INVOICE</div>
    </div>
   
    <div class="container checkout-invoice print-container">
    <div class="row">
        <div class="col-md-12">
    		<div class="invoice-title">
    			<h2>Invoice</h2><h3 class="pull-right">Order # <asp:Label ID="lblInvoice" runat="server" Text=""></asp:Label></h3>
    		</div>
    		<hr>
    		<div class="row">
    			<div class="col-md-6">
    				<address>
    				<strong>Deliver To:</strong><br>
    					<asp:Label ID="lblName" runat="server" Text=""></asp:Label><br>
    					<asp:Label ID="lblAdd1" runat="server" Text=""></asp:Label><br>
    					<asp:Label ID="lblAdd2" runat="server" Text=""></asp:Label><br>
    					<asp:Label ID="lblPostCode" runat="server" Text=""></asp:Label>, <asp:Label ID="lblCity" runat="server" Text=""></asp:Label><br>
                        <asp:Label ID="lblState" runat="server" Text=""></asp:Label>
    				</address>
    			</div>
    		</div>
    		<div class="row">
    			<div class="col-md-6">
    				<address>
    					<strong>Payment Method:</strong><br>
    					<asp:Label ID="lblPayMethod" runat="server" Text=""></asp:Label><br>
    					<asp:Label ID="lblEmail" runat="server" Text=""></asp:Label>
    				</address>
    			</div>
    			<div class="col-md-6 text-end">
    				<address>
    					<strong>Order Date:</strong><br>
    					<asp:Label ID="lblPaymentDate" runat="server" Text=""></asp:Label><br><br>
    				</address>
    			</div>
    		</div>
    	</div>
    </div>
    
    <div class="row">
    	<div class="col-md-12">
    		<div class="panel panel-default invoice-panel">
    			<div class="panel-heading invoice-heading">
    				<h3 class="panel-title invoice-title"><strong>Order summary</strong></h3>
    			</div>
    			<div class="panel-body invoice-body">
    				<div class="table-responsive">
    					<table class="table table-condensed">
    						<thead>
                                <tr>
        							<td><strong>Item</strong></td>
        							<td class="text-center"><strong>Price(RM)</strong></td>
        							<td class="text-center"><strong>Quantity</strong></td>
        							<td class="text-end"><strong>Totals(RM)</strong></td>
                                </tr>
    						</thead>
    						<tbody>
    							<asp:Repeater ID="cartItemsRepeater" runat="server">
                                    <ItemTemplate>
    							        <tr>
    								        <td><%# Eval("productName") %></td>
    								        <td class="text-center"><%# Eval("price", "{0:#,##0.00}") %></td>
    								        <td class="text-center"><%# Eval("qty") %></td>
    								        <td class="text-end"><%# Eval("subtotal", "{0:#,##0.00}") %></td>
    							        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <tr>
    								<td class="thick-line"></td>
    								<td class="thick-line"></td>
    								<td class="thick-line text-center"><strong>Subtotal</strong></td>
    								<td class="thick-line text-end">
                                    <asp:Label ID="lblSubtotal" runat="server" Text=""></asp:Label></td>
    							</tr>
    							<tr>
    								<td class="no-line"></td>
    								<td class="no-line"></td>
    								<td class="no-line text-center"><strong>Tax(10%)</strong></td>
    								<td class="no-line text-end"><asp:Label ID="lblTax" runat="server" Text=""></asp:Label></td>
    							</tr>
    							<tr>
    								<td class="no-line"></td>
    								<td class="no-line"></td>
    								<td class="no-line text-center"><strong>Total</strong></td>
    								<td class="no-line text-end"><asp:Label ID="lblTotal" runat="server" Text=""></asp:Label></td>
    							</tr>
    						</tbody>
    					</table>
    				</div>
    			</div>
    		</div>
    	</div>
    </div>
    <div class="row noprint">
        <div class="col-md-12 text-center">
            <asp:Button ID="btnPrint" runat="server" CssClass="button" Text="Print" OnClientClick="window.print();" />
            <asp:Button ID="btnOrder" runat="server" CssClass="button" Text="View Order" OnClick="btnOrder_Click" />
        </div>
    </div>
</div>
</asp:Content>
