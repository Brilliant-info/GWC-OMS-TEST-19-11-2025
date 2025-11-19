using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Data;
using PowerOnRentwebapp.Login;
using PowerOnRentwebapp.CompanySetupService;
using System.IO;
using System.Web.Services;
using PowerOnRentwebapp.PORServicePartRequest;
using iTextSharp.text.pdf;

namespace PowerOnRentwebapp.PowerOnRent
{
    public partial class VirtualProductReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    Session["RptOrderID"] = Request.QueryString["id"];
                    int packageid = Convert.ToInt16(Request.QueryString["PackageID"]);
                    Session["PackageID"] = Request.QueryString["PackageID"].ToString();
                    long OrderId = Convert.ToInt64(Request.QueryString["id"]);
                    string skucode = Request.QueryString["Skucode"].ToString();
                    Session["Skucode"] = Request.QueryString["Skucode"].ToString();
                    string srno = Request.QueryString["srno"].ToString();
                    hdnparameter.Value = Request.QueryString["id"];
                    hdnskucode.Value = Request.QueryString["Skucode"];
                    iPartRequestClient objService = new iPartRequestClient();
                    CustomProfile profile = CustomProfile.GetProfile();
                    //int cnt = 0;
                    //cnt = objService.GetOrdersCnt(Convert.ToString(OrderId), profile.DBConnection._constr);
                    string skuType = "";
                    skuType = objService.GetOrderType(long.Parse(hdnparameter.Value), hdnskucode.Value, packageid, profile.DBConnection._constr);
                    if (skuType.ToUpper() == "POSTPAID")  //if (hdnskucode.Value == "110038")
                    {
                        if (packageid >= 2)
                        {
                            Delivery.Visible = false;
                        }
                        else
                        {
                            Delivery.Visible = true;
                        }
                        Broadband.Visible = true;
                        prepaid.Visible = false;
                        FMS.Visible = false;
                    }
                    else if (skuType.ToUpper() == "PREPAID") //else if (skuType == "Prepaid") if (hdnskucode.Value == "110037")
                    {
                        if (packageid >= 2)
                        {
                            Delivery.Visible = false;
                        }
                        else
                        {
                            Delivery.Visible = true;
                        }
                        Broadband.Visible = false;
                        prepaid.Visible = true;
                        FMS.Visible = false;
                    }
                    else if (skuType.ToUpper() == "FMS") //if    (hdnskucode.Value == "SimFMSIn")
                    {
                        if (packageid >= 2)
                        {
                            Delivery.Visible = false;
                        }
                        else
                        {
                            Delivery.Visible = true;
                        }
                        Broadband.Visible = false;
                        prepaid.Visible = false;
                        FMS.Visible = true;
                    }
                    else
                    {
                        if (packageid >= 2)
                        {
                            Delivery.Visible = false;
                        }
                        else
                        {
                            Delivery.Visible = true;
                        }
                        Broadband.Visible = true;
                        prepaid.Visible = true;
                        FMS.Visible = true;
                    }

                    CreateBarCode(OrderId);
                }
            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, "VirtualProductReport.aspx", "Page_Load");
            }

        }

        private void CreateBarCode(long orderId)
        {
            try
            {
                iPartRequestClient objService = new iPartRequestClient();
                CustomProfile profile = CustomProfile.GetProfile();
                string ChkBarCodeCreateOrNot = objService.ChkBarcode(orderId, profile.DBConnection._constr);
                if (ChkBarCodeCreateOrNot == "No")
                {
                    DataSet ds = new DataSet();
                    string Orderno = "", Amtpaid = "";
                    ds = objService.GetOrderDetails(orderId, profile.DBConnection._constr);
                    Orderno = ds.Tables[0].Rows[0]["Orderno"].ToString();
                    Amtpaid = ds.Tables[0].Rows[0]["amounttobecollected"].ToString();
                    Barcode128 bc = new Barcode128();
                    bc.Code = Orderno;
                    System.Drawing.Image imgbrcd = bc.CreateDrawingImage(System.Drawing.Color.Black, System.Drawing.Color.White);
                    bc.BarHeight = 1000f;
                    byte[] ss = imgtobytearray(imgbrcd);
                    Barcode128 bc1 = new Barcode128();
                    bc1.Code = Amtpaid;
                    System.Drawing.Image imgbrcd1 = bc1.CreateDrawingImage(System.Drawing.Color.Black, System.Drawing.Color.White);
                    bc1.BarHeight = 1000f;
                    byte[] ss1 = imgtobytearray(imgbrcd1);
                    objService.InsertintomDeliverybarCode(orderId, ss, ss1, profile.DBConnection._constr);
                }
            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, "VirtualProductReport.aspx", "CreateBarCode");
            }

        }

        public byte[] imgtobytearray(System.Drawing.Image imgin)
        {
            MemoryStream ms = new MemoryStream();
            imgin.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        [WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public static int WMVirtualReport(string report)
        {
            int result = 0; string cname = "", deliverytype = "";
            DataSet dsPrepaidRpt = new DataSet();
            iPartRequestClient objService = new iPartRequestClient();
            CustomProfile profile = CustomProfile.GetProfile();
            string OrderID = HttpContext.Current.Session["RptOrderID"].ToString();
            dsPrepaidRpt = objService.GetReportData(long.Parse(OrderID), profile.DBConnection._constr);
            dsPrepaidRpt.Tables[0].TableName = "dsPrepaidRpt";
            if (dsPrepaidRpt.Tables[0].Rows.Count > 0)
            {
                deliverytype = dsPrepaidRpt.Tables[0].Rows[0]["DeliveryType"].ToString();
                // if (deliverytype.ToUpper()=="HUB")
                //  {
                //     cname = dsPrepaidRpt.Tables[0].Rows[0]["VodafoneStoreName"].ToString();
                //  }
                //  else
                //   {
                cname = dsPrepaidRpt.Tables[0].Rows[0]["CustomerFirstName"].ToString() + " " + dsPrepaidRpt.Tables[0].Rows[0]["CustomerLastName"].ToString();
                //  }
                // cname = dsPrepaidRpt.Tables[0].Rows[0]["VodafoneStoreName"].ToString();
                HttpContext.Current.Session["Fullname"] = cname;
                HttpContext.Current.Session["Idt"] = dsPrepaidRpt.Tables[0].Rows[0]["Idt"].ToString();
                HttpContext.Current.Session["IDExpiryDate"] = dsPrepaidRpt.Tables[0].Rows[0]["IDExpiryDate"].ToString();
                HttpContext.Current.Session["OrderCreationDate"] = dsPrepaidRpt.Tables[0].Rows[0]["OrderCreationDate"].ToString();
                HttpContext.Current.Session["ReportDS"] = dsPrepaidRpt;
                HttpContext.Current.Session["SelObject"] = dsPrepaidRpt;
                result = Convert.ToInt16(dsPrepaidRpt.Tables[0].Rows.Count);
            }
            else { result = 0; }
            return result;
        }

        [WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public static int WMPostpaidReport(string report)
        {
            int result = 0;
            DataSet dsPostpaidRpt = new DataSet();
            iPartRequestClient objService = new iPartRequestClient();
            CustomProfile profile = CustomProfile.GetProfile();
            string OrderID = HttpContext.Current.Session["RptOrderID"].ToString();
            string PackageID = HttpContext.Current.Session["PackageID"].ToString();
            string Skucode = HttpContext.Current.Session["Skucode"].ToString();
            dsPostpaidRpt = objService.GetBoardbandReportData(long.Parse(OrderID), profile.DBConnection._constr);
            dsPostpaidRpt.Tables[0].TableName = "dsBoardband";
            if (dsPostpaidRpt.Tables[0].Rows.Count > 0)
            {
                string cname = dsPostpaidRpt.Tables[0].Rows[0]["Fullname"].ToString();
                //  if (string.IsNullOrEmpty(cname))
                if (cname == " ")
                {
                    HttpContext.Current.Session["Fullname"] = dsPostpaidRpt.Tables[0].Rows[0]["VodafoneStoreName"].ToString();
                }
                else
                {
                    HttpContext.Current.Session["Fullname"] = dsPostpaidRpt.Tables[0].Rows[0]["Fullname"].ToString();
                }

                if (dsPostpaidRpt.Tables[0].Rows[0]["idtype"].ToString().Trim() == "Passport")
                {
                    HttpContext.Current.Session["pidt"] = dsPostpaidRpt.Tables[0].Rows[0]["idt"].ToString();
                    HttpContext.Current.Session["qidt"] = "";
                }
                else
                {
                    HttpContext.Current.Session["qidt"] = dsPostpaidRpt.Tables[0].Rows[0]["idt"].ToString();
                    HttpContext.Current.Session["pidt"] = "";
                }
                string Packname = "";
                // Packname = objService.GetMobilenumberforpostpaidreport(OrderID, Skucode, PackageID, profile.DBConnection._constr);
                Packname = objService.GetMobilePackName(OrderID, Skucode, PackageID, profile.DBConnection._constr);
                string Mobilenumber = "";
                Mobilenumber = objService.GetMobilenumberforpostpaidreport(OrderID, "999999", PackageID, profile.DBConnection._constr);
                string Extrasvalue = objService.GetExtrasvalues(OrderID, profile.DBConnection._constr);
                HttpContext.Current.Session["Extrasvalue"] = Extrasvalue;
                HttpContext.Current.Session["Packname"] = Packname;
                HttpContext.Current.Session["Mobilenumber"] = Mobilenumber;
                HttpContext.Current.Session["ReportDS"] = dsPostpaidRpt;
                HttpContext.Current.Session["SelObject"] = dsPostpaidRpt;
                result = Convert.ToInt16(dsPostpaidRpt.Tables[0].Rows.Count);
            }
            else { result = 0; }
            return result;
        }

        [WebMethod]
        [System.Web.Script.Services.ScriptMethod]
        public static int WMDeliveryReport(string report)
        {
            int result = 0; string cname = "", address = "", deliverytype = "";
            DataSet dsDeliveryFormRpt = new DataSet();
            iPartRequestClient objService = new iPartRequestClient();
            CustomProfile profile = CustomProfile.GetProfile();
            string OrderID = HttpContext.Current.Session["RptOrderID"].ToString();
            dsDeliveryFormRpt = objService.GetReportData(long.Parse(OrderID), profile.DBConnection._constr);
            dsDeliveryFormRpt.Tables[0].TableName = "dsDeliveryFormRpt";
            if (dsDeliveryFormRpt.Tables[0].Rows.Count > 0)
            {
                string ISFlextProduct = "";
                ISFlextProduct=objService.ChkISFlextProduct(long.Parse(OrderID), profile.DBConnection._constr);
                deliverytype = dsDeliveryFormRpt.Tables[0].Rows[0]["DeliveryType"].ToString();
                cname = dsDeliveryFormRpt.Tables[0].Rows[0]["CustomerFirstName"].ToString() + " " + dsDeliveryFormRpt.Tables[0].Rows[0]["CustomerLastName"].ToString();
                if (ISFlextProduct == "Yes")
                {
                    address = dsDeliveryFormRpt.Tables[0].Rows[0]["Streetname"].ToString();
                }
                else if(deliverytype =="Home")
                {
                    address = dsDeliveryFormRpt.Tables[0].Rows[0]["BuildingName"].ToString() + ", " + dsDeliveryFormRpt.Tables[0].Rows[0]["Streetname"].ToString() + ", " + dsDeliveryFormRpt.Tables[0].Rows[0]["City"].ToString() + ", " + dsDeliveryFormRpt.Tables[0].Rows[0]["Country"].ToString();
                }
                else
                {
                    address = dsDeliveryFormRpt.Tables[0].Rows[0]["BuildingName2"].ToString() + ", " + dsDeliveryFormRpt.Tables[0].Rows[0]["Streetname2"].ToString() + ", " + dsDeliveryFormRpt.Tables[0].Rows[0]["City2"].ToString() + ", " + dsDeliveryFormRpt.Tables[0].Rows[0]["Country2"].ToString();
                }
                HttpContext.Current.Session["Fullname"] = cname;
                HttpContext.Current.Session["Address"] = address;
                HttpContext.Current.Session["ReportDS"] = dsDeliveryFormRpt;
                HttpContext.Current.Session["SelObject"] = dsDeliveryFormRpt;
                result = Convert.ToInt16(dsDeliveryFormRpt.Tables[0].Rows.Count);
            }
            else { result = 0; }
            return result;
        }


        [WebMethod]
        public static int WMFMSReport(string report)
        {
            int result = 0;
            DataSet dsPostpaidRpt = new DataSet();
            iPartRequestClient objService = new iPartRequestClient();
            CustomProfile profile = CustomProfile.GetProfile();
            string OrderID = HttpContext.Current.Session["RptOrderID"].ToString();
            string PackageID = HttpContext.Current.Session["PackageID"].ToString();
            string Skucode = HttpContext.Current.Session["Skucode"].ToString();
            dsPostpaidRpt = objService.GetFMSReportData(long.Parse(OrderID), profile.DBConnection._constr);
            dsPostpaidRpt.Tables[0].TableName = "dsFMSRpt";
            if (dsPostpaidRpt.Tables[0].Rows.Count > 0)
            {
                string cname = dsPostpaidRpt.Tables[0].Rows[0]["Fullname"].ToString();
                //  if (string.IsNullOrEmpty(cname))
                if (cname == " ")
                {
                    HttpContext.Current.Session["Fullname"] = dsPostpaidRpt.Tables[0].Rows[0]["VodafoneStoreName"].ToString();
                }
                else
                {
                    HttpContext.Current.Session["Fullname"] = dsPostpaidRpt.Tables[0].Rows[0]["Fullname"].ToString();
                }

                if (dsPostpaidRpt.Tables[0].Rows[0]["idtype"].ToString().Trim() == "Passport")
                {
                    HttpContext.Current.Session["pidt"] = dsPostpaidRpt.Tables[0].Rows[0]["idt"].ToString();
                    HttpContext.Current.Session["qidt"] = "";
                }
                else
                {
                    HttpContext.Current.Session["qidt"] = dsPostpaidRpt.Tables[0].Rows[0]["idt"].ToString();
                    HttpContext.Current.Session["pidt"] = "";
                }
                string Packname = "";
                // Packname = objService.GetMobilenumberforpostpaidreport(OrderID, Skucode, PackageID, profile.DBConnection._constr);
                Packname = objService.GetMobilePackName(OrderID, Skucode, PackageID, profile.DBConnection._constr);
                string Mobilenumber = "";
                Mobilenumber = objService.GetMobilenumberforpostpaidreport(OrderID, "999999", PackageID, profile.DBConnection._constr);
                string Extrasvalue = objService.GetExtrasvalues(OrderID, profile.DBConnection._constr);
                HttpContext.Current.Session["Extrasvalue"] = Extrasvalue;
                HttpContext.Current.Session["Packname"] = Packname;
                HttpContext.Current.Session["Mobilenumber"] = Mobilenumber;
                HttpContext.Current.Session["ReportDS"] = dsPostpaidRpt;
                HttpContext.Current.Session["SelObject"] = dsPostpaidRpt;
                result = Convert.ToInt16(dsPostpaidRpt.Tables[0].Rows.Count);
            }
            else { result = 0; }
            return result;
        }


    }
}