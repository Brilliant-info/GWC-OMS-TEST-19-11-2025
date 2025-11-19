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
    public partial class UpdateMSISDN : System.Web.UI.Page
    {
        ResourceManager rm;
        CultureInfo ci;
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
            }

                hdnselectvalue.Value = Request.QueryString["Id"];
            hdnskucode.Value= Request.QueryString["Skucode"];
        }

        private void loadstring()
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(Session["Lang"].ToString());
                rm = new ResourceManager("PowerOnRentwebapp.App_GlobalResources.Lang", Assembly.GetExecutingAssembly());
                ci = Thread.CurrentThread.CurrentCulture;

                lblmsisdn.Text = rm.GetString("SIM", ci);

                btnMSISDN.Value = rm.GetString("Add", ci);

                // UC_AttachmentDocument1.FormHeaderText = rm.GetString("CustomerList", ci);


            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "UpdateMSISDN", "loadstring");
            }
        }


        [WebMethod]
        public static string WMUpdateMSISDN(long Id, string MSSIDN,string skucode)
        {
            string result = "";
            iPartRequestClient objService = new iPartRequestClient();
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                result = objService.UpdateMSISDN(Id, MSSIDN, skucode,profile.DBConnection._constr);

            }
            catch (Exception ex)
            {
                Login.Profile.ErrorHandling(ex, "UpdateMSISDN.aspx", "UpdateMSISDN");
            }
            finally
            { objService.Close(); }
            return result;
        }

    }
}