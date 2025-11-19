using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PowerOnRentwebapp.Login;
using System.Web.Services;
using System.Data;
using Microsoft.Reporting.WebForms;
using PowerOnRentwebapp.PORServiceUCCommonFilter;
using PowerOnRentwebapp.CommonControls;
using System.Resources;
using System.Globalization;
using System.Threading;
using System.Reflection;
using PowerOnRentwebapp.PORServicePartRequest;
using iTextSharp.text.pdf;
using System.IO;
using Newtonsoft.Json;
using System.Net;
using RestSharp;
using System.Data.SqlClient;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text;

namespace PowerOnRentwebapp.PowerOnRent
{
    public partial class TransactionReportPopup : System.Web.UI.Page
    {
        string jsondataurl = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            UserCreationService.iUserCreationClient UserCreationClient = new UserCreationService.iUserCreationClient();
            CustomProfile profile = CustomProfile.GetProfile();
            if (Session.Timeout > 0)
            {
                long CmpnyID = profile.Personal.CompanyID;
                string CompanyLogoPath = "";
                CompanyLogoPath = UserCreationClient.GetComppanyLogo(CmpnyID, profile.DBConnection._constr);
                ClientLogo.ImageUrl = CompanyLogoPath;
            }
                if (!this.IsPostBack)
            {
                string Selectedcategory = Request.QueryString["Selectedcategory"];
                string Selectedcateria = Request.QueryString["Selectedcateria"];
                fillTransactionReport();
            }
        }
        

            [WebMethod]
            public static string GetTransactionReportList(string jsonData)
            {
            string abc = "";
            abc = jsonData;
            return "";
            }

        
        private static DataSet ConvertJsonToDataTable(string jsonData)
            {
                try
                {
                    return JsonConvert.DeserializeObject<DataSet>(jsonData);
                }
                catch
                {
                    return null;
                }
            }
        public void fillTransactionReport()
        {
            iUCCommonFilterClient UCCommonFilter = new iUCCommonFilterClient();
            CustomProfile profile = CustomProfile.GetProfile();
            GVTransactionReportInfo.DataSource = null;
            GVTransactionReportInfo.DataBind();
            try
            {
               
                string Selectedcategory = Request.QueryString["Selectedcategory"];
                string Selectedcateria = Request.QueryString["Selectedcateria"];
                
                if (Selectedcategory != "" && Selectedcateria != "")
                {
                    string URL = "";
                    string FINALURL = "";
                    string Parameter = "";
                    try { 
                    if (Selectedcategory == "Devices")
                    {
                        URL = "https://gwc-apim.azure-api.net/testoms_vf/DWHReport/category_imei?IMEI=";
                        FINALURL = URL + Selectedcateria;
                        Parameter = "IMEI=" + Selectedcateria + "";
                    }
                    else if (Selectedcategory == "SIM-MSISDN")
                    {
                        URL = "https://gwc-apim.azure-api.net/testoms_vf/DWHReport/category_msisdn?msisdn=";
                        FINALURL = URL + Selectedcateria;
                        Parameter = "msisdn=" + Selectedcateria + "";
                    }
                    else if (Selectedcategory == "SIM-ICCID")
                    {
                        URL = "https://gwc-apim.azure-api.net/testoms_vf/DWHReport/category_iccid?iccid=";
                        FINALURL = URL + Selectedcateria;
                        Parameter = "iccid=" + Selectedcateria + "";
                    }
                    else if (Selectedcategory == "Recharge cards/All other Devices")
                    {
                        URL = "https://gwc-apim.azure-api.net/testoms_vf/DWHReport/category_serinalno?serialno=";
                        FINALURL = URL + Selectedcateria;
                        Parameter = "Serialno=" + Selectedcateria + "";
                    }

                    string APIKey = "eecbc02cdea04b1f8951a1ce018879a4";
                    var clientlbl = new RestClient(FINALURL);
                        var requestlbl = new RestRequest(Method.GET);
                        requestlbl.AddHeader("Ocp-Apim-Subscription-Key", APIKey);
                        
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                        IRestResponse reslabel = clientlbl.Execute(requestlbl);
                        string Result = reslabel.Content;
                        string ErrorMessage = reslabel.ErrorMessage;
                        string StatusCode = Convert.ToString(reslabel.StatusCode);
                        string StatusDescription = reslabel.StatusDescription;
                        DataTable dt = JsonConvert.DeserializeObject<DataTable>(Result);
                    
                    this.GVTransactionReportInfo.DataSource = dt;
                    this.GVTransactionReportInfo.DataBind();
                        //int getRowCount = GVTransactionReportInfo.Rows.Count;
                        //int getCellCount = GVTransactionReportInfo.Rows[0].Cells.Count;
                        //for (int i = 0; i < getRowCount; i++)
                        //{
                        //    for (int k = 0; k < getCellCount; k++)
                        //    {
                        //        GVTransactionReportInfo.Rows[i].Cells[k].Attributes.Add("style", "padding:10px");
                        //    }
                        //}
                    }
                    catch (Exception ex)
                    {
                        ErrorTracking(Convert.ToString(ex));
                    }
                }

                
            }

            catch (Exception ex)
            {
                ErrorTracking(Convert.ToString(ex));
            }
        }

        public void ErrorTracking(string ex)
        {
            SqlConnection con = new SqlConnection("Data Source=10.228.134.66; Initial Catalog=GWCDEEcomm; User ID=GWCTEST; Password='Password123#';");
            

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Sp_EnterErrorTracking";
            cmd.Connection = con;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("Data", "fillTransactionReport");
            cmd.Parameters.AddWithValue("GetType", "System.Web");
            cmd.Parameters.AddWithValue("InnerException", "Error");
            cmd.Parameters.AddWithValue("Message", ex);
            cmd.Parameters.AddWithValue("Source", "fillTransactionReport");
            cmd.Parameters.AddWithValue("DateTime", DateTime.Now);
            cmd.Parameters.AddWithValue("UserID", "123");
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();

        }

        protected void ExporttoExcel_Click(object sender, EventArgs e)
        {
            ExporttoExcel();
        }

        private void ExporttoExcel()
        {
            
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=TransportRecordData.xls");
            Response.ContentType = "File/Data.xls";
            StringWriter StringWriter = new System.IO.StringWriter();
            HtmlTextWriter HtmlTextWriter = new HtmlTextWriter(StringWriter);
            GVTransactionReportInfo.RenderControl(HtmlTextWriter);
            Response.Write(StringWriter.ToString());
            Response.End();

            Response.Clear();

            

        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }
        protected void ExporttoPDF_Click(object sender, EventArgs e)
        {
            ExporttoPDF();
        }
        private void ExporttoPDF()
        {
            //GVTransactionReportInfo.AllowPaging = false;
            //GVTransactionReportInfo.DataBind();
            //Response.ClearContent();
            //Response.AddHeader("content-disposition", "attachment;filename=TransportRecordData.pdf");
            //Response.Charset = "";
            //Response.ContentType = "application/pdf";
            //StringWriter sw = new StringWriter();
            //HtmlTextWriter htw = new HtmlTextWriter(sw);
            //GVTransactionReportInfo.RenderControl(htw);
            //Response.Write(sw.ToString());
            //Response.End();

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=transaction_report.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            //sw.Write("This is text");
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            //hw.Write("<h1>PDF Document</h1>");
            GVTransactionReportInfo.RenderControl(hw);
            TextReader sr = new StringReader(sw.ToString());
            iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(PageSize.A4, 10f, 10f, 10f, 0f);
            //pdfDoc.AddTitle("Transaction Report List");
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();
            GVTransactionReportInfo.AllowPaging = true;
            GVTransactionReportInfo.DataBind();
        }

        // private void GeneratePDF(DataTable dataTable, string pdfFileName)
        //{
        //    try
        //    {
        //        string[] columnNames = (from dc in dataTable.Columns.Cast<DataColumn>()
        //                                select dc.ColumnName).ToArray();
        //        int Cell = 0;
        //        int count = columnNames.Length;
        //        object[] array = new object[count];

        //        dataTable.Rows.Add(array);

        //        iTextSharp.text.Document pdfDoc = new iTextSharp.text.Document(PageSize.A2, 10f, 10f, 10f, 0f);
        //        System.IO.MemoryStream mStream = new System.IO.MemoryStream();
        //        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, mStream);
        //        //  writer.DirectContent
        //        int cols = dataTable.Columns.Count;
        //        int rows = dataTable.Rows.Count;

        //        iTextSharp.text.HeaderFooter header = new HeaderFooter(new Phrase(pdfFileName), false);
        //        //  iTextSharp.text.Image img = new Image("bispl - wms - logo.png");
        //        //  string imageURL = Server.MapPath(".") + "/bispl - wms - logo.png";
        //        //string imageURL = @"D:\WMSGUIOms\WestCostGUITest\BrilliantWMS\PowerOnRentwebapp\ReportDesigner\bispl-wms-logo.png";


        //        //Test Link
        //       // string imageURL = @"D:\IMFPAProj\BrilliantWMS\PowerOnRentwebapp\ReportDesigner\bispl-wms-logo.png";
        //        //Production link
        //       // string imageURL = @"C:\IMFPAWebAppProd\ReportDesigner\bispl-wms-logo.png";
        //        string path = HttpContext.Current.Server.MapPath("~/ReportDesigner/bispl-wms-logo.png");
        //        iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(path);
        //        //Resize image depend upon your need

        //        jpg.ScaleToFit(140f, 120f);
        //        //Give space before image

        //        jpg.SpacingBefore = 10f;
        //        //Give some space after the image

        //        jpg.SpacingAfter = 1f;
        //        jpg.Alignment = Element.ALIGN_LEFT;

        //        // Remove the border that is set by default  
        //        header.Border = iTextSharp.text.Rectangle.TITLE;
        //        // Align the text: 0 is left, 1 center and 2 right.  
        //        header.Alignment = Element.ALIGN_CENTER;
        //        //pdfDoc.Header = header;
        //        // Header. 
        //        pdfDoc.Open();

        //        iTextSharp.text.html.HtmlTags = 

        //        HtmlString myHtml = new HtmlString("<h1>My Heading 1</h1>");
        //        pdfDoc.Add(myHtml);

        //        iTextSharp.text.Table pdfTable = new iTextSharp.text.Table(cols, rows);
        //        pdfTable.BorderWidth = 1; pdfTable.Width = 100;
        //        pdfTable.Padding = 1; pdfTable.Spacing = 4;

        //        //creating table headers  
        //        for (int i = 0; i < cols; i++)
        //        {
        //            Cell cellCols = new Cell();
        //            Chunk chunkCols = new Chunk();
        //            cellCols.BackgroundColor = new iTextSharp.text.Color(System.Drawing.ColorTranslator.FromHtml("#548B54"));
        //            iTextSharp.text.Font ColFont = FontFactory.GetFont(FontFactory.HELVETICA, 14, iTextSharp.text.Font.BOLD, iTextSharp.text.Color.WHITE);

        //            chunkCols = new Chunk(dataTable.Columns[i].ColumnName, ColFont);

        //            cellCols.Add(chunkCols);
        //            pdfTable.AddCell(cellCols);
        //        }
        //        //creating table data (actual result)   

        //        for (int k = 0; k < rows; k++)
        //        {
        //            for (int j = 0; j < cols; j++)
        //            {
        //                Cell cellRows = new Cell();
        //                if (k % 2 == 0)
        //                {
        //                    cellRows.BackgroundColor = new iTextSharp.text.Color(System.Drawing.ColorTranslator.FromHtml("#cccccc")); ;
        //                }
        //                else { cellRows.BackgroundColor = new iTextSharp.text.Color(System.Drawing.ColorTranslator.FromHtml("#ffffff")); }
        //                iTextSharp.text.Font RowFont = FontFactory.GetFont(FontFactory.HELVETICA, 12);
        //                Chunk chunkRows = new Chunk(dataTable.Rows[k][j].ToString(), RowFont);
        //                cellRows.Add(chunkRows);

        //                pdfTable.AddCell(cellRows);
        //            }
        //        }
        //        PdfContentByte cb = writer.DirectContent;
        //        BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
        //        cb.SaveState();
        //        cb.BeginText();
        //        cb.MoveText(700, 30);
        //        cb.SetFontAndSize(bf, 12);
        //        cb.ShowText("My status");
        //        cb.EndText();
        //        cb.RestoreState();

        //        // HEADER TEMPLATE....
        //        iTextSharp.text.Element myHtmlElement = new Element();
        //        iTextSharp.text.Table myHeaderTable = new Table(3);
        //        // First Cell
        //        iTextSharp.text.Cell myFirstHeaderCell = new Cell();
        //        myFirstHeaderCell.Add(jpg);
        //        //Second Cell
        //        iTextSharp.text.Cell mySecondHeaderCell = new Cell();
        //        iTextSharp.text.Font CompantNameFont = FontFactory.GetFont(FontFactory.HELVETICA, 16);
        //        Chunk headerRows = new Chunk("BRILLIANT INFOSYSTEM PVT. LTD.", CompantNameFont);
        //        mySecondHeaderCell.Add(headerRows);
        //        myHeaderTable.AddCell(mySecondHeaderCell);
        //        //Third Cell
        //        iTextSharp.text.Cell myThirdHeaderCell = new Cell();
        //        iTextSharp.text.Font ContactFont = FontFactory.GetFont(FontFactory.HELVETICA, 12);
        //        Chunk headerContactCell = new Chunk("Contact: 9923099230", ContactFont);
        //        myThirdHeaderCell.Add(headerContactCell);
        //        myHeaderTable.AddCell(myThirdHeaderCell);
        //        // Add Table to PDF                
        //        pdfDoc.Add(myHeaderTable);
        //        // HEADER TEMPLATE....


        //        pdfDoc.Add(jpg);
        //        // pdfDoc.Add(new Phrase(this.textbox.Text.Trim()));


        //        pdfDoc.Add(pdfTable);

        //        pdfDoc.Close();
        //        HttpContext.Current.Response.ContentType = "application/octet-stream";
        //        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + pdfFileName);
        //        HttpContext.Current.Response.Clear();
        //        HttpContext.Current.Response.BinaryWrite(mStream.ToArray());
        //        HttpContext.Current.Response.Flush();
        //        HttpContext.Current.Response.End();

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        //change by suraj khopade
    }
}