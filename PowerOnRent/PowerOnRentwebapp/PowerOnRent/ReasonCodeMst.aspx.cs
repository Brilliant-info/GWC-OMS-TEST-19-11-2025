using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using PowerOnRentwebapp.DesignationService;
using PowerOnRentwebapp.RoleMasterService;
//using PowerOnRentwebapp.UserCreationService;
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
//using PowerOnRentwebapp.PORServiceUCCommonFilter;

namespace PowerOnRentwebapp.PowerOnRent
{
    public partial class ReasonCodeMst : System.Web.UI.Page
    {
        ResourceManager rm;
        CultureInfo ci;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Lang"] == "")
                {
                    Session["Lang"] = Request.UserLanguages[0];
                }
                if (string.IsNullOrEmpty((string)Session["Lang"]))
                {
                    Session["Lang"] = Request.UserLanguages[0];
                }
                loadstring();

                UCFormHeader1.FormHeaderText = "Reason Code";                       
                ActiveTab("");
                BindDropdown();
            }

            this.UCToolbar1.evClickSave += pageSave;
            this.UCToolbar1.evClickAddNew += pageAddNew;
            this.UCToolbar1.evClickClear += pageClear;

            //ListItem lstdept = new ListItem { Text = "-Select-", Value = "0" };
            //ddlSites.Items.Insert(0, lstdept);

            //ListItem lstdeptnew = new ListItem { Text = "-Select-", Value = "0" };
            //ddldepartment.Items.Insert(0, lstdeptnew);
        }

        private void loadstring()
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(Session["Lang"].ToString());
                rm = new ResourceManager("PowerOnRentwebapp.App_GlobalResources.Lang", Assembly.GetExecutingAssembly());
                ci = Thread.CurrentThread.CurrentCulture;

                lblcustsearch.Text = rm.GetString("Customer", ci);
                lbldeptsearch.Text = rm.GetString("Department", ci);
                lblheader.Text = rm.GetString("ReasonCodeList", ci);
                TabPanelReasonList.HeaderText = rm.GetString("ReasonCodeList", ci);
                 lblCustomer.Text = rm.GetString("Customer", ci);
                lbldept.Text = rm.GetString("Department", ci);
                lbltype.Text = rm.GetString("Type", ci);
                lblreason.Text = rm.GetString("ReasonCode", ci);
                lblreasonarb.Text = rm.GetString("ReasoncodeArabic", ci);
                lbldescription.Text = rm.GetString("Description", ci); 
                    lblactive.Text = rm.GetString("Active", ci);
                lbldefault.Text = rm.GetString("Default", ci);
                TabPanelReasonData.HeaderText = rm.GetString("ReasonCode", ci);              
            }
            catch (Exception ex)
            {
                Login.Profile.ErrorHandling(ex, "ReasonCodeMst.aspx", "Loadstring");
            }
        }



        private void BindDropdown()
        {
            iCompanySetupClient comsetup = new iCompanySetupClient();
            CustomProfile profile = CustomProfile.GetProfile();
            long UID = profile.Personal.UserID;
            List<mCompany> CompanyLst = new List<mCompany>();
            CompanyLst = comsetup.GetUserCompanyNameNEW(UID, profile.DBConnection._constr).ToList();
            ddlCompany.DataSource = CompanyLst;
            ddlCompany.DataBind();
            ListItem lstContact = new ListItem { Text = "-Select-", Value = "0" };
            ddlCompany.Items.Insert(0, lstContact);

            ddlcompanynew.DataSource = CompanyLst;
            ddlcompanynew.DataBind();
            ListItem lstCompany = new ListItem { Text = "-Select-", Value = "0" };
            ddlcompanynew.Items.Insert(0, lstCompany);


        }

        private void BindReasonGV(string deptid)
        {
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                iCompanySetupClient comsetup = new iCompanySetupClient();
                DataSet dsbind = new DataSet();
                dsbind = comsetup.BindSearchDataIntoGrid( Convert.ToString(deptid), profile.DBConnection._constr);
                if (dsbind.Tables[0].Rows.Count > 0)
                {
                    gvReasoncodelist.DataSource = dsbind;
                    gvReasoncodelist.DataBind();
                }
                else
                {
                    dsbind = null;
                    gvReasoncodelist.DataSource = dsbind;
                    gvReasoncodelist.DataBind();
                }                
            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "Reason Code", "BindReasonGV");
            }
        }



        [WebMethod]
        public static List<mTerritory> WMGetDept(int Cmpny)
        {
            iCompanySetupClient comsetup = new iCompanySetupClient();
            List<mTerritory> SiteLst = new List<mTerritory>();         
            CustomProfile profile = CustomProfile.GetProfile();
            SiteLst = comsetup.GetDepartmentList1(Cmpny, profile.DBConnection._constr).ToList();
            return SiteLst;
        }

        [WebMethod]
        public static List<mTerritory>WMGetDeptnew(int Cmpny)
        {
            iCompanySetupClient comsetup = new iCompanySetupClient();
            List<mTerritory> SiteLst = new List<mTerritory>();
            CustomProfile profile = CustomProfile.GetProfile();
            SiteLst = comsetup.GetDepartmentList1(Cmpny, profile.DBConnection._constr).ToList();
            return SiteLst;
        }

        #region Toolbar Code
        protected void pageAddNew(Object sender, ToolbarService.iUCToolbarClient e)
        {
            TabPanelReasonList.Visible = true;
            TabContainerReasoncodeGV.ActiveTabIndex = 1;
            TabPanelReasonData.Visible = true;
            hndRoleSate.Value = "Insert";
            ClearAll();
           
        }

        private void ClearAll()
        {
            ddlcompanynew.SelectedValue = "0";
          //  ddldepartment.SelectedValue = "0";
         
            ddltype.SelectedValue = "0";
            txtreason.Text = "";
            txtdescription.Text = "";
            rbtactYes.Checked = true;
            rbtactYes.Checked = true;
            txtreasoninarb.Text = "";
            DataSet ds = new DataSet();
            ds = null;
            //ddldepartment.DataSource = ds;
            //ddldepartment.DataBind();
            gvReasoncodelist.DataSource = ds;
            gvReasoncodelist.DataBind();


        }

        protected void pageClear(Object sender, ToolbarService.iUCToolbarClient e)
        {
            ClearAll();
        }

        protected void pageSave(Object sender, ToolbarService.iUCToolbarClient e)
        {
            iCompanySetupClient comsetup = new iCompanySetupClient();
            CustomProfile profile = CustomProfile.GetProfile();
            string comid, deptid, typeid, rcode, rcodedes, defaultvalue = "", active = "", reasonarb = "";
            string state = hndRoleSate.Value.ToString();
            try
            {
                comid = ddlcompanynew.SelectedValue;
                deptid = hdnSelectedDeptvalue.Value;
                typeid = ddltype.SelectedValue;
                rcode = txtreason.Text;
                rcodedes = txtdescription.Text;
                reasonarb = txtreasoninarb.Text;
                if (rbtnNo.Checked == true)
                {
                    defaultvalue = "No";
                }
                else
                {
                    defaultvalue = "Yes";
                }

                if (rbtactYes.Checked == true)
                {
                    active = "Yes";
                }
                else
                {
                    active = "No";
                }               

                if (state == "Edit")
                {
                    deptid = ddldepartment.SelectedValue;
                    long rid = 0;
                    rid = Convert.ToInt64(Session["ReasonId"]);
                    if (defaultvalue == "Yes")
                    {
                        comsetup.UpdateRecord(comid, deptid, typeid, profile.DBConnection._constr);
                        comsetup.UpdateReasonCodeData(rid, comid, deptid, typeid, rcode, rcodedes, defaultvalue, active, reasonarb, profile.DBConnection._constr);
                    }
                    else
                    {
                        comsetup.UpdateReasonCodeData(rid, comid, deptid, typeid, rcode, rcodedes, defaultvalue, active, reasonarb, profile.DBConnection._constr);
                    }
                    MsgBox.Show("Record update successfully");                    
                    ClearAll();
                    TabPanelReasonList.Visible = true;
                    TabContainerReasoncodeGV.ActiveTabIndex = 0;
                    TabPanelReasonData.Visible = false;
                    ddlCompany.SelectedValue = comid;
                    UC_Territory uc_territory = new UC_Territory();
                    ddlSites.DataSource = uc_territory.GetDepartmentList(Convert.ToInt64(comid)).ToList();
                    ddlSites.DataBind();
                    ddlSites.SelectedIndex = ddlSites.Items.IndexOf(ddlSites.Items.FindByValue(deptid.ToString()));
                    BindReasonGV(deptid);
                    
                }
                else
                {
                    string IsDuplicate = "";
                    IsDuplicate = ChecIskDuplicate(comid, deptid, typeid, rcode);
                    if (IsDuplicate != "Yes")
                    {
                        if (defaultvalue == "Yes")
                        {
                            comsetup.UpdateRecord(comid, deptid, typeid, profile.DBConnection._constr);
                            comsetup.InsertReasonCodeData(comid, deptid, typeid, rcode, rcodedes, defaultvalue, active, reasonarb, profile.DBConnection._constr);
                        }
                        else
                        {
                            comsetup.InsertReasonCodeData(comid, deptid, typeid, rcode, rcodedes, defaultvalue, active, reasonarb, profile.DBConnection._constr);
                        }
                        MsgBox.Show("Record save successfully");                       
                        ClearAll();
                        TabPanelReasonList.Visible = true;
                        TabContainerReasoncodeGV.ActiveTabIndex = 0;
                        TabPanelReasonData.Visible = false;
                        ddlCompany.SelectedValue = comid;
                        UC_Territory uc_territory = new UC_Territory();
                        ddlSites.DataSource = uc_territory.GetDepartmentList(Convert.ToInt64(comid)).ToList();
                        ddlSites.DataBind();
                        ddlSites.SelectedIndex = ddlSites.Items.IndexOf(ddlSites.Items.FindByValue(deptid.ToString()));
                        BindReasonGV(deptid);
                        
                    }
                    else
                    {
                        MsgBox.Show("Reason code alrady available..");
                    }
                }
            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "Reason code", "pageSave");
            }
            finally
            {
                comsetup.Close();
            }
        }

        private string ChecIskDuplicate(string comid, string deptid, string typeid, string rcode)
        {
            iCompanySetupClient comsetup = new iCompanySetupClient();
            CustomProfile profile = CustomProfile.GetProfile();
            string result = "";
            result = comsetup.ResoncodeCheckDuplicate(comid, deptid, typeid, rcode, profile.DBConnection._constr);
            return result;
        }

        [WebMethod]

        public static string WMInsertDefalutvalue(string compid, string deptid, string reasoncode, string rdescription, string typeid, string isactive, string deafultvalue, string id, string reasonarb)
        {
            string result = "";
            iCompanySetupClient comsetup = new iCompanySetupClient();
            CustomProfile profile = CustomProfile.GetProfile();
            if (id == "" || id == null)
            {
                string getid = comsetup.GetReasoncodeId(compid, deptid, reasoncode, typeid, rdescription, isactive, deafultvalue, profile.DBConnection._constr);
                comsetup.UpdateReasonCodeData(Convert.ToInt64(getid), compid, deptid, typeid, reasoncode, rdescription, "No", isactive, reasonarb, profile.DBConnection._constr);

                comsetup.InsertReasonCodeData(compid, deptid, typeid, reasoncode, rdescription, deafultvalue, isactive, reasonarb, profile.DBConnection._constr);
                result = "Insert";

            }
            else
            {
                string getid = comsetup.GetReasoncodeId(compid, deptid, reasoncode, typeid, rdescription, isactive, deafultvalue, profile.DBConnection._constr);
                comsetup.UpdateReasonCodeDataUsingId(Convert.ToInt64(getid), "No", profile.DBConnection._constr);

                comsetup.UpdateReasonCodeData(Convert.ToInt64(id), compid, deptid, typeid, reasoncode, rdescription, deafultvalue, isactive, reasonarb, profile.DBConnection._constr);
                result = "Update";
            }
            return result;
        }
        protected void ActiveTab(string state)
        {

            TabPanelReasonList.Visible = true;
            TabContainerReasoncodeGV.ActiveTabIndex = 0;
            TabPanelReasonData.Visible = false;

        }
        #endregion


        protected void gvReasoncodelist_Rebind(object sender, EventArgs e)
        {
            CustomProfile profile = CustomProfile.GetProfile();
            string deptid = hdnSelectedDept.Value;
            BindReasonGV(deptid);
        }

        protected void gvReasoncodelist_Select(object sender, Obout.Grid.GridRecordEventArgs e)
        {
            CustomProfile profile = CustomProfile.GetProfile();
            iCompanySetupClient comsetup = new iCompanySetupClient();
            hndRoleSate.Value = "Edit";
            Hashtable selectedrec = (Hashtable)gvReasoncodelist.SelectedRecords[0];
            Session["ReasonId"] = selectedrec["Id"].ToString();
            ddlcompanynew.SelectedIndex = ddlcompanynew.Items.IndexOf(ddlcompanynew.Items.FindByValue(selectedrec["companyId"].ToString()));
            if (ddlcompanynew.SelectedValue != "0" || ddlcompanynew.SelectedValue != "")
            {
                UC_Territory uc_territory = new UC_Territory();
                ddldepartment.DataSource = null;
                ddldepartment.DataBind();
                ddldepartment.DataSource = uc_territory.GetDepartmentList(Convert.ToInt64(ddlcompanynew.SelectedValue)).ToList();
                ddldepartment.DataBind();
                ddldepartment.SelectedIndex = ddldepartment.Items.IndexOf(ddldepartment.Items.FindByValue(selectedrec["DeptId"].ToString()));
            }
            ddltype.SelectedIndex = ddltype.Items.IndexOf(ddltype.Items.FindByValue(selectedrec["Type"].ToString()));
            txtreason.Text = selectedrec["ReasonCode"].ToString();
            txtdescription.Text = selectedrec["ReasonDetails"].ToString();
            txtreasoninarb.Text= selectedrec["reasoncodeArb"].ToString();
            if (selectedrec["Active"].ToString() == "Yes")
            {
                rbtactYes.Checked = true; rbtactNo.Checked = false;
            }
            else
            {
                rbtactNo.Checked = true; rbtactYes.Checked = false;

            }

            if (selectedrec["DefaultValue"].ToString() == "Yes")
            {
                rbtnYes.Checked = true; rbtnNo.Checked = false;
            }
            else
            {
                rbtnNo.Checked = true; rbtnYes.Checked = false;
            }

            TabPanelReasonList.Visible = true;
            TabContainerReasoncodeGV.ActiveTabIndex = 1;
            TabPanelReasonData.Visible = true;

        }

        protected void btnserach_Click(object sender, EventArgs e)
        {
           // BindReasonGV();
        }
    }
}