using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PowerOnRentwebapp.Login;
using System.Data;
using System.Web.Services;
using PowerOnRentwebapp.PORServicePartRequest;

namespace PowerOnRentwebapp.PowerOnRent
{
    public partial class InstSebilAPIRetriger : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrdSebilFailed();
            }
        }
        protected void GrdSebilReTrigger_Rebind(object sender, EventArgs e)
        {
            BindGrdSebilFailed();
        }

        private void BindGrdSebilFailed()
        {
            iPartRequestClient objService = new iPartRequestClient();
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                long companyId = profile.Personal.CompanyID;
                long userId = profile.Personal.UserID;
                DataSet dsgrd = new DataSet();
                dsgrd = objService.GetInstalSebilTriggerList(profile.DBConnection._constr);
                GrdSebilReTrigger.DataSource = dsgrd;
                GrdSebilReTrigger.DataBind();
            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "Install Sebil Retrigger", "BindGrdSebilFailed()");
            }
        }

        [WebMethod]
        public static string ReTriggerSebilService(int APILogID)
        {
            string result = "";
            CustomProfile profile = CustomProfile.GetProfile();
            iPartRequestClient objService = new iPartRequestClient();
            try
            {
              int returnval =  objService.UpdateSebilRetrigger(APILogID, profile.DBConnection._constr);
                if (returnval > 0)
                {
                    result = "Success";
                }
                else
                {
                    result = "Fail";
                }
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
    }
 }
   
    
