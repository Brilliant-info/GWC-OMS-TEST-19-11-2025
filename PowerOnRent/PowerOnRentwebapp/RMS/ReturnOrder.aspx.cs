using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using PowerOnRentwebapp.Login;
using System.Web.UI.WebControls;

namespace PowerOnRentwebapp.RMS
{
    public partial class ReturnOrder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CustomProfile profile = CustomProfile.GetProfile();
            hdnUserID.Value = profile.Personal.UserID.ToString();
            hdnCompanyId.Value = profile.Personal.CompanyID.ToString();
            hdnUserType.Value = profile.Personal.UserType.ToString();
            hdnUserName.Value = profile.Personal.UserName.ToString();
        }
    }
}