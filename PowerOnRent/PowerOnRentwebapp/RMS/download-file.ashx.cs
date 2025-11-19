using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.IO;
using System.Drawing;
using System.Net;

namespace PowerOnRentwebapp.RMS
{
    /// <summary>
    /// Summary description for download_file
    /// </summary>
    public class download_file : IHttpHandler
    {
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter da = new SqlDataAdapter();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        SqlDataReader dr;
        long ResourceId = 1;
        string strcon = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;

        public void ProcessRequest(HttpContext context)
        {
            byte[] buffer = null;
            string querySqlStr = "";
            string Obj = "";
            SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString());
            try
            {
                if (context.Request.QueryString["DocId"] != null)
                {
                    querySqlStr = "select * from tDocument where Id=" + context.Request.QueryString["DocId"] + " AND DocumentSavePath is not null";
                }
                if (context.Request.QueryString["obj"] != null)
                {
                    Obj = context.Request.QueryString["obj"];

                }
                    SqlCommand command = new SqlCommand(querySqlStr, connection);
                SqlDataReader reader = null;

                connection.Open();
                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    //get the extension name of image
                    while (reader.Read())
                    {
                        try
                        {
                            string name = reader["DocumentDownloadPath"].ToString();
                            string[] getFileName = name.Split('/');
                            int endIndex = name.LastIndexOf('.');
                            string extensionName = name.Remove(0, endIndex + 1);
                            // buffer = (byte[])reader["DocumentSavePath"];                       
                            context.Response.Clear();
                            context.Response.ContentType = "image/" + extensionName;
                            // context.Response.ContentType = "application/octet-stream";
                            context.Response.AddHeader("Content-Disposition", "attachment; filename=" + getFileName[getFileName.Length - 1]);
                            // context.Response.BinaryWrite(buffer);
                            if (Obj == "RMSDocument")
                            {
                                context.Response.WriteFile(System.Web.HttpContext.Current.Server.MapPath("../Document/Attach_Document/" + name));
                            }
                            else
                            {
                                //context.Response.WriteFile(System.Web.HttpContext.Current.Server.MapPath("https://testoms.gwclogistics.com/GWCMobileTestV3/Deliveries/Attachment/RMS/" + name));
                                // context.Response.WriteFile(name);
                                //context.Response.WriteFile("C:\\GWCMobileTestV3\\Deliveries\\Attachment\\RMS\\IMG_20231124_131753.jpg");
                                //string localFileName = "C:\\GWCMobileTestV3\\Deliveries\\Attachment\\RMS\\" + getFileName ;
                                //WebClient webClient = new WebClient();
                                //webClient.DownloadFile(name, localFileName);
                                context.Response.WriteFile(System.Web.HttpContext.Current.Server.MapPath("../Document/Mobile_RMS_Document/" + name));
                            }

                            context.Response.Flush();
                            context.Response.Close();
                        }
                        catch (Exception ex)
                        {
                            ErrorHandling(ex.Data.ToString(), ex.Source, ex.Message, "downloadfile", "Local");
                        }

                    }
                }
                else
                {
                 
                    context.Response.ContentType = "image/jpg";
                    context.Response.WriteFile(System.Web.HttpContext.Current.Server.MapPath("images/no-preview.jpg"));
                }
                reader.Close();

            }
            catch (Exception ex)
            {
                ErrorHandling(ex.Data.ToString(), ex.Source, ex.Message, "downloadfile", "global");
            }
            finally
            {
                connection.Close();
            }



        }
        private void ErrorHandling(string Data, string Source, string Message, string Page, string function)
        {
            SqlConnection con = new SqlConnection(strcon);
            SqlCommand cmd = new SqlCommand();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                // MessageBox.Show("ErrorHandling" + Message);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Sp_EnterErrorTracking";
                cmd.Connection = con;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Data", Data);
                cmd.Parameters.AddWithValue("@GetType", Source);
                cmd.Parameters.AddWithValue("InnerException", "Error");
                cmd.Parameters.AddWithValue("@Message", Message);
                cmd.Parameters.AddWithValue("@Page", Page);
                cmd.Parameters.AddWithValue("@Source", function);
                cmd.Parameters.AddWithValue("DateTime", DateTime.Now);
                cmd.Parameters.AddWithValue("@UserID", '1');
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            { }
            finally
            { }
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