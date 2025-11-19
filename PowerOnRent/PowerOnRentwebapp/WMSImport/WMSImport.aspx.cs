using PowerOnRentwebapp.CompanySetupService;
using PowerOnRentwebapp.Login;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace BrilliantWMS.WMSImport
{
    public partial class WMSImport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)

        {
            CustomProfile profile = CustomProfile.GetProfile();
            string getCompanyId = profile.Personal.CompanyID.ToString();
            long CompanyId = long.Parse(getCompanyId);
            hdnCompanyId.Value = getCompanyId;

            hdnUser.Value = profile.Personal.UserID.ToString();
            string userid= profile.Personal.UserID.ToString();
            if (Request.QueryString["obj"] != null)
            {
                hdnImportObject.Value = Request.QueryString["obj"].ToString();
                hdnImportSessionId.Value= userid+"_"+ DateTime.Now.ToString("yyyyMMddHHmmss");
            }
           // getCustomer(CompanyId);
            getCustomer(userid);
        }
        protected void FillCompany()
        {
            ddlcompanymain.Items.Clear();
            iCompanySetupClient CompanyClient = new iCompanySetupClient();
            CustomProfile profile = CustomProfile.GetProfile();
            ddlcompanymain.DataSource = CompanyClient.GetCompanyDropDown(profile.Personal.CompanyID, profile.DBConnection._constr);
            ddlcompanymain.DataBind();
            ListItem lst = new ListItem { Text = "-Select-", Value = "0" };
            ddlcompanymain.Items.Insert(0, lst);
            CompanyClient.Close();
        }
        public void getCustomer(string userid)
        {
            ddlcustomer.Items.Clear();
            iCompanySetupClient CompanyClient = new iCompanySetupClient();
            CustomProfile profile = CustomProfile.GetProfile();
            // ddlcustomer.DataSource = StatutoryClient.GetCustomerList(CompanyID, profile.DBConnection._constr);
                ddlcustomer.DataSource = CompanyClient.GetCustomerID(Convert.ToInt64(userid), profile.DBConnection._constr);
            ddlcustomer.DataTextField = "Name";
            ddlcustomer.DataValueField = "ID";
            ddlcustomer.DataBind();
            ListItem lst = new ListItem { Text = "-Select-", Value = "0" };
            ddlcustomer.Items.Insert(0, lst);
            CompanyClient.Close();
          //  ddlcustomer.SelectedIndex = 1;
        }

        
    }
}