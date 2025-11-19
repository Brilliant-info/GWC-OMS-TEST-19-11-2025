using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using PowerOnRentwebapp.Login;
using System.Data;
using System.Net.Mail;
using PowerOnRentwebapp.ApprovalLevelMasterService;
using System.Web.Services;
using PowerOnRentwebapp.PORServicePartRequest;

namespace PowerOnRentwebapp.PowerOnRent
{
    public partial class ReTrigger : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGV();
            }
        }

        protected void GridReTrigger_Rebind(object sender, EventArgs e)
        {
            BindGV();
        }

        private void BindGV()
        {

            iPartRequestClient objService = new iPartRequestClient();

            ApprovalLevelMasterService.iApprovalLevelMasterClient ApprovalClient = new ApprovalLevelMasterService.iApprovalLevelMasterClient();
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                long companyId = profile.Personal.CompanyID;
                long userId = profile.Personal.UserID;
                DataSet dsgrd = new DataSet();
                dsgrd = ApprovalClient.GetTriggerList(profile.DBConnection._constr);
                
                GridReTrigger.DataSource = dsgrd;
                GridReTrigger.DataBind();
                ApprovalClient.Close();
            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "Stock Config", "GetUserByID");
            }
        }

        [WebMethod]
        public static string ReTriggerService()
        {
            string result = "";
            CustomProfile profile = CustomProfile.GetProfile();
            try
            {
                result = "Success";
            }
            catch (Exception ex)
            {
                // Login.Profile.ErrorHandling(ex,"",  "Product master", "GetProductDetailByProductID");
                result = "Fail";
            }
            finally
            {

            }
            return result;
        }
        public class contact
        {
            private string _name;
            public string Name
            {
                get { return _name; }
                set { _name = value; }
            }

            private string _id;
            public string Id
            {
                get { return _id; }
                set { _id = value; }
            }
        }
    }
}