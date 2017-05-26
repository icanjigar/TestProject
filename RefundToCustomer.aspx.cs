using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Order_RefundToCustomer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            BindRefundData();
            BindCustomerAccountType();
            ddlCutomerAccounttype.SelectedIndex = 1;
            GenerateReferenceNumber();
            txtPaymentDueDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
        }
       
    }


    private void GenerateReferenceNumber()
    {
        int intUserId = int.Parse(Session["UserId"].ToString());
        DALmstRefundAmount objDALmstRefundAmount = new DALmstRefundAmount();
        DataSet dsobjDALmstRefundAmount = new DataSet();

        dsobjDALmstRefundAmount = objDALmstRefundAmount.SelectAll(intUserId);
        if (dsobjDALmstRefundAmount.Tables[0].Rows.Count > 0)
        {
            int Rowcnt = dsobjDALmstRefundAmount.Tables[0].Rows.Count;
            Rowcnt = Rowcnt - 1;
            string nRefundAmountId = dsobjDALmstRefundAmount.Tables[0].Rows[Rowcnt]["nRefundAmountId"].ToString();
            int IncrementNumber = Convert.ToInt32(nRefundAmountId) + 1;
            string finalstring = "CF" + IncrementNumber;
            // string finalstring = IncrementID(startValue);
            txtReferencenumber.Text = finalstring;


        }
        else
        {
            txtReferencenumber.Text = "CF" + 1;
        }
    }
    



    private void BindRefundData()
    {
        float fTotal = 0;
        float TotalAmountDue = 0;
        int intUserId = int.Parse(Session["UserId"].ToString());
        int nOrderId = int.Parse(Request.QueryString["OrderId"].ToString());
        DALmstOrder objDALmstOrder = new DALmstOrder();
        DataSet dsDALmstOrder = new DataSet();
        dsDALmstOrder = objDALmstOrder.SelectRowForReturnToInvoice(intUserId, nOrderId);
        if (dsDALmstOrder.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dsDALmstOrder.Tables[0].Rows.Count; i++)
            {
                string cOrderTitle = dsDALmstOrder.Tables[0].Rows[i]["cOrderCode"].ToString();
                cOrderTitle = "Credit From " + cOrderTitle;
                txtNotes.Text =  cOrderTitle;
                string cCustomerName = dsDALmstOrder.Tables[0].Rows[i]["cCustomerName"].ToString();
                int nCustomerId = int.Parse(dsDALmstOrder.Tables[0].Rows[i]["nCustomerId"].ToString());

                lblId.Text = nCustomerId.ToString();
                lblCustomerName.Text = cCustomerName;



                float fTotalPriceNew = float.Parse(dsDALmstOrder.Tables[0].Rows[i]["fTotalPrice"].ToString());
               

                DALmstRefundAmount objDALmstRefundAmount = new DALmstRefundAmount();
                DataSet dsobjDALmstRefundAmount = new DataSet();

                dsobjDALmstRefundAmount = objDALmstRefundAmount.SelectDataForApplyToInvoices(intUserId, nOrderId);
                if (dsobjDALmstRefundAmount.Tables[0].Rows.Count > 0)
                {
                    for (int Total = 0; Total < dsobjDALmstRefundAmount.Tables[0].Rows.Count; Total++)
                    {
                        float fRefundAmount = float.Parse(dsobjDALmstRefundAmount.Tables[0].Rows[Total]["fRefundAmount"].ToString());
                        fTotal += fRefundAmount;
                    }
                    TotalAmountDue = fTotalPriceNew - fTotal;
                    txtRefundAmount.Text = TotalAmountDue.ToString();
                    lblTotal.Text = txtRefundAmount.Text;
                }
                else
                {
                    txtRefundAmount.Text = fTotalPriceNew.ToString();
                    lblTotal.Text = txtRefundAmount.Text;
                }
               


               /* string cCurrentStatus = "Pending";
                DataSet ds = new DataSet();
                ds = objDALmstOrder.SelectTotalAmountRecive(intUserId, nCustomerId, cCurrentStatus);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int Total = 0; Total < ds.Tables[0].Rows.Count; Total++)
                    {
                        float FtotalAmountRecived = float.Parse(ds.Tables[0].Rows[Total]["fPaymentAmount"].ToString());
                        fTotal += FtotalAmountRecived;

                    }
                    TotalAmountDue = fTotalPriceNew - fTotal;
                    txtRefundAmount.Text = TotalAmountDue.ToString();
                }
                else
                {
                    txtRefundAmount.Text = fTotalPriceNew.ToString();
                }*/


              /*  string fAmountDue = dsDALmstOrder.Tables[0].Rows[i]["fAmountDue"].ToString();
                if (fAmountDue == "")
                {
                    float fTotalPriceNew = float.Parse(dsDALmstOrder.Tables[0].Rows[i]["fTotalPrice"].ToString());
                    txtRefundAmount.Text = fTotalPriceNew.ToString();
                }
                else
                {
                    float fTotalPriceNew = float.Parse(dsDALmstOrder.Tables[0].Rows[i]["fTotalPrice"].ToString());
                    float AmountDue = float.Parse(dsDALmstOrder.Tables[0].Rows[i]["fAmountDue"].ToString());
                   
                    txtRefundAmount.Text = AmountDue.ToString();
                }*/


            }

        }
        else
        {

        }
        
       
    }

    static string IncrementID(string startValue)
    {
        char letter = startValue[0];
        char seletter = startValue[1];
        string str = letter + "" + seletter;
        int len = startValue.Length - 2;
        //String.Format("{0}{1:D}",letter);
        int number = int.Parse(startValue.Substring(2));
        number++;
        if (number >= Math.Pow(10, len)) number = 1; // start again at 1
        return String.Format("{0}{1:D" + len.ToString() + "}", str, number);
    }


    public void BindCustomerAccountType()
    {
        int intUserId = int.Parse(Session["UserId"].ToString());
        try
        {
            DALCutomerAccountType objCustomerAccountType = new DALCutomerAccountType();
            DataSet dsCustomerAccountType = new DataSet();
            dsCustomerAccountType = objCustomerAccountType.SelectAll(intUserId);

            if (dsCustomerAccountType.Tables[0].Rows.Count > 0)
            {
                ddlCutomerAccounttype.DataSource = dsCustomerAccountType.Tables[0];
                ddlCutomerAccounttype.DataTextField = "cAccountType";
                ddlCutomerAccounttype.DataValueField = "nCutomerAccountTypeId";
                ddlCutomerAccounttype.DataBind();
                ddlCutomerAccounttype.Items.Insert(0, "Customer Account type");
            }
            else
            {
                
            }
            //int intLastItem = int.Parse(ddlCustomer.Items.Count.ToString());
            //ddlCustomer.Items.Insert(intLastItem, "Add Customer");
        }
        catch (Exception ex)
        {
            DALExceptionDetail objDALExceptionDetail = new DALExceptionDetail();
            objDALExceptionDetail.InsertRow(ex.Message, "RefundToCustomer.aspx", intUserId, DateTime.Now, true);
        }
    }

    protected void btnCancle_Click(object sender, EventArgs e)
    {
        Response.Redirect("ViewInvoice.aspx?PageId=0");
    }
    protected void btnSaveProduct_Click(object sender, EventArgs e)
    {
        int intUserId = int.Parse(Session["UserId"].ToString());
        try
        {
            DALmstOrder objDALmstOrder = new DALmstOrder();
            DALmstRefundAmount objDALmstRefundAmount = new DALmstRefundAmount();
            DataSet dsobjDALmstRefundAmount = new DataSet();
     
            if (txtRefundAmount.Text != "0")
            {
              
                int nOrderId = int.Parse(Request.QueryString["OrderId"].ToString());
                int nCustomerId = int.Parse(lblId.Text);
                int nLanguageId = 1;
                DateTime dtPaymentReceived = DateTime.Parse(txtPaymentDueDate.Text);
                float fRefundAmount = float.Parse(txtRefundAmount.Text);
                int nCutomerAccountTypeId = int.Parse(ddlCutomerAccounttype.SelectedValue);
                string cReferenceNumber = txtReferencenumber.Text;
                string cNotes = txtNotes.Text;

                float TotalAmount=float.Parse(lblTotal.Text);
                if (fRefundAmount == TotalAmount)
                {
                    objDALmstOrder.UpdateStatusAfterTakePayment(nOrderId, intUserId, "Return", dtPaymentReceived);
                 

                    objDALmstRefundAmount.InsertRow(nOrderId, nCustomerId, dtPaymentReceived, fRefundAmount, nCutomerAccountTypeId, cReferenceNumber, cNotes, intUserId, true, nLanguageId, false, "", "", "", "");

                    #region //START: Enter Log


                    string strchatUserNAme = Session["ChatUsername"].ToString();

                    string strCustomerName = "";

                    DALCustomer objCustomer = new DALCustomer();
                    DataSet dsCustomer = new DataSet();
                    dsCustomer = objCustomer.SelectRow(nCustomerId, intUserId);
                    if (dsCustomer.Tables[0].Rows.Count > 0)
                    {
                        strCustomerName = dsCustomer.Tables[0].Rows[0]["cCustomerFirstName"].ToString() + " " + dsCustomer.Tables[0].Rows[0]["cCustomerLastName"].ToString();
                    }

                    string strDescription = "Refund payment for 'CN'" + nOrderId + "(" + strCustomerName + ")";
                    DALmstLog objLog = new DALmstLog();
                    objLog.InsertRow(cReferenceNumber, strDescription, "insert", strchatUserNAme, DateTime.Now, intUserId, 0, true, false, "", "", "");

                    strDescription = "Full Refund payment for 'CN'" + nOrderId + "(" + strCustomerName + ")";

                    objLog.InsertRow(cReferenceNumber, strDescription, "insert", strchatUserNAme, DateTime.Now, intUserId, 0, true, false, "", "", "");

                    #endregion

                    Response.Redirect("ViewInvoice.aspx?PageId=0");
                }
                else if (fRefundAmount>TotalAmount)
                {
                    string strTitleN = "Take Payment";
                    string strdescriptions = "The amount cannot be more than the credit available to the customer.";
                    NotificationMessage1.NotificationDetails(strTitleN, strdescriptions);
                }
                else
                {

                    objDALmstRefundAmount.InsertRow(nOrderId, nCustomerId, dtPaymentReceived, fRefundAmount, nCutomerAccountTypeId, cReferenceNumber, cNotes, intUserId, true, nLanguageId, false, "", "", "", "");

                    #region //START: Enter Log


                    string strchatUserNAme = Session["ChatUsername"].ToString();

                    string strCustomerName = "";

                    DALCustomer objCustomer = new DALCustomer();
                    DataSet dsCustomer = new DataSet();
                    dsCustomer = objCustomer.SelectRow(nCustomerId, intUserId);
                    if (dsCustomer.Tables[0].Rows.Count > 0)
                    {
                        strCustomerName = dsCustomer.Tables[0].Rows[0]["cCustomerFirstName"].ToString() + " " + dsCustomer.Tables[0].Rows[0]["cCustomerLastName"].ToString();
                    }

                    string strDescription = "Refund amount for 'CN'" + nOrderId + "(" + strCustomerName + ")";
                    DALmstLog objLog = new DALmstLog();
                    objLog.InsertRow(cReferenceNumber, strDescription, "insert", strchatUserNAme, DateTime.Now, intUserId, 0, true, false, "", "", "");


                    #endregion

                    Response.Redirect("ViewInvoice.aspx?PageId=0");
                }
             }
         

            else
            {
                string strTitleN = "";
                string strdescriptions = "The Amount field cannot be $0.00. Please enter an amount other than zero.";
                NotificationMessage1.NotificationDetails(strTitleN, strdescriptions);
            }

            

        }
        catch (Exception ex)
        {
            DALExceptionDetail objDALExceptionDetail = new DALExceptionDetail();
            objDALExceptionDetail.InsertRow(ex.Message, "RefundToCustomer.aspx", intUserId, DateTime.Now, true);
        }
    }

    protected void txtRefundAmount_TextChanged(object sender, EventArgs e)
    {

        if (txtRefundAmount.Text != "" && txtRefundAmount.Text != "0")
        {
            float PaymentAmount = float.Parse(txtRefundAmount.Text);
        }
    }
}