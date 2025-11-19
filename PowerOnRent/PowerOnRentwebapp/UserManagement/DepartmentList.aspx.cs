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
    public partial class DepartmentList : System.Web.UI.Page
    {
        ResourceManager rm;
        CultureInfo ci;
        long companyId = 0;
        long userid = 0;
      
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Lang"] == "")
            {
                Session["Lang"] = Request.UserLanguages[0];
            }
            if (string.IsNullOrEmpty((string)Session["Lang"]))
            {
                Session["Lang"] = Request.UserLanguages[0];
            }
            loadstring();

            companyId = Convert.ToInt64(Request.QueryString["CompanyId"].ToString());
            userid = Convert.ToInt64(Request.QueryString["userid"].ToString());
            hdncompanyid.Value = companyId.ToString();
            hnduserID.Value = userid.ToString();
            RebindGrid(sender, e);
        }

        private void loadstring()
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(Session["Lang"].ToString());
                rm = new ResourceManager("PowerOnRentwebapp.App_GlobalResources.Lang", Assembly.GetExecutingAssembly());
                ci = Thread.CurrentThread.CurrentCulture;

                lblheader.Text = rm.GetString("DeptList", ci);
                btnSubmitProductSearch1.Value = rm.GetString("Submit", ci);
                btnSubmitProductSearch2.Value = rm.GetString("Submit", ci);


            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "Account Master", "loadstring");
            }
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
                Login.Profile.ErrorHandling(ex, this, "DepartmentList.aspx.cs", "RebindGrid");
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

                str = "select MT.ID,MT.Territory,MT.StoreCode,MT.Active,MT.OrderFormat,MT.PriceChange,MT.AutoCancel,MC.Name as Company from mTerritory MT left outer join mCompany MC on MT.ParentID=MC.ID where MT.ParentID=" + companyId + " ";
                SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(str, conn);
                ds1.Reset();
                da.Fill(ds1);
            }
            else
            {
                str = "select MT.ID,MT.Territory,MT.StoreCode,MT.Active,MT.OrderFormat,MT.PriceChange,MT.AutoCancel,MC.Name as Company from mTerritory MT left outer join mCompany MC on MT.ParentID=MC.ID where MT.ParentID=" + companyId + " and (MT.Territory like '%" + filter + "%' or  MT.StoreCode like '%" + filter + "%' or MC.Name like '%" + filter + "%') ";
                SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(str, conn);
                ds1.Reset();
                da.Fill(ds1);
            }

            return ds1;
        }


        [WebMethod]
        public static void PMSaveDepartment(string selectedIds, string UserID)
        {
            iUserCreationClient userClient = new iUserCreationClient();
            CustomProfile profile = CustomProfile.GetProfile();
            try
            {
                long userID = long.Parse(UserID);
                string ids = selectedIds.ToString();
                string[] words = ids.Split(',');
                for (int i = 1; i < words.Length; i++)
                {
                    // long level = 0;
                    //int userid = 0;
                    long locationid = long.Parse(words[i]);
                    if (locationid != 0)
                    {
                        DataSet ds = new DataSet();
                        long count = userClient.CheckUserterritoryDuplicate(locationid, userID, profile.Personal.UserID, profile.DBConnection._constr);
                        if (count <= 0)
                        {
                            userClient.insertUserterritory(locationid, userID, profile.Personal.UserID, profile.DBConnection._constr);
                        }
                    }

                }
            }
            catch { }
            finally
            {
                userClient.Close();
            }

        }

       
       
    }
}