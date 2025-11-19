using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PowerOnRentwebapp.Login;
using PowerOnRentwebapp.PORServicePartRequest;
using System.Resources;
using System.Globalization;
using System.Threading;
using System.Reflection;
using System.Data;
using System.Web.Services;

namespace PowerOnRentwebapp.PowerOnRent
{
    public partial class AllocateDriver : System.Web.UI.Page
    {
        ResourceManager rm;
        CultureInfo ci;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                RebindGrid(sender, e);
            }
            catch (System.Exception ex) { Login.Profile.ErrorHandling(ex, this, "AllocateDriver.aspx.cs", "Page_Load"); }
            finally { }

            if (Session["Lang"] == "")
            {
                Session["Lang"] = Request.UserLanguages[0];
            }
            string Qvalue = "";
            Qvalue = Request.QueryString["Default"].ToString();
            Session["Driverval"] = Qvalue;


            loadstring();
        }
        protected void RebindGrid(object sender, EventArgs e)
        {
            iPartRequestClient assignDriver = new iPartRequestClient();
            CustomProfile profile = CustomProfile.GetProfile();
            var SearchedValue = hdnFilterText.Value;
            DataSet ds = new DataSet();
            if (SearchedValue == "")
            {
                ds = assignDriver.GetDriverDetails(profile.DBConnection._constr);
            }
            else
            {
                ds = assignDriver.GetFilteredDriverList(SearchedValue, profile.DBConnection._constr);
            }
            GVDriver.DataSource = ds;
            GVDriver.DataBind();
        }

        [System.Web.Services.WebMethod]
        public static string WMAssignDriver(long hndSelectedRec, string OrderID, string TruckDetails)
        {
            string str = "0", Allocate = "";
            try
            {
                iPartRequestClient assignDriver = new iPartRequestClient();
                CustomProfile profile = CustomProfile.GetProfile();
                DataSet ds = new DataSet();
                Allocate = HttpContext.Current.Session["Driverval"].ToString();
                int previouscnt = 0;
                string[] eorder = OrderID.Split(',');
                int cnt = eorder.Count();
                ds = assignDriver.CheckPreviousOrderCount(hndSelectedRec, profile.DBConnection._constr);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    previouscnt = Convert.ToInt32(ds.Tables[0].Rows[0]["cnt"]);
                }
                //int totcnt = 15 + 14;
                int totcnt = previouscnt + cnt;
                int assigncnt = 25 - previouscnt;
                if (totcnt > 25)
                {
                    str = "You assign only maximum 25 order per driver. currently you assignonly " + assigncnt + "  order.";
                    str = "5";
                }
                else
                {
                    for (int i = 0; i <= cnt - 1; i++)
                    {
                        assignDriver.AssignSelectedDriver(long.Parse(eorder[i].ToString()), hndSelectedRec, TruckDetails, profile.Personal.UserID, profile.DBConnection._constr);
                        str = "2";
                        //add by suraj for virtual products assign driver for documentation
                        String isCheckOrderIsEcomOrNot = assignDriver.ISEcommOrNot(long.Parse(eorder[i].ToString()), profile.DBConnection._constr);
                        if (isCheckOrderIsEcomOrNot == "Yes")
                        {
                            string temp = assignDriver.UpdateVirtualProduct(long.Parse(eorder[i].ToString()), profile.DBConnection._constr);
                            if (Allocate.ToUpper() == "YES")
                            {
                                str = "3";
                            }
                            else
                            {
                                str = "1";
                            }
                        }
                        //add by suraj for virtual products assign driver for documentation
                    }

                }


            }
            catch (System.Exception ex) { Login.Profile.ErrorHandling(ex, "AllocateDriver.aspx", "WMAssignDriver"); }
            return str;
        }

        private void loadstring()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(Session["Lang"].ToString());
            rm = new ResourceManager("PowerOnRentwebapp.App_GlobalResources.Lang", Assembly.GetExecutingAssembly());
            ci = Thread.CurrentThread.CurrentCulture;

            lblTruckDetail.Text = rm.GetString("TruckDetails", ci);
            lblheader.Text = rm.GetString("DriverList", ci);

        }
    }
}