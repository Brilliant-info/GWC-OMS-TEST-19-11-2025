using PowerOnRentwebapp.Login;
using PowerOnRentwebapp.PORServicePartRequest;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PowerOnRentwebapp.PowerOnRent
{
    public partial class DocDownloadWindow : System.Web.UI.Page
    {
        ResourceManager rm;
        CultureInfo ci;
        string OrderNumber = "", Skucode = "";
        protected void Page_Load(object sender, EventArgs e)
        {           
            if (!IsPostBack)
            {
                if (Session["Lang"] == "")
                {
                    Session["Lang"] = Request.UserLanguages[0];
                }
                if (string.IsNullOrEmpty((string)Session["Lang"]))
                {
                    Session["Lang"] = Request.UserLanguages[0];
                }
                loadstring();

                UC_AttachmentDocument1.ClearDocument("RequestPartDetail");
                CustomProfile profile = CustomProfile.GetProfile();
                // string OrderID = HttpContext.Current.Session["PORRequestID"].ToString();  
                string OrderID = Request.QueryString["Sequence"].ToString();
                UC_AttachmentDocument1.FillDocumentByObjectNameReferenceID(long.Parse(OrderID), "RequestPartDetail", "RequestPartDetail");
                hndSelectedRec.Value = OrderID.ToString();
                hndSelectedRecSku.Value = Request.QueryString["Skucode"];
                //CheckPendingforAvtivation(OrderID, hndSelectedRecSku.Value);
                GetInvoiceNo(OrderID);
            }
        }


        private void loadstring()
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(Session["Lang"].ToString());
                rm = new ResourceManager("PowerOnRentwebapp.App_GlobalResources.Lang", Assembly.GetExecutingAssembly());
                ci = Thread.CurrentThread.CurrentCulture;

                btncloseorder.Text = rm.GetString("CloseOrder", ci);
                btnCompleted.Value = rm.GetString("Complete", ci);
                btncancel.Value = rm.GetString("Cancel", ci);

                // UC_AttachmentDocument1.FormHeaderText = rm.GetString("CustomerList", ci);


            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "DocDownloadWindow", "loadstring");
            }
        }

        public void GetInvoiceNo(string OrderID)
        {
            CustomProfile profile = CustomProfile.GetProfile();
            iPartRequestClient objService = new iPartRequestClient();
            OrderNumber = objService.GetOrderNo(long.Parse(OrderID), profile.DBConnection._constr);
            lblmsg.Text = "Completion for  Delivery and Dispatch Order :- " + OrderNumber ;
        }

        [WebMethod]
        public static string CompletedOrder(long RequestID, string Skucode)
        {
            string result = "";
            iPartRequestClient objService = new iPartRequestClient();
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                result = objService.UpdateCompletedOrder(RequestID, Skucode, profile.DBConnection._constr);

            }
            catch (Exception ex)
            {
                Login.Profile.ErrorHandling(ex, "ActivationForm.aspx", "WMGetADDDispatchStatus");
            }
            finally
            { objService.Close(); }
            return result;
        }

        protected void btncloseorder_Click(object sender, EventArgs e)
        {
            CustomProfile profile = CustomProfile.GetProfile();
            iPartRequestClient objService = new iPartRequestClient();
            string result = objService.ISCompleteOrderOrNot(hndSelectedRec.Value.ToString(), hndSelectedRecSku.Value.ToString(), profile.DBConnection._constr);
            if(result=="No")
            {
                //alrady close
            }
            else
            {
                paneldocument.Visible = false;
                panelconfirmwindow.Visible = true;
               
            }
         
        }

        [WebMethod]
        public static string CompleteOrder(string OrderID, string Skucode)
        {
            CustomProfile profile = CustomProfile.GetProfile();
            iPartRequestClient objService = new iPartRequestClient();
            string result = objService.ISCompleteOrderOrNot(OrderID, Skucode, profile.DBConnection._constr);
            return result;
        }
        
    }
}