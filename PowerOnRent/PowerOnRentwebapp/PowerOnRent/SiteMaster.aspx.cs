using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Resources;
using System.Globalization;
using System.Threading;
using System.Reflection;
using PowerOnRentwebapp.Login;
using PowerOnRentwebapp.ContactPerson;
using PowerOnRentwebapp.PORServiceUCCommonFilter;
using System.Data;

namespace PowerOnRentwebapp.PowerOnRent
{
    public partial class SiteMaster : System.Web.UI.Page
    {
        string pid = "0";
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.QueryString["pid"] != null || Request.QueryString["pid"] != "")
            {
                pid = Request.QueryString["pid"].ToString();
                BindSiteList(pid);
            }
            else
            {
                pid = Request.QueryString["pid"].ToString();
                BindSiteList(pid);
            }

            //if (!IsPostBack)
            //{
            //    BindSiteList();
            //    BindProjectListDDL();
            //}

        }

        [WebMethod]
        public static string WMSaveRequestHead(object site, string State)
        {
            string result = "";
            CustomProfile profile = CustomProfile.GetProfile();
            iUCCommonFilterClient UCCommonFilter = new iUCCommonFilterClient();
            try
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic = (Dictionary<string, object>)site;             

                if (State == "Edit")
                {
                  //  UCCommonFilter.ADDorEditSite("0", dic["projectype"].ToString(), dic["sitecode"].ToString(), dic["sitename"].ToString(), dic["Longitude"].ToString(), dic["Latitude"].ToString(), dic["AccessRequirement"].ToString(), dic["Active"].ToString(), profile.Personal.UserID, profile.Personal.CompanyID, "Update", profile.DBConnection._constr);
                }
                else
                {
                   // UCCommonFilter.ADDorEditSite("0", dic["projectype"].ToString(), dic["sitecode"].ToString(), dic["sitename"].ToString(), dic["Longitude"].ToString(), dic["Latitude"].ToString(), dic["AccessRequirement"].ToString(), dic["Active"].ToString(), profile.Personal.UserID, profile.Personal.CompanyID, "ADD", profile.DBConnection._constr);
                }
                result = "Site Details Saved Successfully";
            }
            catch { result = "Some error occurred"; }
            finally { UCCommonFilter.Close(); }

            return result;
        }


        protected void gvContactPerson_Rebind(object sender, EventArgs e)
        {
            BindSiteList(pid);
        }

        protected void BindSiteList(string pid)
        {
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                iUCCommonFilterClient UCCommonFilter = new iUCCommonFilterClient();
                DataSet ds = new DataSet();
                string filter = hdnFilterText.Value;
                if (filter == "" || filter == null) { filter = "0"; }
                ds = UCCommonFilter.BindSiteList(filter, pid, profile.DBConnection._constr);

                gvContactPerson.DataSource = ds;
                gvContactPerson.DataBind();
            }
            catch
            {
            }

        }

        protected void BindProjectListDDL()
        {
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                iUCCommonFilterClient UCCommonFilter = new iUCCommonFilterClient();
                DataSet ds = new DataSet();
                long ID = 0;
                ds = UCCommonFilter.GetProjectTypeListDDL(ID, profile.DBConnection._constr);
                ddlprojecttype.DataSource = ds;
                ddlprojecttype.DataBind();
            }
            catch
            {
            }

        }
    }
}