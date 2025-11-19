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
using PowerOnRentwebapp.PORServicePartRequest;
using PowerOnRentwebapp.AvailableQtyService;
using System.Text;

namespace PowerOnRentwebapp.PowerOnRent
{
    public partial class DirectBulkImport : System.Web.UI.Page
    {
        ResourceManager rm;
        CultureInfo ci;
        DataTable dt;
        DataSet ds = new DataSet();
        long value = 1;
        long companyID = 0, DeptID = 0, locationId = 0, paymentId = 0;
        DateTime Expecteddate;
        System.Data.SqlClient.SqlConnection sqlConn = new System.Data.SqlClient.SqlConnection(@"Data Source=SERVER\SQLEXPRESS;Initial Catalog=GWCDETestNew;User ID=sa;Password=Password123#;");
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
            if (!IsPostBack)
            {
                UpdateRealTimeStockInOMS();
                DisplayGrid();
            }
        }

        public void UpdateRealTimeStockInOMS()
        {
            iProductMasterClient productClient = new iProductMasterClient();
            iPartRequestClient objService = new iPartRequestClient();
            CustomProfile profile = CustomProfile.GetProfile();
            UCProductSearchService.iUCProductSearchClient productSearchService = new UCProductSearchService.iUCProductSearchClient();
            CheckStockAvailableClient RealTimeStock = new CheckStockAvailableClient();
            DataSet dsschema = new DataSet();
            try

            {
                ds = productClient.UpdateRealStockBeforeValidatebulk(profile.Personal.UserID,profile.DBConnection._constr);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string skucode = ds.Tables[0].Rows[i]["ProductCode"].ToString();
                    string DeptCode = ds.Tables[0].Rows[i]["StoreCode"].ToString();
                    long disDeptid = long.Parse(ds.Tables[0].Rows[i]["DeptID"].ToString());
                    decimal AvailableBalance = decimal.Parse(ds.Tables[0].Rows[i]["AvailableBalance"].ToString());
                    decimal ResurveQty = decimal.Parse(ds.Tables[0].Rows[i]["ResurveQty"].ToString());
                    if (skucode != "" || DeptCode != "")
                    {
                        dsschema = productClient.GetDatabaseSchemaforDept(disDeptid, profile.DBConnection._constr);
                        string DatabaseName = dsschema.Tables[0].Rows[0]["DatabaseName"].ToString();
                        string ConnectionString = dsschema.Tables[0].Rows[0]["ConnectionString"].ToString();
                        string Schemaname = dsschema.Tables[0].Rows[0]["Schemaname"].ToString();
                        string wmsstorecode = dsschema.Tables[0].Rows[0]["wmsstorecode"].ToString();
                        if (string.IsNullOrEmpty(wmsstorecode))
                        {
                        }
                        else
                        {
                            DeptCode = wmsstorecode;
                        }
                        if (Schemaname != "")
                        {
                            string companyisrealtime = productClient.GetcompanyRealtimestock(disDeptid, profile.DBConnection._constr);
                            if (companyisrealtime == "Yes")
                            {
                                decimal totalQTy = AvailableBalance + ResurveQty;
                                decimal RealTimeStockQty = RealTimeStock.CheckAvailableQty(Convert.ToString(skucode), DeptCode, DatabaseName, ConnectionString, Schemaname);
                                //real time stock log logic
                                productSearchService.insertlog(skucode, disDeptid, "BulkImport", AvailableBalance, ResurveQty, RealTimeStockQty, profile.DBConnection._constr);
                                //End
                                if (totalQTy != RealTimeStockQty)
                                {
                                    productSearchService.Createtransaction(Convert.ToString(skucode), Convert.ToString(disDeptid), RealTimeStockQty, profile.DBConnection._constr);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Login.Profile.ErrorHandling(ex, "DirectBulkImport.aspx", "UpdateRealTimeStockInOMS");


            }
            finally
            {
                productClient.Close();
                objService.Close();
                productSearchService.Close();
                RealTimeStock.Close();
                dsschema.Dispose();
            }
        }



        public void DisplayGrid()
           {
            iProductMasterClient productClient = new iProductMasterClient();
            CustomProfile profile = CustomProfile.GetProfile();
            ds = productClient.GetDirectBulkOrderData(profile.Personal.UserID, profile.DBConnection._constr);
            dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                GVImportView.DataSource = ds.Tables[0];
                GVImportView.DataBind();
            }
            else
            {
                value = 3;
            }
            if (dt.Rows.Count > 100)
            {
                btnnext.Enabled = false;
                btnnext.CssClass = "class1";
                lblbackMessage.Text = "Direct Order Could not Process more than 100 Records";
            }
            else if (value == 8)
            {
                btnnext.Enabled = false;
                btnnext.CssClass = "class1";
                lblbackMessage.Text = "Department Not Available";
            }
            else if (value == 0)
            {
                btnnext.Enabled = false;
                btnnext.CssClass = "class1";
                lblbackMessage.Text = "Colored Row SKU not Available in OMS.Please click on Back Button";
            }
            else if (value == 2)
            {
                btnnext.Enabled = false;
                btnnext.CssClass = "class1";
                lblbackMessage.Text = "Characters and blank values not accepted in Price columns";
            }
            else if (value == 3)
            {
                btnnext.Enabled = false;
                btnnext.CssClass = "class1";
                lblbackMessage.Text = "Please Select Correct Company and department for Uploading SKU price";
            }
            else if (value == 4)
            {
                btnnext.Enabled = false;
                btnnext.CssClass = "class1";
                lblbackMessage.Text = "Current Stock is Less than requested Qty";
            }
            else if (value == 5)
            {
                btnnext.Enabled = false;
                btnnext.CssClass = "class1";
                lblbackMessage.Text = "Requested Qty is not according to MOQ";
            }
            else if (value == 6)
            {
                btnnext.Enabled = false;
                btnnext.CssClass = "class1";
                lblbackMessage.Text = "Quantity Should not be blank,Zero or Charecters";
            }
            else if (value == 7)
            {
                btnnext.Enabled = false;
                btnnext.CssClass = "class1";
                lblbackMessage.Text = "SKU not available in Given Department";
            }
           
            else if (value == 9)
            {
                btnnext.Enabled = false;
                btnnext.CssClass = "class1";
                lblbackMessage.Text = "Location Not Available";
            }
            else if (value == 10)
            {
                btnnext.Enabled = false;
                btnnext.CssClass = "class1";
                lblbackMessage.Text = "Current Quantity for colored product is less than combine Order Quantity for same product & Department";
            }
            else if (value == 11)
            {
                btnnext.Enabled = false;
                btnnext.CssClass = "class1";
                lblbackMessage.Text = "Product is Duplicate for same Department and Location";
            }
            else if (value == 12)
            {
                btnnext.Enabled = false;
                btnnext.CssClass = "class1";
                lblbackMessage.Text = "Product,Department Or Location Is Not Active";
            }
            else if (value == 13)
            {
                btnnext.Enabled = false;
                btnnext.CssClass = "class1";
                lblbackMessage.Text = "Title Not Available";
            }
            else if (value == 14)
            {
                btnnext.Enabled = false;
                btnnext.CssClass = "class1";
                lblbackMessage.Text = "Payment Method Not Available For This Department";
            }
            else if (value == 15)
            {
                btnnext.Enabled = false;
                btnnext.CssClass = "class1";
                lblbackMessage.Text = "PayMethodValue Not Available";
            }
            else if (value == 16)
            {
                btnnext.Enabled = false;
                btnnext.CssClass = "class1";
                lblbackMessage.Text = "Costcenter User Not Available";
            }
            
            else if (value == 17)
            {
                btnnext.Enabled = false;
                btnnext.CssClass = "class1";
                lblbackMessage.Text = "LPO - Local Purchase Order Payment Method Value Not Available";
            }
            else if (value == 18)
            {
                btnnext.Enabled = false;
                btnnext.CssClass = "class1";
                lblbackMessage.Text = "Bill To Account Payment Method Value Not Available";
            }
            else if (value == 19)
            {
                btnnext.Enabled = false;
                btnnext.CssClass = "class1";
                lblbackMessage.Text = "COD - Cash On Delivery (QAR) Payment Method Value Not Available";
            }
            else if (value == 20)
            {
                btnnext.Enabled = false;
                btnnext.CssClass = "class1";
                lblbackMessage.Text = "SIP Payment Method Value Not Available";
            }
            else if (value == 21)
            {
                btnnext.Enabled = false;
                btnnext.CssClass = "class1";
                lblbackMessage.Text = "Credit Card Payment Method Value Not Available";
            }
            else if (value == 22)
            {
                btnnext.Enabled = false;
                btnnext.CssClass = "class1";
                lblbackMessage.Text = "Ecommerce Department Is Not Applicable";
            }
            else if (value == 23)
            {
                btnnext.Enabled = false;
                btnnext.CssClass = "class1";
                lblbackMessage.Text = "Approval Is Not Configured For This Department";
            }
            else if (value == 24)
            {
                btnnext.Enabled = false;
                btnnext.CssClass = "class1";
                lblbackMessage.Text = "Financial Approver Is Not Configured For This Department";
            }
            else if (value == 25)
            {
                btnnext.Enabled = false;
                btnnext.CssClass = "class1";
                lblbackMessage.Text = "Installment payment value is not inserted";
            }
            else if (value == 26)
            {
                btnnext.Enabled = false;
                btnnext.CssClass = "class1";
                lblbackMessage.Text = "Enterprise Segment value is not inserted";
            }
            else
            {
                lblOkMessage.Text = "All data are verified.Please click on Next Button ";
                btnnext.Enabled = true;
                btnnext.CssClass = "class2";
            }
        }

        protected void GVImportView_OnRowDataBound(object sender, Obout.Grid.GridRowEventArgs e)
        {
            try
            {
                decimal price = 0m;
                long CurrentStock = 0;
                string ProductCode = e.Row.Cells[12].Text;
                string ProdDept = e.Row.Cells[13].Text;
                string Department = e.Row.Cells[14].Text;
                string Location = e.Row.Cells[15].Text;
                string BalanceOrderQty = e.Row.Cells[20].Text;
                string Duplicate = e.Row.Cells[21].Text;
                string ActiveLocProd = e.Row.Cells[22].Text;
                string Title = e.Row.Cells[24].Text;

                string Paymenttype = e.Row.Cells[26].Text;
                string costcenter = e.Row.Cells[28].Text;
                string costcenteruser = e.Row.Cells[29].Text;

                string chklpopayment = e.Row.Cells[30].Text;
                string chklbtapayment = e.Row.Cells[31].Text;
                string chkcodpayment = e.Row.Cells[32].Text;
                string chksippayment = e.Row.Cells[33].Text;
                string chkccpayment = e.Row.Cells[34].Text;
                string Ecommercedept = e.Row.Cells[35].Text;
                string ChkApprovalDept = e.Row.Cells[36].Text;
                string finapproval = e.Row.Cells[37].Text;
                string chkinstpayment = e.Row.Cells[39].Text;
                string chksegment = e.Row.Cells[41].Text;
                long MOQchk = long.Parse(e.Row.Cells[17].Text);
                if (e.Row.Cells[10].Text != "")
                {
                    price = decimal.Parse(e.Row.Cells[10].Text);
                }
                else
                {
                    e.Row.BackColor = System.Drawing.Color.DarkCyan;
                    e.Row.ForeColor = System.Drawing.Color.White;
                    e.Row.Cells[10].BackColor = System.Drawing.Color.Tomato;
                    e.Row.Cells[10].ToolTip = "Price Should not be blank or Charecters";
                    value = 2;
                }

                if (e.Row.Cells[7].Text != "" && double.Parse(e.Row.Cells[7].Text) != 0.00)
                {
                    CurrentStock = long.Parse(e.Row.Cells[16].Text);
                }
                else
                {
                    e.Row.BackColor = System.Drawing.Color.DarkCyan;
                    e.Row.ForeColor = System.Drawing.Color.White;
                    e.Row.Cells[7].BackColor = System.Drawing.Color.Tomato;
                    e.Row.Cells[7].ToolTip = "Quantity Should not be blank, Zero or Charecters";
                    value = 6;
                }
                 if (Department == "NotAvailable")
                {
                    value = 8;
                    e.Row.BackColor = System.Drawing.Color.DarkCyan;
                    e.Row.ForeColor = System.Drawing.Color.White;
                    e.Row.Cells[18].BackColor = System.Drawing.Color.Tomato;
                    e.Row.Cells[18].ToolTip = "Department Not Available";
                }
                else if (ProductCode == "NotAvailable")
                {
                    value = 0;
                    e.Row.BackColor = System.Drawing.Color.DarkCyan;
                    e.Row.ForeColor = System.Drawing.Color.White;
                    e.Row.Cells[1].BackColor = System.Drawing.Color.Tomato;
                    e.Row.Cells[1].ToolTip = "SKU not available";
                }
                else if (long.Parse(e.Row.Cells[16].Text) == 0)
                {
                    value = 4;
                    e.Row.BackColor = System.Drawing.Color.DarkCyan;
                    e.Row.ForeColor = System.Drawing.Color.White;
                    e.Row.Cells[5].BackColor = System.Drawing.Color.Tomato;
                    e.Row.Cells[5].ToolTip = "Current Stock is Less than requested Qty";
                }
                else if (MOQchk == 1)
                {
                    value = 5;
                    e.Row.BackColor = System.Drawing.Color.DarkCyan;
                    e.Row.ForeColor = System.Drawing.Color.White;
                    e.Row.Cells[4].BackColor = System.Drawing.Color.Tomato;
                    e.Row.Cells[4].ToolTip = "Requested Qty is not according to MOQ";
                    e.Row.Cells[7].BackColor = System.Drawing.Color.Tomato;
                    e.Row.Cells[7].ToolTip = "Requested Qty is not according to MOQ";
                }
                else if (ProdDept == "NotInDepartment")
                {
                    value = 7;
                    e.Row.BackColor = System.Drawing.Color.DarkCyan;
                    e.Row.ForeColor = System.Drawing.Color.White;
                    e.Row.Cells[1].BackColor = System.Drawing.Color.Tomato;
                    e.Row.Cells[1].ToolTip = "SKU not available in Given Department";
                }
                
                else if (Location == "NotAvailable")
                {
                    value = 9;
                    e.Row.BackColor = System.Drawing.Color.DarkCyan;
                    e.Row.ForeColor = System.Drawing.Color.White;
                    e.Row.Cells[19].BackColor = System.Drawing.Color.Tomato;
                    e.Row.Cells[19].ToolTip = "Location Not Available";
                }
                else if (BalanceOrderQty == "NotAvailable")
                {
                    value = 10;
                    e.Row.BackColor = System.Drawing.Color.DarkCyan;
                    e.Row.ForeColor = System.Drawing.Color.White;
                    e.Row.Cells[1].BackColor = System.Drawing.Color.Tomato;
                    e.Row.Cells[1].ToolTip = "Available Quantity for colored product is less than total Order Quantity for Department";
                }
                else if (Duplicate == "Duplicate")
                {
                    value = 11;
                    e.Row.BackColor = System.Drawing.Color.DarkCyan;
                    e.Row.ForeColor = System.Drawing.Color.White;
                    e.Row.Cells[1].BackColor = System.Drawing.Color.Tomato;
                    e.Row.Cells[19].BackColor = System.Drawing.Color.Tomato;
                    e.Row.Cells[18].BackColor = System.Drawing.Color.Tomato;
                    e.Row.Cells[1].ToolTip = "Product is Duplicate for same Department and Location";
                    e.Row.Cells[18].ToolTip = "Product is Duplicate for same Department and Location";
                    e.Row.Cells[19].ToolTip = "Product is Duplicate for same Department and Location";
                }
                else if (ActiveLocProd == "NotActive")
                {
                    value = 12;
                    e.Row.BackColor = System.Drawing.Color.DarkCyan;
                    e.Row.ForeColor = System.Drawing.Color.White;
                    e.Row.Cells[1].BackColor = System.Drawing.Color.Tomato;
                    e.Row.Cells[19].BackColor = System.Drawing.Color.Tomato;
                    e.Row.Cells[18].BackColor = System.Drawing.Color.Tomato;
                    e.Row.Cells[1].ToolTip = "Product,Department Or Location Is Not Active";
                    e.Row.Cells[18].ToolTip = "Product,Department Or Location Is Not Active";
                    e.Row.Cells[19].ToolTip = "Product,Department Or Location Is Not Active";
                }
                else if (Title == "NotAvailable")
                {
                    value = 13;
                    e.Row.BackColor = System.Drawing.Color.DarkCyan;
                    e.Row.ForeColor = System.Drawing.Color.White;
                    e.Row.Cells[24].BackColor = System.Drawing.Color.Tomato;
                    e.Row.Cells[24].ToolTip = "Title Not Available";
                }
                else if (Paymenttype == "NotAvailable")
                {
                    value = 14;
                    e.Row.BackColor = System.Drawing.Color.DarkCyan;
                    e.Row.ForeColor = System.Drawing.Color.White;
                    e.Row.Cells[26].BackColor = System.Drawing.Color.Tomato;
                    e.Row.Cells[26].ToolTip = "Payment Method Not Available For This Department";
                }
                else if (costcenter == "NotAvailable")
                {
                    value = 15;
                    e.Row.BackColor = System.Drawing.Color.DarkCyan;
                    e.Row.ForeColor = System.Drawing.Color.White;
                    e.Row.Cells[28].BackColor = System.Drawing.Color.Tomato;
                    e.Row.Cells[28].ToolTip = "PayMethodValue Not Available";
                }
                else if (costcenteruser == "NotAvailable")
                {
                    value = 16;
                    e.Row.BackColor = System.Drawing.Color.DarkCyan;
                    e.Row.ForeColor = System.Drawing.Color.White;
                    e.Row.Cells[29].BackColor = System.Drawing.Color.Tomato;
                    e.Row.Cells[29].ToolTip = "Costcenter User Not Available";
                }               
                else if (chklpopayment == "NotAvailable")
                {
                    value = 17;
                    e.Row.BackColor = System.Drawing.Color.DarkCyan;
                    e.Row.ForeColor = System.Drawing.Color.White;
                    e.Row.Cells[30].BackColor = System.Drawing.Color.Tomato;
                    e.Row.Cells[30].ToolTip = "LPO - Local Purchase Order Payment Method Value Not Available";
                }
                else if (chklbtapayment == "NotAvailable")
                {
                    value = 18;
                    e.Row.BackColor = System.Drawing.Color.DarkCyan;
                    e.Row.ForeColor = System.Drawing.Color.White;
                    e.Row.Cells[31].BackColor = System.Drawing.Color.Tomato;
                    e.Row.Cells[31].ToolTip = "Bill To Account Payment Method Value Not Available";
                }
                else if (chkcodpayment == "NotAvailable")
                {
                    value = 19;
                    e.Row.BackColor = System.Drawing.Color.DarkCyan;
                    e.Row.ForeColor = System.Drawing.Color.White;
                    e.Row.Cells[32].BackColor = System.Drawing.Color.Tomato;
                    e.Row.Cells[32].ToolTip = "COD - Cash On Delivery (QAR) Payment Method Value Not Available";
                }
                else if (chksippayment == "NotAvailable")
                {
                    value = 20;
                    e.Row.BackColor = System.Drawing.Color.DarkCyan;
                    e.Row.ForeColor = System.Drawing.Color.White;
                    e.Row.Cells[33].BackColor = System.Drawing.Color.Tomato;
                    e.Row.Cells[33].ToolTip = "SIP Payment Method Value Not Available";
                }
                else if (chkccpayment == "NotAvailable")
                {
                    value = 21;
                    e.Row.BackColor = System.Drawing.Color.DarkCyan;
                    e.Row.ForeColor = System.Drawing.Color.White;
                    e.Row.Cells[34].BackColor = System.Drawing.Color.Tomato;
                    e.Row.Cells[34].ToolTip = "Credit Card Payment Method Value Not Available";
                }
                else if (Ecommercedept == "NotAvailable")
                {
                    value = 22;
                    e.Row.BackColor = System.Drawing.Color.DarkCyan;
                    e.Row.ForeColor = System.Drawing.Color.White;
                    e.Row.Cells[35].BackColor = System.Drawing.Color.Tomato;
                    e.Row.Cells[35].ToolTip = "Ecommerce Department Is Not Applicable";
                }
                else if (ChkApprovalDept == "NotAvailable")
                {
                    value = 23;
                    e.Row.BackColor = System.Drawing.Color.DarkCyan;
                    e.Row.ForeColor = System.Drawing.Color.White;
                    e.Row.Cells[36].BackColor = System.Drawing.Color.Tomato;
                    e.Row.Cells[36].ToolTip = "Approval Is Not Configured For This Department";
                }
                else if (finapproval == "NotAvailable")
                {
                    value = 24;
                    e.Row.BackColor = System.Drawing.Color.DarkCyan;
                    e.Row.ForeColor = System.Drawing.Color.White;
                    e.Row.Cells[37].BackColor = System.Drawing.Color.Tomato;
                    e.Row.Cells[37].ToolTip = "Financial Approver Is Not Configured For This Department";
                }

                else if (chkinstpayment == "NotAvailable")
                {
                    value = 25;
                    e.Row.BackColor = System.Drawing.Color.DarkCyan;
                    e.Row.ForeColor = System.Drawing.Color.White;
                    e.Row.Cells[38].BackColor = System.Drawing.Color.Tomato;
                    e.Row.Cells[38].ToolTip = "Installment payment value is not inserted";
                }
                else if (chksegment == "NotAvailable")
                {
                    value = 26;
                    e.Row.BackColor = System.Drawing.Color.DarkCyan;
                    e.Row.ForeColor = System.Drawing.Color.White;
                    e.Row.Cells[40].BackColor = System.Drawing.Color.Tomato;
                    e.Row.Cells[40].ToolTip = "Enterprise Segment value is not inserted";
                }

            }
            catch (Exception ex)
            {
                value = 2;
                btnnext.Enabled = false;
                btnnext.CssClass = "class1";
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Only Numbers are allowed in Price columns');", true);

            }

        }





        protected void btnnext_Click(object sender, EventArgs e)
        {
            DataSet ds1 = new DataSet();
            DataTable dt1 = new DataTable();
            iProductMasterClient productClient = new iProductMasterClient();
            iPartRequestClient objService = new iPartRequestClient();
            CustomProfile profile = CustomProfile.GetProfile();
            decimal TotalProdQty = 0m, totalPrice = 0m;
            //  string Title = "Direct Order Import";
            string OrderNumber = "";
            DateTime orderdate = DateTime.Now;
            try
            {

                ds = productClient.GetDistinctPaymentmethodCodes(profile.Personal.UserID, profile.DBConnection._constr);
                dt = ds.Tables[0];
                for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    DataSet ds2 = new DataSet();
                    DataTable dt2 = new DataTable();
                    long disDeptid = long.Parse(ds.Tables[0].Rows[i]["deptId"].ToString());
                    long DisLocId = long.Parse(ds.Tables[0].Rows[i]["LocID"].ToString());
                    long CompanyID = long.Parse(ds.Tables[0].Rows[i]["ParentID"].ToString());
                    long Paymentid = long.Parse(ds.Tables[0].Rows[i]["Paymentid"].ToString());
                    long segmentID = Convert.ToInt64(ds.Tables[0].Rows[i]["segmentID"].ToString());
                    //string paymethodvalue = Convert.ToString(ds.Tables[0].Rows[i]["PaymethodValue"].ToString());

                    string Paymentiddata = "";
                    long Orderheadid = 0;
                    ds2 = productClient.GetTotalForBulkOrderHead(disDeptid, DisLocId, Paymentid,profile.Personal.UserID, profile.DBConnection._constr);
                    dt2 = ds2.Tables[0];
                    if (dt2.Rows.Count > 0)
                    {
                        TotalProdQty = decimal.Parse(dt2.Rows[0]["TotalOrderQty"].ToString());
                        totalPrice = decimal.Parse(dt2.Rows[0]["GrandTotalPrice"].ToString());
                    }
                    OrderNumber = productClient.getOrderFormatNumber(disDeptid, profile.DBConnection._constr);
                    ds1 = productClient.GetBulkImportDatabyDisDeptLocId(disDeptid, DisLocId, Paymentid, 0,profile.Personal.UserID, profile.DBConnection._constr);
                    dt1 = ds1.Tables[0];
                    long sequence = 0;
                    for (int j = 0; j <= ds1.Tables[0].Rows.Count - 1; j++)
                    {
                        long UOMID = 16;
                        string skucode = ds1.Tables[0].Rows[j]["SKUCode"].ToString();
                        string SkuName = ds1.Tables[0].Rows[j]["Name"].ToString();
                        string Description = ds1.Tables[0].Rows[j]["Description"].ToString();
                        decimal AvailableBalance = decimal.Parse(ds1.Tables[0].Rows[j]["AvailableBalance"].ToString());
                        decimal RequestQty = decimal.Parse(ds1.Tables[0].Rows[j]["RequestQty"].ToString());
                        decimal OrderQty = decimal.Parse(ds1.Tables[0].Rows[j]["OrderQty"].ToString());
                        decimal Price = decimal.Parse(ds1.Tables[0].Rows[j]["Price"].ToString());
                        decimal Total = decimal.Parse(ds1.Tables[0].Rows[j]["Total"].ToString());
                        long locationid = long.Parse(ds1.Tables[0].Rows[j]["locationid"].ToString());
                        long prodID = long.Parse(ds1.Tables[0].Rows[j]["prodID"].ToString());
                        long storeID = long.Parse(ds1.Tables[0].Rows[j]["ID"].ToString());
                        decimal Dispatch = decimal.Parse(ds1.Tables[0].Rows[j]["TotalDispatchQty"].ToString());
                        string title = Convert.ToString(ds1.Tables[0].Rows[j]["title"].ToString());


                        long maxdeldays = 1;
                        DateTime Deliveryday = DateTime.Now.AddDays(maxdeldays);
                        decimal CurrentAvailBalance = AvailableBalance - OrderQty;
                        decimal CurrentDispatchQty = Dispatch + OrderQty;

                        sequence = sequence + 1;
                        if (j == 0)
                        {
                            if (Paymentid == 9)
                            {
                              string  paymentmenthodvlaue = Convert.ToString(ds1.Tables[0].Rows[j]["PaymethodValue"].ToString());
                                string installmentvalue= Convert.ToString(ds1.Tables[0].Rows[j]["BTAInstallmentValue"].ToString());
                                Paymentiddata = paymentmenthodvlaue + ',' + installmentvalue;
                            }
                            else { Paymentiddata = Convert.ToString(ds1.Tables[0].Rows[j]["PaymethodValue"].ToString()); }
                            
                            Orderheadid = productClient.SaveBulkOrderHeaderImport(storeID, orderdate, Deliveryday, 0, 2, profile.Personal.UserID, DateTime.Now, title, DateTime.Now, TotalProdQty, totalPrice, OrderNumber, locationid, Convert.ToInt64(Paymentid), 0, segmentID,profile.DBConnection._constr);

                        }

                        productClient.SaveBulkOrderDetailImport(Orderheadid, prodID, OrderQty, UOMID, sequence, SkuName, Description, skucode, Price, Total, profile.DBConnection._constr);
                        // productClient.updateproductstockdetailimport(storeID, prodID, CurrentAvailBalance, CurrentDispatchQty, profile.DBConnection._constr);

                    }
                    int Result = objService.SetApproverDataafterSaveBulk("", Orderheadid, Convert.ToString(profile.Personal.UserID), 2, disDeptid, 0, profile.DBConnection._constr);

                     objService.SaveCostcenterApprover(Paymentiddata, Paymentid, Orderheadid, disDeptid, profile.Personal.UserID, profile.DBConnection._constr);


                }

                productClient.DeleteBulkOrderImport(profile.Personal.UserID, profile.DBConnection._constr);
                Response.Redirect("ImportFinish.aspx");
            }
            catch (Exception ex)
            {
                productClient.DeleteBulkOrderImport(profile.Personal.UserID, profile.DBConnection._constr);
                Login.Profile.ErrorHandling(ex, "DirectBulkImport.aspx", "btnnext_Click");
            }
            finally
            {
                productClient.Close();
            }
        }




        protected void btnback_Click(object sender, EventArgs e)
        {
            iProductMasterClient productClient = new iProductMasterClient();
            CustomProfile profile = CustomProfile.GetProfile();
            productClient.CancelBulkOrderImport(profile.Personal.UserID, profile.DBConnection._constr);
            productClient.Close();
            Response.Redirect("DirectImportVC.aspx");
        }

        private void loadstring()
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(Session["Lang"].ToString());
                rm = new ResourceManager("PowerOnRentwebapp.App_GlobalResources.Lang", Assembly.GetExecutingAssembly());
                ci = Thread.CurrentThread.CurrentCulture;

                lblHeading.Text = rm.GetString("ImportDirectOrder", ci);
                lblstep1.Text = rm.GetString("UploadFile", ci);
                lblstep2.Text = rm.GetString("DataValidationVerification", ci);
                lblstep3.Text = rm.GetString("Finished", ci);

                lbladdresslist.Text = rm.GetString("SKUList", ci);
                btnnext.Text = rm.GetString("Next", ci);

                btnback.Text = rm.GetString("back", ci);
            }
            catch (Exception ex)
            {
                Login.Profile.ErrorHandling(ex, "DirectBulkImport.aspx", "Loadstring");
            }
        }
    }
}