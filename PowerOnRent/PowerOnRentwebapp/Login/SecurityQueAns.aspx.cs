using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using PowerOnRentwebapp.UserCreationService;
using System.Web.Services;
using PowerOnRentwebapp.Login;
using System.Web.Security;
using WebMsgBox;
using Obout.Grid;
using System.Web.UI.HtmlControls;
using System.Resources;
using System.Globalization;
using System.Threading;
using System.Reflection;
using System.Data;
using System.Net.Mail;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;
using System.Net;
using System.Security.Cryptography;
using System.IO;

namespace PowerOnRentwebapp.Login
{
    public partial class SecurityQueAns : System.Web.UI.Page
    {
        protected void Page_PreInit(Object sender, EventArgs e)
        { CustomProfile profile = CustomProfile.GetProfile(); if (profile.Personal.Theme == null || profile.Personal.Theme == string.Empty) { Page.Theme = "Blue"; } else { Page.Theme = profile.Personal.Theme; } }

        protected void Page_Load(object sender, EventArgs e)
        {

            CustomProfile profile = CustomProfile.GetProfile();
            BindDDLQuestion();
            BindQuestionandAnswer();
            //Toolbar1.SetEditRight(false, "Not Allowed");
            //Toolbar1.SetAddNewRight(false, "Not Allowed");

        }

        public void BindQuestionandAnswer()
        {
            try
            {
                iUserCreationClient userClient = new iUserCreationClient();
                CustomProfile profile = CustomProfile.GetProfile();
                DataSet ds = new DataSet();
                ds = userClient.BindQuestionandAnswer(profile.Personal.UserID, profile.DBConnection._constr);
                if(ds.Tables[0].Rows.Count>0)
                {
                    ddlquestion1.SelectedIndex = ddlquestion1.Items.IndexOf(ddlquestion1.Items.FindByValue(ds.Tables[0].Rows[0]["Question"].ToString()));
                    txtanswer1.Text =Decryptstring(ds.Tables[0].Rows[0]["answer"].ToString());
                    ddlquestion2.SelectedIndex = ddlquestion2.Items.IndexOf(ddlquestion2.Items.FindByValue(ds.Tables[0].Rows[1]["Question"].ToString()));
                    txtanswer2.Text = Decryptstring(ds.Tables[0].Rows[1]["answer"].ToString());
                    ddlquestion3.SelectedIndex = ddlquestion3.Items.IndexOf(ddlquestion3.Items.FindByValue(ds.Tables[0].Rows[2]["Question"].ToString()));
                    txtanswer3.Text = Decryptstring(ds.Tables[0].Rows[2]["answer"].ToString());
                }
            }
            catch (System.Exception ex)
            {
                Profile.ErrorHandling(ex, "SecurityQueAns.aspx", "BindQuestionandAnswer");
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
                ds = userClient.BindDDLQuestion(Profile.DBConnection._constr);
                ddlquestion1.DataTextField = "Value";
                ddlquestion1.DataValueField = "ID";
                ddlquestion1.DataSource = ds;
                ddlquestion1.DataBind();
                ListItem lst = new ListItem();
                lst.Text = "-Select-";
                lst.Value = "0";
                ddlquestion1.Items.Insert(0, lst);

                //bind 2nd ddl
                ds = userClient.BindDDLQuestion(Profile.DBConnection._constr);
                ddlquestion2.DataTextField = "Value";
                ddlquestion2.DataValueField = "ID";
                ddlquestion2.DataSource = ds;
                ddlquestion2.DataBind();
                ListItem lst1 = new ListItem();
                lst1.Text = "-Select-";
                lst1.Value = "0";
                ddlquestion2.Items.Insert(0, lst1);


                //bind 3rd ddl
                ds = userClient.BindDDLQuestion(Profile.DBConnection._constr);
                ddlquestion3.DataTextField = "Value";
                ddlquestion3.DataValueField = "ID";
                ddlquestion3.DataSource = ds;
                ddlquestion3.DataBind();
                ListItem lst2 = new ListItem();
                lst2.Text = "-Select-";
                lst2.Value = "0";
                ddlquestion3.Items.Insert(0, lst2);
            }
            catch (System.Exception ex)
            {
                Profile.ErrorHandling(ex, "SecurityQueAns.aspx", "BindDDLQuestion");
            }
            finally
            {
            }
        }

        [WebMethod]
        public static long WMSaveSecurityQuestion(object objReq)
        {
            long i = 0;
            try
            {
                CustomProfile Profile = CustomProfile.GetProfile();
                iUserCreationClient userClient = new iUserCreationClient();
                Dictionary<string, object> dictionary = new Dictionary<string, object>();
                dictionary = (Dictionary<string, object>)objReq;
                string ans1 = "", ans2 = "", ans3 = "";
                ans1 = encryptstring(Convert.ToString(dictionary["txtanswer1"]));
                ans2 = encryptstring(Convert.ToString(dictionary["txtanswer2"]));
                ans3 = encryptstring(Convert.ToString(dictionary["txtanswer3"]));
                i = userClient.SaveSecurityQuestion(Convert.ToString(dictionary["question1"]), Convert.ToString(dictionary["question2"]), Convert.ToString(dictionary["question3"]), ans1, ans2, ans3, Profile.Personal.UserID, Profile.DBConnection._constr);
            }
            catch (System.Exception ex)
            {
                i = 0;
                Profile.ErrorHandling(ex, "SecurityQueAns.aspx", "WMSaveSecurityQuestion");
            }

            return i;
        }

        #region New Encrypt Decrypt code
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

        #endregion
    }
}