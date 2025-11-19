using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data.SqlClient;
using PowerOnRentwebapp.Login;
using PowerOnRentwebapp.ToolbarService;
using System.Web.Services;
using System.Configuration;
using System.IO;
using System.Data;
using System.Resources;
using System.Globalization;
using System.Threading;
using System.Reflection;
using System.Drawing;
using PowerOnRentwebapp.ProductMasterService;
using PowerOnRentwebapp.PORServiceUCCommonFilter;

namespace PowerOnRentwebapp.Product
{
    public partial class VSkuImportD : System.Web.UI.Page
    {
        ResourceManager rm;
        CultureInfo ci;
        SqlConnection con1 = new SqlConnection("");
        long value = 7;
        protected void Page_Load(object sender, EventArgs e)
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

            pnlImport.Visible = true;
            //Up_PnlGirdProduct.Visible = true;
            //btnimpnext.Enabled = false;
            //btnimpnext.CssClass = "class1";
            btnimportNext.Enabled = false;
            btnimportNext.CssClass = "class1";
            uploadMessage.Visible = false;
            lblmessagesuccess.Visible = false;
            if (!IsPostBack)
            {
                string Object = "";
                hdnobject.Value = Object;
            }
        }


        private void loadstring()
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(Session["Lang"].ToString());
                rm = new ResourceManager("PowerOnRentwebapp.App_GlobalResources.Lang", Assembly.GetExecutingAssembly());
                ci = Thread.CurrentThread.CurrentCulture;

                lblimportdata.Text = rm.GetString("ImportData", ci);              
                lblstep1.Text = rm.GetString("UploadFile", ci);
                lblstep2.Text = rm.GetString("DataValidationVerification", ci);
                lblstep3.Text = rm.GetString("Finished", ci);
                lbltext1.Text = rm.GetString("VSkuImportPrice", ci);
                Lbl.Text = rm.GetString("VSkuImportPrice1", ci);
                lblSelecFile.Text = rm.GetString("SelectImportFile", ci);
                btnupload.Text = rm.GetString("Upload", ci);
                lbluploading.Text = rm.GetString("Uploadingfile", ci);
                lblmessagesuccess.Text = rm.GetString("Fileuploadsucessfully", ci);
                btnimportNext.Text = rm.GetString("Next", ci);
                btnimportcancel.Text = rm.GetString("Cancel", ci);
                lblHeading.Text = rm.GetString("ValidateData", ci);

                Label1.Text = rm.GetString("UploadFile", ci);
                Label2.Text = rm.GetString("DataValidationVerification", ci);
                Label3.Text = rm.GetString("Finished", ci);
                lbladdresslist.Text = rm.GetString("ImportList", ci);

                btnnext.Text = rm.GetString("Next", ci);
                btnback.Text = rm.GetString("back", ci);
                Label4.Text = rm.GetString("ImportFinish", ci);

                Label5.Text = rm.GetString("UploadFile", ci);
                Label6.Text = rm.GetString("DataValidationVerification", ci);
                Label7.Text = rm.GetString("Finished", ci);
                btnfinish.Text = rm.GetString("Finish", ci);
                Label8.Text = rm.GetString("DataImporting", ci);
            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "Account Master", "loadstring");
            }
        }

        protected void btnUploadPo_Click(object sender, EventArgs e)
        {
            con1.ConnectionString = ConfigurationManager.ConnectionStrings["elegantcrm7ConnectionString"].ConnectionString;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            iProductMasterClient productClient = new iProductMasterClient();
            CustomProfile profile = CustomProfile.GetProfile();
            try
            {
                    productClient.DeleteTempVirtualSKUImport(profile.Personal.UserID, profile.DBConnection._constr);
                    string connString = "";
                // string path = FileuploadPO.PostedFile.FileName;

                string path = FileuploadPO.FileName;

                string strFileType = System.IO.Path.GetExtension(path).ToString().ToLower();
                  string Fullpath = Server.MapPath("~/Product/ImportPrice/" + path);

               // string Fullpath = @"C:\GWCVersion2\GWCTestVersion2\Product\ImportPrice\" + path;
                // Label1.Text = "";
                 FileuploadPO.PostedFile.SaveAs(Server.MapPath("~/Product/ImportPrice/" + path));
               // FileuploadPO.PostedFile.SaveAs(@"C:\GWCVersion2\GWCTestVersion2\Product\ImportPrice\" + path);


                if (strFileType.Trim() == ".xls")
                    {
                        connString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Fullpath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                    }
                    else if (strFileType.Trim() == ".xlsx")
                    {
                        connString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Fullpath + ";Extended Properties='Excel 12.0;HDR=Yes'";
                    }
                    using (OleDbConnection excelConnection = new OleDbConnection(connString))
                    {
                        using (OleDbCommand cmd1 = new OleDbCommand("Select * from [Template$]", excelConnection))
                        {

                            excelConnection.Open();
                            OleDbDataReader dReader;
                            dReader = cmd1.ExecuteReader();
                            SqlBulkCopy sqlBulk = new SqlBulkCopy(con1);
                            con1.Open();
                            sqlBulk.DestinationTableName = "TempVirtualSkuImport";
                            sqlBulk.WriteToServer(dReader);
                            excelConnection.Close();
                            productClient.UpdateUserIDTempVirtualSKU(profile.Personal.UserID, profile.DBConnection._constr);

                            ds = productClient.GetVirtualSKUTemp(profile.Personal.UserID, profile.DBConnection._constr);
                            dt = ds.Tables[0];

                            if (dt.Rows.Count > 0)
                            {
                                btnimportNext.Enabled = true;
                                btnimportNext.CssClass = "class2";
                                uploadMessage.Visible = true;
                                lblmessagesuccess.Visible = true;
                            }
                            else
                            {
                                WebMsgBox.MsgBox.Show("Upload Not Successful, Please Upload Right Template");
                                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Upload Not Successful,Please upload Right template!');", true);
                            }
                        }
                    }

            }
            catch (Exception ex)
            {
                
                ScriptManager.RegisterStartupScript(this, GetType(), "showAlert", "showAlert('" + ex.ToString() + "','Error','#');", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Exception in Catch!');", true);
            }
            finally {
                con1.Close();
                productClient.Close();
                
                ds.Dispose();
                dt.Dispose();
            }
        }

        protected void btnimportNext_Click(object sender, EventArgs e)
        {
            pnlImport.Visible = false;
            pnlvalidate.Visible = true;
            DisplayGrid();
        }

        protected void btnimportcancel_Click(object sender, EventArgs e)
        {
            using (iProductMasterClient productClient = new iProductMasterClient())
            {
                CustomProfile profile = CustomProfile.GetProfile();
                productClient.DeleteTempVirtualSKUImport(profile.Personal.UserID, profile.DBConnection._constr);
            }
        }

        protected void btnnext_Click(object sender, EventArgs e)
        {
            using (iProductMasterClient productClient = new iProductMasterClient())
            {
                CustomProfile profile = CustomProfile.GetProfile();
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                string createdby = profile.Personal.UserID.ToString();
                ds = productClient.validateVSKUImportData(profile.Personal.UserID, profile.DBConnection._constr);
                dt = ds.Tables[0];
                for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    string ProductCode = ds.Tables[0].Rows[i]["SKUCode"].ToString();
                    string Name = ds.Tables[0].Rows[i]["Name"].ToString();
                    string Description = ds.Tables[0].Rows[i]["Description"].ToString();
                    decimal PrinciplePrice = decimal.Parse(ds.Tables[0].Rows[i]["PrinciplePrice"].ToString());
                    string DepartmentCode = ds.Tables[0].Rows[i]["DepartmentCode"].ToString();
                    string Packkey = ds.Tables[0].Rows[i]["Packkey"].ToString();
                    decimal VirtualSkuQty = decimal.Parse(ds.Tables[0].Rows[i]["VirtualSkuQty"].ToString());
                    decimal VirtualReOrderQty = decimal.Parse(ds.Tables[0].Rows[i]["VirtualReOrderQty"].ToString());
                    long DeptID = long.Parse(ds.Tables[0].Rows[i]["DeptID"].ToString());
                    long CompanyId= long.Parse(ds.Tables[0].Rows[i]["parentid"].ToString());
                    productClient.InsertIntomProduct(ProductCode, Name, Description, PrinciplePrice, DeptID, Packkey, createdby, VirtualSkuQty, VirtualReOrderQty, CompanyId, profile.DBConnection._constr);

                }
            }

            pnlImport.Visible = false;
            pnlvalidate.Visible = false;
            pnlfinish.Visible = true;

        }

        protected void btnback_Click(object sender, EventArgs e)
        {
            using (iProductMasterClient productClient = new iProductMasterClient())
            {
                CustomProfile profile = CustomProfile.GetProfile();
                productClient.DeleteTempVirtualSKUImport(profile.Personal.UserID, profile.DBConnection._constr);
                pnlvalidate.Visible = false;
                pnlImport.Visible = true;
                lblbackMessage.Text = "";

            }
        }

        protected void btnfinish_Click(object sender, EventArgs e)
        {
            using (iProductMasterClient productClient = new iProductMasterClient())
            {
                pnlImport.Visible = true;
                pnlvalidate.Visible = false;
                pnlfinish.Visible = false;
                CustomProfile profile = CustomProfile.GetProfile();
                productClient.DeleteTempVirtualSKUImport(profile.Personal.UserID, profile.DBConnection._constr);
            }
        }

        public void DisplayGrid()
        {
            using (iProductMasterClient productClient = new iProductMasterClient())
            {
                CustomProfile profile = CustomProfile.GetProfile();
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                lblOkMessage.Text = "";
                ds = productClient.validateVSKUImportData(profile.Personal.UserID, profile.DBConnection._constr);
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    Grid1.DataSource = ds.Tables[0];
                    Grid1.DataBind();
                }
                else
                {
                    value = 3;
                }
                if (value == 0)
                {
                    btnnext.Enabled = false;
                    btnnext.CssClass = "class1";
                    lblbackMessage.Text = "Colored Row SKU allready present in given department";
                }
                else if (value == 1)
                {
                    btnnext.Enabled = false;
                    btnnext.CssClass = "class1";
                    lblbackMessage.Text = "Colored Row SKU not Available in OMS.Please click on Back Button";
                }

                else if (value == 2)
                {
                    btnnext.Enabled = false;
                    btnnext.CssClass = "class1";
                    lblbackMessage.Text = "Characters and blank values not asccepted in Quantity columns";
                }
                else if (value == 3)
                {
                    btnnext.Enabled = false;
                    btnnext.CssClass = "class1";
                    lblbackMessage.Text = "Given Department code is not present in system";
                }
                else if (value == 4)
                {
                    btnnext.Enabled = false;
                    btnnext.CssClass = "class1";
                    lblbackMessage.Text = "SKU is Duplicate for same Department in Template";
                }
                else if (value == 5)
                {
                    btnnext.Enabled = false;
                    btnnext.CssClass = "class1";
                    lblbackMessage.Text = "Quantity Should not be blank, Zero or Character";
                }
                else if (value == 6)
                {
                    btnnext.Enabled = false;
                    btnnext.CssClass = "class1";
                    lblbackMessage.Text = "Virtual Re-Order Quantity Should not be blank or Character";
                }
                else
                {
                    lblOkMessage.Text = "All data are verified.Please click on Next Button ";
                    btnnext.Enabled = true;
                    btnnext.CssClass = "class2";
                }
            }
           
        }

        protected void GVImportView_OnRowDataBound(object sender, Obout.Grid.GridRowEventArgs e)
        {
            try
            {
                decimal CurrentStock = 0;            
                string Product = e.Row.Cells[9].Text;
                string SkuPresent = e.Row.Cells[10].Text;
                string Department = e.Row.Cells[11].Text;
                string Quantity = e.Row.Cells[12].Text;
                string Duplicate = e.Row.Cells[13].Text;
                string ReOrderQty= e.Row.Cells[14].Text;

                if (e.Row.Cells[7].Text != "" && double.Parse(e.Row.Cells[7].Text) != 0.00)
                {
                    CurrentStock = decimal.Parse(e.Row.Cells[7].Text.ToString());
                }
                else
                {
                    e.Row.BackColor = System.Drawing.Color.DarkCyan;
                    e.Row.ForeColor = System.Drawing.Color.White;
                    e.Row.Cells[7].BackColor = System.Drawing.Color.Tomato;
                    e.Row.Cells[7].ToolTip = "Quantity Should not be blank, Zero or Character";
                    value = 5;
                }

                if (ReOrderQty == "NotAvailable")
                {                   
                    e.Row.BackColor = System.Drawing.Color.DarkCyan;
                    e.Row.ForeColor = System.Drawing.Color.White;
                    e.Row.Cells[8].BackColor = System.Drawing.Color.Tomato;
                    e.Row.Cells[8].ToolTip = "Virtual Re-Order Quantity Should not be blank or Character";
                    value = 6;
                }

                if (Product == "NotAvailable")
                {
                    value = 1;
                    e.Row.BackColor = System.Drawing.Color.DarkCyan;
                    e.Row.ForeColor = System.Drawing.Color.White;
                    e.Row.Cells[1].BackColor = System.Drawing.Color.Tomato;
                    e.Row.Cells[1].ToolTip = "SKU code should not be blank";
                }

                if (SkuPresent == "AllreadyPresent")
                {
                    value = 0;
                    e.Row.BackColor = System.Drawing.Color.DarkCyan;
                    e.Row.ForeColor = System.Drawing.Color.White;
                    e.Row.Cells[1].BackColor = System.Drawing.Color.Tomato;
                    e.Row.Cells[1].ToolTip = "SKU code Allredy present in same department";
                }

                if (Department == "NotAvailable")
                {
                    value = 3;
                    e.Row.BackColor = System.Drawing.Color.DarkCyan;
                    e.Row.ForeColor = System.Drawing.Color.White;
                    e.Row.Cells[5].BackColor = System.Drawing.Color.Tomato;
                    e.Row.Cells[5].ToolTip = "Department Not Available";
                }
               
                if (Duplicate == "Duplicate")
                {
                    value = 4;
                    e.Row.BackColor = System.Drawing.Color.DarkCyan;
                    e.Row.ForeColor = System.Drawing.Color.White;
                    e.Row.Cells[1].BackColor = System.Drawing.Color.Tomato;
                    e.Row.Cells[5].BackColor = System.Drawing.Color.Tomato;
                    e.Row.Cells[1].ToolTip = "SKU is Duplicate for same Department in Template";
                    e.Row.Cells[5].ToolTip = "SKU is Duplicate for same Department in Template";
                }
            }
            catch (Exception ex)
            {
                value = 2;
                btnnext.Enabled = false;
                btnnext.CssClass = "class1";
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Only Numbers are allowed in Quantity columns');", true);
            }

        }
    }
}