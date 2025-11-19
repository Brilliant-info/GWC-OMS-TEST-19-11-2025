using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrilliantWMS.WMSImport
{
    /// <summary>
    /// Summary description for template_details
    /// </summary>
    public class template_details : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string jsonResponse = "";
            context.Response.ContentType = "application/json";
            jsonResponse = "{" +
"\"GetTempDataResult\": {" +
"\"templateId\": \"123\"," +
"\"CompanyId\": \"10322\"," +
"\"CustomerId\": \"10082\"," +
"\"UserId\": \"10650\"," +
"\"Object\": \"SKU\"," +
"\"columnName\": \"Prod_Code:nvarchar(10):0|Prod_Name(20):nvarchar:0|Prod_Category:nvarchar(100):0|Prod_SubCategory:nvarchar(20):0|AliasSKUCode:nvarchar:0|Cost:decimal:0|RetailPrice:decimal:0|PickingMethod:nvarchar:1|UPCBarcode:nvarchar:1|Brand:nvarchar:1|CartonType:nvarchar:1|Height:nvarchar:1|Width:nvarchar:1|Length:nvarchar:1|Weight:nvarchar:1|UOM1Name:nvarchar:1|UOM1Quantity:nvarchar:1|UOM2Name:nvarchar:1|UOM2Quantity:nvarchar:1|UOM3Name:nvarchar:1|UOM3Quantity:nvarchar:0\"" +
"}" +
"}";
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