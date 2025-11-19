using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PowerOnRentwebapp.Login;
using System.Web.Services;
using PowerOnRentwebapp.ToolbarService;
using System.Configuration;
using System.Resources;
using System.Globalization;
using System.Threading;
using System.Reflection;
using PowerOnRentwebapp.PORServicePartRequest;
using PowerOnRentwebapp.UserCreationService;
using PowerOnRentwebapp.RoleMasterService;
using PowerOnRentwebapp.PORServiceUCCommonFilter;
using System.Data;
using System.Data.SqlClient;

namespace PowerOnRentwebapp.PowerOnRent
{
    public partial class Default : System.Web.UI.Page
    {
        ResourceManager rm;
        CultureInfo ci;
        protected void Page_PreInit(Object sender, EventArgs e)
        {
            CustomProfile profile = CustomProfile.GetProfile();
            if (profile.Personal.Theme == null || profile.Personal.Theme == string.Empty) { Page.Theme = "Blue"; } else { Page.Theme = profile.Personal.Theme; }           
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try {
                string val = "";
                IframeMethod();
                Toolbar1.SetSaveRight(false, "Not Allowed");
                Toolbar1.SetClearRight(false, "Not Allowed");
                if (Session["Lang"] == "")
                {
                    Session["Lang"] = Request.UserLanguages[0];
                }
                if (string.IsNullOrEmpty((string)Session["Lang"]))
                {
                    Session["Lang"] = Request.UserLanguages[0];
                }
                loadstring();
                CustomProfile profile = CustomProfile.GetProfile();
                iPartRequestClient objServie = new iPartRequestClient();
                string CheckISEcommerceOrNot1 = objServie.ISeCommerceOrNot(Convert.ToString(profile.Personal.UserID), profile.DBConnection._constr);
                if (CheckISEcommerceOrNot1 == "Yes")
                {
                    if (profile.Personal.UserType.ToString() == "Super Admin")
                    {
                        btnmarkasreturn.Visible = true;
                        btnchangestatus.Visible = true;
                    }
                    else if (profile.Personal.UserType.ToString() == "Retail User Admin" || profile.Personal.UserType.ToString() == "Fulfilment")
                    {
                        btnchangestatus.Visible = true;
                    }
                    else
                    {
                        btnchangestatus.Visible = false;
                    }
                }

                //if (!IsPostBack)
                //{
                //    if (hdndropdown.Value == "" || hdndropdown.Value == null)
                //    {
                //        val = ddlSelectOrderType.SelectedValue.ToString();
                //        Session["ORDType"] = val.ToString();
                //        hdndropdown.Value = val;
                //    }
                //    else
                //    {

                //    } 
                //////Button btnExport = (Button)Toolbar1.FindControl("btnExport");
                //////btnExport.Visible = false;
                //////Button btnImport = (Button)Toolbar1.FindControl("btnImport");
                //////btnImport.Visible = false;
                //////Button btmMail = (Button)Toolbar1.FindControl("btmMail");
                //////btmMail.Visible = false;
                //////Button btnPrint = (Button)Toolbar1.FindControl("btnPrint");
                //////btnPrint.Visible = false;
                //  }

                Disebledcontrol();
                hdnUserType.Value = Convert.ToString(profile.Personal.UserType);
                hdnadvancesearch.Value = Convert.ToString(Session["ORDType"]);
                if (hdnadvancesearch.Value == null || hdnadvancesearch.Value == "")
                {
                    //if (hdnUserType.Value == "Super Admin")
                    //{
                    //    hdnadvancesearch.Value = "ECommerce";
                    //}
                    hdnadvancesearch.Value = "Normal";
                }
                Session["AdSOrderType"] = "";

            }
            catch
            {
                Exception ex;
            }
        
            
        }

     

        private void IframeMethod()
        {
            if (Request.QueryString["invoker"].ToString() == "Request")
            {
                h4DivHead.InnerText = " Request";
                UCFormHeader1.FormHeaderText = "Request";
                //  iframePOR.Attributes.Add("src", "../PowerOnRent/GridRequestSummary.aspx?FillBy=UserID");
                iframePOR.Attributes.Add("src", "../PowerOnRent/GridRequestSummary.aspx?FillBy=UserID&Invoker=Request");
                Toolbar1.SetUserRights("MaterialRequest", "Summary", "");
                btnDriver.Visible = true;
                btnCancelOrder.Visible = true;
                // btnpending.Visible = true;
                btnpending.Attributes.Add("style", "display:inline-block;");
            }
            else if (Request.QueryString["invoker"].ToString() == "Approval")
            {
                h4DivHead.InnerText = " Approvals ";
                UCFormHeader1.FormHeaderText = "Approvals";
                iframePOR.Attributes.Add("src", "../PowerOnRent/GridRequestSummary.aspx?FillBy=UserID&Invoker=Request");
                Toolbar1.SetUserRights("MaterialRequest", "Summary", "");
                Toolbar1.SetAddNewRight(false, "Click on pending Approved record [Red box] to Add New / Edit Issue");
                btnDriver.Visible = false;
                btnCancelOrder.Visible = false;
            }
            else if (Request.QueryString["invoker"].ToString() == "Issue")
            {
                h4DivHead.InnerText = " Dispatch";
                UCFormHeader1.FormHeaderText = "Dispatch";
                //iframePOR.Attributes.Add("src", "../PowerOnRent/GridIssueSummary.aspx?FillBy=UserID");
                iframePOR.Attributes.Add("src", "../PowerOnRent/GridRequestSummary.aspx?FillBy=UserID&Invoker=Issue");
                Toolbar1.SetUserRights("MaterialIssue", "Summary", "");
                Toolbar1.SetAddNewRight(false, "Click on pending Issue record [Red box] to Add New / Edit Issue");
                btnDriver.Visible = false;
                btnCancelOrder.Visible = false;
            }
            else if (Request.QueryString["invoker"].ToString() == "Receipt")
            {
                h4DivHead.InnerText = "List of Material Receipts";
                UCFormHeader1.FormHeaderText = "Material Receipts";
                iframePOR.Attributes.Add("src", "../PowerOnRent/GridReceiptSummary.aspx?FillBy=UserID");
                Toolbar1.SetUserRights("MaterialReceipt", "Summary", "");
                Toolbar1.SetAddNewRight(false, "Click on pending Receipt record [Red box] to Add New / Edit Receipt");
            }
            else if (Request.QueryString["invoker"].ToString() == "Consumption")
            {
                h4DivHead.InnerText = "List of Consumption";
                UCFormHeader1.FormHeaderText = "Consumption";
                iframePOR.Attributes.Add("src", "../PowerOnRent/GridConsumptionSummary.aspx?FillBy=UserID");
                Toolbar1.SetUserRights("Consumption", "Summary", "");
            }
            else if (Request.QueryString["invoker"].ToString() == "HQReceipt")
            {
                h4DivHead.InnerText = "List of Goods Receipts [HQ]";
                UCFormHeader1.FormHeaderText = "Goods Receipts [HQ]";
                iframePOR.Attributes.Add("src", "../PowerOnRent/GridHQReceiptSummary.aspx?FillBy=UserID");
                Toolbar1.SetUserRights("GoodsReceipt", "Summary", "");
            }
        }

        //public int CheckRMSUser(string userid, string[] conn)
        //{
        //    CustomProfile profile = CustomProfile.GetProfile();
        //    int result = 0;
        //   // SqlConnection con = new SqlConnection(profile.DBConnection._constr);
        //    string[] con = profile.DBConnection._constr;
        //    using (SqlCommand cmd = new SqlCommand())
        //    {
        //        using (SqlDataAdapter da = new SqlDataAdapter())
        //        {
        //            using (DataSet ds = new DataSet())
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //                cmd.CommandText = "SP_ChkReturnUser";
        //                //cmd.Connection = svr.GetSqlConn(conn);
        //                cmd.Connection = con;
        //                cmd.Parameters.Clear();
        //                cmd.Parameters.AddWithValue("@userid", userid);
        //                da.SelectCommand = cmd;
        //                da.Fill(ds);
        //                if (ds.Tables[0].Rows.Count > 0)
        //                {
        //                    result = 1;
        //                }
        //                else
        //                {
        //                    result = 0;
        //                }
        //            }
        //        }
        //    }
        //    return result;

        //}

        private void Disebledcontrol()
        {
            CustomProfile profile = CustomProfile.GetProfile();
            iPartRequestClient objServie = new iPartRequestClient();
            iUserCreationClient usercreation = new iUserCreationClient();
            //iRoleMasterClient RoleMasterService = new iRoleMasterClient();
            int CheckRMSUser = 0;
            //  int CheckRMSUser = usercreation.CheckRMSUser(Convert.ToString(profile.Personal.UserID), profile.DBConnection._constr);
            //int CheckRMSUser = RoleMasterService.CheckRMSUser(Convert.ToString(profile.Personal.UserID), profile.DBConnection._constr);
            //  int CheckRMSUser = objServie.CheckRMSUser(Convert.ToString(profile.Personal.UserID), profile.DBConnection._constr);
            CheckRMSUser = usercreation.CheckRMSUser(profile.Personal.UserID, profile.DBConnection._constr);

            if (profile.Personal.UserType.ToString() == "Super Admin" || profile.Personal.UserType.ToString() == "Admin")
            {
                if (Request.QueryString["invoker"].ToString() == "Request")
                {
                    btnmarkasreturn.Visible = true;
                    btnDriver.Visible = true;
                    // btnpending.Visible = true;
                    btnpending.Attributes.Add("style", "display:inline-block;");
                    btnrefund.Visible = true;
                }
                else { btnDriver.Visible = false; }
            }
            else
            {
                btnDriver.Visible = false;
                //  btnpending.Visible = false;
                btnpending.Attributes.Add("style","display:none;");
            }
            string CheckISEcommerceOrNot = objServie.ISeCommerceOrNot(Convert.ToString(profile.Personal.UserID), profile.DBConnection._constr);
            if (profile.Personal.UserType.ToString() == "Super Admin")
            {
                //lblselectord.Visible = true;
               // ddlSelectOrderType.Visible = true;
                btnadvancesearch.Visible = true;
                hdnvisibleornot.Value = "No";
                // btnpending.Visible = true;
                btnpending.Attributes.Add("style", "display:inline-block;");
                btnrefund.Visible = true;
            }
            else if (profile.Personal.UserType.ToString() == "Retail User" || profile.Personal.UserType.ToString() == "Retail User Admin")
            {
                //lblselectord.Visible = false;
                //ddlSelectOrderType.Visible = false;
                btnadvancesearch.Visible = true;
                hdnvisibleornot.Value = "Yes";
                if (CheckRMSUser==1)
                {
                    btnmarkasreturn.Visible = true;
                }
            }
            else if (profile.Personal.UserType.ToString() == "Admin")
            {
                if (CheckISEcommerceOrNot == "Yes")
                {
                    //lblselectord.Visible = true;
                   // ddlSelectOrderType.Visible = true;
                    btnadvancesearch.Visible = true;
                    hdnvisibleornot.Value = "No";
                    // btnpending.Visible = true;
                    btnpending.Attributes.Add("style", "display:inline-block;");
                }
                else
                {
                   // lblselectord.Visible = false;
                   // ddlSelectOrderType.Visible = false;
                    btnadvancesearch.Visible = false;
                    // btnpending.Visible = true;
                    btnpending.Attributes.Add("style", "display:inline-block;");
                }
                if (CheckRMSUser == 1)
                {
                    btnmarkasreturn.Visible = true;
                }

            }
            else if (profile.Personal.UserType.ToString() == "Requestor")
            {
                if (CheckISEcommerceOrNot == "Yes")
                {
                   // lblselectord.Visible = true;
                   // ddlSelectOrderType.Visible = true;
                    btnadvancesearch.Visible = true;
                    hdnvisibleornot.Value = "No";

                }
                else
                {
                   // lblselectord.Visible = false;
                   // ddlSelectOrderType.Visible = false;
                    btnadvancesearch.Visible = false;
                }
                if (CheckRMSUser == 1)
                {
                    btnmarkasreturn.Visible = true;
                }

            }
            else if (profile.Personal.UserType.ToString() == "Approver" || profile.Personal.UserType.ToString() == "Requestor And Approver")
            {

                if (CheckISEcommerceOrNot == "Yes")
                {
                    //lblselectord.Visible = true;
                   // ddlSelectOrderType.Visible = true;
                    btnadvancesearch.Visible = true;
                    hdnvisibleornot.Value = "No";
                    // btnpending.Visible = true;
                    btnpending.Attributes.Add("style", "display:inline-block;");
                }
                else
                {
                   /// lblselectord.Visible = false;
                   // ddlSelectOrderType.Visible = false;
                    btnadvancesearch.Visible = false;
                    hdnvisibleornot.Value = "Yes";
                    // btnpending.Visible = true;
                    btnpending.Attributes.Add("style", "display:inline-block;");
                }
                if (CheckRMSUser == 1)
                {
                    btnmarkasreturn.Visible = true;
                }
            }
            else if (profile.Personal.UserType.ToString() == "Fulfilment" || profile.Personal.UserType.ToString() == "Logistic" || profile.Personal.UserType.ToString() == "GWC User")
            {
                //lblselectord.Visible = false;
               // ddlSelectOrderType.Visible = false;
                btnadvancesearch.Visible = true;
                hdnvisibleornot.Value = "Yes";
                //    btnpending.Visible = true;
                btnpending.Attributes.Add("style","display:inline-block;");
                if (CheckRMSUser == 1)
                {
                    btnmarkasreturn.Visible = true;
                }
            }
            else if (profile.Personal.UserType.ToString() == "Account")
            {
                btnrefund.Visible = true;
                btnCancelOrder.Visible = false;
                btnadvancesearch.Visible = false;
                //  btnpending.Visible = true;       
                btnpending.Attributes.Add("style", "display:none;");
                if (CheckRMSUser == 1)
                {
                    btnmarkasreturn.Visible = true;
                }
            }
        }

        [WebMethod]
        public static string WMSetSessionAddNew(string ObjectName, string state)
        {
            HttpContext.Current.Session["PORstate"] = state;
            switch (ObjectName)
            {
                case "Request":
                    HttpContext.Current.Session["PORRequestID"] = 0;
                    HttpContext.Current.Session["TemplateID"] = "0";
                    break;
                case "Issue":
                    HttpContext.Current.Session["PORIssueID"] = 0;
                    break;
                case "Receipt":
                    HttpContext.Current.Session["PORReceiptID"] = 0;
                    break;
                case "Consumption":
                    HttpContext.Current.Session["PORConsumptionID"] = 0;
                    break;
                case "HQReceipt":
                    HttpContext.Current.Session["PORHQReceiptID"] = 0;
                    break;
            }

            return ObjectName;
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

        [WebMethod]
        public static string WMSetSessionIssue(string ObjectName, long IssueID, string state)
        {
            ClearSession();
            HttpContext.Current.Session["PORIssueID"] = IssueID;
            HttpContext.Current.Session["PORstate"] = state;
            iUCToolbarClient objService = new iUCToolbarClient();
            mUserRolesDetail checkRole = new mUserRolesDetail();
            CustomProfile profile = CustomProfile.GetProfile();
            switch (ObjectName)
            {
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
            return ObjectName;
        }

        [WebMethod]
        public static string WMSetSessionReceipt(string ObjectName, long ReceiptID, string state)
        {
            ClearSession();
            HttpContext.Current.Session["PORReceiptID"] = ReceiptID;
            HttpContext.Current.Session["PORstate"] = state;
            iUCToolbarClient objService = new iUCToolbarClient();
            mUserRolesDetail checkRole = new mUserRolesDetail();
            CustomProfile profile = CustomProfile.GetProfile();
            switch (ObjectName)
            {
                case "Receipt":
                    checkRole = objService.GetUserRightsBy_ObjectNameUserID("MaterialReceipt", profile.Personal.UserID, profile.DBConnection._constr);
                    break;
                case "Consumption":
                    checkRole = objService.GetUserRightsBy_ObjectNameUserID("Consumption", profile.Personal.UserID, profile.DBConnection._constr);
                    HttpContext.Current.Session["PORConsumptionID"] = null;
                    break;
            }
            if (checkRole.Add == false && checkRole.View == false)
            {
                ObjectName = "AccessDenied";
            }
            return ObjectName;
        }

        [WebMethod]
        public static string WMSetSessionConsumption(string ObjectName, long ConsumptionID, string state)
        {
            ClearSession();
            HttpContext.Current.Session["PORConsumptionID"] = ConsumptionID;
            HttpContext.Current.Session["PORstate"] = state;
            iUCToolbarClient objService = new iUCToolbarClient();
            mUserRolesDetail checkRole = new mUserRolesDetail();
            CustomProfile profile = CustomProfile.GetProfile();
            switch (ObjectName)
            {
                case "Consumption":
                    checkRole = objService.GetUserRightsBy_ObjectNameUserID("Consumption", profile.Personal.UserID, profile.DBConnection._constr);
                    break;
            }
            if (checkRole.Add == false && checkRole.View == false)
            {
                ObjectName = "AccessDenied";
            }
            return ObjectName;
        }

        [WebMethod]
        public static string WMSetSessionHQReceipt(string ObjectName, long ReceiptID, string state)
        {
            ClearSession();
            HttpContext.Current.Session["PORHQReceiptID"] = ReceiptID;
            HttpContext.Current.Session["PORstate"] = state;
            iUCToolbarClient objService = new iUCToolbarClient();
            mUserRolesDetail checkRole = new mUserRolesDetail();
            CustomProfile profile = CustomProfile.GetProfile();
            switch (ObjectName)
            {
                case "HQReceipt":
                    checkRole = objService.GetUserRightsBy_ObjectNameUserID("GoodsReceipt", profile.Personal.UserID, profile.DBConnection._constr);
                    break;
            }
            if (checkRole.Add == false && checkRole.View == false)
            {
                ObjectName = "AccessDenied";
            }
            return ObjectName;
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
        public static int WMChkDispatchedOrder(string SelectedOrder)
        {
            iPartRequestClient objServie = new iPartRequestClient();
            CustomProfile profile = CustomProfile.GetProfile();
            int result = objServie.GetDispatchedOrders(SelectedOrder, profile.DBConnection._constr);
            return result;
        }

        [WebMethod]
        public static int WMChkChangeOrderStatus(string SelectedOrder)
        {
            iPartRequestClient objServie = new iPartRequestClient();
            CustomProfile profile = CustomProfile.GetProfile();
            int result = objServie.GetDispatchedstatusOrders(SelectedOrder, profile.DBConnection._constr);
            return result;
        }


        
        [WebMethod]
        public static int WMChkChangeOrderStatusfulfillment(string SelectedOrder)
        {
            string[] eorder = SelectedOrder.Split(',');
            int cnt = eorder.Count();
            iPartRequestClient objServie = new iPartRequestClient();
            CustomProfile profile = CustomProfile.GetProfile();
            int result = objServie.GetDispstatusOrdforfullfillment(SelectedOrder, profile.DBConnection._constr);
            if(cnt== result)
            {
                result = 0;
            }
            else
            {
                result = 2;
            }
            return result;
        }

  //    for project 2 super Admin change status
        [WebMethod]
         
        public static int WMChkChangeOrderStatusSuperAdmin(string SelectedOrder, string userType)
        //public static int WMChkChangeOrderStatusSuperAdmin(string SelectedOrder, string userType)
        {
            int result = 0;
            try
            {
                string[] eorder = SelectedOrder.Split(',');
                int cnt = eorder.Count();

                if (cnt <= 1)
                {
                    iPartRequestClient objServie = new iPartRequestClient();
                    CustomProfile profile = CustomProfile.GetProfile();
                    //int result = objServie.GestatusOrdforSuperAdmin(Convert.ToInt64(SelectedOrder), userType, profile.DBConnection._constr);
                    result = objServie.GestatusOrdforSuperAdmin1(Convert.ToInt64(SelectedOrder), userType, profile.DBConnection._constr);
                    // int result = 0;
                    if (result == 1)
                    {
                        result = 0;
                    }
                    else
                    {
                        result = 2;
                    }
                }
                else
                {
                    result = 3;
                }

            }
            catch
            {
                Exception ex;
            }
            return result;
        }
        // Start Added by suraj khopade for RMS
        [WebMethod]

        public static int WMChkChangeMarkAsReturnOrder(string SelectedOrder, string userType)
        {

            int result = 0;
            try
            {
                string[] eorder = SelectedOrder.Split(',');
                int cnt = eorder.Count();

                iUserCreationClient usercreation = new iUserCreationClient();
                CustomProfile profile = CustomProfile.GetProfile();
               
               // int result = 0;
                // result = usercreation.GetMarkAsReturnOrder(SelectedOrder, userType, profile.DBConnection._constr);

                //if (cnt == result)
                //{
                //    usercreation.insertReturnOrder(SelectedOrder, profile.DBConnection._constr);
                //    result = 0;
                //}
                //else
                //{
                //    result = 2;
                //}

                if (cnt <= 1)
                {
                    result = usercreation.GetMarkAsReturnOrder(SelectedOrder, userType, profile.DBConnection._constr);
                    if (result == 1)
                    {
                        usercreation.insertReturnOrder(SelectedOrder, Convert.ToInt64(profile.Personal.UserID), profile.DBConnection._constr);
                        result = 0;
                    }else if (result == 1001)
                    {
                        result = 1001;
                    }
                    else
                    {
                        result = 2;
                    }
                }
                else
                {
                    result = 3;
                }
            }
            catch
            {
                Exception ex;
            }
            return result;
        }

        // end Added by suraj khopade for RMS
        [WebMethod]
        public static int WMCancelOrder(string SelectedOrder)
        {
            int result = 0;
            iPartRequestClient objServie = new iPartRequestClient();
            CustomProfile profile = CustomProfile.GetProfile();
            string[] aa = SelectedOrder.Split(',').ToArray();
            int cnt = Convert.ToInt32(aa.Length);
            for (int i = 0; i < cnt; i++)
            {
                long UserID = profile.Personal.UserID;
                string orderid = aa[i].ToString();
                // result = objServie.CancelSelectedOrder(Convert.ToInt64(SelectedOrder), UserID, profile.DBConnection._constr);       
                //result = objServie.CancelSelectedOrder(Convert.ToInt64(orderid), UserID, profile.DBConnection._constr);                
            }
            return result;
        }

        //string[] MySplit(string input)
        //{
        //    List<string> results = new List<string>();
        //    int count = 0;
        //    string temp = "";

        //    foreach (char c in input)
        //    {
        //        temp += c;
        //        count++;
        //        if (count == 3)
        //        {
        //            result.Add(temp);
        //            temp = "";
        //            count = 0;
        //        }
        //    }

        //    if (temp != "")
        //        result.Add(temp);

        //    return result.ToArray();
        //}

        private void loadstring()
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(Session["Lang"].ToString());
                //rm = new ResourceManager("PowerOnRentwebapp.App_GlobalResources.Lang", Assembly.GetExecutingAssembly());
                rm = new ResourceManager("PowerOnRentwebapp.App_GlobalResources.Lang", Assembly.GetExecutingAssembly());
                ci = Thread.CurrentThread.CurrentCulture;

                lblHeading.Text = rm.GetString("NotApplicable", ci);
                lblCompleted.Text = rm.GetString("Completed", ci);
                if (Request.QueryString["invoker"].ToString() == "Request")
                {
                    UCFormHeader1.FormHeaderText = rm.GetString("Request", ci);
                    h4DivHead.InnerText = rm.GetString("Request", ci);
                    btnCancelOrder.Value = rm.GetString("CancelOrder", ci);

                }
                else if (Request.QueryString["invoker"].ToString() == "Approval")
                {
                    UCFormHeader1.FormHeaderText = rm.GetString("Approval", ci);
                    h4DivHead.InnerText = rm.GetString("Approval", ci);
                }
                else if (Request.QueryString["invoker"].ToString() == "Issue")
                {
                    UCFormHeader1.FormHeaderText = rm.GetString("Dispatch", ci);
                    h4DivHead.InnerText = rm.GetString("Dispatch", ci);
                    btnDriver.Value = rm.GetString("AllocateDriver", ci);
                }

                lblPending.Text = rm.GetString("Pending", ci);
                lblCancelled.Text = rm.GetString("Cancelled", ci);

               // lblselectord.Text = rm.GetString("SelectOrder", ci);
                btnadvancesearch.Value= rm.GetString("AdvanceSearch", ci);


            }
            catch (Exception ex)
            {
                Login.Profile.ErrorHandling(ex, "Default.aspx", "Loadstring");
            }
        }

        //protected void ddlSelectOrderType_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string val = "";
        //    val = ddlSelectOrderType.SelectedValue.ToString();
        //    Session["ORDType"] = val.ToString();
           
        //}


        [WebMethod]
        public static int AssignValueToSession(string searchval, string frmdate, string tdate, string ordcategory, string ordno, string location, string passport, string ordtype, string misidnno, string Email, string paymenttype, string simserial)
        {
            int result = 0;
            HttpContext.Current.Session["ASValue"] = searchval;
            HttpContext.Current.Session["ASFdate"] = frmdate;
            HttpContext.Current.Session["ASTdate"] = tdate;
            HttpContext.Current.Session["ASOcategory"] = ordcategory;
            HttpContext.Current.Session["ASOrdNo"] = ordno;
            HttpContext.Current.Session["ASLocation"] = location;
            HttpContext.Current.Session["ASPassport"] = passport;
            HttpContext.Current.Session["ASOrdtype"] = ordtype;
            HttpContext.Current.Session["ASMisidn"] = misidnno;
            HttpContext.Current.Session["ASEmail"] = Email;
            HttpContext.Current.Session["ASPaymenttype"] = paymenttype;
            HttpContext.Current.Session["ASSemserial"] = simserial;
            return 1;
        }
        [WebMethod]
        public static string getismoneydept(string OrderID)


        {
            CustomProfile profile = CustomProfile.GetProfile();
            iPartRequestClient objService = new iPartRequestClient();
            DataSet ds = new DataSet();
            string ismoneyflag = "";
            try
            {
                long oid = 0;
                oid = Convert.ToInt64(OrderID);
                ds = objService.getismoneydept(oid, profile.DBConnection._constr);
                //ds = objService.getismoneydept(Convert.ToInt64(OrderID).ToString(), profile.DBConnection._constr);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ismoneyflag = ds.Tables[0].Rows[0]["Result"].ToString();

                }
            }
            catch (Exception ex) { }
            finally
            {

            }
            return ismoneyflag;
        }

        #region  combine 2022 CR changes Project 3
        [WebMethod]
        public static int checkrefundorderstatus(long SelectedOrder, string UserType)
        {
            iPartRequestClient objServie = new iPartRequestClient();
            CustomProfile profile = CustomProfile.GetProfile();
            int result = objServie.checkstatusforRefundupdate(SelectedOrder, UserType, profile.DBConnection._constr);
            return result;
        }
        #endregion
    }
}
