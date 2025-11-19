using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Data.OleDb;
using BrilliantWMS.Login;
using BrilliantWMS.ImportService;
using BrilliantWMS.WarehouseService;
using System.IO;
using System.Configuration;

namespace BrilliantWMS.CommonControls
{
    public partial class popup : System.Web.UI.Page
    {
        string ObjectName = "";
        long CustomerID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["Objectname"] != null) ObjectName = Request.QueryString["Objectname"].ToString();
            if (Request.QueryString["CustomerID"] != null) CustomerID = long.Parse(Request.QueryString["CustomerID"].ToString());
            OpenPlace();
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "closePopup()", true);
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "window", "javascript:window.close();", true);
           
        }

        public void OpenPlace()
        {
            String FileName = "";
            if (ObjectName== "Campaign")
            {
                 FileName = "Campaign.xls";
            }
            else
            {
                 FileName = "Errormsg.xls";
            }
          
            String FilePath = AppDomain.CurrentDomain.BaseDirectory + FileName;
            StreamWriter wr = new StreamWriter(FilePath);
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
            CustomProfile profile = CustomProfile.GetProfile();
            ImportServiceClient Import = new ImportServiceClient();
            DataTable dt = new DataTable();
            try
            {         
                SqlCommand cmd = new SqlCommand("Import_SP_getTempTableDataExport", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ObjectName", ObjectName);
                cmd.Parameters.AddWithValue("@CustomerId", CustomerID);
                cmd.Parameters.AddWithValue("@UserID", profile.Personal.UserID);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                con.Close();
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
                System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                response.ClearContent();
                response.Clear();
                response.ContentType = "application/vnd.ms-excel";
                response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName + "");
                response.TransmitFile(FilePath);
                response.Flush();
                response.Close();
            }
            catch (Exception ex)
            { }
            finally
            {
                Import.Close();
                wr.Dispose();
            }
        }
    }
}