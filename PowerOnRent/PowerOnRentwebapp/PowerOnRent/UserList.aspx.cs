using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PowerOnRentwebapp.Login;
using PowerOnRentwebapp.AccountSearchService;
using PowerOnRentwebapp.CompanySetupService;
using System.Resources;
using System.Globalization;
using System.Threading;
using System.Reflection;
using PowerOnRentwebapp.UserCreationService;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;

namespace PowerOnRentwebapp.PowerOnRent
{
    public partial class UserList : System.Web.UI.Page
    {
        ResourceManager rm;
        CultureInfo ci;
        long CompanyID;
        long DeptId;
        protected void Page_PreInit(Object sender, EventArgs e)
        {
            CustomProfile profile = CustomProfile.GetProfile();
            if (profile.Personal.Theme == null || profile.Personal.Theme == string.Empty) { Page.Theme = "Blue"; } else { if (profile.Personal.Theme == null || profile.Personal.Theme == string.Empty) { Page.Theme = "Blue"; } else { Page.Theme = profile.Personal.Theme; } }
        }

        protected void Page_Load(object sender, EventArgs e)
        {


            loadstring();
            CompanyID = Convert.ToInt64(Request.QueryString["Com"].ToString());
            DeptId = Convert.ToInt64(Request.QueryString["Dept"].ToString());          

            if (!IsPostBack)
            {
                FillCustomerDeatilGrid();
            }

        }

        [WebMethod]
        public static string PMGetHiddenValue(string UserSelectedRec)
        {

            HttpContext.Current.Session["Userconfig"] = UserSelectedRec;
          
            return UserSelectedRec;
        }

        private void FillCustomerDeatilGrid()
        {
            try
            {
                iUserCreationClient userClient = new iUserCreationClient();
                CustomProfile profile = CustomProfile.GetProfile();
                DataSet ds = new DataSet();
                ds= userClient.GWCSearchUserListVF(CompanyID, DeptId, profile.DBConnection._constr);
                GvAccount.DataSource = ds;
                GvAccount.DataBind();
            }
            catch (System.Exception ex)
            {
                 Login.Profile.ErrorHandling(ex, this, "UserList.aspx", "FillCustomerDeatilGrid");
            }
            finally
            {
            }
        }


        private void loadstring()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(Session["Lang"].ToString());
            rm = new ResourceManager("PowerOnRentwebapp.App_GlobalResources.Lang", Assembly.GetExecutingAssembly());
            ci = Thread.CurrentThread.CurrentCulture;

            lbluserlist.Text = rm.GetString("UserList", ci);
            btnSubmitAccountSearch.Value = rm.GetString("Submit", ci);
            Button1.Value = rm.GetString("Submit", ci);
        }
    }
}