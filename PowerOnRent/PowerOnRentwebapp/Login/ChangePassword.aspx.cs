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
using System.Resources;
using System.Globalization;
using System.Threading;
using System.Reflection;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Net;
using System.Net.Sockets;

namespace PowerOnRentwebapp.Login
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        ResourceManager rm;
        CultureInfo ci;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Lang"] == "")
            {
                Session["Lang"] = Request.UserLanguages[0];
            }
            loadstring();
            txtConfirmNewPassword.Attributes.Add("onblur", "CheckPassword();");
            if (!IsPostBack)
            {
                CustomProfile profile = CustomProfile.GetProfile();
                lblLoginName.Text = profile.UserName;
                BindDDLQuestion();
                dchangepassword.Attributes.Add("style", "display:none;");
                dsecurityquestion.Attributes.Add("style", "display:block;");
            }
           
        }


        public void BindDDLQuestion()
        {
            try
            {
                CustomProfile Profile = CustomProfile.GetProfile();
                iUserCreationClient userClient = new iUserCreationClient();
                DataSet ds = new DataSet();
                //bind 1st ddl
                ds = userClient.BindDDLQuestionforuser(Profile.Personal.UserID, Profile.DBConnection._constr);
                ddlquestion1.DataTextField = "Value";
                ddlquestion1.DataValueField = "ID";
                ddlquestion1.DataSource = ds;
                ddlquestion1.DataBind();
                ListItem lst = new ListItem();
                lst.Text = "-Select Question-";
                lst.Value = "0";
                ddlquestion1.Items.Insert(0, lst);

                //bind 2nd ddl
                //ds = userClient.BindDDLQuestionforuser(Profile.Personal.UserID, Profile.DBConnection._constr);
                //ddlquestion2.DataTextField = "Value";
                //ddlquestion2.DataValueField = "ID";
                //ddlquestion2.DataSource = ds;
                //ddlquestion2.DataBind();
                //ListItem lst1 = new ListItem();
                //lst1.Text = "-Select-";
                //lst1.Value = "0";
                //ddlquestion2.Items.Insert(0, lst1);
            }
            catch (System.Exception ex)
            {
                Profile.ErrorHandling(ex, "ChangePassword.aspx", "BindDDLQuestion");
            }
            finally
            {
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
                    userClient.SavePasswordDetails(profile.Personal.UserID, Email, loginname, EncryptPassword, profile.Personal.UserName, ipadd,"ChangePassword", profile.Personal.UserID, profile.DBConnection._constr);
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


        private void loadstring()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(Session["Lang"].ToString());
            rm = new ResourceManager("PowerOnRentwebapp.App_GlobalResources.Lang", Assembly.GetExecutingAssembly());
            ci = Thread.CurrentThread.CurrentCulture;
            lblheader.Text = rm.GetString("ChangePassword", ci);
            btnSaveChangePassword.Value = rm.GetString("Save", ci);
            btnClearChangePassword.Value = rm.GetString("Clear", ci);
            lblusername.Text = rm.GetString("UserName", ci);
            lblcurrentpass.Text = rm.GetString("CurrentPassword", ci);
            lblnewpassword.Text = rm.GetString("NewPassword", ci);
            lblconfirmpass.Text = rm.GetString("ConfirmPassword", ci);
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


        [WebMethod]
        public static string WMChkSecurityQuestion(object obj1)
        {
            string result = "0";
            try
            {
                CustomProfile Profile = CustomProfile.GetProfile();
                iUserCreationClient userClient = new iUserCreationClient();
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary = (Dictionary<string, object>)obj1;
                string q1 = "", q2 = "", a1 = "", a2 = "";
                q1 = dictionary["question1"].ToString();
               // q2 = dictionary["question2"].ToString();
                a1 = dictionary["txtanswer1"].ToString();
             //   a2 = dictionary["txtanswer2"].ToString();
                if (q1 == q2)
                {
                    result = "100";
                }
                else
                {
                    // result = userClient.WMChkSecurityQuestion(q1, q2, a1, a2, Profile.Personal.UserID, Profile.DBConnection._constr);
                    string userid = Convert.ToString(Profile.Personal.UserID);
                    DataSet dscnt = new DataSet();
                    dscnt = userClient.GetDataSetTable(userid, q1,Profile.DBConnection._constr);
                    if (dscnt.Tables[0].Rows.Count > 0)
                    {
                        string ans1 = "";
                        ans1 = userClient.GetStringReturn(userid, q1,Profile.DBConnection._constr);
                        string decryans1 = Decryptstring(ans1);
                        if (decryans1 == a1)
                        {
                            result = "match";
                            //DataSet dscnt1 = new DataSet();
                            //dscnt1 = userClient.GetDataSetTable(userid, q2, Profile.DBConnection._constr);
                            //if (dscnt1.Tables[0].Rows.Count > 0)
                            //{
                            //    string ans2 = "";
                            //    ans2 = userClient.GetStringReturn(userid, q2, Profile.DBConnection._constr);
                            //    string decryans2 = Decryptstring(ans2);
                            //    if (decryans2 == a2)
                            //    {
                            //        result = "match";
                            //    }
                            //    else
                            //    {
                            //        result = "notmatch";
                            //    }
                            //}
                            //else
                            //{
                            //    result = "notmatch";
                            //}
                        }
                        else
                        {
                            result = "notmatch";
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                result = "200";
                Profile.ErrorHandling(ex, "ChangePassword.aspx", "BindDDLQuestion");
            }
            return result;
        }

       
    }
}