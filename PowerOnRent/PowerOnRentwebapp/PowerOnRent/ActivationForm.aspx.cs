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

namespace PowerOnRentwebapp.PowerOnRent
{
    public partial class Dispatch1 : System.Web.UI.Page
    {
        ResourceManager rm;
        CultureInfo ci;
        static string ObjectName = "RequestPartDetail";
        string OrderID = "", Skucode="";
        protected void Page_Load(object sender, EventArgs e)
        {
            OrderID = Request.QueryString["OrdNo"];
            Skucode = Request.QueryString["Skucode"];
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
        }


        private void loadstring()
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(Session["Lang"].ToString());
                rm = new ResourceManager("PowerOnRentwebapp.App_GlobalResources.Lang", Assembly.GetExecutingAssembly());
                ci = Thread.CurrentThread.CurrentCulture;

               
                btnactivedispatch.Value = rm.GetString("Activate", ci);
                btncanceldispatch.Value = rm.GetString("Cancel", ci);

                // UC_AttachmentDocument1.FormHeaderText = rm.GetString("CustomerList", ci);


            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "Account Master", "loadstring");
            }
        }
        public void GetInvoiceNo(string OrderID)
        {
            string orderdate = "", Orderno = ""; 
            CustomProfile profile = CustomProfile.GetProfile();
            iPartRequestClient objService = new iPartRequestClient();
            DataSet ds = new DataSet();
            ds = objService.GetOrderdate(long.Parse(OrderID), profile.DBConnection._constr);
            orderdate = ds.Tables[0].Rows[0]["Orderdate"].ToString();
            Orderno = ds.Tables[0].Rows[0]["OrderNo"].ToString();
            lblmsg.Text = "Activate the SIM for Order ID :- " + Orderno + " and  Order Date:- " + orderdate ;            
        }

        [WebMethod]
        public static string WMActivateSim(long RequestID, string SkuCode)
        {
            string result = "";
            iPartRequestClient objService = new iPartRequestClient();
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                result = objService.UpdatePendingforActivation(RequestID, SkuCode, profile.DBConnection._constr);
                //send email  for gwc user
               // objService.ActivtaedSendEmail(profile.Personal.UserID,RequestID, profile.DBConnection._constr);

            }
            catch (Exception ex)
            {
                Login.Profile.ErrorHandling(ex, "ActivationForm.aspx", "WMGetADDDispatchStatus");
            }
            finally
            { objService.Close(); }
            return result;
        }

    }
}