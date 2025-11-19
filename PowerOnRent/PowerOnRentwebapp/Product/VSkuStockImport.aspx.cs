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
   
    public partial class VSkuStockImport : System.Web.UI.Page
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
            btnimportNext.Enabled = false;
            btnimportNext.CssClass = "class1";
            uploadMessage.Visible = false;
            lblmessagesuccess.Visible = false;
        }


        private void loadstring()
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(Session["Lang"].ToString());
                rm = new ResourceManager("PowerOnRentwebapp.App_GlobalResources.Lang", Assembly.GetExecutingAssembly());
                ci = Thread.CurrentThread.CurrentCulture;

                lblHeading.Text = rm.GetString("ValidateData", ci);

                lblstep1.Text = rm.GetString("UploadFile", ci);
                lblstep2.Text = rm.GetString("DataValidationVerification", ci);
                lblstep3.Text = rm.GetString("Finished", ci);
                lbluploading.Text = rm.GetString("Uploadingfile", ci);
                lbltext1.Text = rm.GetString("VskuStockimport", ci);
                Lbl.Text = rm.GetString("VskuStockimport1", ci);
                lblSelecFile.Text = rm.GetString("SelectImportFile", ci);
                btnupload.Text = rm.GetString("Upload", ci);
                lbluploading.Text = rm.GetString("Uploadingfile", ci);
                lblmessagesuccess.Text = rm.GetString("Fileuploadsucessfully", ci);

                btnimportNext.Text = rm.GetString("Next", ci);
                btnimportcancel.Text = rm.GetString("Cancel", ci);
                Label1.Text = rm.GetString("UploadFile", ci);
                Label2.Text = rm.GetString("DataValidationVerification", ci);
                Label3.Text = rm.GetString("Finished", ci);
                lbladdresslist.Text = rm.GetString("Finished", ci);

                btnnext.Text = rm.GetString("Next", ci);
                btnback.Text = rm.GetString("back", ci);
                Label4.Text = rm.GetString("ImportFinish", ci);

                Label5.Text = rm.GetString("UploadFile", ci);
                Label6.Text = rm.GetString("DataValidationVerification", ci);
                Label7.Text = rm.GetString("Finished", ci);
                btnfinish.Text = rm.GetString("Finish", ci);



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
           // try
           // {
                productClient.DeleteVistualStockImport(profile.Personal.UserID, profile.DBConnection._constr);
                string connString = "";
                // string path = FileuploadPO.PostedFile.FileName;
             
                string path = FileuploadPO.FileName;
               

                string strFileType = System.IO.Path.GetExtension(path).ToString().ToLower();
                //string Fullpath = Server.MapPath("~/Product/ImportPrice/" + path);
                //// Label1.Text = "";
                //FileuploadPO.PostedFile.SaveAs(Server.MapPath("~/Product/ImportPrice/" + path));
            
                  string Fullpath = Server.MapPath("../Product/ImportPrice/" + path);            
              //  string Fullpath = @"C:\GWCProductionVersion2\GWCProdV2\Product\ImportPrice\" + path;
                // Label1.Text = "";

                 FileuploadPO.PostedFile.SaveAs(Server.MapPath("../Product/ImportPrice/" + path));
               //  FileuploadPO.PostedFile.SaveAs(@"C:\GWCProductionVersion2\GWCProdV2\Product\ImportPrice\" + path);

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
                        sqlBulk.DestinationTableName = "TempVirtualStockImport";
                        sqlBulk.WriteToServer(dReader);
                        excelConnection.Close();
                        productClient.UpdateUserIDtoVirtualStock(profile.Personal.UserID, profile.DBConnection._constr);

                        ds = productClient.GetVirtualStocktemp(profile.Personal.UserID, profile.DBConnection._constr);
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

            //}
           /* catch (Exception ex)
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "showAlert", "showAlert('" + ex.ToString() + "','Error','#');", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Exception in Catch!');", true);
            }
            finally
            {*/
                con1.Close();
                productClient.Close();

                ds.Dispose();
                dt.Dispose();
           // }
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
                productClient.DeleteVistualStockImport(profile.Personal.UserID, profile.DBConnection._constr);
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
                ds = productClient.GetVirtualStocktemp(profile.Personal.UserID, profile.DBConnection._constr);
                dt = ds.Tables[0];
                for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    long ProdID = long.Parse(ds.Tables[0].Rows[i]["SKUId"].ToString());
                    long DeptID = long.Parse(ds.Tables[0].Rows[i]["DeptID"].ToString());
                    decimal VirtualSkuQty = decimal.Parse(ds.Tables[0].Rows[i]["VirtualQty"].ToString());
                    productClient.UpdateVirtualStockbyInport(ProdID, DeptID, VirtualSkuQty,profile.Personal.UserID, profile.DBConnection._constr);
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
                productClient.DeleteVistualStockImport(profile.Personal.UserID, profile.DBConnection._constr);
                pnlvalidate.Visible = false;
                pnlImport.Visible = true;
                lblbackMessage.Text = "";
            }
        }

        protected void btnfinish_Click(object sender, EventArgs e)
        {
            pnlImport.Visible = true;
            pnlvalidate.Visible = false;
            pnlfinish.Visible = false;
        }

        public void DisplayGrid()
        {
            using (iProductMasterClient productClient = new iProductMasterClient())
            {
                CustomProfile profile = CustomProfile.GetProfile();
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                lblOkMessage.Text = "";
                ds = productClient.GetVirtualStocktemp(profile.Personal.UserID, profile.DBConnection._constr);
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
                    lblbackMessage.Text = "Colored Row SKU not present in given department";
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
                    lblbackMessage.Text = "Quantity Should not be blank, Zero or Character";
                }
                //else if (value == 5)
                //{
                //    btnnext.Enabled = false;
                //    btnnext.CssClass = "class1";
                //    lblbackMessage.Text = "Quantity Should not be blank, Zero or Character";
                //}
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
                string Product = e.Row.Cells[4].Text;
                string ProdDeptChk = e.Row.Cells[5].Text;
                string Department = e.Row.Cells[6].Text;
                string Quantity = e.Row.Cells[7].Text;
                string ProductType = e.Row.Cells[8].Text;

                if (e.Row.Cells[3].Text != "" && double.Parse(e.Row.Cells[3].Text) != 0.00)
                {
                    CurrentStock = decimal.Parse(e.Row.Cells[3].Text.ToString());
                }
                else
                {
                   // e.Row.BackColor = System.Drawing.Color.DarkCyan;
                  //  e.Row.ForeColor = System.Drawing.Color.White;
                  //  e.Row.Cells[3].BackColor = System.Drawing.Color.Tomato;
                  //  e.Row.Cells[3].ToolTip = "Quantity Should not be blank, Zero or Character";
                 //   value = 5;
                }

                if (Product == "NotAvailable")
                {
                    value = 1;
                    e.Row.BackColor = System.Drawing.Color.DarkCyan;
                    e.Row.ForeColor = System.Drawing.Color.White;
                    e.Row.Cells[1].BackColor = System.Drawing.Color.Tomato;
                    e.Row.Cells[1].ToolTip = "SKU code should not be blank";
                }

                if (ProdDeptChk == "NotInDepartment")
                {
                    value = 0;
                    e.Row.BackColor = System.Drawing.Color.DarkCyan;
                    e.Row.ForeColor = System.Drawing.Color.White;
                    e.Row.Cells[1].BackColor = System.Drawing.Color.Tomato;
                    e.Row.Cells[1].ToolTip = "SKU code not present in given department";
                }

                if (Department == "NotAvailable")
                {
                    value = 3;
                    e.Row.BackColor = System.Drawing.Color.DarkCyan;
                    e.Row.ForeColor = System.Drawing.Color.White;
                    e.Row.Cells[2].BackColor = System.Drawing.Color.Tomato;
                    e.Row.Cells[2].ToolTip = "Department Not Available";
                }

                if (Quantity == "NotAvailable")
                {
                    value = 4;
                    e.Row.BackColor = System.Drawing.Color.DarkCyan;
                    e.Row.ForeColor = System.Drawing.Color.White;
                    e.Row.Cells[3].BackColor = System.Drawing.Color.Tomato;
                    e.Row.Cells[3].ToolTip = "Virtual Quantity is not given";
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