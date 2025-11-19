using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PowerOnRentwebapp.Login;
using PowerOnRentwebapp.UCProductSearchService;
using System.Resources;
using System.Globalization;
using System.Threading;
using System.Reflection;
using System.Web.Services;
using PowerOnRentwebapp.CompanySetupService;
using System.Data;
using System.Data.SqlClient;
using PowerOnRentwebapp.AvailableQtyService;
using PowerOnRentwebapp.ProductMasterService;
using PowerOnRentwebapp.UserCreationService;
using System.Text;

namespace PowerOnRentwebapp.Product
{
    public partial class ProductSearch : System.Web.UI.Page
    {
        ResourceManager rm;
        CultureInfo ci;
        protected void Page_PreInit(Object sender, EventArgs e)
        { //CustomProfile profile = CustomProfile.GetProfile(); if (profile.Personal.Theme == null || profile.Personal.Theme == string.Empty) { Page.Theme = "Blue"; } else { Page.Theme = profile.Personal.Theme; } 
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // hndgrupByGrid.Value = GridProductSearch.GroupBy;

                RebindGrid(sender, e);
                if (Session["Lang"] == "")
                {
                    Session["Lang"] = Request.UserLanguages[0];
                }
                loadstring();
            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "ProductSearch.aspx.cs", "Page_Load");
            }

        }

        protected void RebindGrid(object sender, EventArgs e)
        {
            iUserCreationClient userClient = new iUserCreationClient();
            UCProductSearchService.iUCProductSearchClient productSearchService = new UCProductSearchService.iUCProductSearchClient();
            CustomProfile profile = CustomProfile.GetProfile();
            DataSet dsnew = new DataSet();
            DataSet dsgrd = new DataSet();
            try
            {
                long DeptID = long.Parse(Session["DeptID"].ToString());
                long UserID = profile.Personal.UserID;
                dsnew = productSearchService.GetSkuListDeptWiseToUpdate(GridProductSearch.CurrentPageIndex, hdnFilterText.Value, UserID, DeptID, profile.DBConnection._constr);
                for (int i = 0; i < dsnew.Tables[0].Rows.Count; i++)
                {
                    long SKUid = long.Parse(dsnew.Tables[0].Rows[i]["ID"].ToString());
                    UpdateAvailableQty(DeptID, SKUid);
                }
                dsnew.Dispose();
                dsgrd = productSearchService.GetSkuListDeptWiseNewe(GridProductSearch.CurrentPageIndex, hdnFilterText.Value, UserID, DeptID, profile.DBConnection._constr);
                // dsgrd = userClient.GetSkuListDeptWiseNewe1(GridProductSearch.CurrentPageIndex, hdnFilterText.Value, UserID, DeptID, profile.DBConnection._constr);
                GridProductSearch.DataSource = dsgrd;
                // GridProductSearch.GroupBy = hndgrupByGrid.Value;
                GridProductSearch.DataBind();
                productSearchService.Close();
            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "ProductSearch.aspx.cs", "RebindGrid");
            }
            finally
            {
                dsgrd.Dispose();
                productSearchService.Close();
            }
        }

        private void loadstring()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(Session["Lang"].ToString());
            rm = new ResourceManager("PowerOnRentwebapp.App_GlobalResources.Lang", Assembly.GetExecutingAssembly());
            ci = Thread.CurrentThread.CurrentCulture;

            lblheader.Text = rm.GetString("SKUList", ci);
            //   lblwithbom.Text = rm.GetString("WithBOM", ci);
            btnSubmitProductSearch1.Value = rm.GetString("Submit", ci);
            btnSubmitProductSearch2.Value = rm.GetString("Submit", ci);
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
                // dsnew = userClient.GetProdAvailableBal1(skuid, profile.DBConnection._constr);
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
                // dsschema = userClient.GetDatabaseSchemaforDept1(Deptid, profile.DBConnection._constr);
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
                    string companyisrealtime = productClient.GetcompanyRealtimestock(Deptid, profile.DBConnection._constr);
                    if (companyisrealtime == "Yes")
                    {
                        // decimal RealTimeStockQty = 0;
                        decimal RealTimeStockQty = RealTimeStock.CheckAvailableQty(ProductCode, DeptCode, DatabaseName, ConnectionString, Schemaname);
                        //real time stock log
                        if (totalQTy != RealTimeStockQty)
                        {
                            productSearchService.Createtransaction(Convert.ToString(ProductCode), Convert.ToString(Deptid), RealTimeStockQty, profile.DBConnection._constr);
                        }
                    }

                }

            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, "ProductSearch.aspx.cs", "RebindGrid");
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
        public static decimal PrductStockCount(string Pid)
        {
            CheckStockAvailableClient RealTimeStock = new CheckStockAvailableClient();
            iProductMasterClient productClient = new iProductMasterClient();
            CustomProfile profile = CustomProfile.GetProfile();
            DataSet dsschema = new DataSet();
            string Schemaname="",DatabaseName="",ConnectionString = "";
            decimal i = 0;
            UCProductSearchService.iUCProductSearchClient productSearchService = new UCProductSearchService.iUCProductSearchClient();
            try
            {
                DataSet dsnew = new DataSet();
                string[] SkuIds = Pid.Split(',');
                foreach (string item in SkuIds)
                {
                    string ProductCode = "", DeptCode = "";
                    decimal AvailableBalance = 0, ResurveQty = 0;
                    long Deptid = 0;
                    long skuid = Convert.ToInt64(item);
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
                        i = RealTimeStockQty;
                        //real time stock log logic
                        
                        productSearchService.insertlog(ProductCode, Deptid,"ProductSearch", AvailableBalance, ResurveQty, RealTimeStockQty, profile.DBConnection._constr);

                        if (totalQTy != RealTimeStockQty)
                        {
                            if (profile.Personal.CompanyID == 10266 || profile.Personal.CompanyID == 10261)
                            {

                            }
                            else
                            {
                                productSearchService.Createtransaction(Convert.ToString(ProductCode), Convert.ToString(Deptid), RealTimeStockQty, profile.DBConnection._constr);
                            }
                        }
                    }
                }

            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, "ProductSearch.aspx.cs", "PrductStockCount");
            }
            finally
            {
                // RealTimeStock.Close();
                productSearchService.Close();
                productClient.Close();
                dsschema.Dispose();
            }
            return i;

        }



    }
}
