<%@ Page Title="Create Order" Language="C#" MasterPageFile="~/Design/ECPMasterPage.master" AutoEventWireup="true" CodeFile="CreateOrder.aspx.cs" Inherits="Order_CreateOrder" %>

<%@ Register Src="../NotificationMessage.ascx" TagName="NotificationMessage" TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/CurrentCurrency.ascx" TagName="CurrentCurrency" TagPrefix="CurrentCurrency" %>
<%@ Register Src="~/LoadingImage.ascx" TagName="LoadingImage" TagPrefix="LoadingImage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <!-- Core JS files -->
    <script type="text/javascript" src="../Design/assets/js/plugins/loaders/pace.min.js"></script>
    <script type="text/javascript" src="../Design/assets/js/core/libraries/jquery.min.js"></script>
    <script type="text/javascript" src="../Design/assets/js/core/libraries/bootstrap.min.js"></script>
    <script type="text/javascript" src="../Design/assets/js/plugins/loaders/blockui.min.js"></script>
    <!-- /core JS files -->
    <!-- Theme JS files -->
    <script type="text/javascript" src="../Design/assets/js/plugins/forms/wizards/stepy.min.js"></script>
    <script type="text/javascript" src="../Design/assets/js/plugins/forms/selects/select2.min.js"></script>
    <script type="text/javascript" src="../Design/assets/js/plugins/forms/styling/uniform.min.js"></script>
    <script type="text/javascript" src="../Design/assets/js/core/libraries/jasny_bootstrap.min.js"></script>
    <script type="text/javascript" src="../Design/assets/js/plugins/forms/validation/validate.min.js"></script>

    <script type="text/javascript" src="../Design/assets/js/core/app.js"></script>
    <script type="text/javascript" src="../Design/assets/js/pages/wizard_stepy.js"></script>
    <!-- Theme JS files -->
    <script type="text/javascript" src="../Design/assets/js/core/libraries/jquery_ui/datepicker.min.js"></script>
    <script type="text/javascript" src="../Design/assets/js/core/libraries/jquery_ui/effects.min.js"></script>
    <script type="text/javascript" src="../Design/assets/js/plugins/notifications/jgrowl.min.js"></script>
    <script type="text/javascript" src="../Design/assets/js/plugins/ui/moment/moment.min.js"></script>
    <script type="text/javascript" src="../Design/assets/js/plugins/pickers/daterangepicker.js"></script>
    <script type="text/javascript" src="../Design/assets/js/plugins/pickers/anytime.min.js"></script>
    <script type="text/javascript" src="../Design/assets/js/plugins/pickers/pickadate/picker.js"></script>
    <script type="text/javascript" src="../Design/assets/js/plugins/pickers/pickadate/picker.date.js"></script>
    <script type="text/javascript" src="../Design/assets/js/plugins/pickers/pickadate/picker.time.js"></script>
    <script type="text/javascript" src="../Design/assets/js/plugins/pickers/pickadate/legacy.js"></script>
    <script type="text/javascript" src="../Design/assets/js/pages/picker_date.js"></script>
    <!-- /theme JS files -->
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <form id="Form1" class="form-horizontal" runat="server">
        <ajaxToolkit:ToolkitScriptManager runat="server" ID="ScriptManager1" />
        <LoadingImage:LoadingImage ID="LoadingImage1" runat="server" />
        <asp:UpdatePanel ID="up1" runat="server">
            <ContentTemplate>


                <asp:Label ID="lblQuatationType" runat="server" Visible="false"></asp:Label>

                <!-- Main content -->

                <!-- Page header -->
                <div class="page-header">
                    <div class="page-header-content">
                        <div class="page-title">
                            <h4><i class="icon-cart5 position-left"></i><span class="text-semibold">Create </span>Order</h4>
                        </div>


                    </div>

                </div>
                <!-- /page header -->


                <!-- Content area -->
                <div class="content">
                    <uc3:NotificationMessage ID="NotificationMessage1" runat="server" />
                    <!-- Basic setup -->
                    <div class="panel panel-white">

                        <div class="content">
                            <div class="row">
                                <div class="col-md-12">

                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-3">

                                                <div class="thumbnail">

                                                    <div class="thumb thumb-rounded thumb-slide">


                                                        <img id="imgCustomerlogo" runat="server" alt="">

                                                        <div class="caption">
                                                            <span>
                                                                <a id="infoImage" runat="server" class="btn bg-success-400 btn-icon btn-xs"><i class="icon-pencil7"></i></a>
                                                            </span>
                                                        </div>
                                                    </div>

                                                    <div class="caption text-center">
                                                        <h6 class="text-semibold no-margin">
                                                            <asp:Label ID="lblFirstName" runat="server"></asp:Label>
                                                            <asp:Label ID="lblMiddleName" runat="server"></asp:Label>

                                                            <asp:Label ID="lblLastName" runat="server"></asp:Label>
                                                            <small class="display-block">
                                                                <asp:Literal ID="lblCustomer" runat="server"></asp:Literal></small></h6>
                                                        <asp:Literal ID="lblEmailId" runat="server"></asp:Literal>
                                                        <asp:Literal ID="lblCantactNo" runat="server"></asp:Literal>

                                                    </div>


                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="panel panel-flat">
                                                    <div class="panel-heading">
                                                        <h6 class="panel-title">User Information</h6>
                                                        <div class="heading-elements">
                                                            <a id="linkchange" runat="server" class="heading-text">Change →</a>
                                                        </div>
                                                        <a class="heading-elements-toggle"><i class="icon-menu"></i></a><a class="heading-elements-toggle"><i class="icon-menu"></i></a>
                                                    </div>

                                                    <div class="list-group list-group-borderless no-padding-top">
                                                        <a href="#" class="list-group-item"><i class="icon-user"></i>Consult person<span class="pull-right"><asp:Label ID="lblcunsultperson" runat="server"></asp:Label></span></a>
                                                        <a href="#" class="list-group-item"><i class="icon-cash3"></i>Total Business</a>
                                                        <a href="#" class="list-group-item"><i class="icon-tree7"></i>Total Oders <span class="badge bg-danger pull-right">
                                                        <asp:Label ID="lblTotalOrder" runat="server"></asp:Label></span></a>
                                                        <a href="#" class="list-group-item"><i class="icon-users"></i>Client owner<span class="pull-right"><asp:Label ID="lblLeadOwnerNAme" runat="server"></asp:Label></span></a>
                                                        <div class="list-group-divider"></div>
                                                        <a href="#" class="list-group-item"><i class="icon-calendar3"></i>Delivery Pending<span class="badge bg-teal-400 pull-right"><asp:Label ID="lblDeliveryPending" runat="server"></asp:Label></span></a>
                                                    </div>
                                                </div>

                                            </div>

                                            <div class="col-md-3">
                                                <div class="panel panel-flat">
                                                    <div class="panel-heading">
                                                        <h6 class="panel-title">Account Information</h6>
                                                        <div class="heading-elements">
                                                        </div>
                                                        <a class="heading-elements-toggle"><i class="icon-menu"></i></a><a class="heading-elements-toggle"><i class="icon-menu"></i></a>
                                                    </div>

                                                    <div class="list-group list-group-borderless no-padding-top">
                                                        <a href="#" class="list-group-item"><i class="icon-paypal2	"></i>Paid Amount</a>
                                                        <a href="#" class="list-group-item"><i class=" icon-point-right"></i>Unpaid Amount</a>
                                                        <a href="#" class="list-group-item"><i class=" icon-bell3"></i>Over Due Oders <%--<span class="badge bg-danger pull-right">29</span>--%></a>
                                                        <a href="#" class="list-group-item"><i class=" icon-cash3"></i>Credit Notes (Wallet)</a>
                                                        <div class="list-group-divider"></div>
                                                        <a href="#" class="list-group-item"><i class="icon-calendar3"></i>Old Payments<%-- <span class="badge bg-teal-400 pull-right">48</span>--%></a>

                                                    </div>
                                                </div>

                                            </div>

                                            <div class="col-md-3">
                                                <div class="panel panel-flat">
                                                    <div class="panel-heading">
                                                        <h6 class="panel-title">Complains &amp; Services</h6>
                                                        <div class="heading-elements">
                                                        </div>
                                                        <a class="heading-elements-toggle"><i class="icon-menu"></i></a><a class="heading-elements-toggle"><i class="icon-menu"></i></a>
                                                    </div>

                                                    <div class="list-group list-group-borderless no-padding-top">
                                                        <a href="#" class="list-group-item"><i class="icon-star-half"></i>Total Reviews</a>
                                                        <a href="#" class="list-group-item"><i class=" icon-hammer-wrench"></i>Service Requests</a>
                                                        <a href="#" class="list-group-item"><i class="icon-thumbs-up3"></i>Solved Issues <%--<span class="badge bg-teal-400 pull-right">29</span>--%></a>
                                                        <a href="#" class="list-group-item"><i class=" icon-thumbs-down3"></i>Unresolved Issues<%--<span class="badge bg-danger pull-right">29</span>--%></a>
                                                        <div class="list-group-divider"></div>
                                                        <a href="#" class="list-group-item"><i class="icon-calendar3"></i>Under Delivery<%-- <span class="badge bg-teal-400 pull-right">48</span>--%></a>

                                                    </div>
                                                </div>

                                            </div>

                                        </div>
                                    </div>

                                </div>

                            </div>


                            <div class="">
                                <div class="row">
                                    <div class="col-md-12">

                                        <div class="row">
                                            <div class="col-md-12">
                                                <asp:Label ID="txtQuetationTitle" runat="server" Visible="false"></asp:Label>
                                                <asp:Label ID="txtQuatationType" runat="server" Visible="false"></asp:Label>
                                                <div class="col-md-2">
                                                    <asp:DropDownList ID="ddlProduct" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged"></asp:DropDownList>

                                                </div>
                                                <div class="col-md-2" id="divddlAllocateTwo" runat="server">

                                                    <asp:DropDownList ID="ddlAllocateTo" runat="server" class="form-control" AutoPostBack="true"></asp:DropDownList>

                                                </div>
                                                <div class="col-md-1">
                                                    <asp:TextBox ID="txtQuantity" runat="server" class="form-control" placeholder="QTY"></asp:TextBox>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:DropDownList ID="ddlQuantityType" runat="server" class="form-control"></asp:DropDownList>

                                                </div>
                                                <div class="col-md-1">
                                                    <asp:TextBox ID="txtDisCount" runat="server" class="form-control" placeholder="Discount"></asp:TextBox>
                                                </div>
                                                <div class="col-md-1">
                                                    <b style="font-size: large">%</b>
                                                </div>
                                                <div class="col-md-1" style="margin-left: -64px;">
                                                    <asp:TextBox ID="txtPrice" runat="server" class="form-control" placeholder="Price"></asp:TextBox>
                                                    <asp:TextBox ID="txtUniqueProductId" Visible="false" runat="server" class="form-control" placeholder="Unique product code" AutoPostBack="true" OnTextChanged="txtUniqueProductId_TextChanged"></asp:TextBox>
                                                </div>

                                                 <div class="col-md-1" style="display:none;" id="divshipping" runat="server">

                                                    <asp:DropDownList ID="ddlShippingTo" runat="server" class="form-control" AutoPostBack="true"></asp:DropDownList>

                                                </div>
                                                <div class="col-md-1">
                                                    <asp:LinkButton ID="lnkAdd" runat="server" CssClass="button-next btn btn-primary" OnClick="lnkAdd_Click">
                                                        <asp:Label ID="lblName" Text="Add Items" runat="server"></asp:Label><i class="icon-arrow-right14 position-right"></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                            </div>

                            <br />
                            <div class="row">
                                <div class="col-md-12">
                                    <%--<div class="row">
                                  <div class="col-md-12">--%>
                                    <div class="table-responsive">

                                        <asp:GridView ID="grvTempBill" runat="server" AutoGenerateColumns="False" OnRowDataBound="grvTempBill_RowDataBound"
                                            CssClass="datatable table table-striped table-bordered table-hover style2" OnRowDeleting="grvTempBill_RowDeleting" OnPreRender="grvTempBill_PreRender" OnRowEditing="grvTempBill_RowEditing">
                                            <Columns>
                                                <asp:TemplateField HeaderText="#" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField Visible="false" DataField="nOrderDetailId" HeaderText="#" ItemStyle-Width="5%" ReadOnly="true" />
                                                <asp:TemplateField Visible="false" HeaderStyle-Font-Bold="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProductImage" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cImage") %>'></asp:Label>
                                                        <%-- <asp:Image ID="imgProduct" runat="server" ImageUrl='<%# DataBinder.Eval(Container, "DataItem.cImage") %>'  />--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Item/Description" HeaderStyle-Width="20%" HeaderStyle-Font-Bold="true">
                                                    <ItemTemplate>


                                                        <div style="float: left;">
                                                            <%--<h4 style="margin-top:5px;">--%>



                                                            <b>



                                                                <asp:Literal ID="ltrProductImage" runat="server"></asp:Literal>
                                                            </b>

                                                        </div>
                                                        <div style="float: left; margin-left: 9px;">
                                                            <%--</h4>--%>
                                                            <b>
                                                                <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cName") %>'></asp:Label></b>
                                                            <p>
                                                                <i class="fa fa-file-text-o"></i>
                                                                <asp:Label ID="lblProductDescription" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cDescription") %>'></asp:Label>
                                                                <br />
                                                                Warrenty - 
                                             <asp:Label ID="nWarrentyMonths" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.nWarrantyMonth") %>'></asp:Label>
                                                                <asp:Label ID="cWarrentytype" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cWarrantyType") %>'></asp:Label>

                                                            </p>
                                                        </div>





                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="QTY" HeaderStyle-Width="5%" HeaderStyle-Font-Bold="true">
                                                    <ItemTemplate>

                                                        <asp:Label ID="fQuantity" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.fQuantity") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Type" Visible="false" HeaderStyle-Font-Bold="true">
                                                    <ItemTemplate>

                                                        <asp:Label ID="cQuantityType" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cQuantityType") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Unit Price" HeaderStyle-Font-Bold="true" HeaderStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <CurrentCurrency:CurrentCurrency ID="CurrentCurrency4" runat="server" />
                                                        <asp:Label ID="fProductMRP" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.fMRP") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Discount " HeaderStyle-Font-Bold="true" HeaderStyle-Width="10%">

                                                    <ItemTemplate>
                                                        <asp:Label ID="dglblDiscount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.fDiscount") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Tax" Visible="false" HeaderStyle-Font-Bold="true">
                                                    <ItemTemplate>

                                                        <asp:Label ID="cTaxName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cTaxName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total Price" HeaderStyle-Width="10%" HeaderStyle-Font-Bold="true">
                                                    <ItemTemplate>
                                                        <CurrentCurrency:CurrentCurrency ID="Currency1" runat="server" />
                                                        <asp:Label ID="fTotal" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.fTotal") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="cProductName" Visible="false" HeaderText="Products" />
                                                <asp:BoundField DataField="cProductDescription" Visible="false" HeaderText="Description" />
                                                <asp:BoundField DataField="cWarrentyType" Visible="false" HeaderText="Warranty Type" />
                                                <asp:BoundField DataField="cTaxName" Visible="false" HeaderText="Tax" />
                                                <asp:BoundField DataField="nWorentyMonth" Visible="false" HeaderText="Warranty Months" />
                                                <asp:BoundField DataField="fProductMRP" Visible="false" HeaderText="Price" />
                                                <asp:BoundField DataField="fQuantity" Visible="false" HeaderText="Quantity" />
                                                <asp:TemplateField HeaderText="ProductId " Visible="false">

                                                    <ItemTemplate>
                                                        <asp:Label ID="dgtProductId" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.nProductId") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ProductId " Visible="false">

                                                    <ItemTemplate>
                                                        <asp:Label ID="dgtnOrderDetailId" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.nOrderDetailId") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="fTotal" Visible="false" HeaderText="Total " />
                                                <asp:BoundField DataField="nWarrentyDuration" HeaderText="Periodic Service" Visible="False" />
                                                <asp:TemplateField HeaderStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ImageUrl="~/Design/Icon/user_edit.png" ID="ImageButton1" runat="server" CausesValidation="false"
                                                            CommandName="Edit" Text="Edit"></asp:ImageButton>
                                                        <asp:ImageButton ImageUrl="~/Design/Icon/trash.png" ID="dglbnDelete" runat="server" CausesValidation="false"
                                                            CommandName="Delete" Text="Delete"></asp:ImageButton>

                                                        <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtondglbnDelete" runat="server" TargetControlID="dglbnDelete"
                                                            DisplayModalPopupID="ModalPopupdglbnDelete" />
                                                        <ajaxToolkit:ModalPopupExtender ID="ModalPopupdglbnDelete" runat="server" TargetControlID="dglbnDelete"
                                                            PopupControlID="dgPNL" OkControlID="dgButtonOk" />
                                                        <asp:Panel ID="dgPNL" runat="server" Style="display: none; width: 300px; background-color: White; border-width: 2px; border-color: Black; border-style: solid; padding: 20px;">
                                                            Are you sure you want to Delete the Item?
                                                        <br />
                                                            <br />
                                                            <div style="text-align: right;">
                                                                <asp:Button ID="dgButtonOk" runat="server" Text="OK" CssClass="btn btn-primary" />
                                                                <asp:Button ID="ButtonCancel" runat="server" Text="Cancel" CssClass="btn btn-primary" />
                                                            </div>
                                                        </asp:Panel>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <%-- </div>
                                              </div>--%>
                                </div>
                            </div>
                            <br />
                            <div class="row" runat="server" id="divtotal_div" visible="false">
                                <div class="col-md-8" id="tr1" runat="server">
                                    <div class="row">
                                        <div class="col-md-12">

                                            <%--<div class="row">--%>
                                            <div class="col-md-4">
                                                <label>Payment Due Date</label>
                                                <%-- <div class="form-group">--%>
                                                <div class="input-group">
                                                    <span class="input-group-addon"><i class="icon-calendar3"></i></span>
                                                    <asp:TextBox ID="txtPaymentDueDate" CssClass="form-control" runat="server" placeholder="Payment Due Date"></asp:TextBox>
                                                </div>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtPaymentDueDate" Format="MM/dd/yyyy" />

                                                <%-- </div>--%>
                                            </div>


                                            <div class="col-md-4">
                                                <label>Validity Date</label>
                                                <%--<div class="form-group">--%>
                                                <div class="input-group">
                                                    <span class="input-group-addon"><i class="icon-calendar3"></i></span>
                                                    <asp:TextBox ID="txtValidityDAte" CssClass="form-control" runat="server" placeholder="Validity Date"></asp:TextBox>
                                                </div>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtValidityDAte" Format="MM/dd/yyyy" />
                                                <%-- </div>--%>
                                            </div>

                                            <div class="col-md-4">
                                                <label>Delivary Date</label>
                                                <%--<div class="form-group">--%>
                                                <div class="input-group">
                                                    <span class="input-group-addon"><i class="icon-calendar3"></i></span>
                                                    <asp:TextBox ID="txtDelivaryDate" CssClass="form-control" runat="server" placeholder="Delivary Date"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDelivaryDate" Format="MM/dd/yyyy" />
                                                </div>
                                                <%-- </div>--%>
                                            </div>
                                            <%--</div>--%>
                                        </div>
                                    </div>
                                    <br />



                                    <br />

                                    <div class="row">
                                        <div class="col-md-12">

                                            <div class="col-sm-6">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><i class=" icon-home2"></i></span>
                                                    <asp:TextBox ID="txtAddress" CssClass="form-control" TextMode="MultiLine" runat="server" placeholder="Address"></asp:TextBox>
                                                </div>
                                            </div>



                                            <div class="col-sm-6">
                                                <div class="col-md-10">

                                                    <div class="input-group">
                                                        <span class="input-group-addon"><i class="icon-file-check"></i></span>
                                                        <asp:TextBox ID="txtPaymentTems" CssClass="form-control" runat="server" placeholder="Payment Terms"></asp:TextBox>

                                                    </div>



                                                </div>

                                                <div class="col-md-2">

                                                    <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-primary" OnClick="btnAdd_Click" />

                                                </div>

                                            </div>
                                        </div>
                                    </div>

                                    <br />

                                    <div id="divCupon" runat="server">
                                        <div class="col-sm-4">
                                            <div class="input-group">
                                                <span class="input-group-addon"><i class="icon-barcode2"></i></span>
                                                <asp:TextBox ID="txtCupCode" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtCupCode_TextChanged" placeholder="Enter cupon code" runat="server"></asp:TextBox>
                                            </div>

                                        </div>
                                        <div class="col-sm-2" style="display: none;">

                                            <asp:Button ID="btnOfferApply" runat="server" CssClass="btn btn-primary" Text="Apply" />
                                            <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" TargetControlID="btnOfferApply"
                                                DisplayModalPopupID="ModalPopupExtender1" />
                                            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnOfferApply"
                                                PopupControlID="PNL" CancelControlID="ButtonCancel34" />
                                            <asp:Panel runat="server" ID="PNL" Style="display: none;">
                                                <div class="modal-dialog">
                                                    <div class="modal-content modal-table">
                                                        <div class="modal-header">
                                                            <h4 class="modal-title">Offer Detail</h4>
                                                        </div>
                                                        <div class="modal-body">
                                                            <div class="col-md-12">
                                                                <div class="">

                                                                    <div class="box-body">
                                                                        <!-- BOX -->

                                                                        <br />
                                                                        <div class="form-group">

                                                                            <div class="col-sm-offset-5 col-sm-4">
                                                                                <asp:Label ID="lblofferDetail" class="exampleInputEmail1" runat="server" Font-Bold="true"
                                                                                    Text=""></asp:Label>
                                                                                <asp:Label ID="lblOfferId" class="exampleInputEmail1" runat="server" Visible="false" Font-Bold="False"
                                                                                    Text=""></asp:Label>

                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group">

                                                                            <div class="col-sm-offset-3 col-sm-8">
                                                                                <asp:Label ID="lblCashbackDetail" class="exampleInputEmail1" runat="server" Font-Bold="False"
                                                                                    Text="Your CashBack Is: "></asp:Label>

                                                                            </div>

                                                                            <%-- <div class="col-sm-offset-0 col-sm-3">
                                        <asp:Label ID="lblDiscountDetail" class="exampleInputEmail1" runat="server" Font-Bold="False"
                                            Text="Your CashBack Is: "></asp:Label>
                                    </div>--%>
                                                                            <div class="col-sm-offset-0 col-sm-1">
                                                                                <asp:Label ID="lblTotalCashbackDetail" class="exampleInputEmail1" Visible="false" runat="server" Font-Bold="False"
                                                                                    Text="Your CashBack Is: "></asp:Label>

                                                                            </div>


                                                                        </div>








                                                                        <%--  <div class="form-group">
                                    
                                    <div class="col-sm-offset-4 col-sm-4">

                                       
                                    

                                    </div>

                                </div>
                                                                        --%>
                                                                    </div>

                                                                </div>
                                                            </div>

                                                        </div>
                                                        <br />
                                                        <br />
                                                        <div class="modal-footer">
                                                            <div class="form-group">

                                                                <asp:Button ID="ButtonCancel34" runat="server" Text="Ok" CssClass="btn btn-primary" />

                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>




                                            </asp:Panel>

                                        </div>
                                    </div>

                                    <br />
                                    <br />

                                    <div class="row" id="tr2" runat="server">
                                        <div class="col-md-12">
                                            <div class="col-sm-12 col-lg-offset-0">
                                                <div style="overflow-x: auto;">
                                                    <asp:DataGrid runat="server" AutoGenerateColumns="False" OnItemCommand="dgPaymentTerms_ItemCommand"
                                                        CssClass="datatable table table-striped table-bordered table-hover style2"
                                                        ID="dgPaymentTerms">
                                                        <Columns>
                                                            <asp:TemplateColumn HeaderText="Id" Visible="false" HeaderStyle-Font-Bold="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="dglblOrderPaymentTermsId" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.nPaymentTermsId") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Payment Terms" HeaderStyle-Font-Bold="true">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPaymentTems" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cPaymentTerms") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Quetation Id" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblQuotationId" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.nOrderId") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="User ID" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUserId" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.nUserId") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="IsActive" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIsActive" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.IsActive") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>

                                                            <asp:TemplateColumn HeaderText="Delete" HeaderStyle-Font-Bold="true">
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ImageUrl="~/Design/Icon/trash.png" ID="lnkDel" runat="server" CausesValidation="false"
                                                                        CommandName="Del" Text="Delete"></asp:ImageButton>

                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                        </Columns>
                                                    </asp:DataGrid>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>

                                <div class="col-md-4" id="divTotal" runat="server">

                                    <div class="panel panel-flat">

                                        <div class="list-group list-group-borderless no-padding-top">

                                            <div class="list-group-item" style="font-size: 18px !important;">
                                                <i class="icon-cash3"></i>Total <span class="badge bg-danger pull-right" style="font-size: 15px !important;">
                                                    <CurrentCurrency:CurrentCurrency ID="CurrentCurrency2" runat="server" />
                                                    <asp:Label ID="lblTotal" runat="server" Text="0" Visible="False"></asp:Label></span>
                                            </div>
                                            <div style="display: none;">
                                                <div class="list-group-divider">
                                                </div>
                                                <div class="list-group-item">
                                                    <i class=" icon-percent"></i>Discount <span class="badge bg-grey pull-right">
                                                        <CurrentCurrency:CurrentCurrency ID="CurrentCurrency3" Visible="false" runat="server" />
                                                        <asp:Label ID="lblDiscount" runat="server" Text="0" Visible="False"></asp:Label>
                                                    </span>
                                                </div>
                                                <div class="list-group-item">
                                                    <i class="icon-credit-card"></i>Payable<span class="badge bg-teal-400 pull-right">
                                                        <CurrentCurrency:CurrentCurrency ID="CurrentCurrency1" runat="server" />
                                                        <asp:Label ID="lblPayable" runat="server" Text="0" Visible="False"></asp:Label>
                                                    </span>
                                                </div>
                                                <div class="list-group-item" id="linkCashback" runat="server" visible="false">
                                                    <i class="icon-credit-card"></i>CashBack<span class="badge bg-teal-400 pull-right">
                                                        <CurrentCurrency:CurrentCurrency ID="CurrentCurrency4" runat="server" />
                                                        <asp:Label ID="lblCashbackTotal" runat="server" Text="0" Visible="False"></asp:Label>
                                                    </span>
                                                </div>
                                            </div>







                                        </div>
                                    </div>


                                    <%-- <div class="">
                                         <div class="row">
                                            <div class="col-md-12">--%>
                                    <label class="col-md-4"><b>Payment Mode :</b></label>
                                    <div class="col-md-7">

                                        <asp:RadioButtonList ID="ddlPaymentMode" RepeatDirection="Vertical" CellPadding="20" CellSpacing="20" runat="server" style="text-indent:5px">
                                        </asp:RadioButtonList>
                                    </div>

                                    <%--<div class="col-md-2"></div>--%>



                                    <%--    </div>
                                            </div>
                                         
                                        </div>--%>
                                </div>

                                <div id="divAddress" runat="server">

                                    <div class="box-title" style="display: none;">
                                        &nbsp;&nbsp;
                                        <h4>
                                            <i class="fa fa-file-text-o"></i>Shipping Address</h4>
                                    </div>
                                    <div class="box-body" style="display: none;">
                                        <div class="form-group" id="Div1" runat="server">
                                            <label class="col-sm-4 control-label">
                                                Ship To:
                                            </label>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="ddlbranch" CssClass="form-control" runat="server"></asp:DropDownList>

                                            </div>
                                            <label class="col-sm-1 control-label">
                                                Other:
                                            </label>
                                            <div class="col-sm-1">
                                                <asp:CheckBox ID="rdOtherAdrress" runat="server"></asp:CheckBox>

                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-sm-4 control-label">
                                                Address:
                                            </label>


                                        </div>
                                        <div class="form-group" id="Div2" runat="server">
                                            <div class="col-sm-offset-4 col-sm-10">
                                                <%--  <asp:Button ID="Button1" ValidationGroup="b" runat="server" Text="Save"
                                            OnClick="btnSave_Click" CssClass="btn btn-primary" />--%>
                                                <%--<art:PopupDisplay ID="PopupDisplay1" runat="server" />--%>
                                            </div>
                                        </div>

                                    </div>
                                </div>




                            </div>
                            <div class="row text-right" id="trSave" runat="server">
                                <asp:Button ID="btnSave" runat="server" Text="Save"
                                    OnClick="btnSave_Click" CssClass="btn btn-primary" Enabled="false" />
                                <asp:Button ID="btncancel" runat="server" Text="Cancel"
                                    OnClick="btncancel_Click" CssClass="btn btn-default" />
                            </div>
                            <br />
                        </div>
                        <br />


                    </div>



                </div>
                <!-- /main content -->

                <%--</div>--%>
            </ContentTemplate>
            <Triggers>
                <%--  <asp:PostBackTrigger ControlID="ddlProduct" />
                <asp:PostBackTrigger ControlID="ddlQuantityType" />
                  <asp:PostBackTrigger ControlID="lnkAdd" />
           <asp:PostBackTrigger ControlID="grvTempBill" />
                 <asp:PostBackTrigger ControlID="btnAdd" />
                <asp:PostBackTrigger ControlID="dgPaymentTerms" />
                 <asp:PostBackTrigger ControlID="txtCupCode" />
               <asp:PostBackTrigger ControlID="txtUniqueProductId" />--%>
            </Triggers>
        </asp:UpdatePanel>
    </form>

</asp:Content>

