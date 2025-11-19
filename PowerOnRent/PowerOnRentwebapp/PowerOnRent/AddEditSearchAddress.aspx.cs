using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PowerOnRentwebapp.Login;
//using PowerOnRentwebapp.AddressInfoService;
using PowerOnRentwebapp.ServiceContactPersonInfo;
using System.Web.Services;
using System.Data;
using System.Resources;
using System.Globalization;
using System.Threading;
using System.Reflection;
using PowerOnRentwebapp.ContactPerson;
using PowerOnRentwebapp.PORServiceUCCommonFilter;

namespace PowerOnRentwebapp.PowerOnRent
{
    public partial class AddEditSearchAddress : System.Web.UI.Page
    {
        ResourceManager rm;
        CultureInfo ci;
        long companyID = 0;
        static string sessionID;
        static string TargetObject;
        static string Sequence;
        static Page thispage;
        long DeptID = 0;
        long UserID = 0;

        protected void Page_PreInit(Object sender, EventArgs e)
        {
            CustomProfile profile = CustomProfile.GetProfile();
            if (profile.Personal.Theme == null || profile.Personal.Theme == string.Empty) { Page.Theme = "Blue"; } else { Page.Theme = profile.Personal.Theme; }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CustomProfile profile = CustomProfile.GetProfile();
            try
            {
                UserID = profile.Personal.UserID;

                sessionID = Session.SessionID;
                thispage = this;

                DeptID = long.Parse(Session["DeptID"].ToString());

                bindDropdown();
                BindAddressList(DeptID);
                clear();

                if (Session["Lang"] == "")
                {
                    Session["Lang"] = Request.UserLanguages[0];
                }
                loadstring();

                // Page.ClientScript.RegisterStartupScript(GetType(), "fillCountry1" + sessionID, "setCountry('Select Country','Select State','0','0');", true);
            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "AddEditSearchAddress", "Page_Load");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "showAlert('No Address Available','Error','#')", true);
            }
            finally { }
        }
        public void bindDropdown()
        {
            CustomProfile profile = CustomProfile.GetProfile();
            iUCCommonFilterClient UCCommonFilter = new iUCCommonFilterClient();
            DataSet ds = new DataSet();
            ds = UCCommonFilter.GetStateList(profile.DBConnection._constr);
            ddlState.DataSource = ds;
            ddlState.DataBind();
        }
        protected void BindAddressList(long DeptID)
        {
            List<tAddress> AdrsLst = new List<tAddress>();
            iUCCommonFilterClient UCCommonFilter = new iUCCommonFilterClient();
            CustomProfile profile = CustomProfile.GetProfile();

            //string AdrsType = UCCommonFilter.GetAdrsType(DeptID, profile.DBConnection._constr);
            //if (AdrsType == "Location")
            //{
            //    tblAddAdrs.Attributes.Add("style", "display:none");
            //    gvContactPerson.Columns[0].Visible = false;
            //    AdrsLst = UCCommonFilter.GetUserLocation(profile.Personal.UserID, profile.DBConnection._constr).ToList();
            //}
            //else
            //{
                tblAddAdrs.Attributes.Add("style", "display:''");
                long CompanyID = UCCommonFilter.GetCompanyIDFromSiteID(DeptID, profile.DBConnection._constr);
                // AdrsLst = UCCommonFilter.GetDeptAddressList(CompanyID, profile.DBConnection._constr).ToList();
                AdrsLst = UCCommonFilter.GetDeptAddressListAdrsType(CompanyID, DeptID, profile.DBConnection._constr).ToList();
            //}
            gvContactPerson.DataSource = AdrsLst;
            gvContactPerson.DataBind();
        }
        protected void gvContactPerson_OnRebind(object sender, EventArgs e)
        {
            BindAddressList(DeptID);
        }
        protected void clear()
        {
            txtAddress.Text = "";
            txtcity.Text = "";
            //txtMobileNo.Text = "";
        }
        protected void imgBtnEdit_OnClick(object sender, ImageClickEventArgs e)
        {
            ImageButton imgbtn = (ImageButton)sender;
            clear();
            string prdSelValue = hdnSelectedRec.Value.ToString();
            hdnConID.Value = hdnSelectedRec.Value.ToString();
            Session["DeptID"] = DeptID.ToString();
            Session["ConID"] = hdnConID.Value.ToString();
            GetContactDetailByContactID();
            hdnstate.Value = "Edit";
        }
        protected void GetContactDetailByContactID()
        {
            CustomProfile profile = CustomProfile.GetProfile();
            iUCCommonFilterClient UCCommonFilter = new iUCCommonFilterClient();
            try
            {
                tAddress conper = new tAddress();
                conper = UCCommonFilter.GetAddressDetailsByID(long.Parse(hdnConID.Value), profile.DBConnection._constr);
                if (sessionID == null) sessionID = Session.SessionID;
                if (conper.AddressLine1 != null) txtAddress.Text = conper.AddressLine1.ToString();
                if (conper.City != null) txtcity.Text = conper.City.ToString();
                bindDropdown();
                ddlState.SelectedIndex = ddlState.Items.IndexOf(ddlState.Items.FindByValue(conper.State.ToString()));

                // Page.ClientScript.RegisterStartupScript(GetType(), "fillCountry1" + sessionID, "setCountry('India','Maharashtra','0','0');", true);
                // Page.ClientScript.RegisterStartupScript(GetType(), "fillCountry1" + sessionID, "setCountry('" + conper.County + "','" + conper.State + "','0','0');", true);
                // Page.ClientScript.RegisterStartupScript(GetType(), "fillCountry" + sessionID, "setCountry('" + conper.County + "','" + conper.State + "','" + conper.Zone + "','" + conper.SubZone + "');", true);
            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "AddEditSearchAddress", "GetContactDetailByContactID");
            }
            finally
            {
                UCCommonFilter.Close();

            }
        }
        [WebMethod]
        public static string WMSaveRequestHead(object objAdr, string State)
        {
            string result = "";
            CustomProfile profile = CustomProfile.GetProfile();
            iUCCommonFilterClient UCCommonFilter = new iUCCommonFilterClient();
            tAddress Adrs = new tAddress();
            try
            {
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary = (Dictionary<string, object>)objAdr;

                string Addline1= dictionary["AddressLine1"].ToString();
                Addline1 = Addline1.Replace("\"", "").Replace("'", "");
                Adrs.AddressLine1 = Addline1;
                Adrs.City = dictionary["City"].ToString();
                Adrs.County = "Qatar";
                Adrs.State = dictionary["State"].ToString();


                if (State == "Edit")
                {
                    Adrs.ID = Convert.ToInt64(HttpContext.Current.Session["ConID"].ToString());
                    Adrs.LastModifiedBy = profile.Personal.UserID.ToString();
                    Adrs.LastModifiedDate = DateTime.Now;

                    UCCommonFilter.EditAddress(Adrs, profile.DBConnection._constr);
                }
                else
                {
                    long DID = Convert.ToInt64(HttpContext.Current.Session["DeptID"].ToString());
                    long CompanyID = UCCommonFilter.GetCompanyIDFromSiteID(DID, profile.DBConnection._constr);

                    Adrs.ObjectName = "Account";
                    Adrs.ReferenceID = DID;
                    Adrs.RouteID = CompanyID;
                    Adrs.CompanyID = CompanyID;
                    Adrs.Sequence = 1;
                    Adrs.AddressType = "none";
                    Adrs.IsDefault = "N";
                    Adrs.Active = "Y";//Set Active Y N
                    Adrs.CreatedBy = profile.Personal.UserID.ToString();
                    Adrs.CreationDate = DateTime.Now;
                    UCCommonFilter.AddIntotAddress(Adrs, profile.DBConnection._constr);
                }
                result = "Address saved successfully";
            }
            catch { result = "Some error occurred"; }
            finally { UCCommonFilter.Close(); }

            return result;
        }

        private void loadstring()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(Session["Lang"].ToString());
            rm = new ResourceManager("PowerOnRentwebapp.App_GlobalResources.Lang", Assembly.GetExecutingAssembly());
            ci = Thread.CurrentThread.CurrentCulture;

            lblstate.Text = rm.GetString("State", ci);
            lbladdress1.Text = rm.GetString("AddressLine", ci);
            lblcity.Text = rm.GetString("City", ci);
            lblAddressList.Text = rm.GetString("AddressList", ci);
            btnSave.Value = rm.GetString("Save", ci);
            btnSubmit.Value = rm.GetString("Submit", ci);
        }

    }
}