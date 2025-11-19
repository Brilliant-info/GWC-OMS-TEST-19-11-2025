using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace PowerOnRentwebapp.UserManagement
{
    public partial class DecryptPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btndecript_Click(object sender, EventArgs e)
        {
            string PreCode = txtencryptpass.Text.Substring(0,3);
            try
            {
                if (PreCode == "|@|")
                {
                    string Encryptpass = txtencryptpass.Text.Substring(3);
                    lbldecryptpass.Text = Decryptstring(Encryptpass);
                }
                else
                {
                    lbldecryptpass.Text = Decryptstring(txtencryptpass.Text);
                }
            }
            catch(Exception ex)
            {
                lbldecryptpass.Text = "Only encrypted Text will be decrypted";
            }
        }

        protected void btnencrypt_Click(object sender, EventArgs e)
        {
            string Plainstring = txtplainstring.Text.Substring(0, 3);
            try
            {
                string EncryptPassword = encryptstring(Plainstring);
                EncryptPassword = "|@|" + EncryptPassword;
                lblencryptpass.Text = EncryptPassword;
            }
            catch (Exception ex)
            {
                lbldecryptpass.Text = "Only Plain Text will be encrypted";
            }
        }

        public string encryptstring(string encryptString)
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

        public string Decryptstring(string cipherText)
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

        protected void btnencrypt_Click1(object sender, EventArgs e)
        {

        }
    }
}