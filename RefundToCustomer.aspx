<%@ Page Title="Refund To Customer" Language="C#" MasterPageFile="~/Design/ECPMasterPage.master" AutoEventWireup="true" CodeFile="RefundToCustomer.aspx.cs" Inherits="Order_RefundToCustomer" %>

<%@ Register Src="../NotificationMessage.ascx" TagName="NotificationMessage" TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="~/CurrentCurrency.ascx" TagName="CurrentCurrency" TagPrefix="CurrentCurrency" %>
<%@ Register Src="~/LoadingImage.ascx" TagName="LoadingImage" TagPrefix="LoadingImage" %>




<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    
     <!-- Global stylesheets -->
    <link href="https://fonts.googleapis.com/css?family=Roboto:400,300,100,500,700,900" rel="stylesheet" type="text/css" />
    <link href="../Design/assets/css/icons/icomoon/styles.css" rel="stylesheet" type="text/css" />
     <!-- Core JS files -->
    <script type="text/javascript" src="../Design/assets/js/plugins/loaders/pace.min.js"></script>
    <script type="text/javascript" src="../Design/assets/js/core/libraries/jquery.min.js"></script>
    <script type="text/javascript" src="../Design/assets/js/core/libraries/bootstrap.min.js"></script>
    <script type="text/javascript" src="../Design/assets/js/plugins/loaders/blockui.min.js"></script>
    <!-- /core JS files -->






</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


      <form id="Form1" class="form-horizontal" runat="server">
        <ajaxToolkit:ToolkitScriptManager runat="server" ID="ScriptManager1" />
     <LoadingImage:LoadingImage ID="LoadingImage1" runat="server" />
        <asp:UpdatePanel ID="up1" runat="server">
            <ContentTemplate>
                <!-- Main content -->
                <%--<div class="content">--%>

                <!-- Page header -->
                <div class="page-header">
                    <div class="page-header-content">
                        <div class="page-title">
                            <h4><i class="icon-tree7 position-left"></i><span class="text-semibold">Refund to customer</span></h4>
                        </div>

                    </div>

                </div>
                <!-- /page header -->

                <!-- Content area -->
                <div class="content">
                    <uc3:NotificationMessage ID="NotificationMessage1" runat="server" />
                    <!-- Basic setup -->
                    <div class="panel panel-white">
                        <div class="panel-heading">
                            <h6 class="panel-title"></h6>

                        </div>
                        <div class="content">
                            <div class="row">
                                      
                                 <div class="col-md-12" id="customerInfo" visible="true" runat="server">
                                                                                      
                                             
                                                             
                                                   <div class="col-md-2">                                             
                                                       <asp:Label ID="lblmsg" runat="server" Font-Bold="true" Font-Size="Large">Customer:</asp:Label>


                                                     
                                                       <asp:Label ID="lblCustomerName" runat="server" Font-Size="Large"></asp:Label>
                                                       <asp:Label ID="lblId" runat="server" Visible="false"></asp:Label>
                                                        
                                                   </div> 
                                                                                          
                                                   <div class="col-md-2">          
                                                        <label> Date </label>
                                                    <%-- <div class="form-group">--%>
                                                    <div class="input-group">
                                                        <span class="input-group-addon"><i class="icon-calendar3"></i></span>
                                                        <asp:TextBox ID="txtPaymentDueDate" CssClass="form-control daterange-single" runat="server" placeholder="Date of issue"></asp:TextBox>
                                                    </div>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtPaymentDueDate" />
                                                  
                                                  </div>     
                                     
                                                   <div class="col-md-2">
                                                       <label>Reference number</label>
                                                      <div class="input-group">
                                                      <asp:TextBox ID="txtReferencenumber" runat="server" class="form-control" AutoPostBack="true" ReadOnly="true"></asp:TextBox>  
                                                          </div>                                                                                                                                                                        
                                                  </div>       
                                                                                                                            
                                                  <div class="col-md-2">
                                                       <label>Refund amount</label>
                                                      <div class="input-group">
                                                       <asp:TextBox ID="txtRefundAmount" runat="server" class="form-control" OnTextChanged="txtRefundAmount_TextChanged" AutoPostBack="true"></asp:TextBox>  
                                                        <asp:Label ID="lblTotal" Visible="false" runat="server"></asp:Label>
                                                            </div>                                                                                                                                                                        
                                                  </div>                                                                                                                      
                                                                                        
                                                <div class="col-md-2" id="divtxtUniqueProductId" runat="server">       
                                                    <label>From account</label>
                                                    <div class="input-group">
                                                         <asp:DropDownList ID="ddlCutomerAccounttype" runat="server" class="form-control" ></asp:DropDownList>                                                   
                                                    </div>                                                  
                                                </div>
                                                <div class="col-md-2" id="div4" runat="server">    
                                                       <label> Notes </label>
                                                      <div class="input-group">                                               
                                                    <asp:TextBox ID="txtNotes" runat="server" class="form-control"></asp:TextBox>  
                                                          </div>                                                  
                                               <br />
                                                    <br />                                 
                                              </div>                                                                                                                             
                  
                                     <br />
                               
                                        <div class="col-sm-offset-5 col-sm-6">
                                                 <asp:Button ID="btnSaveProduct" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSaveProduct_Click"/>

                                              <asp:Button ID="btnCancle" runat="server" Text="Cancel" CssClass="btn btn-primary" OnClick="btnCancle_Click"/>
                                            </div>


                                  
                                            </div>
                                          <asp:Label ID="lblmsgFordgtxt" runat="server" Visible="false" ForeColor="#ff0000"></asp:Label>
                                </div>
                              
                            </div>
                        </div>
                        <!-- /main content -->

                        <%--</div>--%>
            </ContentTemplate>
            <Triggers>
               
             
            </Triggers>
        </asp:UpdatePanel>
    </form>




</asp:Content>

