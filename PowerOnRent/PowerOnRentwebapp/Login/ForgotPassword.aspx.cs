using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Collections;
using Obout.Grid;
using System.IO;
using WebMsgBox;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Net;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

namespace PowerOnRentwebapp.Login
{
    public partial class ForgotPassword : System.Web.UI.Page
    {   

        protected void Page_Load(object sender, EventArgs e)
        {
            string uname = Request.QueryString["uname"];
            hdnUserNameVal.Value = Decryptstring(uname);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "changemodeRequest" + Session.SessionID, "changemode(true, 'dive');LoadingOff();", true);

            //ClientLogo1.ImageUrl = "#";
            //if (Request.QueryString["ID"] != null)
            //{
            //    ClientLogo1.ImageUrl = "~/Company/Logo/" + Request.QueryString["ID"].ToString() + ".png";          //}   
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
