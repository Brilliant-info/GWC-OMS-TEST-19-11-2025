using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Net.Sockets;

namespace PowerOnRentwebapp.Login
{
    public partial class ForgetPasswordByAdmin : System.Web.UI.Page
    {
        public static string strcon = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {

            }
            else
            {
                forgetpasswordsuperadmin();
            }
        }

        public void forgetpasswordsuperadmin()
        {
            CustomProfile profile = CustomProfile.GetProfile();
            string ipadd = "";string uid = "0";
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress address in ipHostInfo.AddressList)
            {
                if (address.AddressFamily == AddressFamily.InterNetwork)
                    ipadd = address.ToString();
            }
            if(hdnuname.Value=="" ||hdnuname.Value == null)
            {
                uid = "0";
            }
            else
            {
                uid = hdnuname.Value;
            }
            SqlConnection conn2 = new SqlConnection(strcon);
            SqlCommand cmd3 = new SqlCommand();
            cmd3.CommandType = CommandType.StoredProcedure;
            cmd3.CommandText = "SP_InsertForgetPasswordByAdmin";
            cmd3.Connection = conn2;
            cmd3.Parameters.Clear();
            cmd3.Parameters.AddWithValue("UserProfileID", uid);
            cmd3.Parameters.AddWithValue("transactiontype", "ForgetPasswordBySuperAdmin");
            cmd3.Parameters.AddWithValue("IPAddress", ipadd);
            cmd3.Parameters.AddWithValue("transactedby", profile.Personal.UserName);
            cmd3.Parameters.AddWithValue("createdby", profile.Personal.UserID);
            cmd3.Connection.Open();
            cmd3.ExecuteNonQuery();
            cmd3.Connection.Close();
        }
    }
}