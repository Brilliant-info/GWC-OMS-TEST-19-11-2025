using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PowerOnRentwebapp.Login;
using System.Web.Services;
using System.Data;
using Microsoft.Reporting.WebForms;
using PowerOnRentwebapp.PORServiceUCCommonFilter;
using PowerOnRentwebapp.CommonControls;
using System.Resources;
using System.Globalization;
using System.Threading;
using System.Reflection;
using PowerOnRentwebapp.PORServicePartRequest;
using iTextSharp.text.pdf;
using System.IO;
using PowerOnRentwebapp.UserCreationService;


//namespace WebClientElegantCRM.PowerOnRent
namespace PowerOnRentwebapp.PowerOnRent
{
    public partial class CommonReport : System.Web.UI.Page
    {
        ResourceManager rm;
        CultureInfo ci;
        string QueryParameter, ObjectName;


        protected void Page_Load(object sender, EventArgs e)
        {
            CustomProfile profile = CustomProfile.GetProfile();
            if (Session["Lang"] == "")
            {
                Session["Lang"] = Request.UserLanguages[0];
            }
            if (string.IsNullOrEmpty((string)Session["Lang"]))
            {
                Session["Lang"] = Request.UserLanguages[0];
            }
            //loadstring();
            //UCCommonFilter1.fillDetail();
            if (!IsPostBack)
            {
                if (profile.Personal.UserType == "Super Admin")
                {
                    QueryParameter = Request.QueryString["invoker"];
                    normal.Visible = true;
                    ecomm.Visible = true;
                    hmc.Visible = false;
                    vfq.Visible = true;
                    vftechnical.Visible = true;


                    //normal.Visible = false;
                    //ecomm.Visible = false;
                    //hmc.Visible = false;
                    //vfq.Visible = false;
                    //vftechnical.Visible = true;
                }
                else
                {
                    iPartRequestClient objservice = new iPartRequestClient();
                    string CkhUserType = "";
                    CkhUserType = objservice.CkhUserType(profile.Personal.UserID, profile.DBConnection._constr);
                    //CkhUserType = "vftechnical";
                    QueryParameter = Request.QueryString["invoker"];
                    if (profile.Personal.UserType == "Admin")
                    {
                        if (CkhUserType == "Ecomm")
                        {
                            if (QueryParameter == "sku")
                            {
                                QueryParameter = "ecommerce1";
                                Response.Redirect("~/PowerOnRent/CommonReport.aspx?invoker=ecommerce1");
                            }
                            else
                            {
                                QueryParameter = Request.QueryString["invoker"];
                            }
                            normal.Visible = false;
                            ecomm.Visible = true;
                            hmc.Visible = false;
                            vfq.Visible = false;
                            vftechnical.Visible = false;
                        }
                        else
                        {
                            QueryParameter = Request.QueryString["invoker"];
                            normal.Visible = true;
                            ecomm.Visible = false;
                            hmc.Visible = false;
                            vfq.Visible = false;
                            vftechnical.Visible = false;
                        }


                    }
                    else
                    {
                        if (profile.Personal.UserType == "Logistic" || profile.Personal.UserType == "Fulfilment" || profile.Personal.UserType == "GWC User" || profile.Personal.UserType == "Retail User Admin" || profile.Personal.UserType == "Retail User")
                        {

                            if (QueryParameter == "sku")
                            {
                                QueryParameter = "ecommerce1";
                                Response.Redirect("~/PowerOnRent/CommonReport.aspx?invoker=ecommerce1");
                            }
                            else
                            {
                                QueryParameter = Request.QueryString["invoker"];
                            }
                            normal.Visible = false;
                            ecomm.Visible = true;
                            hmc.Visible = false;
                            vfq.Visible = false;
                            vftechnical.Visible = false;
                        }
                        else if (CkhUserType == "QNBN")
                        {
                            if (QueryParameter == "sku")
                            {
                                QueryParameter = "qnbnstock";
                                Response.Redirect("~/PowerOnRent/CommonReport.aspx?invoker=qnbnstock");
                            }
                            else
                            {
                                QueryParameter = Request.QueryString["invoker"];
                            }
                            normal.Visible = false;
                            ecomm.Visible = false;
                            vfq.Visible = true;
                            hmc.Visible = false;
                            vftechnical.Visible = false;
                        }
                        else if (CkhUserType == "vftechnical")
                        {
                            if (QueryParameter == "sku")
                            {
                                QueryParameter = "skutrack";
                                Response.Redirect("~/PowerOnRent/CommonReport.aspx?invoker=skutrack");
                            }
                            else
                            {
                                QueryParameter = Request.QueryString["invoker"];
                            }
                            normal.Visible = false;
                            ecomm.Visible = false;
                            vfq.Visible = false;
                            hmc.Visible = false;
                            vftechnical.Visible = true;
                        }
                        else
                        {
                            QueryParameter = Request.QueryString["invoker"];
                            normal.Visible = true;
                            ecomm.Visible = false;
                            hmc.Visible = false;
                            vfq.Visible = false;
                            vftechnical.Visible = true;
                        }
                    }
                }
                // QueryParameter = Request.QueryString["invoker"];

            }
            else
            {
                QueryParameter = Request.QueryString["invoker"];
            }
            if (QueryParameter == null) { UCCommonFilter1.GridVisible(); }

            if (QueryParameter == "partrequest")
            { lblRptName.Text = "Part Requisition Report"; }
            else if (QueryParameter == "partissue")
            { lblRptName.Text = "Part Issue Report"; }
            else if (QueryParameter == "partstock")
            { lblRptName.Text = "Part Stock Report"; }
            else if (QueryParameter == "partreceipt")
            { lblRptName.Text = "Part Receipt Report"; }
            else if (QueryParameter == "partconsumption")
            { lblRptName.Text = "Part Consumption Report"; }
            else if (QueryParameter == "monthly")
            { lblRptName.Text = "PR-Report Monthly"; }
            else if (QueryParameter == "weeklylst")
            { lblRptName.Text = "Weekly Analysis"; }
            else if (QueryParameter == "consumabletracker")
            { lblRptName.Text = "Consumable Tracker"; }
            else if (QueryParameter == "productdtl")
            { lblRptName.Text = "Product Report"; }
            else if (QueryParameter == "transfer")
            { lblRptName.Text = "Transfer Report"; }
            else if (QueryParameter == "asset")
            { lblRptName.Text = "Site Wise Asset & Equipment Report"; }
            else if (QueryParameter == "avgtime")
            { lblRptName.Text = "Delivery List Report"; }
            else if (QueryParameter == "noofdelivery")
            { lblRptName.Text = "Delivery Detail Report"; }
            else if (QueryParameter == "driverschedule")
            { lblRptName.Text = "Driver Schedule Report"; }
            else if (QueryParameter == "sku")
            {
                rptbutton.Attributes.Add("Style", "padding: 0 0 0 75%;");
                lblRptName.Text = "SKU List Report";
            }
            else if (QueryParameter == "SkuDetails")
            {
                rptbutton.Attributes.Add("Style", "padding: 0 0 0 75%;");
                lblRptName.Text = "SKU Details Report";
            }
            else if (QueryParameter == "BomDetail")
            {
                rptbutton.Attributes.Add("Style", "padding: 0 0 0 75%;");
                lblRptName.Text = "BOM Details Report";
            }
            else if (QueryParameter == "user")
            {
                lblRptName.Text = "User Report";
                rptbutton.Attributes.Add("Style", "padding: 0 0 0 87%;");
            }
            else if (QueryParameter == "order")
            {
                lblRptName.Text = "Order List Report";
                rptbutton.Attributes.Add("Style", "padding: 0 0 0 70%;");
            }
            else if (QueryParameter == "orderdetail")
            {
                lblRptName.Text = "Order Detail Report";
                rptbutton.Attributes.Add("Style", "padding: 0 0 0 70%;");
            }
            else if (QueryParameter == "orderlead")
            {
                lblRptName.Text = "Order Lead Time Report";
                rptbutton.Attributes.Add("Style", "padding: 0 0 0 70%;");
            }
            else if (QueryParameter == "imgaudit")
            {
                rptbutton.Attributes.Add("Style", "padding: 0 0 0 70%;");
                lblRptName.Text = "Image Audit Trails";
            }
            else if (QueryParameter == "orderdelivery")
            {
                rptbutton.Attributes.Add("Style", "padding: 0 0 0 70%;");
                lblRptName.Text = "Order Delivery Report";
            }
            else if (QueryParameter == "sla")
            {
                rptbutton.Attributes.Add("Style", "padding: 0 0 0 70%;");
                lblRptName.Text = "Service Level Agreement Report";
            }
            else if (QueryParameter == "totaldeliveryvstotalrequest")
            {
                rptbutton.Attributes.Add("Style", "padding: 0 0 0 70%;");
                lblRptName.Text = "Total Delivery Vs Total Request";
            }
            else if (QueryParameter == "location")
            {
                rptbutton.Attributes.Add("Style", "padding: 0 0 0 70%;");
                lblRptName.Text = "Payment Detail Report";
            }
            else if (QueryParameter == "deliverylogrpt")
            {
                rptbutton.Attributes.Add("Style", "padding: 0 0 0 70%;");
                lblRptName.Text = "Delivery Driver Log Report";
            }
            else if (QueryParameter == "bulkrpt")
            {
                rptbutton.Attributes.Add("Style", "padding: 0 0 0 70%;");
                lblRptName.Text = "CAF & Delivery Note";
            }
            else if (QueryParameter == "ecommerce1")
            {
                rptbutton.Attributes.Add("Style", "padding: 0 0 0 70%;");
                lblRptName.Text = "E-Commerce1 Report";
            }
            else if (QueryParameter == "ecommerce2")
            {
                rptbutton.Attributes.Add("Style", "padding: 0 0 0 70%;");
                lblRptName.Text = "E-Commerce2 Report";
            }
            else if (QueryParameter == "deliverynote")
            {
                rptbutton.Attributes.Add("Style", "padding: 0 0 0 70%;");
                lblRptName.Text = "Delivery Note";
            }
            else if (QueryParameter == "normalorderdeliverynote")
            {
                rptbutton.Attributes.Add("Style", "padding: 0 0 0 70%;");
                lblRptName.Text = "Normal Order Delivery Note";
            }
            else if (QueryParameter == "qnbnstock")
            {
                rptbutton.Attributes.Add("Style", "padding: 0 0 0 75%;");
                lblRptName.Text = "QNBN Stock Report";
            }
            else if (QueryParameter == "qnbnorder")
            {
                rptbutton.Attributes.Add("Style", "padding: 0 0 0 75%;");
                lblRptName.Text = "QNBN Order Report";
            }
            else if (QueryParameter == "skutrack")
            {
                rptbutton.Attributes.Add("Style", "padding: 0 0 0 75%;");
                lblRptName.Text = "SKU Tracking Report";
            }
            else if (QueryParameter == "sitetrack")
            {
                rptbutton.Attributes.Add("Style", "padding: 0 0 0 75%;");
                lblRptName.Text = "Site Tracking Report";
            }
            else if (QueryParameter == "depttrack")
            {
                rptbutton.Attributes.Add("Style", "padding: 0 0 0 75%;");
                lblRptName.Text = "Department Tracking Report";
            }
            else if (QueryParameter == "vat")
            {
                lblRptName.Text = "Vat Report";
                rptbutton.Attributes.Add("Style", "padding: 0 0 0 70%;");
            }
            //change by suraj khopade
            else if (QueryParameter == "Transaction")
            {
                lblRptName.Text = "Transaction Report";
                rptbutton.Attributes.Add("Style", "padding: 0 0 0 70%;");
            }
            //Start for RMS
            else if (QueryParameter == "ReturnCollectionReport")
            {
                lblRptName.Text = "Collection Report";
                rptbutton.Attributes.Add("Style", "padding: 0 0 0 70%;");
            }
            else if (QueryParameter == "ReceiptSummaryReport")
            {
                lblRptName.Text = "Receipt Summary Report";
                rptbutton.Attributes.Add("Style", "padding: 0 0 0 70%;");
            }
            //End for RMS
            //change by suraj khopade
            if (QueryParameter != null) UCCommonFilter1.GridVisibleTF(QueryParameter);

            changecolor(QueryParameter);

            /*New Code For Two Reports Not Showing for Requestor*/

            string UsrType = profile.Personal.UserType.ToString();
            if (UsrType == "Super Admin")
            {
                imgadt1.Visible = true;
                imgadt2.Visible = true;
                usrrpt1.Visible = true;
                usrrpt2.Visible = true;
            }
            else
            {
                imgadt1.Visible = false;
                imgadt2.Visible = false;
                usrrpt1.Visible = false;
                usrrpt2.Visible = false;
            }
            /*New Code For Two Reports Not Showing for Requestor*/
            if (QueryParameter == "SkuDetails") { btnSKUTransaction.Visible = true; }
            else { btnSKUTransaction.Visible = false; }

            if (QueryParameter == "user") { btnusertransaction.Visible = true; }
            else { btnusertransaction.Visible = false; }

            if (QueryParameter == "ecommerce2") { btnexport.Visible = true; }
            else { btnexport.Visible = false; }

            //change by suraj khopade
            if (QueryParameter == "Transaction")
            {
                btnViewReport.Visible = false;
               // btnExportToPDF.Visible = true;
                //btnExportToEXCEL.Visible = true;
            }
            else
            {
               // btnExportToPDF.Visible = false;
                //btnExportToEXCEL.Visible = false;
               // btnExportToEXCEL.Visible = false;
                //btnexport.Visible = false;
                //btnusertransaction.Visible = false;
                //btnSKUTransaction.Visible = false;
                //btnViewReport.Visible = false;
            }
            //change by suraj khopade


        }


        protected void fillDataSet()
        {
            DataSet dsCmnRpt = new DataSet();
            iUCCommonFilterClient UCCommonFilterClient = new iUCCommonFilterClient();
            CustomProfile profile = CustomProfile.GetProfile();
            QueryParameter = Request.QueryString["invoker"];
            try
            {
                if (QueryParameter.ToLower() == "partconsumption")
                {
                    ObjectName = "PartConsumption";
                    Session["ReportHeading"] = "Consumption Report";
                    dsCmnRpt = UCCommonFilterClient.GetReportData(UCCommonFilter1.hfCount_lcl, UCCommonFilter1.hdnEngineSelectedRec_lcl, UCCommonFilter1.hdnProductSelectedRec_lcl, UCCommonFilter1.frmdt_lcl, UCCommonFilter1.todt_lcl, ObjectName, profile.DBConnection._constr);
                }
                else if (QueryParameter.ToLower() == "partrequest")
                {
                    ObjectName = "PartRequisition";
                    Session["ReportHeading"] = "Part Requisition Register";
                    dsCmnRpt = UCCommonFilterClient.GetRequisitionData(UCCommonFilter1.hfCount_lcl, UCCommonFilter1.hdnRequestSelectedRec_lcl, UCCommonFilter1.hdnProductSelectedRec_lcl, UCCommonFilter1.frmdt_lcl, UCCommonFilter1.todt_lcl, ObjectName, profile.DBConnection._constr);
                }
                else if (QueryParameter.ToLower() == "partissue")
                {
                    ObjectName = "PartIssue";
                    Session["ReportHeading"] = "Issue Register";
                    dsCmnRpt = UCCommonFilterClient.GetIssueData(UCCommonFilter1.hfCount_lcl, UCCommonFilter1.hdnIssueSelectedRec_lcl, UCCommonFilter1.hdnProductSelectedRec_lcl, UCCommonFilter1.frmdt_lcl, UCCommonFilter1.todt_lcl, ObjectName, profile.DBConnection._constr);
                }
                else if (QueryParameter.ToLower() == "partreceipt")
                {
                    ObjectName = "partreceipt";
                    Session["ReportHeading"] = "Receipt Register";
                    dsCmnRpt = UCCommonFilterClient.GetReceiptData(UCCommonFilter1.hfCount_lcl, UCCommonFilter1.hdnReceiptSelectedRec_lcl, UCCommonFilter1.hdnProductSelectedRec_lcl, UCCommonFilter1.frmdt_lcl, UCCommonFilter1.todt_lcl, ObjectName, profile.DBConnection._constr);
                }

                Session["ReportDS"] = dsCmnRpt;
                Session["FromDt"] = UCCommonFilter1.frmdt_lcl;
                Session["ToDt"] = UCCommonFilter1.todt_lcl;
                Session["SelObject"] = QueryParameter;

                DataSet ds = new DataSet();
                ds = dsCmnRpt;
                ds.Tables[0].TableName = "dsSiteConsumption";
                ReportDataSource rds = new ReportDataSource
                    (ds.Tables[0].TableName, ds.Tables[0]);
                //Response.Redirect("<script>window.open('../POR/Reports/ReportViewer.aspx', null, 'height=510, width=990,status= 0, resizable= 0, scrollbars=0, toolbar=0,location=0,menubar=0, screenX=0; screenY=0');</script>");
                Response.Redirect("../POR/Reports/ReportViewer.aspx");
            }
            catch (System.Exception ex)
            {
                //Login.Profile.ErrorHandling(ex, this, "CommonReport", "fillDataSet");
            }
            finally
            {
                UCCommonFilterClient.Close();
            }
        }

        protected void btnViewReport_Click(object sender, EventArgs e)
        {
            QueryParameter = Request.QueryString["invoker"];

            if (QueryParameter.ToLower() == "partrequest")
            {
                if (UCCommonFilter1.hdnRequestSelectedRec_lcl == "")
                {
                    WebMsgBox.MsgBox.Show("Please Select Request");
                }
                else if (UCCommonFilter1.hdnProductSelectedRec_lcl == "")
                {
                    WebMsgBox.MsgBox.Show("Please Select Product");
                }
                else
                {
                    fillDataSet();
                }
            }


        }

        protected void changecolor(string qrypara)
        {
            // switch (qrypara)
            {
                //case "partrequest":
                //    partrequisition.Attributes.Add("style", "color:Navy");
                //    break;
                //case "partissue":
                //    partissue.Attributes.Add("style", "color:Navy");
                //    break;
                //case "partreceipt":
                //    partreceipt.Attributes.Add("style", "color:Navy");
                //    break;
                //case "rptsku":
                //    partreceipt.Attributes.Add("style", "color:Navy");
                //    break;
                //case "rptuser":
                //    partreceipt.Attributes.Add("style", "color:Navy");
                //    break;
                //case "rptorder":
                //    partreceipt.Attributes.Add("style", "color:Navy");
                //    break;

                //old
                //case "partconsumption":
                //  partconsumption.Attributes.Add("style", "color:Navy");
                //break;
                //case "monthly":
                //  monthly.Attributes.Add("style", "color:Navy");
                //break;
                //case "weeklylst":
                //  weeklylst.Attributes.Add("style", "color:Navy");
                //break;
                //case "consumabletracker":
                //  consumabletracker.Attributes.Add("style", "color:Navy");
                //break;
                // case "productdtl":
                //   productdtl.Attributes.Add("style", "color:Navy");
                // break;
            }
        }

        [WebMethod]
        public static int WMGetReportData(string invoker, string SeletedParts, string SeletedRefIDs, string FromDt, string ToDt, string Site, string ChkAll, string ChkPrd)
        {
            int result = 0;
            DataSet dsCmnRpt = new DataSet();
            iUCCommonFilterClient UCCommonFilterClient = new iUCCommonFilterClient();
            CustomProfile profile = CustomProfile.GetProfile();

            if (invoker.ToLower() == "partconsumption")
            {
                HttpContext.Current.Session["ReportHeading"] = "Consumption Report";
                if (ChkAll != "1" && ChkPrd != "1")
                {
                    dsCmnRpt = UCCommonFilterClient.GetReportData(Site, SeletedRefIDs, SeletedParts, FromDt, ToDt, "", profile.DBConnection._constr);
                }
                else if (ChkAll == "1" && ChkPrd == "1")
                {
                    dsCmnRpt = UCCommonFilterClient.GetAllReportData(Site, FromDt, ToDt, profile.DBConnection._constr);
                }
                else if (ChkAll == "1" && ChkPrd != "1")
                {
                    //  dsCmnRpt = UCCommonFilterClient.GetReportDataAllEngine(Site, SeletedRefIDs, SeletedParts, FromDt, ToDt, "", profile.DBConnection._constr);
                }
                else if (ChkAll != "1" && ChkPrd == "1")
                {
                    //    dsCmnRpt = UCCommonFilterClient.GetReportDataAllPrd(Site, SeletedRefIDs, SeletedParts, FromDt, ToDt, "", profile.DBConnection._constr);
                }

            }
            else if (invoker.ToLower() == "partrequest")
            {
                HttpContext.Current.Session["ReportHeading"] = "Part Requisition Register";
                if (ChkAll != "1" && ChkPrd != "1")
                {
                    dsCmnRpt = UCCommonFilterClient.GetRequisitionData(Site, SeletedRefIDs, SeletedParts, FromDt, ToDt, "", profile.DBConnection._constr);
                }
                else if (ChkAll == "1" && ChkPrd == "1")
                {
                    dsCmnRpt = UCCommonFilterClient.GetAllRequisitionData(Site, FromDt, ToDt, profile.DBConnection._constr);
                }
                else if (ChkAll == "1" && ChkPrd != "1")
                {
                    dsCmnRpt = UCCommonFilterClient.GetAllRequisitionDataAllRequest(Site, SeletedRefIDs, SeletedParts, FromDt, ToDt, "", profile.DBConnection._constr);
                }
                else if (ChkAll != "1" && ChkPrd == "1")
                {
                    dsCmnRpt = UCCommonFilterClient.GetAllRequisitionDataAllPrd(Site, SeletedRefIDs, SeletedParts, FromDt, ToDt, "", profile.DBConnection._constr);
                }
            }
            else if (invoker.ToLower() == "partissue")
            {
                HttpContext.Current.Session["ReportHeading"] = "Issue Register";
                if (ChkAll != "1" && ChkPrd != "1")
                {
                    dsCmnRpt = UCCommonFilterClient.GetIssueData(Site, SeletedRefIDs, SeletedParts, FromDt, ToDt, "", profile.DBConnection._constr);
                }
                else if (ChkAll == "1" && ChkPrd == "1")
                {
                    dsCmnRpt = UCCommonFilterClient.GetAllIssueData(Site, FromDt, ToDt, profile.DBConnection._constr);
                }
                else if (ChkAll == "1" && ChkPrd != "1")
                {
                    dsCmnRpt = UCCommonFilterClient.GetIssueDataAllIssue(Site, SeletedRefIDs, SeletedParts, FromDt, ToDt, "", profile.DBConnection._constr);
                }
                else if (ChkAll != "1" && ChkPrd == "1")
                {
                    dsCmnRpt = UCCommonFilterClient.GetIssueDataAllPrd(Site, SeletedRefIDs, SeletedParts, FromDt, ToDt, "", profile.DBConnection._constr);
                }
            }
            else if (invoker.ToLower() == "partreceipt")
            {
                HttpContext.Current.Session["ReportHeading"] = "Receipt Register";
                if (ChkAll != "1" && ChkPrd != "1")
                {
                    dsCmnRpt = UCCommonFilterClient.GetReceiptData(Site, SeletedRefIDs, SeletedParts, FromDt, ToDt, "", profile.DBConnection._constr);
                }
                else if (ChkAll == "1" && ChkPrd == "1")
                {
                    dsCmnRpt = UCCommonFilterClient.GetAllReceiptData(Site, FromDt, ToDt, profile.DBConnection._constr);
                }
                else if (ChkAll == "1" && ChkPrd != "1")
                {
                    dsCmnRpt = UCCommonFilterClient.GetReceiptDataAllReceipt(Site, SeletedRefIDs, SeletedParts, FromDt, ToDt, "", profile.DBConnection._constr);
                }
                else if (ChkAll != "1" && ChkPrd == "1")
                {
                    dsCmnRpt = UCCommonFilterClient.GetReceiptDataAllPrd(Site, SeletedRefIDs, SeletedParts, FromDt, ToDt, "", profile.DBConnection._constr);
                }
            }
            else if (invoker.ToLower() == "monthly")
            {
                HttpContext.Current.Session["ReportHeading"] = "PR-Report Monthly";
                dsCmnRpt = UCCommonFilterClient.GetPenComRequestData(Site, FromDt, ToDt, profile.DBConnection._constr);
            }
            else if (invoker.ToLower() == "weeklylst")
            {
                HttpContext.Current.Session["ReportHeading"] = "Weekly Analysis";
                dsCmnRpt = UCCommonFilterClient.GetWeeklyConsumption(Site, FromDt, ToDt, profile.DBConnection._constr);
            }
            else if (invoker.ToLower() == "consumabletracker")
            {
                HttpContext.Current.Session["ReportHeading"] = "Consumable Tracker";
                dsCmnRpt = UCCommonFilterClient.GetConsumableStock(SeletedRefIDs, Site, FromDt, ToDt, profile.DBConnection._constr);
            }

            else if (invoker.ToLower() == "productdtl")
            {
                HttpContext.Current.Session["ReportHeading"] = "productdtl";
                dsCmnRpt = UCCommonFilterClient.GetProductBalanceOfSite(Site, SeletedParts, ChkPrd, ChkAll, profile.DBConnection._constr);
            }

            else if (invoker.ToLower() == "transfer")
            {
                HttpContext.Current.Session["ReportHeading"] = "transfer";
                dsCmnRpt = UCCommonFilterClient.GetTransferRptData(SeletedParts, SeletedRefIDs, FromDt, ToDt, profile.DBConnection._constr);
            }

            else if (invoker.ToLower() == "asset")
            {
                HttpContext.Current.Session["ReportHeading"] = "asset";
                dsCmnRpt = UCCommonFilterClient.GetAssetRptData(SeletedParts, SeletedRefIDs, FromDt, ToDt, profile.DBConnection._constr);
            }

            dsCmnRpt.Tables[0].TableName = "dsSiteConsumption";
            HttpContext.Current.Session["ReportDS"] = dsCmnRpt;
            HttpContext.Current.Session["FromDt"] = FromDt;
            HttpContext.Current.Session["ToDt"] = ToDt;
            HttpContext.Current.Session["SelObject"] = invoker;


            if (invoker.ToLower() == "partconsumption")
            {
                int EngCount;

                if (ChkAll == "1")
                {
                    EngCount = UCCommonFilterClient.GetEngineCountAll(profile.DBConnection._constr);
                    HttpContext.Current.Session["Generator"] = EngCount;
                }
                else
                {
                    EngCount = UCCommonFilterClient.GetEngineCount(SeletedRefIDs, profile.DBConnection._constr);
                    HttpContext.Current.Session["Generator"] = EngCount;
                }
            }

            //DataSet ds = new DataSet();
            //ds = dsCmnRpt;
            //ds.Tables[0].TableName = "dsSiteConsumption";
            //ReportDataSource rds = new ReportDataSource(ds.Tables[0].TableName, ds.Tables[0]);
            result = Convert.ToInt16(dsCmnRpt.Tables[0].Rows.Count);
            return result;
        }

        [WebMethod]
        public static List<mTerritory> WMGetFromSite(long FrmSiteID)
        {
            List<mTerritory> SiteLst = new List<mTerritory>();
            iUCCommonFilterClient UCCommonFilter = new iUCCommonFilterClient();
            CustomProfile profile = CustomProfile.GetProfile();

            SiteLst = UCCommonFilter.GetToSiteName_Transfer(FrmSiteID, profile.DBConnection._constr).ToList();

            return SiteLst;
        }


        [WebMethod]
        public static int WMGetGWCQNBNSKUReportData(string invoker, string SelectedProducts, string SelectedCompany, string SelectedDepartment, string SelectedGroupSet, string SelectedImage, string AllSkU, string WithZero)
        {
            int result = 0;
            DataSet dsCmnRpt = new DataSet();

            iUCCommonFilterClient UCCommonFilterClient = new iUCCommonFilterClient();
            CustomProfile profile = CustomProfile.GetProfile();

            if (WithZero == "") { WithZero = "1"; }
            if (SelectedImage == "1") SelectedImage = "";

            if (invoker.ToLower() == "qnbnstock")
            {
                HttpContext.Current.Session["ReportHeading"] = "SKU Report";
                if (AllSkU == "1")
                {
                    //add by suraj
                    if (SelectedCompany == "0" || SelectedCompany == "") { SelectedDepartment = "0"; }
                    //dsCmnRpt = UCCommonFilterClient.GetAllSKUData(SelectedCompany, SelectedDepartment, SelectedGroupSet, profile.DBConnection._constr);
                    dsCmnRpt = UCCommonFilterClient.GetAllSKUData(SelectedCompany, SelectedDepartment, SelectedGroupSet, SelectedImage, WithZero, profile.DBConnection._constr);
                }
                else if (AllSkU != "1")
                {
                    dsCmnRpt = UCCommonFilterClient.GetAllSKUDataSelectedRow(SelectedProducts, SelectedImage, WithZero, profile.DBConnection._constr);
                }
            }
            dsCmnRpt.Tables[0].TableName = "QNBNdsSku";
            if (dsCmnRpt.Tables[0].Rows.Count > 0)
            {
                //string Company = dsCmnRpt.Tables[0].Rows[0]["CompanyName"].ToString();
                // string Department = dsCmnRpt.Tables[0].Rows[0]["Territory"].ToString() + "-" + dsCmnRpt.Tables[0].Rows[0]["StoreCode"].ToString();
                if (SelectedCompany == "0" || SelectedCompany == "") { SelectedCompany = "All Company"; SelectedDepartment = "0"; } else { SelectedCompany = dsCmnRpt.Tables[0].Rows[0]["CompanyName"].ToString(); }
                if (SelectedDepartment == "0" || SelectedDepartment == "") { SelectedDepartment = "All Department"; } else { SelectedDepartment = dsCmnRpt.Tables[0].Rows[0]["Territory"].ToString() + "-" + dsCmnRpt.Tables[0].Rows[0]["StoreCode"].ToString(); }
                if (SelectedGroupSet == "0" || SelectedGroupSet == "") { SelectedGroupSet = "Yes / No"; }

                string Image = SelectedImage;
                if (SelectedImage == "1")
                { Image = "Yes / No"; }
                else if (SelectedImage == "") { Image = "Yes / No"; }
                HttpContext.Current.Session["ReportDS"] = dsCmnRpt;
                HttpContext.Current.Session["Company"] = SelectedCompany;
                HttpContext.Current.Session["Department"] = SelectedDepartment;
                HttpContext.Current.Session["BOM"] = SelectedGroupSet;
                HttpContext.Current.Session["Image"] = Image;
                HttpContext.Current.Session["SelObject"] = invoker;
                //string imagePath = new Uri("c:\\RAH1012-18-18.png").AbsoluteUri;
                string imagePath = dsCmnRpt.Tables[0].Rows[0]["Path"].ToString();
                HttpContext.Current.Session["ImagePath"] = imagePath;
                result = Convert.ToInt16(dsCmnRpt.Tables[0].Rows.Count);
            }
            else { result = 0; }
            return result;
        }

        [WebMethod]
        public static int WMGetGWCQNBNOrderReportData(string invoker, string FromDate, string ToDate, string SelectedOrder, string SelectedProduct, string SelectedCompany, string SelectedDepartment, string AllOrder, string AllProduct, string SelectedUser, string SelectedStatus)
        {
            int result = 0;
            DataSet dsCmnRpt = new DataSet();
            iUCCommonFilterClient UCCommonFilterClient = new iUCCommonFilterClient();
            CustomProfile profile = CustomProfile.GetProfile();
            string frmdt_lcl, todt_lcl;
            frmdt_lcl = Convert.ToDateTime(FromDate).Date.ToString("yyyy/MM/dd"); // Value.ToString("yyyy/MM/dd");
            todt_lcl = Convert.ToDateTime(ToDate).Date.ToString("yyyy/MM/dd"); //.Value.ToString("yyyy/MM/dd"); 
            string comid = profile.Personal.CompanyID.ToString();
            string utype = profile.Personal.UserType.ToString();

            if (invoker.ToLower() == "qnbnorder")
            {
                HttpContext.Current.Session["ReportHeading"] = "Order Report";
                if (AllOrder == "1")
                {
                    if (SelectedCompany == "0" || SelectedCompany == "") { SelectedDepartment = "0"; }
                    if (utype == "Super Admin")
                    { dsCmnRpt = UCCommonFilterClient.AllOrderReports(FromDate, ToDate, SelectedCompany, SelectedDepartment, SelectedUser, SelectedStatus, profile.DBConnection._constr); }
                    else
                    {
                        SelectedCompany = comid;
                        dsCmnRpt = UCCommonFilterClient.AllOrderReports(FromDate, ToDate, SelectedCompany, SelectedDepartment, SelectedUser, SelectedStatus, profile.DBConnection._constr);
                    }

                }
                else if (AllOrder != "1")
                {
                    dsCmnRpt = UCCommonFilterClient.GetAllOrderDataSelectedRow(SelectedOrder, profile.DBConnection._constr);
                }

            }
            dsCmnRpt.Tables[0].TableName = "QNBNdsOrder";
            if (dsCmnRpt.Tables[0].Rows.Count > 0)
            {
                if (SelectedCompany == "0" || SelectedCompany == "") { SelectedCompany = "All"; } else { SelectedCompany = dsCmnRpt.Tables[0].Rows[0]["CompanyName"].ToString(); }
                // string Company = dsCmnRpt.Tables[0].Rows[0]["CompanyName"].ToString();
                //  if (SelectedCompany == "0") Company = "All";
                if (SelectedCompany == "0" || SelectedDepartment == "") { SelectedDepartment = "All"; } else { SelectedDepartment = dsCmnRpt.Tables[0].Rows[0]["Territory"].ToString() + "-" + dsCmnRpt.Tables[0].Rows[0]["StoreCode"].ToString(); }
                //  string Department = dsCmnRpt.Tables[0].Rows[0]["Territory"].ToString() + "-" + dsCmnRpt.Tables[0].Rows[0]["StoreCode"].ToString();
                //   if (SelectedDepartment == "0") Department = "All";
                if (SelectedUser == "0" || SelectedUser == "") { SelectedUser = "All"; } else { SelectedUser = dsCmnRpt.Tables[0].Rows[0]["UserName"].ToString(); }
                //string User = dsCmnRpt.Tables[0].Rows[0]["UserName"].ToString();
                //if (SelectedUser == "0") User = "All";
                if (SelectedStatus == "0" || SelectedStatus == "") { SelectedStatus = "All"; } else { SelectedStatus = dsCmnRpt.Tables[0].Rows[0]["StatusName"].ToString(); }
                //  string Status = dsCmnRpt.Tables[0].Rows[0]["StatusName"].ToString();
                //  if (SelectedStatus == "0") Status = "All";


                HttpContext.Current.Session["ReportDS"] = dsCmnRpt;
                HttpContext.Current.Session["FromDt"] = frmdt_lcl;
                HttpContext.Current.Session["ToDt"] = todt_lcl;
                HttpContext.Current.Session["Company"] = SelectedCompany;
                HttpContext.Current.Session["Department"] = SelectedDepartment;
                HttpContext.Current.Session["Status"] = SelectedStatus;
                HttpContext.Current.Session["User"] = SelectedUser;

                HttpContext.Current.Session["SelObject"] = invoker;
                result = Convert.ToInt16(dsCmnRpt.Tables[0].Rows.Count);
            }
            else { result = 0; }
            return result;
        }


        [WebMethod]
        public static int WMGetGWCSKUReportData(string invoker, string SelectedProducts, string SelectedCompany, string SelectedDepartment, string SelectedGroupSet, string SelectedImage, string AllSkU, string WithZero)
        {
            int result = 0;
            DataSet dsCmnRpt = new DataSet();

            iUCCommonFilterClient UCCommonFilterClient = new iUCCommonFilterClient();
            CustomProfile profile = CustomProfile.GetProfile();

            if (WithZero == "") { WithZero = "1"; }
            if (SelectedImage == "1") SelectedImage = "";

            if (invoker.ToLower() == "sku")
            {
                HttpContext.Current.Session["ReportHeading"] = "SKU Report";
                if (AllSkU == "1")
                {
                    //add by suraj
                    if (SelectedCompany == "0" || SelectedCompany == "") { SelectedDepartment = "0"; }
                    //dsCmnRpt = UCCommonFilterClient.GetAllSKUData(SelectedCompany, SelectedDepartment, SelectedGroupSet, profile.DBConnection._constr);
                    dsCmnRpt = UCCommonFilterClient.GetAllSKUData(SelectedCompany, SelectedDepartment, SelectedGroupSet, SelectedImage, WithZero, profile.DBConnection._constr);
                }
                else if (AllSkU != "1")
                {
                    dsCmnRpt = UCCommonFilterClient.GetAllSKUDataSelectedRow(SelectedProducts, SelectedImage, WithZero, profile.DBConnection._constr);
                }
            }
            dsCmnRpt.Tables[0].TableName = "dsSKU";
            if (dsCmnRpt.Tables[0].Rows.Count > 0)
            {
                //string Company = dsCmnRpt.Tables[0].Rows[0]["CompanyName"].ToString();
                // string Department = dsCmnRpt.Tables[0].Rows[0]["Territory"].ToString() + "-" + dsCmnRpt.Tables[0].Rows[0]["StoreCode"].ToString();
                if (SelectedCompany == "0" || SelectedCompany == "") { SelectedCompany = "All Company"; SelectedDepartment = "0"; } else { SelectedCompany = dsCmnRpt.Tables[0].Rows[0]["CompanyName"].ToString(); }
                if (SelectedDepartment == "0" || SelectedDepartment == "") { SelectedDepartment = "All Department"; } else { SelectedDepartment = dsCmnRpt.Tables[0].Rows[0]["Territory"].ToString() + "-" + dsCmnRpt.Tables[0].Rows[0]["StoreCode"].ToString(); }
                if (SelectedGroupSet == "0" || SelectedGroupSet == "") { SelectedGroupSet = "Yes / No"; }

                string Image = SelectedImage;
                if (SelectedImage == "1")
                { Image = "Yes / No"; }
                else if (SelectedImage == "") { Image = "Yes / No"; }
                HttpContext.Current.Session["ReportDS"] = dsCmnRpt;
                HttpContext.Current.Session["Company"] = SelectedCompany;
                HttpContext.Current.Session["Department"] = SelectedDepartment;
                HttpContext.Current.Session["BOM"] = SelectedGroupSet;
                HttpContext.Current.Session["Image"] = Image;
                HttpContext.Current.Session["SelObject"] = invoker;
                //string imagePath = new Uri("c:\\RAH1012-18-18.png").AbsoluteUri;
                string imagePath = dsCmnRpt.Tables[0].Rows[0]["Path"].ToString();
                HttpContext.Current.Session["ImagePath"] = imagePath;
                result = Convert.ToInt16(dsCmnRpt.Tables[0].Rows.Count);
            }
            else { result = 0; }
            return result;
        }




        [WebMethod]
        public static int WMGetGWCUserReportData(string invoker, string SelectedUser, string SelectedCompany, string SelectedDepartment, string AllUser, string Role, string Active)
        {
            int result = 0;
            DataSet dsCmnRpt = new DataSet();
            iUCCommonFilterClient UCCommonFilterClient = new iUCCommonFilterClient();
            CustomProfile profile = CustomProfile.GetProfile();

            if (invoker.ToLower() == "user")
            {
                HttpContext.Current.Session["ReportHeading"] = "User Report";
                if (AllUser == "1")
                {
                    if (SelectedCompany == "0" || SelectedCompany == "") { SelectedDepartment = "0"; }
                    dsCmnRpt = UCCommonFilterClient.GetAllUserData(SelectedCompany, SelectedDepartment, Role, Active, profile.DBConnection._constr);
                }
                else if (AllUser != "1")
                {
                    dsCmnRpt = UCCommonFilterClient.GetAllUserDataSelectedRow(SelectedUser, profile.DBConnection._constr);
                }

            }
            dsCmnRpt.Tables[0].TableName = "dsUser";
            if (dsCmnRpt.Tables[0].Rows.Count > 0)
            {
                if (SelectedCompany == "0" || SelectedCompany == "")
                {
                    SelectedCompany = "All";
                }
                else
                {
                    SelectedCompany = dsCmnRpt.Tables[0].Rows[0]["Company"].ToString();
                }
                if (SelectedDepartment == "0" || SelectedDepartment == "")
                {
                    SelectedDepartment = "All";
                }
                else
                {
                    SelectedDepartment = dsCmnRpt.Tables[0].Rows[0]["Department"].ToString() + "-" + dsCmnRpt.Tables[0].Rows[0]["StoreCode"].ToString();
                }
                if (Role == "0" || Role == "")
                {
                    Role = "All";
                }
                else
                {
                    Role = dsCmnRpt.Tables[0].Rows[0]["RoleName"].ToString();
                }

                if (Active == "0" || Active == "")
                {
                    Active = "All";
                }
                else
                {
                    Active = dsCmnRpt.Tables[0].Rows[0]["Active"].ToString();
                }
                // string Company = dsCmnRpt.Tables[0].Rows[0]["Company"].ToString();
                //  if (Company == "0" || Company=="") Company = "All";
                // string Department = dsCmnRpt.Tables[0].Rows[0]["Department"].ToString() + "-" + dsCmnRpt.Tables[0].Rows[0]["StoreCode"].ToString();
                //  if (Department == "0") Department = "All";
                // string RoleName = dsCmnRpt.Tables[0].Rows[0]["RoleName"].ToString();
                //   if (RoleName == "0") RoleName = "All";
                //   string Active1 = dsCmnRpt.Tables[0].Rows[0]["Active"].ToString();


                HttpContext.Current.Session["ReportDS"] = dsCmnRpt;
                HttpContext.Current.Session["Company"] = SelectedCompany;
                HttpContext.Current.Session["Department"] = SelectedDepartment;
                HttpContext.Current.Session["RoleName"] = Role;
                HttpContext.Current.Session["Active"] = Active;

                HttpContext.Current.Session["SelObject"] = invoker;
                result = Convert.ToInt16(dsCmnRpt.Tables[0].Rows.Count);
            }
            else { result = 0; }
            return result;
        }


        [WebMethod]
        public static int WMGetGWCOrderReportData(string invoker, string FromDate, string ToDate, string SelectedOrder, string SelectedProduct, string SelectedCompany, string SelectedDepartment, string AllOrder, string AllProduct, string SelectedUser, string SelectedStatus)
        {
            int result = 0;
            DataSet dsCmnRpt = new DataSet();
            iUCCommonFilterClient UCCommonFilterClient = new iUCCommonFilterClient();
            CustomProfile profile = CustomProfile.GetProfile();

            string frmdt_lcl, todt_lcl;
            frmdt_lcl = Convert.ToDateTime(FromDate).Date.ToString("yyyy/MM/dd"); // Value.ToString("yyyy/MM/dd");
            todt_lcl = Convert.ToDateTime(ToDate).Date.ToString("yyyy/MM/dd"); //.Value.ToString("yyyy/MM/dd"); 
            string usertype = profile.Personal.UserType.ToString();
            string companyid = profile.Personal.CompanyID.ToString();

            if (invoker.ToLower() == "order")
            {
                HttpContext.Current.Session["ReportHeading"] = "Order Report";
                if (AllOrder == "1")
                {
                    if (SelectedCompany == "0" || SelectedCompany == "") { SelectedDepartment = "0"; }
                    if (usertype == "Super Admin")
                    {
                        dsCmnRpt = UCCommonFilterClient.AllOrderReports(FromDate, ToDate, SelectedCompany, SelectedDepartment, SelectedUser, SelectedStatus, profile.DBConnection._constr);
                    }
                    else
                    {
                        SelectedCompany = companyid;
                        dsCmnRpt = UCCommonFilterClient.AllOrderReports(FromDate, ToDate, SelectedCompany, SelectedDepartment, SelectedUser, SelectedStatus, profile.DBConnection._constr);
                    }

                }
                else if (AllOrder != "1")
                {
                    dsCmnRpt = UCCommonFilterClient.GetAllOrderDataSelectedRow(SelectedOrder, profile.DBConnection._constr);
                }

            }
            dsCmnRpt.Tables[0].TableName = "dsOrder";
            if (dsCmnRpt.Tables[0].Rows.Count > 0)
            {
                if (SelectedCompany == "0" || SelectedCompany == "") { SelectedCompany = "All"; } else { SelectedCompany = dsCmnRpt.Tables[0].Rows[0]["CompanyName"].ToString(); }
                // string Company = dsCmnRpt.Tables[0].Rows[0]["CompanyName"].ToString();
                //  if (SelectedCompany == "0") Company = "All";
                if (SelectedCompany == "0" || SelectedDepartment == "") { SelectedDepartment = "All"; } else { SelectedDepartment = dsCmnRpt.Tables[0].Rows[0]["Territory"].ToString() + "-" + dsCmnRpt.Tables[0].Rows[0]["StoreCode"].ToString(); }
                //  string Department = dsCmnRpt.Tables[0].Rows[0]["Territory"].ToString() + "-" + dsCmnRpt.Tables[0].Rows[0]["StoreCode"].ToString();
                //   if (SelectedDepartment == "0") Department = "All";
                if (SelectedUser == "0" || SelectedUser == "") { SelectedUser = "All"; } else { SelectedUser = dsCmnRpt.Tables[0].Rows[0]["UserName"].ToString(); }
                //string User = dsCmnRpt.Tables[0].Rows[0]["UserName"].ToString();
                //if (SelectedUser == "0") User = "All";
                if (SelectedStatus == "0" || SelectedStatus == "") { SelectedStatus = "All"; } else { SelectedStatus = dsCmnRpt.Tables[0].Rows[0]["StatusName"].ToString(); }
                //  string Status = dsCmnRpt.Tables[0].Rows[0]["StatusName"].ToString();
                //  if (SelectedStatus == "0") Status = "All";


                HttpContext.Current.Session["ReportDS"] = dsCmnRpt;
                HttpContext.Current.Session["FromDt"] = frmdt_lcl;
                HttpContext.Current.Session["ToDt"] = todt_lcl;
                HttpContext.Current.Session["Company"] = SelectedCompany;
                HttpContext.Current.Session["Department"] = SelectedCompany;
                HttpContext.Current.Session["Status"] = SelectedStatus;
                HttpContext.Current.Session["User"] = SelectedUser;

                HttpContext.Current.Session["SelObject"] = invoker;
                result = Convert.ToInt16(dsCmnRpt.Tables[0].Rows.Count);
            }
            else { result = 0; }
            return result;
        }

        [WebMethod]
        public static int WMGetGWCPaymentDetailReport(string invoker, string FromDate, string ToDate, string SelectedOrder, string SelectedProduct, string SelectedCompany, string SelectedDepartment, string AllOrder, string AllProduct, string SelectedLocation, string SelectedPaymentMode, string IncludeEcommerce)
        {
            int result = 0;
            DataSet dsCmnRpt = new DataSet();
            iUCCommonFilterClient UCCommonFilterClient = new iUCCommonFilterClient();
            CustomProfile profile = CustomProfile.GetProfile();

            string frmdt_lcl, todt_lcl;
            frmdt_lcl = Convert.ToDateTime(FromDate).Date.ToString("yyyy/MM/dd");
            todt_lcl = Convert.ToDateTime(ToDate).Date.ToString("yyyy/MM/dd");

            HttpContext.Current.Session["ReportHeading"] = "Payment Detail Report";
            if (AllOrder == "1")
            {
                if (SelectedCompany == "0" || SelectedCompany == "") { SelectedDepartment = "0"; }
                dsCmnRpt = UCCommonFilterClient.GetAllOrderLocationPaymentModeRpt(FromDate, ToDate, SelectedCompany, SelectedDepartment, SelectedLocation, SelectedPaymentMode, IncludeEcommerce, profile.DBConnection._constr);

            }
            else if (AllOrder != "1")
            {
                dsCmnRpt = UCCommonFilterClient.GetAllOrderLocationPMode(SelectedOrder, profile.DBConnection._constr);
            }
            dsCmnRpt.Tables[0].TableName = "dsLocation";
            if (dsCmnRpt.Tables[0].Rows.Count > 0)
            {

                if (SelectedCompany == "0" || SelectedCompany == "") { SelectedCompany = "All"; SelectedDepartment = "0"; } else { SelectedCompany = dsCmnRpt.Tables[0].Rows[0]["CompanyName"].ToString(); }
                //  string Department = dsCmnRpt.Tables[0].Rows[0]["Territory"].ToString() + "-" + dsCmnRpt.Tables[0].Rows[0]["StoreCode"].ToString();
                if (SelectedDepartment == "0" || SelectedDepartment == "") { SelectedDepartment = "All"; } else { SelectedDepartment = dsCmnRpt.Tables[0].Rows[0]["Territory"].ToString() + "-" + dsCmnRpt.Tables[0].Rows[0]["StoreCode"].ToString(); }
                if (SelectedLocation == "0" || SelectedLocation == "") { SelectedLocation = "All"; } else { SelectedLocation = dsCmnRpt.Tables[0].Rows[0]["LocationCode"].ToString(); }
                // string location = dsCmnRpt.Tables[0].Rows[0]["LocationCode"].ToString();//
                // if (SelectedLocation == "0" || SelectedLocation == "") location = "All";
                //  string paymentMode = dsCmnRpt.Tables[0].Rows[0]["PaymentMethod"].ToString();//
                if (SelectedPaymentMode == "0" || SelectedPaymentMode == "") { SelectedPaymentMode = "All"; } else { SelectedPaymentMode = dsCmnRpt.Tables[0].Rows[0]["PaymentMethod"].ToString(); }

                HttpContext.Current.Session["ReportDS"] = dsCmnRpt;
                HttpContext.Current.Session["FromDt"] = frmdt_lcl;
                HttpContext.Current.Session["ToDt"] = todt_lcl;
                HttpContext.Current.Session["Company"] = SelectedCompany;
                HttpContext.Current.Session["Department"] = SelectedDepartment;
                HttpContext.Current.Session["location"] = SelectedLocation;
                HttpContext.Current.Session["paymentMode"] = SelectedPaymentMode;

                HttpContext.Current.Session["SelObject"] = invoker;
                result = Convert.ToInt16(dsCmnRpt.Tables[0].Rows.Count);
            }
            else { result = 0; }
            return result;

        }

        [WebMethod]
        public static int WMGetGWCOrderLeadReportData(string invoker, string FromDate, string ToDate, string SelectedOrder, string SelectedProduct, string SelectedCompany, string SelectedDepartment, string AllOrder, string AllProduct, string SelectedUser, string SelectedStatus)
        {
            int result = 0;
            DataSet dsCmnRpt = new DataSet();
            iUCCommonFilterClient UCCommonFilterClient = new iUCCommonFilterClient();
            CustomProfile profile = CustomProfile.GetProfile();

            string frmdt_lcl, todt_lcl;
            frmdt_lcl = Convert.ToDateTime(FromDate).Date.ToShortDateString(); // Value.ToString("yyyy/MM/dd");
            todt_lcl = Convert.ToDateTime(ToDate).Date.ToShortDateString();
            string comid = profile.Personal.CompanyID.ToString();
            string usertypr = profile.Personal.UserType.ToString();

            if (invoker.ToLower() == "orderlead")
            {
                HttpContext.Current.Session["ReportHeading"] = "Order Lead Report";
                if (AllOrder == "1")
                {
                    if (SelectedCompany == "0" || SelectedCompany == "") { SelectedDepartment = "0"; }
                    if (usertypr == "Super Admin")
                    {
                        dsCmnRpt = dsCmnRpt = UCCommonFilterClient.GetAllOrderLeadReprtData(FromDate, ToDate, SelectedCompany, SelectedDepartment, SelectedStatus, SelectedUser, profile.DBConnection._constr);
                    }
                    else
                    {
                        SelectedCompany = comid;
                        dsCmnRpt = dsCmnRpt = UCCommonFilterClient.GetAllOrderLeadReprtData(FromDate, ToDate, SelectedCompany, SelectedDepartment, SelectedStatus, SelectedUser, profile.DBConnection._constr);
                    }
                }
                else if (AllOrder != "1")
                {
                    dsCmnRpt = UCCommonFilterClient.GetAllOrderSelectedOrderRpt(SelectedOrder, profile.DBConnection._constr);
                }

            }
            dsCmnRpt.Tables[0].TableName = "dsOrderLeadTime";
            if (dsCmnRpt.Tables[0].Rows.Count > 0)
            {
                string Company = SelectedCompany; //dsCmnRpt.Tables[0].Rows[0]["CompanyName"].ToString();
                if (Company == "0" || Company == "") { Company = "All Company"; SelectedDepartment = "0"; } else { Company = dsCmnRpt.Tables[0].Rows[0]["CompanyName"].ToString(); }
                string Department = SelectedDepartment;// dsCmnRpt.Tables[0].Rows[0]["Territory"].ToString() + "-" + dsCmnRpt.Tables[0].Rows[0]["StoreCode"].ToString();
                if (Department == "0" || Department == "") { Department = "All Department"; } else { Department = dsCmnRpt.Tables[0].Rows[0]["Territory"].ToString() + "-" + dsCmnRpt.Tables[0].Rows[0]["StoreCode"].ToString(); }
                string User = SelectedUser;//dsCmnRpt.Tables[0].Rows[0]["UserName"].ToString();
                if (User == "0" || User == "") { User = "All Users"; } else { User = dsCmnRpt.Tables[0].Rows[0]["UserName"].ToString(); }
                string Status = SelectedStatus; // dsCmnRpt.Tables[0].Rows[0]["StatusName"].ToString();
                if (Status == "0" || Status == "") { Status = "All"; } else { Status = dsCmnRpt.Tables[0].Rows[0]["StatusName"].ToString(); }

                HttpContext.Current.Session["ReportDS"] = dsCmnRpt;
                HttpContext.Current.Session["FromDt"] = frmdt_lcl;
                HttpContext.Current.Session["ToDt"] = todt_lcl;
                HttpContext.Current.Session["Company"] = Company;
                HttpContext.Current.Session["Department"] = Department;
                HttpContext.Current.Session["Status"] = Status;
                HttpContext.Current.Session["User"] = User;
                HttpContext.Current.Session["SelObject"] = invoker;
                result = Convert.ToInt16(dsCmnRpt.Tables[0].Rows.Count);
            }
            else
            {
                result = 0;
            }
            return result;

        }


        [WebMethod]
        public static int WMGetGWCOrderDetailsReportData(string invoker, string FromDate, string ToDate, string SelectedOrder, string SelectedProduct, string SelectedCompany, string SelectedDepartment, string AllOrder, string AllProduct, string SelectedUser, string SelectedStatus)
        {
            int result = 0;
            DataSet dsCmnRpt = new DataSet();
            iUCCommonFilterClient UCCommonFilterClient = new iUCCommonFilterClient();
            CustomProfile profile = CustomProfile.GetProfile();

            string frmdt_lcl, todt_lcl;
            frmdt_lcl = Convert.ToDateTime(FromDate).Date.ToShortDateString(); // Value.ToString("yyyy/MM/dd");
            todt_lcl = Convert.ToDateTime(ToDate).Date.ToShortDateString();
            string usertype = profile.Personal.UserType.ToString();
            string comid = profile.Personal.CompanyID.ToString();
            if (invoker.ToLower() == "orderdetail")
            {
                HttpContext.Current.Session["ReportHeading"] = "Order Details Report";
                if (AllOrder == "1")
                {
                    if (SelectedCompany == "0" || SelectedCompany == "") { SelectedDepartment = "0"; }
                    if (usertype == "Super Admin")
                    {
                        dsCmnRpt = UCCommonFilterClient.GetOrderDetailsReprtData(SelectedCompany, SelectedDepartment, SelectedUser, SelectedStatus, profile.DBConnection._constr);
                    }
                    else
                    {
                        SelectedCompany = comid;
                        dsCmnRpt = UCCommonFilterClient.GetOrderDetailsReprtData(SelectedCompany, SelectedDepartment, SelectedUser, SelectedStatus, profile.DBConnection._constr);
                    }
                }
                else if (AllOrder != "1")
                {
                    dsCmnRpt = UCCommonFilterClient.GetAllOrderDetailsDataSelectedRow(SelectedOrder, profile.DBConnection._constr);
                }

            }
            dsCmnRpt.Tables[0].TableName = "dsOrderDetails";
            if (dsCmnRpt.Tables[0].Rows.Count > 0)
            {
                // string Company = dsCmnRpt.Tables[0].Rows[0]["CompanyName"].ToString();
                if (SelectedCompany == "0" || SelectedCompany == "") { SelectedCompany = "All"; } else { SelectedCompany = dsCmnRpt.Tables[0].Rows[0]["CompanyName"].ToString(); }
                // string Department = dsCmnRpt.Tables[0].Rows[0]["Territory"].ToString() + "-" + dsCmnRpt.Tables[0].Rows[0]["StoreCode"].ToString();
                if (SelectedDepartment == "0" || SelectedDepartment == "") { SelectedDepartment = "All"; } else { SelectedDepartment = dsCmnRpt.Tables[0].Rows[0]["Territory"].ToString() + "-" + dsCmnRpt.Tables[0].Rows[0]["StoreCode"].ToString(); }
                //  string User = dsCmnRpt.Tables[0].Rows[0]["UserName"].ToString();
                if (SelectedUser == "0" || SelectedUser == "") { SelectedUser = "All"; } else { SelectedUser = dsCmnRpt.Tables[0].Rows[0]["UserName"].ToString(); }
                // string Status = dsCmnRpt.Tables[0].Rows[0]["Status"].ToString();
                if (SelectedStatus == "0" || SelectedStatus == "") { SelectedStatus = "All"; } else { SelectedStatus = dsCmnRpt.Tables[0].Rows[0]["Status"].ToString(); }


                HttpContext.Current.Session["ReportDS"] = dsCmnRpt;
                HttpContext.Current.Session["FromDt"] = frmdt_lcl;
                HttpContext.Current.Session["ToDt"] = todt_lcl;
                HttpContext.Current.Session["Company"] = SelectedCompany;
                HttpContext.Current.Session["Department"] = SelectedDepartment;
                HttpContext.Current.Session["Status"] = SelectedStatus;
                HttpContext.Current.Session["User"] = SelectedUser;
                HttpContext.Current.Session["SelObject"] = invoker;
                result = Convert.ToInt16(dsCmnRpt.Tables[0].Rows.Count);
            }
            else
            {
                result = 0;
            }
            return result;

        }


        [WebMethod]
        public static int WMGetGWCSKUDetailsReportData(string invoker, string SelectedProducts, string SelectedCompany, string SelectedDepartment, string SelectedGroupSet, string SelectedImage, string AllSkU)
        {
            int result = 0;
            DataSet dsCmnRpt = new DataSet();
            iUCCommonFilterClient UCCommonFilterClient = new iUCCommonFilterClient();
            CustomProfile profile = CustomProfile.GetProfile();
            string usertype = profile.Personal.UserType.ToString();
            string comid = profile.Personal.CompanyID.ToString();
            if (SelectedCompany == "0") { SelectedDepartment = "0"; }
            if (invoker.ToLower() == "skudetails")
            {
                HttpContext.Current.Session["ReportHeading"] = "SKU Details Report";

                if (AllSkU == "1")
                {
                    if (SelectedCompany == "0" || SelectedCompany == "") { SelectedDepartment = "0"; }
                    if (usertype == "Super Admin")
                    {
                        dsCmnRpt = UCCommonFilterClient.GetSKUDetailsReprtData(SelectedCompany, SelectedDepartment, SelectedGroupSet, profile.DBConnection._constr);
                    }
                    else
                    {
                        SelectedCompany = comid;
                        dsCmnRpt = UCCommonFilterClient.GetSKUDetailsReprtData(SelectedCompany, SelectedDepartment, SelectedGroupSet, profile.DBConnection._constr);
                    }
                }
                else if (AllSkU != "1")
                {
                    dsCmnRpt = UCCommonFilterClient.GetSKUDetailsSelectedRow(SelectedProducts, profile.DBConnection._constr);
                }
            }
            dsCmnRpt.Tables[0].TableName = "dsSKUDetails";
            if (dsCmnRpt.Tables[0].Rows.Count > 0)
            {
                // string Company = dsCmnRpt.Tables[0].Rows[0]["CompanyName"].ToString();
                if (SelectedCompany == "0" || SelectedCompany == "") { SelectedCompany = "All"; SelectedDepartment = "0"; } else { SelectedCompany = dsCmnRpt.Tables[0].Rows[0]["CompanyName"].ToString(); }
                // string Department = dsCmnRpt.Tables[0].Rows[0]["Territory"].ToString() + "-" + dsCmnRpt.Tables[0].Rows[0]["StoreCode"].ToString();
                if (SelectedDepartment == "0" || SelectedDepartment == "") { SelectedDepartment = "All"; } else { SelectedDepartment = dsCmnRpt.Tables[0].Rows[0]["Territory"].ToString() + "-" + dsCmnRpt.Tables[0].Rows[0]["StoreCode"].ToString(); }
                // string BOM = SelectedGroupSet;
                if (SelectedGroupSet == "0" || SelectedGroupSet == "") { SelectedGroupSet = "Yes / No"; }
                string Image = SelectedImage;
                if (SelectedImage == "1" || SelectedImage == "")
                { Image = "Yes / No"; }

                HttpContext.Current.Session["ReportDS"] = dsCmnRpt;
                HttpContext.Current.Session["Company"] = SelectedCompany;
                HttpContext.Current.Session["Department"] = SelectedDepartment;
                HttpContext.Current.Session["BOM"] = SelectedGroupSet;
                HttpContext.Current.Session["Image"] = Image;
                HttpContext.Current.Session["SelObject"] = invoker;
                result = Convert.ToInt16(dsCmnRpt.Tables[0].Rows.Count);
            }
            else { result = 0; }
            return result;

        }

        [WebMethod]
        public static int WMGetGWCBOMDetailsReportData(string invoker, string SelectedProducts, string SelectedCompany, string SelectedDepartment, string SelectedGroupSet, string SelectedImage, string AllSkU)
        {
            int result = 0;
            DataSet dsCmnRpt = new DataSet();
            iUCCommonFilterClient UCCommonFilterClient = new iUCCommonFilterClient();
            CustomProfile profile = CustomProfile.GetProfile();

            if (invoker.ToLower() == "bomdetail")
            {
                HttpContext.Current.Session["ReportHeading"] = "Order Details Report";
                if (AllSkU == "1")
                {
                    if (SelectedCompany == "0" || SelectedCompany == "") { SelectedDepartment = "0"; }
                    dsCmnRpt = UCCommonFilterClient.GetBOMDetailsReprtData(SelectedCompany, SelectedDepartment, SelectedGroupSet, profile.DBConnection._constr);
                }
                else if (AllSkU != "1")
                {
                    dsCmnRpt = UCCommonFilterClient.GetBOMDetailsSelectedRow(SelectedProducts, profile.DBConnection._constr);
                }

            }
            dsCmnRpt.Tables[0].TableName = "dsBOMDetails";
            if (dsCmnRpt.Tables[0].Rows.Count > 0)
            {
                //string Company = dsCmnRpt.Tables[0].Rows[0]["CompanyName"].ToString();
                if (SelectedCompany == "0" || SelectedCompany == "") { SelectedCompany = "All"; } else { SelectedCompany = dsCmnRpt.Tables[0].Rows[0]["CompanyName"].ToString(); }
                // string Department = dsCmnRpt.Tables[0].Rows[0]["territory"].ToString() + "-" + dsCmnRpt.Tables[0].Rows[0]["StoreCode"].ToString();
                if (SelectedDepartment == "0" || SelectedDepartment == "") { SelectedDepartment = "All"; } else { SelectedDepartment = dsCmnRpt.Tables[0].Rows[0]["territory"].ToString() + "-" + dsCmnRpt.Tables[0].Rows[0]["StoreCode"].ToString(); }

                HttpContext.Current.Session["ReportDS"] = dsCmnRpt;
                HttpContext.Current.Session["Company"] = SelectedCompany;
                HttpContext.Current.Session["Department"] = SelectedDepartment;
                HttpContext.Current.Session["SelObject"] = invoker;

                result = Convert.ToInt16(dsCmnRpt.Tables[0].Rows.Count);
            }
            else
            {
                result = 0;
            }
            return result;
        }

        [WebMethod]
        public static int WMGetImageAudit(string invoker, string FromDate, string ToDate, string SelectedProducts, string SelectedCompany, string SelectedDepartment, string SelectedUser, string AllSkU, string ImgStatus)
        {
            int result = 0;
            DataSet dsCmnRpt = new DataSet();
            iUCCommonFilterClient UCCommonFilterClient = new iUCCommonFilterClient();
            CustomProfile profile = CustomProfile.GetProfile();
            if (invoker.ToLower() == "imgaudit")
            {
                HttpContext.Current.Session["ReportHeading"] = "Image Audit Trails";
                if (ImgStatus == "Success")
                {
                    if (AllSkU == "1")
                    {
                        if (SelectedCompany == "0" || SelectedCompany == "") { SelectedDepartment = "0"; }
                        dsCmnRpt = UCCommonFilterClient.GetImageAuditAllPrd(FromDate, ToDate, SelectedCompany, SelectedDepartment, SelectedUser, profile.DBConnection._constr);
                    }
                    else if (AllSkU != "1")
                    {
                        dsCmnRpt = UCCommonFilterClient.GetImageAuditSelectedPrd(FromDate, ToDate, SelectedProducts, SelectedUser, profile.DBConnection._constr);
                    }
                    dsCmnRpt.Tables[0].TableName = "dsImageAudit";
                }
                else
                {
                    if (AllSkU == "1")
                    {
                        if (SelectedCompany == "0" || SelectedCompany == "") { SelectedDepartment = "0"; }
                        dsCmnRpt = UCCommonFilterClient.GetImageAuditFail(FromDate, ToDate, SelectedCompany, SelectedDepartment, SelectedUser, profile.DBConnection._constr);
                    }
                    else
                    {
                        dsCmnRpt = UCCommonFilterClient.GetImageAuditFailSelectedProduct(FromDate, ToDate, SelectedProducts, SelectedCompany, SelectedDepartment, SelectedUser, profile.DBConnection._constr);
                    }
                    dsCmnRpt.Tables[0].TableName = "dsImageAuditFail";
                }

            }

            if (dsCmnRpt.Tables[0].Rows.Count > 0)
            {
                //string Company = dsCmnRpt.Tables[0].Rows[0]["CompanyName"].ToString();
                if (SelectedCompany == "0" || SelectedCompany == "") { SelectedCompany = "All"; } else { SelectedCompany = dsCmnRpt.Tables[0].Rows[0]["CompanyName"].ToString(); }
                //  string Department = dsCmnRpt.Tables[0].Rows[0]["territory"].ToString();
                if (SelectedDepartment == "0" || SelectedDepartment == "") { SelectedDepartment = "All"; } else { SelectedDepartment = dsCmnRpt.Tables[0].Rows[0]["territory"].ToString(); }
                // string UsrName = dsCmnRpt.Tables[0].Rows[0]["UserName"].ToString();
                if (SelectedUser == "0" || SelectedUser == "") { SelectedUser = "All"; } else { SelectedUser = dsCmnRpt.Tables[0].Rows[0]["territory"].ToString(); }
                HttpContext.Current.Session["ReportDS"] = dsCmnRpt;
                HttpContext.Current.Session["Company"] = SelectedCompany;
                HttpContext.Current.Session["Department"] = SelectedDepartment;
                HttpContext.Current.Session["ImgStatus"] = ImgStatus;
                HttpContext.Current.Session["UsrName"] = SelectedUser;
                HttpContext.Current.Session["SelObject"] = invoker;
                HttpContext.Current.Session["FrmDt"] = FromDate;
                HttpContext.Current.Session["ToDt"] = ToDate;
                result = Convert.ToInt16(dsCmnRpt.Tables[0].Rows.Count);
            }
            else
            {
                result = 0;
            }


            return result;
        }


        [WebMethod]
        public static int WMGetGWCSKUTransactionReportData(string FromDate, string ToDate, string SelectedProducts, string SelectedCompany, string SelectedDepartment, string SelectedGroupSet, string SelectedImage)
        {
            int result = 0;
            DataSet dsCmnRpt = new DataSet();
            iUCCommonFilterClient UCCommonFilterClient = new iUCCommonFilterClient();
            CustomProfile profile = CustomProfile.GetProfile();
            dsCmnRpt = UCCommonFilterClient.GetSKUTransaction(SelectedProducts, profile.DBConnection._constr);
            dsCmnRpt.Tables[0].TableName = "dsSKUTrans";
            if (dsCmnRpt.Tables[0].Rows.Count > 0)
            {
                string Company = dsCmnRpt.Tables[0].Rows[0]["CompanyName"].ToString();
                if (SelectedCompany == "0" || SelectedCompany == "") Company = "All";
                string Department = dsCmnRpt.Tables[0].Rows[0]["Territory"].ToString();
                if (SelectedDepartment == "0" || SelectedDepartment == "") Department = "All";
                string BOM = SelectedGroupSet;
                if (SelectedGroupSet == "0") BOM = "Yes / No";
                string Image = SelectedImage;
                if (SelectedImage == "1")
                { Image = "Yes / No"; }

                HttpContext.Current.Session["ReportDS"] = dsCmnRpt;
                HttpContext.Current.Session["Company"] = Company;
                HttpContext.Current.Session["Department"] = Department;
                HttpContext.Current.Session["BOM"] = BOM;
                HttpContext.Current.Session["Image"] = Image;
                HttpContext.Current.Session["SelObject"] = "SkuDetails";
                result = Convert.ToInt16(dsCmnRpt.Tables[0].Rows.Count);
            }
            else { result = 0; }
            return result;
        }

        [WebMethod]
        public static int WMGetGWCUserTransactionReportData(string hdnUserSelectedRec)
        {
            int result = 0;
            DataSet dsCmnRpt = new DataSet();
            iUCCommonFilterClient UCCommonFilterClient = new iUCCommonFilterClient();
            CustomProfile profile = CustomProfile.GetProfile();
            dsCmnRpt = UCCommonFilterClient.GetUserTransaction(hdnUserSelectedRec, profile.DBConnection._constr);
            dsCmnRpt.Tables[0].TableName = "dsUserTrans";
            if (dsCmnRpt.Tables[0].Rows.Count > 0)
            {
                HttpContext.Current.Session["Company"] = "Vodafone";
                HttpContext.Current.Session["Department"] = "Vodafone";
                HttpContext.Current.Session["RoleName"] = "User";
                HttpContext.Current.Session["Active"] = "Yes";

                HttpContext.Current.Session["ReportDS"] = dsCmnRpt;
                HttpContext.Current.Session["SelObject"] = "user";
                result = Convert.ToInt16(dsCmnRpt.Tables[0].Rows.Count);
            }
            else { result = 0; }
            return result;
        }
        /**********/
        [WebMethod]
        public static int WMGetGWCOrderDeliveryReport(string invoker, string FromDate, string ToDate, string SelectedOrder, string SelectedProduct, string SelectedCompany, string SelectedDepartment, string AllOrder, string AllProduct, string SelectedDriver, string SelectedPaymentMode)
        {
            int result = 0;


            DataSet dsCmnRpt = new DataSet();
            iUCCommonFilterClient UCCommonFilterClient = new iUCCommonFilterClient();
            CustomProfile profile = CustomProfile.GetProfile();
            string comid = profile.Personal.CompanyID.ToString();
            string usertype = profile.Personal.UserType.ToString();

            string frmdt_lcl, todt_lcl;
            frmdt_lcl = Convert.ToDateTime(FromDate).Date.ToString("yyyy/MM/dd"); // Value.ToString("yyyy/MM/dd");
            todt_lcl = Convert.ToDateTime(ToDate).Date.ToString("yyyy/MM/dd"); //.Value.ToString("yyyy/MM/dd");

            if (invoker.ToLower() == "orderdelivery")
            {
                HttpContext.Current.Session["ReportHeading"] = "Order Deilvery Report";
                if (AllOrder == "1")
                {
                    if (SelectedCompany == "0" || SelectedCompany == "") { SelectedDepartment = "0"; }
                    if (usertype == "Super Admin")
                    {
                        dsCmnRpt = UCCommonFilterClient.GetAllOrderDelivery(FromDate, ToDate, SelectedCompany, SelectedDepartment, SelectedDriver, SelectedPaymentMode, profile.DBConnection._constr);
                    }
                    else
                    {
                        SelectedCompany = comid;
                        dsCmnRpt = UCCommonFilterClient.GetAllOrderDelivery(FromDate, ToDate, SelectedCompany, SelectedDepartment, SelectedDriver, SelectedPaymentMode, profile.DBConnection._constr);
                    }

                }
                else if (AllOrder != "1")
                {
                    dsCmnRpt = UCCommonFilterClient.GetAllOrderDeliveryDataSelectedRow(SelectedOrder, profile.DBConnection._constr);
                }


            }
            dsCmnRpt.Tables[0].TableName = "dsOrderDelivery";
            if (dsCmnRpt.Tables[0].Rows.Count > 0)
            {
                string Company = dsCmnRpt.Tables[0].Rows[0]["Company"].ToString();
                if (SelectedCompany == "0") Company = "All";
                string Department = dsCmnRpt.Tables[0].Rows[0]["Department"].ToString();// +"-" + dsCmnRpt.Tables[0].Rows[0]["DepartmentID"].ToString();
                if (SelectedDepartment == "0") Department = "All";
                string DriverName = dsCmnRpt.Tables[0].Rows[0]["DriverName"].ToString();
                if (SelectedDriver == "0") DriverName = "All";
                string PaymentMode = dsCmnRpt.Tables[0].Rows[0]["PaymentMode"].ToString();
                if (SelectedPaymentMode == "0") PaymentMode = "All";


                HttpContext.Current.Session["ReportDS"] = dsCmnRpt;
                HttpContext.Current.Session["FrmDt"] = frmdt_lcl;
                HttpContext.Current.Session["ToDt"] = todt_lcl;
                HttpContext.Current.Session["Company"] = Company;
                HttpContext.Current.Session["Department"] = Department;
                HttpContext.Current.Session["DriverName"] = DriverName;
                HttpContext.Current.Session["PaymentMode"] = PaymentMode;



                HttpContext.Current.Session["SelObject"] = invoker;
                result = Convert.ToInt16(dsCmnRpt.Tables[0].Rows.Count);
            }
            else { result = 0; }

            return result;

        }

        [WebMethod]
        public static int WMGetGWCSLAReport(string invoker, string FromDate, string ToDate, string SelectedOrder, string SelectedProduct, string SelectedCompany, string SelectedDepartment, string AllOrder, string AllProduct, string SelectedStatus, string SelectedDriver, string SelectedDeliveryType)
        {
            int result = 0;
            DataSet dsCmnRpt = new DataSet();
            iUCCommonFilterClient UCCommonFilterClient = new iUCCommonFilterClient();
            CustomProfile profile = CustomProfile.GetProfile();

            string frmdt_lcl, todt_lcl;
            frmdt_lcl = Convert.ToDateTime(FromDate).Date.ToString("yyyy/MM/dd"); // Value.ToString("yyyy/MM/dd");
            todt_lcl = Convert.ToDateTime(ToDate).Date.ToString("yyyy/MM/dd"); //.Value.ToString("yyyy/MM/dd");

            if (invoker.ToLower() == "sla")
            {
                HttpContext.Current.Session["ReportHeading"] = "Service Level Agreement Report";
                if (AllOrder == "1")
                {
                    if (SelectedCompany == "0" || SelectedCompany == "") { SelectedDepartment = "0"; }
                    dsCmnRpt = UCCommonFilterClient.GetAllSlaData(FromDate, ToDate, SelectedCompany, SelectedDepartment, SelectedStatus, SelectedDriver, SelectedDeliveryType, profile.DBConnection._constr);

                }
                else if (AllOrder != "1")
                {
                    dsCmnRpt = UCCommonFilterClient.GetAllSlaDataSelectedRow(SelectedOrder, profile.DBConnection._constr);
                }

            }
            dsCmnRpt.Tables[0].TableName = "dsSla";
            if (dsCmnRpt.Tables[0].Rows.Count > 0)
            {
                string Company = dsCmnRpt.Tables[0].Rows[0]["Company"].ToString();
                if (SelectedCompany == "0") Company = "All";
                string Department = dsCmnRpt.Tables[0].Rows[0]["Department"].ToString();// +"-" + dsCmnRpt.Tables[0].Rows[0]["DepartmentID"].ToString();
                if (SelectedDepartment == "0") Department = "All";
                string Status = dsCmnRpt.Tables[0].Rows[0]["Status"].ToString();
                if (SelectedStatus == "0") Status = "All";
                string DriverName = dsCmnRpt.Tables[0].Rows[0]["DriverName"].ToString();
                if (SelectedDriver == "0") DriverName = "All";
                string DeliveryType = dsCmnRpt.Tables[0].Rows[0]["DeliveryType"].ToString();
                if (SelectedDeliveryType == "0") DeliveryType = "All";


                HttpContext.Current.Session["ReportDS"] = dsCmnRpt;
                HttpContext.Current.Session["FrmDt"] = frmdt_lcl;
                HttpContext.Current.Session["ToDt"] = todt_lcl;
                HttpContext.Current.Session["Company"] = Company;
                HttpContext.Current.Session["Department"] = Department;
                HttpContext.Current.Session["DriverName"] = DriverName;
                HttpContext.Current.Session["DeliveryType"] = DeliveryType;



                HttpContext.Current.Session["SelObject"] = invoker;
                result = Convert.ToInt16(dsCmnRpt.Tables[0].Rows.Count);
            }
            else { result = 0; }
            return result;

        }


        [WebMethod]
        public static int WMGetGWCTotalDeliveryVsTotalRequestReport(string invoker, string FromDate, string ToDate, string SelectedProducts, string SelectedCompany, string SelectedDepartment, string AllSkU)
        {
            int result = 0;
            DataSet dsCmnRpt = new DataSet();
            iUCCommonFilterClient UCCommonFilterClient = new iUCCommonFilterClient();
            CustomProfile profile = CustomProfile.GetProfile();
            if (invoker.ToLower() == "totaldeliveryvstotalrequest")
            {
                HttpContext.Current.Session["ReportHeading"] = "Total Delivery Vs Total Request";
                if (AllSkU == "1")
                {
                    if (SelectedCompany == "0" || SelectedCompany == "") { SelectedDepartment = "0"; }
                    dsCmnRpt = UCCommonFilterClient.GetToTalDeliveryVSTotalReq(FromDate, ToDate, SelectedCompany, SelectedDepartment, profile.DBConnection._constr);
                }
                else if (AllSkU != "1")
                {
                    dsCmnRpt = UCCommonFilterClient.GetToTalDeliveryVSTotalReqDataSelectedRow(FromDate, ToDate, SelectedProducts, profile.DBConnection._constr);
                }

            }

            dsCmnRpt.Tables[0].TableName = "dsTDVSTR";
            if (dsCmnRpt.Tables[0].Rows.Count > 0)
            {
                string Company = dsCmnRpt.Tables[0].Rows[0]["CompanyName"].ToString();
                if (SelectedCompany == "0" || SelectedCompany == "") Company = "All";
                string Department = dsCmnRpt.Tables[0].Rows[0]["Territory"].ToString();// +"-" + dsCmnRpt.Tables[0].Rows[0]["DepartmentID"].ToString();
                if (SelectedDepartment == "0" || SelectedDepartment == "") Department = "All";

                HttpContext.Current.Session["ReportDS"] = dsCmnRpt;
                HttpContext.Current.Session["FrmDt"] = FromDate;
                HttpContext.Current.Session["ToDt"] = ToDate;
                HttpContext.Current.Session["Company"] = Company;
                HttpContext.Current.Session["Department"] = Department;


                HttpContext.Current.Session["SelObject"] = invoker;
                result = Convert.ToInt16(dsCmnRpt.Tables[0].Rows.Count);
            }
            else { result = 0; }
            return result;
        }




        [WebMethod]
        public static int WMGetGWCDriverLog(string invoker, string FromDt, string ToDt, string SelectedOrder, string SelectedCompany, string SelectedDepartment, string SelectedStatus, string SelectedUser, string SelectedPaymentMode, string AllRecord)
        {
            int result = 0;
            DataSet dsdriverlogRpt = new DataSet();
            iUCCommonFilterClient UCCommonFilterClient = new iUCCommonFilterClient();
            CustomProfile profile = CustomProfile.GetProfile();
            string comid = profile.Personal.CompanyID.ToString();
            string utype = profile.Personal.UserType.ToString();
            if (invoker.ToLower() == "deliverylogrpt")
            {
                HttpContext.Current.Session["ReportHeading"] = "Delivery Log Report";

                if (AllRecord == "1")
                {
                    if (SelectedCompany == "0" || SelectedCompany == "") { SelectedDepartment = "0"; }
                    if (utype == "Super Admin")
                    {
                        dsdriverlogRpt = UCCommonFilterClient.AllGetDriverlogReport(FromDt, ToDt, SelectedCompany, SelectedDepartment, SelectedStatus, SelectedUser, SelectedPaymentMode, profile.DBConnection._constr);
                    }
                    else
                    {
                        SelectedCompany = comid;
                        dsdriverlogRpt = UCCommonFilterClient.AllGetDriverlogReport(FromDt, ToDt, SelectedCompany, SelectedDepartment, SelectedStatus, SelectedUser, SelectedPaymentMode, profile.DBConnection._constr);
                    }

                }
                else
                {
                    dsdriverlogRpt = UCCommonFilterClient.GetDriverlogReport(SelectedOrder, profile.DBConnection._constr);
                }

                //  dsCmnRpt = UCCommonFilterClient.GetToTalDeliveryVSTotalReq(FromDate, ToDate, SelectedCompany, SelectedDepartment, profile.DBConnection._constr);               
            }

            dsdriverlogRpt.Tables[0].TableName = "dsdriverdeliverylogrpt";
            if (dsdriverlogRpt.Tables[0].Rows.Count > 0)
            {

                if (SelectedCompany == "0" || SelectedCompany == "")
                { SelectedCompany = "All"; }
                else { SelectedCompany = UCCommonFilterClient.GetcompanyNamerpt(Convert.ToInt64(SelectedCompany), profile.DBConnection._constr); }

                if (SelectedDepartment == "0" || SelectedDepartment == "")
                { SelectedDepartment = "All"; }
                else { SelectedDepartment = UCCommonFilterClient.GetdepartmentNamerpt(Convert.ToInt64(SelectedDepartment), profile.DBConnection._constr); }

                if (SelectedStatus == "0") SelectedStatus = "All";
                if (SelectedUser == "0") SelectedUser = "All";

                if (SelectedPaymentMode == "0")
                { SelectedPaymentMode = "All"; }
                else { SelectedPaymentMode = UCCommonFilterClient.GetpaymentmodeNamerpt(Convert.ToInt64(SelectedPaymentMode), profile.DBConnection._constr); }

                HttpContext.Current.Session["ReportDS"] = dsdriverlogRpt;
                HttpContext.Current.Session["FromDt"] = FromDt;
                HttpContext.Current.Session["ToDt"] = ToDt;
                HttpContext.Current.Session["Company"] = SelectedCompany;
                HttpContext.Current.Session["Department"] = SelectedDepartment;
                HttpContext.Current.Session["Status"] = SelectedStatus;
                HttpContext.Current.Session["User"] = SelectedUser;
                HttpContext.Current.Session["PaymentMode"] = SelectedPaymentMode;

                HttpContext.Current.Session["SelObject"] = invoker;
                result = Convert.ToInt16(dsdriverlogRpt.Tables[0].Rows.Count);
            }
            else { result = 0; }
            return result;
        }



        [WebMethod]
        public static int WMGetGWCCAFDN(string invoker, string FromDt, string ToDt, string Ordercategory, string Rpttype)
        {
            int result = 0;
            //DataSet dsdriverlogRpt = new DataSet();
            //iUCCommonFilterClient UCCommonFilterClient = new iUCCommonFilterClient();
            //CustomProfile profile = CustomProfile.GetProfile();
            //string comid = profile.Personal.CompanyID.ToString();
            //string utype = profile.Personal.UserType.ToString();
            //if (invoker.ToLower() == "bulkrpt")
            //{
            //    HttpContext.Current.Session["ReportHeading"] = "Delivery Log Report";

            //    if (AllRecord == "1")
            //    {
            //        if (SelectedCompany == "0" || SelectedCompany == "") { SelectedDepartment = "0"; }
            //        if (utype == "Super Admin")
            //        {
            //            dsdriverlogRpt = UCCommonFilterClient.AllGetDriverlogReport(FromDt, ToDt, SelectedCompany, SelectedDepartment, SelectedStatus, SelectedUser, SelectedPaymentMode, profile.DBConnection._constr);
            //        }
            //        else
            //        {
            //            SelectedCompany = comid;
            //            dsdriverlogRpt = UCCommonFilterClient.AllGetDriverlogReport(FromDt, ToDt, SelectedCompany, SelectedDepartment, SelectedStatus, SelectedUser, SelectedPaymentMode, profile.DBConnection._constr);
            //        }

            //    }
            //    else
            //    {
            //        dsdriverlogRpt = UCCommonFilterClient.GetDriverlogReport(SelectedOrder, profile.DBConnection._constr);
            //    }

            //    //  dsCmnRpt = UCCommonFilterClient.GetToTalDeliveryVSTotalReq(FromDate, ToDate, SelectedCompany, SelectedDepartment, profile.DBConnection._constr);               
            //}

            //dsdriverlogRpt.Tables[0].TableName = "dsdriverdeliverylogrpt";
            //if (dsdriverlogRpt.Tables[0].Rows.Count > 0)
            //{

            //    if (SelectedCompany == "0" || SelectedCompany == "")
            //    { SelectedCompany = "All"; }
            //    else { SelectedCompany = UCCommonFilterClient.GetcompanyNamerpt(Convert.ToInt64(SelectedCompany), profile.DBConnection._constr); }

            //    if (SelectedDepartment == "0" || SelectedDepartment == "")
            //    { SelectedDepartment = "All"; }
            //    else { SelectedDepartment = UCCommonFilterClient.GetdepartmentNamerpt(Convert.ToInt64(SelectedDepartment), profile.DBConnection._constr); }

            //    if (SelectedStatus == "0") SelectedStatus = "All";
            //    if (SelectedUser == "0") SelectedUser = "All";

            //    if (SelectedPaymentMode == "0")
            //    { SelectedPaymentMode = "All"; }
            //    else { SelectedPaymentMode = UCCommonFilterClient.GetpaymentmodeNamerpt(Convert.ToInt64(SelectedPaymentMode), profile.DBConnection._constr); }

            //    HttpContext.Current.Session["ReportDS"] = dsdriverlogRpt;
            //    HttpContext.Current.Session["FromDt"] = FromDt;
            //    HttpContext.Current.Session["ToDt"] = ToDt;
            //    HttpContext.Current.Session["Company"] = SelectedCompany;
            //    HttpContext.Current.Session["Department"] = SelectedDepartment;
            //    HttpContext.Current.Session["Status"] = SelectedStatus;
            //    HttpContext.Current.Session["User"] = SelectedUser;
            //    HttpContext.Current.Session["PaymentMode"] = SelectedPaymentMode;

            //    HttpContext.Current.Session["SelObject"] = invoker;
            //    result = Convert.ToInt16(dsdriverlogRpt.Tables[0].Rows.Count);
            //}
            //else { result = 0; }
            return result;
        }

        [WebMethod]
        public static int WMGetGWCVatOrderReportData(string invoker, string FromDate, string ToDate, string SelectedOrder, string SelectedProduct, string SelectedCompany, string SelectedDepartment, string AllOrder, string AllProduct, string SelectedUser, string SelectedStatus)
        {
            int result = 0;
            DataSet dsCmnRpt = new DataSet();
            iUCCommonFilterClient UCCommonFilterClient = new iUCCommonFilterClient();
            CustomProfile profile = CustomProfile.GetProfile();

            string frmdt_lcl, todt_lcl;
            frmdt_lcl = Convert.ToDateTime(FromDate).Date.ToString("yyyy/MM/dd"); // Value.ToString("yyyy/MM/dd");
            todt_lcl = Convert.ToDateTime(ToDate).Date.ToString("yyyy/MM/dd"); //.Value.ToString("yyyy/MM/dd"); 
            string usertype = profile.Personal.UserType.ToString();
            string companyid = profile.Personal.CompanyID.ToString();

            if (invoker.ToLower() == "vat")
            {
                HttpContext.Current.Session["ReportHeading"] = "VAT Report";
                if (AllOrder == "1")
                {
                    if (SelectedCompany == "0" || SelectedCompany == "") { SelectedDepartment = "0"; }
                    if (usertype == "Super Admin")
                    {
                        dsCmnRpt = UCCommonFilterClient.AllVATOrderReports(FromDate, ToDate, SelectedCompany, SelectedDepartment, SelectedUser, SelectedStatus, profile.DBConnection._constr);
                    }
                    else
                    {
                        SelectedCompany = companyid;
                        dsCmnRpt = UCCommonFilterClient.AllVATOrderReports(FromDate, ToDate, SelectedCompany, SelectedDepartment, SelectedUser, SelectedStatus, profile.DBConnection._constr);
                    }

                }
                else if (AllOrder != "1")
                {
                    dsCmnRpt = UCCommonFilterClient.getVATOrderbyID(SelectedOrder, profile.DBConnection._constr);
                }

            }
            dsCmnRpt.Tables[0].TableName = "dsvat";
            if (dsCmnRpt.Tables[0].Rows.Count > 0)
            {
                if (SelectedCompany == "0" || SelectedCompany == "") { SelectedCompany = "All"; } else { SelectedCompany = dsCmnRpt.Tables[0].Rows[0]["CompanyName"].ToString(); }
                // string Company = dsCmnRpt.Tables[0].Rows[0]["CompanyName"].ToString();
                //  if (SelectedCompany == "0") Company = "All";
                if (SelectedCompany == "0" || SelectedDepartment == "") { SelectedDepartment = "All"; } else { SelectedDepartment = dsCmnRpt.Tables[0].Rows[0]["Territory"].ToString() + "-" + dsCmnRpt.Tables[0].Rows[0]["Storer"].ToString(); }
                //  string Department = dsCmnRpt.Tables[0].Rows[0]["Territory"].ToString() + "-" + dsCmnRpt.Tables[0].Rows[0]["StoreCode"].ToString();
                //   if (SelectedDepartment == "0") Department = "All";
                if (SelectedUser == "0" || SelectedUser == "") { SelectedUser = "All"; } else { SelectedUser = dsCmnRpt.Tables[0].Rows[0]["UserName"].ToString(); }
                //string User = dsCmnRpt.Tables[0].Rows[0]["UserName"].ToString();
                //if (SelectedUser == "0") User = "All";
                if (SelectedStatus == "0" || SelectedStatus == "") { SelectedStatus = "All"; } else { SelectedStatus = dsCmnRpt.Tables[0].Rows[0]["StatusName"].ToString(); }
                //  string Status = dsCmnRpt.Tables[0].Rows[0]["StatusName"].ToString();
                //  if (SelectedStatus == "0") Status = "All";


                HttpContext.Current.Session["ReportDS"] = dsCmnRpt;
                HttpContext.Current.Session["FromDt"] = frmdt_lcl;
                HttpContext.Current.Session["ToDt"] = todt_lcl;
                HttpContext.Current.Session["Company"] = SelectedCompany;
                HttpContext.Current.Session["Department"] = SelectedDepartment;
                HttpContext.Current.Session["Status"] = SelectedStatus;
                HttpContext.Current.Session["User"] = SelectedUser;

                HttpContext.Current.Session["SelObject"] = invoker;
                result = Convert.ToInt16(dsCmnRpt.Tables[0].Rows.Count);
            }
            else { result = 0; }
            return result;
        }

        /**********/
        #region [GWC]all report Dropdown
        [WebMethod]
        public static List<mTerritory> PMGetDepartmentList(int CompanyID)
        {
            List<mTerritory> DeptLst = new List<mTerritory>();
            iUCCommonFilterClient UCCommonFilter = new iUCCommonFilterClient();
            CustomProfile profile = CustomProfile.GetProfile();

            if (profile.Personal.UserType == "Super Admin")
            {
                DeptLst = UCCommonFilter.GetDepartmentList(CompanyID, profile.DBConnection._constr).ToList();
            }
            else
            {
                DeptLst = UCCommonFilter.GetAddedDepartmentList(CompanyID, profile.Personal.UserID, profile.DBConnection._constr).ToList();
            }
            return DeptLst;
        }


        [WebMethod]
        public static List<string> PMGetReturnDepartmentList(string SelectedReturnCustomer)
        {

            iUCCommonFilterClient UCCommonFilter = new iUCCommonFilterClient();
            iUserCreationClient usercreation = new iUserCreationClient();
            CustomProfile profile = CustomProfile.GetProfile();
            DataSet ds = new DataSet();

            ds =  usercreation.GetReturnDepartmentList(Convert.ToInt16(SelectedReturnCustomer), profile.DBConnection._constr);

            string id = ds.Tables[0].Rows[0]["id"].ToString();
            string name = ds.Tables[0].Rows[0]["Territory"].ToString();

            List<string> lstName = new List<string>();
            lstName.Add(id);
            lstName.Add(name);
            return lstName;
            
        }

        [WebMethod]
        public static List<mTerritory> PMGetAllDepartment()
        {
            List<mTerritory> DeptLst = new List<mTerritory>();
            iUCCommonFilterClient UCCommonFilter = new iUCCommonFilterClient();
            CustomProfile profile = CustomProfile.GetProfile();
            DeptLst = UCCommonFilter.GetAllDepartmentList(profile.DBConnection._constr).ToList();
            return DeptLst;
        }

        [WebMethod]
        public static List<mTerritory> PMGetAllDepartmentForRPT()
        {
            List<mTerritory> DeptLst = new List<mTerritory>();
            iUCCommonFilterClient UCCommonFilter = new iUCCommonFilterClient();
            CustomProfile profile = CustomProfile.GetProfile();
            DeptLst = UCCommonFilter.GetAddedDepartmentList(0, profile.Personal.UserID, profile.DBConnection._constr).ToList();

            return DeptLst;
        }

        [WebMethod]
        public static List<mBOMHeader> PMGetGroupsetList(int CompanyID, int DeptID)
        {
            List<mBOMHeader> GroupList = new List<mBOMHeader>();
            iUCCommonFilterClient UCCommonFilter = new iUCCommonFilterClient();
            CustomProfile profile = CustomProfile.GetProfile();
            GroupList = UCCommonFilter.GetGroupSet(CompanyID, DeptID, profile.DBConnection._constr).ToList();
            return GroupList;
        }

        [WebMethod]
        public static List<mBOMHeader> PMGetAllGroupsetList()
        {
            List<mBOMHeader> GroupList = new List<mBOMHeader>();
            iUCCommonFilterClient UCCommonFilter = new iUCCommonFilterClient();
            CustomProfile profile = CustomProfile.GetProfile();

            GroupList = UCCommonFilter.GetAllGroupsetList(profile.DBConnection._constr).ToList();

            return GroupList;
        }

        [WebMethod]
        public static List<mBOMHeader> PMGetGroupsetListByDept(int DeptID)
        {
            List<mBOMHeader> GroupList = new List<mBOMHeader>();
            iUCCommonFilterClient UCCommonFilter = new iUCCommonFilterClient();
            CustomProfile profile = CustomProfile.GetProfile();

            GroupList = UCCommonFilter.GetGroupSetByDept(DeptID, profile.DBConnection._constr).ToList();

            return GroupList;
        }


        [WebMethod]
        public static List<mBOMHeader> PMGetGroupSetListByCompanyId(int CompanyID)
        {
            List<mBOMHeader> GroupList = new List<mBOMHeader>();
            iUCCommonFilterClient UCCommonFilter = new iUCCommonFilterClient();
            CustomProfile profile = CustomProfile.GetProfile();

            GroupList = UCCommonFilter.GetGroupSetByCompany(CompanyID, profile.DBConnection._constr).ToList();

            return GroupList;
        }

        [WebMethod]
        public static List<VW_GetUserInformation> PMGetUserList(string CompanyID, string DeptID)
        {
            List<VW_GetUserInformation> UserList = new List<VW_GetUserInformation>();
            iUCCommonFilterClient UCCommonFilter = new iUCCommonFilterClient();
            CustomProfile profile = CustomProfile.GetProfile();

            UserList = UCCommonFilter.GetUser(Convert.ToInt32(CompanyID), Convert.ToInt32(DeptID), profile.DBConnection._constr).ToList();

            return UserList;
        }

        [WebMethod]
        public static List<SP_GetUsers_Result> WMGetUserList(string SelectedDepartment, string selectedCompany)
        {
            DataSet ds = new DataSet();
            //List<SP_GWC_GetUserInfo_Result> UserList = new List<SP_GWC_GetUserInfo_Result>();
            List<SP_GetUsers_Result> UsersList = new List<SP_GetUsers_Result>();
            iUCCommonFilterClient UCCommonFilter = new iUCCommonFilterClient();
            CustomProfile profile = CustomProfile.GetProfile();

            //UserList = UCCommonFilter.GetUsrLst1(selectedCompany, SelectedDepartment, profile.DBConnection._constr).ToList();
            UsersList = UCCommonFilter.GetUsersDepartmentWise(selectedCompany, SelectedDepartment, profile.DBConnection._constr).ToList();
            return UsersList;
        }

        #endregion


        private void loadstring()
        {
            try
            {
                QueryParameter = Request.QueryString["invoker"];
                Thread.CurrentThread.CurrentCulture = new CultureInfo(Session["Lang"].ToString());
                rm = new ResourceManager("PowerOnRentwebapp.App_GlobalResources.Lang", Assembly.GetExecutingAssembly());
                ci = Thread.CurrentThread.CurrentCulture;
                //suraj
                //lblpartreport.Text = rm.GetString("PartRequisitionReport", ci);
                //lblskureport.Text = rm.GetString("SKUReport", ci);
                //lblpartreceipt.Text = rm.GetString("PartReceiptReport", ci);
                //lblorderreport.Text = rm.GetString("OrderReport", ci);
                //suraj
                lblpartissuereport.Text = rm.GetString("PartIssueReport", ci);
                lbluserreport.Text = rm.GetString("UserReport", ci);
                lblReportType.Text = rm.GetString("ReportType", ci);
                btnViewReport.Value = rm.GetString("ViewReport", ci);

                lblSKUDetail.Text = rm.GetString("SKUDetailReport", ci);
                lblbomdetail.Text = rm.GetString("BOMDetailReport", ci);
                lblorderdetail.Text = rm.GetString("OrderDetailReport", ci);
                lblleadtime.Text = rm.GetString("OrderLeadTime", ci);
                lblImageAuditTrails.Text = rm.GetString("ImageAuditTrails", ci);
                lblorderdeliveryreport.Text = rm.GetString("OrderDeliveryReport", ci);
                lblslareport.Text = rm.GetString("SLAReport", ci);
                lbltdtr.Text = rm.GetString("TotalDeliveryVsTotalRequest", ci);
                lbldeliverylog.Text = rm.GetString("DeliveryDriverLogReport", ci);
                lblEcommerceRpt1.Text = rm.GetString("ECommerceReport1", ci);
                Label1.Text = rm.GetString("ECommerceReport2", ci);
                lblLocationRpt.Text = rm.GetString("PaymentDetailReport", ci);
                lbltransactionreport.Text = rm.GetString("TransactionReport", ci);
                //change by suraj khopade
                //btnExportToPDF.Value = rm.GetString("ExportToPDF", ci);
                //btnExportToEXCEL.Value = rm.GetString("ExportToEXCEL", ci);
                if (QueryParameter == "sku")
                {
                    lblRptName.Text = rm.GetString("SKUListReport", ci);
                }
                else if (QueryParameter == "SkuDetails")
                {
                    lblRptName.Text = rm.GetString("SkuDetailsReport", ci);
                }
                else if (QueryParameter == "BomDetail")
                {
                    lblRptName.Text = rm.GetString("BOMDetailReport", ci);
                }
                else if (QueryParameter == "user")
                {
                    lblRptName.Text = rm.GetString("UserListReport", ci);
                }
                else if (QueryParameter == "order")
                {
                    lblRptName.Text = rm.GetString("OrderListReport", ci);
                }
                else if (QueryParameter == "orderdetail")
                {
                    lblRptName.Text = rm.GetString("OrderDetailReport", ci);
                }
                else if (QueryParameter == "orderlead")
                {
                    lblRptName.Text = rm.GetString("OrderLeadTimeReport", ci);
                }
                else if (QueryParameter == "orderdelivery")
                {
                    lblRptName.Text = rm.GetString("OrderDeliveryReport", ci);
                }
                else if (QueryParameter == "sla")
                {
                    lblRptName.Text = rm.GetString("ServiceLevelAgreementReport", ci);
                }
                else if (QueryParameter == "totaldeliveryvstotalrequest")
                {
                    lblRptName.Text = rm.GetString("TotalDeliveryVsTotalRequest", ci);
                }
                else if (QueryParameter == "location")
                {
                    lblRptName.Text = rm.GetString("PaymentDetailReport", ci);
                }
                else if (QueryParameter == "deliverydriverlogreport")
                {
                    lblRptName.Text = rm.GetString("DeliveryDriverLogReport", ci);
                }

                else if (QueryParameter == "ecommerce1")
                {
                    lblRptName.Text = rm.GetString("ECommerceReport1", ci);
                }

                else if (QueryParameter == "ecommerce2")
                {
                    lblRptName.Text = rm.GetString("ECommerceReport2", ci);
                }

                else if (QueryParameter == "qnbnstock")
                {
                    lblRptName.Text = rm.GetString("SKUListReport", ci);
                }

                else if (QueryParameter == "qnbnorder")
                {
                    lblRptName.Text = rm.GetString("OrderListReport", ci);
                }

                else if (QueryParameter == "skutrack")
                {
                    lblRptName.Text = rm.GetString("SkuTrackReport", ci);
                }
                else if (QueryParameter == "sitetrack")
                {
                    lblRptName.Text = rm.GetString("SiteTrackReport", ci);
                }
                else if (QueryParameter == "depttrack")
                {
                    lblRptName.Text = rm.GetString("DepartmentTrackReport", ci);
                }
                //change by suraj khopade
                else if (QueryParameter == "Transaction")
                {
                    lblRptName.Text = rm.GetString("TransactionReport", ci);
                }
                //Start for RMS
                else if (QueryParameter == "ReturnCollectionReport")
                {
                    lblRptName.Text = rm.GetString("Collection Report", ci);
                }
                else if (QueryParameter == "ReceiptSummaryReport")
                {
                    lblRptName.Text = rm.GetString("Receipt Summary Report", ci);
                }
                //End for RMS
                //change by suraj khopade
                //else if (QueryParameter == "imgaudit")
                //{
                //    lblRptName.Text = rm.GetString("ImageAuditTrails", ci);
                //}
            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "CommonReport", "LoadString");
            }
            finally { }
        }

        [WebMethod]
        public static List<VW_DeptWisePaymentMethod> WMGetPaymentMethod(long Dept)
        {
            List<VW_DeptWisePaymentMethod> AdrsLst = new List<VW_DeptWisePaymentMethod>();

            CustomProfile profile = CustomProfile.GetProfile();
            iPartRequestClient objService = new iPartRequestClient();
            AdrsLst = objService.GEtDeptPaymentmethod(Dept, profile.DBConnection._constr).ToList();
            return AdrsLst;
        }



        [WebMethod]
        public static int WMGetGWCEcommrce1Report(string invoker, string FromDt, string ToDt, string SelectedOrder, string SelectedCompany, string SelectedDepartment, string SelectedStatus, string SelectedUser, string SelectedPaymentMode, string AllRecord)
        {
            int result = 0;
            DataSet dsecommRpt = new DataSet();
            iUCCommonFilterClient UCCommonFilterClient = new iUCCommonFilterClient();
            CustomProfile profile = CustomProfile.GetProfile();
            if (invoker.ToLower() == "ecommerce1")
            {
                HttpContext.Current.Session["ReportHeading"] = "E-Commerce Detail Report1";
                if (AllRecord == "1")
                {
                    if (SelectedCompany == "0" || SelectedCompany == "") { SelectedDepartment = "0"; }
                    dsecommRpt = UCCommonFilterClient.GetAllEcommerce1Data(FromDt, ToDt, SelectedCompany, SelectedDepartment, SelectedStatus, SelectedUser, SelectedPaymentMode, profile.DBConnection._constr);
                }
                else
                {
                    dsecommRpt = UCCommonFilterClient.GetAllEcommerce1DataReport1(SelectedOrder, profile.DBConnection._constr);
                }

            }

            DataSet orddetail = new DataSet();
            orddetail = UCCommonFilterClient.GetAllEcommDetailDataReport1(SelectedOrder, profile.DBConnection._constr);

            if (SelectedCompany == "0" || SelectedCompany == "")
            {
                SelectedCompany = "All"; SelectedDepartment = "0";
            }
            else
            {
                SelectedCompany = UCCommonFilterClient.GetcompanyNamerpt(Convert.ToInt64(SelectedCompany), profile.DBConnection._constr);
            }
            if (SelectedDepartment == "0" || SelectedDepartment == "")
            {
                SelectedDepartment = "All";
            }
            else
            {
                SelectedDepartment = UCCommonFilterClient.GetdepartmentNamerpt(Convert.ToInt64(SelectedDepartment), profile.DBConnection._constr);
            }

            if (SelectedStatus == "0" || SelectedStatus == "")
            { SelectedStatus = "All"; }
            else
            {
                SelectedStatus = UCCommonFilterClient.GetStatusName(Convert.ToInt64(SelectedStatus), profile.DBConnection._constr);
            }

            if (SelectedUser == "0" || SelectedUser == "") SelectedUser = "All";

            if (SelectedPaymentMode == "0" || SelectedPaymentMode == "")
            { SelectedPaymentMode = "All"; }
            else
            {
                SelectedPaymentMode = UCCommonFilterClient.GetpaymentmodeNamerpt(Convert.ToInt64(SelectedPaymentMode), profile.DBConnection._constr);
            }


            dsecommRpt.Tables[0].TableName = "dsEcommerceRpt1";
            orddetail.Tables[0].TableName = "dsEcommdetailRpt1";

            if (dsecommRpt.Tables[0].Rows.Count > 0)
            {
                HttpContext.Current.Session["ReportDS"] = dsecommRpt;
                HttpContext.Current.Session["ReportDS1"] = orddetail;
                HttpContext.Current.Session["FromDt"] = FromDt;
                HttpContext.Current.Session["ToDt"] = ToDt;
                HttpContext.Current.Session["Company"] = SelectedCompany;
                HttpContext.Current.Session["Department"] = SelectedDepartment;
                HttpContext.Current.Session["Status"] = SelectedStatus;
                HttpContext.Current.Session["User"] = SelectedUser;
                HttpContext.Current.Session["PaymentMode"] = SelectedPaymentMode;
                HttpContext.Current.Session["SelObject"] = invoker;
                result = Convert.ToInt16(dsecommRpt.Tables[0].Rows.Count);
            }
            else { result = 0; }
            return result;
        }

        [WebMethod]
        public static int WMGetGWCEcommrce2Dump(string invoker, string FromDt, string ToDt, string SelectedOrder, string SelectedCompany, string SelectedDepartment, string SelectedStatus, string SelectedUser, string SelectedPaymentMode, string AllRecord)
        {
            int result = 0;
            DataSet dsecommRpt = new DataSet();
            iUCCommonFilterClient UCCommonFilterClient = new iUCCommonFilterClient();
            CustomProfile profile = CustomProfile.GetProfile();
            HttpContext.Current.Session["ReportHeading"] = "EcommerceDump";
            //if (AllRecord == "1")
            //{
            if (SelectedCompany == "0" || SelectedCompany == "") { SelectedDepartment = "0"; }
            //  dsecommRpt = UCCommonFilterClient.ExportDatainExcel(FromDt, ToDt, SelectedCompany, SelectedDepartment, SelectedStatus, SelectedUser, SelectedPaymentMode, profile.DBConnection._constr);
            dsecommRpt = UCCommonFilterClient.ExportDatainExcelNew(FromDt, ToDt, SelectedOrder,SelectedCompany, SelectedDepartment, SelectedStatus, SelectedUser, SelectedPaymentMode, profile.DBConnection._constr);
            //}
            //else
            //{
            //    dsecommRpt = UCCommonFilterClient.GetAllEcommerce1DataReport1(SelectedOrder, profile.DBConnection._constr);
            //}

            if (SelectedCompany == "0" || SelectedCompany == "")
            {
                SelectedCompany = "All"; SelectedDepartment = "0";
            }
            else
            {
                SelectedCompany = UCCommonFilterClient.GetcompanyNamerpt(Convert.ToInt64(SelectedCompany), profile.DBConnection._constr);
            }
            if (SelectedDepartment == "0" || SelectedDepartment == "")
            {
                SelectedDepartment = "All";
            }
            else
            {
                SelectedDepartment = UCCommonFilterClient.GetdepartmentNamerpt(Convert.ToInt64(SelectedDepartment), profile.DBConnection._constr);
            }
            if (SelectedStatus == "0" || SelectedStatus == "")
            { SelectedStatus = "All"; }
            else
            {
                SelectedStatus = UCCommonFilterClient.GetStatusName(Convert.ToInt64(SelectedStatus), profile.DBConnection._constr);
            }
            if (SelectedUser == "0" || SelectedUser == "") { SelectedUser = "All"; }
            else { SelectedUser = ""; }
            if (SelectedPaymentMode == "0" || SelectedPaymentMode == "") SelectedPaymentMode = "All";
            if (SelectedCompany == "0" || SelectedCompany == "") SelectedDepartment = "All";

            dsecommRpt.Tables[0].TableName = "dsEcommerceDump";
            if (dsecommRpt.Tables[0].Rows.Count > 0)
            {
                HttpContext.Current.Session["ReportDS"] = dsecommRpt;
                HttpContext.Current.Session["FromDt"] = FromDt;
                HttpContext.Current.Session["ToDt"] = ToDt;
                HttpContext.Current.Session["Company"] = SelectedCompany;
                HttpContext.Current.Session["Department"] = SelectedDepartment;
                HttpContext.Current.Session["Status"] = SelectedStatus;
                HttpContext.Current.Session["User"] = SelectedUser;
                HttpContext.Current.Session["PaymentMode"] = SelectedPaymentMode;
                HttpContext.Current.Session["SelObject"] = invoker;
                result = Convert.ToInt16(dsecommRpt.Tables[0].Rows.Count);
            }
            else { result = 0; }
            return result;
        }

        [WebMethod]
        public static int WMGetGWCEcommrce2Report(string invoker, string FromDt, string ToDt, string SelectedOrder, string SelectedCompany, string SelectedDepartment, string SelectedStatus, string SelectedUser, string SelectedPaymentMode, string AllRecord)
        {
            int result = 0;
            DataSet dsecommRpt = new DataSet();
            iUCCommonFilterClient UCCommonFilterClient = new iUCCommonFilterClient();
            CustomProfile profile = CustomProfile.GetProfile();
            if (invoker.ToLower() == "ecommerce2")
            {
                HttpContext.Current.Session["ReportHeading"] = "E-Commerce Detail Report2";
                if (AllRecord == "1")
                {
                    if (SelectedCompany == "0" || SelectedCompany == "") { SelectedDepartment = "0"; }
                    dsecommRpt = UCCommonFilterClient.GetAllEcommerce1Data(FromDt, ToDt, SelectedCompany, SelectedDepartment, SelectedStatus, SelectedUser, SelectedPaymentMode, profile.DBConnection._constr);
                }
                else
                {
                    dsecommRpt = UCCommonFilterClient.GetAllEcommerce1DataReport1(SelectedOrder, profile.DBConnection._constr);
                }

            }


            if (SelectedCompany == "0" || SelectedCompany == "")
            {
                SelectedCompany = "All"; SelectedDepartment = "0";
            }
            else
            {
                SelectedCompany = UCCommonFilterClient.GetcompanyNamerpt(Convert.ToInt64(SelectedCompany), profile.DBConnection._constr);
            }
            if (SelectedDepartment == "0" || SelectedDepartment == "")
            {
                SelectedDepartment = "All";
            }
            else
            {
                SelectedDepartment = UCCommonFilterClient.GetdepartmentNamerpt(Convert.ToInt64(SelectedDepartment), profile.DBConnection._constr);
            }
            if (SelectedStatus == "0" || SelectedStatus == "")
            { SelectedStatus = "All"; }
            else
            {
                SelectedStatus = UCCommonFilterClient.GetStatusName(Convert.ToInt64(SelectedStatus), profile.DBConnection._constr);
            }
            if (SelectedUser == "0" || SelectedUser == "") { SelectedUser = "All"; }
            else { SelectedUser = ""; }
            if (SelectedPaymentMode == "0" || SelectedPaymentMode == "") SelectedPaymentMode = "All";
            if (SelectedCompany == "0" || SelectedCompany == "") SelectedDepartment = "All";

            //DataSet dsexport = new DataSet();
            //dsexport = UCCommonFilterClient.ExportDatainExcel(FromDt, ToDt, SelectedCompany, SelectedDepartment, SelectedStatus, SelectedUser, SelectedPaymentMode, profile.DBConnection._constr);
            //HttpContext.Current.Session["ExcelReport"] = dsexport;            
            //GVexport.DataSource = dsexport;
            //GVexport.DataBind();

            dsecommRpt.Tables[0].TableName = "dsEcommerceRpt2";
            if (dsecommRpt.Tables[0].Rows.Count > 0)
            {
                HttpContext.Current.Session["ReportDS"] = dsecommRpt;
                HttpContext.Current.Session["FromDt"] = FromDt;
                HttpContext.Current.Session["ToDt"] = ToDt;
                HttpContext.Current.Session["Company"] = SelectedCompany;
                HttpContext.Current.Session["Department"] = SelectedDepartment;
                HttpContext.Current.Session["Status"] = SelectedStatus;
                HttpContext.Current.Session["User"] = SelectedUser;
                HttpContext.Current.Session["PaymentMode"] = SelectedPaymentMode;
                HttpContext.Current.Session["SelObject"] = invoker;
                result = Convert.ToInt16(dsecommRpt.Tables[0].Rows.Count);
            }
            else { result = 0; }
            return result;
        }


        [WebMethod]
        public static int WMGetGWCDelivrynoteReport(string invoker, string FromDt, string ToDt, string SelectedOrder, string SelectedCompany, string SelectedDepartment, string OrderNumber)
        {
            int result = 0;
            try
            {
                string[] eorder = SelectedOrder.Split(',');
                long orderId = 0;
                orderId = Convert.ToInt64(eorder[0]);
                DataSet dsecommRpt = new DataSet();
                iUCCommonFilterClient UCCommonFilterClient = new iUCCommonFilterClient();
                CustomProfile profile = CustomProfile.GetProfile();
                string deliverytype = "", cname = "", address = "";

                if (OrderNumber == "" || OrderNumber == null) { OrderNumber = ""; }
                if (invoker.ToLower() == "deliverynote")
                {
                    iPartRequestClient objService = new iPartRequestClient();
                    // CustomProfile profile = CustomProfile.GetProfile();
                    string ChkBarCodeCreateOrNot = objService.ChkBarcode(orderId, profile.DBConnection._constr);
                    if (ChkBarCodeCreateOrNot == "No")
                    {
                        DataSet ds = new DataSet();
                        string Orderno = "", Amtpaid = "";
                        ds = objService.GetOrderDetails(orderId, profile.DBConnection._constr);
                        Orderno = ds.Tables[0].Rows[0]["OrderNumber"].ToString();
                        Amtpaid = ds.Tables[0].Rows[0]["amounttobecollected"].ToString();
                        Barcode128 bc = new Barcode128();
                        bc.Code = Orderno;
                        System.Drawing.Image imgbrcd = bc.CreateDrawingImage(System.Drawing.Color.Black, System.Drawing.Color.White);
                        bc.BarHeight = 1000f;
                        MemoryStream ms = new MemoryStream();
                        imgbrcd.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                        byte[] ss = ms.ToArray();

                        Barcode128 bc1 = new Barcode128();
                        bc1.Code = Amtpaid;
                        System.Drawing.Image imgbrcd1 = bc1.CreateDrawingImage(System.Drawing.Color.Black, System.Drawing.Color.White);
                        bc1.BarHeight = 1000f;
                        MemoryStream ms1 = new MemoryStream();
                        imgbrcd1.Save(ms1, System.Drawing.Imaging.ImageFormat.Gif);
                        byte[] ss1 = ms1.ToArray();
                        objService.InsertintomDeliverybarCode(orderId, ss, ss1, profile.DBConnection._constr);
                    }

                    HttpContext.Current.Session["ReportHeading"] = "Delivery Note Report";
                    if (SelectedCompany == "0" || SelectedCompany == "") { SelectedDepartment = "0"; }
                    // dsecommRpt = UCCommonFilterClient.GetDeliveryNoteData(FromDt, ToDt, SelectedCompany, SelectedDepartment, OrderNumber, profile.DBConnection._constr);
                    dsecommRpt = UCCommonFilterClient.GetReportDataDelivery(long.Parse(SelectedOrder), profile.DBConnection._constr);

                }
                if (SelectedCompany == "0" || SelectedCompany == "")
                {
                    SelectedCompany = "All"; SelectedDepartment = "0";
                }
                else
                {
                    SelectedCompany = UCCommonFilterClient.GetcompanyNamerpt(Convert.ToInt64(SelectedCompany), profile.DBConnection._constr);
                }
                if (SelectedDepartment == "0" || SelectedDepartment == "")
                {
                    SelectedDepartment = "All";
                }
                else
                {
                    SelectedDepartment = UCCommonFilterClient.GetdepartmentNamerpt(Convert.ToInt64(SelectedDepartment), profile.DBConnection._constr);
                }
                dsecommRpt.Tables[0].TableName = "dsDeliveryFormRpt";
                if (dsecommRpt.Tables[0].Rows.Count > 0)
                {
                    deliverytype = dsecommRpt.Tables[0].Rows[0]["DeliveryType"].ToString();
                    //if (deliverytype.ToUpper() == "HUB")
                    //{
                    //    cname = dsecommRpt.Tables[0].Rows[0]["VodafoneStoreName"].ToString();
                    //}
                    //else
                    //{
                    //    cname = dsecommRpt.Tables[0].Rows[0]["CustomerFirstName"].ToString() + " " + dsecommRpt.Tables[0].Rows[0]["CustomerLastName"].ToString();
                    //}
                    cname = dsecommRpt.Tables[0].Rows[0]["CustomerFirstName"].ToString() + " " + dsecommRpt.Tables[0].Rows[0]["CustomerLastName"].ToString();
                    address = dsecommRpt.Tables[0].Rows[0]["BuildingName2"].ToString() + ", " + dsecommRpt.Tables[0].Rows[0]["Streetname2"].ToString() + ", " + dsecommRpt.Tables[0].Rows[0]["City2"].ToString() + ", " + dsecommRpt.Tables[0].Rows[0]["Country2"].ToString();
                    HttpContext.Current.Session["ReportDS"] = dsecommRpt;
                    HttpContext.Current.Session["Fullname"] = cname;
                    HttpContext.Current.Session["Address"] = address;
                    HttpContext.Current.Session["FromDt"] = FromDt;
                    HttpContext.Current.Session["ToDt"] = ToDt;
                    HttpContext.Current.Session["Company"] = SelectedCompany;
                    HttpContext.Current.Session["Department"] = SelectedDepartment;
                    HttpContext.Current.Session["SelObject"] = invoker;
                    result = Convert.ToInt16(dsecommRpt.Tables[0].Rows.Count);
                }
                else { result = 0; }
            }
            catch (Exception)
            {
                result = 0;
                //throw;
            }
            return result;
        }


        [WebMethod]
        public static string GetTransactionReportList(string jsonData)
        {
            string abc = "";
            abc = jsonData;
            //fillTransactionReports(jsonData);
            //DataTable dt = JsonConvert.DeserializeObject<DataTable>(jsonData);
            return "";
        }

        //change by suraj khopade

        [WebMethod]
        public static string WMGetTransactionExportDatatoPDF(string txtcategory, string txtcateria)
        {

            iUCCommonFilterClient UCCommonFilter = new iUCCommonFilterClient();
            CustomProfile profile = CustomProfile.GetProfile();
            DataSet ds = new DataSet();
            // ds = UCCommonFilter.GetcriteriaList(Convert.ToInt16(categoryID), profile.DBConnection._constr);
            //ds = UCCommonFilter.GetExportDatatoPDF(txtcategory, txtcateria, profile.DBConnection._constr);
            //string criteriaid = ds.Tables[0].Rows[0]["id"].ToString();
            string criterianame = ds.Tables[0].Rows[0]["Criteria"].ToString();

            //List<string> lstCriteriaName = new List<string>();
            //lstCriteriaName.Add(criterianame);
            return criterianame;
        }

        [WebMethod]
        public static int WMGetGWCNormalOrderDelivrynoteReport(string invoker, string FromDt, string ToDt, string SelectedOrder, string SelectedCompany, string SelectedDepartment, string OrderNumber)
        {
            int result = 0;
            try
            {
                string[] eorder = SelectedOrder.Split(',');
                long orderId = 0;
                orderId = Convert.ToInt64(eorder[0]);
                DataSet dsecommRpt = new DataSet();
                iUCCommonFilterClient UCCommonFilterClient = new iUCCommonFilterClient();
                CustomProfile profile = CustomProfile.GetProfile();
                string deliverytype = "", cname = "", address = "";
                if (OrderNumber == "" || OrderNumber == null) { OrderNumber = ""; }
                if (invoker.ToLower() == "normalorderdeliverynote")
                {
                    iPartRequestClient objService = new iPartRequestClient();
                    // CustomProfile profile = CustomProfile.GetProfile();
                    string ChkBarCodeCreateOrNot = objService.ChkNormalOrderBarcode(orderId, profile.DBConnection._constr);

                    if (ChkBarCodeCreateOrNot == "No")
                    {
                        DataSet ds = new DataSet();
                        string Orderno = "", Amtpaid = "";
                        ds = objService.GetNormalOrderDetails(orderId, profile.DBConnection._constr);
                        Orderno = ds.Tables[0].Rows[0]["OrderNo"].ToString();
                        //Amtpaid = ds.Tables[0].Rows[0]["amounttobecollected"].ToString();
                        Barcode128 bc = new Barcode128();
                        bc.Code = Orderno;
                        System.Drawing.Image imgbrcd = bc.CreateDrawingImage(System.Drawing.Color.Black, System.Drawing.Color.White);
                        bc.BarHeight = 1000f;
                        MemoryStream ms = new MemoryStream();
                        imgbrcd.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                        byte[] ss = ms.ToArray();

                        Barcode128 bc1 = new Barcode128();
                        bc1.Code = Amtpaid;
                        System.Drawing.Image imgbrcd1 = bc1.CreateDrawingImage(System.Drawing.Color.Black, System.Drawing.Color.White);
                        bc1.BarHeight = 1000f;
                        MemoryStream ms1 = new MemoryStream();
                        imgbrcd1.Save(ms1, System.Drawing.Imaging.ImageFormat.Gif);
                        byte[] ss1 = ms1.ToArray();
                        objService.InsertintoNormalOrdermDeliverybarCode(orderId, ss, ss1, profile.DBConnection._constr);

                    }
                    HttpContext.Current.Session["ReportHeading"] = "Delivery Note Report";
                    if (SelectedCompany == "0" || SelectedCompany == "") { SelectedDepartment = "0"; }
                    dsecommRpt = UCCommonFilterClient.GetNormalOrderReportDataDelivery(long.Parse(SelectedOrder), profile.DBConnection._constr);

                }
                if (SelectedCompany == "0" || SelectedCompany == "")
                {
                    SelectedCompany = "All"; SelectedDepartment = "0";
                }
                else
                {
                    SelectedCompany = UCCommonFilterClient.GetcompanyNameNormalOrder(Convert.ToInt64(SelectedCompany), profile.DBConnection._constr);// ***

                }
                if (SelectedDepartment == "0" || SelectedDepartment == "")
                {
                    SelectedDepartment = "All";
                }
                else
                {
                    SelectedDepartment = UCCommonFilterClient.GetdepartmentNameNormalOrder(Convert.ToInt64(SelectedDepartment), profile.DBConnection._constr);// ***

                }
                dsecommRpt.Tables[0].TableName = "dsDeliveryFormNormalOrder";
                if (dsecommRpt.Tables[0].Rows.Count > 0)
                {
                    // deliverytype = dsecommRpt.Tables[0].Rows[0]["DeliveryType"].ToString();
                    cname = dsecommRpt.Tables[0].Rows[0]["CustomerFirstName"].ToString() + " " + dsecommRpt.Tables[0].Rows[0]["CustomerLastName"].ToString();
                    address = dsecommRpt.Tables[0].Rows[0]["BuildingName2"].ToString() + ", " + dsecommRpt.Tables[0].Rows[0]["Streetname2"].ToString() + ", " + dsecommRpt.Tables[0].Rows[0]["City2"].ToString() + ", " + dsecommRpt.Tables[0].Rows[0]["Country2"].ToString();
                    HttpContext.Current.Session["ReportDS"] = dsecommRpt;
                    HttpContext.Current.Session["Fullname"] = cname;
                    HttpContext.Current.Session["Address"] = address;
                    HttpContext.Current.Session["FromDt"] = FromDt;
                    HttpContext.Current.Session["ToDt"] = ToDt;
                    HttpContext.Current.Session["Company"] = SelectedCompany;
                    HttpContext.Current.Session["Department"] = SelectedDepartment;
                    HttpContext.Current.Session["SelObject"] = invoker;
                    result = Convert.ToInt16(dsecommRpt.Tables[0].Rows.Count);
                }
                else { result = 0; }
            }
            catch (Exception e)
            {
                result = 0;
                //throw;
            }
            return result;

        }

        [WebMethod]
        public static int WMGetGWCReturnOrderCollectionReport(string invoker, string FromDate, string ToDate, string SelectedRec, string SelectedCompany, string SelectedDepartment, string SelectedDriver, string SelectedStatus, string OrderNumber)
        {
            int result = 0;
            DataSet dsCmnRpt = new DataSet();
            iUserCreationClient usercreation = new iUserCreationClient();
            CustomProfile profile = CustomProfile.GetProfile();

            string frmdt_lcl, todt_lcl;
            //frmdt_lcl = Convert.ToDateTime(FromDate).Date.ToShortDateString(); // Value.ToString("yyyy/MM/dd");
            //todt_lcl = Convert.ToDateTime(ToDate).Date.ToShortDateString();

            frmdt_lcl = Convert.ToDateTime(FromDate).Date.ToString("yyyy/MM/dd");
            todt_lcl = Convert.ToDateTime(ToDate).Date.ToString("yyyy/MM/dd");
            string usertype = profile.Personal.UserType.ToString();
            string comid = profile.Personal.CompanyID.ToString();
           // if (invoker.ToLower() == "ReturnCollectionReport")
            //{
                HttpContext.Current.Session["ReportHeading"] = "Return Order Details Report";
                if (SelectedRec != "0" || SelectedRec != "")
                {
                    if (SelectedCompany == "0" || SelectedCompany == "") { SelectedDepartment = "0"; }
                    if (usertype == "Super Admin")
                    {
                        dsCmnRpt = usercreation.GetReturnOrderCollectionReportData(frmdt_lcl, todt_lcl,SelectedRec, SelectedCompany, SelectedDepartment, SelectedDriver, SelectedStatus, profile.DBConnection._constr);
                    }
                    else
                    {
                        SelectedCompany = comid;
                        dsCmnRpt = usercreation.GetReturnOrderCollectionReportData(frmdt_lcl, todt_lcl,SelectedRec, SelectedCompany, SelectedDepartment, SelectedDriver, SelectedStatus, profile.DBConnection._constr);
                    }
                }
            //}
            dsCmnRpt.Tables[0].TableName = "dsReturnOrderCollectionDetails";
            if (dsCmnRpt.Tables[0].Rows.Count > 0)
            {
                // string Company = dsCmnRpt.Tables[0].Rows[0]["CompanyName"].ToString();
                // if (SelectedCompany == "0" || SelectedCompany == "") { SelectedCompany = "All"; } else { SelectedCompany = dsCmnRpt.Tables[0].Rows[0]["CompanyName"].ToString(); }
                // string Department = dsCmnRpt.Tables[0].Rows[0]["Territory"].ToString() + "-" + dsCmnRpt.Tables[0].Rows[0]["StoreCode"].ToString();
                // if (SelectedDepartment == "0" || SelectedDepartment == "") { SelectedDepartment = "All"; } else { SelectedDepartment = dsCmnRpt.Tables[0].Rows[0]["Territory"].ToString() + "-" + dsCmnRpt.Tables[0].Rows[0]["StoreCode"].ToString(); }
                //  string User = dsCmnRpt.Tables[0].Rows[0]["UserName"].ToString();

                // string Status = dsCmnRpt.Tables[0].Rows[0]["Status"].ToString();
                //if (SelectedStatus == "0" || SelectedStatus == "") { SelectedStatus = "All"; } else { SelectedStatus = dsCmnRpt.Tables[0].Rows[0]["Status"].ToString(); }
                string deptname = "";
                deptname = dsCmnRpt.Tables[0].Rows[0]["Department"].ToString();
                if (SelectedDepartment == "0")
                {
                    SelectedDepartment = "ALL";
                }
                else
                {
                    SelectedDepartment = deptname;
                }

                HttpContext.Current.Session["ReportDS"] = dsCmnRpt;
                HttpContext.Current.Session["FromDt"] = frmdt_lcl;
                HttpContext.Current.Session["ToDt"] = todt_lcl;
                HttpContext.Current.Session["Company"] = SelectedCompany;
                HttpContext.Current.Session["Department"] = SelectedDepartment;
               // HttpContext.Current.Session["Status"] = SelectedStatus;
               // HttpContext.Current.Session["User"] = SelectedUser;
                HttpContext.Current.Session["SelObject"] = invoker;
                result = Convert.ToInt16(dsCmnRpt.Tables[0].Rows.Count);
            }
            else
            {
                result = 0;
            }
            return result;

        }

        [WebMethod]
        public static int WMGetGWCReturnOrderReceiptSummaryReport(string invoker, string FromDate, string ToDate, string SelectedRec, string SelectedCompany, string SelectedDepartment, string SelectedStatus, string OrderNumber)
        {
            int result = 0;
            DataSet dsCmnRpt = new DataSet();
            iUserCreationClient usercreation = new iUserCreationClient();
            CustomProfile profile = CustomProfile.GetProfile();

            string frmdtlcl, todtlcl;
            //frmdtlcl = Convert.ToDateTime(FromDate).Date.ToShortDateString(); // Value.ToString("yyyy/MM/dd");
            //todtlcl = Convert.ToDateTime(ToDate).Date.ToShortDateString();

            frmdtlcl = Convert.ToDateTime(FromDate).Date.ToString("yyyy/MM/dd");
            todtlcl = Convert.ToDateTime(ToDate).Date.ToString("yyyy/MM/dd");
            string usertype = profile.Personal.UserType.ToString();
            string comid = profile.Personal.CompanyID.ToString();
            // if (invoker.ToLower() == "ReturnCollectionReport")
            //{
            HttpContext.Current.Session["ReportHeading"] = "Return Order Receipt Summary Report";
            if (SelectedRec != "0" || SelectedRec != "")
            {
                if (SelectedCompany == "0" || SelectedCompany == "") { SelectedDepartment = "0"; }
                if (usertype == "Super Admin")
                {
                    dsCmnRpt = usercreation.GetReturnOrderReceiptSummaryReport(frmdtlcl, todtlcl, SelectedRec, SelectedCompany, SelectedDepartment, SelectedStatus, profile.DBConnection._constr);
                }
                else
                {
                    SelectedCompany = comid;
                    dsCmnRpt = usercreation.GetReturnOrderReceiptSummaryReport(frmdtlcl, todtlcl, SelectedRec, SelectedCompany, SelectedDepartment, SelectedStatus, profile.DBConnection._constr);
                }
            }
            //}
            dsCmnRpt.Tables[0].TableName = "dsReturnOrderReceiptSummaryReport";
            if (dsCmnRpt.Tables[0].Rows.Count > 0)
            {
                // string Company = dsCmnRpt.Tables[0].Rows[0]["CompanyName"].ToString();
                // if (SelectedCompany == "0" || SelectedCompany == "") { SelectedCompany = "All"; } else { SelectedCompany = dsCmnRpt.Tables[0].Rows[0]["CompanyName"].ToString(); }
                // string Department = dsCmnRpt.Tables[0].Rows[0]["Territory"].ToString() + "-" + dsCmnRpt.Tables[0].Rows[0]["StoreCode"].ToString();
                // if (SelectedDepartment == "0" || SelectedDepartment == "") { SelectedDepartment = "All"; } else { SelectedDepartment = dsCmnRpt.Tables[0].Rows[0]["Territory"].ToString() + "-" + dsCmnRpt.Tables[0].Rows[0]["StoreCode"].ToString(); }
                //  string User = dsCmnRpt.Tables[0].Rows[0]["UserName"].ToString();

                // string Status = dsCmnRpt.Tables[0].Rows[0]["Status"].ToString();
                //if (SelectedStatus == "0" || SelectedStatus == "") { SelectedStatus = "All"; } else { SelectedStatus = dsCmnRpt.Tables[0].Rows[0]["Status"].ToString(); }
                string deptname = "";
                deptname = dsCmnRpt.Tables[0].Rows[0]["Department"].ToString();
                if (SelectedDepartment == "0")
                {
                    SelectedDepartment = "ALL";
                }
                else
                {
                    SelectedDepartment = deptname;
                }

                HttpContext.Current.Session["ReportDS"] = dsCmnRpt;
                HttpContext.Current.Session["FromDt"] = frmdtlcl;
                HttpContext.Current.Session["ToDt"] = todtlcl;
                HttpContext.Current.Session["Company"] = SelectedCompany;
                HttpContext.Current.Session["Department"] = SelectedDepartment;
                // HttpContext.Current.Session["Status"] = SelectedStatus;
                // HttpContext.Current.Session["User"] = SelectedUser;
                HttpContext.Current.Session["SelObject"] = invoker;
                result = Convert.ToInt16(dsCmnRpt.Tables[0].Rows.Count);
            }
            else
            {
                result = 0;
            }
            return result;

        }
        //change by suraj khopade

        [WebMethod]
        public static int WMGetSkuTrackingReport(string invoker, string FromDt, string ToDt, string Selectedsku, string AllRecord, string selectedrecord)
        {
            int result = 0;
            string skuname = "";
            DataSet dsecommRpt = new DataSet();
            iUCCommonFilterClient UCCommonFilterClient = new iUCCommonFilterClient();
            CustomProfile profile = CustomProfile.GetProfile();
            HttpContext.Current.Session["ReportHeading"] = "Sku Tracking Report";


            dsecommRpt = UCCommonFilterClient.GetAllSkudtata(FromDt, ToDt, Selectedsku, AllRecord, selectedrecord, profile.Personal.UserID, profile.Personal.CompanyID, profile.DBConnection._constr);
            dsecommRpt.Tables[0].TableName = "dsskutracking";
            if (dsecommRpt.Tables[0].Rows.Count > 0)
            {
                if (AllRecord == "1")
                {
                    skuname = "All";
                }
                else
                {
                    skuname = dsecommRpt.Tables[0].Rows[0]["Prod_Name"].ToString();
                }
                HttpContext.Current.Session["ReportDS"] = dsecommRpt;
                HttpContext.Current.Session["FromDt"] = FromDt;
                HttpContext.Current.Session["ToDt"] = ToDt;
                HttpContext.Current.Session["Skunm"] = skuname;
                HttpContext.Current.Session["SelObject"] = invoker;
                result = Convert.ToInt16(dsecommRpt.Tables[0].Rows.Count);
            }
            else { result = 0; }
            return result;
        }


        [WebMethod]
        public static int WMGetSiteTrackingReport(string invoker, string FromDt, string ToDt, string Selectedsite, string AllRecord, string selectedrecord)
        {
            int result = 0;
            string skuname = "";
            DataSet dsecommRpt = new DataSet();
            iUCCommonFilterClient UCCommonFilterClient = new iUCCommonFilterClient();
            CustomProfile profile = CustomProfile.GetProfile();
            HttpContext.Current.Session["ReportHeading"] = "Site Tracking Report";

            dsecommRpt = UCCommonFilterClient.GetAllSitedtata(FromDt, ToDt, Selectedsite, AllRecord, selectedrecord, profile.Personal.UserID, profile.Personal.CompanyID, profile.DBConnection._constr);
            dsecommRpt.Tables[0].TableName = "dssitetracking";
            if (dsecommRpt.Tables[0].Rows.Count > 0)
            {
                if (AllRecord == "1")
                {
                    skuname = "All";
                }
                else
                {
                    skuname = dsecommRpt.Tables[0].Rows[0]["msitesitecode"].ToString();
                }
                HttpContext.Current.Session["ReportDS"] = dsecommRpt;
                HttpContext.Current.Session["FromDt"] = FromDt;
                HttpContext.Current.Session["ToDt"] = ToDt;
                HttpContext.Current.Session["Sitenm"] = skuname;
                HttpContext.Current.Session["SelObject"] = invoker;
                result = Convert.ToInt16(dsecommRpt.Tables[0].Rows.Count);
            }
            else { result = 0; }
            return result;
        }

        [WebMethod]
        public static int WMGetDepartmentTrackingReport(string invoker, string FromDt, string ToDt, string Selecteddeptid, string AllRecord, string selectedrecord)
        {
            int result = 0;
            string skuname = "";
            DataSet dsecommRpt = new DataSet();
            iUCCommonFilterClient UCCommonFilterClient = new iUCCommonFilterClient();
            CustomProfile profile = CustomProfile.GetProfile();
            HttpContext.Current.Session["ReportHeading"] = "Department Tracking Report";

            dsecommRpt = UCCommonFilterClient.GetAllDeptData(FromDt, ToDt, Selecteddeptid, AllRecord, selectedrecord, profile.Personal.UserID, profile.Personal.CompanyID, profile.DBConnection._constr);
            dsecommRpt.Tables[0].TableName = "dsDepttrDacking";
            if (dsecommRpt.Tables[0].Rows.Count > 0)
            {
                if (AllRecord == "1")
                {
                    skuname = "All";
                }
                else
                {
                    skuname = dsecommRpt.Tables[0].Rows[0]["Territory"].ToString();
                }
                HttpContext.Current.Session["ReportDS"] = dsecommRpt;
                HttpContext.Current.Session["FromDt"] = FromDt;
                HttpContext.Current.Session["ToDt"] = ToDt;
                HttpContext.Current.Session["Deptnm"] = skuname;
                HttpContext.Current.Session["SelObject"] = invoker;
                result = Convert.ToInt16(dsecommRpt.Tables[0].Rows.Count);
            }
            else { result = 0; }
            return result;
        }


        public byte[] imgtobytearray(System.Drawing.Image imgin)
        {
         
            MemoryStream ms = new MemoryStream();
            imgin.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();          
        }

        [WebMethod]
        public static int WMGetBulkOrderReport(string invoker, string FromDt, string ToDt, string hdnSelectedOrdcategory, string hdnSelectedOrdtype, string hdnSelectedfromtype)
        {
            int result = 0;
            string skuname = "";
            DataSet dsecommRpt = new DataSet();
            iUCCommonFilterClient UCCommonFilterClient = new iUCCommonFilterClient();
            CustomProfile profile = CustomProfile.GetProfile();
            HttpContext.Current.Session["ReportHeading"] = "CAF & Delivery Note Report";

            if (hdnSelectedOrdtype == "DN")
            {
                dsecommRpt = UCCommonFilterClient.GetAllDeliveryNote(FromDt, ToDt, hdnSelectedOrdcategory, hdnSelectedOrdtype, hdnSelectedfromtype, profile.DBConnection._constr);
                dsecommRpt.Tables[0].TableName = "dsDN";
                if (dsecommRpt.Tables[0].Rows.Count > 0)
                {
                    HttpContext.Current.Session["ReportDS"] = dsecommRpt;
                    HttpContext.Current.Session["SelObject"] = invoker;
                    result = Convert.ToInt16(dsecommRpt.Tables[0].Rows.Count);
                }
            }
            else
            {
                if (hdnSelectedfromtype == "Prepaid")
                {
                    dsecommRpt = UCCommonFilterClient.GetAllDetaPrepaid(FromDt, ToDt, hdnSelectedOrdcategory, hdnSelectedOrdtype, hdnSelectedfromtype, profile.DBConnection._constr);
                    dsecommRpt.Tables[0].TableName = "dsPrepaidCAFDN";
                    if (dsecommRpt.Tables[0].Rows.Count > 0)
                    {
                        HttpContext.Current.Session["ReportDS"] = dsecommRpt;
                        HttpContext.Current.Session["SelObject"] = invoker;
                        result = Convert.ToInt16(dsecommRpt.Tables[0].Rows.Count);
                    }
                }
                else
                {
                    dsecommRpt = UCCommonFilterClient.GetAllDetaPostpaid(FromDt, ToDt, hdnSelectedOrdcategory, hdnSelectedOrdtype, hdnSelectedfromtype, profile.DBConnection._constr);
                    dsecommRpt.Tables[0].TableName = "dsPostpaidCAFDN";
                    if (dsecommRpt.Tables[0].Rows.Count > 0)
                    {
                        HttpContext.Current.Session["ReportDS"] = dsecommRpt;
                        HttpContext.Current.Session["SelObject"] = invoker;
                        result = Convert.ToInt16(dsecommRpt.Tables[0].Rows.Count);
                    }
                }
            }

        
            return result;
        }

        [WebMethod]
        public static List<OrderData> WMGetOrdReport(string invoker, string FromDt, string ToDt, string hdnSelectedOrdcategory, string hdnSelectedOrdtype, string hdnSelectedfromtype)
        {
            CustomProfile profile = CustomProfile.GetProfile();
            iUCCommonFilterClient UCCommonFilter = new iUCCommonFilterClient();
            List<OrderData> ord = new List<OrderData>();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                ds = UCCommonFilter.GetOrdDetails(FromDt, ToDt, hdnSelectedOrdcategory, hdnSelectedOrdtype, hdnSelectedfromtype, profile.DBConnection._constr);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                   
                    ord = (from DataRow dr in dt.Rows
                                   select new OrderData()
                                   {
                                       TotalOrder = Convert.ToInt64(dr["TotalOrder"]),
                                       PrintLimit = Convert.ToInt64(dr["PrintLimit"]),
                                       Printed = Convert.ToInt64(dr["Printed"]),
                                       PendingtoPrint = Convert.ToInt64(dr["PendingtoPrint"]),
                                   }).ToList();
                }
            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, "CommonReport", "WMGetOrdReport");
            }
            finally
            {
                UCCommonFilter.Close();
            }
            return ord;
        }

        public class OrderData
        {
            public long TotalOrder { get; set; }
            public long PrintLimit { get; set; }
            public long Printed { get; set; }
            public long PendingtoPrint { get; set; }
        }

        //[WebMethod]
        //public static List<SP_GetFromLocation_Result> WMGetLocationList(string SelectedDriver)
        //{
        //    CustomProfile profile = CustomProfile.GetProfile();
        //    iUCCommonFilterClient UCCommonFilter = new iUCCommonFilterClient();
        //    List<SP_GetFromLocation_Result> usrLoc = new List<SP_GetFromLocation_Result>();
        //    try
        //    {
        //        usrLoc = UCCommonFilter.GetFromDriverLoc(long.Parse(SelectedDriver), profile.DBConnection._constr).ToList();
        //    }
        //    catch (System.Exception ex)
        //    {
        //        Login.Profile.ErrorHandling(ex, "CommonReport", "WMGetLocationList");
        //    }
        //    finally
        //    {
        //        UCCommonFilter.Close();
        //    }
        //    return usrLoc;
        //}

        //[WebMethod]
        //public static List<SP_GetToLocation_Result> WMGetToLocationList(string SelectedDriver)
        //{
        //    CustomProfile profile = CustomProfile.GetProfile();
        //    iUCCommonFilterClient UCCommonFilter = new iUCCommonFilterClient();
        //    List<SP_GetToLocation_Result> usrLocTo = new List<SP_GetToLocation_Result>();
        //    try
        //    {
        //        usrLocTo = UCCommonFilter.GetToDriverLoc(long.Parse(SelectedDriver), profile.DBConnection._constr).ToList();
        //    }
        //    catch (System.Exception ex)
        //    {
        //        Login.Profile.ErrorHandling(ex, "CommonReport", "WMGetToLocationList");
        //    }
        //    finally
        //    {
        //        UCCommonFilter.Close();
        //    }
        //    return usrLocTo;
        //}


        //[WebMethod]
        //public static List<VW_DriverLocation> WMGetLocationList(string SelectedDriver)
        //{
        //    CustomProfile profile = CustomProfile.GetProfile();
        //    iUCCommonFilterClient UCCommonFilter = new iUCCommonFilterClient();
        //    List<VW_DriverLocation> usrLoc = new List<VW_DriverLocation>();
        //    try
        //    {
        //        usrLoc = UCCommonFilter.GetDriverLoc(long.Parse(SelectedDriver), profile.DBConnection._constr).ToList();
        //    }
        //    catch (System.Exception ex)
        //    {
        //        Login.Profile.ErrorHandling(ex, "CommonReport", "WMGetLocationList");
        //    }
        //    finally
        //    {
        //        UCCommonFilter.Close();
        //    }
        //    return usrLoc;
        //}

        //[WebMethod]
        //public static List<VW_DriverToLocation> WMGetToLocationList(string SelectedDriver)
        //{
        //    CustomProfile profile = CustomProfile.GetProfile();
        //    iUCCommonFilterClient UCCommonFilter = new iUCCommonFilterClient();
        //    List<VW_DriverToLocation> usrLocTo = new List<VW_DriverToLocation>();
        //    try
        //    {
        //        usrLocTo = UCCommonFilter.GetDriverToLoc(long.Parse(SelectedDriver), profile.DBConnection._constr).ToList();
        //    }
        //    catch (System.Exception ex)
        //    {
        //        Login.Profile.ErrorHandling(ex,"CommonReport", "WMGetToLocationList");
        //    }
        //    finally
        //    {
        //        UCCommonFilter.Close();
        //    }
        //    return usrLocTo;
        //}

        //[WebMethod]
        //public static List<VW_GetRoute> WMRouteList(string SelectedLocation, string SelectedToLocation)
        //{
        //    CustomProfile profile = CustomProfile.GetProfile();
        //    iUCCommonFilterClient UCCommonFilter = new iUCCommonFilterClient();
        //    List<VW_GetRoute> usrRoute = new List<VW_GetRoute>();

        //    usrRoute = UCCommonFilter.GetLocationRoute(long.Parse(SelectedLocation), long.Parse(SelectedToLocation), profile.DBConnection._constr).ToList();

        //    return usrRoute;
        //}

        [WebMethod]
        public static int WMGetGWCDelivryListReport(string invoker, string FromDt, string ToDt, string hdnRequestSelectedRec, string hdnSelectedStatus, string hdnSelectedDriver, string hdnSelectedLocation, string hdnSelectedToLocation, string hdnSelectedRoute, string AllRecord)
        {
            int result = 0;
            DataSet dsecommRpt = new DataSet();
            iUCCommonFilterClient UCCommonFilterClient = new iUCCommonFilterClient();
            CustomProfile profile = CustomProfile.GetProfile();
            string Companyid = profile.Personal.CompanyID.ToString();
            string UserType = profile.Personal.UserType.ToString();

            HttpContext.Current.Session["ReportHeading"] = "Delivery List Report";
            if (AllRecord == "1")
            {
                if (UserType == "Super Admin")
                {
                    //  dsecommRpt = UCCommonFilterClient.GetAllDeliveryLstReq(FromDt, ToDt, hdnSelectedStatus, hdnSelectedDriver, hdnSelectedLocation, hdnSelectedToLocation, hdnSelectedRoute, "0", profile.DBConnection._constr);
                }
                else
                {
                    // dsecommRpt = UCCommonFilterClient.GetAllDeliveryLstReq(FromDt, ToDt, hdnSelectedStatus, hdnSelectedDriver, hdnSelectedLocation, hdnSelectedToLocation, hdnSelectedRoute, Companyid, profile.DBConnection._constr);
                }
            }
            else
            {
                //  dsecommRpt = UCCommonFilterClient.GetDeliveryLstSelected(hdnRequestSelectedRec, profile.DBConnection._constr);
            }

            if (hdnSelectedDriver == "0" || hdnSelectedDriver == "")
            {
                hdnSelectedDriver = "All";
            }
            else
            {
                // hdnSelectedDriver = UCCommonFilterClient.GetDriverName(hdnSelectedDriver, profile.DBConnection._constr);
            }

            if (hdnSelectedLocation == "0" || hdnSelectedLocation == "")
            {
                hdnSelectedLocation = "All";
            }
            else
            {
                // hdnSelectedLocation = UCCommonFilterClient.GetLocationName(hdnSelectedLocation, profile.DBConnection._constr);
            }

            if (hdnSelectedRoute == "0" || hdnSelectedRoute == "")
            { hdnSelectedRoute = "All"; }
            else
            { //hdnSelectedRoute = UCCommonFilterClient.GetRouteName(hdnSelectedRoute, profile.DBConnection._constr); 
            }

            if (hdnSelectedStatus == "0" || hdnSelectedStatus == "")
            { hdnSelectedStatus = "All"; }
            else
            {
                hdnSelectedStatus = UCCommonFilterClient.GetStatusName(Convert.ToInt64(hdnSelectedStatus), profile.DBConnection._constr);
            }

            dsecommRpt.Tables[0].TableName = "dsavgtime";
            if (dsecommRpt.Tables[0].Rows.Count > 0)
            {
                HttpContext.Current.Session["ReportDS"] = dsecommRpt;
                HttpContext.Current.Session["FromDt"] = FromDt;
                HttpContext.Current.Session["ToDt"] = ToDt;
                HttpContext.Current.Session["Driver"] = hdnSelectedDriver;
                HttpContext.Current.Session["Location"] = hdnSelectedLocation;
                HttpContext.Current.Session["Route"] = hdnSelectedRoute;
                HttpContext.Current.Session["Status"] = hdnSelectedStatus;
                HttpContext.Current.Session["SelObject"] = invoker;
                result = Convert.ToInt16(dsecommRpt.Tables[0].Rows.Count);
            }
            else { result = 0; }
            return result;
        }

        [WebMethod]
        public static int WMGetGPSOrderDetail(string invoker, string txtFromDt, string txtToDt, string hdnRequestSelectedRec, string hdnSelectedCompany, string hdnSelectedDepartment, string hdnSelectedDriver, string hdnSelectedStatus)
        {
            int result = 0;
            DataSet dsecommRpt = new DataSet();
            DataSet dsApprvlDetail = new DataSet();
            iUCCommonFilterClient UCCommonFilterClient = new iUCCommonFilterClient();
            CustomProfile profile = CustomProfile.GetProfile();
            HttpContext.Current.Session["ReportHeading"] = "Delivery Detail Report";
            string Companyid = profile.Personal.CompanyID.ToString();
            string UserType = profile.Personal.UserType.ToString();
            if (UserType == "Super Admin")
            {
                // dsecommRpt = UCCommonFilterClient.GetGPSOrderDtail(hdnRequestSelectedRec, profile.DBConnection._constr);
                //  dsApprvlDetail = UCCommonFilterClient.GetGPSApproverDetail(hdnRequestSelectedRec, profile.DBConnection._constr);
            }
            else
            {
                //  dsecommRpt = UCCommonFilterClient.GetGPSOrderDtail(hdnRequestSelectedRec, profile.DBConnection._constr);
                //  dsApprvlDetail = UCCommonFilterClient.GetGPSApproverDetail(hdnRequestSelectedRec, profile.DBConnection._constr);
            }

            if (hdnSelectedCompany == "0" || hdnSelectedCompany == "")
            {
                hdnSelectedCompany = "All"; hdnSelectedDepartment = "0";
            }
            else
            {
                hdnSelectedCompany = UCCommonFilterClient.GetcompanyNamerpt(Convert.ToInt64(hdnSelectedCompany), profile.DBConnection._constr);
            }
            if (hdnSelectedDepartment == "0" || hdnSelectedDepartment == "")
            {
                hdnSelectedDepartment = "All";
            }
            else
            {
                hdnSelectedDepartment = UCCommonFilterClient.GetdepartmentNamerpt(Convert.ToInt64(hdnSelectedDepartment), profile.DBConnection._constr);
            }

            dsecommRpt.Tables[0].TableName = "dsnoofdelivery";
            dsApprvlDetail.Tables[0].TableName = "dsApprovalDetail";

            if (dsecommRpt.Tables[0].Rows.Count > 0)
            {
                HttpContext.Current.Session["ReportDS"] = dsecommRpt;
                HttpContext.Current.Session["ReportDS1"] = dsApprvlDetail;
                HttpContext.Current.Session["Company"] = hdnSelectedCompany;
                HttpContext.Current.Session["Department"] = hdnSelectedDepartment;
                HttpContext.Current.Session["SelObject"] = invoker;
                result = Convert.ToInt16(dsecommRpt.Tables[0].Rows.Count);
            }
            else { result = 0; }

            return result;
        }
        //private void bindGridGv()
        //{
        //    DataSet dsexport = new DataSet();
        //    dsexport = UCCommonFilterClient.ExportDatainExcel(FromDt, ToDt, SelectedCompany, SelectedDepartment, SelectedStatus, SelectedUser, SelectedPaymentMode, profile.DBConnection._constr);
        //    HttpContext.Current.Session["ExcelReport"] = dsexport;
        //    GVexport.DataSource = dsexport;
        //    GVexport.DataBind();
        //}
        //export data on excel
        protected void btnexportdta_Click(object sender, EventArgs e)
        {
            iUCCommonFilterClient UCCommonFilterClient = new iUCCommonFilterClient();
            CustomProfile profile = CustomProfile.GetProfile();
            try
            {
                DataSet ds1 = new DataSet();
                ds1 = (DataSet)Session["ReportDS1"];


            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "CommonReport", "btnexportdta_Click");
            }
            finally
            {
                UCCommonFilterClient.Close();
            }
        }

        [WebMethod]
        public static int WMGetDriverSchedule(string invoker, string txtFromDt, string txtToDt, string hdnSelectedDriver, string hdnSelectedLocation, string hdnSelectedToLocation, string hdnSelectedRoute)
        {
            int result = 0;
            DataSet dsecommRpt = new DataSet();
            iUCCommonFilterClient UCCommonFilterClient = new iUCCommonFilterClient();
            CustomProfile profile = CustomProfile.GetProfile();
            string companyid = profile.Personal.CompanyID.ToString();
            string Usertype = profile.Personal.UserType.ToString();
            HttpContext.Current.Session["ReportHeading"] = "Driver Schedule Report";
            if (Usertype == "Super Admin")
            {
                // dsecommRpt = UCCommonFilterClient.GetDriverSchedule(txtFromDt, txtToDt, hdnSelectedDriver, hdnSelectedLocation, hdnSelectedToLocation, hdnSelectedRoute, "0", profile.DBConnection._constr);
            }
            else
            {
                // dsecommRpt = UCCommonFilterClient.GetDriverSchedule(txtFromDt, txtToDt, hdnSelectedDriver, hdnSelectedLocation, hdnSelectedToLocation, hdnSelectedRoute, companyid, profile.DBConnection._constr);
            }

            if (hdnSelectedDriver == "0" || hdnSelectedDriver == "") { hdnSelectedDriver = "All"; }
            else
            {// hdnSelectedDriver = UCCommonFilterClient.GetDriverName(hdnSelectedDriver, profile.DBConnection._constr); 
            }

            if (hdnSelectedLocation == "0" || hdnSelectedLocation == "") { hdnSelectedLocation = "All"; }
            else
            { //hdnSelectedLocation = UCCommonFilterClient.GetLocationName(hdnSelectedLocation, profile.DBConnection._constr); 
            }

            if (hdnSelectedRoute == "0" || hdnSelectedRoute == "")
            { hdnSelectedRoute = "All"; }
            else
            { //hdnSelectedRoute = UCCommonFilterClient.GetRouteName(hdnSelectedRoute, profile.DBConnection._constr); 
            }

            dsecommRpt.Tables[0].TableName = "dsdriverschedule";
            if (dsecommRpt.Tables[0].Rows.Count > 0)
            {
                HttpContext.Current.Session["ReportDS"] = dsecommRpt;
                HttpContext.Current.Session["Driver"] = hdnSelectedDriver;
                HttpContext.Current.Session["Location"] = hdnSelectedLocation;
                HttpContext.Current.Session["Route"] = hdnSelectedRoute;
                HttpContext.Current.Session["SelObject"] = invoker;
                result = Convert.ToInt16(dsecommRpt.Tables[0].Rows.Count);
            }
            else { result = 0; }
            return result;
        }




    }


}