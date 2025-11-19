using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PowerOnRentwebapp.PORServiceUCCommonFilter;
using PowerOnRentwebapp.PORServicePartRequest;
using PowerOnRentwebapp.PORServiceEngineMaster;
using PowerOnRentwebapp.Login;
using System.Web.Services;
using PowerOnRentwebapp.ProductMasterService;
using System.Data;
using System.Configuration;
using System.Resources;
using System.Globalization;
using System.Threading;
using System.Reflection;
using PowerOnRentwebapp.DocumentService;
using System.Xml.Linq;
using System.IO;
//using PowerOnRentwebapp.AvailableQtyService;
using System.Collections;
using System.Net.Mail;
using System.Net;
using PowerOnRentwebapp.AvailableQtyService;
using System.Dynamic;
using RestSharp.Authenticators;
using RestSharp;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;

namespace PowerOnRentwebapp.PowerOnRent
{
    public partial class PartRequestEntry : System.Web.UI.Page
    {
        ResourceManager rm;
        CultureInfo ci;

        static string ObjectName = "RequestPartDetail";
        static string ObjectNameSerial = "RequestSerialNumber";
        string SelTemplateID = "";
        static long UOMID = 0;
        #region Page Events
        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            Session.Add("RequestorID", "");
            if (Session["TemplateID"] != null)
            {
                hdnSelTemplateID.Value = Session["TemplateID"].ToString();
            }

            if (!IsPostBack)
            {
                FillSites(); UC_AttachmentDocument1.ClearDocument("RequestPartDetail");
                if (Session["PORRequestID"] != null)
                {
                    if (Session["PORRequestID"].ToString() != "0")
                    {
                        hdncustanalya.Value = Session["PORRequestID"].ToString();//for open customer analytics window
                        lblApprovalDate.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm tt");
                        GetRequestHead();
                        gvApprovalRemarkBind(Convert.ToInt64(HttpContext.Current.Session["PORRequestID"].ToString()));
                        GVInboxPOR_OnRebind(sender, e);
                        BindGVOrderParameter(Convert.ToInt64(HttpContext.Current.Session["PORRequestID"].ToString()));
                        BindGVEcomInstallmentDetails(Convert.ToInt64(HttpContext.Current.Session["PORRequestID"].ToString()));
                    }
                    else if (Session["TemplateID"] != null)
                    {
                        hdncustanalya.Value = "0";
                        GetTemplateDetails(hdnSelTemplateID.Value);
                        UC_ExpDeliveryDate.startdate(DateTime.Now);
                        string mdd = hdnMaxDeliveryDays.Value;
                        if (mdd == "") { }
                        else
                        {
                            UC_ExpDeliveryDate.enddate(DateTime.Now.AddDays(int.Parse(mdd)));
                        }
                        ddlStatus.DataSource = WMFillStatus();
                        ddlStatus.DataBind();
                    }
                    else
                    {
                        hdncustanalya.Value = "0";
                        WMpageAddNew();
                        UC_ExpDeliveryDate.startdate(DateTime.Now);
                        ddlStatus.DataSource = WMFillStatus();
                        ddlStatus.DataBind();
                    }

                }

                divVisibility();

            }
            UC_DateRequestDate.DateIsRequired(true, "", "");

            //add for btncustomeranalys show or not
            //  Showbtncustomeranalys(Session["PORRequestID"].ToString());

            // show serial icon in request grid
            iPartRequestClient objService = new iPartRequestClient();
            CustomProfile profile = CustomProfile.GetProfile();
            string ChkSerialedCompany = "";
            if (hdnselectedCompany.Value == "" || hdnselectedCompany.Value == null) { hdnselectedCompany.Value = "0"; }
            ChkSerialedCompany = objService.ChkSerialedCompany(Convert.ToInt64(hdnselectedCompany.Value), profile.DBConnection._constr);
            if (profile.Personal.UserType == "Super Admin")
            {
                Grid1.Columns[0].Visible = false; Grid1.Columns[6].Visible = false; Grid1.Columns[11].Visible = false;
                Grid1.Columns[18].Visible = false; Grid1.Columns[21].Visible = false;
                Grid1.Columns[1].Visible = true; Grid1.Columns[2].Visible = true; Grid1.Columns[3].Visible = true;
                Grid1.Columns[4].Visible = true; Grid1.Columns[5].Visible = true; Grid1.Columns[7].Visible = true;
                Grid1.Columns[8].Visible = true; Grid1.Columns[9].Visible = true; Grid1.Columns[10].Visible = true;
                Grid1.Columns[12].Visible = true; Grid1.Columns[13].Visible = true;
                if (hdnordertype.Value == "E-Commerce Order")
                {

                    //Grid1.Columns[14].Visible = true;
                    //Grid1.Columns[15].Visible = true;
                    //Grid1.Columns[14].Width = "5%";
                    //Grid1.Columns[15].Width = "5%";
                    //Grid1.Columns[17].Visible = true;
                    //Grid1.Columns[17].Width = "5%";

                    //tdvatamt.Attributes.Add("style", "display:'none'");
                    //tdvatexclamt.Attributes.Add("style", "display:'none'");
                }
                
                Grid1.Columns[16].Visible = true;
                Grid1.Columns[19].Visible = true;
                Grid1.Columns[20].Visible = true;
                Grid1.Columns[19].Width = "5%";
                Grid1.Columns[20].Width = "5%";

              
            }
            else
            {
                if (ChkSerialedCompany == "Yes")
                {
                    Grid1.Columns[0].Visible = false; Grid1.Columns[6].Visible = false; Grid1.Columns[11].Visible = false;
                    Grid1.Columns[18].Visible = false; Grid1.Columns[21].Visible = false;
                    Grid1.Columns[1].Visible = true; Grid1.Columns[2].Visible = true; Grid1.Columns[3].Visible = true;
                    Grid1.Columns[4].Visible = true; Grid1.Columns[5].Visible = true; Grid1.Columns[7].Visible = true;
                    Grid1.Columns[8].Visible = true; Grid1.Columns[9].Visible = true; Grid1.Columns[10].Visible = true;
                    Grid1.Columns[12].Visible = true; Grid1.Columns[13].Visible = true;
                    if (hdnordertype.Value == "E-Commerce Order")
                    {

                        Grid1.Columns[14].Visible = true;
                        Grid1.Columns[15].Visible = true;
                        Grid1.Columns[14].Width = "5%";
                        Grid1.Columns[15].Width = "5%";
                        Grid1.Columns[17].Visible = true;
                        Grid1.Columns[17].Width = "5%";

                        tdvatamt.Attributes.Add("style", "display:'none'");
                        tdvatexclamt.Attributes.Add("style", "display:'none'");
                    }
                    
                    Grid1.Columns[16].Visible = true;
                    Grid1.Columns[19].Visible = true;
                    Grid1.Columns[20].Visible = true;
                    Grid1.Columns[19].Width = "5%";
                    Grid1.Columns[20].Width = "5%";

                    

                }
                else
                {
                    Grid1.Columns[0].Visible = false; Grid1.Columns[6].Visible = false; Grid1.Columns[11].Visible = false;
                    Grid1.Columns[18].Visible = false; Grid1.Columns[21].Visible = false;

                    Grid1.Columns[1].Visible = true; Grid1.Columns[2].Visible = true; Grid1.Columns[3].Visible = true;
                    Grid1.Columns[4].Visible = true; Grid1.Columns[5].Visible = true; Grid1.Columns[7].Visible = true;
                    Grid1.Columns[8].Visible = true; Grid1.Columns[9].Visible = true; Grid1.Columns[10].Visible = true;
                    Grid1.Columns[12].Visible = true; Grid1.Columns[13].Visible = true;
                    if (hdnordertype.Value == "E-Commerce Order")
                    {

                        Grid1.Columns[14].Visible = true;
                        Grid1.Columns[15].Visible = true;
                        Grid1.Columns[14].Width = "5%";
                        Grid1.Columns[15].Width = "5%";
                        Grid1.Columns[17].Visible = true;
                        Grid1.Columns[17].Width = "5%";

                        tdvatamt.Attributes.Add("style", "display:'none'");
                        tdvatexclamt.Attributes.Add("style", "display:'none'");
                    }
                    
                    Grid1.Columns[16].Visible = true;

                    Grid1.Columns[19].Visible = false;
                    Grid1.Columns[20].Visible = false;
                    Grid1.Columns[19].Width = "0%";
                    Grid1.Columns[20].Width = "0%";

                    


                }
            }
        }




        protected void Page_PreInit(Object sender, EventArgs e)
        {
            CustomProfile profile = CustomProfile.GetProfile(); if (profile.Personal.Theme == null || profile.Personal.Theme == string.Empty) { Page.Theme = "Blue"; } else { Page.Theme = profile.Personal.Theme; }
        }

        //Add By Suresh
        protected void Page_PreRender(object sender, EventArgs e)
        {
            //ModelPopup1.Hide();
            // ModalPopupTemplate.Hide();
        }
        //Add By Suresh

        protected void Page_Load(Object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty((string)Session["Lang"]))
            {
                Session["Lang"] = Request.UserLanguages[0];
            }
            loadstring();
            iPartRequestClient objService = new iPartRequestClient();
            CustomProfile profile = CustomProfile.GetProfile();
            if (!string.IsNullOrEmpty((string)Session["ORDType"]))
            {
                if (Session["ORDType"].ToString() == "Ecommerce")
                {
                    btncustomeranalys.Visible = true;

                }
                else
                {
                    btncustomeranalys.Visible = false;

                }
            }


            if (!IsPostBack)
            {
                try
                {
                    Toolbar1.SetUserRights("MaterialRequest", "EntryForm", "");
                    objService.ClearTempDataFromDBNEW(HttpContext.Current.Session.SessionID, profile.Personal.UserID.ToString(), ObjectName, profile.DBConnection._constr);
                    objService.ClearTempDataFromDBNEW(HttpContext.Current.Session.SessionID, profile.Personal.UserID.ToString(), ObjectNameSerial, profile.DBConnection._constr);
                    objService.DeleteSerialTemptable(profile.Personal.UserID, profile.DBConnection._constr);

                }
                catch (Exception ex)
                {

                }

            }

            // CustomProfile profile = CustomProfile.GetProfile();
            hdnUserType.Value = profile.Personal.UserType.ToString();
            hdndeliverytype.Value = objService.GetDeliveryType(Session["PORRequestID"].ToString(), profile.DBConnection._constr);

            // ConnectionOepn();


        }
        #endregion




        #region Toolbar Code
        [WebMethod]
        public static void WMpageAddNew()
        {
            iPartRequestClient objService = new iPartRequestClient();
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                HttpContext.Current.Session["PORRequestID"] = 0;
                HttpContext.Current.Session["PORstate"] = "Add";
                HttpContext.Current.Session["TemplateID"] = "0";
                // objService.ClearTempDataFromDB(HttpContext.Current.Session.SessionID, profile.Personal.UserID.ToString(), ObjectName, profile.DBConnection._constr);
                objService.ClearTempDataFromDBNEW(HttpContext.Current.Session.SessionID, profile.Personal.UserID.ToString(), ObjectName, profile.DBConnection._constr);
                objService.ClearTempDataFromDBNEW(HttpContext.Current.Session.SessionID, profile.Personal.UserID.ToString(), ObjectNameSerial, profile.DBConnection._constr);

            }
            catch { }
            finally { objService.Close(); }
        }
        #endregion

        #region Fill DropDown
        protected void FillSites()
        {
            iUCCommonFilterClient objService = new iUCCommonFilterClient();
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                // ddlStatus.Enabled = false;
                List<mCompany> CompanyLst = new List<mCompany>();
                string UserType = profile.Personal.UserType.ToString();
                long UID = profile.Personal.UserID;
                if (UserType == "Admin")
                {
                    //  CompanyLst = objService.GetCompanyName(profile.DBConnection._constr).ToList();
                    //CompanyLst = objService.GetUserCompanyName(UID, profile.DBConnection._constr).ToList();

                    CompanyLst = objService.GetUserCompanyNameNEW(UID, profile.DBConnection._constr).ToList();
                }
                else if (UserType == "User" || UserType == "Requester And Approver" || UserType == "Requester" || profile.Personal.UserType == "Requestor" || profile.Personal.UserType == "Requestor And Approver" || profile.Personal.UserType == "Approver")
                {
                    CompanyLst = objService.GetUserCompanyName(UID, profile.DBConnection._constr).ToList();
                }
                else
                {
                    CompanyLst = objService.GetCompanyName(profile.DBConnection._constr).ToList();
                }
                ddlCompany.DataSource = CompanyLst;
                ddlCompany.DataBind();


                if (UserType == "Admin")
                {
                    ListItem lstCmpny = new ListItem { Text = "-Select-", Value = "0" };
                    ddlCompany.Items.Insert(0, lstCmpny);
                    if (ddlCompany.Items.Count > 0) ddlCompany.SelectedIndex = 1;
                    ddlCompany.SelectedIndex = ddlCompany.Items.IndexOf(ddlCompany.Items.FindByValue(profile.Personal.CompanyID.ToString()));
                    hdnselectedCompany.Value = profile.Personal.CompanyID.ToString();

                    List<mTerritory> SiteLst = new List<mTerritory>();
                    iUCCommonFilterClient UCCommonFilter = new iUCCommonFilterClient();

                    //SiteLst = UCCommonFilter.GetSiteNameByUserID(Convert.ToInt16(UID), profile.DBConnection._constr).ToList();
                    int Cmpny = int.Parse(hdnselectedCompany.Value);
                    SiteLst = WMGetSelDept(Cmpny, profile.Personal.UserID);

                    ddlSites.DataSource = SiteLst;
                    ddlSites.DataBind();
                    if (ddlSites.Items.Count > 0) ddlSites.SelectedIndex = 0;

                    hdnselectedDept.Value = ddlSites.SelectedValue.ToString(); Session["DeptID"] = ddlSites.SelectedValue.ToString();
                    hdnMaxDeliveryDays.Value = Convert.ToString(WMGetMaxDeliveryDays(long.Parse(hdnselectedDept.Value)));

                    //  long DeptID = UCCommonFilter.GetSiteIdOfUser(UID, profile.DBConnection._constr); hdnselectedDept.Value = DeptID.ToString(); Session["DeptID"] = DeptID.ToString();
                    //    long CompanyID = UCCommonFilter.GetCompanyIDFromSiteID(DeptID, profile.DBConnection._constr); hdnselectedCompany.Value = CompanyID.ToString();

                    //ddlContact1.DataSource = WMGetContactPersonLst(CompanyID); //WMGetContactPersonLst(DeptID);
                    //ddlContact1.DataBind();
                    //ListItem lstContact = new ListItem { Text = "-Select-", Value = "0" };
                    //ddlContact1.Items.Insert(0, lstContact);

                    //ddlAddress.DataSource = WMGetDeptAddress(CompanyID); //WMGetDeptAddress(DeptID);
                    //ddlAddress.DataBind();
                    //ListItem lstAdrs = new ListItem { Text = "-Select-", Value = "0" };
                    //ddlAddress.Items.Insert(0, lstAdrs);
                }
                else if (UserType == "User" || UserType == "Requester And Approver" || UserType == "Requester" || profile.Personal.UserType == "Requestor" || profile.Personal.UserType == "Requestor And Approver" || profile.Personal.UserType == "Approver")
                {
                    ddlCompany.Enabled = false;
                    ddlCompany.SelectedIndex = ddlCompany.Items.IndexOf(ddlCompany.Items.FindByValue(profile.Personal.CompanyID.ToString()));
                    hdnselectedCompany.Value = profile.Personal.CompanyID.ToString();

                    List<mTerritory> SiteLst = new List<mTerritory>();
                    iUCCommonFilterClient UCCommonFilter = new iUCCommonFilterClient();

                    //SiteLst = UCCommonFilter.GetDepartmentListUserWise(Convert.ToInt16(UID), profile.DBConnection._constr).ToList();
                    SiteLst = UCCommonFilter.GetSiteNameByUserID(Convert.ToInt16(UID), profile.DBConnection._constr).ToList();

                    ddlSites.DataSource = SiteLst;
                    ddlSites.DataBind();
                    if (ddlSites.Items.Count > 0) ddlSites.SelectedIndex = 0;

                    //  ddlSites.Enabled = false;
                    long DeptID = UCCommonFilter.GetSiteIdOfUser(UID, profile.DBConnection._constr);
                    hdnselectedDept.Value = DeptID.ToString();
                    Session["DeptID"] = DeptID.ToString();
                    hdnMaxDeliveryDays.Value = Convert.ToString(WMGetMaxDeliveryDays(long.Parse(hdnselectedDept.Value)));
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "displaysegmenttype", "displaysegmenttype();", true);
                    long CompanyID = UCCommonFilter.GetCompanyIDFromSiteID(DeptID, profile.DBConnection._constr);
                    hdnselectedCompany.Value = CompanyID.ToString();

                    ddlContact1.DataSource = WMGetContactPersonLst(CompanyID); //WMGetContactPersonLst(DeptID);
                    ddlContact1.DataBind();
                    ListItem lstContact = new ListItem { Text = "-Select-", Value = "0" };
                    ddlContact1.Items.Insert(0, lstContact);

                    ddlAddress.DataSource = WMGetDeptAddress(CompanyID); //WMGetDeptAddress(DeptID);
                    ddlAddress.DataBind();
                    ListItem lstAdrs = new ListItem { Text = "-Select-", Value = "0" };
                    ddlAddress.Items.Insert(0, lstAdrs);
                }
                else
                {
                    ListItem lstCmpny = new ListItem { Text = "-Select-", Value = "0" };
                    ddlCompany.Items.Insert(0, lstCmpny);
                    if (ddlCompany.Items.Count > 0) ddlCompany.SelectedIndex = 1;
                    ddlCompany.SelectedIndex = ddlCompany.Items.IndexOf(ddlCompany.Items.FindByValue(profile.Personal.CompanyID.ToString()));
                    hdnselectedCompany.Value = profile.Personal.CompanyID.ToString();

                    List<mTerritory> SiteLst = new List<mTerritory>();
                    iUCCommonFilterClient UCCommonFilter = new iUCCommonFilterClient();

                    SiteLst = UCCommonFilter.GetSiteNameByUserID(Convert.ToInt16(UID), profile.DBConnection._constr).ToList();

                    ddlSites.DataSource = SiteLst;
                    ddlSites.DataBind();
                    if (ddlSites.Items.Count > 0) ddlSites.SelectedIndex = 0;

                    long DeptID = UCCommonFilter.GetSiteIdOfUser(UID, profile.DBConnection._constr); hdnselectedDept.Value = DeptID.ToString(); Session["DeptID"] = DeptID.ToString(); hdnMaxDeliveryDays.Value = Convert.ToString(WMGetMaxDeliveryDays(long.Parse(hdnselectedDept.Value)));
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "displaysegmenttype", "displaysegmenttype();", true);
                    long CompanyID = UCCommonFilter.GetCompanyIDFromSiteID(DeptID, profile.DBConnection._constr);
                    hdnselectedCompany.Value = CompanyID.ToString();

                    ddlContact1.DataSource = WMGetContactPersonLst(CompanyID); //WMGetContactPersonLst(DeptID);
                    ddlContact1.DataBind();
                    ListItem lstContact = new ListItem { Text = "-Select-", Value = "0" };
                    ddlContact1.Items.Insert(0, lstContact);

                    ddlAddress.DataSource = WMGetDeptAddress(CompanyID); //WMGetDeptAddress(DeptID);
                    ddlAddress.DataBind();
                    ListItem lstAdrs = new ListItem { Text = "-Select-", Value = "0" };
                    ddlAddress.Items.Insert(0, lstAdrs);
                }
                if (hdnselectedCompany.Value.ToString() == "" || hdnselectedCompany.Value == null)
                {
                    hdnselectedCompany.Value = "0";
                }
                if (Session["PORRequestID"] != null && Session["PORRequestID"].ToString() != "0")
                {
                    iPartRequestClient objService1 = new iPartRequestClient();
                    //PORtPartRequestHead RequestHead = new PORtPartRequestHead();
                    tOrderHead RequestHead = new tOrderHead();
                    RequestHead = objService1.GetOrderHeadByOrderID(Convert.ToInt64(HttpContext.Current.Session["PORRequestID"].ToString()), profile.DBConnection._constr);
                    long SiteID = long.Parse(RequestHead.StoreId.ToString());
                    HttpContext.Current.Session["PORSitetID"] = Convert.ToString(SiteID);
                    long CompanyID = objService.GetCompanyIDFromSiteID(SiteID, profile.DBConnection._constr);
                    string ISProjectSiteDetails = "";
                    hdnselectedCompany.Value = Convert.ToString(CompanyID);
                    ISProjectSiteDetails = objService.ISProjectSiteDetails(Convert.ToString(hdnselectedCompany.Value), profile.DBConnection._constr);
                    //if (hdnselectedCompany.Value.ToString() == Convert.ToString("10266"))
                    if (ISProjectSiteDetails == "Yes")
                    {
                        hdnISProjectSiteDetails.Value = "Yes";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowDetails", "ShowDetails();", true);

                    }
                    else
                    {
                        hdnISProjectSiteDetails.Value = "No";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowDetails", "NotShowDetails();", true);
                    }
                }
                else
                {


                    string ISProjectSiteDetails = "";
                    ISProjectSiteDetails = objService.ISProjectSiteDetails(Convert.ToString(hdnselectedCompany.Value), profile.DBConnection._constr);
                    //if (hdnselectedCompany.Value.ToString() == Convert.ToString("10266"))
                    if (ISProjectSiteDetails == "Yes")
                    {
                        hdnISProjectSiteDetails.Value = "Yes";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowDetails", "ShowDetails();", true);

                    }
                    else
                    {
                        hdnISProjectSiteDetails.Value = "No";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowDetails", "NotShowDetails();", true);
                    }
                }

                fillPaymentMethod(long.Parse(hdnselectedDept.Value));
            }
            catch { }
            finally { objService.Close(); }
        }

        public void fillPaymentMethod(long selectedDept)
        {
            iPartRequestClient objService = new iPartRequestClient();
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                DataSet ds = new DataSet();
                ds = objService.GetDeptWisePaymentMethod(selectedDept, profile.DBConnection._constr);
                ddlPaymentMethod.DataSource = ds;
                ddlPaymentMethod.DataBind();
                //ListItem lstpm = new ListItem { Text = "--Select--", Value = "0" };
                //ddlPaymentMethod.Items.Insert(0, lstpm);

                ddlFOC.DataSource = WMGetCostCenter(selectedDept);
                ddlFOC.DataBind();
                ListItem lstfoc = new ListItem { Text = "--Select--", Value = "0" };
                ddlFOC.Items.Insert(0, lstfoc);
            }
            catch { }
            finally { objService.Close(); }
        }

        [WebMethod]
        public static List<PORServicePartRequest.mStatu> WMFillStatus()
        {
            string state = HttpContext.Current.Session["PORstate"].ToString();
            iPartRequestClient objService = new iPartRequestClient();
            List<PORServicePartRequest.mStatu> StatusList = new List<PORServicePartRequest.mStatu>();
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();

                if (HttpContext.Current.Session["PORRequestID"].ToString() == "0" && state == "Add")
                {
                    //if (profile.Personal.UserType == "User")
                    //{
                    //    StatusList = objService.GetStatusListForRequest("Request", state, profile.Personal.UserID, profile.DBConnection._constr).ToList();
                    //}
                    //else
                    //{
                    StatusList = objService.GetStatusListForRequest("All,Request", state, profile.Personal.UserID, profile.DBConnection._constr).ToList();
                    StatusList = StatusList.Where(s => s.ID == 1 || s.ID == 2).ToList();
                    // }
                }
                else if (HttpContext.Current.Session["PORRequestID"].ToString() != "0" && state == "Edit")
                {
                    if (HttpContext.Current.Session["OrderStatus"].ToString() == "1")
                    {
                        StatusList = objService.GetStatusListForRequest("All,Request", state, profile.Personal.UserID, profile.DBConnection._constr).ToList();
                        StatusList = StatusList.Where(s => s.ID == 1 || s.ID == 2).ToList();
                    }
                    else
                    {
                        StatusList = objService.GetStatusListForRequest("All,Request", state, profile.Personal.UserID, profile.DBConnection._constr).ToList();
                    }
                }
                else if (HttpContext.Current.Session["PORRequestID"].ToString() != "0" && state == "View")
                {
                    StatusList = objService.GetStatusListForRequest("", "", 0, profile.DBConnection._constr).ToList();
                }

                PORServicePartRequest.mStatu select = new PORServicePartRequest.mStatu() { ID = 0, Status = "-Select-" };
                StatusList.Insert(0, select);
            }
            catch { }
            finally { objService.Close(); }
            return StatusList;
        }

        public List<vGetUserProfileByUserID> FillCurrentUserList(long SiteID)
        {
            iUCCommonFilterClient objService = new iUCCommonFilterClient();
            List<vGetUserProfileByUserID> UsersList = new List<vGetUserProfileByUserID>();
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                UsersList = objService.GetUserListBySiteID(SiteID, profile.DBConnection._constr).ToList();
                //UsersList = UsersList.GroupBy(x => x.userID).Select(x => x.FirstOrDefault()).ToList();
                UsersList = UsersList.Where(x => x.userID == profile.Personal.UserID).ToList();
                vGetUserProfileByUserID select = new vGetUserProfileByUserID() { userID = 0, userName = "-Select-" };
                UsersList.Insert(0, select);
            }
            catch { }
            finally { objService.Close(); }
            return UsersList;
        }

        [WebMethod]
        public static List<vGetUserProfileByUserID> WMFillUserList(long SiteID)
        {
            iUCCommonFilterClient objService = new iUCCommonFilterClient();
            List<vGetUserProfileByUserID> UsersList = new List<vGetUserProfileByUserID>();
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                UsersList = objService.GetUserListBySiteID(SiteID, profile.DBConnection._constr).ToList();
                UsersList = UsersList.GroupBy(x => x.userID).Select(x => x.FirstOrDefault()).ToList();
                //  UsersList = UsersList.Where(x => x.userID == profile.Personal.UserID).ToList();
                vGetUserProfileByUserID select = new vGetUserProfileByUserID() { userID = 0, userName = "-Select-" };
                UsersList.Insert(0, select);
            }
            catch { }
            finally { objService.Close(); }
            return UsersList;
        }

        [WebMethod]
        public static List<PORServiceUCCommonFilter.v_GetEngineDetails> WMFillEnginList(long SiteID)
        {
            iUCCommonFilterClient objService = new iUCCommonFilterClient();
            List<PORServiceUCCommonFilter.v_GetEngineDetails> EngineList = new List<PORServiceUCCommonFilter.v_GetEngineDetails>();
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                EngineList = objService.GetEngineOfSite(SiteID.ToString(), profile.DBConnection._constr).ToList();
                PORServiceUCCommonFilter.v_GetEngineDetails select = new PORServiceUCCommonFilter.v_GetEngineDetails() { ID = 0, Container = "-Select-" };
                EngineList.Insert(0, select);
            }
            catch { }
            finally { objService.Close(); }
            return EngineList;
        }

        [WebMethod]
        public static List<mTerritory> WMGetDept(int Cmpny)
        {
            List<mTerritory> SiteLst = new List<mTerritory>();
            iUCCommonFilterClient UCCommonFilter = new iUCCommonFilterClient();
            CustomProfile profile = CustomProfile.GetProfile();
            // SiteLst = UCCommonFilter.GetDepartmentList(Cmpny, profile.DBConnection._constr).ToList();
            if (profile.Personal.UserType == "Admin")
            {
                SiteLst = UCCommonFilter.GetAddedDepartmentList(Cmpny, profile.Personal.UserID, profile.DBConnection._constr).ToList();
            }
            else
            {
                SiteLst = UCCommonFilter.GetDepartmentList(Cmpny, profile.DBConnection._constr).ToList();
            }
            return SiteLst;
        }

        [WebMethod]
        public static List<tContactPersonDetail> WMGetContactPersonLst(long Dept)
        {
            List<tContactPersonDetail> ConPerLst = new List<tContactPersonDetail>();
            iUCCommonFilterClient UCCommonFilter = new iUCCommonFilterClient();
            CustomProfile profile = CustomProfile.GetProfile();

            ConPerLst = UCCommonFilter.GetContactPersonList(Dept, profile.DBConnection._constr).ToList();

            return ConPerLst;
        }

        [WebMethod]
        public static List<tContactPersonDetail> WMGetContactPerson2Lst(long Dept, long Cont1)
        {
            List<tContactPersonDetail> ConPerLst = new List<tContactPersonDetail>();
            iUCCommonFilterClient UCCommonFilter = new iUCCommonFilterClient();
            CustomProfile profile = CustomProfile.GetProfile();

            ConPerLst = UCCommonFilter.GetContactPerson2List(Dept, Cont1, profile.DBConnection._constr).ToList();

            return ConPerLst;
        }

        [WebMethod]
        public static List<tAddress> WMGetDeptAddress(long Dept)
        {
            List<tAddress> AdrsLst = new List<tAddress>();
            iUCCommonFilterClient UCCommonFilter = new iUCCommonFilterClient();
            CustomProfile profile = CustomProfile.GetProfile();

            AdrsLst = UCCommonFilter.GetDeptAddressList(Dept, profile.DBConnection._constr).ToList();

            return AdrsLst;
        }
        #endregion

        #region RequestHead
        protected void GetRequestHead()
        {
            iPartRequestClient objService = new iPartRequestClient();
            iUCCommonFilterClient objCommon = new iUCCommonFilterClient();
            //PORtPartRequestHead RequestHead = new PORtPartRequestHead();
            tOrderHead RequestHead = new tOrderHead();
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                //RequestHead = objService.GetRequestHeadByRequestID(Convert.ToInt64(HttpContext.Current.Session["PORRequestID"].ToString()), profile.DBConnection._constr);
                RequestHead = objService.GetOrderHeadByOrderID(Convert.ToInt64(HttpContext.Current.Session["PORRequestID"].ToString()), profile.DBConnection._constr);
                hdnordertype.Value = RequestHead.Title;
                hdnOrderStatus.Value = RequestHead.Status.ToString(); Session["OrderStatus"] = hdnOrderStatus.Value;
                FillGrid1ByRequestID(RequestHead.Id, Convert.ToInt64(RequestHead.StoreId));
                txtTitle.Text = RequestHead.Title;

                long SiteID = long.Parse(RequestHead.StoreId.ToString());
                Session["DeptID"] = SiteID;
                if (SiteID == 10307)
                {
                    DataSet dsseg = new DataSet();
                    dsseg = objCommon.getsegmentbyOrderID(RequestHead.Id, profile.DBConnection._constr);
                    if (dsseg.Tables[0].Rows.Count > 0)
                    {
                        txtsengmenttype.Text = dsseg.Tables[0].Rows[0]["Segmenttype"].ToString();
                    }
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "displaysegmenttype", "displaysegmenttype();", true);
                }
                HttpContext.Current.Session["PORSitetID"] = Convert.ToString(SiteID);
                
                long CompanyID = objCommon.GetCompanyIDFromSiteID(SiteID, profile.DBConnection._constr);
                List<mCompany> CompanyLst = new List<mCompany>();
                CompanyLst = objCommon.GetCompanyName(profile.DBConnection._constr).ToList();
                ddlCompany.DataSource = CompanyLst;
                ddlCompany.DataBind();
                ddlCompany.SelectedIndex = ddlCompany.Items.IndexOf(ddlCompany.Items.FindByValue(CompanyID.ToString()));
                ddlCompany.Enabled = false;
                long UID = profile.Personal.UserID;
                List<mTerritory> SiteLst = new List<mTerritory>();
                if (profile.Personal.UserType == "Admin")
                {
                    SiteLst = objCommon.GetSiteNameByUserID(Convert.ToInt16(UID), profile.DBConnection._constr).ToList();
                }
                else if (profile.Personal.UserType == "Super Admin")
                {
                    SiteLst = objCommon.GetAllDepartmentList(profile.DBConnection._constr).ToList();
                }
                SiteLst = objCommon.GetAllDepartmentList(profile.DBConnection._constr).ToList();
                ddlSites.DataSource = SiteLst;
                ddlSites.DataBind();
                ddlSites.SelectedIndex = ddlSites.Items.IndexOf(ddlSites.Items.FindByValue(SiteID.ToString()));
                if (hdnOrderStatus.Value == "1") { lblRequestNo.Text = "Generate when Save"; }
                else
                {
                    lblRequestNo.Text = RequestHead.OrderNo.ToString(); //RequestHead.Id.ToString();
                }
                ddlStatus.DataSource = WMFillStatus();
                ddlStatus.DataBind();
                ddlStatus.SelectedIndex = ddlStatus.Items.IndexOf(ddlStatus.Items.FindByValue(RequestHead.Status.ToString()));

                UC_DateRequestDate.Date = RequestHead.Orderdate;
                txtRequestDate.Text = Convert.ToString(RequestHead.Orderdate.Value.ToString("dd-MMM-yyyy"));

                //      ddlRequestType.SelectedIndex = ddlRequestType.Items.IndexOf(ddlRequestType.Items.FindByValue(RequestHead.Priority.ToString()));
                if (hdnOrderStatus.Value == "1")
                {
                    ddlRequestByUserID.DataSource = FillCurrentUserList(Convert.ToInt64(hdnselectedDept.Value));
                }
                else
                {
                    ddlRequestByUserID.DataSource = WMFillUserList(Convert.ToInt64(RequestHead.StoreId)); hdnselectedDept.Value = RequestHead.StoreId.ToString();
                }
                ddlRequestByUserID.DataBind();
                ddlRequestByUserID.SelectedIndex = ddlRequestByUserID.Items.IndexOf(ddlRequestByUserID.Items.FindByValue(RequestHead.RequestBy.ToString()));

                txtRemark.Text = RequestHead.Remark;

                txtCustOrderRefNo.Text = RequestHead.OrderNumber;
                UC_ExpDeliveryDate.Date = RequestHead.Deliverydate; //if (RequestHead.Status >= 2) { Page.ClientScript.RegisterStartupScript(this.GetType(), "reset", " disableExpDeliveryDate();", true); }

                if (profile.Personal.UserType != "User" || profile.Personal.UserType != "Requester And Approver" || profile.Personal.UserType != "Requester" || profile.Personal.UserType != "Requestor" || profile.Personal.UserType != "Requestor And Approver" || profile.Personal.UserType != "Approver")
                {
                    ddlAddress.DataSource = WMGetDeptAddress(CompanyID); //WMGetDeptAddress(SiteID);
                    ddlAddress.DataBind();
                    ListItem lstAdrs = new ListItem { Text = "-Select-", Value = "0" };
                    ddlAddress.Items.Insert(0, lstAdrs);

                    // ddlContact1.DataSource = WMGetContactPersonLst(SiteID);
                    ddlContact1.DataSource = WMGetContactPersonLst(CompanyID);
                    ddlContact1.DataBind();
                    ListItem lstContact = new ListItem { Text = "-Select-", Value = "0" };
                    ddlContact1.Items.Insert(0, lstContact);
                }
                ddlAddress.SelectedIndex = ddlAddress.Items.IndexOf(ddlAddress.Items.FindByValue(RequestHead.AddressId.ToString())); hdnSelAddress.Value = RequestHead.AddressId.ToString();
                ddlContact1.SelectedIndex = ddlContact1.Items.IndexOf(ddlContact1.Items.FindByValue(RequestHead.ContactId1.ToString())); hdnselectedCont1.Value = RequestHead.ContactId1.ToString();
                // ddlContact2.DataSource = WMGetContactPerson2Lst(Convert.ToInt64(RequestHead.StoreId), Convert.ToInt64(ddlContact1.SelectedIndex));
                ddlContact2.DataSource = WMGetContactPerson2Lst(CompanyID, Convert.ToInt64(ddlContact1.SelectedIndex));
                ddlContact2.DataBind();
                ddlContact2.SelectedIndex = ddlContact2.Items.IndexOf(ddlContact2.Items.FindByValue(RequestHead.ContactId2.ToString())); //hdnselectedCont2.Value = RequestHead.ContactId2.ToString();

                /*New Change Code*/
                long EdtCon1 = long.Parse(RequestHead.ContactId1.ToString());
                long EdtAddress = long.Parse(RequestHead.AddressId.ToString());

                long LocAddress = long.Parse(RequestHead.LocationID.ToString());

                string EdtCon2 = RequestHead.Con2; hdnselectedCont2.Value = RequestHead.Con2.ToString();
                if (EdtCon1 != 0)
                {
                    txtContact1.Text = objCommon.getContact1NameByID(EdtCon1, profile.DBConnection._constr);
                }
                //if (EdtCon2 != "0") 
                //{
                //    txtContact2.Text = objCommon.getContact2NamesByIDs(EdtCon2, profile.DBConnection._constr);
                //}
                if (EdtCon2 != string.Empty) { txtContact2.Text = objCommon.getContact2NamesByIDs(EdtCon2, profile.DBConnection._constr); }

                if (EdtAddress != 0)
                {
                    txtAddress.Text = objCommon.GetAddressLineByAdrsID(EdtAddress, profile.DBConnection._constr);
                    lblAddress.Text = objCommon.GetAddressLineByAdrsID(EdtAddress, profile.DBConnection._constr);
                }

                if (LocAddress != 0)
                {
                    txtLocation.Text = objCommon.GetLocationCodeName(LocAddress, profile.DBConnection._constr);
                    lblLocation.Text = objCommon.GetAddressLineByAdrsID(LocAddress, profile.DBConnection._constr);
                }
                /*New Change Code*/

                // lblAddress.Text = ddlAddress.SelectedItem.ToString();

                if (RequestHead.DispatchDate != null)
                { txtShippedDate.Text = RequestHead.DispatchDate.ToString(); }
                else { lblshipeddate.Visible = false; txtShippedDate.Visible = false; }
                //RequestHead.CompletedDate.ToString("dd/MM/yyyy").ToString()
                if (RequestHead.CompletedDate != null)
                {
                    DateTime date1 = new DateTime();
                    date1 = Convert.ToDateTime(RequestHead.CompletedDate.Value.ToString());
                    string cOrderDate = date1.ToString("dd/MM/yyyy hh:mm:ss");
                    // txtReceivedDate.Text = RequestHead.CompletedDate.ToString();
                    txtReceivedDate.Text = cOrderDate;
                }
                else
                {
                    lblReceivedDate.Visible = false; txtReceivedDate.Visible = false;
                }
                if (RequestHead.CancelDate != null) { txtCloseDate.Text = RequestHead.CancelDate.ToString(); } else { lblCloseDate.Visible = false; txtCloseDate.Visible = false; }
                if (RequestHead.WMSRemark != null) { txtDispatchRemark.Text = RequestHead.WMSRemark.ToString(); }
                if (RequestHead.ReciptNo != null) { txtreciptno.Text = RequestHead.ReciptNo.ToString(); }
                if (RequestHead.ASNNumber != null) { txtasnno.Text = RequestHead.ASNNumber.ToString(); }
                if (RequestHead.RefundCode != null) { txtrefundcode.Text = RequestHead.RefundCode.ToString(); }
                // DateTime refundate = new DateTime();

                // start combine CR 2022 project 2 changes
                string refundate = "";
                refundate = objService.getrefunddate(RequestHead.Id, profile.DBConnection._constr);
                txtrefunddate.Text = refundate;
                // end combine CR 2022 project 2 changes

                txtTotalQty.Text = RequestHead.TotalQty.ToString();
                //  txtvattotalamt.Text = RequestHead.TotalVATAmt.ToString();            ****commented becayuse of VAT CR not up yet remove comment when VAT CR up
                //  txtvatexclamt.Text = RequestHead.VATExclTotalamt.ToString();         **** commented becayuse of VAT CR not up yet remove comment when VAT CR up
                txtGrandTotal.Text = RequestHead.GrandTotal.ToString();
                fillPaymentMethod(long.Parse(hdnselectedDept.Value));
                ddlPaymentMethod.SelectedIndex = ddlPaymentMethod.Items.IndexOf(ddlPaymentMethod.Items.FindByValue(RequestHead.PaymentID.ToString()));
                Page.ClientScript.RegisterStartupScript(this.GetType(), "reset", " GetPaymentMethodID();", true);
                if (RequestHead.PaymentID == 5)
                {
                   
                    DataSet dsCostCenter = new DataSet();
                    dsCostCenter = objService.GetSelectedCostCenter(long.Parse(RequestHead.Id.ToString()), profile.DBConnection._constr);
                    if (dsCostCenter.Tables[0].Rows.Count > 0)
                    {
                        long SelectedCostCenter = long.Parse(dsCostCenter.Tables[0].Rows[0]["StatutoryValue"].ToString());
                        ddlFOC.DataSource = WMGetCostCenter(long.Parse(hdnselectedDept.Value));
                        ddlFOC.DataBind();
                        ddlFOC.SelectedIndex = ddlFOC.Items.IndexOf(ddlFOC.Items.FindByValue(SelectedCostCenter.ToString()));
                        ddlFOC.SelectedValue = SelectedCostCenter.ToString();
                    }
                    
                  
                }
                else
                {
                    //LstStatutoryInfo.Enabled = false;
                    if (RequestHead.Status >= 2) { LstStatutoryInfo.Enabled = false; }
                    DataSet dsAdditionalFields = new DataSet();
                    dsAdditionalFields = objService.GetAddedAdditionalFields(long.Parse(RequestHead.Id.ToString()), profile.DBConnection._constr);
                    LstStatutoryInfo.DataSource = dsAdditionalFields;
                    LstStatutoryInfo.DataBind();

                }
                GetDocumentByOrderID(long.Parse(RequestHead.Id.ToString()));
                GetApprovalDetails();

                //  GetDeliveryDetails(RequestHead.Id);

                //show project type and site
                hdnselectedCompany.Value = CompanyID.ToString();
                if (hdnselectedCompany.Value.ToString() == "" || hdnselectedCompany.Value == null)
                {
                    hdnselectedCompany.Value = "0";
                }
                iUCCommonFilterClient UCCommonFilter = new iUCCommonFilterClient();
                string ISProjectSiteDetails = "";
                ISProjectSiteDetails = UCCommonFilter.ISProjectSiteDetails(Convert.ToString(hdnselectedCompany.Value), profile.DBConnection._constr);
                if (ISProjectSiteDetails == "Yes")
                {
                    hdnISProjectSiteDetails.Value = "Yes";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowDetails", "ShowDetails();", true);
                    DataSet dsvft = new DataSet();
                    dsvft = objService.GetProjectSiteDetails(RequestHead.Id, profile.DBConnection._constr);
                    if (dsvft.Tables[0].Rows.Count > 0)
                    {
                        hdnIsState.Value = "EditProjectsite";
                        hdnselectedprojectid.Value = dsvft.Tables[0].Rows[0]["Projtypeid"].ToString();
                        hdnselectedsiteid.Value = dsvft.Tables[0].Rows[0]["siteid"].ToString();
                        txtprojectype.Text = dsvft.Tables[0].Rows[0]["projecttypenm"].ToString();
                        txtsitecode.Text = dsvft.Tables[0].Rows[0]["SiteCode1"].ToString();
                        txtsitenm.Text = dsvft.Tables[0].Rows[0]["SiteName"].ToString();
                        txtLatitude.Text = dsvft.Tables[0].Rows[0]["Latitude"].ToString();
                        txtLongitude.Text = dsvft.Tables[0].Rows[0]["Longitude"].ToString();
                        txtAccessRequirement.Text = dsvft.Tables[0].Rows[0]["AccessRequirement"].ToString();
                    }
                }
                else
                {
                    hdnISProjectSiteDetails.Value = "No";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowDetails", "NotShowDetails();", true);
                }

                //Check order for edit or not
                string ChkISOrdCreator = objService.ChkISOrdCreatorOrNot(Convert.ToInt64(RequestHead.Id), profile.Personal.UserID, profile.DBConnection._constr);
                if (ChkISOrdCreator == "Yes")
                {
                    hdnOrderisEdit.Value = "Yes";
                }
                else
                {
                    hdnOrderisEdit.Value = "No";
                }


                //add by suraj for show new dispatch grid
                ShowDispatchDetails(RequestHead.Id);
                //add by suraj for show new dispatch grid


            }
            catch (Exception ex) {  }
            finally { objService.Close(); }
        }

        private void ShowDispatchDetails(long RequestHead)
        {
            iPartRequestClient objService = new iPartRequestClient();
            CustomProfile profile = CustomProfile.GetProfile();
            DataSet dsorderdetails = new DataSet();
            dsorderdetails = objService.ShowDispatchGrid(RequestHead, profile.DBConnection._constr);
            if (dsorderdetails.Tables[0].Rows.Count > 0)
            {
                GridDispatchstatus.DataSource = dsorderdetails;
                GridDispatchstatus.DataBind();
            }
        }





        public void GetDocumentByOrderID(long OrderID)
        {
            CustomProfile profile = CustomProfile.GetProfile();
            UC_AttachmentDocument1.FillDocumentByObjectNameReferenceID(OrderID, "RequestPartDetail", "RequestPartDetail");
        }

        public void GetDeliveryDetails(long RequestHeadId)
        {
            iPartRequestClient objService = new iPartRequestClient();
            VW_OrderDeliveryDetails ODD = new VW_OrderDeliveryDetails();
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                ODD = objService.GetOrderDeliveryDetails(RequestHeadId, profile.DBConnection._constr);

                if (ODD.Name != null) txtCustName.Text = ODD.Name.ToString();
                if (ODD.ContactNo != null) txtContactNo.Text = ODD.ContactNo.ToString();

                if (ODD.Address != null) txtCustAddress.Text = ODD.Address.ToString();
                if (ODD.EmailId != null) txtEmail.Text = ODD.EmailId.ToString();

                if (ODD.Landmark != null) txtLandmark.Text = ODD.Landmark.ToString();
                if (ODD.Zipcode != null) txtZipcode.Text = ODD.Zipcode.ToString();
                if (ODD.PaymentMode != null) txtPaymentMode.Text = ODD.PaymentMode.ToString();
                if (ODD.CardNo != null) txtCardNo.Text = ODD.CardNo.ToString();
                if (ODD.Remark != null) txtPaymentRemark.Text = ODD.Remark.ToString();
                if (ODD.BankName != null) txtBankName.Text = ODD.BankName.ToString();
                if (ODD.PaymentDate != null) txtPaymentDate.Text = ODD.PaymentDate.ToString();
                if (ODD.DriverName != null) txtDriverName.Text = ODD.DriverName.ToString();
                if (ODD.DriverMobileNo != null) txtDriverContactNo.Text = ODD.DriverMobileNo.ToString();
                if (ODD.DriverEmailID != null) txtDriverEmail.Text = ODD.DriverEmailID.ToString();
                if (ODD.TruckDetail != null) txtTruckDetails.Text = ODD.TruckDetail.ToString();
                if (ODD.AssignDate != null) txtAssignDate.Text = ODD.AssignDate.ToString();
                if (ODD.DeliveryType != null) txtDeliveryType.Text = ODD.DeliveryType.ToString();

                if (ODD.Card_Verified != null)
                {
                    string CV = ODD.Card_Verified.ToString();
                    if (CV == "Verified") { Verified.Visible = true; }
                    else if (CV == "Pending") { Pending.Visible = true; }
                    else if (CV == "Decline") { Decline.Visible = true; }
                }
            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, "PartRequestEntry.aspx", "GetDeliveryDetails");
            }
            finally { objService.Close(); }
        }

        [WebMethod]
        public static PORServiceEngineMaster.v_GetEngineDetails WMGetEngineDetails(int EngineID)
        {
            iEngineMasterClient objService = new iEngineMasterClient();
            PORServiceEngineMaster.v_GetEngineDetails EngineRec = new PORServiceEngineMaster.v_GetEngineDetails();
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                EngineRec = objService.GetmEngineListByID(EngineID, profile.DBConnection._constr);
            }
            catch { }
            finally { objService.Close(); }
            return EngineRec;
        }

        [WebMethod]
        public static string WMSaveEditmodeData(string pid, string sid)
        {
            iPartRequestClient objService = new iPartRequestClient();
            CustomProfile profile = CustomProfile.GetProfile();
            string result = "";
            try
            {
                result = objService.SaveEditmodedata(HttpContext.Current.Session["PORRequestID"].ToString(), pid, sid, profile.Personal.UserID, profile.DBConnection._constr);
                string ChkSerialedCompany = objService.ChkOrderSerialedCompany(Convert.ToInt64(HttpContext.Current.Session["PORRequestID"].ToString()), profile.DBConnection._constr);
                if (ChkSerialedCompany == "Yes")
                {
                    string ChkISOrdCreator = objService.ChkISOrdCreatorOrNot(Convert.ToInt64(HttpContext.Current.Session["PORRequestID"].ToString()), profile.Personal.UserID, profile.DBConnection._constr);
                    if (ChkISOrdCreator == "Yes")
                    {
                        //int RSLT = objService.FinalSaveRequestPartDetailSerial(HttpContext.Current.Session.SessionID, ObjectNameSerial, Convert.ToInt64(HttpContext.Current.Session["PORRequestID"].ToString()), profile.Personal.UserID.ToString(), Convert.ToInt64(HttpContext.Current.Session["PORSitetID"]), 0, profile.DBConnection._constr);
                      int RSLT = objService.FinalSaveRequestPartDetailSerial_New(HttpContext.Current.Session.SessionID, ObjectNameSerial, Convert.ToInt64(HttpContext.Current.Session["PORRequestID"].ToString()), profile.Personal.UserID.ToString(), Convert.ToInt64(HttpContext.Current.Session["PORSitetID"]), 0, profile.DBConnection._constr);
                        //objService.EmailSendWhenRequestreSubmit(Convert.ToInt64(HttpContext.Current.Session["PORRequestID"].ToString()), profile.DBConnection._constr);
                       objService.EmailSendWhenRequestreSubmit_New(Convert.ToInt64(HttpContext.Current.Session["PORRequestID"].ToString()), profile.DBConnection._constr);
                    }
                }
            }
            catch
            (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, "PartRequestEntry.aspx", "WMSaveEditmodeData");
                result = "0"; // "Some error occurred";
            }
            return result;
        }

        [WebMethod]
        public static long WMSaveRequestHead_New(object objReq)
        {
            long result = 0;
            long PreviousStatusID = 0;
            int RSLT = 0; long RequestID = 0;
            iPartRequestClient objService = new iPartRequestClient();   
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                List<POR_SP_GetPartDetail_ForRequest_Result> RequestPartList = new List<POR_SP_GetPartDetail_ForRequest_Result>();
                RequestPartList = objService.GetExistingTempDataBySessionIDObjectName(HttpContext.Current.Session.SessionID, profile.Personal.UserID.ToString(), ObjectName, profile.DBConnection._constr).ToList();
                //RequestPartList.Select( )
                bool found = false;
                XElement xmlEle = new XElement("Request", from rec in RequestPartList
                                                          select new XElement("PartList",
                                                          new XElement("Prod_ID", Convert.ToInt64(rec.Prod_ID)),
                                                          new XElement("RequestQty", Convert.ToDecimal(rec.RequestQty)),
                                                          new XElement("AvailableBalance", Convert.ToDecimal(rec.CurrentStock))
                                                          ));
                DataSet ds = new DataSet();
                ds.ReadXml(new StringReader(xmlEle.ToString()));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        long pid = Convert.ToInt64(ds.Tables[0].Rows[i]["Prod_ID"].ToString());
                        decimal RQuantity = Convert.ToDecimal(ds.Tables[0].Rows[i]["RequestQty"].ToString());
                        decimal AvlBlc = Convert.ToDecimal(ds.Tables[0].Rows[i]["AvailableBalance"].ToString());
                      //  decimal crntStk = 0;

                         decimal crntStk = objService.GetCurrentStockofProduct_New(pid, profile.DBConnection._constr);

                        if (RQuantity == 0)
                        {
                            found = true;
                            break;
                        }
                        if (AvlBlc > crntStk)
                        {
                            found = true;
                            break;
                        }
                    }
                }
                if (found == true)
                {
                    WebMsgBox.MsgBox.Show("Request Quantity Not Equal to 0!");
                    result = -5;
                }
                else
                {
                    Dictionary<string, object> dictionary = new Dictionary<string, object>();
                    dictionary = (Dictionary<string, object>)objReq;

                    long CreatedBy = 0;
                    DateTime Creationdate1 = new DateTime();
                    string Creationdate = "";
                    string LastModifiedDt = "";
                    long Id = 0; long LastModifiedBy = 0;
                    DateTime LastModifiedDt1 = new DateTime();
                    string OrderNo = "";


                    if (HttpContext.Current.Session["PORRequestID"] != null)
                    {
                        if (HttpContext.Current.Session["PORRequestID"].ToString() == "0")
                        {
                              CreatedBy = profile.Personal.UserID;

                            Creationdate1 = DateTime.Now;
                            Creationdate = Creationdate1.ToString("yyyy-MM-dd HH:mm:ss.fff");

                            Creationdate = Creationdate.Replace('.', ':');  // Replacing periods with colons

                                                                            //  Creationdate1 = DateTime.Now.Date;
                                                                            //Creationdate = Creationdate1.ToString("yyyy-MM-dd HH:mm:ss.fff");


                            // Creationdate = DateTime.Parse(Creationdate12);

                            /* Creationdate = DateTime.ParseExact(Creationdate12, "yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                             //Creationdate = Creationdate12;
                             Creationdate.ToString("yyyy-MM-dd HH:mm:ss.fff");*/
                        }
                        else
                        {
                             Id = Convert.ToInt64(HttpContext.Current.Session["PORRequestID"].ToString());
                            PreviousStatusID = objService.GetPreviousStatusID_New(Id, profile.DBConnection._constr);
                             LastModifiedBy = profile.Personal.UserID;
                           
                            LastModifiedDt1 = DateTime.Now;
                            LastModifiedDt = LastModifiedDt1.ToString("yyyy-MM-dd HH:mm:ss.fff");
                        }
                       
                        long StoreId = Convert.ToInt64(dictionary["StoreId"]);
                        string OrderNumber = dictionary["OrderNumber"].ToString();
                        string Priority = dictionary["Priority"].ToString();
                        long Status = Convert.ToInt64(dictionary["Status"]);
                        string Title = dictionary["Title"].ToString();

                        DateTime Orderdate1 = new DateTime();
                        Orderdate1 = Convert.ToDateTime(DateTime.Now.ToShortDateString());   //ToShortDateString
                        string Orderdate2 = Orderdate1.ToString("yyyy-MM-dd HH:mm:ss.fff");
                        Orderdate2 = Orderdate2.Replace('.', ':');  // Replacing periods with colons
                        //DateTime Orderdate1 = new DateTime();
                        //Orderdate1 = Convert.ToDateTime(DateTime.Now.ToShortDateString());   //ToShortDateString
                        //string Orderdate2 = Orderdate1.ToString("yyyy-MM-dd HH:mm:ss.fff");
                      //  DateTime Orderdate = DateTime.ParseExact(Orderdate2, "yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);

                        long RequestBy = Convert.ToInt64(dictionary["RequestBy"]);
                        string Remark = dictionary["Remark"].ToString();

                        DateTime Deliverydate1 = new DateTime();
                        Deliverydate1 = Convert.ToDateTime(dictionary["Deliverydate"]);
                        string Deliverydate2 = Deliverydate1.ToString("yyyy-MM-dd HH:mm:ss.fff");
                        Deliverydate2 = Deliverydate2.Replace('.', ':');  // Replacing periods with colons

                        /* DateTime Deliverydate1 = new DateTime();
                         Deliverydate1 = Convert.ToDateTime(dictionary["Deliverydate"]);
                         string Deliverydate2 = Deliverydate1.ToString("yyyy-MM-dd HH:mm:ss.fff");*/
                        // DateTime Deliverydate = DateTime.ParseExact(Deliverydate2, "yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);


                        long ContactId1 = Convert.ToInt64(dictionary["ContactId1"].ToString());
                        long AddressId = Convert.ToInt64(dictionary["AddressId"].ToString());
                        string Con2 = dictionary["ContactId2"].ToString();
                        long PaymentID = Convert.ToInt64(dictionary["PaymentID"].ToString());
                        decimal TotalQty = Convert.ToDecimal(dictionary["TotalQty"].ToString());
                        decimal GrandTotal = Convert.ToDecimal(dictionary["GrandTotal"].ToString());
                        long LocationID = Convert.ToInt64(dictionary["LocationID"].ToString());
                        long ProjTypeID = Convert.ToInt64(dictionary["ProjTypeID"].ToString());
                        long SiteID = Convert.ToInt64(dictionary["SiteID"].ToString());
                        string ASNNo = "";
                        string RefundCode = "";

                        

                        if (Convert.ToInt64(dictionary["Status"]) == 1) { }
                        else
                        {
                             OrderNo = objService.GetNewOrderNo(Convert.ToInt64(dictionary["StoreId"]), profile.DBConnection._constr);
                        }
                        string orderType = "OMS";
                        long segmentID = Convert.ToInt64(dictionary["SegmentID"].ToString());

                        //  long RequestID = objService.SetIntoPartRequestHead(RequestHead, profile.DBConnection._constr);

                        // RequestID = objService.SetIntotOrderHead(RequestHead, profile.DBConnection._constr);*


                       RequestID = objService.SetIntotOrderHead_New(CreatedBy, Creationdate, Id, LastModifiedBy, LastModifiedDt, StoreId, OrderNumber, Priority, Status,
                              Title, Orderdate2, RequestBy, Remark, Deliverydate2, ContactId1, AddressId, Con2, PaymentID, TotalQty, GrandTotal, LocationID, ProjTypeID,
                              SiteID, ASNNo, RefundCode, OrderNo, orderType, segmentID, profile.DBConnection._constr);

                        if (RequestID > 0)
                        {

                            RSLT = objService.FinalSaveRequestPartDetail_New(HttpContext.Current.Session.SessionID, ObjectName, RequestID, profile.Personal.UserID.ToString(), Convert.ToInt32(Status), Convert.ToInt64(StoreId), PreviousStatusID, profile.DBConnection._constr);

                            #region ERP Service
                            string autoapproval = "False";
                            autoapproval = objService.CheckStoreERPAutoApproved(Convert.ToInt64(StoreId), profile.DBConnection._constr);
                            DataSet dsh = new DataSet();
                            dsh = objService.getErpOrderhead(RequestID, profile.DBConnection._constr);
                            long statusID = 0;
                            if (dsh.Tables[0].Rows.Count > 0)
                            {
                                statusID = Convert.ToInt64(dsh.Tables[0].Rows[0]["StatusID"].ToString());
                            }
                            if (autoapproval == "True" && statusID != 31)
                            {
                                string json = string.Empty;
                                dynamic dynamicJSON = new ExpandoObject();
                                HttpClient objclient = new HttpClient();
                                List<orderDetailList> orddetaillst = new List<orderDetailList>();
                                orderDetailList ord = new orderDetailList();
                                ord.orderId = dsh.Tables[0].Rows[0]["OrderID"].ToString();
                                ord.orderType = dsh.Tables[0].Rows[0]["Ordertype"].ToString();
                                ord.orderDate = dsh.Tables[0].Rows[0]["Orderdate"].ToString();
                                ord.orderCreationDate = dsh.Tables[0].Rows[0]["Ordercreationdate"].ToString();
                                ord.locationCode = dsh.Tables[0].Rows[0]["locationcode"].ToString(); ;
                                ord.locationName = dsh.Tables[0].Rows[0]["locationname"].ToString();
                                ord.orderCurrency = "";
                                DataSet dsord = new DataSet();
                                dsord = objService.getErpOrderdetail(RequestID, profile.DBConnection._constr);
                                List<orderLines> ordlnlst = new List<orderLines>();
                                for (int j = 0; j < dsord.Tables[0].Rows.Count; j++)
                                {
                                    orderLines ordLn = new orderLines();
                                    ordLn.lineNumber = dsord.Tables[0].Rows[j]["sequence"].ToString();
                                    ordLn.skuCode = dsord.Tables[0].Rows[j]["Skucode"].ToString();
                                    ordLn.lineAmount = decimal.Parse(dsord.Tables[0].Rows[j]["LineAmount"].ToString());
                                    ordLn.lineQuantity = decimal.Parse(dsord.Tables[0].Rows[j]["LineQuantity"].ToString());
                                    ordlnlst.Add(ordLn);
                                }
                                ord.orderLines = ordlnlst;
                                orddetaillst.Add(ord);
                                json = JsonConvert.SerializeObject(orddetaillst);
                                char[] MyChar = { '[', ']' };
                                json = json.TrimStart(MyChar);
                                json = json.TrimEnd(MyChar);
                                //string username = "muhammadrafay.khan@evosysglobal.com";
                                //string password = "Welcome@123456";
                                string username = "muhammadrafay.khan@evosysglobal.com";
                                string password = "Vodafone@1234";  //28-01-2025
                                string svcCredentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(username + ":" + password));

                                var clienttokan = new RestClient("https://erp-oic-instance-vodafoneqatar-fr.integration.ocp.oraclecloud.com/ic/api/integration/v1/flows/rest/INT201_INPUT_OMS_DATA/1.0/oms/invoice/process");
                                clienttokan.Authenticator = new HttpBasicAuthenticator(username, password);
                                var reqtokan = new RestRequest(Method.POST);
                                reqtokan.AddParameter("application/json;", json, ParameterType.RequestBody);
                                //reqtokan.AddHeader("Authentication", "Basic" + svcCredentials);
                                //reqtokan.AddHeader("Username", username);
                                //reqtokan.AddHeader("Password", password);
                                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls;
                                IRestResponse response = clienttokan.Execute(reqtokan);
                                string status = Convert.ToString(response.StatusCode);
                                if (status != "Accepted")
                                {
                                    objService.Savefailedaprorder(RequestID, status, status, profile.DBConnection._constr);
                                }
                                else
                                {
                                    objService.InsetERPOrderNotificationLog(RequestID, profile.Personal.UserID, Convert.ToInt64(StoreId), profile.DBConnection._constr);
                                    ///created order email notification logic
                                }
                            }

                            #endregion
                            string ChkSerialedCompany = objService.ChkSerialedCompanyDeptID(Convert.ToInt64(StoreId), profile.DBConnection._constr);
                            if (ChkSerialedCompany == "Yes")
                            {
                                RSLT = objService.FinalSaveRequestPartDetailSerial(HttpContext.Current.Session.SessionID, ObjectNameSerial, RequestID, profile.Personal.UserID.ToString(), Convert.ToInt64(StoreId), PreviousStatusID, profile.DBConnection._constr);
                            }
                            if (RSLT == 1 || RSLT == 2) { result = RequestID; }  //"Request saved successfully";
                            else if (RSLT == 3) { result = -3; } //"Request saved successfully. Email Notification Failed..."
                            else if (RSLT == 0) { result = 0; }  //"Some error occurred";

                            // objService.UpdateStatutoryDetails(RequestID, profile.DBConnection._constr); //Update StatutoryDetails
                            iUC_AttachDocumentClient DocumentSourceClient = new iUC_AttachDocumentClient();//Document Save
                            DocumentSourceClient.FinalSaveToDBtDocument(HttpContext.Current.Session.SessionID, RequestID, profile.Personal.UserID.ToString(), ObjectName + "Document", HttpRuntime.AppDomainAppPath.ToString(), profile.DBConnection._constr);
                        }
                        //if (PreviousStatusID == 2)
                        //{
                        //    System.Web.HttpResponse Response = System.Web.HttpContext.Current.Response;
                        //    Response.Write("<script>");
                        //    Response.Write("window.open('../PowerOnRent/Approval.aspx?REQID='" + RequestID + "'&ST=24', null, 'height=180px, width=595px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=300; screenY=50');");
                        //    Response.Write("</script>");
                        //}

                    }
                }
            }
            catch
            (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, "PartRequestEntry.aspx", "WMSaveRequestHead");
                result = 0; // "Some error occurred";
            }
            finally { objService.Close(); HttpContext.Current.Session["TemplateID"] = "0"; HttpContext.Current.Session["OrderID"] = RequestID; }
            if (PreviousStatusID == 2)
            {
                // result = HttpContext.Current.Session["PORRequestID"].ToString();
            }
            return result;
        }

        [WebMethod]
        public static long WMSaveRequestHead(object objReq)
        {
            long result = 0;
            long PreviousStatusID = 0;
            int RSLT = 0; long RequestID = 0;
            iPartRequestClient objService = new iPartRequestClient();
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                List<POR_SP_GetPartDetail_ForRequest_Result> RequestPartList = new List<POR_SP_GetPartDetail_ForRequest_Result>();
                RequestPartList = objService.GetExistingTempDataBySessionIDObjectName(HttpContext.Current.Session.SessionID, profile.Personal.UserID.ToString(), ObjectName, profile.DBConnection._constr).ToList();
                //RequestPartList.Select( )
                bool found = false;
                XElement xmlEle = new XElement("Request", from rec in RequestPartList
                                                          select new XElement("PartList",
                                                          new XElement("Prod_ID", Convert.ToInt64(rec.Prod_ID)),
                                                          new XElement("RequestQty", Convert.ToDecimal(rec.RequestQty)),
                                                          new XElement("AvailableBalance", Convert.ToDecimal(rec.CurrentStock))
                                                          ));

                DataSet ds = new DataSet();
                ds.ReadXml(new StringReader(xmlEle.ToString()));

                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        long pid = Convert.ToInt64(ds.Tables[0].Rows[i]["Prod_ID"].ToString());
                        decimal RQuantity = Convert.ToDecimal(ds.Tables[0].Rows[i]["RequestQty"].ToString());
                        decimal AvlBlc = Convert.ToDecimal(ds.Tables[0].Rows[i]["AvailableBalance"].ToString());

                        //decimal crntStk = objService.GetCurrentStockofProduct(pid, profile.DBConnection._constr);
                        decimal crntStk = objService.GetCurrentStockofProduct_New(pid, profile.DBConnection._constr);

                        if (RQuantity == 0)
                        {
                            found = true;
                            break;
                        }
                        if (AvlBlc > crntStk)
                        {
                            found = true;
                            break;
                        }
                    }
                }

                if (found == true)
                {
                    WebMsgBox.MsgBox.Show("Request Quantity Not Equal to 0!");
                    result = -5;
                }
                else
                {
                    tOrderHead RequestHead = new tOrderHead();
                    //PORtPartRequestHead RequestHead = new PORtPartRequestHead();
                    Dictionary<string, object> dictionary = new Dictionary<string, object>();
                    dictionary = (Dictionary<string, object>)objReq;

                    if (HttpContext.Current.Session["PORRequestID"] != null)
                    {
                        if (HttpContext.Current.Session["PORRequestID"].ToString() == "0")
                        {
                            RequestHead.CreatedBy = profile.Personal.UserID;
                            // RequestHead.CreationDt = DateTime.Now;
                            RequestHead.Creationdate = DateTime.Now;
                        }
                        else
                        {
                            //RequestHead = objService.GetRequestHeadByRequestID(Convert.ToInt64(HttpContext.Current.Session["PORRequestID"].ToString()), profile.DBConnection._constr);
                            RequestHead.Id = Convert.ToInt64(HttpContext.Current.Session["PORRequestID"].ToString());
                            PreviousStatusID = objService.GetPreviousStatusID(RequestHead.Id, profile.DBConnection._constr);
                            RequestHead.LastModifiedBy = profile.Personal.UserID;
                            RequestHead.LastModifiedDt = DateTime.Now;
                        }
                        RequestHead.StoreId = Convert.ToInt64(dictionary["StoreId"]);
                        RequestHead.OrderNumber = dictionary["OrderNumber"].ToString();
                        RequestHead.Priority = dictionary["Priority"].ToString();
                        RequestHead.Status = Convert.ToInt64(dictionary["Status"]);
                        RequestHead.Title = dictionary["Title"].ToString();
                        //RequestHead.Orderdate = Convert.ToDateTime(dictionary["Orderdate"]);
                        RequestHead.Orderdate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                        RequestHead.RequestBy = Convert.ToInt64(dictionary["RequestBy"]);
                        RequestHead.Remark = dictionary["Remark"].ToString();
                        RequestHead.Deliverydate = Convert.ToDateTime(dictionary["Deliverydate"]);
                        RequestHead.ContactId1 = Convert.ToInt64(dictionary["ContactId1"].ToString());
                        // RequestHead.ContactId2 = Convert.ToInt64(dictionary["ContactId2"].ToString());
                        RequestHead.AddressId = Convert.ToInt64(dictionary["AddressId"].ToString());
                        RequestHead.Con2 = dictionary["ContactId2"].ToString();
                        RequestHead.PaymentID = Convert.ToInt64(dictionary["PaymentID"].ToString());
                        RequestHead.TotalQty = Convert.ToDecimal(dictionary["TotalQty"].ToString());
                        RequestHead.GrandTotal = Convert.ToDecimal(dictionary["GrandTotal"].ToString());
                        RequestHead.LocationID = Convert.ToInt64(dictionary["LocationID"].ToString());
                        RequestHead.ProjTypeID = Convert.ToInt64(dictionary["ProjTypeID"].ToString());
                        RequestHead.SiteID = Convert.ToInt64(dictionary["SiteID"].ToString());
                        RequestHead.ASNNo = "";
                        RequestHead.RefundCode = "";

                        if (Convert.ToInt64(dictionary["Status"]) == 1) { }
                        else
                        {
                            RequestHead.OrderNo = objService.GetNewOrderNo(Convert.ToInt64(dictionary["StoreId"]), profile.DBConnection._constr);
                        }
                        RequestHead.orderType = "OMS";
                        RequestHead.segmentID= Convert.ToInt64(dictionary["SegmentID"].ToString());

                        //  long RequestID = objService.SetIntoPartRequestHead(RequestHead, profile.DBConnection._constr);
                        RequestID = objService.SetIntotOrderHead(RequestHead, profile.DBConnection._constr);

                        if (RequestID > 0)
                        {

                            RSLT = objService.FinalSaveRequestPartDetail(HttpContext.Current.Session.SessionID, ObjectName, RequestID, profile.Personal.UserID.ToString(), Convert.ToInt32(RequestHead.Status), Convert.ToInt64(RequestHead.StoreId), PreviousStatusID, profile.DBConnection._constr);

                            #region ERP Service
                            string autoapproval = "False";
                            autoapproval = objService.CheckStoreERPAutoApproved(Convert.ToInt64(RequestHead.StoreId), profile.DBConnection._constr);
                            DataSet dsh = new DataSet();
                            dsh = objService.getErpOrderhead(RequestID, profile.DBConnection._constr);
                            long statusID = 0;
                            if (dsh.Tables[0].Rows.Count > 0)
                            {
                                statusID = Convert.ToInt64(dsh.Tables[0].Rows[0]["StatusID"].ToString());
                            }
                            if (autoapproval == "True" && statusID != 31)
                            {
                                string json = string.Empty;
                                dynamic dynamicJSON = new ExpandoObject();
                                HttpClient objclient = new HttpClient();
                                List<orderDetailList> orddetaillst = new List<orderDetailList>();
                                orderDetailList ord = new orderDetailList();
                                ord.orderId = dsh.Tables[0].Rows[0]["OrderID"].ToString();
                                ord.orderType = dsh.Tables[0].Rows[0]["Ordertype"].ToString();
                                ord.orderDate = dsh.Tables[0].Rows[0]["Orderdate"].ToString();
                                ord.orderCreationDate = dsh.Tables[0].Rows[0]["Ordercreationdate"].ToString();
                                ord.locationCode = dsh.Tables[0].Rows[0]["locationcode"].ToString(); ;
                                ord.locationName = dsh.Tables[0].Rows[0]["locationname"].ToString();
                                ord.orderCurrency = "";
                                DataSet dsord = new DataSet();
                                dsord = objService.getErpOrderdetail(RequestID, profile.DBConnection._constr);
                                List<orderLines> ordlnlst = new List<orderLines>();
                                for (int j = 0; j < dsord.Tables[0].Rows.Count; j++)
                                {
                                    orderLines ordLn = new orderLines();
                                    ordLn.lineNumber = dsord.Tables[0].Rows[j]["sequence"].ToString();
                                    ordLn.skuCode = dsord.Tables[0].Rows[j]["Skucode"].ToString();
                                    ordLn.lineAmount = decimal.Parse(dsord.Tables[0].Rows[j]["LineAmount"].ToString());
                                    ordLn.lineQuantity = decimal.Parse(dsord.Tables[0].Rows[j]["LineQuantity"].ToString());
                                    ordlnlst.Add(ordLn);                                }
                                ord.orderLines = ordlnlst;
                                orddetaillst.Add(ord);
                                json = JsonConvert.SerializeObject(orddetaillst);
                                char[] MyChar = { '[', ']' };
                                json = json.TrimStart(MyChar);
                                json = json.TrimEnd(MyChar);
                                string username = "muhammadrafay.khan@evosysglobal.com";
                                //string password = "Welcome@123456";
                                string password = "Vodafone@1234";
                                string svcCredentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(username + ":" + password));

                                var clienttokan = new RestClient("https://erp-oic-instance-vodafoneqatar-fr.integration.ocp.oraclecloud.com/ic/api/integration/v1/flows/rest/INT201_INPUT_OMS_DATA/1.0/oms/invoice/process");
                                clienttokan.Authenticator = new HttpBasicAuthenticator(username, password);
                                var reqtokan = new RestRequest(Method.POST);
                                reqtokan.AddParameter("application/json;", json, ParameterType.RequestBody);
                                //reqtokan.AddHeader("Authentication", "Basic" + svcCredentials);
                                //reqtokan.AddHeader("Username", username);
                                //reqtokan.AddHeader("Password", password);
                                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls;
                                IRestResponse response = clienttokan.Execute(reqtokan);
                                string status = Convert.ToString(response.StatusCode);
                                if (status != "Accepted")
                                {
                                    objService.Savefailedaprorder(RequestID, status, status, profile.DBConnection._constr);
                                }
                                else
                                {
                                    objService.InsetERPOrderNotificationLog(RequestID,profile.Personal.UserID, Convert.ToInt64(RequestHead.StoreId), profile.DBConnection._constr);
                                    ///created order email notification logic
                                }
                            }

                            #endregion
                            string ChkSerialedCompany = objService.ChkSerialedCompanyDeptID(Convert.ToInt64(RequestHead.StoreId), profile.DBConnection._constr);
                            if (ChkSerialedCompany == "Yes")
                            {
                                RSLT = objService.FinalSaveRequestPartDetailSerial(HttpContext.Current.Session.SessionID, ObjectNameSerial, RequestID, profile.Personal.UserID.ToString(), Convert.ToInt64(RequestHead.StoreId), PreviousStatusID, profile.DBConnection._constr);
                            }
                            if (RSLT == 1 || RSLT == 2) { result = RequestID; }  //"Request saved successfully";
                            else if (RSLT == 3) { result = -3; } //"Request saved successfully. Email Notification Failed..."
                            else if (RSLT == 0) { result = 0; }  //"Some error occurred";

                            // objService.UpdateStatutoryDetails(RequestID, profile.DBConnection._constr); //Update StatutoryDetails
                            iUC_AttachDocumentClient DocumentSourceClient = new iUC_AttachDocumentClient();//Document Save
                            DocumentSourceClient.FinalSaveToDBtDocument(HttpContext.Current.Session.SessionID, RequestID, profile.Personal.UserID.ToString(), ObjectName + "Document", HttpRuntime.AppDomainAppPath.ToString(), profile.DBConnection._constr);
                        }
                        //if (PreviousStatusID == 2)
                        //{
                        //    System.Web.HttpResponse Response = System.Web.HttpContext.Current.Response;
                        //    Response.Write("<script>");
                        //    Response.Write("window.open('../PowerOnRent/Approval.aspx?REQID='" + RequestID + "'&ST=24', null, 'height=180px, width=595px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=300; screenY=50');");
                        //    Response.Write("</script>");
                        //}

                    }
                }
            }
            catch
            (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, "PartRequestEntry.aspx", "WMSaveRequestHead");
                result = 0; // "Some error occurred";
            }
            finally { objService.Close(); HttpContext.Current.Session["TemplateID"] = "0"; HttpContext.Current.Session["OrderID"] = RequestID; }
            if (PreviousStatusID == 2)
            {
                // result = HttpContext.Current.Session["PORRequestID"].ToString();
            }
            return result;
        }


        public static void SendMail(string MailBody, string MailSubject, string ToEmailIDs)
        {
            try
            {
                //SmtpClient smtpClient = new SmtpClient("smtpout.asia.secureserver.net", 25);
                //  SmtpClient smtpClient = new SmtpClient("10.228.134.54", 25);
                SmtpClient smtpClient = new SmtpClient("mail.brilliantinfosys.com", 587);
                MailMessage message = new MailMessage();


                // MailAddress fromAddress = new MailAddress("admin@brilliantinfosys.com", "GWC");
                // MailAddress fromAddress = new MailAddress("OMSTest@gulfwarehousing.com", "GWC");
                MailAddress fromAddress = new MailAddress("suraj@brilliantinfosys.com", "GWC");

                //From address will be given as a MailAddress Object
                message.From = fromAddress;

                //To address collection of MailAddress
                message.To.Add(ToEmailIDs);
                message.Subject = MailSubject;

                //Body can be Html or text format
                //Specify true if it  is html message
                message.IsBodyHtml = true;

                //Message body content
                message.Body = MailBody;

                smtpClient.EnableSsl = false;
                //Send SMTP mail
                smtpClient.UseDefaultCredentials = false;
                NetworkCredential basicCredential = new NetworkCredential("suraj@brilliantinfosys.com", "6march1986");
                /// NetworkCredential basicCredential = new NetworkCredential("OMSTest@gulfwarehousing.com", "");
                smtpClient.Credentials = basicCredential;

                smtpClient.Send(message);
            }
            catch { }
        }
        protected void divVisibility()
        {
            CustomProfile profile = CustomProfile.GetProfile();
            iPartRequestClient objService = new iPartRequestClient();
            string chkordisgps = "";

            divApprovalHead.Attributes.Add("style", "display:none");
            divApprovalDetail.Attributes.Add("style", "display:none");

            divIssueHead.Attributes.Add("style", "display:none");
            divIssueDetail.Attributes.Add("style", "display:none");

            divCorrespondanceHead.Attributes.Add("style", "display:none");
            divCorrespondanceDetails.Attributes.Add("style", "display:none");

            if (ddlStatus.SelectedIndex == 2 || ddlStatus.SelectedValue == "22" || ddlStatus.SelectedValue == "21" || ddlStatus.SelectedValue == "2" || ddlStatus.SelectedValue == "21" || ddlStatus.SelectedValue == "4" || ddlStatus.SelectedIndex == 21 || ddlStatus.SelectedIndex == 22 || ddlStatus.SelectedIndex == 4 || ddlStatus.SelectedValue == "31" || ddlStatus.SelectedValue == "32")
            {
                divApprovalDetail.Attributes.Add("style", "display:'';");
                linkRequest.Attributes["innerHTML"] = "Collapse";
                linkRequest.InnerText = "Expand";
                divRequestDetail.Attributes["class"] = "divDetailCollapse";

                linkIssueDetail.Attributes.Add("InnerHtml", "Expand");
                linkIssueDetail.InnerText = "Expand";
                divIssueDetail.Attributes.Add("class", "divDetailCollapse");

                divCorrespondanceHead.Attributes.Add("style", "display:''");
                divCorrespondanceDetails.Attributes.Add("style", "display:''");

                linkCorrespondanceDetail.Attributes.Add("InnerHtml", "Expand");
                linkCorrespondanceDetail.InnerText = "Expand";
                divCorrespondanceDetails.Attributes.Add("class", "divDetailCollapse");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "changemodeRequest" + Session.SessionID, "changemode(true, 'OrderHead');LoadingOff();", true);

                Toolbar1.SetSaveRight(false, "Not Allowed");
                Toolbar1.SetClearRight(false, "Not Allowed");
                Toolbar1.SetEditRight(false, "Not Allowed");
            }
            if (ddlStatus.SelectedIndex == 3 || ddlStatus.SelectedValue == "7" || ddlStatus.SelectedValue == "10" || ddlStatus.SelectedValue == "8" || ddlStatus.SelectedValue == "25" || ddlStatus.SelectedValue == "26" || ddlStatus.SelectedValue == "28" || ddlStatus.SelectedValue == "34" || ddlStatus.SelectedValue == "35" || ddlStatus.SelectedValue == "37" || ddlStatus.SelectedValue == "38" || ddlStatus.SelectedValue == "39" || ddlStatus.SelectedValue == "10036"|| ddlStatus.SelectedValue=="10037" || ddlStatus.SelectedValue == "10038" || ddlStatus.SelectedValue == "10039" || ddlStatus.SelectedValue == "10040" || ddlStatus.SelectedValue == "10041")
            {
                divIssueHead.Attributes.Add("style", "display:'';");
                divIssueDetail.Attributes.Add("style", "display:'';");

                linkRequest.Attributes["innerHTML"] = "Expand";
                linkRequest.InnerText = "Expand";
                divRequestDetail.Attributes["class"] = "divDetailCollapse";

                divApprovalHead.Attributes.Add("style", "display:'';");
                divApprovalDetail.Attributes.Add("style", "display:'';");

                linkApprovalDetail.Attributes["innerHTML"] = "Expand";
                linkApprovalDetail.InnerText = "Expand";
                divApprovalDetail.Attributes["class"] = "divDetailCollapse";

                divCorrespondanceHead.Attributes.Add("style", "display:''");
                divCorrespondanceDetails.Attributes.Add("style", "display:''");

                linkCorrespondanceDetail.Attributes["innerHTML"] = "Expand";
                linkCorrespondanceDetail.InnerText = "Expand";
                divCorrespondanceDetails.Attributes["class"] = "divDetailCollapse";


                // ScriptManager.RegisterStartupScript(this, this.GetType(), "changemodeRequest" + Session.SessionID, "changemode(true, 'divRequestDetail');LoadingOff();", true);
                if (chkordisgps == "Yes")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "changemodeRequest" + Session.SessionID, "changemode(true, 'OrderHeadGps');LoadingOff();", true);

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "changemodeRequest" + Session.SessionID, "changemode(true, 'OrderHead');LoadingOff();", true);
                }
                Toolbar1.SetSaveRight(false, "Not Allowed");
                Toolbar1.SetClearRight(false, "Not Allowed");
                Toolbar1.SetEditRight(false, "Not Allowed");
            }

            if (ddlStatus.Items.Count > 0)
            {

                if (Convert.ToInt32(ddlStatus.SelectedItem.Value) == 3 || Convert.ToInt32(ddlStatus.SelectedItem.Value) == 4 || Convert.ToInt32(ddlStatus.SelectedItem.Value) == 21 || Convert.ToInt32(ddlStatus.SelectedItem.Value) == 22 || Convert.ToInt32(ddlStatus.SelectedItem.Value) == 2 || Convert.ToInt32(ddlStatus.SelectedItem.Value) == 31 || Convert.ToInt32(ddlStatus.SelectedItem.Value) == 32)
                {
                    Toolbar1.SetSaveRight(false, "Not Allowed");
                    Toolbar1.SetClearRight(false, "Not Allowed");
                    Toolbar1.SetEditRight(false, "Not Allowed");
                }
                if (Convert.ToInt32(ddlStatus.SelectedItem.Value) == 1)
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "changemodeRequest" + Session.SessionID, "changemode(false, 'OrderHead');LoadingOff();", true);

                    Toolbar1.SetSaveRight(true, "Not Allowed");
                    Toolbar1.SetClearRight(false, "Not Allowed");

                }

            }

            if (Session["PORstate"] != null)
            {
                if (Session["PORstate"].ToString() == "Add")
                {

                    if (ddlStatus.Items.Count > 0) ddlStatus.SelectedIndex = 2;
                    lblRequestNo.Text = "Generate when Save";
                    UC_DateRequestDate.Date = DateTime.Now;

                    DateTime crntdt = Convert.ToDateTime(DateTime.Now.ToShortDateString());

                    txtRequestDate.Text = crntdt.ToString("dd-MMM-yyyy");// .ToShortDateString();


                    ddlRequestByUserID.DataSource = null;
                    ddlRequestByUserID.DataBind();

                    if (hdnselectedDept.Value == "") hdnselectedDept.Value = "0";



                    GetUserbyDepartment(Convert.ToInt64(hdnselectedDept.Value));

                    ddlRequestByUserID.SelectedIndex = ddlRequestByUserID.Items.IndexOf(ddlRequestByUserID.Items.FindByValue(profile.Personal.UserID.ToString()));


                    if (profile.Personal.UserType == "User")
                    {
                        ddlRequestByUserID.Enabled = false;
                    }

                    if (hdnselectedCompany.Value.ToString() == "" || hdnselectedCompany.Value == null)
                    {
                        hdnselectedCompany.Value = "0";
                    }
                    iUCCommonFilterClient UCCommonFilter = new iUCCommonFilterClient();
                    string ISProjectSiteDetails = "";
                    ISProjectSiteDetails = UCCommonFilter.ISProjectSiteDetails(Convert.ToString(hdnselectedCompany.Value), profile.DBConnection._constr);
                    //if (hdnselectedCompany.Value.ToString() == Convert.ToString("10266"))
                    if (ISProjectSiteDetails == "Yes")                   // if (hdnselectedCompany.Value == "10266")
                    {
                        hdnISProjectSiteDetails.Value = "Yes";
                        if (ddlStatus.SelectedValue == "2" || ddlStatus.SelectedValue == "21" || ddlStatus.SelectedValue == "22" || ddlStatus.SelectedValue == "31" || ddlStatus.SelectedValue == "32")
                        {

                            string usertype = profile.Personal.UserType;
                            if (usertype == "Approver" || usertype == "Requestor And Approver")
                            {
                                DataSet dsapp = new DataSet();
                                dsapp = objService.GetApprovalDetailsNew(Convert.ToInt64(Session["PORRequestID"]), profile.Personal.UserID, profile.DBConnection._constr);
                                if (dsapp.Tables[0].Rows.Count > 0)
                                {
                                    Toolbar1.SetSaveRight(true, "Allowed");
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "changemode" + Session.SessionID, "changemode(false, 'OrderHead');LoadingOff();", true);


                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "EbabledTrRow" + Session.SessionID, "EbabledTrRow(true);", true);
                                    Page.ClientScript.RegisterStartupScript(this.GetType(), "EbabledTrRow1", "EbabledTrRow1();", true);
                                }
                            }
                        }
                    }
                    else
                    {
                        hdnISProjectSiteDetails.Value = "No";
                    }

                    hdnselectedsiteid.Value = "";
                    hdnselectedprojectid.Value = "";
                    hdnISProjectSiteDetails.Value = "";

                }
            }

            //only vodafone technical user show save button
            if (Session["PORRequestID"] != null)
            {
                if (hdnselectedCompany.Value.ToString() == "" || hdnselectedCompany.Value == null)
                {
                    hdnselectedCompany.Value = "0";
                }
                iUCCommonFilterClient UCCommonFilter = new iUCCommonFilterClient();
                string ISProjectSiteDetails = "";
                ISProjectSiteDetails = UCCommonFilter.ISProjectSiteDetails(Convert.ToString(hdnselectedCompany.Value), profile.DBConnection._constr);
                //if (hdnselectedCompany.Value.ToString() == Convert.ToString("10266"))
                if (ISProjectSiteDetails == "Yes")                   // if (hdnselectedCompany.Value == "10266")
                {
                    hdnISProjectSiteDetails.Value = "Yes";
                    if (ddlStatus.SelectedValue == "2" || ddlStatus.SelectedValue == "21" || ddlStatus.SelectedValue == "22" || ddlStatus.SelectedValue == "31" || ddlStatus.SelectedValue == "32")
                    {

                        string usertype = profile.Personal.UserType;
                        if (usertype == "Approver" || usertype == "Requestor And Approver")
                        {
                            DataSet dsapp = new DataSet();
                            dsapp = objService.GetApprovalDetailsNew(Convert.ToInt64(Session["PORRequestID"]), profile.Personal.UserID, profile.DBConnection._constr);
                            if (dsapp.Tables[0].Rows.Count > 0)
                            {
                                Toolbar1.SetSaveRight(true, "Allowed");
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "changemode" + Session.SessionID, "changemode(false, 'OrderHead');LoadingOff();", true);


                                ScriptManager.RegisterStartupScript(this, this.GetType(), "EbabledTrRow" + Session.SessionID, "EbabledTrRow(true);", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "EbabledTrRow1", "EbabledTrRow1();", true);
                            }
                        }

                    }
                }
                else
                {
                    hdnISProjectSiteDetails.Value = "No";
                }
                //suraj
                string ChkISOrdCreator = objService.ChkISOrdCreatorOrNot(Convert.ToInt64(HttpContext.Current.Session["PORRequestID"].ToString()), profile.Personal.UserID, profile.DBConnection._constr);
                if (ChkISOrdCreator == "Yes")
                {
                    Toolbar1.SetSaveRight(true, "Allowed");
                }
            }
        }


        #endregion

        #region Request Part Detail
        protected void FillGrid1ByRequestID(long RequestID, long SiteID)
        {
            iPartRequestClient objService = new iPartRequestClient();
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                List<POR_SP_GetPartDetail_ForRequest_Result> RequestPartList = new List<POR_SP_GetPartDetail_ForRequest_Result>();
                RequestPartList = objService.GetRequestPartDetailByRequestID(RequestID, SiteID, Session.SessionID, profile.Personal.UserID.ToString(), ObjectName, profile.DBConnection._constr).ToList();
                Grid1.DataSource = RequestPartList;
                string ChkSerialedCompany = "";
                if (hdnselectedCompany.Value == "" || hdnselectedCompany.Value == null) { hdnselectedCompany.Value = "0"; }
                ChkSerialedCompany = objService.ChkSerialedCompany(Convert.ToInt64(hdnselectedCompany.Value), profile.DBConnection._constr);
                if (profile.Personal.UserType == "Super Admin")
                {
                    Grid1.Columns[0].Visible = false; Grid1.Columns[6].Visible = false; Grid1.Columns[11].Visible = false;
                    Grid1.Columns[18].Visible = false; Grid1.Columns[21].Visible = false;
                    Grid1.Columns[1].Visible = true; Grid1.Columns[2].Visible = true; Grid1.Columns[3].Visible = true;
                    Grid1.Columns[4].Visible = true; Grid1.Columns[5].Visible = true; Grid1.Columns[7].Visible = true;
                    Grid1.Columns[8].Visible = true; Grid1.Columns[9].Visible = true; Grid1.Columns[10].Visible = true;
                    Grid1.Columns[12].Visible = true;
                    Grid1.Columns[13].Visible = true;
                    if (hdnordertype.Value == "E-Commerce Order")
                    {
                        Grid1.Columns[14].Visible = true;
                        Grid1.Columns[15].Visible = true;
                        Grid1.Columns[17].Visible = true;


                        tdvatamt.Attributes.Add("style", "display:'none'");
                        tdvatexclamt.Attributes.Add("style", "display:'none'");
                    }
                    else
                    {
                        Grid1.Columns[14].Visible = false;
                        Grid1.Columns[15].Visible = false;
                        Grid1.Columns[17].Visible = false;

                        tdvatamt.Attributes.Add("style", "display:none");
                        tdvatexclamt.Attributes.Add("style", "display:none");
                    }
                    
                    Grid1.Columns[16].Visible = true;
                    Grid1.Columns[19].Visible = true;
                    Grid1.Columns[20].Visible = true;
                    Grid1.Columns[19].Width = "5%";
                    Grid1.Columns[20].Width = "5%";
                }
                else
                {
                    if (ChkSerialedCompany == "Yes")
                    {
                        Grid1.Columns[0].Visible = false; Grid1.Columns[6].Visible = false; Grid1.Columns[11].Visible = false;
                        Grid1.Columns[18].Visible = false; Grid1.Columns[21].Visible = false;
                        Grid1.Columns[1].Visible = true; Grid1.Columns[2].Visible = true; Grid1.Columns[3].Visible = true;
                        Grid1.Columns[4].Visible = true; Grid1.Columns[5].Visible = true; Grid1.Columns[7].Visible = true;
                        Grid1.Columns[8].Visible = true; Grid1.Columns[9].Visible = true; Grid1.Columns[10].Visible = true;
                        Grid1.Columns[12].Visible = true; Grid1.Columns[13].Visible = true;
                        if (hdnordertype.Value == "E-Commerce Order")
                        {

                            Grid1.Columns[14].Visible = true;
                            Grid1.Columns[15].Visible = true;
                            Grid1.Columns[17].Visible = true;

                            tdvatamt.Attributes.Add("style", "display:'none'");
                            tdvatexclamt.Attributes.Add("style", "display:'none'");
                        }
                        else
                        {
                            Grid1.Columns[14].Visible = false;
                            Grid1.Columns[15].Visible = false;
                            Grid1.Columns[17].Visible = false;

                            tdvatamt.Attributes.Add("style", "display:none");
                            tdvatexclamt.Attributes.Add("style", "display:none");
                        }
                        Grid1.Columns[16].Visible = true;
                        Grid1.Columns[19].Visible = true;
                        Grid1.Columns[20].Visible = true;
                        Grid1.Columns[19].Width = "5%";
                        Grid1.Columns[20].Width = "5%";

                    }
                    else
                    {
                        Grid1.Columns[0].Visible = false; Grid1.Columns[6].Visible = false; Grid1.Columns[11].Visible = false;
                        Grid1.Columns[18].Visible = false; Grid1.Columns[21].Visible = false;

                        Grid1.Columns[1].Visible = true; Grid1.Columns[2].Visible = true; Grid1.Columns[3].Visible = true;
                        Grid1.Columns[4].Visible = true; Grid1.Columns[5].Visible = true; Grid1.Columns[7].Visible = true;
                        Grid1.Columns[8].Visible = true; Grid1.Columns[9].Visible = true; Grid1.Columns[10].Visible = true;
                        Grid1.Columns[12].Visible = true; Grid1.Columns[13].Visible = true;
                        if (hdnordertype.Value == "E-Commerce Order")
                        {

                            Grid1.Columns[14].Visible = true;
                            Grid1.Columns[15].Visible = true;
                            Grid1.Columns[17].Visible = true;

                            tdvatamt.Attributes.Add("style", "display:'none'");
                            tdvatexclamt.Attributes.Add("style", "display:'none'");
                        }
                        else
                        {
                            Grid1.Columns[14].Visible = false;
                            Grid1.Columns[15].Visible = false;
                            Grid1.Columns[17].Visible = false;

                            tdvatamt.Attributes.Add("style", "display:none");
                            tdvatexclamt.Attributes.Add("style", "display:none");
                        }
                        Grid1.Columns[16].Visible = true;
                        Grid1.Columns[19].Visible = false;
                        Grid1.Columns[20].Visible = false;
                        Grid1.Columns[19].Width = "0%";
                        Grid1.Columns[20].Width = "0%";

                    }
                }

                Grid1.DataBind();



                string ChkSerialedCompanyDeptID = objService.ChkSerialedCompanyDeptID(SiteID, profile.DBConnection._constr);
                if (ChkSerialedCompanyDeptID == "Yes")
                {
                    List<SP_GetPartSerialDetail_Result> RequestPartserialList = new List<SP_GetPartSerialDetail_Result>();
                    RequestPartserialList = objService.GetRequestPartserialDetailByRequestID(RequestID, SiteID, Session.SessionID, profile.Personal.UserID.ToString(), ObjectNameSerial, profile.DBConnection._constr).ToList();
                }

            }
            catch (System.Exception ex) { Login.Profile.ErrorHandling(ex, this, "PartRequestEntry.aspx", "FillGrid1ByRequestID"); }
            finally { objService.Close(); }
        }

        protected void Grid1_OnRebind(object sender, EventArgs e)
        {
            iPartRequestClient objService = new iPartRequestClient();
            try
            {
                string ChkSerialedCompany = "";
                Grid1.DataSource = null;
                Grid1.DataBind();
                CustomProfile profile = CustomProfile.GetProfile();
                HiddenField hdn = (HiddenField)UCProductSearch1.FindControl("hdnProductSearchSelectedRec");
                List<POR_SP_GetPartDetail_ForRequest_Result> RequestPartList = new List<POR_SP_GetPartDetail_ForRequest_Result>();
                if (hdn.Value != "")
                {


                    string[] idP = hdn.Value.Split(',').ToArray();
                    for (int j = 0; j <= idP.Length - 1; j++)
                    {

                        long proid = Convert.ToInt64(idP[j]);
                        RequestPartList = objService.GetExistingTempDataBySessionIDObjectName(Session.SessionID, profile.Personal.UserID.ToString(), ObjectName, profile.DBConnection._constr).ToList();
                        RequestPartList = RequestPartList.Where(i => i.Prod_ID == proid).ToList();
                        string pro = proid.ToString();
                        if (RequestPartList.Count > 0)
                        {
                            RequestPartList = objService.GetExistingTempDataBySessionIDObjectName(Session.SessionID, profile.Personal.UserID.ToString(), ObjectName, profile.DBConnection._constr).ToList();
                            Grid1.DataSource = RequestPartList;

                            if (hdnselectedCompany.Value == "" || hdnselectedCompany.Value == null) { hdnselectedCompany.Value = "0"; }
                            ChkSerialedCompany = objService.ChkSerialedCompany(Convert.ToInt64(hdnselectedCompany.Value), profile.DBConnection._constr);
                            if (profile.Personal.UserType == "Super Admin")
                            {
                                Grid1.Columns[0].Visible = false; Grid1.Columns[6].Visible = false; Grid1.Columns[11].Visible = false;
                                Grid1.Columns[18].Visible = false; Grid1.Columns[21].Visible = false;
                                Grid1.Columns[1].Visible = true; Grid1.Columns[2].Visible = true; Grid1.Columns[3].Visible = true;
                                Grid1.Columns[4].Visible = true; Grid1.Columns[5].Visible = true; Grid1.Columns[7].Visible = true;
                                Grid1.Columns[8].Visible = true; Grid1.Columns[9].Visible = true; Grid1.Columns[10].Visible = true;
                                Grid1.Columns[12].Visible = true; Grid1.Columns[13].Visible = true; Grid1.Columns[14].Visible = true;
                                Grid1.Columns[15].Visible = true; Grid1.Columns[16].Visible = true; Grid1.Columns[17].Visible = true;
                                Grid1.Columns[19].Visible = true;
                                Grid1.Columns[20].Visible = true;
                                Grid1.Columns[19].Width = "5%";
                                Grid1.Columns[20].Width = "5%";
                            }
                            else
                            {
                                if (ChkSerialedCompany == "Yes")
                                {
                                    Grid1.Columns[0].Visible = false; Grid1.Columns[6].Visible = false; Grid1.Columns[11].Visible = false;
                                    Grid1.Columns[18].Visible = false; Grid1.Columns[21].Visible = false;
                                    Grid1.Columns[1].Visible = true; Grid1.Columns[2].Visible = true; Grid1.Columns[3].Visible = true;
                                    Grid1.Columns[4].Visible = true; Grid1.Columns[5].Visible = true; Grid1.Columns[7].Visible = true;
                                    Grid1.Columns[8].Visible = true; Grid1.Columns[9].Visible = true; Grid1.Columns[10].Visible = true;
                                    Grid1.Columns[12].Visible = true; Grid1.Columns[13].Visible = true; Grid1.Columns[14].Visible = true;
                                    Grid1.Columns[15].Visible = true; Grid1.Columns[16].Visible = true; Grid1.Columns[17].Visible = true;
                                    Grid1.Columns[19].Visible = true;
                                    Grid1.Columns[20].Visible = true;
                                    Grid1.Columns[19].Width = "5%";
                                    Grid1.Columns[20].Width = "5%";

                                }
                                else
                                {
                                    Grid1.Columns[0].Visible = false; Grid1.Columns[6].Visible = false; Grid1.Columns[11].Visible = false;
                                    Grid1.Columns[18].Visible = false; Grid1.Columns[21].Visible = false;

                                    Grid1.Columns[1].Visible = true; Grid1.Columns[2].Visible = true; Grid1.Columns[3].Visible = true;
                                    Grid1.Columns[4].Visible = true; Grid1.Columns[5].Visible = true; Grid1.Columns[7].Visible = true;
                                    Grid1.Columns[8].Visible = true; Grid1.Columns[9].Visible = true; Grid1.Columns[10].Visible = true;
                                    Grid1.Columns[12].Visible = true; Grid1.Columns[13].Visible = true; Grid1.Columns[14].Visible = true;
                                    Grid1.Columns[15].Visible = true; Grid1.Columns[16].Visible = true; Grid1.Columns[17].Visible = true;

                                    Grid1.Columns[19].Visible = false;
                                    Grid1.Columns[20].Visible = false;
                                    Grid1.Columns[19].Width = "0%";
                                    Grid1.Columns[20].Width = "0%";

                                }
                            }
                            Grid1.DataBind();

                        }
                        else
                        {
                            if (pro == "")
                            {
                                RequestPartList = objService.GetExistingTempDataBySessionIDObjectName(Session.SessionID, profile.Personal.UserID.ToString(), ObjectName, profile.DBConnection._constr).ToList();
                            }
                            else if (pro != "")
                            {


                                // RequestPartList = objService.AddPartIntoRequest_TempData(hdn.Value, Session.SessionID, profile.Personal.UserID.ToString(), ObjectName, Convert.ToInt32(profile.Personal.DepartmentID), profile.DBConnection._constr).ToList();
                                //RequestPartList = objService.AddPartIntoRequest_TempData(hdn.Value, Session.SessionID, profile.Personal.UserID.ToString(), ObjectName, 18, profile.DBConnection._constr).ToList();
                                RequestPartList = objService.AddPartIntoRequest_TempData(pro, Session.SessionID, profile.Personal.UserID.ToString(), ObjectName, Convert.ToInt32(hdnselectedDept.Value), profile.DBConnection._constr).ToList();
                                //  RequestPartList = objService.AddPartIntoRequest_TempData(hdn.Value, Session.SessionID, profile.Personal.UserID.ToString(), ObjectName, Convert.ToInt32(ddlSites.SelectedItem.Value), profile.DBConnection._constr).ToList();

                            }

                            //Add by Suresh
                            if (hdnprodID.Value != "")
                            {
                                RequestPartList = objService.AddPartIntoRequest_TempData(pro, Session.SessionID, profile.Personal.UserID.ToString(), ObjectName, Convert.ToInt32(ddlSites.SelectedItem.Value), profile.DBConnection._constr).ToList();
                                hdnprodID.Value = "";
                            }

                            if (hdnChngDept.Value == "0x00x0")
                            {
                                objService.ClearTempDataFromDBNEW(HttpContext.Current.Session.SessionID, profile.Personal.UserID.ToString(), ObjectName, profile.DBConnection._constr);
                                objService.ClearTempDataFromDBNEW(HttpContext.Current.Session.SessionID, profile.Personal.UserID.ToString(), ObjectNameSerial, profile.DBConnection._constr);
                                //delete serial temptable
                                objService.DeleteSerialTemptable(profile.Personal.UserID, profile.DBConnection._constr);
                                RequestPartList = null;
                            }
                            hdnChngDept.Value = "";
                            var chngdpt = "1x1";
                            hdnChngDept.Value = chngdpt;

                            if (hdnChangePrdQtyPrice.Value == "1")
                            {
                                RequestPartList = objService.GetRequestPartDetailByRequestID(long.Parse(Session["PORRequestID"].ToString()), long.Parse(hdnselectedDept.Value), Session.SessionID, profile.Personal.UserID.ToString(), ObjectName, profile.DBConnection._constr).ToList();
                                //tOrderHead RequestHead = new tOrderHead();
                                //RequestHead = objService.GetOrderHeadByOrderID(Convert.ToInt64(HttpContext.Current.Session["PORRequestID"].ToString()), profile.DBConnection._constr);
                                //txtTotalQty.Text = RequestHead.TotalQty.ToString();
                                //txtGrandTotal.Text = RequestHead.GrandTotal.ToString();
                            }

                            Grid1.DataSource = RequestPartList;

                            if (hdnselectedCompany.Value == "" || hdnselectedCompany.Value == null) { hdnselectedCompany.Value = "0"; }
                            ChkSerialedCompany = objService.ChkSerialedCompany(Convert.ToInt64(hdnselectedCompany.Value), profile.DBConnection._constr);
                            if (profile.Personal.UserType == "Super Admin")
                            {
                                Grid1.Columns[0].Visible = false; Grid1.Columns[6].Visible = false; Grid1.Columns[11].Visible = false;
                                Grid1.Columns[18].Visible = false; Grid1.Columns[21].Visible = false;
                                Grid1.Columns[1].Visible = true; Grid1.Columns[2].Visible = true; Grid1.Columns[3].Visible = true;
                                Grid1.Columns[4].Visible = true; Grid1.Columns[5].Visible = true; Grid1.Columns[7].Visible = true;
                                Grid1.Columns[8].Visible = true; Grid1.Columns[9].Visible = true; Grid1.Columns[10].Visible = true;
                                Grid1.Columns[12].Visible = true; Grid1.Columns[13].Visible = true; Grid1.Columns[14].Visible = true;
                                Grid1.Columns[15].Visible = true; Grid1.Columns[16].Visible = true; Grid1.Columns[17].Visible = true;
                                Grid1.Columns[19].Visible = true;
                                Grid1.Columns[20].Visible = true;
                                Grid1.Columns[19].Width = "5%";
                                Grid1.Columns[20].Width = "5%";
                            }
                            else
                            {
                                if (ChkSerialedCompany == "Yes")
                                {
                                    Grid1.Columns[0].Visible = false; Grid1.Columns[6].Visible = false; Grid1.Columns[11].Visible = false;
                                    Grid1.Columns[18].Visible = false; Grid1.Columns[21].Visible = false;
                                    Grid1.Columns[1].Visible = true; Grid1.Columns[2].Visible = true; Grid1.Columns[3].Visible = true;
                                    Grid1.Columns[4].Visible = true; Grid1.Columns[5].Visible = true; Grid1.Columns[7].Visible = true;
                                    Grid1.Columns[8].Visible = true; Grid1.Columns[9].Visible = true; Grid1.Columns[10].Visible = true;
                                    Grid1.Columns[12].Visible = true; Grid1.Columns[13].Visible = true; Grid1.Columns[14].Visible = true;
                                    Grid1.Columns[15].Visible = true; Grid1.Columns[16].Visible = true; Grid1.Columns[17].Visible = true;
                                    Grid1.Columns[19].Visible = true;
                                    Grid1.Columns[20].Visible = true;
                                    Grid1.Columns[19].Width = "5%";
                                    Grid1.Columns[20].Width = "5%";

                                }
                                else
                                {
                                    Grid1.Columns[0].Visible = false; Grid1.Columns[6].Visible = false; Grid1.Columns[11].Visible = false;
                                    Grid1.Columns[18].Visible = false; Grid1.Columns[21].Visible = false;

                                    Grid1.Columns[1].Visible = true; Grid1.Columns[2].Visible = true; Grid1.Columns[3].Visible = true;
                                    Grid1.Columns[4].Visible = true; Grid1.Columns[5].Visible = true; Grid1.Columns[7].Visible = true;
                                    Grid1.Columns[8].Visible = true; Grid1.Columns[9].Visible = true; Grid1.Columns[10].Visible = true;
                                    Grid1.Columns[12].Visible = true; Grid1.Columns[13].Visible = true; Grid1.Columns[14].Visible = true;
                                    Grid1.Columns[15].Visible = true; Grid1.Columns[16].Visible = true;
                                    Grid1.Columns[17].Visible = true;

                                    Grid1.Columns[19].Visible = false;
                                    Grid1.Columns[20].Visible = false;
                                    Grid1.Columns[19].Width = "0%";
                                    Grid1.Columns[20].Width = "0%";

                                }
                            }
                            Grid1.DataBind();
                            //show serial icon in request grid                           
                        }
                    }

                }
                else if (hdnChngDept.Value == "0x00x0")
                {
                    objService.ClearTempDataFromDBNEW(HttpContext.Current.Session.SessionID, profile.Personal.UserID.ToString(), ObjectName, profile.DBConnection._constr);
                    RequestPartList = null;

                    hdnChngDept.Value = "";
                    var chngdpt = "1x1";
                    hdnChngDept.Value = chngdpt;
                }
                else
                {

                    RequestPartList = objService.GetExistingTempDataBySessionIDObjectName(Session.SessionID, profile.Personal.UserID.ToString(), ObjectName, profile.DBConnection._constr).ToList();

                    Grid1.DataSource = RequestPartList;

                    if (hdnselectedCompany.Value == "" || hdnselectedCompany.Value == null) { hdnselectedCompany.Value = "0"; }
                    ChkSerialedCompany = objService.ChkSerialedCompany(Convert.ToInt64(hdnselectedCompany.Value), profile.DBConnection._constr);
                    if (profile.Personal.UserType == "Super Admin")
                    {
                        Grid1.Columns[0].Visible = false; Grid1.Columns[6].Visible = false; Grid1.Columns[11].Visible = false;
                        Grid1.Columns[18].Visible = false; Grid1.Columns[21].Visible = false;
                        Grid1.Columns[1].Visible = true; Grid1.Columns[2].Visible = true; Grid1.Columns[3].Visible = true;
                        Grid1.Columns[4].Visible = true; Grid1.Columns[5].Visible = true; Grid1.Columns[7].Visible = true;
                        Grid1.Columns[8].Visible = true; Grid1.Columns[9].Visible = true; Grid1.Columns[10].Visible = true;
                        Grid1.Columns[12].Visible = true; Grid1.Columns[13].Visible = true; Grid1.Columns[14].Visible = true;
                        Grid1.Columns[15].Visible = true; Grid1.Columns[16].Visible = true; Grid1.Columns[17].Visible = true;
                        Grid1.Columns[19].Visible = true;
                        Grid1.Columns[20].Visible = true;
                        Grid1.Columns[19].Width = "5%";
                        Grid1.Columns[20].Width = "5%";
                    }
                    else
                    {
                        if (ChkSerialedCompany == "Yes")
                        {
                            Grid1.Columns[0].Visible = false; Grid1.Columns[6].Visible = false; Grid1.Columns[11].Visible = false;
                            Grid1.Columns[18].Visible = false; Grid1.Columns[21].Visible = false;
                            Grid1.Columns[1].Visible = true; Grid1.Columns[2].Visible = true; Grid1.Columns[3].Visible = true;
                            Grid1.Columns[4].Visible = true; Grid1.Columns[5].Visible = true; Grid1.Columns[7].Visible = true;
                            Grid1.Columns[8].Visible = true; Grid1.Columns[9].Visible = true; Grid1.Columns[10].Visible = true;
                            Grid1.Columns[12].Visible = true; Grid1.Columns[13].Visible = true; Grid1.Columns[14].Visible = true;
                            Grid1.Columns[15].Visible = true; Grid1.Columns[16].Visible = true; Grid1.Columns[17].Visible = true;
                            Grid1.Columns[19].Visible = true;
                            Grid1.Columns[20].Visible = true;
                            Grid1.Columns[19].Width = "5%";
                            Grid1.Columns[20].Width = "5%";

                        }
                        else
                        {
                            Grid1.Columns[0].Visible = false; Grid1.Columns[6].Visible = false; Grid1.Columns[11].Visible = false;
                            Grid1.Columns[18].Visible = false; Grid1.Columns[21].Visible = false;

                            Grid1.Columns[1].Visible = true; Grid1.Columns[2].Visible = true; Grid1.Columns[3].Visible = true;
                            Grid1.Columns[4].Visible = true; Grid1.Columns[5].Visible = true; Grid1.Columns[7].Visible = true;
                            Grid1.Columns[8].Visible = true; Grid1.Columns[9].Visible = true; Grid1.Columns[10].Visible = true;
                            Grid1.Columns[12].Visible = true; Grid1.Columns[13].Visible = true; Grid1.Columns[14].Visible = true;
                            Grid1.Columns[15].Visible = true; Grid1.Columns[16].Visible = true; Grid1.Columns[17].Visible = true;

                            Grid1.Columns[19].Visible = false;
                            Grid1.Columns[20].Visible = false;
                            Grid1.Columns[19].Width = "0%";
                            Grid1.Columns[20].Width = "0%";

                        }
                    }
                    Grid1.DataBind();

                }


                ////show serial icon in request grid
                //string ChkSerialedCompany = "";
                //if (hdnselectedCompany.Value == "" || hdnselectedCompany.Value == null) { hdnselectedCompany.Value = "0"; }
                //ChkSerialedCompany = objService.ChkSerialedCompany(Convert.ToInt64(hdnselectedCompany.Value), profile.DBConnection._constr);
                //if (ChkSerialedCompany == "Yes")
                //{
                //    Grid1.Columns[18].Visible = true;
                //    Grid1.Columns[19].Visible = true;

                //}
                //else
                //{
                //    Grid1.Columns[18].Visible = false;
                //    Grid1.Columns[19].Visible = false;
                //}
            }
            catch (System.Exception ex) { Login.Profile.ErrorHandling(ex, this, "PartRequestEntry.aspx", "Grid1_OnRebind"); }
            finally { objService.Close(); }
        }

        [WebMethod]
        public static void WmUpdateRequestPartUOM(object objRequest)
        {
            iPartRequestClient objService = new iPartRequestClient();
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary = (Dictionary<string, object>)objRequest;
                CustomProfile profile = CustomProfile.GetProfile();

                POR_SP_GetPartDetail_ForRequest_Result PartRequest = new POR_SP_GetPartDetail_ForRequest_Result();
                PartRequest.Sequence = Convert.ToInt64(dictionary["Sequence"]);
                PartRequest.UOMID = Convert.ToInt64(dictionary["UOMID"]);

                objService.UpdatePartRequest_TempData(HttpContext.Current.Session.SessionID, ObjectName, profile.Personal.UserID.ToString(), PartRequest, profile.DBConnection._constr);
            }
            catch (System.Exception ex)
            {
                // Login.Profile.ErrorHandling(ex, "PartRequestEntry.aspx", "WmUpdateRequestPartUOM");
            }
            finally { objService.Close(); }
        }

        [WebMethod]
        public static string WMUpdateRequestQty1(object objRequest)
        {
            string result = "sameqty";
            iPartRequestClient objService = new iPartRequestClient();
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary = (Dictionary<string, object>)objRequest;
                CustomProfile profile = CustomProfile.GetProfile();

                POR_SP_GetPartDetail_ForRequest_Result PartRequest = new POR_SP_GetPartDetail_ForRequest_Result();
                PartRequest.Sequence = Convert.ToInt64(dictionary["Sequence"]);
                PartRequest.RequestQty = Convert.ToDecimal(dictionary["RequestQty"]);
                List<POR_SP_GetPartDetail_ForRequest_Result> TemplatePartList = new List<POR_SP_GetPartDetail_ForRequest_Result>();
                TemplatePartList = objService.GetExistingTempDataBySessionIDObjectName(HttpContext.Current.Session.SessionID, profile.Personal.UserID.ToString(), ObjectName, profile.DBConnection._constr).ToList();
                POR_SP_GetPartDetail_ForRequest_Result updateRec = new POR_SP_GetPartDetail_ForRequest_Result();
                updateRec = TemplatePartList.Where(g => g.Sequence == Convert.ToInt64(dictionary["Sequence"])).FirstOrDefault();
                if (updateRec.SerialFlag == "Yes")
                {
                    int rqty = 0;
                    long Sid = Convert.ToInt64(updateRec.RequestQty);
                    List<PowerOnRentwebapp.PORServicePartRequest.SP_GetPartSerialDetail_Result> RequestPartList1 = new List<PowerOnRentwebapp.PORServicePartRequest.SP_GetPartSerialDetail_Result>();
                    RequestPartList1 = objService.GetPartSerialDetailByRequestID(0, Convert.ToInt64(Sid), HttpContext.Current.Session.SessionID, profile.Personal.UserID.ToString(), ObjectNameSerial, profile.DBConnection._constr).ToList();
                    rqty = Convert.ToInt32(RequestPartList1.Count.ToString());
                    if (rqty == 0)
                    {
                        result = "sameqty";
                        objService.UpdatePartRequest_TempData(HttpContext.Current.Session.SessionID, ObjectName, profile.Personal.UserID.ToString(), PartRequest, profile.DBConnection._constr);
                    }
                    else
                    {
                        if (rqty == Convert.ToInt32(dictionary["RequestQty"]))
                        {
                            result = "sameqty";
                            objService.UpdatePartRequest_TempData(HttpContext.Current.Session.SessionID, ObjectName, profile.Personal.UserID.ToString(), PartRequest, profile.DBConnection._constr);
                        }
                        else
                        {
                            result = "notsameqty" + "," + Convert.ToString(dictionary["RequestQty"]);
                        }
                    }
                }
                else
                {
                    objService.UpdatePartRequest_TempData(HttpContext.Current.Session.SessionID, ObjectName, profile.Personal.UserID.ToString(), PartRequest, profile.DBConnection._constr);
                    result = "sameqty";
                }

            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, "PartRequestEntry.aspx", "WMUpdateRequestQty");
            }
            finally { objService.Close(); }
            return result;
        }

        [WebMethod]
        public static void WMUpdRequestPart(object objRequest)
        {
            iPartRequestClient objService = new iPartRequestClient();
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary = (Dictionary<string, object>)objRequest;
                CustomProfile profile = CustomProfile.GetProfile();

                string uom = objService.GetUOMName(Convert.ToInt64(dictionary["UOMID"]), profile.DBConnection._constr);

                POR_SP_GetPartDetail_ForRequest_Result PartRequest = new POR_SP_GetPartDetail_ForRequest_Result();
                PartRequest.Sequence = Convert.ToInt64(dictionary["Sequence"]);
                PartRequest.RequestQty = Convert.ToDecimal(dictionary["RequestQty"]);
                PartRequest.UOM = uom;
                PartRequest.UOMID = Convert.ToInt64(dictionary["UOMID"]);
                PartRequest.Total = Convert.ToDecimal(dictionary["Total"]);
                objService.UpdatePartRequest_TempData1(HttpContext.Current.Session.SessionID, ObjectName, profile.Personal.UserID.ToString(), PartRequest, profile.DBConnection._constr);
            }
            catch (System.Exception ex)
            {
                //      Login.Profile.ErrorHandling(ex, "PartRequestEntry.aspx", "WMUpdRequestPart");
            }
            finally { objService.Close(); }
        }

        [WebMethod]
        public static void WMUpdRequestPartPrice(object objRequest, int ProdID)
        {
            iPartRequestClient objService = new iPartRequestClient();
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary = (Dictionary<string, object>)objRequest;
                CustomProfile profile = CustomProfile.GetProfile();
                string uom = objService.GetUOMName(Convert.ToInt64(dictionary["UOMID"]), profile.DBConnection._constr);
                POR_SP_GetPartDetail_ForRequest_Result PartRequest = new POR_SP_GetPartDetail_ForRequest_Result();
                PartRequest.Sequence = Convert.ToInt64(dictionary["Sequence"]);
                PartRequest.RequestQty = Convert.ToDecimal(dictionary["RequestQty"]); PartRequest.UOM = uom;
                PartRequest.UOMID = Convert.ToInt64(dictionary["UOMID"]);
                PartRequest.Total = Convert.ToDecimal(dictionary["Total"]);
                PartRequest.Price = Convert.ToDecimal(dictionary["Price"]);
                // PartRequest.IsPriceChange = Convert.ToInt16(dictionary["IsPriceChange"]);
                decimal price = Convert.ToDecimal(dictionary["Price"]);
                int ISPriceChangedYN = objService.IsPriceChanged(ProdID, price, profile.DBConnection._constr);
                if (ISPriceChangedYN == 0) { PartRequest.IsPriceChange = 0; }
                else { PartRequest.IsPriceChange = 1; }
                objService.UpdatePartRequest_TempData12(HttpContext.Current.Session.SessionID, ObjectName, profile.Personal.UserID.ToString(), PartRequest, profile.DBConnection._constr);
            }
            catch (System.Exception ex)
            {
                //  Login.Profile.ErrorHandling(ex, "PartRequestEntry.aspx", "WMUpdRequestPart");
            }
            finally { objService.Close(); }
        }

        [WebMethod]
        public static int WMRemovePartFromRequest(Int32 Sequence, long skuid, string serilaflag)
        {
            int editOrder = 0;
            iPartRequestClient objService = new iPartRequestClient();
            CustomProfile profile = CustomProfile.GetProfile();
            try
            {
                if (long.Parse(HttpContext.Current.Session["PORRequestID"].ToString()) > 0)
                {
                    tOrderHead RequestHead = new tOrderHead();
                    long ReqID = long.Parse(HttpContext.Current.Session["PORRequestID"].ToString());
                    RequestHead = objService.GetOrderHeadByOrderID(Convert.ToInt64(HttpContext.Current.Session["PORRequestID"].ToString()), profile.DBConnection._constr);
                    string Status = RequestHead.Status.ToString();
                    if (Status == "1")
                    {
                        Dictionary<string, object> dictionary = new Dictionary<string, object>();
                        objService.RemovePartFromRequest_TempData(HttpContext.Current.Session.SessionID, profile.Personal.UserID.ToString(), ObjectName, Sequence, profile.DBConnection._constr);
                        editOrder = 1;
                    }
                    else { editOrder = 0; }
                }
                else
                {
                    Dictionary<string, object> dictionary = new Dictionary<string, object>();
                    objService.RemovePartFromRequest_TempData(HttpContext.Current.Session.SessionID, profile.Personal.UserID.ToString(), ObjectName, Sequence, profile.DBConnection._constr);
                    editOrder = 1;
                }

                if (serilaflag == "Yes")
                {
                    if (editOrder == 1)
                    {
                        objService.RemoveSkuserialFromRequest_TempData(HttpContext.Current.Session.SessionID, profile.Personal.UserID.ToString(), ObjectNameSerial, Convert.ToInt64(skuid), profile.DBConnection._constr);
                        long ReqID1 = 0;
                        if (long.Parse(HttpContext.Current.Session["PORRequestID"].ToString()) > 0)
                        {
                            ReqID1 = Convert.ToInt64(HttpContext.Current.Session["PORRequestID"].ToString());
                            objService.DeleteSkuwiseSerialTemptable(profile.Personal.UserID, Convert.ToInt64(skuid), Convert.ToInt64(ReqID1), profile.DBConnection._constr);
                        }
                        else
                        {
                            ReqID1 = 0;
                            objService.DeleteSkuwiseSerialTemptable(profile.Personal.UserID, Convert.ToInt64(skuid), Convert.ToInt64(ReqID1), profile.DBConnection._constr);

                        }
                    }
                }

            }
            catch { }
            finally { objService.Close(); }
            return editOrder;
        }

        #endregion

        #region Approval Code
        [WebMethod]
        public static string WMSaveApproval(string ApprovalStatus, string ApprovalRemark)
        {
            iPartRequestClient objService = new iPartRequestClient();
            string result = "";
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                result = objService.SaveApprovalStatus(Convert.ToInt64(HttpContext.Current.Session["PORRequestID"].ToString()), ApprovalStatus, ApprovalRemark, profile.Personal.UserID, profile.DBConnection._constr);

                if (result == "true")
                {
                    objService.ClearTempDataFromDB(HttpContext.Current.Session.SessionID, profile.Personal.UserID.ToString(), ObjectName, profile.DBConnection._constr);
                }
            }
            catch (System.Exception ex) { Login.Profile.ErrorHandling(ex, "PartRequestEntry.aspx", "WMSaveApproval"); }
            finally
            { objService.Close(); }
            return result;
        }

        protected void GetApprovalDetails()
        {
            iPartRequestClient objService = new iPartRequestClient();
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                tApprovalDetail ApprovalRec = new tApprovalDetail();
                ApprovalRec = objService.GetApprovalDetailsByReqestID(Convert.ToInt64(Session["PORRequestID"].ToString()), profile.DBConnection._constr);
                if (ApprovalRec != null)
                {
                    CheckBoxApproved.Checked = false; CheckBoxRejected.Checked = false;
                    if (ApprovalRec.Status == "Approved") { CheckBoxApproved.Checked = true; }
                    else if (ApprovalRec.Status == "Rejected") { CheckBoxRejected.Checked = true; }
                    lblApprovalDate.Text = ApprovalRec.ApprovedDate.Value.ToString("dd-MMM-yyyy hh:mm tt");
                    txtApprovalRemark.Text = ApprovalRec.Remark = ApprovalRec.Remark;

                    if (ApprovalRec.ApproverUserID != profile.Personal.UserID)
                    {
                        divApprovalDetail.Disabled = true;
                    }
                }
            }
            catch (System.Exception ex) { Login.Profile.ErrorHandling(ex, this, "PartRequestEntry.aspx", "GetApprovalDetails"); }
            finally { objService.Close(); }
        }
        #endregion

        #region Add by Suresh

        //protected void Grid1_OnRowDataBound(object sender, Obout.Grid.GridRowEventArgs e)
        //{
        //    iPartRequestClient objService = new iPartRequestClient();
        //    CustomProfile profile = CustomProfile.GetProfile();

        //    try
        //    {
        //        if (e.Row.RowType == Obout.Grid.GridRowType.DataRow)
        //        {
        //            //Obout.Grid.GridDataControlFieldCell cell = e.Row.Cells[6] as Obout.Grid.GridDataControlFieldCell;
        //            // Obout.Grid.GridDataControlFieldCell cell = e.Row.Cells[7] as Obout.Grid.GridDataControlFieldCell;
        //            Obout.Grid.GridDataControlFieldCell cell = e.Row.Cells[10] as Obout.Grid.GridDataControlFieldCell;

        //            DropDownList ddl = cell.FindControl("ddlUOM") as DropDownList;
        //            HiddenField hdnUOM = cell.FindControl("hdnMyUOM") as HiddenField;
        //            //Label rowQtySpn = e.Row.Cells[9].FindControl("rowQtyTotal") as Label;
        //            Label rowQtySpn = e.Row.Cells[12].FindControl("rowQtyTotal") as Label;

        //            //TextBox txtUsrQty = e.Row.Cells[8].FindControl("txtUsrQty") as TextBox;

        //            //TextBox txtUsrQty = e.Row.Cells[6].FindControl("txtUsrQty") as TextBox;
        //            TextBox txtUsrQty = e.Row.Cells[9].FindControl("txtUsrQty") as TextBox;

        //            int ProdID = Convert.ToInt32(e.Row.Cells[0].Text);
        //            // decimal CrntStock = Convert.ToDecimal(e.Row.Cells[10].Text);
        //            decimal CrntStock = Convert.ToDecimal(e.Row.Cells[7].Text);
        //            decimal moq = Convert.ToDecimal(e.Row.Cells[5].Text);

        //            string hdnUOMText = e.Row.Cells[10].Text;
        //            string hdnUOM1 = e.Row.Cells[11].Text;
        //            /*New Price Added*/
        //            TextBox txtUsrPrice = e.Row.Cells[13].FindControl("txtUsrPrice") as TextBox; //txtUsrPrice.Enabled = false;   //Product Price
        //            Label rowPriceTotal = e.Row.Cells[14].FindControl("rowPriceTotal") as Label;

        //            int pricechange = objService.GetDeptPriceChange(long.Parse(hdnselectedDept.Value), profile.DBConnection._constr);
        //            if (pricechange == 1)
        //            {
        //                txtUsrPrice.Enabled = true;
        //            }
        //            else { txtUsrPrice.Enabled = false; }
        //            /*New Price Added*/
        //            int isprichange = Convert.ToInt32(e.Row.Cells[17].Text);
        //            if (isprichange == 1)
        //            {
        //                e.Row.BackColor = System.Drawing.Color.DarkCyan;
        //                e.Row.ForeColor = System.Drawing.Color.Red;

        //            }

        //            DataSet dsUOM = new DataSet();
        //            dsUOM = objService.GetUOMofSelectedProduct(ProdID, profile.DBConnection._constr);

        //            ddl.DataSource = dsUOM;
        //            ddl.DataTextField = "Description";
        //            ddl.DataValueField = "UMOGroup";
        //            ddl.DataBind();


        //            //if (txtUsrQty.Text!= "0.00")
        //            //{
        //            //    ddl.SelectedValue = hdnUOM1.ToString();
        //            //}

        //            //ddl.SelectedValue = e.Row.Cells[6].Text;
        //            //  string SelTmplt = Session["TemplateID"].ToString();

        //            //Grid1.Columns[16].Visible = false;

        //            if (Convert.ToInt64(HttpContext.Current.Session["PORRequestID"].ToString()) > 0)
        //            {

        //                long ReqId = Convert.ToInt64(HttpContext.Current.Session["PORRequestID"].ToString());
        //                string selectedUomTmpl = objService.GetSelectedUom(ReqId, ProdID, profile.DBConnection._constr);
        //                DataTable dt = new DataTable();
        //                decimal SelectedQty = 0, SelectedUOM = 0;
        //                if (selectedUomTmpl != "0")
        //                {
        //                    //ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByValue(selectedUomTmpl.ToString()));

        //                    dsUOM.Tables[0].DefaultView.RowFilter = "UOMID = " + hdnUOM1 + "";
        //                    dt = (dsUOM.Tables[0].DefaultView).ToTable();
        //                    string group = dt.Rows[0]["UMOGroup"].ToString();
        //                    ddl.SelectedValue = group;

        //                    SelectedQty = decimal.Parse(dt.Rows[0]["Quantity"].ToString());
        //                    SelectedUOM = decimal.Parse(dt.Rows[0]["UOMID"].ToString());
        //                }
        //                else
        //                {
        //                    dsUOM.Tables[0].DefaultView.RowFilter = "UOMID = " + hdnUOM1 + "";
        //                    dt = (dsUOM.Tables[0].DefaultView).ToTable();
        //                    string group = dt.Rows[0]["UMOGroup"].ToString();
        //                    ddl.SelectedValue = group;
        //                    SelectedQty = decimal.Parse(dt.Rows[0]["Quantity"].ToString());
        //                    SelectedUOM = decimal.Parse(dt.Rows[0]["UOMID"].ToString());
        //                    // SelectedQty = decimal.Parse(dsUOM.Tables[0].Rows[2]["Quantity"].ToString());
        //                    // SelectedUOM = decimal.Parse(dsUOM.Tables[0].Rows[2]["UOMID"].ToString());
        //                }

        //                rowQtySpn.Text = txtUsrQty.Text;
        //                decimal UserQty = decimal.Parse(txtUsrQty.Text.ToString());
        //                //int SelInd = 0;
        //                //SelInd = ddl.SelectedIndex;


        //                rowPriceTotal.Text = e.Row.Cells[14].Text;
        //                if (hdnOrderStatus.Value == "1")
        //                {
        //                    UCProductSearch1.Visible = true;

        //                    decimal rowQty = decimal.Parse(txtUsrQty.Text.ToString());
        //                    decimal UsrQty = decimal.Parse(txtUsrQty.Text.ToString()); //SelectedQty * rowQty;
        //                    decimal Price = decimal.Parse(txtUsrPrice.Text.ToString());

        //                    hdnSelectedQty.Value = SelectedQty.ToString();
        //                    rowQtySpn.Text = UsrQty.ToString();

        //                    if (UsrQty > CrntStock)
        //                    { rowQtySpn.Text = "0"; }
        //                    else
        //                    {
        //                        rowQtySpn.Text = UsrQty.ToString();
        //                        txtUsrQty.Text = Convert.ToString(UsrQty / SelectedQty);
        //                    }

        //                    ddl.Attributes.Add("onchange", "javascript:GetIndex(this,'" + hdnUOM.ClientID.ToString() + "','" + rowQtySpn.ClientID.ToString() + "','" + txtUsrQty.ClientID.ToString() + "'," + CrntStock + "," + e.Row.RowIndex + "," + Price + ",'" + rowPriceTotal.ClientID.ToString() + "'," + moq + ",'" + txtUsrPrice.ClientID.ToString() + "','" + ddl.ClientID.ToString() + "')");
        //                    txtUsrQty.Attributes.Add("onblur", "javascript:GetIndexQty(this," + SelectedQty + "," + SelectedUOM + ",'" + rowQtySpn.ClientID.ToString() + "','" + txtUsrQty.ClientID.ToString() + "'," + CrntStock + "," + e.Row.RowIndex + "," + Price + ",'" + rowPriceTotal.ClientID.ToString() + "'," + moq + ",'" + txtUsrPrice.ClientID.ToString() + "','" + ddl.ClientID.ToString() + "')");
        //                    txtUsrPrice.Attributes.Add("onblur", "javascript:GetChangedPrice(this," + SelectedQty + "," + SelectedUOM + ",'" + rowQtySpn.ClientID.ToString() + "','" + txtUsrQty.ClientID.ToString() + "'," + CrntStock + "," + e.Row.RowIndex + ",'" + rowPriceTotal.ClientID.ToString() + "'," + ProdID + ",'" + txtUsrPrice.ClientID.ToString() + "','" + ddl.ClientID.ToString() + "')");
        //                }
        //                else
        //                {

        //                    string ChkISOrdCreator = objService.ChkISOrdCreatorOrNot(Convert.ToInt64(HttpContext.Current.Session["PORRequestID"].ToString()), profile.Personal.UserID, profile.DBConnection._constr);
        //                    if (ChkISOrdCreator == "Yes")
        //                    {
        //                        decimal rowQty = decimal.Parse(txtUsrQty.Text.ToString());
        //                        decimal UsrQty = decimal.Parse(txtUsrQty.Text.ToString()); //SelectedQty * rowQty;
        //                        decimal Price = decimal.Parse(txtUsrPrice.Text.ToString());

        //                        hdnSelectedQty.Value = SelectedQty.ToString();
        //                        rowQtySpn.Text = UsrQty.ToString();

        //                        if (UsrQty > CrntStock)
        //                        { rowQtySpn.Text = "0"; }
        //                        else
        //                        {
        //                            rowQtySpn.Text = UsrQty.ToString();
        //                            txtUsrQty.Text = Convert.ToString(UsrQty / SelectedQty);
        //                        }

        //                        ddl.Attributes.Add("onchange", "javascript:GetIndex(this,'" + hdnUOM.ClientID.ToString() + "','" + rowQtySpn.ClientID.ToString() + "','" + txtUsrQty.ClientID.ToString() + "'," + CrntStock + "," + e.Row.RowIndex + "," + Price + ",'" + rowPriceTotal.ClientID.ToString() + "'," + moq + ",'" + txtUsrPrice.ClientID.ToString() + "','" + ddl.ClientID.ToString() + "')");
        //                        txtUsrQty.Attributes.Add("onblur", "javascript:GetIndexQty(this," + SelectedQty + "," + SelectedUOM + ",'" + rowQtySpn.ClientID.ToString() + "','" + txtUsrQty.ClientID.ToString() + "'," + CrntStock + "," + e.Row.RowIndex + "," + Price + ",'" + rowPriceTotal.ClientID.ToString() + "'," + moq + ",'" + txtUsrPrice.ClientID.ToString() + "','" + ddl.ClientID.ToString() + "')");
        //                        txtUsrPrice.Attributes.Add("onblur", "javascript:GetChangedPrice(this," + SelectedQty + "," + SelectedUOM + ",'" + rowQtySpn.ClientID.ToString() + "','" + txtUsrQty.ClientID.ToString() + "'," + CrntStock + "," + e.Row.RowIndex + ",'" + rowPriceTotal.ClientID.ToString() + "'," + ProdID + ",'" + txtUsrPrice.ClientID.ToString() + "','" + ddl.ClientID.ToString() + "')");

        //                        txtUsrQty.Enabled = true;
        //                        ddl.Enabled = true;
        //                        UCProductSearch1.Visible = true;
        //                        Toolbar1.SetSaveRight(true, "Allowed");
        //                    }
        //                    else
        //                    {
        //                        txtUsrQty.Enabled = false;
        //                        ddl.Enabled = false;
        //                        UCProductSearch1.Visible = false;
        //                        txtUsrQty.Text = Convert.ToString(UserQty / SelectedQty);
        //                    }
        //                    // UCProductSearch1.Visible = false;                           
        //                    // txtUsrQty.Enabled = false;
        //                    txtUsrPrice.Enabled = false;
        //                    // ddl.Enabled = false;
        //                }
        //                // Grid1.Columns[16].Visible = true;
        //            }
        //            else if (Convert.ToInt64(HttpContext.Current.Session["TemplateID"].ToString()) > 0)
        //            {
        //                long TemplID = Convert.ToInt64(HttpContext.Current.Session["TemplateID"].ToString());
        //                string selectedUom = objService.GetSelectedUomTemplate(TemplID, ProdID, profile.DBConnection._constr);

        //                dsUOM.Tables[0].DefaultView.RowFilter = "UOMID = " + hdnUOM1 + "";
        //                DataTable dt = (dsUOM.Tables[0].DefaultView).ToTable();
        //                string group = dt.Rows[0]["UMOGroup"].ToString();
        //                ddl.SelectedValue = group;


        //                //  ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByValue(selectedUom.ToString()));
        //                rowQtySpn.Text = txtUsrQty.Text;

        //                //int SelIndx = 0;
        //                //if (selectedUom == "0")
        //                //{
        //                //    SelIndx = 2;
        //                // }
        //                // else
        //                // {
        //                //     SelIndx = ddl.SelectedIndex;
        //                // }

        //                decimal SelectedQty = decimal.Parse(dt.Rows[0]["Quantity"].ToString());
        //                decimal SelectedUOM = decimal.Parse(dt.Rows[0]["UOMID"].ToString());
        //                decimal rowQty = decimal.Parse(txtUsrQty.Text.ToString());
        //                decimal UsrQty = decimal.Parse(txtUsrQty.Text.ToString()); //SelectedQty * rowQty;
        //                decimal Price = decimal.Parse(txtUsrPrice.Text.ToString());
        //                rowPriceTotal.Text = e.Row.Cells[14].Text;

        //                hdnSelectedQty.Value = SelectedQty.ToString();
        //                rowQtySpn.Text = UsrQty.ToString();

        //                if (UsrQty > CrntStock)
        //                { rowQtySpn.Text = "0"; }
        //                else
        //                {
        //                    rowQtySpn.Text = UsrQty.ToString();
        //                    txtUsrQty.Text = Convert.ToString(UsrQty / SelectedQty);
        //                }

        //                // ddl.Attributes.Add("onchange", "javascript:GetIndex(this,'" + hdnUOM.ClientID.ToString() + "','" + rowQtySpn.ClientID.ToString() + "','" + txtUsrQty.ClientID.ToString() + "'," + CrntStock + "," + e.Row.RowIndex + ")"); //old
        //                ddl.Attributes.Add("onchange", "javascript:GetIndex(this,'" + hdnUOM.ClientID.ToString() + "','" + rowQtySpn.ClientID.ToString() + "','" + txtUsrQty.ClientID.ToString() + "'," + CrntStock + "," + e.Row.RowIndex + "," + Price + ",'" + rowPriceTotal.ClientID.ToString() + "'," + moq + ",'" + txtUsrPrice.ClientID.ToString() + "','" + ddl.ClientID.ToString() + "')");
        //                //txtUsrQty.Attributes.Add("onblur", "javascript:GetIndexQty(this," + SelectedQty + "," + SelectedUOM + ",'" + rowQtySpn.ClientID.ToString() + "','" + txtUsrQty.ClientID.ToString() + "'," + CrntStock + "," + e.Row.RowIndex + ")"); //old
        //                txtUsrQty.Attributes.Add("onblur", "javascript:GetIndexQty(this," + SelectedQty + "," + SelectedUOM + ",'" + rowQtySpn.ClientID.ToString() + "','" + txtUsrQty.ClientID.ToString() + "'," + CrntStock + "," + e.Row.RowIndex + "," + Price + ",'" + rowPriceTotal.ClientID.ToString() + "'," + moq + ",'" + txtUsrPrice.ClientID.ToString() + "','" + ddl.ClientID.ToString() + "')");
        //                txtUsrPrice.Attributes.Add("onblur", "javascript:GetChangedPrice(this," + SelectedQty + "," + SelectedUOM + ",'" + rowQtySpn.ClientID.ToString() + "','" + txtUsrQty.ClientID.ToString() + "'," + CrntStock + "," + e.Row.RowIndex + ",'" + rowPriceTotal.ClientID.ToString() + "'," + ProdID + ",'" + txtUsrPrice.ClientID.ToString() + "','" + ddl.ClientID.ToString() + "')");
        //            }
        //            else
        //            {
        //                //ddl.SelectedIndex = 2;
        //                dsUOM.Tables[0].DefaultView.RowFilter = "UOMID = " + hdnUOM1 + "";
        //                DataTable dt = (dsUOM.Tables[0].DefaultView).ToTable();
        //                string group = dt.Rows[0]["UMOGroup"].ToString();
        //                ddl.SelectedValue = group;

        //                decimal SelectedQty = decimal.Parse(dt.Rows[0]["Quantity"].ToString());
        //                decimal SelectedUOM = decimal.Parse(dt.Rows[0]["UOMID"].ToString());

        //                decimal rowQty = decimal.Parse(txtUsrQty.Text.ToString());
        //                decimal UsrQty = SelectedQty * rowQty;
        //                decimal Price = decimal.Parse(txtUsrPrice.Text.ToString());

        //                hdnSelectedQty.Value = SelectedQty.ToString();
        //                rowQtySpn.Text = UsrQty.ToString();
        //                decimal TotalPrice = Price * UsrQty;

        //                rowPriceTotal.Text = TotalPrice.ToString();
        //                if (UsrQty > CrntStock)
        //                { rowQtySpn.Text = "0"; }
        //                else
        //                {
        //                    rowQtySpn.Text = UsrQty.ToString();
        //                    //Price = decimal.Parse(rowQtySpn.Text.ToString()) * decimal.Parse(txtUsrPrice.Text.ToString());
        //                    //rowPriceTotal.Text = Price.ToString();

        //                }
        //                ddl.Attributes.Add("onchange", "javascript:GetIndex(this,'" + hdnUOM.ClientID.ToString() + "','" + rowQtySpn.ClientID.ToString() + "','" + txtUsrQty.ClientID.ToString() + "'," + CrntStock + "," + e.Row.RowIndex + "," + Price + ",'" + rowPriceTotal.ClientID.ToString() + "'," + moq + ",'" + txtUsrPrice.ClientID.ToString() + "','" + ddl.ClientID.ToString() + "')");

        //                txtUsrQty.Attributes.Add("onblur", "javascript:GetIndexQty(this," + SelectedQty + "," + SelectedUOM + ",'" + rowQtySpn.ClientID.ToString() + "','" + txtUsrQty.ClientID.ToString() + "'," + CrntStock + "," + e.Row.RowIndex + "," + Price + ",'" + rowPriceTotal.ClientID.ToString() + "'," + moq + ",'" + txtUsrPrice.ClientID.ToString() + "','" + ddl.ClientID.ToString() + "')");
        //                //  txtUsrQty.Attributes.Add("onkeydown", "javascript:GetIndexQty(this," + SelectedQty + "," + SelectedUOM + ",'" + rowQtySpn.ClientID.ToString() + "','" + txtUsrQty.ClientID.ToString() + "'," + CrntStock + "," + e.Row.RowIndex + "," + Price + ",'" + rowPriceTotal.ClientID.ToString() + "'," + moq + ")");
        //                //    txtUsrQty.Attributes.Add("onkeypress", "javascript:GetIndexQty(this," + SelectedQty + "," + SelectedUOM + ",'" + rowQtySpn.ClientID.ToString() + "','" + txtUsrQty.ClientID.ToString() + "'," + CrntStock + "," + e.Row.RowIndex + "," + Price + ",'" + rowPriceTotal.ClientID.ToString() + "'," + moq + ")");

        //                txtUsrPrice.Attributes.Add("onblur", "javascript:GetChangedPrice(this," + SelectedQty + "," + SelectedUOM + ",'" + rowQtySpn.ClientID.ToString() + "','" + txtUsrQty.ClientID.ToString() + "'," + CrntStock + "," + e.Row.RowIndex + ",'" + rowPriceTotal.ClientID.ToString() + "'," + ProdID + ",'" + txtUsrPrice.ClientID.ToString() + "','" + ddl.ClientID.ToString() + "')");
        //                //    txtUsrPrice.Attributes.Add("onkeydown", "javascript:GetChangedPrice(this," + SelectedQty + "," + SelectedUOM + ",'" + rowQtySpn.ClientID.ToString() + "','" + txtUsrQty.ClientID.ToString() + "'," + CrntStock + "," + e.Row.RowIndex + ",'" + rowPriceTotal.ClientID.ToString() + "'," + ProdID + ")");
        //                //    txtUsrPrice.Attributes.Add("onkeypress", "javascript:GetChangedPrice(this," + SelectedQty + "," + SelectedUOM + ",'" + rowQtySpn.ClientID.ToString() + "','" + txtUsrQty.ClientID.ToString() + "'," + CrntStock + "," + e.Row.RowIndex + ",'" + rowPriceTotal.ClientID.ToString() + "'," + ProdID + ")");
        //            }
        //        }
        //    }
        //    catch (System.Exception ex) { Login.Profile.ErrorHandling(ex, this, "PartRequestEntry.aspx", "Grid1_OnRowDataBound"); }
        //    finally { objService.Close(); }


        //}


        protected void Grid1_OnRowDataBound(object sender, Obout.Grid.GridRowEventArgs e)
        {


            iPartRequestClient objService = new iPartRequestClient();
            CustomProfile profile = CustomProfile.GetProfile();

            try
            {
                if (e.Row.RowType == Obout.Grid.GridRowType.DataRow)
                {
                    Obout.Grid.GridDataControlFieldCell cell = e.Row.Cells[10] as Obout.Grid.GridDataControlFieldCell;

                    DropDownList ddl = cell.FindControl("ddlUOM") as DropDownList;
                    HiddenField hdnUOM = cell.FindControl("hdnMyUOM") as HiddenField;
                    Label rowQtySpn = e.Row.Cells[12].FindControl("rowQtyTotal") as Label;


                    TextBox txtUsrQty = e.Row.Cells[9].FindControl("txtUsrQty") as TextBox;

                    int ProdID = Convert.ToInt32(e.Row.Cells[0].Text);

                    decimal CrntStock = Convert.ToDecimal(e.Row.Cells[7].Text);
                    decimal moq = Convert.ToDecimal(e.Row.Cells[5].Text);

                    string hdnUOMText = e.Row.Cells[10].Text;
                    string hdnUOM1 = e.Row.Cells[11].Text;
                    /*New Price Added*/
                    TextBox txtUsrPrice = e.Row.Cells[13].FindControl("txtUsrPrice") as TextBox; //txtUsrPrice.Enabled = false;   //Product Price
                    Label rowPriceTotal = e.Row.Cells[16].FindControl("rowPriceTotal") as Label;

                    int pricechange = objService.GetDeptPriceChange(long.Parse(hdnselectedDept.Value), profile.DBConnection._constr);
                    if (pricechange == 1)
                    {
                        txtUsrPrice.Enabled = true;
                    }
                    else { txtUsrPrice.Enabled = false; }
                    /*New Price Added*/
                    int isprichange = Convert.ToInt32(e.Row.Cells[20].Text);
                    if (isprichange == 1)
                    {
                        e.Row.BackColor = System.Drawing.Color.DarkCyan;
                        e.Row.ForeColor = System.Drawing.Color.Red;

                    }

                    DataSet dsUOM = new DataSet();
                    dsUOM = objService.GetUOMofSelectedProduct(ProdID, profile.DBConnection._constr);

                    ddl.DataSource = dsUOM;
                    ddl.DataTextField = "Description";
                    ddl.DataValueField = "UMOGroup";
                    ddl.DataBind();

                    if (Convert.ToInt64(HttpContext.Current.Session["PORRequestID"].ToString()) > 0)
                    {

                        long ReqId = Convert.ToInt64(HttpContext.Current.Session["PORRequestID"].ToString());
                        string selectedUomTmpl = objService.GetSelectedUom(ReqId, ProdID, profile.DBConnection._constr);
                        DataTable dt = new DataTable();
                        decimal SelectedQty = 0, SelectedUOM = 0;
                        if (selectedUomTmpl != "0")
                        {

                            dsUOM.Tables[0].DefaultView.RowFilter = "UOMID = " + hdnUOM1 + "";
                            dt = (dsUOM.Tables[0].DefaultView).ToTable();
                            string group = dt.Rows[0]["UMOGroup"].ToString();
                            ddl.SelectedValue = group;

                            SelectedQty = decimal.Parse(dt.Rows[0]["Quantity"].ToString());
                            SelectedUOM = decimal.Parse(dt.Rows[0]["UOMID"].ToString());
                        }
                        else
                        {
                            dsUOM.Tables[0].DefaultView.RowFilter = "UOMID = " + hdnUOM1 + "";
                            dt = (dsUOM.Tables[0].DefaultView).ToTable();
                            string group = dt.Rows[0]["UMOGroup"].ToString();
                            ddl.SelectedValue = group;
                            SelectedQty = decimal.Parse(dt.Rows[0]["Quantity"].ToString());
                            SelectedUOM = decimal.Parse(dt.Rows[0]["UOMID"].ToString());

                        }

                        rowQtySpn.Text = txtUsrQty.Text;
                        decimal UserQty = decimal.Parse(txtUsrQty.Text.ToString());



                        rowPriceTotal.Text = e.Row.Cells[16].Text;
                        if (hdnOrderStatus.Value == "1")
                        {
                            UCProductSearch1.Visible = true;

                            decimal rowQty = decimal.Parse(txtUsrQty.Text.ToString());
                            decimal UsrQty = decimal.Parse(txtUsrQty.Text.ToString()); //SelectedQty * rowQty;
                            decimal Price = decimal.Parse(txtUsrPrice.Text.ToString());

                            hdnSelectedQty.Value = SelectedQty.ToString();
                            rowQtySpn.Text = UsrQty.ToString();

                            if (UsrQty > CrntStock)
                            { rowQtySpn.Text = "0"; }
                            else
                            {
                                rowQtySpn.Text = UsrQty.ToString();
                                txtUsrQty.Text = Convert.ToString(UsrQty / SelectedQty);
                            }

                            ddl.Attributes.Add("onchange", "javascript:GetIndex(this,'" + hdnUOM.ClientID.ToString() + "','" + rowQtySpn.ClientID.ToString() + "','" + txtUsrQty.ClientID.ToString() + "'," + CrntStock + "," + e.Row.RowIndex + "," + Price + ",'" + rowPriceTotal.ClientID.ToString() + "'," + moq + ",'" + txtUsrPrice.ClientID.ToString() + "','" + ddl.ClientID.ToString() + "')");
                            txtUsrQty.Attributes.Add("onblur", "javascript:GetIndexQty(this," + SelectedQty + "," + SelectedUOM + ",'" + rowQtySpn.ClientID.ToString() + "','" + txtUsrQty.ClientID.ToString() + "'," + CrntStock + "," + e.Row.RowIndex + "," + Price + ",'" + rowPriceTotal.ClientID.ToString() + "'," + moq + ",'" + txtUsrPrice.ClientID.ToString() + "','" + ddl.ClientID.ToString() + "')");
                            txtUsrPrice.Attributes.Add("onblur", "javascript:GetChangedPrice(this," + SelectedQty + "," + SelectedUOM + ",'" + rowQtySpn.ClientID.ToString() + "','" + txtUsrQty.ClientID.ToString() + "'," + CrntStock + "," + e.Row.RowIndex + ",'" + rowPriceTotal.ClientID.ToString() + "'," + ProdID + ",'" + txtUsrPrice.ClientID.ToString() + "','" + ddl.ClientID.ToString() + "')");
                        }
                        else
                        {
                            txtUsrQty.Text = Convert.ToString(UserQty / SelectedQty);
                            UCProductSearch1.Visible = false;
                            txtUsrQty.Enabled = false;
                            txtUsrPrice.Enabled = false;
                            ddl.Enabled = false;
                        }

                    }
                    else if (Convert.ToInt64(HttpContext.Current.Session["TemplateID"].ToString()) > 0)
                    {
                        long TemplID = Convert.ToInt64(HttpContext.Current.Session["TemplateID"].ToString());
                        string selectedUom = objService.GetSelectedUomTemplate(TemplID, ProdID, profile.DBConnection._constr);

                        dsUOM.Tables[0].DefaultView.RowFilter = "UOMID = " + hdnUOM1 + "";
                        DataTable dt = (dsUOM.Tables[0].DefaultView).ToTable();
                        string group = dt.Rows[0]["UMOGroup"].ToString();
                        ddl.SelectedValue = group;


                        rowQtySpn.Text = txtUsrQty.Text;
                        decimal SelectedQty = decimal.Parse(dt.Rows[0]["Quantity"].ToString());
                        decimal SelectedUOM = decimal.Parse(dt.Rows[0]["UOMID"].ToString());
                        decimal rowQty = decimal.Parse(txtUsrQty.Text.ToString());
                        decimal UsrQty = decimal.Parse(txtUsrQty.Text.ToString()); //SelectedQty * rowQty;
                        decimal Price = decimal.Parse(txtUsrPrice.Text.ToString());
                        rowPriceTotal.Text = e.Row.Cells[16].Text;

                        hdnSelectedQty.Value = SelectedQty.ToString();
                        rowQtySpn.Text = UsrQty.ToString();

                        if (UsrQty > CrntStock)
                        { rowQtySpn.Text = "0"; }
                        else
                        {
                            rowQtySpn.Text = UsrQty.ToString();
                            txtUsrQty.Text = Convert.ToString(UsrQty / SelectedQty);
                        }

                        ddl.Attributes.Add("onchange", "javascript:GetIndex(this,'" + hdnUOM.ClientID.ToString() + "','" + rowQtySpn.ClientID.ToString() + "','" + txtUsrQty.ClientID.ToString() + "'," + CrntStock + "," + e.Row.RowIndex + "," + Price + ",'" + rowPriceTotal.ClientID.ToString() + "'," + moq + ",'" + txtUsrPrice.ClientID.ToString() + "','" + ddl.ClientID.ToString() + "')");
                        txtUsrQty.Attributes.Add("onblur", "javascript:GetIndexQty(this," + SelectedQty + "," + SelectedUOM + ",'" + rowQtySpn.ClientID.ToString() + "','" + txtUsrQty.ClientID.ToString() + "'," + CrntStock + "," + e.Row.RowIndex + "," + Price + ",'" + rowPriceTotal.ClientID.ToString() + "'," + moq + ",'" + txtUsrPrice.ClientID.ToString() + "','" + ddl.ClientID.ToString() + "')");
                        txtUsrPrice.Attributes.Add("onblur", "javascript:GetChangedPrice(this," + SelectedQty + "," + SelectedUOM + ",'" + rowQtySpn.ClientID.ToString() + "','" + txtUsrQty.ClientID.ToString() + "'," + CrntStock + "," + e.Row.RowIndex + ",'" + rowPriceTotal.ClientID.ToString() + "'," + ProdID + ",'" + txtUsrPrice.ClientID.ToString() + "','" + ddl.ClientID.ToString() + "')");
                    }
                    else
                    {
                        dsUOM.Tables[0].DefaultView.RowFilter = "UOMID = " + hdnUOM1 + "";
                        DataTable dt = (dsUOM.Tables[0].DefaultView).ToTable();
                        string group = dt.Rows[0]["UMOGroup"].ToString();
                        ddl.SelectedValue = group;

                        decimal SelectedQty = decimal.Parse(dt.Rows[0]["Quantity"].ToString());
                        decimal SelectedUOM = decimal.Parse(dt.Rows[0]["UOMID"].ToString());

                        decimal rowQty = decimal.Parse(txtUsrQty.Text.ToString());
                        decimal UsrQty = SelectedQty * rowQty;
                        decimal Price = decimal.Parse(txtUsrPrice.Text.ToString());

                        hdnSelectedQty.Value = SelectedQty.ToString();
                        rowQtySpn.Text = UsrQty.ToString();
                        decimal TotalPrice = Price * UsrQty;

                        rowPriceTotal.Text = TotalPrice.ToString();
                        if (UsrQty > CrntStock)
                        { rowQtySpn.Text = "0"; }
                        else
                        {
                            rowQtySpn.Text = UsrQty.ToString();
                        }
                        ddl.Attributes.Add("onchange", "javascript:GetIndex(this,'" + hdnUOM.ClientID.ToString() + "','" + rowQtySpn.ClientID.ToString() + "','" + txtUsrQty.ClientID.ToString() + "'," + CrntStock + "," + e.Row.RowIndex + "," + Price + ",'" + rowPriceTotal.ClientID.ToString() + "'," + moq + ",'" + txtUsrPrice.ClientID.ToString() + "','" + ddl.ClientID.ToString() + "')");

                        txtUsrQty.Attributes.Add("onblur", "javascript:GetIndexQty(this," + SelectedQty + "," + SelectedUOM + ",'" + rowQtySpn.ClientID.ToString() + "','" + txtUsrQty.ClientID.ToString() + "'," + CrntStock + "," + e.Row.RowIndex + "," + Price + ",'" + rowPriceTotal.ClientID.ToString() + "'," + moq + ",'" + txtUsrPrice.ClientID.ToString() + "','" + ddl.ClientID.ToString() + "')");

                        txtUsrPrice.Attributes.Add("onblur", "javascript:GetChangedPrice(this," + SelectedQty + "," + SelectedUOM + ",'" + rowQtySpn.ClientID.ToString() + "','" + txtUsrQty.ClientID.ToString() + "'," + CrntStock + "," + e.Row.RowIndex + ",'" + rowPriceTotal.ClientID.ToString() + "'," + ProdID + ",'" + txtUsrPrice.ClientID.ToString() + "','" + ddl.ClientID.ToString() + "')");
                    }

                }
            }
            catch (System.Exception ex) { Login.Profile.ErrorHandling(ex, this, "PartRequestEntry.aspx", "Grid1_OnRowDataBound"); }
            finally { objService.Close(); }


        }
        protected void Grid1_RowCommand(object sender, Obout.Grid.GridRowEventArgs e)
        {
            iPartRequestClient objService = new iPartRequestClient();
            CustomProfile profile = CustomProfile.GetProfile();

            try
            {
                Obout.Grid.GridDataControlFieldCell cell = e.Row.Cells[6] as Obout.Grid.GridDataControlFieldCell;
                DropDownList ddl = cell.FindControl("ddlUOM") as DropDownList;

                ddl.Attributes.Add("onchange", "javascript:GetIndex('" + ddl.SelectedIndex + "'," + e.Row.RowIndex + ")");
            }
            catch { }
            finally { objService.Close(); }
        }

        protected void gvApprovalRemarkBind(long RequestID)
        {
            iPartRequestClient objService = new iPartRequestClient();
            CustomProfile profile = CustomProfile.GetProfile();
            gvApprovalRemark.DataSource = null;
            gvApprovalRemark.DataBind();
            DataSet dsGetApprovalDetail = new DataSet();
            if (profile.Personal.UserType == "Admin")
            {
                dsGetApprovalDetail = objService.GetApprovalDetailsNewAdmin(RequestID, profile.DBConnection._constr);
            }
            else
            {
                dsGetApprovalDetail = objService.GetApprovalDetailsNew(RequestID, profile.Personal.UserID, profile.DBConnection._constr);
            }
            gvApprovalRemark.DataSource = dsGetApprovalDetail;
            gvApprovalRemark.DataBind();
        }

        protected void gvApprovalRemark_OnRebind(object sender, EventArgs e)
        {
            gvApprovalRemarkBind(Convert.ToInt64(HttpContext.Current.Session["PORRequestID"].ToString()));
        }

        protected void GetTemplateDetails(string hdnSelTemplateID)
        {
            iPartRequestClient objService = new iPartRequestClient();
            mRequestTemplateHead ReqTemHead = new mRequestTemplateHead();
            try
            {
                /*Bind Template Details*/
                CustomProfile profile = CustomProfile.GetProfile();
                ReqTemHead = objService.GetTemplateOrderHead(Convert.ToInt64(hdnSelTemplateID), profile.DBConnection._constr);
                if (ReqTemHead != null)
                {
                    // lblSelectedTemplateTitle.Text = ReqTemHead.TemplateTitle;

                    long SiteID = long.Parse(ReqTemHead.Department.ToString());
                    ddlSites.SelectedIndex = ddlSites.Items.IndexOf(ddlSites.Items.FindByValue(ReqTemHead.Department.ToString()));
                    hdnselectedDept.Value = SiteID.ToString(); Session["DeptID"] = SiteID.ToString();

                    txtTemplateTitleNew.Text = ReqTemHead.TemplateTitle;
                    ddlAccessTypeNew.SelectedIndex = ddlAccessTypeNew.Items.IndexOf(ddlAccessTypeNew.Items.FindByValue(ReqTemHead.Accesstype.ToString()));
                    // ddlAccessType.SelectedValue = ReqTemHead.Accesstype;
                    //    ddlContact1.SelectedIndex = ddlContact1.Items.IndexOf(ddlContact1.Items.FindByValue(ReqTemHead.Contact1.ToString()));
                    //  ddlContact2.DataSource = WMGetContactPerson2Lst(Convert.ToInt64(ReqTemHead.Department), Convert.ToInt64(ddlContact1.SelectedIndex));
                    //ddlContact2.DataBind();
                    //ddlContact2.SelectedIndex = ddlContact2.Items.IndexOf(ddlContact2.Items.FindByValue(ReqTemHead.Contact2.ToString()));
                    //ddlAddress.SelectedIndex = ddlAddress.Items.IndexOf(ddlAddress.Items.FindByValue(ReqTemHead.Address.ToString()));
                    //lblAddress.Text = ddlAddress.SelectedItem.ToString();
                    /*Bind Template Details*/

                    /*Bind Template Product*/
                    DataSet dsTemplatePartLst = new DataSet();
                    dsTemplatePartLst = objService.GetTemplatePartLstByTemplateID(Convert.ToInt64(hdnSelTemplateID), profile.DBConnection._constr);
                    List<POR_SP_GetPartDetail_ForRequest_Result> TemplatePartList = new List<POR_SP_GetPartDetail_ForRequest_Result>();
                    if (dsTemplatePartLst.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i <= dsTemplatePartLst.Tables[0].Rows.Count - 1; i++)
                        {
                            //************ COde for Real time stock check for Template Product list***
                            long skuid = Convert.ToInt64(dsTemplatePartLst.Tables[0].Rows[i]["PrdID"].ToString());
                            UpdateAvailableQty(SiteID, skuid);

                            //TemplatePartList = objService.AddPartIntoRequest_TempData(dsTemplatePartLst.Tables[0].Rows[i]["PrdID"].ToString(), Session.SessionID, profile.Personal.UserID.ToString(), ObjectName, Convert.ToInt64(hdnselectedDept.Value), profile.DBConnection._constr).ToList();
                            TemplatePartList = objService.AddPartIntoRequest_TempData(dsTemplatePartLst.Tables[0].Rows[i]["PrdID"].ToString(), Session.SessionID, profile.Personal.UserID.ToString(), ObjectName, SiteID, profile.DBConnection._constr).ToList();
                            string uom = objService.GetUOMName(Convert.ToInt64(dsTemplatePartLst.Tables[0].Rows[i]["UOMID"].ToString()), profile.DBConnection._constr);
                            POR_SP_GetPartDetail_ForRequest_Result PartRequest = new POR_SP_GetPartDetail_ForRequest_Result();
                            PartRequest.Sequence = i + 1;
                            PartRequest.RequestQty = Convert.ToDecimal(dsTemplatePartLst.Tables[0].Rows[i]["Qty"].ToString()); // Convert.ToDecimal(dictionary["RequestQty"]);
                            PartRequest.Price = Convert.ToDecimal(dsTemplatePartLst.Tables[0].Rows[i]["Price"].ToString());
                            PartRequest.Total = Convert.ToDecimal(dsTemplatePartLst.Tables[0].Rows[i]["Total"].ToString());
                            PartRequest.IsPriceChange = Convert.ToInt16(dsTemplatePartLst.Tables[0].Rows[i]["IsPriceChange"].ToString());
                            PartRequest.UOMID = Convert.ToInt64(dsTemplatePartLst.Tables[0].Rows[i]["UOMID"].ToString());
                            PartRequest.UOM = uom;

                            objService.UpdatePartRequest_TempData12(HttpContext.Current.Session.SessionID, ObjectName, profile.Personal.UserID.ToString(), PartRequest, profile.DBConnection._constr);
                            //objService.UpdatePartRequest_TempData(HttpContext.Current.Session.SessionID, ObjectName, profile.Personal.UserID.ToString(), PartRequest, profile.DBConnection._constr);
                            // TemplatePartList = objService.AddPartIntoRequest_TempData(dsTemplatePartLst.Tables[0].Rows[i]["PrdID"].ToString(), Session.SessionID, profile.Personal.UserID.ToString(), ObjectName, Convert.ToInt64(hdnselectedDept.Value), profile.DBConnection._constr).ToList();

                            TemplatePartList = objService.GetExistingTempDataBySessionIDObjectName(Session.SessionID, profile.Personal.UserID.ToString(), ObjectName, profile.DBConnection._constr).ToList();
                        }
                    }

                    Grid1.DataSource = TemplatePartList;
                    string ChkSerialedCompany = "";
                    if (hdnselectedCompany.Value == "" || hdnselectedCompany.Value == null) { hdnselectedCompany.Value = "0"; }
                    ChkSerialedCompany = objService.ChkSerialedCompany(Convert.ToInt64(hdnselectedCompany.Value), profile.DBConnection._constr);
                    if (profile.Personal.UserType == "Super Admin")
                    {
                        Grid1.Columns[0].Visible = false; Grid1.Columns[6].Visible = false; Grid1.Columns[11].Visible = false;
                        Grid1.Columns[17].Visible = false; Grid1.Columns[20].Visible = false;
                        Grid1.Columns[1].Visible = true; Grid1.Columns[2].Visible = true; Grid1.Columns[3].Visible = true;
                        Grid1.Columns[4].Visible = true; Grid1.Columns[5].Visible = true; Grid1.Columns[7].Visible = true;
                        Grid1.Columns[8].Visible = true; Grid1.Columns[9].Visible = true; Grid1.Columns[10].Visible = true;
                        Grid1.Columns[12].Visible = true; Grid1.Columns[13].Visible = true; Grid1.Columns[14].Visible = true;
                        Grid1.Columns[15].Visible = true; Grid1.Columns[16].Visible = true;
                        Grid1.Columns[18].Visible = true;
                        Grid1.Columns[19].Visible = true;
                        Grid1.Columns[18].Width = "5%";
                        Grid1.Columns[19].Width = "5%";
                    }
                    else
                    {
                        if (ChkSerialedCompany == "Yes")
                        {
                            Grid1.Columns[0].Visible = false; Grid1.Columns[6].Visible = false; Grid1.Columns[11].Visible = false;
                            Grid1.Columns[17].Visible = false; Grid1.Columns[20].Visible = false;
                            Grid1.Columns[1].Visible = true; Grid1.Columns[2].Visible = true; Grid1.Columns[3].Visible = true;
                            Grid1.Columns[4].Visible = true; Grid1.Columns[5].Visible = true; Grid1.Columns[7].Visible = true;
                            Grid1.Columns[8].Visible = true; Grid1.Columns[9].Visible = true; Grid1.Columns[10].Visible = true;
                            Grid1.Columns[12].Visible = true; Grid1.Columns[13].Visible = true;
                            Grid1.Columns[14].Visible = true;
                            Grid1.Columns[15].Visible = true;
                            Grid1.Columns[16].Visible = true;
                            Grid1.Columns[18].Visible = true;
                            Grid1.Columns[19].Visible = true;
                            Grid1.Columns[18].Width = "5%";
                            Grid1.Columns[19].Width = "5%";

                        }
                        else
                        {
                            Grid1.Columns[0].Visible = false; Grid1.Columns[6].Visible = false; Grid1.Columns[11].Visible = false;
                            Grid1.Columns[17].Visible = false; Grid1.Columns[20].Visible = false;

                            Grid1.Columns[1].Visible = true; Grid1.Columns[2].Visible = true; Grid1.Columns[3].Visible = true;
                            Grid1.Columns[4].Visible = true; Grid1.Columns[5].Visible = true; Grid1.Columns[7].Visible = true;
                            Grid1.Columns[8].Visible = true; Grid1.Columns[9].Visible = true; Grid1.Columns[10].Visible = true;
                            Grid1.Columns[12].Visible = true; Grid1.Columns[13].Visible = true; Grid1.Columns[14].Visible = true;
                            Grid1.Columns[15].Visible = true; Grid1.Columns[16].Visible = true;

                            Grid1.Columns[18].Visible = false;
                            Grid1.Columns[19].Visible = false;
                            Grid1.Columns[18].Width = "0%";
                            Grid1.Columns[19].Width = "0%";

                        }
                    }
                    Grid1.DataBind();

                    /*Bind Template Product*/
                    decimal totQty = WMGetTotalQty(); txtTotalQty.Text = totQty.ToString();
                    decimal GrandTot = WMGetTotal(); txtGrandTotal.Text = GrandTot.ToString();
                }
            }
            catch (System.Exception ex) { Login.Profile.ErrorHandling(ex, this, "PartRequestEntry.aspx", "GetTemplateDetails"); }
            finally { objService.Close(); }

        }

        protected void ddlUOM_SelectedIndexChanged(long selid)
        {

        }


        [WebMethod]
        public static string WMSaveTemplateHead(object obj1)
        {
            string result = "";
            iPartRequestClient objService = new iPartRequestClient();
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                mRequestTemplateHead ReqTemplHead = new mRequestTemplateHead();

                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary = (Dictionary<string, object>)obj1;

                ReqTemplHead.TemplateTitle = dictionary["TemplateTitle"].ToString();
                long TemplTitle = objService.ChkTemplateTitle(ReqTemplHead.TemplateTitle, profile.DBConnection._constr);
                if (TemplTitle == 0)
                {
                    ReqTemplHead.Accesstype = dictionary["Accesstype"].ToString();
                    ReqTemplHead.Department = Convert.ToInt64(dictionary["StoreId"].ToString());
                    //ReqTemplHead.Customer = Convert.ToInt64(ddlCompany.SelectedValue.ToString());
                    ReqTemplHead.Active = "Yes";
                    ReqTemplHead.CreatedBy = profile.Personal.UserID;
                    ReqTemplHead.CreatedDate = DateTime.Now;
                    ReqTemplHead.Remark = dictionary["Remark"].ToString();


                    long TemplateHeadID = objService.InsertIntomRequestTemplateHead(ReqTemplHead, profile.DBConnection._constr);

                    if (TemplateHeadID > 0)
                    {
                        objService.FinalSavemRequestTemplateDetail(HttpContext.Current.Session.SessionID, ObjectName, TemplateHeadID, profile.Personal.UserID.ToString(), profile.DBConnection._constr);
                    }

                    result = "Template Saved Successfully";
                }
                else
                {
                    result = "Title Already Available";
                }
            }
            catch (System.Exception ex) { result = "Some error occurred"; Login.Profile.ErrorHandling(ex, "PartRequestEntry.aspx", "WMSaveTemplateHead"); }
            finally { objService.Close(); }

            return result;
        }
        //protected void btnSubmitTemplate_Onclick(object sender, EventArgs e)
        //{
        //    CustomProfile profile = CustomProfile.GetProfile();
        //    iPartRequestClient objService = new iPartRequestClient();

        //    mRequestTemplateHead ReqTemplHead = new mRequestTemplateHead();

        //    try
        //    {
        //        if (txtTemplateTitle.Text == string.Empty)
        //        {
        //            WebMsgBox.MsgBox.Show("Please Enter Template Title.");
        //        }
        //        else if (ddlAccessType.SelectedIndex == 0)
        //        {
        //            WebMsgBox.MsgBox.Show("Please Select Priority.");
        //        }
        //        //else if (txtRemark.Text == string.Empty)
        //        //{
        //        //    WebMsgBox.MsgBox.Show("Please Enter Remark.");
        //        //    txtRemark.Focus();
        //        //}
        //        ////else if (ddlContact1.SelectedIndex == 0)
        //        //else if (hdnselectedCont1.Value == "")
        //        //{
        //        //    WebMsgBox.MsgBox.Show("Please Select Contact 1.");
        //        //    ddlContact1.Focus();
        //        //}
        //        //else if (hdnselectedCont2.Value == "")
        //        //{
        //        //    WebMsgBox.MsgBox.Show("Please Select Contact 2.");
        //        //    ddlContact2.Focus();
        //        //}
        //        //// else if (ddlAddress.SelectedIndex == 0)
        //        //else if (hdnSelAddress.Value == "")
        //        //{
        //        //    WebMsgBox.MsgBox.Show("Please Select Address.");
        //        //    ddlAddress.Focus();
        //        //}

        //        else
        //        {
        //            ReqTemplHead.TemplateTitle = txtTemplateTitle.Text;
        //            ReqTemplHead.Accesstype = ddlAccessType.SelectedItem.ToString();

        //            if (profile.Personal.UserType == "User" || profile.Personal.UserType == "Requester And Approver" || profile.Personal.UserType == "Requester" || profile.Personal.UserType == "Requestor" || profile.Personal.UserType == "Requestor And Approver")
        //            { ReqTemplHead.Department = Convert.ToInt64(ddlSites.SelectedValue.ToString()); }
        //            else { ReqTemplHead.Department = Convert.ToInt64(hdnselectedCompany.Value); }
        //            ReqTemplHead.Customer = Convert.ToInt64(ddlCompany.SelectedValue.ToString());
        //            ReqTemplHead.Active = "Yes";
        //            ReqTemplHead.CreatedBy = profile.Personal.UserID;
        //            ReqTemplHead.CreatedDate = DateTime.Now;
        //            ReqTemplHead.Remark = txtRemark.Text;
        //            //if (profile.Personal.UserType == "User")
        //            if (profile.Personal.UserType == "User" || profile.Personal.UserType == "Requester And Approver" || profile.Personal.UserType == "Requester" || profile.Personal.UserType == "Requestor" || profile.Personal.UserType == "Requestor And Approver")
        //            {
        //                ReqTemplHead.Contact1 = Convert.ToInt64(ddlContact1.SelectedValue.ToString());
        //            }
        //            else
        //            {
        //                ReqTemplHead.Contact1 = Convert.ToInt64(hdnselectedCont1.Value);
        //            }
        //            if (ReqTemplHead.Contact1 > 0)
        //            {
        //                ReqTemplHead.Contact2 = Convert.ToInt64(hdnselectedCont2.Value);
        //            }
        //            else { ReqTemplHead.Contact2 = 0; }
        //            //if (profile.Personal.UserType == "User")
        //            if (profile.Personal.UserType == "User" || profile.Personal.UserType == "Requester And Approver" || profile.Personal.UserType == "Requester" || profile.Personal.UserType == "Requestor" || profile.Personal.UserType == "Requestor And Approver")
        //            { ReqTemplHead.Address = Convert.ToInt64(ddlAddress.SelectedValue.ToString()); }
        //            else { ReqTemplHead.Address = Convert.ToInt64(hdnSelAddress.Value); }

        //            //long TemplateHeadID = objService.InsertIntomRequestTemplateHead(ReqTemplHead, profile.DBConnection._constr);

        //            //if (TemplateHeadID > 0)
        //            //{
        //            //    objService.FinalSavemRequestTemplateDetail(HttpContext.Current.Session.SessionID, ObjectName, TemplateHeadID, profile.Personal.UserID.ToString(), profile.DBConnection._constr);
        //            //    WebMsgBox.MsgBox.Show("Template Saved Successfully");
        //            //    txtTemplateTitle.Text = "";
        //            //    ddlAccessType.SelectedIndex = 0;
        //            //}
        //        }
        //    }
        //    catch { }
        //    finally { objService.Close(); }
        //}

        protected void GVInboxPOR_OnRebind(object sender, EventArgs e)
        {
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                iPartRequestClient objService = new iPartRequestClient();
                DataSet ds = new DataSet();
                long RequestID = long.Parse(Session["PORRequestID"].ToString());
                ds = objService.GetCorrespondance(RequestID, profile.DBConnection._constr);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GVInboxPOR.DataSource = ds;
                    GVInboxPOR.DataBind();
                }
            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, "Part Request", "GVInboxPOR_OnRebind");
            }

        }

        [WebMethod]
        public static string WMGetDepartmentSession(string Dept)
        {

            CustomProfile profile = CustomProfile.GetProfile();
            iPartRequestClient objService = new iPartRequestClient();
            Page objp = new Page();
            objp.Session["DeptID"] = Dept;
            DataSet ds = new DataSet();
            string isERP = "0";
            ds = objService.chkisERPAutoApproval(Dept, profile.DBConnection._constr);
            if (ds.Tables[0].Rows.Count > 0)
            {
                isERP = ds.Tables[0].Rows[0]["AutoApproval"].ToString();
            }
            return isERP;
        }

        [WebMethod]
        public static string WMGetRequestorIDLoc(string Requestor)
        {
            Page obj = new Page();
            obj.Session["RequestorID"] = Requestor;
            return Requestor;
        }

        [WebMethod]
        public static string WMGetApproverForApprove(long AprvlID, long requestID, long DeligateID)
        {
            string result = "";
            CustomProfile profile = CustomProfile.GetProfile();
            long UsrID = profile.Personal.UserID;
            if (DeligateID == UsrID)
            {
                result = requestID.ToString();
            }
            else if (AprvlID == UsrID)
            {
                result = requestID.ToString();
            }
            else
            {
                result = "AccessDenied";
            }
            return result;
        }

        [WebMethod]
        public static string WMGetApproverForReject(long AprvlID, long requestID, long DeligateID)
        {
            string result = "";
            CustomProfile profile = CustomProfile.GetProfile();
            long UsrID = profile.Personal.UserID;
            if (DeligateID == UsrID)
            {
                result = requestID.ToString();
            }
            else if (AprvlID == UsrID)
            {
                result = requestID.ToString();
            }
            else
            {
                result = "AccessDenied";
            }
            return result;
        }

        protected void imgBtnView_OnClick(object sender, ImageClickEventArgs e)
        {
            ImageButton imgbtn = (ImageButton)sender;

            var CorID = imgbtn.ToolTip.ToString();

            Session["CORID"] = CorID.ToString();

            //string path = "../PowerOnRent/Correspondance.aspx?CORID='" + CorID + "'";
            //string s = "window.open('" + path + "','width=300,height=100,left=100,top=100,resizable=yes,toolbar=no,scrollbars=no,');";
            //ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);

            // ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('../PowerOnRent/Correspondance.aspx?CORID='" + CorID + "'', 'popup_window', 'height=550px, width=825px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=300; screenY=50');", true);
            // ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);

            //  Response.Write("<script type='text/javascript'> window.open('../PowerOnRent/Correspondance.aspx?CORID='" + CorID + "'', null, 'height=550px, width=825px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=300; screenY=50');</script>");
            //Response.Write("window.open('../PowerOnRent/Correspondance.aspx?CORID='"+ CorID +"'', null, 'height=550px, width=825px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=300; screenY=50');");            
            //Response.Write("</script>");
        }

        public List<mTerritory> WMGetSelDept(int Cmpny, long UserID)
        {
            List<mTerritory> SiteLst = new List<mTerritory>();
            iUCCommonFilterClient UCCommonFilter = new iUCCommonFilterClient();
            CustomProfile profile = CustomProfile.GetProfile();

            SiteLst = UCCommonFilter.GetAddedDepartmentList(Cmpny, UserID, profile.DBConnection._constr).ToList();

            return SiteLst;
        }

        //protected void btnSubmit_Onclick(object sender, EventArgs e)
        //{
        //    //if (txtProductCode.Text == string.Empty)
        //    //{
        //    //    WebMsgBox.MsgBox.Show("Please Enter Product Code");
        //    //}
        //    if (txtProductName.Text == string.Empty)
        //    {
        //        WebMsgBox.MsgBox.Show("Please Enter Product Name");
        //    }

        //    try
        //    {
        //        string state;
        //        CustomProfile profile = CustomProfile.GetProfile();
        //        //if (checkduplicate() == "")
        //        //{
        //            iProductMasterClient productclient = new iProductMasterClient();
        //            mProduct obj = new mProduct();

        //            state = "AddNew";
        //            obj.CreatedBy = profile.Personal.UserID.ToString();
        //            obj.CreationDate = DateTime.Now;

        //            obj.ProductTypeID = 1;
        //            obj.ProductCategoryID = 2;
        //            obj.ProductSubCategoryID = 6;
        //           // obj.ProductCode = txtProductCode.Text.ToString().Trim();
        //            obj.ProductCode = "New Product"+ " " + DateTime.Now.ToString("ddMMyy HHmmss")+" " + profile.Personal.UserID.ToString();

        //            obj.Name = txtProductName.Text.ToString().Trim();
        //            obj.Description = txtDesc.Text.ToString().Trim();
        //            obj.UOMID = 17;
        //            obj.PrincipalPrice = 1;
        //            obj.FixedDiscount = 0;
        //            obj.FixedDiscountPercent =Convert.ToBoolean(0);
        //            obj.Installable = Convert.ToBoolean(1);
        //            obj.AMC = Convert.ToBoolean(0);
        //            obj.WarrantyDays = 0;
        //            obj.GuaranteeDays = 0;
        //            obj.Active = "Y";

        //            hdnprodID.Value = productclient.FinalSaveProductDetailByProductID(obj, profile.DBConnection._constr).ToString();

        //            productclient.Close();

        //            Grid1_OnRebind(sender,e);
        //        //}
        //    }
        //    catch (System.Exception ex)
        //    {
        //        Login.Profile.ErrorHandling(ex, this, "PartRequestEntry", "btnSubmit_Onclick");
        //    }
        //    finally
        //    {
        //    }
        //    //txtProductCode.Text = "";
        //    txtProductName.Text = "";
        //    txtDesc.Text = "";
        //}


        //public string checkduplicate()
        //{
        //    try
        //    {
        //        CustomProfile profile = CustomProfile.GetProfile();
        //        iProductMasterClient productclient = new iProductMasterClient();
        //        string result="";

        //        result = productclient.checkDuplicateRecord(txtProductName.Text.Trim(), profile.DBConnection._constr);
        //        if (result != string.Empty)
        //        {
        //            WebMsgBox.MsgBox.Show(result);
        //        }
        //        txtProductName.Focus();
        //        return result;
        //    }
        //    catch (System.Exception ex)
        //    {
        //        Login.Profile.ErrorHandling(ex, this, "PartRequestEntry", "checkDuplicate");
        //        string result = "";
        //        return result;
        //    }
        //    finally
        //    {
        //    }
        //}
        #endregion

        #region GWCVer2
        public void LstStatutoryInfo_OnLoad(object sender, EventArgs e)
        {
            iPartRequestClient objService = new iPartRequestClient();
            CustomProfile profile = CustomProfile.GetProfile();
            if (hdnSelPaymentMethod.Value != "")
            {
                long SelpaymentMethod = long.Parse(hdnSelPaymentMethod.Value);
                if (SelpaymentMethod > 0)
                {
                    if (Convert.ToInt64(Session["PORRequestID"].ToString()) > 0)
                    {
                        if (hdnOrderStatus.Value == "1")
                        {
                            if (hdnPmethodChng.Value == "1")
                            {
                                LstStatutoryInfo.DataSource = objService.GetPaymentMethodFields(SelpaymentMethod, profile.DBConnection._constr);
                                LstStatutoryInfo.DataBind();
                            }
                        }
                    }
                    else
                    {
                        LstStatutoryInfo.DataSource = objService.GetPaymentMethodFields(SelpaymentMethod, profile.DBConnection._constr);
                        LstStatutoryInfo.DataBind();
                    }
                }
            }
            objService.Close();
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
        public static List<mCostCenterMain> WMGetCostCenter(long Dept)
        {
            List<mCostCenterMain> costcenter = new List<mCostCenterMain>();
            CustomProfile profile = CustomProfile.GetProfile();
            iPartRequestClient objService = new iPartRequestClient();
            costcenter = objService.GetCostCenter(Dept, profile.DBConnection._constr).ToList();
            return costcenter;
        }

        [WebMethod]
        public static decimal WMGetTotal()
        {
            iPartRequestClient objService = new iPartRequestClient();
            decimal tot = 0;
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                POR_SP_GetPartDetail_ForRequest_Result PartRequest = new POR_SP_GetPartDetail_ForRequest_Result();
                tot = objService.GetTotalFromTempData(HttpContext.Current.Session.SessionID, ObjectName, profile.Personal.UserID.ToString(), profile.DBConnection._constr);
            }
            catch (System.Exception ex)
            {
                // Login.Profile.ErrorHandling(ex, "PartRequestEntry.aspx", "WMGetTotal");
            }
            finally { objService.Close(); }
            return tot;
        }

        [WebMethod]
        public static decimal WMGetTotalQty()
        {
            iPartRequestClient objService = new iPartRequestClient();
            decimal tot = 0;
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                POR_SP_GetPartDetail_ForRequest_Result PartRequest = new POR_SP_GetPartDetail_ForRequest_Result();
                tot = objService.GetTotalQTYFromTempData(HttpContext.Current.Session.SessionID, ObjectName, profile.Personal.UserID.ToString(), profile.DBConnection._constr);
            }
            catch (System.Exception ex)
            {
                //   Login.Profile.ErrorHandling(ex, "PartRequestEntry.aspx", "WMGetTotalQty");
            }
            finally { objService.Close(); }
            return tot;
        }

        [WebMethod]
        public static long WMGetMaxDeliveryDays(long Dept)
        {
            iPartRequestClient objService = new iPartRequestClient();
            long mdd = 0;
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                mdd = objService.GetMaxDeliveryDaysofDept(Dept, profile.DBConnection._constr);
            }
            catch (System.Exception ex) { Login.Profile.ErrorHandling(ex, "PartRequestEntry.aspx", "WMGetMaxDeliveryDays"); }
            finally { objService.Close(); }
            return mdd;
        }

        public void UC_ExpDeliveryDate_OnLoad(object sender, EventArgs e)
        {
            string mdd = hdnMaxDeliveryDays.Value;
            if (mdd == "") { }
            else
            {
                UC_ExpDeliveryDate.enddate(DateTime.Now.AddDays(int.Parse(mdd)));
            }
        }

        [WebMethod]
        public static string WMGetMandatoryDetails(long pm)
        {
            iPartRequestClient objService = new iPartRequestClient();
            string seq = "0";
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                seq = objService.GetMandatoryFields(pm, profile.DBConnection._constr);
            }
            catch (System.Exception ex) { Login.Profile.ErrorHandling(ex, "PartRequestEntry.aspx", "WMGetMandatoryDetails"); }
            finally { objService.Close(); }
            return seq;
        }

        [WebMethod]
        public static void WmGetPaymentMethodLabelText(string PMLabel, string PMText, long pmID, int Seq, long OrderID)
        {
            iPartRequestClient objService = new iPartRequestClient();
            CustomProfile profile = CustomProfile.GetProfile();
            long ApproverID = 0;
            try
            {
                long StatutoryID = objService.GetStatutoryID(PMLabel, pmID, profile.DBConnection._constr);

                if (pmID == 5) { ApproverID = objService.GetCostCenterApproverID(Convert.ToInt64(PMText), profile.DBConnection._constr); }

                //tStatutoryDetail pmd = new tStatutoryDetail();
                //pmd.ObjectName = "RequestPartDetail";
                //pmd.ReferenceID = OrderID;
                //pmd.StatutoryID = StatutoryID;
                //pmd.StatutoryValue = PMText;
                //pmd.Active = "1";
                //pmd.CreatedBy = profile.Personal.UserID.ToString();
                //pmd.CreatedDate = DateTime.Now;
                //pmd.CompanyID = 0;
                //pmd.Sequence = Seq;
                //pmd.ApproverID = ApproverID;

                //objService.AddIntotStatutory(pmd, profile.DBConnection._constr);

                objService.AddIntotStatutoryNEW("RequestPartDetail", OrderID, StatutoryID, PMText, profile.Personal.UserID, Seq, ApproverID, profile.DBConnection._constr);
            }
            catch (System.Exception ex) { Login.Profile.ErrorHandling(ex, "PartRequestEntry.aspx", "WmGetPaymentMethodLabelText"); }
            finally { objService.Close(); }
        }

        [WebMethod]
        public static int WMGetAccessOfProductChange(int Seq)
        {
            iPartRequestClient objService = new iPartRequestClient(); HttpContext.Current.Session["SEQ"] = Seq;
            CustomProfile profile = CustomProfile.GetProfile();
            int YN = objService.GetPartAccessofUser(Convert.ToInt64(HttpContext.Current.Session["PORRequestID"].ToString()), profile.Personal.UserID, profile.DBConnection._constr);
            return YN;
        }
        [WebMethod]
        public static List<tOrderHead> WMGetTotalQtyGrandTotal()
        {
            iPartRequestClient objService = new iPartRequestClient();
            List<tOrderHead> RequestHead = new List<tOrderHead>();
            CustomProfile profile = CustomProfile.GetProfile();
            RequestHead = objService.GetOrderHeadByOrderIDQTYTotal(Convert.ToInt64(HttpContext.Current.Session["PORRequestID"].ToString()), profile.DBConnection._constr).ToList();
            return RequestHead;
        }
        [WebMethod]
        public static void WMPaymentMethodNone(long pm, long OrderID)
        {
            iPartRequestClient objService = new iPartRequestClient();
            CustomProfile profile = CustomProfile.GetProfile();
            objService.RemoveFromTStatutory(OrderID, profile.DBConnection._constr);
        }

        [WebMethod]
        public static int WMcheckLocationForDepartment(long deptid)
        {
            int loc = 0;
            iPartRequestClient objService = new iPartRequestClient();
            CustomProfile profile = CustomProfile.GetProfile();
            DataSet ds = new DataSet();
            ds = objService.checkLocationForDepartment(deptid, profile.DBConnection._constr);
            if (ds.Tables[0].Rows.Count > 0)
            {
                string Location = ds.Tables[0].Rows[0]["Location"].ToString();
                string imoney = ds.Tables[0].Rows[0]["Emoney"].ToString();
                if (Location == "Yes")
                {
                    loc = 1;
                }
                if (imoney == "True")
                {
                    loc = 1;
                }
            }
            return loc;
        }


        #endregion

        private void loadstring()
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(Session["Lang"].ToString());
                rm = new ResourceManager("PowerOnRentwebapp.App_GlobalResources.Lang", Assembly.GetExecutingAssembly());
                ci = Thread.CurrentThread.CurrentCulture;


                lblTitle.Text = rm.GetString("Title", ci);
                lblCustomerOrderRefNo.Text = rm.GetString("CustomerOrderRefNo", ci);
                lblRequestNumber.Text = rm.GetString("RequestNo", ci);
                lblStatus.Text = rm.GetString("Status", ci);
                lblRequestDate.Text = rm.GetString("RequestDate", ci);
                //   lblPriority.Text = rm.GetString("Priority", ci);
                lblRequestedBy.Text = rm.GetString("RequestedBy", ci);
                lblExpDeliveryDate.Text = rm.GetString("ExpDeliveryDate", ci);
                lblCustomerName.Text = rm.GetString("CustomerName", ci);
                lblDepartment.Text = rm.GetString("Department", ci);
                lblContact1.Text = rm.GetString("conatact1", ci);
                lblContact2.Text = rm.GetString("conatact2", ci);
                lblCustomerAddress.Text = rm.GetString("CustomerAddress", ci);
                lblAddressLabel.Text = rm.GetString("Address", ci);
                lblRemark.Text = rm.GetString("Remark", ci);
                lblrequestpartlist.Text = rm.GetString("RequestPartList", ci);
                btnNewPrduct.Text = rm.GetString("AddNewProduct", ci);
                lblApproval.Text = rm.GetString("Approval", ci);
                lblApprovalHistory.Text = rm.GetString("ApprovalHistory", ci);
                lblDispatch.Text = rm.GetString("Dispatch", ci);
                //  lblIssueNo1.Text = rm.GetString("IssueNo", ci);
                //   lbltranfersite.Text = rm.GetString("transferfromsite", ci);
                //   lblcreatereceipt.Text = rm.GetString("creategoodsreceipt", ci);
                lblshipeddate.Text = rm.GetString("ShippedDate", ci);
                //   lblReceivedDate.Text = rm.GetString("ReceivedDate", ci);
                lblCloseDate.Text = rm.GetString("CloseDate", ci);
                lblRemark1.Text = rm.GetString("Remark", ci);
                lblCorrespondance.Text = rm.GetString("Correspondance", ci);
                lblinbox.Text = rm.GetString("Inbox", ci);
                btnAddNewCorrespondance.Value = rm.GetString("AddNew", ci);
                lblOperationApproval.Text = rm.GetString("OperationApproval", ci);
                lblApproved1.Text = rm.GetString("Approved", ci);
                lblCancelled.Text = rm.GetString("Cancelled", ci);
                lblapprovrevision.Text = rm.GetString("ApproveWithRevision", ci);
                lblDate.Text = rm.GetString("Date", ci);
                lblApprovRemark.Text = rm.GetString("Remark", ci);
                lblrequest.Text = rm.GetString("Request", ci);
                // lblcollapse.Text = rm.GetString("Collapse", ci);


                btnGenerateFromTemplate.Text = rm.GetString("GenerateFromTemplate", ci);
                btnSaveAsTemplateNew.Text = rm.GetString("SaveAsTemplate", ci);
                lblRequestNo.Text = rm.GetString("Generatewhensave", ci);
                lblTemplateTitleNew.Text = rm.GetString("TemplateTitle", ci);
                lblAccessTypeNew.Text = rm.GetString("AccessType", ci);
                lblReceivedDate.Text = rm.GetString("DispatchDate", ci);

                lblPaymentMethod.Text = rm.GetString("PaymentMethod", ci);
                lblDocument.Text = rm.GetString("Document", ci);
                lblTQty.Text = rm.GetString("TotalQuantity", ci);
                lblGTotal.Text = rm.GetString("GrandTotal", ci);


                lblDispatchDetails.Text = rm.GetString("DispatchDetails", ci);
                lblCustomerDetails.Text = rm.GetString("CustomerDetails", ci);
                lblCustName.Text = rm.GetString("CustomerName", ci);
                lblContactNo.Text = rm.GetString("ContactNo", ci);

                lblCustAddress.Text = rm.GetString("Address", ci);

                lblEmail.Text = rm.GetString("EmailID", ci);
                lblLandmark.Text = rm.GetString("Landmark", ci);
                lblZipCode.Text = rm.GetString("ZIPCode", ci);
                lblPaymentDetail.Text = rm.GetString("PaymentDetails", ci);

                lblPaymentMode.Text = rm.GetString("PaymentMode", ci);
                lblCardNo.Text = rm.GetString("CardNo", ci);
                lblPaymentRemark.Text = rm.GetString("Remark", ci);
                lblBankName.Text = rm.GetString("BankName", ci);
                lblPaymentDate.Text = rm.GetString("PaymentDate", ci);
                lblVerified.Text = rm.GetString("Verified", ci);

                lblDecline.Text = rm.GetString("Decline", ci);
                lblPending.Text = rm.GetString("Pending", ci);
                lblDriverDetails.Text = rm.GetString("DriverDetails", ci);
                lblDriverName.Text = rm.GetString("DriverName", ci);
                lblDriverContactNo.Text = rm.GetString("ContactNo", ci);
                lblDriverEmail.Text = rm.GetString("EmailID", ci);
                lblTruckDetail.Text = rm.GetString("TruckDetail", ci);
                lblAssignDate.Text = rm.GetString("AssignDate", ci);
                lblDeliveryType.Text = rm.GetString("DeliveryType", ci);


                btncustomeranalys.Value = rm.GetString("CustomerAnalytics", ci);
                lblorderparameter.Text = rm.GetString("OrderParameter", ci);
                Label4.Text = rm.GetString("OrderParameter", ci);
                Label2.Text = rm.GetString("DispatchStatus", ci);
                lblrno.Text = rm.GetString("ReciptNo", ci);

                lblLocationLabel.Text = rm.GetString("LocationDetails", ci);
                lblLocationID.Text = rm.GetString("LocationID", ci);
            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, "Part Request", "loadstring");
            }


        }


        #region New Change Request Coading

        public void GetUserbyDepartment(long DeptId)
        {

            CustomProfile profile = CustomProfile.GetProfile();
            iPartRequestClient request = new iPartRequestClient();
            long UserID = long.Parse(profile.Personal.UserID.ToString());
            DataSet ds;
            ds = request.GetDeptReqUser(DeptId, profile.DBConnection._constr);
            ddlRequestByUserID.DataSource = ds;
            ddlRequestByUserID.DataTextField = "userName";
            ddlRequestByUserID.DataValueField = "userID";
            ddlRequestByUserID.DataBind();
            ListItem lst = new ListItem { Text = "-Select-", Value = "0" };
            ddlRequestByUserID.Items.Insert(0, lst);
            request.Close();

        }

        [WebMethod]
        public static List<contact> GetDeptReqUsr(string DeptID)
        {

            iPartRequestClient request = new iPartRequestClient();
            CustomProfile profile = CustomProfile.GetProfile();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            List<contact> LocList = new List<contact>();
            try
            {
                ds = request.GetDeptReqUser(long.Parse(DeptID.ToString()), profile.DBConnection._constr);
                dt = ds.Tables[0];


                contact Loc = new contact();
                Loc.Name = "--Select--";
                Loc.Id = "0";
                LocList.Add(Loc);
                Loc = new contact();

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Loc.Id = dt.Rows[i]["userID"].ToString();
                        Loc.Name = dt.Rows[i]["userName"].ToString();
                        LocList.Add(Loc);
                        Loc = new contact();

                    }

                }
            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, "Request Master", "Getdelegate");
            }
            finally
            {
                request.Close();

            }
            return LocList;
        }

        public class contact
        {
            private string _name;
            public string Name
            {
                get { return _name; }
                set { _name = value; }
            }

            private string _id;
            public string Id
            {
                get { return _id; }
                set { _id = value; }
            }
        }

        #endregion


        #region version3   suraj jagtap

        protected void GridDispatchstatus_Select(object sender, Obout.Grid.GridRecordEventArgs e)
        {
            CustomProfile profile = CustomProfile.GetProfile();
            Hashtable selectedrec = (Hashtable)GridDispatchstatus.SelectedRecords[0];
            hdnselectedorderMsidn.Value = selectedrec["OrderNo"].ToString();
        }

        protected void GridDispatchstatus_Rebind(object sender, EventArgs e)
        {
            ddlStatus.DataSource = WMFillStatus();
            ddlStatus.DataBind();
            ShowDispatchDetails(Convert.ToInt64(HttpContext.Current.Session["PORRequestID"].ToString()));
        }


        protected void Showbtncustomeranalys(string OrderNo)
        {
            iPartRequestClient objService = new iPartRequestClient();
            string str = "";
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                str = objService.CheckDeptIsEcomOrNot(Convert.ToInt64(OrderNo), profile.DBConnection._constr);
                if (str == "Yes")
                {
                    //btncustomeranalys.Attributes.Add("style", "display:'';");
                    btncustomeranalys.Visible = true;
                }
                else
                {
                    // btncustomeranalys.Attributes.Add("style", "display:none");
                    btncustomeranalys.Visible = false;
                }
            }
            catch (System.Exception ex) { Login.Profile.ErrorHandling(ex, "PartRequestEntry.aspx", "Showbtncustomeranalys"); }
            finally { objService.Close(); }

        }

        [WebMethod]
        public static string WMCheckAllProductISActiveOrNot(string OrderNo)
        {
            iPartRequestClient objService = new iPartRequestClient();
            string str = "";
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                str = objService.CheckAllProductISActiveOrNot(Convert.ToInt64(OrderNo), profile.DBConnection._constr);
            }
            catch (System.Exception ex) { Login.Profile.ErrorHandling(ex, "PartRequestEntry.aspx", "WMCheckAllProductISActiveOrNot"); }
            finally { objService.Close(); }
            return str;
        }

        private void BindGVOrderParameter(long OrdNo)
        {
            iPartRequestClient objService = new iPartRequestClient();
            DataSet ds = new DataSet();
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                ds = objService.BindGVOrderParameter(Convert.ToInt64(OrdNo), profile.DBConnection._constr);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GVOrderParameter.DataSource = ds;
                    GVOrderParameter.DataBind();
                }
                else
                {
                    ds = null;
                    GVOrderParameter.DataSource = ds;
                    GVOrderParameter.DataBind();
                }
            }
            catch (System.Exception ex) { Login.Profile.ErrorHandling(ex, "PartRequestEntry.aspx", "BindGVOrderParameter"); }
            finally { objService.Close(); }

        }





        #endregion


        [WebMethod]
        public static string WMISProjectSiteDetails(string companyid)
        {
            string ISProjectSiteDetails = "";
            CustomProfile profile = CustomProfile.GetProfile();
            iUCCommonFilterClient UCCommonFilter = new iUCCommonFilterClient();
            try
            {


                ISProjectSiteDetails = UCCommonFilter.ISProjectSiteDetails(Convert.ToString(companyid), profile.DBConnection._constr);


            }
            catch (System.Exception ex) { Login.Profile.ErrorHandling(ex, "PartRequestEntry.aspx", "WMISProjectSiteDetails"); }

            return ISProjectSiteDetails;
        }



        [WebMethod]
        public static string WMISProjectSiteDetails1(string companyid)
        {
            string ISProjectSiteDetails = "";
            CustomProfile profile = CustomProfile.GetProfile();
            iUCCommonFilterClient UCCommonFilter = new iUCCommonFilterClient();
            try
            {


                ISProjectSiteDetails = UCCommonFilter.ISProjectSiteDetails(Convert.ToString(companyid), profile.DBConnection._constr);


            }
            catch (System.Exception ex) { Login.Profile.ErrorHandling(ex, "PartRequestEntry.aspx", "WMISProjectSiteDetails1"); }

            return ISProjectSiteDetails;
        }

        #region serial number
        [WebMethod]
        public static string WMGetSelectedSKUQty(string skuid, string oid)
        {
            string result = "0";
            iPartRequestClient objService = new iPartRequestClient();
            CustomProfile profile = CustomProfile.GetProfile();
            List<PowerOnRentwebapp.PORServicePartRequest.POR_SP_GetPartDetail_ForRequest_Result> RequestPartListqty = new List<PowerOnRentwebapp.PORServicePartRequest.POR_SP_GetPartDetail_ForRequest_Result>();

            try
            {
                string rqty = "0"; int sqty = 0;
                RequestPartListqty = objService.GetSelectedSKUQty1(HttpContext.Current.Session.SessionID, profile.Personal.UserID.ToString(), ObjectName, skuid, profile.DBConnection._constr).ToList();

                POR_SP_GetPartDetail_ForRequest_Result updateRec = new POR_SP_GetPartDetail_ForRequest_Result();
                updateRec = RequestPartListqty.Where(g => g.Prod_ID == Convert.ToInt64(skuid)).FirstOrDefault();
                rqty = Convert.ToString(Convert.ToInt64(updateRec.RequestQty));

                List<PowerOnRentwebapp.PORServicePartRequest.SP_GetPartSerialDetail_Result> RequestPartList1 = new List<PowerOnRentwebapp.PORServicePartRequest.SP_GetPartSerialDetail_Result>();
                RequestPartList1 = objService.GetPartSerialDetailByRequestID(Convert.ToInt64(oid), Convert.ToInt64(skuid), HttpContext.Current.Session.SessionID, profile.Personal.UserID.ToString(), ObjectNameSerial, profile.DBConnection._constr).ToList();
                sqty = Convert.ToInt32(RequestPartList1.Count);
                if (sqty == 0) { result = rqty + "," + "0"; }
                else { result = rqty + "," + "1"; }


            }

            catch (System.Exception ex) { Login.Profile.ErrorHandling(ex, "PartRequestEntry.aspx", "WMGetSelectedSKUQty"); }
            return result;
        }

        [WebMethod]
        public static int WMChkSerialNoaddorNot(string Sid, string Oid)
        {
            int result = 0;
            try
            {
                iPartRequestClient objService = new iPartRequestClient();
                CustomProfile profile = CustomProfile.GetProfile();
                List<PowerOnRentwebapp.PORServicePartRequest.SP_GetPartSerialDetail_Result> RequestPartList1 = new List<PowerOnRentwebapp.PORServicePartRequest.SP_GetPartSerialDetail_Result>();
                RequestPartList1 = objService.GetPartSerialDetailByRequestID(Convert.ToInt64(Oid), Convert.ToInt64(Sid), HttpContext.Current.Session.SessionID, profile.Personal.UserID.ToString(), ObjectNameSerial, profile.DBConnection._constr).ToList();
                result = RequestPartList1.Count;
            }
            catch (System.Exception ex) { Login.Profile.ErrorHandling(ex, "PartRequestEntry.aspx", "WMGetSelectedSKUQty"); }
            return result;
        }





        [WebMethod]
        public static string WMRemoveAssignSkuSerial(string Oid, string Sid, string qty)
        {
            string result = "";
            try
            {
                iPartRequestClient objService = new iPartRequestClient();
                CustomProfile profile = CustomProfile.GetProfile();
                objService.RemoveSkuserialFromRequest_TempData(HttpContext.Current.Session.SessionID, profile.Personal.UserID.ToString(), ObjectNameSerial, Convert.ToInt64(Sid), profile.DBConnection._constr);
                objService.DeleteSkuwiseSerialTemptable(profile.Personal.UserID, Convert.ToInt64(Sid), Convert.ToInt64(Oid), profile.DBConnection._constr);

                result = "1";
            }
            catch (System.Exception ex) { result = "0"; Login.Profile.ErrorHandling(ex, "PartRequestEntry.aspx", "WMGetSelectedSKUQty"); }
            return result;
        }

        [WebMethod]
        public static string WMGetPreviousserialcnt(string Oid, string Sid, string qty)
        {
            string result = "0";
            try
            {
                iPartRequestClient objService = new iPartRequestClient();
                CustomProfile profile = CustomProfile.GetProfile();
                List<PowerOnRentwebapp.PORServicePartRequest.SP_GetPartSerialDetail_Result> RequestPartList1 = new List<PowerOnRentwebapp.PORServicePartRequest.SP_GetPartSerialDetail_Result>();
                RequestPartList1 = objService.GetPartSerialDetailByRequestID(Convert.ToInt64(Oid), Convert.ToInt64(Sid), HttpContext.Current.Session.SessionID, profile.Personal.UserID.ToString(), ObjectNameSerial, profile.DBConnection._constr).ToList();

                result = RequestPartList1.Count.ToString();
            }
            catch (System.Exception ex) { result = "0"; Login.Profile.ErrorHandling(ex, "PartRequestEntry.aspx", "WMGetSelectedSKUQty"); }
            return result;
        }

        [WebMethod]
        public static void WMUpdateRequestQty(object objRequest)
        {
            iPartRequestClient objService = new iPartRequestClient();
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary = (Dictionary<string, object>)objRequest;
                CustomProfile profile = CustomProfile.GetProfile();

                POR_SP_GetPartDetail_ForRequest_Result PartRequest = new POR_SP_GetPartDetail_ForRequest_Result();
                PartRequest.Sequence = Convert.ToInt64(dictionary["Sequence"]);
                PartRequest.RequestQty = Convert.ToDecimal(dictionary["RequestQty"]);

                objService.UpdatePartRequest_TempData(HttpContext.Current.Session.SessionID, ObjectName, profile.Personal.UserID.ToString(), PartRequest, profile.DBConnection._constr);
            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, "PartRequestEntry.aspx", "WMUpdateRequestQty1");
            }
            finally { objService.Close(); }
        }



        [WebMethod]
        public static string WMUpdRequestPartNew(object objRequest)
        {
            string result = "";
            iPartRequestClient objService = new iPartRequestClient();
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary = (Dictionary<string, object>)objRequest;
                CustomProfile profile = CustomProfile.GetProfile();

                string uom = objService.GetUOMName(Convert.ToInt64(dictionary["UOMID"]), profile.DBConnection._constr);

                POR_SP_GetPartDetail_ForRequest_Result PartRequest = new POR_SP_GetPartDetail_ForRequest_Result();
                PartRequest.Sequence = Convert.ToInt64(dictionary["Sequence"]);
                PartRequest.RequestQty = Convert.ToDecimal(dictionary["RequestQty"]);
                PartRequest.UOM = uom;
                PartRequest.UOMID = Convert.ToInt64(dictionary["UOMID"]);
                PartRequest.Total = Convert.ToDecimal(dictionary["Total"]);



                List<POR_SP_GetPartDetail_ForRequest_Result> TemplatePartList = new List<POR_SP_GetPartDetail_ForRequest_Result>();
                TemplatePartList = objService.GetExistingTempDataBySessionIDObjectName(HttpContext.Current.Session.SessionID, profile.Personal.UserID.ToString(), ObjectName, profile.DBConnection._constr).ToList();
                POR_SP_GetPartDetail_ForRequest_Result updateRec = new POR_SP_GetPartDetail_ForRequest_Result();
                updateRec = TemplatePartList.Where(g => g.Sequence == Convert.ToInt64(dictionary["Sequence"])).FirstOrDefault();
                if (updateRec.SerialFlag == "Yes")
                {
                    int rqty = 0;
                    long Sid = Convert.ToInt64(updateRec.Prod_ID);
                    List<PowerOnRentwebapp.PORServicePartRequest.SP_GetPartSerialDetail_Result> RequestPartList1 = new List<PowerOnRentwebapp.PORServicePartRequest.SP_GetPartSerialDetail_Result>();
                    RequestPartList1 = objService.GetPartSerialDetailByRequestID(0, Convert.ToInt64(Sid), HttpContext.Current.Session.SessionID, profile.Personal.UserID.ToString(), ObjectNameSerial, profile.DBConnection._constr).ToList();
                    rqty = Convert.ToInt32(RequestPartList1.Count.ToString());
                    if (rqty == 0)
                    {
                        result = "sameqty";
                        objService.UpdatePartRequest_TempData1(HttpContext.Current.Session.SessionID, ObjectName, profile.Personal.UserID.ToString(), PartRequest, profile.DBConnection._constr);
                    }
                    else
                    {
                        if (rqty == Convert.ToInt32(dictionary["RequestQty"]))
                        {
                            result = "sameqty";
                            objService.UpdatePartRequest_TempData1(HttpContext.Current.Session.SessionID, ObjectName, profile.Personal.UserID.ToString(), PartRequest, profile.DBConnection._constr);
                        }
                        else
                        {
                            result = "notsameqty" + "," + Convert.ToString(dictionary["RequestQty"]) + "," + Convert.ToString(Sid) + "," + Convert.ToString(rqty);
                        }
                    }
                }
                else
                {
                    objService.UpdatePartRequest_TempData1(HttpContext.Current.Session.SessionID, ObjectName, profile.Personal.UserID.ToString(), PartRequest, profile.DBConnection._constr);
                    result = "sameqty";
                }

                // objService.UpdatePartRequest_TempData1(HttpContext.Current.Session.SessionID, ObjectName, profile.Personal.UserID.ToString(), PartRequest, profile.DBConnection._constr);
            }
            catch (System.Exception ex)
            {
                //      Login.Profile.ErrorHandling(ex, "PartRequestEntry.aspx", "WMUpdRequestPart");
            }
            finally { objService.Close(); }
            return result;
        }



        [WebMethod]
        public static string WMRemoveAssignSkuSerialNew(string Oid, string Sid, string qty)
        {
            string result = "";
            try
            {
                iPartRequestClient objService = new iPartRequestClient();
                CustomProfile profile = CustomProfile.GetProfile();
                objService.RemoveSkuserialFromRequest_TempData(HttpContext.Current.Session.SessionID, profile.Personal.UserID.ToString(), ObjectNameSerial, Convert.ToInt64(Sid), profile.DBConnection._constr);
                objService.DeleteSkuwiseSerialTemptable(profile.Personal.UserID, Convert.ToInt64(Sid), Convert.ToInt64(Oid), profile.DBConnection._constr);
                objService.UpdatePartRequest_TempDataNew(HttpContext.Current.Session.SessionID, profile.Personal.UserID.ToString(), ObjectName, Convert.ToInt64(Sid), Convert.ToDecimal(qty), profile.DBConnection._constr);
                result = "1";
            }
            catch (System.Exception ex) { result = "0"; Login.Profile.ErrorHandling(ex, "PartRequestEntry.aspx", "WMGetSelectedSKUQty"); }
            return result;
        }

        #endregion

        #region stock adjustment through real time stock service method
        public void UpdateAvailableQty(long Deptid, long skuid)
        {
            CheckStockAvailableClient RealTimeStock = new CheckStockAvailableClient();
            // iUserCreationClient userClient = new iUserCreationClient();
            iProductMasterClient productClient = new iProductMasterClient();
            UCProductSearchService.iUCProductSearchClient productSearchService = new UCProductSearchService.iUCProductSearchClient();
            CustomProfile profile = CustomProfile.GetProfile();
            DataSet dsschema = new DataSet();
            DataSet dsnew = new DataSet();
            try
            {
                string ProductCode = "", DeptCode = "", Schemaname = "", DatabaseName = "", ConnectionString = "";
                decimal AvailableBalance = 0, ResurveQty = 0;
                dsnew = productSearchService.GetProdAvailableBal(skuid, profile.DBConnection._constr);
                // dsnew = userClient.GetProdAvailableBal1(skuid, profile.DBConnection._constr);
                if (dsnew.Tables[0].Rows.Count > 0)
                {
                    ProductCode = dsnew.Tables[0].Rows[0]["ProductCode"].ToString();
                    AvailableBalance = decimal.Parse(dsnew.Tables[0].Rows[0]["AvailableBalance"].ToString());
                    ResurveQty = decimal.Parse(dsnew.Tables[0].Rows[0]["ResurveQty"].ToString());
                    Deptid = long.Parse(dsnew.Tables[0].Rows[0]["StoreId"].ToString());
                    DeptCode = dsnew.Tables[0].Rows[0]["StoreCode"].ToString();
                }
                dsnew.Dispose();
                decimal totalQTy = AvailableBalance + ResurveQty;
                dsschema = productClient.GetDatabaseSchemaforDept(Deptid, profile.DBConnection._constr);
                // dsschema = userClient.GetDatabaseSchemaforDept1(Deptid, profile.DBConnection._constr);
                if (dsschema.Tables[0].Rows.Count > 0)
                {
                    DatabaseName = dsschema.Tables[0].Rows[0]["DatabaseName"].ToString();
                    ConnectionString = dsschema.Tables[0].Rows[0]["ConnectionString"].ToString();
                    Schemaname = dsschema.Tables[0].Rows[0]["Schemaname"].ToString();
                    string wmsstorecode = dsschema.Tables[0].Rows[0]["wmsstorecode"].ToString();
                    if (string.IsNullOrEmpty(wmsstorecode))
                    {
                    }
                    else
                    {
                        DeptCode = wmsstorecode;
                    }
                }
                if (Schemaname != "")
                {
                    string companyisrealtime = productClient.GetcompanyRealtimestock(Deptid, profile.DBConnection._constr);
                    if (companyisrealtime == "Yes")
                    {
                        // decimal RealTimeStockQty = 0;
                        decimal RealTimeStockQty = RealTimeStock.CheckAvailableQty(ProductCode, DeptCode, DatabaseName, ConnectionString, Schemaname);
                        if (totalQTy != RealTimeStockQty)
                        {
                            productSearchService.Createtransaction(Convert.ToString(ProductCode), Convert.ToString(Deptid), RealTimeStockQty, profile.DBConnection._constr);
                        }
                    }

                }

            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, "ProductSearch.aspx.cs", "RebindGrid");
            }
            finally
            {
                // RealTimeStock.Close();
                productSearchService.Close();
                productClient.Close();
                dsschema.Dispose();
            }
        }


        #endregion
        #region ERP

        public class orderDetailList
        {
            [JsonProperty(Order = 1)]
            public string orderId { get; set; }

            [JsonProperty(Order = 2)]
            public string orderType { get; set; }

            [JsonProperty(Order = 3)]
            public string orderDate { get; set; }

            [JsonProperty(Order = 4)]
            public string orderCreationDate { get; set; }

            [JsonProperty(Order = 5)]
            public string locationCode { get; set; }

            [JsonProperty(Order = 6)]
            public string locationName { get; set; }

            [JsonProperty(Order = 7)]
            public string orderCurrency { get; set; }

            [JsonProperty(Order = 8)]

            public List<orderLines> orderLines;
        }
        public class orderLines
        {
            [JsonProperty(Order = 1)]
            public string lineNumber { get; set; }

            [JsonProperty(Order = 2)]
            public string skuCode { get; set; }

            [JsonProperty(Order = 3)]
            public decimal lineAmount { get; set; }

            [JsonProperty(Order = 4)]
            public decimal lineQuantity { get; set; }
        }
        #endregion

        #region Ecomm Order Installment Grid Bind 22-07-2025 Device Installment CR

        private void BindGVEcomInstallmentDetails(long OrderID)

        {

            iPartRequestClient objService = new iPartRequestClient();

            DataSet ds = new DataSet();

            try

            {

                CustomProfile profile = CustomProfile.GetProfile();

                ds = objService.GetEcomInstallmentdetails(OrderID, profile.DBConnection._constr);

                if (ds.Tables[0].Rows.Count > 0)

                {

                    grdinstallment.DataSource = ds;

                    grdinstallment.DataBind();

                }

                else

                {

                    ds = null;

                    grdinstallment.DataSource = ds;

                    grdinstallment.DataBind();

                }

            }

            catch (System.Exception ex) { Login.Profile.ErrorHandling(ex, "PartRequestEntry.aspx", "BindGVEcomInstallmentDetails"); }

            finally { objService.Close(); }

        }

        #endregion
    }

}
