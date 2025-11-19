using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PowerOnRentwebapp.Login;
using PowerOnRentwebapp.UCProductSearchService;
using System.Resources;
using System.Globalization;
using System.Threading;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using WebMsgBox;
using System.Web.Services;
using PowerOnRentwebapp.UserCreationService;

namespace PowerOnRentwebapp.UserManagement
{
    public partial class LocationList : System.Web.UI.Page
    {
        ResourceManager rm;
        CultureInfo ci;
        long userId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Lang"] == null)
            {
                Session["Lang"] = Request.UserLanguages[0];
            }
            loadstring();
            userId = Convert.ToInt64(Request.QueryString["UserId"].ToString());
            hdnuserid.Value = userId.ToString();
            RebindGrid(sender, e);
        }

        protected void RebindGrid(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = GetPrdLst(GridProductSearch.CurrentPageIndex, hdnFilterText.Value);
                GridProductSearch.DataSource = ds;
                GridProductSearch.DataBind();
            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "LocationList.aspx.cs", "RebindGrid");
            }
        }

        DataSet GetPrdLst(int pageIndex, string filter)
        {
            DataSet ds1 = new DataSet();
            ds1.Reset();
            SqlConnection conn = new SqlConnection("");
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["elegantcrm7ConnectionString"].ConnectionString;
            string str = "";
            pageIndex = pageIndex + 1;
            if (filter == "")
            {
                // str = "select Id,ProductCode,OMSSKUCode,Name,Description from mProduct where StoreId = '" + StoreId + "'";

                str = "select A.ID,A.LocationCode,A.AddressLine1, A.City, A.State, A.ContactName, A.ContactEmail from tAddress A where A.AddressType = 'Location' and A.Active = 'Y' and A.CompanyID = (select UP.CompanyID from mUserProfileHead UP where UP.ID = '" + userId + "')";
                SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(str, conn);
                ds1.Reset();
                da.Fill(ds1);
            }
            else
            {
                str = "select A.ID,A.LocationCode,A.AddressLine1, A.City, A.State, A.ContactName, A.ContactEmail from tAddress A where A.AddressType = 'Location' and A.Active = 'Y' and A.CompanyID = (select UP.CompanyID from mUserProfileHead UP where UP.ID = '" + userId + "') and (A.AddressLine1 like '%" + filter + "%' or  A.State like '%" + filter + "%' or A.LocationCode like '%" + filter + "%')";
                SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(str, conn);
                ds1.Reset();
                da.Fill(ds1);
            }

            return ds1;
        }

        [WebMethod]
        public static void SaveAllLocation(long userid)
        {
            using (iUserCreationClient userClient = new iUserCreationClient())
            {
                CustomProfile profile = CustomProfile.GetProfile();
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                ds = userClient.GetAllLocation(userid, profile.DBConnection._constr);
                dt = ds.Tables[0];
               //// userClient.InsertAllLocation(dt, profile.DBConnection._constr);
               userClient.InsertAllLocation(dt, userid, profile.DBConnection._constr);
            }
        }

        [WebMethod]
        public static void PMSaveLocation(string selectedIds, long userid)
        {
            iUserCreationClient userClient = new iUserCreationClient();
            CustomProfile profile = CustomProfile.GetProfile();
            
            string ids = selectedIds.ToString();
            string[] words = ids.Split(',');
            for (int i = 1; i < words.Length; i++)
            {
                long locationid = long.Parse(words[i]);
                long count = userClient.GetDuplicatlocationUser(userid, locationid, profile.DBConnection._constr);
                if (count <= 0)
                {
                    userClient.InsertIntoUserLocation(userid, locationid, profile.DBConnection._constr);
                }
            }

        }



        private void loadstring()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(Session["Lang"].ToString());
            rm = new ResourceManager("PowerOnRentwebapp.App_GlobalResources.Lang", Assembly.GetExecutingAssembly());
            ci = Thread.CurrentThread.CurrentCulture;

            lblheader.Text = rm.GetString("LocationList", ci);
            lblselectall.Text = rm.GetString("Selectall", ci);
            btnSubmitProductSearch1.Value = rm.GetString("Submit", ci);
            btnSubmitProductSearch2.Value = rm.GetString("Submit", ci);


        }
    }
}