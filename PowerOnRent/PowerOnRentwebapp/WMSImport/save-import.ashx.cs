using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrilliantWMS.WMSImport
{
    /// <summary>
    /// Summary description for save_import
    /// </summary>
    public class save_import : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string jsonResponse = "";
            context.Response.ContentType = "application/json";
            long getStartRow = long.Parse(context.Request.QueryString["sr"]);
            long getEndRow = long.Parse(context.Request.QueryString["er"]);
            string isRevalidate = context.Request.QueryString["revalidate"];
            if ((getStartRow == 41 || getStartRow == 201 || getStartRow == 321) && isRevalidate == "false")
            {
                //jsonResponse += "{ " +
                //    "\"ImportRequstResult\": " +
                //    "{ " +
                //    "\"Description\": \"Failed\",  " +
                //    "\"FailRows\": \""+ (getStartRow + 7) + ":Prod_Code:Sku already Available in system|" + (getStartRow + 7) + ":Prod_Category:Product category not Available in system|" + (getStartRow + 7) + ":Prod_SubCategory:Product sub category not Available in system|" + (getStartRow + 14) + ":Prod_Code:Sku already Available in system|" + (getStartRow + 14) + ":Prod_Category:Product category not Available in system|" + (getStartRow + 14) + ":Prod_SubCategory:Product sub category not Available in system\", " +
                //    "\"StatusCode\": \"101\" " +
                //    "}" +
                //    "}";
                jsonResponse += "{ " +
                   "\"ImportRequstResult\": " +
                   "{ " +
                   "\"Description\": \"Failed\",  " +
                   "\"FailRows\": \"" + (getStartRow + 7) + ":Prod_Code:Sku already Available in system\", " +
                   "\"StatusCode\": \"101\" " +
                   "}" +
                   "}";
            }
            else
            {
                jsonResponse += "{ " +
                    "\"ImportRequstResult\": " +
                    "{ " +
                    "\"Description\": \"Success\",  " +
                    "\"FailRows\": \"\", " +
                    "\"StatusCode\": \"100\" " +
                    "}" +
                    "}";
            }
            context.Response.Write(jsonResponse);
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