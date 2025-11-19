using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PowerOnRentwebapp.Login;
using PowerOnRentwebapp.PORServicePartRequest;
using PowerOnRentwebapp.ToolbarService;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Resources;
using System.Globalization;
using System.Threading;
using System.Reflection;

namespace PowerOnRentwebapp.PowerOnRent
{
    public partial class GridRequestSummary : System.Web.UI.Page
    {
        ResourceManager rm;
        CultureInfo ci;
        string val = "", ordtype = "";
        string GWCOrdertype = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                val = ddlSelectOrderTypenew.SelectedValue.ToString();
                Session["ORDType"] = val.ToString();
                ordtype = Session["ORDType"].ToString();
            }

            if (Page.Request.QueryString["Ordertype"] != null)
            {
                if (Page.Request.QueryString["Ordertype"] != "")
                {
                    string getOrderType = Page.Request.QueryString["Ordertype"].ToString();
                    GWCOrdertype = getOrderType;
                    Session["AdSOrderType"] = GWCOrdertype.ToString();
                }
            }


            Disebledcontrol();

            if (Request.QueryString["FillBy"] != null)
            {
                ordtype = Session["ORDType"].ToString();
                //FillGVRequest(Request.QueryString["FillBy"].ToString());
                FillGVRequest(Request.QueryString["FillBy"].ToString(), Request.QueryString["Invoker"].ToString(), ordtype);
            }


            if (Session["Lang"] == "")
            {
                Session["Lang"] = Request.UserLanguages[0];
            }
            if (string.IsNullOrEmpty((string)Session["Lang"]))
            {
                Session["Lang"] = Request.UserLanguages[0];
            }
            loadstring();
        }

        private void loadstring()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(Session["Lang"].ToString());
            //rm = new ResourceManager("PowerOnRentwebapp.App_GlobalResources.Lang", Assembly.GetExecutingAssembly());
            rm = new ResourceManager("PowerOnRentwebapp.App_GlobalResources.Lang", Assembly.GetExecutingAssembly());
            ci = Thread.CurrentThread.CurrentCulture;
            lblselectord.Text = rm.GetString("SelectOrder", ci);

        }

        protected void Page_PreInit(Object sender, EventArgs e)
        {
            CustomProfile profile = CustomProfile.GetProfile();
            if (profile.Personal.Theme == null || profile.Personal.Theme == string.Empty) { Page.Theme = "Blue"; } else { Page.Theme = profile.Personal.Theme; }
        }

        protected void GVRequest_OnRebind(object sender, EventArgs e)
        {
            FillGVRequest(Request.QueryString["FillBy"].ToString(), Request.QueryString["Invoker"].ToString(), Session["ORDType"].ToString());
        }

        private void Disebledcontrol()
        {
            CustomProfile profile = CustomProfile.GetProfile();
            iPartRequestClient objServie = new iPartRequestClient();
            string CheckISEcommerceOrNot = objServie.ISeCommerceOrNot(Convert.ToString(profile.Personal.UserID), profile.DBConnection._constr);
            if (profile.Personal.UserType.ToString() == "Super Admin")
            {
                lblselectord.Visible = true;
                ddlSelectOrderTypenew.Visible = true;

                // btnadvancesearch.Visible = true;
                hdnvisibleornot.Value = "No";


            }
            else if (profile.Personal.UserType.ToString() == "Retail User" || profile.Personal.UserType.ToString() == "Retail User Admin" || profile.Personal.UserType.ToString() == "Super Admin")
            {
                lblselectord.Visible = false;
                ddlSelectOrderTypenew.Visible = false;

                // btnadvancesearch.Visible = true;
                hdnvisibleornot.Value = "Yes";
                ordtype = "Ecommerce";
                Session["ORDType"] = "Ecommerce";

            }
            else if (profile.Personal.UserType.ToString() == "Admin")
            {
                if (CheckISEcommerceOrNot == "Yes")
                {
                    lblselectord.Visible = true;
                    ddlSelectOrderTypenew.Visible = true;

                    // btnadvancesearch.Visible = true;
                    hdnvisibleornot.Value = "No";

                }
                else
                {
                    lblselectord.Visible = false;
                    ddlSelectOrderTypenew.Visible = false;

                    //  btnadvancesearch.Visible = false;
                }

            }
            else if (profile.Personal.UserType.ToString() == "Requestor")
            {
                if (CheckISEcommerceOrNot == "Yes")
                {
                    lblselectord.Visible = true;
                    ddlSelectOrderTypenew.Visible = true;

                    //  btnadvancesearch.Visible = true;
                    hdnvisibleornot.Value = "No";


                }
                else
                {
                    lblselectord.Visible = false;
                    ddlSelectOrderTypenew.Visible = false;

                    //  btnadvancesearch.Visible = false;

                }

            }
            else if (profile.Personal.UserType.ToString() == "Approver" || profile.Personal.UserType.ToString() == "Requestor And Approver" || profile.Personal.UserType.ToString() == "Account")
            {

                if (CheckISEcommerceOrNot == "Yes")
                {
                    lblselectord.Visible = true;
                    ddlSelectOrderTypenew.Visible = true;


                    // btnadvancesearch.Visible = true;
                    hdnvisibleornot.Value = "No";
                }
                else
                {
                    lblselectord.Visible = false;
                    ddlSelectOrderTypenew.Visible = false;

                    // btnadvancesearch.Visible = false;
                    hdnvisibleornot.Value = "Yes";

                }
            }
            else if (profile.Personal.UserType.ToString() == "Fulfilment" || profile.Personal.UserType.ToString() == "Logistic" || profile.Personal.UserType.ToString() == "GWC User")
            {
                lblselectord.Visible = false;
                ddlSelectOrderTypenew.Visible = false;

                // btnadvancesearch.Visible = true;
                hdnvisibleornot.Value = "Yes";
                ordtype = "Ecommerce";
                Session["ORDType"] = "Ecommerce";

            }
        }

        protected void FillGVRequest(string FillBy, string Invoker, string ordtype)



        {
            iPartRequestClient objServie = new iPartRequestClient();
            try
            {
                DataSet ds = new DataSet();
                CustomProfile profile = CustomProfile.GetProfile();
                GVRequest.DataSource = null;
                GVRequest.DataBind();
                string hdnpendingnormal = "";
                hdnpendingnormal = hdnpendingordertype.Value;
                if (hdnpendingnormal == "Normal")
                {
                    FillBy = "UserID";
                    hdnpendingordertype.Value = "";
                }


                if (FillBy == "AdvanceSearch")
                {
                    if (ordtype == "")
                    {
                        string sUrl = HttpContext.Current.Request.Url.AbsoluteUri;
                        string oldval = Request.QueryString["FillBy"];
                        sUrl = sUrl.Replace("FillBy=" + oldval, "FillBy=" + "UserID");
                        Response.Redirect(sUrl);

                        FillBy = "UserID";
                        ordtype = "Normal";
                        ddlSelectOrderTypenew.SelectedValue = "Normal";
                    }
                }
                //New Added By Suresh for GWC
                string UserType = profile.Personal.UserType.ToString();
                //New Added By Suresh for GWC
                if (FillBy == "UserID")
                {
                    if (Invoker == "Request")
                    {
                        if (UserType == "User")
                        {
                            GVRequest.DataSource = objServie.GetRequestSummayByUserIDNew(profile.Personal.UserID, profile.DBConnection._constr, ordtype);
                        }
                        else if (UserType == "Requestor")
                        {
                            GVRequest.DataSource = objServie.GetRequestSummayByUserIDNew(profile.Personal.UserID, profile.DBConnection._constr, ordtype);
                        }
                        else if (UserType == "Super Admin")
                        {
                            if (GWCOrdertype != "")
                            {
                                string getOrderType = GWCOrdertype;
                                ordtype = getOrderType;
                            }
                            GVRequest.DataSource = objServie.GetRequestSummayByUserIDNew(profile.Personal.UserID, profile.DBConnection._constr, ordtype);
                        }

                        else if (UserType == "Fulfilment" || UserType == "Retail User Admin" || UserType == "Logistic" || UserType == "GWC User" )
                        {
                            GVRequest.DataSource = objServie.GetRequestSummayByUserIDNew(profile.Personal.UserID, profile.DBConnection._constr, "Ecommerce");
                            ordtype = "Ecommerce";

                        }
                        else if (UserType == "Approver" || UserType == "Requestor And Approver" || UserType == "Admin") //|| UserType == "Account"
                        {
                            if (GWCOrdertype != "")
                            {
                                //string getOrderType = Page.Request.QueryString["Ordertype"].ToString();
                                string getOrderType = GWCOrdertype;
                                ordtype = getOrderType;
                            }
                            string CheckISEcommerceOrNot = objServie.ISeCommerceOrNot(Convert.ToString(profile.Personal.UserID), profile.DBConnection._constr);
                            if (CheckISEcommerceOrNot == "Yes")
                            {
                                GVRequest.DataSource = objServie.GetRequestSummayByUserIDNew(profile.Personal.UserID, profile.DBConnection._constr, ordtype);
                            }
                            else
                            {
                                GVRequest.DataSource = objServie.GetRequestSummayByUserIDNew(profile.Personal.UserID, profile.DBConnection._constr, "Normal");
                            }
                        }
                        else if (UserType == "Retail User")
                        {
                            GVRequest.DataSource = objServie.GetRequestSummayByUserIDNew(profile.Personal.UserID, profile.DBConnection._constr, "Ecommerce");
                            ordtype = "Ecommerce";
                        }
                        else if(UserType == "Account")
                        {
                            if (GWCOrdertype != "")
                            {
                                string getOrderType = GWCOrdertype;
                                ordtype = getOrderType;
                            }
                            string CheckISEcommerceOrNot = objServie.ISeCommerceOrNot(Convert.ToString(profile.Personal.UserID), profile.DBConnection._constr);
                            if (CheckISEcommerceOrNot == "Yes")
                            {
                                GVRequest.DataSource = objServie.GetAccountPendingfinActivity(profile.Personal.UserID, profile.DBConnection._constr, ordtype);
                            }
                            else
                            {
                                GVRequest.DataSource = objServie.GetAccountPendingfinActivity(profile.Personal.UserID, profile.DBConnection._constr, "Normal");
                            }
                        }


                        GVRequest.AllowMultiRecordSelection = true; GVRequest.AllowRecordSelection = true;
                    }
                    else if (Invoker == "Issue")
                    {
                        if (UserType == "User")
                        {
                            GVRequest.DataSource = objServie.GetRequestSummayOfUserIssue(profile.Personal.UserID, profile.DBConnection._constr, ordtype);
                        }
                        else if (UserType == "Requester")
                        {
                            GVRequest.DataSource = objServie.GetRequestSummayOfUserIssue(profile.Personal.UserID, profile.DBConnection._constr, ordtype);
                        }
                        else if (UserType == "Retail User")
                        {
                            GVRequest.DataSource = objServie.GetRequestSummayByUserIDIssue(profile.Personal.UserID, profile.DBConnection._constr, "Ecommerce");
                            ordtype = "Ecommerce";
                        }
                        else if (UserType == "Fulfilment" || UserType == "Retail User Admin" || UserType == "Requestor And Approver" || UserType == "Logistic" || UserType == "GWC User")
                        {
                            GVRequest.DataSource = objServie.GetRequestSummayByUserIDIssue(profile.Personal.UserID, profile.DBConnection._constr, "Ecommerce");
                            ordtype = "Ecommerce";
                        }
                        else
                        {
                            GVRequest.DataSource = objServie.GetRequestSummayByUserIDIssue(profile.Personal.UserID, profile.DBConnection._constr, ordtype);
                        }
                        GVRequest.Columns[12].Visible = true; GVRequest.AllowMultiRecordSelection = true; GVRequest.AllowRecordSelection = true;
                    }
                }
                else if (FillBy == "SiteIDs")
                {
                    // GVRequest.DataSource = objServie.GetRequestSummayBySiteIDs(Session["SiteIDs"].ToString(), profile.DBConnection._constr);
                    GVRequest.DataSource = objServie.GetRequestSummayBySiteIDs(Session["SiteIDs"].ToString(), profile.DBConnection._constr, ordtype);
                }
                // add by suraj advance search 
                else if (FillBy == "AdvanceSearch")
                {
                    //advance search data as per user choice
                    string fdate = "", todate = "", ordcat = "", ordno = "", lcode = "", passport = "", ordertype = "", misidn = "", email = "", paymenttype = "", semserial = "";
                    fdate = HttpContext.Current.Session["ASFdate"].ToString();
                    todate = HttpContext.Current.Session["ASTdate"].ToString();
                    ordcat = HttpContext.Current.Session["ASOcategory"].ToString();
                    ordno = HttpContext.Current.Session["ASOrdNo"].ToString();
                    lcode = HttpContext.Current.Session["ASLocation"].ToString();
                    passport = HttpContext.Current.Session["ASPassport"].ToString();
                    ordertype = HttpContext.Current.Session["ASOrdtype"].ToString();
                    misidn = HttpContext.Current.Session["ASMisidn"].ToString();
                    email = HttpContext.Current.Session["ASEmail"].ToString();
                    paymenttype = HttpContext.Current.Session["ASPaymenttype"].ToString();
                    semserial = HttpContext.Current.Session["ASSemserial"].ToString();
                    // fdate = Session["AdvanFdate"].ToString();
                    //  todate = Session["AdvanTdate"].ToString();
                    if (Invoker == "Request")
                    {
                        GVRequest.DataSource = objServie.GetRequestSearch(fdate, todate, ordcat, ordno, lcode, passport, ordertype, misidn, email, profile.Personal.UserID, semserial, paymenttype, profile.DBConnection._constr);
                    }
                    else if (Invoker == "Issue")
                    {
                        if (UserType == "Retail User")
                        {
                            GVRequest.DataSource = objServie.GetRequestSummayOfRetailUserSearch(fdate, todate, ordcat, ordno, lcode, passport, ordertype, misidn, email, profile.Personal.UserID, semserial, paymenttype, profile.DBConnection._constr);

                        }
                        else if (UserType == "Fulfilment" || UserType == "Retail User Admin" || UserType == "Requester" || UserType == "Requestor And Approver" || UserType == "GWC User")
                        {
                            GVRequest.DataSource = objServie.GetRequestSummayOfRetailUserSearch(fdate, todate, ordcat, ordno, lcode, passport, ordertype, misidn, email, profile.Personal.UserID, semserial, paymenttype, profile.DBConnection._constr);

                        }
                        else
                        {
                            GVRequest.DataSource = objServie.GetRequestSummayOfRetailUserSearch(fdate, todate, ordcat, ordno, lcode, passport, ordertype, misidn, email, profile.Personal.UserID, semserial, paymenttype, profile.DBConnection._constr);
                        }
                    }
                    GVRequest.Columns[12].Visible = true; GVRequest.AllowMultiRecordSelection = true; GVRequest.AllowRecordSelection = true;

                    GVRequest.DataBind();
                    ordtype = "Ecommerce";
                    HttpContext.Current.Session["ASFdate"] = null; HttpContext.Current.Session["ASTdate"] = null; HttpContext.Current.Session["ASOcategory"] = null;
                    HttpContext.Current.Session["ASOrdNo"] = null; HttpContext.Current.Session["ASLocation"] = null; HttpContext.Current.Session["ASPassport"] = null;
                    HttpContext.Current.Session["ASOrdtype"] = null; HttpContext.Current.Session["ASMisidn"] = null; HttpContext.Current.Session["ASEmail"] = null;
                    ddlSelectOrderTypenew.SelectedValue = "Ecommerce";
                    Session["ORDType"] = "Ecommerce";
                    FillBy = "UserID";
                }

                // Show Approval Penidng Order list
                else if (FillBy == "Pendingrequest")
                {

                    // string OrderType = Page.Request.QueryString["Ordertype"].ToString();
                    string OrderType = GWCOrdertype;
                    if (OrderType == null || OrderType == "")
                    {
                        OrderType = "Normal";
                    }
                   
                    if (UserType == "Approver" || UserType == "Requestor And Approver" || UserType == "Super Admin" || UserType == "Admin" || UserType == "Logistic")
                    {
                        string CheckISEcommerceOrNot = objServie.ISeCommerceOrNot(Convert.ToString(profile.Personal.UserID), profile.DBConnection._constr);
                        if (CheckISEcommerceOrNot == "Yes")
                        {
                            GVRequest.DataSource = objServie.GetPendingApprovalList(profile.Personal.UserID, profile.DBConnection._constr, OrderType);

                        }
                        else
                        {
                            GVRequest.DataSource = objServie.GetPendingApprovalList(profile.Personal.UserID, profile.DBConnection._constr, OrderType);
                        }
                        ordtype = OrderType;
                    }
                    if (UserType == "Fulfilment")
                    {                       
                        GVRequest.DataSource = objServie.GetPendingApprovalList(profile.Personal.UserID, profile.DBConnection._constr, "Ecommerce");
                        ordtype = "Ecommerce";
                    }


                }

                if (ordtype == "Normal")
                {
                    GVRequest.Columns[2].Visible = false; GVRequest.Columns[7].Visible = false; GVRequest.Columns[8].Visible = false; GVRequest.Columns[9].Visible = false;
                    GVRequest.Columns[12].Visible = false; GVRequest.Columns[13].Visible = false; GVRequest.Columns[14].Visible = false; GVRequest.Columns[15].Visible = false;
                    GVRequest.Columns[16].Visible = false; GVRequest.Columns[10].Visible = true; GVRequest.Columns[11].Visible = true;GVRequest.Columns[17].Visible = false;
                    // GVRequest.ScrollingSettings.ScrollWidth = 1300;
                }
                if (ordtype == "Ecommerce")
                {
                    GVRequest.Columns[2].Visible = true; GVRequest.Columns[7].Visible = true; GVRequest.Columns[8].Visible = true; GVRequest.Columns[9].Visible = true;
                    GVRequest.Columns[12].Visible = true; GVRequest.Columns[13].Visible = true; GVRequest.Columns[14].Visible = true; GVRequest.Columns[15].Visible = true;
                    GVRequest.Columns[16].Visible = true; GVRequest.Columns[10].Visible = false; GVRequest.Columns[11].Visible = false; GVRequest.Columns[17].Visible = true;
                    //GVRequest.ScrollingSettings.ScrollWidth = 1400;
                }
                GVRequest.DataBind();
            }
            catch (Exception ex)
            {
                string sUrl = HttpContext.Current.Request.Url.AbsoluteUri;
                string oldval = Request.QueryString["FillBy"];
                sUrl = sUrl.Replace("FillBy=" + oldval, "FillBy=" + "UserID");
                Response.Redirect(sUrl);
                ddlSelectOrderTypenew.SelectedValue = "Normal";
                //throw;
            }
            finally { objServie.Close(); }
        }


        static void ClearSession()
        {
            HttpContext.Current.Session["PORRequestID"] = null;
            HttpContext.Current.Session["PORIssueID"] = null;
            HttpContext.Current.Session["PORReceiptID"] = null;
            HttpContext.Current.Session["PORConsumptionID"] = null;
            HttpContext.Current.Session["PORHQReceiptID"] = null;
            HttpContext.Current.Session["PORstate"] = null;
        }

        [WebMethod]
        public static string WMSetSessionRequest(string ObjectName, long RequestID, string state)
        {
            ClearSession();
            HttpContext.Current.Session["PORRequestID"] = RequestID;
            HttpContext.Current.Session["PORstate"] = state;
            iUCToolbarClient objService = new iUCToolbarClient();
            mUserRolesDetail checkRole = new mUserRolesDetail();
            CustomProfile profile = CustomProfile.GetProfile();
            switch (ObjectName)
            {
                case "Request":
                    checkRole = objService.GetUserRightsBy_ObjectNameUserID("MaterialRequest", profile.Personal.UserID, profile.DBConnection._constr);
                    break;
                case "Approval":
                    checkRole = objService.GetUserRightsBy_ObjectNameUserID("MaterialRequest", profile.Personal.UserID, profile.DBConnection._constr);
                    break;
                case "Issue":
                    checkRole = objService.GetUserRightsBy_ObjectNameUserID("MaterialIssue", profile.Personal.UserID, profile.DBConnection._constr);
                    break;
                case "Receipt":
                    checkRole = objService.GetUserRightsBy_ObjectNameUserID("MaterialReceipt", profile.Personal.UserID, profile.DBConnection._constr);
                    break;
                case "Consumption":
                    checkRole = objService.GetUserRightsBy_ObjectNameUserID("Consumption", profile.Personal.UserID, profile.DBConnection._constr);
                    break;
            }
            if (checkRole.Add == false && checkRole.View == false)
            {
                ObjectName = "AccessDenied";
            }
            else if (ObjectName == "Approval" && checkRole.Approval == false)
            {
                ObjectName = "AccessDenied";
            }
            return ObjectName;
        }

       /* protected void ddlSelectOrderTypenew_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                string val = "";
                val = ddlSelectOrderTypenew.SelectedValue.ToString();
                Session["ORDType"] = val.ToString();
                Session["AdSOrderType"] = val.ToString();

                if (Request.QueryString["FillBy"] != null)
                {
                    string FillBynew = Request.QueryString["FillBy"].ToString();
                    FillBynew = "UserID";
                    GWCOrdertype = val;
                    FillGVRequest(FillBynew, Request.QueryString["Invoker"].ToString(), val);
                    //FillGVRequest(Request.QueryString["FillBy"].ToString());
                    // FillGVRequest(Request.QueryString["FillBy"].ToString(), Request.QueryString["Invoker"].ToString(), val);
                }
            }
            
        }*/
    }
}