using PowerOnRentwebapp.Login;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BrilliantWMS.ImportDesign
{
    /// <summary>
    /// Summary description for GetobjectColumnImport
    /// </summary>
    public class GetobjectColumnImport : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            string objname = context.Request.Form["objname"];
            // string objname = context.Request.QueryString["objname"];
            CustomProfile profile = CustomProfile.GetProfile();
            string htmlOutput = "";
            SqlConnection conn = new SqlConnection("");
            string str = "";
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["elegantcrm7ConnectionString"].ConnectionString;
            DataSet ds1 = new DataSet();
            DataSet ds = new DataSet();
            try
            {
                ds.Reset();
               
                str = "exec SP_getObjectColumnImport "+ objname+"";
                SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(str, conn);
                ds.Reset();
                da.Fill(ds);
                String Objectname= ds.Tables[0].Rows[0]["ImportQuery"].ToString();
                //CustomProfile profile = CustomProfile.GetProfile();
                ds1.Reset();
                string str1 = "";
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["elegantcrm7ConnectionString"].ConnectionString;
                //  str = "select top(1)WarehoueID from mUserWarehouse where UserID=" + userID + "";
                str1 = "SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" + Objectname + "'";
                SqlDataAdapter daa = new System.Data.SqlClient.SqlDataAdapter(str1, conn);
              
                daa.Fill(ds1);
                string warehouseID = "0";
                int getRowCount = ds1.Tables[0].Rows.Count;

                if (getRowCount > 0)
                {
                    for (int rc = 0; rc <= (getRowCount - 1); rc++)
                    {
                        string getColumnName = ds1.Tables[0].Rows[rc]["COLUMN_NAME"].ToString();
                        string getDataType = ds1.Tables[0].Rows[rc]["DATA_TYPE"].ToString();
                        if (rc == 0)
                        {
                            htmlOutput += getColumnName + ":" + getDataType;
                        }
                        else
                        {
                            htmlOutput += "|" + getColumnName + ":" + getDataType;
                        }

                    }
                }

                // htmlOutput = "success";
            }
            catch (System.Exception ex)
            {
                //Login.Profile.ErrorHandling(ex, "Query Builder", "saveissuedata");
            }
            finally
            {

            }
            context.Response.Write(htmlOutput);
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