using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PowerOnRentwebapp.Login;
using PowerOnRentwebapp.InboxService;
using System.Web.Services;
using PowerOnRentwebapp.Approval;
using System.Threading;
using System.Resources;
using System.Reflection;
using System.Globalization;
using System.Data;
using PowerOnRentwebapp.UserCreationService;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace PowerOnRentwebapp.Inbox
{
    public partial class InboxPOR : System.Web.UI.Page
    {
        ResourceManager rm;
        CultureInfo ci;
        static Page staticThispage;
        static int ShowMsg = 0;
        protected void Page_PreInit(Object sender, EventArgs e)
        {
            CustomProfile profile = CustomProfile.GetProfile();
            if (profile.Personal.Theme == null || profile.Personal.Theme == string.Empty) { Page.Theme = "Blue"; } else { if (profile.Personal.Theme == null || profile.Personal.Theme == string.Empty) { Page.Theme = "Blue"; } else { Page.Theme = profile.Personal.Theme; } }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
            

            CustomProfile profile = CustomProfile.GetProfile();
            iInboxClient InboxService = new iInboxClient();
            BindDDLQuestion();
            if (Session["Lang"].ToString() == "")
            {
                Session["Lang"] = Request.UserLanguages[0];
            }
            loadstring();
            if (hndLinkValue.Value == "") hndLinkValue.Value = "All";
            if (profile.Personal.UserType == "Driver")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "showAlert('You are not allow to access web system...!!!','Error','../Login/Login.aspx')", true);
            }
            else
            {
                DataSet dsInbox = new DataSet();
                if (profile.Personal.UserType == "User")
                {
                    dsInbox = InboxService.GetUserInbox(profile.Personal.UserID, profile.DBConnection._constr);
                    if (dsInbox.Tables[0].Rows.Count > 0)
                    {
                        GVInbox.DataSource = dsInbox;
                    }
                }
                else
                {
                    dsInbox = InboxService.GetInbox(profile.Personal.UserID, profile.DBConnection._constr);
                    if (dsInbox.Tables[0].Rows.Count > 0)
                    {
                        GVInbox.DataSource = dsInbox;
                    }
                }
                GVInbox.DataBind();
                dsInbox.Dispose();

                //if (ShowMsg == 0)
                //{  

                string result = "";
                result = InboxService.ChkFillSecurityQue(profile.Personal.UserID, profile.DBConnection._constr);
                if (result == "no")
                {
                    popupSecurityQuestion.Attributes.Add("style", "display:block;");
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "showAlert('Please fill the security question and answer!!!','Error','../Login/SecurityQueAns.aspx')", true);
                }

                long Days = CheckPasswordAge();
                if (Days > 14) { }
                else if (Days < 14 && Days > 0)
                {
                    string msg = "Your Password Will Be Expired Within " + Days + " Days. Please Change Your Password.";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "showAlert('" + msg + "','Error','#')", true);
                }
                else if (Days <= 0)
                {
                    //iUserCreationClient userClient = new iUserCreationClient();
                    //long UserID = profile.Personal.UserID;
                    //string UserName = userClient.GetUserNameByID(UserID, profile.DBConnection._constr);
                    //DataSet ds = new DataSet();
                    //byte lockunlock = 1;
                    //userClient.updatelockunlock(UserName, lockunlock, profile.DBConnection._constr);

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "showAlert('Your Password Is Expired. Please Change The Password.','Error','../Login/ChangeLockedPassword.aspx')", true);

                    //    ShowMsg = 1;
                    //}
                }
            }

            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, "InboxPOR.aspx", "Page_Load");
            }
        }

        public long CheckPasswordAge()
        {
            long DtRemaining = 0;
            CustomProfile profile = CustomProfile.GetProfile();
            iInboxClient InboxService = new iInboxClient();
            try
            {
                long UserID = profile.Personal.UserID;

                DateTime PasswordChngDays = InboxService.GetLastPasswordChangeDate(UserID, profile.DBConnection._constr);

                DateTime CrntDt = DateTime.Now;

                System.TimeSpan datediff = CrntDt - PasswordChngDays;

                long DtDiff = Convert.ToInt64(datediff.Days);

                DtRemaining = 60 - DtDiff;

            }
            catch { }
            finally { }
            return DtRemaining;
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
                Profile.ErrorHandling(ex, "inboxpor.aspx", "BindDDLQuestion");
            }
            finally
            {
            }
        }

        protected void GVInbox_OnRebind(object sender, EventArgs e)
        {
            CustomProfile profile = CustomProfile.GetProfile();
            iInboxClient InboxService = new iInboxClient();
            try
            {
                var SelectedValue = hndLinkValue.Value;
                if (profile.Personal.UserType == "User")
                {
                    if (SelectedValue == "")
                    {
                        GVInbox.DataSource = InboxService.GetUserInbox(profile.Personal.UserID, profile.DBConnection._constr);
                    }
                    else
                    {
                        GVInbox.DataSource = InboxService.GetUserInboxWhere(profile.Personal.UserID, SelectedValue, profile.DBConnection._constr);
                    }
                }
                else
                {
                    if (SelectedValue == "")
                    {
                        GVInbox.DataSource = InboxService.GetInbox(profile.Personal.UserID, profile.DBConnection._constr);
                    }
                    else
                    {
                        GVInbox.DataSource = InboxService.GetInboxWhere(profile.Personal.UserID, SelectedValue, profile.DBConnection._constr);
                    }
                }

            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, this, "inbox", "GVInbox_OnRebind");

            }
            finally
            {
                InboxService.Close();
            }
            GVInbox.DataBind();
        }

        protected void imgBtnView_OnClick(object sender, ImageClickEventArgs e)
        {
            ImageButton imgbtn = (ImageButton)sender;

            var CorID = imgbtn.ToolTip.ToString();

            Session["CORID"] = CorID.ToString();
        }

        [WebMethod]
        public static string WMSetArchive(string SelectedRec)
        {
            CustomProfile profile = CustomProfile.GetProfile();
            iInboxClient InboxService = new iInboxClient();
            try
            {
                InboxService.SetArchive(SelectedRec, profile.DBConnection._constr);

            }
            catch (Exception ex)
            {
                Login.Profile.ErrorHandling(ex, "Inbox", "WMSetArchive");
            }
            finally { InboxService.Close(); }

            return "true";
        }

        [WebMethod]
        public static string wmUpdateApproval(string Status, string Remark, string tApprovalIDs)
        {
            try
            {
                CustomProfile profile = CustomProfile.GetProfile();
                UC_Approval uc = new UC_Approval();
                return uc.FinalUpdateApproval(Status, Remark, tApprovalIDs, profile.Personal.UserID);
            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, staticThispage, "Inbox", "wmUpdateApproval");
                return "";
            }

        }

        private void loadstring()
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo(Session["Lang"].ToString());
                rm = new ResourceManager("PowerOnRentwebapp.App_GlobalResources.Lang", Assembly.GetExecutingAssembly());
                ci = Thread.CurrentThread.CurrentCulture;

                //  lblTodaysMsg.Text = rm.GetString("TodayMsg", ci);
                lblSystemGeneratedMessage.Text = rm.GetString("SystemGeneratedMessage", ci);

                // lblYestardayMsg.Text = rm.GetString("YestMsg", ci);
                lblCorrespondanceMessage.Text = rm.GetString("CorrespondanceMessage", ci);

                lblAllMsg.Text = rm.GetString("AllMessages", ci);
                lblArchiveMsg.Text = rm.GetString("ArchiveMessages", ci);
                lblInbox.Text = rm.GetString("Inbox", ci);
                lblInbox1.Text = rm.GetString("lblInbox1", ci);
                btnArchive.Value = rm.GetString("Archive", ci);
            }
            catch (System.Exception ex)
            {
                Login.Profile.ErrorHandling(ex, staticThispage, "Inbox", "loadstring");
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
                Profile.ErrorHandling(ex, "inboxpor.aspx", "WMSaveSecurityQuestion");
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