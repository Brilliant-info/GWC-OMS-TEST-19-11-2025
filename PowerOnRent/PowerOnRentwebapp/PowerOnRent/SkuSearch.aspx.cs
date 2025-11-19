using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using PowerOnRentwebapp.DesignationService;
using PowerOnRentwebapp.RoleMasterService;
using PowerOnRentwebapp.UserCreationService;
using PowerOnRentwebapp.CompanySetupService;
using System.Web.Services;
using PowerOnRentwebapp.Login;
using System.Web.Security;
using WebMsgBox;
using Obout.Grid;
//using PowerOnRentwebapp.ServiceTerritory;
using PowerOnRentwebapp.Territory;
using System.Web.UI.HtmlControls;
using System.Resources;
using System.Globalization;
using System.Threading;
using System.Reflection;
using System.Data;
using System.Net.Mail;
using PowerOnRentwebapp.ProductMasterService;
using System.Configuration;
using System.Data.SqlClient;
using System.Xml;
using PowerOnRentwebapp.AvailableQtyService;
using System.Configuration;
using System.Web.Profile;

namespace PowerOnRentwebapp.PowerOnRent
{
    public partial class SkuSearch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            hndgrupByGrid.Value = GridProductSearch.GroupBy;
            if (!IsPostBack)
            {
                fillCompany();
                FillDepartment();

            }
        }

        private void FillDepartment()
        {
            DataSet dscom = new DataSet();
            dscom = null;
            ddldepartment.DataSource = dscom;
            ddldepartment.DataBind();
            ListItem lst = new ListItem();
            lst.Text = "-Select All-";
            lst.Value = "0";
            ddldepartment.Items.Insert(0, lst);
        }

        protected void GridProductSearch_Rebind(object sender, EventArgs e)
        {
            BindGV(long.Parse(hdndeptid.Value));
        }

        private void BindGV(long deptID)
        {
            using (UCProductSearchService.iUCProductSearchClient productSearchService = new UCProductSearchService.iUCProductSearchClient())
            {
                try
                {
                    CustomProfile profile = CustomProfile.GetProfile();
                    DataSet dsnew = new DataSet();
                    long DeptID = long.Parse(hdndeptid.Value);
                    long UserID = profile.Personal.UserID;
                    dsnew = productSearchService.GetSkuListDeptWiseToUpdate(GridProductSearch.CurrentPageIndex, hdnFilterText.Value, UserID, DeptID, profile.DBConnection._constr);
                    for (int i = 0; i < dsnew.Tables[0].Rows.Count; i++)
                    {
                        long SKUid = long.Parse(dsnew.Tables[0].Rows[i]["ID"].ToString());
                        UpdateAvailableQty(DeptID, SKUid);
                    }
                    DataSet dsgrd = new DataSet();
                    dsgrd = productSearchService.GetSkuListDeptWiseNewe(GridProductSearch.CurrentPageIndex, hdnFilterText.Value, UserID, DeptID, profile.DBConnection._constr);
                    GridProductSearch.DataSource = dsgrd;
                    GridProductSearch.GroupBy = hndgrupByGrid.Value;
                    GridProductSearch.DataBind();
                    productSearchService.Close();

                }
                catch (System.Exception ex)
                {
                    Login.Profile.ErrorHandling(ex, this, "Stock Config", "GetUserByID");
                }
            }
        }


        public void UpdateAvailableQty(long Deptid, long skuid)
        {
            CheckStockAvailableClient RealTimeStock = new CheckStockAvailableClient();
            iProductMasterClient productClient = new iProductMasterClient();
            UCProductSearchService.iUCProductSearchClient productSearchService = new UCProductSearchService.iUCProductSearchClient();
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
        private void fillCompany()
        {
            using (iCompanySetupClient comsetup = new iCompanySetupClient())
            {
                CustomProfile profile = CustomProfile.GetProfile();
                DataSet dscom = new DataSet();
                if (profile.Personal.UserType == "Super Admin")
                {
                    dscom = comsetup.BindCompany(profile.DBConnection._constr);
                }
                else
                {
                    dscom = comsetup.BindCompanyUserWise(profile.Personal.CompanyID, profile.DBConnection._constr);
                }
                ddlcompany.DataSource = dscom;
                ddlcompany.DataBind();
                ListItem lst = new ListItem();
                lst.Text = "-Select All-";
                lst.Value = "0";
                ddlcompany.Items.Insert(0, lst);
            }
        }





        [WebMethod]
        public static List<contact> GetDepartment(object objReq)
        {
            iProductMasterClient productClient = new iProductMasterClient();
            CustomProfile profile = CustomProfile.GetProfile();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            List<contact> LocList = new List<contact>();
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary = (Dictionary<string, object>)objReq;
                //ds = ReceivableClient.GetProdLocations(ProdCode.Trim());
                long ddlcompanyId = long.Parse(dictionary["ddlcompanyId"].ToString());
                ds = productClient.GetDepartment(ddlcompanyId, profile.DBConnection._constr);
                dt = ds.Tables[0];
                contact Loc = new contact();
                Loc.Name = "Select Department";
                Loc.Id = "0";
                LocList.Add(Loc);
                Loc = new contact();

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Loc.Id = dt.Rows[i]["ID"].ToString();
                        Loc.Name = dt.Rows[i]["Territory"].ToString();
                        LocList.Add(Loc);
                        Loc = new contact();
                    }
                }
            }
            catch (Exception ex)
            {
                Login.Profile.ErrorHandling(ex, "SkuSearch", "GetDepartment");
            }
            finally
            {
                productClient.Close();
            }
            return LocList;
        }


        public class contact
        {
            private string _name;
            public string Name
            {
                get { return _name; }
                set { _name = value; }
            }

            private string _id;
            public string Id
            {
                get { return _id; }
                set { _id = value; }
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            iProductMasterClient productClient = new iProductMasterClient();
            CustomProfile profile = CustomProfile.GetProfile();
            DataSet ds = new DataSet();
            try
            {
                BindGV(long.Parse(hdndeptid.Value));
                DataSet dscom = new DataSet();
                dscom = productClient.BindDept(hdnSelectedCompany.Value, profile.DBConnection._constr);
                ddldepartment.DataSource = dscom;
                ddldepartment.DataBind();
                ddldepartment.SelectedValue = hdndeptid.Value;
                // ddldepartment.SelectedIndex = ddldepartment.Items.IndexOf(ddldepartment.Items.FindByText(hdndeptid.Value.ToString()));


            }
            catch (Exception ex)
            {
                Login.Profile.ErrorHandling(ex, "SkuSearch", "GetDepartment");
            }
            finally
            {
                productClient.Close();
            }
        }
    }
}