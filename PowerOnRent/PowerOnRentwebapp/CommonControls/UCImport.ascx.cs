using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Data.OleDb;
using PowerOnRentwebapp.Login;
using PowerOnRentwebapp.ImportService;
using PowerOnRentwebapp.PORServicePartRequest;
using PowerOnRentwebapp.ProductMasterService;
using System.Configuration;
using System.Web.Services;
using PowerOnRentwebapp.RTSerialStock;
using PowerOnRentwebapp.AvailableQtyService;
using PowerOnRentwebapp.UCProductSearchService;

namespace BrilliantWMS.CommonControls
{
    public partial class UCImport : System.Web.UI.UserControl
    {
        string Validate = "", chkserial = "";
        //string TabaleName = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            pnlImport.Visible = true;
            // pnlvalidate.Visible = true;
            // pnlfinish.Visible = true;

            //btnimpnext.Enabled = false;
            //btnimpnext.CssClass = "class1";
            btnImportNext.Enabled = false;

            uploadMessage.Visible = false;
            lblmessagesuccess.Visible = false;
            if (!IsPostBack)
            {
                string Object = "";
                Object = Session["Object"].ToString();
                hdnobject.Value = Object;
                hdnexportobject.Value = Object;
                BindCustomer();
                GetImportheadData(Object);
                // GetSchedulecount(Object);
                hdncustomerID.Value = ddlCustomer.SelectedValue.ToString();
            }
        }

        public void GetImportheadData(string Object)
        {

            ImportServiceClient Import = new ImportServiceClient();
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                long CompanyID = 0;
                long UserID = 0;
                CompanyID = profile.Personal.CompanyID;
                UserID = profile.Personal.UserID;

                DataSet ds = Import.GetTemplateDataByObject(Object, UserID, Convert.ToInt64(ddlCustomer.SelectedItem.Value));//,profile.DBConnection._constr);                
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    lbltext1.Text = ds.Tables[0].Rows[0]["Instruction"].ToString();
                    downloadlink.HRef = ds.Tables[0].Rows[0]["Template"].ToString();
                    //  hdntablename.Value = ds.Tables[0].Rows[0]["tempTableName"].ToString();
                    hdntablename.Value = "TempVTImport";
                }
                ds.Clear();
                Import.Close();
            }

            catch (System.Exception ex) { PowerOnRentwebapp.Login.Profile.ErrorHandling(ex, "UCImport.aspx", "GetImportheadData"); }
            finally { }

        }

        /* public void GetSchedulecount(string Object)
         {
             ImportServiceClient Import = new ImportServiceClient();
             try
             {
                 CustomProfile profile = CustomProfile.GetProfile();
                 long ScheduleCount = Import.GetScheduleCount(Object, profile.Personal.CompanyID, Convert.ToInt64(ddlCustomer.SelectedItem.Value), profile.Personal.UserID);//,profile.DBConnection._constr);
                 lblschedulecount.Text = ScheduleCount.ToString();
             }
             catch (System.Exception ex)
             {
                 PowerOnRentwebapp.Login.Profile.ErrorHandling(ex, "UCImport.aspx", "GetSchedulecount");
             }
         }*/

        protected void btnUploadPo_Click(object sender, EventArgs e)
        {
            ImportServiceClient Import = new ImportServiceClient();
            try
            {
                //System.Data.SqlClient.SqlConnection sqlConn = new System.Data.SqlClient.SqlConnection(@"Data Source=166.62.35.21;Initial Catalog=WCoastWMSTestGUI;User ID=sa;Password=Password123#;Connection Timeout=80000;");
                string connection = ConfigurationManager.ConnectionStrings["elegantcrm7ConnectionString"].ConnectionString;
                System.Data.SqlClient.SqlConnection sqlConn = new SqlConnection(connection);

                DataTable dt = new DataTable();
                DataSet ImportData = new DataSet();
                string connString = "";
                CustomProfile profile = CustomProfile.GetProfile();
                Import.DeleteFromtempTable(hdnobject.Value, profile.Personal.UserID, Convert.ToInt64(ddlCustomer.SelectedItem.Value));   // , profile.DBConnection._constr);
                Import.Close();
                string path = FileuploadPO.PostedFile.FileName;
                string strFileType = System.IO.Path.GetExtension(path).ToString().ToLower();
                //old string Fullpath = Server.MapPath("../CommonControls/ImportFiles/" + path);
                //old FileuploadPO.PostedFile.SaveAs(Server.MapPath("../CommonControls/ImportFiles/" + path));
                string Fullpath = Server.MapPath("../Product/ImportPrice/" + path);

                FileuploadPO.PostedFile.SaveAs(Server.MapPath("../Product/ImportPrice/" + path));
                if (strFileType.Trim() == ".xls")
                {
                    connString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Fullpath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                }
                else if (strFileType.Trim() == ".xlsx")
                {
                    connString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Fullpath + ";Extended Properties='Excel 8.0;HDR=Yes'";
                }
                OleDbConnection excelConnection = new OleDbConnection(connString);
                OleDbCommand cmd1 = new OleDbCommand("Select * from [Template$]", excelConnection);
                OleDbDataAdapter oda = new OleDbDataAdapter();
                excelConnection.Open();
                oda.SelectCommand = cmd1;
                oda.Fill(ImportData);
                dt = ImportData.Tables[0];
                dt.Columns.Add("ValidRemark", typeof(System.String));
                dt.Columns.Add("CreatedBy", typeof(System.Int64));
                dt.Columns.Add("CustomerId", typeof(System.Int64));
                foreach (DataRow dr in dt.Rows)
                {
                    dr["ValidRemark"] = null;
                    dr["CreatedBy"] = profile.Personal.UserID;
                    dr["CustomerId"] = ddlCustomer.SelectedItem.Value;
                }
                SqlBulkCopy sqlBulk = new SqlBulkCopy(sqlConn);
                sqlConn.Open();
                sqlBulk.DestinationTableName = "TempVTImport";// TabaleName
                //sqlBulk.DestinationTableName = hdntablename.Value;
                sqlBulk.WriteToServer(dt);
                sqlConn.Close();
                Session.Add("ImportValues", ImportData);
                excelConnection.Close();
                sqlBulk.Close();
                //  btnimpnext.Enabled = true;
                //  btnimpnext.CssClass = "class2";
                btnImportNext.Enabled = true;

                uploadMessage.Visible = true;
                lblmessagesuccess.Visible = true;
            }
            catch (Exception ex)
            {
                if (ex.Message.ToString() == "The given Column Mapping does not match up with any column in the source or destination.")
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "showAlert('Uploaded template is Wrong. Please check the template.','error','#')", true);
                }
                else
                {
                    Boolean b = ex.Message.ToString().Contains("because it is being used by another process.");
                    if (b == true)
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "showAlert('Please rename the template & try again. ','error','#')", true);
                    }
                    else
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "showAlert('Please check the template. ','error','#')", true);
                    }
                }
            }
            finally
            {
                Import.Close();
            }
        }
        protected void btnimportNext_Click(object sender, EventArgs e)
        {
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                //BrilliantWMS.ImportService.iImportClient Import = new iImportClient();
                ImportServiceClient Import = new ImportServiceClient();

                DataSet ds = new DataSet();
                DataSet Importvalue = new DataSet();
                Importvalue = (DataSet)Session["ImportValues"];
                hdncheckvalid.Value = "";
                UpdateRealTimeStockInOMS();

                /*long CountRecord = Import.GetTemptablerecordCount(hdnobject.Value, profile.Personal.UserID, profile.Personal.CustomerId,profile.DBConnection._constr);
                if(CountRecord <= 1)
                  {
                      Import.NewValidateImportData(hdnobject.Value, profile.Personal.UserID, profile.Personal.CompanyID, Importvalue,profile.DBConnection._constr);
                  }
                  else
                  {
                      Import.ValidateImportDataMorethan100(hdnobject.Value, profile.Personal.UserID, profile.Personal.CompanyID, profile.DBConnection._constr);
                  }*/
                Import.ValidateImportDataMorethan100(hdnobject.Value, profile.Personal.UserID, profile.Personal.CompanyID, Convert.ToInt64(ddlCustomer.SelectedItem.Value)); //,profile.DBConnection._constr);
                lblbackMessage.Text = "";
                lblbackMessage.Text = "";
                lblOkMessage.Text = "";
                lblscheduleQue.Text = "";

                ds = Import.getTempTableValue(hdnobject.Value, profile.Personal.UserID, Convert.ToInt64(ddlCustomer.SelectedItem.Value));    //,profile.DBConnection._constr);
                GVImportView.DataSource = ds;
                GVImportView.DataBind();
                GVImportView.Columns[0].Wrap = true;
                if (ds.Tables[0].Rows.Count == 0)
                {
                    lblbackMessage.Visible = true;
                    lblbackMessage.Text = "Record Not Available, Please Fill Record and Upload it Again.";
                    btnnext.Enabled = false;
                    btnnext.CssClass = "class1";
                    lblOkMessage.Visible = false;
                    lblscheduleQue.Visible = false;
                    lblserialnotmatching.Visible = false;
                    lnkbtnserial.Visible = false;
                }
                else if (Validate != "")
                {
                    lblbackMessage.Visible = true;
                    lblbackMessage.Text = "Colored Row Data is not valid, Please Check Remark and Click on Back button.";
                    btnnext.Enabled = false;
                    btnnext.CssClass = "class1";
                    lblOkMessage.Visible = false;
                    lblscheduleQue.Visible = false;
                    lblserialnotmatching.Visible = false;
                    lnkbtnserial.Visible = false;
                }
                else
                {
                    long CountRecord = Import.GetTemptablerecordCount(hdnobject.Value, profile.Personal.UserID, Convert.ToInt64(ddlCustomer.SelectedItem.Value));   //,profile.DBConnection._constr);
                    if (CountRecord <= 100)  //Limit
                    {
                        if (chkserial != "")
                        {
                            lblbackMessage.Text = "Please check serial no.";
                            btnnext.Enabled = false;
                            btnnext.CssClass = "class1";
                            lblserialnotmatching.Visible = false;
                            lnkbtnserial.Visible = false;
                            btnvalidate.Enabled = true;
                        }
                        else
                        {
                            btnvalidate.Enabled = false;
                            btnvalidate.CssClass = "class1";


                            hdncheckvalid.Value = "Valid";
                            lblOkMessage.Text = "All data verified.Please click on Next Button ";
                            btnnext.Enabled = true;
                            lblbackMessage.Visible = false;
                            lblserialnotmatching.Visible = false;
                            lnkbtnserial.Visible = false;
                            btnnext.CssClass = "class2";
                        }
                    }
                    else
                    {
                        hdncheckvalid.Value = "Valid";
                        //lblscheduleQue.Text = "Data verification completed! Import for more than 40 records will queued, Click on Next Button for adding to Import Queue !!";
                        lblscheduleQue.Text = "upload template have more than 100 rows, please add rows below 100 for successfull import, Please click on back button. !!";
                        btnnext.Enabled = false;
                        lblserialnotmatching.Visible = false;
                        lnkbtnserial.Visible = false;
                        btnnext.CssClass = "class1";
                        btnvalidate.Enabled = false;
                        btnvalidate.CssClass = "class1";
                    }
                }

                // Up_PnlGirdProduct.Visible = false;
                // UpdateGirdProductProcess.Visible = false;
                Import.Close();
                pnlImport.Visible = false;
                pnlvalidate.Visible = true;
            }
            catch (Exception ex)
            { }
        }

        protected void Clickbtvalidate_Click(object sender, EventArgs e)
        {
            ImportServiceClient Import = new ImportServiceClient();
            SerialRTimeStockServiceClient SerialStk = new SerialRTimeStockServiceClient();
            iUCProductSearchClient productSearchService = new iUCProductSearchClient();
            CustomProfile profile = CustomProfile.GetProfile();
            try
            {
                DataSet ds = new DataSet();
                string SKUId = "", SKUCode = "", StoreCode = "", Schemaname = "", DatabaseName = "", ConnectionString = "", SerialNo = "", NotavailbleSR = "";
                ds = Import.GetDistictSKUSchemaforSerial(profile.Personal.UserID, Convert.ToInt64(ddlCustomer.SelectedItem.Value));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        List<string> Impoerserial;
                        List<GetSerial> serialList = new List<GetSerial>();
                        SKUId = ds.Tables[0].Rows[i]["Productid"].ToString();
                        StoreCode = ds.Tables[0].Rows[i]["DeptCode"].ToString();
                        SKUCode = ds.Tables[0].Rows[i]["SKUCode"].ToString();
                        Schemaname = ds.Tables[0].Rows[i]["Schemaname"].ToString();
                        DatabaseName = ds.Tables[0].Rows[i]["DatabaseName"].ToString();
                        ConnectionString = ds.Tables[0].Rows[i]["ConnectionString"].ToString();
                        SerialNo = ds.Tables[0].Rows[i]["SerialNo"].ToString();
                        if (SerialNo != "")
                        {
                            Impoerserial = SerialNo.Split('|').ToList();
                            if (Schemaname != "")
                            {
                                serialList = SerialStk.CheckAvailableQty(SKUCode, StoreCode, DatabaseName, ConnectionString, Schemaname);
                                //  serialList = SerialStk.CheckAvailableQty("DRUM1", "VF5264PB", DatabaseName, ConnectionString, "wmwhse4");

                                List<string> stringlistk = serialList.Where(x => x != null)
                                            .Select(x => x.SERIAL)
                                            .ToList();

                                DataSet ds1 = new DataSet();
                                ds1 = productSearchService.GetProductSerialDetails(0, Convert.ToInt64(SKUId), profile.DBConnection._constr);
                                List<string> Result3 = ds1.Tables[0].Rows[0]["SerialNumber"].ToString().ToUpper().Split(',').ToList();
                                ds1.Clear();
                                List<string> NotavailSrlst1 = stringlistk.Except(Result3, StringComparer.OrdinalIgnoreCase).ToList();

                                List<string> NotavailSrlst = Impoerserial.Except(NotavailSrlst1, StringComparer.OrdinalIgnoreCase).ToList();
                                if (NotavailSrlst.Count > 0)
                                {
                                    string SrString = String.Join(",", NotavailSrlst);
                                    NotavailbleSR = NotavailbleSR + " | " + SKUCode + "-" + SrString;
                                }
                            }
                        }
                    }

                    if (NotavailbleSR != "")
                    {
                        lblserialnotmatching.Visible = true;
                        lnkbtnserial.Visible = true;
                        lblbackMessage.Visible = false;
                        lblserialnotmatching.Text = "Some of the Entered serial no. not available.";
                        btnnext.Enabled = false;
                        btnnext.CssClass = "class1";
                        lblOkMessage.Visible = false;
                        lblscheduleQue.Visible = false;
                        pnlImport.Visible = false;
                        pnlvalidate.Visible = true;
                        hdnnotavailserial.Value = NotavailbleSR;
                        ds = Import.getTempTableValue(hdnobject.Value, profile.Personal.UserID, Convert.ToInt64(ddlCustomer.SelectedItem.Value));    //,profile.DBConnection._constr);
                        GVImportView.DataSource = ds;
                        GVImportView.DataBind();
                    }
                    else
                    {
                        hdncheckvalid.Value = "Valid";
                        lblOkMessage.Visible = true;
                        lblOkMessage.Text = "All data verified.Please click on Next Button ";
                        btnnext.Enabled = true;
                        lblbackMessage.Visible = false;
                        lblserialnotmatching.Visible = false;
                        btnnext.CssClass = "class2";
                        ds = Import.getTempTableValue(hdnobject.Value, profile.Personal.UserID, Convert.ToInt64(ddlCustomer.SelectedItem.Value));    //,profile.DBConnection._constr);
                        GVImportView.DataSource = ds;
                        GVImportView.DataBind();
                    }
                }

            }
            catch { }
            finally
            {
                pnlImport.Visible = false;
                pnlvalidate.Visible = true;
                Import.Close();
                SerialStk.Close();
            }
        }

        protected void LinkButton_Click(Object sender, EventArgs e)
        {
            ImportServiceClient Import = new ImportServiceClient();
            CustomProfile profile = CustomProfile.GetProfile();
            DataSet ds = new DataSet();
            try
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "OpenSerial()", true);
                pnlImport.Visible = false;
                pnlvalidate.Visible = true;
                ds = Import.getTempTableValue(hdnobject.Value, profile.Personal.UserID, Convert.ToInt64(ddlCustomer.SelectedItem.Value));    //,profile.DBConnection._constr);
                GVImportView.DataSource = ds;
                GVImportView.DataBind();
            }
            catch { }
            finally
            {
                Import.Close();
                ds.Dispose();
            }

        }


        public void UpdateRealTimeStockInOMS()
        {
            iProductMasterClient productClient = new iProductMasterClient();
            iPartRequestClient objService = new iPartRequestClient();
            CustomProfile profile = CustomProfile.GetProfile();
            iUCProductSearchClient productSearchService = new iUCProductSearchClient();
            CheckStockAvailableClient RealTimeStock = new CheckStockAvailableClient();
            DataSet dsschema = new DataSet();
            DataSet ds = new DataSet();
            try
            {
                ds = productClient.UpdateSRNewIMPBeforeValidate(profile.DBConnection._constr);
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

                            decimal totalQTy = AvailableBalance + ResurveQty;
                            string companyisrealtime = productClient.GetcompanyRealtimestock(disDeptid, profile.DBConnection._constr);
                            if (companyisrealtime == "Yes")
                            {
                                decimal RealTimeStockQty = RealTimeStock.CheckAvailableQty(Convert.ToString(skucode), DeptCode, DatabaseName, ConnectionString, Schemaname);
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
                //  ErrorHandling(ex, "DirectImportV.aspx", "UpdateRealTimeStockInOMS")
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

        public class SerialList
        {
            string SERIAL { get; set; }
            int QTY { get; set; }
        }

        protected void btnimportcancel_Click(object sender, EventArgs e)
        {
            CustomProfile profile = CustomProfile.GetProfile();
            // BrilliantWMS.ImportService.iImportClient Import = new iImportClient();
            ImportServiceClient Import = new ImportServiceClient();
            Import.DeleteFromtempTable(hdnobject.Value, profile.Personal.UserID, Convert.ToInt64(ddlCustomer.SelectedItem.Value)); //, profile.DBConnection._constr);
            Import.Close();
            btnImportNext.Visible = true;
            btnFileUpload.Visible = true;
        }
        protected void btnnext_Click(object sender, EventArgs e)
        {
            ImportServiceClient Import = new ImportServiceClient();
            iProductMasterClient productClient = new iProductMasterClient();
            iPartRequestClient objService = new iPartRequestClient();
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                DataSet DSInsert = new DataSet();
                DataTable dt = new DataTable();
                pnlImport.Visible = false;
                pnlvalidate.Visible = false;
                pnlfinish.Visible = true;
                DSInsert = (DataSet)Session["ImportValues"];
                long CountRecord = Import.GetTemptablerecordCount(hdnobject.Value, profile.Personal.UserID, Convert.ToInt64(ddlCustomer.SelectedItem.Value));   //,profile.DBConnection._constr);
                if (CountRecord <= 100)   //Limit
                {
                    DataSet ds = new DataSet();
                    ds = Import.GetDistinctPLCodes(profile.Personal.UserID, Convert.ToInt64(ddlCustomer.SelectedItem.Value));
                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        long OrderheadID = 0;
                        long Deptid = long.Parse(ds.Tables[0].Rows[i]["deptId"].ToString());
                        long LocId = long.Parse(ds.Tables[0].Rows[i]["LocID"].ToString());
                        long CompanyID = long.Parse(ds.Tables[0].Rows[i]["ParentID"].ToString());
                        long ProjectID = long.Parse(ds.Tables[0].Rows[i]["ProjID"].ToString());
                        long SiteID = long.Parse(ds.Tables[0].Rows[i]["SiteCodeID"].ToString());

                        //apply loop for disticnt sku from order combination and pass to query only sku record get inserted in order 
                        // DataSet dsSKU = new DataSet();
                        // dsSKU = Import.GetDistinctSKU(Deptid, LocId, CompanyID, ProjectID, SiteID, profile.Personal.UserID, Convert.ToInt64(ddlCustomer.SelectedItem.Value));


                        OrderheadID = Import.NewInsertVTImportdata(hdnobject.Value, Deptid, LocId, ProjectID, SiteID, profile.Personal.UserID, Convert.ToInt64(ddlCustomer.SelectedItem.Value));
                        if (OrderheadID != 0)
                        {
                            lblImportSuccess.Text = "Data Importing Successfully Finished!";
                            objService.AddIntomMessageTransNew(OrderheadID, profile.DBConnection._constr);
                            // productClient.ImportMsgTransHeader(OrderheadID, profile.DBConnection._constr);

                            ////int x = objService.EmailSendWhenRequestSubmit(OrderheadID, profile.DBConnection._constr);
                            ////DataSet dss = new DataSet();
                            ////dss = objService.GetApproverDepartmentWise(Deptid, profile.DBConnection._constr);
                            ////for (int t = 0; t <= dss.Tables[0].Rows.Count - 1; t++)
                            ////{
                            ////    long approverid = Convert.ToInt64(dss.Tables[0].Rows[t]["UserId"].ToString());
                            ////    objService.EmailSendofApproved(approverid, OrderheadID, t, profile.DBConnection._constr);
                            ////}
                        }

                    }
                    // Result = Import.InsertImportLocation(hdnobject.Value, profile.Personal.UserID, profile.Personal.CompanyID, Convert.ToInt64(ddlCustomer.SelectedItem.Value));//, DSInsert);   //, profile.DBConnection._constr);
                    //  Import.DeleteFromtempTable(hdnobject.Value, profile.Personal.UserID, Convert.ToInt64(ddlCustomer.SelectedItem.Value));   //, profile.DBConnection._constr);

                }
                else
                {
                    /* string Status = "InProcess";
                     long WarehouseID = 0;
                     string TaskCode = DateTime.Now.ToString() + profile.Personal.UserID.ToString();
                     // it will add record to schedulare
                     Import.AddRecordToschedular(TaskCode, hdnobject.Value, profile.Personal.CompanyID, Convert.ToInt64(ddlCustomer.SelectedItem.Value), WarehouseID, CountRecord, Status, profile.Personal.UserID); //,profile.DBConnection._constr);
                     lblQueSchedule.Text = "Import request validated and added in the Queue ID : '" + TaskCode + "'";*/
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Import.Close();
                productClient.Close();
                objService.Close();
            }
        }

        protected void btnback_Click(object sender, EventArgs e)
        {
            CustomProfile profile = CustomProfile.GetProfile();
            //BrilliantWMS.ImportService.iImportClient Import = new iImportClient();
            ImportServiceClient Import = new ImportServiceClient();
            Import.DeleteFromtempTable(hdnobject.Value, profile.Personal.UserID, Convert.ToInt64(ddlCustomer.SelectedItem.Value));   //, profile.DBConnection._constr);
            pnlvalidate.Visible = false;
            pnlImport.Visible = true;
            btnnext.Enabled = true;
            Import.Close();

        }
        protected void GVImportView_OnRowDataBound(object sender, Obout.Grid.GridRowEventArgs e)
        {
            //  if (DataBinder.Eval(e.Row.DataItem, "Remark") != null)
            if (e.Row.Cells[GVImportView.Columns["ValidRemark"].Index].Text != "" || e.Row.Cells[GVImportView.Columns["ValidRemark"].Index].Text != null)
            {
                string Remark = e.Row.Cells[GVImportView.Columns["ValidRemark"].Index].Text;
                if (Remark != "")
                {
                    e.Row.BackColor = System.Drawing.Color.Cyan;
                    Validate = "False";
                }
            }
            if (e.Row.Cells[GVImportView.Columns["SerialNo"].Index].Text != "" || e.Row.Cells[GVImportView.Columns["SerialNo"].Index].Text != null)
            {
                string Serialflag = e.Row.Cells[GVImportView.Columns["SerialNo"].Index].Text;
                if (Serialflag != "")
                {
                    //  e.Row.BackColor = System.Drawing.Color.Cyan;
                    chkserial = "False";
                }
            }

        }

        protected void btnfinish_Click(object sender, EventArgs e)
        {
            pnlfinish.Visible = false;
            pnlvalidate.Visible = false;
            pnlImport.Visible = true;
        }

        //public void BindCustomer()
        //{
        //    ImportServiceClient Import = new ImportServiceClient();
        //    CustomProfile profile = CustomProfile.GetProfile();
        //    try
        //    {
        //        DataSet ds = new DataSet();
        //        ds = Import.GetUserCustomer(profile.Personal.UserID, profile.DBConnection._constr);
        //        ddlCustomer.DataSource = ds; //warehouse.GetUserCustomer(profile.Personal.UserID, profile.DBConnection._constr);
        //        ddlCustomer.DataBind();
        //        //ListItem lstV = new ListItem { Text = "-Select-", Value = "0" };
        //        ddlCustomer.SelectedIndex = ddlCustomer.Items.IndexOf(ddlCustomer.Items.FindByValue(ds.Tables[0].Rows[0]["ID"].ToString()));
        //        // ddlCustomer.Items.Insert(0, lstV);
        //        hdnSelectedCustomer.Value = ds.Tables[0].Rows[0]["ID"].ToString();
        //}
        //    catch (Exception ex)
        //    { }
        //    finally
        //    {
        //        Import.Close();
        //    }
        //}
        public void BindCustomer()
        {
            iPartRequestClient objService = new iPartRequestClient();
            CustomProfile profile = CustomProfile.GetProfile();
            try
            {
                ddlCustomer.DataSource = objService.GetUserCustomer(profile.Personal.UserID, profile.Personal.CompanyID, profile.DBConnection._constr);
                ddlCustomer.DataBind();
            }
            catch (Exception ex)
            { }
            finally
            {
                objService.Close();
            }
        }

        [WebMethod]
        public static string PMMoveAddressToArchive()
        {
            string result = "";


            return result;
        }
    }

    public class SerialList
    {
        string SERIAL { get; set; }
        int QTY { get; set; }
    }
}