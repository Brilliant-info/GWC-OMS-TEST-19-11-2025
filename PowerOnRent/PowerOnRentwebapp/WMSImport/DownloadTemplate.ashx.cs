///using BrilliantWMS.Login;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Microsoft.Office.Interop.Excel;
using PowerOnRentwebapp.Login;

namespace BrilliantWMS.WMSImport
{
    /// <summary>
    /// Summary description for DownloadTemplate
    /// </summary>
    public class DownloadTemplate : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string ImportID = context.Request.QueryString["tid"];
            string FileType = context.Request.QueryString["type"];
            string objectName= context.Request.QueryString["ObjectName"];
            if (FileType == "txt")
            {
                CreateTextTemplate(ImportID, objectName);
            }
            if (FileType == "csv")
            {
                CreateCsvtemplate(ImportID, objectName);
            }
            if (FileType == "excel")
            {
                CreateexcelsTemplte(ImportID, objectName);
            }
            if (FileType == "excels")
            {
                CreateExcelTemplte(ImportID, objectName);
            }
        }
        protected void CreateExcelTemplte(string ImportID,string objectName)
        {
            // String FileName = "ProductImport.xls";
            String FileName = objectName + ".xls";
            String FilePath = AppDomain.CurrentDomain.BaseDirectory + FileName;
            //  StreamWriter wr = new StreamWriter(FilePath);
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                //iProductMasterClient // = new iProductMasterClient();
                System.Data.DataSet ds = new System.Data.DataSet();
                System.Data.DataSet ds1 = new System.Data.DataSet();
              //  string objectName = "Product";
                long companyID = profile.Personal.CompanyID;
                //long customerID = profile.Personal.CustomerId;
                 string Sheet1 = "ImportSheet1" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
              //  string Sheet1 = "Sheet1.xls";
                //string sheet1path = Path.Combine(Server.MapPath("~/ImportDesign/ImportTemplate"), Sheet1 + ".xls");
                string sheet1path = Path.Combine(HttpContext.Current.Server.MapPath("~/ImportDesign/ImportTemplate"), Sheet1);
                StreamWriter wr = new StreamWriter(sheet1path);
                PowerOnRentwebapp.DocumentService.iUC_AttachDocumentClient DocumentClient = new PowerOnRentwebapp.DocumentService.iUC_AttachDocumentClient();
                ds = DocumentClient.GetImportTemplateData(ImportID, profile.DBConnection._constr);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string data1 = ds.Tables[0].Rows[i]["fieldName"].ToString().ToUpper() + "\t";
                    wr.Write(ds.Tables[0].Rows[i]["fieldName"].ToString().ToUpper() + "\t");
                }
                wr.WriteLine();
                wr.Close();
                string Sheet2 = "Sheet2" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
               // string Sheet2 = "Sheet2.xls";
                //  string sheet2path = Path.Combine(Server.MapPath("~/ImportDesign/ImportTemplate"), Sheet2 + ".xls");
                //string sheet2path = Path.Combine(HttpContext.Current.Server.MapPath("~/ImportDesign/ImportTemplate"), Sheet2 + ".xls");
                string sheet2path = Path.Combine(HttpContext.Current.Server.MapPath("~/ImportDesign/ImportTemplate"), Sheet2);
                StreamWriter wr1 = new StreamWriter(sheet2path);
                ds1 = DocumentClient.GetImportTemplateData(ImportID, profile.DBConnection._constr);
                for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                {
                    wr1.Write(ds.Tables[0].Columns[i].ToString().ToUpper() + "\t");
                }
                wr1.WriteLine();
                //write rows to excel file
                for (int i = 0; i < (ds1.Tables[0].Rows.Count); i++)
                {
                    for (int j = 0; j < ds1.Tables[0].Columns.Count; j++)
                    {
                        if (ds.Tables[0].Rows[i][j] != null)
                        {
                            wr1.Write(Convert.ToString(ds1.Tables[0].Rows[i][j]) + "\t");
                        }
                        else
                        {
                            wr1.Write("\t");
                        }
                    }
                    //go to next line
                    wr1.WriteLine();
                }
                //close file
                wr1.Close();
                Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                app.Visible = false;
                app.Workbooks.Add("");
                app.Workbooks.Add(sheet1path);
                app.Workbooks.Add(sheet2path);
                int k = 1;
                for (int i = 2; i <= app.Workbooks.Count; i++)
                {
                    for (int j = 1; j <= app.Workbooks[i].Worksheets.Count; j++)
                    {
                        Microsoft.Office.Interop.Excel.Worksheet ws = app.Workbooks[i].Worksheets[j];
                        ws.Copy(app.Workbooks[1].Worksheets[k]);
                    }
                    k++;
                }
                app.Workbooks[1].SaveCopyAs(FilePath);
                System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                response.ClearContent();
                response.Clear();
                response.ContentType = "application/vnd.ms-excel";
                response.AddHeader("Content-Disposition", "attachment; filename=" + FileName + "");
                response.TransmitFile(FilePath);
                response.Flush();
                response.Close();
                //CreateTextTemplate();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void CreateTextTemplate(string ImportID, string objectName)
        {
            String FileName = objectName + ".txt";
            String FilePath = AppDomain.CurrentDomain.BaseDirectory + FileName;
            StreamWriter wrt = new StreamWriter(FilePath);
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                System.Data.DataSet ds = new System.Data.DataSet();
               // string objectName = "Product";
                long companyID = profile.Personal.CompanyID;
                //long customerID = profile.Personal.CustomerId;
                PowerOnRentwebapp.DocumentService.iUC_AttachDocumentClient DocumentClient = new PowerOnRentwebapp.DocumentService.iUC_AttachDocumentClient();
                ds = DocumentClient.GetImportTemplateData(ImportID, profile.DBConnection._constr);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string TxtData = "";
                    if (i == 0)
                    {
                        TxtData = ds.Tables[0].Rows[i]["fieldName"].ToString().ToUpper();
                    }
                    else
                    {
                        TxtData = "\t" + ds.Tables[0].Rows[i]["fieldName"].ToString().ToUpper();
                    }
                    wrt.Write(TxtData);

                }
                wrt.WriteLine();
                wrt.Close();
                System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                response.ClearContent();
                response.Clear();
                //response.ContentType = "application/vnd.ms-word";
                response.ContentType = "text/plain";
                response.AddHeader("Content-Disposition", "attachment; filename=" + FileName + "");
                response.TransmitFile(FilePath);
                response.Flush();
                response.Close();
                // CreateCsvtemplate();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void CreateCsvtemplate(string ImportID, string objectName)
        {
            String FileName = objectName + ".csv";
            String FilePath = AppDomain.CurrentDomain.BaseDirectory + FileName;
            StreamWriter wr = new StreamWriter(FilePath);
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                System.Data.DataSet ds = new System.Data.DataSet();
              //  string objectName = "Product";
                long companyID = profile.Personal.CompanyID;
                // long customerID = profile.Personal.CustomerId;
                PowerOnRentwebapp.DocumentService.iUC_AttachDocumentClient DocumentClient = new PowerOnRentwebapp.DocumentService.iUC_AttachDocumentClient();

                ds = DocumentClient.GetImportTemplateData(ImportID, profile.DBConnection._constr);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string CsvData = "";
                    if (i == 0)
                    {
                        CsvData = ds.Tables[0].Rows[i]["fieldName"].ToString().ToUpper();
                    }
                    else
                    {
                        CsvData = "," + ds.Tables[0].Rows[i]["fieldName"].ToString().ToUpper();
                    }
                    wr.Write(CsvData);

                }
                wr.WriteLine();
                wr.Close();
                System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                response.ClearContent();
                response.Clear();
                response.ContentType = "application/vnd.ms-excel";
                response.AddHeader("Content-Disposition", "attachment; filename=" + FileName + "");
                response.TransmitFile(FilePath);
                response.Flush();
                response.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        protected void CreateexcelsTemplte(string ImportID, string objectName)
        {
            string data1 = "";
            String FileName = objectName + ".xls";
            //String FilePath = AppDomain.CurrentDomain.BaseDirectory + FileName;
            //StreamWriter wr = new StreamWriter(FilePath);
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                System.Data.DataSet ds = new System.Data.DataSet();
              //  string objectName = "Product";
                long companyID = profile.Personal.CompanyID;
                //long customerID = profile.Personal.CustomerId;
                 PowerOnRentwebapp.DocumentService.iUC_AttachDocumentClient DocumentClient = new PowerOnRentwebapp.DocumentService.iUC_AttachDocumentClient();
                ds = DocumentClient.GetImportTemplateData(ImportID, profile.DBConnection._constr);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    data1 += ds.Tables[0].Rows[i]["fieldName"].ToString().ToUpper() + "\t";

                   // wr.Write(ds.Tables[0].Rows[i]["fieldName"].ToString().ToUpper() + "\t");

                } 
               //
                System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                response.ClearContent();
                response.Clear();
                response.ContentType = "application/vnd.ms-excel";
                response.AddHeader("Content-Disposition", "attachment; filename=" + FileName + "");
                //response.TransmitFile(FilePath);
                response.Write(data1);
                response.Flush();
                response.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}