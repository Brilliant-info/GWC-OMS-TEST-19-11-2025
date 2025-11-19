using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Data;
using PowerOnRentwebapp.Login;
using PowerOnRentwebapp.CompanySetupService;
using PowerOnRentwebapp.PORServicePartRequest;
using System.IO;
using System.Drawing.Printing;
using System.Drawing.Imaging;
using PowerOnRentwebapp.POR;
using System.Data.SqlClient;
using System.Configuration;
using iTextSharp.text.pdf;

namespace PowerOnRentwebapp.POR.Reports
{
    public partial class NewReportViewer : System.Web.UI.Page
    {
       
        PrintReport PR = new PrintReport();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                ShowReport();
            }
        }

        protected void ShowReport()
        {
            string language = Session["Lang"].ToString();
            DataSet ds = new DataSet();

            if (Session["ReportDS"] != null)
            {
                string Teststr;
                if (Session["SelObject"] != null)
                {
                    Teststr = Session["SelObject"].ToString();
                }

                ds = (DataSet)Session["ReportDS"];

                if (ds.Tables[0].TableName == "dsavgtime")
                {
                    ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(rds1);
                    ReportViewer1.LocalReport.ReportPath = "POR/Reports/DeliveryListReport.rdlc";
                    ReportViewer1.LocalReport.SetParameters(RptParametersavgTime());
                    ReportViewer1.ShowParameterPrompts = false;
                    ReportViewer1.ShowPromptAreaButton = false;
                    ReportViewer1.LocalReport.Refresh();
                }
                else if (ds.Tables[0].TableName == "dsnoofdelivery")
                {
                    DataSet ds1 = new DataSet();
                    ds1 = (DataSet)Session["ReportDS1"];
                    ds1.Tables[0].TableName = "dsApprovalDetail";

                    ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                    ReportDataSource rds2 = new ReportDataSource(ds1.Tables[0].TableName, ds1.Tables[0]);
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(rds1);
                    ReportViewer1.LocalReport.DataSources.Add(rds2);
                    ReportViewer1.LocalReport.ReportPath = "POR/Reports/DeliveryDetailReport.rdlc";
                    ReportViewer1.LocalReport.SetParameters(RptParameters6());
                    ReportViewer1.ShowParameterPrompts = false;
                    ReportViewer1.ShowPromptAreaButton = false;
                    ReportViewer1.LocalReport.Refresh();
                }
                else if (ds.Tables[0].TableName == "dsdriverschedule")
                {

                    ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(rds1);
                    ReportViewer1.LocalReport.ReportPath = "POR/Reports/DriverSchedule.rdlc";
                    ReportViewer1.LocalReport.SetParameters(RptParametersSch());
                    ReportViewer1.ShowParameterPrompts = false;
                    ReportViewer1.ShowPromptAreaButton = false;
                    ReportViewer1.LocalReport.Refresh();
                }
                else if (ds.Tables[0].TableName == "dsSKU")
                {

                    if (language == "ar-QA")
                    {
                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.ReportPath = "POR/Reports/RptSKUNEWArabic.rdlc";
                        ReportViewer1.LocalReport.EnableExternalImages = true;
                        ReportViewer1.LocalReport.SetParameters(RptParameters2());
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();
                    }
                    else
                    {
                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.ReportPath = "POR/Reports/RptSKU.rdlc";
                        ReportViewer1.LocalReport.EnableExternalImages = true;
                        ReportViewer1.LocalReport.SetParameters(RptParameters2());
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();

                    }


                }
                else if (ds.Tables[0].TableName == "dsUser")//c
                {

                    if (language == "ar-QA")
                    {
                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.ReportPath = "POR/Reports/RptUserNEWArabic.rdlc";
                        ReportViewer1.LocalReport.SetParameters(RptParameters3());
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();
                    }
                    else
                    {
                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.ReportPath = "POR/Reports/RptUser.rdlc";
                        ReportViewer1.LocalReport.SetParameters(RptParameters3());
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();
                    }

                }
                else if (ds.Tables[0].TableName == "dsOrder")
                {

                    if (language == "ar-QA")
                    {
                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.ReportPath = "POR/Reports/RptOrderNEWArabiic.rdlc";
                        ReportViewer1.LocalReport.SetParameters(RptParameters5());
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();
                    }
                    else
                    {
                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.ReportPath = "POR/Reports/RptOrder.rdlc";
                        ReportViewer1.LocalReport.SetParameters(RptParameters5());
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();
                    }


                }
                else if (ds.Tables[0].TableName == "dsfailed")
                {
                    ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(rds1);
                    ReportViewer1.LocalReport.ReportPath = "POR/Reports/Importfailed.rdlc";
                    ReportViewer1.ShowParameterPrompts = false;
                    ReportViewer1.ShowPromptAreaButton = false;
                    ReportViewer1.LocalReport.Refresh();

                }

                else if (ds.Tables[0].TableName == "dsOrderLeadTime")
                {

                    if (language == "ar-QA")
                    {
                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.ReportPath = "POR/Reports/RptOrderLeadTimeNEWArabic.rdlc";
                        ReportViewer1.LocalReport.SetParameters(RptParameters5());
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();
                    }
                    else
                    {
                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.ReportPath = "POR/Reports/RptOrderLeadTime.rdlc";
                        ReportViewer1.LocalReport.SetParameters(RptParameters5());
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();
                    }

                }

                else if (ds.Tables[0].TableName == "dsOrderDetails")
                {
                    if (language == "ar-QA")
                    {
                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.ReportPath = "POR/Reports/OrderDetailsNEWArabic.rdlc";
                        ReportViewer1.LocalReport.SetParameters(RptParameters5());
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();
                    }
                    else
                    {
                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.ReportPath = "POR/Reports/OrderDetails.rdlc";
                        ReportViewer1.LocalReport.SetParameters(RptParameters5());
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();
                    }


                }
                else if (ds.Tables[0].TableName == "dsBOMDetails")
                {

                    if (language == "ar-QA")
                    {
                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.ReportPath = "POR/Reports/RptBOMDetailsNEWArabic.rdlc";
                        ReportViewer1.LocalReport.SetParameters(RptParameters6());
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();
                    }
                    else
                    {

                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.ReportPath = "POR/Reports/RptBOMDetails.rdlc";
                        ReportViewer1.LocalReport.SetParameters(RptParameters6());
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();
                    }
                }
                else if (ds.Tables[0].TableName == "dsSKUTrans")
                {
                    if (language == "ar-QA")
                    {
                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.ReportPath = "POR/Reports/SkuTransactionReportNEWArabic.rdlc";
                        ReportViewer1.LocalReport.SetParameters(RptParameters7());
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();
                    }
                    else
                    {
                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.ReportPath = "POR/Reports/SkuTransactionReport.rdlc";
                        ReportViewer1.LocalReport.SetParameters(RptParameters7());
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();
                    }
                }
                else if (ds.Tables[0].TableName == "dsSKUDetails")
                {
                    if (language == "ar-QA")
                    {
                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.ReportPath = "POR/Reports/RptSKUDetailNEWArabic.rdlc";
                        ReportViewer1.LocalReport.SetParameters(RptParameters7());
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();
                    }
                    else
                    {
                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.ReportPath = "POR/Reports/RptSKUDetail.rdlc";
                        ReportViewer1.LocalReport.SetParameters(RptParameters7());
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();
                    }

                }
                else if (ds.Tables[0].TableName == "dsImageAudit")
                {
                    if (language == "ar-QA")
                    {
                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.ReportPath = "POR/Reports/RptImageAudit.rdlc";
                        ReportViewer1.LocalReport.SetParameters(RptParametersImg());
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();
                    }
                    else
                    {
                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.ReportPath = "POR/Reports/RptImageAudit.rdlc";
                        ReportViewer1.LocalReport.SetParameters(RptParametersImg());
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();
                    }
                }
                else if (ds.Tables[0].TableName == "dsImageAuditFail")
                {
                    if (language == "ar-QA")
                    {
                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.ReportPath = "POR/Reports/RptImageAuditFailNEWARABIC.rdlc";
                        ReportViewer1.LocalReport.SetParameters(RptParametersImg());
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();
                    }
                    else
                    {
                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.ReportPath = "POR/Reports/RptImageAuditFail.rdlc";
                        ReportViewer1.LocalReport.SetParameters(RptParametersImg());
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();
                    }
                }
                else if (ds.Tables[0].TableName == "dsUserTrans")
                {
                    if (language == "ar-QA")
                    {
                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.ReportPath = "POR/Reports/UserTransactionReportNEWArabic.rdlc";
                        ReportViewer1.LocalReport.SetParameters(RptParameters3());
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();
                    }
                    else
                    {
                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.ReportPath = "POR/Reports/UserTransactionReport.rdlc";
                        ReportViewer1.LocalReport.SetParameters(RptParameters3());
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();
                    }
                }
                else if (ds.Tables[0].TableName == "dsOrderDelivery")
                {

                    if (language == "ar-QA")
                    {
                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.ReportPath = "POR/Reports/OrderDeliveryReportArabic.rdlc";
                        ReportViewer1.LocalReport.SetParameters(RptParameters8());
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();
                    }
                    else
                    {
                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.ReportPath = "POR/Reports/OrderDeliveryReport.rdlc";
                        ReportViewer1.LocalReport.SetParameters(RptParameters8());
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();
                    }


                }
                else if (ds.Tables[0].TableName == "dsSla")
                {

                    if (language == "ar-QA")
                    {
                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.ReportPath = "POR/Reports/RptSLAArabic.rdlc";
                        ReportViewer1.LocalReport.SetParameters(RptParameters9());
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();
                    }
                    else
                    {
                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.ReportPath = "POR/Reports/RptSLA.rdlc";
                        ReportViewer1.LocalReport.SetParameters(RptParameters9());
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();
                    }
                }
                else if (ds.Tables[0].TableName == "dsTDVSTR")
                {
                    if (language == "ar-QA")
                    {
                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.ReportPath = "POR/Reports/TotalDeliveryvsTotalRequestReportNewArabic.rdlc";
                        ReportViewer1.LocalReport.SetParameters(RptParameters10());
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();
                    }
                    else
                    {
                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.ReportPath = "POR/Reports/TotalDeliveryvsTotalRequestReport.rdlc";
                        ReportViewer1.LocalReport.SetParameters(RptParameters10());
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();
                    }
                }
                else if (ds.Tables[0].TableName == "dsLocation")
                {
                    if (language == "ar-QA")
                    {
                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.ReportPath = "POR/Reports/RptPaymentDetailNewArabic.rdlc";
                        ReportViewer1.LocalReport.SetParameters(RptParametersPD());
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();
                    }
                    else
                    {
                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.ReportPath = "POR/Reports/RptPaymentDetail.rdlc";
                        ReportViewer1.LocalReport.SetParameters(RptParametersPD());
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();
                    }
                }
                else if (ds.Tables[0].TableName == "dsdriverdeliverylogrpt")
                {

                    if (language == "ar-QA")
                    {
                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.ReportPath = "POR/Reports/DriverDeliveryLogReportArabic.rdlc";
                        ReportViewer1.LocalReport.SetParameters(RptParametersDriverLogForm());
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();
                    }
                    else
                    {
                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.ReportPath = "POR/Reports/DeliveryLog.rdlc";
                        ReportViewer1.LocalReport.SetParameters(RptParametersDriverLogForm());
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();
                    }
                }

                else if (ds.Tables[0].TableName == "dsPrepaidRpt")
                {
                    if (language == "ar-QA")
                    {
                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.ReportPath = "POR/Reports/PrepaidCustAppFrm.rdlc";
                        ReportViewer1.LocalReport.SetParameters(RptParametersPrepaidForm());
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();
                    }
                    else
                    {
                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.ReportPath = "POR/Reports/PrepaidCustAppFrm.rdlc";
                        ReportViewer1.LocalReport.SetParameters(RptParametersPrepaidForm());
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();
                    }
                }
                else if (ds.Tables[0].TableName == "dsBoardband")//dsBoardband
                {
                    if (language == "ar-QA")
                    {
                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.ReportPath = "POR/Reports/BroadbandFormRpt.rdlc";
                        ReportViewer1.LocalReport.SetParameters(RptParametersBoardbandForm());
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();
                    }
                    else
                    {
                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.ReportPath = "POR/Reports/BroadbandFormRpt.rdlc";
                        ReportViewer1.LocalReport.SetParameters(RptParametersBoardbandForm());
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();
                    }
                }

                else if (ds.Tables[0].TableName == "dsDeliveryFormRpt")
                {
                    if (language == "ar-QA")
                    {
                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.ReportPath = "POR/Reports/EcomDeliveryfrom.rdlc";
                        // ReportViewer1.LocalReport.ReportPath = "POR/Reports/DeliveryFromRpt.rdlc";
                        ReportViewer1.LocalReport.SetParameters(RptParametersDeliveryFormRpt());
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();
                    }
                    else
                    {
                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.ReportPath = "POR/Reports/EcomDeliveryfrom.rdlc";
                        // ReportViewer1.LocalReport.ReportPath = "POR/Reports/DeliveryFromRpt.rdlc";
                        ReportViewer1.LocalReport.SetParameters(RptParametersDeliveryFormRpt());
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();
                        //  PR.Run(rds1, "POR/Reports/EcomDeliveryfrom.rdlc", ds.Tables[0].TableName, ds);
                    }

                    // LocalReport report = new LocalReport();
                    // report.ReportPath = @"POR/Reports/EcomDeliveryfrom.rdlc";
                    ////  report.DataSources.Add(new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]));
                    //   Export1(report);
                    //  Print1();
                    //   Print();
                }
                else if (ds.Tables[0].TableName == "dsDeliveryFormNormalOrder")//normal order change by suraj khopade
                {
                    if (language == "ar-QA")
                    {
                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.ReportPath = "POR/Reports/NormalOrderReportDataDelivery.rdlc";
                        // ReportViewer1.LocalReport.SetParameters(RptParametersNormalOrderFormRpt());
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();
                    }
                    else
                    {
                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.ReportPath = "POR/Reports/NormalOrderReportDataDelivery.rdlc";
                       // ReportViewer1.LocalReport.SetParameters(RptParametersNormalOrderFormRpt());
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();
                    }
                }

                else if (ds.Tables[0].TableName == "dsEcommerceRpt1")
                {
                    if (language == "ar-QA")
                    {

                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.ReportPath = "POR/Reports/EcommerceReport1Arabic.rdlc";
                        ReportViewer1.LocalReport.SetParameters(RptParametersEcomrrpt());
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();
                    }
                    else
                    {
                        DataSet ds1 = new DataSet();
                        ds1 = (DataSet)Session["ReportDS1"];
                        ds1.Tables[0].TableName = "dsEcommdetailRpt1";
                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportDataSource rds2 = new ReportDataSource(ds1.Tables[0].TableName, ds1.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.DataSources.Add(rds2);
                        ReportViewer1.LocalReport.ReportPath = "POR/Reports/EcommerceReport1.rdlc";
                        ReportViewer1.LocalReport.SetParameters(RptParametersEcomrrpt());
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();
                    }
                }

                else if (ds.Tables[0].TableName == "dsEcommerceRpt2")
                {
                    if (language == "ar-QA")
                    {
                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.ReportPath = "POR/Reports/EcommerceReport2Arabic.rdlc";
                        ReportViewer1.LocalReport.SetParameters(RptParametersEcomrrpt2());
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();
                    }
                    else
                    {
                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.ReportPath = "POR/Reports/EcommerceReport2.rdlc";
                        ReportViewer1.LocalReport.SetParameters(RptParametersEcomrrpt2());
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();
                    }
                }
                else if (ds.Tables[0].TableName == "dsEcommerceDump")
                {
                    ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportViewer1.LocalReport.DataSources.Add(rds1);
                    ReportViewer1.LocalReport.ReportPath = "POR/Reports/EcommerceDumpReport.rdlc";
                    ReportViewer1.LocalReport.SetParameters(RptParametersEcomrrpt2());
                    ReportViewer1.ShowParameterPrompts = false;
                    ReportViewer1.ShowPromptAreaButton = false;
                    ReportViewer1.LocalReport.Refresh();
                }
                else if (ds.Tables[0].TableName == "QNBNdsSku")
                {

                    if (language == "ar-QA")
                    {
                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.ReportPath = "POR/Reports/QNBNRptSKU.rdlc";
                        ReportViewer1.LocalReport.EnableExternalImages = true;
                        ReportViewer1.LocalReport.SetParameters(RptParameters2());
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();
                    }
                    else
                    {
                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.ReportPath = "POR/Reports/QNBNRptSKU.rdlc";
                        ReportViewer1.LocalReport.EnableExternalImages = true;
                        ReportViewer1.LocalReport.SetParameters(RptParameters2());
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();

                    }


                }

                else if (ds.Tables[0].TableName == "QNBNdsOrder")
                {

                    if (language == "ar-QA")
                    {
                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.ReportPath = "POR/Reports/QNBNRptOrder.rdlc";
                        ReportViewer1.LocalReport.SetParameters(RptParameters5());
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();
                    }
                    else
                    {
                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.ReportPath = "POR/Reports/QNBNRptOrder.rdlc";
                        ReportViewer1.LocalReport.SetParameters(RptParameters5());
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();
                    }


                }

                else if (ds.Tables[0].TableName == "dsFMSRpt")
                {

                    if (language == "ar-QA")
                    {
                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.ReportPath = "POR/Reports/CAFRpt.rdlc";
                        ReportViewer1.LocalReport.SetParameters(RptParametersFMSForm());
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();
                    }
                    else
                    {
                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.ReportPath = "POR/Reports/CAFRpt.rdlc";
                        ReportViewer1.LocalReport.SetParameters(RptParametersFMSForm());
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();
                    }


                }

                else if (ds.Tables[0].TableName == "dsskutracking")
                {

                    if (language == "ar-QA")
                    {
                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.ReportPath = "POR/Reports/Skutrack.rdlc";
                        ReportViewer1.LocalReport.SetParameters(RptParametersskutrack());
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();
                    }
                    else
                    {
                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.ReportPath = "POR/Reports/Skutrack.rdlc";
                        ReportViewer1.LocalReport.SetParameters(RptParametersskutrack());
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();
                    }


                }

                else if (ds.Tables[0].TableName == "dssitetracking")
                {

                    if (language == "ar-QA")
                    {
                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.ReportPath = "POR/Reports/Sitetrack.rdlc";
                        ReportViewer1.LocalReport.SetParameters(RptParameterssitetrack());
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();
                    }
                    else
                    {
                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.ReportPath = "POR/Reports/Sitetrack.rdlc";
                        ReportViewer1.LocalReport.SetParameters(RptParameterssitetrack());
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();
                    }


                }
                else if (ds.Tables[0].TableName == "dsDepttrDacking")
                {

                    if (language == "ar-QA")
                    {
                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.ReportPath = "POR/Reports/Depttrack.rdlc";
                        ReportViewer1.LocalReport.SetParameters(RptParametersDepttrack());
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();
                    }
                    else
                    {
                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.ReportPath = "POR/Reports/Depttrack.rdlc";
                        ReportViewer1.LocalReport.SetParameters(RptParametersDepttrack());
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();
                    }


                }
                
                else if (ds.Tables[0].TableName == "dsPrepaidCAFDN")
                {

                    if (language == "ar-QA")
                    {
                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SubreportProcessingPrepaidCAFDN);
                        ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SubreportProcessing2);
                        ReportViewer1.LocalReport.ReportPath = "POR/Reports/DNPrepaid.rdlc";
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();

                    }
                    else
                    {

                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SubreportProcessingPrepaidCAFDN);
                        ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SubreportProcessing2);
                        ReportViewer1.LocalReport.ReportPath = "POR/Reports/DNPrepaid.rdlc";
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();

                    }


                }
                else if (ds.Tables[0].TableName == "dsPostpaidCAFDN")
                {

                    if (language == "ar-QA")
                    {
                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SubreportProcessingPostpaidCAFDN);
                        ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SubreportProcessing1);
                        ReportViewer1.LocalReport.ReportPath = "POR/Reports/DNPostpaid.rdlc";
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();
                    }
                    else
                    {

                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SubreportProcessingPostpaidCAFDN);
                        ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SubreportProcessing1);
                        ReportViewer1.LocalReport.ReportPath = "POR/Reports/DNPostpaid.rdlc";
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();

                    }


                }


                else if (ds.Tables[0].TableName == "dsDN")
                {

                    if (language == "ar-QA")
                    {
                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SubreportProcessing);                      
                        ReportViewer1.LocalReport.ReportPath = "POR/Reports/DNOnly.rdlc";
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();
                    }
                    else
                    {

                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SubreportProcessing);                     
                        ReportViewer1.LocalReport.ReportPath = "POR/Reports/DNOnly.rdlc";
                        //string Url = ConvertReportToPDF(ReportViewer1.LocalReport);
                        //System.Diagnostics.Process.Start(Url);
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();

                    }

                }
                else if (ds.Tables[0].TableName == "dsvat")
                {

                    if (language == "ar-QA")
                    {
                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.ReportPath = "POR/Reports/RptOrderNEWArabiic.rdlc";
                        ReportViewer1.LocalReport.SetParameters(RptParametersvat());
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();
                    }
                    else
                    {
                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.ReportPath = "POR/Reports/VatReport.rdlc";
                        ReportViewer1.LocalReport.SetParameters(RptParametersvat());
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();
                    }


                }
                // return order collection report
                else if (ds.Tables[0].TableName == "dsReturnOrderCollectionDetails")
                {

                    if (language == "ar-QA")
                    {
                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.ReportPath = "POR/Reports/RptReturnOrderCollection.rdlc";
                          ReportViewer1.LocalReport.SetParameters(RptParameterReturnOrderCollection());//FromDt ToDt Department
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();
                    }
                    else
                    {
                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                         ReportViewer1.LocalReport.ReportPath = "POR/Reports/RptReturnOrderCollection.rdlc";
                        ReportViewer1.LocalReport.SetParameters(RptParameterReturnOrderCollection());
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();
                    }


                }
                else if (ds.Tables[0].TableName == "dsReturnOrderReceiptSummaryReport")
                {

                    if (language == "ar-QA")
                    {
                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.ReportPath = "POR/Reports/RptReturnOrderReceiptSummaryReport.rdlc";
                        ReportViewer1.LocalReport.SetParameters(RptParameterReturnOrderReceiptSummery());
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();
                    }
                    else
                    {
                        ReportDataSource rds1 = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
                        ReportViewer1.LocalReport.DataSources.Clear();
                        ReportViewer1.LocalReport.DataSources.Add(rds1);
                        ReportViewer1.LocalReport.ReportPath = "POR/Reports/RptReturnOrderReceiptSummaryReport.rdlc";
                         ReportViewer1.LocalReport.SetParameters(RptParameterReturnOrderReceiptSummery());
                        ReportViewer1.ShowParameterPrompts = false;
                        ReportViewer1.ShowPromptAreaButton = false;
                        ReportViewer1.LocalReport.Refresh();
                    }


                }
            }
        }

        private string ConvertReportToPDF(LocalReport rep)
        {
            string reportType = "PDF";
            string mimeType;
            string encoding;

            string deviceInfo = "<DeviceInfo>" +
               "  <OutputFormat>PDF</OutputFormat>" +
               "  <PageWidth>8.27in</PageWidth>" +
               "  <PageHeight>6.0in</PageHeight>" +
               "  <MarginTop>0.2in</MarginTop>" +
               "  <MarginLeft>0.2in</MarginLeft>" +
               "  <MarginRight>0.2in</MarginRight>" +
               "  <MarginBottom>0.2in</MarginBottom>" +
               "</DeviceInfo>";

            Warning[] warnings;
            string[] streamIds;
            string extension = string.Empty;

            byte[] bytes = rep.Render(reportType, "", out mimeType, out encoding, out extension, out streamIds, out warnings);
            //string localPath = System.Configuration.ConfigurationManager.AppSettings["TempFiles"].ToString();  
            string localPath = AppDomain.CurrentDomain.BaseDirectory;
            string fileName = Guid.NewGuid().ToString() + ".pdf";
            localPath = localPath + fileName;
            System.IO.File.WriteAllBytes(localPath, bytes);
            return localPath;
        }

        void SubreportProcessing2(object sender, SubreportProcessingEventArgs e)
        {
            string constr = "";
            SqlConnection conn = new SqlConnection();
            constr = ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString();
            conn = new SqlConnection(constr);
            // SqlConnection con = new SqlConnection("Data Source=10.228.134.66; Initial Catalog=GWCDEEcomm; User ID=GWCTEST; Password='Password123#';connect timeout=0;Max Pool Size=20000;");
            long orderid = int.Parse(e.Parameters["oid"].Values[0].ToString());
            DataTable dt = new System.Data.DataTable();
            SqlDataAdapter adp = new SqlDataAdapter("SP_CAFPrepaid", conn);
            adp.SelectCommand.CommandType = CommandType.StoredProcedure;
            adp.SelectCommand.Parameters.AddWithValue("@orderid", orderid);
            adp.Fill(dt);
            ReportDataSource dts = new ReportDataSource("dsPrepaidRpt", dt);
            e.DataSources.Add(dts);
        }

        void SubreportProcessing1(object sender, SubreportProcessingEventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString()))
            {
                //string constr = "";
                //SqlConnection conn = new SqlConnection();
                //constr = ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString();
                //conn = new SqlConnection(constr);
                // SqlConnection con = new SqlConnection("Data Source=10.228.134.66; Initial Catalog=GWCDEEcomm; User ID=GWCTEST; Password='Password123#';connect timeout=0;Max Pool Size=20000;");
                long orderid = int.Parse(e.Parameters["oid"].Values[0].ToString());
                DataTable dt = new System.Data.DataTable();
                SqlDataAdapter adp = new SqlDataAdapter("SP_CAFSR", conn);
                adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                adp.SelectCommand.Parameters.AddWithValue("@orderid", orderid);
                adp.Fill(dt);
                ReportDataSource dts = new ReportDataSource("dsBoardband", dt);
                e.DataSources.Add(dts);
            }
        }

        void SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
           
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString()))
            {
               // constr = ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString();
                //conn = new SqlConnection(constr);
                // SqlConnection con = new SqlConnection("Data Source=10.228.134.66; Initial Catalog=GWCDEEcomm; User ID=GWCTEST; Password='Password123#';connect timeout=0;Max Pool Size=20000;");
                long orderid = int.Parse(e.Parameters["oid"].Values[0].ToString());
                CreateBarCode(orderid);
                InsertPrintData(orderid,"DN");
                DataTable dt = new System.Data.DataTable();
                SqlDataAdapter adp = new SqlDataAdapter("SP_DeliveryNoteSR", conn);
                adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                adp.SelectCommand.Parameters.AddWithValue("@orderid", orderid);
                adp.Fill(dt);
                ReportDataSource dts = new ReportDataSource("dsDeliveryFormRpt", dt);
                e.DataSources.Add(dts);
            }
        }

        void SubreportProcessingPrepaidCAFDN(object sender, SubreportProcessingEventArgs e)
        {

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString()))
            {
                // constr = ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString();
                //conn = new SqlConnection(constr);
                // SqlConnection con = new SqlConnection("Data Source=10.228.134.66; Initial Catalog=GWCDEEcomm; User ID=GWCTEST; Password='Password123#';connect timeout=0;Max Pool Size=20000;");
                long orderid = int.Parse(e.Parameters["oid"].Values[0].ToString());
                CreateBarCode(orderid);
                InsertPrintData(orderid, "Prepaid");
                DataTable dt = new System.Data.DataTable();
                SqlDataAdapter adp = new SqlDataAdapter("SP_DeliveryNoteSR", conn);
                adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                adp.SelectCommand.Parameters.AddWithValue("@orderid", orderid);
                adp.Fill(dt);
                ReportDataSource dts = new ReportDataSource("dsDeliveryFormRpt", dt);
                e.DataSources.Add(dts);
            }
        }

        void SubreportProcessingPostpaidCAFDN(object sender, SubreportProcessingEventArgs e)
        {

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString()))
            {
                // constr = ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString();
                //conn = new SqlConnection(constr);
                // SqlConnection con = new SqlConnection("Data Source=10.228.134.66; Initial Catalog=GWCDEEcomm; User ID=GWCTEST; Password='Password123#';connect timeout=0;Max Pool Size=20000;");
                long orderid = int.Parse(e.Parameters["oid"].Values[0].ToString());
                CreateBarCode(orderid);
                InsertPrintData(orderid, "Postpaid");
                DataTable dt = new System.Data.DataTable();
                SqlDataAdapter adp = new SqlDataAdapter("SP_DeliveryNoteSR", conn);
                adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                adp.SelectCommand.Parameters.AddWithValue("@orderid", orderid);
                adp.Fill(dt);
                ReportDataSource dts = new ReportDataSource("dsDeliveryFormRpt", dt);
                e.DataSources.Add(dts);
            }
        }
        public void InsertPrintData(long orderId,string rpttype)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString()))
            {
                CustomProfile profile = CustomProfile.GetProfile();
                DataTable dt = new System.Data.DataTable();
                SqlDataAdapter adp = new SqlDataAdapter("SP_InsertPrintdata", conn);
                adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                adp.SelectCommand.Parameters.AddWithValue("@orderid", orderId);
                adp.SelectCommand.Parameters.AddWithValue("@Userid", profile.Personal.UserID);
                adp.SelectCommand.Parameters.AddWithValue("@rpttype", rpttype);
                adp.Fill(dt);             
            }
        }

        private void CreateBarCode(long orderId)
        {
            try
            {
                iPartRequestClient objService = new iPartRequestClient();
                CustomProfile profile = CustomProfile.GetProfile();
                string ChkBarCodeCreateOrNot = objService.ChkBarcode(orderId, profile.DBConnection._constr);
                if (ChkBarCodeCreateOrNot == "No")
                {
                    DataSet ds = new DataSet();
                    string Orderno = "", Amtpaid = "";
                    ds = objService.GetOrderDetails(orderId, profile.DBConnection._constr);
                    Orderno = ds.Tables[0].Rows[0]["Orderno"].ToString();
                    Amtpaid = ds.Tables[0].Rows[0]["amounttobecollected"].ToString();
                    Barcode128 bc = new Barcode128();
                    bc.Code = Orderno;
                    System.Drawing.Image imgbrcd = bc.CreateDrawingImage(System.Drawing.Color.Black, System.Drawing.Color.White);
                    bc.BarHeight = 1000f;
                    byte[] ss = imgtobytearray(imgbrcd);
                    Barcode128 bc1 = new Barcode128();
                    bc1.Code = Amtpaid;
                    System.Drawing.Image imgbrcd1 = bc1.CreateDrawingImage(System.Drawing.Color.Black, System.Drawing.Color.White);
                    bc1.BarHeight = 1000f;
                    byte[] ss1 = imgtobytearray(imgbrcd1);
                    objService.InsertintomDeliverybarCode(orderId, ss, ss1, profile.DBConnection._constr);
                }
            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, "VirtualProductReport.aspx", "CreateBarCode");
            }

        }

        public byte[] imgtobytearray(System.Drawing.Image imgin)
        {
            MemoryStream ms = new MemoryStream();
            imgin.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }


        private List<ReportParameter>  RptParameterssitetrack()
        {
            CustomProfile profile = CustomProfile.GetProfile();
            List<ReportParameter> parameters = new List<ReportParameter>();
            mCompany compdetails = new mCompany();
            iCompanySetupClient compclient = new iCompanySetupClient();
            compdetails = compclient.GetCompanyById(profile.Personal.CompanyID, profile.DBConnection._constr);
            parameters.Add(new ReportParameter("FromDt", Session["FromDt"].ToString()));
            parameters.Add(new ReportParameter("ToDt", Session["ToDt"].ToString()));
            parameters.Add(new ReportParameter("Sitenm", Session["Sitenm"].ToString()));

            return parameters;
        }
        private List<ReportParameter> RptParametersDepttrack()
        {
            CustomProfile profile = CustomProfile.GetProfile();
            List<ReportParameter> parameters = new List<ReportParameter>();
            mCompany compdetails = new mCompany();
            iCompanySetupClient compclient = new iCompanySetupClient();
            compdetails = compclient.GetCompanyById(profile.Personal.CompanyID, profile.DBConnection._constr);
            parameters.Add(new ReportParameter("FromDt", Session["FromDt"].ToString()));
            parameters.Add(new ReportParameter("ToDt", Session["ToDt"].ToString()));
            parameters.Add(new ReportParameter("Deptnm", Session["Deptnm"].ToString()));

            return parameters;
        }


        private List<ReportParameter> RptParametersskutrack()
        {
            CustomProfile profile = CustomProfile.GetProfile();
            List<ReportParameter> parameters = new List<ReportParameter>();
            mCompany compdetails = new mCompany();
            iCompanySetupClient compclient = new iCompanySetupClient();
            compdetails = compclient.GetCompanyById(profile.Personal.CompanyID, profile.DBConnection._constr);
            parameters.Add(new ReportParameter("FromDt", Session["FromDt"].ToString()));
            parameters.Add(new ReportParameter("ToDt", Session["ToDt"].ToString()));
            parameters.Add(new ReportParameter("Skunm", Session["Skunm"].ToString()));

            return parameters;
        }


        private List<ReportParameter> RptParametersavgTime()
        {
            CustomProfile profile = CustomProfile.GetProfile();
            List<ReportParameter> parameters = new List<ReportParameter>();
            mCompany compdetails = new mCompany();
            iCompanySetupClient compclient = new iCompanySetupClient();
            compdetails = compclient.GetCompanyById(profile.Personal.CompanyID, profile.DBConnection._constr);
            parameters.Add(new ReportParameter("FromDt", Session["FromDt"].ToString()));
            parameters.Add(new ReportParameter("ToDt", Session["ToDt"].ToString()));
            parameters.Add(new ReportParameter("Driver", Session["Driver"].ToString()));
            parameters.Add(new ReportParameter("Location", Session["Location"].ToString()));
            parameters.Add(new ReportParameter("Route", Session["Route"].ToString()));
            parameters.Add(new ReportParameter("Status", Session["Status"].ToString()));
            return parameters;
        }

        private IEnumerable<ReportParameter> RptParametersDeliveryFormRpt()
        {
            List<ReportParameter> parameters = new List<ReportParameter>();
            parameters.Add(new ReportParameter("Fullname", Session["Fullname"].ToString()));
            parameters.Add(new ReportParameter("Address", Session["Address"].ToString()));
            return parameters;
        }

        //change by suraj khopade
        private IEnumerable<ReportParameter> RptParametersNormalOrderFormRpt()
        {
            List<ReportParameter> parameters = new List<ReportParameter>();
            parameters.Add(new ReportParameter("Fullname", Session["Fullname"].ToString()));
            parameters.Add(new ReportParameter("Address", Session["Address"].ToString()));
            return parameters;
        }

        private IEnumerable<ReportParameter> RptParametersBoardbandForm()
        {
            List<ReportParameter> parameters = new List<ReportParameter>();
            parameters.Add(new ReportParameter("Fullname", Session["Fullname"].ToString()));
            parameters.Add(new ReportParameter("pidt", Session["pidt"].ToString()));
            parameters.Add(new ReportParameter("qidt", Session["qidt"].ToString()));
            parameters.Add(new ReportParameter("Packname", Session["Packname"].ToString()));
            parameters.Add(new ReportParameter("Mobilenumber", Session["Mobilenumber"].ToString()));
            parameters.Add(new ReportParameter("Extrasvalue", Session["Extrasvalue"].ToString()));
            return parameters;
        }

        private IEnumerable<ReportParameter> RptParametersFMSForm()
        {
            List<ReportParameter> parameters = new List<ReportParameter>();
            parameters.Add(new ReportParameter("Fullname", Session["Fullname"].ToString()));
            parameters.Add(new ReportParameter("pidt", Session["pidt"].ToString()));
            parameters.Add(new ReportParameter("qidt", Session["qidt"].ToString()));
            parameters.Add(new ReportParameter("Packname", Session["Packname"].ToString()));
            parameters.Add(new ReportParameter("Mobilenumber", Session["Mobilenumber"].ToString()));
            parameters.Add(new ReportParameter("Extrasvalue", Session["Extrasvalue"].ToString()));
            return parameters;
        }

        private List<ReportParameter> RptParametersEcomrrpt2()
        {
            List<ReportParameter> parameters = new List<ReportParameter>();
            parameters.Add(new ReportParameter("Company", Session["Company"].ToString()));
            parameters.Add(new ReportParameter("Department", Session["Department"].ToString()));
            parameters.Add(new ReportParameter("Status", Session["Status"].ToString()));
            parameters.Add(new ReportParameter("User", Session["User"].ToString()));
            return parameters;
        }

        private List<ReportParameter> RptParametersEcomrrpt()
        {
            List<ReportParameter> parameters = new List<ReportParameter>();
            parameters.Add(new ReportParameter("Company", Session["Company"].ToString()));
            parameters.Add(new ReportParameter("Department", Session["Department"].ToString()));
            parameters.Add(new ReportParameter("FromDt", Session["FromDt"].ToString()));
            parameters.Add(new ReportParameter("ToDt", Session["ToDt"].ToString()));
            parameters.Add(new ReportParameter("paymentMode", Session["paymentMode"].ToString()));
            parameters.Add(new ReportParameter("Status", Session["Status"].ToString()));
            return parameters;
        }



        private List<ReportParameter> RptParametersPrepaidForm()
        {
            List<ReportParameter> parameters = new List<ReportParameter>();
            parameters.Add(new ReportParameter("FullName", Session["FullName"].ToString()));
            parameters.Add(new ReportParameter("Idt", Session["Idt"].ToString()));
            parameters.Add(new ReportParameter("IDExpiryDate", Session["IDExpiryDate"].ToString()));
            parameters.Add(new ReportParameter("OrderCreationDate", Session["OrderCreationDate"].ToString()));
            return parameters;
        }

        private List<ReportParameter> RptParametersDriverLogForm()
        {

            List<ReportParameter> parameters = new List<ReportParameter>();
            parameters.Add(new ReportParameter("FromDt", Session["FromDt"].ToString()));
            parameters.Add(new ReportParameter("ToDt", Session["ToDt"].ToString()));
            parameters.Add(new ReportParameter("Company", Session["Company"].ToString()));
            parameters.Add(new ReportParameter("Department", Session["Department"].ToString()));
            parameters.Add(new ReportParameter("Status", Session["Status"].ToString()));
            parameters.Add(new ReportParameter("User", Session["User"].ToString()));
            parameters.Add(new ReportParameter("PaymentMode", Session["PaymentMode"].ToString()));
            return parameters;
        }


        private List<ReportParameter> RptParametersPD()
        {
            List<ReportParameter> parameters = new List<ReportParameter>();
            parameters.Add(new ReportParameter("Company", Session["Company"].ToString()));
            parameters.Add(new ReportParameter("Department", Session["Department"].ToString()));
            parameters.Add(new ReportParameter("FromDt", Session["FromDt"].ToString()));
            parameters.Add(new ReportParameter("ToDt", Session["ToDt"].ToString()));
            parameters.Add(new ReportParameter("location", Session["location"].ToString()));
            parameters.Add(new ReportParameter("paymentMode", Session["paymentMode"].ToString()));
            return parameters;
        }

        private List<ReportParameter> RptParameters10()
        {
            List<ReportParameter> parameters = new List<ReportParameter>();
            parameters.Add(new ReportParameter("Company", Session["Company"].ToString()));
            parameters.Add(new ReportParameter("Department", Session["Department"].ToString()));
            parameters.Add(new ReportParameter("FrmDt", Session["FrmDt"].ToString()));
            parameters.Add(new ReportParameter("ToDt", Session["ToDt"].ToString()));
            return parameters;
        }

        private List<ReportParameter> RptParameters9()
        {
            List<ReportParameter> parameters = new List<ReportParameter>();
            parameters.Add(new ReportParameter("Company", Session["Company"].ToString()));
            parameters.Add(new ReportParameter("Department", Session["Department"].ToString()));

            parameters.Add(new ReportParameter("DriverName", Session["DriverName"].ToString()));
            parameters.Add(new ReportParameter("DeliveryType", Session["DeliveryType"].ToString()));
            parameters.Add(new ReportParameter("FrmDt", Session["FrmDt"].ToString()));
            parameters.Add(new ReportParameter("ToDt", Session["ToDt"].ToString()));
            return parameters;
        }
        private List<ReportParameter> RptParameters8()
        {
            List<ReportParameter> parameters = new List<ReportParameter>();
            parameters.Add(new ReportParameter("Company", Session["Company"].ToString()));
            parameters.Add(new ReportParameter("Department", Session["Department"].ToString()));
            parameters.Add(new ReportParameter("DriverName", Session["DriverName"].ToString()));
            parameters.Add(new ReportParameter("PaymentMode", Session["PaymentMode"].ToString()));
            parameters.Add(new ReportParameter("FrmDt", Session["FrmDt"].ToString()));
            parameters.Add(new ReportParameter("ToDt", Session["ToDt"].ToString()));
            return parameters;
        }

        private List<ReportParameter> RptParameters7()
        {

            List<ReportParameter> parameters = new List<ReportParameter>();
            parameters.Add(new ReportParameter("Company", Session["Company"].ToString()));
            parameters.Add(new ReportParameter("Department", Session["Department"].ToString()));
            parameters.Add(new ReportParameter("BOM", Session["BOM"].ToString()));
            parameters.Add(new ReportParameter("Image", Session["Image"].ToString()));
            return parameters;
        }

        private List<ReportParameter> RptParametersImg()
        {
            List<ReportParameter> parameters = new List<ReportParameter>();
            parameters.Add(new ReportParameter("Company", Session["Company"].ToString()));
            parameters.Add(new ReportParameter("Department", Session["Department"].ToString()));
            parameters.Add(new ReportParameter("ImgStatus", Session["ImgStatus"].ToString()));
            parameters.Add(new ReportParameter("UsrName", Session["UsrName"].ToString()));
            parameters.Add(new ReportParameter("FrmDt", Session["FrmDt"].ToString()));
            parameters.Add(new ReportParameter("ToDt", Session["ToDt"].ToString()));

            return parameters;
        }

        private List<ReportParameter> RptParameters6()
        {

            List<ReportParameter> parameters = new List<ReportParameter>();
            parameters.Add(new ReportParameter("Company", Session["Company"].ToString()));
            parameters.Add(new ReportParameter("Department", Session["Department"].ToString()));
            return parameters;
        }
        private List<ReportParameter> RptParameters4()
        {

            List<ReportParameter> parameters = new List<ReportParameter>();
            parameters.Add(new ReportParameter("Company", Session["Company"].ToString()));
            parameters.Add(new ReportParameter("Department", Session["Department"].ToString()));
            parameters.Add(new ReportParameter("FromDt", Session["FromDt"].ToString()));
            parameters.Add(new ReportParameter("ToDt", Session["ToDt"].ToString()));
            parameters.Add(new ReportParameter("Status", Session["Status"].ToString()));
            parameters.Add(new ReportParameter("User", Session["User"].ToString()));
            return parameters;
        }

        private List<ReportParameter> RptParameters2()
        {

            List<ReportParameter> parameters = new List<ReportParameter>();
            parameters.Add(new ReportParameter("Company", Session["Company"].ToString()));
            parameters.Add(new ReportParameter("Department", Session["Department"].ToString()));
            //  parameters.Add(new ReportParameter("ImagePath", Session["ImagePath"].ToString()));
            parameters.Add(new ReportParameter("BOM", Session["BOM"].ToString()));
            parameters.Add(new ReportParameter("Image", Session["Image"].ToString()));
            return parameters;
        }
        private List<ReportParameter> RptParameters3()
        {

            List<ReportParameter> parameters = new List<ReportParameter>();
            parameters.Add(new ReportParameter("Company", Session["Company"].ToString()));
            parameters.Add(new ReportParameter("Department", Session["Department"].ToString()));
            parameters.Add(new ReportParameter("RoleName", Session["RoleName"].ToString()));
            parameters.Add(new ReportParameter("Active", Session["Active"].ToString()));
            return parameters;
        }
        private List<ReportParameter> RptParameters5()
        {

            List<ReportParameter> parameters = new List<ReportParameter>();
            parameters.Add(new ReportParameter("Company", Session["Company"].ToString()));
            parameters.Add(new ReportParameter("Department", Session["Department"].ToString()));
            parameters.Add(new ReportParameter("Status", Session["Status"].ToString()));
            parameters.Add(new ReportParameter("User", Session["User"].ToString()));
            parameters.Add(new ReportParameter("FromDt", Session["FromDt"].ToString()));
            parameters.Add(new ReportParameter("ToDt", Session["ToDt"].ToString()));
            return parameters;
        }
        private List<ReportParameter> RptParametersvat()
        {

            List<ReportParameter> parameters = new List<ReportParameter>();
            parameters.Add(new ReportParameter("Company", Session["Company"].ToString()));
            parameters.Add(new ReportParameter("Department", Session["Department"].ToString()));
            parameters.Add(new ReportParameter("Status", Session["Status"].ToString()));
            parameters.Add(new ReportParameter("User", Session["User"].ToString()));
            parameters.Add(new ReportParameter("FromDt", Session["FromDt"].ToString()));
            parameters.Add(new ReportParameter("ToDt", Session["ToDt"].ToString()));
            return parameters;
        }
        private List<ReportParameter> RptParameterReturnOrderCollection()
        {

            List<ReportParameter> parameters = new List<ReportParameter>();
            parameters.Add(new ReportParameter("FromDt", Session["FromDt"].ToString()));
            parameters.Add(new ReportParameter("ToDt", Session["ToDt"].ToString()));
            parameters.Add(new ReportParameter("Department", Session["Department"].ToString()));
            return parameters;
        }
        private List<ReportParameter> RptParameterReturnOrderReceiptSummery()
        {

            List<ReportParameter> parameters = new List<ReportParameter>();
            parameters.Add(new ReportParameter("FromDt", Session["FromDt"].ToString()));
            parameters.Add(new ReportParameter("ToDt", Session["ToDt"].ToString()));
            parameters.Add(new ReportParameter("Department", Session["Department"].ToString()));
            return parameters;
        }

        private List<ReportParameter> RptParameters()
        {
            CustomProfile profile = CustomProfile.GetProfile();
            List<ReportParameter> parameters = new List<ReportParameter>();
            mCompany compdetails = new mCompany();
            iCompanySetupClient compclient = new iCompanySetupClient();
            compdetails = compclient.GetCompanyById(profile.Personal.CompanyID, profile.DBConnection._constr);
            parameters.Add(new ReportParameter("CompName", compdetails.Name));
            parameters.Add(new ReportParameter("CompAdd", compdetails.AddressLine1));
            parameters.Add(new ReportParameter("FromDt", Session["FromDt"].ToString()));
            parameters.Add(new ReportParameter("ToDt", Session["ToDt"].ToString()));
            parameters.Add(new ReportParameter("ReportHeading", Session["ReportHeading"].ToString()));
            // parameters.Add(new ReportParameter("CompLogo","../MasterPage/Logo/" + compdetails.LogoPath));
            return parameters;
        }
        private List<ReportParameter> RptParameters1()
        {
            CustomProfile profile = CustomProfile.GetProfile();
            List<ReportParameter> parameters = new List<ReportParameter>();
            mCompany compdetails = new mCompany();
            iCompanySetupClient compclient = new iCompanySetupClient();
            compdetails = compclient.GetCompanyById(profile.Personal.CompanyID, profile.DBConnection._constr);
            parameters.Add(new ReportParameter("CompName", compdetails.Name));
            parameters.Add(new ReportParameter("CompAdd", compdetails.AddressLine1));
            parameters.Add(new ReportParameter("FromDt", Session["FromDt"].ToString()));
            parameters.Add(new ReportParameter("ToDt", Session["ToDt"].ToString()));
            parameters.Add(new ReportParameter("ReportHeading", Session["ReportHeading"].ToString()));
            parameters.Add(new ReportParameter("Generator", Session["Generator"].ToString()));
            // parameters.Add(new ReportParameter("CompLogo","../MasterPage/Logo/" + compdetails.LogoPath));
            return parameters;
        }


        private List<ReportParameter> RptParametersSch()
        {
            CustomProfile profile = CustomProfile.GetProfile();
            List<ReportParameter> parameters = new List<ReportParameter>();
            mCompany compdetails = new mCompany();
            iCompanySetupClient compclient = new iCompanySetupClient();
            compdetails = compclient.GetCompanyById(profile.Personal.CompanyID, profile.DBConnection._constr);

            parameters.Add(new ReportParameter("Driver", Session["Driver"].ToString()));
            parameters.Add(new ReportParameter("Location", Session["Location"].ToString()));
            parameters.Add(new ReportParameter("Route", Session["Route"].ToString()));
            return parameters;
        }



        private IList<Stream> m_streams;
        private int m_currentPageIndex;
        private void Print1()
        {
            if (m_streams == null || m_streams.Count == 0)
                throw new Exception("Error: no stream to print.");
            PrintDocument printDoc = new PrintDocument();
            if (!printDoc.PrinterSettings.IsValid)
            {
                throw new Exception("Error: cannot find the default printer.");
            }
            else
            {
                printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
                m_currentPageIndex = 0;
                printDoc.Print();
            }
        }

        private void Print()
        {
            PrinterSettings settings = new PrinterSettings(); //set printer settings
            string printerName = settings.PrinterName; //use default printer name
                                                       // string printerName= PopulateInstalledPrintersCombo();
            if (m_streams == null || m_streams.Count == 0)
                return;
            //if (printDialog.ShowDialog() == DialogResult.OK) 
            //{
            //    m_pageSettings .PrinterSettings.PrinterName = printDialog.PrinterSettings.PrinterName;
            //}
            PrintDocument printDoc = new PrintDocument();
            printDoc.PrinterSettings.PrinterName = printerName;
            if (!printDoc.PrinterSettings.IsValid)
            {
                string msg = String.Format(
                "Can't find printer \"{0}\".", printerName);
                //  MessageBox.Show(msg, "Print Error");
                return;
            }

            printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
            printDoc.Print();
            foreach (Stream stream in m_streams)
            {
                stream.Dispose();
            }
        }

        private void PrintPage(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new
               Metafile(m_streams[m_currentPageIndex]);
            ev.Graphics.DrawImage(pageImage, ev.PageBounds);
            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }

    }
}