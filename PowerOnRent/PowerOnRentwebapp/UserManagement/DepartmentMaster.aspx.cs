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
using PowerOnRentwebapp.ToolbarService;
using WebMsgBox;

namespace PowerOnRentwebapp.UserManagement
{
    public partial class DepartmentMaster : System.Web.UI.Page
    {
        DepartmentService.iDepartmentMasterClient DepartmentClient = new DepartmentService.iDepartmentMasterClient();
        //PopupMessages.PopupMessage pop = new PopupMessages.PopupMessage();

        protected void Page_PreInit(Object sender, EventArgs e)
        {
            CustomProfile profile = CustomProfile.GetProfile();
            if (profile.Personal.Theme == null || profile.Personal.Theme == string.Empty) { Page.Theme = "Blue"; } else { Page.Theme = profile.Personal.Theme; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            UCFormHeader1.FormHeaderText = "Department Master";
            if (!IsPostBack)
            {
                BindGrid();
                hdnDepartmentID.Value = null;
               
            }
            this.UCToolbar1.ToolbarAccess("DepartmentMaster");
            this.UCToolbar1.evClickAddNew += pageAddNew;
            this.UCToolbar1.evClickSave += pageSave;
            this.UCToolbar1.evClickClear += pageClear;
        }

        public void BindGrid()
        {
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                gvDepartmentM.DataSource = DepartmentClient.GetDepartmentRecordToBind(profile.DBConnection._constr);
                gvDepartmentM.DataBind();
            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "Department Master", "BindGrid");
            }
            finally
            {
            }
        }

        public void clear()
        {
            txtDepartment.Text = "";
            txtSequence.Text = "";
            hdnDepartmentID.Value = null;
            rbtnYes.Checked = true;
            rbtnNo.Checked = false;
        }

        protected void pageAddNew(Object sender, ToolbarService.iUCToolbarClient e)
        { clear(); }

        protected void pageSave(Object sender, ToolbarService.iUCToolbarClient e)
        {
            if (checkDuplicate() == "")
            {
                try
                {
                    CustomProfile profile = CustomProfile.GetProfile();
                    mDepartment ObjDepartment = new mDepartment();
                    if (hdnDepartmentID.Value == string.Empty)
                    {
                        ObjDepartment.Name = txtDepartment.Text;
                        if (txtSequence.Text != string.Empty)
                        { ObjDepartment.Sequence = Convert.ToInt64(txtSequence.Text); }
                        else
                        { ObjDepartment.Sequence = 0; }
                        if (rbtnYes.Checked == true)
                        { ObjDepartment.Active = "Y"; }
                        else
                        { ObjDepartment.Active = "N"; }
                        ObjDepartment.CreatedBy = profile.Personal.UserID.ToString();
                        ObjDepartment.CreationDate = DateTime.Now;

                        ObjDepartment.CompanyID = profile.Personal.CompanyID;
                        int result = DepartmentClient.InsertmDepartment(ObjDepartment, profile.DBConnection._constr);
                        if (result == 1)
                        {
                            WebMsgBox.MsgBox.Show("Record saved successfully");      
                        }
                        BindGrid();
                        clear();
                    }
                    else
                    {
                        ObjDepartment = DepartmentClient.GetDepartmentListByID(Convert.ToInt32(hdnDepartmentID.Value), profile.DBConnection._constr);
                        ObjDepartment.Name = txtDepartment.Text;
                        if (txtSequence.Text != string.Empty)
                        { ObjDepartment.Sequence = Convert.ToInt64(txtSequence.Text); }
                        else
                        { ObjDepartment.Sequence = 0; }
                        if (rbtnYes.Checked == true)
                        { ObjDepartment.Active = "Y"; }
                        else
                        { ObjDepartment.Active = "N"; }
                        ObjDepartment.LastModifiedBy = profile.Personal.UserID.ToString();
                        ObjDepartment.LastModifiedDate = DateTime.Now;
                        int result = DepartmentClient.updatemDepartment(ObjDepartment, profile.DBConnection._constr);
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
                    Login.Profile.ErrorHandling(ex, this, "Department Master", "pageSave");
                }
                finally
                {
                }
            }
        }

        public string checkDuplicate()
        {
            try
            {
                string result = "";
                CustomProfile profile = CustomProfile.GetProfile();
                if (hdnDepartmentID.Value == string.Empty)
                {
                    
                    result = DepartmentClient.checkDuplicateRecord(txtDepartment.Text.Trim(), profile.DBConnection._constr);
                    if (result != string.Empty)
                    {
                        WebMsgBox.MsgBox.Show(result);
                        txtDepartment.Text = "";
                    }
                    txtSequence.Focus();
                }
                else
                {
                    result = DepartmentClient.checkDuplicateRecordEdit(Convert.ToInt32(hdnDepartmentID.Value), txtDepartment.Text.Trim(), profile.DBConnection._constr);
                    if (result != string.Empty)
                    {
                        WebMsgBox.MsgBox.Show(result);
                        txtDepartment.Text = "";
                    }
                }
                return result;
            }
            catch (System.Exception ex)
            {
                string result = "";
                Login.Profile.ErrorHandling(ex, this, "Department Master", "checkDuplicate");
                return result;
            }
            finally
            {
            }

        }

        protected void pageClear(Object sender, ToolbarService.iUCToolbarClient e)
        { clear(); }

        protected void gvDepartmentM_Select(object sender, Obout.Grid.GridRecordEventArgs e)
        {
            try
            {
                rbtnNo.Checked = false;
                rbtnYes.Checked = false;
                Hashtable selectedrec = (Hashtable)gvDepartmentM.SelectedRecords[0];
                hdnDepartmentID.Value = selectedrec["ID"].ToString();
                txtSequence.Text = selectedrec["Sequence"].ToString();
                txtDepartment.Text = selectedrec["Name"].ToString();
                if (selectedrec["Active"].ToString() == "No")
                { rbtnNo.Checked = true; }
                else
                { rbtnYes.Checked = true; }
                txtDepartment.Focus();
            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "Department Master", "gvDepartmentM_Select");
            }
            finally
            {
            }
        }


    }
}