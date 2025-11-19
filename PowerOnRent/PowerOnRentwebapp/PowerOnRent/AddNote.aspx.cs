using System;
using System.Data;
using System.Collections.Generic;
using System.Web.Services;
using PowerOnRentwebapp.Login;

using System.Text;
//using RestSharp;
using Newtonsoft.Json;
using RestSharp;
using PowerOnRentwebapp.PORServicePartRequest;
using System.Net;
using RestSharp.Authenticators;

namespace PowerOnRentwebapp.PowerOnRent
{
    public partial class AddNote : System.Web.UI.Page
    {
        static string val = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                val = Request.QueryString["ID"].ToString();
            }
        }
        [WebMethod]
        public static string SaveReTrigger(object objReq)
        {
            string result = "";
            CustomProfile profile = CustomProfile.GetProfile();
            ApprovalLevelMasterService.iApprovalLevelMasterClient ApprovalClient = new ApprovalLevelMasterService.iApprovalLevelMasterClient();
            iPartRequestClient objService = new iPartRequestClient();
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary = (Dictionary<string, object>)objReq;
                string ServiceDescription = dictionary["note"].ToString();
                string Id = val;
                

                string json = string.Empty;

                DataSet dsh = new DataSet();
                dsh = ApprovalClient.getErpAPROrderhead(Convert.ToInt64(Id), profile.DBConnection._constr);
                if (dsh.Tables[0].Rows.Count > 0)
                {
                   
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
                    dsord = ApprovalClient.getErpAPROrderdetail(Convert.ToInt64(Id), profile.DBConnection._constr);
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
                    var clienttokan = new RestClient("hhttps://erp-oic-instance-vodafoneqatar-fr.integration.ocp.oraclecloud.com/ic/api/integration/v1/flows/rest/INT201_INPUT_OMS_DATA/1.0/oms/invoice/process");
                    clienttokan.Authenticator = new HttpBasicAuthenticator(username, password);
                    var reqtokan = new RestRequest(Method.POST);
                    reqtokan.AddParameter("application/json;", json, ParameterType.RequestBody);
                    //reqtokan.AddHeader("Authentication", "Basic" + svcCredentials);
                    //reqtokan.AddHeader("Username", username);
                    //reqtokan.AddHeader("Password", password);
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                    IRestResponse response = clienttokan.Execute(reqtokan);
                    string status = Convert.ToString(response.StatusCode);
                    if (status != "Accepted")
                    {
                        ApprovalClient.UpdateReTriggerDetails(long.Parse(Id), status, ServiceDescription, status, profile.DBConnection._constr);
                        result = "Failed";

                    }
                    else
                    {
                        ApprovalClient.UpdateStatus(long.Parse(Id), status, profile.DBConnection._constr); objService.InsetERPOrderNotificationLog(Convert.ToInt64(Id), profile.Personal.UserID, 0, profile.DBConnection._constr);
                        result = "Success";
                    }
                    
                }              
                
                
            }
            catch (System.Exception ex)
            {
                //Login.Profile.ErrorHandling(ex, "", "Approval Level Master", "BindApprovalGrid");
                result = "Fail";
            }
            finally
            {
                
            }
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