using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PowerOnRentwebapp.DashBoard;
using System.Web.UI.DataVisualization.Charting;
using PowerOnRentwebapp.Login;
//using PowerOnRentwebapp.PORServiceSiteMaster;
using Microsoft.Reporting.WebForms;
using PowerOnRentwebapp.UserCreationService;
using PowerOnRentwebapp.PORServiceUCCommonFilter;
using PowerOnRentwebapp.PORServicePartRequest;
using PowerOnRentwebapp.PORServicePartIssue;
using PowerOnRentwebapp.PORServicePartReceipts;
using PowerOnRentwebapp.ProductCategoryService;
using PowerOnRentwebapp.UCProductSearchService;
using System.Data;
using System.Collections;
using System.Resources;
using System.Globalization;
using System.Threading;
using System.Reflection;
using System.Xml.Linq;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Services;
//using System.Data.Linq;
//using System.Data.Linq.SqlClient;

//namespace WebClientElegantCRM.PowerOnRent
namespace PowerOnRentwebapp.PowerOnRent
{
    public partial class UCCommonFilter : System.Web.UI.UserControl
    {
        ResourceManager rm;
        CultureInfo ci;
        public string hfCount_lcl, hdnEngineSelectedRec_lcl, hdnProductSelectedRec_lcl, frmdt_lcl, todt_lcl, hdnRequestSelectedRec_lcl, hdnIssueSelectedRec_lcl, hdnReceiptSelectedRec_lcl, hdnProductCategory_lcl;

        protected void Page_Load(object sender, EventArgs e)
        {
            string Language = "";
            if (Session["Lang"] == null)
            {
                Session["Lang"] = Request.UserLanguages[0];
                Language = Convert.ToString(Session["Lang"]);
            }
            if (string.IsNullOrEmpty((string)Session["Lang"]))
            {
                Session["Lang"] = Request.UserLanguages[0];
            }
            Language = Convert.ToString(Session["Lang"]);
            loadstring();
            hndGroupByGrid.Value = GVEngineInfo.GroupBy;
            hndGroupByPrd.Value = GVProductInfo.GroupBy;
            if (!IsPostBack)
            {
                //Get Order No
                GetWebOrderNumber();
                FillDriver();
                //fillRoute();
                fillsite();
                fillsitecode();
                fillCategory();
                fillStatus();
                fillCompany();
                BindRole();
                fillLocation();
                fillsku();
                //Created by suraj khopade
                fillReturnCustomer();
                fillReturnStatus();
                fillReturnDriver();
                fillTransactionCategory();
                GVreturncollectionreport_OnRebind(sender, e);
                // TransactionReportgridexporttopdf_Rebind(sender, e);
                //TransactionReportgridexporttoEXCEL_Rebind(sender, e);
                //Created by suraj khopade
                GVRequestInfo_OnRebind(sender, e);
                GVEngineInfo_OnRebind(sender, e);
                GVIssueInfo_OnRebind(sender, e);
                GVReceiptInfo_OnRebind(sender, e);
                GVProductInfo_OnRebind(sender, e);
                GVUserInfo_OnRebind(sender, e);
                gridexport_Rebind(sender, e);
                FrmDate.Date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                To_Date.Date = DateTime.Now.Date;

                /*suraj*/
                //if (Language != "ar-QA")
                //{
                //    FrmDate.Date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                //    To_Date.Date = DateTime.Now.Date;
                //   // frmdt_lcl = FrmDate.Date.Value.ToString("yyyy/MM/dd");
                //   // hdnNewFDt.Value = frmdt_lcl;
                //   // todt_lcl = To_Date.Date.Value.ToString("yyyy/MM/dd");
                //   // hdnNewTDt.Value = todt_lcl;

                //    frmdt_lcl = FrmDate.Date.Value.ToString("dd/MMM/yyyy");
                //    todt_lcl = To_Date.Date.Value.ToString("dd/MMM/yyyy");
                //    hdnNewFDt.Value = frmdt_lcl;
                //    hdnNewTDt.Value = todt_lcl;


                //}
                //else if (Language == "ar-QA")
                //{
                //    FrmDate.Date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                //    To_Date.Date = DateTime.Now.Date;
                //    frmdt_lcl = FrmDate.Date.Value.ToString("dd/MMM/yyyy");
                //    todt_lcl = To_Date.Date.Value.ToString("dd/MMM/yyyy");
                //    //FDate.Attributes.Add("style", "visibility: hidden");
                //    // TDate.Attributes.Add("style", "visibility: hidden");
                //    hdnNewFDt.Value = frmdt_lcl;
                //    hdnNewTDt.Value = todt_lcl;
                //}
                /*suraj*/

                //if (Request.QueryString["invoker"] == "orderdetail")
                //{ lblselectallorder.Visible = false; chkSelectAll.Visible = false; } else { lblselectallorder.Visible = true; chkSelectAll.Visible = true; }
                //if (Request.QueryString["invoker"] == "SkuDetails") { lblallsku.Visible = false; chkSelectProduct.Visible = false; } else { lblallsku.Visible = true; chkSelectProduct.Visible = true; }
                //if (Request.QueryString["invoker"] == "deliverynote") { lblselectallorder.Visible = false; chkSelectAll.Visible = false; } else { lblselectallorder.Visible = true; chkSelectAll.Visible = true; }
                //if (Request.QueryString["invoker"] == "noofdelivery") { lblselectallorder.Visible = false; chkSelectAll.Visible = false; } else { lblselectallorder.Visible = true; chkSelectAll.Visible = true; }
                //if (Request.QueryString["invoker"] == "noofdelivery") { selall.Visible = false; } else { selall.Visible = true; }
                //if (Request.QueryString["invoker"] == "ecommerce1") { lblselectallorder.Visible = false; chkSelectAll.Visible = false; } else { lblselectallorder.Visible = true; chkSelectAll.Visible = true; }


                if (Request.QueryString["invoker"] == "orderdetail" || Request.QueryString["invoker"] == "deliverynote" || Request.QueryString["invoker"] == "noofdelivery" || Request.QueryString["invoker"] == "ecommerce1")
                { lblselectallorder.Visible = false; chkSelectAll.Visible = false; }
                else
                { lblselectallorder.Visible = true; chkSelectAll.Visible = true; }
                if (Request.QueryString["invoker"] == "SkuDetails") { lblallsku.Visible = false; chkSelectProduct.Visible = false; } else { lblallsku.Visible = true; chkSelectProduct.Visible = true; }
                if (Request.QueryString["invoker"] == "noofdelivery") { selall.Visible = false; } else { selall.Visible = true; }


            }

            hfCount_lcl = hfCount.Value;
            hdnEngineSelectedRec_lcl = hdnEngineSelectedRec.Value;
            hdnProductSelectedRec_lcl = hdnProductSelectedRec.Value;
            frmdt_lcl = FrmDate.Date.Value.ToString("yyyy/MM/dd"); hdnNewFDt.Value = frmdt_lcl;
            todt_lcl = To_Date.Date.Value.ToString("yyyy/MM/dd"); hdnNewTDt.Value = todt_lcl;
            hdnRequestSelectedRec_lcl = hdnRequestSelectedRec.Value;
            hdnIssueSelectedRec_lcl = hdnIssueSelectedRec.Value;
            hdnReceiptSelectedRec_lcl = hdnReceiptSelectedRec.Value;
            hdnProductCategory_lcl = hdnProductCategory.Value;

            //asign uid
            CustomProfile profile = CustomProfile.GetProfile();
            hdnLoginUid.Value =Convert.ToString(profile.Personal.UserID);

        }

        private void GetWebOrderNumber()
        {
            hdnOrderNo.Value = txtordno.Text;
        }

        public void BindRole()
        {
            RoleMasterService.iRoleMasterClient roleMasterService = new RoleMasterService.iRoleMasterClient();
            CustomProfile profile = CustomProfile.GetProfile();
            ddlrole.DataSource = roleMasterService.GetRoleList(profile.DBConnection._constr);
            ddlrole.DataBind();
            ListItem lst = new ListItem();
            lst.Text = "--SelectAll--";
            lst.Value = "0";
            ddlrole.Items.Insert(0, lst);

        }
        public void fillsku()
        {
            iUCCommonFilterClient UCCommonFilter = new iUCCommonFilterClient();
            CustomProfile profile = CustomProfile.GetProfile();
            DataSet dssku = new DataSet();
            dssku = UCCommonFilter.BindDDLSku(profile.Personal.CompanyID,profile.Personal.DepartmentID,profile.Personal.UserID, profile.DBConnection._constr);
            ddlsku.DataSource = dssku;
            ddlsku.DataBind();
            ListItem lst = new ListItem();
            lst.Text = "-Select All-";
            lst.Value = "0";
            ddlsku.Items.Insert(0, lst);
        }
        public void fillsitecode()
        {
            iUCCommonFilterClient UCCommonFilter = new iUCCommonFilterClient();
            CustomProfile profile = CustomProfile.GetProfile();
            DataSet dssku = new DataSet();
            dssku = UCCommonFilter.BindDDLSitecode(profile.Personal.CompanyID, profile.Personal.DepartmentID, profile.DBConnection._constr);
            ddlsitecode.DataSource = dssku;
            ddlsitecode.DataBind();
            ListItem lst = new ListItem();
            lst.Text = "-Select All-";
            lst.Value = "0";
            ddlsitecode.Items.Insert(0, lst);
        }

        public void fillStatus()
        {
            iUCCommonFilterClient UCCommonFilter = new iUCCommonFilterClient();
            CustomProfile profile = CustomProfile.GetProfile();
            string invkr = Request.QueryString["invoker"].ToString();
            if (invkr == "avgtime" || invkr == "noofdelivery")
            {
               // ddlStatus.DataSource = UCCommonFilter.GetStatusforGPS(profile.DBConnection._constr);
            }
            else if (invkr == "qnbnstock" || invkr == "qnbnorder")
            {
                ddlStatus.DataSource = UCCommonFilter.GetQNBNStatus(profile.DBConnection._constr);
            }
            else if (invkr == "ecommerce1" || invkr == "ecommerce2")
            {
                ddlStatus.DataSource = UCCommonFilter.GetEcommStatus(profile.DBConnection._constr);
            }
            else
            {
                ddlStatus.DataSource = UCCommonFilter.GetStatus(profile.DBConnection._constr);
            }
            ddlStatus.DataBind();
            ListItem lst = new ListItem();
            lst.Text = "-Select All-";
            lst.Value = "0";
            ddlStatus.Items.Insert(0, lst);
        }

        public void fillCompany()
        {
            iUCCommonFilterClient UCCommonFilter = new iUCCommonFilterClient();
            CustomProfile profile = CustomProfile.GetProfile();

            ddlcompany.Enabled = true;
            ddldepartment.Enabled = true;
            long UID = profile.Personal.UserID;

            string UsrType = profile.Personal.UserType.ToString();
            if (UsrType == "Super Admin")
            {
                ddlcompany.DataSource = UCCommonFilter.GetCompanyName(profile.DBConnection._constr);
            }
            else
            {
                //ddlcompany.DataSource = UCCommonFilter.GetUserCompanyName(UID, profile.DBConnection._constr);     //UCCommonFilter.GetCompanyName(profile.DBConnection._constr);
                ddlcompany.DataSource = UCCommonFilter.GetUserCompanyNameNEW(UID, profile.DBConnection._constr);
            }

            //  ddlcompany.DataSource = UCCommonFilter.GetUserCompanyName(UID, profile.DBConnection._constr);     //UCCommonFilter.GetCompanyName(profile.DBConnection._constr);
            ddlcompany.DataBind();
            ListItem lst = new ListItem();
            lst.Text = "-Select All-";
            lst.Value = "0";
            ddlcompany.Items.Insert(0, lst);
            ddlcompany.SelectedIndex = 1;
            hdnSelectedCompany.Value = ddlcompany.SelectedValue.ToString();
            List<mTerritory> SiteLst = new List<mTerritory>();
            // SiteLst = UCCommonFilter.GetSiteNameByUserID(Convert.ToInt16(UID), profile.DBConnection._constr).ToList();
         
            //SiteLst = UCCommonFilter.GetAddedDepartmentList(Convert.ToInt16(hdnSelectedCompany.Value), Convert.ToInt16(UID), profile.DBConnection._constr).ToList();

            //add by suraj

            if (UsrType == "Super Admin")
            {
                SiteLst = UCCommonFilter.GetDepartmentList(Convert.ToInt16(hdnSelectedCompany.Value), profile.DBConnection._constr).ToList();
            }
            else
            {
                SiteLst = UCCommonFilter.GetAddedDepartmentList(Convert.ToInt16(hdnSelectedCompany.Value), Convert.ToInt16(UID), profile.DBConnection._constr).ToList();
            }

            ddldepartment.DataSource = SiteLst;
            ddldepartment.DataBind();
            ListItem lst1 = new ListItem();
            lst1.Text = "--Select--";
            lst1.Value = "0";
            ddldepartment.Items.Insert(0, lst1);
            //ListItem l = new ListItem();
            //l.Text = "Select All";
            //l.Value = "1";
            //ddldepartment.Items.Insert(1, l);
           // if (ddldepartment.Items.Count > 0) ddldepartment.SelectedIndex = 2;
            ddldepartment.SelectedIndex = 1;

            long DeptID = UCCommonFilter.GetSiteIdOfUser(UID, profile.DBConnection._constr);
            hdnSelectedDepartment.Value = DeptID.ToString();
            long CompanyID = UCCommonFilter.GetCompanyIDFromSiteID(DeptID, profile.DBConnection._constr); hdnSelectedCompany.Value = CompanyID.ToString();
            fillPaymentMethod(DeptID);

            string invkr = Request.QueryString["invoker"].ToString();

            if (invkr == "sku" || invkr == "SkuDetails" || invkr == "BomDetail" || invkr == "qnbnstock")
            {
                ddlgset.SelectedIndex = 0;
                hdnSelectedGroupSet.Value = "0";

                ddlImage.SelectedIndex = 1;
                hdnSelectedImage.Value = "1";
            }
            else if (invkr == "order" || invkr == "orderdetail" || invkr == "orderlead" || invkr == "qnbnorder" ||invkr=="vat")
            {
                DataSet ds = new DataSet();
                //List<SP_GWC_GetUserInfo_Result> UserList = new List<SP_GWC_GetUserInfo_Result>();
                //UserList = UCCommonFilter.GetUsrLst1(hdnSelectedCompany.Value, hdnSelectedDepartment.Value, profile.DBConnection._constr).ToList();
                List<SP_GetUsers_Result> UsersList = new List<SP_GetUsers_Result>();
                UsersList = UCCommonFilter.GetUsersDepartmentWise(hdnSelectedCompany.Value, hdnSelectedDepartment.Value, profile.DBConnection._constr).ToList();
                ddlUser.DataSource = UsersList;
                ddlUser.DataBind();
                ListItem lusr = new ListItem();
                lusr.Text = "Select All";
                lusr.Value = "0";
                ddlUser.Items.Insert(0, lusr);
                if (ddlUser.Items.Count > 0) ddlUser.SelectedIndex = 0;
                hdnSelectedUser.Value = ddlUser.SelectedItem.Value.ToString();

                // ddlStatus.SelectedIndex = 0;
                hdnSelectedStatus.Value = "0";// ddlStatus.SelectedValue.ToString();// ddlStatus.SelectedItem.Value.ToString();
            }
            else if (invkr == "user")
            {
                hdnSelectedRole.Value = "0";

                ddlActive.SelectedIndex = 1;
                hdnSelectedActive.Value = "Yes";
            }
            else if (invkr == "imgaudit")
            {
                DataSet ds = new DataSet();
                List<SP_GWC_GetUserInfo_Result> UserList = new List<SP_GWC_GetUserInfo_Result>();
                UserList = UCCommonFilter.GetUsrLst1(hdnSelectedCompany.Value, hdnSelectedDepartment.Value, profile.DBConnection._constr).ToList();

                ddlUser.DataSource = UserList;
                ddlUser.DataBind();
                ListItem lusr = new ListItem();
                lusr.Text = "Select All";
                lusr.Value = "0";
                ddlUser.Items.Insert(0, lusr);
                if (ddlUser.Items.Count > 0) ddlUser.SelectedIndex = 0;
                hdnSelectedUser.Value = ddlUser.SelectedItem.Value.ToString();

                ddlImgstatus.SelectedIndex = 1;
                hdnImgStatus.Value = "Success";
            }
            else if (invkr == "orderdelivery")
            {
                ddlDriver.SelectedIndex = 0;
                hdnSelectedDriver.Value = ddlDriver.SelectedValue.ToString();

                ddlPytMode.SelectedIndex = 0;
                hdnSelectedPaymentMode.Value = ddlPytMode.SelectedValue.ToString();

            }

            else if (invkr == "sla")
            {
                ddlDriver.SelectedIndex = 0;
                hdnSelectedDriver.Value = ddlDriver.SelectedValue.ToString();

                ddlDlrytype.SelectedIndex = 0;
                hdnSelectedDeliveryType.Value = ddlDlrytype.SelectedValue.ToString();
            }


            else if (invkr == "ecommerce1")
            {
                DataSet ds = new DataSet();
                List<SP_GWC_GetUserInfo_Result> UserList = new List<SP_GWC_GetUserInfo_Result>();
                UserList = UCCommonFilter.GetUsrLst1(hdnSelectedCompany.Value, hdnSelectedDepartment.Value, profile.DBConnection._constr).ToList();
                if (UsrType == "Super Admin")
                {
                    ds = UCCommonFilter.GetUserListall(hdnSelectedCompany.Value, hdnSelectedDepartment.Value, profile.DBConnection._constr);
                    ddlUser.DataSource = ds;
                }
                else
                {
                    ds = UCCommonFilter.GetUserList(hdnSelectedCompany.Value, hdnSelectedDepartment.Value, profile.DBConnection._constr);
                    ddlUser.DataSource = ds;
                }              
                ddlUser.DataBind();
                ListItem lusr = new ListItem();
                lusr.Text = "Select All";
                lusr.Value = "0";
                ddlUser.Items.Insert(0, lusr);
                if (ddlUser.Items.Count > 0) ddlUser.SelectedIndex = 0;
                hdnSelectedUser.Value = ddlUser.SelectedItem.Value.ToString();

                ddlImgstatus.SelectedIndex = 1;
                hdnImgStatus.Value = "Success";

                ddlPytMode.SelectedIndex = 0;
                hdnSelectedPaymentMode.Value = ddlPytMode.SelectedValue.ToString();
            }

            else if (invkr == "ecommerce2")
            {
                DataSet ds = new DataSet();
                List<SP_GWC_GetUserInfo_Result> UserList = new List<SP_GWC_GetUserInfo_Result>();
                UserList = UCCommonFilter.GetUsrLst1(hdnSelectedCompany.Value, hdnSelectedDepartment.Value, profile.DBConnection._constr).ToList();

                if (UsrType == "Super Admin")
                {
                    ds = UCCommonFilter.GetUserList(hdnSelectedCompany.Value, hdnSelectedDepartment.Value, profile.DBConnection._constr);
                    ddlUser.DataSource = ds;
                }
                else
                {
                    ds = UCCommonFilter.GetUserList(hdnSelectedCompany.Value, hdnSelectedDepartment.Value, profile.DBConnection._constr);
                    ddlUser.DataSource = ds;
                }
                ddlUser.DataBind();
                ListItem lusr = new ListItem();
                lusr.Text = "Select All";
                lusr.Value = "0";
                ddlUser.Items.Insert(0, lusr);
                if (ddlUser.Items.Count > 0) ddlUser.SelectedIndex = 0;
                hdnSelectedUser.Value = ddlUser.SelectedItem.Value.ToString();

                ddlImgstatus.SelectedIndex = 1;
                hdnImgStatus.Value = "Success";

                ddlPytMode.SelectedIndex = 0;
                hdnSelectedPaymentMode.Value = ddlPytMode.SelectedValue.ToString();
            }

            else if (invkr == "deliverylogrpt")
            {

                DataSet ds = new DataSet();
                ds = UCCommonFilter.GetDriverList(hdnSelectedCompany.Value, hdnSelectedDepartment.Value, profile.DBConnection._constr);
                ddlUser.DataSource = ds;
                ddlUser.DataBind();
                ListItem lusr = new ListItem();
                lusr.Text = "Select All";
                lusr.Value = "0";
                ddlUser.Items.Insert(0, lusr);
                if (ddlUser.Items.Count > 0) ddlUser.SelectedIndex = 0;
                hdnSelectedUser.Value = ddlUser.SelectedItem.Value.ToString();

                DataSet ds1 = new DataSet();
                ds1 = UCCommonFilter.GetDriverStatusList(profile.DBConnection._constr);
                ddlStatus.DataSource = ds1;
                ddlStatus.DataBind();
                ListItem lusr1 = new ListItem();
                lusr1.Text = "Select All";
                lusr1.Value = "0";
                ddlStatus.Items.Insert(0, lusr1);
                if (ddlStatus.Items.Count > 0) ddlStatus.SelectedIndex = 0;
                hdnSelectedStatus.Value = ddlStatus.SelectedItem.Value.ToString();

                ddlImgstatus.SelectedIndex = 1;
                hdnImgStatus.Value = "Success";

                ddlPytMode.SelectedIndex = 0;
                hdnSelectedPaymentMode.Value = ddlPytMode.SelectedValue.ToString();
            }
            
            else if (invkr == "qnbnstock" || invkr == "qnbnorder")
            {
                if (UsrType == "Super Admin" || UsrType == "Admin")
                {
                    ddlcompany.Enabled = true;
                    ddldepartment.Enabled = true;
                }
                else
                {
                    ddlcompany.Enabled = false;
                    ddldepartment.Enabled = false;
                }

            }
            else if (invkr == "depttrack")
            {
                DataSet ds1 = new DataSet();
                ds1 = UCCommonFilter.GetTechnicalDept(profile.Personal.UserID,profile.Personal.CompanyID,profile.DBConnection._constr);
                ddldepartment.DataSource = ds1;
                ddldepartment.DataBind();
                ListItem lusr1 = new ListItem();
                lusr1.Text = "Select All";
                lusr1.Value = "0";
                ddldepartment.Items.Insert(0, lusr1);
            }


        }

        //change by suraj khopade
        public void fillTransactionCategory()
        {
            iUCCommonFilterClient UCCommonFilter = new iUCCommonFilterClient();
            CustomProfile profile = CustomProfile.GetProfile();

            ddltransactioncategory.Enabled = true;
            //ddlcateria.Enabled = true;
            long UID = profile.Personal.UserID;

            string UsrType = profile.Personal.UserType.ToString();

            ddltransactioncategory.DataSource = UCCommonFilter.GetTransactionCategory(profile.DBConnection._constr);

            ddltransactioncategory.DataBind();
            ListItem lst = new ListItem();
            lst.Text = "-Select All-";
            lst.Value = "0";
            ddltransactioncategory.Items.Insert(0, lst);
            //hdnSelectedcategory.Value = ddltransactioncategory.SelectedValue.ToString();
            //hdncateria.Text = UCCommonFilter.GetcriteriaList(Convert.ToInt16(hdnSelectedcategory.Value), profile.DBConnection._constr);//

            //ddlcateria.DataSource = UCCommonFilter.GetcriteriaList(Convert.ToInt16(hdnSelectedcategory.Value), profile.DBConnection._constr);//

            //ddlcateria.DataBind();
            //ListItem lst1 = new ListItem();
            //lst1.Text = "--Select--";
            //lst1.Value = "0";
            //ddlcateria.Items.Insert(0, lst1);
            //ddlcateria.SelectedIndex = 1;
            //hdncateria.Text = ddlcateria.SelectedValue.ToString();
        }
        //created by suraj khopade
        public void fillDepartment()
        {
            iUCCommonFilterClient UCCommonFilter = new iUCCommonFilterClient();
            CustomProfile profile = CustomProfile.GetProfile();
            // ddldepartment.DataSource = UCCommonFilter.GetDepartmentName(Convert.ToInt32(hdnSelectedCompany.Value), profile.DBConnection._constr);
            ddldepartment.DataBind();
            ListItem lst = new ListItem();
            lst.Text = "--Select--";
            lst.Value = "0";
            ddldepartment.Items.Insert(0, lst);
            ListItem l = new ListItem();
            l.Text = "Select All";
            l.Value = "1";
            ddldepartment.Items.Insert(1, l);
        }


        public void fillsite()
        {
            iUCCommonFilterClient UCCommonFilter = new iUCCommonFilterClient();
            CustomProfile profile = CustomProfile.GetProfile();
            string UsrType = profile.Personal.UserType.ToString();
            if (UsrType == "Super Admin")
            {
                ddlSite.DataSource = UCCommonFilter.GetAllSites(profile.DBConnection._constr);
            }
            else
            {
                ddlSite.DataSource = UCCommonFilter.GetSiteNameByUserID(profile.Personal.UserID, profile.DBConnection._constr);
            }
            ddlSite.DataBind();
            ListItem lst = new ListItem();
            lst.Text = "--Select--";
            lst.Value = "0";
            ddlSite.Items.Insert(0, lst);
            ListItem l = new ListItem();
            l.Text = "Select All";
            l.Value = "1";
            ddlSite.Items.Insert(1, l);

            ddlFrmSite.DataSource = UCCommonFilter.GetSiteNameByUserID_Transfer(profile.Personal.UserID, profile.DBConnection._constr);
            ddlFrmSite.DataBind();
            ListItem lstfrm = new ListItem();
            lstfrm.Text = "--Select--";
            lstfrm.Value = "0";
            ddlFrmSite.Items.Insert(0, lstfrm);
        }

        public void fillCategory()
        {
            iUCCommonFilterClient UCCommonFilter = new iUCCommonFilterClient();
            iProductCategoryMasterClient ProductCategory = new iProductCategoryMasterClient();
            CustomProfile profile = CustomProfile.GetProfile();
            ddlCategory.DataSource = ProductCategory.GetProductCategoryList(profile.DBConnection._constr);
            ddlCategory.DataBind();
            ListItem lst1 = new ListItem();
            lst1.Text = "--Select--";
            lst1.Value = "0";
            ddlCategory.Items.Insert(0, lst1);
        }

        public void fillLocation()
        {
            iUCCommonFilterClient UCCommonFilter = new iUCCommonFilterClient();
            CustomProfile profile = CustomProfile.GetProfile();
            ddlLocation.DataSource = UCCommonFilter.GetLocationOfUser(profile.Personal.UserID, profile.DBConnection._constr);
            ddlLocation.DataBind();
            ListItem lstLoc = new ListItem();
            lstLoc.Text = "--Select All--";
            lstLoc.Value = "0";
            ddlLocation.Items.Insert(0, lstLoc);
            ListItem lstToLoc = new ListItem();
            lstToLoc.Text = "--Select All--";
            lstToLoc.Value = "0";
            ddltoLocation.Items.Insert(0, lstToLoc);

        }
        public void fillReturnOrderDetail()
        {
            iUCCommonFilterClient UCCommonFilter = new iUCCommonFilterClient();
            iPartRequestClient PartRequest = new iPartRequestClient();
            iPartIssueClient PartIssue = new iPartIssueClient();
            iPartReceiptClient PartReceipt = new iPartReceiptClient();

            iUserCreationClient usercreation = new iUserCreationClient();

            CustomProfile profile = CustomProfile.GetProfile();
            DataSet ReqDtLst = new DataSet();
            CustomProfile profile1 = CustomProfile.GetProfile();
            
            string comid = profile.Personal.CompanyID.ToString();
            string utype = profile.Personal.UserType.ToString();

            var SearchedValue = hdnFilterText.Value;

            /* if (hdnSelectedReturnCustomer.Value == null || hdnSelectedReturnCustomer.Value == "0") hdnSelectedReturnCustomer.Value = "";
             if (hdnSelectedReturnDepartment.Value == null || hdnSelectedReturnDepartment.Value == "0") hdnSelectedReturnDepartment.Value = "";
             if (hdnSelectedUser.Value == null || hdnSelectedUser.Value == "0") hdnSelectedUser.Value = "";
             //if (ddlStatus.SelectedValue == null || ddlStatus.SelectedValue == "0") ddlStatus.SelectedValue = ""; ddlReturnStatus
             if (ddlReturnStatus.SelectedValue == null || ddlReturnStatus.SelectedValue == "0") ddlReturnStatus.SelectedValue = "";*/
            if (frmdt_lcl == null) frmdt_lcl = "0";
            if (todt_lcl == null) todt_lcl = "0";
            string driver =  hdnSelectedReturnDriver.Value;

            try
            {
                if (hdnSelectedReturnCustomer.Value == "0") { hdnSelectedReturnDepartment.Value = "0"; }
                if (utype == "Super Admin")
                {
                    ReqDtLst = usercreation.GetAllReturnOrdercollectionreport(frmdt_lcl, todt_lcl, hdnSelectedReturnCustomer.Value, hdnSelectedReturnDepartment.Value, hdnSelectedUser.Value, ddlReturnStatus.SelectedValue, hdnSelectedReturnDriver.Value, profile.DBConnection._constr);
                    // ReqDtLst = UCCommonFilter.GetAllOrderDetails(frmdt_lcl, todt_lcl, hdnSelectedCompany.Value, hdnSelectedDepartment.Value, hdnSelectedUser.Value, ddlStatus.SelectedValue, profile.DBConnection._constr);
                }
                else
                {
                    hdnSelectedReturnCustomer.Value = comid;
                    ReqDtLst = usercreation.GetAllReturnOrdercollectionreport(frmdt_lcl, todt_lcl, hdnSelectedReturnCustomer.Value, hdnSelectedReturnDepartment.Value, hdnSelectedUser.Value, ddlReturnStatus.SelectedValue, hdnSelectedReturnDriver.Value, profile.DBConnection._constr);
                    //ReqDtLst = UCCommonFilter.GetAllOrderDetails(frmdt_lcl, todt_lcl, hdnSelectedCompany.Value, hdnSelectedDepartment.Value, hdnSelectedUser.Value, ddlStatus.SelectedValue, profile.DBConnection._constr);
                }
                GVreturncollectionreport.DataSource = ReqDtLst;
                GVreturncollectionreport.DataBind();
            }
            catch  (Exception ex){ }
            finally { }
        }

        public void fillReturnOrderSummaryReportDetail()
        {
            iUCCommonFilterClient UCCommonFilter = new iUCCommonFilterClient();
            iPartRequestClient PartRequest = new iPartRequestClient();
            iPartIssueClient PartIssue = new iPartIssueClient();
            iPartReceiptClient PartReceipt = new iPartReceiptClient();

            iUserCreationClient usercreation = new iUserCreationClient();

            CustomProfile profile = CustomProfile.GetProfile();
            DataSet ReqDtLst = new DataSet();
            CustomProfile profile1 = CustomProfile.GetProfile();

            string comid = profile.Personal.CompanyID.ToString();
            string utype = profile.Personal.UserType.ToString();

            var SearchedValue = hdnFilterText.Value;

            /* if (hdnSelectedReturnCustomer.Value == null || hdnSelectedReturnCustomer.Value == "0") hdnSelectedReturnCustomer.Value = "";
             if (hdnSelectedReturnDepartment.Value == null || hdnSelectedReturnDepartment.Value == "0") hdnSelectedReturnDepartment.Value = "";
             if (hdnSelectedUser.Value == null || hdnSelectedUser.Value == "0") hdnSelectedUser.Value = "";
             //if (ddlStatus.SelectedValue == null || ddlStatus.SelectedValue == "0") ddlStatus.SelectedValue = ""; ddlReturnStatus
             if (ddlReturnStatus.SelectedValue == null || ddlReturnStatus.SelectedValue == "0") ddlReturnStatus.SelectedValue = "";*/
            if (frmdt_lcl == null) frmdt_lcl = "0";
            if (todt_lcl == null) todt_lcl = "0";
            //string driver = hdnSelectedReturnDriver.Value;

            try
            {
                if (hdnSelectedReturnCustomer.Value == "0") { hdnSelectedReturnDepartment.Value = "0"; }
                if (utype == "Super Admin")
                {
                    ReqDtLst = usercreation.GetAllReturnOrderSummaryreport(frmdt_lcl, todt_lcl, hdnSelectedReturnCustomer.Value, hdnSelectedReturnDepartment.Value, hdnSelectedUser.Value, ddlReturnStatus.SelectedValue,  profile.DBConnection._constr);
                    // ReqDtLst = UCCommonFilter.GetAllOrderDetails(frmdt_lcl, todt_lcl, hdnSelectedCompany.Value, hdnSelectedDepartment.Value, hdnSelectedUser.Value, ddlStatus.SelectedValue, profile.DBConnection._constr);
                }
                else
                {
                    hdnSelectedReturnCustomer.Value = comid;
                    ReqDtLst = usercreation.GetAllReturnOrderSummaryreport(frmdt_lcl, todt_lcl, hdnSelectedReturnCustomer.Value, hdnSelectedReturnDepartment.Value, hdnSelectedUser.Value, ddlReturnStatus.SelectedValue, profile.DBConnection._constr);
                    //ReqDtLst = UCCommonFilter.GetAllOrderDetails(frmdt_lcl, todt_lcl, hdnSelectedCompany.Value, hdnSelectedDepartment.Value, hdnSelectedUser.Value, ddlStatus.SelectedValue, profile.DBConnection._constr);
                }
                GVreturncollectionreport.DataSource = ReqDtLst;
                GVreturncollectionreport.DataBind();
            }
            catch (Exception ex) { }
            finally { }
        }
        public void fillDetail()

        {
            iUCCommonFilterClient UCCommonFilter = new iUCCommonFilterClient();
            iPartRequestClient PartRequest = new iPartRequestClient();
            iPartIssueClient PartIssue = new iPartIssueClient();
            iPartReceiptClient PartReceipt = new iPartReceiptClient();
            CustomProfile profile = CustomProfile.GetProfile();
            if (ddlSite.SelectedIndex >= -1)
            {

                if (Request.QueryString["invoker"] == "partrequest")
                {
                    try
                    {
                        List<POR_SP_GetRequestBySiteIDsOrUserID_Result> RequestList = new List<POR_SP_GetRequestBySiteIDsOrUserID_Result>();
                        List<POR_SP_GetRequestBySiteIDsOrUserID_Result> FinalRequestList = new List<POR_SP_GetRequestBySiteIDsOrUserID_Result>();

                        //GVRequestInfo.DataSource = null;
                        //GVRequestInfo.DataBind();

                        //add by suraj for ecom dash board
                        string ordtype = "";
                        if (Session["ORDType"] != null)
                        {
                            ordtype = Session["ORDType"].ToString();
                        }
                        //add by suraj for ecom dash board
                        //RequestList = PartRequest.GetRequestSummayBySiteIDs(hfCount.Value, profile.DBConnection._constr).ToList();
                        RequestList = PartRequest.GetRequestSummayBySiteIDs(hfCount.Value, profile.DBConnection._constr, ordtype).ToList();
                        RequestList = RequestList.Where(l => (l.OrderDate >= FrmDate.Date)).ToList();
                        RequestList = RequestList.Where(l => (l.OrderDate <= To_Date.Date)).ToList();
                        //if (txtRequestSearch.Text != "")
                        //{
                        //    FinalRequestList = RequestList.Where(e => e.RequestNo.Contains(txtRequestSearch.Text) || e.RequestByUserName.Contains(txtRequestSearch.Text)).ToList();
                        //    RequestList = new List<POR_SP_GetRequestBySiteIDsOrUserID_Result>();
                        //    RequestList = FinalRequestList;
                        //}
                        //GVRequestInfo.DataSource = RequestList;
                        //GVRequestInfo.DataBind();

                        if (hdnRequestSelectedRec.Value == "0")
                        {
                            GVRequestInfo.SelectedRecords = new ArrayList();
                            foreach (POR_SP_GetRequestBySiteIDsOrUserID_Result rec in RequestList)
                            {
                                Hashtable row = new Hashtable();
                                row["PRH_ID"] = rec.ID;
                                row["RequestDate"] = rec.OrderDate;
                                row["RequestByUserName"] = rec.RequestByUserName;
                                GVRequestInfo.SelectedRecords.Add(row);
                                if (hdnRequestSelectedRec.Value != "") { hdnRequestSelectedRec.Value = hdnRequestSelectedRec.Value + "," + rec.ID.ToString(); }
                                else if (hdnRequestSelectedRec.Value == "") { hdnRequestSelectedRec.Value = rec.ID.ToString(); }
                            }
                        }
                        GVRequestInfo.DataSource = RequestList;
                        GVRequestInfo.DataBind();
                    }
                    catch { }
                    finally { PartRequest.Close(); }
                }
                else
                    if (Request.QueryString["invoker"] == "partconsumption")
                {
                    try
                    {
                        //  UCProductSearchService.iUCProductSearchClient productSearchService = new UCProductSearchService.iUCProductSearchClient();

                        // GVEngineInfo.DataSource = null;
                        // GVEngineInfo.DataBind();

                        List<v_GetEngineDetails> EngineList = new List<v_GetEngineDetails>();
                        List<v_GetEngineDetails> FinalEngineList = new List<v_GetEngineDetails>();
                        EngineList = UCCommonFilter.GetEngineOfSite(hfCount.Value, profile.DBConnection._constr).ToList();
                        //if (txtEngineSearch.Text != "")
                        //{
                        //    FinalEngineList = EngineList.Where(e => e.EngineSerial.Contains(txtEngineSearch.Text) || e.GeneratorSerial.Contains(txtEngineSearch.Text)).ToList();
                        //    EngineList = new List<v_GetEngineDetails>();
                        //    EngineList = FinalEngineList;
                        //}

                        //GVEngineInfo.DataSource = EngineList;
                        //GVEngineInfo.GroupBy = hndGroupByGrid.Value;
                        //if (!Page.IsPostBack) { GVEngineInfo.GroupBy = "ProductCategory"; }
                        //GVEngineInfo.DataBind();
                        //productSearchService.Close();
                        //ID EngineSerial Container  EngineModel  EngineSerial GeneratorModel Territory
                        if (hdnEngineSelectedRec.Value == "0")
                        {
                            GVEngineInfo.SelectedRecords = new ArrayList();
                            foreach (v_GetEngineDetails rec in EngineList)
                            {
                                Hashtable row = new Hashtable();

                                row["ID"] = rec.ID;
                                row["EngineSerial"] = rec.EngineSerial;
                                row["Container"] = rec.Container;
                                row["EngineModel"] = rec.EngineModel;
                                row["EngineSerial"] = rec.EngineSerial;
                                row["GeneratorModel"] = rec.GeneratorModel;
                                row["Territory"] = rec.Territory;
                                GVEngineInfo.SelectedRecords.Add(row);
                                if (hdnEngineSelectedRec.Value != "") { hdnEngineSelectedRec.Value = hdnEngineSelectedRec.Value + "," + rec.ID.ToString(); }
                                else if (hdnEngineSelectedRec.Value == "") { hdnEngineSelectedRec.Value = rec.ID.ToString(); }
                            }
                        }
                        GVEngineInfo.DataSource = EngineList;
                        GVEngineInfo.DataBind();

                    }
                    catch { }
                    finally { UCCommonFilter.Close(); }
                }
                else
                        if (Request.QueryString["invoker"] == "partissue")
                {
                    try
                    {
                        // List<POR_SP_GetIssueSummaryBySiteIDsOrUserIDOrRequestIDOrIssueIDs_Result> IssueList = new List<POR_SP_GetIssueSummaryBySiteIDsOrUserIDOrRequestIDOrIssueIDs_Result>();
                        //  List<POR_SP_GetIssueSummaryBySiteIDsOrUserIDOrRequestIDOrIssueIDs_Result> FinalList = new List<POR_SP_GetIssueSummaryBySiteIDsOrUserIDOrRequestIDOrIssueIDs_Result>();

                        //GVIssueInfo.DataSource = null;
                        //GVIssueInfo.DataBind();

                        // IssueList = PartIssue.GetIssueSummayBySiteIDs(hfCount.Value, profile.DBConnection._constr).ToList();
                        //  IssueList = IssueList.Where(i => (i.IssueDate >= FrmDate.Date)).ToList();
                        //  IssueList = IssueList.Where(i => (i.IssueDate <= To_Date.Date)).ToList();
                        //if (txtIssueSearch.Text != "")
                        //{
                        //    FinalList = IssueList.Where(e => e.IssueNo.Contains(txtIssueSearch.Text) || e.IssuedByUserName.Contains(txtIssueSearch.Text)).ToList();
                        //    IssueList = new List<POR_SP_GetIssueSummaryBySiteIDsOrUserIDOrRequestIDOrIssueIDs_Result>();
                        //    IssueList = FinalList;
                        //}
                        //GVIssueInfo.DataSource = IssueList;
                        //GVIssueInfo.DataBind();

                        if (hdnIssueSelectedRec.Value == "0")
                        {
                            GVIssueInfo.SelectedRecords = new ArrayList();
                            /*  foreach (POR_SP_GetIssueSummaryBySiteIDsOrUserIDOrRequestIDOrIssueIDs_Result rec in IssueList)
                              {
                                  Hashtable row = new Hashtable();
                                  //MINH_ID IssueDate IssuedByUserName
                                  row["MINH_ID"] = rec.MINH_ID;
                                  row["IssueDate"] = rec.IssueDate;
                                  row["IssuedByUserName"] = rec.IssuedByUserName;
                                  GVIssueInfo.SelectedRecords.Add(row);
                                  if (hdnIssueSelectedRec.Value != "") { hdnIssueSelectedRec.Value = hdnIssueSelectedRec.Value + "," + rec.MINH_ID.ToString(); }
                                  else if (hdnIssueSelectedRec.Value == "") { hdnIssueSelectedRec.Value = rec.MINH_ID.ToString(); }
                              }*/
                        }
                        //   GVIssueInfo.DataSource = IssueList;
                        //    GVIssueInfo.DataBind();

                    }
                    catch { }
                    finally { PartIssue.Close(); }
                }
                else if (Request.QueryString["invoker"] == "partreceipt")
                {
                    try
                    {
                        /*  List<POR_SP_GetReceiptSummaryBySiteIDsOrUserIDOrRequestID_Result> ReceiptList = new List<POR_SP_GetReceiptSummaryBySiteIDsOrUserIDOrRequestID_Result>();
                          List<POR_SP_GetReceiptSummaryBySiteIDsOrUserIDOrRequestID_Result> FinalReceiptList = new List<POR_SP_GetReceiptSummaryBySiteIDsOrUserIDOrRequestID_Result>();

                          //GVReceiptInfo.DataSource = null;
                          //GVReceiptInfo.DataBind();

                          ReceiptList = PartReceipt.GetReceiptSummaryBySiteIDs(hfCount.Value, profile.DBConnection._constr).ToList();
                          ReceiptList = ReceiptList.Where(i => (i.GRN_Date >= FrmDate.Date)).ToList();
                          ReceiptList = ReceiptList.Where(i => (i.GRN_Date <= To_Date.Date)).ToList();*/
                        //if (txtReceiptSearch.Text != "")
                        //{
                        //    FinalReceiptList = ReceiptList.Where(e => e.GRNNo.Contains(txtReceiptSearch.Text) || e.ReceiptByUserName.Contains(txtReceiptSearch.Text)).ToList();
                        //    ReceiptList = new List<POR_SP_GetReceiptSummaryBySiteIDsOrUserIDOrRequestID_Result>();
                        //    ReceiptList = FinalReceiptList;
                        //}
                        //GVReceiptInfo.DataSource = ReceiptList;
                        //GVReceiptInfo.DataBind();

                        /*    if (hdnReceiptSelectedRec.Value == "0")
                            {
                                GVReceiptInfo.SelectedRecords = new ArrayList();
                                foreach (POR_SP_GetReceiptSummaryBySiteIDsOrUserIDOrRequestID_Result rec in ReceiptList)
                                {
                                    Hashtable row = new Hashtable();
                                    //GRNH_ID GRN_Date ReceiptByUserName
                                    row["GRNH_ID"] = rec.GRNH_ID;
                                    row["GRN_Date"] = rec.GRN_Date;
                                    row["ReceiptByUserName"] = rec.ReceiptByUserName;
                                    GVReceiptInfo.SelectedRecords.Add(row);
                                    if (hdnReceiptSelectedRec.Value != "") { hdnReceiptSelectedRec.Value = hdnReceiptSelectedRec.Value + "," + rec.GRNH_ID.ToString(); }
                                    else if (hdnReceiptSelectedRec.Value == "") { hdnReceiptSelectedRec.Value = rec.GRNH_ID.ToString(); }
                                }
                            }
                            GVReceiptInfo.DataSource = ReceiptList;
                            GVReceiptInfo.DataBind();*/

                    }
                    catch { }
                    finally { PartReceipt.Close(); }

                }

                else if (Request.QueryString["invoker"] == "user")
                {
                    List<SP_GWC_GetUserInfoRoleWise_Result> UsrLst = new List<SP_GWC_GetUserInfoRoleWise_Result>();
                    //List<VW_GetUserInformation> UserList = new List<VW_GetUserInformation>();
                    //DataSet UserList = new DataSet();
                    CustomProfile profile1 = CustomProfile.GetProfile();

                    //frmdt_lcl = FrmDate.Date.Value.ToString("yyyy/MM/dd");
                    //hdnNewFDt.Value = frmdt_lcl;
                    //todt_lcl = To_Date.Date.Value.ToString("yyyy/MM/dd");
                    //hdnNewTDt.Value = todt_lcl;

                    var SearchedValue = hdnFilterText.Value;

                    try
                    {
                        //if (hdnUser.Value == "1")
                        //{
                        //    UserList = UCCommonFilter.GetUserInformation(Convert.ToInt64(hdnSelectedCompany.Value), Convert.ToInt64(hdnSelectedDepartment.Value), Convert.ToInt64(hdnSelectedRole.Value), ddlActive.SelectedValue, profile1.DBConnection._constr).ToList();
                        //}
                        if (hdnSelectedCompany.Value == "0") { hdnSelectedDepartment.Value = "0"; }
                        if (SearchedValue == "")
                        {
                            UsrLst = UCCommonFilter.GetUserInformation(hdnSelectedCompany.Value, hdnSelectedDepartment.Value, hdnSelectedRole.Value, ddlActive.SelectedValue, profile1.DBConnection._constr).ToList();
                        }
                        else
                        {
                            //UsrLst = UCCommonFilter.GetUserInformationSearched(hdnSelectedCompany.Value, hdnSelectedDepartment.Value, hdnSelectedRole.Value, ddlActive.SelectedValue, SearchedValue ,profile1.DBConnection._constr).ToList();
                            UsrLst = UCCommonFilter.GetUserInformation(hdnSelectedCompany.Value, hdnSelectedDepartment.Value, hdnSelectedRole.Value, ddlActive.SelectedValue, profile1.DBConnection._constr).ToList();
                            UsrLst = UsrLst.Where(r => r.Name.StartsWith(SearchedValue) || r.EmailID.StartsWith(SearchedValue) || r.EmployeeID.StartsWith(SearchedValue)).ToList();
                        }
                        GVUserInfo.DataSource = UsrLst;
                        GVUserInfo.DataBind();
                    }

                    catch { }
                    finally { }

                }

                else if (Request.QueryString["invoker"] == "order")
                {
                    DataSet ReqLst = new DataSet();
                    CustomProfile profile1 = CustomProfile.GetProfile();
                    string companyid = profile1.Personal.CompanyID.ToString();
                    string usertype = profile.Personal.UserType.ToString();
                    try
                    {
                        if (hdnSelectedCompany.Value == "0") { hdnSelectedDepartment.Value = "0"; }
                        if (usertype == "Super Admin")
                        {
                            // hdnSelectedCompany.Value = "0";
                            ReqLst = UCCommonFilter.GetAllOrderDetails(frmdt_lcl, todt_lcl, hdnSelectedCompany.Value, hdnSelectedDepartment.Value, hdnSelectedUser.Value, ddlStatus.SelectedValue, profile1.DBConnection._constr);
                        }
                        else
                        {
                            hdnSelectedCompany.Value = companyid;
                            ReqLst = UCCommonFilter.GetAllOrderDetails(frmdt_lcl, todt_lcl, hdnSelectedCompany.Value, hdnSelectedDepartment.Value, hdnSelectedUser.Value, ddlStatus.SelectedValue, profile1.DBConnection._constr);
                        }
                        GVRequestInfo.DataSource = ReqLst;
                        GVRequestInfo.DataBind();
                    }
                    catch { }
                    finally { }
                }
                else if (Request.QueryString["invoker"] == "location")
                {
                    DataSet ReqLst = new DataSet();
                    CustomProfile profile1 = CustomProfile.GetProfile();
                    List<SP_AllOrderLocationPaymentMode_Result> ReqstLst = new List<SP_AllOrderLocationPaymentMode_Result>();
                    string utype = profile1.Personal.UserType.ToString();
                    string comid = profile1.Personal.CompanyID.ToString();
                    try
                    {
                        if (hdnSelectedCompany.Value == "0") { hdnSelectedDepartment.Value = "0"; }
                        //ReqstLst = UCCommonFilter.GetAllOrderLocationPaymentMode(frmdt_lcl, todt_lcl, hdnSelectedCompany.Value, hdnSelectedDepartment.Value, hdnSelectedLocation.Value, hdnSelectedPaymentMode.Value, hdnIncludeECommerce.Value, profile1.DBConnection._constr).ToList();
                        if (utype == "Super Admin")
                        {
                            ReqLst = UCCommonFilter.GetAllOrderLocationPaymentMode(frmdt_lcl, todt_lcl, hdnSelectedCompany.Value, hdnSelectedDepartment.Value, hdnSelectedLocation.Value, hdnSelectedPaymentMode.Value, hdnIncludeECommerce.Value, profile1.DBConnection._constr);
                        }
                        else
                        {
                            hdnSelectedCompany.Value = comid;
                            ReqLst = UCCommonFilter.GetAllOrderLocationPaymentMode(frmdt_lcl, todt_lcl, hdnSelectedCompany.Value, hdnSelectedDepartment.Value, hdnSelectedLocation.Value, hdnSelectedPaymentMode.Value, hdnIncludeECommerce.Value, profile1.DBConnection._constr);
                        }

                        //if (hdnRequestSelectedRec.Value == "0")
                        //{
                        //    GVRequestInfo.SelectedRecords = new ArrayList();
                        //    foreach (SP_AllOrderLocationPaymentMode_Result rec in ReqstLst)
                        //    {
                        //        Hashtable row = new Hashtable();
                        //        row["PRH_ID"] =rec.ID;
                        //        row["RequestDate"] = rec.OrderDate;
                        //        row["RequestByUserName"] = rec.RequestByUserName;
                        //        GVRequestInfo.SelectedRecords.Add(row);
                        //        if (hdnRequestSelectedRec.Value != "") { hdnRequestSelectedRec.Value = hdnRequestSelectedRec.Value + "," + rec.ID.ToString(); }
                        //        else if (hdnRequestSelectedRec.Value == "") { hdnRequestSelectedRec.Value = rec.ID.ToString(); }
                        //    }
                        //}

                        GVRequestInfo.DataSource = ReqLst;
                        GVRequestInfo.DataBind();
                    }
                    catch { }
                    finally { }
                }
                else if (Request.QueryString["invoker"] == "orderdetail")
                {
                    DataSet ReqDtLst = new DataSet();
                    CustomProfile profile1 = CustomProfile.GetProfile();
                    string comid = profile1.Personal.CompanyID.ToString();
                    string utype = profile1.Personal.UserType.ToString();
                    try
                    {
                        if (hdnSelectedCompany.Value == "0") { hdnSelectedDepartment.Value = "0"; }
                        if (utype == "Super Admin")
                        {
                            ReqDtLst = UCCommonFilter.GetAllOrderDetails(frmdt_lcl, todt_lcl, hdnSelectedCompany.Value, hdnSelectedDepartment.Value, hdnSelectedUser.Value, ddlStatus.SelectedValue, profile.DBConnection._constr);
                        }
                        else
                        {
                            hdnSelectedCompany.Value = comid;
                            ReqDtLst = UCCommonFilter.GetAllOrderDetails(frmdt_lcl, todt_lcl, hdnSelectedCompany.Value, hdnSelectedDepartment.Value, hdnSelectedUser.Value, ddlStatus.SelectedValue, profile.DBConnection._constr);
                        }
                        GVRequestInfo.DataSource = ReqDtLst;
                        GVRequestInfo.DataBind();
                    }
                    catch { }
                    finally { }
                }
                else if (Request.QueryString["invoker"] == "orderlead")
                {
                    DataSet ReqDtLst = new DataSet();
                    CustomProfile profile1 = CustomProfile.GetProfile();
                    string comid = profile1.Personal.CompanyID.ToString();
                    string utype = profile1.Personal.UserType.ToString();
                    try
                    {
                        if (hdnSelectedCompany.Value == "0") { hdnSelectedDepartment.Value = "0"; }
                        if (utype == "Super Admin")
                        {
                            ReqDtLst = UCCommonFilter.GetAllOrderDetails(frmdt_lcl, todt_lcl, hdnSelectedCompany.Value, hdnSelectedDepartment.Value, hdnSelectedUser.Value, ddlStatus.SelectedValue, profile.DBConnection._constr);
                        }
                        else
                        {
                            hdnSelectedCompany.Value = comid;
                            ReqDtLst = UCCommonFilter.GetAllOrderDetails(frmdt_lcl, todt_lcl, hdnSelectedCompany.Value, hdnSelectedDepartment.Value, hdnSelectedUser.Value, ddlStatus.SelectedValue, profile.DBConnection._constr);
                        }
                        GVRequestInfo.DataSource = ReqDtLst;
                        GVRequestInfo.DataBind();
                    }
                    catch { }
                    finally { }
                }
                /*******/
                else if (Request.QueryString["invoker"] == "orderdelivery")
                {
                    DataSet ReqDtLst = new DataSet();
                    CustomProfile profile1 = CustomProfile.GetProfile();
                    string comid = profile1.Personal.CompanyID.ToString();
                    string utype = profile1.Personal.UserType.ToString();
                    try
                    {
                        if (hdnSelectedCompany.Value == "0") { hdnSelectedDepartment.Value = "0"; }
                        if (utype == "Super Admin")
                        {
                            ReqDtLst = UCCommonFilter.GetAllOrderDelivery(frmdt_lcl, todt_lcl, hdnSelectedCompany.Value, hdnSelectedDepartment.Value, hdnSelectedDriver.Value, hdnSelectedPaymentMode.Value, profile.DBConnection._constr);
                        }
                        else
                        {
                            hdnSelectedCompany.Value = comid;
                            ReqDtLst = UCCommonFilter.GetAllOrderDelivery(frmdt_lcl, todt_lcl, hdnSelectedCompany.Value, hdnSelectedDepartment.Value, hdnSelectedDriver.Value, hdnSelectedPaymentMode.Value, profile.DBConnection._constr);
                        }
                        GVRequestInfo.DataSource = ReqDtLst;
                        GVRequestInfo.DataBind();
                    }
                    catch { }
                    finally { }
                }
                else if (Request.QueryString["invoker"] == "sla")
                {
                    DataSet ReqDtLst = new DataSet();
                    CustomProfile profile1 = CustomProfile.GetProfile();
                    try
                    {
                        if (hdnSelectedCompany.Value == "0") { hdnSelectedDepartment.Value = "0"; }
                        ReqDtLst = UCCommonFilter.GetAllSlaData(frmdt_lcl, todt_lcl, hdnSelectedCompany.Value, hdnSelectedDepartment.Value, ddlStatus.SelectedValue, hdnSelectedDriver.Value, hdnSelectedDeliveryType.Value, profile.DBConnection._constr);
                        GVRequestInfo.DataSource = ReqDtLst;
                        GVRequestInfo.DataBind();

                    }
                    catch { }
                    finally { }
                }
                else if (Request.QueryString["invoker"] == "deliverylogrpt")
                {
                    DataSet ReqDtLst = new DataSet();
                    CustomProfile profile1 = CustomProfile.GetProfile();
                    string utype = profile1.Personal.UserType.ToString();
                    string comid = profile1.Personal.CompanyID.ToString();
                    try
                    {
                        if (hdnSelectedCompany.Value == "0") hdnSelectedDepartment.Value = "0";
                        if (utype == "Super Admin")
                        {
                            ReqDtLst = UCCommonFilter.GetDriverLogRpt(frmdt_lcl, todt_lcl, hdnSelectedCompany.Value, hdnSelectedDepartment.Value, ddlStatus.SelectedValue, hdnSelectedUser.Value, hdnSelectedPaymentMode.Value, profile.DBConnection._constr);
                        }
                        else
                        {
                            hdnSelectedCompany.Value = comid;
                            ReqDtLst = UCCommonFilter.GetDriverLogRpt(frmdt_lcl, todt_lcl, hdnSelectedCompany.Value, hdnSelectedDepartment.Value, ddlStatus.SelectedValue, hdnSelectedUser.Value, hdnSelectedPaymentMode.Value, profile.DBConnection._constr);
                        }
                        GVRequestInfo.DataSource = ReqDtLst;
                        GVRequestInfo.DataBind();

                    }
                    catch { }
                    finally { }
                }

                //else if (Request.QueryString["invoker"] == "bulkrpt")
                //{
                //    DataSet ReqDtLst = new DataSet();
                //    CustomProfile profile1 = CustomProfile.GetProfile();
                //    string utype = profile1.Personal.UserType.ToString();
                //    string comid = profile1.Personal.CompanyID.ToString();
                //    try
                //    {
                //        if (hdnSelectedCompany.Value == "0") hdnSelectedDepartment.Value = "0";
                //        if (utype == "Super Admin")
                //        {
                //            ReqDtLst = UCCommonFilter.GetDriverLogRpt(frmdt_lcl, todt_lcl, hdnSelectedCompany.Value, hdnSelectedDepartment.Value, ddlStatus.SelectedValue, hdnSelectedUser.Value, hdnSelectedPaymentMode.Value, profile.DBConnection._constr);
                //        }
                //        else
                //        {
                //            hdnSelectedCompany.Value = comid;
                //            ReqDtLst = UCCommonFilter.GetDriverLogRpt(frmdt_lcl, todt_lcl, hdnSelectedCompany.Value, hdnSelectedDepartment.Value, ddlStatus.SelectedValue, hdnSelectedUser.Value, hdnSelectedPaymentMode.Value, profile.DBConnection._constr);
                //        }
                //        GVRequestInfo.DataSource = ReqDtLst;
                //        GVRequestInfo.DataBind();

                //    }
                //    catch { }
                //    finally { }
                //}

                else if (Request.QueryString["invoker"] == "ecommerce1")
                {
                    DataSet ReqLst = new DataSet();
                    CustomProfile profile1 = CustomProfile.GetProfile();
                    try
                    {
                        if (hdnSelectedCompany.Value == "0") hdnSelectedDepartment.Value = "0";
                        if (ddlType.SelectedItem.Text == "Daily")
                        {
                            ReqLst = UCCommonFilter.GetAllEcommerce1Data(frmdt_lcl, todt_lcl, hdnSelectedCompany.Value, hdnSelectedDepartment.Value, ddlStatus.SelectedValue, hdnSelectedUser.Value, hdnSelectedPaymentMode.Value, profile.DBConnection._constr);
                            GVRequestInfo.DataSource = ReqLst;
                            GVRequestInfo.DataBind();
                        }
                        else if (ddlType.SelectedItem.Text == "Up to Month" || ddlType.SelectedItem.Text == "Monthly")
                        {
                            FrmDate.Date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                            To_Date.Date = DateTime.Now.Date;
                            frmdt_lcl = FrmDate.Date.Value.ToString("yyyy/MM/dd"); hdnNewFDt.Value = frmdt_lcl;
                            todt_lcl = To_Date.Date.Value.ToString("yyyy/MM/dd"); hdnNewTDt.Value = todt_lcl;
                            ReqLst = UCCommonFilter.GetAllEcommerce1Data(frmdt_lcl, todt_lcl, hdnSelectedCompany.Value, hdnSelectedDepartment.Value, ddlStatus.SelectedValue, hdnSelectedUser.Value, hdnSelectedPaymentMode.Value, profile.DBConnection._constr);
                            GVRequestInfo.DataSource = ReqLst;
                            GVRequestInfo.DataBind();
                        }
                        else
                        {
                            ReqLst = UCCommonFilter.GetAllEcommerce1Data(frmdt_lcl, todt_lcl, hdnSelectedCompany.Value, hdnSelectedDepartment.Value, ddlStatus.SelectedValue, hdnSelectedUser.Value, hdnSelectedPaymentMode.Value, profile.DBConnection._constr);
                            GVRequestInfo.DataSource = ReqLst;
                            GVRequestInfo.DataBind();
                        }

                    }
                    catch { }
                    finally { }
                }
                else if (Request.QueryString["invoker"] == "ecommerce2")
                {
                    DataSet ReqDtLst = new DataSet();
                    CustomProfile profile1 = CustomProfile.GetProfile();
                    try
                    {
                        if (hdnSelectedCompany.Value == "0") hdnSelectedDepartment.Value = "0";

                        if (ddlType.SelectedItem.Text == "Daily")
                        {
                            ReqDtLst = UCCommonFilter.GetAllEcommerce1Data(frmdt_lcl, todt_lcl, hdnSelectedCompany.Value, hdnSelectedDepartment.Value, ddlStatus.SelectedValue, hdnSelectedUser.Value, hdnSelectedPaymentMode.Value, profile.DBConnection._constr);
                            GVRequestInfo.DataSource = ReqDtLst;
                            GVRequestInfo.DataBind();
                        }
                        else if (ddlType.SelectedItem.Text == "Up to Month" || ddlType.SelectedItem.Text == "Monthly")
                        {
                            FrmDate.Date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                            To_Date.Date = DateTime.Now.Date;
                            frmdt_lcl = FrmDate.Date.Value.ToString("yyyy/MM/dd"); hdnNewFDt.Value = frmdt_lcl;
                            todt_lcl = To_Date.Date.Value.ToString("yyyy/MM/dd"); hdnNewTDt.Value = todt_lcl;
                            ReqDtLst = UCCommonFilter.GetAllEcommerce1Data(frmdt_lcl, todt_lcl, hdnSelectedCompany.Value, hdnSelectedDepartment.Value, ddlStatus.SelectedValue, hdnSelectedUser.Value, hdnSelectedPaymentMode.Value, profile.DBConnection._constr);
                            GVRequestInfo.DataSource = ReqDtLst;
                            GVRequestInfo.DataBind();
                        }
                        else
                        {
                            ReqDtLst = UCCommonFilter.GetAllEcommerce1Data(frmdt_lcl, todt_lcl, hdnSelectedCompany.Value, hdnSelectedDepartment.Value, ddlStatus.SelectedValue, hdnSelectedUser.Value, hdnSelectedPaymentMode.Value, profile.DBConnection._constr);
                            GVRequestInfo.DataSource = ReqDtLst;
                            GVRequestInfo.DataBind();
                        }


                    }
                    catch { }
                    finally { }
                }
                else if (Request.QueryString["invoker"] == "deliverynote")
                {
                    DataSet ReqDtLst = new DataSet();
                    CustomProfile profile1 = CustomProfile.GetProfile();
                    try
                    {
                        if (hdnSelectedCompany.Value == "0") { hdnSelectedDepartment.Value = "0"; }
                        ReqDtLst = UCCommonFilter.GetDeliveryNoteDataGrid(frmdt_lcl, todt_lcl, hdnSelectedCompany.Value, hdnSelectedDepartment.Value, txtordno.Text, profile.DBConnection._constr);
                        GVRequestInfo.DataSource = ReqDtLst;
                        GVRequestInfo.DataBind();
                    }
                    catch { }
                    finally { }
                }
                else if (Request.QueryString["invoker"] == "normalorderdeliverynote")//change by suraj khopade
                {
                    DataSet ReqDtLst = new DataSet();
                    CustomProfile profile1 = CustomProfile.GetProfile();
                    try
                    {
                        if (hdnSelectedCompany.Value == "0") { hdnSelectedDepartment.Value = "0"; }
                        ReqDtLst = UCCommonFilter.GetNormalOrderDeliveryNoteDataGrid(frmdt_lcl, todt_lcl, hdnSelectedCompany.Value, hdnSelectedDepartment.Value, txtordno.Text, profile.DBConnection._constr);
                        GVRequestInfo.DataSource = ReqDtLst;
                        GVRequestInfo.DataBind();
                    }
                    catch { }
                    finally { }
                }
                else if (Request.QueryString["invoker"] == "avgtime")
                {
                    DataSet ReqDtLst = new DataSet();
                    CustomProfile profile1 = CustomProfile.GetProfile();
                    string ComapnyId = profile.Personal.CompanyID.ToString();
                    string Usertype = profile.Personal.UserType.ToString();
                    try
                    {
                        if (Usertype == "Super Admin")
                        {
                          //  ReqDtLst = UCCommonFilter.GetDeliveryLstReq(frmdt_lcl, todt_lcl, hdnSelectedStatus.Value, hdnSelectedDriver.Value, hdnSelectedLocation.Value, hdnSelectedToLocation.Value, hdnSelectedRoute.Value, "0", profile.DBConnection._constr);
                        }
                        else
                        {
                           // ReqDtLst = UCCommonFilter.GetDeliveryLstReq(frmdt_lcl, todt_lcl, hdnSelectedStatus.Value, hdnSelectedDriver.Value, hdnSelectedLocation.Value, hdnSelectedToLocation.Value, hdnSelectedRoute.Value, ComapnyId, profile.DBConnection._constr);
                        }
                        GVRequestInfo.DataSource = ReqDtLst;
                        GVRequestInfo.DataBind();
                    }
                    catch { }
                    finally { }
                }
                else if (Request.QueryString["invoker"] == "noofdelivery")
                {
                    DataSet ReqDtLst = new DataSet();
                    CustomProfile profile1 = CustomProfile.GetProfile();
                    string ComapnyId = profile.Personal.CompanyID.ToString();
                    string Usertype = profile.Personal.UserType.ToString();

                    try
                    {
                        hdnSelectedCompany.Value = "";
                        hdnSelectedDepartment.Value = "";
                        //if (hdnSelectedCompany.Value == "0") { hdnSelectedDepartment.Value = "0"; }
                        //if (hdnSelectedCompany.Value == "0") hdnSelectedCompany.Value = "";
                        // if (hdnSelectedDepartment.Value == "0") hdnSelectedDepartment.Value = "";
                        if (hdnSelectedDriver.Value == "0") hdnSelectedDriver.Value = "";

                        if (Usertype == "Super Admin")
                        {
                          //  ReqDtLst = UCCommonFilter.GetAllOrdrDetDriver(frmdt_lcl, todt_lcl, hdnSelectedCompany.Value, hdnSelectedDepartment.Value, hdnSelectedDriver.Value, ddlStatus.SelectedValue, "0", profile.DBConnection._constr);
                        }
                        else
                        {
                           // ReqDtLst = UCCommonFilter.GetAllOrdrDetDriver(frmdt_lcl, todt_lcl, hdnSelectedCompany.Value, hdnSelectedDepartment.Value, hdnSelectedDriver.Value, ddlStatus.SelectedValue, ComapnyId, profile.DBConnection._constr);
                        }
                        GVRequestInfo.DataSource = ReqDtLst;
                        GVRequestInfo.DataBind();
                    }
                    catch { }
                    finally { }
                }

                //if (Request.QueryString["invoker"] == "ecommerce2")
                //{                   
                //    DataSet dsexport = new DataSet();
                //    dsexport = UCCommonFilter.ExportDatainExcel(frmdt_lcl, todt_lcl, hdnSelectedCompany.Value, hdnSelectedDepartment.Value, ddlStatus.SelectedValue, hdnSelectedUser.Value, hdnSelectedPaymentMode.Value, profile.DBConnection._constr);
                //    gridexport.DataSource = dsexport;
                //    gridexport.DataBind();
                //}

                else if (Request.QueryString["invoker"] == "qnbnorder")
                {
                    DataSet ReqLst = new DataSet();
                    CustomProfile profile1 = CustomProfile.GetProfile();
                    string comid = profile1.Personal.CompanyID.ToString();
                    string utype = profile1.Personal.UserType.ToString();
                    try
                    {
                        if (hdnSelectedCompany.Value == "0") { hdnSelectedDepartment.Value = "0"; }
                        if (utype == "Super Admin")
                        {
                            ReqLst = UCCommonFilter.GetAllOrderDetails(frmdt_lcl, todt_lcl, hdnSelectedCompany.Value, hdnSelectedDepartment.Value, hdnSelectedUser.Value, ddlStatus.SelectedValue, profile1.DBConnection._constr);
                        }
                        else
                        {
                            hdnSelectedCompany.Value = comid;
                            ReqLst = UCCommonFilter.GetAllOrderDetails(frmdt_lcl, todt_lcl, hdnSelectedCompany.Value, hdnSelectedDepartment.Value, hdnSelectedUser.Value, ddlStatus.SelectedValue, profile1.DBConnection._constr);
                        }
                        GVRequestInfo.DataSource = ReqLst;
                        GVRequestInfo.DataBind();
                    }
                    catch { }
                    finally { }
                }

                /*******/



                else if (Request.QueryString["invoker"] == "skutrack")
               {
                    DataSet ReqDtLst = new DataSet();
                    CustomProfile profile1 = CustomProfile.GetProfile();
                    try
                    {
                        if (hdnSelectedSku.Value == "") { hdnSelectedSku.Value = "0"; }
                        ReqDtLst = UCCommonFilter.GetSkutrackDataGrid(frmdt_lcl, todt_lcl, hdnSelectedSku.Value, profile.DBConnection._constr);
                        GVRequestInfo.DataSource = ReqDtLst;
                        GVRequestInfo.DataBind();
                    }
                    catch { }
                    finally { }
                }
                else if (Request.QueryString["invoker"] == "sitetrack")
                {
                    DataSet ReqDtLst = new DataSet();
                    CustomProfile profile1 = CustomProfile.GetProfile();
                    try
                    {
                        if (hdnSelectedSitecode.Value == "") { hdnSelectedSitecode.Value = "0"; }
                        ReqDtLst = UCCommonFilter.GetSitetrackDataGrid(frmdt_lcl, todt_lcl, hdnSelectedSitecode.Value, profile.DBConnection._constr);
                        GVRequestInfo.DataSource = ReqDtLst;
                        GVRequestInfo.DataBind();
                    }
                    catch { }
                    finally { }
                }
                else if (Request.QueryString["invoker"] == "depttrack")
                {
                    DataSet ReqDtLst = new DataSet();
                    CustomProfile profile1 = CustomProfile.GetProfile();
                    try
                    {
                        if (hdnSelectedDepartment.Value == "") { hdnSelectedDepartment.Value = "0"; }
                        ReqDtLst = UCCommonFilter.GetDepttrackDataGrid(frmdt_lcl, todt_lcl, hdnSelectedDepartment.Value,profile.Personal.UserID, profile.DBConnection._constr);
                        GVRequestInfo.DataSource = ReqDtLst;
                        GVRequestInfo.DataBind();
                    }
                    catch { }
                    finally { }
                }
                else if (Request.QueryString["invoker"] == "vat")
                {
                    DataSet ReqDtLst = new DataSet();
                    CustomProfile profile1 = CustomProfile.GetProfile();
                    string comid = profile1.Personal.CompanyID.ToString();
                    string utype = profile1.Personal.UserType.ToString();
                    try
                    {
                        if (hdnSelectedCompany.Value == "0") { hdnSelectedDepartment.Value = "0"; }
                        if (utype == "Super Admin")
                        {
                            ReqDtLst = UCCommonFilter.GetAllOrderDetails(frmdt_lcl, todt_lcl, hdnSelectedCompany.Value, hdnSelectedDepartment.Value, hdnSelectedUser.Value, ddlStatus.SelectedValue, profile.DBConnection._constr);
                        }
                        else
                        {
                            hdnSelectedCompany.Value = comid;
                            ReqDtLst = UCCommonFilter.GetAllOrderDetails(frmdt_lcl, todt_lcl, hdnSelectedCompany.Value, hdnSelectedDepartment.Value, hdnSelectedUser.Value, ddlStatus.SelectedValue, profile.DBConnection._constr);
                        }
                        GVRequestInfo.DataSource = ReqDtLst;
                        GVRequestInfo.DataBind();
                    }
                    catch { }
                    finally { }
                }

            }
        }

        public void fillProduct()
        {
            iUCCommonFilterClient UCCommonFilter = new iUCCommonFilterClient();
            CustomProfile profile = CustomProfile.GetProfile();
            GVProductInfo.DataSource = null;
            GVProductInfo.DataBind();
            try
            {
                //UCProductSearchService.iUCProductSearchClient productSearchService = new UCProductSearchService.iUCProductSearchClient();
                //List<GetProductDetail> ProductList = new List<GetProductDetail>();
                //List<GetProductDetail> FinalProductList = new List<GetProductDetail>();
                DataSet prdlist = new DataSet();

                GVProductInfo.DataSource = null;
                GVProductInfo.DataBind();

                if (Request.QueryString["invoker"] == "partconsumption")
                {
                    if (hdnEngineSelectedRec.Value == "")
                    {
                        GVProductInfo.DataSource = null;
                        GVProductInfo.DataBind();
                    }
                    else
                    {
                        var frmdt = FrmDate.Date.ToString();
                        var todt = To_Date.Date.ToString();


                        prdlist = UCCommonFilter.GetProductOfSelectedEngine(hdnEngineSelectedRec.Value, hdnFilterText.Value, frmdt, todt, profile.DBConnection._constr);
                        if (hdnProductSelectedRec.Value == "0")
                        {
                            GVProductInfo.SelectedRecords = new ArrayList();
                            foreach (DataRow rec in prdlist.Tables[0].Rows)
                            {
                                Hashtable row = new Hashtable();
                                //row["ID"] = rec.ID;
                                row["ID"] = rec["ID"];
                                //row["ProductCode"] = rec.ProductCode;
                                row["ProductCode"] = rec["ProductCode"];
                                //row["Name"] = rec.Name;
                                row["Name"] = rec["Name"];
                                GVProductInfo.SelectedRecords.Add(row);
                                if (hdnProductSelectedRec.Value != "") { hdnProductSelectedRec.Value = hdnProductSelectedRec.Value + "," + ID.ToString(); }
                                else if (hdnProductSelectedRec.Value == "") { hdnProductSelectedRec.Value = ID.ToString(); }
                            }
                        }
                        GVProductInfo.DataSource = prdlist;
                        GVProductInfo.DataBind();
                    }
                }
                else if (Request.QueryString["invoker"] == "partrequest")
                {
                    if (hdnRequestSelectedRec.Value == "")
                    {
                        GVProductInfo.DataSource = null;
                        GVProductInfo.DataBind();
                    }
                    else
                    {
                        prdlist = UCCommonFilter.GetProductofRequest(hdnRequestSelectedRec.Value, hdnFilterText.Value, profile.DBConnection._constr);

                        if (hdnProductSelectedRec.Value == "0")
                        {
                            GVProductInfo.SelectedRecords = new ArrayList();
                            foreach (DataRow rec in prdlist.Tables[0].Rows)
                            {
                                Hashtable row = new Hashtable();
                                //row["ID"] = rec.ID;
                                row["ID"] = rec["ID"];
                                //row["ProductCode"] = rec.ProductCode;
                                row["ProductCode"] = rec["ProductCode"];
                                //row["Name"] = rec.Name;
                                row["Name"] = rec["Name"];
                                GVProductInfo.SelectedRecords.Add(row);
                                if (hdnProductSelectedRec.Value != "") { hdnProductSelectedRec.Value = hdnProductSelectedRec.Value + "," + ID.ToString(); }
                                else if (hdnProductSelectedRec.Value == "") { hdnProductSelectedRec.Value = ID.ToString(); }
                            }
                        }

                        GVProductInfo.DataSource = prdlist;
                        GVProductInfo.DataBind();
                    }
                }
                else if (Request.QueryString["invoker"] == "partissue")
                {
                    if (hdnIssueSelectedRec.Value == "")
                    {
                        GVProductInfo.DataSource = null;
                        GVProductInfo.DataBind();
                    }

                    else
                    {
                        prdlist = UCCommonFilter.GetProductofIssue(hdnIssueSelectedRec.Value, hdnFilterText.Value, profile.DBConnection._constr);
                        if (hdnProductSelectedRec.Value == "0")
                        {
                            GVProductInfo.SelectedRecords = new ArrayList();
                            foreach (DataRow rec in prdlist.Tables[0].Rows)
                            {
                                Hashtable row = new Hashtable();
                                //row["ID"] = rec.ID;
                                row["ID"] = rec["ID"];
                                //row["ProductCode"] = rec.ProductCode;
                                row["ProductCode"] = rec["ProductCode"];
                                //row["Name"] = rec.Name;
                                row["Name"] = rec["Name"];
                                GVProductInfo.SelectedRecords.Add(row);
                                if (hdnProductSelectedRec.Value != "") { hdnProductSelectedRec.Value = hdnProductSelectedRec.Value + "," + ID.ToString(); }
                                else if (hdnProductSelectedRec.Value == "") { hdnProductSelectedRec.Value = ID.ToString(); }
                            }
                        }
                        GVProductInfo.DataSource = prdlist;
                        GVProductInfo.DataBind();
                    }
                }
                else if (Request.QueryString["invoker"] == "partreceipt")
                {
                    if (hdnReceiptSelectedRec.Value == "")
                    {
                        GVProductInfo.DataSource = null;
                        GVProductInfo.DataBind();
                    }
                    else
                    {
                        prdlist = UCCommonFilter.GetProductofReceipt(hdnReceiptSelectedRec.Value, hdnFilterText.Value, profile.DBConnection._constr);
                        if (hdnProductSelectedRec.Value == "0")
                        {
                            GVProductInfo.SelectedRecords = new ArrayList();
                            foreach (DataRow rec in prdlist.Tables[0].Rows)
                            {
                                Hashtable row = new Hashtable();
                                //row["ID"] = rec.ID;
                                row["ID"] = rec["ID"];
                                //row["ProductCode"] = rec.ProductCode;
                                row["ProductCode"] = rec["ProductCode"];
                                //row["Name"] = rec.Name;
                                row["Name"] = rec["Name"];
                                GVProductInfo.SelectedRecords.Add(row);
                                if (hdnProductSelectedRec.Value != "") { hdnProductSelectedRec.Value = hdnProductSelectedRec.Value + "," + ID.ToString(); }
                                else if (hdnProductSelectedRec.Value == "") { hdnProductSelectedRec.Value = ID.ToString(); }
                            }
                        }
                        GVProductInfo.DataSource = prdlist;
                        GVProductInfo.DataBind();
                    }
                }

                else if (Request.QueryString["invoker"] == "productdtl")
                {
                    List<GetPrdDetail> PrdList = new List<GetPrdDetail>();


                    PrdList = UCCommonFilter.AllProductOnSite(profile.DBConnection._constr).ToList();

                    if (hdnProductSelectedRec.Value == "0")
                    {

                        GVProductInfo.SelectedRecords = new ArrayList();
                        foreach (GetPrdDetail rec in PrdList)
                        {
                            Hashtable row = new Hashtable();
                            row["ID"] = rec.ID;
                            row["ProductCode"] = rec.ProductCode;
                            row["Name"] = rec.Name;
                            GVProductInfo.SelectedRecords.Add(row);
                            if (hdnProductSelectedRec.Value != "") { hdnProductSelectedRec.Value = hdnProductSelectedRec.Value + "," + ID.ToString(); }
                            else if (hdnProductSelectedRec.Value == "") { hdnProductSelectedRec.Value = ID.ToString(); }
                        }
                    }
                    if (hdnFilterText.Value != "")
                    {
                        try
                        {
                            iUCProductSearchClient productSearchService = new iUCProductSearchClient();
                            CustomProfile profile1 = CustomProfile.GetProfile();
                            List<GetProductDetail> SProductList = new List<GetProductDetail>();
                            SProductList = productSearchService.GetProductList1(GVProductInfo.CurrentPageIndex, hdnFilterText.Value, profile1.DBConnection._constr).ToList();

                            GVProductInfo.DataSource = SProductList;
                            GVProductInfo.DataBind();
                            productSearchService.Close();
                        }
                        catch (System.Exception ex)
                        {

                        }
                    }
                    else
                    {

                        GVProductInfo.DataSource = PrdList;
                        GVProductInfo.DataBind();
                    }

                }

                else if (Request.QueryString["invoker"] == "sku")
                {
                    iUCCommonFilterClient CommonFltr = new iUCCommonFilterClient();
                    CustomProfile profile1 = CustomProfile.GetProfile();
                    string compid = profile1.Personal.CompanyID.ToString();
                    string utype = profile1.Personal.UserType.ToString();
                    try
                    {
                        //select MP.ProductCode,MP.Name,MP.Description,img.Path,MP.CompanyID ,MP.StoreId,MP.GroupSet  from mproduct MP left outer join tImage img on  MP.ID=img.ReferenceID   where MP.CompanyID like '%%' and MP.StoreId like '%%' and MP.GroupSet like '%%'  
                        //if((hdnSelectedCompany.Value != "0") && (hdnSelectedCompany.Value != "") && (hdnSelectedDepartment.Value != "0") && (hdnSelectedDepartment.Value != "") && (hdnSelectedGroupSet.Value != "0") && (hdnSelectedGroupSet.Value != ""))                        
                        DataSet dsSkuFltr = new DataSet();
                        var SearchedValue = hdnFilterText.Value;
                        if (hdnSelectedCompany.Value == "0") { hdnSelectedDepartment.Value = "0"; }
                        if (hdnSKU.Value == "sku")
                        {
                            if (SearchedValue == "")
                            {
                                if (utype == "Super Admin")
                                {
                                    dsSkuFltr = CommonFltr.SkulistReport(hdnSelectedCompany.Value, hdnSelectedDepartment.Value, hdnSelectedGroupSet.Value, hdnSelectedImage.Value, profile.DBConnection._constr);
                                }
                                else
                                {
                                    hdnSelectedCompany.Value = compid;
                                    dsSkuFltr = CommonFltr.SkulistReport(hdnSelectedCompany.Value, hdnSelectedDepartment.Value, hdnSelectedGroupSet.Value, hdnSelectedImage.Value, profile.DBConnection._constr);
                                }
                            }
                            else
                            {
                                if (utype == "Super Admin")
                                {
                                    dsSkuFltr = CommonFltr.SkulistReportSearch(hdnSelectedCompany.Value, hdnSelectedDepartment.Value, hdnSelectedGroupSet.Value, hdnSelectedImage.Value, SearchedValue, profile.DBConnection._constr);
                                }
                                else
                                {
                                    hdnSelectedCompany.Value = compid;
                                    dsSkuFltr = CommonFltr.SkulistReportSearch(hdnSelectedCompany.Value, hdnSelectedDepartment.Value, hdnSelectedGroupSet.Value, hdnSelectedImage.Value, SearchedValue, profile.DBConnection._constr);
                                }
                            }
                            GVProductInfo.DataSource = dsSkuFltr;
                            GVProductInfo.DataBind();
                        }
                    }
                    catch { }
                    finally { CommonFltr.Close(); }
                }
                else if (Request.QueryString["invoker"] == "SkuDetails")
                {
                    iUCCommonFilterClient CommonFltr = new iUCCommonFilterClient();
                    CustomProfile profile1 = CustomProfile.GetProfile();
                    try
                    {
                        DataSet dsSkuFltr = new DataSet();
                        var SearchedValue = hdnFilterText.Value;
                        if (hdnSKU.Value == "sku")
                        {
                            if (hdnSelectedCompany.Value == "0") { hdnSelectedDepartment.Value = "0"; }
                            if (SearchedValue == "")
                            {
                                dsSkuFltr = CommonFltr.SkulistReport(hdnSelectedCompany.Value, hdnSelectedDepartment.Value, hdnSelectedGroupSet.Value, hdnSelectedImage.Value, profile.DBConnection._constr);
                            }
                            else
                            {
                                dsSkuFltr = CommonFltr.SkulistReportSearch(hdnSelectedCompany.Value, hdnSelectedDepartment.Value, hdnSelectedGroupSet.Value, hdnSelectedImage.Value, SearchedValue, profile.DBConnection._constr);
                            }
                            GVProductInfo.DataSource = dsSkuFltr;
                            GVProductInfo.DataBind();
                        }
                    }
                    catch { }
                    finally { CommonFltr.Close(); }
                }
                else if (Request.QueryString["invoker"] == "BomDetail")
                {
                    iUCCommonFilterClient CommonFltr = new iUCCommonFilterClient();
                    CustomProfile profile1 = CustomProfile.GetProfile();
                    try
                    {
                        DataSet dsSkuFltr = new DataSet();
                        var SearchedValue = hdnFilterText.Value;
                        //showAlert("Select BOM Value Yes For BOM Detail Report...", "Error", "#")
                        if (hdnSKU.Value == "sku")
                        {
                            if (hdnSelectedGroupSet.Value == "No" || hdnSelectedGroupSet.Value == "0")
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "showAlert('Select BOM Value Yes For BOM Detail Report...','Error','#')", true);
                            }
                            else
                            {
                                if (SearchedValue == "")
                                {
                                    dsSkuFltr = CommonFltr.SkulistReport(hdnSelectedCompany.Value, hdnSelectedDepartment.Value, hdnSelectedGroupSet.Value, hdnSelectedImage.Value, profile.DBConnection._constr);
                                }
                                else
                                {
                                    dsSkuFltr = CommonFltr.SkulistReportSearch(hdnSelectedCompany.Value, hdnSelectedDepartment.Value, hdnSelectedGroupSet.Value, hdnSelectedImage.Value, SearchedValue, profile.DBConnection._constr);
                                }
                                GVProductInfo.DataSource = dsSkuFltr;
                                GVProductInfo.DataBind();
                            }
                        }
                    }
                    catch { }
                    finally { CommonFltr.Close(); }
                }

                else if (Request.QueryString["invoker"] == "order")
                {
                    if (hdnRequestSelectedRec.Value == "")
                    {
                        GVProductInfo.DataSource = null;
                        GVProductInfo.DataBind();
                    }
                    else
                    {
                        prdlist = UCCommonFilter.SKUDetailsBySelectedRequestID(hdnRequestSelectedRec.Value, profile.DBConnection._constr);

                        if (hdnProductSelectedRec.Value == "0")
                        {
                            GVProductInfo.SelectedRecords = new ArrayList();
                            foreach (DataRow rec in prdlist.Tables[0].Rows)
                            {
                                Hashtable row = new Hashtable();
                                //row["ID"] = rec.ID;
                                row["ID"] = rec["ID"];
                                //row["ProductCode"] = rec.ProductCode;
                                row["ProductCode"] = rec["ProductCode"];
                                //row["Name"] = rec.Name;
                                row["Name"] = rec["Name"];
                                GVProductInfo.SelectedRecords.Add(row);
                                if (hdnProductSelectedRec.Value != "") { hdnProductSelectedRec.Value = hdnProductSelectedRec.Value + "," + ID.ToString(); }
                                else if (hdnProductSelectedRec.Value == "") { hdnProductSelectedRec.Value = ID.ToString(); }
                            }
                        }

                        GVProductInfo.DataSource = prdlist;
                        GVProductInfo.DataBind();
                    }
                }
                else if (Request.QueryString["invoker"] == "imgaudit")
                {
                    iUCCommonFilterClient CommonFltr = new iUCCommonFilterClient();
                    CustomProfile profile1 = CustomProfile.GetProfile();
                    try
                    {
                        var frmdt = FrmDate.Date.ToString();
                        var todt = To_Date.Date.ToString();

                        DataSet dsSkuFltr = new DataSet();
                        var SearchedValue = hdnFilterText.Value;
                        if (hdnSKU.Value == "sku")
                        {
                            if (hdnImgStatus.Value == "Success")
                            {
                                if (SearchedValue == "")
                                {
                                    dsSkuFltr = CommonFltr.GetImageAuditAllPrd(frmdt, todt, hdnSelectedCompany.Value, hdnSelectedDepartment.Value, hdnSelectedUser.Value, profile.DBConnection._constr);
                                }
                                else
                                {
                                    dsSkuFltr = CommonFltr.GetImageAuditAllPrdSearched(frmdt, todt, hdnSelectedCompany.Value, hdnSelectedDepartment.Value, hdnSelectedUser.Value, SearchedValue, profile.DBConnection._constr);
                                }
                                //dsSkuFltr = CommonFltr.SkulistReport(hdnSelectedCompany.Value, hdnSelectedDepartment.Value, hdnSelectedGroupSet.Value, hdnSelectedImage.Value, profile.DBConnection._constr);
                            }
                            else
                            {
                                if (SearchedValue == "")
                                {
                                    dsSkuFltr = CommonFltr.GetImageAuditFailPrdLst(frmdt, todt, hdnSelectedCompany.Value, hdnSelectedDepartment.Value, hdnSelectedUser.Value, profile.DBConnection._constr);
                                }
                                else
                                {
                                    dsSkuFltr = CommonFltr.GetImageAuditFailPrdLstSearched(frmdt, todt, hdnSelectedCompany.Value, hdnSelectedDepartment.Value, hdnSelectedUser.Value, SearchedValue, profile.DBConnection._constr);
                                }
                            }
                            GVProductInfo.DataSource = dsSkuFltr;
                            GVProductInfo.DataBind();
                        }
                    }
                    catch { }
                    finally { CommonFltr.Close(); }
                }

                else if (Request.QueryString["invoker"] == "totaldeliveryvstotalrequest")
                {
                    iUCCommonFilterClient CommonFltr = new iUCCommonFilterClient();
                    CustomProfile profile1 = CustomProfile.GetProfile();
                    try
                    {
                        //select MP.ProductCode,MP.Name,MP.Description,img.Path,MP.CompanyID ,MP.StoreId,MP.GroupSet  from mproduct MP left outer join tImage img on  MP.ID=img.ReferenceID   where MP.CompanyID like '%%' and MP.StoreId like '%%' and MP.GroupSet like '%%'  
                        //if((hdnSelectedCompany.Value != "0") && (hdnSelectedCompany.Value != "") && (hdnSelectedDepartment.Value != "0") && (hdnSelectedDepartment.Value != "") && (hdnSelectedGroupSet.Value != "0") && (hdnSelectedGroupSet.Value != ""))                        
                        DataSet dsSkuFltr = new DataSet();
                        var SearchedValue = hdnFilterText.Value;
                        if (hdnSKU.Value == "sku")
                        {
                            if (SearchedValue == "")
                            {
                                dsSkuFltr = CommonFltr.SkulistReport(hdnSelectedCompany.Value, hdnSelectedDepartment.Value, hdnSelectedGroupSet.Value, hdnSelectedImage.Value, profile.DBConnection._constr);
                            }
                            else
                            {
                                dsSkuFltr = CommonFltr.SkulistReportSearch(hdnSelectedCompany.Value, hdnSelectedDepartment.Value, hdnSelectedGroupSet.Value, hdnSelectedImage.Value, SearchedValue, profile.DBConnection._constr);
                            }
                            GVProductInfo.DataSource = dsSkuFltr;
                            GVProductInfo.DataBind();
                        }
                    }
                    catch { }
                    finally { CommonFltr.Close(); }
                }


                else if (Request.QueryString["invoker"] == "deliverylogrpt")
                {
                    if (hdnRequestSelectedRec.Value == "")
                    {
                        GVProductInfo.DataSource = null;
                        GVProductInfo.DataBind();
                    }
                    else
                    {
                        DataSet dsecom1 = new DataSet();
                        dsecom1 = UCCommonFilter.GetDriverlogReport(hdnRequestSelectedRec.Value, profile.DBConnection._constr);
                        if (hdnProductSelectedRec.Value == "0")
                        {
                            GVProductInfo.SelectedRecords = new ArrayList();
                            foreach (DataRow rec in dsecom1.Tables[0].Rows)
                            {
                                Hashtable row = new Hashtable();
                                //row["ID"] = rec.ID;
                                row["ID"] = rec["ID"];
                                //row["ProductCode"] = rec.ProductCode;
                                row["ProductCode"] = rec["ProductCode"];
                                //row["Name"] = rec.Name;
                                row["Name"] = rec["Name"];
                                GVProductInfo.SelectedRecords.Add(row);
                                if (hdnProductSelectedRec.Value != "") { hdnProductSelectedRec.Value = hdnProductSelectedRec.Value + "," + ID.ToString(); }
                                else if (hdnProductSelectedRec.Value == "") { hdnProductSelectedRec.Value = ID.ToString(); }
                            }
                        }

                        GVProductInfo.DataSource = dsecom1;
                        GVProductInfo.DataBind();
                    }
                }

                //else if (Request.QueryString["invoker"] == "bulkrpt")
                //{
                //    if (hdnRequestSelectedRec.Value == "")
                //    {
                //        GVProductInfo.DataSource = null;
                //        GVProductInfo.DataBind();
                //    }
                //    else
                //    {
                //        DataSet dsecom1 = new DataSet();
                //        dsecom1 = UCCommonFilter.GetDriverlogReport(hdnRequestSelectedRec.Value, profile.DBConnection._constr);
                //        if (hdnProductSelectedRec.Value == "0")
                //        {
                //            GVProductInfo.SelectedRecords = new ArrayList();
                //            foreach (DataRow rec in dsecom1.Tables[0].Rows)
                //            {
                //                Hashtable row = new Hashtable();
                //                //row["ID"] = rec.ID;
                //                row["ID"] = rec["ID"];
                //                //row["ProductCode"] = rec.ProductCode;
                //                row["ProductCode"] = rec["ProductCode"];
                //                //row["Name"] = rec.Name;
                //                row["Name"] = rec["Name"];
                //                GVProductInfo.SelectedRecords.Add(row);
                //                if (hdnProductSelectedRec.Value != "") { hdnProductSelectedRec.Value = hdnProductSelectedRec.Value + "," + ID.ToString(); }
                //                else if (hdnProductSelectedRec.Value == "") { hdnProductSelectedRec.Value = ID.ToString(); }
                //            }
                //        }

                //        GVProductInfo.DataSource = dsecom1;
                //        GVProductInfo.DataBind();
                //    }
                //}

                else if (Request.QueryString["invoker"] == "ecommerce1")
                {
                    if (hdnRequestSelectedRec.Value == "")
                    {
                        GVProductInfo.DataSource = null;
                        GVProductInfo.DataBind();
                    }
                    else
                    {
                        DataSet dsecom1 = new DataSet();
                        dsecom1 = UCCommonFilter.GetAllEcommerce1DataSelectedRequestID(hdnRequestSelectedRec.Value, profile.DBConnection._constr);

                        if (hdnProductSelectedRec.Value == "0")
                        {
                            GVProductInfo.SelectedRecords = new ArrayList();
                            foreach (DataRow rec in dsecom1.Tables[0].Rows)
                            {
                                Hashtable row = new Hashtable();
                                //row["ID"] = rec.ID;
                                row["ID"] = rec["ID"];
                                //row["ProductCode"] = rec.ProductCode;
                                row["ProductCode"] = rec["ProductCode"];
                                //row["Name"] = rec.Name;
                                row["Name"] = rec["Name"];
                                GVProductInfo.SelectedRecords.Add(row);
                                if (hdnProductSelectedRec.Value != "") { hdnProductSelectedRec.Value = hdnProductSelectedRec.Value + "," + ID.ToString(); }
                                else if (hdnProductSelectedRec.Value == "") { hdnProductSelectedRec.Value = ID.ToString(); }
                            }
                        }

                        GVProductInfo.DataSource = dsecom1;
                        GVProductInfo.DataBind();
                    }
                }

                else if (Request.QueryString["invoker"] == "ecommerce2")
                {
                    if (hdnRequestSelectedRec.Value == "")
                    {
                        GVProductInfo.DataSource = null;
                        GVProductInfo.DataBind();
                    }
                    else
                    {
                        DataSet dsecom1 = new DataSet();
                        dsecom1 = UCCommonFilter.GetAllEcommerce1DataSelectedRequestID(hdnRequestSelectedRec.Value, profile.DBConnection._constr);

                        if (hdnProductSelectedRec.Value == "0")
                        {
                            GVProductInfo.SelectedRecords = new ArrayList();
                            foreach (DataRow rec in dsecom1.Tables[0].Rows)
                            {
                                Hashtable row = new Hashtable();
                                //row["ID"] = rec.ID;
                                row["ID"] = rec["ID"];
                                //row["ProductCode"] = rec.ProductCode;
                                row["ProductCode"] = rec["ProductCode"];
                                //row["Name"] = rec.Name;
                                row["Name"] = rec["Name"];
                                GVProductInfo.SelectedRecords.Add(row);
                                if (hdnProductSelectedRec.Value != "") { hdnProductSelectedRec.Value = hdnProductSelectedRec.Value + "," + ID.ToString(); }
                                else if (hdnProductSelectedRec.Value == "") { hdnProductSelectedRec.Value = ID.ToString(); }
                            }
                        }

                        GVProductInfo.DataSource = dsecom1;
                        GVProductInfo.DataBind();
                    }
                }


                else if (Request.QueryString["invoker"] == "deliverynote")
                {
                    if (hdnRequestSelectedRec.Value == "")
                    {
                        GVProductInfo.DataSource = null;
                        GVProductInfo.DataBind();
                    }
                    else
                    {
                        DataSet dsecom1 = new DataSet();
                        dsecom1 = UCCommonFilter.GetReportDataDelivery(long.Parse(hdnRequestSelectedRec.Value), profile.DBConnection._constr);

                        if (hdnProductSelectedRec.Value == "0")
                        {
                            GVProductInfo.SelectedRecords = new ArrayList();
                            foreach (DataRow rec in dsecom1.Tables[0].Rows)
                            {
                                Hashtable row = new Hashtable();
                                //row["ID"] = rec.ID;
                                row["ID"] = rec["ID"];
                                //row["ProductCode"] = rec.ProductCode;
                                row["ProductCode"] = rec["ProductCode"];
                                //row["Name"] = rec.Name;
                                row["Name"] = rec["Name"];
                                GVProductInfo.SelectedRecords.Add(row);
                                if (hdnProductSelectedRec.Value != "") { hdnProductSelectedRec.Value = hdnProductSelectedRec.Value + "," + ID.ToString(); }
                                else if (hdnProductSelectedRec.Value == "") { hdnProductSelectedRec.Value = ID.ToString(); }
                            }
                        }

                        GVProductInfo.DataSource = dsecom1;
                        GVProductInfo.DataBind();
                    }
                }
                else if (Request.QueryString["invoker"] == "normalorderdeliverynote")//created by suraj khopade
                {
                    if (hdnRequestSelectedRec.Value == "")
                    {
                        GVProductInfo.DataSource = null;
                        GVProductInfo.DataBind();
                    }
                    else
                    {
                        DataSet dsecom1 = new DataSet();
                        dsecom1 = UCCommonFilter.GetNormalOrderReportDataDelivery(long.Parse(hdnRequestSelectedRec.Value), profile.DBConnection._constr);

                        if (hdnProductSelectedRec.Value == "0")
                        {
                            GVProductInfo.SelectedRecords = new ArrayList();
                            foreach (DataRow rec in dsecom1.Tables[0].Rows)
                            {
                                Hashtable row = new Hashtable();
                                //row["ID"] = rec.ID;
                                row["ID"] = rec["ID"];
                                //row["ProductCode"] = rec.ProductCode;
                                row["ProductCode"] = rec["ProductCode"];
                                //row["Name"] = rec.Name;
                                row["Name"] = rec["Name"];
                                GVProductInfo.SelectedRecords.Add(row);
                                if (hdnProductSelectedRec.Value != "") { hdnProductSelectedRec.Value = hdnProductSelectedRec.Value + "," + ID.ToString(); }
                                else if (hdnProductSelectedRec.Value == "") { hdnProductSelectedRec.Value = ID.ToString(); }
                            }
                        }

                        GVProductInfo.DataSource = dsecom1;
                        GVProductInfo.DataBind();
                    }
                }

                //Add by suraj
                else if (Request.QueryString["invoker"] == "orderlead")
                {
                    if (hdnRequestSelectedRec.Value == "")
                    {
                        GVProductInfo.DataSource = null;
                        GVProductInfo.DataBind();
                    }
                    else
                    {
                        DataSet dsordlead = new DataSet();
                        dsordlead = UCCommonFilter.OrderLeadReport(hdnRequestSelectedRec.Value, profile.DBConnection._constr);
                        if (hdnProductSelectedRec.Value == "0")
                        {
                            GVProductInfo.SelectedRecords = new ArrayList();
                            foreach (DataRow rec in dsordlead.Tables[0].Rows)
                            {
                                Hashtable row = new Hashtable();
                                //row["ID"] = rec.ID;
                                row["ID"] = rec["ID"];
                                //row["ProductCode"] = rec.ProductCode;
                                row["ProductCode"] = rec["ProductCode"];
                                //row["Name"] = rec.Name;
                                row["Name"] = rec["Name"];
                                GVProductInfo.SelectedRecords.Add(row);
                                if (hdnProductSelectedRec.Value != "") { hdnProductSelectedRec.Value = hdnProductSelectedRec.Value + "," + ID.ToString(); }
                                else if (hdnProductSelectedRec.Value == "") { hdnProductSelectedRec.Value = ID.ToString(); }
                            }
                        }

                        GVProductInfo.DataSource = dsordlead;
                        GVProductInfo.DataBind();
                    }
                }

                else if (Request.QueryString["invoker"] == "qnbnstock")
                {
                    iUCCommonFilterClient CommonFltr = new iUCCommonFilterClient();
                    CustomProfile profile1 = CustomProfile.GetProfile();
                    try
                    {
                        //select MP.ProductCode,MP.Name,MP.Description,img.Path,MP.CompanyID ,MP.StoreId,MP.GroupSet  from mproduct MP left outer join tImage img on  MP.ID=img.ReferenceID   where MP.CompanyID like '%%' and MP.StoreId like '%%' and MP.GroupSet like '%%'  
                        //if((hdnSelectedCompany.Value != "0") && (hdnSelectedCompany.Value != "") && (hdnSelectedDepartment.Value != "0") && (hdnSelectedDepartment.Value != "") && (hdnSelectedGroupSet.Value != "0") && (hdnSelectedGroupSet.Value != ""))                        
                        DataSet dsSkuFltr = new DataSet();
                        var SearchedValue = hdnFilterText.Value;
                        if (hdnSelectedCompany.Value == "0") { hdnSelectedDepartment.Value = "0"; }
                        if (hdnSKU.Value == "sku")
                        {
                            if (SearchedValue == "")
                            {
                                dsSkuFltr = CommonFltr.SkulistReport(hdnSelectedCompany.Value, hdnSelectedDepartment.Value, hdnSelectedGroupSet.Value, hdnSelectedImage.Value, profile.DBConnection._constr);
                            }
                            else
                            {
                                dsSkuFltr = CommonFltr.SkulistReportSearch(hdnSelectedCompany.Value, hdnSelectedDepartment.Value, hdnSelectedGroupSet.Value, hdnSelectedImage.Value, SearchedValue, profile.DBConnection._constr);
                            }
                            GVProductInfo.DataSource = dsSkuFltr;
                            GVProductInfo.DataBind();
                        }
                    }
                    catch { }
                    finally { CommonFltr.Close(); }
                }

                else if (Request.QueryString["invoker"] == "qnbnorder")
                {
                    if (hdnRequestSelectedRec.Value == "")
                    {
                        GVProductInfo.DataSource = null;
                        GVProductInfo.DataBind();
                    }
                    else
                    {
                        prdlist = UCCommonFilter.SKUDetailsBySelectedRequestID(hdnRequestSelectedRec.Value, profile.DBConnection._constr);

                        if (hdnProductSelectedRec.Value == "0")
                        {
                            GVProductInfo.SelectedRecords = new ArrayList();
                            foreach (DataRow rec in prdlist.Tables[0].Rows)
                            {
                                Hashtable row = new Hashtable();
                                //row["ID"] = rec.ID;
                                row["ID"] = rec["ID"];
                                //row["ProductCode"] = rec.ProductCode;
                                row["ProductCode"] = rec["ProductCode"];
                                //row["Name"] = rec.Name;
                                row["Name"] = rec["Name"];
                                GVProductInfo.SelectedRecords.Add(row);
                                if (hdnProductSelectedRec.Value != "") { hdnProductSelectedRec.Value = hdnProductSelectedRec.Value + "," + ID.ToString(); }
                                else if (hdnProductSelectedRec.Value == "") { hdnProductSelectedRec.Value = ID.ToString(); }
                            }
                        }

                        GVProductInfo.DataSource = prdlist;
                        GVProductInfo.DataBind();
                    }
                }
                else if (Request.QueryString["invoker"] == "vat")
                {
                    DataSet ReqLst = new DataSet();
                    CustomProfile profile1 = CustomProfile.GetProfile();
                    string companyid = profile1.Personal.CompanyID.ToString();
                    string usertype = profile.Personal.UserType.ToString();
                    try
                    {
                        if (hdnSelectedCompany.Value == "0") { hdnSelectedDepartment.Value = "0"; }
                        if (usertype == "Super Admin")
                        {
                            // hdnSelectedCompany.Value = "0";
                            ReqLst = UCCommonFilter.GetAllOrderDetails(frmdt_lcl, todt_lcl, hdnSelectedCompany.Value, hdnSelectedDepartment.Value, hdnSelectedUser.Value, ddlStatus.SelectedValue, profile1.DBConnection._constr);
                        }
                        else
                        {
                            hdnSelectedCompany.Value = companyid;
                            ReqLst = UCCommonFilter.GetAllOrderDetails(frmdt_lcl, todt_lcl, hdnSelectedCompany.Value, hdnSelectedDepartment.Value, hdnSelectedUser.Value, ddlStatus.SelectedValue, profile1.DBConnection._constr);
                        }
                        GVRequestInfo.DataSource = ReqLst;
                        GVRequestInfo.DataBind();
                    }
                    catch { }
                    finally { }
                }
            }

            catch (SystemException ex)
            {

            }

        }

        protected void GVEngineInfo_OnRebind(object sender, EventArgs e)
        {
            if (Request.QueryString["invoker"] == "partconsumption")
            {
                fillDetail();
            }
        }
        protected void GVreturncollectionreport_OnRebind(object sender, EventArgs e)
        {
            if (Request.QueryString["invoker"] == "ReturnCollectionReport")
            {
                fillReturnOrderDetail();
            }else if(Request.QueryString["invoker"] == "ReceiptSummaryReport")
            {
                fillReturnOrderSummaryReportDetail();
            }
        }

        protected void GVRequestInfo_OnRebind(object sender, EventArgs e)
        {
            if (Request.QueryString["invoker"] == "partrequest")
            {
                fillDetail();
            }
            else if (Request.QueryString["invoker"] == "order")
            {
                fillDetail();
            }
            else if (Request.QueryString["invoker"] == "orderdetail")
            {
                fillDetail();
            }
            else if (Request.QueryString["invoker"] == "orderlead")
            {
                fillDetail();
            }
            else if (Request.QueryString["invoker"] == "orderdelivery")
            {
                fillDetail();
            }
            else if (Request.QueryString["invoker"] == "sla")
            {
                fillDetail();
            }
            else if (Request.QueryString["invoker"] == "location")
            {
                fillDetail();
            }
            else if (Request.QueryString["invoker"] == "deliverylogrpt")
            {
                fillDetail();
            }
            //else if (Request.QueryString["invoker"] == "bulkrpt")
            //{
            //    fillDetail();
            //}
            else if (Request.QueryString["invoker"] == "ecommerce1")
            {
                fillDetail();
            }
            else if (Request.QueryString["invoker"] == "ecommerce2")
            {
                fillDetail();
            }
            else if (Request.QueryString["invoker"] == "deliverynote")
            {
                fillDetail();
            }
            else if (Request.QueryString["invoker"] == "normalorderdeliverynote")   //change by suraj khopade
            {
                fillDetail();
            }
            else if (Request.QueryString["invoker"] == "avgtime")
            {
                fillDetail();
            }
            else if (Request.QueryString["invoker"] == "noofdelivery")
            {
                fillDetail();
            }
            else if (Request.QueryString["invoker"] == "qnbnorder")
            {
                fillDetail();
            }
            else if (Request.QueryString["invoker"] == "skutrack")
            {
                fillDetail();
            }
            else if (Request.QueryString["invoker"] == "sitetrack")
            {
                fillDetail();
            }
            else if (Request.QueryString["invoker"] == "depttrack")
            {
                fillDetail();
            }
            else if (Request.QueryString["invoker"] == "vat")
            {
                fillDetail();
            }

        }

        protected void GVIssueInfo_OnRebind(object sender, EventArgs e)
        {
            if (Request.QueryString["invoker"] == "partissue")
            {
                fillDetail();
            }
        }

        protected void GVReceiptInfo_OnRebind(object sender, EventArgs e)
        {
            if (Request.QueryString["invoker"] == "partreceipt")
            {
                fillDetail();
            }
        }

        protected void GVProductInfo_OnRebind(object sender, EventArgs e)
        {
            fillProduct();
        }

        protected void GVUserInfo_OnRebind(object sender, EventArgs e)
        {
            if (Request.QueryString["invoker"] == "user")
            {
                fillDetail();
            }
        }

        protected void gridexport_Rebind(object sender, EventArgs e)
        {
            if (Request.QueryString["invoker"] == "ecommerce2")
            {
                fillDetail();
            }
        }

        //change by suraj khopade
        protected void btntransactionExQuery_Click(object sender, EventArgs e)
        {
            //fillTransactionReport();
            //string Selectedcategory = hdnSelectedcategory.Value;
            //string Selectedcateria = hdnSelectedcateria.Value;
            string Selectedcategory = ddltransactioncategory.SelectedItem.ToString();

            string Selectedcateria = txtcateria.Text;
            string pageurl = "TransactionReportPopup.aspx?Selectedcategory=" + Selectedcategory + "&Selectedcateria=" + Selectedcateria + "";

            string strPopup = "<script language='javascript' ID='script1'>"



            // Passing intId to popup window.
            + "window.open( '" + pageurl

            + "','new window', 'top=150, left=400, width=1200, height=800, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=yes, scrollbars=y, toolbar=no, status=no, center=yes')"

            + "</script>";

            ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);
        }

        public void GridVisibleTF(string invoker)
        {
            tblProduct.Attributes.Add("style", "display:table;");
            if (invoker == "partconsumption")
            {
                UpToDate.Attributes.Add("Style", "display:none;");
                tblRequest.Attributes.Add("style", "display:none;");
                tblIssue.Attributes.Add("style", "display:none;");
                tblReceipt.Attributes.Add("style", "display:none;");
                tblEngine.Attributes.Add("style", "display:table;");
                PrdCategory.Attributes.Add("style", "display:none;");
                divECommerce.Attributes.Add("Style", "display:none");
                ExcludeZero.Attributes.Add("style", "display:none;");
                dvLocation.Attributes.Add("Style", "display:none");
                frmSite.Attributes.Add("style", "display:none;");
                toSite.Attributes.Add("style", "display:none;"); dvVehicle.Attributes.Add("Style", "display:none");
                divOrdno.Attributes.Add("style", "display:none;"); dvRoute.Attributes.Add("Style", "display:none");
                dvtolocation.Attributes.Add("Style", "display:none");
                 divsite.Attributes.Add("style", "display:none;");
                divsku.Attributes.Add("style", "display:none;");
                divordcategory.Attributes.Add("style", "display:none;");
                divordtype.Attributes.Add("style", "display:none;");
                tbulkrpt.Attributes.Add("style", "display:none;");

                Category.Attributes.Add("style", "display:none;");
                cateria.Attributes.Add("style", "display:none;");

                //tblTransactionReport.Attributes.Add("style", "display:none;");
                btntransactionExQuery.Attributes.Add("style", "display:none;");
                ReturnDepartment.Attributes.Add("style", "display:none;");
                ReturnCustomer.Attributes.Add("style", "display:none;");
                ReturnDriver.Attributes.Add("style", "display:none;");
                ReturnStatus.Attributes.Add("style", "display:none;");
                tblreturncollectionreport.Attributes.Add("style", "display:none;");
            }
            else if (invoker == "partrequest")
            {
                UpToDate.Attributes.Add("Style", "display:none;");
                tblEngine.Attributes.Add("style", "display:none;");
                tblIssue.Attributes.Add("style", "display:none;");
                tblReceipt.Attributes.Add("style", "display:none;");
                tblRequest.Attributes.Add("style", "display:table;");
                PrdCategory.Attributes.Add("style", "display:none;");
                divECommerce.Attributes.Add("Style", "display:none");
                ExcludeZero.Attributes.Add("style", "display:none;");
                dvLocation.Attributes.Add("Style", "display:none");
                frmSite.Attributes.Add("style", "display:none;");
                toSite.Attributes.Add("style", "display:none;"); dvVehicle.Attributes.Add("Style", "display:none");
                divOrdno.Attributes.Add("style", "display:none;"); dvRoute.Attributes.Add("Style", "display:none");
                dvtolocation.Attributes.Add("Style", "display:none");
                divsite.Attributes.Add("style", "display:none;");
                divsku.Attributes.Add("style", "display:none;");
                divordcategory.Attributes.Add("style", "display:none;");
                divordtype.Attributes.Add("style", "display:none;");
                tbulkrpt.Attributes.Add("style", "display:none;");

                Category.Attributes.Add("style", "display:none;");
                cateria.Attributes.Add("style", "display:none;");

                //tblTransactionReport.Attributes.Add("style", "display:none;");
                btntransactionExQuery.Attributes.Add("style", "display:none;");
                ReturnDepartment.Attributes.Add("style", "display:none;");
                ReturnCustomer.Attributes.Add("style", "display:none;");
                ReturnDriver.Attributes.Add("style", "display:none;");
                ReturnStatus.Attributes.Add("style", "display:none;");
                tblreturncollectionreport.Attributes.Add("style", "display:none;");

            }
            else if (invoker == "partissue")
            {
                tblEngine.Attributes.Add("style", "display:none;");
                tblRequest.Attributes.Add("style", "display:none;");
                tblReceipt.Attributes.Add("style", "display:none;");
                tblIssue.Attributes.Add("style", "display:table;");
                PrdCategory.Attributes.Add("style", "display:none;");
                UpToDate.Attributes.Add("Style", "display:none;");
                ExcludeZero.Attributes.Add("style", "display:none;");
                dvLocation.Attributes.Add("Style", "display:none");
                frmSite.Attributes.Add("style", "display:none;"); dvVehicle.Attributes.Add("Style", "display:none");
                toSite.Attributes.Add("style", "display:none;"); divECommerce.Attributes.Add("Style", "display:none");
                divOrdno.Attributes.Add("style", "display:none;"); dvRoute.Attributes.Add("Style", "display:none");
                dvtolocation.Attributes.Add("Style", "display:none");
                divsite.Attributes.Add("style", "display:none;");

                divsku.Attributes.Add("style", "display:none;");
                divordcategory.Attributes.Add("style", "display:none;");
                divordtype.Attributes.Add("style", "display:none;");
                tbulkrpt.Attributes.Add("style", "display:none;");

                Category.Attributes.Add("style", "display:none;");
                cateria.Attributes.Add("style", "display:none;");
             //   tblTransactionReport.Attributes.Add("style", "display:none;");
                btntransactionExQuery.Attributes.Add("style", "display:none;");
                ReturnDepartment.Attributes.Add("style", "display:none;");
                ReturnCustomer.Attributes.Add("style", "display:none;");
                ReturnDriver.Attributes.Add("style", "display:none;");
                ReturnStatus.Attributes.Add("style", "display:none;");
                tblreturncollectionreport.Attributes.Add("style", "display:none;");
            }
            else if (invoker == "partreceipt")
            {
                tblEngine.Attributes.Add("style", "display:none;");
                tblRequest.Attributes.Add("style", "display:none;");
                tblIssue.Attributes.Add("style", "display:none;");
                tblReceipt.Attributes.Add("style", "display:table;");
                PrdCategory.Attributes.Add("style", "display:none;");
                UpToDate.Attributes.Add("Style", "display:none;");
                ExcludeZero.Attributes.Add("style", "display:none;");
                dvLocation.Attributes.Add("Style", "display:none");
                frmSite.Attributes.Add("style", "display:none;"); dvVehicle.Attributes.Add("Style", "display:none");
                toSite.Attributes.Add("style", "display:none;"); divECommerce.Attributes.Add("Style", "display:none");
                divOrdno.Attributes.Add("style", "display:none;"); dvRoute.Attributes.Add("Style", "display:none");
                dvtolocation.Attributes.Add("Style", "display:none");
                divsite.Attributes.Add("style", "display:none;");
                divsku.Attributes.Add("style", "display:none;");
                divordcategory.Attributes.Add("style", "display:none;");
                divordtype.Attributes.Add("style", "display:none;");
                tbulkrpt.Attributes.Add("style", "display:none;");

                Category.Attributes.Add("style", "display:none;");
                cateria.Attributes.Add("style", "display:none;");
              //  tblTransactionReport.Attributes.Add("style", "display:none;");
                btntransactionExQuery.Attributes.Add("style", "display:none;");
                ReturnDepartment.Attributes.Add("style", "display:none;");
                ReturnCustomer.Attributes.Add("style", "display:none;");
                ReturnDriver.Attributes.Add("style", "display:none;");
                ReturnStatus.Attributes.Add("style", "display:none;");
                tblreturncollectionreport.Attributes.Add("style", "display:none;");
            }
            else if (invoker == "monthly")
            {
                tblEngine.Attributes.Add("style", "display:none;");
                tblRequest.Attributes.Add("style", "display:none;");
                tblIssue.Attributes.Add("style", "display:none;");
                tblReceipt.Attributes.Add("style", "display:none;");
                tblProduct.Attributes.Add("style", "display:none;");
                PrdCategory.Attributes.Add("style", "display:none;");
                UpToDate.Attributes.Add("Style", "display:none;");
                ExcludeZero.Attributes.Add("style", "display:none;");
                dvLocation.Attributes.Add("Style", "display:none");
                frmSite.Attributes.Add("style", "display:none;"); dvVehicle.Attributes.Add("Style", "display:none");
                toSite.Attributes.Add("style", "display:none;"); divECommerce.Attributes.Add("Style", "display:none");
                divOrdno.Attributes.Add("style", "display:none;"); dvRoute.Attributes.Add("Style", "display:none");
                dvtolocation.Attributes.Add("Style", "display:none");
                divsite.Attributes.Add("style", "display:none;");
                divsku.Attributes.Add("style", "display:none;");
                divordcategory.Attributes.Add("style", "display:none;");
                divordtype.Attributes.Add("style", "display:none;");
                tbulkrpt.Attributes.Add("style", "display:none;");

                Category.Attributes.Add("style", "display:none;");
                cateria.Attributes.Add("style", "display:none;");
               // tblTransactionReport.Attributes.Add("style", "display:none;");
                btntransactionExQuery.Attributes.Add("style", "display:none;");
                ReturnDepartment.Attributes.Add("style", "display:none;");
                ReturnCustomer.Attributes.Add("style", "display:none;");
                ReturnDriver.Attributes.Add("style", "display:none;");
                ReturnStatus.Attributes.Add("style", "display:none;");
                tblreturncollectionreport.Attributes.Add("style", "display:none;");

            }
            else if (invoker == "weeklylst")
            {
                tblEngine.Attributes.Add("style", "display:none;");
                tblRequest.Attributes.Add("style", "display:none;");
                tblIssue.Attributes.Add("style", "display:none;");
                tblReceipt.Attributes.Add("style", "display:none;");
                tblProduct.Attributes.Add("style", "display:none;");
                //SiteList.Attributes.Add("style", "display:none;");
                PrdCategory.Attributes.Add("style", "display:none;");
                UpToDate.Attributes.Add("Style", "display:none;");
                ExcludeZero.Attributes.Add("style", "display:none;");
                dvLocation.Attributes.Add("Style", "display:none");
                frmSite.Attributes.Add("style", "display:none;"); dvVehicle.Attributes.Add("Style", "display:none");
                toSite.Attributes.Add("style", "display:none;"); divECommerce.Attributes.Add("Style", "display:none");
                divOrdno.Attributes.Add("style", "display:none;"); dvRoute.Attributes.Add("Style", "display:none");
                dvtolocation.Attributes.Add("Style", "display:none");
                divsite.Attributes.Add("style", "display:none;");
                divsku.Attributes.Add("style", "display:none;");
                divordcategory.Attributes.Add("style", "display:none;");
                divordtype.Attributes.Add("style", "display:none;");
                tbulkrpt.Attributes.Add("style", "display:none;");

                Category.Attributes.Add("style", "display:none;");
                cateria.Attributes.Add("style", "display:none;");
               // tblTransactionReport.Attributes.Add("style", "display:none;");
                btntransactionExQuery.Attributes.Add("style", "display:none;");
                ReturnDepartment.Attributes.Add("style", "display:none;");
                ReturnCustomer.Attributes.Add("style", "display:none;");
                ReturnDriver.Attributes.Add("style", "display:none;");
                ReturnStatus.Attributes.Add("style", "display:none;");
                tblreturncollectionreport.Attributes.Add("style", "display:none;");
            }
            else if (invoker == "consumabletracker")
            {
                tblEngine.Attributes.Add("style", "display:none;");
                tblRequest.Attributes.Add("style", "display:none;");
                tblIssue.Attributes.Add("style", "display:none;");
                tblReceipt.Attributes.Add("style", "display:none;");
                tblProduct.Attributes.Add("style", "display:none;");
                //SiteList.Attributes.Add("style", "display:none;");
                UpToDate.Attributes.Add("Style", "display:none;");
                ExcludeZero.Attributes.Add("style", "display:none;");
                dvLocation.Attributes.Add("Style", "display:none");
                frmSite.Attributes.Add("style", "display:none;");
                toSite.Attributes.Add("style", "display:none;");
                divECommerce.Attributes.Add("Style", "display:none"); dvVehicle.Attributes.Add("Style", "display:none");
                divOrdno.Attributes.Add("style", "display:none;"); dvRoute.Attributes.Add("Style", "display:none");
                dvtolocation.Attributes.Add("Style", "display:none");
                divsite.Attributes.Add("style", "display:none;");
                divsku.Attributes.Add("style", "display:none;");
                divordcategory.Attributes.Add("style", "display:none;");
                divordtype.Attributes.Add("style", "display:none;");
                tbulkrpt.Attributes.Add("style", "display:none;");

                Category.Attributes.Add("style", "display:none;");
                cateria.Attributes.Add("style", "display:none;");
             //   tblTransactionReport.Attributes.Add("style", "display:none;");
                btntransactionExQuery.Attributes.Add("style", "display:none;");
                ReturnDepartment.Attributes.Add("style", "display:none;");
                ReturnCustomer.Attributes.Add("style", "display:none;");
                ReturnDriver.Attributes.Add("style", "display:none;");
                ReturnStatus.Attributes.Add("style", "display:none;");
                tblreturncollectionreport.Attributes.Add("style", "display:none;");
            }

            else if (invoker == "productdtl")
            {
                UpToDate.Attributes.Add("Style", "display:none;");
                tblEngine.Attributes.Add("style", "display:none;");
                tblRequest.Attributes.Add("style", "display:none;");
                tblIssue.Attributes.Add("style", "display:none;");
                tblReceipt.Attributes.Add("style", "display:none;");
                tblProduct.Attributes.Add("style", "display:table;");
                PrdCategory.Attributes.Add("style", "display:none;");
                FDate.Attributes.Add("style", "display:none;");
                TDate.Attributes.Add("style", "display:none;");
                ExcludeZero.Attributes.Add("style", "display:table;"); dvVehicle.Attributes.Add("Style", "display:none");
                frmSite.Attributes.Add("style", "display:none;"); divECommerce.Attributes.Add("Style", "display:none");
                toSite.Attributes.Add("style", "display:none;"); dvLocation.Attributes.Add("Style", "display:none");
                divOrdno.Attributes.Add("style", "display:none;"); dvRoute.Attributes.Add("Style", "display:none");
                dvtolocation.Attributes.Add("Style", "display:none");
                divsite.Attributes.Add("style", "display:none;");
                divsku.Attributes.Add("style", "display:none;");
                divordcategory.Attributes.Add("style", "display:none;");
                divordtype.Attributes.Add("style", "display:none;");
                tbulkrpt.Attributes.Add("style", "display:none;");

                Category.Attributes.Add("style", "display:none;");
                cateria.Attributes.Add("style", "display:none;");
              //  tblTransactionReport.Attributes.Add("style", "display:none;");
                btntransactionExQuery.Attributes.Add("style", "display:none;");
                ReturnDepartment.Attributes.Add("style", "display:none;");
                ReturnCustomer.Attributes.Add("style", "display:none;");
                ReturnDriver.Attributes.Add("style", "display:none;");
                ReturnStatus.Attributes.Add("style", "display:none;");
                tblreturncollectionreport.Attributes.Add("style", "display:none;");
            }
            else if (invoker == "transfer")
            {
                UpToDate.Attributes.Add("Style", "display:none;");
                tblEngine.Attributes.Add("style", "display:none;");
                tblRequest.Attributes.Add("style", "display:none;");
                tblIssue.Attributes.Add("style", "display:none;");
                tblReceipt.Attributes.Add("style", "display:none;");
                tblProduct.Attributes.Add("style", "display:none;");
                PrdCategory.Attributes.Add("style", "display:none;");
                ExcludeZero.Attributes.Add("style", "display:none;");
                frmSite.Attributes.Add("style", "display:table;");
                toSite.Attributes.Add("style", "display:table;"); dvVehicle.Attributes.Add("Style", "display:none");
                SiteList.Attributes.Add("style", "display:none;"); divECommerce.Attributes.Add("Style", "display:none");
                ImgStatus.Attributes.Add("style", "display:none;"); dvLocation.Attributes.Add("Style", "display:none");
                divOrdno.Attributes.Add("style", "display:none;"); dvRoute.Attributes.Add("Style", "display:none");
                dvtolocation.Attributes.Add("Style", "display:none");
                divsite.Attributes.Add("style", "display:none;");
                divsku.Attributes.Add("style", "display:none;");
                divordcategory.Attributes.Add("style", "display:none;");
                divordtype.Attributes.Add("style", "display:none;");
                tbulkrpt.Attributes.Add("style", "display:none;");

                Category.Attributes.Add("style", "display:none;");
                cateria.Attributes.Add("style", "display:none;");
              //  tblTransactionReport.Attributes.Add("style", "display:none;");
                btntransactionExQuery.Attributes.Add("style", "display:none;");
                ReturnDepartment.Attributes.Add("style", "display:none;");
                ReturnCustomer.Attributes.Add("style", "display:none;");
                ReturnDriver.Attributes.Add("style", "display:none;");
                ReturnStatus.Attributes.Add("style", "display:none;");
                tblreturncollectionreport.Attributes.Add("style", "display:none;");
            }
            else if (invoker == "asset")
            {
                UpToDate.Attributes.Add("Style", "display:none;");
                tblEngine.Attributes.Add("style", "display:none;");
                tblRequest.Attributes.Add("style", "display:none;");
                tblIssue.Attributes.Add("style", "display:none;");
                tblReceipt.Attributes.Add("style", "display:none;");
                tblProduct.Attributes.Add("style", "display:none;");
                PrdCategory.Attributes.Add("style", "display:none;");
                ExcludeZero.Attributes.Add("style", "display:none;");
                frmSite.Attributes.Add("style", "display:table;");
                toSite.Attributes.Add("style", "display:table;"); dvVehicle.Attributes.Add("Style", "display:none");
                SiteList.Attributes.Add("style", "display:none;"); divECommerce.Attributes.Add("Style", "display:none");
                ImgStatus.Attributes.Add("style", "display:none;"); dvLocation.Attributes.Add("Style", "display:none");
                divOrdno.Attributes.Add("style", "display:none;"); dvRoute.Attributes.Add("Style", "display:none");
                dvtolocation.Attributes.Add("Style", "display:none");
                divsite.Attributes.Add("style", "display:none;");
                divsku.Attributes.Add("style", "display:none;");
                divordcategory.Attributes.Add("style", "display:none;");
                divordtype.Attributes.Add("style", "display:none;");
                tbulkrpt.Attributes.Add("style", "display:none;");

                Category.Attributes.Add("style", "display:none;");
                cateria.Attributes.Add("style", "display:none;");
              //  tblTransactionReport.Attributes.Add("style", "display:none;");
                btntransactionExQuery.Attributes.Add("style", "display:none;");
                ReturnDepartment.Attributes.Add("style", "display:none;");
                ReturnCustomer.Attributes.Add("style", "display:none;");
                ReturnDriver.Attributes.Add("style", "display:none;");
                ReturnStatus.Attributes.Add("style", "display:none;");
                tblreturncollectionreport.Attributes.Add("style", "display:none;");
            }

            else if (invoker == "sku")
            {
                tblEngine.Attributes.Add("style", "display:none;");
                tblRequest.Attributes.Add("style", "display:none;");
                tblIssue.Attributes.Add("style", "display:none;");
                tblReceipt.Attributes.Add("style", "display:none;");
                PrdCategory.Attributes.Add("style", "display:none;");
                ExcludeZero.Attributes.Add("style", "display:none;");
                frmSite.Attributes.Add("style", "display:none;");
                toSite.Attributes.Add("style", "display:none;");
                Status.Attributes.Add("style", "display:none;");
                User.Attributes.Add("style", "display:none;");
                FDate.Attributes.Add("style", "display:none;");
                TDate.Attributes.Add("style", "display:none;");
                tblProduct.Attributes.Add("style", "display:table;");
                SiteList.Attributes.Add("style", "display:none;");
                Company.Attributes.Add("style", "display:table;");
                Department.Attributes.Add("style", "display:table;");
                Groupset1.Attributes.Add("style", "display:table;");
                Image.Attributes.Add("style", "display:table;");
                tblUserInfo.Attributes.Add("style", "display:none;");
                ZeroBalance.Attributes.Add("style", "display:table;");
                UpToDate.Attributes.Add("Style", "display:none;");
                Role.Attributes.Add("style", "display:none;");
                Active.Attributes.Add("style", "display:none;");
                dvLocation.Attributes.Add("Style", "display:none");
                ImgStatus.Attributes.Add("style", "display:none;");
                Driver.Attributes.Add("Style", "display:none");
                PytMode.Attributes.Add("Style", "display:none"); divECommerce.Attributes.Add("Style", "display:none");
                DlryType.Attributes.Add("Style", "display:none"); dvVehicle.Attributes.Add("Style", "display:none");
                divOrdno.Attributes.Add("style", "display:none;"); dvRoute.Attributes.Add("Style", "display:none");
                dvtolocation.Attributes.Add("Style", "display:none");
                divsite.Attributes.Add("style", "display:none;");
                divsku.Attributes.Add("style", "display:none;");
                divordcategory.Attributes.Add("style", "display:none;");
                divordtype.Attributes.Add("style", "display:none;");
                tbulkrpt.Attributes.Add("style", "display:none;");

                Category.Attributes.Add("style", "display:none;");
                cateria.Attributes.Add("style", "display:none;");
              //  tblTransactionReport.Attributes.Add("style", "display:none;");
                btntransactionExQuery.Attributes.Add("style", "display:none;");
                ReturnDepartment.Attributes.Add("style", "display:none;");
                ReturnCustomer.Attributes.Add("style", "display:none;");
                ReturnDriver.Attributes.Add("style", "display:none;");
                ReturnStatus.Attributes.Add("style", "display:none;");
                tblreturncollectionreport.Attributes.Add("style", "display:none;");
            }


            else if (invoker == "user")
            {
                UpToDate.Attributes.Add("Style", "display:none;");
                tblEngine.Attributes.Add("style", "display:none;");
                tblRequest.Attributes.Add("style", "display:none;");
                tblIssue.Attributes.Add("style", "display:none;");
                tblReceipt.Attributes.Add("style", "display:none;");
                PrdCategory.Attributes.Add("style", "display:none;");
                ExcludeZero.Attributes.Add("style", "display:none;");
                frmSite.Attributes.Add("style", "display:none;");
                toSite.Attributes.Add("style", "display:none;");
                tblProduct.Attributes.Add("style", "display:none;");
                Groupset1.Attributes.Add("style", "display:none;");
                Image.Attributes.Add("style", "display:none;");
                SiteList.Attributes.Add("style", "display:none;");
                Company.Attributes.Add("style", "display:table;");
                Department.Attributes.Add("style", "display:table;");
                SiteList.Attributes.Add("style", "display:none;");
                tblRequest.Attributes.Add("style", "display:none;");
                tblProduct.Attributes.Add("style", "display:none;");
                FDate.Attributes.Add("style", "display:none;");
                TDate.Attributes.Add("style", "display:none;");
                Department.Attributes.Add("style", "display:table;");
                Company.Attributes.Add("style", "display:table;");
                Status.Attributes.Add("style", "display:none;");
                User.Attributes.Add("style", "display:none;");
                tblUserInfo.Attributes.Add("style", "display:table;");
                Role.Attributes.Add("style", "display:table;");
                Active.Attributes.Add("style", "display:table;");
                ZeroBalance.Attributes.Add("style", "display:none;");
                ImgStatus.Attributes.Add("style", "display:none;");
                Driver.Attributes.Add("Style", "display:none"); dvVehicle.Attributes.Add("Style", "display:none");
                PytMode.Attributes.Add("Style", "display:none"); divECommerce.Attributes.Add("Style", "display:none");
                DlryType.Attributes.Add("Style", "display:none"); dvLocation.Attributes.Add("Style", "display:none");
                divOrdno.Attributes.Add("style", "display:none;"); dvRoute.Attributes.Add("Style", "display:none");
                dvtolocation.Attributes.Add("Style", "display:none");
                divsite.Attributes.Add("style", "display:none;");
                divsku.Attributes.Add("style", "display:none;");
                divordcategory.Attributes.Add("style", "display:none;");
                divordtype.Attributes.Add("style", "display:none;");
                tbulkrpt.Attributes.Add("style", "display:none;");

                Category.Attributes.Add("style", "display:none;");
                cateria.Attributes.Add("style", "display:none;");
              //  tblTransactionReport.Attributes.Add("style", "display:none;");
                btntransactionExQuery.Attributes.Add("style", "display:none;");
                ReturnDepartment.Attributes.Add("style", "display:none;");
                ReturnCustomer.Attributes.Add("style", "display:none;");
                ReturnDriver.Attributes.Add("style", "display:none;");
                ReturnStatus.Attributes.Add("style", "display:none;");
                tblreturncollectionreport.Attributes.Add("style", "display:none;");

            }
            else if (invoker == "order")
            {
                tblEngine.Attributes.Add("style", "display:none;");
                tblIssue.Attributes.Add("style", "display:none;");
                tblReceipt.Attributes.Add("style", "display:none;");
                PrdCategory.Attributes.Add("style", "display:none;");
                ExcludeZero.Attributes.Add("style", "display:none;");
                frmSite.Attributes.Add("style", "display:none;");
                toSite.Attributes.Add("style", "display:none;");

                Groupset1.Attributes.Add("style", "display:none;");
                Image.Attributes.Add("style", "display:none;");
                SiteList.Attributes.Add("style", "display:none;");

                FDate.Attributes.Add("style", "display:table;");
                TDate.Attributes.Add("style", "display:table;");
                Company.Attributes.Add("style", "display:table;");
                Department.Attributes.Add("style", "display:table;");
                Status.Attributes.Add("style", "display:table;");
                User.Attributes.Add("style", "display:table;");
                tblProduct.Attributes.Add("style", "display:none;");
                tblRequest.Attributes.Add("style", "display:table;");
                tblUserInfo.Attributes.Add("style", "display:none;");

                Role.Attributes.Add("style", "display:none;");
                Active.Attributes.Add("style", "display:none;");
                UpToDate.Attributes.Add("Style", "display:none;");
                ZeroBalance.Attributes.Add("style", "display:none;");
                ImgStatus.Attributes.Add("style", "display:none;");
                Driver.Attributes.Add("Style", "display:none"); dvVehicle.Attributes.Add("Style", "display:none");
                PytMode.Attributes.Add("Style", "display:none"); divECommerce.Attributes.Add("Style", "display:none");
                DlryType.Attributes.Add("Style", "display:none"); dvLocation.Attributes.Add("Style", "display:none");
                divOrdno.Attributes.Add("style", "display:none;"); dvRoute.Attributes.Add("Style", "display:none");
                dvtolocation.Attributes.Add("Style", "display:none");
                divsite.Attributes.Add("style", "display:none;");
                divsku.Attributes.Add("style", "display:none;");
                divordcategory.Attributes.Add("style", "display:none;");
                divordtype.Attributes.Add("style", "display:none;");
                tbulkrpt.Attributes.Add("style", "display:none;");

                Category.Attributes.Add("style", "display:none;");
                cateria.Attributes.Add("style", "display:none;");
              //  tblTransactionReport.Attributes.Add("style", "display:none;");
                btntransactionExQuery.Attributes.Add("style", "display:none;");
                ReturnDepartment.Attributes.Add("style", "display:none;");
                ReturnCustomer.Attributes.Add("style", "display:none;");
                ReturnDriver.Attributes.Add("style", "display:none;");
                ReturnStatus.Attributes.Add("style", "display:none;");
                tblreturncollectionreport.Attributes.Add("style", "display:none;");
            }

            else if (invoker == "orderlead")
            {
                tblEngine.Attributes.Add("style", "display:none;");
                tblIssue.Attributes.Add("style", "display:none;");
                tblReceipt.Attributes.Add("style", "display:none;");
                PrdCategory.Attributes.Add("style", "display:none;");
                ExcludeZero.Attributes.Add("style", "display:none;");
                frmSite.Attributes.Add("style", "display:none;");
                toSite.Attributes.Add("style", "display:none;");

                Groupset1.Attributes.Add("style", "display:none;");
                Image.Attributes.Add("style", "display:none;");
                SiteList.Attributes.Add("style", "display:none;");

                FDate.Attributes.Add("style", "display:table;");
                TDate.Attributes.Add("style", "display:table;");
                Company.Attributes.Add("style", "display:table;");
                Department.Attributes.Add("style", "display:table;");
                Status.Attributes.Add("style", "display:table;");
                User.Attributes.Add("style", "display:table;");
                tblProduct.Attributes.Add("style", "display:none;");
                tblRequest.Attributes.Add("style", "display:table;");
                tblUserInfo.Attributes.Add("style", "display:none;");

                Role.Attributes.Add("style", "display:none;");
                Active.Attributes.Add("style", "display:none;");
                UpToDate.Attributes.Add("Style", "display:none;");
                ZeroBalance.Attributes.Add("style", "display:none;");
                ImgStatus.Attributes.Add("style", "display:none;");
                Driver.Attributes.Add("Style", "display:none"); dvVehicle.Attributes.Add("Style", "display:none");
                PytMode.Attributes.Add("Style", "display:none"); divECommerce.Attributes.Add("Style", "display:none");
                DlryType.Attributes.Add("Style", "display:none"); dvLocation.Attributes.Add("Style", "display:none");
                divOrdno.Attributes.Add("style", "display:none;"); dvRoute.Attributes.Add("Style", "display:none");
                dvtolocation.Attributes.Add("Style", "display:none");
                divsite.Attributes.Add("style", "display:none;");
                divsku.Attributes.Add("style", "display:none;");
                divordcategory.Attributes.Add("style", "display:none;");
                divordtype.Attributes.Add("style", "display:none;");
                tbulkrpt.Attributes.Add("style", "display:none;");

                Category.Attributes.Add("style", "display:none;");
                cateria.Attributes.Add("style", "display:none;");
              //  tblTransactionReport.Attributes.Add("style", "display:none;");
                btntransactionExQuery.Attributes.Add("style", "display:none;");
                ReturnDepartment.Attributes.Add("style", "display:none;");
                ReturnCustomer.Attributes.Add("style", "display:none;");
                ReturnDriver.Attributes.Add("style", "display:none;");
                ReturnStatus.Attributes.Add("style", "display:none;");
                tblreturncollectionreport.Attributes.Add("style", "display:none;");
            }

            else if (invoker == "orderdetail")
            {
                tblEngine.Attributes.Add("style", "display:none;");
                tblIssue.Attributes.Add("style", "display:none;");
                tblReceipt.Attributes.Add("style", "display:none;");
                PrdCategory.Attributes.Add("style", "display:none;");
                ExcludeZero.Attributes.Add("style", "display:none;");
                frmSite.Attributes.Add("style", "display:none;");
                toSite.Attributes.Add("style", "display:none;");

                Groupset1.Attributes.Add("style", "display:none;");
                Image.Attributes.Add("style", "display:none;");
                SiteList.Attributes.Add("style", "display:none;");

                FDate.Attributes.Add("style", "display:table;");
                TDate.Attributes.Add("style", "display:table;");
                Company.Attributes.Add("style", "display:table;");
                Department.Attributes.Add("style", "display:table;");
                Status.Attributes.Add("style", "display:table;");
                User.Attributes.Add("style", "display:table;");
                tblProduct.Attributes.Add("style", "display:none;");
                tblRequest.Attributes.Add("style", "display:table;");
                tblUserInfo.Attributes.Add("style", "display:none;");
                UpToDate.Attributes.Add("Style", "display:none;");
                Role.Attributes.Add("style", "display:none;");
                Active.Attributes.Add("style", "display:none;");

                ZeroBalance.Attributes.Add("style", "display:none;");
                ImgStatus.Attributes.Add("style", "display:none;");
                Driver.Attributes.Add("Style", "display:none"); dvVehicle.Attributes.Add("Style", "display:none");
                PytMode.Attributes.Add("Style", "display:none"); divECommerce.Attributes.Add("Style", "display:none");
                DlryType.Attributes.Add("Style", "display:none"); dvLocation.Attributes.Add("Style", "display:none");
                divOrdno.Attributes.Add("style", "display:none;"); dvRoute.Attributes.Add("Style", "display:none");
                dvtolocation.Attributes.Add("Style", "display:none");
                divsite.Attributes.Add("style", "display:none;");
                divsku.Attributes.Add("style", "display:none;");
                divordcategory.Attributes.Add("style", "display:none;");
                divordtype.Attributes.Add("style", "display:none;");
                tbulkrpt.Attributes.Add("style", "display:none;");

                Category.Attributes.Add("style", "display:none;");
                cateria.Attributes.Add("style", "display:none;");
               // tblTransactionReport.Attributes.Add("style", "display:none;");
                btntransactionExQuery.Attributes.Add("style", "display:none;");
                ReturnDepartment.Attributes.Add("style", "display:none;");
                ReturnCustomer.Attributes.Add("style", "display:none;");
                ReturnDriver.Attributes.Add("style", "display:none;");
                ReturnStatus.Attributes.Add("style", "display:none;");
                tblreturncollectionreport.Attributes.Add("style", "display:none;");
            }

            else if (invoker == "SkuDetails")
            {
                tblEngine.Attributes.Add("style", "display:none;");
                tblRequest.Attributes.Add("style", "display:none;");
                tblIssue.Attributes.Add("style", "display:none;");
                tblReceipt.Attributes.Add("style", "display:none;");
                PrdCategory.Attributes.Add("style", "display:none;");
                ExcludeZero.Attributes.Add("style", "display:none;");
                frmSite.Attributes.Add("style", "display:none;");
                toSite.Attributes.Add("style", "display:none;");
                Status.Attributes.Add("style", "display:none;");
                User.Attributes.Add("style", "display:none;");
                FDate.Attributes.Add("style", "display:table;");
                TDate.Attributes.Add("style", "display:table;");
                tblProduct.Attributes.Add("style", "display:table;");
                SiteList.Attributes.Add("style", "display:none;");
                Company.Attributes.Add("style", "display:table;");
                Department.Attributes.Add("style", "display:table;");
                Groupset1.Attributes.Add("style", "display:table;");
                Image.Attributes.Add("style", "display:table;");
                tblUserInfo.Attributes.Add("style", "display:none;");

                Role.Attributes.Add("style", "display:none;");
                Active.Attributes.Add("style", "display:none;");
                UpToDate.Attributes.Add("Style", "display:none;");
                ZeroBalance.Attributes.Add("style", "display:none;");
                ImgStatus.Attributes.Add("style", "display:none;");
                Driver.Attributes.Add("Style", "display:none"); dvVehicle.Attributes.Add("Style", "display:none");
                PytMode.Attributes.Add("Style", "display:none"); divECommerce.Attributes.Add("Style", "display:none");
                DlryType.Attributes.Add("Style", "display:none"); dvLocation.Attributes.Add("Style", "display:none");
                divOrdno.Attributes.Add("style", "display:none;"); dvRoute.Attributes.Add("Style", "display:none");
                dvtolocation.Attributes.Add("Style", "display:none");
                divsite.Attributes.Add("style", "display:none;");
                divsku.Attributes.Add("style", "display:none;");
                divordcategory.Attributes.Add("style", "display:none;");
                divordtype.Attributes.Add("style", "display:none;");
                tbulkrpt.Attributes.Add("style", "display:none;");

                Category.Attributes.Add("style", "display:none;");
                cateria.Attributes.Add("style", "display:none;");
               // tblTransactionReport.Attributes.Add("style", "display:none;");
                btntransactionExQuery.Attributes.Add("style", "display:none;");
                ReturnDepartment.Attributes.Add("style", "display:none;");
                ReturnCustomer.Attributes.Add("style", "display:none;");
                ReturnDriver.Attributes.Add("style", "display:none;");
                ReturnStatus.Attributes.Add("style", "display:none;");
                tblreturncollectionreport.Attributes.Add("style", "display:none;");
            }

            else if (invoker == "BomDetail")
            {
                tblEngine.Attributes.Add("style", "display:none;");
                tblRequest.Attributes.Add("style", "display:none;");
                tblIssue.Attributes.Add("style", "display:none;");
                tblReceipt.Attributes.Add("style", "display:none;");
                PrdCategory.Attributes.Add("style", "display:none;");
                ExcludeZero.Attributes.Add("style", "display:none;");
                frmSite.Attributes.Add("style", "display:none;");
                toSite.Attributes.Add("style", "display:none;");
                Status.Attributes.Add("style", "display:none;");
                User.Attributes.Add("style", "display:none;");
                FDate.Attributes.Add("style", "display:none;");
                TDate.Attributes.Add("style", "display:none;");
                tblProduct.Attributes.Add("style", "display:table;");
                SiteList.Attributes.Add("style", "display:none;");
                Company.Attributes.Add("style", "display:table;");
                Department.Attributes.Add("style", "display:table;");
                Groupset1.Attributes.Add("style", "display:table;");
                Image.Attributes.Add("style", "display:table;");
                tblUserInfo.Attributes.Add("style", "display:none;");
                UpToDate.Attributes.Add("Style", "display:none;");
                Role.Attributes.Add("style", "display:none;");
                Active.Attributes.Add("style", "display:none;");

                ZeroBalance.Attributes.Add("style", "display:none;");
                ImgStatus.Attributes.Add("style", "display:none;");
                Driver.Attributes.Add("Style", "display:none"); dvVehicle.Attributes.Add("Style", "display:none");
                PytMode.Attributes.Add("Style", "display:none"); divECommerce.Attributes.Add("Style", "display:none");
                DlryType.Attributes.Add("Style", "display:none"); dvLocation.Attributes.Add("Style", "display:none");
                divOrdno.Attributes.Add("style", "display:none;"); dvRoute.Attributes.Add("Style", "display:none");
                dvtolocation.Attributes.Add("Style", "display:none");
                divsite.Attributes.Add("style", "display:none;");
                divsku.Attributes.Add("style", "display:none;");
                divordcategory.Attributes.Add("style", "display:none;");
                divordtype.Attributes.Add("style", "display:none;");
                tbulkrpt.Attributes.Add("style", "display:none;");

                Category.Attributes.Add("style", "display:none;");
                cateria.Attributes.Add("style", "display:none;");
                //tblTransactionReport.Attributes.Add("style", "display:none;");
                btntransactionExQuery.Attributes.Add("style", "display:none;");
                ReturnDepartment.Attributes.Add("style", "display:none;");
                ReturnCustomer.Attributes.Add("style", "display:none;");
                ReturnDriver.Attributes.Add("style", "display:none;");
                ReturnStatus.Attributes.Add("style", "display:none;");
                tblreturncollectionreport.Attributes.Add("style", "display:none;");
            }

            else if (invoker == "imgaudit")
            {
                tblEngine.Attributes.Add("style", "display:none;");
                tblIssue.Attributes.Add("style", "display:none;");
                tblReceipt.Attributes.Add("style", "display:none;");
                PrdCategory.Attributes.Add("style", "display:none;");
                ExcludeZero.Attributes.Add("style", "display:none;");
                frmSite.Attributes.Add("style", "display:none;");
                toSite.Attributes.Add("style", "display:none;");

                Groupset1.Attributes.Add("style", "display:none;");
                Image.Attributes.Add("style", "display:none;");
                SiteList.Attributes.Add("style", "display:none;");

                UpToDate.Attributes.Add("Style", "display:none;");
                FDate.Attributes.Add("style", "display:table;");
                TDate.Attributes.Add("style", "display:table;");
                Company.Attributes.Add("style", "display:table;");
                Department.Attributes.Add("style", "display:table;");
                Status.Attributes.Add("style", "display:none;");
                User.Attributes.Add("style", "display:table;");
                tblProduct.Attributes.Add("style", "display:table;");
                tblRequest.Attributes.Add("style", "display:none;");
                tblUserInfo.Attributes.Add("style", "display:none;");

                Role.Attributes.Add("style", "display:none;");
                Active.Attributes.Add("style", "display:none;");

                ZeroBalance.Attributes.Add("style", "display:none;");
                ImgStatus.Attributes.Add("style", "display:table;");
                Driver.Attributes.Add("Style", "display:none"); dvVehicle.Attributes.Add("Style", "display:none");
                PytMode.Attributes.Add("Style", "display:none"); divECommerce.Attributes.Add("Style", "display:none");
                DlryType.Attributes.Add("Style", "display:none"); dvLocation.Attributes.Add("Style", "display:none");
                divOrdno.Attributes.Add("style", "display:none;"); dvRoute.Attributes.Add("Style", "display:none");
                dvtolocation.Attributes.Add("Style", "display:none");
                divsite.Attributes.Add("style", "display:none;");
                divsku.Attributes.Add("style", "display:none;");
                divordcategory.Attributes.Add("style", "display:none;");
                divordtype.Attributes.Add("style", "display:none;");
                tbulkrpt.Attributes.Add("style", "display:none;");

                Category.Attributes.Add("style", "display:none;");
                cateria.Attributes.Add("style", "display:none;");
              //  tblTransactionReport.Attributes.Add("style", "display:none;");
                btntransactionExQuery.Attributes.Add("style", "display:none;");
                ReturnDepartment.Attributes.Add("style", "display:none;");
                ReturnCustomer.Attributes.Add("style", "display:none;");
                ReturnDriver.Attributes.Add("style", "display:none;");
                ReturnStatus.Attributes.Add("style", "display:none;");
                tblreturncollectionreport.Attributes.Add("style", "display:none;");
            }
            else if (invoker == "orderdelivery")
            {
                tblEngine.Attributes.Add("style", "display:none;");
                tblIssue.Attributes.Add("style", "display:none;");
                tblReceipt.Attributes.Add("style", "display:none;");
                PrdCategory.Attributes.Add("style", "display:none;");
                ExcludeZero.Attributes.Add("style", "display:none;");
                frmSite.Attributes.Add("style", "display:none;");
                toSite.Attributes.Add("style", "display:none;");

                Groupset1.Attributes.Add("style", "display:none;");
                Image.Attributes.Add("style", "display:none;");
                SiteList.Attributes.Add("style", "display:none;");

                UpToDate.Attributes.Add("Style", "display:none;");
                FDate.Attributes.Add("style", "display:table;");
                TDate.Attributes.Add("style", "display:table;");
                Company.Attributes.Add("style", "display:table;");
                Department.Attributes.Add("style", "display:table;");
                Status.Attributes.Add("style", "display:none;");
                User.Attributes.Add("style", "display:none;");
                tblProduct.Attributes.Add("style", "display:none;");
                tblRequest.Attributes.Add("style", "display:table;");
                tblUserInfo.Attributes.Add("style", "display:none;");

                Role.Attributes.Add("style", "display:none;");
                Active.Attributes.Add("style", "display:none;");

                ZeroBalance.Attributes.Add("style", "display:none;");
                ImgStatus.Attributes.Add("style", "display:none;");
                Driver.Attributes.Add("Style", "display:table"); dvVehicle.Attributes.Add("Style", "display:none");
                PytMode.Attributes.Add("Style", "display:table"); divECommerce.Attributes.Add("Style", "display:none");
                DlryType.Attributes.Add("Style", "display:none"); dvLocation.Attributes.Add("Style", "display:none");
                divOrdno.Attributes.Add("style", "display:none;"); dvRoute.Attributes.Add("Style", "display:none");
                dvtolocation.Attributes.Add("Style", "display:none");
                divsite.Attributes.Add("style", "display:none;");
                divsku.Attributes.Add("style", "display:none;");
                divordcategory.Attributes.Add("style", "display:none;");
                divordtype.Attributes.Add("style", "display:none;");
                tbulkrpt.Attributes.Add("style", "display:none;");

                Category.Attributes.Add("style", "display:none;");
                cateria.Attributes.Add("style", "display:none;");
               // tblTransactionReport.Attributes.Add("style", "display:none;");
                btntransactionExQuery.Attributes.Add("style", "display:none;");
                ReturnDepartment.Attributes.Add("style", "display:none;");
                ReturnCustomer.Attributes.Add("style", "display:none;");
                ReturnDriver.Attributes.Add("style", "display:none;");
                ReturnStatus.Attributes.Add("style", "display:none;");
                tblreturncollectionreport.Attributes.Add("style", "display:none;");
            }

            else if (invoker == "sla")
            {
                tblEngine.Attributes.Add("style", "display:none;");
                tblIssue.Attributes.Add("style", "display:none;");
                tblReceipt.Attributes.Add("style", "display:none;");
                PrdCategory.Attributes.Add("style", "display:none;");
                ExcludeZero.Attributes.Add("style", "display:none;");
                frmSite.Attributes.Add("style", "display:none;");
                toSite.Attributes.Add("style", "display:none;");

                Groupset1.Attributes.Add("style", "display:none;");
                Image.Attributes.Add("style", "display:none;");
                SiteList.Attributes.Add("style", "display:none;");

                UpToDate.Attributes.Add("Style", "display:none;");
                FDate.Attributes.Add("style", "display:table;");
                TDate.Attributes.Add("style", "display:table;");
                Company.Attributes.Add("style", "display:table;");
                Department.Attributes.Add("style", "display:table;");
                Status.Attributes.Add("style", "display:table;");
                User.Attributes.Add("style", "display:none;");
                tblProduct.Attributes.Add("style", "display:none;");
                tblRequest.Attributes.Add("style", "display:table;");
                tblUserInfo.Attributes.Add("style", "display:none;");

                Role.Attributes.Add("style", "display:none;");
                Active.Attributes.Add("style", "display:none;");

                ZeroBalance.Attributes.Add("style", "display:none;");
                ImgStatus.Attributes.Add("style", "display:none;");
                Driver.Attributes.Add("Style", "display:table");
                PytMode.Attributes.Add("Style", "display:none");
                DlryType.Attributes.Add("Style", "display:table"); dvVehicle.Attributes.Add("Style", "display:none");
                dvLocation.Attributes.Add("Style", "display:none"); divECommerce.Attributes.Add("Style", "display:none");
                divOrdno.Attributes.Add("style", "display:none;"); dvRoute.Attributes.Add("Style", "display:none");
                dvtolocation.Attributes.Add("Style", "display:none");
                divsite.Attributes.Add("style", "display:none;");
                divsku.Attributes.Add("style", "display:none;");
                divordcategory.Attributes.Add("style", "display:none;");
                divordtype.Attributes.Add("style", "display:none;");
                tbulkrpt.Attributes.Add("style", "display:none;");

                Category.Attributes.Add("style", "display:none;");
                cateria.Attributes.Add("style", "display:none;");
               // tblTransactionReport.Attributes.Add("style", "display:none;");
                btntransactionExQuery.Attributes.Add("style", "display:none;");
                ReturnDepartment.Attributes.Add("style", "display:none;");
                ReturnCustomer.Attributes.Add("style", "display:none;");
                ReturnDriver.Attributes.Add("style", "display:none;");
                ReturnStatus.Attributes.Add("style", "display:none;");
                tblreturncollectionreport.Attributes.Add("style", "display:none;");
            }
            else if (invoker == "totaldeliveryvstotalrequest")
            {
                tblEngine.Attributes.Add("style", "display:none;");
                tblIssue.Attributes.Add("style", "display:none;");
                tblReceipt.Attributes.Add("style", "display:none;");
                PrdCategory.Attributes.Add("style", "display:none;");
                ExcludeZero.Attributes.Add("style", "display:none;");
                frmSite.Attributes.Add("style", "display:none;");
                toSite.Attributes.Add("style", "display:none;");

                Groupset1.Attributes.Add("style", "display:none;");
                Image.Attributes.Add("style", "display:none;");
                SiteList.Attributes.Add("style", "display:none;");

                UpToDate.Attributes.Add("Style", "display:none;");
                FDate.Attributes.Add("style", "display:table;");
                TDate.Attributes.Add("style", "display:table;");
                Company.Attributes.Add("style", "display:table;");
                Department.Attributes.Add("style", "display:table;");
                Status.Attributes.Add("style", "display:none;");
                User.Attributes.Add("style", "display:none;");
                tblProduct.Attributes.Add("style", "display:table;");
                tblRequest.Attributes.Add("style", "display:none;");
                tblUserInfo.Attributes.Add("style", "display:none;");

                Role.Attributes.Add("style", "display:none;");
                Active.Attributes.Add("style", "display:none;");

                ZeroBalance.Attributes.Add("style", "display:none;");
                ImgStatus.Attributes.Add("style", "display:none;");
                Driver.Attributes.Add("Style", "display:none");
                PytMode.Attributes.Add("Style", "display:none");
                DlryType.Attributes.Add("Style", "display:none"); dvLocation.Attributes.Add("Style", "display:none");
                divECommerce.Attributes.Add("Style", "display:none"); dvVehicle.Attributes.Add("Style", "display:none");
                divOrdno.Attributes.Add("style", "display:none;"); dvRoute.Attributes.Add("Style", "display:none");
                dvtolocation.Attributes.Add("Style", "display:none");
                divsite.Attributes.Add("style", "display:none;");
                divsku.Attributes.Add("style", "display:none;");
                divordcategory.Attributes.Add("style", "display:none;");
                divordtype.Attributes.Add("style", "display:none;");
                tbulkrpt.Attributes.Add("style", "display:none;");

                Category.Attributes.Add("style", "display:none;");
                cateria.Attributes.Add("style", "display:none;");
               // tblTransactionReport.Attributes.Add("style", "display:none;");
                btntransactionExQuery.Attributes.Add("style", "display:none;");
                ReturnDepartment.Attributes.Add("style", "display:none;");
                ReturnCustomer.Attributes.Add("style", "display:none;");
                ReturnDriver.Attributes.Add("style", "display:none;");
                ReturnStatus.Attributes.Add("style", "display:none;");
                tblreturncollectionreport.Attributes.Add("style", "display:none;");
            }

            else if (invoker == "location")
            {
                tblEngine.Attributes.Add("style", "display:none;");
                tblIssue.Attributes.Add("style", "display:none;");
                tblReceipt.Attributes.Add("style", "display:none;");
                PrdCategory.Attributes.Add("style", "display:none;");
                ExcludeZero.Attributes.Add("style", "display:none;");
                frmSite.Attributes.Add("style", "display:none;");
                toSite.Attributes.Add("style", "display:none;");

                Groupset1.Attributes.Add("style", "display:none;");
                Image.Attributes.Add("style", "display:none;");
                SiteList.Attributes.Add("style", "display:none;");

                UpToDate.Attributes.Add("Style", "display:none;");
                FDate.Attributes.Add("style", "display:table;");
                TDate.Attributes.Add("style", "display:table;");
                Company.Attributes.Add("style", "display:table;");
                Department.Attributes.Add("style", "display:table;");
                Status.Attributes.Add("style", "display:none;");
                User.Attributes.Add("style", "display:none;");
                tblProduct.Attributes.Add("style", "display:none;");
                tblRequest.Attributes.Add("style", "display:table;");
                tblUserInfo.Attributes.Add("style", "display:none;");

                Role.Attributes.Add("style", "display:none;");
                Active.Attributes.Add("style", "display:none;");

                ZeroBalance.Attributes.Add("style", "display:none;");
                ImgStatus.Attributes.Add("style", "display:none;");
                Driver.Attributes.Add("Style", "display:none");
                PytMode.Attributes.Add("Style", "display:table");
                DlryType.Attributes.Add("Style", "display:none");
                dvLocation.Attributes.Add("Style", "display:table");
                divECommerce.Attributes.Add("Style", "display:table"); dvVehicle.Attributes.Add("Style", "display:none");
                divOrdno.Attributes.Add("style", "display:none;"); dvRoute.Attributes.Add("Style", "display:none");
                dvtolocation.Attributes.Add("Style", "display:none");
                divsite.Attributes.Add("style", "display:none;");
                divsku.Attributes.Add("style", "display:none;");
                divordcategory.Attributes.Add("style", "display:none;");
                divordtype.Attributes.Add("style", "display:none;");
                tbulkrpt.Attributes.Add("style", "display:none;");

                Category.Attributes.Add("style", "display:none;");
                cateria.Attributes.Add("style", "display:none;");
               // tblTransactionReport.Attributes.Add("style", "display:none;");
                btntransactionExQuery.Attributes.Add("style", "display:none;");
                ReturnDepartment.Attributes.Add("style", "display:none;");
                ReturnCustomer.Attributes.Add("style", "display:none;");
                ReturnDriver.Attributes.Add("style", "display:none;");
                ReturnStatus.Attributes.Add("style", "display:none;");
                tblreturncollectionreport.Attributes.Add("style", "display:none;");
            }
            else if (invoker == "deliverylogrpt")
            {
                tblEngine.Attributes.Add("style", "display:none;");
                tblIssue.Attributes.Add("style", "display:none;");
                tblReceipt.Attributes.Add("style", "display:none;");
                PrdCategory.Attributes.Add("style", "display:none;");
                ExcludeZero.Attributes.Add("style", "display:none;");
                frmSite.Attributes.Add("style", "display:none;");
                toSite.Attributes.Add("style", "display:none;");

                Groupset1.Attributes.Add("style", "display:none;");
                Image.Attributes.Add("style", "display:none;");
                SiteList.Attributes.Add("style", "display:none;");
                UpToDate.Attributes.Add("Style", "display:none;");
                FDate.Attributes.Add("style", "display:table;");
                TDate.Attributes.Add("style", "display:table;");
                Company.Attributes.Add("style", "display:table;");
                Department.Attributes.Add("style", "display:table;");
                Status.Attributes.Add("style", "display:table;");
                User.Attributes.Add("style", "display:table;");
                tblProduct.Attributes.Add("style", "display:none;");
                tblRequest.Attributes.Add("style", "display:table;");
                tblUserInfo.Attributes.Add("style", "display:none;");

                Role.Attributes.Add("style", "display:none;");
                Active.Attributes.Add("style", "display:none;");

                ZeroBalance.Attributes.Add("style", "display:none;");
                ImgStatus.Attributes.Add("style", "display:none;");
                Driver.Attributes.Add("Style", "display:none");
                PytMode.Attributes.Add("Style", "display:table");
                DlryType.Attributes.Add("Style", "display:none");
                dvLocation.Attributes.Add("Style", "display:none");
                divECommerce.Attributes.Add("Style", "display:none");
                divOrdno.Attributes.Add("style", "display:none;"); dvVehicle.Attributes.Add("Style", "display:none");
                divOrdno.Attributes.Add("style", "display:none;"); dvRoute.Attributes.Add("Style", "display:none");
                dvtolocation.Attributes.Add("Style", "display:none");
                divsite.Attributes.Add("style", "display:none;");
                divsku.Attributes.Add("style", "display:none;");
                divordcategory.Attributes.Add("style", "display:none;");
                divordtype.Attributes.Add("style", "display:none;");
                tbulkrpt.Attributes.Add("style", "display:none;");

                Category.Attributes.Add("style", "display:none;");
                cateria.Attributes.Add("style", "display:none;");
                //tblTransactionReport.Attributes.Add("style", "display:none;");
                btntransactionExQuery.Attributes.Add("style", "display:none;");
                ReturnDepartment.Attributes.Add("style", "display:none;");
                ReturnCustomer.Attributes.Add("style", "display:none;");
                ReturnDriver.Attributes.Add("style", "display:none;");
                ReturnStatus.Attributes.Add("style", "display:none;");
                tblreturncollectionreport.Attributes.Add("style", "display:none;");
            }
            else if (invoker == "bulkrpt")
            {
                tblEngine.Attributes.Add("style", "display:none;");
                tblIssue.Attributes.Add("style", "display:none;");
                tblReceipt.Attributes.Add("style", "display:none;");
                PrdCategory.Attributes.Add("style", "display:none;");
                ExcludeZero.Attributes.Add("style", "display:none;");
                frmSite.Attributes.Add("style", "display:none;");
                toSite.Attributes.Add("style", "display:none;");

                Groupset1.Attributes.Add("style", "display:none;");
                Image.Attributes.Add("style", "display:none;");
                SiteList.Attributes.Add("style", "display:none;");
                UpToDate.Attributes.Add("Style", "display:none;");
                FDate.Attributes.Add("style", "display:table;");
                TDate.Attributes.Add("style", "display:table;");
                Company.Attributes.Add("style", "display:none;");
                Department.Attributes.Add("style", "display:none;");
                Status.Attributes.Add("style", "display:none;");
                User.Attributes.Add("style", "display:none;");
                tblProduct.Attributes.Add("style", "display:none;");
                tblRequest.Attributes.Add("style", "display:none;");
                tblUserInfo.Attributes.Add("style", "display:none;");

                Role.Attributes.Add("style", "display:none;");
                Active.Attributes.Add("style", "display:none;");

                ZeroBalance.Attributes.Add("style", "display:none;");
                ImgStatus.Attributes.Add("style", "display:none;");
                Driver.Attributes.Add("Style", "display:none");
                PytMode.Attributes.Add("Style", "display:none");
                DlryType.Attributes.Add("Style", "display:none");
                dvLocation.Attributes.Add("Style", "display:none");
                divECommerce.Attributes.Add("Style", "display:none");
                divOrdno.Attributes.Add("style", "display:none;"); dvVehicle.Attributes.Add("Style", "display:none");
                divOrdno.Attributes.Add("style", "display:none;"); dvRoute.Attributes.Add("Style", "display:none");
                dvtolocation.Attributes.Add("Style", "display:none");
                divsite.Attributes.Add("style", "display:none;");
                divsku.Attributes.Add("style", "display:none;");
                divordcategory.Attributes.Add("style", "display:table;");
                divordtype.Attributes.Add("style", "display:table;");
                tbulkrpt.Attributes.Add("style", "display:table;");

                Category.Attributes.Add("style", "display:none;");
                cateria.Attributes.Add("style", "display:none;");
               // tblTransactionReport.Attributes.Add("style", "display:none;");
                btntransactionExQuery.Attributes.Add("style", "display:none;");
                ReturnDepartment.Attributes.Add("style", "display:none;");
                ReturnCustomer.Attributes.Add("style", "display:none;");
                ReturnDriver.Attributes.Add("style", "display:none;");
                ReturnStatus.Attributes.Add("style", "display:none;");
                tblreturncollectionreport.Attributes.Add("style", "display:none;");
            }

            else if (invoker == "ecommerce1")
            {
                tblEngine.Attributes.Add("style", "display:none;");
                tblIssue.Attributes.Add("style", "display:none;");
                tblReceipt.Attributes.Add("style", "display:none;");
                PrdCategory.Attributes.Add("style", "display:none;");
                ExcludeZero.Attributes.Add("style", "display:none;");
                frmSite.Attributes.Add("style", "display:none;");
                toSite.Attributes.Add("style", "display:none;");

                Groupset1.Attributes.Add("style", "display:none;");
                Image.Attributes.Add("style", "display:none;");
                SiteList.Attributes.Add("style", "display:none;");
                UpToDate.Attributes.Add("Style", "display:table;");
                FDate.Attributes.Add("style", "display:table;");
                TDate.Attributes.Add("style", "display:table;");
                Company.Attributes.Add("style", "display:table;");
                Department.Attributes.Add("style", "display:table;");
                Status.Attributes.Add("style", "display:table;");
                User.Attributes.Add("style", "display:table;");
                tblProduct.Attributes.Add("style", "display:none;");
                tblRequest.Attributes.Add("style", "display:table;");
                tblUserInfo.Attributes.Add("style", "display:none;");

                Role.Attributes.Add("style", "display:none;");
                Active.Attributes.Add("style", "display:none;");

                ZeroBalance.Attributes.Add("style", "display:none;");
                ImgStatus.Attributes.Add("style", "display:none;");
                Driver.Attributes.Add("Style", "display:none");
                PytMode.Attributes.Add("Style", "display:table");
                DlryType.Attributes.Add("Style", "display:none");
                dvLocation.Attributes.Add("Style", "display:none");
                divECommerce.Attributes.Add("Style", "display:none"); dvVehicle.Attributes.Add("Style", "display:none");
                divOrdno.Attributes.Add("style", "display:none;"); dvRoute.Attributes.Add("Style", "display:none");
                dvtolocation.Attributes.Add("Style", "display:none");
                divsite.Attributes.Add("style", "display:none;");
                divsku.Attributes.Add("style", "display:none;");
                divordcategory.Attributes.Add("style", "display:none;");
                divordtype.Attributes.Add("style", "display:none;");
                tbulkrpt.Attributes.Add("style", "display:none;");

                Category.Attributes.Add("style", "display:none;");
                cateria.Attributes.Add("style", "display:none;");
                //tblTransactionReport.Attributes.Add("style", "display:none;");
                btntransactionExQuery.Attributes.Add("style", "display:none;");
                ReturnDepartment.Attributes.Add("style", "display:none;");
                ReturnCustomer.Attributes.Add("style", "display:none;");
                ReturnDriver.Attributes.Add("style", "display:none;");
                ReturnStatus.Attributes.Add("style", "display:none;");
                tblreturncollectionreport.Attributes.Add("style", "display:none;");


            }

            else if (invoker == "ecommerce2")
            {
                tblEngine.Attributes.Add("style", "display:none;");
                tblIssue.Attributes.Add("style", "display:none;");
                tblReceipt.Attributes.Add("style", "display:none;");
                PrdCategory.Attributes.Add("style", "display:none;");
                ExcludeZero.Attributes.Add("style", "display:none;");
                frmSite.Attributes.Add("style", "display:none;");
                toSite.Attributes.Add("style", "display:none;");

                Groupset1.Attributes.Add("style", "display:none;");
                Image.Attributes.Add("style", "display:none;");
                SiteList.Attributes.Add("style", "display:none;");
                UpToDate.Attributes.Add("Style", "display:table;");
                FDate.Attributes.Add("style", "display:table;");
                TDate.Attributes.Add("style", "display:table;");
                Company.Attributes.Add("style", "display:table;");
                Department.Attributes.Add("style", "display:table;");
                Status.Attributes.Add("style", "display:table;");
                User.Attributes.Add("style", "display:table;");
                tblProduct.Attributes.Add("style", "display:none;");
                tblRequest.Attributes.Add("style", "display:table;");
                tblUserInfo.Attributes.Add("style", "display:none;");

                Role.Attributes.Add("style", "display:none;");
                Active.Attributes.Add("style", "display:none;");

                ZeroBalance.Attributes.Add("style", "display:none;");
                ImgStatus.Attributes.Add("style", "display:none;");
                Driver.Attributes.Add("Style", "display:none");
                PytMode.Attributes.Add("Style", "display:table");
                DlryType.Attributes.Add("Style", "display:none");
                dvLocation.Attributes.Add("Style", "display:none");
                divECommerce.Attributes.Add("Style", "display:none"); dvVehicle.Attributes.Add("Style", "display:none");
                divOrdno.Attributes.Add("style", "display:none;"); dvRoute.Attributes.Add("Style", "display:none");
                dvtolocation.Attributes.Add("Style", "display:none");
                divsite.Attributes.Add("style", "display:none;");
                divsku.Attributes.Add("style", "display:none;");
                divordcategory.Attributes.Add("style", "display:none;");
                divordtype.Attributes.Add("style", "display:none;");
                tbulkrpt.Attributes.Add("style", "display:none;");

                Category.Attributes.Add("style", "display:none;");
                cateria.Attributes.Add("style", "display:none;");
                //tblTransactionReport.Attributes.Add("style", "display:none;");
                btntransactionExQuery.Attributes.Add("style", "display:none;");
                ReturnDepartment.Attributes.Add("style", "display:none;");
                ReturnCustomer.Attributes.Add("style", "display:none;");
                ReturnDriver.Attributes.Add("style", "display:none;");
                ReturnStatus.Attributes.Add("style", "display:none;");
                tblreturncollectionreport.Attributes.Add("style", "display:none;");

            }
            else if (invoker == "deliverynote")
            {
                tblEngine.Attributes.Add("style", "display:none;");
                tblIssue.Attributes.Add("style", "display:none;");
                tblReceipt.Attributes.Add("style", "display:none;");
                PrdCategory.Attributes.Add("style", "display:none;");
                ExcludeZero.Attributes.Add("style", "display:none;");
                frmSite.Attributes.Add("style", "display:none;");
                toSite.Attributes.Add("style", "display:none;");

                Groupset1.Attributes.Add("style", "display:none;");
                Image.Attributes.Add("style", "display:none;");
                SiteList.Attributes.Add("style", "display:none;");
                UpToDate.Attributes.Add("Style", "display:none;");
                FDate.Attributes.Add("style", "display:none;");
                TDate.Attributes.Add("style", "display:none;");
                Company.Attributes.Add("style", "display:none;");
                Department.Attributes.Add("style", "display:none;");
                Status.Attributes.Add("style", "display:none;");
                User.Attributes.Add("style", "display:none;");
                tblProduct.Attributes.Add("style", "display:none;");
                tblRequest.Attributes.Add("style", "display:table;");
                tblUserInfo.Attributes.Add("style", "display:none;");

                Role.Attributes.Add("style", "display:none;");
                Active.Attributes.Add("style", "display:none;");

                ZeroBalance.Attributes.Add("style", "display:none;");
                ImgStatus.Attributes.Add("style", "display:none;");
                Driver.Attributes.Add("Style", "display:none");
                PytMode.Attributes.Add("Style", "display:none");
                DlryType.Attributes.Add("Style", "display:none");
                dvLocation.Attributes.Add("Style", "display:none");
                divECommerce.Attributes.Add("Style", "display:none");
                divOrdno.Attributes.Add("style", "display:table;");
                dvRoute.Attributes.Add("Style", "display:none"); dvVehicle.Attributes.Add("Style", "display:none");
                dvtolocation.Attributes.Add("Style", "display:none");
                divsite.Attributes.Add("style", "display:none;");
                divsku.Attributes.Add("style", "display:none;");
                divordcategory.Attributes.Add("style", "display:none;");
                divordtype.Attributes.Add("style", "display:none;");
                tbulkrpt.Attributes.Add("style", "display:none;");

                Category.Attributes.Add("style", "display:none;");
                cateria.Attributes.Add("style", "display:none;");
               // tblTransactionReport.Attributes.Add("style", "display:none;");
                btntransactionExQuery.Attributes.Add("style", "display:none;");
                ReturnDepartment.Attributes.Add("style", "display:none;");
                ReturnCustomer.Attributes.Add("style", "display:none;");
                ReturnDriver.Attributes.Add("style", "display:none;");
                ReturnStatus.Attributes.Add("style", "display:none;");
                tblreturncollectionreport.Attributes.Add("style", "display:none;");
            }
            else if (invoker == "avgtime")
            {
                tblEngine.Attributes.Add("style", "display:none;");
                tblIssue.Attributes.Add("style", "display:none;");
                tblReceipt.Attributes.Add("style", "display:none;");
                PrdCategory.Attributes.Add("style", "display:none;");
                ExcludeZero.Attributes.Add("style", "display:none;");
                frmSite.Attributes.Add("style", "display:none;");
                toSite.Attributes.Add("style", "display:none;");

                Groupset1.Attributes.Add("style", "display:none;");
                Image.Attributes.Add("style", "display:none;");
                SiteList.Attributes.Add("style", "display:none;");
                UpToDate.Attributes.Add("Style", "display:none;");
                FDate.Attributes.Add("style", "display:table;");
                TDate.Attributes.Add("style", "display:table;");
                Company.Attributes.Add("style", "display:none;");
                Department.Attributes.Add("style", "display:none;");
                Status.Attributes.Add("style", "display:table;");
                User.Attributes.Add("style", "display:none;");
                tblProduct.Attributes.Add("style", "display:none;");
                tblRequest.Attributes.Add("style", "display:table;");
                tblUserInfo.Attributes.Add("style", "display:none;");

                Role.Attributes.Add("style", "display:none;");
                Active.Attributes.Add("style", "display:none;");

                ZeroBalance.Attributes.Add("style", "display:none;");
                ImgStatus.Attributes.Add("style", "display:none;");
                Driver.Attributes.Add("Style", "display:table");
                PytMode.Attributes.Add("Style", "display:none");
                DlryType.Attributes.Add("Style", "display:none");
                dvLocation.Attributes.Add("Style", "display:table");
                divECommerce.Attributes.Add("Style", "display:none");
                divOrdno.Attributes.Add("style", "display:none;");
                dvRoute.Attributes.Add("Style", "display:table");
                dvVehicle.Attributes.Add("Style", "display:none");
                dvtolocation.Attributes.Add("Style", "display:table");
                divsite.Attributes.Add("style", "display:none;");
                divsku.Attributes.Add("style", "display:none;");
                divordcategory.Attributes.Add("style", "display:none;");
                divordtype.Attributes.Add("style", "display:none;");
                tbulkrpt.Attributes.Add("style", "display:none;");

                Category.Attributes.Add("style", "display:none;");
                cateria.Attributes.Add("style", "display:none;");
               // tblTransactionReport.Attributes.Add("style", "display:none;");
                btntransactionExQuery.Attributes.Add("style", "display:none;");
                ReturnDepartment.Attributes.Add("style", "display:none;");
                ReturnCustomer.Attributes.Add("style", "display:none;");
                ReturnDriver.Attributes.Add("style", "display:none;");
                ReturnStatus.Attributes.Add("style", "display:none;");
                tblreturncollectionreport.Attributes.Add("style", "display:none;");
            }
            else if (invoker == "noofdelivery")
            {
                tblEngine.Attributes.Add("style", "display:none;");
                tblIssue.Attributes.Add("style", "display:none;");
                tblReceipt.Attributes.Add("style", "display:none;");
                PrdCategory.Attributes.Add("style", "display:none;");
                ExcludeZero.Attributes.Add("style", "display:none;");
                frmSite.Attributes.Add("style", "display:none;");
                toSite.Attributes.Add("style", "display:none;");

                Groupset1.Attributes.Add("style", "display:none;");
                Image.Attributes.Add("style", "display:none;");
                SiteList.Attributes.Add("style", "display:none;");
                UpToDate.Attributes.Add("Style", "display:none;");
                FDate.Attributes.Add("style", "display:table;");
                TDate.Attributes.Add("style", "display:table;");
                Company.Attributes.Add("style", "display:none;");
                Department.Attributes.Add("style", "display:none;");
                Status.Attributes.Add("style", "display:table;");
                User.Attributes.Add("style", "display:none;");
                tblProduct.Attributes.Add("style", "display:none;");
                tblRequest.Attributes.Add("style", "display:table;");
                tblUserInfo.Attributes.Add("style", "display:none;");

                Role.Attributes.Add("style", "display:none;");
                Active.Attributes.Add("style", "display:none;");

                ZeroBalance.Attributes.Add("style", "display:none;");
                ImgStatus.Attributes.Add("style", "display:none;");
                Driver.Attributes.Add("Style", "display:table");
                PytMode.Attributes.Add("Style", "display:none");
                DlryType.Attributes.Add("Style", "display:none");
                dvLocation.Attributes.Add("Style", "display:none");
                divECommerce.Attributes.Add("Style", "display:none");
                divOrdno.Attributes.Add("style", "display:none;");
                dvRoute.Attributes.Add("Style", "display:none");
                dvVehicle.Attributes.Add("Style", "display:none");
                dvtolocation.Attributes.Add("Style", "display:none");
                divsite.Attributes.Add("style", "display:none;");
                divsku.Attributes.Add("style", "display:none;");
                divordcategory.Attributes.Add("style", "display:none;");
                divordtype.Attributes.Add("style", "display:none;");
                tbulkrpt.Attributes.Add("style", "display:none;");

                Category.Attributes.Add("style", "display:none;");
                cateria.Attributes.Add("style", "display:none;");
               // tblTransactionReport.Attributes.Add("style", "display:none;");
                btntransactionExQuery.Attributes.Add("style", "display:none;");
                ReturnDepartment.Attributes.Add("style", "display:none;");
                ReturnCustomer.Attributes.Add("style", "display:none;");
                ReturnDriver.Attributes.Add("style", "display:none;");
                ReturnStatus.Attributes.Add("style", "display:none;");
                tblreturncollectionreport.Attributes.Add("style", "display:none;");
            }
            else if (invoker == "driverschedule")
            {
                tblEngine.Attributes.Add("style", "display:none;");
                tblIssue.Attributes.Add("style", "display:none;");
                tblReceipt.Attributes.Add("style", "display:none;");
                PrdCategory.Attributes.Add("style", "display:none;");
                ExcludeZero.Attributes.Add("style", "display:none;");
                frmSite.Attributes.Add("style", "display:none;");
                toSite.Attributes.Add("style", "display:none;");

                Groupset1.Attributes.Add("style", "display:none;");
                Image.Attributes.Add("style", "display:none;");
                SiteList.Attributes.Add("style", "display:none;");
                UpToDate.Attributes.Add("Style", "display:none;");
                FDate.Attributes.Add("style", "display:table;");
                TDate.Attributes.Add("style", "display:table;");
                Company.Attributes.Add("style", "display:none;");
                Department.Attributes.Add("style", "display:none;");
                Status.Attributes.Add("style", "display:none;");
                User.Attributes.Add("style", "display:none;");
                tblProduct.Attributes.Add("style", "display:none;");
                tblRequest.Attributes.Add("style", "display:none;");
                tblUserInfo.Attributes.Add("style", "display:none;");

                Role.Attributes.Add("style", "display:none;");
                Active.Attributes.Add("style", "display:none;");

                ZeroBalance.Attributes.Add("style", "display:none;");
                ImgStatus.Attributes.Add("style", "display:none;");
                Driver.Attributes.Add("Style", "display:table");
                PytMode.Attributes.Add("Style", "display:none");
                DlryType.Attributes.Add("Style", "display:none");
                dvLocation.Attributes.Add("Style", "display:table");
                divECommerce.Attributes.Add("Style", "display:none");
                divOrdno.Attributes.Add("style", "display:none;");
                dvRoute.Attributes.Add("Style", "display:table");
                dvVehicle.Attributes.Add("Style", "display:none");
                btnexQuery.Attributes.Add("Style", "display:none");
                dvtolocation.Attributes.Add("Style", "display:table");
                divsite.Attributes.Add("style", "display:none;");
                divsku.Attributes.Add("style", "display:none;");
                divordcategory.Attributes.Add("style", "display:none;");
                divordtype.Attributes.Add("style", "display:none;");
                tbulkrpt.Attributes.Add("style", "display:none;");

                Category.Attributes.Add("style", "display:none;");
                cateria.Attributes.Add("style", "display:none;");
               // tblTransactionReport.Attributes.Add("style", "display:none;");
                btntransactionExQuery.Attributes.Add("style", "display:none;");
                ReturnDepartment.Attributes.Add("style", "display:none;");
                ReturnCustomer.Attributes.Add("style", "display:none;");
                ReturnDriver.Attributes.Add("style", "display:none;");
                ReturnStatus.Attributes.Add("style", "display:none;");
                tblreturncollectionreport.Attributes.Add("style", "display:none;");
            }
            else if (invoker == "qnbnstock")
            {
                tblEngine.Attributes.Add("style", "display:none;");
                tblRequest.Attributes.Add("style", "display:none;");
                tblIssue.Attributes.Add("style", "display:none;");
                tblReceipt.Attributes.Add("style", "display:none;");
                PrdCategory.Attributes.Add("style", "display:none;");
                ExcludeZero.Attributes.Add("style", "display:none;");
                frmSite.Attributes.Add("style", "display:none;");
                toSite.Attributes.Add("style", "display:none;");
                Status.Attributes.Add("style", "display:none;");
                User.Attributes.Add("style", "display:none;");
                FDate.Attributes.Add("style", "display:none;");
                TDate.Attributes.Add("style", "display:none;");
                tblProduct.Attributes.Add("style", "display:table;");
                SiteList.Attributes.Add("style", "display:none;");
                Company.Attributes.Add("style", "display:table;");
                Department.Attributes.Add("style", "display:table;");
                Groupset1.Attributes.Add("style", "display:table;");
                Image.Attributes.Add("style", "display:table;");
                tblUserInfo.Attributes.Add("style", "display:none;");
                ZeroBalance.Attributes.Add("style", "display:table;");
                UpToDate.Attributes.Add("Style", "display:none;");
                Role.Attributes.Add("style", "display:none;");
                Active.Attributes.Add("style", "display:none;");
                dvLocation.Attributes.Add("Style", "display:none");
                ImgStatus.Attributes.Add("style", "display:none;");
                Driver.Attributes.Add("Style", "display:none");
                PytMode.Attributes.Add("Style", "display:none"); divECommerce.Attributes.Add("Style", "display:none");
                DlryType.Attributes.Add("Style", "display:none"); dvVehicle.Attributes.Add("Style", "display:none");
                divOrdno.Attributes.Add("style", "display:none;"); dvRoute.Attributes.Add("Style", "display:none");
                dvtolocation.Attributes.Add("Style", "display:none");
                divsite.Attributes.Add("style", "display:none;");
                divsku.Attributes.Add("style", "display:none;");
                divordcategory.Attributes.Add("style", "display:none;");
                divordtype.Attributes.Add("style", "display:none;");
                tbulkrpt.Attributes.Add("style", "display:none;");

                Category.Attributes.Add("style", "display:none;");
                cateria.Attributes.Add("style", "display:none;");
                //tblTransactionReport.Attributes.Add("style", "display:none;");
                btntransactionExQuery.Attributes.Add("style", "display:none;");
                ReturnDepartment.Attributes.Add("style", "display:none;");
                ReturnCustomer.Attributes.Add("style", "display:none;");
                ReturnDriver.Attributes.Add("style", "display:none;");
                ReturnStatus.Attributes.Add("style", "display:none;");
                tblreturncollectionreport.Attributes.Add("style", "display:none;");
            }
            else if (invoker == "qnbnorder")
            {
                tblEngine.Attributes.Add("style", "display:none;");
                tblIssue.Attributes.Add("style", "display:none;");
                tblReceipt.Attributes.Add("style", "display:none;");
                PrdCategory.Attributes.Add("style", "display:none;");
                ExcludeZero.Attributes.Add("style", "display:none;");
                frmSite.Attributes.Add("style", "display:none;");
                toSite.Attributes.Add("style", "display:none;");

                Groupset1.Attributes.Add("style", "display:none;");
                Image.Attributes.Add("style", "display:none;");
                SiteList.Attributes.Add("style", "display:none;");

                FDate.Attributes.Add("style", "display:table;");
                TDate.Attributes.Add("style", "display:table;");
                Company.Attributes.Add("style", "display:table;");
                Department.Attributes.Add("style", "display:table;");
                Status.Attributes.Add("style", "display:table;");
                User.Attributes.Add("style", "display:table;");
                tblProduct.Attributes.Add("style", "display:none;");
                tblRequest.Attributes.Add("style", "display:table;");
                tblUserInfo.Attributes.Add("style", "display:none;");

                Role.Attributes.Add("style", "display:none;");
                Active.Attributes.Add("style", "display:none;");
                UpToDate.Attributes.Add("Style", "display:none;");
                ZeroBalance.Attributes.Add("style", "display:none;");
                ImgStatus.Attributes.Add("style", "display:none;");
                Driver.Attributes.Add("Style", "display:none"); dvVehicle.Attributes.Add("Style", "display:none");
                PytMode.Attributes.Add("Style", "display:none"); divECommerce.Attributes.Add("Style", "display:none");
                DlryType.Attributes.Add("Style", "display:none"); dvLocation.Attributes.Add("Style", "display:none");
                divOrdno.Attributes.Add("style", "display:none;"); dvRoute.Attributes.Add("Style", "display:none");
                dvtolocation.Attributes.Add("Style", "display:none");
                divsite.Attributes.Add("style", "display:none;");
                divsku.Attributes.Add("style", "display:none;");
                divordcategory.Attributes.Add("style", "display:none;");
                divordtype.Attributes.Add("style", "display:none;");
                tbulkrpt.Attributes.Add("style", "display:none;");

                Category.Attributes.Add("style", "display:none;");
                cateria.Attributes.Add("style", "display:none;");
               // tblTransactionReport.Attributes.Add("style", "display:none;");
                btntransactionExQuery.Attributes.Add("style", "display:none;");
                ReturnDepartment.Attributes.Add("style", "display:none;");
                ReturnCustomer.Attributes.Add("style", "display:none;");
                ReturnDriver.Attributes.Add("style", "display:none;");
                ReturnStatus.Attributes.Add("style", "display:none;");
                tblreturncollectionreport.Attributes.Add("style", "display:none;");
            }

            else if (invoker == "skutrack")
            {
                tblEngine.Attributes.Add("style", "display:none;");
                tblIssue.Attributes.Add("style", "display:none;");
                tblReceipt.Attributes.Add("style", "display:none;");
                PrdCategory.Attributes.Add("style", "display:none;");
                ExcludeZero.Attributes.Add("style", "display:none;");
                frmSite.Attributes.Add("style", "display:none;");
                toSite.Attributes.Add("style", "display:none;");

                Groupset1.Attributes.Add("style", "display:none;");
                Image.Attributes.Add("style", "display:none;");
                SiteList.Attributes.Add("style", "display:none;");

                FDate.Attributes.Add("style", "display:table;");
                TDate.Attributes.Add("style", "display:table;");
                Company.Attributes.Add("style", "display:none;");
                Department.Attributes.Add("style", "display:none;");
                Status.Attributes.Add("style", "display:none;");
                User.Attributes.Add("style", "display:none;");
                tblProduct.Attributes.Add("style", "display:none;");
                tblRequest.Attributes.Add("style", "display:table;");
                tblUserInfo.Attributes.Add("style", "display:none;");

                Role.Attributes.Add("style", "display:none;");
                Active.Attributes.Add("style", "display:none;");
                UpToDate.Attributes.Add("Style", "display:none;");
                ZeroBalance.Attributes.Add("style", "display:none;");
                ImgStatus.Attributes.Add("style", "display:none;");
                Driver.Attributes.Add("Style", "display:none"); dvVehicle.Attributes.Add("Style", "display:none");
                PytMode.Attributes.Add("Style", "display:none"); divECommerce.Attributes.Add("Style", "display:none");
                DlryType.Attributes.Add("Style", "display:none"); dvLocation.Attributes.Add("Style", "display:none");
                divOrdno.Attributes.Add("style", "display:none;"); dvRoute.Attributes.Add("Style", "display:none");
                dvtolocation.Attributes.Add("Style", "display:none");
                divsite.Attributes.Add("style", "display:none;");
                divsku.Attributes.Add("style", "display:table;");
                divordcategory.Attributes.Add("style", "display:none;");
                divordtype.Attributes.Add("style", "display:none;");
                tbulkrpt.Attributes.Add("style", "display:none;");


                Category.Attributes.Add("style", "display:none;");
                cateria.Attributes.Add("style", "display:none;");
               // tblTransactionReport.Attributes.Add("style", "display:none;");
                btntransactionExQuery.Attributes.Add("style", "display:none;");
                ReturnDepartment.Attributes.Add("style", "display:none;");
                ReturnCustomer.Attributes.Add("style", "display:none;");
                ReturnDriver.Attributes.Add("style", "display:none;");
                ReturnStatus.Attributes.Add("style", "display:none;");
                tblreturncollectionreport.Attributes.Add("style", "display:none;");
            }
            else if (invoker == "sitetrack")
            {
                tblEngine.Attributes.Add("style", "display:none;");
                tblIssue.Attributes.Add("style", "display:none;");
                tblReceipt.Attributes.Add("style", "display:none;");
                PrdCategory.Attributes.Add("style", "display:none;");
                ExcludeZero.Attributes.Add("style", "display:none;");
                frmSite.Attributes.Add("style", "display:none;");
                toSite.Attributes.Add("style", "display:none;");

                Groupset1.Attributes.Add("style", "display:none;");
                Image.Attributes.Add("style", "display:none;");
                SiteList.Attributes.Add("style", "display:none;");

                FDate.Attributes.Add("style", "display:table;");
                TDate.Attributes.Add("style", "display:table;");
                Company.Attributes.Add("style", "display:none;");
                Department.Attributes.Add("style", "display:none;");
                Status.Attributes.Add("style", "display:none;");
                User.Attributes.Add("style", "display:none;");
                tblProduct.Attributes.Add("style", "display:none;");
                tblRequest.Attributes.Add("style", "display:table;");
                tblUserInfo.Attributes.Add("style", "display:none;");

                Role.Attributes.Add("style", "display:none;");
                Active.Attributes.Add("style", "display:none;");
                UpToDate.Attributes.Add("Style", "display:none;");
                ZeroBalance.Attributes.Add("style", "display:none;");
                ImgStatus.Attributes.Add("style", "display:none;");
                Driver.Attributes.Add("Style", "display:none"); dvVehicle.Attributes.Add("Style", "display:none");
                PytMode.Attributes.Add("Style", "display:none"); divECommerce.Attributes.Add("Style", "display:none");
                DlryType.Attributes.Add("Style", "display:none"); dvLocation.Attributes.Add("Style", "display:none");
                divOrdno.Attributes.Add("style", "display:none;"); dvRoute.Attributes.Add("Style", "display:none");
                dvtolocation.Attributes.Add("Style", "display:none");
                divsite.Attributes.Add("style", "display:table;");
                divsku.Attributes.Add("style", "display:none;");
                divordcategory.Attributes.Add("style", "display:none;");
                divordtype.Attributes.Add("style", "display:none;");
                tbulkrpt.Attributes.Add("style", "display:none;");

                Category.Attributes.Add("style", "display:none;");
                cateria.Attributes.Add("style", "display:none;");
               // tblTransactionReport.Attributes.Add("style", "display:none;");
                btntransactionExQuery.Attributes.Add("style", "display:none;");
                ReturnDepartment.Attributes.Add("style", "display:none;");
                ReturnCustomer.Attributes.Add("style", "display:none;");
                ReturnDriver.Attributes.Add("style", "display:none;");
                ReturnStatus.Attributes.Add("style", "display:none;");
                tblreturncollectionreport.Attributes.Add("style", "display:none;");
            }
            else if (invoker == "depttrack")
            {
                tblEngine.Attributes.Add("style", "display:none;");
                tblIssue.Attributes.Add("style", "display:none;");
                tblReceipt.Attributes.Add("style", "display:none;");
                PrdCategory.Attributes.Add("style", "display:none;");
                ExcludeZero.Attributes.Add("style", "display:none;");
                frmSite.Attributes.Add("style", "display:none;");
                toSite.Attributes.Add("style", "display:none;");

                Groupset1.Attributes.Add("style", "display:none;");
                Image.Attributes.Add("style", "display:none;");
                SiteList.Attributes.Add("style", "display:none;");

                FDate.Attributes.Add("style", "display:table;");
                TDate.Attributes.Add("style", "display:table;");
                Company.Attributes.Add("style", "display:none;");
                Department.Attributes.Add("style", "display:table;");
                Status.Attributes.Add("style", "display:none;");
                User.Attributes.Add("style", "display:none;");
                tblProduct.Attributes.Add("style", "display:none;");
                tblRequest.Attributes.Add("style", "display:table;");
                tblUserInfo.Attributes.Add("style", "display:none;");

                Role.Attributes.Add("style", "display:none;");
                Active.Attributes.Add("style", "display:none;");
                UpToDate.Attributes.Add("Style", "display:none;");
                ZeroBalance.Attributes.Add("style", "display:none;");
                ImgStatus.Attributes.Add("style", "display:none;");
                Driver.Attributes.Add("Style", "display:none"); dvVehicle.Attributes.Add("Style", "display:none");
                PytMode.Attributes.Add("Style", "display:none"); divECommerce.Attributes.Add("Style", "display:none");
                DlryType.Attributes.Add("Style", "display:none"); dvLocation.Attributes.Add("Style", "display:none");
                divOrdno.Attributes.Add("style", "display:none;"); dvRoute.Attributes.Add("Style", "display:none");
                dvtolocation.Attributes.Add("Style", "display:none");
                divsite.Attributes.Add("style", "display:none;");
                divsku.Attributes.Add("style", "display:none;");
                divordcategory.Attributes.Add("style", "display:none;");
                divordtype.Attributes.Add("style", "display:none;");

                Category.Attributes.Add("style", "display:none;");
                cateria.Attributes.Add("style", "display:none;");
               // tblTransactionReport.Attributes.Add("style", "display:none;");
                btntransactionExQuery.Attributes.Add("style", "display:none;");
                ReturnDepartment.Attributes.Add("style", "display:none;");
                ReturnCustomer.Attributes.Add("style", "display:none;");
                ReturnDriver.Attributes.Add("style", "display:none;");
                ReturnStatus.Attributes.Add("style", "display:none;");
                tblreturncollectionreport.Attributes.Add("style", "display:none;");
            }
            else if (invoker == "vat")
            {
                tblEngine.Attributes.Add("style", "display:none;");
                tblIssue.Attributes.Add("style", "display:none;");
                tblReceipt.Attributes.Add("style", "display:none;");
                PrdCategory.Attributes.Add("style", "display:none;");
                ExcludeZero.Attributes.Add("style", "display:none;");
                frmSite.Attributes.Add("style", "display:none;");
                toSite.Attributes.Add("style", "display:none;");

                Groupset1.Attributes.Add("style", "display:none;");
                Image.Attributes.Add("style", "display:none;");
                SiteList.Attributes.Add("style", "display:none;");

                FDate.Attributes.Add("style", "display:table;");
                TDate.Attributes.Add("style", "display:table;");
                Company.Attributes.Add("style", "display:table;");
                Department.Attributes.Add("style", "display:table;");
                Status.Attributes.Add("style", "display:table;");
                User.Attributes.Add("style", "display:table;");
                tblProduct.Attributes.Add("style", "display:none;");
                tblRequest.Attributes.Add("style", "display:table;");
                tblUserInfo.Attributes.Add("style", "display:none;");
                UpToDate.Attributes.Add("Style", "display:none;");
                Role.Attributes.Add("style", "display:none;");
                Active.Attributes.Add("style", "display:none;");

                ZeroBalance.Attributes.Add("style", "display:none;");
                ImgStatus.Attributes.Add("style", "display:none;");
                Driver.Attributes.Add("Style", "display:none"); dvVehicle.Attributes.Add("Style", "display:none");
                PytMode.Attributes.Add("Style", "display:none"); divECommerce.Attributes.Add("Style", "display:none");
                DlryType.Attributes.Add("Style", "display:none"); dvLocation.Attributes.Add("Style", "display:none");
                divOrdno.Attributes.Add("style", "display:none;"); dvRoute.Attributes.Add("Style", "display:none");
                dvtolocation.Attributes.Add("Style", "display:none");
                divsite.Attributes.Add("style", "display:none;");
                divsku.Attributes.Add("style", "display:none;");
                divordcategory.Attributes.Add("style", "display:none;");
                divordtype.Attributes.Add("style", "display:none;");
                tbulkrpt.Attributes.Add("style", "display:none;");
                Category.Attributes.Add("style", "display:none;");
                cateria.Attributes.Add("style", "display:none;");
              //  tblTransactionReport.Attributes.Add("style", "display:none;");
                btntransactionExQuery.Attributes.Add("style", "display:none;");
                ReturnDepartment.Attributes.Add("style", "display:none;");
                ReturnCustomer.Attributes.Add("style", "display:none;");
                ReturnDriver.Attributes.Add("style", "display:none;");
                ReturnStatus.Attributes.Add("style", "display:none;");
                tblreturncollectionreport.Attributes.Add("style", "display:none;");
            }
            //change by suraj khopade
            else if (invoker == "Transaction")
            {
                tblEngine.Attributes.Add("style", "display:none;");                tblIssue.Attributes.Add("style", "display:none;");                tblRequest.Attributes.Add("style", "display:none;");                tblReceipt.Attributes.Add("style", "display:none;");                PrdCategory.Attributes.Add("style", "display:none;");                ExcludeZero.Attributes.Add("style", "display:none;");                frmSite.Attributes.Add("style", "display:none;");                toSite.Attributes.Add("style", "display:none;");                Groupset1.Attributes.Add("style", "display:none;");                Image.Attributes.Add("style", "display:none;");                SiteList.Attributes.Add("style", "display:none;");                FDate.Attributes.Add("style", "display:none;");                TDate.Attributes.Add("style", "display:none;");                Company.Attributes.Add("style", "display:none;");                Department.Attributes.Add("style", "display:none;");                Category.Attributes.Add("style", "display:table;");                cateria.Attributes.Add("style", "display:table;");               // tblTransactionReport.Attributes.Add("style", "display:table;");                btnexQuery.Attributes.Add("Style", "display:none");                btntransactionExQuery.Attributes.Add("style", "display:table;");                Status.Attributes.Add("style", "display:none;");                User.Attributes.Add("style", "display:none;");                tblProduct.Attributes.Add("style", "display:none;");                tblUserInfo.Attributes.Add("style", "display:none;");                Role.Attributes.Add("style", "display:none;");                Active.Attributes.Add("style", "display:none;");                UpToDate.Attributes.Add("Style", "display:none;");                ZeroBalance.Attributes.Add("style", "display:none;");                ImgStatus.Attributes.Add("style", "display:none;");                Driver.Attributes.Add("Style", "display:none"); dvVehicle.Attributes.Add("Style", "display:none");                PytMode.Attributes.Add("Style", "display:none"); divECommerce.Attributes.Add("Style", "display:none");                DlryType.Attributes.Add("Style", "display:none"); dvLocation.Attributes.Add("Style", "display:none");                divOrdno.Attributes.Add("style", "display:none;"); dvRoute.Attributes.Add("Style", "display:none");                dvtolocation.Attributes.Add("Style", "display:none");                divsite.Attributes.Add("style", "display:none;");                divsku.Attributes.Add("style", "display:none;");                divordcategory.Attributes.Add("style", "display:none;");                divordtype.Attributes.Add("style", "display:none;");                tbulkrpt.Attributes.Add("style", "display:none;");

                ReturnDepartment.Attributes.Add("style", "display:none;");
                ReturnCustomer.Attributes.Add("style", "display:none;");
                ReturnDriver.Attributes.Add("style", "display:none;");
                ReturnStatus.Attributes.Add("style", "display:none;");
                tblreturncollectionreport.Attributes.Add("style", "display:none;");

            }
            else if (invoker == "normalorderdeliverynote")
            {
                tblEngine.Attributes.Add("style", "display:none;");
                tblIssue.Attributes.Add("style", "display:none;");
                tblReceipt.Attributes.Add("style", "display:none;");
                PrdCategory.Attributes.Add("style", "display:none;");
                ExcludeZero.Attributes.Add("style", "display:none;");
                frmSite.Attributes.Add("style", "display:none;");
                toSite.Attributes.Add("style", "display:none;");

                Groupset1.Attributes.Add("style", "display:none;");
                Image.Attributes.Add("style", "display:none;");
                SiteList.Attributes.Add("style", "display:none;");
                UpToDate.Attributes.Add("Style", "display:none;");
                FDate.Attributes.Add("style", "display:none;");
                TDate.Attributes.Add("style", "display:none;");
                Company.Attributes.Add("style", "display:none;");
                Department.Attributes.Add("style", "display:none;");
                Status.Attributes.Add("style", "display:none;");
                User.Attributes.Add("style", "display:none;");
                tblProduct.Attributes.Add("style", "display:none;");
                tblRequest.Attributes.Add("style", "display:table;");
                tblUserInfo.Attributes.Add("style", "display:none;");

                Role.Attributes.Add("style", "display:none;");
                Active.Attributes.Add("style", "display:none;");

                ZeroBalance.Attributes.Add("style", "display:none;");
                ImgStatus.Attributes.Add("style", "display:none;");
                Driver.Attributes.Add("Style", "display:none");
                PytMode.Attributes.Add("Style", "display:none");
                DlryType.Attributes.Add("Style", "display:none");
                dvLocation.Attributes.Add("Style", "display:none");
                divECommerce.Attributes.Add("Style", "display:none");
                divOrdno.Attributes.Add("style", "display:table;");
                dvRoute.Attributes.Add("Style", "display:none"); dvVehicle.Attributes.Add("Style", "display:none");
                dvtolocation.Attributes.Add("Style", "display:none");
                divsite.Attributes.Add("style", "display:none;");
                divsku.Attributes.Add("style", "display:none;");
                divordcategory.Attributes.Add("style", "display:none;");
                divordtype.Attributes.Add("style", "display:none;");
                tbulkrpt.Attributes.Add("style", "display:none;");

                Category.Attributes.Add("style", "display:none;");
                cateria.Attributes.Add("style", "display:none;");
                //tblTransactionReport.Attributes.Add("style", "display:none;");
                btntransactionExQuery.Attributes.Add("style", "display:none;");

                ReturnDepartment.Attributes.Add("style", "display:none;");
                ReturnCustomer.Attributes.Add("style", "display:none;");
                ReturnDriver.Attributes.Add("style", "display:none;");
                ReturnStatus.Attributes.Add("style", "display:none;");
                tblreturncollectionreport.Attributes.Add("style", "display:none;");
            }
            else if (invoker == "ReturnCollectionReport")
            {
                tblEngine.Attributes.Add("style", "display:none;");
                tblIssue.Attributes.Add("style", "display:none;");
                tblReceipt.Attributes.Add("style", "display:none;");
                PrdCategory.Attributes.Add("style", "display:none;");
                ExcludeZero.Attributes.Add("style", "display:none;");
                frmSite.Attributes.Add("style", "display:none;");
                toSite.Attributes.Add("style", "display:none;");

                Groupset1.Attributes.Add("style", "display:none;");
                Image.Attributes.Add("style", "display:none;");
                SiteList.Attributes.Add("style", "display:none;");

                FDate.Attributes.Add("style", "display:table;");
                TDate.Attributes.Add("style", "display:table;");
                Company.Attributes.Add("style", "display:none;");
                Department.Attributes.Add("style", "display:none;");
                Status.Attributes.Add("style", "display:none;");
                User.Attributes.Add("style", "display:none;");
                tblProduct.Attributes.Add("style", "display:none;");
                tblRequest.Attributes.Add("style", "display:none;");
                tblUserInfo.Attributes.Add("style", "display:none;");
                UpToDate.Attributes.Add("Style", "display:none;");
                Role.Attributes.Add("style", "display:none;");
                Active.Attributes.Add("style", "display:none;");

                ZeroBalance.Attributes.Add("style", "display:none;");
                ImgStatus.Attributes.Add("style", "display:none;");
                Driver.Attributes.Add("Style", "display:none"); dvVehicle.Attributes.Add("Style", "display:none");
                PytMode.Attributes.Add("Style", "display:none"); divECommerce.Attributes.Add("Style", "display:none");
                DlryType.Attributes.Add("Style", "display:none"); dvLocation.Attributes.Add("Style", "display:none");
                divOrdno.Attributes.Add("style", "display:none;"); dvRoute.Attributes.Add("Style", "display:none");
                dvtolocation.Attributes.Add("Style", "display:none");
                divsite.Attributes.Add("style", "display:none;");
                divsku.Attributes.Add("style", "display:none;");
                divordcategory.Attributes.Add("style", "display:none;");
                divordtype.Attributes.Add("style", "display:none;");
                tbulkrpt.Attributes.Add("style", "display:none;");

                Category.Attributes.Add("style", "display:none;");
                cateria.Attributes.Add("style", "display:none;");
                // tblTransactionReport.Attributes.Add("style", "display:none;");
                btntransactionExQuery.Attributes.Add("style", "display:none;");

                ReturnDepartment.Attributes.Add("style", "display:table;");
                ReturnCustomer.Attributes.Add("style", "display:table;");
                ReturnDriver.Attributes.Add("style", "display:table;");
                ReturnStatus.Attributes.Add("style", "display:table;");
                tblreturncollectionreport.Attributes.Add("style", "display:table;");
            }
            else if (invoker == "ReceiptSummaryReport")
            {
                tblEngine.Attributes.Add("style", "display:none;");
                tblIssue.Attributes.Add("style", "display:none;");
                tblReceipt.Attributes.Add("style", "display:none;");
                PrdCategory.Attributes.Add("style", "display:none;");
                ExcludeZero.Attributes.Add("style", "display:none;");
                frmSite.Attributes.Add("style", "display:none;");
                toSite.Attributes.Add("style", "display:none;");

                Groupset1.Attributes.Add("style", "display:none;");
                Image.Attributes.Add("style", "display:none;");
                SiteList.Attributes.Add("style", "display:none;");

                FDate.Attributes.Add("style", "display:table;");
                TDate.Attributes.Add("style", "display:table;");
                Company.Attributes.Add("style", "display:none;");
                Department.Attributes.Add("style", "display:none;");
                Status.Attributes.Add("style", "display:none;");
                User.Attributes.Add("style", "display:none;");
                tblProduct.Attributes.Add("style", "display:none;");
                tblRequest.Attributes.Add("style", "display:none;");
                tblUserInfo.Attributes.Add("style", "display:none;");
                UpToDate.Attributes.Add("Style", "display:none;");
                Role.Attributes.Add("style", "display:none;");
                Active.Attributes.Add("style", "display:none;");

                ZeroBalance.Attributes.Add("style", "display:none;");
                ImgStatus.Attributes.Add("style", "display:none;");
                Driver.Attributes.Add("Style", "display:none"); dvVehicle.Attributes.Add("Style", "display:none");
                PytMode.Attributes.Add("Style", "display:none"); divECommerce.Attributes.Add("Style", "display:none");
                DlryType.Attributes.Add("Style", "display:none"); dvLocation.Attributes.Add("Style", "display:none");
                divOrdno.Attributes.Add("style", "display:none;"); dvRoute.Attributes.Add("Style", "display:none");
                dvtolocation.Attributes.Add("Style", "display:none");
                divsite.Attributes.Add("style", "display:none;");
                divsku.Attributes.Add("style", "display:none;");
                divordcategory.Attributes.Add("style", "display:none;");
                divordtype.Attributes.Add("style", "display:none;");
                tbulkrpt.Attributes.Add("style", "display:none;");

                Category.Attributes.Add("style", "display:none;");
                cateria.Attributes.Add("style", "display:none;");
                // tblTransactionReport.Attributes.Add("style", "display:none;");
                btntransactionExQuery.Attributes.Add("style", "display:none;");

                ReturnDepartment.Attributes.Add("style", "display:table;");
                ReturnCustomer.Attributes.Add("style", "display:table;");
                ReturnDriver.Attributes.Add("style", "display:none;");
                ReturnStatus.Attributes.Add("style", "display:table;");
                tblreturncollectionreport.Attributes.Add("style", "display:table;");
            }
            //change by suraj khopade
        }

        public void GridVisible()
        {
            tblRequest.Attributes.Add("style", "display:none;");
            tblEngine.Attributes.Add("style", "display:none;");
            tblProduct.Attributes.Add("style", "display:none;");
            tblIssue.Attributes.Add("style", "display:none;");
            tblReceipt.Attributes.Add("style", "display:none");
            //change by suraj khopade
            //tblTransactionReport.Attributes.Add("style", "display:none;");
            //change by suraj khopade
        }

        private void loadstring()
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(Session["Lang"].ToString());
                rm = new ResourceManager("PowerOnRentwebapp.App_GlobalResources.Lang", Assembly.GetExecutingAssembly());
                ci = Thread.CurrentThread.CurrentCulture;

                lblFromDate.Text = rm.GetString("FromDate", ci);
                lblToDate.Text = rm.GetString("ToDate", ci);
                lblcompany.Text = rm.GetString("company", ci);
                lbldept.Text = rm.GetString("Department", ci);
                lblGroupSet.Text = rm.GetString("BOM", ci);
                lblimage.Text = rm.GetString("Image", ci);
                lblStatus.Text = rm.GetString("Status", ci);
                lblUser.Text = rm.GetString("User", ci);
                lblRole.Text = rm.GetString("Role", ci);
                lblActive.Text = rm.GetString("Active", ci);

                lblorderlist.Text = rm.GetString("OrderList", ci);
                lblselectallorder.Text = rm.GetString("SelectAllOrder", ci);
                lblissuelist.Text = rm.GetString("IssueList", ci);
                lblallissue.Text = rm.GetString("SelectAllIssue", ci);

                lblskulist.Text = rm.GetString("SKUList", ci);
                lblallsku.Text = rm.GetString("SelectAllSKU", ci);

                lbluserlist.Text = rm.GetString("UserList", ci);
                lblalluser.Text = rm.GetString("SelectAllUser", ci);

                btnexQuery.Value = rm.GetString("executequery", ci);
                lblWithZeroBalance.Text = rm.GetString("withzerobalance", ci);

                lblDriver.Text = rm.GetString("Driver", ci);
                lblPytMode.Text = rm.GetString("PaymentMode", ci);

                lbldeliverytype.Text = rm.GetString("DeliveryType", ci);
                lbltype.Text = rm.GetString("Type", ci);

                //change by suraj khopade
               // lbltransactionreport.Text = rm.GetString("TransactionReportList", ci);
                //change by suraj khopade

            }
            catch (System.Exception ex)
            {

            }
            finally { }

        }



        public void FillDriver()
        {
            iUCCommonFilterClient UCCommonFilter = new iUCCommonFilterClient();
            CustomProfile profile = CustomProfile.GetProfile();
            string invkr = Request.QueryString["invoker"].ToString();
            if (invkr == "avgtime" || invkr == "noofdelivery" || invkr == "driverschedule")
            {
                if (profile.Personal.UserType == "Super Admin")
                {
                   // ddlDriver.DataSource = UCCommonFilter.GetAllDriverListGPS(profile.DBConnection._constr);
                }
                else
                {
                    //ddlDriver.DataSource = UCCommonFilter.GetAllDeptDriverListGPS(profile.Personal.DepartmentID, profile.DBConnection._constr);
                }
            }
            else
            {
                ddlDriver.DataSource = UCCommonFilter.GetAllDriverList(profile.DBConnection._constr);

            }
            ddlDriver.DataBind();
            ListItem lst = new ListItem();
            lst.Text = "-Select All-";
            lst.Value = "0";
            ddlDriver.Items.Insert(0, lst);

        }

        public void fillRoute()
        {
            iUCCommonFilterClient UCCommonFilter = new iUCCommonFilterClient();
            CustomProfile profile = CustomProfile.GetProfile();
            var frmdt = FrmDate.Date.ToString();
            var todt = To_Date.Date.ToString();

            //ddlRoute.DataSource = UCCommonFilter.GetAllRouteList(profile.DBConnection._constr);
            //ddlRoute.DataBind();
            //ListItem lst = new ListItem();
            //lst.Text = "-Select All-";
            //lst.Value = "0";
            //ddlRoute.Items.Insert(0, lst);
        }

        public void fillPaymentMethod(long selectedDept)
        {
            iPartRequestClient objService = new iPartRequestClient();
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                DataSet ds = new DataSet();
                ds = objService.GetDeptWisePaymentMethod(selectedDept, profile.DBConnection._constr);
                ddlPytMode.DataSource = ds;
                ddlPytMode.DataBind();
                ListItem lstpm = new ListItem { Text = "-Select All-", Value = "0" };
                ddlPytMode.Items.Insert(0, lstpm);
            }
            catch { }
            finally { objService.Close(); }
        }

        [WebMethod]
        public static string WMShowDate(string hdnUserSelectedRec)
        {
            string result = "";



            return result;
        }



        public void fillReturnCustomer()
        {
            iUserCreationClient usercreation = new iUserCreationClient();
            CustomProfile profile = CustomProfile.GetProfile();
            ddlReturncustomer.Enabled = true;
            ddlReturndepartment.Enabled = true;
            long UID = profile.Personal.UserID;
            string UsrType = profile.Personal.UserType.ToString();
            if (UsrType == "Super Admin")
            {
                ddlReturncustomer.DataSource = usercreation.GetReturnCustomerName(UID, profile.DBConnection._constr);
            }
            else
            {
                ddlReturncustomer.DataSource = usercreation.GetReturnCustomerName(UID, profile.DBConnection._constr);
            }
            ddlReturncustomer.DataBind();
            ListItem lst = new ListItem();
            lst.Text = "-Select All-";
            lst.Value = "0";
            ddlReturncustomer.Items.Insert(0, lst);
            ddlReturncustomer.SelectedIndex = 1;
            hdnSelectedReturnCustomer.Value = ddlReturncustomer.SelectedValue.ToString();

            ddlReturndepartment.DataSource = usercreation.GetReturnDepartmentList(Convert.ToInt16(hdnSelectedReturnCustomer.Value), profile.DBConnection._constr);

            //if (UsrType == "Super Admin")
            //{
            //    ddlReturndepartment.DataSource = usercreation.GetReturnDepartmentList(Convert.ToInt16(hdnSelectedReturnCustomer.Value), profile.DBConnection._constr);
            //}
            //else
            //{
            //    //ddlReturndepartment.DataSource = usercreation.GetAddedDepartmentList(Convert.ToInt16(hdnSelectedCompany.Value), Convert.ToInt16(UID), profile.DBConnection._constr).ToList();
            //}
            //ddlReturndepartment.DataSource = ddlReturndepartment;
            ddlReturndepartment.DataBind();
            ListItem lst1 = new ListItem();
            lst1.Text = "--Select--";
            lst1.Value = "0";
            ddlReturndepartment.Items.Insert(0, lst1);
            ddlReturndepartment.SelectedIndex = 0;
            hdnSelectedReturnDepartment.Value = ddlReturndepartment.SelectedValue.ToString();
        }


        public void fillReturnStatus()
        {
            iUserCreationClient usercreation = new iUserCreationClient();
            CustomProfile profile = CustomProfile.GetProfile();
            ddlReturnStatus.Enabled = true;
           // ddlReturndepartment.Enabled = true;
            long UID = profile.Personal.UserID;
            string UsrType = profile.Personal.UserType.ToString();
            if (UsrType == "Super Admin")
            {
                ddlReturnStatus.DataSource = usercreation.GetReturnStatus(profile.DBConnection._constr);
            }
            else
            {
                ddlReturnStatus.DataSource = usercreation.GetReturnStatus(profile.DBConnection._constr);
            }
            ddlReturnStatus.DataBind();
            ListItem lst = new ListItem();
            lst.Text = "-Select All-";
            lst.Value = "0";
            ddlReturnStatus.Items.Insert(0, lst);
            ddlReturnStatus.SelectedIndex = 1;
            hdnSelectedReturnStatus.Value = ddlReturnStatus.SelectedValue.ToString();
            
        }
        
        public void fillReturnDriver()
        {
            iUserCreationClient usercreation = new iUserCreationClient();
            CustomProfile profile = CustomProfile.GetProfile();
            ddlReturnDriver.Enabled = true;
            // ddlReturndepartment.Enabled = true;
            long UID = profile.Personal.UserID;
            string UsrType = profile.Personal.UserType.ToString();
            if (UsrType == "Super Admin")
            {
                ddlReturnDriver.DataSource = usercreation.GetReturnDriver(profile.DBConnection._constr);
            }
            else
            {
                ddlReturnDriver.DataSource = usercreation.GetReturnDriver(profile.DBConnection._constr);
            }
            ddlReturnDriver.DataBind();
            ListItem lst = new ListItem();
            lst.Text = "-Select All-";
            lst.Value = "0";
            ddlReturnDriver.Items.Insert(0, lst);
            ddlReturnDriver.SelectedIndex = 1;
            hdnSelectedReturnDriver.Value = ddlReturnDriver.SelectedValue.ToString();
            //hdnSelectedReturnCustomer.Value = ddlReturncustomer.SelectedValue.ToString();

        }
    }
}