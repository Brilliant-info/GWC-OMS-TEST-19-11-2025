using PowerOnRentwebapp.Login;
using PowerOnRentwebapp.PORServicePartRequest;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebMsgBox;

namespace PowerOnRentwebapp.PowerOnRent
{
    public partial class VirtualSkuDocument : System.Web.UI.Page
    {
        ResourceManager rm;
        CultureInfo ci;
        string OrderID = "", Skucode = "";
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
                 OrderID= Request.QueryString["Sequence"].ToString();
                UC_AttachmentDocument1.FillDocumentByObjectNameReferenceID(long.Parse(OrderID), "RequestPartDetail", "RequestPartDetail");              
                hndSelectedRec.Value = OrderID.ToString();
                hndSelectedRecSku.Value = Request.QueryString["Skucode"];
                  Skucode = Request.QueryString["Skucode"];
                GetInvoiceNo(OrderID);
            }
            paneldoc.Visible = true;
            panelmsg.Visible = false;
        }

        private void loadstring()
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(Session["Lang"].ToString());
                rm = new ResourceManager("PowerOnRentwebapp.App_GlobalResources.Lang", Assembly.GetExecutingAssembly());
                ci = Thread.CurrentThread.CurrentCulture;

                btndocuplodcomplete.Text = rm.GetString("DocumentUploadComplete", ci);
                btnCompleted.Value= rm.GetString("Complete", ci);
                btncancel.Value = rm.GetString("Cancel", ci);

                // UC_AttachmentDocument1.FormHeaderText = rm.GetString("CustomerList", ci);


            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "Account Master", "loadstring");
            }
        }

        public void GetInvoiceNo(string OrderID)
        {
            string OrderNumber = "";
            CustomProfile profile = CustomProfile.GetProfile();
            iPartRequestClient objService = new iPartRequestClient();
            OrderNumber = objService.GetOrderNo(long.Parse(OrderID), profile.DBConnection._constr);
            lblmsg.Text = "Documentation for Order :- " + OrderNumber + " is signed by customer and Upload to OMS Portel ";
        }


        [WebMethod]
        public static string CheckPendingforAvtivation(string OrderID, string Skucode)
        {
             CustomProfile profile = CustomProfile.GetProfile();
            iPartRequestClient objService = new iPartRequestClient();
            string result = objService.ISCheckPendingforAvtivation(OrderID, Skucode, profile.DBConnection._constr);
            return result;
        }

        [WebMethod]
        public static string CompletedDocUpload(long RequestID, string Skucode)
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
                Login.Profile.ErrorHandling(ex, "VirtualSkuDocument.aspx", "CompletedDocUpload");
            }
            finally
            { objService.Close(); }
            return result;
        }

        protected void btndocuplodcomplete_Click(object sender, EventArgs e)
        {
            CustomProfile profile = CustomProfile.GetProfile();
            iPartRequestClient objService = new iPartRequestClient();
            string result = objService.ISCheckPendingforAvtivation(hndSelectedRec.Value, hndSelectedRecSku.Value, profile.DBConnection._constr);
            if (result == "No")
            {
                MsgBox.Show("Documentation Upload Already Completed...");
            }
            else
            {
                paneldoc.Visible = false;
                panelmsg.Visible = true;
            }
        }
    }
}