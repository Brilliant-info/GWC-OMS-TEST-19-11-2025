using PowerOnRentwebapp.Login;
using PowerOnRentwebapp.PORServicePartRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PowerOnRentwebapp.PowerOnRent
{
    public partial class GridRequestEcommerceSummary : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["FillBy"] != null)
            {
                //FillGVRequest(Request.QueryString["FillBy"].ToString());
                FillGVEcomRequest(Request.QueryString["FillBy"].ToString(), Request.QueryString["Invoker"].ToString());
            }
        }

        protected void Page_PreInit(Object sender, EventArgs e)
        {
            CustomProfile profile = CustomProfile.GetProfile();
            if (profile.Personal.Theme == null || profile.Personal.Theme == string.Empty) { Page.Theme = "Blue"; } else { Page.Theme = profile.Personal.Theme; }
        }

        protected void GVEcomRequest_Rebind(object sender, EventArgs e)
        {
            FillGVEcomRequest(Request.QueryString["FillBy"].ToString(), Request.QueryString["Invoker"].ToString());
        }

        protected void FillGVEcomRequest(string FillBy, string Invoker)
        {
            iPartRequestClient objServie = new iPartRequestClient();
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                GVEcomRequest.DataSource = null;
                GVEcomRequest.DataBind();
                //New Added By Suraj for GWCEcommerce

                string UserType = profile.Personal.UserType.ToString();

                //New Added By Suraj for GWCEcommerce
                if (FillBy == "UserID")
                {
                    if (Invoker == "Request")
                    {
                        if (UserType == "User" || UserType == "Requester" || UserType == "Requestor")
                        {
                           // GVEcomRequest.DataSource = objServie.GetRequestSummayOfUser(profile.Personal.UserID, profile.DBConnection._constr);
                            GVEcomRequest.DataSource = objServie.GetRequestEcommerceSummayOfUser(profile.Personal.UserID, profile.DBConnection._constr); 
                        }
                        else
                        {
                            //GVEcomRequest.DataSource = objServie.GetRequestSummayByUserID(profile.Personal.UserID, profile.DBConnection._constr);
                            GVEcomRequest.DataSource = objServie.GetRequestEcommerceSummayByUserID(profile.Personal.UserID, profile.DBConnection._constr);
                        }
                        GVEcomRequest.Columns[12].Visible = false; GVEcomRequest.AllowMultiRecordSelection = true; GVEcomRequest.AllowRecordSelection = true;
                    }
                    else if (Invoker == "Issue")
                    {
                        if (UserType == "User" || UserType == "Requester" || UserType == "Requestor")
                        {
                            //GVEcomRequest.DataSource = objServie.GetRequestSummayOfUserIssue(profile.Personal.UserID, profile.DBConnection._constr);

                            GVEcomRequest.DataSource = objServie.GetRequestEcommerceSummayOfUserIssue(profile.Personal.UserID, profile.DBConnection._constr);
                        }
                        else
                        {
                           // GVEcomRequest.DataSource = objServie.GetRequestSummayByUserIDIssue(profile.Personal.UserID, profile.DBConnection._constr);
                            GVEcomRequest.DataSource = objServie.GetRequestEcommerceSummayByUserIDIssue(profile.Personal.UserID, profile.DBConnection._constr);
                        }
                        GVEcomRequest.Columns[12].Visible = true; GVEcomRequest.AllowMultiRecordSelection = true; GVEcomRequest.AllowRecordSelection = true;
                    }
                }
                else if (FillBy == "SiteIDs")
                {
                   // GVEcomRequest.DataSource = objServie.GetRequestSummayBySiteIDs(Session["SiteIDs"].ToString(), profile.DBConnection._constr);
                    GVEcomRequest.DataSource = objServie.GetRequestEcommerceSummayBySiteIDs(Session["SiteIDs"].ToString(), profile.DBConnection._constr);
                }
                GVEcomRequest.DataBind();
            }
            catch { }
            finally { objServie.Close(); }
        }
    }
}