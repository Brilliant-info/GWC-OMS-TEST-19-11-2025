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
    public partial class UserConfig : System.Web.UI.Page
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


                UCFormHeader1.FormHeaderText = "Direct Import User Config";
                ActiveTab("");
                BindDropdown();
                BindUserGrid();

            }

            this.UCToolbar1.evClickSave += pageSave;
            this.UCToolbar1.evClickAddNew += pageAddNew;
            this.UCToolbar1.evClickClear += pageClear;

            //ListItem lstdept = new ListItem { Text = "-Select-", Value = "0" };
            //ddlSites.Items.Insert(0, lstdept);

            //ListItem lstdeptnew = new ListItem { Text = "-Select-", Value = "0" };
            //ddldepartment.Items.Insert(0, lstdeptnew);
        }





        private void BindDropdown()
        {
            iCompanySetupClient comsetup = new iCompanySetupClient();
            CustomProfile profile = CustomProfile.GetProfile();
            long UID = profile.Personal.UserID;
            //List<mCompany> CompanyLst = new List<mCompany>();
            DataSet ds = new DataSet();
            ds = comsetup.GetCompanyListNEw(profile.DBConnection._constr);
           // CompanyLst = comsetup.GetUserCompanyNameNEW(UID, profile.DBConnection._constr).ToList();
            ddlCompany.DataSource = ds;
            ddlCompany.DataBind();
            ListItem lstContact = new ListItem { Text = "-Select-", Value = "0" };
            ddlCompany.Items.Insert(0, lstContact);

        }



        private void BindUserGrid()
        {
            try
            {
                iCompanySetupClient comsetup = new iCompanySetupClient();               
                CustomProfile profile = CustomProfile.GetProfile();
                DataSet ds = new DataSet();
                ds = comsetup.GetUserDetails(profile.DBConnection._constr);
                gvUserconfig.DataSource = ds;
                gvUserconfig.DataBind();
            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "UserConfig.aspx", "BindUserGrid");
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
        public static List<mTerritory> WMGetDeptnew(int Cmpny)
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
            //  TabPanelReasonData.Visible = true;
            hndRoleSate.Value = "Insert";
            ddlCompany.SelectedValue = "0";
            ddlSites.SelectedValue = "0";
            txtContact1.Text = "";
            hdnSelectedCompany.Value = "0";
            hdnSelectedDept.Value = "0";
            hdnidno.Value = "0";


        }



        protected void pageClear(Object sender, ToolbarService.iUCToolbarClient e)
        {
            ddlCompany.SelectedValue = "0";
            ddlSites.SelectedValue = "0";
            txtContact1.Text = "";
            hdnSelectedCompany.Value = "0";
            hdnSelectedDept.Value = "0";
            hdnidno.Value = "0";
        }
        
        protected void pageSave(Object sender, ToolbarService.iUCToolbarClient e)
        {
            iCompanySetupClient comsetup = new iCompanySetupClient();
            CustomProfile profile = CustomProfile.GetProfile();
            try
            {
                if(hdnSelectedCompany.Value=="0" || hdnSelectedCompany.Value == null || hdnSelectedCompany.Value =="")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert('Select Customer.')", "", true);
                
                    
                }
                else if (hdnSelectedDept.Value == "0" || hdnSelectedDept.Value == null || hdnSelectedDept.Value == "")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert('Select Customer.')", "", true);                   
                }
                else if (hdnidno.Value == "0" || hdnidno.Value == null || hdnidno.Value == "")
                {
                    MsgBox.Show("Select Users.");
                }
                else
                {
                    string [] id = hdnidno.Value.Split(',');
                    for(int i = 0; i <= id.Count() - 1; i++)
                    {
                        comsetup.SaveUserDetails(hdnSelectedCompany.Value, hdnSelectedDept.Value, id[i].ToString(),profile.Personal.UserID, profile.DBConnection._constr);
                    }
                    MsgBox.Show("Record save successfully");
                    ClearAll();
                }

                BindUserGrid();
                TabPanelReasonList.Visible = true;
                TabContainerReasoncodeGV.ActiveTabIndex = 0;
               
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

        private void ClearAll()
        {
            throw new NotImplementedException();
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
            // TabPanelReasonData.Visible = false;

        }
        #endregion


        protected void gvReasoncodelist_Rebind(object sender, EventArgs e)
        {
            CustomProfile profile = CustomProfile.GetProfile();
            string deptid = hdnSelectedDept.Value;
            // BindReasonGV(deptid);
        }


        protected void btnserach_Click(object sender, EventArgs e)
        {
            // BindReasonGV();
        }

      
        
        [WebMethod]

        public static string SaveUserConfig(string  compid,string deptid,string uid)
        {
            string result = "";
            try
            {
                iCompanySetupClient comsetup = new iCompanySetupClient();
                CustomProfile profile = CustomProfile.GetProfile();
                string[] id = uid.Split(',');
                for (int i = 0; i <= id.Count() - 1; i++)
                {
                    comsetup.SaveUserDetails(compid, deptid, id[i].ToString(), profile.Personal.UserID, profile.DBConnection._constr);
                }
                result = "0";
            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, "Reason code", "SaveUserConfig");
            }
            return result;
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {

        }

        protected void gvUserconfig_Rebind(object sender, EventArgs e)
        {
            BindUserGrid();
        }

        protected void gvUserconfig_Select(object sender, GridRecordEventArgs e)
        {
           
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                iCompanySetupClient comsetup = new iCompanySetupClient();
                Hashtable selectedrec = (Hashtable)gvUserconfig.SelectedRecords[0];
                string ID= selectedrec["Id"].ToString();
                comsetup.DeleteUserConfig(ID, profile.Personal.UserID, profile.DBConnection._constr);
                BindUserGrid();
            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, "Reason code", "gvUserconfig_Select");
            }

        }
    }
}