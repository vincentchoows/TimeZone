<%@ Page Title="" Language="C#" MasterPageFile="~/front-end/frontend.Master" AutoEventWireup="true" CodeBehind="checkout.aspx.cs" Inherits="TimeZone_Assign.front_end.checkout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Banner -->
    <div class="cart-banner top-banner">
        <div class="txt">PAYMENT</div>
    </div>
    <div class="checkout container col-8">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="row">
            <div class="col-8">
                <div class="row">
                    <div class="col-12">
                        <label class="form-label">Name</label>
                        <asp:TextBox CssClass="form-control" ID="txtName" runat="server"></asp:TextBox>
                    </div>
                    
                    <div class="col-6">
                        <label class="form-label">Phone Number</label>
                        <asp:TextBox CssClass="form-control" ID="txtContact" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-6">
                        <label class="form-label">Email</label>
                        <asp:TextBox CssClass="form-control" ID="txtEmail" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-12">
                        <label class="form-label">Address 1 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAdd1" ErrorMessage="*Required" ForeColor="Red"></asp:RequiredFieldValidator>
                        </label>
                        &nbsp;<asp:TextBox CssClass="form-control" ID="txtAdd1" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-12">
                        <label class="form-label">Address 2 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAdd2" ErrorMessage="*Required" ForeColor="Red"></asp:RequiredFieldValidator>
                        </label>
                        &nbsp;<asp:TextBox CssClass="form-control" ID="txtAdd2" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-4">
                        <label class="form-label">City <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCity" ErrorMessage="*Required" ForeColor="Red"></asp:RequiredFieldValidator>
                        </label>
                        &nbsp;<asp:TextBox CssClass="form-control" ID="txtCity" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-4">
                        <label class="form-label">State <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtState" ErrorMessage="*Required" ForeColor="Red"></asp:RequiredFieldValidator>
                        </label>
                        &nbsp;<asp:TextBox CssClass="form-control" ID="txtState" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-4">
                        <label class="form-label">Postcode <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtPostcode" ErrorMessage="*Required" ForeColor="Red"></asp:RequiredFieldValidator>&nbsp;</label>&nbsp;<asp:TextBox CssClass="form-control" ID="txtPostcode" runat="server"></asp:TextBox>
                        <label class="form-label">
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPostcode" ErrorMessage="Only Allow 5 digits" ForeColor="Red" ValidationExpression="^\d{5}$"></asp:RegularExpressionValidator>
                        </label>
                    </div>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="row-payment">
                        <hr />
                    
                        <h4>Payment</h4>
                        <div class="form-check" style="padding-left: 0px; padding-top: 0px;">
                            <asp:RadioButtonList CssClass="paymentMethod" ID="paymentMethod" runat="server" Width="150px"  AutoPostBack="true" OnSelectedIndexChanged="paymentMethod_SelectedIndexChanged">
                                <asp:ListItem Selected="True" Value="Card">Credit Card</asp:ListItem>
                                <asp:ListItem id="paypalRadioButton" Value="PayPal">Paypal</asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                        </div>
                    </div>
                    <div class="cardDetailsFields row" runat="server" id="cardDetailsDiv" Visible="true">
                        <div class="col-6">
                            <label class="form-label">Name on Card</label>
                            <asp:TextBox CssClass="form-control" ID="txtCardName" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-6">
                            <label class="form-label">Card Number</label>
                            <asp:TextBox CssClass="form-control" ID="txtCardNumber" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-3">
                            <label class="form-label">Good Thru</label>
                            <asp:TextBox CssClass="form-control" ID="txtExpiration" runat="server"></asp:TextBox>
                        </div>
                        <div class="col-3">
                            <label class="form-label">CVV</label>
                            <asp:TextBox CssClass="form-control" ID="txtCvv" runat="server"></asp:TextBox>

                        </div>
                    </div>
                </ContentTemplate>
                </asp:UpdatePanel>
                <div class="row-payment">
                    <hr />
                    <div id="paypal-payment-button" >

                    </div>
                    <asp:Button CssClass="button" type="button" runat="server" Text="Place Order (Card)" OnClick="payment_Click" />


                    <!--paypal method-->
                    <script src="https://www.paypal.com/sdk/js?client-id=AZUeEbGunNtxr9EFRxDzLSmH98bqCgtLwsCfaWI_XS0YAe3pdaL0jRBF_xf10UPwzClv5gol22SrUNmG&disable-funding=credit,card"></script>
                    <asp:HiddenField ID="totalHiddenField" runat="server" Value="" />
                    <asp:HiddenField ID="paymentHiddenField1" runat="server" />
                    <script >
                        paypal.Buttons(
                            {

                                createOrder: function (data, actions) {
                                    return actions.order.create({
                                        purchase_units: [{
                                            amount: {

                                                value: <%= this.totalHiddenField.Value %>
                                            }
                                        }]
                                    });
                                },
                                onApprove: function(data, actions) {
                                    return actions.order.capture().then(function (details) {
                                        console.log(details)
                                        window.location.replace("https://localhost:44397/front-end/paypalUpdate.aspx")
                                    })
                                },
                                onCancel: function (data) {
                                    
                                    window.location.replace("https://localhost:44397/front-end/home.aspx")
                                }
                            }).render('#paypal-payment-button');
                    </script>
                    <!--Pay pal ending-->
                </div>
            </div>
            <div class="col-4">
                <h4 class="">
                    <span class="text-muted">Yout Cart</span>
                </h4>
                <ul class="list-group">
                    <asp:Repeater ID="cartItemsRepeater" runat="server">
                        <ItemTemplate>
                            <li class="list-group-item d-flex justify-content-between">
                                <div>
                                    <h6><%# Eval("productName") %></h6>

                                </div>
                                <span class="text-muted">X<%# Eval("qty") %></span>
                                <span class="text-muted"><%# Eval("price", "{0:#,##0.00}") %></span>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                    <li class="list-group-item d-flex justify-content-between" style="border: none;">
                        <div>
                            <h6>Subtotal</h6>

                        </div>
                        <span class="text-muted">
                            <asp:Label ID="lblSubtotal" runat="server" Text=""></asp:Label></span>
                    </li>
                    <li class="list-group-item d-flex justify-content-between" style="border: none;">
                        <div>
                            <h6>Tax(10%)</h6>

                        </div>
                        <span class="text-muted"><asp:Label ID="lblTax" runat="server" Text=""></asp:Label></span>
                    </li>
                    <li class="list-group-item d-flex justify-content-between" style="border: none;">
                        <div>
                            <h6>Total</h6>

                        </div>
                        <span class="text-muted"><asp:Label ID="lblTotal" runat="server" Text=""></asp:Label></span>
                    </li>
                    
                </ul>
            </div>
        </div>
    </div>
</asp:Content>
