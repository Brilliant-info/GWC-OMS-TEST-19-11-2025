using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using PowerOnRentwebapp.CompanySetupService;
using System.Web.Services;
using PowerOnRentwebapp.Login;
using System.Web.Security;
using WebMsgBox;
using Obout.Grid;
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

namespace PowerOnRentwebapp.PowerOnRent
{
    public partial class DedicatedDriver : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDropdown();
                BindDriverList();
            }

        }

        public void BindDropdown()
        {
            iCompanySetupClient comsetup = new iCompanySetupClient();
            CustomProfile profile = CustomProfile.GetProfile();
            try
            {
                DataSet ds = new DataSet();
                ds = comsetup.GetEcommerceCompany(profile.DBConnection._constr);
                ddlCompany1.DataSource = ds;
                ddlCompany1.DataBind();
                ListItem lstContact = new ListItem { Text = "-Select-", Value = "0" };
                ddlCompany1.Items.Insert(0, lstContact);
            }
            catch (Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "DedicatedDriver.aspx", "BindDropdown");
            }

        }


        [WebMethod]
        public static List<mTerritory> WMGetDept(int Cmpny)
        {
            iCompanySetupClient comsetup = new iCompanySetupClient();
            List<mTerritory> SiteLst = new List<mTerritory>();
            CustomProfile profile = CustomProfile.GetProfile();
            SiteLst = comsetup.GetDepartmentList2(Cmpny, profile.DBConnection._constr).ToList();
            return SiteLst;
        }

        [WebMethod]
        public static string WMSaveRequestHead(string State, string Driverid, string Customer, string Dept)
        {
            string result = "";
            CustomProfile profile = CustomProfile.GetProfile();
            iCompanySetupClient comsetup = new iCompanySetupClient();
            try
            {               

                if (State == "Edit")
                {
                    string ID = HttpContext.Current.Session["did"].ToString();
                    comsetup.ADDDedicatedDR(Convert.ToInt64(ID), State, Driverid, Customer, Dept, profile.Personal.UserID, profile.DBConnection._constr);
                    result = "2";
                }
                else
                {
                    string checkdulicate = "";
                    checkdulicate = comsetup.CHKDuplicateDedicateddr(State, Driverid, Customer, Dept, profile.DBConnection._constr);
                    if (checkdulicate == "Yes")
                    {
                        result = "1";
                    }
                    else
                    {
                        comsetup.ADDDedicatedDR(0, State, Driverid, Customer, Dept, profile.Personal.UserID, profile.DBConnection._constr);
                        result = "2";
                    }
                }
            }
            catch { result = "Some error occurred"; }
            finally { comsetup.Close(); }

            return result;
        }


        protected void gvContactPerson_Rebind(object sender, EventArgs e)
        {
            BindDriverList();
        }

        protected void BindDriverList()
        {
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                iCompanySetupClient comsetup = new iCompanySetupClient();
                DataSet ds = new DataSet();
                string filter = hdnFilterText.Value;
                if (filter == "" || filter == null) { filter = "0"; }
                ds = comsetup.GetDRList(filter, profile.DBConnection._constr);
                gvContactPerson.DataSource = ds;
                gvContactPerson.DataBind();
            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "DedicatedDriver.aspx", "BindDriverList");
            }

        }

        protected void imgBtnEdit1_Command(object sender, CommandEventArgs e)
        {
            CustomProfile profile = CustomProfile.GetProfile();
            iCompanySetupClient comsetup = new iCompanySetupClient();
            try
            {
                string id = e.CommandArgument.ToString();
                Session["did"] = id;               
                hdnstateNM.Value = "Edit";
                DataSet ds = new DataSet();
                ds = comsetup.GetDriverData(id,profile.DBConnection._constr);
                if(ds.Tables[0].Rows.Count >0)
                {
                    BindDropdown();
                    ddlCompany1.SelectedIndex = ddlCompany1.Items.IndexOf(ddlCompany1.Items.FindByValue(ds.Tables[0].Rows[0]["companyid"].ToString()));
                    hdnSelectedCompany.Value= ds.Tables[0].Rows[0]["companyid"].ToString();
                    BindDepartment(ds.Tables[0].Rows[0]["companyid"].ToString());
                    ddlSites.SelectedIndex = ddlSites.Items.IndexOf(ddlSites.Items.FindByValue(ds.Tables[0].Rows[0]["departmentid"].ToString()));
                    hdnsiteid.Value = ds.Tables[0].Rows[0]["departmentid"].ToString();
                    txtprojectype.Text = ds.Tables[0].Rows[0]["Drivernm"].ToString();
                    hdnSelectedRecID.Value= ds.Tables[0].Rows[0]["driverid"].ToString();
                    ddlCompany1.Enabled = false;
                    ddlSites.Enabled = false;
                }
            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "DedicatedDriver.aspx", "imgBtnEdit1_Command");
            }
        }

        public void BindDepartment(string Cmpny)
        {
            iCompanySetupClient comsetup = new iCompanySetupClient();
            CustomProfile profile = CustomProfile.GetProfile();
            try
            {                
                List<mTerritory> SiteLst = new List<mTerritory>();                
                SiteLst = comsetup.GetDepartmentList2(Convert.ToInt32(Cmpny), profile.DBConnection._constr).ToList();
                ddlSites.DataSource = SiteLst;
                ddlSites.DataBind();
                ListItem lstContact = new ListItem { Text = "-Select-", Value = "0" };
                ddlCompany1.Items.Insert(0, lstContact);
            }
            catch (Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "DedicatedDriver.aspx", "BindDepartment");
            }

        }


        
    }
}