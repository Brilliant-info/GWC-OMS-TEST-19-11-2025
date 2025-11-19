using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PowerOnRentwebapp.ProductSubCategoryService;
using Obout.Interface;
using System.Collections;
using PowerOnRentwebapp.Login;
using WebMsgBox;
namespace PowerOnRentwebapp.Product
{
    public partial class ProductSubCategoryMaster : System.Web.UI.Page
    {
        
        ProductCategoryService.iProductCategoryMasterClient ProductCategoryClient = new ProductCategoryService.iProductCategoryMasterClient();
        ProductSubCategoryService.iProductSubCategoryMasterClient ProductSubCategoryClient = new ProductSubCategoryService.iProductSubCategoryMasterClient();
        //ProductCategoryService.connectiondetails profile.DBConnection._constr1 = new ProductCategoryService.connectiondetails();
        
        protected void Page_PreInit(Object sender, EventArgs e)
        {
            CustomProfile profile = CustomProfile.GetProfile();
            if (profile.Personal.Theme == null || profile.Personal.Theme == string.Empty) { Page.Theme = "Blue"; } else { Page.Theme = profile.Personal.Theme; }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            
            //profile.DBConnection._constr1.DataBaseName = Profile.DataBase;
            //profile.DBConnection._constr1.DataSource = Profile.DataSource;
            //profile.DBConnection._constr1.DBPassword = Profile.DBPassword;
            UCFormHeader1.FormHeaderText = "Product Sub-Category Master";
            if (!IsPostBack)
            {
                BinddlProductCategory();
                BindGrid();
                hdnPrdSubCategoryID.Value = null;
            }
            this.UCToolbar1.ToolbarAccess("ProductSubCategoryMaster");
            this.UCToolbar1.evClickAddNew += pageAddNew;
            this.UCToolbar1.evClickSave += pageSave;
            this.UCToolbar1.evClickClear += pageClear;
        }

        public void BindGrid()
        {
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                gvPrdSubCategoryM.DataSource = ProductSubCategoryClient.GetPrdSubCategoryRecordToBind(profile.DBConnection._constr);
                gvPrdSubCategoryM.DataBind();
            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "Product Sub Category Master", "BindGrid");
            }
            finally
            {
            }
        }

        public void BinddlProductCategory()
        {
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                ddlPrdCategory.DataSource = ProductCategoryClient.GetProductCategoryList(profile.DBConnection._constr);
                ddlPrdCategory.DataBind();
                ListItem lst = new ListItem();
                lst.Text = "-Select-";
                lst.Value = "0";
                ddlPrdCategory.Items.Insert(0, lst);
            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "Product Sub Category Master", "BinddlProductCategory");
            }
            finally
            {
            }
        }

        public void clear()
        {
            txtPrdSubCategory.Text = "";
            txtSequence.Text = "";
            hdnPrdSubCategoryID.Value = null;
            ddlPrdCategory.SelectedIndex = -1;
            rbtnYes.Checked = true;
            rbtnNo.Checked = false;
        }

        protected void pageAddNew(Object sender, ToolbarService.iUCToolbarClient e)
        { clear(); }

        protected void pageSave(Object sender, ToolbarService.iUCToolbarClient e)
        {
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                if (checkDuplicate() == "")
                {
                    PowerOnRentwebapp.ProductSubCategoryService.mProductSubCategory ObjPrdSubCategory = new ProductSubCategoryService.mProductSubCategory();
                    if (hdnPrdSubCategoryID.Value == string.Empty)
                    {
                        ObjPrdSubCategory.Name = txtPrdSubCategory.Text;
                        if (Convert.ToInt32(ddlPrdCategory.SelectedValue) == 0)
                        { ObjPrdSubCategory.ProductCategoryID = 0; }
                        else { ObjPrdSubCategory.ProductCategoryID = Convert.ToInt64(ddlPrdCategory.SelectedValue); }
                        if (txtSequence.Text != string.Empty) { ObjPrdSubCategory.Sequence = Convert.ToInt64(txtSequence.Text); }
                        else { ObjPrdSubCategory.Sequence = 0; }
                        if (rbtnYes.Checked == true) { ObjPrdSubCategory.Active = "Y"; }
                        else { ObjPrdSubCategory.Active = "N"; }
                        ObjPrdSubCategory.CreatedBy = profile.Personal.UserID.ToString();
                        ObjPrdSubCategory.CreationDate = DateTime.Now;

                        ObjPrdSubCategory.Companyid = profile.Personal.CompanyID;
                        int result = ProductSubCategoryClient.InsertmProductSubCategory(ObjPrdSubCategory, profile.DBConnection._constr);
                        if (result == 1)
                        {
                            WebMsgBox.MsgBox.Show("Record saved successfully"); 
                        }
                        BindGrid();
                        clear();
                    }
                    else
                    {
                        ObjPrdSubCategory = ProductSubCategoryClient.GetProductSubCategoryListByID(Convert.ToInt32(hdnPrdSubCategoryID.Value), profile.DBConnection._constr);

                        ObjPrdSubCategory.Name = txtPrdSubCategory.Text;
                        ObjPrdSubCategory.ProductCategoryID = Convert.ToInt64(ddlPrdCategory.SelectedValue);
                        if (txtSequence.Text != string.Empty) { ObjPrdSubCategory.Sequence = Convert.ToInt64(txtSequence.Text); }
                        else { ObjPrdSubCategory.Sequence = 0; }
                        if (rbtnYes.Checked == true) { ObjPrdSubCategory.Active = "Y"; }
                        else { ObjPrdSubCategory.Active = "N"; }
                        ObjPrdSubCategory.LastModifiedBy =profile.Personal.UserID.ToString();
                        ObjPrdSubCategory.LastModifiedDate = DateTime.Now;
                        int result = ProductSubCategoryClient.updatemProductSubCategory(ObjPrdSubCategory, profile.DBConnection._constr);
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
                Login.Profile.ErrorHandling(ex, this, "Product Sub Category Master", "pageSave");
            }
            finally
            {
            }
        }

        protected void pageClear(Object sender, ToolbarService.iUCToolbarClient e)
        {
            clear();
        }

        public string checkDuplicate()
        {
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                string result = "";

                if (hdnPrdSubCategoryID.Value == string.Empty)
                {
                    result = ProductSubCategoryClient.checkDuplicateRecord(txtPrdSubCategory.Text, Convert.ToInt32(ddlPrdCategory.SelectedValue), profile.DBConnection._constr);
                    if (result != string.Empty)
                    {
                        WebMsgBox.MsgBox.Show(result);   
                        txtPrdSubCategory.Text = "";
                    }
                    txtSequence.Focus();
                }
                else
                {
                    result = ProductSubCategoryClient.checkDuplicateRecordEdit(Convert.ToInt32(hdnPrdSubCategoryID.Value), txtPrdSubCategory.Text, Convert.ToInt32(ddlPrdCategory.SelectedValue), profile.DBConnection._constr);
                    if (result != string.Empty)
                    {
                        WebMsgBox.MsgBox.Show(result);                          
                        txtPrdSubCategory.Text = "";
                    }
                }
                return result;
            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "Product Sub Category Master", "checkDuplicate");
                string result = "";
                return result;
            }
            finally
            {
            }
        }

        protected void gvPrdSubCategoryM_Select(object sender, Obout.Grid.GridRecordEventArgs e)
        {
            try
            {
                rbtnNo.Checked = false;
                rbtnYes.Checked = false;
                Hashtable selectedrec = (Hashtable)gvPrdSubCategoryM.SelectedRecords[0];
                hdnPrdSubCategoryID.Value = selectedrec["ID"].ToString();
                txtSequence.Text = selectedrec["Sequence"].ToString();
                ddlPrdCategory.SelectedIndex = ddlPrdCategory.Items.IndexOf(ddlPrdCategory.Items.FindByText(selectedrec["PrdCategoryName"].ToString()));
                txtPrdSubCategory.Text = selectedrec["Name"].ToString();
                if (selectedrec["Active"].ToString() == "No")
                { rbtnNo.Checked = true; }
                else
                { rbtnYes.Checked = true; }
            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "Product Sub Category Master", "gvPrdSubCategoryM_Select");
            }
            finally
            {
            }
        }
    }
}