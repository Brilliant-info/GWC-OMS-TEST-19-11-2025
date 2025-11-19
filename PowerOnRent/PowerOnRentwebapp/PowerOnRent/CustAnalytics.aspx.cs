using PowerOnRentwebapp.Login;
using PowerOnRentwebapp.PORServicePartRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Resources;
using System.Globalization;
using System.Threading;
using System.Reflection;

namespace PowerOnRentwebapp.PowerOnRent
{
    public partial class CustAnalytics : System.Web.UI.Page
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
                string id = Request.QueryString["Id"].ToString();
            iPartRequestClient objService = new iPartRequestClient();
            CustomProfile profile = CustomProfile.GetProfile();
            DataSet ds = new DataSet();
            ds = objService.GetCustomerAnalyticsData(id, profile.DBConnection._constr);
            GVCustAnalytics.DataSource = ds;
            GVCustAnalytics.DataBind();
        }
        private void loadstring()
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(Session["Lang"].ToString());
                rm = new ResourceManager("PowerOnRentwebapp.App_GlobalResources.Lang", Assembly.GetExecutingAssembly());
                ci = Thread.CurrentThread.CurrentCulture;
                btncancel.Value = rm.GetString("Close", ci);

                // UC_AttachmentDocument1.FormHeaderText = rm.GetString("CustomerList", ci);


            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "DocDownloadWindow", "loadstring");
            }
        }
    }
}