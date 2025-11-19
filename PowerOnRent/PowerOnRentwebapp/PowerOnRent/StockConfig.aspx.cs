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


namespace PowerOnRentwebapp.PowerOnRent
{
    public partial class StockConfig : System.Web.UI.Page
    {
        //public string xmlfile = @"E:\Product Version 3\GWCPartRequestSystevVer2\GWCPartRequestSystem\PowerOnRent\PowerOnRentwebapp\XML\StockCount.xml";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnAdd.Value = "Add";
                ddlcompany.Enabled = true;
                // BindGV();
                fillCompany();
                FillDepartment();
            }
            this.UCToolbar1.evClickAddNew += pageAddNew;
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

        private void fillCompany()
        {
            using (iCompanySetupClient comsetup = new iCompanySetupClient())
            {
                CustomProfile profile = CustomProfile.GetProfile();
                DataSet dscom = new DataSet();
                dscom = comsetup.BindCompany(profile.DBConnection._constr);
                ddlcompany.DataSource = dscom;
                ddlcompany.DataBind();
                ListItem lst = new ListItem();
                lst.Text = "-Select All-";
                lst.Value = "0";
                ddlcompany.Items.Insert(0, lst);
            }
        }

        /* protected void ddldepartment_SelectedIndexChanged(object sender, EventArgs e)
         {
             //long DeptID = long.Parse(ddldepartment.SelectedItem.Value);
             //hdndeptid.Value = DeptID.ToString();
             BindGV(long.Parse(hdndeptid.Value));
         }*/

        private void BindGV(long deptID)
        {
            using (iCompanySetupClient comsetup = new iCompanySetupClient())
            {
                try
                {
                    CustomProfile profile = CustomProfile.GetProfile();
                    DataTable table = new DataTable();
                    DataSet ds1 = new DataSet();
                    ds1 = comsetup.GetDBConfigureDataByID(deptID, profile.DBConnection._constr);
                    if (ds1 != null)
                    {
                        gvDepartmentlist.DataSource = ds1;
                        gvDepartmentlist.DataBind();
                    }
                    else
                    {
                        ds1 = null;
                        gvDepartmentlist.DataSource = ds1;
                        gvDepartmentlist.DataBind();
                    }

                }
                catch (System.Exception ex)
                {
                    Login.Profile.ErrorHandling(ex, this, "Stock Config", "GetUserByID");
                }
            }
        }

        [WebMethod]
        public static int InsertRecord(string action, long CompanyId, string CompanyNm, long DeptId, string DeptNm, string SchemaNm, string DatabaseName, string ConnectionString, string Active, string wmsstorecode)
        {
            CustomProfile profile = CustomProfile.GetProfile();
            iCompanySetupClient comsetup = new iCompanySetupClient();
            //    CheckStockAvailableClient avail = new CheckStockAvailableClient();
            string DeptCode = comsetup.GetDepartmentCode(CompanyId, DeptId, profile.DBConnection._constr);
            long Id = 0;
            if (action == "Add")
            {
                comsetup.AddData(CompanyId, CompanyNm, DeptId, DeptNm, SchemaNm, DatabaseName, ConnectionString, DeptCode, Active, wmsstorecode, profile.Personal.UserID, profile.DBConnection._constr);
            }
            else
            {
                Id = Convert.ToInt64(HttpContext.Current.Session["XMLId"].ToString());
                comsetup.UpdateData(Id, CompanyId, CompanyNm, DeptId, DeptNm, SchemaNm, DatabaseName, ConnectionString, DeptCode, Active, wmsstorecode, profile.Personal.UserID, profile.DBConnection._constr);
            }
            return 1;
        }

        private void ClareAll()
        {
            txtconstring.Text = "";
            txtdatabse.Text = "";
            txtschema.Text = "";
            txtwmsstorecode.Text = "";
            fillCompany();
            FillDepartment();
        }

        //[WebMethod]
        //public static List<mTerritory> PMGetDepartmentList(long CompanyID)
        //{
        //    List<mTerritory> TerritoryList = new List<mTerritory>();
        //    try
        //    {
        //        UC_Territory uc_territory = new UC_Territory();
        //        TerritoryList = uc_territory.GetDepartmentList(CompanyID).ToList();
        //    }
        //    catch { }
        //    finally { }
        //    return TerritoryList;
        //}

        protected void gvDepartmentlist_Rebind(object sender, EventArgs e)
        {
            BindGV(long.Parse(hdndeptid.Value));
        }

        protected void gvDepartmentlist_Select(object sender, GridRecordEventArgs e)
        {
            CustomProfile profile = CustomProfile.GetProfile();
            iCompanySetupClient comsetup = new iCompanySetupClient();
            iProductMasterClient productClient = new iProductMasterClient();
            DataSet ds = new DataSet();
            try
            {
                hndRoleSate.Value = "Edit";
                Hashtable selectedrec = (Hashtable)gvDepartmentlist.SelectedRecords[0];
                hdnEditrecId.Value = selectedrec["ID"].ToString();
                HttpContext.Current.Session["XMLId"] = hdnEditrecId.Value;
                txtdatabse.Text = selectedrec["DatabaseName"].ToString();
                txtconstring.Text = selectedrec["ConnectionString"].ToString();
                txtschema.Text = selectedrec["SchemaNm"].ToString();
                txtwmsstorecode.Text = selectedrec["WmsStorecode"].ToString();
                if (selectedrec["Active"].ToString() == "Yes")
                {
                    btnradioyes.Checked = true;
                    btnradiono.Checked = false;
                }
                else
                {
                    btnradioyes.Checked = false;
                    btnradiono.Checked = true;
                }
                ddlcompany.SelectedIndex = ddlcompany.Items.IndexOf(ddlcompany.Items.FindByValue(selectedrec["CompanyId"].ToString()));
                if (ddlcompany.SelectedValue != "0" || ddlcompany.SelectedValue != "")
                {
                    UC_Territory uc_territory = new UC_Territory();
                    ddldepartment.DataSource = null;
                    ddldepartment.DataBind();
                    long CompanyID = Convert.ToInt64(ddlcompany.SelectedValue);
                    ds = productClient.GetDepartment(CompanyID, profile.DBConnection._constr);
                    // ddldepartment.DataSource = uc_territory.GetDepartmentList(Convert.ToInt64(ddlcompany.SelectedValue)).ToList();
                    ddldepartment.DataSource = ds;
                    ddldepartment.DataBind();
                    ddldepartment.SelectedIndex = ddldepartment.Items.IndexOf(ddldepartment.Items.FindByValue(selectedrec["DeptId"].ToString()));
                }
                btnAdd.Value = "Update";
                ddlcompany.Enabled = false;
            }
            catch (Exception ex)
            {
                Login.Profile.ErrorHandling(ex, "StockConfig.aspx.cs", "gvDepartmentlist_Select");
            }
            finally
            {
                comsetup.Close();
                productClient.Close();
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
                Login.Profile.ErrorHandling(ex, "StockConfig", "GetDepartment");
            }
            finally
            {
                productClient.Close();
            }
            return LocList;
        }

        protected void pageAddNew(Object sender, ToolbarService.iUCToolbarClient e)
        {
            ClareAll();
            ddlcompany.Enabled = true;
            btnAdd.Value = "Add";
            btnradioyes.Checked = true;
            btnradiono.Checked = false;
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
    }
}