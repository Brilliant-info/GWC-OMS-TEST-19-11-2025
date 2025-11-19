using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;

namespace BrilliantWMS.WMSImport
{
    /// <summary>
    /// Summary description for convertExcelToCSV
    /// </summary>
    public class convertExcelToCSV : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string sourceFile;
            string worksheetName = "Sheet1";
            string targetFile;
            string csvData = "";
             
            //DataTable dt = new DataTable();
            DataSet ImportData = new DataSet();
            string connString = "";
            HttpFileCollection files = context.Request.Files;
            HttpPostedFile file = files[0];
            string path = DateTime.Now.ToString("yyyyMMddHHmmss") +'_'+file.FileName;
            //string path = Fileup//loadPO.PostedFile.FileName;
            string strFileType = System.IO.Path.GetExtension(path).ToString().ToLower();
            string Fullpath = HttpContext.Current.Server.MapPath("~/Product/TempImg/" + path);
           // FileuploadPO.PostedFile.SaveAs(Server.MapPath("../CommonControls/ImportFiles/" + path));
            file.SaveAs(Fullpath);
            if (strFileType.Trim() == ".xls")
            {
               // connString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Fullpath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=1\"";
                connString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Fullpath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
            }
            else if (strFileType.Trim() == ".xlsx")
            {
                connString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Fullpath + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1'";
            }
            OleDbConnection excelConnection = new OleDbConnection(connString);
            excelConnection.Open();
            OleDbCommand cmd1;

            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Fullpath + ";Extended Properties=\" Excel.0;HDR=Yes;IMEX=1\"";
            OleDbConnection conn = null;
            //StreamWriter wrtr = null;
            OleDbCommand cmd = null;
            OleDbDataAdapter da = null;
            try
            {
                conn = new OleDbConnection(connString);
                conn.Open();
                DataTable tables = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string name = "";
                if (tables.Rows.Count > 0)
                {
                    DataRow table = tables.Rows[0];
                    name = table["TABLE_NAME"].ToString();
                }
               

                cmd = new OleDbCommand("SELECT * FROM [" + name +"]", conn);
                //cmd = new OleDbCommand("SELECT * FROM [$" + worksheetName + "]", conn);
              //  cmd = new OleDbCommand("Select * from [Sheet1(2)$]", conn);
                cmd.CommandType = CommandType.Text;
              //  wrtr = new StreamWriter(targetFile);
                da = new OleDbDataAdapter(cmd);
                System.Data.DataTable dt = new System.Data.DataTable();
                da.Fill(dt);
                //String[] excelSheets = new String[dt.Rows.Count];
                //int j = 0;

                //// Add the sheet name to the string array.
                //foreach (DataRow row in dt.Rows)
                //{
                //    excelSheets[j] = row["TABLE_NAME"].ToString();
                //    j++;
                //}
                // string data1 = "";
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (i == 0)
                    {
                        csvData += (dt.Columns[i].ToString().ToUpper());
                    }
                    else
                    {
                        csvData += "," + (dt.Columns[i].ToString().ToUpper());
                    }
                }
                csvData += "\n";
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    string rowString = "";
                    for (int y = 0; y < dt.Columns.Count; y++)
                    {
                        // rowString += "\"" + dt.Rows[x][y].ToString() + "\",";
                        // data1 += " " + dt.Rows[x][y].ToString() + ",";
                        if (y == 0)
                        {
                            csvData += dt.Rows[x][y].ToString().ToUpper();
                        }
                        else
                        {
                            csvData += "," + dt.Rows[x][y].ToString().ToUpper();
                        }
                    }
                    csvData += "\n";
                    // wrtr.WriteLine(rowString);
                   // txtCSVdata.Text = data1;
                }
               // Console.WriteLine();
               // Console.WriteLine("Done! Your " + sourceFile + " has been converted into " + targetFile + ".");
               // Console.WriteLine();
            }
            //catch (Exception ex)
            //{
            //   // Console.WriteLine(ex.ToString());
            //   // Console.ReadLine();
            //   // Login.Profile.ErrorHandling(ex, "ConvertExcelToCsv", "ProcessRequest");
            //}
            finally
            {
                if (conn.State == ConnectionState.Open)
                conn.Close();
                conn.Dispose();
                cmd.Dispose();
                da.Dispose();
               // wrtr.Close();
               // wrtr.Dispose();

            }
            context.Response.ContentType = "text/plain";
            context.Response.Write(csvData);
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