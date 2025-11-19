using PowerOnRentwebapp.Login;
using PowerOnRentwebapp.PORServicePartRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
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
    public partial class CancelOrderConfirm : System.Web.UI.Page
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
                hdnselectvalue.Value = Request.QueryString["Id"];
                BindReasonCode(hdnselectvalue.Value);
            }
        }


        private void loadstring()
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(Session["Lang"].ToString());
                rm = new ResourceManager("PowerOnRentwebapp.App_GlobalResources.Lang", Assembly.GetExecutingAssembly());
                ci = Thread.CurrentThread.CurrentCulture;

                lblreason.Text = rm.GetString("Reason", ci);
                btncancelorder.Value = rm.GetString("Complete", ci);
            
                // UC_AttachmentDocument1.FormHeaderText = rm.GetString("CustomerList", ci);


            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "CancelOrderConfirm", "loadstring");
            }
        }
        private void BindReasonCode(string ordno)
        {
            iPartRequestClient objServie = new iPartRequestClient();
            CustomProfile profile = CustomProfile.GetProfile();
            DataSet ds = new DataSet();
            ds = objServie.BindReasonCodeDDLForCancel(ordno,profile.DBConnection._constr);
            ddlreason.DataSource = ds;
            ddlreason.DataTextField = "ReasonCode";
            ddlreason.DataValueField = "Id";
            ddlreason.DataBind();

            ddlreason.Items.Insert(0, "--Select--");
        }


        [WebMethod]
        public static int WMCancelOrder(string SelectedOrder, int reasoncode)
        {
            int result = 0;
            iPartRequestClient objServie = new iPartRequestClient();
            CustomProfile profile = CustomProfile.GetProfile();
            string[] aa = SelectedOrder.Split(',').ToArray();
            int cnt = Convert.ToInt32(aa.Length);
            for (int i = 0; i < cnt; i++)
            {
                long UserID = profile.Personal.UserID;
                string orderid = aa[i].ToString();
                result = objServie.CancelSelectedOrder(Convert.ToInt64(orderid), reasoncode,UserID, profile.DBConnection._constr);              
            }
           // result = 1;
            return result;

        }
    }
}