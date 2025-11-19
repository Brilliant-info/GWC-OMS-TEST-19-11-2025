using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PowerOnRentwebapp.Login;
using System.Resources;
using System.Globalization;
using System.Threading;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using WebMsgBox;
using System.Web.Services;
using PowerOnRentwebapp.CompanySetupService;


namespace PowerOnRentwebapp.Account
{
    public partial class PaymentMethod : System.Web.UI.Page
    {
        ResourceManager rm;
        CultureInfo ci;
        long StoreId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Lang"] == "")
            {
                Session["Lang"] = Request.UserLanguages[0];
            }
            //if (Request.QueryString["deptid"] != null)
            //{
            //    StoreId = long.Parse(Request.QueryString["deptid"].ToString());            
                
            //}
            loadstring();
            RebindGrid(sender, e);
        }

        protected void RebindGrid(object sender, EventArgs e)
        {
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                CompanySetupService.iCompanySetupClient CompanyClient = new CompanySetupService.iCompanySetupClient();
                DataSet ds = new DataSet();
                //  ds = GetPrdLst(GridProductSearch.CurrentPageIndex);
                StoreId = Convert.ToInt64(Session["PDeptID"].ToString());
                ds = CompanyClient.GetPaymentLst(StoreId, profile.DBConnection._constr);
                GridProductSearch.DataSource = ds;
                GridProductSearch.DataBind();
            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "SearchProduct.aspx.cs", "RebindGrid");
            }
        }

        DataSet GetPrdLst(int pageIndex)
        {
            DataSet ds1 = new DataSet();
            ds1.Reset();
            SqlConnection conn = new SqlConnection("");
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["elegantcrm7ConnectionString"].ConnectionString;
            string str = "";
            pageIndex = pageIndex + 1;

            str = "select * from mPaymentMethodMain";
            SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(str, conn);
            ds1.Reset();
            da.Fill(ds1);
            return ds1;
        }

        [WebMethod]
        public static void PMSaveContactD(string selectedIds)
        {
            CompanySetupService.iCompanySetupClient CompanyClient = new CompanySetupService.iCompanySetupClient();
            CustomProfile profile = CustomProfile.GetProfile();
            int sequence = 0;
            string ids = selectedIds.ToString();
            string[] words = ids.Split(',');
            for (int i = 1; i < words.Length; i++)
            {
                long MethodID = long.Parse(words[i]);
                long DeptID = 0;
                long ChkDeptID = long.Parse(HttpContext.Current.Session["PDeptID"].ToString());
                sequence = sequence + 1;
                long count = CompanyClient.CheckDuplicatePMethod(MethodID, ChkDeptID, profile.DBConnection._constr);
                if (count <= 0)
                {
                    CompanyClient.InsertIntoDeptPayment(DeptID, MethodID, sequence, profile.DBConnection._constr);
                }
            }
            //string selectedIds = rec["SelectedIds"].ToString();
        }

        private void loadstring()
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(Session["Lang"].ToString());
                rm = new ResourceManager("PowerOnRentwebapp.App_GlobalResources.Lang", Assembly.GetExecutingAssembly());
                ci = Thread.CurrentThread.CurrentCulture;

                lblheader.Text = rm.GetString("SKUList", ci);
                btnSubmitProductSearch1.Value = rm.GetString("Submit", ci);               
                btnSubmitProductSearch2.Value = rm.GetString("Submit", ci);
            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "Account Master", "loadstring");
            }
        }
    }
}