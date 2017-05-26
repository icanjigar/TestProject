using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Order_CreateOrder : System.Web.UI.Page
{
    //Golbal Declaration used in Cupon apply event

    #region

    float fTotalProductAmount = 0;
    string strOfferValid = "No";
    float ftotaltemp = 0;
    float ffixDiscount = 0;
    float fPercentageDisc = 0;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        int intUserId = int.Parse(Session["UserId"].ToString());
        int intCustomerId = int.Parse(Request.QueryString["intCustomerId"].ToString());

        try
        {
            if (!IsPostBack)
            {
                if (intUserId != 0)
                {
                    BindList();
                    BindEmployees();

                    //bindTotalOrder();
                 
                    //START: DataTable for Payment terms
                    #region
                    DataTable tempPaymentTerms = new DataTable("PaymentTerms");
                    DataColumn cl = new DataColumn();
                    tempPaymentTerms.Columns.Add(new DataColumn("nPaymentTermsId", Type.GetType("System.String")));
                    tempPaymentTerms.Columns.Add(new DataColumn("cPaymentTerms", Type.GetType("System.String")));
                    tempPaymentTerms.Columns.Add(new DataColumn("nOrderId", Type.GetType("System.Int32")));
                    tempPaymentTerms.Columns.Add(new DataColumn("nUserId", Type.GetType("System.Int32")));
                    tempPaymentTerms.Columns.Add(new DataColumn("IsActive", Type.GetType("System.Boolean")));

                    Session["tempPaymentTerms"] = tempPaymentTerms;
                    #endregion
                    //END: DataTable for Payment terms
                    BindAccountType();

                    //START: Datatable for order item Add

                    #region
                    DataTable dtBill = new DataTable();
                    dtBill = CreateTempTable();
                    Session["dtBill"] = dtBill;
                    #endregion
                    //END: Datatable for order item Add

                    #region //START: Declare Function 

                            //Function for Customer Detail 
                            BindCustomerDetail(intCustomerId);

                            //Function for Product 
                            BindProduct();

                            //Function for Quantity type 
                            BindQuantityType(intUserId);

                    #endregion //END: Declare Function 

                    if (Request.QueryString["page"].ToString() == "0")
                    {

                    }
                    else
                    {
                        if (Request.QueryString["OrderId"].ToString() != "")
                        {
                            EditQuetationmaster();
                        }
                    }
                }
                else
                {
                    Response.Redirect("~/Defualt.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            string strMasssage = ex.Message;
            DALExceptionDetail objExceptionDetail = new DALExceptionDetail();
            objExceptionDetail.InsertRow(strMasssage, "Order_CreateOrder.aspx", intUserId, DateTime.Now, true);

        }
    }

    private void BindEmployees()
    {
        int intUserId = int.Parse(Session["UserId"].ToString());
        DALmstEmployee objDALmstEmployee = new DALmstEmployee();
        DataSet dsDALmstEmployee = new DataSet();
        dsDALmstEmployee = objDALmstEmployee.SelectAll(intUserId);
        if (dsDALmstEmployee.Tables[0].Rows.Count > 0)
        {
            ddlShippingTo.DataSource = dsDALmstEmployee.Tables[0];
            ddlShippingTo.DataTextField = "cEmpName";
            ddlShippingTo.DataValueField = "nEmpId";
            ddlShippingTo.DataBind();
            ddlShippingTo.Items.Insert(0, "Shipping To");
        }
        else
        {
            ddlShippingTo.Items.Insert(0, "Shipping To");
        }
    }

    #region Function Body

    public void BindQuantityType(int intUserId)
    {
        try
        {
            DALQuantity objDALQuantity = new DALQuantity();
            DataSet ds = new DataSet();
            ds = objDALQuantity.SelectAll(intUserId);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlQuantityType.DataSource = ds.Tables[0];
                ddlQuantityType.DataValueField = "nQuantityId";
                ddlQuantityType.DataTextField = "cQuantityType";
                ddlQuantityType.DataBind();
                ddlQuantityType.Items.Insert(0, new ListItem("Select a QTY type", "0"));

            }
        }
        catch (Exception ex)
        {
            string strMessage = ex.Message;
            DALExceptionDetail objDALExceptionDetail = new DALExceptionDetail();
            objDALExceptionDetail.InsertRow(strMessage, "Order_CreateOrder.aspx", intUserId, DateTime.Now, true);
        }
    }
    public void BindProduct()
    {
        int intUserId = int.Parse(Session["UserId"].ToString());
        try
        {
            DALmstProduct objProduct = new DALmstProduct();
            DataSet dsProduct = new DataSet();
            dsProduct = objProduct.SelectAllByType(intUserId, 2);
            if (dsProduct.Tables[0].Rows.Count > 0)
            {
                ddlProduct.DataSource = dsProduct.Tables[0];
                ddlProduct.DataTextField = "cName";
                ddlProduct.DataValueField = "nProductId";
                ddlProduct.DataBind();
                ddlProduct.Items.Insert(0, "Select a item");
            }
        }
        catch (Exception ex)
        {
            string strMasssage = ex.Message;
            DALExceptionDetail objExceptionDetail = new DALExceptionDetail();
            objExceptionDetail.InsertRow(strMasssage, "Order_CreateOrder.aspx", intUserId, DateTime.Now, true);

        }
    }

    public void BindCustomerDetail(int intCustomerId)
    {
        string strType = "Admin Order";
        int intUserId = int.Parse(Session["UserId"].ToString());
        DALCustomer objDALmstCustomer = new DALCustomer();
        DataSet dsCustomerName = new DataSet();
        dsCustomerName = objDALmstCustomer.SelectRow(intCustomerId, intUserId);
        if (dsCustomerName.Tables[0].Rows.Count > 0)
        {

            string strImage = dsCustomerName.Tables[0].Rows[0]["cCustomerImage"].ToString();
            if (strImage != "")
            {

                strImage = strImage.Replace("~", "..");
                imgCustomerlogo.Src = strImage;

            }
            else
            {
                strImage = "../Design/assets/images/placeholder.jpg";
                imgCustomerlogo.Src = strImage;
            }

            DALmstOrder objOrder = new DALmstOrder();
            DataSet dsOrder = new DataSet();
            dsOrder = objOrder.SelectRowByCustomerId(intCustomerId, intUserId, strType);
            if (dsOrder.Tables[0].Rows.Count > 0)
            {
                lblTotalOrder.Text = dsOrder.Tables[0].Rows.Count.ToString();
                lblDeliveryPending.Text = dsOrder.Tables[1].Rows.Count.ToString();
            }
            else
            {
                lblTotalOrder.Text = "0";
                lblDeliveryPending.Text = "0";
            }          


            lblEmailId.Text = "<span class='icon-envelop3 text-muted'></span><a href='mailto:" + dsCustomerName.Tables[0].Rows[0]["cCustomerEmailId"].ToString() + "' /><span class='text-muted'>&nbsp;" + dsCustomerName.Tables[0].Rows[0]["cCustomerEmailId"].ToString() + "</span></a>";
            lblCantactNo.Text = "<br><span class='icon-phone2 text-muted'></span><a href='callto:" + dsCustomerName.Tables[0].Rows[0]["cCustomerContactNo"].ToString() + "' /><span class='text-muted'>&nbsp;" + dsCustomerName.Tables[0].Rows[0]["cCustomerContactNo"].ToString() + "</span></a>";


            infoImage.HRef = "~/Customer/AddCustomer.aspx?intCustomerId=" + intCustomerId + "&page=2";
            linkchange.HRef = "~/Customer/AddCustomer.aspx?intCustomerId=" + intCustomerId + "&page=2";
            lblFirstName.Text = dsCustomerName.Tables[0].Rows[0]["cCustomerFirstName"].ToString();
            lblMiddleName.Text = dsCustomerName.Tables[0].Rows[0]["cCustomerMiddleName"].ToString();
            lblLastName.Text = dsCustomerName.Tables[0].Rows[0]["cCustomerLastName"].ToString();
            lblcunsultperson.Text = dsCustomerName.Tables[0].Rows[0]["cPrimaryPerson"].ToString();
            if (lblcunsultperson.Text == "")
            {
                lblcunsultperson.Text = "No consult person";
            }
            else
            {
                lblcunsultperson.Text = dsCustomerName.Tables[0].Rows[0]["cPrimaryPerson"].ToString();

            }


            string strCustomerType = dsCustomerName.Tables[0].Rows[0]["cCustomerType"].ToString();



            if (strCustomerType == "Salesman Reference")
            {
                //lblReference.Text = "Sales Team";
                // lblviewcustomerref.Text = "ST";
                lblLeadOwnerNAme.Text = dsCustomerName.Tables[0].Rows[0]["cEmpName"].ToString();

            }
            else if (strCustomerType == "Customer Reference")
            {
                lblLeadOwnerNAme.Text = dsCustomerName.Tables[0].Rows[0]["referenceCustomerName"].ToString();

            }
        }
    }

    #endregion
    
    #region Service Detail


    public DataTable CreateServices()
    {
        DataTable dtService = new DataTable();
        try
        {
            DataColumn dtColumn;

            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.Int32");
            dtColumn.ColumnName = "nServiceId";
            dtColumn.AllowDBNull = false;
            dtColumn.AutoIncrement = true;
            dtColumn.AutoIncrementSeed = 1;
            dtService.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.String");
            dtColumn.ColumnName = "cTypeOfService";
            dtService.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.DateTime");
            dtColumn.ColumnName = "dtServiceDate";
            dtService.Columns.Add(dtColumn);
            
            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.DateTime");
            dtColumn.ColumnName = "dtRegistrationDate";
            dtService.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.String");
            dtColumn.ColumnName = "cRegisteredBy";
            dtService.Columns.Add(dtColumn);


            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.DateTime");
            dtColumn.ColumnName = "dtAllocatedDate";
            dtService.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.Int32");
            dtColumn.ColumnName = "nAllocatedEmployee";
            dtService.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.String");
            dtColumn.ColumnName = "cReportFault";
            dtService.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.String");
            dtColumn.ColumnName = "cSiteAddress";
            dtService.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.String");
            dtColumn.ColumnName = "cSiteContact";
            dtService.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.Decimal");
            dtColumn.ColumnName = "fServiceCallCharge";
            dtService.Columns.Add(dtColumn);
            
            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.Int32");
            dtColumn.ColumnName = "nServiceStatusId";
            dtService.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.Boolean");
            dtColumn.ColumnName = "IsJobCompleted";
            dtService.Columns.Add(dtColumn);
            
            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.Int32");
            dtColumn.ColumnName = "nCompletedBy";
            dtService.Columns.Add(dtColumn);





            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.Int32");
            dtColumn.ColumnName = "nServiceCallChargeModel";
            dtService.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.String");
            dtColumn.ColumnName = "cServiceCallChargeSerialNo";
            dtService.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.Int32");
            dtColumn.ColumnName = "nServiceBillId";
            dtService.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.String");
            dtColumn.ColumnName = "cLatitude";
            dtService.Columns.Add(dtColumn);


            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.String");
            dtColumn.ColumnName = "cLongitude";
            dtService.Columns.Add(dtColumn);


            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.String");
            dtColumn.ColumnName = "cCustomerSignature";
            dtService.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.String");
            dtColumn.ColumnName = "cArePartsRequired";
            dtService.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.Int32");
            dtColumn.ColumnName = "nUserId";
            dtService.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.Int32");
            dtColumn.ColumnName = "nLanguageId";
            dtService.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.Boolean");
            dtColumn.ColumnName = "IsActive";
            dtService.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.String");
            dtColumn.ColumnName = "cRemark";
            dtService.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.String");
            dtColumn.ColumnName = "cRemark1";
            dtService.Columns.Add(dtColumn);
            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.String");
            dtColumn.ColumnName = "cRemark2";
            dtService.Columns.Add(dtColumn);
            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.String");
            dtColumn.ColumnName = "cRemark3";
            dtService.Columns.Add(dtColumn);


            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.String");
            dtColumn.ColumnName = "cRemark4";
            dtService.Columns.Add(dtColumn);
        }
        catch (Exception ex)
        {
            string strMessage = ex.Message;
            DALExceptionDetail objDALExceptionDetail = new DALExceptionDetail();
            int intUserId = int.Parse(Session["UserId"].ToString());

            objDALExceptionDetail.InsertRow(strMessage, "BillCreation.aspx12", intUserId, DateTime.Now, true);
        }
        return dtService;
    }

    public DataTable AddServicesTable(DataTable dtServices, DataTable dtBill)
    {
        DataTable ds = dtBill;
        try
        {

            DataRow dtRow;

            for (int i = 0; i < dtBill.Rows.Count; i++)
            {
                if (ds.Rows[i]["cWarrantyType"].ToString() == "Periodic")
                {
                    int intserviceDuration = int.Parse(ds.Rows[i]["nWarrentyDuration"].ToString());
                    int intWarrentyMonths = int.Parse(ds.Rows[i]["nWarrantyMonth"].ToString());
                    int intProductID = int.Parse(ds.Rows[i]["nProductId"].ToString());
                    int intCurrentDate = int.Parse(DateTime.Now.Day.ToString());
                    int intCurrentMonth = int.Parse(DateTime.Now.Month.ToString());
                    int intCurrentYear = int.Parse(DateTime.Now.Year.ToString());
                    DateTime dtServiceDate = DateTime.Now;
                    //if (txtdatechange.Text != "")
                    //{
                    //    dtServiceDate = Convert.ToDateTime(txtdatechange.Text.ToString());
                    //}

                    int intTotalServices = intWarrentyMonths / intserviceDuration;
                    int intFinalTotalService = intTotalServices;
                    int intBillId = int.Parse(ds.Rows[0]["nOrderId"].ToString());
                    int intref_UserId = int.Parse(ds.Rows[0]["nUserId"].ToString());
                    string strWarrentyType = ds.Rows[0]["cWarrantyType"].ToString();
                    int intBranchId = int.Parse(Session["BranchId"].ToString());

                    for (int intAddallservices = 0; intAddallservices < intFinalTotalService; intAddallservices++)
                    {
                        int intCustId = int.Parse(Request.QueryString["intCustomerId"]);

                        dtRow = dtServices.NewRow();
                        dtServiceDate = dtServiceDate.AddMonths(intserviceDuration);
                        
                        dtRow["cTypeOfService"] = strWarrentyType;
                        dtRow["dtServiceDate"] = dtServiceDate;
                        dtRow["dtRegistrationDate"] = DateTime.Now;

                        string strCustomerName = "";
                        DALCustomer objCustomer = new DALCustomer();
                        DataSet dsCustomer = new DataSet();
                        dsCustomer = objCustomer.SelectRow(intCustId, intref_UserId);
                        if (dsCustomer.Tables[0].Rows.Count > 0)
                        {
                            strCustomerName = dsCustomer.Tables[0].Rows[0]["cCustomerFirstName"].ToString() + "" + dsCustomer.Tables[0].Rows[0]["cCustomerLastName"].ToString();
                        }

                        dtRow["cRegisteredBy"] = strCustomerName;
                        dtRow["dtAllocatedDate"] = DateTime.Now;

                       // dtRow["nUserId"] = intref_UserId;
                        dtRow["nAllocatedEmployee"] = 0;
                        dtRow["cReportFault"] = "";
                        dtRow["cSiteAddress"] = "";
                        dtRow["cSiteContact"] = "";
                        dtRow["fServiceCallCharge"] = 0;
                        dtRow["nServiceStatusId"] =1;
                        dtRow["IsJobCompleted"] = "False";
                        dtRow["nCompletedBy"] = 0;
                        dtRow["nServiceCallChargeModel"] = 0;
                        dtRow["cServiceCallChargeSerialNo"] = "";

                        dtRow["cArePartsRequired"] = "No";
                        dtRow["nServiceBillId"] = 0;


                        dtRow["cLatitude"] = "";
                        dtRow["cLongitude"] = "";
                        dtRow["cCustomerSignature"] = "";
                        dtRow["nUserId"] = intref_UserId;
                       
                        dtRow["nLanguageId"] = 1;
                        dtRow["IsActive"] = "True";
                        dtRow["cRemark"] = "";


                        dtRow["cRemark1"] = "";
                        dtRow["cRemark2"] = "";
                        dtRow["cRemark3"] = "";
                        dtRow["cRemark4"] = "";
                       

                        dtServices.Rows.Add(dtRow);
                        intCurrentMonth = int.Parse(dtServiceDate.Month.ToString());
                        intCurrentDate = int.Parse(dtServiceDate.Day.ToString());
                        intCurrentYear = int.Parse(dtServiceDate.Year.ToString());
                    }
                }
            }
        }
        catch (Exception ex)
        {
            string strMessage = ex.Message;
            DALExceptionDetail objDALExceptionDetail = new DALExceptionDetail();
            int intUserId = int.Parse(Session["UserId"].ToString());

            objDALExceptionDetail.InsertRow(strMessage, "BillCreation.aspx13", intUserId, DateTime.Now, true);
        }
        return dtServices;
    }



    #endregion

    #region Datatable Body for order item add

    public DataTable CreateTempTable()
    {
        DataTable dtBill = new DataTable();
        try
        {


            DataColumn dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.Int32");
            dtColumn.ColumnName = "nOrderDetailId";
            dtColumn.AllowDBNull = false;
            dtColumn.AutoIncrement = true;
            dtColumn.AutoIncrementSeed = 1;
            dtBill.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.Int32");
            dtColumn.ColumnName = "nUserId";
            dtBill.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.Int32");
            dtColumn.ColumnName = "nOrderId";
            dtBill.Columns.Add(dtColumn);



            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.Int32");
            dtColumn.ColumnName = "nProductId";
            dtBill.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.Int32");
            dtColumn.ColumnName = "nPartsId";
            dtBill.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.String");
            dtColumn.ColumnName = "cName";
            dtBill.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.Int32");
            dtColumn.ColumnName = "nQuantityId";
            dtBill.Columns.Add(dtColumn);


            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.String");
            dtColumn.ColumnName = "cQuantityType";
            dtBill.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.Double");
            dtColumn.ColumnName = "fQuantity";
            dtBill.Columns.Add(dtColumn);


            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.String");
            dtColumn.ColumnName = "cDescription";
            dtBill.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.Double");
            dtColumn.ColumnName = "fProductPrice";
            dtBill.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.Double");
            dtColumn.ColumnName = "fMRP";
            dtBill.Columns.Add(dtColumn);


            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.String");
            dtColumn.ColumnName = "cWarrantyType";
            dtBill.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.String");
            dtColumn.ColumnName = "cTaxName";
            dtBill.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.Int32");
            dtColumn.ColumnName = "nTaxId";
            dtBill.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.Double");
            dtColumn.ColumnName = "fTaxPercentage";
            dtBill.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.Int32");
            dtColumn.ColumnName = "nWarrentyDuration";
            dtBill.Columns.Add(dtColumn);


            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.Int32");
            dtColumn.ColumnName = "nWarrantyMonth";
            dtBill.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.Double");
            dtColumn.ColumnName = "fDiscount";
            dtBill.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.Double");
            dtColumn.ColumnName = "fTotal";
            dtBill.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.Double");
            dtColumn.ColumnName = "fOrignalAmount";
            dtBill.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.DateTime");
            dtColumn.ColumnName = "dtDate";
            dtBill.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.Boolean");
            dtColumn.ColumnName = "IsActive";
            dtBill.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.Boolean");
            dtColumn.ColumnName = "IsDisable";
            dtBill.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.String");
            dtColumn.ColumnName = "cRemarks1";
            dtBill.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.String");
            dtColumn.ColumnName = "cRemarks2";
            dtBill.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.String");
            dtColumn.ColumnName = "cRemarks3";
            dtBill.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.String");
            dtColumn.ColumnName = "cImage";
            dtBill.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.Int32");
            dtColumn.ColumnName = "nCutomerAccountTypeId";
            dtBill.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.DataType = Type.GetType("System.String");
            dtColumn.ColumnName = "cAccountType";
            dtBill.Columns.Add(dtColumn);

        }
        catch (Exception ex)
        {
            string strMessage = ex.Message;
            DALExceptionDetail objDALExceptionDetail = new DALExceptionDetail();
            int intUserId = int.Parse(Session["UserId"].ToString());
            objDALExceptionDetail.InsertRow(strMessage, "CreateOrder.aspx", intUserId, DateTime.Now, true);
        }
        return dtBill;
    }

    #endregion

    #region Add/Update Order item into datatable

    public DataTable AddDataTable(DataTable dtBill)
    {
        try
        {
           
            // intPage = int.Parse(Request.QueryString["page"].ToString());
            DALmstProduct objDALProductMaster = new DALmstProduct();
            DataSet ds = new DataSet();
            if (ddlProduct.SelectedIndex != 0 && txtQuantity.Text != "")
            {
                #region
                int intProductId = int.Parse(ddlProduct.SelectedValue.ToString());
                int intUserId = int.Parse(Session["UserId"].ToString());
                ds = objDALProductMaster.Select_Product_Details(intProductId, intUserId);
                DataRow dtRow;
                dtRow = dtBill.NewRow();
                dtRow["nUserId"] = intUserId;
                // dtRow["nPartsId"] = 0;
                dtRow["nOrderId"] = 0;
                dtRow["IsDisable"] = false;
                dtRow["cImage"] = ds.Tables[0].Rows[0]["cImage"].ToString();
                dtRow["dtDate"] = DateTime.Now;
                dtRow["cName"] = ds.Tables[0].Rows[0]["cName"].ToString();
                dtRow["cDescription"] = ds.Tables[0].Rows[0]["cDescription"].ToString();
                dtRow["cWarrantyType"] = ds.Tables[0].Rows[0]["cWarrantyType"].ToString();
                if (ds.Tables[0].Rows[0]["nWarrentyId"].ToString() != "0")
                {
                    if (ds.Tables[0].Rows[0]["nWarrentyDuration"] != DBNull.Value)
                    {
                        dtRow["nWarrantyMonth"] = ds.Tables[0].Rows[0]["nWarrentyDuration"].ToString();
                        //dtRow["nWarrantyMonth"] = int.Parse(ds.Tables[0].Rows[0]["nWarrantyMonth"].ToString());
                    }
                    else
                    {
                        dtRow["nWarrantyMonth"] = 0;
                    }
                }
                else
                {
                    dtRow["nWarrantyMonth"] = 0;
                }
                int nBuyTaxId = int.Parse(ds.Tables[0].Rows[0]["nBuyTaxtype"].ToString());

                DALmstTax objTax = new DALmstTax();
                DataSet dsTax = new DataSet();
                dsTax = objTax.SelectRow(nBuyTaxId, intUserId);
                if (dsTax.Tables[0].Rows.Count > 0)
                {
                    dtRow["cTaxName"] = dsTax.Tables[0].Rows[0]["cTaxName"].ToString();
                    // dtRow["fTaxPercentage"] = dsTax.Tables[0].Rows[0]["fTaxPercentage"].ToString();
                }
                string str = ds.Tables[0].Rows[0]["cTaxName"].ToString();
                dtRow["nTaxId"] = ds.Tables[0].Rows[0]["nBuyTaxtype"].ToString();
                // dtRow["fTaxPercentage"] = ds.Tables[0].Rows[0]["fTaxPercentage"].ToString();
                dtRow["nProductId"] = intProductId;
                if (ds.Tables[0].Rows[0]["nWarrentyDuration"] != DBNull.Value)
                {
                    dtRow["nWarrentyDuration"] = int.Parse(ds.Tables[0].Rows[0]["nWarrentyDuration"].ToString());
                }
                else
                {
                    dtRow["nWarrentyDuration"] = 0;
                }

                float fPriceMRP = 0;
                fPriceMRP = float.Parse(txtPrice.Text);

                dtRow["fMRP"] = fPriceMRP;

                float fQuantity = float.Parse(txtQuantity.Text);
                float fDiscountPer = 0;
                if (txtDisCount.Text != "")
                {
                    fDiscountPer = float.Parse(txtDisCount.Text);
                }

                dtRow["fQuantity"] = fQuantity;
                dtRow["cQuantityType"] = ddlQuantityType.SelectedItem.Text;
                dtRow["nQuantityId"] = int.Parse(ddlQuantityType.SelectedValue.ToString());

                float fTempPRice = fPriceMRP * fQuantity;
                fTempPRice = fTempPRice - ((fTempPRice * fDiscountPer) / 100);
                dtRow["fProductPrice"] = fPriceMRP;

                dtRow["fTotal"] = fTempPRice;
                dtRow["fDiscount"] = fDiscountPer;
                dtRow["IsActive"] = true;

                if (ddlAllocateTo.SelectedIndex != 0)
                {
                    dtRow["nCutomerAccountTypeId"] = Convert.ToInt32(ddlAllocateTo.SelectedValue.ToString());

                    dtRow["cAccountType"] = ddlAllocateTo.SelectedItem.ToString();
                }

                DataTable dtProductCkeck = new DataTable();
                DataView dv = new DataView(dtBill);
                if (ddlProduct.SelectedItem.Text != "SELECT")
                {
                    dv.RowFilter = "nProductId='" + intProductId.ToString() + "'";
                    dtProductCkeck = dv.ToTable();
                }
                if (dtProductCkeck.Rows.Count <= 0)
                {
                    dtBill.Rows.Add(dtRow);
                    txtDisCount.Text = "0";
                    ClearText();
                }
                else if (dtProductCkeck.Rows.Count == 1)
                {


                    float fTempQuantity = 0;
                    for (int intitem = 0; intitem < dtProductCkeck.Rows.Count; intitem++)
                    {
                        fTempQuantity += float.Parse(dtProductCkeck.Rows[intitem]["fQuantity"].ToString());
                    }

                    fQuantity += fTempQuantity;
                    DataRow dr = dtBill.Select("nProductId=" + intProductId.ToString()).FirstOrDefault(); // finds all rows with id==2 and selects first or null if haven't found any
                    if (dr != null)
                    {
                        dr["fQuantity"] = fQuantity; //changes the Product_name
                        float fTPRice = fPriceMRP * fQuantity;
                        fTPRice = fTPRice - ((fTPRice * fDiscountPer) / 100);

                        dr["fMRP"] = fPriceMRP;
                        dr["fProductPrice"] = fPriceMRP;
                        // dr["fTotal"] = fTPRice - ((fTPRice * fDiscountPer) / 100);
                        dr["fTotal"] = fTPRice;
                        dr["fDiscount"] = fDiscountPer;
                    }

                    ClearText();
                    txtDisCount.Text = "0";
                    dtBill.Rows.Add(dr);
                   

                }
                else
                {
                    txtDisCount.Text = "0";
                    ClearText();
                    dtBill.Rows.Add(dtRow);
                   
                }
                #endregion
            }
            else if (txtUniqueProductId.Text != "")
            {
                #region START:Unique Code Add Item

                int intProductId = int.Parse(Session["ProductId"].ToString());
                int intUserId = int.Parse(Session["UserId"].ToString());
                ds = objDALProductMaster.Select_Product_Details(intProductId, intUserId);
                DataRow dtRow;
                dtRow = dtBill.NewRow();
                dtRow["nUserId"] = intUserId;
                //dtRow["nPartsId"] = 0;
                dtRow["nOrderId"] = 0;
                dtRow["IsDisable"] = false;
                dtRow["cImage"] = ds.Tables[0].Rows[0]["cImage"].ToString();
                dtRow["dtDate"] = DateTime.Now;
                dtRow["cName"] = ds.Tables[0].Rows[0]["cName"].ToString();
                dtRow["cDescription"] = ds.Tables[0].Rows[0]["cDescription"].ToString();
                dtRow["cWarrantyType"] = ds.Tables[0].Rows[0]["cWarrantyType"].ToString();
                if (ds.Tables[0].Rows[0]["nWarrentyId"].ToString() != "0")
                {
                    if (ds.Tables[0].Rows[0]["nWarrentyDuration"] != DBNull.Value)
                    {
                        dtRow["nWarrantyMonth"] = ds.Tables[0].Rows[0]["nWarrentyDuration"].ToString();
                        //dtRow["nWarrantyMonth"] = int.Parse(ds.Tables[0].Rows[0]["nWarrantyMonth"].ToString());
                    }
                    else
                    {
                        dtRow["nWarrantyMonth"] = 0;
                    }
                }
                else
                {
                    dtRow["nWarrantyMonth"] = 0;
                }
                int nBuyTaxId = int.Parse(ds.Tables[0].Rows[0]["nBuyTaxtype"].ToString());

                DALmstTax objTax = new DALmstTax();
                DataSet dsTax = new DataSet();
                dsTax = objTax.SelectRow(nBuyTaxId, intUserId);
                if (dsTax.Tables[0].Rows.Count > 0)
                {
                    dtRow["cTaxName"] = dsTax.Tables[0].Rows[0]["cTaxName"].ToString();
                    //  dtRow["fTaxPercentage"] = dsTax.Tables[0].Rows[0]["fTaxPercentage"].ToString();
                }

                dtRow["nTaxId"] = ds.Tables[0].Rows[0]["nBuyTaxtype"].ToString();
                //dtRow["cTaxName"] = ds.Tables[0].Rows[0]["cTaxName"].ToString();
                //dtRow["nTaxId"] = ds.Tables[0].Rows[0]["nTaxId"].ToString();
                dtRow["nProductId"] = intProductId;
                if (ds.Tables[0].Rows[0]["nWarrentyDuration"] != DBNull.Value)
                {
                    dtRow["nWarrentyDuration"] = int.Parse(ds.Tables[0].Rows[0]["nWarrentyDuration"].ToString());
                }
                else
                {
                    dtRow["nWarrentyDuration"] = 0;
                }

                float fPriceMRP = 0;

                fPriceMRP = float.Parse(txtPrice.Text);
                dtRow["fMRP"] = fPriceMRP;

              
                float fQuantity = float.Parse(txtQuantity.Text);
                float fDiscountPer = 0;
                if (txtDisCount.Text != "")
                {
                    fDiscountPer = float.Parse(txtDisCount.Text);
                }

                dtRow["fQuantity"] = fQuantity;
                dtRow["cQuantityType"] = ddlQuantityType.SelectedItem.Text;
                dtRow["nQuantityId"] = int.Parse(ddlQuantityType.SelectedValue.ToString());

                float fTempPRice = fPriceMRP * fQuantity;
                dtRow["fProductPrice"] = fPriceMRP;

                dtRow["fTotal"] = fTempPRice;
                dtRow["fDiscount"] = fDiscountPer;

                dtRow["IsActive"] = true;

                if (ddlAllocateTo.SelectedIndex != 0)
                {
                    dtRow["nCutomerAccountTypeId"] = Convert.ToInt32(ddlAllocateTo.SelectedValue.ToString());

                    dtRow["cAccountType"] = ddlAllocateTo.SelectedItem.ToString();
                }


                DataTable dtProductCkeck = new DataTable();
                DataView dv = new DataView(dtBill);
                if (ddlProduct.SelectedItem.Text != "SELECT")
                {
                    dv.RowFilter = "nProductId='" + intProductId.ToString() + "'";
                    dtProductCkeck = dv.ToTable();
                }
                if (dtProductCkeck.Rows.Count <= 0)
                {
                    dtBill.Rows.Add(dtRow);
                    txtDisCount.Text = "0";
                    ClearText();
                }
                else if (dtProductCkeck.Rows.Count == 1)
                {
                    float fTempQuantity = 0;
                    for (int intitem = 0; intitem < dtProductCkeck.Rows.Count; intitem++)
                    {
                        fTempQuantity += float.Parse(dtProductCkeck.Rows[intitem]["fQuantity"].ToString());
                    }
                    fQuantity += fTempQuantity;
                    DataRow dr = dtBill.Select("nProductId=" + intProductId.ToString()).FirstOrDefault(); // finds all rows with id==2 and selects first or null if haven't found any
                    if (dr != null)
                    {
                        dr["fQuantity"] = fQuantity; //changes the Product_name
                        float fTPRice = fPriceMRP * fQuantity;

                        dr["fMRP"] = fPriceMRP;
                        dr["fProductPrice"] = fPriceMRP;
                        dr["fTotal"] = fTPRice;
                        dr["fDiscount"] = fDiscountPer;
                    }

                    dtBill.Rows.Add(dr);
                    txtDisCount.Text = "0";
                    ClearText();

                }
                else
                {
                    dtBill.Rows.Add(dtRow);
                    txtDisCount.Text = "0";
                    ClearText();
                }
                #endregion

            }
            else if (ddlProduct.SelectedIndex == 0)
            {
                string strTitleN = "Data missing";
                string strDescriptions = "Select item";
                NotificationMessage1.NotificationDetails(strTitleN, strDescriptions);
            }
            else if (txtQuantity.Text == "")
            {
                string strTitleN = "Data missing";
                string strDescriptions = "Quantity required";
                NotificationMessage1.NotificationDetails(strTitleN, strDescriptions);
            }
           
        }
        catch (Exception ex)
        {
            string strMessage = ex.Message;
            DALExceptionDetail objDALExceptionDetail = new DALExceptionDetail();
            int intUserId = int.Parse(Session["UserId"].ToString());

            objDALExceptionDetail.InsertRow(strMessage, "BillCreation.aspx", intUserId, DateTime.Now, true);
        }
        Session["dtBill"] = dtBill;
        return dtBill;
    }
    public DataTable UpdateDataTable(DataTable dtBill)
    {
        try
        {
            DALmstProduct objDALProductMaster = new DALmstProduct();
            DataSet ds = new DataSet();

            if (ddlProduct.SelectedItem.Text != "SELECT" && txtQuantity.Text != "" && txtDisCount.Text != "")
            {
                int intProductId = int.Parse(ddlProduct.SelectedValue.ToString());
                int intUserId = int.Parse(Session["UserId"].ToString());
                ds = objDALProductMaster.Select_Product_Details(intProductId, intUserId);
                DataRow dtRow;
                dtRow = dtBill.NewRow();

                DataTable dtProductCkeck = new DataTable();
                DataView dv = new DataView(dtBill);
                if (ddlProduct.SelectedItem.Text != "SELECT")
                {
                    dv.RowFilter = "nProductId='" + intProductId.ToString() + "'";
                    dtProductCkeck = dv.ToTable();
                }

                float fPriceMRP = 0;
                fPriceMRP = float.Parse(txtPrice.Text);

                dtRow["fMRP"] = fPriceMRP;

                float fDiscountPer = 0;
                float fQuantity = float.Parse(txtQuantity.Text);
                float fTempPRice = fPriceMRP * fQuantity;
                if (dtProductCkeck.Rows.Count == 1)
                {
                    DataRow dr = dtBill.Select("nProductId=" + intProductId.ToString()).FirstOrDefault(); // finds all rows with id==2 and selects first or null if haven't found any
                    if (dr != null)
                    {
                        dr["cImage"] = ds.Tables[0].Rows[0]["cImage"].ToString();
                        dr["nUserId"] = intUserId;

                        dr["nOrderId"] = 0;
                        dr["IsDisable"] = false;
                        dr["dtDate"] = DateTime.Now;
                        dr["cName"] = ds.Tables[0].Rows[0]["cName"].ToString();
                        dr["cDescription"] = ds.Tables[0].Rows[0]["cDescription"].ToString();
                        dr["cWarrantyType"] = ds.Tables[0].Rows[0]["cWarrantyType"].ToString();
                        if (ds.Tables[0].Rows[0]["nWarrentyId"].ToString() != "0")
                        {
                            if (ds.Tables[0].Rows[0]["nWarrentyDuration"] != DBNull.Value)
                            {
                                dr["nWarrantyMonth"] = ds.Tables[0].Rows[0]["nWarrentyDuration"].ToString();
                            }
                            else
                            {
                                dr["nWarrantyMonth"] = 0;
                            }
                        }
                        else
                        {
                            dr["nWarrantyMonth"] = 0;
                        }
                        int nBuyTaxId = int.Parse(ds.Tables[0].Rows[0]["nBuyTaxtype"].ToString());

                        DALmstTax objTax = new DALmstTax();
                        DataSet dsTax = new DataSet();
                        dsTax = objTax.SelectRow(nBuyTaxId, intUserId);
                        if (dsTax.Tables[0].Rows.Count > 0)
                        {
                            dr["cTaxName"] = dsTax.Tables[0].Rows[0]["cTaxName"].ToString();
                            //  dr["fTaxPercentage"] = dsTax.Tables[0].Rows[0]["fTaxPercentage"].ToString();
                        }

                        dr["nTaxId"] = ds.Tables[0].Rows[0]["nBuyTaxtype"].ToString();

                        if (ds.Tables[0].Rows[0]["nWarrentyDuration"] != DBNull.Value)
                        {
                            dr["nWarrentyDuration"] = int.Parse(ds.Tables[0].Rows[0]["nWarrentyDuration"].ToString());
                        }
                        else
                        {
                            dr["nWarrentyDuration"] = 0;
                        }

                        fPriceMRP = float.Parse(txtPrice.Text);

                        if (txtDisCount.Text != "")
                        {
                            fDiscountPer = float.Parse(txtDisCount.Text);
                        }
                        dr["IsActive"] = true;
                        dr["fQuantity"] = fQuantity; //changes the Product_name
                        dr["fMRP"] = fPriceMRP;
                        float fTPRice = fPriceMRP * fQuantity;
                        dr["fProductPrice"] = fPriceMRP;
                        dr["cQuantityType"] = ddlQuantityType.SelectedItem.Text;
                        dr["nQuantityId"] = int.Parse(ddlQuantityType.SelectedValue.ToString());
                        dr["fTotal"] = fTPRice - ((fTPRice * fDiscountPer) / 100);
                        //dr["fTotal"] = fTPRice;
                        dr["fDiscount"] = fDiscountPer;

                        if (ddlAllocateTo.SelectedIndex != 0)
                        {
                            dr["nCutomerAccountTypeId"] = Convert.ToInt32(ddlAllocateTo.SelectedValue.ToString());
                            dr["cAccountType"] = ddlAllocateTo.SelectedItem.ToString();
                        }

                    }
                    ClearText();
                    txtDisCount.Text = "0";
                    dtBill.Rows.Add(dr);
                }
                else
                {
                  
                    int nOrderDetailId = int.Parse(Session["nOrderDetailId"].ToString());

                    DataRow dr = dtBill.Select("nOrderDetailId=" + nOrderDetailId.ToString()).FirstOrDefault(); // finds all rows with id==2 and selects first or null if haven't found any
                    if (dr != null)
                    {

                        float fTPRice = fPriceMRP * fQuantity;
                        dr["cName"] = ds.Tables[0].Rows[0]["cName"].ToString();
                        dr["cDescription"] = ds.Tables[0].Rows[0]["cDescription"].ToString();
                        dr["cWarrantyType"] = ds.Tables[0].Rows[0]["cWarrantyType"].ToString();
                        if (ds.Tables[0].Rows[0]["nWarrentyId"].ToString() != "0")
                        {
                            if (ds.Tables[0].Rows[0]["nWarrentyDuration"] != DBNull.Value)
                            {
                                dr["nWarrantyMonth"] = ds.Tables[0].Rows[0]["nWarrentyDuration"].ToString();
                            }
                            else
                            {
                                dr["nWarrantyMonth"] = 0;
                            }
                        }
                        else
                        {
                            dr["nWarrantyMonth"] = 0;
                        }
                        int nBuyTaxId = int.Parse(ds.Tables[0].Rows[0]["nBuyTaxtype"].ToString());

                        DALmstTax objTax = new DALmstTax();
                        DataSet dsTax = new DataSet();
                        dsTax = objTax.SelectRow(nBuyTaxId, intUserId);
                        if (dsTax.Tables[0].Rows.Count > 0)
                        {
                            dr["cTaxName"] = dsTax.Tables[0].Rows[0]["cTaxName"].ToString();
                            //  dr["fTaxPercentage"] = dsTax.Tables[0].Rows[0]["fTaxPercentage"].ToString();
                        }

                        dr["nTaxId"] = ds.Tables[0].Rows[0]["nBuyTaxtype"].ToString();
                        dr["nProductId"] = intProductId;
                        if (ds.Tables[0].Rows[0]["nWarrentyDuration"] != DBNull.Value)
                        {
                            dr["nWarrentyDuration"] = int.Parse(ds.Tables[0].Rows[0]["nWarrentyDuration"].ToString());
                        }
                        else
                        {
                            dr["nWarrentyDuration"] = 0;
                        }

                        fPriceMRP = float.Parse(txtPrice.Text);

                        dr["fMRP"] = fPriceMRP;

                        if (txtDisCount.Text != "")
                        {
                            fDiscountPer = float.Parse(txtDisCount.Text);
                        }

                        dr["fQuantity"] = fQuantity;
                        dr["fProductPrice"] = fPriceMRP;
                        fTPRice = fTPRice - ((fTPRice * fDiscountPer) / 100);
                        dr["fTotal"] = fTPRice;
                        dr["fDiscount"] = fDiscountPer;

                        dr["IsActive"] = true;

                        if (ddlAllocateTo.SelectedIndex != 0)
                        {
                            dr["nCutomerAccountTypeId"] = Convert.ToInt32(ddlAllocateTo.SelectedValue.ToString());
                            dr["cAccountType"] = ddlAllocateTo.SelectedItem.ToString();
                        }

                        dr["fMRP"] = fPriceMRP;
                    }
                    txtDisCount.Text = "0";
                    ClearText();
                    dtBill.Rows.Add(dr);
                }
            }
            else
            {
                int intProductId = int.Parse(Session["ProductID"].ToString());
                int intUserId = int.Parse(Session["UserId"].ToString());
                ds = objDALProductMaster.Select_Product_Details(intProductId, intUserId);
                DataRow dtRow;
                dtRow = dtBill.NewRow();
                dtRow["nUserId"] = intUserId;
                dtRow["nPartsId"] = 0;
                dtRow["nOrderId"] = 0;
                dtRow["cName"] = ds.Tables[0].Rows[0]["cName"].ToString();
                dtRow["cDescription"] = ds.Tables[0].Rows[0]["cDescription"].ToString();
                dtRow["cWarrantyType"] = ds.Tables[0].Rows[0]["cWarrantyType"].ToString();
                if (ds.Tables[0].Rows[0]["nWarrentyId"].ToString() != "0")
                {
                    if (ds.Tables[0].Rows[0]["nWarrentyDuration"] != DBNull.Value)
                    {
                        dtRow["nWarrantyMonth"] = ds.Tables[0].Rows[0]["nWarrentyDuration"].ToString();
                    }
                    else
                    {
                        dtRow["nWarrantyMonth"] = 0;
                    }
                }
                else
                {
                    dtRow["nWarrantyMonth"] = 0;
                }
                int nBuyTaxId = int.Parse(ds.Tables[0].Rows[0]["nBuyTaxtype"].ToString());

                DALmstTax objTax = new DALmstTax();
                DataSet dsTax = new DataSet();
                dsTax = objTax.SelectRow(nBuyTaxId, intUserId);
                if (dsTax.Tables[0].Rows.Count > 0)
                {
                    dtRow["cTaxName"] = dsTax.Tables[0].Rows[0]["cTaxName"].ToString();
                    //dtRow["fTaxPercentage"] = dsTax.Tables[0].Rows[0]["fTaxPercentage"].ToString();
                }

                dtRow["nTaxId"] = ds.Tables[0].Rows[0]["nBuyTaxtype"].ToString();
                dtRow["nProductId"] = intProductId;
                if (ds.Tables[0].Rows[0]["nWarrentyDuration"] != DBNull.Value)
                {
                    dtRow["nWarrentyDuration"] = int.Parse(ds.Tables[0].Rows[0]["nWarrentyDuration"].ToString());
                }
                else
                {
                    dtRow["nWarrentyDuration"] = 0;
                }

                float fPriceMRP = 0;

                fPriceMRP = float.Parse(txtPrice.Text);

                dtRow["fMRP"] = fPriceMRP;
                float fQuantity = float.Parse(txtQuantity.Text);
                float fDiscountPer = 0;
                if (txtDisCount.Text != "")
                {
                    fDiscountPer = float.Parse(txtDisCount.Text);
                }

                dtRow["fQuantity"] = fQuantity;
                dtRow["cQuantityType"] = ddlQuantityType.SelectedItem.Text;
                dtRow["nQuantityId"] = int.Parse(ddlQuantityType.SelectedValue.ToString());

                float fTempPRice = fPriceMRP * fQuantity;
                dtRow["fProductPrice"] = fPriceMRP;
                dtRow["fTotal"] = fTempPRice - ((fTempPRice * fDiscountPer) / 100);
                //dtRow["fTotal"] = fTempPRice;
                dtRow["fDiscount"] = fDiscountPer;
                //dtRow["cDetails"] = txtDetails.Text;
                dtRow["IsActive"] = true;

                if (ddlAllocateTo.SelectedIndex != 0)
                {
                    dtRow["nCutomerAccountTypeId"] = Convert.ToInt32(ddlAllocateTo.SelectedValue.ToString());
                    dtRow["cAccountType"] = ddlAllocateTo.SelectedItem.ToString();
                }

                DataTable dtProductCkeck = new DataTable();
                DataView dv = new DataView(dtBill);
                if (ddlProduct.SelectedItem.Text != "SELECT")
                {
                    dv.RowFilter = "nProductId='" + intProductId.ToString() + "'";
                    dtProductCkeck = dv.ToTable();
                }
                if (dtProductCkeck.Rows.Count <= 0)
                {
                    dtBill.Rows.Add(dtRow);
                    txtDisCount.Text = "0";
                    ClearText();
                }
                else if (dtProductCkeck.Rows.Count == 1)
                {
                    float fTempQuantity = 0;
                    for (int intitem = 0; intitem < dtProductCkeck.Rows.Count; intitem++)
                    {
                        fTempQuantity += float.Parse(dtProductCkeck.Rows[intitem]["fQuantity"].ToString());
                    }
                    fQuantity += fTempQuantity;
                    DataRow dr = dtBill.Select("nProductId=" + intProductId.ToString()).FirstOrDefault(); // finds all rows with id==2 and selects first or null if haven't found any
                    if (dr != null)
                    {
                        dr["fQuantity"] = fQuantity; //changes the Product_name
                        float fTPRice = fPriceMRP * fQuantity;


                        dr["fMRP"] = fPriceMRP;
                        dr["fProductPrice"] = fPriceMRP;
                        dr["fTotal"] = fTPRice - ((fTPRice * fDiscountPer) / 100);
                        // dr["fTotal"] = fTPRice;
                        dr["fDiscount"] = fDiscountPer;
                    }

                    dtBill.Rows.Add(dr);
                    txtDisCount.Text = "0";
                    ClearText();

                }
                else
                {
                    DataRow dr = dtBill.Select("nOrderDetailId=" + intProductId.ToString()).FirstOrDefault(); // finds all rows with id==2 and selects first or null if haven't found any
                    if (dr != null)
                    {
                        dr["fQuantity"] = fQuantity; //changes the Product_name
                        float fTPRice = fPriceMRP * fQuantity;


                        dr["fMRP"] = fPriceMRP;
                        dr["fProductPrice"] = fPriceMRP;
                        dr["fTotal"] = fTPRice - ((fTPRice * fDiscountPer) / 100);
                        // dr["fTotal"] = fTPRice;
                        dr["fDiscount"] = fDiscountPer;
                    }
                    dtBill.Rows.Add(dtRow);
                    txtDisCount.Text = "0";
                    ClearText();
                }
            }
        }
        catch (Exception ex)
        {
            string strMessage = ex.Message;
            DALExceptionDetail objDALExceptionDetail = new DALExceptionDetail();
            int intUserId = int.Parse(Session["UserId"].ToString());

            objDALExceptionDetail.InsertRow(strMessage, "BillCreation.aspx", intUserId, DateTime.Now, true);
        }
        Session["dtBill"] = dtBill;
        return dtBill;
    }
    #endregion

    public void BindAccountType()
    {
        int intUserId = int.Parse(Session["UserId"].ToString());
        try
        {
            DALCutomerAccountType objCustomerAccountType = new DALCutomerAccountType();
            DataSet dsCustomerAccountType = new DataSet();
            dsCustomerAccountType = objCustomerAccountType.SelectAll(intUserId);
            if (dsCustomerAccountType.Tables[0].Rows.Count > 0)
            {
                ddlAllocateTo.DataSource = dsCustomerAccountType.Tables[0];
                ddlAllocateTo.DataTextField = "cAccountType";
                ddlAllocateTo.DataValueField = "nCutomerAccountTypeId";
                ddlAllocateTo.DataBind();
                ddlAllocateTo.Items.Insert(0, "Allocate To");
            }
            else
            {
                ddlAllocateTo.Items.Insert(0, "Allocate To");
            }
           
        }
        catch (Exception ex)
        {
            DALExceptionDetail objDALExceptionDetail = new DALExceptionDetail();
            objDALExceptionDetail.InsertRow(ex.Message, "CreateInvoice.aspx", intUserId, DateTime.Now, true);
        }
    }



    #region Order Item Grid Event

    protected void grvTempBill_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {

            DataTable dtTemp = new DataTable();
            dtTemp = (DataTable)Session["dtBill"];
            int intUserId = int.Parse(Session["UserId"].ToString());
            if (btnSave.Text == "Update")
            {

                GridViewRow currentRow = this.grvTempBill.Rows[e.RowIndex];
                Label parentLicense = currentRow.FindControl("dgtProductId") as Label;
                Label dgnOrderDetailId = currentRow.FindControl("dgtnOrderDetailId") as Label;
                int intPurchaseOrderDetailId = 0;
                if (dgnOrderDetailId.Text != "")
                {
                     intPurchaseOrderDetailId = Convert.ToInt32(dgnOrderDetailId.Text);

                    DALdtlOrderDetail objDALPurchaseDetail = new DALdtlOrderDetail();

                    objDALPurchaseDetail.DeleteRow(intPurchaseOrderDetailId, intUserId);
                }
               
            }

            int intId = e.RowIndex;// Convert.ToInt32(grvTempBill.Rows[e.RowIndex].Cells[0].Text);

            float fltTotalAmount = float.Parse(lblTotal.Text);
            float fltReduceAmount = float.Parse(dtTemp.Rows[intId]["fTotal"].ToString());
            fltTotalAmount = fltTotalAmount - fltReduceAmount;
            lblTotal.Text = Math.Round(fltTotalAmount, 2).ToString();
            dtTemp.Rows[intId].Delete();
            dtTemp.AcceptChanges();
        
            dtTemp.AcceptChanges();
            grvTempBill.DataSource = dtTemp;
            grvTempBill.DataBind();

            float fltTempTotal = 0;
            for (int intTotalRows = 0; intTotalRows < grvTempBill.Rows.Count; intTotalRows++)
            {
                float fltTempMRP = float.Parse(dtTemp.Rows[intTotalRows]["fTotal"].ToString());
                fltTempTotal = fltTempTotal + fltTempMRP;

            }
            float fTotal = 0;
            float fBillAmt = 0;
            float fDiscount = 0;
            float fTotalDiscount = 0;
            for (int rowId = 0; rowId < dtTemp.Rows.Count; rowId++)
            {
                fBillAmt += float.Parse(dtTemp.Rows[rowId]["fTotal"].ToString());
                fDiscount += float.Parse(dtTemp.Rows[rowId]["fDiscount"].ToString());
            }
            if (fDiscount == 0)
            {
                divCupon.Visible = true;
            }
            else
            {
                divCupon.Visible = false;
            }
            fTotal = fTotal + fBillAmt;
            lblTotal.Visible = true;
            lblTotal.Text = Math.Round(fTotal, 2).ToString();
            lblDiscount.Visible = true;

            fTotalDiscount = fTotalDiscount + fDiscount;
            lblDiscount.Text = "%" + fTotalDiscount.ToString();
            float fPayable = fTotal - ((fTotal * fTotalDiscount) / 100);
            lblPayable.Visible = true;
            lblPayable.Text = fPayable.ToString();

            if (grvTempBill.Rows.Count > 0)
            {
                btnSave.Enabled = true;
            }
            else
            {
                btnSave.Enabled = false;
            }
            try
            {
                if (grvTempBill.Rows.Count <= 0)
                {
                    lblTotal.Text = "0";
                }
            }
            catch (Exception ex)
            {
            }
        }
        catch (Exception ex)
        {
            string strMessage = ex.Message;
            DALExceptionDetail objDALExceptionDetail = new DALExceptionDetail();
            int intUserId = int.Parse(Session["UserId"].ToString());
            objDALExceptionDetail.InsertRow(strMessage, "Order_CreateOrder.aspx", intUserId, DateTime.Now, true);
        }
    }

    protected void grvTempBill_PreRender(object sender, EventArgs e)
    {
       
    }

    protected void grvTempBill_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int intUserId = int.Parse(Session["UserId"].ToString());
        DataTable dtBill = (DataTable)Session["dtBill"];

        GridViewRow currentRow = this.grvTempBill.Rows[e.NewEditIndex];
        Label parentLicense = currentRow.FindControl("dgtProductId") as Label;
        Label dgnOrderDetailId = currentRow.FindControl("dgtnOrderDetailId") as Label;
        Session["nOrderDetailId"] = dgnOrderDetailId.Text;
        int intProductId = 0;
        if (parentLicense.Text != "")
        {
            intProductId = int.Parse(parentLicense.Text.ToString());
        }

        DataTable dtProductCkeck = new DataTable();
        DataView dv = new DataView(dtBill);

        dv.RowFilter = "nProductId='" + intProductId.ToString() + "'";
        dtProductCkeck = dv.ToTable();
        if (dtProductCkeck.Rows.Count == 1)
        {
            lblName.Text = "Update Item";

            txtDisCount.Text = dtProductCkeck.Rows[0]["fDiscount"].ToString();
            txtQuantity.Text = dtProductCkeck.Rows[0]["fQuantity"].ToString();
            ddlQuantityType.SelectedValue = dtProductCkeck.Rows[0]["nQuantityId"].ToString();
            if (dtProductCkeck.Rows[0]["nCutomerAccountTypeId"].ToString() != null && dtProductCkeck.Rows[0]["nCutomerAccountTypeId"].ToString() != "" && dtProductCkeck.Rows[0]["nCutomerAccountTypeId"].ToString() !="0")
            {
                ddlAllocateTo.SelectedValue = dtProductCkeck.Rows[0]["nCutomerAccountTypeId"].ToString();
            }

            txtPrice.Text = dtProductCkeck.Rows[0]["fMRP"].ToString();

            DALmstProduct objProductMaster = new DALmstProduct();
            DataSet dsProductMaster = new DataSet();
            dsProductMaster = objProductMaster.SelectRow(intProductId, intUserId);
            if (dsProductMaster.Tables[0].Rows.Count > 0)
            {
                string str = dtProductCkeck.Rows[0]["nProductId"].ToString();
                ddlProduct.SelectedValue = str.ToString();
            }
        }

    }

    protected void grvTempBill_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblProductImage = (Label)e.Row.FindControl("lblProductImage");
            Literal ltrProductImage = (Literal)e.Row.FindControl("ltrProductImage");
            //Image imgProduct = (Image)grvTempBill.FindControl("imgProduct");
            string strCustomerImage = lblProductImage.Text;
            strCustomerImage = strCustomerImage.Replace("~/", "../");
            ltrProductImage.Text = "<img src="+System.Configuration.ConfigurationManager.AppSettings["ServerPath"]  + strCustomerImage + " class='img-circle img-lg' alt=''>";
        }
    }

    #endregion

    #region Clear item detail

    public void ClearText()
    {
        txtUniqueProductId.Text = "";
        ddlProduct.SelectedIndex = 0;
        txtQuantity.Text = "";
        ddlQuantityType.SelectedIndex = 0;
        txtDisCount.Text = "0";
        ddlAllocateTo.SelectedIndex = 0;
        txtPrice.Text = "";
    }

    #endregion

    #region Add/Update Item Method
 
    protected void lnkAdd_Click(object sender, EventArgs e)
    {
        ddlPaymentMode.SelectedIndex = 0;
        int CustId = 0;
        if (!string.IsNullOrEmpty(Request.QueryString["intCustomerId"]))
        {
            CustId = int.Parse(Request.QueryString["intCustomerId"].ToString());
        }
        try
        {
            int intUserId = int.Parse(Session["UserId"].ToString());
            if (CustId > 0)
            {
                int nflag = 0;
                if (txtPrice.Visible == true && txtPrice.Text == "")
                {
                    nflag = 1;
                }

                if (ddlProduct.SelectedIndex != 0 || txtUniqueProductId.Text != "")
                {
                    float fDiscount = 0;

                    if (txtDisCount.Text != "")
                    {
                        fDiscount = float.Parse(txtDisCount.Text);
                    }
                    if (int.Parse(fDiscount.ToString()) < 100)
                    {
                        if (nflag == 0)
                        {
                            if (lblName.Text == "Add Items")
                            {
                                DataTable dtBill = new DataTable();
                                if ((ddlProduct.SelectedIndex != 0 || txtUniqueProductId.Text != "") && txtQuantity.Text != "" && txtQuantity.Text != "0")
                                {
                                    dtBill = (DataTable)Session["dtBill"];
                                    dtBill = AddDataTable(dtBill);
                                    Session["dtBill"] = dtBill;
                                    BindTableGrid();
                                }
                                else if ((ddlProduct.SelectedIndex == 0 && txtUniqueProductId.Text == ""))
                                {
                                    if (ddlProduct.SelectedIndex == 0)
                                    {
                                        string strTitleN = "Data missing";
                                        string strDescriptions = "Select item";
                                        NotificationMessage1.NotificationDetails(strTitleN, strDescriptions);
                                    }
                                    else if (txtUniqueProductId.Text == "")
                                    {
                                        string strTitleN = "Data missing";
                                        string strDescriptions = "Enter item Code";
                                        NotificationMessage1.NotificationDetails(strTitleN, strDescriptions);
                                    }
                                }
                                else if (txtQuantity.Text == "" || txtQuantity.Text == "0")
                                {
                                    string strTitleN = "Data missing";
                                    string strDescriptions = "Quantity required and quantity must be greater than zero";
                                    NotificationMessage1.NotificationDetails(strTitleN, strDescriptions);
                                }                                
                            }
                            else
                            {
                                if (ddlProduct.SelectedIndex != 0 && txtQuantity.Text != "" && txtQuantity.Text != "0")
                                {

                                    DataTable dtBill = new DataTable();
                                    dtBill = (DataTable)Session["dtBill"];
                                    dtBill = UpdateDataTable(dtBill);
                                    Session["dtBill"] = dtBill;
                                    BindUpdateGrid();
                                }
                                else if (ddlProduct.SelectedIndex == 0)
                                {
                                    string strTitleN = "Data missing";
                                    string strDescriptions = "Select item";
                                    NotificationMessage1.NotificationDetails(strTitleN, strDescriptions);
                                }
                                else if (txtQuantity.Text == "" || txtQuantity.Text == "0")
                                {
                                    string strTitleN = "Data missing";
                                    string strDescriptions = "Quantity required and quantity must be greater than zero";
                                    NotificationMessage1.NotificationDetails(strTitleN, strDescriptions);
                                }
                            }
                        }
                        else
                        {

                            string strTitleN = "Data Missing";
                            string strDescriptions = "Please enter price";
                            NotificationMessage1.NotificationDetails(strTitleN, strDescriptions);

                        }
                    }
                    else
                    {
                        string strTitleN = "Data Missing";
                        string strDescriptions = "Discount Enter Beetween 0 to 100";
                        NotificationMessage1.NotificationDetails(strTitleN, strDescriptions);
                    }

                }
                else
                {
                    string strTitleN = "Data missing";
                    string strDescriptions = "Select item";
                    NotificationMessage1.NotificationDetails(strTitleN, strDescriptions);
                }
                int intCustomerId = Convert.ToInt32(Request.QueryString["intCustomerId"].ToString());

            }
            else
            {
                string strTitleN = "Data missing";
                string strDescriptions = "Please select customer";
                NotificationMessage1.NotificationDetails(strTitleN, strDescriptions);
            }
        }
        catch (Exception ex)
        {
            string strMessage = ex.Message;
            DALExceptionDetail objDALExceptionDetail = new DALExceptionDetail();
            int intUserId = int.Parse(Session["UserId"].ToString());
            objDALExceptionDetail.InsertRow(strMessage, "CreateOrder2.aspx", intUserId, DateTime.Now, true);
        }

    }

    #endregion

    #region Save (Create Order) Method

    protected void btnSave_Click(object sender, EventArgs e)
    {
        int intUserId = int.Parse(Session["UserId"].ToString());
        int intBranchId = int.Parse(Session["BranchId"].ToString());
        int intQuatationID = 0;

        int intPage = 0;
        int intSupplier = 0;
        if (ddlShippingTo.SelectedIndex == 0)
        {
            intSupplier = 0;
        }
        else
        {
             intSupplier = int.Parse(ddlShippingTo.SelectedValue.ToString());
        }
       
        int intCustId = 0;
        if (intPage == 0 || intPage == 2)
        {
            intCustId = int.Parse(Request.QueryString["intCustomerId"].ToString());
        }
       
        string strQuatationCode = "";
        string strQuatationType = "Admin Order";
        DateTime dtDelivaryDate = DateTime.Now;
        DateTime dtPaymentDueDate = DateTime.Now;
        DateTime dtValidityDate = DateTime.Now;
        string strPAymentTerms = "";
        string strQuatationTitle = "";
        string strbranchaddress = txtAddress.Text;

        if (rdOtherAdrress.Checked == true)
        {
            strbranchaddress = txtAddress.Text;
        }
       
        //if (txtPaymentDueDate.Text != "")
        //{
        if (txtPaymentDueDate.Text != "")
        {
            dtPaymentDueDate = DateTime.ParseExact(txtPaymentDueDate.Text, "MM/dd/yyyy", CultureInfo.CurrentCulture);
        }
        else
        {
           // string dateTime = DateTime.Now.ToString();
            string createddate = Convert.ToDateTime(DateTime.Now.ToString()).ToString("yyyy-MM-dd h:mm tt");
            dtPaymentDueDate = Convert.ToDateTime(createddate);
        }
          
       
            //if (txtValidityDAte.Text != "")
            //{
        if (txtValidityDAte.Text != "")
        {
            dtValidityDate = DateTime.ParseExact(txtValidityDAte.Text, "MM/dd/yyyy", CultureInfo.CurrentCulture); 
           // dtPaymentDueDate = DateTime.ParseExact(txtPaymentDueDate.Text, "MM/dd/yyyy", CultureInfo.CurrentCulture);
        }
        else
        {      
            string createddate = Convert.ToDateTime(DateTime.Now.ToString()).ToString("yyyy-MM-dd h:mm tt");
            dtValidityDate = Convert.ToDateTime(createddate);
            //dtPaymentDueDate = DateTime.ParseExact(dtNow, "MM/dd/yyyy", CultureInfo.CurrentCulture);
        }
            
                if (txtDelivaryDate.Text != "")
                {
                    //if (txtDelivaryDate.Text != "" && txtPaymentDueDate.Text != "" && txtValidityDAte.Text != "")
                    //{
                        
                        dtDelivaryDate =  DateTime.ParseExact(txtDelivaryDate.Text, "MM/dd/yyyy", CultureInfo.CurrentCulture);

                        string onlydate = dtDelivaryDate.ToString("MM/dd/yyyy");
                        string Currenttime  = DateTime.Now.ToString("h:mm:ss tt");
                        string strdatetime = onlydate + " " + Currenttime;

                        dtDelivaryDate = Convert.ToDateTime(strdatetime);

                        if (dtDelivaryDate.Date >= DateTime.Now.Date)
                        {
                            if (btnSave.Text == "Save")
                            {
                                float fTotal = 0;
                                if (lblTotal.Text != "0")
                                {
                                    fTotal = float.Parse(lblPayable.Text);
                                }
                              
                                string strRoll = Session["UserRoll"].ToString();
                                string strCurrentStatus = null;
                                int intEmpId = 0;

                                if (strRoll == "Admin")
                                {
                                    strCurrentStatus = "Pending";
                                }
                                else
                                {
                                    strCurrentStatus = "Pending";
                                }

                                strQuatationType = "Admin Order";
                                if (ddlPaymentMode.SelectedItem != null)
                                {
                                    strPAymentTerms = ddlPaymentMode.SelectedItem.Text;
                                    DALmstOrder objDALQuotationMaster = new DALmstOrder();

                                    intQuatationID = objDALQuotationMaster.InsertRow(intUserId, intSupplier, intCustId, strQuatationType, DateTime.Now, fTotal, strPAymentTerms, "", dtDelivaryDate, dtPaymentDueDate, dtValidityDate, strQuatationCode, true, false, strQuatationTitle, strCurrentStatus, intEmpId, strbranchaddress, "", "", "");
                                    DataTable dtBill = (DataTable)Session["dtBill"];
                                    int nQuanity = 0;
                                    for (int i = 0; i < dtBill.Rows.Count; i++)
                                    {
                                        nQuanity += int.Parse(dtBill.Rows[i]["fQuantity"].ToString());
                                        //bill Id will be added after bill generation so we will got new bill Id
                                        dtBill.Rows[i]["nOrderId"] = intQuatationID.ToString();
                                    }
                                    Session["dtBill"] = dtBill;
                                    DALdtlOrderDetail objDALQuotationDetail = new DALdtlOrderDetail();
                                    objDALQuotationDetail.InsertOrderDetails(dtBill);
                                    int nPoint = dtBill.Rows.Count;

                                    DALloyaltyPoint objLoyaltiPont = new DALloyaltyPoint();
                                    objLoyaltiPont.InsertRow(intUserId, nQuanity, intCustId, intQuatationID, DateTime.Now, "", "", true, false, "", "", "", "", "");

                                    DataTable tempPaymentTerms = (DataTable)Session["tempPaymentTerms"];
                                    DALdtlPaymentTems objDALdtlPaymentTems = new DALdtlPaymentTems();
                                    int rows = tempPaymentTerms.Rows.Count;
                                    for (int i = 0; i < rows; i++)
                                    {
                                        tempPaymentTerms.Rows[i]["nOrderId"] = intQuatationID.ToString();
                                    }
                                    objDALdtlPaymentTems.InsertRow(tempPaymentTerms);
                                    float dcCashback = 0;
                                    if (txtCupCode.Text != "")
                                    {
                                        if (Session["Cashback"].ToString() != "null")
                                        {
                                            dcCashback = float.Parse(Session["Cashback"].ToString());
                                            int nOfferId = int.Parse(lblOfferId.Text.ToString());
                                            DALCustomerWalletDetail objCustomerWalletDetail = new DALCustomerWalletDetail();
                                            objCustomerWalletDetail.InsertRow(intCustId, dcCashback, intQuatationID, nOfferId, DateTime.Now, 0, DateTime.Now, 0, DateTime.Now, intUserId, true, "", "", "", "", "");

                                        }
                                    }

                                    DataTable dtServices = CreateServices();
                                    dtServices = AddServicesTable(dtServices, dtBill);

                                    string strTitleN = "Saved successfully";
                                    string strDescriptions = " ";
                                    NotificationMessage1.NotificationDetails(strTitleN, strDescriptions);
                                 
                                    if (Request.QueryString["page"].ToString() == "0")
                                    {
                                        Response.Redirect("OrderList.aspx?PageId=0&Start=0");
                                    }
                                    else
                                    {
                                        Response.Redirect("OrderList.aspx?PageId=0&Start=0");
                                    }
                                }
                                else
                                {
                                    string strTitleN = "Data Missing";
                                    string strDescriptions = "Select Payment Mode";
                                    NotificationMessage1.NotificationDetails(strTitleN, strDescriptions);
                                }
                            }
                            else
                            {
                                float fTotal = 0;
                                if (lblTotal.Text != "0")
                                {
                                    fTotal = float.Parse(lblPayable.Text);
                                }

                                string strRoll = Session["UserRoll"].ToString();
                                string strCurrentStatus = null;
                                int intEmpId = 0;
                                if (strRoll == "Admin")
                                {
                                    strCurrentStatus = "Pending";
                                }
                                else
                                {
                                    strCurrentStatus = "Pending";
                                }

                                if(lblQuatationType.Text !="")
                                {
                                    strQuatationType = lblQuatationType.Text;
                                }
                                else
                                {
                                    strQuatationType = "Admin Order";
                                }

                                if (ddlPaymentMode.SelectedItem != null)
                                {
                                    strPAymentTerms = ddlPaymentMode.SelectedItem.Text;
                                    DALmstOrder objDALQuotationMaster = new DALmstOrder();

                                    int OrderId = int.Parse(Request.QueryString["OrderId"].ToString());
                                    objDALQuotationMaster.UpdateRow(OrderId,
                                    intUserId, intSupplier, intCustId, strQuatationType, DateTime.Now, fTotal, strPAymentTerms, "", dtDelivaryDate, dtPaymentDueDate, dtValidityDate, strQuatationCode, true, false, strQuatationTitle, strCurrentStatus, intEmpId, strbranchaddress, "", "", "");
                                    DataTable dtBill = (DataTable)Session["dtBill"];

                                    for (int i = 0; i < dtBill.Rows.Count; i++)
                                    {
                                        dtBill.Rows[i]["nOrderId"] = OrderId.ToString();
                                    }
                                    DALdtlOrderDetail objDALdtlOrderDetail = new DALdtlOrderDetail();
                                    DataSet dsDALQuotationDetail = new DataSet();
                                    dsDALQuotationDetail = objDALdtlOrderDetail.SelectAllByOrderId(OrderId, intUserId);
                                    if (dsDALQuotationDetail.Tables[0].Rows.Count > 0)
                                    {
                                        for (int i = 0; i < dtBill.Rows.Count; i++)
                                        {
                                            dtBill.Rows[i]["nOrderId"] = OrderId.ToString();
                                            string str12 = dtBill.Rows[i]["nOrderDetailId"].ToString();

                                            if (dtBill.Rows[i]["nOrderDetailId"].ToString() != "")
                                            {
                                                dtBill.Rows[i]["nOrderDetailId"] = dsDALQuotationDetail.Tables[0].Rows[i]["nOrderDetailId"].ToString();
                                            }
                                            else
                                            {
                                                dtBill.Rows[i]["nOrderDetailId"] = 0;
                                            }
                                        }
                                    }
                                    float dcCashback = 0;
                                    if (txtCupCode.Text != "")
                                    {
                                        if (Session["Cashback"].ToString() != "null")
                                        {
                                            dcCashback = float.Parse(Session["Cashback"].ToString());
                                            int nOfferId = int.Parse(lblOfferId.ToString());
                                            DALCustomerWalletDetail objCustomerWalletDetail = new DALCustomerWalletDetail();
                                            objCustomerWalletDetail.InsertRow(intCustId, dcCashback, OrderId, nOfferId, DateTime.Now, 0, DateTime.Now, 0, DateTime.Now, intUserId, true, "", "", "", "", "");

                                        }
                                    }
                                    for (int itme = 0; itme < dtBill.Rows.Count; itme++)
                                    {
                                        string str12 = dtBill.Rows[itme]["nOrderDetailId"].ToString();

                                        if (dtBill.Rows[itme]["nOrderDetailId"].ToString() == "")
                                        {
                                            dtBill.Rows[itme]["nOrderDetailId"] = 0;
                                        }
                                        int nOrderDetailId = int.Parse(dtBill.Rows[itme]["nOrderDetailId"].ToString());
                                        int nOrderId = int.Parse(dtBill.Rows[itme]["nOrderId"].ToString());
                                        int nProductId = int.Parse(dtBill.Rows[itme]["nProductId"].ToString());

                                        float fProductPrice = float.Parse(dtBill.Rows[itme]["fProductPrice"].ToString());
                                        int nWarrantyMonth = int.Parse(dtBill.Rows[itme]["nWarrantyMonth"].ToString());
                                        string cName = dtBill.Rows[itme]["cName"].ToString();
                                        string cWarrantyType = dtBill.Rows[itme]["cWarrantyType"].ToString();
                                        float fQuantity = float.Parse(dtBill.Rows[itme]["fQuantity"].ToString());
                                        int nQuantityId = int.Parse(dtBill.Rows[itme]["nQuantityId"].ToString());
                                        float fDiscount = float.Parse(dtBill.Rows[itme]["fDiscount"].ToString());
                                        int nCutomerAccountTypeId = 0;
                                        if (dtBill.Rows[itme]["nCutomerAccountTypeId"].ToString() != "" && dtBill.Rows[itme]["nCutomerAccountTypeId"].ToString() !="0")
                                        {
                                            nCutomerAccountTypeId = int.Parse(dtBill.Rows[itme]["nCutomerAccountTypeId"].ToString());
                                        }
                                        if (nOrderDetailId != 0)
                                        {
                                            objDALdtlOrderDetail.UpdateRow(nOrderDetailId, intUserId, nOrderId, nProductId, fProductPrice, nWarrantyMonth, fDiscount,
                                       cName, true, false, fQuantity, nQuantityId, cWarrantyType, DateTime.Now, "", "", "", nCutomerAccountTypeId);
                                        }
                                        else
                                        {
                                            objDALdtlOrderDetail.InsertRow(intUserId, nOrderId, nProductId, fProductPrice, nWarrantyMonth, fDiscount,
                                             cName, true, false, fQuantity, nQuantityId, cWarrantyType, DateTime.Now, "", "", "", nCutomerAccountTypeId);
                                        }
                                    }

                                    Session["dtBill"] = dtBill;

                                    DataTable tempPaymentTerms = (DataTable)Session["tempPaymentTerms"];
                                    DALdtlPaymentTems objDALdtlPaymentTems = new DALdtlPaymentTems();

                                    int rows = tempPaymentTerms.Rows.Count;
                                    for (int i = 0; i < rows; i++)
                                    {
                                        int intId = 0;
                                        intId = Convert.ToInt32(tempPaymentTerms.Rows[i]["nPaymentTermsId"].ToString());

                                        tempPaymentTerms.Rows[i]["nOrderId"] = OrderId.ToString();

                                        string strPaymentTerms = tempPaymentTerms.Rows[i]["cPaymentTerms"].ToString();

                                        if (intId != 0)
                                        {
                                            objDALdtlPaymentTems.UpdateRow(intId, strPaymentTerms, OrderId, intUserId, true);
                                        }
                                        else
                                        {
                                            int id = objDALdtlPaymentTems.InsertRow(strPaymentTerms, OrderId, intUserId, true);
                                        }
                                    }


                                    //objDALdtlPaymentTems.InsertRow(tempPaymentTerms);
                                    string strTitleN = "Update successfully";
                                    string strDescriptions = " ";
                                    NotificationMessage1.NotificationDetails(strTitleN, strDescriptions);
                                   
                                    if (Request.QueryString["page"].ToString() == "0")
                                    {
                                        Response.Redirect("OrderList.aspx?PageId=0&Start=0");
                                    }
                                    else
                                    {
                                        Response.Redirect("OrderList.aspx?PageId=0&Start=0");
                                    }
                                }
                                else {
                                    string strTitleN = "Data Missing";
                                    string strDescriptions = "Select Payment Mode";
                                    NotificationMessage1.NotificationDetails(strTitleN, strDescriptions);
                                }
                            }
                        }
                        else
                        {
                            string strTitleN = "Delivary date is smaller then the current date";
                            string strDescriptions = " ";
                            NotificationMessage1.NotificationDetails(strTitleN, strDescriptions);
                        }
                   // }
                }
                else
                {
                    string strTitleN = "Data Missing";
                    string strDescriptions = "Delivary Date is required";
                    NotificationMessage1.NotificationDetails(strTitleN, strDescriptions);

                }
            //}
            //else
            //{
            //    string strTitleN = "Data Missing";
            //    string strDescriptions = "Validity Date is required";
            //    NotificationMessage1.NotificationDetails(strTitleN, strDescriptions);
            //}
        //}
        //else
        //{
        //    string strTitleN = "Data Missing";
        //    string strDescriptions = "Payment Due Date is required";
        //    NotificationMessage1.NotificationDetails(strTitleN, strDescriptions);
        //}

    }

    #endregion

    #region Add Payment Terms

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            BindTempGrid();
            txtPaymentTems.Text = "";
            txtPaymentTems.Focus();
        }
        catch (Exception ex)
        {
            string strMessage = ex.Message;
            DALExceptionDetail objDALExceptionDetail = new DALExceptionDetail();
            int intUserId = int.Parse(Session["UserId"].ToString());
            objDALExceptionDetail.InsertRow(strMessage, "Quetation.aspx", intUserId, DateTime.Now, true);
        }
    }

    public void BindTempGrid()
    {
        try
        {
            int intUserId = int.Parse(Session["UserId"].ToString());
            //Save data in Temporary Table
            if (txtPaymentTems.Text != "")
            {
                DataTable tempPaymentTerms = (DataTable)Session["tempPaymentTerms"];//new DataTable("PaymentTerms");
                DataRow dr = tempPaymentTerms.NewRow();
           
                dr[0] = 0;
                dr[1] = txtPaymentTems.Text;
                dr[2] = 0;
                dr[3] = intUserId;
                dr[4] = true;

                tempPaymentTerms.Rows.Add(dr);
                dgPaymentTerms.DataSource = tempPaymentTerms;
                dgPaymentTerms.DataBind();
                dgPaymentTerms.Visible = true;
            }
            else
            {
                string strTitleN = "Data missing";
                string strdescriptions = "Enter payment terms";
                NotificationMessage1.NotificationDetails(strTitleN, strdescriptions);
            }
        }
        catch (Exception ex)
        {
            int intUserId = int.Parse(Session["UserId"].ToString());
            DALExceptionDetail objDALExceptionDetail = new DALExceptionDetail();
            objDALExceptionDetail.InsertRow(ex.Message, "CreateOrder.aspx", intUserId, DateTime.Now, true);
        }
    }

    #endregion

    #region Control Event

    //START: Get Price of selected product

    protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        int intUserId = int.Parse(Session["UserId"].ToString());
        try
        {
            if (ddlProduct.SelectedIndex > 0)
            {
                int nProductId = int.Parse(ddlProduct.SelectedValue.ToString());
                DALmstProduct ObjDALProduct = new DALmstProduct();
                DataSet ds = new DataSet();
                ds = ObjDALProduct.SelectRow(nProductId, intUserId);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    int nTaxId = int.Parse(ds.Tables[0].Rows[0]["nTaxId"].ToString());
                    float fPrice = float.Parse(ds.Tables[0].Rows[0]["fMRP"].ToString());

                    if(fPrice > 0)
                    {
                        fPrice = float.Parse(ds.Tables[0].Rows[0]["fBuyPrice"].ToString());
                    }
                  
                    int nUnitType = int.Parse(ds.Tables[0].Rows[0]["nUnitType"].ToString());
                    ddlQuantityType.SelectedValue = nUnitType.ToString();
                    int nAccountTypeId = int.Parse(ds.Tables[0].Rows[0]["nAccountTypeId"].ToString());
                    ddlAllocateTo.SelectedValue = nAccountTypeId.ToString();
                    txtPrice.Text = ds.Tables[0].Rows[0]["fMRP"].ToString();

                    txtPrice.Text = ds.Tables[0].Rows[0]["fMRP"].ToString();
                   // txtPrice.Text = ds.Tables[0].Rows[0]["fMRP"].ToString();
                }
            }

        }
        catch (Exception ex)
        {
            string strMessage = ex.Message;
            DALExceptionDetail objDALExceptionDetail = new DALExceptionDetail();
            objDALExceptionDetail.InsertRow(strMessage, "inventory_Quetation.aspx", intUserId, DateTime.Now, true);
        }
    }

    //END: Get Price of selected product

    //START: Get product id from product code

    protected void txtUniqueProductId_TextChanged(object sender, EventArgs e)
    {
        int intUserId = int.Parse(Session["UserId"].ToString());
        if (txtUniqueProductId.Text != "")
        {
            string strproductCOde = txtUniqueProductId.Text;
            DALmstProduct objProduct = new DALmstProduct();
            DataSet dsProduct = new DataSet();
            dsProduct = objProduct.Select_Product_DetailsByUnicCode(strproductCOde, intUserId);
            if (dsProduct.Tables[0].Rows.Count > 0)
            {
                txtPrice.Text = dsProduct.Tables[0].Rows[0]["fMRP"].ToString();
                Session["txtPrice"] = dsProduct.Tables[0].Rows[0]["fMRP"].ToString();
                Session["ProductId"] = dsProduct.Tables[0].Rows[0]["nProductId"].ToString();
            }
        }
        else
        {

        }
    }

    //END: Get product id from product code

    //START: Apply Cuponcode

    protected void txtCupCode_TextChanged(object sender, EventArgs e)
    {
        int intUserId = int.Parse(Session["UserId"].ToString());
        Session["Cashback"] = "null";
        Session["Discount"] = "null";
        #region Offer add for user

        string strcupNo = "";
        if (txtCupCode.Text != "")
        {
            strcupNo = txtCupCode.Text;
        }

        DALOffer objoffer = new DALOffer();
        DataSet dsOffer = new DataSet();
        dsOffer = objoffer.SelectRowByCuponName(strcupNo, DateTime.Now, intUserId);
        string strAppliedOn = "";

        int intOfferId = 0;
        int intAppliedId = 0;
        float fCuponDiscount = 0;
        float nCashBackPercentageOnTotal = 0;
        float fFixAmount = 0;
        float fPercentage = 0;
        bool IsCashBak = true;
        bool IsPublic = true;
        float fMinTotalCashBack = 0;
        float fMaxTotalCashBack = 0;
        if (dsOffer.Tables[0].Rows.Count > 0)
        {
            int intRepeat = int.Parse(dsOffer.Tables[0].Rows[0]["cRemark1"].ToString());
            IsPublic = bool.Parse(dsOffer.Tables[0].Rows[0]["IsPublic"].ToString());

            if (intRepeat > 0)
            {
                intOfferId = int.Parse(dsOffer.Tables[0].Rows[0]["nOfferId"].ToString());
                strAppliedOn = dsOffer.Tables[0].Rows[0]["cAppliedOn"].ToString();
                intAppliedId = int.Parse(dsOffer.Tables[0].Rows[0]["nAppliedItemId"].ToString());
                fCuponDiscount = float.Parse(dsOffer.Tables[0].Rows[0]["nCashBackPercentageOnTotal"].ToString());

                IsCashBak = bool.Parse(dsOffer.Tables[0].Rows[0]["IsCashBack"].ToString());
                if (IsCashBak == true)
                {
                    nCashBackPercentageOnTotal = float.Parse(dsOffer.Tables[0].Rows[0]["nCashBackPercentageOnTotal"].ToString());
                    fMinTotalCashBack = float.Parse(dsOffer.Tables[0].Rows[0]["fMinTotalCashBack"].ToString());
                    fMaxTotalCashBack = float.Parse(dsOffer.Tables[0].Rows[0]["fMaxTotalCashBack"].ToString());
                }
                else
                {
                    fFixAmount = float.Parse(dsOffer.Tables[0].Rows[0]["fFixAmount"].ToString());
                    fPercentage = float.Parse(dsOffer.Tables[0].Rows[0]["fPercentage"].ToString());
                }
            }
            else
            {
                string strTitleN = "Coupon Already used.....";
                string strDescriptions = "";
                NotificationMessage1.NotificationDetails(strTitleN, strDescriptions);
            }
           
        }

        int nProductId = 0;
        int nProductSubcat = 0;
        int nProductCat = 0;
        DataTable dtBill = (DataTable)Session["dtBill"];

        #region For Offer Apply

        if (dtBill.Rows.Count > 0)
        {
            for (int item = 0; item < dtBill.Rows.Count; item++)
            {
                nProductId = int.Parse(dtBill.Rows[item]["nProductId"].ToString());
              
                DALmstProduct objProductMaster = new DALmstProduct();
                DataSet dsProductMaster = new DataSet();
                dsProductMaster = objProductMaster.SelectRow(nProductId, intUserId);
               
                #region check cupon valid or not

                if (strAppliedOn == "Product")
                {
                    if (intAppliedId == nProductId)
                    {
                        ftotaltemp += float.Parse(dtBill.Rows[item]["fTotal"].ToString());
                        // float fProducttotaltemp = float.Parse(dtBill.Rows[item]["fTotal"].ToString());
                        strOfferValid = "Yes";
                        string strTitleN = "Congratulation.....";
                        string strDescriptions = "Your coupon offer succesfully applied on product....";
                        NotificationMessage1.NotificationDetails(strTitleN, strDescriptions);
                    }
                    else
                    {
                        strOfferValid = "No";
                        string strTitleN = "Sorry..";
                        string strDescriptions = "Your coupon offer expire for product....";
                        NotificationMessage1.NotificationDetails(strTitleN, strDescriptions);
                    }
                }

                if (strAppliedOn == "Product Sub category")
                {


                    if (intAppliedId == nProductSubcat)
                    {
                        ftotaltemp += float.Parse(dtBill.Rows[item]["fTotal"].ToString());
                        // ftotaltemp = ftotaltemp - ((ftotaltemp * fCuponDiscount) / 100);

                        strOfferValid = "Yes";
                        string strTitleN = "Congratulation.....";
                        string strDescriptions = "Your coupon offer succesfully applied on Product Sub category....";
                        NotificationMessage1.NotificationDetails(strTitleN, strDescriptions);
                    }
                    else
                    {
                        strOfferValid = "No";
                        string strTitleN = "Sorry..";
                        string strDescriptions = "Your coupon offer expire....";
                        NotificationMessage1.NotificationDetails(strTitleN, strDescriptions);
                    }
                }

                if (strAppliedOn == "Product Category")
                {


                    if (intAppliedId == nProductCat)
                    {
                        ftotaltemp += float.Parse(dtBill.Rows[item]["fTotal"].ToString());
                        strOfferValid = "Yes";
                        string strTitleN = "Congratulation.....";
                        string strDescriptions = "Your coupon offer succesfully applied on Product Category....";
                        NotificationMessage1.NotificationDetails(strTitleN, strDescriptions);
                    }
                    else
                    {
                        strOfferValid = "No";
                        string strTitleN = "Sorry..";
                        string strDescriptions = "Your coupon offer expire....";
                        NotificationMessage1.NotificationDetails(strTitleN, strDescriptions);
                    }
                }

                #endregion

            }

            if (IsPublic == true)
            {
                #region Cashback calculation

                fTotalProductAmount = ftotaltemp;

                if (IsCashBak == true)
                {
                    
                    CurrentCurrency3.Visible = false;
                    float fcashbackAmount = (ftotaltemp * nCashBackPercentageOnTotal) / 100;

                    ftotaltemp = ftotaltemp - ((ftotaltemp * nCashBackPercentageOnTotal) / 100);
                    if (fMinTotalCashBack <= fcashbackAmount && fMaxTotalCashBack >= fcashbackAmount)
                    {
                        linkCashback.Visible = true;
                        lblCashbackTotal.Visible = true;
                        lblofferDetail.Text = "CASHBACK DETAIL";
                        lblCashbackDetail.Text = "Your cashback is: <b>" + ftotaltemp.ToString() + "</b>";
                        lblTotalCashbackDetail.Text = fTotalProductAmount.ToString();
                        Session["Cashback"] = ftotaltemp.ToString();
                        lblCashbackTotal.Visible = true;
                        lblCashbackTotal.Text= ftotaltemp.ToString();
                        lblOfferId.Text = intOfferId.ToString();
                        lblDiscount.Text = "% 0";
                        lblPayable.Text = lblTotal.Text;
                    }
                    else if (fMinTotalCashBack > fcashbackAmount)
                    {
                        linkCashback.Visible = true;
                        lblCashbackTotal.Visible = true;
                        lblCashbackDetail.Text = "Your cashback is: <b>" + ftotaltemp.ToString() + "</b> mininum so cant apply ";
                        lblCashbackTotal.Text = ftotaltemp.ToString() + " Mininum";
                        lblDiscount.Text = "% 0";
                        lblPayable.Text = lblTotal.Text;
                    }
                    else if (fMaxTotalCashBack < fcashbackAmount)
                    {
                        linkCashback.Visible = true;
                        lblCashbackTotal.Visible = true;
                        lblCashbackDetail.Text = "Your cashback is: <b>" + ftotaltemp.ToString() + "</b> maximum so cant apply ";
                        lblCashbackTotal.Text = ftotaltemp.ToString() + " Maximum";
                        lblDiscount.Text = "% 0";
                        lblPayable.Text = lblTotal.Text;
                    }
                    if (fTotalProductAmount <= 0)
                    {
                        lblCashbackDetail.Text = "This offer can't apply";
                        
                        Session["Cashback"] = "null";

                        string strTitleN = "This offer can't apply";
                        string strDescriptions = "";
                        NotificationMessage1.NotificationDetails(strTitleN, strDescriptions);


                    }
                }
                else
                {
                    linkCashback.Visible = false;
                    lblCashbackTotal.Visible = false;
                    if (fFixAmount != 0)
                    {
                        lblofferDetail.Text = "DISCOUNT DETAIL";
                        ftotaltemp = ftotaltemp - fFixAmount;
                        lblTotalCashbackDetail.Text = fTotalProductAmount.ToString();
                        lblCashbackDetail.Text = "Your Discount is: <b>" + fFixAmount.ToString() + "Rs</b> Now your total is: <b>" + ftotaltemp.ToString() + "</b>";
                        Session["Discount"] = ftotaltemp.ToString();
                        CurrentCurrency3.Visible = true;
                        lblPayable.Text= ftotaltemp.ToString();
                        lblDiscount.Text = fFixAmount.ToString();

                        lblOfferId.Text = intOfferId.ToString();
                    }
                    else if (fPercentage != 0)
                    {
                        lblofferDetail.Text = "DISCOUNT DETAIL";
                        float fdiscountAmount = (ftotaltemp * fFixAmount) / 100;

                        ftotaltemp = ftotaltemp - ((ftotaltemp * fFixAmount) / 100);
                        lblTotalCashbackDetail.Text = fTotalProductAmount.ToString();
                        lblCashbackDetail.Text = "Your Discount is: <b>" + fdiscountAmount.ToString() + "% </b> Now your total is: <b>" + ftotaltemp.ToString() + "</b>";
                        Session["Discount"] = ftotaltemp.ToString();
                        lblOfferId.Text = intOfferId.ToString();
                        lblPayable.Text = ftotaltemp.ToString();
                        CurrentCurrency3.Visible = false;
                        lblDiscount.Text = "%" + fdiscountAmount.ToString();
                    }
                    if (ftotaltemp <= 0)
                    {
                        lblCashbackDetail.Text = "This offer can't apply";
                        Session["Discount"] = "null";
                    }
                }

                #endregion
            }
            else
            {
                lblCashbackDetail.Text = "This offer not valid for you";
            }
        }

        #endregion

        #endregion
    }

    //END: Apply Cuponcode

    #endregion


    #region Payment Terms Grid Event

    protected void dgPaymentTerms_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            int intUserId = int.Parse(Session["UserId"].ToString());
            DataTable tempPaymentTerms = (DataTable)Session["tempPaymentTerms"];

            tempPaymentTerms.Rows[e.Item.ItemIndex].Delete();
            tempPaymentTerms.AcceptChanges();

            if (btnSave.Text == "Update")
            {
                Label lblId = (Label)e.Item.Cells[0].FindControl("dglblOrderPaymentTermsId");

                int Id = 0;

                Id = Convert.ToInt32(lblId.Text);

                if (Id > 0)
                {
                    DALdtlPurchasePaymentTerms objDALPurchasePaymentTerms = new DALdtlPurchasePaymentTerms();

                    objDALPurchasePaymentTerms.DeleteRow(Id, intUserId);
                }

            }
            if (tempPaymentTerms.Rows.Count > 0)
            {
                dgPaymentTerms.DataSource = tempPaymentTerms;
                dgPaymentTerms.DataBind();
            }
            else
            {
                dgPaymentTerms.Visible = false;
            }
        }
        catch (Exception ex)
        {
            string strMessage = ex.Message;
            DALExceptionDetail objDALExceptionDetail = new DALExceptionDetail();
            int intUserId = int.Parse(Session["UserId"].ToString());
            objDALExceptionDetail.InsertRow(strMessage, "CreateOrder.aspx", intUserId, DateTime.Now, true);
        }
    }

    #endregion


    #region Update Order

    public void EditQuetationmaster()
    {
        try
        {
            if (Request.QueryString["OrderId"].ToString() != "")
            {
                int id = int.Parse(Request.QueryString["OrderId"].ToString());

                if (id != 0)
                {
                    btnSave.Text = "Update";
                   
                    divtotal_div.Visible = true;
                    tr1.Visible = true;
                    tr2.Visible = true;
                  
                    trSave.Visible = true;
                 
                    int intUserId = int.Parse(Session["UserId"].ToString());


                    int intBranchId = int.Parse(Session["BranchId"].ToString());

                    DALmstOrder objDALQuotationMaster = new DALmstOrder();
                    DataSet ds = new DataSet();
                    ds = objDALQuotationMaster.SelectRow(id, intUserId);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        int intCustomerID = int.Parse(ds.Tables[0].Rows[0]["nCustomerId"].ToString());

                        if (intCustomerID != 0)
                        {
                            Session["CustomerID"] = intCustomerID;
                           
                            BindCustomerDetail(intCustomerID);
                        }
                     
                        txtDelivaryDate.Text =Convert.ToDateTime(ds.Tables[0].Rows[0]["dtDeliveryDate"].ToString()).ToString("MM/dd/yyyy");

                        txtPaymentDueDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["dtPaymentDueDate"].ToString()).ToString("MM/dd/yyyy");
                        BindPaymentterms(intUserId, id);

                        txtPaymentTems.Text = ds.Tables[0].Rows[0]["cPaymentTerms"].ToString();

                        txtPrice.Visible = true;
                        txtQuatationType.Text = ds.Tables[0].Rows[0]["cOrderType"].ToString();
                        lblQuatationType.Text= ds.Tables[0].Rows[0]["cOrderType"].ToString();

                        lblTotal.Text = ds.Tables[0].Rows[0]["fTotalPrice"].ToString();
                      
                        txtValidityDAte.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["dtValidityDate"].ToString()).ToString("MM/dd/yyyy");
                        txtQuetationTitle.Text = ds.Tables[0].Rows[0]["cOrderTitle"].ToString();

                        string strPaymentTerms = "";

                        strPaymentTerms = ds.Tables[0].Rows[0]["cPaymentTerms"].ToString();

                        txtAddress.Text= ds.Tables[0].Rows[0]["cAddress"].ToString();

                        // ddlPaymentMode.SelectedValue = strPaymentTerms;

                        ddlPaymentMode.ClearSelection();
                        ddlPaymentMode.SelectedIndex = ddlPaymentMode.Items.IndexOf(ddlPaymentMode.Items.FindByText(strPaymentTerms));

                        DALdtlOrderDetail objDALQuotationDetail = new DALdtlOrderDetail();
                        DataSet dsDALQuotationDetail = new DataSet();
                        dsDALQuotationDetail = objDALQuotationDetail.SelectAllByOrderId(id, intUserId);
                        if (dsDALQuotationDetail.Tables[0].Rows.Count > 0)
                        {
                            grvTempBill.Visible = true;

                            grvTempBill.DataSource = dsDALQuotationDetail.Tables[0];
                            grvTempBill.DataBind();

                            float fTotal = 0;
                            float fBillAmt = 0;
                            float fDiscount = 0;
                            float fTotalDiscount = 0;
                            for (int rowId = 0; rowId < grvTempBill.Rows.Count; rowId++)
                            {
                                Label lblfTotal = grvTempBill.Rows[rowId].FindControl("fTotal") as Label;
                                Label dglblDiscount = grvTempBill.Rows[rowId].FindControl("dglblDiscount") as Label;
                                fBillAmt += float.Parse(lblfTotal.Text.ToString());
                                fDiscount += float.Parse(dglblDiscount.Text.ToString());
                            }
                            if (fDiscount == 0)
                            {
                                divCupon.Visible = true;
                            }
                            else
                            {
                                divCupon.Visible = false;
                            }
                            fTotal = fTotal + fBillAmt;

                            lblDiscount.Visible = true;

                            fTotalDiscount = fTotalDiscount + fDiscount;
                            lblDiscount.Text = "%" + fTotalDiscount.ToString();
                            lblTotal.Visible = true;
                            lblTotal.Text = Math.Round(fTotal,2).ToString();

                            float fPayable = fTotal - ((fTotal * fTotalDiscount) / 100);
                            lblPayable.Visible = true;
                            lblPayable.Text = Math.Round(fTotal, 2).ToString();

                            if (grvTempBill.Rows.Count > 0)
                            {
                                btnSave.Enabled = true;
                            }
                            else
                            {
                                btnSave.Enabled = false;
                            }
                            Session["dtBill"] = dsDALQuotationDetail.Tables[0];
                        }
                      
                        lblTotal.Visible = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            string strMessage = ex.Message;
            int intUserId = int.Parse(Session["UserId"].ToString());
            DALExceptionDetail objDALExceptionDetail = new DALExceptionDetail();
            objDALExceptionDetail.InsertRow(strMessage, "Order_CreateOrder.aspx", intUserId, DateTime.Now, true);
        }
    }

    public void BindPaymentterms(int intUserId, int intQuotationId)
    {
        try
        {
            DALdtlPaymentTems objDALdtlPaymentTems = new DALdtlPaymentTems();
            DataSet dsPaymentterms = objDALdtlPaymentTems.SelectRow(intQuotationId, intUserId);
            if (dsPaymentterms.Tables[0].Rows.Count > 0)
            {
                dgPaymentTerms.Visible = true;
                dgPaymentTerms.DataSource = dsPaymentterms.Tables[0];
                dgPaymentTerms.DataBind();
                Session["tempPaymentTerms"]= dsPaymentterms.Tables[0];
            }
            else
            {
                dgPaymentTerms.Visible = false;
            }
        }
        catch (Exception ex)
        {
            string strMessage = ex.Message;
            DALExceptionDetail objDALExceptionDetail = new DALExceptionDetail();
            objDALExceptionDetail.InsertRow(strMessage, "inventory_UpdateQuetationData.aspx", intUserId, DateTime.Now, true);
        }
    }

    #endregion

    #region Bind Payment Mode 


    private void BindList()
    {
        int intUserId = int.Parse(Session["UserId"].ToString());
        DALPaymentMode objPaymentMode = new DALPaymentMode();
        DataSet dsPaymentMode = new DataSet();
        dsPaymentMode = objPaymentMode.SelectAll(intUserId);
        if (dsPaymentMode.Tables[0].Rows.Count > 0)
        {
            ddlPaymentMode.DataSource = dsPaymentMode.Tables[0];
            ddlPaymentMode.DataTextField = "cPaymentModeName";
            ddlPaymentMode.DataValueField = "cPaymentModeId";
            ddlPaymentMode.DataBind();
        }
     
    }

    #endregion

    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Order/OrderList.aspx?PageId=0&Start=0");
    }

    public void BindTableGrid()
    {

        int intUserId = int.Parse(Session["UserId"].ToString());
        DataTable dtBill = (DataTable)Session["dtBill"];
        grvTempBill.Visible = true;
        grvTempBill.DataSource = dtBill;
        grvTempBill.DataBind();

        if(grvTempBill.Rows.Count > 0)
        {
            divtotal_div.Visible = true;
            Session["dtBill"] = dtBill;
            float fTotal = 0;
            float fBillAmt = 0;
            float fDiscount = 0;
            float fTotalDiscount = 0;
            float GSTTAX = 0;
            float BillAmt = 0;
            float Discount = 0;
            float TotalGST = 0;
            float fMRP = 0;
            float GSTTAXOfUnitPrice = 0;
            float fQuantity = 0;
           
            for (int rowId = 0; rowId < dtBill.Rows.Count; rowId++)
            {
                fBillAmt += float.Parse(dtBill.Rows[rowId]["fTotal"].ToString());
                fDiscount += float.Parse(dtBill.Rows[rowId]["fDiscount"].ToString());


                BillAmt = float.Parse(dtBill.Rows[rowId]["fTotal"].ToString());
                fQuantity = float.Parse(dtBill.Rows[rowId]["fQuantity"].ToString());
                Discount = float.Parse(dtBill.Rows[rowId]["fDiscount"].ToString());
                int TaxId = int.Parse(dtBill.Rows[rowId]["nTaxId"].ToString());

                fMRP = float.Parse(dtBill.Rows[rowId]["fMRP"].ToString());

                DALmstTax objDALmstTax = new DALmstTax();
                DataSet dsobjDALmstTax = new DataSet();
                dsobjDALmstTax = objDALmstTax.SelectRow(TaxId, intUserId);
                if (dsobjDALmstTax.Tables[0].Rows.Count > 0)
                {
                    for (int gst = 0; gst < dsobjDALmstTax.Tables[0].Rows.Count; gst++)
                    {
                        float fTaxPercentage = float.Parse(dsobjDALmstTax.Tables[0].Rows[gst]["fTaxPercentage"].ToString());

                        GSTTAX = (fMRP * fQuantity) * fTaxPercentage / 100;
                        GSTTAXOfUnitPrice = fMRP * fTaxPercentage / 100;
                        // lblGSTTotalForUnitPrice.Text = GSTTAXOfUnitPrice.ToString();
                        TotalGST += GSTTAX;
                        // lblGST.Text = string.Format("{0:0.00}", TotalGST);
                        Label GST = (Label)grvTempBill.Rows[rowId].FindControl("GST");
                        Label lblGSTTotalOfUnitPriceExcluding = (Label)grvTempBill.Rows[rowId].FindControl("lblGSTTotalOfUnitPriceExcluding");
                        //GST.Text = string.Format("{0:0.00}", GSTTAX);

                    }
                }
            }
            if (fDiscount == 0)
            {
                divCupon.Visible = true;
            }
            else
            {
                divCupon.Visible = false;
            }
            fTotal = fTotal + fBillAmt + TotalGST;
            float fTotal1 = 0;
            fTotal1 += fBillAmt;

            lblDiscount.Visible = true;

            fTotalDiscount = fTotalDiscount + fDiscount;
            lblDiscount.Text = "%" + string.Format("{0:0.00}", fTotalDiscount);
            lblTotal.Visible = true;
            lblTotal.Text = string.Format("{0:0.00}", fTotal);

            float fPayable = fTotal;
            lblPayable.Visible = true;
            lblPayable.Text = string.Format("{0:0.00}", fPayable);
            tr1.Visible = true;
            tr2.Visible = true;
            divTotal.Visible = true;
            trSave.Visible = true;
            trSave.Visible = true;
            btnSave.Enabled = true;

        }
        else
        {
            btnSave.Enabled = false;
        }
    }

    public void BindUpdateGrid()
    {
        int intUserId = int.Parse(Session["UserId"].ToString());
        DataTable dtBill = (DataTable)Session["dtBill"];
        grvTempBill.DataSource = dtBill;
        grvTempBill.DataBind();
        divtotal_div.Visible = true;
        Session["dtBill"] = dtBill;
        float fTotal = 0;
        float fBillAmt = 0;
        float fDiscount = 0;
        float fTotalDiscount = 0;
        float GSTTAX = 0;
        float BillAmt = 0;
        float Discount = 0;
        float TotalGST = 0;
        float fQuantity = 0;
        float fMRP = 0;
        for (int rowId = 0; rowId < dtBill.Rows.Count; rowId++)
        {
            fBillAmt += float.Parse(dtBill.Rows[rowId]["fTotal"].ToString());
            fDiscount += float.Parse(dtBill.Rows[rowId]["fDiscount"].ToString());

            BillAmt = float.Parse(dtBill.Rows[rowId]["fTotal"].ToString());
            fMRP = float.Parse(dtBill.Rows[rowId]["fMRP"].ToString());
            fQuantity = float.Parse(dtBill.Rows[rowId]["fQuantity"].ToString());
            Discount = float.Parse(dtBill.Rows[rowId]["fDiscount"].ToString());
            int TaxId = int.Parse(dtBill.Rows[rowId]["nTaxId"].ToString());
            DALmstTax objDALmstTax = new DALmstTax();
            DataSet dsobjDALmstTax = new DataSet();
            dsobjDALmstTax = objDALmstTax.SelectRow(TaxId, intUserId);
            if (dsobjDALmstTax.Tables[0].Rows.Count > 0)
            {
                for (int gst = 0; gst < dsobjDALmstTax.Tables[0].Rows.Count; gst++)
                {
                    float fTaxPercentage = float.Parse(dsobjDALmstTax.Tables[0].Rows[gst]["fTaxPercentage"].ToString());
                    GSTTAX = (fMRP * fQuantity) * fTaxPercentage / 100;
                    // GSTTAX = BillAmt * fTaxPercentage / 100;
                    TotalGST += GSTTAX;
                   // lblGST.Text = string.Format("{0:0.00}", TotalGST);
                   // Label GST = (Label)grvTempBill.Rows[rowId].FindControl("GST");
                  //  GST.Text = string.Format("{0:0.00}", GSTTAX);
                    //lblsubTotal.Text = GridIncludivTotal.ToString();
                  
                }
            }
        }
        if (fDiscount == 0)
        {
            divCupon.Visible = true;
        }
        else
        {
            divCupon.Visible = false;
        }
        fTotal = fTotal + fBillAmt + TotalGST;
        float fTotal1 = 0;
        fTotal1 = fBillAmt;

        lblTotal.Visible = true;
        lblTotal.Text = string.Format("{0:0.00}", fTotal);
        lblDiscount.Visible = true;

        fTotalDiscount = fTotalDiscount + fDiscount;
        lblDiscount.Text = "%" + string.Format("{0:0.00}", fTotalDiscount);
        //  float fPayable = fTotal - ((fTotal * fTotalDiscount) / 100);
        float fPayable = fTotal;
        lblPayable.Visible = true;
        lblPayable.Text = string.Format("{0:0.00}", fPayable);
        tr1.Visible = true;
        tr2.Visible = true;
        btnSave.Enabled = true;

        trSave.Visible = true;
        divTotal.Visible = true;
        trSave.Visible = true;
        lblName.Text = "Add Items";
    }
}