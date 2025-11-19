using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Resources;
using System.Globalization;
using System.Collections;
using System.Threading;
using System.Reflection;
using PowerOnRentwebapp.ProductMasterService;
using System.Data;
using System.Web.Services;
using PowerOnRentwebapp.Login;

namespace PowerOnRentwebapp.Product
{
    public partial class VirtualQty : System.Web.UI.Page
    {
        ResourceManager rm;
        CultureInfo ci;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Lang"] == "")
            {
                Session["Lang"] = Request.UserLanguages[0];
            }
            loadstring();
            long skuid = long.Parse(Request.QueryString["skuid"].ToString());
            hdnskuid.Value = skuid.ToString();

        }

        [WebMethod]
        public static void SaveVirtualQty(object objReq)
        {
            iProductMasterClient productClient = new iProductMasterClient();
            CustomProfile profile = CustomProfile.GetProfile();
            DataTable dt = new DataTable();
            decimal availbalance = 0m, virtualQty = 0m, AvailVirtyalQty = 0m;
            long storeID = 0;
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary = (Dictionary<string, object>)objReq;
              
                decimal Quantity = decimal.Parse(dictionary["quantity"].ToString());
                decimal ReOrderQty = decimal.Parse(dictionary["ReOrderQty"].ToString()); 
                long hdnskuid = long.Parse(dictionary["hdnskuid"].ToString());
                DataSet ds = productClient.GetAvailQuantity(hdnskuid, profile.DBConnection._constr);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    storeID = long.Parse(dt.Rows[0]["SiteID"].ToString());
                    availbalance = decimal.Parse(dt.Rows[0]["AvailableBalance"].ToString());
                    virtualQty = decimal.Parse(dt.Rows[0]["VirtualQty"].ToString());
                    AvailVirtyalQty = decimal.Parse(dt.Rows[0]["AvailVirtualQty"].ToString());
                }
                if (virtualQty == 0)
                {
                    AvailVirtyalQty = availbalance + Quantity;
                    virtualQty = Quantity; 
                }
                else
                {
                    AvailVirtyalQty = availbalance + virtualQty + Quantity;
                    virtualQty = virtualQty + Quantity;
                }
                productClient.UpdateVirtualBalance(hdnskuid,virtualQty, AvailVirtyalQty, ReOrderQty, profile.DBConnection._constr);
                productClient.InsertIntoInventry(hdnskuid, storeID, DateTime.Now, Quantity, profile.DBConnection._constr);

            }
            catch (System.Exception ex)
            {
                productClient.Close();
            }
            finally
            {
                productClient.Close();
            }

        }

        private void loadstring()
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(Session["Lang"].ToString());
                rm = new ResourceManager("PowerOnRentwebapp.App_GlobalResources.Lang", Assembly.GetExecutingAssembly());
                ci = Thread.CurrentThread.CurrentCulture;

                lblVirtualqtyFormHeader.Text = rm.GetString("AddVirtualQuantity", ci);
                btnVirtualSubmit.Value = rm.GetString("Submit", ci);
                btnvirtualClear.Value = rm.GetString("Clear", ci);
                lblVirtualQuantity.Text = rm.GetString("EnterVirtualQuantity", ci);
                Button1.Value = rm.GetString("Submit", ci);
                Button2.Value = rm.GetString("Clear", ci);
                lblvreqty.Text = rm.GetString("AddVirtualQuantity", ci);
            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "VirtualQty", "loadstring");
            }
        }
    }
}