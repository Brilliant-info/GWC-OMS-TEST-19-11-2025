using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PowerOnRentwebapp.Login;
using System.Web.Services;
using System.Data;
using System.Resources;
using System.Globalization;
using System.Threading;
using System.Reflection;
using PowerOnRentwebapp.ContactPerson;
using PowerOnRentwebapp.CompanySetupService;


namespace PowerOnRentwebapp.PowerOnRent
{
    public partial class DedicatedDriverList : System.Web.UI.Page
    {

       

        protected void Page_Load(object sender, EventArgs e)
        {
            long companyid = 0;
           
            if (Request.QueryString["Id"] == null || Request.QueryString["Id"].ToString() =="")
            {
                companyid = 0;
            }
            else
            {
                 companyid = Convert.ToInt64(Request.QueryString["Id"].ToString());
            }

            if (!IsPostBack)
            {
                BindDriverList(companyid);
            }
        }

        public void BindDriverList(long companyid)
        {
            iCompanySetupClient comsetup = new iCompanySetupClient();
            CustomProfile profile = CustomProfile.GetProfile();
            try
            {
                DataSet ds = new DataSet();
                ds = comsetup.GrtDriverList(companyid,profile.DBConnection._constr);
                gvContactPerson.DataSource = ds;
                gvContactPerson.DataBind();
            }
            catch (Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "DedicatedDriverList.aspx", "BindDriverList"); 
            }
        }


        
              [WebMethod]
        public static string WMChkDrAssignActiveOrd(string drid)
        {
            string result = "";
            CustomProfile profile = CustomProfile.GetProfile();
            iCompanySetupClient comsetup = new iCompanySetupClient();
            try
            {

                result=comsetup.ChkDrAssignActiveOrd(drid, profile.DBConnection._constr);
                 
               
            }
            catch(Exception ex) {
                result = "0";
                Login.Profile.ErrorHandling(ex, "DedicatedDriverList.aspx", "WMChkDrAssignActiveOrd");
            }
            finally { comsetup.Close(); }

            return result;
        }

    }
}