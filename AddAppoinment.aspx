<%@ Page Title="Appoinment" Language="C#" MasterPageFile="~/Design/ECPMasterPage.master" AutoEventWireup="true" CodeFile="AddAppoinment.aspx.cs" Inherits="Appointment_AddAppoinment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="../NotificationMessage.ascx" TagName="NotificationMessage" TagPrefix="uc3" %>
<%@ Register Src="~/LoadingImage.ascx" TagPrefix="LoadingImage" TagName="LoadingImage" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
    <!-- Core JS files -->
    <script type="text/javascript" src="../Design/assets/js/plugins/loaders/pace.min.js"></script>
    <script type="text/javascript" src="../Design/assets/js/core/libraries/jquery.min.js"></script>
    <script type="text/javascript" src="../Design/assets/js/core/libraries/bootstrap.min.js"></script>
    <script type="text/javascript" src="../Design/assets/js/plugins/loaders/blockui.min.js"></script>
    <!-- /core JS files -->

    <!-- Theme JS files -->
    <script type="text/javascript" src="../Design/assets/js/core/libraries/jquery_ui/core.min.js"></script>
    <script type="text/javascript" src="../Design/assets/js/plugins/forms/wizards/form_wizard/form.min.js"></script>
    <script type="text/javascript" src="../Design/assets/js/plugins/forms/wizards/form_wizard/form_wizard.min.js"></script>
    <script type="text/javascript" src="../Design/assets/js/plugins/forms/selects/select2.min.js"></script>
    <script type="text/javascript" src="../Design/assets/js/plugins/forms/styling/uniform.min.js"></script>
    <script type="text/javascript" src="../Design/assets/js/core/libraries/jasny_bootstrap.min.js"></script>
    <script type="text/javascript" src="../Design/assets/js/plugins/forms/validation/validate.min.js"></script>
    <script type="text/javascript" src="../Design/assets/js/plugins/notifications/sweet_alert.min.js"></script>

    <script type="text/javascript" src="../Design/assets/js/core/app.js"></script>
    <script type="text/javascript" src="../Design/assets/js/pages/wizard_form.js"></script>


    <script type="text/javascript" src="../Design/assets/js/core/libraries/jquery_ui/interactions.min.js"></script>
    <script type="text/javascript" src="../Design/assets/js/plugins/forms/selects/select2.min.js"></script>

    <script type="text/javascript" src="../Design/assets/js/pages/form_select2.js"></script>
    <!-- /theme JS files -->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


      <div class="content-wrapper">
        <form id="Form1" class="form-horizontal" runat="server">


            <ajaxToolkit:ToolkitScriptManager runat="server" ID="ScriptManager1" />
                      <LoadingImage:LoadingImage ID="LoadingImage1" runat="server" />

            <asp:UpdatePanel ID="up1" runat="server">
                <ContentTemplate>
                         <uc3:NotificationMessage ID="NotificationMessage2" runat="server" />

                    <!-- Page header -->
                    <div class="page-header">
                        <div class="page-header-content">
                            <div class="page-title">
                                <h4><i class="icon-tree7 position-left"></i><span class="text-semibold">Appoinment</span></h4>
                            </div>
                              <div class="heading-elements">
                                <asp:LinkButton ID="btnCreateOrder" CssClass="btn bg-blue btn-labeled heading-btn"  runat="server" visible="false"><b><i class="icon-plus-circle2"></i></b>Create Order</asp:LinkButton>
                            </div>
                          
                        </div>
                    </div>
                    <!-- /page header -->


                    <!-- Content area -->
                    <div class="content">

                        <!-- Vertical form options -->
                        <div class="row">

                            <!-- Basic layout-->
                            <div role="form">
                                <div class="panel panel-flat">
                                    <div class="panel-heading">
                                        <h5 class="panel-title">Appoinment Information</h5>
                                        <div class="heading-elements">
                                            <ul class="icons-list">
                                                <li><a data-action="collapse"></a></li>
                                                <li><a data-action="reload"></a></li>
                                                <li><a data-action="close"></a></li>
                                            </ul>
                                        </div>
                                    </div>
                                    
                                  
                      
                                    <center><asp:Label ID="lblmsg" Visible="false" runat="server" Font-Bold="true"></asp:Label></center>
                                    <div class="panel-body" id="divGrid" runat="server">
                                        <div class="row col-lg-12">

                                            <asp:DataGrid ID="dgAppoinments" runat="server" AutoGenerateColumns="False"  OnDeleteCommand="dgAppoinments_DeleteCommand" 
                                               
                                                AllowPaging="false" PageSize="10" PagerStyle-Mode="NumericPages" CssClass="datatable table table-striped table-bordered table-hover">
                                                <Columns>
                                                    <asp:TemplateColumn HeaderText="Number" Visible="false" HeaderStyle-Font-Bold="true" HeaderStyle-CssClass="sorting">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnAppointmentId" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.nAppointmentId") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>

                                                 

                                                    <asp:TemplateColumn HeaderText="Customer" HeaderStyle-Font-Bold="true" HeaderStyle-CssClass="sorting" HeaderStyle-Width="15%">

                                                        <ItemTemplate>
                                                    <asp:Label ID="lblcCustomerName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cCustomerName") %>'></asp:Label>


                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>


                                               

                                                       
                                                    <asp:TemplateColumn HeaderText="Appointment Description" HeaderStyle-Font-Bold="true" HeaderStyle-CssClass="sorting" HeaderStyle-Width="20%">

                                                        <ItemTemplate>
                                                    <asp:Label ID="lblcAppointmentDesc" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cAppointmentDesc") %>'></asp:Label>


                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>


                                                     <asp:TemplateColumn HeaderText="Location" HeaderStyle-Font-Bold="true" HeaderStyle-CssClass="sorting">

                                                        <ItemTemplate>
                                                    <asp:Label ID="lblcLocation" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cLocation") %>'></asp:Label>


                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>


                                                    <asp:TemplateColumn HeaderText="Start Date" HeaderStyle-Font-Bold="true" HeaderStyle-CssClass="sorting" HeaderStyle-Width="20%">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldtStartDate" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.dtStartDate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>

                                                      <asp:TemplateColumn HeaderText="End Date" HeaderStyle-Font-Bold="true" HeaderStyle-CssClass="sorting" HeaderStyle-Width="20%">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldtEndDate" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.dtEndDate")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>

                                                       <asp:TemplateColumn HeaderText="No Of People" HeaderStyle-Font-Bold="true" HeaderStyle-CssClass="sorting">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblnNoOfPeople" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.nNoOfPeople") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>

                                                       <asp:TemplateColumn HeaderText="Alert" HeaderStyle-Font-Bold="true" HeaderStyle-CssClass="sorting"  HeaderStyle-Width="10%">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblcAlert" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cAlert") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>

                                                  
                                                         
                                                    <asp:TemplateColumn HeaderText="Status" HeaderStyle-Font-Bold="true" HeaderStyle-CssClass="sorting">

                                                        <ItemTemplate>
                                                    <asp:Label ID="lblcAppointmentTitle" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.cAppointmentTitle") %>'></asp:Label>


                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>


                                                     <asp:TemplateColumn HeaderText="Re-Schedule" HeaderStyle-Font-Bold="true" HeaderStyle-CssClass="sorting" HeaderStyle-Width="1%">

                                                        <ItemTemplate>
                                                   
                                                             <asp:ImageButton ID="lnkres" ImageUrl="~/Design/Icon/reshedule.jpg" CommandName="ReSchedule" runat="server" Height="20px" Width="20px"
                                                               Text="ReSchedule"></asp:ImageButton>




                                            <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtenderName" runat="server" TargetControlID="lnkres"
                                                DisplayModalPopupID="ModalPopupExtenderName">
                                            </ajaxToolkit:ConfirmButtonExtender>
                                            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtenderName" runat="server" TargetControlID="lnkres"
                                                PopupControlID="tablemodal2"  CancelControlID="ButtonCancelName" />
                                             <asp:Panel runat="server" ID="tablemodal2" Style="display: none;">
                                                  <div class="modal-dialog">
                                                    <div class="modal-content modal-table">
                                                        <div class="modal-header">
                                                            <h4 class="modal-title"><asp:Label ID="lbltitle1" runat="server"></asp:Label></h4>
                                                        </div>
                                                        <div class="modal-body">
                                                            <br />
                                                           
                                                              <div class="form-group">

                                                                   <asp:Label id="lblStartDate" class="col-sm-offset-1 col-sm-3" visible="true" runat="server" Font-Bold="true">
                                                                   Start Date:
                                                                </asp:Label>

                                                                   <div class="col-sm-offset-0 col-sm-6">
                                                              
                                                                 <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control daterange-single"></asp:TextBox>
                                                                   <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtStartDate" />
                                                                       </div>
                                                                 </div>



                                                            
                                                                 <div class="form-group">

                                                                  <asp:Label id="lblStartTime" class="col-sm-offset-1 col-sm-3" visible="true" runat="server" Font-Bold="true">
                                                                   Select Start Time:
                                                              
                                                                      </asp:Label>
                                                                 <div class="col-sm-offset-0 col-sm-1">
                                           
                                        <asp:TextBox ID="txtTimeSDHH1" CssClass="form-control" runat="server" placeholder="HH" Width="80px"></asp:TextBox>
                                                                      <asp:Label id="lblhhh" runat="server" ForeColor="Red" Visible="true" ></asp:Label>
                                        </div>
                                     <div class="col-sm-offset-0 col-sm-1">
                
                                        <asp:TextBox ID="txtTimeSDMM1" CssClass="form-control" runat="server" placeholder="MM" Width="80px"></asp:TextBox>
                                           <asp:Label id="lblmmm" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                                         </div>
                                     <div class="col-sm-offset-0 col-sm-1">

                                        <asp:DropDownList ID="ddlSD1" runat="server" CssClass="form-control" Width="80px" AutoPostBack="false">
                                              <asp:ListItem>AM</asp:ListItem>
                                              <asp:ListItem>PM</asp:ListItem>
                                        </asp:DropDownList>
                                       



                                    </div>
                                                            </div>

                                                                </div>
                                                            
                                                       
                                                            <div class="form-group">
                                                                <div class="col-sm-offset-5 col-sm-3">
                                                                <asp:Button ID="btnSave"  runat="server" Text="OK"  CssClass="btn btn-success" OnClick="btnSave_Click" />
                                                                <asp:Button ID="ButtonCancelName" runat="server" Text="Cancel" CssClass="btn btn-danger" />
                                                                    </div>
                                                            </div>

                                                      
                                                    </div>
                                                </div>
                                            </asp:Panel>







                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>


                                                    <asp:TemplateColumn HeaderText="Change Status" HeaderStyle-Font-Bold="true" HeaderStyle-CssClass="sorting" HeaderStyle-Width="3%" Visible="true">
                                                         <ItemTemplate>
                                                            <asp:ImageButton ID="ImageButton1" ImageUrl="~/Design/Icon/user_edit.png" runat="server"
                                                               Text="Edit"></asp:ImageButton>


                                                             
                                                   
                                                  <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtenderName1" runat="server" TargetControlID="ImageButton1"
                                                DisplayModalPopupID="ModalPopupExtenderName1">
                                            </ajaxToolkit:ConfirmButtonExtender>
                                            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtenderName1" runat="server" TargetControlID="ImageButton1"
                                                PopupControlID="tablemodal1" CancelControlID="ButtonCancelName1" />
                                             <asp:Panel runat="server" ID="tablemodal1" Style="display: none;">
                                                  <div class="modal-dialog">
                                                    <div class="modal-content modal-table">
                                                        <div class="modal-header">
                                                            <h4 class="modal-title"><asp:Label ID="lbltitle" runat="server"></asp:Label></h4>
                                                        </div>
                                                        <div class="modal-body">
                                                            <br />
                                                            <div class="form-group">
                                                                <asp:Label id="lblsts" class="col-sm-offset-1 col-sm-3" visible="true" runat="server" Font-Bold="true">
                                                                    Change Status:
                                                                </asp:Label>

                                                                <div class="col-sm-offset-0 col-sm-6">
                                                                    <asp:DropDownList ID="ddlStatus" CssClass="form-control" runat="server">
                                                                         <asp:ListItem Value="SELECT">SELECT</asp:ListItem>
                                                                        <asp:ListItem Value="Confirm">Confirm</asp:ListItem>
                                                                        <asp:ListItem Value="Cancle">Cancle</asp:ListItem>
                                                                         <asp:ListItem Value="Pending">Pending</asp:ListItem>
                                                                       
                                                                    </asp:DropDownList>
                                                                 
                                                                </div>
                                                             
                                                                    </div>


                                                              <div class="form-group">

                                                                   <asp:Label id="lblEndDate" class="col-sm-offset-1 col-sm-3" visible="true" runat="server" Font-Bold="true">
                                                                   End Date:
                                                                </asp:Label>

                                                                   <div class="col-sm-offset-0 col-sm-6">
                                                              
                                                                 <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control daterange-single"></asp:TextBox>
                                                                   <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtEndDate" />
                                                                       </div>
                                                                 </div>



                                                            
                                                                 <div class="form-group">

                                                                  <asp:Label id="Label1" class="col-sm-offset-1 col-sm-3" visible="true" runat="server" Font-Bold="true">
                                                                   Select End Time:
                                                              
                                                                      </asp:Label>
                                                                 <div class="col-sm-offset-0 col-sm-1">
                                            <uc3:NotificationMessage ID="NotificationMessage1" runat="server" />
                                        <asp:TextBox ID="txtTimeSDHH2" CssClass="form-control" runat="server" placeholder="HH" Width="80px"></asp:TextBox>
                                                                      <asp:Label id="lblhh" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                                        </div>
                                     <div class="col-sm-offset-0 col-sm-1">
                
                                        <asp:TextBox ID="txtTimeSDMM2" CssClass="form-control" runat="server" placeholder="MM" Width="80px"></asp:TextBox>
                                           <asp:Label id="lblmm" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                                         </div>
                                     <div class="col-sm-offset-0 col-sm-1">

                                        <asp:DropDownList ID="ddlSD2" runat="server" CssClass="form-control" Width="80px" AutoPostBack="false">
                                              <asp:ListItem>AM</asp:ListItem>
                                              <asp:ListItem>PM</asp:ListItem>
                                        </asp:DropDownList>
                                       



                                    </div>
                                                            </div>

                                                                </div>
                                                            
                                                       
                                                            <div class="form-group">
                                                                <div class="col-sm-offset-5 col-sm-3">
                                                                <asp:Button ID="btnChanfeStatus"  runat="server" Text="OK"  CssClass="btn btn-success" OnClick="btnChanfeStatus_Click"/>
                                                                <asp:Button ID="ButtonCancelName1" runat="server" Text="Cancel" CssClass="btn btn-danger" />
                                                                    </div>
                                                            </div>

                                                      
                                                    </div>
                                                </div>
                                            </asp:Panel>




                                                        </ItemTemplate>

                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Delete" HeaderStyle-Font-Bold="true" HeaderStyle-CssClass="sorting" HeaderStyle-Width="3%" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ImageUrl="~/Design/Icon/trash.png" ID="delete" runat="server" CausesValidation="false"
                                                                CommandName="Delete" Text="Delete"></asp:ImageButton>
                                                            <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender2" runat="server" TargetControlID="delete"
                                                                DisplayModalPopupID="ModalPopupExtender1" />
                                                            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="delete"
                                                                PopupControlID="PNL" OkControlID="ButtonOk" />
                                                            <asp:Panel ID="PNL" runat="server" Style="display: none; width: 300px; background-color: White; border-width: 2px; border-color: Black; border-style: solid; padding: 20px;">
                                                                Are you sure you want to Delete the Item?
                                                                <br />
                                                                <br />
                                                                <div style="text-align: right;">
                                                                    <asp:Button ID="ButtonOk" runat="server" Text="OK" />
                                                                    <asp:Button ID="ButtonCancel" runat="server" Text="Cancel" />
                                                                </div>
                                                            </asp:Panel>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid>


                                          <br />
                                            <br />


                                        </div>
                                    </div>

                                </div>



                            </div>
                       
                        </div>
                    
                    </div>
               
                </ContentTemplate>
        
             
            </asp:UpdatePanel>
        </form>
    </div>



</asp:Content>

