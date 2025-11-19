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

namespace PowerOnRentwebapp.PowerOnRent
{
    public partial class DirectImportVC : System.Web.UI.Page
    {
        ResourceManager rm;
        CultureInfo ci;
        SqlConnection con1 = new SqlConnection("");
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
                GetCompany();
            }
            UC_DateofBirth.startdate(DateTime.Now);
            lblmessagesuccess.Visible = false;
            Button12.Enabled = false;
            Button12.CssClass = "class1";
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
                List<mCompany> CompanyLst = new List<mCompany>();
                long UID = profile.Personal.UserID;
                string UserType = profile.Personal.UserType.ToString();
                if (UserType == "Admin")
                {
                    CompanyLst = objService.GetUserCompanyNameNEW(UID, profile.DBConnection._constr).ToList();
                }
                else
                {
                    CompanyLst = objService.GetCompanyName(profile.DBConnection._constr).ToList();
                }
                //ddlcompany.DataSource = ds;
                ddlcompany.DataSource = CompanyLst;
                ddlcompany.DataTextField = "Name";
                ddlcompany.DataValueField = "ID";
                ddlcompany.DataBind();
                ListItem lst = new ListItem { Text = "-Select-", Value = "0" };
                ddlcompany.Items.Insert(0, lst);
            }
            catch { }
            finally { productClient.Close(); }
        }

        public void getDepartment(long Companyid)
        {
            DataSet ds;
            CustomProfile profile = CustomProfile.GetProfile();
            iProductMasterClient productClient = new iProductMasterClient();
            ds = productClient.GetDepartment(Companyid, profile.DBConnection._constr);
            ddldepartment.DataSource = ds;
            ddldepartment.DataTextField = "Territory";
            ddldepartment.DataValueField = "ID";
            ddldepartment.DataBind();
            ListItem lst = new ListItem { Text = "-Select-", Value = "0" };
            ddldepartment.Items.Insert(0, lst);

        }
        public void GetLocation(long Companyid)
        {
            DataSet ds;
            CustomProfile profile = CustomProfile.GetProfile();
            iProductMasterClient productClient = new iProductMasterClient();
            ds = productClient.GetLocation(Companyid, profile.DBConnection._constr);
            ddllocation.DataSource = ds;
            ddllocation.DataTextField = "AddressLine1";
            ddllocation.DataValueField = "ID";
            ddllocation.DataBind();
            ListItem lst = new ListItem { Text = "-Select-", Value = "0" };
            ddllocation.Items.Insert(0, lst);
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

                /*Add By Suresh For Selected Department List Show */

                iUCCommonFilterClient UCCommonFilter = new iUCCommonFilterClient();

                //SiteLst = UCCommonFilter.GetAddedDepartmentList(int.Parse(ddlcompanyId.ToString()), profile.Personal.UserID, profile.DBConnection._constr).ToList();
                /* Add By Suresh For Selected Department List Show */

                if (profile.Personal.UserType == "Admin")
                {
                    ds = UCCommonFilter.GetAddedDepartmentListDS(int.Parse(ddlcompanyId.ToString()), profile.Personal.UserID, profile.DBConnection._constr);
                }
                else
                {
                    ds = productClient.GetDepartment(ddlcompanyId, profile.DBConnection._constr);
                }

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
            catch
            {
            }
            finally
            {
                productClient.Close();
            }
            return LocList;
        }

        [WebMethod]
        public static List<contact> GetLocation(object objreq)
        {
            iProductMasterClient productClient = new iProductMasterClient();
            CustomProfile profile = CustomProfile.GetProfile();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            List<contact> LocList = new List<contact>();
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary = (Dictionary<string, object>)objreq;
                //ds = ReceivableClient.GetProdLocations(ProdCode.Trim());
                long ddlcompanyId = long.Parse(dictionary["ddlcompanyId"].ToString());
                ds = productClient.GetLocation(ddlcompanyId, profile.DBConnection._constr);

                dt = ds.Tables[0];
                contact Loc = new contact();
                Loc.Name = "Select Location";
                Loc.Id = "0";
                LocList.Add(Loc);
                Loc = new contact();

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Loc.Id = dt.Rows[i]["ID"].ToString();
                        Loc.Name = dt.Rows[i]["AddressLine1"].ToString();
                        LocList.Add(Loc);
                        Loc = new contact();
                    }
                }
            }
            catch
            {
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



        protected void btnUploadPo_Click(object sender, EventArgs e)
        {
            con1.ConnectionString = ConfigurationManager.ConnectionStrings["elegantcrm7ConnectionString"].ConnectionString;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            iProductMasterClient productClient = new iProductMasterClient();
            CustomProfile profile = CustomProfile.GetProfile();
            string ObjectName = "BulkImport";
            DataSet ImportData = new DataSet();
            try
            {
                DataSet dsimp = new DataSet();
                dsimp = productClient.getImportStatus(ObjectName, profile.DBConnection._constr);
                string validation = "false'";
                if (dsimp.Tables.Count > 0)
                {
                    validation = dsimp.Tables[0].Rows[0]["result"].ToString();
                }
                if (validation == "true")
                {
                    WebMsgBox.MsgBox.Show("Another user is importing bulk order import. Please wait..");
                }
                else
                {

                    productClient.DeleteBulkOrderImport(profile.Personal.UserID, profile.DBConnection._constr);

                    string connString = "";
                    string path = FileuploadPO.PostedFile.FileName;
                    string strFileType = System.IO.Path.GetExtension(path).ToString().ToLower();

                    //  string Fullpath = Server.MapPath("../PowerOnRent/OrderImport/" + path);

                    string Fullpath = Server.MapPath("../Product/ImportPrice/" + path);


                    //  string Fullpath = @"C:\GWCVersion2\GWCTestVersion2\PowerOnRent\OrderImport\" + path;

                    // string Fullpath = "https://testoms.gwclogistics.com/GWCTestVersion2/PowerOnRent/OrderImport/1DirectOrderImportTest.xlsx";

                    FileuploadPO.PostedFile.SaveAs(Server.MapPath("../Product/ImportPrice/" + path));

                    //   FileuploadPO.PostedFile.SaveAs(Server.MapPath("../PowerOnRent/OrderImport/" + path));
                    //  FileuploadPO.PostedFile.SaveAs(@"C:\GWCVersion2\GWCTestVersion2\PowerOnRent\OrderImport\" + path);

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

                    dt.Columns.Add("CreatedBy", typeof(System.Int64));
                    foreach (DataRow dr in dt.Rows)
                    {

                        dr["CreatedBy"] = profile.Personal.UserID;

                    }
                    excelConnection.Close();
                    string StoreProcedure = "Import_Insert_BulkImport";
                    using (SqlCommand sqlCommand = new SqlCommand(StoreProcedure, con1))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@UserID", profile.Personal.UserID);
                        //cmd.Parameters.AddWithValue("@ValidateLoc", dt);
                        SqlParameter tvpParam = sqlCommand.Parameters.AddWithValue("@VirtualBulkData", dt);
                        tvpParam.SqlDbType = SqlDbType.Structured;
                        con1.Open();
                        sqlCommand.ExecuteNonQuery();
                        con1.Close();
                        // da.SelectCommand = cmd;

                    }
                    //OleDbDataReader dReader;
                    //dReader = cmd1.ExecuteReader();
                    //SqlBulkCopy sqlBulk = new SqlBulkCopy(con1);
                    //con1.Open();
                    //sqlBulk.DestinationTableName = "TempBulkOrderImport";
                    //sqlBulk.WriteToServer(dReader);
                    //excelConnection.Close();


                    ds = productClient.GetTempBulkDirectOrder(profile.Personal.UserID, profile.DBConnection._constr);
                    dt = ds.Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        //System.Threading.Thread.Sleep(8000);
                        // Label1.Text = "Upload Successful !!!";
                        lblmessagesuccess.Visible = true;
                        Button12.Enabled = true;
                        Button12.CssClass = "class2";
                        // adm.UpdateStudenttrim();

                        productClient.insertImportStatus(profile.Personal.UserID, ObjectName, profile.DBConnection._constr);
                    }
                    else
                    {
                        WebMsgBox.MsgBox.Show("Upload Not Successful, Please Upload Right Template");

                    }
                    //adm.UpdateStudenttrim();
                    //}

                    //getDepartment(long.Parse(hdncompanyid.Value));
                    // ddldepartment.SelectedIndex = ddldepartment.Items.IndexOf(ddldepartment.Items.FindByValue(hdndeptid.Value.ToString()));
                    // GetLocation(long.Parse(hdncompanyid.Value));
                    // ddllocation.SelectedIndex = ddllocation.Items.IndexOf(ddllocation.Items.FindByValue(hdnlocation.Value.ToString()));
                }
            }
            catch (Exception ex)
            {
                Login.Profile.ErrorHandling(ex, "DirectImportDVC.aspx", "btnUploadPo_Click");
                ScriptManager.RegisterStartupScript(this, GetType(), "showAlert", "showAlert('" + ex.ToString() + "','Error','#');", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Exception in Catch!');", true);
            }
            finally { con1.Close(); productClient.Close(); }

        }

        protected void Button12_Click(object sender, EventArgs e)
        {

            //Session.Add("CompanyIdPI", long.Parse(hdncompanyid.Value));
            //Session.Add("DepartmentIDPI", long.Parse(hdndeptid.Value));
            //Session.Add("LocationID", long.Parse(hdnlocation.Value));
            //Session.Add("PaymentID", long.Parse(ddlpayment.SelectedValue.ToString()));
            //DateTime date = Convert.ToDateTime(UC_DateofBirth.Date);
            //Session.Add("ExpDate", date);
            Response.Redirect("DirectBulkImport.aspx");

        }



        protected void Btncancel_Click(object sender, EventArgs e)
        {
            iProductMasterClient productClient = new iProductMasterClient();
            CustomProfile profile = CustomProfile.GetProfile();
            productClient.CancelBulkOrderImport(profile.Personal.UserID, profile.DBConnection._constr);
            productClient.Close();
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
                lbltext1.Text = rm.GetString("ImportOrder", ci);
                Lbl.Text = rm.GetString("DownloadImportOrder", ci);
                lblSelecFile.Text = rm.GetString("SelectImportFile", ci);
                btnUploadPo.Text = rm.GetString("Upload", ci);
                lbluploading.Text = rm.GetString("Uploadingfile", ci);
                lblmessagesuccess.Text = rm.GetString("UploadedSucessfully", ci);
                lblcompany.Text = rm.GetString("company", ci);
                lblDept.Text = rm.GetString("Department", ci);
                lbllocation.Text = rm.GetString("Location", ci);
                lblpaymentMethod.Text = rm.GetString("PaymentMethod", ci);
                lblexpDate.Text = rm.GetString("ExpDeliveryDate", ci);
                Button12.Text = rm.GetString("Next", ci);
                Btncancel.Text = rm.GetString("Cancel", ci);
            }
            catch { }

        }



    }
}