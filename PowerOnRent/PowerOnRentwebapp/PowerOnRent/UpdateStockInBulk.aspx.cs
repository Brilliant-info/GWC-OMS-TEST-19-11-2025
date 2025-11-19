using PowerOnRentwebapp.AvailableQtyService;
using PowerOnRentwebapp.Login;
using PowerOnRentwebapp.ProductMasterService;
using PowerOnRentwebapp.UCProductSearchService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PowerOnRentwebapp.PowerOnRent
{
    public partial class UpdateStockInBulk : System.Web.UI.Page
    {
        UCProductSearchService.iUCProductSearchClient productSearchService = new UCProductSearchService.iUCProductSearchClient();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnudpate_Click(object sender, EventArgs e)
        {
            CustomProfile profile = CustomProfile.GetProfile();
            string prodcode = "";string storecode = "";
            long deptID = 0;long skuid = 0; long rec = 0;
            DataSet ds = new DataSet();
            try
            {
                if (txttoprecord.Text.ToString() == "") { rec = 0; }
                else { rec = Convert.ToInt64(txttoprecord.Text.ToString()); }
                ds = productSearchService.getProductdetailsUpdate(rec, profile.DBConnection._constr);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {

                    deptID = Convert.ToInt64(ds.Tables[0].Rows[i]["deptID"].ToString());
                    skuid = Convert.ToInt64(ds.Tables[0].Rows[i]["skuID"].ToString());
                    prodcode= ds.Tables[0].Rows[i]["Productcode"].ToString();
                    storecode = ds.Tables[0].Rows[i]["Storecode"].ToString();
                    UpdateAvailableQty(deptID, skuid);
                    productSearchService.updateIsreadFlag(prodcode, storecode, profile.DBConnection._constr);
                }
                txttoprecord.Text = "";
            }
            catch(Exception ex) { Login.Profile.ErrorHandling(ex, "UpdateStockInBulk.aspx.cs", "btnudpate_Click"); }
        }
        public void UpdateAvailableQty(long Deptid, long skuid)
        {
            CheckStockAvailableClient RealTimeStock = new CheckStockAvailableClient();
            iProductMasterClient productClient = new iProductMasterClient();
            iUCProductSearchClient productSearchService = new iUCProductSearchClient();
            CustomProfile profile = CustomProfile.GetProfile();
            DataSet dsschema = new DataSet();
            DataSet dsnew = new DataSet();
            try
            {
                string DatabaseName = "", ConnectionString = "", Schemaname = "", wmsstorecode = "";
                string ProductCode = "", DeptCode = "";
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
                decimal totalQTy = AvailableBalance + ResurveQty;
                dsschema = productClient.GetDatabaseSchemaforDept(Deptid, profile.DBConnection._constr);
                if (dsschema.Tables[0].Rows.Count > 0)
                {
                    DatabaseName = dsschema.Tables[0].Rows[0]["DatabaseName"].ToString();
                    ConnectionString = dsschema.Tables[0].Rows[0]["ConnectionString"].ToString();
                    Schemaname = dsschema.Tables[0].Rows[0]["Schemaname"].ToString();
                    wmsstorecode = dsschema.Tables[0].Rows[0]["wmsstorecode"].ToString();
                }
                if (string.IsNullOrEmpty(wmsstorecode))
                {
                }
                else
                {
                    DeptCode = wmsstorecode;
                }
                if (Schemaname != "")
                {
                    string companyisrealtime = productClient.GetcompanyRealtimestock(Deptid, profile.DBConnection._constr);
                    if (companyisrealtime == "Yes")
                    {
                        // decimal RealTimeStockQty = 0;
                        decimal RealTimeStockQty = RealTimeStock.CheckAvailableQty(ProductCode, DeptCode, DatabaseName, ConnectionString, Schemaname);
                        if (totalQTy != RealTimeStockQty)
                        {
                            productSearchService.Createtransaction(Convert.ToString(ProductCode), Convert.ToString(Deptid), RealTimeStockQty, profile.DBConnection._constr);
                        }
                    }

                }
            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, "UpdateStockInBulk.aspx.cs", "UpdateAvailableQty");
            }
            finally
            {
                // RealTimeStock.Close();
                productSearchService.Close();
                productClient.Close();
                dsschema.Dispose();
            }
        }

       
    }
}