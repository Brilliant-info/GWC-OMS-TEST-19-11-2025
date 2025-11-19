using PowerOnRentwebapp.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PowerOnRentwebapp.UserConfiguration
{
    public partial class UserConfiguration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CustomProfile profile = CustomProfile.GetProfile();
            hdnConfigUserID.Value = profile.Personal.UserID.ToString();
            hdnConfigCompanyId.Value = profile.Personal.CompanyID.ToString();
        }
    }
}