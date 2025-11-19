using PowerOnRentwebapp.Login;
using PowerOnRentwebapp.PORServiceUCCommonFilter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PowerOnRentwebapp.PowerOnRent
{
    public partial class SegmentType : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindSegmentList();

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
                        UCCommonFilter.ADDorEditSegmentType(ID, projecttype, Active, profile.Personal.UserID, profile.Personal.CompanyID, "Update", profile.DBConnection._constr);
                        result = "Segment Type Saved Successfully";
                    }
                    else
                    {
                        string checkdulicate = "";
                        checkdulicate = UCCommonFilter.CHKDuplicateSegmentType(projecttype, "Edit", profile.DBConnection._constr);
                        if (checkdulicate == "Yes")
                        {
                            result = "Segment Type Already Available";
                        }
                        else
                        {
                            UCCommonFilter.ADDorEditSegmentType(ID, projecttype, Active, profile.Personal.UserID, profile.Personal.CompanyID, "Update", profile.DBConnection._constr);
                            result = "Segment Type Saved Successfully";
                        }
                    }
                    // result = "Project Type Saved Successfully";
                }
                else
                {
                    string checkdulicate = "";
                    checkdulicate = UCCommonFilter.CHKDuplicateSegmentType(projecttype, "Add", profile.DBConnection._constr);
                    if (checkdulicate == "Yes")
                    {
                        result = "Segment Type Already Available";
                    }
                    else
                    {
                        UCCommonFilter.ADDorEditSegmentType("0", projecttype, Active, profile.Personal.UserID, profile.Personal.CompanyID, "ADD", profile.DBConnection._constr);
                        result = "Segment Type Saved Successfully";
                    }
                }
            }
            catch { result = "Some error occurred"; }
            finally { UCCommonFilter.Close(); }

            return result;
        }

        protected void gvContactPerson_Rebind(object sender, EventArgs e)
        {
            BindSegmentList();
        }

        protected void BindSegmentList()
        {
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                iUCCommonFilterClient UCCommonFilter = new iUCCommonFilterClient();
                DataSet ds = new DataSet();
                string filter = hdnFilterText.Value;
                if (filter == "" || filter == null) { filter = "0"; }
                ds = UCCommonFilter.GetSegmentTypeList(filter, "mastertype", profile.DBConnection._constr);

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
                ds = UCCommonFilter.GetSegmentTypeData(hdnConID.Value.ToString(), profile.DBConnection._constr);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtSegmenttype.Text = ds.Tables[0].Rows[0]["SegmentType"].ToString();
                    Session["Projecttypenm"] = ds.Tables[0].Rows[0]["SegmentType"].ToString();
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
                }
                hdnstate.Value = "Edit";
            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "SegmentType.aspx", "imgBtnEdit1_Click");
            }

        }

        protected void clear()
        {
            txtSegmenttype.Text = "";
            rbtnNo.Checked = false;
            rbtnYes.Checked = true;
        }

    }
}