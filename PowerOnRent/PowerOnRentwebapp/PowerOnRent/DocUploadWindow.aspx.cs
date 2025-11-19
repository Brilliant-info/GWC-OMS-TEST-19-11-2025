using PowerOnRentwebapp.Login;
using PowerOnRentwebapp.PORServicePartRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PowerOnRentwebapp.PowerOnRent
{
    public partial class DocUploadWindow : System.Web.UI.Page
    {
        string OrderID = "", OrderNumber = "", Skucode="";
        protected void Page_Load(object sender, EventArgs e)
        {
            OrderID = Request.QueryString["OrdNo"];
            Skucode = Request.QueryString["Skucode"];
            hndSelectedRec.Value = OrderID.ToString();
            hndSelectedRecSku.Value = Skucode;
            GetInvoiceNo(OrderID);
        }

        public void GetInvoiceNo(string OrderID)
        {
            CustomProfile profile = CustomProfile.GetProfile();
            iPartRequestClient objService = new iPartRequestClient();
            OrderNumber = objService.GetOrderNo(long.Parse(OrderID), profile.DBConnection._constr);
            lblmsg.Text = "Documentation for Order :-" + OrderNumber + " is signed by customer and Upload to OMS Portel ";
        }


        [WebMethod]
        public static string CompletedDocUpload(long RequestID,string Skucode)
        {
            string result = "";
            iPartRequestClient objService = new iPartRequestClient();
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                result = objService.UpdateDocumentation(RequestID, Skucode, profile.DBConnection._constr);

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