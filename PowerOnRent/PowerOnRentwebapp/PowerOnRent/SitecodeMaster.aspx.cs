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
    public partial class SitecodeMaster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindSiteList();
                BindProjectListDDL(0);
            }
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
                    string ID = HttpContext.Current.Session["SiteCodeID"].ToString();
                    if (HttpContext.Current.Session["SiteCodeName"].ToString() == dic["sitecode"].ToString())
                    {
                        UCCommonFilter.ADDorEditSite(ID, dic["projectype"].ToString(), dic["sitecode"].ToString(), dic["sitename"].ToString(), dic["Longitude"].ToString(), dic["Latitude"].ToString(), dic["AccessRequirement"].ToString(), dic["Active"].ToString(), profile.Personal.UserID, profile.Personal.CompanyID, "Update", profile.DBConnection._constr);
                        result = "Site Details Saved Successfully";
                    }
                    else
                    {
                        string checkdulicate = "";
                        checkdulicate = UCCommonFilter.CHKDuplicatesiteCode(dic["sitecode"].ToString(), "Edit", profile.DBConnection._constr);
                        if (checkdulicate == "Yes")
                        {
                            result = "Site Already Available";
                        }
                        else
                        {
                            UCCommonFilter.ADDorEditSite(ID, dic["projectype"].ToString(), dic["sitecode"].ToString(), dic["sitename"].ToString(), dic["Longitude"].ToString(), dic["Latitude"].ToString(), dic["AccessRequirement"].ToString(), dic["Active"].ToString(), profile.Personal.UserID, profile.Personal.CompanyID, "Update", profile.DBConnection._constr);
                            result = "Site Details Saved Successfully";
                        }
                    }

                //    UCCommonFilter.ADDorEditSite("0", dic["projectype"].ToString(), dic["sitecode"].ToString(), dic["sitename"].ToString(), dic["Longitude"].ToString(), dic["Latitude"].ToString(), dic["AccessRequirement"].ToString(), dic["Active"].ToString(), profile.Personal.UserID, profile.Personal.CompanyID, "Update", profile.DBConnection._constr);
                }
                else
                {
                    UCCommonFilter.ADDorEditSite("0", dic["projectype"].ToString(), dic["sitecode"].ToString(), dic["sitename"].ToString(), dic["Longitude"].ToString(), dic["Latitude"].ToString(), dic["AccessRequirement"].ToString(), dic["Active"].ToString(), profile.Personal.UserID, profile.Personal.CompanyID, "ADD", profile.DBConnection._constr);
                    result = "Site Details Saved Successfully";
                }
               
            }
            catch { result = "Some error occurred"; }
            finally { UCCommonFilter.Close(); }

            return result;
        }


        protected void gvContactPerson_Rebind(object sender, EventArgs e)
        {
            BindSiteList();
        }

        protected void BindSiteList()
        {
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                iUCCommonFilterClient UCCommonFilter = new iUCCommonFilterClient();
                DataSet ds = new DataSet();
                string filter = hdnFilterText.Value;
                if (filter == "" || filter == null) { filter = "0"; }
                ds = UCCommonFilter.BindSiteList(filter,"noneed", profile.DBConnection._constr);

                gvContactPerson.DataSource = ds;
                gvContactPerson.DataBind();
            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "SitecodeMaster.aspx", "BindSiteList");
            }

        }

        protected void BindProjectListDDL(long ID)
        {
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                iUCCommonFilterClient UCCommonFilter = new iUCCommonFilterClient();
                DataSet ds = new DataSet();
                //long ID = 0;
                ds = UCCommonFilter.GetProjectTypeListDDL(ID, profile.DBConnection._constr);
                ddlprojecttype.DataSource = ds;
                ddlprojecttype.DataBind();
                ListItem lstCompany = new ListItem { Text = "-Select-", Value = "0" };
                ddlprojecttype.Items.Insert(0, lstCompany);
            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "SitecodeMaster.aspx", "BindProjectListDDL");
            }

        }

        protected void imgBtnEdit1_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                iUCCommonFilterClient UCCommonFilter = new iUCCommonFilterClient();
                CustomProfile profile = CustomProfile.GetProfile();
                ImageButton imgbtn = (ImageButton)sender;
                clear();
                string prdSelValue = hdnSelectedRec.Value.ToString();
                hdnConID.Value = hdnSelectedRec.Value.ToString();
                // Session["DeptID"] = DeptID.ToString();
                Session["SiteCodeID"] = hdnConID.Value.ToString();
                DataSet ds = new DataSet();
                ds = UCCommonFilter.GetSiteCodeData(Convert.ToInt64(hdnConID.Value.ToString()), profile.DBConnection._constr);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    BindProjectListDDL(Convert.ToInt64(ds.Tables[0].Rows[0]["projecttype"].ToString()));
                    ddlprojecttype.SelectedIndex = ddlprojecttype.Items.IndexOf(ddlprojecttype.Items.FindByValue(ds.Tables[0].Rows[0]["projecttype"].ToString()));
                    txtsitecode.Text = ds.Tables[0].Rows[0]["SiteCode"].ToString();
                    txtsitename.Text = ds.Tables[0].Rows[0]["SiteName"].ToString();
                    txtLatitude.Text = ds.Tables[0].Rows[0]["Latitude"].ToString();
                    txtLongitude.Text = ds.Tables[0].Rows[0]["Longitude"].ToString();
                    txtAccessRequirement.Text = ds.Tables[0].Rows[0]["AccessRequirement"].ToString();
                    if (ds.Tables[0].Rows[0]["Active"].ToString() == "Yes")
                    {
                        rbtnYes.Checked = true;
                        rbtnNo.Checked = false;
                    }
                    else
                    {
                        rbtnYes.Checked = false;
                        rbtnNo.Checked = true;
                    }
                    Session["SiteCodeName"] = ds.Tables[0].Rows[0]["SiteCode"].ToString();
                }
                hdnstate.Value = "Edit";
            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "ProjectType.aspx", "imgBtnEdit1_Click");
            }
        }


        protected void clear()
        {
            ddlprojecttype.SelectedIndex = 0;
            txtsitecode.Text = "";
            txtsitename.Text = "";
            txtLongitude.Text = "";
            txtLatitude.Text = "";
            txtAccessRequirement.Text = "";
            rbtnNo.Checked = false;
            rbtnYes.Checked = true;
        }
    }
}