using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Appointment_AddAppoinment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            BindAllAppoinments();
        }
        
    }

    private void BindAllAppoinments()
    {
        int nUserId = int.Parse(Session["UserId"].ToString());
        DALmstAppointmentManagement objDALmstAppointmentManagement = new DALmstAppointmentManagement();
        DataSet dsDALmstAppointmentManagement = new DataSet();
        dsDALmstAppointmentManagement = objDALmstAppointmentManagement.SelectAllAppoinment(nUserId);
        if (dsDALmstAppointmentManagement.Tables[0].Rows.Count > 0)
        {
            dgAppoinments.DataSource = dsDALmstAppointmentManagement;
            dgAppoinments.DataBind();
        }
        else
        {
            lblmsg.Visible = true;
            lblmsg.Text = "No Any Appoinmet";
        }
    }
    protected void dgAppoinments_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            Label lblnAppointmentId = (Label)e.Item.FindControl("lblnAppointmentId");
            DALmstAppointmentManagement objDALmstAppointmentManagement = new DALmstAppointmentManagement();
            int intId = int.Parse(lblnAppointmentId.Text);
            int intUserId = int.Parse(Session["UserId"].ToString());
            objDALmstAppointmentManagement.DeleteRow(intId, intUserId);
            BindAllAppoinments();
        }
        catch (Exception ex)
        {
            string strMessage = ex.Message;
            int intUserId = int.Parse(Session["UserId"].ToString());
            DALExceptionDetail objDALExceptionDetail = new DALExceptionDetail();
            objDALExceptionDetail.InsertRow(strMessage, "Appointment.aspx", intUserId, DateTime.Now, true);
        }
    }

    
  

    public DateTime GetDateTime(string strDate, int intHours, string strMinutes, string strTimezone)
    {

        DateTime dtFinalTime;
        string strFinalString = "";
        if (strTimezone == "PM" && intHours!=12)
        {
            intHours = intHours + 12;
        }

        strFinalString = Convert.ToDateTime(strDate + " " + intHours + ":" + strMinutes + ":00").ToString("MM/dd/yyyy hh:mm:ss tt");
        dtFinalTime = Convert.ToDateTime(strFinalString.ToString());
        DateTime dtFinalTime1 = dtFinalTime.AddHours(-5);
        DateTime dtFinalTime2 = dtFinalTime1.AddMinutes(-30);
        // txtLogoutDate.Text = dtFinalTime.ToString();
        return dtFinalTime2;

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
          Button btn = (Button)sender;
          DataGridItem item = (DataGridItem)btn.Parent.Parent.Parent;
       

          Label lblnAppointmentId = (Label)item.FindControl("lblnAppointmentId");
         int nAppointmentId = int.Parse(lblnAppointmentId.Text);
           TextBox txtStartDate = (TextBox)item.FindControl("txtStartDate");
            TextBox txtTimeSDHH1 = (TextBox)item.FindControl("txtTimeSDHH1");
            TextBox txtTimeSDMM1 = (TextBox)item.FindControl("txtTimeSDMM1");
            DropDownList ddlSD1 = (DropDownList)item.FindControl("ddlSD1");

            Label lblhhh = (Label)item.FindControl("lblhhh");
            Label lblmmm = (Label)item.FindControl("lblmmm");


            if (txtStartDate.Text != "" && txtTimeSDHH1.Text != "" && txtTimeSDMM1.Text != "" && ddlSD1.SelectedItem.Text != "")
            {

                int intHH2 = int.Parse(txtTimeSDHH1.Text.ToString());
                if (intHH2 <= 12)
                {

                    int intMM1 = int.Parse(txtTimeSDMM1.Text.ToString());
                    if (intMM1 < 60)
                    {

                        TimeZoneInfo timeZoneInfo;

                        DateTime dateTimeStartTime;

                        DateTime dtStartTime = Convert.ToDateTime(txtStartDate.Text.ToString());

                        DateTime tdTodayDate = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy"));


                        int intHours = int.Parse(txtTimeSDHH1.Text);
                        dtStartTime = GetDateTime(txtStartDate.Text, intHours, txtTimeSDMM1.Text, ddlSD1.SelectedItem.Text);

                        timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("UTC");

                        dateTimeStartTime = TimeZoneInfo.ConvertTime(dtStartTime, timeZoneInfo);

                        timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("UTC");
                        dateTimeStartTime = TimeZoneInfo.ConvertTime(dateTimeStartTime, timeZoneInfo);

                        int nUserId = int.Parse(Session["UserId"].ToString());
                        DALmstAppointmentManagement objDALmstAppointmentManagement = new DALmstAppointmentManagement();
                        DataSet dsDALmstAppointmentManagement = new DataSet();

                        dsDALmstAppointmentManagement = objDALmstAppointmentManagement.UpdateAppoinmentReSchedule(nUserId, nAppointmentId, dateTimeStartTime);
                        BindAllAppoinments();

                    }
                    else
                    {

                        lblmmm.Visible = true;
                        lblhhh.Visible = false;
                        lblmmm.Text = "Select Hours Between 1 To 60";
                        AjaxControlToolkit.ModalPopupExtender modal = (AjaxControlToolkit.ModalPopupExtender)item.FindControl("ModalPopupExtenderName");
                        modal.Show();
                    }
                }
                else
                {
                    lblhhh.Visible = true;
                    lblmmm.Visible = false;
                    lblhhh.Text = "Select Hours Between 1 To 12";
                    AjaxControlToolkit.ModalPopupExtender modal = (AjaxControlToolkit.ModalPopupExtender)item.FindControl("ModalPopupExtenderName");
                    modal.Show();
                }

            }
            else
            {
                string strTitle = "Alert";
                string strDescriptions = "Please Enter Data";
                NotificationMessage2.NotificationDetails(strTitle, strDescriptions);
                AjaxControlToolkit.ModalPopupExtender modal = (AjaxControlToolkit.ModalPopupExtender)item.FindControl("ModalPopupExtenderName");
                modal.Show();

            }

        
    }
    protected void btnChanfeStatus_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        DataGridItem item = (DataGridItem)btn.Parent.Parent.Parent;
        Label lblnAppointmentId = (Label)item.FindControl("lblnAppointmentId");
        int nAppointmentId = int.Parse(lblnAppointmentId.Text);


        DropDownList ddlStatus = (DropDownList)item.FindControl("ddlStatus");


        TextBox txtEndDate = (TextBox)item.FindControl("txtEndDate");
        TextBox txtTimeSDHH2 = (TextBox)item.FindControl("txtTimeSDHH2");
        TextBox txtTimeSDMM2 = (TextBox)item.FindControl("txtTimeSDMM2");
        DropDownList ddlSD2 = (DropDownList)item.FindControl("ddlSD2");

        Label lblhh = (Label)item.FindControl("lblhh");
        Label lblmm = (Label)item.FindControl("lblmm");

        if (ddlStatus.SelectedIndex!=0)
        {
            if (txtEndDate.Text != "" && txtTimeSDMM2.Text != "" && txtTimeSDHH2.Text != "" && ddlSD2.SelectedItem.Text != "")
            {

                int intHH2 = int.Parse(txtTimeSDHH2.Text.ToString());
                if (intHH2 <= 12)
                {

                    int intMM1 = int.Parse(txtTimeSDMM2.Text.ToString());
                    if (intMM1 < 60)
                    {

                        TimeZoneInfo timeZoneInfo;

                        DateTime dateTimeEndTime;

                        DateTime dtEndTime = Convert.ToDateTime(txtEndDate.Text.ToString());

                        DateTime tdTodayDate = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy"));


                        int intHours = int.Parse(txtTimeSDHH2.Text);
                        dtEndTime = GetDateTime(txtEndDate.Text, intHours, txtTimeSDMM2.Text, ddlSD2.SelectedItem.Text);

                        timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("UTC");

                        dateTimeEndTime = TimeZoneInfo.ConvertTime(dtEndTime, timeZoneInfo);

                        timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("UTC");
                        dateTimeEndTime = TimeZoneInfo.ConvertTime(dateTimeEndTime, timeZoneInfo);

                        int nUserId = int.Parse(Session["UserId"].ToString());
                        DALmstAppointmentManagement objDALmstAppointmentManagement = new DALmstAppointmentManagement();
                        DataSet dsDALmstAppointmentManagement = new DataSet();
                        string cAppointmentTitle = ddlStatus.SelectedItem.Text;
                        dsDALmstAppointmentManagement = objDALmstAppointmentManagement.UpdateAppoinmentStatus(nUserId, cAppointmentTitle, nAppointmentId, dateTimeEndTime);
                        BindAllAppoinments();

                    }
                    else
                    {

                        lblmm.Visible = true;
                        lblhh.Visible = false;
                        lblmm.Text = "Select Hours Between 1 To 60";
                        AjaxControlToolkit.ModalPopupExtender modal = (AjaxControlToolkit.ModalPopupExtender)item.FindControl("ModalPopupExtenderName1");
                        modal.Show();
                    }
                }
                else
                {
                    lblhh.Visible = true;
                    lblmm.Visible = false;
                    lblhh.Text = "Select Hours Between 1 To 12";
                    AjaxControlToolkit.ModalPopupExtender modal = (AjaxControlToolkit.ModalPopupExtender)item.FindControl("ModalPopupExtenderName1");
                    modal.Show();
                }

            }
            else
            {
                string strTitle = "Alert";
                string strDescriptions = "Please Enter Data";
                NotificationMessage2.NotificationDetails(strTitle, strDescriptions);
                AjaxControlToolkit.ModalPopupExtender modal = (AjaxControlToolkit.ModalPopupExtender)item.FindControl("ModalPopupExtenderName1");
                modal.Show();

            }
        }
        else
        {
            string strTitle = "Alert";
            string strDescriptions = "Please Select Status";
            NotificationMessage2.NotificationDetails(strTitle, strDescriptions);
            AjaxControlToolkit.ModalPopupExtender modal = (AjaxControlToolkit.ModalPopupExtender)item.FindControl("ModalPopupExtenderName1");
            modal.Show();
        }

       

    }
}