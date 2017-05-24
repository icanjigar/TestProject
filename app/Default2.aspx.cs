using ASPNetMultiLanguage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class app_Default2 : ASPNetMultiLanguage.BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        #region--Show/hide language link
        if (!string.IsNullOrEmpty(Convert.ToString(Session["lang"])))
        {
            //if (Convert.ToString(Session["lang"]) == "en")
            //{
            // //   linkVietnameseLang.Visible = true;
            // //   linkEnglishLang.Visible = false;
            //}
            //else
            //{
            //    //   linkEnglishLang.Visible = true;
            //    //   linkVietnameseLang.Visible = false;
            //    //   Response.Redirect("~/HR/Currency.aspx");
            //}
            string value = Session["lang"].ToString();
            BasePage objBasePage = new BasePage();
            objBasePage.InitializeCulture1(value);
            string LastUrl = Request.UrlReferrer.ToString();
            Response.Redirect(LastUrl);
        }
        else
        {
           
        }
        #endregion--
    }
}