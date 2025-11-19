using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PowerOnRentwebapp.PORServicePartRequest;
using System.Web.Services;
using PowerOnRentwebapp.Login;
using System.Data;
using System.Net;
using System.Net.Mail;

namespace PowerOnRentwebapp.PowerOnRent
{
    public partial class UpdateRefundstatus : System.Web.UI.Page
    {
        string OrderID = "";
        string UserType = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Id"] != null)
                {
                    if (Request.QueryString["userType"] != null)
                    {
                        lblcode.Text = "Refund Code ";
                        OrderID = Request.QueryString["Id"].ToString();
                        hndSelectedOid.Value = OrderID;
                        UserType = Request.QueryString["userType"].ToString();
                        Bindrefundstatus(UserType);
                    }
                }
            }
        }

        private void Bindrefundstatus(string UserType)
        {
            iPartRequestClient objServie = new iPartRequestClient();
            CustomProfile profile = CustomProfile.GetProfile();
            DataSet ds = new DataSet();
            ds = objServie.GetRefundstatus(UserType, profile.DBConnection._constr);
            ddlstatus.DataSource = ds;
            ddlstatus.DataTextField = "Status";
            ddlstatus.DataValueField = "Id";
            ddlstatus.DataBind();
            ddlstatus.Items.Insert(0, "--Select--");
        }

        [WebMethod]
        public static string WMUpdateRefundstatus(long SelectedOrder, long StatusID, string Status, string codevalue)
        {
            string result = "";
            iPartRequestClient objServie = new iPartRequestClient();
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                long UserID = profile.Personal.UserID;
                string Usertype = profile.Personal.UserType.ToString();
                result = objServie.UpdatefinRefundstatus(SelectedOrder, StatusID, Status, UserID, codevalue, profile.DBConnection._constr);
                /*if (Status == "Pending for Finance activity")
                {
                    string AccEmailID = objServie.GetAccountEmail(SelectedOrder, profile.DBConnection._constr);
                    objServie.MailCompose(AccEmailID, SelectedOrder, profile.DBConnection._constr);
                }*/
            }
            catch (Exception ex)
            {
                result = "Fail";
            }
            finally
            {
                objServie.Close();
            }
            return result;
        }
    }
}