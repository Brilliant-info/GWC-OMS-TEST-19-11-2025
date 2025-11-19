using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PowerOnRentwebapp.Login;
using PowerOnRentwebapp.ProductMasterService;


namespace PowerOnRentwebapp.PowerOnRent
{
    public partial class DirectImport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CustomProfile profile = CustomProfile.GetProfile();
            iProductMasterClient Prod = new iProductMasterClient();
            string CompanySerial = "";
            CompanySerial = Prod.GetCompanySerial(profile.Personal.CompanyID, profile.DBConnection._constr);
            if (CompanySerial ==  "Yes")
            {
                Response.Redirect("../PowerOnRent/VTechImport.aspx");
            }
            else
            {                
                Response.Redirect("../PowerOnRent/DirectImportD.aspx");
            }
        }
    }
}