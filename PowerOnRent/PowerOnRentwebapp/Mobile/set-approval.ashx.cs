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
    /// Summary description for set_approval
    /// </summary>
    public class set_approval : IHttpHandler
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

            long aprId =Convert.ToInt64(context.Request.Form["aprId"]);
            long reqId = Convert.ToInt64(context.Request.Form["reqId"]);
            String reqAct = context.Request.Form["reqAct"];
            long statusId = 0;
            if (reqAct == "Approved")
            { statusId = 3; }
            else if (reqAct == "Rejected") { statusId = 4; }

            string reqRemark = context.Request.Form["reqRemark"];
            string aprUserName = context.Request.Form["aprUserName"];

            CustomProfile profile = CustomProfile.GetProfile(aprUserName);
            

            String xmlString = String.Empty;
            context.Response.ContentType = "text/xml";
            xmlString = xmlString + "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            xmlString = xmlString + "<gwcInfo>";
            xmlString = xmlString + "<approvalAction>";

            iPartRequestClient objService = new iPartRequestClient();
            try
            {
                if (statusId == 3)
                {
                    objService.UpdatetApprovalTransAfterApproval(0, reqId, statusId, reqRemark, aprId, "0",profile.DBConnection._constr);
                }
                else if (statusId == 4)
                {
                    objService.UpdatetApprovalTransAfterReject(0, reqId, statusId, reqRemark, aprId, profile.DBConnection._constr);
                }
                xmlString = xmlString + "<authmsg>success</authmsg>";
            }
            catch { xmlString = xmlString + "<authmsg>failed</authmsg>"; }

            xmlString = xmlString + "</approvalAction>";
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