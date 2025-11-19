using System;
using System.Web;
using System.Web.Security;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Drawing;
using PowerOnRentwebapp.Login;
using PowerOnRentwebapp.PORServicePartRequest;

namespace PowerOnRentwebapp.Mobile
{
    /// <summary>
    /// Summary description for new_request_process
    /// </summary>
    public class new_request_process : IHttpHandler
    {
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        SqlDataReader dr;
       
        string strcon = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;

        public void ProcessRequest(HttpContext context)
        {
            SqlConnection conn = new SqlConnection(strcon);

            String userId = context.Request.Form["txtUserName"];

            long StoreId = Convert.ToInt64(context.Request.Form["txtDepartmentId"]);
           // String userPass = context.Request.Form["usrPass"];

            CustomProfile profile = CustomProfile.GetProfile(userId);

            long uId= profile.Personal.UserID;

            DateTime OrderDate = Convert.ToDateTime(context.Request.Form["txtReqDate"].ToString());
            string Title = context.Request.Form["txtTitle"];
            DateTime Deliverydate = Convert.ToDateTime(context.Request.Form["txtDisDate"].ToString());
            string OrderNumber = context.Request.Form["txtOrderNo"];
            string Remark = context.Request.Form["txtRemark"];
            long Contact1 =Convert.ToInt64(context.Request.Form["txtContact1"]);
            long Contact2 =Convert.ToInt64( context.Request.Form["txtContact2"]);
            long AddressId =Convert.ToInt64( context.Request.Form["txtAddress"]);

            string ConID2 = context.Request.Form["txtContact2"];
            string Priority = "High";

           
            long PreviousStatusID = 0;
            iPartRequestClient objService = new iPartRequestClient();
            tOrderHead RequestHead = new tOrderHead();

            RequestHead.CreatedBy = profile.Personal.UserID;            
            RequestHead.Creationdate = DateTime.Now;

            RequestHead.OrderNumber = OrderNumber;
            RequestHead.StoreId = StoreId;
            RequestHead.Priority = Priority;
            RequestHead.Status = 2;
            RequestHead.Title = Title.ToString();
            RequestHead.Orderdate = OrderDate;
            RequestHead.RequestBy = profile.Personal.UserID;
            RequestHead.Remark = Remark;
            RequestHead.Deliverydate = Deliverydate;
            RequestHead.ContactId1 = Contact1;
            RequestHead.ContactId2 = Contact2;
            RequestHead.Con2 = ConID2;
            RequestHead.AddressId = AddressId;


            long RequestID = objService.SetIntotOrderHead(RequestHead, profile.DBConnection._constr);

            string prdDetails = context.Request.Form["txtProductDetails"];
            string myQueryString = " select * from  SplitString('" + prdDetails + "','|')";
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = myQueryString;
            cmd.Connection = conn;
            cmd.Parameters.Clear();          
            da.SelectCommand = cmd;
            da.Fill(ds, "tbl1");
            dt = ds.Tables[0];
            int cntr = dt.Rows.Count;
            if (cntr > 0)
            {
                long MySequence = 1;
                for (int i = 0; i <= (cntr-1); i++)
                {
                    long PrdID=Convert.ToInt64(ds.Tables[0].Rows[i]["part"].ToString());
                    i=i+1;
                    long uomId = Convert.ToInt64(ds.Tables[0].Rows[i]["part"].ToString());
                    i = i + 1;
                    decimal Qty = Convert.ToDecimal(ds.Tables[0].Rows[i]["part"].ToString());

                    SqlCommand cmd1 = new SqlCommand();
                    SqlDataAdapter da1 = new SqlDataAdapter();
                    DataSet ds1 = new DataSet();
                    DataTable dt1 = new DataTable();
                    string dsprdDetails = "select ProductCode,Name , Description  from mProduct where ID=" + PrdID + "";
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = dsprdDetails;
                    cmd1.Connection = conn;
                    cmd1.Parameters.Clear();
                    da1.SelectCommand = cmd1;
                    da1.Fill(ds1, "tbl2");
                    dt1 = ds1.Tables[0];

                    string name=ds1.Tables[0].Rows[0]["Name"].ToString();
                    string descr=ds1.Tables[0].Rows[0]["Description"].ToString();
                    string code=ds1.Tables[0].Rows[0]["ProductCode"].ToString();

                    SqlCommand cmd3 = new SqlCommand();
                    SqlDataAdapter da3 = new SqlDataAdapter();
                    DataSet ds3 = new DataSet();
                    DataTable dt3 = new DataTable();

                    string insPrdDetails = "insert into torderdetail(OrderHeadId,SkuId,OrderQty,UOMID,Sequence,Prod_Name,Prod_Description,Prod_Code,RemaningQty) values(" + RequestID + "," + PrdID + "," + Qty + "," + uomId + "," + MySequence + ",'" + name + "','" + descr + "','" + code + "'," + Qty + ")";
                    cmd3.CommandType = CommandType.Text;
                    cmd3.CommandText = insPrdDetails;
                    cmd3.Connection = conn;
                    cmd3.Parameters.Clear();
                    da3.SelectCommand = cmd3;
                    da3.Fill(ds3, "tbl3");
                    //dt3 = ds3.Tables[0];
                                        
                    //after Save
                    MySequence = MySequence + 1;
                }
            }

            objService.SetApproverDataafterSave("RequestPartDetail", RequestID, uId.ToString(), 2, StoreId, 0, profile.DBConnection._constr);
            String xmlString = String.Empty;
           // String orderNo = String.Empty;

            context.Response.ContentType = "text/xml";
            xmlString = xmlString + "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            xmlString = xmlString + "<gwcInfo>";
            xmlString = xmlString + "<newRequestInfo>";
            if(RequestID>0){
                xmlString = xmlString + "<requestmsg>success</requestmsg>";
                xmlString = xmlString + "<orderno> Order ID : " + RequestID + "</orderno>";
             }
            else
            {
                xmlString = xmlString + "<requestmsg>failed</requestmsg>"; 
            }

            xmlString = xmlString + "</newRequestInfo>";
            xmlString = xmlString + "</gwcInfo>";

            context.Response.Write(xmlString);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}