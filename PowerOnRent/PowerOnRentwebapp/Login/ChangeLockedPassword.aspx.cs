using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Web.Security;
using PowerOnRentwebapp.UserCreationService;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Net;
using System.Net.Sockets;

namespace PowerOnRentwebapp.Login
{
    public partial class ChangeLockedPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtConfirmNewPassword.Attributes.Add("onblur", "CheckPassword();");
            if (!IsPostBack)
            {
                CustomProfile profile = CustomProfile.GetProfile();
                lblLoginName.Text = profile.UserName;
            }
        }

        protected void Page_PreInit(Object sender, EventArgs e)
        {
            CustomProfile profile = CustomProfile.GetProfile();
            if (profile.Personal.Theme == null || profile.Personal.Theme == string.Empty) { Page.Theme = "Blue"; } else { Page.Theme = profile.Personal.Theme; }

            //var onBlurScript = Page.ClientScript.GetPostBackEventReference(txtConfirmNewPassword, "OnBlur");
            //txtConfirmNewPassword.Attributes.Add("onblur", onBlurScript);
        }

        [WebMethod]
        public static string PMSaveChangePassword(string loginname, string currentpassword, string newpassword)
        {
            try
            {
                string ipadd = "";
                IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
                foreach (IPAddress address in ipHostInfo.AddressList)
                {
                    if (address.AddressFamily == AddressFamily.InterNetwork)
                        ipadd = address.ToString();
                }
                CustomProfile profile = CustomProfile.GetProfile();
                iUserCreationClient userClient = new iUserCreationClient();
                MembershipUser u = Membership.GetUser(loginname);
                if (u.IsLockedOut == true) u.UnlockUser();
                if (u.ChangePassword(currentpassword, newpassword) == true)
                {
                    string Email = profile.Personal.EmailID;
                    string EncryptPassword = encryptstring(newpassword);
                    EncryptPassword = "|@|" + EncryptPassword;
                    userClient.SavePasswordDetails(profile.Personal.UserID, Email, loginname, EncryptPassword, profile.Personal.UserName, ipadd, "ChangeExpirePassword", profile.Personal.UserID, profile.DBConnection._constr);
                    return "Saved";
                }
                else
                {
                    return "";
                }

            }
            catch (Exception ex) { return ""; }
            finally { }
        }

        [WebMethod]
        public static string PMCheckPassword(string ConfirmPassword)
        {
            CustomProfile profile = CustomProfile.GetProfile();
            iUserCreationClient userClient = new iUserCreationClient();
            DataSet ds = new DataSet();
            string str = "";
            try
            {
                ds = userClient.CheckPasswordHistory(profile.Personal.UserID, ConfirmPassword, profile.DBConnection._constr);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string Password = "";
                    string PreCode = ds.Tables[0].Rows[i]["Password"].ToString().Substring(0, 3);
                    if (PreCode == "|@|")
                    {
                        string EncriptwithoutPrecode = ds.Tables[0].Rows[i]["Password"].ToString().Substring(3);
                        Password = Decryptstring(EncriptwithoutPrecode);
                    }
                    else
                    {
                        Password = ds.Tables[0].Rows[i]["Password"].ToString();
                    }
                    if (Password == ConfirmPassword)
                    {
                        str = "PasswordFound";
                    }
                }

                if (str == "")
                {
                    string OneDayValidation = userClient.CheckOneDayValidation(profile.Personal.UserID, profile.DBConnection._constr);
                    str = OneDayValidation;
                }
            }
            catch (Exception ex) { }
            finally { }
            return str;
        }

        public static string encryptstring(string encryptString)
        {
            string EncryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            byte[] clearBytes = Encoding.Unicode.GetBytes(encryptString);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    encryptString = Convert.ToBase64String(ms.ToArray());
                }
            }
            return encryptString;
        }

        public static string Decryptstring(string cipherText)
        {
            string EncryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

    }
}