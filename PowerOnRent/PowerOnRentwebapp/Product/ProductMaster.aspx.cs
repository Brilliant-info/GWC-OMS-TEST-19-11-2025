using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Runtime.Serialization;
using System.Collections;
using System.Xml;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using Obout.Grid;
using Obout.Ajax.UI.FileUpload;
using System.Data.OleDb;
using PowerOnRentwebapp.Login;
using PowerOnRentwebapp.ProductMasterService;
using System.Web.Services;
using WebMsgBox;
using PowerOnRentwebapp.DocumentService;
using PowerOnRentwebapp.ProductSubCategoryService;
using PowerOnRentwebapp.PORServiceUCCommonFilter;
using System.Resources;
using System.Globalization;
using System.Threading;
using System.Reflection;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace PowerOnRentwebapp.Product
{
    public partial class ProductMaster : System.Web.UI.Page
    {
        ResourceManager rm;
        CultureInfo ci;
        static string sessionID;
        //long BOmDetailId = 0;
        string editsession = "";
        static string CurrentObject = "ProductInventoryDetail";
        /*Page Methods*/
        #region Page Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            // string path = FileUpload1.FileName; 
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            CustomProfile profile = CustomProfile.GetProfile();

            if (Session["Lang"] == "")
            {
                Session["Lang"] = Request.UserLanguages[0];
            }
            if (string.IsNullOrEmpty((string)Session["Lang"]))
            {
                Session["Lang"] = Request.UserLanguages[0];
            }
            loadstring();
            sessionID = Session.SessionID.ToString();
            if(profile.Personal.UserType != "Super Admin")
            {
                btnupdate.Visible = false;
            }
            string Deptchkid = hdndeptid.Value.ToString();
            if (Deptchkid != "")
            {
                getDepartment(long.Parse(hdncompanyid.Value));
                ddldepartment.SelectedIndex = ddldepartment.Items.IndexOf(ddldepartment.Items.FindByValue(hdndeptid.Value.ToString()));
            }
            UC_EffectiveDateInventory.Date = DateTime.Now;

            if (!IsPostBack)
            {
                Session.Add("hdnedit", editsession);
                GetCompany();
                ResetUserControl();
                GetProductList();
                setActiveTab(0);
                ScriptManager.RegisterStartupScript(this.FlyoutChangeProdPrice, FlyoutChangeProdPrice.GetType(), "reg1", "SaveNewPrice();", false);
            } //GetCompany();
            //this.UCToolbar1.ToolbarAccess("ProductMaster");
            this.UCToolbar1.evClickAddNew += pageAddNew;
            this.UCToolbar1.evClickSave += pageSave;
            this.UCToolbar1.evClickClear += pageClear;
            //this.UCToolbar1.evClickImport += pageImport;


        }

        protected void Page_PreInit(Object sender, EventArgs e)
        { CustomProfile profile = CustomProfile.GetProfile(); if (profile.Personal.Theme == null || profile.Personal.Theme == string.Empty) { Page.Theme = "Blue"; } else { Page.Theme = profile.Personal.Theme; } }

        protected void ResetUserControl()
        {
            iProductMasterClient objService = new iProductMasterClient();
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                UC_AttachDocument1.ClearDocument("Product");
                objService.ClearTempDataFromDB(Session.SessionID, profile.Personal.UserID.ToString(), CurrentObject, profile.DBConnection._constr);
                hdnprodID.Value = "0";
            }
            catch { }
            finally { objService.Close(); }
        }


        /*End*/
        #endregion

        #region Product Info

        protected void GetImageDetails()
        {
            CustomProfile profile = CustomProfile.GetProfile();
            iProductMasterClient productClient = new iProductMasterClient();
            try
            {
                tImage obj = new tImage();
                obj = productClient.GetImageDetailByImageId(Convert.ToInt64(hdnSelImage.Value), profile.DBConnection._constr);
                productClient.Close();

                if (obj.ImgeTitle != null) txtImageTitle.Text = obj.ImgeTitle.ToString();
                if (obj.ImageDesc != null) txtImageDescription.Text = obj.ImageDesc.ToString();

                rbtnNo1.Checked = false;
                rbtnYes1.Checked = false;
                if (obj.Active != null)
                {
                    if (obj.Active == "N") rbtnNo1.Checked = true;
                    if (obj.Active == "Y") rbtnYes1.Checked = true;
                }
            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "Product master", "GetImageDetails");
            }
            finally
            {
                productClient.Close();

            }
        }

        protected void GetProductDetailByProductID()
        {
            CustomProfile profile = CustomProfile.GetProfile();
            iProductMasterClient productClient = new iProductMasterClient();
            try
            {
                GetProductDetail obj = new GetProductDetail();
                obj = productClient.GetProductDetailByProductID(Convert.ToInt64(hdnprodID.Value), profile.DBConnection._constr);
                productClient.Close();
                BindProductSubCategory();
                if (obj.ProductCode != null) txtProductCode.Text = obj.ProductCode.ToString();
                if (obj.Name != null) txtProductName.Text = obj.Name.ToString();
                if (obj.UOMID != null) ddlUOM.SelectedIndex = ddlUOM.Items.IndexOf(ddlUOM.Items.FindByValue(obj.UOMID.Value.ToString()));
                if (obj.PrincipalPrice != null) txtPrincipalPrice.Text = obj.PrincipalPrice.ToString();
                if (obj.moq != null) txtMOQ.Text = obj.moq.ToString();
                if (obj.Description != null) txtdescription.Text = obj.Description.ToString();
                if (obj.Cost != null) txtcost.Text = obj.Cost.ToString();
                if (obj.GroupSet != null) ddlBom.SelectedItem.Text = obj.GroupSet.ToString();
                if (obj.OMSSKUCode != null) txtomsskucode.Text = obj.OMSSKUCode.ToString();
                if (ddlBom.SelectedItem.Text == "Yes")
                {
                    txtMOQ.Enabled = false;
                }
                GetCompany();
                if (obj.CompanyID != null) ddlcompany.SelectedIndex = ddlcompany.Items.IndexOf(ddlcompany.Items.FindByValue(obj.CompanyID.ToString()));
                hdncompanyid.Value = obj.CompanyID.ToString();
                getDepartment(obj.CompanyID);
                if (obj.StoreId != null) ddldepartment.SelectedIndex = ddldepartment.Items.IndexOf(ddldepartment.Items.FindByValue(obj.StoreId.Value.ToString()));
                hdndeptid.Value = obj.StoreId.ToString();
                DeleteMpackUOMClear();
                rbtNo.Checked = false;
                rbtYes.Checked = false;
                if (obj.Active != null)
                {
                    if (obj.Active == "N") rbtNo.Checked = true;
                    if (obj.Active == "Y") rbtYes.Checked = true;
                }
                rbtserialYes.Checked = false;
                rbtserialNo.Checked = false;
                if (obj.serialflag !=null)
                {
                    if (obj.serialflag == "Yes") rbtserialYes.Checked = true;
                    if (obj.serialflag == "No") rbtserialNo.Checked = true;
                }
               
                //GetProductDocumentByProductID();
                //GetProductSpecificationDetailByProductID();
                //GetProductTaxDetailByProductID();
                string Bom = "";
                if (obj.GroupSet != null) Bom = obj.GroupSet.ToString();
                if (Bom == "Yes")
                {
                    GetBOmDeyailbyIdforedit();
                    TabBOM.Visible = true;
                    tabInventory.Visible = false;
                }
                else
                {
                    TabBOM.Visible = false;
                }
                if (obj.ProductType != null)
                {
                    ddlskutype.SelectedItem.Text = obj.ProductType.ToString();
                }
                getProductSpecification(long.Parse(hdnprodID.Value));
                GetProductImagesByProductID();
                GetUOMPackById();
                // GVRateHistory();
                FillInventoryGrid(long.Parse(obj.StoreId.ToString()), long.Parse(hdnprodID.Value));
                changePrice1.Attributes.Add("style", "visibility:visible");
            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "Product master", "GetProductDetailByProductID");
            }
            finally
            {
                productClient.Close();
            }
        }

        protected void FinalSaveProductDetailByProductID()
        {
            iProductMasterClient productClient = new iProductMasterClient();
            try
            {
                string state;
                CustomProfile profile = CustomProfile.GetProfile();
                if (checkDuplicate() == "")
                {
                    mProduct obj = new mProduct();
                    if (hdnprodID.Value != "0")
                    {
                        state = "Edit";
                        obj = productClient.GetmProductToUpdate(Convert.ToInt64(hdnprodID.Value), profile.DBConnection._constr);
                        obj.LastModifiedBy = profile.Personal.UserID.ToString();
                        obj.LastModifiedDate = DateTime.Now;
                    }
                    else
                    {
                        state = "AddNew";
                        obj.CreatedBy = profile.Personal.UserID.ToString();
                        obj.CreationDate = DateTime.Now;
                    }
                    obj.ProductCode = txtProductCode.Text.ToString().Trim();
                    obj.Name = txtProductName.Text.ToString().Trim();
                    obj.CompanyID = long.Parse(ddlcompany.SelectedItem.Value);
                    obj.StoreId = long.Parse(ddldepartment.SelectedItem.Value);
                    obj.OMSSKUCode = txtomsskucode.Text.ToString();
                    if (txtcost.Text == "") txtcost.Text = "0";
                    obj.Cost = Convert.ToDecimal(txtcost.Text);
                    if (txtPrincipalPrice.Text == "") txtPrincipalPrice.Text = "0";
                    obj.PrincipalPrice = Convert.ToDecimal(txtPrincipalPrice.Text);
                    obj.GroupSet = ddlBom.SelectedItem.Text.ToString();
                    obj.Description = txtdescription.Text.ToString();
                    if (ddlBom.SelectedItem.Text != "Yes")
                    {
                        obj.moq = long.Parse(txtMOQ.Text.ToString());
                    }
                    else
                    {
                        obj.moq = 0;
                    }
                    obj.ProductType = ddlskutype.SelectedItem.Text;
                    

                    //obj.Cost = decimal.Parse(txtcost.Text.ToString());
                    //obj.OMSSKUCode = txtomsskucode.Text.ToString();
                    // obj.UOMID = Convert.ToInt64(ddlUOM.SelectedItem.Value);
                    obj.Active = "N";
                    if (rbtYes.Checked == true) obj.Active = "Y";
                    obj.serialflag = "No";
                    if (rbtserialYes.Checked == true) obj.serialflag = "Yes";


                   /* if (hdnstate.Value != "Edit")
                    {
                        obj.GroupSet = "Yes";
                    }*/

                    // if (grdaccessdele.TotalRowCount > 0 && obj.GroupSet=="Yes")
                    if (obj.GroupSet == "Yes")
                    {
                        if (grdaccessdele.TotalRowCount > 0)
                        {
                            hdnprodID.Value = productClient.FinalSaveProductDetailByProductID(obj, profile.DBConnection._constr).ToString();

                            // hdnprodID.Value = "1243";

                            if (hdnstate.Value == "Edit")
                            {
                                productClient.FinalSaveProductImagesByProductIDEdit(Session.SessionID.ToString(), profile.Personal.UserID.ToString(), Convert.ToInt64(hdnprodID.Value), Server.MapPath(""), profile.DBConnection._constr);
                            }
                            else
                            {
                                productClient.FinalSaveProductImagesByProductID(Session.SessionID.ToString(), profile.Personal.UserID.ToString(), Convert.ToInt64(hdnprodID.Value), Server.MapPath(""), profile.DBConnection._constr);
                            }

                            // productClient.FinalSaveProductSpecificationDetailByProductID(sessionID, profile.Personal.UserID.ToString(), Convert.ToInt64(hdnprodID.Value), Convert.ToInt64(profile.Personal.CompanyID), profile.DBConnection._constr);
                            //  productClient.FinalSaveProductInventory(sessionID, CurrentObject, Convert.ToInt64(hdnprodID.Value), DateTime.Now, profile.Personal.UserID.ToString(), profile.DBConnection._constr);

                            if (hdnstate.Value == "Edit")
                            {
                                if (txtOpbalance.Text.ToString() != "")
                                {
                                    productClient.UpdateInventryOpeningBal(decimal.Parse(txtOpbalance.Text.ToString()), Convert.ToInt64(hdnprodID.Value), long.Parse(obj.StoreId.ToString()), profile.DBConnection._constr);
                                }
                            }
                            productClient.UpdatePackUOMforSkuId(Convert.ToInt64(hdnprodID.Value), profile.DBConnection._constr);
                            productClient.UpdateBOMforsSkuId(Convert.ToInt64(hdnprodID.Value), profile.DBConnection._constr);
                            if (hdnstate.Value != "Edit")
                            {

                                decimal availbal = 0;
                                decimal VirtualQty = 1000000, VirtualReOrderQty = 1000;
                                if (ddlBom.SelectedItem.Text == "Yes")
                                {
                                    decimal calreturn = decimal.Parse(CalculateBonStock(Convert.ToInt64(hdnprodID.Value)).ToString());
                                    availbal = calreturn;
                                    VirtualQty = 0;
                                    VirtualReOrderQty = 0;
                                }

                                productClient.SaveProductStockDetail(long.Parse(obj.StoreId.ToString()), Convert.ToInt64(hdnprodID.Value), availbal, VirtualQty, VirtualReOrderQty, profile.DBConnection._constr);
                                // productClient.UpdateProductStockQty(Convert.ToInt64(hdnprodID.Value), profile.DBConnection._constr);
                            }

                            if (hdnprodID.Value != "0")
                            {
                                WebMsgBox.MsgBox.Show("Record saved successfully");
                            }
                            productClient.Close();
                            clear();
                            //GetProductDocumentByProductID();
                            // GetProductSpecificationDetailByProductID();
                            // GetProductTaxDetailByProductID();
                            GetProductImagesByProductID();
                            GetProductList();
                            GVRateHistory();
                            //FillInventoryGrid();
                            setActiveTab(0);
                            Response.Redirect("ProductMaster.aspx");
                            changePrice1.Attributes.Add("style", "visibility:visible");
                        }
                        else
                        {
                            WebMsgBox.MsgBox.Show("Please Add Sku In BOM Sku List...");
                        }

                    }
                    else
                    {
                        hdnprodID.Value = productClient.FinalSaveProductDetailByProductID(obj, profile.DBConnection._constr).ToString();

                        if (hdnstate.Value == "Edit")
                        {
                            productClient.FinalSaveProductImagesByProductIDEdit(Session.SessionID.ToString(), profile.Personal.UserID.ToString(), Convert.ToInt64(hdnprodID.Value), Server.MapPath(""), profile.DBConnection._constr);
                        }
                        else
                        {
                            productClient.FinalSaveProductImagesByProductID(Session.SessionID.ToString(), profile.Personal.UserID.ToString(), Convert.ToInt64(hdnprodID.Value), Server.MapPath(""), profile.DBConnection._constr);
                        }

                        if (hdnstate.Value == "Edit")
                        {
                            if (txtOpbalance.Text.ToString() != "")
                            {
                                productClient.UpdateInventryOpeningBal(decimal.Parse(txtOpbalance.Text.ToString()), Convert.ToInt64(hdnprodID.Value), long.Parse(obj.StoreId.ToString()), profile.DBConnection._constr);
                            }
                        }
                        productClient.UpdatePackUOMforSkuId(Convert.ToInt64(hdnprodID.Value), profile.DBConnection._constr);
                        productClient.UpdateBOMforsSkuId(Convert.ToInt64(hdnprodID.Value), profile.DBConnection._constr);
                        if (hdnstate.Value != "Edit")
                        {
                            decimal availbal = 0;
                            decimal VirtualQty = 1000000, VirtualReOrderQty = 1000;
                            if (ddlBom.SelectedItem.Text == "Yes")
                            {
                                decimal calreturn = decimal.Parse(CalculateBonStock(Convert.ToInt64(hdnprodID.Value)).ToString());
                                availbal = calreturn;
                                VirtualQty = 0;
                                VirtualReOrderQty = 0;
                            }
                                                   
                            productClient.SaveProductStockDetail(long.Parse(obj.StoreId.ToString()), Convert.ToInt64(hdnprodID.Value), availbal,VirtualQty,VirtualReOrderQty, profile.DBConnection._constr);
                        }

                        if (hdnprodID.Value != "0")
                        {
                            WebMsgBox.MsgBox.Show("Record saved successfully");
                        }
                        productClient.Close();
                        clear();

                        GetProductImagesByProductID();
                        GetProductList();
                        GVRateHistory();

                        setActiveTab(0);
                        Response.Redirect("ProductMaster.aspx");
                        changePrice1.Attributes.Add("style", "visibility:visible");
                    }
                }
            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "Product master", "FinalSaveProductDetailByProductID");
            }
            finally
            {
                productClient.Close();
            }
        }

        protected void GetProductList() /*Bind GridView*/
        {
                CustomProfile profile = CustomProfile.GetProfile();
                using (iProductMasterClient productClient = new iProductMasterClient())
                {
                    long UID = profile.Personal.UserID;
                    string UsrType = profile.Personal.UserType.ToString();
                    if (UsrType == "Super Admin")
                    {
                        grvProduct.DataSource = productClient.GetProductList(profile.DBConnection._constr);
                    }
                    else
                    {
                        grvProduct.DataSource = productClient.GetProductListDeptWise(UID, profile.DBConnection._constr);
                    }
                    grvProduct.DataBind();                   
                }
        }

        protected void getProductSpecification(long productid)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            CustomProfile profile = CustomProfile.GetProfile();
            iProductMasterClient productClient = new iProductMasterClient();
            ds = productClient.getProductSpecification(productid, profile.DBConnection._constr);
            dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                Grid1.DataSource = ds.Tables[0];
                Grid1.DataBind();
            }
            else
            {
                Grid1.DataSource = null;
                Grid1.DataBind();

            }
        }

        protected void Grid1_RebindGrid(object sender, EventArgs e)
        {
            getProductSpecification(long.Parse(hdnprodID.Value));
        }
        #endregion

        #region Prodcut Tax Setup
        /*Prodcut Tax Setup*/
        protected void GetProductTaxDetailByProductID()
        {
            CustomProfile profile = CustomProfile.GetProfile();
            if (hdnprodID.Value == "") hdnprodID.Value = "0";
            iProductMasterClient productClient = new iProductMasterClient();
            GVTaxSetup.DataSource = productClient.GetProductTaxDetailByProductID(Convert.ToInt64(hdnprodID.Value), Session.SessionID.ToString(), profile.Personal.UserID.ToString(), profile.DBConnection._constr);
            GVTaxSetup.DataBind();
            productClient.Close();
        }

        [WebMethod]
        public static void TempSaveTaxSetup(string IsChecked, string TaxID)
        {
            CustomProfile profile = CustomProfile.GetProfile();
            iProductMasterClient productClient = new iProductMasterClient();
            //connectiondetails conn = new connectiondetails() { DataBaseName = Profile.DataBase, DataSource = Profile.DataSource, DBPassword = Profile.DBPassword };
            productClient.UpdateTempTaxSetup(TaxID, IsChecked.ToLower(), sessionID, profile.Personal.UserID.ToString(), profile.DBConnection._constr);
            productClient.Close();
        }

        protected void GVTaxSetup_OnRebind(object sender, EventArgs e)
        {
            iProductMasterClient productClient = new iProductMasterClient();
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                GVTaxSetup.DataSource = productClient.GetTempSaveProductTaxDetailBySessionID(Session.SessionID.ToString(), profile.Personal.UserID.ToString(), profile.DBConnection._constr);
                GVTaxSetup.DataBind();
                productClient.Close();
            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "ProductMaster", "GVTaxSetup_OnRebind");
            }
            finally
            {
                productClient.Close();
            }
        }
        #endregion

        #region Prodcut Specification
        /*Prodcut Specification*/
        protected void GetProductSpecificationDetailByProductID()
        {
            CustomProfile profile = CustomProfile.GetProfile();
            if (hdnprodID.Value == "") hdnprodID.Value = "0";
            iProductMasterClient productClient = new iProductMasterClient();
            try
            {
                GVProductSpecification.DataSource = productClient.GetProductSpecificationDetailByProductID(Convert.ToInt64(hdnprodID.Value), Session.SessionID.ToString(), profile.Personal.UserID.ToString(), profile.DBConnection._constr);
                GVProductSpecification.DataBind();
                productClient.Close();
            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "ProductMaster", "GetProductSpecificationDetailByProductID");
            }
            finally
            {
                productClient.Close();
            }
        }

        protected void GVProductSpecification_OnRebind(object sender, EventArgs e)
        {
            GetProductSpecificationDetailByProductID();
        }

        protected void GVProductSpecification_InsertRecord(object sender, Obout.Grid.GridRecordEventArgs e)
        {
            CustomProfile profile = CustomProfile.GetProfile();
            mProductSpecificationDetail oprodspec = new mProductSpecificationDetail();
            oprodspec.SpecificationTitle = e.Record["SpecificationTitle"].ToString();
            oprodspec.SpecificationDescription = e.Record["SpecificationDescription"].ToString();
            oprodspec.Active = "Y";
            oprodspec.ProductID = Convert.ToInt32(hdnprodID.Value);
            oprodspec.CreatedBy = profile.Personal.UserID.ToString(); //need to change
            oprodspec.CreationDate = DateTime.Now;
            oprodspec.CompanyID = profile.Personal.CompanyID; // need to change
            //int upprodsperesult = productClient.InserttProductSpecificationDetail(oprodspec, profile.DBConnection._constr);
            iProductMasterClient productClient = new iProductMasterClient();
            productClient.AddProductSpecificationToTempData(oprodspec, Session.SessionID.ToString(), profile.Personal.UserID.ToString(), profile.DBConnection._constr);
            productClient.Close();

        }

        protected void GVProductSpecification_OnUpdateCommand(object sender, Obout.Grid.GridRecordEventArgs e)
        {
            /*
            if (hdnprodID.Value != null)
            {
                int Spcid = Convert.ToInt32(e.Record["ID"].ToString());
                var prodspec = (from c in productClient.GetProductSpecificationDetailsByProdSpecID(Spcid, profile.DBConnection._constr)
                                select c).ToList();
                foreach (var v in prodspec)
                {
                    mProductSpecificationDetail oprodspec = new mProductSpecificationDetail();
                    oprodspec.ID = v.ID;
                    oprodspec.SpecificationTitle = e.Record["SpecificationTitle"].ToString();
                    oprodspec.SpecificationDescription = e.Record["SpecificationDescription"].ToString();
                    oprodspec.Active = v.Active;
                    oprodspec.ProductID = v.ProductID;
                    oprodspec.CreatedBy = v.CreatedBy;
                    oprodspec.CreationDate = v.CreationDate;
                    oprodspec.LastModifiedBy = profile.Personal.UserID.ToString(); //need to change
                    oprodspec.LastModifiedDate = DateTime.Now;
                    oprodspec.CompanyID = v.CompanyID;
                    int upprodsperesult = productClient.UpdatemProductSpecificationDetail(oprodspec, profile.DBConnection._constr);
                    if (upprodsperesult > 0)
                    {
                        pop.DisplayPopupMessage(this, "Record Updated sucessfully.", PopupMessages.PopupMessage.AlertType.Error);
                        //bindGVProductSpecification();
                    }
                    else
                    {
                        pop.DisplayPopupMessage(this, "Error while updating record! Please try latter.", PopupMessages.PopupMessage.AlertType.Error);
                    }
                }
            }*/
        }

        protected void GVProductSpecification_OnDeleteCommand(object sender, Obout.Grid.GridRecordEventArgs e)
        {
            /*
            if (hdnprodID.Value != null)
            {
                int Spcid = Convert.ToInt32(e.Record["ID"].ToString());
                var prodspec = (from c in productClient.GetProductSpecificationDetailsByProdSpecID(Spcid, profile.DBConnection._constr)
                                select c).ToList();
                foreach (var v in prodspec)
                {
                    PowerOnRentwebapp.ProductService.mProductSpecificationDetail oprodspec = new PowerOnRentwebapp.ProductService.mProductSpecificationDetail();
                    oprodspec.ID = v.ID;
                    oprodspec.SpecificationTitle = e.Record["SpecificationTitle"].ToString();
                    oprodspec.SpecificationDescription = e.Record["SpecificationDescription"].ToString();
                    oprodspec.Active = v.Active;
                    oprodspec.ProductID = v.ProductID;
                    oprodspec.CreatedBy = v.CreatedBy;
                    oprodspec.CreationDate = v.CreationDate;
                    oprodspec.LastModifiedBy = profile.Personal.UserID.ToString(); //need to change
                    oprodspec.LastModifiedDate = DateTime.Now;
                    oprodspec.CompanyID = v.CompanyID;
                    int upprodsperesult = productClient.DeletemProductSpecificationDetail(oprodspec, profile.DBConnection._constr);
                }
            }*/
        }

        #endregion

        #region Prodcut Imgs
        protected void GetProductImagesByProductID()
        {
            CustomProfile profile = CustomProfile.GetProfile();
            if (hdnprodID.Value == "") hdnprodID.Value = "0";
            iProductMasterClient productClient = new iProductMasterClient();
            GVImages.DataSource = productClient.GetProductImagesByProductID(Convert.ToInt64(hdnprodID.Value), Session.SessionID.ToString(), profile.Personal.UserID.ToString(), profile.DBConnection._constr);
            GVImages.DataBind();

            DataSet ds = new DataSet();
            ds = productClient.GetProductImagesByProductID(Convert.ToInt64(hdnprodID.Value), Session.SessionID.ToString(), profile.Personal.UserID.ToString(), profile.DBConnection._constr);
            if (ds.Tables[0].Rows.Count > 0)
            {
                btnProductMasterUploadImg.Visible = false; Updpnl.Visible = false;
            }
            else { btnProductMasterUploadImg.Visible = true; Updpnl.Visible = true; }

            //for (int j = 0; j < GVImages.Rows.Count; j++)
            //{
            //    byte[] bytes = (byte[])ds.Tables[0].Rows[j]["SkuImage"]; 
            //    string strbase64 = Convert.ToBase64String(bytes);

            //    Image img = (Image)GVImages.Rows[j].FindControl("ImageDisplay");
            //}
            productClient.Close();
        }

        protected void btnProductMasterUploadImg_OnClick(object sender, EventArgs e)
        {
            //if (FileUploadProductMasterImg.PostedFiles != null)
            //{
            //    CustomProfile profile = CustomProfile.GetProfile();
            //    iProductMasterClient productClient = new iProductMasterClient();

            //    string uploadfilepath = HttpRuntime.AppDomainAppPath;

            //    foreach (PostedFileInfo info in FileUploadProductMasterImg.PostedFiles)
            //    {
            //        string type = info.ContentType.Replace("image/", "").Replace("application/", "");
            //        //type = type.Replace("application/", "");
            //        string gridDisplayPath = "TempImg\\" + Session.SessionID.ToString() + DateTime.Now.Ticks.ToString() + "." + type;
            //        string SaveAsPath = uploadfilepath + "\\" + gridDisplayPath;
            //        //SaveAsPath = SaveAsPath.Replace('\\', '/');

            //        if (!(Directory.Exists(uploadfilepath + "/TempImg")))
            //        {
            //            Directory.CreateDirectory(uploadfilepath + "/TempImg");
            //        }
            //        info.SaveAs(SaveAsPath);

            //        tImage UploadedImage = new tImage();
            //        UploadedImage.ObjectName = "Product";
            //        UploadedImage.ReferenceID = Convert.ToInt32(hdnprodID.Value);
            //        UploadedImage.ImageName = info.FileName;
            //        UploadedImage.ImgeTitle = txtImageTitle.Text;
            //        UploadedImage.ImageDesc = txtImageDescription.Text;
            //        UploadedImage.Path = gridDisplayPath;
            //        UploadedImage.Extension = type;
            //        //UploadedImage.Active = "Y";
            //        if (rbtnYes.Checked == true)
            //        { UploadedImage.Active = "Y"; }
            //        else
            //        { UploadedImage.Active = "N"; }
            //        UploadedImage.CreatedBy = profile.Personal.UserID.ToString();
            //        UploadedImage.CreationDate = DateTime.Now;
            //        UploadedImage.CompanyID = profile.Personal.CompanyID;
            //        productClient.AddTempProductImages(UploadedImage, Session.SessionID, profile.Personal.UserID.ToString(), profile.DBConnection._constr);
            //    }

            //    GVImages.DataSource = productClient.GetTempSaveProductImagesBySessionID(Session.SessionID, profile.Personal.UserID.ToString(), profile.DBConnection._constr);
            //    GVImages.DataBind();
            //    txtImageDescription.Text = "";
            //    txtImageTitle.Text = "";
            //}
            iProductMasterClient productClient = new iProductMasterClient();
            try
            {
                //HttpFileCollection flImages = Request.Files;
                //HttpPostedFile userPostedFile = flImages[0];

                string path = FileUpload1.FileName;

                if (FileUpload1.FileName != "")
                {
                    if (FileUpload1.FileName != "")
                    {
                        CustomProfile profile = CustomProfile.GetProfile();
                        string uploadfilepath = HttpRuntime.AppDomainAppPath;
                        string gridDisplayPath = "";
                        string SaveAsPath = ""; string type = "";
                        tImage UploadedImage = new tImage();
                        decimal size = Math.Round(((decimal)FileUpload1.PostedFile.ContentLength / (decimal)1024), 2);
                        if (size > 60)                      
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "showAlert('File size must not exceed 60 KB.','Error','#');", true);
                        }
                        else if (size > 45 && size <= 60)
                        {
                            System.Drawing.Image img = System.Drawing.Image.FromStream(FileUpload1.PostedFile.InputStream);
                            int height = img.Height;
                            int width = img.Width;

                            Bitmap original_image = new Bitmap(FileUpload1.FileContent);
                            Bitmap finalImg = null;
                            Graphics graphic = null;
                            decimal w = width / 2;
                            int reqW = Convert.ToInt16(Math.Round(w));
                            //if (reqW > 100) reqW = 100;
                            decimal h = height / 2;
                            int reqH = Convert.ToInt16(Math.Round(h));
                            // if (reqH > 100) reqH = 100;
                            finalImg = new Bitmap(reqW, reqH);
                            graphic = Graphics.FromImage(finalImg);
                            graphic.FillRectangle(new SolidBrush(Color.Transparent), new Rectangle(0, 0, reqW, reqH));
                            graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            graphic.DrawImage(original_image, 0, 0, reqW, reqH);
                            type = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);

                            gridDisplayPath = "TempImg/" + hdnprodID.Value + type;
                            SaveAsPath = uploadfilepath + "Product\\" + gridDisplayPath;

                            finalImg.Save(MapPath("~/Product/TempImg/" + hdnprodID.Value + Path.GetExtension(FileUpload1.FileName)));
                            //FileInfo nfi = new FileInfo(MapPath("~/Product/TempImg/" + hdnprodID.Value + Path.GetExtension(FileUpload1.FileName)));

                            //byte[] newImgSize = System.IO.File.ReadAllBytes(MapPath("~/Product/TempImg/" + hdnprodID.Value + Path.GetExtension(FileUpload1.FileName)));
                            //string NewSize = nfi.Length.ToString();

                            byte[] newImgSize = System.IO.File.ReadAllBytes(Server.MapPath(gridDisplayPath));
                            UploadedImage.SkuImage = newImgSize;
                        }
                        else
                        {

                            //string uploadfilepath = HttpRuntime.AppDomainAppPath;

                            ////Session["PrdImg"] = FileUploadProductMasterImg.PostedFiles;// FileBytes;
                            ////Img1.Src = "../Image.aspx";                   

                            //// foreach (PostedFileInfo info in FileUploadProductMasterImg1.PostedFile)
                            ////{
                            ////string type = info.ContentType.Replace("image/", "").Replace("application/", "");
                            type = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);

                            gridDisplayPath = "TempImg/" + hdnprodID.Value + type;
                            SaveAsPath = uploadfilepath + "Product\\" + gridDisplayPath;
                            //SaveAsPath = SaveAsPath.Replace('\\', '/');

                            if (!(Directory.Exists(uploadfilepath + "/TempImg")))
                            {
                                Directory.CreateDirectory(uploadfilepath + "/TempImg");
                            }
                            FileUpload1.SaveAs(SaveAsPath);

                            /* Image File In Binary  */
                            byte[] img = System.IO.File.ReadAllBytes(Server.MapPath(gridDisplayPath));
                            UploadedImage.SkuImage = img;
                            /* Image File In Binary  */
                        }

                        //if (!(Directory.Exists(uploadfilepath + "/TempImg")))
                        //{
                        //    Directory.CreateDirectory(uploadfilepath + "/TempImg");
                        //}
                        //FileUpload1.SaveAs(SaveAsPath);

                        //   tImage UploadedImage = new tImage();
                        UploadedImage.ObjectName = "Product";
                        UploadedImage.ReferenceID = Convert.ToInt32(hdnprodID.Value);
                        // UploadedImage.ImageName = info.FileName;
                        UploadedImage.ImageName = txtomsskucode.Text;
                        UploadedImage.ImgeTitle = txtImageTitle.Text;
                        UploadedImage.ImageDesc = txtImageDescription.Text;
                        UploadedImage.Path = gridDisplayPath;



                        // UploadedImage.Path = SaveAsPath;
                        UploadedImage.Extension = type;
                        //UploadedImage.Active = "Y";
                        if (rbtnYes1.Checked == true)
                        { UploadedImage.Active = "Y"; }
                        else
                        { UploadedImage.Active = "N"; }
                        UploadedImage.CompanyID = profile.Personal.CompanyID;

                        if (hdnstate.Value == "Edit")
                        {
                            UploadedImage.LastModifiedBy = profile.Personal.UserID.ToString();
                            UploadedImage.LastModifiedDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());

                            //DataSet ds = new DataSet();
                            //ds = productClient.GetCreatedByDate(hdnSelImage.Value, profile.DBConnection._constr); //hdnprodID
                            //UploadedImage.CreatedBy = ds.Tables[0].Rows[0]["CreatedBy"].ToString();
                            //UploadedImage.CreationDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["CreationDate"].ToString());

                            UploadedImage.CreatedBy = profile.Personal.UserID.ToString();
                            UploadedImage.CreationDate = DateTime.Now;

                            if (hdnImgState.Value == "Edit")
                            {
                                productClient.UpdateImageInTImage(Convert.ToInt64(hdnprodID.Value), UploadedImage.ImageName, UploadedImage.Path, UploadedImage.Extension, UploadedImage.LastModifiedBy, UploadedImage.CompanyID, UploadedImage.SkuImage, UploadedImage.ImgeTitle, UploadedImage.ImageDesc, profile.DBConnection._constr);
                            }
                            else
                            {
                                productClient.InsertintotImage("Product", Convert.ToInt64(hdnprodID.Value), UploadedImage.ImageName, UploadedImage.Path, UploadedImage.Extension, profile.Personal.UserID.ToString(), DateTime.Now, UploadedImage.CompanyID, UploadedImage.SkuImage, UploadedImage.ImgeTitle, UploadedImage.ImageDesc, profile.DBConnection._constr);
                            }

                            productClient.EditTempProductImages(UploadedImage, Session.SessionID, profile.Personal.UserID.ToString(), hdnstate.Value, profile.DBConnection._constr);
                        }
                        else
                        {
                            UploadedImage.CreatedBy = profile.Personal.UserID.ToString();
                            UploadedImage.CreationDate = DateTime.Now;
                            if (hdnprodID.Value == "0")
                            {
                                // WebMsgBox.MsgBox.Show("First Save SKU Then Upload Image...");
                                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "showAlert('First Save SKU Then Upload Image...','Error','#');", true);
                            }
                            else
                            {
                                productClient.InsertintotImage("Product", Convert.ToInt64(hdnprodID.Value), UploadedImage.ImageName, UploadedImage.Path, UploadedImage.Extension, profile.Personal.UserID.ToString(), DateTime.Now, UploadedImage.CompanyID, UploadedImage.SkuImage, UploadedImage.ImgeTitle, UploadedImage.ImageDesc, profile.DBConnection._constr);
                                productClient.AddTempProductImages(UploadedImage, Session.SessionID, profile.Personal.UserID.ToString(), profile.DBConnection._constr);
                            }
                        }

                        //Session["PrdImg"] = info.ContentLength;
                        //Img1.Src = "../Image.aspx";
                        //UploadedImage.SkuImage = Response.BinaryWrite((byte[])info.ContentLength); // (byte[])Session["PrdImg"];                        
                        //}

                        if (hdnprodID.Value != "0")
                        {
                            //GVImages.DataSource = productClient.GetTempSaveProductImagesBySessionID(Session.SessionID, profile.Personal.UserID.ToString(), profile.DBConnection._constr);
                            GVImages.DataSource = productClient.GetProductImagesByProductID(Convert.ToInt64(hdnprodID.Value), Session.SessionID, profile.Personal.UserID.ToString(), profile.DBConnection._constr);
                            GVImages.DataBind();
                            txtImageDescription.Text = "";
                            txtImageTitle.Text = "";

                            btnProductMasterUploadImg.Visible = false;
                            Updpnl.Visible = false;

                            WebMsgBox.MsgBox.Show("Image Uploaded successfully");
                        }
                        //}
                    }
                }
                else
                {
                    WebMsgBox.MsgBox.Show("Please Select Image!");
                }
            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "Productmaster", "btnProductMasterUploadImg_OnClick");
            }
            finally { productClient.Close(); }

        }
        #endregion

        #region Prodcut Vendor Details
        protected void GetProductVendorsByProductID()
        {

        }
        protected void FinalSaveProductVendorsByProductID()
        {

        }
        #endregion

        #region Prodcut Documents
        protected void GetProductDocumentByProductID()
        {
            CustomProfile profile = CustomProfile.GetProfile();
            if (hdnprodID.Value == "") hdnprodID.Value = "0";
            UC_AttachDocument1.FillDocumentByObjectNameReferenceID(Convert.ToInt64(hdnprodID.Value), "Product", "Product");
        }
        protected void FinalSaveProductDocumentByProductID()
        {

        }
        #endregion

        #region Bind Dropdown

        public void Bindropdown()
        {
            CustomProfile profile = CustomProfile.GetProfile();
            iProductMasterClient productClient = new iProductMasterClient();
            // ddlProductType.DataSource = productClient.GetProductTypeList(profile.DBConnection._constr);
            // ddlProductType.DataBind();

            ListItem lst1 = new ListItem();
            lst1.Text = "-Select-";
            lst1.Value = "0";
            //ddlProductType.Items.Insert(0, lst1);

            ddlUOM.SelectedIndex = -1;
            ddlUOM.DataSource = productClient.GetProductUOMList(profile.DBConnection._constr);
            ddlUOM.DataBind();
            ListItem lst3 = new ListItem();
            lst3.Text = "-Select-";
            lst3.Value = "0";
            ddlUOM.Items.Insert(0, lst3);
            productClient.Close();

            ProductCategoryService.iProductCategoryMasterClient productcategoryClient = new ProductCategoryService.iProductCategoryMasterClient();
            //ProductCategoryService.connectiondetails conn = new ProductCategoryService.connectiondetails() { DataBaseName = Profile.DataBase, DataSource = Profile.DataSource, DBPassword = Profile.DBPassword };

            //ddlCategory.SelectedIndex = -1;
            // ddlCategory.DataSource = productcategoryClient.GetProductCategoryList(profile.DBConnection._constr);
            //ddlCategory.DataBind();
            productcategoryClient.Close();
            ListItem lst2 = new ListItem();
            lst2.Text = "-Select-";
            lst2.Value = "0";
            // ddlCategory.Items.Insert(0, lst2);
        }

        public void ddl_Category_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            BindProductSubCategory();
        }

        protected void BindProductSubCategory()
        {
            //ddlSubCategory.Items.Clear();

            //if (ddlCategory.SelectedIndex > 0)
            //{
            //    CustomProfile profile = CustomProfile.GetProfile();
            //    ProductSubCategoryService.iProductSubCategoryMasterClient productsubcategoryClient = new ProductSubCategoryService.iProductSubCategoryMasterClient();
            //    //ProductSubCategoryService.connectiondetails conn = new ProductSubCategoryService.connectiondetails() { DataBaseName = Profile.DataBase, DataSource = Profile.DataSource, DBPassword = Profile.DBPassword };
            //    ddlSubCategory.DataSource = productsubcategoryClient.GetProductSubCategoryByProductCategoryID(Convert.ToInt32(ddlCategory.SelectedItem.Value), profile.DBConnection._constr);
            //    ddlSubCategory.DataBind();
            //    productsubcategoryClient.Close();
            //}
            //if (ddlSubCategory.Items.Count > 0)
            //{
            //    if (ddlSubCategory.Items[0].Text != "Not available")
            //    {
            //        ListItem lst = new ListItem();
            //        lst.Text = "-Select-";
            //        lst.Value = "0";
            //        ddlSubCategory.Items.Insert(0, lst);
            //    }
            //}
        }
        #endregion

        protected void BtnSubMitproductSp_Click(object sender, EventArgs e)
        {
            try
            {
                //iProductMasterClient productClient = new iProductMasterClient();
                //iProductMasterClient productClient = new iProductMasterClient();
                //List<mProductSpecificationDetail> ProductSpecificationDetail = new List<mProductSpecificationDetail>();
                //CustomProfile profile = CustomProfile.GetProfile();
                //mProductSpecificationDetail oprodspec = new mProductSpecificationDetail();
                //if (Hndstate.Value == "Edit")
                //{
                //    oprodspec = productClient.GetSpecificationDetailFromTempTableBySequence(Session.SessionID, profile.Personal.UserID.ToString(), 0, Convert.ToInt16(hndsequence.Value), profile.DBConnection._constr);
                //    oprodspec.Sequence = Convert.ToInt64(hndsequence.Value);
                //}
                //else
                //{
                //    oprodspec.Sequence = 0;
                //}
                //oprodspec.SpecificationTitle = txtspecificationtitle.Text; ;
                //oprodspec.SpecificationDescription = txtSpecificationDesc.Text;
                //oprodspec.Active = "Y";
                //oprodspec.ProductID = Convert.ToInt32(hdnprodID.Value);
                //oprodspec.CreatedBy = profile.Personal.UserID.ToString(); //need to change
                //oprodspec.CreationDate = DateTime.Now;
                //oprodspec.CompanyID = profile.Personal.CompanyID; // need to change
                ////int upprodsperesult = productClient.InserttProductSpecificationDetail(oprodspec, profile.DBConnection._constr);
                //if (Hndstate.Value == "Edit")
                //{
                //    ProductSpecificationDetail = productClient.SetValuesToTempData_onChange(0, Session.SessionID, profile.Personal.UserID.ToString(), profile.DBConnection._constr, Convert.ToInt16(hndsequence.Value), oprodspec).ToList();
                //}
                //else
                //{
                //    ProductSpecificationDetail = productClient.AddProductSpecificationToTempData(oprodspec, Session.SessionID.ToString(), profile.Personal.UserID.ToString(), profile.DBConnection._constr).ToList();

                //}
                //GVProductSpecification.DataSource = ProductSpecificationDetail;
                //GVProductSpecification.DataBind();
                //productClient.Close();
                //Hndstate.Value = "";
                //clr();

            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "Productmaster", "BtnSubmit_Click");
            }
            finally
            {
            }
        }

        private void clr()
        {
            // txtSpecificationDesc.Text = "";
            //txtspecificationtitle.Text = "";
        }

        protected void imgBtnEdit_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Hndstate.Value = "Edit";
                Hashtable selectedrec = (Hashtable)GVProductSpecification.SelectedRecords[0];
                hndsequence.Value = selectedrec["Sequence"].ToString();

                GetRecordFromTempTableForUpdate();
            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "Productmaster", "imgBtnEdit_Click");
            }

        }

        private void GetRecordFromTempTableForUpdate()
        {
            try
            {
                //CustomProfile profile = CustomProfile.GetProfile();
                //mProductSpecificationDetail FillList = new mProductSpecificationDetail();

                //iProductMasterClient productClient = new iProductMasterClient();

                //FillList = productClient.GetSpecificationDetailFromTempTableBySequence(Session.SessionID, profile.Personal.UserID.ToString(), 0, Convert.ToInt16(hndsequence.Value), profile.DBConnection._constr);

                //txtspecificationtitle.Text = FillList.SpecificationTitle;
                //txtSpecificationDesc.Text = FillList.SpecificationDescription;
                ////AddressClient.Close();
                //productClient.Close();
            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "UC_AddressInformation", "GetRecordFromTempTableForUpdate");
            }
            finally
            {
            }
        }

        #region Toolbar Code
        protected void pageAddNew(Object sender, ToolbarService.iUCToolbarClient e)
        {
            clear();
            GetProductSpecificationDetailByProductID();
            //ddlBom.Items.Clear();
            //ddlBom.Items.Add("Yes");
            txtMOQ.Enabled = true;

            ddlskutype.Items.Clear();
            ddlskutype.Items.Add("Virtual SKU");

            // GetProductTaxDetailByProductID();
            //GetProductImagesByProductID();
            // GVRateHistory();
            // FillInventoryGrid();
            setActiveTab(2);
            DeleteMpackUOMClear();
            hdnstate.Value = "Add";
            changePrice1.Attributes.Add("style", "visibility:hidden");
            tabInventory.Visible = false;
            txtPrincipalPrice.Enabled = true;
            txtcost.Enabled = true;
            Session["hdnedit"] = "";
            Session["hdnnewDelegateid"] = null;
            btnProductMasterUploadImg.Visible = true; Updpnl.Visible = true;
        }

        protected void pageImport(Object sender, ToolbarService.iUCToolbarClient e)
        { Response.Redirect("../Import/Import.aspx?Objectname=" + "Product"); }

        protected void pageSave(Object sender, ToolbarService.iUCToolbarClient e)
        {
            FinalSaveProductDetailByProductID();
            //txtImageDescription.Text = "";
            // txtImageTitle.Text = "";
        }

        protected void pageClear(Object sender, ToolbarService.iUCToolbarClient e)
        {
            clear();
            //GetProductList();
            setActiveTab(1);
            tabInventory.Visible = false;
            txtPrincipalPrice.Enabled = false;
            txtcost.Enabled = false;
        }

        protected void imgBtnEdit_OnClick(object sender, ImageClickEventArgs e)
        {
            ImageButton imgbtn = (ImageButton)sender;
            clear();
            string prdSelValue = hdnPartSelectedRec.Value.ToString();
            hdnprodID.Value = hdnPartSelectedRec.Value.ToString();
            setActiveTab(1);
            GetProductDetailByProductID();
            hdnstate.Value = "Edit";
            // btnProductMasterUploadImg.Visible = false;
        }

        protected void imgImageBtnEdit_OnClick(object sender, ImageClickEventArgs e)
        {
            ImageButton imgbtn = (ImageButton)sender;
            hdnSelImage.Value = imgbtn.ToolTip.ToString();
            GetImageDetails();

            btnProductMasterUploadImg.Visible = true; Updpnl.Visible = true;
            hdnImgState.Value = "Edit";
        }

        protected void clear()
        {
            CustomProfile profile = CustomProfile.GetProfile();
            txtProductCode.Text = "";
            txtProductName.Text = "";
            ddlUOM.SelectedIndex = -1;
            txtPrincipalPrice.Text = "";
            txtomsskucode.Text = "";
            txtOpbalance.Text = "";
            txtdescription.Text = "";
            txtcost.Text = "";
            ddlcompany.SelectedIndex = -1;
            ddldepartment.SelectedIndex = -1;
            hdnprodID.Value = null;
            hdnstate.Value = null;
            hdnImgState.Value = null;
            GVImages.DataSource = null;
            GVImages.DataBind();
            GVProductSpecification.DataSource = null;
            GVProductSpecification.DataBind();
            Grid2.DataSource = null;
            Grid2.DataBind();
            Grid1.DataSource = null;
            Grid1.DataBind();
            grdaccessdele.DataSource = null;
            grdaccessdele.DataBind();
            DeleteMpackUOMClear();
            iProductMasterClient productClient = new iProductMasterClient();
            productClient.ClearTempSaveProductSpecificationDetailBySessionID(Session.SessionID.ToString(), profile.Personal.UserID.ToString(), profile.DBConnection._constr);
            productClient.ClearTempSaveProductTaxDetailBySessionID(Session.SessionID.ToString(), profile.Personal.UserID.ToString(), profile.DBConnection._constr);
            productClient.ClearTempSaveProductImagesBySessionID(Session.SessionID.ToString(), profile.Personal.UserID.ToString(), profile.DBConnection._constr);
            productClient.Close();
            ResetUserControl();
        }

        protected void setActiveTab(int ActiveTab)
        {
            if (ActiveTab == 0)
            {
                TabPanelProductList.Visible = true;
                tabProductDetails.Visible = false;
                tabSpecification.Visible = false;
                tabTaxSetup.Visible = false;
                tabImages.Visible = false;
                Bindropdown();
                tabDocuments.Visible = false;
                tabInventory.Visible = false;
                TabBOM.Visible = false;
                Tabpack.Visible = false;
                tabVendor.Visible = false;
                TabContainerProductMaster.ActiveTabIndex = 0;
            }
            else if (ActiveTab == 1)
            {
                tabProductDetails.Visible = true;
                tabSpecification.Visible = false;
                //tabTaxSetup.Visible = true;
                tabTaxSetup.Visible = false;
                tabImages.Visible = true;
                tabDocuments.Visible = false;
                tabInventory.Visible = true;
                TabBOM.Visible = true;
                Tabpack.Visible = true;
                tabVendor.Visible = false;
                TabContainerProductMaster.ActiveTabIndex = 1;
            }
            else
            {
                TabPanelProductList.Visible = true;
                tabProductDetails.Visible = true;
                tabSpecification.Visible = false;
                tabTaxSetup.Visible = false;
                tabImages.Visible = true;
                Bindropdown();
                tabDocuments.Visible = false;
                tabInventory.Visible = true;
                TabBOM.Visible = false;
                Tabpack.Visible = true;
                tabVendor.Visible = false;
                TabContainerProductMaster.ActiveTabIndex = 1;
            }
        }
        #endregion

        #region  checkDuplicate
        public string checkDuplicate()
        {
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                iProductMasterClient productClient = new iProductMasterClient();
                string result = "";
                if (hdnprodID.Value == string.Empty)
                {
                    result = productClient.checkDuplicateRecord(txtProductName.Text.Trim(), profile.DBConnection._constr);
                    if (result != string.Empty)
                    {
                        WebMsgBox.MsgBox.Show(result);
                        //txtProductName.Text = "";
                    }
                    txtProductName.Focus();

                    result = productClient.checkDuplicateRecordSKUCODE(txtomsskucode.Text.Trim(), profile.DBConnection._constr);
                    if (result != string.Empty)
                    {
                        WebMsgBox.MsgBox.Show(result);
                    }
                    txtomsskucode.Focus();
                }
                else
                {
                    result = productClient.checkDuplicateRecordEdit(Convert.ToInt32(hdnprodID.Value), txtProductName.Text.Trim(), profile.DBConnection._constr);
                    if (result != string.Empty)
                    {
                        WebMsgBox.MsgBox.Show(result);
                        //txtProductName.Text = "";
                    }

                    result = productClient.checkDuplicateRecordEditSKUCODE(Convert.ToInt32(hdnprodID.Value), txtomsskucode.Text.Trim(), profile.DBConnection._constr);
                    if (result != string.Empty)
                    {
                        WebMsgBox.MsgBox.Show(result);
                    }
                }
                return result;
            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "Leads Source master", "checkDuplicate");
                string result = "";
                return result;
            }
            finally
            {
            }
        }

        #endregion

        [WebMethod]
        public static List<vGetProductSubCagetoryList> PMprint_ProductSubCategory(long ProductSubCategoryID)
        {
            CustomProfile profile = CustomProfile.GetProfile();
            ProductSubCategoryService.iProductSubCategoryMasterClient productsubcategoryClient = new ProductSubCategoryService.iProductSubCategoryMasterClient();
            List<vGetProductSubCagetoryList> SubCategoryList = new List<vGetProductSubCagetoryList>();
            SubCategoryList = productsubcategoryClient.GetProductSubCategoryByProductCategoryID(ProductSubCategoryID, profile.DBConnection._constr).ToList();
            return SubCategoryList;
        }

        #region Rate History Code
        [WebMethod]
        public static string PMSaveNewRates(object newPrice0)
        {
            string result = "";
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary = (Dictionary<string, object>)newPrice0;

                mProductRateDetail newPrice = new mProductRateDetail();
                if (dictionary["ProdID"].ToString() != "0" && dictionary["ProdID"].ToString() != "")
                {
                    newPrice.ProdID = Convert.ToInt64(dictionary["ProdID"].ToString());
                    newPrice.Rate = Convert.ToDecimal(dictionary["Rate"].ToString());
                    if (dictionary["EffectiveDate"] != null) newPrice.EffectiveDate = Convert.ToDateTime(dictionary["EffectiveDate"]);
                    if (dictionary["StartDate"] != null) newPrice.StartDate = Convert.ToDateTime(dictionary["StartDate"]);
                    if (dictionary["EndDate "] != null) newPrice.EndDate = Convert.ToDateTime(dictionary["EndDate"]);
                    result = "";
                }
                else
                {
                    result = "Please save the product / part first";
                }
            }
            catch (Exception ex) { }
            finally { }
            return result;
        }

        protected void GVRateHistory()
        {
            iProductMasterClient productClient = new iProductMasterClient();
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                // GVRateHistory1.DataSource = null;
                // GVRateHistory1.DataBind();

                if (hdnprodID.Value != "" && hdnprodID.Value != "0")
                {
                    //GVRateHistory1.DataSource = productClient.GetProductRateHistory(Convert.ToInt64(hdnprodID.Value), profile.DBConnection._constr);
                    //GVRateHistory1.DataBind();
                }
            }
            catch (Exception ex) { Login.Profile.ErrorHandling(ex, "ProductMaster", "GVRateHistory"); }
            finally { productClient.Close(); }
        }

        protected void GVRateHistory_OnRebind(object sender, EventArgs e)
        {
            GVRateHistory();
        }
        #endregion

        #region Inventory code

        public void FillInventoryGrid(long DeptId, long ProductId)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            CustomProfile profile = CustomProfile.GetProfile();
            iProductMasterClient productClient = new iProductMasterClient();
            try
            {
                ds = productClient.FillInventoryGrid(DeptId, ProductId, profile.DBConnection._constr);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    GVInventory.DataSource = ds.Tables[0];
                    GVInventory.DataBind();
                }
                else
                {
                    GVInventory.DataSource = null;
                    GVInventory.DataBind();

                }
            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "Productmaster", "FillInventoryGrid");
            }
            finally
            {
                productClient.Close();
            }
        }



        //protected void FillInventoryGrid()
        //{
        //    iProductMasterClient objService = new iProductMasterClient();
        //    try
        //    {
        //        CustomProfile profile = CustomProfile.GetProfile();
        //        List<SP_GetSiteWiseInventoryByProductIDs_Result> InventoryList = new List<SP_GetSiteWiseInventoryByProductIDs_Result>();
        //        InventoryList = objService.GetInventoryDataByProductIDs(hdnprodID.Value, Session.SessionID, profile.Personal.UserID.ToString(), CurrentObject, profile.DBConnection._constr).ToList();
        //        GVInventory.DataSource = null;
        //        GVInventory.DataBind();
        //        GVInventory.DataSource = InventoryList;
        //        GVInventory.DataBind();

        //        if (InventoryList.Count > 0)
        //        {
        //            if (InventoryList[0].EffectiveDate != null)
        //            {
        //               // UC_EffectiveDateInventory.Date = InventoryList[0].EffectiveDate;
        //            }
        //        }
        //    }
        //    catch { }
        //    finally { objService.Close(); }
        //}

        [WebMethod]
        public static long WMUpdateInventoryQty(long SiteID, long OpeningStock, long MaxStockLimit, long ReorderQty)
        {
            iProductMasterClient objService = new iProductMasterClient();
            long AvailableBalance = 0;
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                SP_GetSiteWiseInventoryByProductIDs_Result rec = new SP_GetSiteWiseInventoryByProductIDs_Result();
                rec.SiteID = SiteID;
                rec.OpeningStock = OpeningStock;
                rec.ReorderQty = ReorderQty;
                rec.MaxStockLimit = MaxStockLimit;
                AvailableBalance = objService.UpdateProductInvetory_TempData(HttpContext.Current.Session.SessionID.ToString(), CurrentObject, profile.Personal.UserID.ToString(), rec, profile.DBConnection._constr);
            }
            catch { }
            finally { objService.Close(); }
            return AvailableBalance;
        }
        #endregion

        # region New Code For WMS

        private void loadstring()
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(Session["Lang"].ToString());
                rm = new ResourceManager("PowerOnRentwebapp.App_GlobalResources.Lang", Assembly.GetExecutingAssembly());
                ci = Thread.CurrentThread.CurrentCulture;
                lblskulist.Text = rm.GetString("SKUList", ci);
                tabProductDetails.HeaderText = rm.GetString("SKUDetails", ci);
                lblskucode.Text = rm.GetString("SKUCodeWms", ci);
                lblskuname.Text = rm.GetString("SKUName", ci);
                lblcost.Text = rm.GetString("Cost", ci);
                lblpriciprice.Text = rm.GetString("PrincipalPrice", ci);
                lblactive.Text = rm.GetString("Active", ci);
                rbtYes.Text = rm.GetString("Yes", ci);
                rbtNo.Text = rm.GetString("No", ci);
                lblgrpset.Text = rm.GetString("GroupSet", ci);
                lblspecifictn.Text = rm.GetString("SKUSpecificationList", ci);
                tabImages.HeaderText = rm.GetString("Images", ci);
                lblimagetitle.Text = rm.GetString("ImageTitle", ci);
                lblimageselect.Text = rm.GetString("SelectImage", ci);
                lblimagedescri.Text = rm.GetString("ImageDescription", ci);
                lblactive.Text = rm.GetString("Active", ci);
                rbtnYes1.Text = rm.GetString("Yes", ci);
                rbtnNo1.Text = rm.GetString("No", ci);
                lblimages.Text = rm.GetString("Images", ci);
                tabInventory.HeaderText = rm.GetString("Inventory", ci);
                lbldeptinventry.Text = rm.GetString("DepartmentwiseInventory", ci);
                TabBOM.HeaderText = rm.GetString("BOM", ci);
                lblbomsku.Text = rm.GetString("SelectBOMSKU", ci);
                lblquantity.Text = rm.GetString("Quantity", ci);
                lblrwmark.Text = rm.GetString("Remark", ci);
                btnsubmit.Text = rm.GetString("Submit", ci);
                lblbomskulist.Text = rm.GetString("BOMSKUList", ci);
                Tabpack.HeaderText = rm.GetString("Pack", ci);
                btnadd.Text = rm.GetString("Add", ci);
                UCFormHeader1.FormHeaderText = rm.GetString("SKUMaster", ci);
                lbldescri.Text = rm.GetString("Description", ci);
                TabPanelProductList.HeaderText = rm.GetString("SKUList", ci);
                lblCompany.Text = rm.GetString("company", ci);
                lbldepartment.Text = rm.GetString("Department", ci);
                lblomsskucode.Text = rm.GetString("SKUcode", ci);
                lblopeningbal.Text = rm.GetString("OpeningBalance", ci);
                lblopbaldate.Text = rm.GetString("Date", ci);
                lblactive1.Text = rm.GetString("Active", ci);
                btnProductMasterUploadImg.Text = rm.GetString("UploadImage", ci);

                lblpack.Text = rm.GetString("Pack", ci);
                btnvirtualQty.Text = rm.GetString("AddVirtualQuantity", ci);


                lblBomSequence.Text = rm.GetString("Sequance", ci);
                lblskutype.Text = rm.GetString("SKUType", ci);
            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "Productmaster", "imgBtnEdit_Click");
            }
            finally { }
        }

        public void GetCompany()
        {
            DataSet ds;
            CustomProfile profile = CustomProfile.GetProfile();
            iProductMasterClient productClient = new iProductMasterClient();

            iUCCommonFilterClient objService = new iUCCommonFilterClient();

            try
            {
                //ds = productClient.GetCompanyname(profile.DBConnection._constr);
                //ddlcompany.DataSource = ds;
                //ddlcompany.DataTextField = "Name";
                //ddlcompany.DataValueField = "ID";
                //ddlcompany.DataBind();
                //ListItem lst = new ListItem { Text = "-Select-", Value = "0" };
                //ddlcompany.Items.Insert(0, lst);

                List<mCompany> CompanyLst = new List<mCompany>();
                string UserType = profile.Personal.UserType.ToString();
                long UID = profile.Personal.UserID;
                if (UserType == "Admin")
                {
                    CompanyLst = objService.GetUserCompanyName(UID, profile.DBConnection._constr).ToList();
                }
                else if (UserType == "User" || UserType == "Requester And Approver" || UserType == "Requester")
                {
                    CompanyLst = objService.GetUserCompanyName(UID, profile.DBConnection._constr).ToList();
                }
                else
                {
                    CompanyLst = objService.GetCompanyName(profile.DBConnection._constr).ToList();
                }
                ddlcompany.DataSource = CompanyLst;
                ddlcompany.DataBind();

                if (UserType == "Admin")
                {
                    ListItem lstCmpny = new ListItem { Text = "-Select-", Value = "0" };
                    ddlcompany.Items.Insert(0, lstCmpny);
                    if (ddlcompany.Items.Count > 0) ddlcompany.SelectedIndex = 1; hdncompanyid.Value = ddlcompany.SelectedValue;

                    List<mTerritory> SiteLst = new List<mTerritory>();
                    iUCCommonFilterClient UCCommonFilter = new iUCCommonFilterClient();

                    SiteLst = UCCommonFilter.GetSiteNameByUserID(Convert.ToInt16(UID), profile.DBConnection._constr).ToList();

                    ddldepartment.DataSource = SiteLst;
                    ddldepartment.DataBind();
                    if (ddldepartment.Items.Count > 0) ddldepartment.SelectedIndex = 1;
                    hdndeptid.Value = ddldepartment.SelectedValue;

                }
                else if (UserType == "User" || UserType == "Requester And Approver" || UserType == "Requester")
                {
                    ListItem lstCmpny = new ListItem { Text = "-Select-", Value = "0" };
                    ddlcompany.Items.Insert(0, lstCmpny);
                    if (ddlcompany.Items.Count > 0) ddlcompany.SelectedIndex = 1; hdncompanyid.Value = ddlcompany.SelectedValue;


                    List<mTerritory> SiteLst = new List<mTerritory>();
                    iUCCommonFilterClient UCCommonFilter = new iUCCommonFilterClient();

                    //SiteLst = UCCommonFilter.GetDepartmentListUserWise(Convert.ToInt16(UID), profile.DBConnection._constr).ToList();
                    SiteLst = UCCommonFilter.GetSiteNameByUserID(Convert.ToInt16(UID), profile.DBConnection._constr).ToList();

                    ddldepartment.DataSource = SiteLst;
                    ddldepartment.DataBind();
                    if (ddldepartment.Items.Count > 0) ddldepartment.SelectedIndex = 1;
                    hdndeptid.Value = ddldepartment.SelectedValue;

                }
                else
                {
                    ListItem lstCmpny = new ListItem { Text = "-Select-", Value = "0" };
                    ddlcompany.Items.Insert(0, lstCmpny);
                    if (ddlcompany.Items.Count > 0) ddlcompany.SelectedIndex = 1; hdncompanyid.Value = ddlcompany.SelectedValue;
                    List<mTerritory> SiteLst = new List<mTerritory>();
                    iUCCommonFilterClient UCCommonFilter = new iUCCommonFilterClient();

                    SiteLst = UCCommonFilter.GetSiteNameByUserID(Convert.ToInt16(UID), profile.DBConnection._constr).ToList();

                    ddldepartment.DataSource = SiteLst;
                    ddldepartment.DataBind();
                    if (ddldepartment.Items.Count > 0) ddldepartment.SelectedIndex = 1;
                    hdndeptid.Value = ddldepartment.SelectedValue;
                }

            }
            catch (Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "Productmaster", "GetCompany");
            }
            finally { productClient.Close(); }
        }

        public void getDepartment(long companyid)
        {
            DataSet ds;
            CustomProfile profile = CustomProfile.GetProfile();
            iProductMasterClient productClient = new iProductMasterClient();
            try
            {

                ds = productClient.GetDepartment(companyid, profile.DBConnection._constr);
                ddldepartment.DataSource = ds;
                ddldepartment.DataTextField = "Territory";
                ddldepartment.DataValueField = "ID";
                ddldepartment.DataBind();
                ListItem lst = new ListItem { Text = "-Select-", Value = "0" };
                ddldepartment.Items.Insert(0, lst);
            }
            catch (Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "Productmaster", "getDepartment");
            }
            finally { productClient.Close(); }
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
                Login.Profile.ErrorHandling(ex, "Productmaster", "GetDepartment");
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

        protected void Grid2_RebindGrid(object sender, EventArgs e)
        {
            if (hdnstate.Value == "Edit")
            {
                GetUOMPackById();
            }
            else
            {
                BindgridForPackUOM();
            }
        }

        protected void GVInventory_RebindGrid(object sender, EventArgs e)
        {
            FillInventoryGrid(long.Parse(hdndeptid.Value), long.Parse(hdnprodID.Value));
        }

        public void BindgridForPackUOM()
        {

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            CustomProfile profile = CustomProfile.GetProfile();
            iProductMasterClient productClient = new iProductMasterClient();
            try
            {
                ds = productClient.GetUOMPackDetails(profile.DBConnection._constr);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    Grid2.DataSource = ds.Tables[0];
                    Grid2.DataBind();
                }
                else
                {
                    Grid2.DataSource = null;
                    Grid2.DataBind();

                }
            }
            catch (Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "Productmaster", "BindgridForPackUOM");
            }
            finally { productClient.Close(); }
        }

        protected void grdaccessdele_RebindGrid(object sender, EventArgs e)
        {
            BindBomgrid(hdnstate.Value, long.Parse(hdnprodID.Value));
        }

        public void BindBomgrid(string state, long prodId)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            CustomProfile profile = CustomProfile.GetProfile();
            iProductMasterClient productClient = new iProductMasterClient();
            try
            {
                ds = productClient.GetBomProductDetail(state, prodId, profile.DBConnection._constr);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    grdaccessdele.DataSource = ds.Tables[0];
                    grdaccessdele.DataBind();
                }
                else
                {
                    grdaccessdele.DataSource = null;
                    grdaccessdele.DataBind();

                }
            }
            catch (Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "Productmaster", "BindBomgrid");
            }
            finally
            { productClient.Close(); }
        }

        [WebMethod]
        public static string SaveBomDetail(object objReq)
        {
            string result = "";
            iProductMasterClient productClient = new iProductMasterClient();
            CustomProfile profile = CustomProfile.GetProfile();
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary = (Dictionary<string, object>)objReq;
                long hdnproductsearchId = long.Parse(dictionary["hdnproductsearchId"].ToString());
                long Quantity = long.Parse(dictionary["Quantity"].ToString());
                string BomRemark = dictionary["BomRemark"].ToString();
                string TxtProduct = dictionary["TxtProduct"].ToString();
                string hdnstate = dictionary["hdnstate"].ToString();

                long Sequence = long.Parse(dictionary["Sequence"].ToString());

                long BOmDetailId = 0;
                if (hdnstate == "Edit")
                {
                    if (HttpContext.Current.Session["hdnedit"].ToString() == "Edit")
                    {
                        string hdnbomeditstate = HttpContext.Current.Session["hdnedit"].ToString();
                        hdnstate = hdnbomeditstate.ToString();
                        BOmDetailId = long.Parse(HttpContext.Current.Session["hdnnewDelegateid"].ToString());
                        HttpContext.Current.Session["hdnnewDelegateid"] = "";
                        HttpContext.Current.Session["hdnedit"] = "";
                    }
                    else
                    {
                        hdnstate = "Add";
                    }
                    //BOmDetailId = long.Parse(dictionary["BomDetailId"].ToString());

                }
                else
                {
                    if (HttpContext.Current.Session["hdnedit"].ToString() == "Edit")
                    {
                        string hdnbomeditstate = HttpContext.Current.Session["hdnedit"].ToString();
                        hdnstate = hdnbomeditstate.ToString();
                        BOmDetailId = long.Parse(HttpContext.Current.Session["hdnnewDelegateid"].ToString());
                        HttpContext.Current.Session["hdnnewDelegateid"] = "";
                        HttpContext.Current.Session["hdnedit"] = "";
                    }
                }
                long BomHeaderId = 0;
                //long Sequence = 0;
                productClient.saveBomDetail(BomHeaderId, hdnproductsearchId, Quantity, Sequence, BomRemark, hdnstate, BOmDetailId, profile.DBConnection._constr);
            }
            catch (Exception ex)
            {

                Login.Profile.ErrorHandling(ex, "Productmaster", "SaveBomDetail");
            }
            finally
            { productClient.Close(); }
            return result;
        }

        [WebMethod]
        public static string RemoveSku(object objReq)
        {
            string result = "";
            iProductMasterClient productClient = new iProductMasterClient();
            CustomProfile profile = CustomProfile.GetProfile();
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            dictionary = (Dictionary<string, object>)objReq;

            long BOMDetailsku = long.Parse(dictionary["SkuDetailId"].ToString());
            productClient.RemoveBOMDetailSKu(BOMDetailsku, profile.DBConnection._constr);
            result = "success";
            return result;
        }

        //protected void grdaccessdele_Select(object sender, Obout.Grid.GridRecordEventArgs e)
        //{
        //    try
        //    {
        //        CustomProfile profile = CustomProfile.GetProfile();
        //        Hashtable selectedrec = (Hashtable)grdaccessdele.SelectedRecords[0];
        //        BOmDetailId = long.Parse(selectedrec["Id"].ToString());
        //        hdnstate.Value = "Edit";
        //        GetBomDetail(BOmDetailId);
        //    }
        //    catch { }
        //    finally { }
        //}

        public void GetBomDetail(long BOMDetailId)
        {
            iProductMasterClient productClient = new iProductMasterClient();
            CustomProfile profile = CustomProfile.GetProfile();
            try
            {
                DataSet ds;
                DataTable dt;
                ds = productClient.GetBOMDetailById(BOMDetailId, profile.DBConnection._constr);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    txtbomsku.Text = dt.Rows[0]["productCode"].ToString();
                    txtQuantity.Text = dt.Rows[0]["Quantity"].ToString();
                    txtremarkbom.Text = dt.Rows[0]["Remark"].ToString();
                    hdnproductsearchId.Value = dt.Rows[0]["SKUId"].ToString();
                    txtBOMSequence.Text = dt.Rows[0]["Sequence"].ToString();
                }
            }
            catch (Exception ex)
            {

                Login.Profile.ErrorHandling(ex, this, "Productmaster", "GetBomDetail");
            }
            finally
            { productClient.Close(); }

        }

        protected void imgBtnEditbom_OnClick(object sender, ImageClickEventArgs e)
        {
            ImageButton imgbtn = (ImageButton)sender;
            // clear();
            hdnBomDetailId.Value = imgbtn.ToolTip.ToString();
            GetBomDetail(long.Parse(hdnBomDetailId.Value));
            //GetProductDetailByProductID();
            //setActiveTab(1);
            Session["hdnedit"] = "Edit";
            hdnbomeditstate.Value = "Edit";
            Session.Add("hdnnewDelegateid", hdnBomDetailId.Value);


        }

        public void GetUOMPackById()
        {

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            CustomProfile profile = CustomProfile.GetProfile();
            iProductMasterClient productClient = new iProductMasterClient();
            ds = productClient.GetUOMPackDetailsByProdId(long.Parse(hdnprodID.Value), profile.DBConnection._constr);
            dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                Grid2.DataSource = ds.Tables[0];
                Grid2.DataBind();
            }
            else
            {
                Grid2.DataSource = null;
                Grid2.DataBind();

            }
        }

        public void GetBOmDeyailbyIdforedit()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            CustomProfile profile = CustomProfile.GetProfile();
            iProductMasterClient productClient = new iProductMasterClient();
            try
            {
                ds = productClient.GetBOmDeyailbyIdforedit(long.Parse(hdnprodID.Value), profile.DBConnection._constr);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    grdaccessdele.DataSource = ds.Tables[0];
                    grdaccessdele.DataBind();
                }
                else
                {
                    grdaccessdele.DataSource = null;
                    grdaccessdele.DataBind();

                }
            }
            catch (Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "Productmaster", "GetBOmDeyailbyIdforedit");
            }
            finally
            { productClient.Close(); }
        }

        public void DeleteMpackUOMClear()
        {
            CustomProfile profile = CustomProfile.GetProfile();
            iProductMasterClient productClient = new iProductMasterClient();
            productClient.DeleteMpackUOMBOMClear(profile.DBConnection._constr);

        }

        //protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        //{
        //    hdnstate.Value = "Edit";
        //    GetBomDetail(long.Parse(hdnBomDetailId.Value));
        //}

        public decimal CalculateBonStock(long Prodid)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            CustomProfile profile = CustomProfile.GetProfile();
            iProductMasterClient productClient = new iProductMasterClient();
            decimal availablebalance = 150;
            long[] values = null;
            try
            {
                List<long> list = new List<long>();
                string MinQty = "";
                ds = productClient.GetBOmDeyailbyIdforedit(long.Parse(hdnprodID.Value), profile.DBConnection._constr);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        long SKUID = 0;
                        long Quantity = 0;
                        long Answer = 0;

                        SKUID = long.Parse(dt.Rows[i]["SKUId"].ToString());
                        Quantity = long.Parse(dt.Rows[i]["Quantity"].ToString());
                        decimal availQty = productClient.GetAvailStockById(SKUID, profile.DBConnection._constr);
                        if (availQty != 0)
                        {
                            long div = Convert.ToInt64(availQty);
                            Answer = (div / Quantity);
                        }

                        list.Add(Answer);

                        if (i == 0)
                        {
                            MinQty = Convert.ToString(Answer);
                        }
                        else
                        {
                            MinQty = MinQty + "|" + Convert.ToString(Answer);
                        }
                    }

                    string FnlQty = MinQty;
                    long ABalance = productClient.GetAVLBalance(MinQty, profile.DBConnection._constr);
                    values = list.ToArray();
                    long min = values.Min();
                    //  availablebalance = decimal.Parse(min.ToString());

                    availablebalance = decimal.Parse(ABalance.ToString());
                }
            }
            catch (Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "Productmaster", "CalculateBonStock");
            }
            finally
            {
                productClient.Close();
            }
            return availablebalance;
        }
        #endregion

        #region serial flag update 
        [WebMethod]
        public static string Updateserialflag(string SelectedSKUIds)
        {
            string result="";
            CustomProfile profile = CustomProfile.GetProfile();
            iProductMasterClient productClient = new iProductMasterClient();
            try
            {
                string[] skuids = SelectedSKUIds.Split(',');
                for(int i = 0; i < skuids.Length; i++)
                {
                    long SKUID = Convert.ToInt64(skuids[i].ToString());
                    productClient.UpdateserialflagProduct(SKUID, profile.DBConnection._constr); 
                }
                result = "Success";
            }
            catch(Exception ex)
            {
                // Login.Profile.ErrorHandling(ex,"",  "Product master", "GetProductDetailByProductID");
                result = "Fail";
            }
            finally
            {
                productClient.Close();
            }
            return result;
        }


        #endregion



    }
}
