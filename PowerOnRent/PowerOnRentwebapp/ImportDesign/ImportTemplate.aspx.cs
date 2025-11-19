using PowerOnRentwebapp.Login;
using PowerOnRentwebapp.PORServiceUCCommonFilter;
using PowerOnRentwebapp.ProductMasterService;
using Microsoft.Office.Interop.Excel;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BrilliantWMS.ImportDesign
{
    public partial class ImportTemplate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            String FileName = "ProductImport.xls";
            String FilePath = AppDomain.CurrentDomain.BaseDirectory + FileName;
          //  StreamWriter wr = new StreamWriter(FilePath);
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                iProductMasterClient productClient = new iProductMasterClient();
                System.Data.DataTable dt = new System.Data.DataTable();
                System.Data.DataTable dt1 = new System.Data.DataTable();
                string objectName = "Product";
                long companyID = profile.Personal.CompanyID;
                //long customerID = profile.Personal.CustomerId;
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
                SqlCommand cmd = new SqlCommand("Exec sp_GetImportTemplateData " + objectName + ","+companyID+"", con);
               
               SqlDataAdapter da = new SqlDataAdapter(cmd);
                string Sheet1 = "Sheet1";// + DateTime.Now.ToString("yyyyMMddHHmmss");
                string sheet1path = Path.Combine(Server.MapPath("~/ImportDesign/ImportTemplate"), Sheet1 + ".xls");
                StreamWriter wr = new StreamWriter(sheet1path);
                con.Open();
                
                da.Fill(dt);
                
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string data1 = dt.Rows[i]["fieldName"].ToString().ToUpper() + "\t";
                    
                    wr.Write(dt.Rows[i]["fieldName"].ToString().ToUpper() + "\t");
                 //  wr.Write(ConsoleColor.Cyan);
                // wr.Write(FontSize.Large)
                   
                }
                //  Style.BottomBorder.SetColor("#ff6600");


               
                wr.WriteLine();

                //write rows to excel file
                //for (int i = 0; i < (dt.Rows.Count); i++)
                //{
                //    for (int j = 0; j < dt.Columns.Count; j++)
                //    {
                //        if (dt.Rows[i][j] != null)
                //        {
                //            wr.Write(Convert.ToString(dt.Rows[i][j]) + "\t");
                //        }
                //        else
                //        {
                //            wr.Write("\t");
                //        }
                //    }
                //    //go to next line
                //    wr.WriteLine();
                //}
                //close file
                wr.Close();

                //System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                //response.ClearContent();
                //response.Clear();
                //response.ContentType = "application/vnd.ms-excel";
                //response.AddHeader("Content-Disposition", "attachment; filename=" + FileName + "");
                //response.TransmitFile(FilePath);
                //response.Flush();
                //response.Close();

                //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
                SqlCommand cmd1 = new SqlCommand("Exec sp_GetImportTemplateData " + objectName + "," + companyID + "", con);

                SqlDataAdapter sda = new SqlDataAdapter(cmd1);
                string Sheet2 = "Sheet2";// + DateTime.Now.ToString("yyyyMMddHHmmss");
                string sheet2path = Path.Combine(Server.MapPath("~/ImportDesign/ImportTemplate"), Sheet2 + ".xls");
                StreamWriter wr1 = new StreamWriter(sheet2path);
             

                sda.Fill(dt1);
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    wr1.Write(dt.Columns[i].ToString().ToUpper() + "\t");
                }

                wr1.WriteLine();

                //write rows to excel file
                for (int i = 0; i < (dt1.Rows.Count); i++)
                {
                    for (int j = 0; j < dt1.Columns.Count; j++)
                    {
                        if (dt.Rows[i][j] != null)
                        {
                            wr1.Write(Convert.ToString(dt1.Rows[i][j]) + "\t");
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


                //for (int i = 0; i < dt1.Rows.Count; i++)
                //{
                //    string data1 = dt.Rows[i]["fieldName"].ToString().ToUpper() + "\t";

                //    wr1.Write(dt.Rows[i]["fieldName"].ToString().ToUpper() + "\t");
                //    wr.Write(ConsoleColor.Cyan);
                //    wr.Write(FontSize.Large)

                //}
                //Style.BottomBorder.SetColor("#ff6600");



                //wr1.WriteLine();

                //write rows to excel file
                //for (int i = 0; i < (dt.Rows.Count); i++)
                //{
                //    for (int j = 0; j < dt.Columns.Count; j++)
                //    {
                //        if (dt.Rows[i][j] != null)
                //        {
                //            wr.Write(Convert.ToString(dt.Rows[i][j]) + "\t");
                //        }
                //        else
                //        {
                //            wr.Write("\t");
                //        }
                //    }
                //    //go to next line
                //    wr.WriteLine();
                //}
                //close file
              

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
        }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnimportTxt_Click(object sender, EventArgs e)
        {
            String FileName = "ProductImport.txt";
            String FilePath = AppDomain.CurrentDomain.BaseDirectory + FileName;
            StreamWriter wr = new StreamWriter(FilePath);
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                iProductMasterClient productClient = new iProductMasterClient();
                System.Data.DataTable dt = new System.Data.DataTable();
                string objectName = "Product";
                long companyID = profile.Personal.CompanyID;
                //long customerID = profile.Personal.CustomerId;
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
                SqlCommand cmd = new SqlCommand("Exec sp_GetImportTemplateData " + objectName + "," + companyID + "", con);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                con.Open();

                da.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string data1 = dt.Rows[i]["fieldName"].ToString().ToUpper() + "\t";

                    wr.Write(dt.Rows[i]["fieldName"].ToString().ToUpper() + "\t");

                }
                //  Style.BottomBorder.SetColor("#ff6600");

                wr.WriteLine();

                //write rows to excel file
                //for (int i = 0; i < (dt.Rows.Count); i++)
                //{
                //    for (int j = 0; j < dt.Columns.Count; j++)
                //    {
                //        if (dt.Rows[i][j] != null)
                //        {
                //            wr.Write(Convert.ToString(dt.Rows[i][j]) + "\t");
                //        }
                //        else
                //        {
                //            wr.Write("\t");
                //        }
                //    }
                //    //go to next line
                //    wr.WriteLine();
                //}
                //close file
                wr.Close();

                System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                response.ClearContent();
                response.Clear();
                response.ContentType = "application/vnd.ms-word";
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

        protected void btnimportCsv_Click(object sender, EventArgs e)
        {
            String FileName = "ProductImport.csv";
            String FilePath = AppDomain.CurrentDomain.BaseDirectory + FileName;
            StreamWriter wr = new StreamWriter(FilePath);
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                iProductMasterClient productClient = new iProductMasterClient();
                System.Data.DataTable dt = new System.Data.DataTable();
                string objectName = "Product";
                long companyID = profile.Personal.CompanyID;
                //long customerID = profile.Personal.CustomerId;
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
                SqlCommand cmd = new SqlCommand("Exec sp_GetImportTemplateData " + objectName + "," + companyID + "", con);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                con.Open();

                da.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string data1 = dt.Rows[i]["fieldName"].ToString().ToUpper() + ",";

                    wr.Write(dt.Rows[i]["fieldName"].ToString().ToUpper() + ",");

                }
                //  Style.BottomBorder.SetColor("#ff6600");

                wr.WriteLine();

                //write rows to excel file
                //for (int i = 0; i < (dt.Rows.Count); i++)
                //{
                //    for (int j = 0; j < dt.Columns.Count; j++)
                //    {
                //        if (dt.Rows[i][j] != null)
                //        {
                //            wr.Write(Convert.ToString(dt.Rows[i][j]) + "\t");
                //        }
                //        else
                //        {
                //            wr.Write("\t");
                //        }
                //    }
                //    //go to next line
                //    wr.WriteLine();
                //}
                //close file
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

        protected void btnImportExcel_Click(object sender, EventArgs e)
        {
            string FileName = "D:\\Testing.xslx";
            string SQLQuery = "Exec sp_GetImportTemplateData  'Product',10322";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
            DataSet ds = new DataSet("myDataset");
            SqlDataAdapter da = new SqlDataAdapter(SQLQuery, con);
            da.Fill(ds);
            Application ExcelApp = new Application();
            Workbook ExcelWorkBook = null;
            Worksheet ExcelWorkSheet = null;
            ExcelApp.Visible = true;

            ExcelWorkBook = ExcelApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);

            List<string> SheetNames = new List<string>();

            SheetNames.Add("Employee Details");

            SheetNames.Add("IndividualCustomer Details");

            SheetNames.Add("Contact Details");
            try
            {
                for (int i = 1; i < ds.Tables.Count; i++)

                    ExcelWorkBook.Worksheets.Add(); //Adding New sheet in Excel Workbook

                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    int r = 1; // Initialize Excel Row Start Position  = 1

                    ExcelWorkSheet = ExcelWorkBook.Worksheets[i + 1];

                    //Writing Columns Name in Excel Sheet

                    for (int col = 1; col < ds.Tables[i].Columns.Count; col++)

                        ExcelWorkSheet.Cells[r, col] = ds.Tables[i].Columns[col - 1].ColumnName;

                    r++;

                    //Writing Rows into Excel Sheet

                    for (int row = 0; row < ds.Tables[i].Rows.Count; row++) //r stands for ExcelRow and col for ExcelColumn

                    {
                        // Excel row and column start positions for writing Row=1 and Col=1

                        for (int col = 1; col < ds.Tables[i].Columns.Count; col++)

                            ExcelWorkSheet.Cells[r, col] = ds.Tables[i].Rows[row][col - 1].ToString();

                        r++;

                    }

                    ExcelWorkSheet.Name = SheetNames[i];//Renaming the ExcelSheets
                }

                ExcelWorkBook.SaveAs(FileName);

                ExcelWorkBook.Close();

                ExcelApp.Quit();

                Marshal.ReleaseComObject(ExcelWorkSheet);

                Marshal.ReleaseComObject(ExcelWorkBook);

                Marshal.ReleaseComObject(ExcelApp);
            
            }
            catch (Exception exHandle)
            {
                Console.WriteLine("Exception: " + exHandle.Message);
                Console.ReadLine();
            }
            finally
            { }      
        }
        private void InvoiceWorldWide(string qry)
        {
            CustomProfile profile = CustomProfile.GetProfile();
            iUCCommonFilterClient objService = new iUCCommonFilterClient();
            DataSet ds1 = new DataSet();
            DataSet ds2 = new DataSet();
            System.Data.DataTable dt = new System.Data.DataTable();
            ReportViewer rv = new ReportViewer();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
            String FileName = "Invoice_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
            String FilePath = AppDomain.CurrentDomain.BaseDirectory + FileName;

            try
            {
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                long frmID = (long)dt.Compute("MIN([Invoice_Number])", "");
                long toID = (long)dt.Compute("MAX([Invoice_Number])", "");
              //  ds1 = objService.InvoiceHead_WW(frmID, toID, profile.Personal.UserID, profile.DBConnection._constr);
               // ds2 = objService.InvoiceDetail_WW(frmID, toID, profile.DBConnection._constr);
                ReportDataSource rds1;
                ReportDataSource rds2;
                rds1 = new ReportDataSource("DataSet1", ds1.Tables[0]);
                rds2 = new ReportDataSource("DataSet2", ds2.Tables[0]);

                rv.LocalReport.DataSources.Clear();
                rv.LocalReport.DataSources.Add(rds1);
                rv.LocalReport.DataSources.Add(rds2);
                string path = Path.Combine(HttpContext.Current.Server.MapPath("~/WMS/WMSReport"), "InvoiceWorldWide.rdlc");
                rv.LocalReport.ReportPath = path;
                rv.ShowParameterPrompts = false;
                rv.ShowPromptAreaButton = false;
                rv.LocalReport.Refresh();

                byte[] streamBytes = null;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string filenameExtension = string.Empty;
                string[] streamids;
                Warning[] warnings;

                streamBytes = rv.LocalReport.Render("Excel", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);

                 string sheet1 = "sheet1" + DateTime.Now.ToString("yyyyMMddHHmmss");
                string sheet1path = Path.Combine(Server.MapPath("~/WMS/LabelData"), sheet1 + ".xls");

                if (qry != string.Empty)
                {
                    string Instruction = "Instruction" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    string sheet2path = Path.Combine(Server.MapPath("~/WMS/LabelData"), Instruction + ".xls");
                    StreamWriter wr = new StreamWriter(sheet2path);
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        wr.Write(dt.Columns[i].ToString().ToUpper() + "\t");
                    }

                    wr.WriteLine();

                    //write rows to excel file
                    for (int i = 0; i < (dt.Rows.Count); i++)
                    {
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            if (dt.Rows[i][j] != null)
                            {
                                wr.Write(Convert.ToString(dt.Rows[i][j]) + "\t");
                            }
                            else
                            {
                                wr.Write("\t");
                            }
                        }
                        //go to next line
                        wr.WriteLine();
                    }
                    //close file
                    wr.Close();

                    //Merge Excel

                    // sheet3 = "I3" + DateTime.Now.ToString("yyyyMMddHHmmss");

                  //  string sheet1path = Path.Combine(Server.MapPath("~/WMS/LabelData"), sheet1 + ".xls");
                    // sheet3path = Path.Combine(Server.MapPath("~/WMS/LabelData"), sheet3 + ".xls");

                    //Workbook book1 = Workbook.Load(sheet1path);
                    //Worksheet sh1 = book1.Worksheets[0];

                    //Workbook book2 = Workbook.Load(sheet1path);
                    //Worksheet sh2 = book2.Worksheets[0];

                    //Workbook workbook = new Workbook();
                    //workbook.Worksheets.Add(sh1);
                    //workbook.Worksheets.Add(sh2);
                    //workbook.Save(FilePath);

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
                }

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
                //Login.Profile.ErrorHandling(ex, "Query Builder", "GetQueryResult");
            }
            finally
            {
                objService.Close();
            }
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
            String FileName = "ImportProduct_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
            String FilePath = AppDomain.CurrentDomain.BaseDirectory + FileName;
         CustomProfile profile = CustomProfile.GetProfile();
            iProductMasterClient productClient = new iProductMasterClient();
            System.Data.DataTable dt = new System.Data.DataTable();
            string objectName = "Product";
            long companyID = profile.Personal.CompanyID;
            //long customerID = profile.Personal.CustomerId;
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
            //SqlCommand cmd = new SqlCommand("Exec sp_GetImportTemplateData " + objectName + "," + companyID + "", con);

            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            //con.Open();

            //da.Fill(dt);

           // string sheet1 = "I1" + DateTime.Now.ToString("yyyyMMddHHmmss");
            string sheet1 = "sheet1";
           string sheet1path = Path.Combine(Server.MapPath("~/ImportDesign/ImportTemplate"), sheet1 + ".xls");

            //  if (qry != string.Empty)
            //  {
           //  string sheet2 = "I2" + DateTime.Now.ToString("yyyyMMddHHmmss");
            string Sheet2 = "Sheet2";
                string sheet2path = Path.Combine(Server.MapPath("~/ImportDesign/ImportTemplate"), Sheet2 + ".xls");
                StreamWriter wr = new StreamWriter(sheet1path);
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
            SqlCommand cmd = new SqlCommand("Exec sp_GetImportTemplateData " + objectName + "," + companyID + "", con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Open();

            da.Fill(dt);
            
            for (int i = 0; i < (dt.Rows.Count); i++)
                {
                //string data1 = dt.Rows[i]["fieldName"].ToString().ToUpper() + "\t";

                //wr.Write(dt.Rows[i]["fieldName"].ToString().ToUpper() + "\t");
                string CsvData = "";
                if (i == 0)
                {
                    CsvData =dt.Rows[i]["fieldName"].ToString().ToUpper();
                }
                else
                {
                    CsvData = "," + dt.Rows[i]["fieldName"].ToString().ToUpper();
                }
                // wr.Write(CsvData);
                CsvData = "\n";

            }
                //go to next line
                wr.WriteLine();

             //   }
                //close file
             //   wr.Close();

                //Merge Excel

                // sheet3 = "I3" + DateTime.Now.ToString("yyyyMMddHHmmss");

            //   string sheet1path = Path.Combine(Server.MapPath("~/WMS/LabelData"), sheet1 + ".xls");
            // sheet3path = Path.Combine(Server.MapPath("~/WMS/LabelData"), sheet3 + ".xls");

            //Workbook book1 = Workbook.Load(sheet1path);
            // Worksheet sh1 = book1.Worksheets[0];

            //Workbook book2 = Workbook.Load(sheet2path);
            //Worksheet sh2 = book2.Worksheets[0];

            // Workbook workbook = new Workbook();
            // workbook.Worksheets.Add(sh1);
            // workbook.Worksheets.Add(sh2);
            // workbook.Save(FilePath);

            // Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();

            // app.Visible = false;
            // app.Workbooks.Add("");
            //// app.Workbooks.Add(sheet1path);
            // app.Workbooks.Add(sheet2path);
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();

            app.Visible = false;
            app.Workbooks.Add("");
            app.Workbooks.Add(sheet1path);
            app.Workbooks.Add(sheet2path);
            int k = 1;
    
                for (int i1 = 2; i1 <= app.Workbooks.Count; i1++)
                {
                    for (int j = 1; j <= app.Workbooks[i1].Worksheets.Count; j++)
                    {
                        Microsoft.Office.Interop.Excel.Worksheet ws = app.Workbooks[i1].Worksheets[j];
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


        }
        private void InvoiceWorldWide1(string qry)
        {
            CustomProfile profile = CustomProfile.GetProfile();
            iUCCommonFilterClient objService = new iUCCommonFilterClient();
            DataSet ds1 = new DataSet();
            DataSet ds2 = new DataSet();
            System.Data.DataTable dt = new System.Data.DataTable();
            ReportViewer rv = new ReportViewer();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
            String FileName = "Invoice_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
            String FilePath = AppDomain.CurrentDomain.BaseDirectory + FileName;

            try
            {
                SqlCommand cmd = new SqlCommand(qry, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                long frmID = (long)dt.Compute("MIN([Invoice_Number])", "");
                long toID = (long)dt.Compute("MAX([Invoice_Number])", "");
                //ds1 = objService.InvoiceHead_WW(frmID, toID, profile.Personal.UserID, profile.DBConnection._constr);
                //ds2 = objService.InvoiceDetail_WW(frmID, toID, profile.DBConnection._constr);
                ReportDataSource rds1;
                ReportDataSource rds2;
                rds1 = new ReportDataSource("DataSet1", ds1.Tables[0]);
                rds2 = new ReportDataSource("DataSet2", ds2.Tables[0]);

                rv.LocalReport.DataSources.Clear();
                rv.LocalReport.DataSources.Add(rds1);
                rv.LocalReport.DataSources.Add(rds2);
                string path = Path.Combine(HttpContext.Current.Server.MapPath("~/WMS/WMSReport"), "InvoiceWorldWide.rdlc");
                rv.LocalReport.ReportPath = path;
                rv.ShowParameterPrompts = false;
                rv.ShowPromptAreaButton = false;
                rv.LocalReport.Refresh();

                byte[] streamBytes = null;
                string mimeType = string.Empty;
                string encoding = string.Empty;
                string filenameExtension = string.Empty;
                string[] streamids;
                Warning[] warnings;

                streamBytes = rv.LocalReport.Render("Excel", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);

                string sheet1 = "I1" + DateTime.Now.ToString("yyyyMMddHHmmss");
                System.IO.File.WriteAllBytes(Server.MapPath("~/WMS/LabelData/") + sheet1 + ".xls", streamBytes);

                if (qry != string.Empty)
                {
                    string sheet2 = "I2" + DateTime.Now.ToString("yyyyMMddHHmmss");
                    string sheet2path = Path.Combine(Server.MapPath("~/WMS/LabelData"), sheet2 + ".xls");
                    StreamWriter wr = new StreamWriter(sheet2path);
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        wr.Write(dt.Columns[i].ToString().ToUpper() + "\t");
                    }

                    wr.WriteLine();

                    //write rows to excel file
                    for (int i = 0; i < (dt.Rows.Count); i++)
                    {
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            if (dt.Rows[i][j] != null)
                            {
                                wr.Write(Convert.ToString(dt.Rows[i][j]) + "\t");
                            }
                            else
                            {
                                wr.Write("\t");
                            }
                        }
                        //go to next line
                        wr.WriteLine();
                    }
                    //close file
                    wr.Close();

                    //Merge Excel

                    // sheet3 = "I3" + DateTime.Now.ToString("yyyyMMddHHmmss");

                    string sheet1path = Path.Combine(Server.MapPath("~/WMS/LabelData"), sheet1 + ".xls");
                    // sheet3path = Path.Combine(Server.MapPath("~/WMS/LabelData"), sheet3 + ".xls");

                    //Workbook book1 = Workbook.Load(sheet1path);
                    //Worksheet sh1 = book1.Worksheets[0];

                    //Workbook book2 = Workbook.Load(sheet1path);
                    //Worksheet sh2 = book2.Worksheets[0];

                    //Workbook workbook = new Workbook();
                    //workbook.Worksheets.Add(sh1);
                    //workbook.Worksheets.Add(sh2);
                    //workbook.Save(FilePath);

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
                }

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
                //Login.Profile.ErrorHandling(ex, "Query Builder", "GetQueryResult");
            }
            finally
            {
                objService.Close();
            }
        }

        public void convertExcelToCSV(string sourceFile, string worksheetName, string targetFile)
        {
            //DataTable dt = new DataTable();
            DataSet ImportData = new DataSet();
            string connString = "";

            string path = FileuploadPO.PostedFile.FileName;
            //string path = Fileup//loadPO.PostedFile.FileName;
            string strFileType = System.IO.Path.GetExtension(path).ToString().ToLower();
            string Fullpath = Server.MapPath("../CommonControls/ImportFiles/" + path);
            //FileuploadPO.PostedFile.SaveAs(Server.MapPath("../CommonControls/ImportFiles/" + path));
            FileuploadPO.PostedFile.SaveAs(Server.MapPath("../CommonControls/ImportFiles/" + path));
            if (strFileType.Trim() == ".xls")
            {
                connString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Fullpath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"";
            }
            else if (strFileType.Trim() == ".xlsx")
            {
                connString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Fullpath + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'";
            }
            OleDbConnection excelConnection = new OleDbConnection(connString);
            excelConnection.Open();
            OleDbCommand cmd1;




            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + sourceFile + ";Extended Properties=\" Excel.0;HDR=Yes;IMEX=1\"";
            OleDbConnection conn = null;
            StreamWriter wrtr = null;
            OleDbCommand cmd = null;
            OleDbDataAdapter da = null;
            try
            {
                conn = new OleDbConnection(connString);
                conn.Open();
                cmd = new OleDbCommand("SELECT * FROM [" + worksheetName + "$]", conn);
                cmd.CommandType = CommandType.Text;
                wrtr = new StreamWriter(targetFile);
                da = new OleDbDataAdapter(cmd);
                System.Data.DataTable dt = new System.Data.DataTable();
                da.Fill(dt);
                string data1 = "";
                for (int i = 0; i < dt.Columns.Count; i++)
                {if(i==0)
                    {
                        data1 += (dt.Columns[i].ToString().ToUpper());
                    }
                else{
                    data1 += ","+(dt.Columns[i].ToString().ToUpper());
                }
                }
                data1 += "\n";
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    string rowString = "";
                    for (int y = 0; y < dt.Columns.Count; y++)
                    {
                       // rowString += "\"" + dt.Rows[x][y].ToString() + "\",";
                       // data1 += " " + dt.Rows[x][y].ToString() + ",";
                        if (y== 0)
                        {
                            data1 += dt.Rows[x][y].ToString().ToUpper();
                        }
                        else
                        {
                            data1  += "," + dt.Rows[x][y].ToString().ToUpper();
                        }
                    }
                    data1 += "\n";
                    // wrtr.WriteLine(rowString);
                    txtCSVdata.Text = data1;
                }
                Console.WriteLine();
                Console.WriteLine("Done! Your " + sourceFile + " has been converted into " + targetFile + ".");
                Console.WriteLine();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.ToString());
                Console.ReadLine();
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                conn.Dispose();
                cmd.Dispose();
                da.Dispose();
                wrtr.Close();
                wrtr.Dispose();

            }

        }

        protected void btnExcelToCsv_Click(object sender, EventArgs e)
        {
            string sourceFile, worksheetName, targetFile;

            sourceFile = "sheet1.xls"; worksheetName = "sheet1"; targetFile = "target.csv";

            convertExcelToCSV(sourceFile, worksheetName, targetFile);
        }
    }
}