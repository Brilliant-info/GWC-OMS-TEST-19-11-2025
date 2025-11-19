using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PowerOnRentwebapp.Login;
using PowerOnRentwebapp.UCProductSearchService;
using PowerOnRentwebapp.UserCreationService;
using PowerOnRentwebapp.ProductMasterService;
using PowerOnRentwebapp.AvailableQtyService;
using System.Web.Services;
using PowerOnRentwebapp.PORServicePartRequest;

namespace PowerOnRentwebapp.PowerOnRent
{
    public partial class ShowOrderSerial : System.Web.UI.Page
    {
        static string ObjectName = "RequestSerialNumber";
        long Oid = 0;
        long Sid = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["oid"] != null)
                {
                    Oid = long.Parse(Request.QueryString["oid"].ToString());
                }
                if (Request.QueryString["sid"] != null)
                {
                    Sid = long.Parse(Request.QueryString["sid"].ToString());
                }
                RebindGrid(sender, e);
                if (Session["Lang"].ToString() == "")
                {
                    Session["Lang"] = Request.UserLanguages[0];
                }
            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "ProductSearchWithSerial.aspx.cs", "Page_Load");
            }
        }

        protected void RebindGrid(object sender, EventArgs e)
        {
            try
            {
                iPartRequestClient objService = new iPartRequestClient();
                CustomProfile profile = CustomProfile.GetProfile();
                List<PowerOnRentwebapp.PORServicePartRequest.SP_GetPartSerialDetail_Result> RequestPartList1 = new List<PowerOnRentwebapp.PORServicePartRequest.SP_GetPartSerialDetail_Result>();
                RequestPartList1 = objService.GetPartSerialDetailByRequestID(Convert.ToInt64(Oid), Convert.ToInt64(Sid), Session.SessionID, profile.Personal.UserID.ToString(), ObjectName, profile.DBConnection._constr).ToList();
                GridProductSearch.DataSource = RequestPartList1;
                GridProductSearch.DataBind();
               
            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "ProductSearchWithSerial.aspx.cs", "RebindGrid");
            }




        }
    }
}