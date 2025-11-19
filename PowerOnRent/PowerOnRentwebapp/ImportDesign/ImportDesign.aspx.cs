using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PowerOnRentwebapp.Login;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Services;
using System.Web.UI.HtmlControls;
using System.IO;


namespace BrilliantWMS.ImportDesign
{
    public partial class ImportDesign : System.Web.UI.Page
    {
        string companyID = "";
        string custid = "";
        string userid = "";
        int currentPageNo = 1;
        protected void Page_Load(object sender, EventArgs e)

        {
            if (Request.QueryString["page"] != null)
            {
                currentPageNo = Convert.ToInt32(Request.QueryString["page"]);
            }
            if (!Page.IsPostBack)
            {
                FillObjectDDL();
                FillCustomerDDL();
                bindGridQueryList();
                TabContainerQueryBuilder.ActiveTabIndex = 1;
                tabQueryBuilderEditor.Visible = false;
                GetObjectName();
            }
            /* Disable Buttons */
            HtmlInputButton btnEdit = (HtmlInputButton)UCToolbar1.FindControl("btnEdit");
            btnEdit.Visible = false;
            Button btnAddNew = (Button)UCToolbar1.FindControl("btnAddNew");
            btnAddNew.Visible = true;
            //Button btnExport = (Button)UCToolbar1.FindControl("btnExport");
            //btnExport.Visible = false;
            //Button btnImport = (Button)UCToolbar1.FindControl("btnImport");
            //btnImport.Visible = false;
            //Button btnPrint = (Button)UCToolbar1.FindControl("btnPrint");
            //btnPrint.Visible = false;
             Button btnSave = (Button)UCToolbar1.FindControl("btnSave");
            btnSave.Visible = false;
            // btnSave.Attributes.Add("onclick", "return SaveColumnDatanew();");
             Toolbar1.SetSaveRight(true, "Not Allowed");
            Toolbar1.SetAddNewRight(false, "Not Allowed");
            Toolbar1.SetEditRight(false, "Not Allowed");
            Toolbar1.SetExportRight(false, "Not Allowed");
            Toolbar1.SetImportRight(false, "Not Allowed");
            Toolbar1.SetClearRight(false, "Not Allowed");
            this.UCToolbar1.evClickAddNew += pageAddNew;
            TabContainerQueryBuilder.ActiveTabIndex = 1;
            tabQueryBuilderEditor.Visible = false;
            tabQueryBuilderList.Visible = true;
        }

        protected void imgBtnEdit_OnClick(object sender, ImageClickEventArgs e)
        {
            CustomProfile profile = CustomProfile.GetProfile();
            PowerOnRentwebapp.DocumentService.iUC_AttachDocumentClient DocumentClient = new PowerOnRentwebapp.DocumentService.iUC_AttachDocumentClient();
            DataSet ds1 = new DataSet();
            TabContainerQueryBuilder.ActiveTabIndex = 2;
            tabQueryBuilderEditor.Visible = true;
            try
            {
                ImageButton imgbtn = (ImageButton)sender;
                string getQueryId = imgbtn.ToolTip;
                hdnImportID.Value = getQueryId;
                ds1.Reset(); 
                ds1 = DocumentClient.GetImportDesignDetailData_Edit(getQueryId, profile.DBConnection._constr);
                string currentSqlTitle = "";
                string TableName = "";
                string cunstomerid = "";
                string Viewname = "";
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    currentSqlTitle = ds1.Tables[0].Rows[0]["ObjectName"].ToString();
                    TableName = ds1.Tables[0].Rows[0]["Vievname"].ToString(); 
                    cunstomerid = ds1.Tables[0].Rows[0]["CustomerID"].ToString();
                    Viewname = ds1.Tables[0].Rows[0]["ImportQuery"].ToString();
                    Button btnSave = (Button)UCToolbar1.FindControl("btnSave");
                    //   btnSave.Text = "Update";
                }
                hdnObjectName.Value = currentSqlTitle;
                ddlobject.SelectedItem.Text = currentSqlTitle;
                //this.UCToolbar1.ToolbarAccess("Edit");
                ddlcustomer.SelectedValue = cunstomerid;
                getdetailofImportdata(getQueryId, Viewname);
            }
            catch (Exception ex) { /*Login.Profile.ErrorHandling(ex, this, "Importdesign", "imgBtnEdit_OnClick");*/ }
            finally
            {
                DocumentClient.Close();
            }
        }
        protected void getdetailofImportdata(string impid, string TableName)
        {
            CustomProfile profile = CustomProfile.GetProfile();
            DataSet ds1 = new DataSet();
            PowerOnRentwebapp.DocumentService.iUC_AttachDocumentClient DocumentClient = new PowerOnRentwebapp.DocumentService.iUC_AttachDocumentClient();
            try
            {
                ds1.Reset();
                ds1 = DocumentClient.GetEditedImportData(impid, TableName, profile.DBConnection._constr);
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    int dscnt = ds1.Tables[0].Rows.Count;
                    dataColumnHolder.Controls.Clear();
                    for (int i = 0; i <= dscnt - 1; i++)
                    {
                        string check = ds1.Tables[0].Rows[i]["ischeck"].ToString();
                        HtmlGenericControl divcontrol = new HtmlGenericControl();
                        var strColIsChecked = "";
                        var strIsNullChecked = "";
                        if (check == "Y")
                        {
                            string checkIsnull = ds1.Tables[0].Rows[i]["IsNULL"].ToString();
                            if (checkIsnull == "1")
                            {
                                strColIsChecked = " checked='checked'";
                                strIsNullChecked = " checked='checked'";
                                //divcontrol.InnerHtml = "<div class='winQueryColumnItem sqlCol' data-type=" + ds1.Tables[0].Rows[i]["fieldName"].ToString() + " data-label=" + ds1.Tables[0].Rows[i]["fieldName"].ToString() + " ><li><table class='dataTable' style='width:90%'><tr><td style='width:50%'><input type='checkbox' class='chkIsSelected' value=" + ds1.Tables[0].Rows[i]["fieldName"].ToString() + "  checked>" + ds1.Tables[0].Rows[i]["fieldName"].ToString() + "</td><td style='width:20%' align='center' > <select name='dataType'class='dataType'><option value='Number'>Number</option><option value='String'>String</option> <option value='Float'>Float</option> <option value='DD/MM/YY'>DD/MM/YY</option> <option value='MM/DD/YYYY'>MM/DD/YYYY</option> <option value='YYYY/MM/DD'>YYYY/MM/DD</option><option value=" + ds1.Tables[0].Rows[i]["fieldDatatype"].ToString() + " selected>" + ds1.Tables[0].Rows[i]["fieldDatatype"].ToString() + "</option></select ></td > <td style='width:10%'><input type='checkbox' class='isNull' value=" + ds1.Tables[0].Rows[i]["IsNULL"].ToString() + " checked /></td> <td style='width:20%'><input type='Text' maxlength='4' size='4' class='length' value=" + ds1.Tables[0].Rows[i]["Flength"].ToString() + "></td></tr ></table ></li></div > ";
                            }
                            else
                            {
                                strColIsChecked = " checked='checked'";
                                //   divcontrol.InnerHtml = "<div class='winQueryColumnItem sqlCol' data-type=" + ds1.Tables[0].Rows[i]["fieldName"].ToString() + " data-label=" + ds1.Tables[0].Rows[i]["fieldName"].ToString() + " ><li><table class='dataTable' style='width:90%'><tr><td style='width:50%'><input type='checkbox' class='chkIsSelected' value=" + ds1.Tables[0].Rows[i]["fieldName"].ToString() + "  checked>" + ds1.Tables[0].Rows[i]["fieldName"].ToString() + "</td><td style='width:20%' align='center' > <select name='dataType'class='dataType'><option value='Number'>Number</option><option value='String'>String</option> <option value='Float'>Float</option> <option value='DD/MM/YY'>DD/MM/YY</option> <option value='MM/DD/YYYY'>MM/DD/YYYY</option> <option value='YYYY/MM/DD'>YYYY/MM/DD</option><option value=" + ds1.Tables[0].Rows[i]["fieldDatatype"].ToString() + " selected>" + ds1.Tables[0].Rows[i]["fieldDatatype"].ToString() + "</option></select ></td > <td style='width:10%'><input type='checkbox'class='isNull' value='Is Null' /></td> <td style='width:20%'><input type='Text' maxlength='4' size='4' class='length' value=" + ds1.Tables[0].Rows[i]["Flength"].ToString() + " ></td></tr ></table ></li></div > ";
                            }

                        }
                        /* else
                         {

                             divcontrol.InnerHtml = "<div class='winQueryColumnItem sqlCol' data-type=" + ds1.Tables[0].Rows[i]["fieldName"].ToString() + " data-label=" + ds1.Tables[0].Rows[i]["fieldName"].ToString() + " ><li><table class='dataTable' style='width:90%'><tr><td style='width:50%'><input type='checkbox' class='chkIsSelected' value=" + ds1.Tables[0].Rows[i]["fieldName"].ToString() + "  />" + ds1.Tables[0].Rows[i]["fieldName"].ToString() + "</td><td style='width:20%' align='center' > <select name='dataType'class='dataType'><option value='Number'>Number</option><option value='String'>String</option> <option value='Float'>Float</option> <option value='DD/MM/YY'>DD/MM/YY</option> <option value='MM/DD/YYYY'>MM/DD/YYYY</option> <option value='YYYY/MM/DD'>YYYY/MM/DD</option></select ></td > <td style='width:10%'><input type='checkbox'class='isNull' value='Is Null'/></td> <td style='width:20%'><input type='Text' maxlength='4' size='4' class='length' value='' /></td></tr ></table ></li></div > ";
                         }*/
                        string Flength1 = ds1.Tables[0].Rows[i]["Flength"].ToString();
                        if (Flength1 == "0")
                        {
                            Flength1 = "";
                        }
                        string selectedOptVal = ds1.Tables[0].Rows[i]["fieldDatatype"].ToString();
                       
                        divcontrol.InnerHtml = "<div class='winQueryColumnItem sqlCol' data-type=" + ds1.Tables[0].Rows[i]["fieldName"].ToString() + " data-label=" + ds1.Tables[0].Rows[i]["fieldName"].ToString() + " ><li><table class='dataTable' style='width:90%'><tr><td style='width:50%'><input type='checkbox' class='chkIsSelected' value=" + ds1.Tables[0].Rows[i]["fieldName"].ToString() + " "+ strColIsChecked + ">" + ds1.Tables[0].Rows[i]["fieldName"].ToString() + "</td><td style='width:20%' align='center' > <select name='dataType'class='dataType'>"+
                           
                            generateOption("String", selectedOptVal) +
                            generateOption("Number", selectedOptVal) +
                            generateOption("Float", selectedOptVal) +
                            generateOption("DD/MM/YY", selectedOptVal) +
                            generateOption("MM/DD/YYYY", selectedOptVal) +
                            generateOption("YYYY/MM/DD", selectedOptVal) +
                            "</select ></td > <td style='width:10%'><input type='checkbox' class='isNull' value=" + ds1.Tables[0].Rows[i]["IsNULL"].ToString() + " "+ strIsNullChecked + " /></td> <td style='width:20%'><input type='Text' maxlength='4' size='4' class='length' value=" + Flength1 + "></td></tr ></table ></li></div > ";
                        dataColumnHolder.Controls.Add(divcontrol);
                    }

                }
            }
            catch (Exception ex) { /*Login.Profile.ErrorHandling(ex, this, "Importdesign", "getdetailofImportdata");*/ }
            finally
            {
                DocumentClient.Close();
            }
            
        }
        protected string generateOption(string optLabel, string selectedValue)
        {
            string strSelected = "";
            if (optLabel == selectedValue)
            {
                strSelected = "selected = 'selected'";
            }
            string ddlOpt = "<option value=\""+ optLabel + "\" "+ strSelected + ">"+ optLabel + "</option>";
            return ddlOpt;
        }
        //protected void btnSaveQueryBuilderNotification_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string getNotificationTo = txtQueryNotificationTo.Text;
        //        string getInterval = hdnQueryInterval.Value;
        //        string getSendAt = qbDDListHH.SelectedValue + ":" + qbDDListMM.SelectedValue + " " + qbDDListAMPM.SelectedValue;
        //        string getDayOfWeek = hdnQueryDayOfWeek.Value;
        //        string getDayOfMonth = "";
        //        if (hdnQueryDayOfMonth.Value == "MidDayOfMonth")
        //        {
        //            getDayOfMonth = qbDDListDate.SelectedValue;
        //        }
        //        else
        //        {
        //            getDayOfMonth = hdnQueryDayOfMonth.Value;
        //        }
        //        long getUserId = long.Parse(userid);
        //        string getObjectName = "";
        //        long getQueryId = long.Parse(hdnQueryIdForNotification.Value);
        //        long getNotificationID = long.Parse(hdnNotificationId.Value);
        //        long getCreatedBy = getUserId;

        //        string getCurrentDate = DateTime.Now.ToString("MM-dd-yyyy");
        //        SqlConnection conn = new SqlConnection("");
        //        conn.ConnectionString = ConfigurationManager.ConnectionStrings["elegantcrm7ConnectionString"].ConnectionString;
        //        string getQueryID = hdnQueryId.Value;
        //        string query = "";

        //        query = "exec SP_QueryBuilderAddUpdateNotifications @Title, @Message, @NotificationTo, @Interval, @SendAt, @NotiDayOfWeek, @NotiDayOfMonth, @UserID, @ObjectName, @QueryId, @CreatedBy, @NotificationId;";
        //        conn.Open();

        //        SqlCommand myCommand = new SqlCommand(query, conn);

        //        myCommand.Parameters.AddWithValue("@Title", "");
        //        myCommand.Parameters.AddWithValue("@Message", "");
        //        myCommand.Parameters.AddWithValue("@NotificationTo", getNotificationTo);
        //        myCommand.Parameters.AddWithValue("@Interval", getInterval);
        //        myCommand.Parameters.AddWithValue("@SendAt", getSendAt);
        //        myCommand.Parameters.AddWithValue("@NotiDayOfWeek", getDayOfWeek);
        //        myCommand.Parameters.AddWithValue("@NotiDayOfMonth", getDayOfMonth);
        //        myCommand.Parameters.AddWithValue("@UserID", getUserId);
        //        myCommand.Parameters.AddWithValue("@ObjectName", getObjectName);
        //        myCommand.Parameters.AddWithValue("@QueryId", getQueryId);
        //        myCommand.Parameters.AddWithValue("@CreatedBy", getCreatedBy);
        //        myCommand.Parameters.AddWithValue("@NotificationId", getNotificationID);

        //        // ... other parameters
        //        myCommand.ExecuteNonQuery();
        //        conn.Close();
        //        bindGridQueryList();
        //        TabContainerQueryBuilder.ActiveTabIndex = 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        Login.Profile.ErrorHandling(ex, this, "Query Builder Notification", "btnSaveQueryBuilderNotification_Click");
        //    }
        //}
        //protected void btnSaveQueryBuilder_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        long getUserId = long.Parse(userid);
        //        string getSqlQueryTitle = txtShortcutTitle.Text;
        //        string getObjectName = hdnObjectName.Value;
        //        string getSqlQuery = hdnSQLQueryText.Value;
        //        string getSqlHtmlObj = hdnHtmlWhereObj.Value;
        //        long getCreatedBy = getUserId;
        //        string getCurrentDate = DateTime.Now.ToString("MM-dd-yyyy");

        //        SqlConnection conn = new SqlConnection("");
        //        conn.ConnectionString = ConfigurationManager.ConnectionStrings["elegantcrm7ConnectionString"].ConnectionString;
        //        string query = "";
        //        string getQueryID = hdnQueryId.Value;

        //        query = "exec SP_QueryBuilderAddUpdateDetail @UserID, @QueryID, @ObjectName, @SqlQuery, @SqlHtmlObj, @CreatedBy, @CreationDate, @SqlTitle";

        //        conn.Open();
        //        SqlCommand myCommand = new SqlCommand(query, conn);
        //        myCommand.Parameters.AddWithValue("@UserID", getUserId);
        //        myCommand.Parameters.AddWithValue("@QueryID", getQueryID);
        //        myCommand.Parameters.AddWithValue("@ObjectName", getObjectName);
        //        myCommand.Parameters.AddWithValue("@SqlQuery", getSqlQuery);
        //        myCommand.Parameters.AddWithValue("@SqlHtmlObj", getSqlHtmlObj);
        //        myCommand.Parameters.AddWithValue("@CreatedBy", getCreatedBy);
        //        myCommand.Parameters.AddWithValue("@CreationDate", getCurrentDate);
        //        myCommand.Parameters.AddWithValue("@SqlTitle", getSqlQueryTitle);
        //        // ... other parameters
        //        myCommand.ExecuteNonQuery();
        //        conn.Close();
        //        bindGridQueryList();
        //        TabContainerQueryBuilder.ActiveTabIndex = 0;
        //    }
        //    catch (Exception ex) { Login.Profile.ErrorHandling(ex, this, "Query Builder Title", "btnSaveQueryBuilder_Click"); }
        //}

        protected void bindGridQueryList()
        {
            CustomProfile profile = CustomProfile.GetProfile();
            long GetUserId = profile.Personal.UserID;
            long CompanyID = profile.Personal.CompanyID;
            grvQueryList.ClearPreviousDataSource();
            grvQueryList.DataSource = null;
            try
            {
                DataSet ds1 = new DataSet();
                ds1.Reset();
                SqlConnection conn = new SqlConnection("");
                string str = "";
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["elegantcrm7ConnectionString"].ConnectionString;
                str = "exec sp_ImportDesignDetailData ";
                SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(str, conn);
                ds1.Reset();
                da.Fill(ds1);
                grvQueryList.DataSource = ds1;
                grvQueryList.DataBind();
                int getRowCount = ds1.Tables[0].Rows.Count;
            }
            catch (System.Exception ex)
            {
                //Login.Profile.ErrorHandling(ex, "ImportDesign", "bindGridQueryList");
            }
            finally
            {

            }
        }

        //protected void grvQueryList_Rebind(object sender, EventArgs e)
        //{

        //    try
        //    {
        //        CreateExcelTemplte();
        //    }
        //    catch (System.Exception ex)
        //    {
        //        Login.Profile.ErrorHandling(ex, "ImportDesign", "grvQueryList_Rebind");
        //    }
        //    finally
        //    {

        //    }
        //}
        //protected void pageSave(Object sender, BrilliantWMS.ToolbarService.iUCToolbarClient e)
        //{
        
        //}

        protected void pageAddNew(Object sender, PowerOnRentwebapp.ToolbarService.iUCToolbarClient e)
        {
            try
            {
                FillObjectDDL();
                bindGridQueryList();
                FillCustomerDDL();
                GetObjectName();
                TabContainerQueryBuilder.ActiveTabIndex = 1;
                tabQueryBuilderEditor.Visible = true;
                hdnImportID.Value = "";
                ddlcustomer.SelectedItem.Value = "0";
                ddlobject.SelectedItem.Value = "0";
            }
            catch (Exception ex)
            { /*System.Web.UI.WebControls.Login.Profile.ErrorHandling(ex, "ImportDesign", "pageAddNew");*/ }

            finally { }
            
        }
        private void GetObjectName()
         {
         CustomProfile profile = CustomProfile.GetProfile();
            PowerOnRentwebapp.DocumentService.iUC_AttachDocumentClient DocumentClient = new PowerOnRentwebapp.DocumentService.iUC_AttachDocumentClient();
            DataSet ds1 = new DataSet();
            try
            {
                ds1.Reset();
                SqlConnection conn = new SqlConnection("");
                long companyID = 0;
                companyID = profile.Personal.CompanyID;
                ds1 = DocumentClient.GetQueryBuilderObjectNew(companyID, profile.DBConnection._constr);
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    string objectList = "";
                    int dscnt = ds1.Tables[0].Rows.Count;
                    for (int i = 0; i <= dscnt - 1; i++)
                    {
                        //objectList += "<li data-value=\"" + ds1.Tables[0].Rows[i]["queryname"].ToString() + "\" ><i class=\"fas fa-caret-right\"></i>" + ds1.Tables[0].Rows[i]["queryname"].ToString() + "</li>";

                        objectList += "<li data-value=\"" + ds1.Tables[0].Rows[i]["importTitle"].ToString() + "\" ><i class=\"fas fa-caret-right\"></i>" + ds1.Tables[0].Rows[i]["importTitle"].ToString() + "</li>";
                    }
                    dataObjectHolder.InnerHtml = objectList;
                }
            }
            catch (System.Exception ex)
            {
                //Login.Profile.ErrorHandling(ex, "ImportDesign", "GetObjectName");
            }
            finally
            {
                DocumentClient.Close();
            }
        }

       
        //[WebMethod]
        //public static string GetObjectColumnCheck(string objName)
        //{
        //    string s = "12";
        //    string b = "4";
        //    return "hello";
        //}

        [WebMethod]
        public static string GetObjectColumn(string objName)
        {
            CustomProfile profile = CustomProfile.GetProfile();
            string result = "";
            try
            {
                //CustomProfile profile = CustomProfile.GetProfile();
                DataSet ds1 = new DataSet();
                ds1.Reset();
                SqlConnection conn = new SqlConnection("");
                string str = "";
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["elegantcrm7ConnectionString"].ConnectionString;
                //  str = "select top(1)WarehoueID from mUserWarehouse where UserID=" + userID + "";
                str = "SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" + objName + "'";
                SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(str, conn);
                ds1.Reset();
                da.Fill(ds1);
                int getRowCount = ds1.Tables[0].Rows.Count;

                if (getRowCount > 0)
                {
                    for (int rc = 0; rc <= (getRowCount - 1); rc++)
                    {
                        string getColumnName = ds1.Tables[0].Rows[rc]["COLUMN_NAME"].ToString();
                        string getDataType = ds1.Tables[0].Rows[rc]["DATA_TYPE"].ToString();
                        if (rc == 0)
                        {
                            result += getColumnName + ":" + getDataType;
                        }
                        else
                        {
                            result += "|" + getColumnName + ":" + getDataType;
                        }

                    }
                }
                conn.Close();
                result = "success";
            }
            catch (System.Exception ex)
            {
                //Login.Profile.ErrorHandling(ex, "ImportDesign", "GetObjectColumn");
            }
            finally
            {
            }
            return result;
        }
        //protected void pageExport(Object sender, BrilliantWMS.ToolbarService.iUCToolbarClient e)
        //{
        //    String FileName = "SQLResult.xls";
        //    String FilePath = AppDomain.CurrentDomain.BaseDirectory + FileName;
        //    StreamWriter wr = new StreamWriter(FilePath);
        //    try
        //    {
        //        CustomProfile profile = CustomProfile.GetProfile();
        //        DataTable dt = new DataTable();
        //        long companyID = profile.Personal.CompanyID;
        //        long customerID = profile.Personal.CustomerId;
        //        long userID = profile.Personal.UserID;
        //        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
        //        string str = "";
        //        //str = hdnSQLQueryText.Value;
        //        string qry = hdnSQLQueryText.Value;
        //        string where = "";
        //        if (hdnwhere.Value != "")
        //        {
        //            where = hdnwhere.Value;
        //            where = where.Replace("'", "''");
        //            if (qry.Contains("WHERE"))
        //            {
        //                qry = qry + " AND " + where;
        //            }
        //            else
        //            {
        //                qry = qry + " where " + where;
        //            }
        //        }
        //        str = GetQueryWithCompanyID(qry, profile.Personal.CompanyID);
        //        if (str != string.Empty)
        //        {
        //            SqlCommand cmd = new SqlCommand(str, con);
        //            SqlDataAdapter da = new SqlDataAdapter(cmd);
        //            da.Fill(dt);
        //            for (int i = 0; i < dt.Columns.Count; i++)
        //            {
        //                wr.Write(dt.Columns[i].ToString().ToUpper() + "\t");
        //            }

        //            wr.WriteLine();

        //            //write rows to excel file
        //            for (int i = 0; i < (dt.Rows.Count); i++)
        //            {
        //                for (int j = 0; j < dt.Columns.Count; j++)
        //                {
        //                    if (dt.Rows[i][j] != null)
        //                    {
        //                        wr.Write(Convert.ToString(dt.Rows[i][j]) + "\t");
        //                    }
        //                    else
        //                    {
        //                        wr.Write("\t");
        //                    }
        //                }
        //                //go to next line
        //                wr.WriteLine();
        //            }
        //            //close file
        //            wr.Close();

        //            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
        //            response.ClearContent();
        //            response.Clear();
        //            response.ContentType = "application/vnd.ms-excel";
        //            response.AddHeader("Content-Disposition", "attachment; filename=" + FileName + "");
        //            response.TransmitFile(FilePath);
        //            response.Flush();
        //            response.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public string GetQueryWithCompanyID(string SQLQueryText, long CompanyID)
        //{
        //    CustomProfile profile = CustomProfile.GetProfile();
        //    string rslt = "";
        //    try
        //    {
        //        DataSet ds1 = new DataSet();
        //        ds1.Reset();
        //        SqlConnection conn = new SqlConnection("");
        //        string str = "";
        //        conn.ConnectionString = ConfigurationManager.ConnectionStrings["elegantcrm7ConnectionString"].ConnectionString;
        //        str = "exec RecordsofQryWithCompanyID '" + SQLQueryText + "'," + CompanyID + "";
        //        SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(str, conn);
        //        ds1.Reset();
        //        da.Fill(ds1);
        //        rslt = ds1.Tables[0].Rows[0]["fnlqry"].ToString();
        //    }
        //    catch (System.Exception ex)
        //    {
        //        Login.Profile.ErrorHandling(ex, "Query Builder", "executesqldata");
        //    }
        //    finally
        //    { }
        //    return rslt;
        //}

        private string getColumnDetails(string query)
        {
            CustomProfile profile = CustomProfile.GetProfile();
            DataSet ds = new DataSet();
            ds.Reset();
            SqlConnection conn = new SqlConnection("");
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["elegantcrm7ConnectionString"].ConnectionString;
            string reportGridHeader = "";
            try
            {
                SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(query, conn);
                ds.Reset();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    reportGridHeader += "<tr>";
                    for (int j = 0; j <= ds.Tables[0].Columns.Count - 1; j++)
                    {
                        reportGridHeader += "<td class='reportHeaderCell'>" + ds.Tables[0].Columns[j].ColumnName.ToString() + "</td>";
                    }
                    reportGridHeader += "</tr>";
                    // Generate Filter Cell
                    reportGridHeader += "<tr>";
                    //reportGridHeader += "<td class='reportHeaderCell'> &nbsp; </td>";
                    for (int j = 0; j <= ds.Tables[0].Columns.Count - 1; j++)
                    {
                        reportGridHeader += "<td class='reportHeaderCell'>" + generateFilterCell(ds.Tables[0].Columns[j].ColumnName.ToString()) + "</td>";
                    }
                    reportGridHeader += "</tr>";
                    // Generate Filter Cell
                }
            }
            catch (Exception ex)
            {
                //Login.Profile.ErrorHandling(ex, "ImportDesign", "getColumnDetails");
            }
            finally
            {

            }
            return reportGridHeader;
        }
        [WebMethod]
        public static int SaveColumnDatanew(object obj1)
        {

            int result = 0;
            CustomProfile profile = CustomProfile.GetProfile();
            PowerOnRentwebapp.DocumentService.iUC_AttachDocumentClient DocumentClient = new PowerOnRentwebapp.DocumentService.iUC_AttachDocumentClient();
            try
            {
                long companyID = profile.Personal.CompanyID;
                //long customerID = profile.Personal.CustomerId;
                long userID = profile.Personal.UserID;
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary = (Dictionary<string, object>)obj1;
                string dataColumn, viewName, objectName;
                dataColumn = dictionary["ColumnData"].ToString();
                objectName = dictionary["objectName"].ToString();
                viewName = dictionary["viewName"].ToString();
                long customerID = Convert.ToInt64(dictionary["Customer"].ToString());
                int importID = 0; 
                if (dictionary["Ipmoprtid"].ToString() == "")
                {
                    importID = 0;
                    DataSet ds = new DataSet();
                    ds = DocumentClient.getCountImportTemplate(objectName, customerID, profile.DBConnection._constr);
                    result = Int16.Parse(ds.Tables[0].Rows[0]["Cnt"].ToString());
                    if (result == 0)
                    {
                        result = DocumentClient.SaveColumnData(dataColumn, objectName, viewName, companyID, customerID, importID, profile.DBConnection._constr);
                        result = 1;
                    }
                    else
                    {
                         result = 0;
                    }
                }
                else
                {
                       importID = Convert.ToInt32(dictionary["Ipmoprtid"]);
                    DataSet ds1 = new DataSet();
                   ds1 = DocumentClient.getObjectName(objectName,profile.DBConnection._constr);
                    string objectname1 = ds1.Tables[0].Rows[0]["OBJECTID"].ToString();
                    result = DocumentClient.SaveColumnData(dataColumn, objectname1, viewName, companyID, customerID, importID, profile.DBConnection._constr);
                     result = 2;
                }   
            }
            catch(Exception ex)
            {
                //Login.Profile.ErrorHandling(ex, "ImportDesign", "SaveColumnDatanew");
            }
            finally { DocumentClient.Close(); }
            return result;
        }
        protected void FillCustomerDDL()
        {
            DataSet ds1 = new DataSet();
            CustomProfile profile = CustomProfile.GetProfile();
            PowerOnRentwebapp.DocumentService.iUC_AttachDocumentClient DocumentClient = new PowerOnRentwebapp.DocumentService.iUC_AttachDocumentClient();
            try
            {
                ddlcustomer.Items.Clear();
                long Companyid = profile.Personal.CompanyID;
                ds1 = DocumentClient.getCustomer(Companyid,profile.DBConnection._constr);
                ddlcustomer.DataSource = ds1;
                ddlcustomer.DataBind();
                ListItem lst = new ListItem { Text = "-Select-", Value = "0" };
                ddlcustomer.Items.Insert(0, lst);
               // ddlcustomer.SelectedIndex = 1;

            }
            catch (Exception ex) { /*Login.Profile.ErrorHandling(ex, "ImportDesign", "FillCustomerDDL");*/ }
            finally { DocumentClient.Close(); }

        }
        protected void FillObjectDDL()
        {
            DataSet ds1 = new DataSet();
            CustomProfile profile = CustomProfile.GetProfile();
            PowerOnRentwebapp.DocumentService.iUC_AttachDocumentClient DocumentClient = new PowerOnRentwebapp.DocumentService.iUC_AttachDocumentClient();
            try
            {
                ddlobject.Items.Clear();
                long UserID = profile.Personal.UserID;
                ds1 = DocumentClient.FillObjectDDL(profile.DBConnection._constr);
                ddlobject.DataSource = ds1;
                ddlobject.DataBind();
                ListItem lst = new ListItem { Text = "-Select-", Value = "0" };
                ddlobject.Items.Insert(0, lst);
                //ddlobject.SelectedIndex = 1;

            }
            catch (Exception ex) { /*Login.Profile.ErrorHandling(ex, "ImportDesign", "FillObjectDDL");*/ }
            finally { DocumentClient.Close(); }
        }
        private string generateFilterCell(string columnName)
        {
            string myCellFilter = "<div class=\"reportFilterContent\" data-value=\"" + columnName + "\">" +
"<select id=\"selFl_" + columnName + "\" value=\"\" onchange=\"updateFilter('" + columnName + "');\">" +
"<option value=\"\">No Filter</option>" +
"<option value=\"Contains\">Contains</option>" +
"<option value=\"Does Not Contain\">Does Not Contain</option>" +
"<option value=\"Starts With\">Starts With</option>" +
"<option value=\"Ends With\">Ends With</option>" +
"<option value=\"Equal To\">Equal To</option>" +
"<option value=\"Not Equal To\">Not Equal To</option>" +
"<option value=\"In Between\">In Between</option>" +
"<option value=\"Smaller Than\">Smaller Than</option>" +
"<option value=\"Greater Than\">Greater Than</option>" +
"<option value=\"Smaller Than Or Equal To\">Smaller Than Or Equal To</option>" +
"<option value=\"Greater Than Or Equal To\">Greater Than Or Equal To</option>" +
"<option value=\"Is Null\">Is Null</option>" +
"<option value=\"Is Not Null\">Is Not Null</option>" +
"<option value=\"Is Empty\">Is Empty</option>" +
"<option value=\"Is Not Empty\">Is Not Empty</option>" +
"</select>" +
"<input type=\"text\" id=\"inFl_" + columnName + "\" value=\"\" onchange=\"updateFilterValue('" + columnName + "');\" style=\"\">" +
"<input type=\"text\" id=\"inFl1_" + columnName + "\" class=\"inputInBetween\" value=\"\" onchange=\"updateFilterValue('" + columnName + "');\" style=\"display: none;\">" +
"<input type=\"text\" id=\"inFl2_" + columnName + "\" class=\"inputInBetween\" value=\"\" onchange=\"updateFilterValue('" + columnName + "');\" style=\"display: none;\">" +
"</div>";
            return myCellFilter;
        }

        protected void grvQueryList_Rebind(object sender, EventArgs e)
        {
            bindGridQueryList();
            TabContainerQueryBuilder.ActiveTabIndex = 1;
            tabQueryBuilderEditor.Visible = false;
            tabQueryBuilderList.Visible = true;
        }
        [WebMethod]
       public static long  CheckImportDataForObj(string Customer, string ddlObject)
        {
            long result = 0;
            DataSet ds = new DataSet();
            CustomProfile profile = CustomProfile.GetProfile();
            PowerOnRentwebapp.DocumentService.iUC_AttachDocumentClient DocumentClient = new PowerOnRentwebapp.DocumentService.iUC_AttachDocumentClient();
            string objectName = "";
            long customerID = 0;
            try
            {
                customerID = Convert.ToInt32(Customer);
                objectName = ddlObject;
                ds = DocumentClient.getCountImportTemplate(objectName, customerID, profile.DBConnection._constr);
                result = Int16.Parse(ds.Tables[0].Rows[0]["Cnt"].ToString());
            }
            catch { }
            finally { }
            return result;
        }
    }
}