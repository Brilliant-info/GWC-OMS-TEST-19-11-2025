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
using PowerOnRentwebapp.Login;
using PowerOnRentwebapp.PORServicePartRequest;
using System.Data;
using System.Data.SqlClient;

namespace PowerOnRentwebapp.PowerOnRent
{
    public partial class ChangeOrderProduct : System.Web.UI.Page
    {
        static string ObjectName = "RequestPartDetail";
        static string ObjectNameSerial = "RequestSerialNumber";
        ResourceManager rm;
        CultureInfo ci;
        static long OrderID, UserID, SiteID;
        static int Sequence;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Lang"] == "")
            {
                Session["Lang"] = Request.UserLanguages[0];
            }
            loadstring();
            CustomProfile profile = CustomProfile.GetProfile();
            OrderID = long.Parse(Session["PORRequestID"].ToString());
            Sequence = int.Parse(Session["SEQ"].ToString());
            UserID = profile.Personal.UserID;
            SiteID = Convert.ToInt64(HttpContext.Current.Session["PORSitetID"]);

            BindProductDetails(OrderID, Sequence, UserID);
        }

        public void BindProductDetails(long OrderID, int Sequence, long UserID)
        {
            CustomProfile profile = CustomProfile.GetProfile();
            iPartRequestClient objService = new iPartRequestClient();
            int IsPriceEdit = objService.IsPriceEditYN(OrderID, UserID, profile.DBConnection._constr);
            int IsSkuChange = objService.IsSkuChangeYN(OrderID, UserID, profile.DBConnection._constr);
            if (IsPriceEdit == 1) { txtPrc.Disabled = false; } else { txtPrc.Disabled = true; }
            if (IsSkuChange == 1) { txtReqQty.Disabled = false; } else { txtReqQty.Disabled = true; }
            hdnSelectedOrdId.Value =Convert.ToString(OrderID.ToString());
            DataSet ds = new DataSet();
            ds = objService.GetProductOfOrder(OrderID, Sequence, profile.DBConnection._constr);
            txtProductCode.Text = ds.Tables[0].Rows[0]["Prod_Code"].ToString();
            txtProductName.Text = ds.Tables[0].Rows[0]["Prod_Name"].ToString();
            txtProductDescription.Text = ds.Tables[0].Rows[0]["Prod_Description"].ToString();
            txtMOQ.Text = ds.Tables[0].Rows[0]["moq"].ToString();
            txtCurrentStock.Text = ds.Tables[0].Rows[0]["AvailableBalance"].ToString();
            txtReserveStock.Text = ds.Tables[0].Rows[0]["ResurveQty"].ToString();
            //   txtReqQty.Value = ds.Tables[0].Rows[0]["OrderQty"].ToString();
            txtOrderQty.Text = ds.Tables[0].Rows[0]["OrderQty"].ToString();
            Session["txtOrderQty"] = ds.Tables[0].Rows[0]["OrderQty"].ToString();
            txtPrc.Value = ds.Tables[0].Rows[0]["Price"].ToString();
            //txtPrice.Text = ds.Tables[0].Rows[0]["Price"].ToString();
            txtTotal.Text = ds.Tables[0].Rows[0]["Total"].ToString();
            long ProdID = long.Parse(ds.Tables[0].Rows[0]["SkuId"].ToString()); hdnSelectedProduct.Value = ProdID.ToString();
            DataSet dsUOM = new DataSet();
            dsUOM = objService.GetUOMofSelectedProduct(Convert.ToInt32(ProdID), profile.DBConnection._constr);
            ddlUOM.DataSource = dsUOM;
            ddlUOM.DataBind();
            //ddlUOM.SelectedValue =ds.Tables[0].Rows[0]["UOMID"].ToString();
            ddlUOM.SelectedIndex = ddlUOM.Items.IndexOf(ddlUOM.Items.FindByValue(ds.Tables[0].Rows[0]["UOMID"].ToString()));
            long UOM = 0;
            UOM = Convert.ToInt64(ds.Tables[0].Rows[0]["UOMID"].ToString());
            DataSet dsUOMSelPrd = new DataSet();
            dsUOMSelPrd = objService.GetUOMofSelectedProduct(Convert.ToInt32(ProdID), profile.DBConnection._constr);
            int SelInd = 0;
            SelInd = ddlUOM.SelectedIndex;
            decimal SelectedQty = decimal.Parse(dsUOMSelPrd.Tables[0].Rows[SelInd]["Quantity"].ToString());
            decimal UserQty = decimal.Parse(ds.Tables[0].Rows[0]["OrderQty"].ToString());
            txtReqQty.Value = Convert.ToString(UserQty / SelectedQty);
            DataSet dsUOM1 = new DataSet();
            dsUOM1 = objService.GetQtyofSelectedUOM(Convert.ToInt64(ProdID), UOM, profile.DBConnection._constr);
            long Qty1 = long.Parse(dsUOM1.Tables[0].Rows[0]["Quantity"].ToString());
            hdnQuantity.Value = Qty1.ToString();
            if (ds.Tables[0].Rows[0]["serialflag"].ToString() == "Yes")
            {
                int rqty = 0;
                List<PowerOnRentwebapp.PORServicePartRequest.SP_GetPartSerialDetail_Result> RequestPartList1 = new List<PowerOnRentwebapp.PORServicePartRequest.SP_GetPartSerialDetail_Result>();
                RequestPartList1 = objService.GetPartSerialDetailByRequestID(OrderID, Convert.ToInt64(ProdID), HttpContext.Current.Session.SessionID, profile.Personal.UserID.ToString(), ObjectNameSerial, profile.DBConnection._constr).ToList();
                rqty = Convert.ToInt32(RequestPartList1.Count.ToString());
                Session["serialOrderQty"] = rqty.ToString();
            }
            else
            {
                Session["serialOrderQty"] = "0";
            }
        }

        [WebMethod]
        public static long WMGetQty(long SelectedProduct, long SelectedUOM)
        {
            CustomProfile profile = CustomProfile.GetProfile();
            iPartRequestClient objService = new iPartRequestClient();
            DataSet dsUOM = new DataSet();
            dsUOM = objService.GetQtyofSelectedUOM(SelectedProduct, SelectedUOM, profile.DBConnection._constr);
            long Qty = long.Parse(dsUOM.Tables[0].Rows[0]["Quantity"].ToString());
            return Qty;
        }

        [WebMethod]
        public static int WMUpdateOrderProductDetails(decimal OrderQty, decimal Price, decimal Total)
        {
            int Result = 0;
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                iPartRequestClient objService = new iPartRequestClient();
                decimal oldqty = Convert.ToDecimal(HttpContext.Current.Session["serialOrderQty"]);
                if (oldqty == 0)
                {                  
                    Result = objService.UpdateOrderQtyTotal(OrderQty, Price, Total, OrderID, Sequence,profile.Personal.UserID, profile.DBConnection._constr);
                    List<POR_SP_GetPartDetail_ForRequest_Result> RequestPartList = new List<POR_SP_GetPartDetail_ForRequest_Result>();
                    RequestPartList = objService.GetRequestPartDetailByRequestID(OrderID, SiteID, HttpContext.Current.Session.SessionID, profile.Personal.UserID.ToString(), ObjectName, profile.DBConnection._constr).ToList();
                    Result = 1;
                }
                else
                {
                    if (oldqty == OrderQty)
                    {
                        Result = objService.UpdateOrderQtyTotal(OrderQty, Price, Total, OrderID, Sequence,profile.Personal.UserID, profile.DBConnection._constr);
                        List<POR_SP_GetPartDetail_ForRequest_Result> RequestPartList = new List<POR_SP_GetPartDetail_ForRequest_Result>();
                        RequestPartList = objService.GetRequestPartDetailByRequestID(OrderID, SiteID, HttpContext.Current.Session.SessionID, profile.Personal.UserID.ToString(), ObjectName, profile.DBConnection._constr).ToList();
                    }
                    else
                    {
                        Result = 9999;
                    }
                }
            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, "ChangeOrderProduct.aspx", "WMUpdateOrderProductDetails");
            }
            return Result;
        }

        private void loadstring()
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(Session["Lang"].ToString());
                rm = new ResourceManager("PowerOnRentwebapp.App_GlobalResources.Lang", Assembly.GetExecutingAssembly());
                ci = Thread.CurrentThread.CurrentCulture;

                lblProductCode.Text = rm.GetString("ProductCode", ci);
                lblProductName.Text = rm.GetString("ProductName", ci);
                lblProdDescription.Text = rm.GetString("ProductDescription", ci);
                lblMOQ.Text = rm.GetString("MOQ", ci);
                lblCurrentStock.Text = rm.GetString("CurrentStock", ci);
                lblReserveQty.Text = rm.GetString("ReserveQty", ci);
                lblRequestQty.Text = rm.GetString("RequestQty", ci);
                lblUOM.Text = rm.GetString("uom", ci);
                lblOrderQty.Text = rm.GetString("OrderQty", ci);
                lblPrice.Text = rm.GetString("Price", ci);
                lblTotal.Text = rm.GetString("Total", ci);
                btnSubmit.Value = rm.GetString("Submit", ci);
            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "Account Master", "loadstring");
            }
        }


        [WebMethod]
        public static string WMRemoveAssignSkuSerial(string Oid, string Sid, string qty, decimal OrderQty, decimal Price, decimal Total)
        {
            string result = "";
            try
            {
                iPartRequestClient objService = new iPartRequestClient();
                CustomProfile profile = CustomProfile.GetProfile();
                objService.RemoveSkuserialFromRequest_TempData(HttpContext.Current.Session.SessionID, profile.Personal.UserID.ToString(), ObjectNameSerial, Convert.ToInt64(Sid), profile.DBConnection._constr);
                objService.DeleteSkuwiseSerialFromqtychange(profile.Personal.UserID, Convert.ToInt64(Sid), Convert.ToInt64(Oid), "After order qty change in qty popup", profile.DBConnection._constr);

                int Result1 = 0;
                Result1 = objService.UpdateOrderQtyTotal(OrderQty, Price, Total, OrderID, Sequence,profile.Personal.UserID, profile.DBConnection._constr);
                List<POR_SP_GetPartDetail_ForRequest_Result> RequestPartList = new List<POR_SP_GetPartDetail_ForRequest_Result>();
                RequestPartList = objService.GetRequestPartDetailByRequestID(OrderID, SiteID, HttpContext.Current.Session.SessionID, profile.Personal.UserID.ToString(), ObjectName, profile.DBConnection._constr).ToList();
              

                result = "1";
            }
            catch (System.Exception ex) { result = "0"; Login.Profile.ErrorHandling(ex, "ChangeOrderProduct.aspx", "WMGetSelectedSKUQty"); }
            return result;
        }


    }
}