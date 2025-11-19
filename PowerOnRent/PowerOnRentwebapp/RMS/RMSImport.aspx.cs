using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PowerOnRentwebapp.Login;

namespace PowerOnRentwebapp.RMS
{
    public partial class RMSImport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CustomProfile profile = CustomProfile.GetProfile();            hdnUserIDimp.Value = profile.Personal.UserID.ToString();            hdnCompanyIdimp.Value = profile.Personal.CompanyID.ToString();
        }
    }
}