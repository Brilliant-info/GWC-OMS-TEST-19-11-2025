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
    public partial class ProjectTypeMaster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindProjectList();

            }
        }
        [WebMethod]
        public static string WMSaveRequestHead(string projecttype, string Active, string State)
        {
            string result = "";
            CustomProfile profile = CustomProfile.GetProfile();
            iUCCommonFilterClient UCCommonFilter = new iUCCommonFilterClient();
            try
            {

                if (State == "Edit")
                {
                    string ID = HttpContext.Current.Session["ProjecttID"].ToString();
                    if (HttpContext.Current.Session["Projecttypenm"].ToString() == projecttype)
                    {
                        UCCommonFilter.ADDorEditProjectType(ID, projecttype, Active, profile.Personal.UserID, profile.Personal.CompanyID, "Update", profile.DBConnection._constr);
                        result = "Project Type Saved Successfully";
                    }
                    else
                    {
                        string checkdulicate = "";
                        checkdulicate = UCCommonFilter.CHKDuplicateProjectType(projecttype, "Edit", profile.DBConnection._constr);
                        if (checkdulicate == "Yes")
                        {
                            result = "Project Type Already Available";
                        }
                        else
                        {
                            UCCommonFilter.ADDorEditProjectType(ID, projecttype, Active, profile.Personal.UserID, profile.Personal.CompanyID, "Update", profile.DBConnection._constr);
                            result = "Project Type Saved Successfully";
                        }
                    }
                    // result = "Project Type Saved Successfully";
                }
                else
                {
                    string checkdulicate = "";
                    checkdulicate = UCCommonFilter.CHKDuplicateProjectType(projecttype, "Add", profile.DBConnection._constr);
                    if (checkdulicate == "Yes")
                    {
                        result = "Project Type Already Available";
                    }
                    else
                    {
                        UCCommonFilter.ADDorEditProjectType("0", projecttype, Active, profile.Personal.UserID, profile.Personal.CompanyID, "ADD", profile.DBConnection._constr);
                        result = "Project Type Saved Successfully";
                    }
                }
            }
            catch { result = "Some error occurred"; }
            finally { UCCommonFilter.Close(); }

            return result;
        }

        protected void gvContactPerson_Rebind(object sender, EventArgs e)
        {
            BindProjectList();
        }

        protected void BindProjectList()
        {
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                iUCCommonFilterClient UCCommonFilter = new iUCCommonFilterClient();
                DataSet ds = new DataSet();
                string filter = hdnFilterText.Value;
                if (filter == "" || filter == null) { filter = "0"; }
                ds = UCCommonFilter.GetProjectTypeList(filter,"master", profile.DBConnection._constr);

                gvContactPerson.DataSource = ds;
                gvContactPerson.DataBind();
            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "ProjectType.aspx", "BindProjectList");
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
                Session["ProjecttID"] = hdnConID.Value.ToString();
                DataSet ds = new DataSet();
                ds = UCCommonFilter.GetProjectTypeData(hdnConID.Value.ToString(), profile.DBConnection._constr);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtprojectype.Text = ds.Tables[0].Rows[0]["projecttype"].ToString();
                    Session["Projecttypenm"] = ds.Tables[0].Rows[0]["projecttype"].ToString();
                    if(ds.Tables[0].Rows[0]["Active"].ToString()=="Yes")
                    {
                        rbtnYes.Checked = true;
                        rbtnNo.Checked = false;
                    }
                    else
                    {
                        rbtnYes.Checked = false;
                        rbtnNo.Checked = true;
                    }
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
            txtprojectype.Text = "";
            rbtnNo.Checked = false;
            rbtnYes.Checked = true;
        }

    }
}