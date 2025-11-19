using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PowerOnRentwebapp.Login;
using PowerOnRentwebapp.PORServicePartRequest;
using PowerOnRentwebapp.ToolbarService;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Resources;
using System.Globalization;
using System.Threading;
using System.Reflection;
using WebMsgBox;
//using System.Windows.Forms;

namespace PowerOnRentwebapp.PowerOnRent
{
    public partial class AdvanceSearch : System.Web.UI.Page
    {
        ResourceManager rm;
        CultureInfo ci;
        public string frmdt_lcl, todt_lcl;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // if (Session["ORDType"].ToString() == "Normal")
                string SessionOrderType = "";
                SessionOrderType = Session["AdSOrderType"].ToString();
                if (SessionOrderType == "Normal" || SessionOrderType == "")
                {
                    MsgBox.Show("Not Applicable For Normal Order");
                    Response.Write("<script>window.close();</" + "script>");

                    // Response.Write("<script>window.close();</" + "script>");
                    // ScriptManager.RegisterStartupScript(this, this.GetType(), "Not Applicable For Normal Order", "window.close()", true);
                }
                UC_fromDate.Date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                UC_Todate.Date = DateTime.Now.Date;
            }

            frmdt_lcl = UC_fromDate.Date.Value.ToString("yyyy/MM/dd");
            todt_lcl = UC_Todate.Date.Value.ToString("yyyy/MM/dd");
            hdnFromDate.Value = frmdt_lcl;
            hdnTodate.Value = todt_lcl;
            Session["AdvanFdate"] = hdnFromDate.Value;
            Session["AdvanTdate"] = hdnTodate.Value;

            if (Session["Lang"] == "")
            {
                Session["Lang"] = Request.UserLanguages[0];
            }
            loadstring();
        }

        private void loadstring()
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(Session["Lang"].ToString());
                rm = new ResourceManager("PowerOnRentwebapp.App_GlobalResources.Lang", Assembly.GetExecutingAssembly());
                ci = Thread.CurrentThread.CurrentCulture;

                lblfdate.Text = rm.GetString("FromDate", ci);
                lbltdate.Text = rm.GetString("ToDate", ci);
                lblordcat.Text = rm.GetString("OrderCategory", ci);
                lblordno.Text = rm.GetString("OrderNo", ci);
                lbllcode.Text = rm.GetString("LocationCode", ci);
                lblpass.Text = rm.GetString("Passport", ci);
                lblordtype.Text = rm.GetString("OrderType", ci);
                lblsimseril.Text = rm.GetString("MISISDN", ci);
                lblemail.Text = rm.GetString("EmailID", ci);
               

                lblSemerial.Text = rm.GetString("SimSerial", ci);
                lblpayment.Text = rm.GetString("PaymentType", ci);

                btnsubmitdata.Value = rm.GetString("Submit", ci);
                btncanceldata.Value = rm.GetString("Cancel", ci);

                // UC_AttachmentDocument1.FormHeaderText = rm.GetString("CustomerList", ci);


            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "AdvanceSearch", "loadstring");
            }
        }
        protected void btnclose_Click(object sender, EventArgs e)
        {

        }



        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            iPartRequestClient objServie = new iPartRequestClient();

            DataSet ds = new DataSet();
            CustomProfile profile = CustomProfile.GetProfile();
            string fdate, todate, ordcat, ordno, lcode, passport, ordtype, misidn;

            fdate = UC_fromDate.Date.Value.ToString("yyyy/MM/dd");
            todate = UC_Todate.Date.Value.ToString("yyyy/MM/dd");
            ordcat = ddlordercategory.SelectedValue.ToString();
            if (ordcat == "0")
            {
                ordcat = "";
            }
            ordno = txtorderno.Text;
            lcode = txtlocationcode.Text;
            passport = txtPassqid.Text;
            ordtype = ddlordertype.SelectedValue.ToString();
            if (ordtype == "0")
            {
                ordtype = "";
            }
            misidn = txtmssidn.Text;

            //  GVRequestSearch.DataSource = objServie.GetRequestSearch(fdate,todate,ordcat,ordno,lcode,passport,ordtype,misidn,profile.Personal.UserID, profile.DBConnection._constr);
            //  GVRequestSearch.DataBind();
        }
    }
}