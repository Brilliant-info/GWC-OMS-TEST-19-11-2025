using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PowerOnRentwebapp.ProductCategoryService;
using Obout.Interface;
using System.Collections;
using PowerOnRentwebapp.Login;
using WebMsgBox;

namespace PowerOnRentwebapp.Product
{
    public partial class ProductCategoryMaster : System.Web.UI.Page
    {
        
        ProductCategoryService.iProductCategoryMasterClient ProductCategoryClient = new ProductCategoryService.iProductCategoryMasterClient();
        //PopupMessages.PopupMessage pop = new PopupMessages.PopupMessage();

        protected void Page_PreInit(Object sender, EventArgs e)
        {
            CustomProfile profile = CustomProfile.GetProfile();
            if (profile.Personal.Theme == null || profile.Personal.Theme == string.Empty) { Page.Theme = "Blue"; } else { Page.Theme = profile.Personal.Theme; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            UCFormHeader1.FormHeaderText = "Product Category Master";
            if (!IsPostBack)
            { 
                BindGrid();
                hdnPrdCategoryID.Value = null;
            }
            this.UCToolbar1.ToolbarAccess("ProductCategoryMaster");
            this.UCToolbar1.evClickAddNew += pageAddNew;
            this.UCToolbar1.evClickSave += pageSave;
            this.UCToolbar1.evClickClear += pageClear; 
        }

        public void BindGrid()
        {
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                gvPrdCat.DataSource = ProductCategoryClient.GetPrdCategoryRecordToBindGrid(profile.DBConnection._constr);
                gvPrdCat.DataBind();
            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "Product Category Master", "BindGrid");
            }
            finally
            {
            }
        }

        public void clear()
        {
            txtPrdCategory.Text = "";
            txtSequence.Text = "";
            hdnPrdCategoryID.Value = null;
            txtPrdCategory.Focus();  
            rbtnYes.Checked = true;
            rbtnNo.Checked = false;
            
        }

        protected void pageAddNew(Object sender, ToolbarService.iUCToolbarClient e)
        { clear();}

        protected void pageSave(Object sender, ToolbarService.iUCToolbarClient e)
        {
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                if (checkDuplicate() == "")
                {
                    mProductCategory ObjPrdCategory = new mProductCategory();
                    if (hdnPrdCategoryID.Value == string.Empty)
                    {
                        ObjPrdCategory.Name = txtPrdCategory.Text.Trim();
                        if (txtSequence.Text != string.Empty)
                        { ObjPrdCategory.Sequence = Convert.ToInt64(txtSequence.Text); }
                        else
                        { ObjPrdCategory.Sequence = 0; }
                        if (rbtnYes.Checked == true)
                        { ObjPrdCategory.Active = "Y"; }
                        else
                        { ObjPrdCategory.Active = "N"; }
                        ObjPrdCategory.CreatedBy = profile.Personal.UserID.ToString();
                        ObjPrdCategory.CreationDate = DateTime.Now;

                        ObjPrdCategory.CompanyID = profile.Personal.CompanyID;
                        int result = ProductCategoryClient.InsertmProductCategory(ObjPrdCategory, profile.DBConnection._constr);
                        if (result == 1)
                        {
                            WebMsgBox.MsgBox.Show("Record saved successfully");                           
                        }
                        BindGrid();
                        clear();
                    }

                    else
                    {
                        ObjPrdCategory = ProductCategoryClient.GetProductCategoryListByID(Convert.ToInt32(hdnPrdCategoryID.Value), profile.DBConnection._constr);
                        ObjPrdCategory.Name = txtPrdCategory.Text.Trim();
                        if (txtSequence.Text != string.Empty)
                        { ObjPrdCategory.Sequence = Convert.ToInt64(txtSequence.Text); }
                        else
                        { ObjPrdCategory.Sequence = 0; }
                        if (rbtnYes.Checked == true)
                        { ObjPrdCategory.Active = "Y"; }
                        else
                        { ObjPrdCategory.Active = "N"; }
                        ObjPrdCategory.LastModifiedBy = profile.Personal.UserID.ToString();
                        ObjPrdCategory.LastModifiedDate = DateTime.Now;
                        int result = ProductCategoryClient.updatemProductCategory(ObjPrdCategory, profile.DBConnection._constr);
                        if (result == 1)
                        {
                            WebMsgBox.MsgBox.Show("Record updated successfully");                              
                        }
                        BindGrid();
                        clear();
                    }
                }
            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "Product Category Master", "pageSave");
            }
            finally
            {
            }
        }

        public string checkDuplicate()
        {
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                string result = "";

                if (hdnPrdCategoryID.Value == string.Empty)
                {
                    result = ProductCategoryClient.checkDuplicateRecord(txtPrdCategory.Text.Trim(), profile.DBConnection._constr);
                    if (result != string.Empty)
                    {
                        WebMsgBox.MsgBox.Show(result);   
                        txtPrdCategory.Text = "";
                    }
                    txtSequence.Focus();
                }
                else
                {
                    result = ProductCategoryClient.checkDuplicateRecordEdit(txtPrdCategory.Text.Trim(), Convert.ToInt32(hdnPrdCategoryID.Value), profile.DBConnection._constr);
                    if (result != string.Empty)
                    {
                        WebMsgBox.MsgBox.Show(result);   
                        txtPrdCategory.Text = "";
                    }
                }
                return result;
            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "Product Category Master", "checkDuplicate");
                string result = "";
                return result;
            }
            finally
            {
            }
        }

        protected void pageClear(Object sender, ToolbarService.iUCToolbarClient e)
        { clear();}

        protected void gvPrdCat_Select(object sender, Obout.Grid.GridRecordEventArgs e)
        {
            try
            {
                rbtnNo.Checked = false;
                rbtnYes.Checked = false;
                Hashtable selectedrec = (Hashtable)gvPrdCat.SelectedRecords[0];
                hdnPrdCategoryID.Value = selectedrec["ID"].ToString();
                txtSequence.Text = selectedrec["Sequence"].ToString();
                txtPrdCategory.Text = selectedrec["Name"].ToString();
                if (selectedrec["Active"].ToString() == "No")
                { rbtnNo.Checked = true; }
                else
                { rbtnYes.Checked = true; }
            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "Product Category Master", "gvPrdCat_Select");
               
            }
            finally
            {
            }
        }      
    }
}