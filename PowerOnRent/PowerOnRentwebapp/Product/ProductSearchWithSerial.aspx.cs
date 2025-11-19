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
using PowerOnRentwebapp.RTSerialStock;
using System.ComponentModel;


namespace PowerOnRentwebapp.Product
{
    public partial class ProductSearchWithSerial : System.Web.UI.Page
    {
        static string ObjectName = "RequestSerialNumber";
        long Oid = 0;
        long Sid = 0;
        long qty = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (Request.QueryString["oid"] != null)
                {
                    Oid = long.Parse(Request.QueryString["oid"].ToString());
                    Session["oid"] = Oid;
                }
                else
                {
                    Session["oid"] = Oid;
                }
                if (Request.QueryString["sid"] != null)
                {
                    Sid = long.Parse(Request.QueryString["sid"].ToString());
                    Session["Sid"] = Sid;
                }
                else
                {
                    Session["Sid"] = Sid;
                }
                if (Request.QueryString["qty"] != null)
                {
                    qty = long.Parse(Request.QueryString["qty"].ToString());
                }

                hdnskuqty.Value = Convert.ToString(qty);
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
            SerialRTimeStockServiceClient SerialwithStock = new SerialRTimeStockServiceClient();
            iPartRequestClient objService = new iPartRequestClient();
            try
            {
                UCProductSearchService.iUCProductSearchClient productSearchService = new UCProductSearchService.iUCProductSearchClient();
                CustomProfile profile = CustomProfile.GetProfile();
                List<GetSerial> serialList = new List<GetSerial>();


                //get skucode, department and database schema details 
                DataSet dssku = new DataSet();
                dssku = productSearchService.GetSkuDeptSchemaData(Sid, profile.DBConnection._constr);
                if (dssku.Tables[0].Rows.Count > 0)
                {
                    long DeptID = Convert.ToInt64(dssku.Tables[0].Rows[0]["Deptid"]);
                    Session["deid"] = DeptID;
                    //Real Time stock update total quantity
                    UpdateAvailableQty(DeptID, Sid);

                    string SKucode = Convert.ToString(dssku.Tables[0].Rows[0]["productcode"].ToString());
                    string Deptcode = Convert.ToString(dssku.Tables[0].Rows[0]["storecode"].ToString());
                    string Database = Convert.ToString(dssku.Tables[0].Rows[0]["DatabaseName"].ToString());
                    string Connection = Convert.ToString(dssku.Tables[0].Rows[0]["ConnectionString"].ToString());
                    string Schema = Convert.ToString(dssku.Tables[0].Rows[0]["SchemaNm"].ToString());
                    string wmsstorecode = Convert.ToString(dssku.Tables[0].Rows[0]["WmsStorecode"].ToString());
                    if (string.IsNullOrEmpty(wmsstorecode))
                    {

                    }
                    else
                    {
                        Deptcode = wmsstorecode;
                    }
                    //Get Real Time Sku Serial with available balance List
                    if (profile.Personal.CompanyID == 10266)
                    {
                        serialList = SerialwithStock.CheckAvailableQty("DRUM1", "VF5264PB", Database, Connection, "wmwhse4");
                    }
                    else
                    {
                        serialList = SerialwithStock.CheckAvailableQty(SKucode, Deptcode, Database, Connection, Schema);
                    }

                    serialList = serialList.ConvertAll(d => new GetSerial
                    {
                        SERIAL = d.SERIAL.ToUpper(),
                        QTY = d.QTY
                    });

                    List<string> stringlistk = serialList.Where(x => x != null)
                                            .Select(x => x.SERIAL.ToUpper())
                                            .ToList();


                   
                    //get resurve serial sku

                    DataSet ds = new DataSet();
                    ds = productSearchService.GetProductSerialDetails(Oid, Sid, profile.DBConnection._constr);
                    List<string> Result3 = ds.Tables[0].Rows[0]["SerialNumber"].ToString().ToUpper().Split(',').ToList();


                    List<string> NotavailSrlst = stringlistk.Except(Result3, StringComparer.OrdinalIgnoreCase).ToList();

                    //check in temp table
                    List<PowerOnRentwebapp.PORServicePartRequest.SP_GetPartSerialDetail_Result> RequestPartList1 = new List<PowerOnRentwebapp.PORServicePartRequest.SP_GetPartSerialDetail_Result>();
                    RequestPartList1 = objService.GetExistingTempDataBySessionIDSerialNumber(HttpContext.Current.Session.SessionID, profile.Personal.UserID.ToString(), ObjectName, profile.DBConnection._constr).ToList();
                    List<string> temptableserialno = RequestPartList1.Where(x => x != null)
                                            .Select(x => x.SerialNumber)
                                            .ToList();

                    List<string> NotavailSrlst1 = new List<string>();
                    NotavailSrlst1 = NotavailSrlst.Except(temptableserialno, StringComparer.OrdinalIgnoreCase).ToList().ToList();
                    if (hdnFilterText.Value == "" || hdnFilterText.Value == null)
                    {
                        GridProductSearch.DataSource = NotavailSrlst1.Select(x => new { Value = x }).ToList();
                        GridProductSearch.DataBind();
                    }
                    else
                    {

                        string srh = hdnFilterText.Value.ToUpper();
                        List<string> NotavailSrlst3 = NotavailSrlst1.Where(x => x.Contains(srh.ToString())).ToList();                       
                        GridProductSearch.DataSource = NotavailSrlst3.Select(x => new { Value = x }).ToList();
                        GridProductSearch.DataBind();

                        //List<string> NotavailSrlst3 = NotavailSrlst1.Where(x => x.Contains(hdnFilterText.Value.ToUpper())).ToList();
                        //GridProductSearch.DataSource = NotavailSrlst3.Select(x => new { Value = x }).ToList();
                       // GridProductSearch.DataBind();
                    }

                    //GridProductSearch.DataSource = NotavailSrlst1.Select(x => new { Value = x }).ToList();
                    //GridProductSearch.DataBind();

                }
            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "ProductSearchWithSerial.aspx.cs", "RebindGrid");
            }
        }



        public void UpdateAvailableQty(long Deptid, long skuid)
        {
            CheckStockAvailableClient RealTimeStock = new CheckStockAvailableClient();
            iUserCreationClient userClient = new iUserCreationClient();
            iProductMasterClient productClient = new iProductMasterClient();
            UCProductSearchService.iUCProductSearchClient productSearchService = new UCProductSearchService.iUCProductSearchClient();
            CustomProfile profile = CustomProfile.GetProfile();
            DataSet dsschema = new DataSet();
            DataSet dsnew = new DataSet();
            try
            {
                string ProductCode = "", DeptCode = "", Schemaname = "", DatabaseName = "", ConnectionString = "";
                decimal AvailableBalance = 0, ResurveQty = 0;
                dsnew = productSearchService.GetProdAvailableBal(skuid, profile.DBConnection._constr);
                if (dsnew.Tables[0].Rows.Count > 0)
                {
                    ProductCode = dsnew.Tables[0].Rows[0]["ProductCode"].ToString();
                    AvailableBalance = decimal.Parse(dsnew.Tables[0].Rows[0]["AvailableBalance"].ToString());
                    ResurveQty = decimal.Parse(dsnew.Tables[0].Rows[0]["ResurveQty"].ToString());
                    Deptid = long.Parse(dsnew.Tables[0].Rows[0]["StoreId"].ToString());
                    DeptCode = dsnew.Tables[0].Rows[0]["StoreCode"].ToString();
                }
                dsnew.Dispose();
                decimal totalQTy = AvailableBalance + ResurveQty;
                dsschema = productClient.GetDatabaseSchemaforDept(Deptid, profile.DBConnection._constr);
                if (dsschema.Tables[0].Rows.Count > 0)
                {
                    DatabaseName = dsschema.Tables[0].Rows[0]["DatabaseName"].ToString();
                    ConnectionString = dsschema.Tables[0].Rows[0]["ConnectionString"].ToString();
                    Schemaname = dsschema.Tables[0].Rows[0]["Schemaname"].ToString();
                    string wmsstorecode = dsschema.Tables[0].Rows[0]["wmsstorecode"].ToString();
                    if (string.IsNullOrEmpty(wmsstorecode))
                    {
                    }
                    else
                    {
                        DeptCode = wmsstorecode;
                    }
                }
                if (Schemaname != "")
                {
                    //decimal RealTimeStockQty = 0;
                    decimal RealTimeStockQty = RealTimeStock.CheckAvailableQty(ProductCode, DeptCode, DatabaseName, ConnectionString, Schemaname);
                    if (totalQTy != RealTimeStockQty)
                    {
                        if (profile.Personal.CompanyID == 10266 || profile.Personal.CompanyID == 10261) { }
                        else
                        {
                            productSearchService.Createtransaction(Convert.ToString(ProductCode), Convert.ToString(Deptid), RealTimeStockQty, profile.DBConnection._constr);
                        }
                    }
                }

            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, "ProductSearchWithSerial.aspx.cs", "RebindGrid");
            }
            finally
            {
                // RealTimeStock.Close();
                productSearchService.Close();
                productClient.Close();
                dsschema.Dispose();
            }
        }

        [WebMethod]
        public static string WMGetSKUSerial(string serialid)
        {
            iPartRequestClient objService = new iPartRequestClient();
            CustomProfile profile = CustomProfile.GetProfile();
            List<PowerOnRentwebapp.PORServicePartRequest.SP_GetPartSerialDetail_Result> RequestPartList1 = new List<PowerOnRentwebapp.PORServicePartRequest.SP_GetPartSerialDetail_Result>();
            string result = "";
            try
            {
                string[] idP = serialid.Split(',').ToArray();
                for (int j = 0; j <= idP.Length - 1; j++)
                {
                    string serial = Convert.ToString(idP[j]);
                    RequestPartList1 = objService.GetExistingTempDataBySessionIDSerialNumber(HttpContext.Current.Session.SessionID, profile.Personal.UserID.ToString(), ObjectName, profile.DBConnection._constr).ToList();
                    RequestPartList1 = RequestPartList1.Where(i => i.SerialNumber == serial).ToList();
                    string pro = serial.ToString();
                    if (RequestPartList1.Count > 0)
                    {
                        RequestPartList1 = objService.GetExistingTempDataBySessionIDSerialNumber(HttpContext.Current.Session.SessionID, profile.Personal.UserID.ToString(), ObjectName, profile.DBConnection._constr).ToList();
                    }
                    else
                    {
                        if (pro == "" || pro == null)
                        {
                            RequestPartList1 = objService.GetExistingTempDataBySessionIDSerialNumber(HttpContext.Current.Session.SessionID, profile.Personal.UserID.ToString(), ObjectName, profile.DBConnection._constr).ToList();
                        }
                        else
                        {
                            long OrdId = Convert.ToInt64(HttpContext.Current.Session["oid"]);

                            long Skuid = Convert.ToInt64(HttpContext.Current.Session["Sid"]);
                            long Storeid = Convert.ToInt64(HttpContext.Current.Session["deid"]);

                            RequestPartList1 = objService.AddPartIntoRqstSeialNumber_TempData(serial, HttpContext.Current.Session.SessionID, profile.Personal.UserID.ToString(), ObjectName, Storeid, Skuid, OrdId, profile.DBConnection._constr).ToList();
                        }
                    }
                }


                RequestPartList1 = objService.GetExistingTempDataBySessionIDSerialNumber(HttpContext.Current.Session.SessionID, profile.Personal.UserID.ToString(), ObjectName, profile.DBConnection._constr).ToList();

                result =Convert.ToString(RequestPartList1.Count.ToString());
            }
            catch (System.Exception ex)
            {
                result = "0";
                Login.Profile.ErrorHandling(ex, "ProductSearchWithSerial.aspx.cs", "WmUpdateRequestPartUOM");
            }
            return result;
        }


        protected void gvLottable_RowCreated(object sender, Obout.Grid.GridRowEventArgs e)
        {

        }

        public class SKUSerialNumberFinal
        {
            private Int32 _QTY;
            public Int32 QTY
            {
                get { return _QTY; }
                set { _QTY = value; }
            }

            private string _SERIAL;
            public string SERIAL
            {
                get { return _SERIAL; }
                set { _SERIAL = value; }
            }
        }
        public class SKUSerialNumber
        {
            private Int32 _QTY;
            public Int32 QTY
            {
                get { return _QTY; }
                set { _QTY = value; }
            }

            private string _SERIAL;
            public string SERIAL
            {
                get { return _SERIAL; }
                set { _SERIAL = value; }
            }
        }
    }
}