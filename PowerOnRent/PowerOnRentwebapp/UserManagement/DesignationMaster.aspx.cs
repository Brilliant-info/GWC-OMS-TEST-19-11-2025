using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PowerOnRentwebapp.DepartmentService;
using Obout.Interface;
using System.Collections;
using PowerOnRentwebapp.Login;
using WebMsgBox;
namespace PowerOnRentwebapp.UserManagement
{
    public partial class DesignationMaster : System.Web.UI.Page
    {
        DepartmentService.iDepartmentMasterClient DepartmentClient = new DepartmentService.iDepartmentMasterClient();
        DesignationService.iDesignationMasterClient DesignationClient = new DesignationService.iDesignationMasterClient();
        PopupMessages.PopupMessage pop = new PopupMessages.PopupMessage();

        protected void Page_PreInit(Object sender, EventArgs e)
        {
            CustomProfile profile = CustomProfile.GetProfile();
            if (profile.Personal.Theme == null || profile.Personal.Theme == string.Empty) { Page.Theme = "Blue"; } else { Page.Theme = profile.Personal.Theme; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            CustomProfile profile = CustomProfile.GetProfile();
            UCFormHeader1.FormHeaderText = "Designation Master";
            if (!IsPostBack)
            {
                BinddlDepartment();
                BindGrid();
                hdnDesignationID.Value = null;
                if (profile.Personal.CompanyID == 14)
                {
                    Button btnExport = (Button)UCToolbar1.FindControl("btnExport");
                    btnExport.Visible = false;
                    Button btnImport = (Button)UCToolbar1.FindControl("btnImport");
                    btnImport.Visible = false;
                    Button btmMail = (Button)UCToolbar1.FindControl("btmMail");
                    btmMail.Visible = false;
                    Button btnPrint = (Button)UCToolbar1.FindControl("btnPrint");
                    btnPrint.Visible = false;
                }
            }
            this.UCToolbar1.ToolbarAccess("DesignationMaster");
            this.UCToolbar1.evClickAddNew += pageAddNew;
            this.UCToolbar1.evClickSave += pageSave;
            this.UCToolbar1.evClickClear += pageClear;
        }

        public void BindGrid()
        {
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                gvDesignationM.DataSource = DesignationClient.GetDesignationRecordToBind(profile.DBConnection._constr);
                gvDesignationM.DataBind();
            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "Designation Master", "BindGrid");
            }
            finally
            {
            }
        }

        public void BinddlDepartment()
        {
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                ddlDepartment.DataSource = DepartmentClient.GetDeparmentList(profile.DBConnection._constr);
                ddlDepartment.DataBind();
                ListItem lst = new ListItem();
                lst.Text = "-Select-";
                lst.Value = "0";
                ddlDepartment.Items.Insert(0, lst);
            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "Designation Master", "BinddlDepartment");
            }
            finally
            {
            }
        }

        public void clear()
        {
            try
            {
                txtDesignation.Text = "";
                txtSequence.Text = "";
                hdnDesignationID.Value = null;
                ddlDepartment.SelectedIndex = -1;
                rbtnYes.Checked = true;
                rbtnNo.Checked = false;
            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "Designation Master", "BinddlDepartment");
            }
            finally
            {
            }
        }

        protected void pageAddNew(Object sender, ToolbarService.iUCToolbarClient e)
        { clear(); }

        protected void pageSave(Object sender, ToolbarService.iUCToolbarClient e)
        {
            PowerOnRentwebapp.DesignationService.mDesignation ObjDesignation = new DesignationService.mDesignation();
            if (checkDuplicate() == "")
            {
                try
                {
                    CustomProfile profile = CustomProfile.GetProfile();
                    if (hdnDesignationID.Value == string.Empty)
                    {
                        ObjDesignation.Name = txtDesignation.Text.Trim();
                        ObjDesignation.DepartmentID = Convert.ToInt64(ddlDepartment.SelectedValue);
                        if (txtSequence.Text != string.Empty)
                        { ObjDesignation.Sequence = Convert.ToInt64(txtSequence.Text); }
                        else
                        { ObjDesignation.Sequence = 0; }
                        if (rbtnYes.Checked == true)
                        { ObjDesignation.Active = "Y"; }
                        else
                        { ObjDesignation.Active = "N"; }
                        ObjDesignation.CreatedBy = profile.Personal.UserID.ToString();
                        ObjDesignation.CreationDate = DateTime.Now;

                        ObjDesignation.CompanyID = profile.Personal.CompanyID; 
                        int result = DesignationClient.InsertmDesignation(ObjDesignation, profile.DBConnection._constr);
                        if (result == 1)
                        {
                            WebMsgBox.MsgBox.Show("Record saved successfully");      
                        }
                        BindGrid();
                        clear();
                    }
                    else
                    {
                        ObjDesignation = DesignationClient.GetDesignationListByID(Convert.ToInt32(hdnDesignationID.Value), profile.DBConnection._constr);
                        ObjDesignation.Name = txtDesignation.Text.Trim();
                        ObjDesignation.DepartmentID = Convert.ToInt64(ddlDepartment.SelectedValue);
                        if (txtSequence.Text != string.Empty)
                        { ObjDesignation.Sequence = Convert.ToInt64(txtSequence.Text); }
                        else
                        { ObjDesignation.Sequence = 0; }
                        if (rbtnYes.Checked == true)
                        { ObjDesignation.Active = "Y"; }
                        else
                        { ObjDesignation.Active = "N"; }
                        ObjDesignation.LastModifiedBy = profile.Personal.UserID.ToString();
                        ObjDesignation.LastModifiedDate = DateTime.Now;
                        int result = DesignationClient.updatemDesignation(ObjDesignation, profile.DBConnection._constr);
                        if (result == 1)
                        {
                            WebMsgBox.MsgBox.Show("Record updated successfully");
                        }
                        BindGrid();
                        clear();
                    }
                }
                catch (System.Exception ex)
                {
                    Login.Profile.ErrorHandling(ex, this, "Designation Master", "pageSave");
                }
                finally
                {
                }
            }
        }

        protected void pageClear(Object sender, ToolbarService.iUCToolbarClient e)
        { clear(); }

        public string checkDuplicate()
        {
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                string result = "";

                if (hdnDesignationID.Value == string.Empty)
                {
                    result = DesignationClient.checkDuplicateRecord(txtDesignation.Text.Trim(), Convert.ToInt32(ddlDepartment.SelectedValue), profile.DBConnection._constr);
                    if (result != string.Empty)
                    {
                        WebMsgBox.MsgBox.Show(result);
                        txtDesignation.Text = "";
                    }
                    txtSequence.Focus();
                }
                else
                {
                    result = DesignationClient.checkDuplicateRecordEdit(Convert.ToInt32(hdnDesignationID.Value), txtDesignation.Text.Trim(), Convert.ToInt32(ddlDepartment.SelectedValue), profile.DBConnection._constr);
                    if (result != string.Empty)
                    {
                        WebMsgBox.MsgBox.Show(result);
                        txtDesignation.Text = "";
                    }
                }
                return result;
            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "Designation Master", "checkDuplicate");
                string result = "";
                return result;
            }
            finally
            {
            }
        }

        protected void gvDesignationM_Select(object sender, Obout.Grid.GridRecordEventArgs e)
        {
            try
            {
                rbtnNo.Checked = false;
                rbtnYes.Checked = false;
                Hashtable selectedrec = (Hashtable)gvDesignationM.SelectedRecords[0];
                hdnDesignationID.Value = selectedrec["ID"].ToString();
                txtSequence.Text = selectedrec["Sequence"].ToString();
                ddlDepartment.SelectedIndex = ddlDepartment.Items.IndexOf(ddlDepartment.Items.FindByText(selectedrec["Department"].ToString()));
                txtDesignation.Text = selectedrec["Name"].ToString();
                if (selectedrec["Active"].ToString() == "No")
                { rbtnNo.Checked = true; }
                else
                { rbtnYes.Checked = true; }
                txtDesignation.Focus();
            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "Designation Master", "gvDesignationM_Select");
            }
            finally
            {
            }
        }
    }
}