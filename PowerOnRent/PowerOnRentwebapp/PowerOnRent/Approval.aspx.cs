using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Resources;
using System.Globalization;
using System.Threading;
using System.Reflection;
using PowerOnRentwebapp.PORServicePartRequest;
using PowerOnRentwebapp.Login;
using System.Data;
using System.Data.SqlClient;
using PowerOnRentwebapp.RTSerialStock;
using PowerOnRentwebapp.AvailableQtyService;
using PowerOnRentwebapp.PORServiceUCCommonFilter;
using Newtonsoft.Json;
using RestSharp;
using System.Net;
using System.Text;
using System.Dynamic;
using System.Net.Http;
using RestSharp.Authenticators;

namespace PowerOnRentwebapp.PowerOnRent
{
    public partial class Approval : System.Web.UI.Page
    {
        ResourceManager rm;
        CultureInfo ci;

        static string ObjectName = "RequestPartDetail";
        static string ObjectNameSerial = "RequestSerialNumber";
        string OrderID = "", ST = "";
        CustomProfile profilenew = CustomProfile.GetProfile();
        protected void Page_Load(object sender, EventArgs e)
        {
            OrderID = Request.QueryString["REQID"];
            ST = Request.QueryString["ST"];
            if (ST == "4")
            {
                lblreason.Visible = true;
                ddlreason.Visible = true;
            }
            else
            {
                lblreason.Visible = false;
                ddlreason.Visible = false;
            }
            GetInvoiceNo(OrderID);

            if (Session["Lang"] == "")
            {
                Session["Lang"] = Request.UserLanguages[0];
            }
            if (string.IsNullOrEmpty((string)Session["Lang"]))
            {
                Session["Lang"] = Request.UserLanguages[0];
            }
            loadstring();
            if (!IsPostBack)
            {
                BindDDlReason(OrderID);
            }

        }

        private void BindDDlReason(string orderno)
        {
            CustomProfile profile = CustomProfile.GetProfile();
            iPartRequestClient objService = new iPartRequestClient();
            DataSet ds = new DataSet();
            ds = objService.BindReasonCodeDDL(orderno, profile.DBConnection._constr);
            ddlreason.DataSource = ds;
            ddlreason.DataTextField = "Reasondetails";
            ddlreason.DataValueField = "Id";
            ddlreason.DataBind();
            ListItem lstdeptnew = new ListItem { Text = "-Select-", Value = "0" };
            ddlreason.Items.Insert(0, lstdeptnew);
            ds.Dispose();
        }

        public void GetInvoiceNo(string OrderID)
        {
            CustomProfile profile = CustomProfile.GetProfile();
            iPartRequestClient objService = new iPartRequestClient();
            string InvNo = objService.GetInvoiceNoofOrder(long.Parse(OrderID), profile.DBConnection._constr);
            if (InvNo == "0") { txtInvoiceNo.Text = ""; } else { txtInvoiceNo.Text = InvNo; }
        }

        [WebMethod]
        public static string WMSaveApproval(long RequestID, string ApprovalStatus, long APL, string ApprovalRemark, string InvoiceNo, string Reasoncode)
        {
            //SerialRTimeStockServiceClient SerialwithStock = new SerialRTimeStockServiceClient();
            iPartRequestClient objService = new iPartRequestClient();
            string result = "";
            long Approvedflag = 0;
            int approvalLevel = Convert.ToInt32(APL);
            string Tag = "0";
            CustomProfile profile = CustomProfile.GetProfile();
            try
            {
                string OrdIsApproved = objService.ChkOrdIsApproved(RequestID, profile.DBConnection._constr);
                if (OrdIsApproved == "NotApproved")
                {
                    Approvedflag = objService.CheckForAllreadyApproved(RequestID, approvalLevel, profile.Personal.UserID, profile.DBConnection._constr);
                    if (Approvedflag != 1)
                    {
                        if (ApprovalStatus == "3")
                        {                         
                            DataSet Chkvft = new DataSet();
                            Chkvft = objService.CheckISvft(RequestID, profile.DBConnection._constr);
                            string ISVFT = "", IsApproved = "";
                            long MaxApproLevel = 0;
                            if (Chkvft.Tables[0].Rows.Count > 0)
                            {
                                ISVFT = Convert.ToString(Chkvft.Tables[0].Rows[0]["ISVFT"]);
                                MaxApproLevel = Convert.ToInt64(Chkvft.Tables[0].Rows[0]["MaxApprovalData"]);
                            }

                            if (ISVFT == "Yes" && MaxApproLevel == APL)
                            {
                                //Check Serialnumber is available or not
                                IsApproved=CheckSerialNumber(RequestID);                              

                            }
                            else
                            {
                                IsApproved = "Yes";
                            }



                            if (IsApproved == "Yes")
                            {
                                UpdatetApprovalTransAfterApproval(APL, RequestID, 3, ApprovalRemark, profile.Personal.UserID, InvoiceNo, Tag, profile.DBConnection._constr);
                                result = "true";
                                if (result == "true")
                                {
                                    updateApprovedflag(RequestID, approvalLevel, profile.Personal.UserID, profile.DBConnection._constr);
                                }
                            }
                            else
                            {
                                result = "showpopup";
                            }
                        }
                        else if (ApprovalStatus == "4")
                        {
                            UpdatetApprovalTransAfterReject(APL, RequestID, 4, ApprovalRemark, profile.Personal.UserID, Reasoncode, profile.DBConnection._constr);
                            result = "true";
                            if (result == "true")
                            {
                                updateApprovedflag(RequestID, approvalLevel, profile.Personal.UserID, profile.DBConnection._constr);
                            }
                        }
                        else if (ApprovalStatus == "24")
                        {
                            UpdatetApprovalTransAfterApproval(0, RequestID, 24, ApprovalRemark, profile.Personal.UserID, "0", Tag, profile.DBConnection._constr);

                            result = "truerev";
                        }
                    }
                    else
                    {
                        result = "Approved";
                    }
                }
                else
                {
                    result = "Approvedother";
                }
            }
            catch (Exception ex)
            {
                Login.Profile.ErrorHandling(ex, "Approval.aspx", RequestID + "WMSaveApproval");
                //interface message number 1 is nor sent then email notification sent 
                result = ChkandSendInterfacemsgnumber1(RequestID, profile.DBConnection._constr);
            }
            finally
            { objService.Close(); }
            return result;
        }

        public static string CheckSerialNumber(long Requestid)
        {
            SerialRTimeStockServiceClient SerialwithStock = new SerialRTimeStockServiceClient();
            iPartRequestClient objService = new iPartRequestClient();
            CustomProfile profile = CustomProfile.GetProfile();
            string result="";
            try
            {
                string check = "";
                DataSet dssku = new DataSet();
                dssku = objService.GetOnlySerialsku(Requestid, profile.DBConnection._constr);
                if (dssku.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dssku.Tables[0].Rows.Count; i++)
                    {
                        List<string> Impoerserial;
                        List<GetSerial> serialList = new List<GetSerial>();
                         //check =string.Empty;
                        string StoreCode =Convert.ToString(dssku.Tables[0].Rows[i]["storecode"].ToString());
                        string SKUCode = Convert.ToString(dssku.Tables[0].Rows[i]["productcode"].ToString());
                        string Schemaname = Convert.ToString(dssku.Tables[0].Rows[i]["SchemaNm"].ToString());
                        string DatabaseName = Convert.ToString(dssku.Tables[0].Rows[i]["DatabaseName"].ToString());
                        string ConnectionString = Convert.ToString(dssku.Tables[0].Rows[i]["ConnectionString"].ToString());
                        string Wmsstorecode = Convert.ToString(dssku.Tables[0].Rows[i]["wmsstorecode"].ToString());
                        string SerialNo = Convert.ToString(dssku.Tables[0].Rows[i]["SerialNumber"].ToString());
                        if (string.IsNullOrEmpty(Wmsstorecode))
                        {                        }
                        else
                        {
                            StoreCode = Wmsstorecode;
                        }

                        if (SerialNo != "NotApplicableSerial")
                        {
                            Impoerserial = SerialNo.Split(',').ToList();
                            if (profile.Personal.CompanyID == 10266)
                            {
                                 serialList = SerialwithStock.CheckAvailableQty("DRUM1", "VF5264PB", "", "", "wmwhse4");
                            }
                            else
                            {
                                serialList = SerialwithStock.CheckAvailableQty(SKUCode, StoreCode, DatabaseName, ConnectionString, Schemaname);
                            }
                            List<string> stringlistk = serialList.Where(x => x != null)
                            .Select(x => x.SERIAL.ToUpper())
                            .ToList();

                            List<string> NotavailSrlst = Impoerserial.Except(stringlistk, StringComparer.OrdinalIgnoreCase).ToList();
                            if (NotavailSrlst.Count > 0)
                            {
                                check = "No";
                                break;
                                
                                //string SrString = String.Join(",", NotavailSrlst);
                                //NotavailbleSR = NotavailbleSR + " | " + SKUCode + "-" + SrString;
                            }
                            else
                            {
                                check = "Yes";
                            }
                        }
                    }
                    result = check;
                }
            }
            catch (Exception ex)
            {
                Login.Profile.ErrorHandling(ex, "Approval.aspx", Requestid + "CheckSerialNumber");
            }
            return result;
        }

        public static void UpdatetApprovalTransAfterReject(long APL, long RequestID, int v, string ApprovalRemark, long userID, string Reasoncode, string[] constr)
        {
            string result = "";
            CustomProfile profile = CustomProfile.GetProfile();
            try
            {
                using (iPartRequestClient objService = new iPartRequestClient())
                {

                    objService.UpdatetApprovalTransAfterReject(APL, RequestID, 4, ApprovalRemark, profile.Personal.UserID, Reasoncode, profile.DBConnection._constr);
                    string check = objService.ChkOrderSerialedCompany(RequestID, profile.DBConnection._constr);
                    if (check == "Yes")
                    {
                        objService.DeleteResurveSerialnumber(HttpContext.Current.Session.SessionID, profile.Personal.UserID.ToString(), ObjectNameSerial, Convert.ToInt64(RequestID), profile.DBConnection._constr);
                        objService.Updateallowflag(Convert.ToInt64(RequestID), profile.DBConnection._constr);

                    }


                }
            }
            catch (Exception ex)
            {
                Login.Profile.ErrorHandling(ex, "Approval.aspx", RequestID + "UpdatetApprovalTransAfterReject");
                result = ChkandSendInterfacemsgnumber1(RequestID, profile.DBConnection._constr);
            }
        }

        public static void updateApprovedflag(long RequestID, int approvalLevel, long userID, string[] constr)
        {
            string result = "";
            CustomProfile profile = CustomProfile.GetProfile();
            try
            {
                using (iPartRequestClient objService = new iPartRequestClient())
                {

                    objService.updateApprovedflag(RequestID, approvalLevel, profile.Personal.UserID, profile.DBConnection._constr);
                }
            }
            catch (Exception ex)
            {
                Login.Profile.ErrorHandling(ex, "Approval.aspx", RequestID + "updateApprovedflag");
                result = ChkandSendInterfacemsgnumber1(RequestID, profile.DBConnection._constr);
            }
        }
        public static void UpdatetApprovalTransAfterApproval(long APL, long RequestID, int v, string ApprovalRemark, long userID, string invoiceNo, string tag, string[] constr)
        {
            string result = "";
            CustomProfile profile = CustomProfile.GetProfile();
            try
            {
                using (iPartRequestClient objService = new iPartRequestClient())
                {
                    //add for ismoney
                    string ismoneyflag = "";
                    DataSet ds = new DataSet();
                    // ds = objService.getismoneydept(Convert.ToInt64(RequestID).ToString(), profile.DBConnection._constr);
                    ds = objService.getismoneydept(RequestID, profile.DBConnection._constr);
                    if (ds.Tables[0].Rows.Count > 0)
                    {ismoneyflag = ds.Tables[0].Rows[0]["Result"].ToString();}
                    
                    ///logic for ERP AUTO apr
                    ///

                    DataSet dschkapr = new DataSet();
                    string autoapproval = "";
                    string usertype = "";
                    string ERPfinalcialapr = "";
                   
                    dschkapr = objService.CheckStoreERPAutoByReqID(RequestID, profile.Personal.UserID, profile.DBConnection._constr);
                    if (dschkapr.Tables[0].Rows.Count > 0)
                    {
                        autoapproval = dschkapr.Tables[0].Rows[0]["AutoApproval"].ToString();
                        usertype = dschkapr.Tables[0].Rows[0]["usertype"].ToString();                   

                    }
                    if (autoapproval == "True" && usertype == "Financial Approver")
                    {
                        ERPfinalcialapr = "True";
                    }
                    objService.UpdatetApprovalTransAfterApproval(APL, RequestID, 3, ApprovalRemark, profile.Personal.UserID, invoiceNo, tag, ismoneyflag, ERPfinalcialapr, profile.DBConnection._constr);
                    #region ERP Service
                    DataSet dsh = new DataSet();
                    dsh = objService.getErpOrderhead(RequestID, profile.DBConnection._constr);
                    long statusID = 0;
                    if (dsh.Tables[0].Rows.Count > 0)
                    {
                        statusID = Convert.ToInt64(dsh.Tables[0].Rows[0]["StatusID"].ToString());
                    }
                    if (autoapproval == "True" && statusID == 2 && usertype == "Financial Approver")
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



                        string username = "muhammadrafay.khan@evosysglobal.com";
                        string password = "Welcome@123456";

                        string svcCredentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(username + ":" + password));

                        var clienttokan = new RestClient("https://erp-oic-instance-vodafoneqatar-fr.integration.ocp.oraclecloud.com/ic/api/integration/v1/flows/rest/INT201_INPUT_OMS_DATA/1.0/oms/invoice/process");
                        clienttokan.Authenticator = new HttpBasicAuthenticator(username, password);
                        var reqtokan = new RestRequest(Method.POST);
                        reqtokan.AddParameter("application/json;", json, ParameterType.RequestBody);

                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                        IRestResponse response = clienttokan.Execute(reqtokan);
                        string status = Convert.ToString(response.StatusCode);

                        if (status != "Accepted")
                        {
                            objService.Savefailedaprorder(RequestID, status, status, profile.DBConnection._constr);
                        }
                        else
                        {
                            long storeID = 0;
                            objService.InsetERPOrderNotificationLog(RequestID, profile.Personal.UserID, storeID, profile.DBConnection._constr);
                        }
                    }

                    #endregion
                    string check = objService.ChkOrderSerialedCompany(RequestID, profile.DBConnection._constr);
                    if (check == "Yes")
                    {
                        objService.Updateallowflag(Convert.ToInt64(RequestID),profile.DBConnection._constr);
                    }
                   
                }
            }
            catch (Exception ex)
            {
                Login.Profile.ErrorHandling(ex, "Approval.aspx", RequestID + "UpdatetApprovalTransAfterApproval");
                result = ChkandSendInterfacemsgnumber1(RequestID, profile.DBConnection._constr);

            }


        }

        public static string ChkandSendInterfacemsgnumber1(long RequestID, string[] conn)
        {
            string result = "";
            iPartRequestClient objService = new iPartRequestClient();
            CustomProfile profile = CustomProfile.GetProfile();
            string ChkOrderIsApprovedorNot = "";
            ChkOrderIsApprovedorNot = objService.FunChkOrderIsApprovedorNotapproval(RequestID, profile.DBConnection._constr);
            if (ChkOrderIsApprovedorNot == "Yes")
            {
                string ChkmsgispresentOrNot = "";
                ChkmsgispresentOrNot = objService.FunChkmsgispresentOrNotforapprovalNew(RequestID, profile.DBConnection._constr);
                //if (ChkmsgispresentOrNot == "No")
                //{
                //    //objService.SendMessageInterfacenotification(RequestID, profile.DBConnection._constr);
                //   // objService.InterfacestaggingfailOrder(RequestID, profile.DBConnection._constr);
                //    string Chkstockadjust = "";
                //    Chkstockadjust = objService.CheckStockadjustmentisdoneapproval(RequestID, profile.DBConnection._constr);
                //    if (Chkstockadjust == "No")
                //    {
                //        objService.UpdateTProductStockReserveQtyTotalDispatchQtyapproval(RequestID, profile.DBConnection._constr);
                //    }
                //}
            }
            result = "error";
            return result;
        }

        private void loadstring()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(Session["Lang"].ToString());
            rm = new ResourceManager("PowerOnRentwebapp.App_GlobalResources.Lang", Assembly.GetExecutingAssembly());
            ci = Thread.CurrentThread.CurrentCulture;

            //lblapprovalop.Text = rm.GetString("OperationApproval", ci);
            //lblDate.Text = rm.GetString("Date", ci);
            lblApprovRemark.Text = rm.GetString("ApproverComments", ci);
            btnSaveApproval.Value = rm.GetString("Submit", ci);
            //lblApproved.Text = rm.GetString("Approve", ci);
            //lblRejected.Text = rm.GetString("Reject", ci);
            //lblRevison.Text = rm.GetString("Revision", ci);
        }


        [WebMethod]
        public static int WMUpdateEditFlag(long RequestID)
        {
            int result = 0;
            iPartRequestClient objService = new iPartRequestClient();
            CustomProfile profile = CustomProfile.GetProfile();
            try
            {
                objService.UpdateOrderEditFlag(RequestID, profile.DBConnection._constr);
                result = 1;
            }
            catch (Exception ex)
            {
                result = 0;
                Login.Profile.ErrorHandling(ex, "Approval.aspx", RequestID + "WMUpdateEditFlag");
            }
            finally
            {  }

            return result;
        }
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

    }
}