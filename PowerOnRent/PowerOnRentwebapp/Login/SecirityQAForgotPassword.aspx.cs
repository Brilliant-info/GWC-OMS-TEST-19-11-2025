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
using System.Configuration;

namespace PowerOnRentwebapp.Login
{
    public partial class SecirityQAForgotPassword : System.Web.UI.Page
    {
        public static string strcon = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HttpContext.Current.Session["upid"] = "0";
                divsecurity.Attributes.Add("style", "display:none;");
                divuserid.Attributes.Add("style", "display:block;");
            }

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
                string q1 = "", q2 = "", a1 = "", a2 = "", un = "";
                string userid = "0";
                q1 = dictionary["question1"].ToString();
                a1 = dictionary["txtanswer1"].ToString();
                un = dictionary["UserName"].ToString();
                //result = userClient.WMChkSecurityQuestion(q1, q2, a1, a2, Profile.Personal.UserID, Profile.DBConnection._constr);
                userid = HttpContext.Current.Session["upid"].ToString();
                DataSet dscnt = new DataSet();
                dscnt = GetDataSetTable(userid, q1);
                if (dscnt.Tables[0].Rows.Count > 0)
                {
                    string ans1 = "";
                    ans1 = GetStringReturn(userid, q1);
                    string decryans1 = Decryptstring(ans1);
                    if (decryans1 == a1)
                    {
                        result = "match";
                        //DataSet dscnt1 = new DataSet();
                        //dscnt1 = GetDataSetTable(userid, q2);
                        //if (dscnt1.Tables[0].Rows.Count > 0)
                        //{
                        //    string ans2 = "";
                        //    ans2 = GetStringReturn(userid, q2);
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
                else
                {
                    result = "notmatch";
                }
                if (result == "match")
                {
                    string ipadd = "";
                    IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
                    foreach (IPAddress address in ipHostInfo.AddressList)
                    {
                        if (address.AddressFamily == AddressFamily.InterNetwork)
                            ipadd = address.ToString();
                    }
                    SqlConnection conn2 = new SqlConnection(strcon);
                    SqlCommand cmd3 = new SqlCommand();
                    cmd3.CommandType = CommandType.StoredProcedure;
                    cmd3.CommandText = "SP_InsertForgetPasswordDetails";
                    cmd3.Connection = conn2;
                    cmd3.Parameters.Clear();
                    cmd3.Parameters.AddWithValue("UserProfileID", userid);
                    cmd3.Parameters.AddWithValue("transactiontype", "ForgetPassword");
                    cmd3.Parameters.AddWithValue("IPAddress", ipadd);
                    cmd3.Parameters.AddWithValue("transactedby", un);
                    cmd3.Connection.Open();
                    cmd3.ExecuteNonQuery();
                    cmd3.Connection.Close();
                }


            }
            catch (System.Exception ex)
            {
                result = "200";
                Profile.ErrorHandling(ex, "SecirityQAForgotPassword.aspx", "WMChkSecurityQuestion");
            }
            return result;
        }

        public static DataSet GetDataSetTable(string userid, string q1)
        {
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection conn2 = new SqlConnection(strcon))
                {
                    SqlCommand cmd = new SqlCommand();
                    SqlDataAdapter da = new SqlDataAdapter();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_Securitycount";
                    cmd.Connection = conn2;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("Userid", userid);
                    cmd.Parameters.AddWithValue("Que", q1);
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                }
            }
            catch
            {

            }
            return ds;
        }

        //AJIT START

        public static DataSet GetDataSetTable1(string username)
        {
            DataSet ds1 = new DataSet();
            try
            {
                using (SqlConnection conn2 = new SqlConnection(strcon))
                {
                    SqlCommand cmd = new SqlCommand();
                    SqlDataAdapter da = new SqlDataAdapter();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Getuserid";
                    cmd.Connection = conn2;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@Username", username);
                    //cmd.Parameters.AddWithValue("Que", q1);
                    da.SelectCommand = cmd;
                    da.Fill(ds1);
                }
            }
            catch
            {

            }
            return ds1;
        }

        public static DataSet GetDataSetTable2(long userid)
        {
            DataSet ds2 = new DataSet();
            try
            {
                using (SqlConnection conn2 = new SqlConnection(strcon))
                {
                    SqlCommand cmd = new SqlCommand();
                    SqlDataAdapter da = new SqlDataAdapter();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "GetSecurityQUE";
                    cmd.Connection = conn2;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@userid", userid);
                    //cmd.Parameters.AddWithValue("Que", q1);
                    da.SelectCommand = cmd;
                    da.Fill(ds2);
                }
            }
            catch
            {

            }
            return ds2;
        }

        //AJIT END

        public static string GetStringReturn(string userid, string q1)
        {
            string result = "";
            try
            {
                using (SqlConnection conn2 = new SqlConnection(strcon))
                {
                    DataSet ds = new DataSet();
                    SqlCommand cmd = new SqlCommand();
                    SqlDataAdapter da = new SqlDataAdapter();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "SP_SecurityQueAns";
                    cmd.Connection = conn2;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("Userid", userid);
                    cmd.Parameters.AddWithValue("Que", q1);
                    da.SelectCommand = cmd;
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        result = ds.Tables[0].Rows[0]["answer"].ToString();
                    }
                }
            }
            catch
            {

            }
            return result;
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


        [WebMethod]
        public static string WMGetUsernameWMEncryptstring(string uname)
        {
            string result = "";
            result = encryptstring(uname);
            return result;
        }

        [WebMethod]
        public static string WMGetUsername(string Username)
        {
            string result = ""; long userid = 0;
            try
            {
                DataSet ds1 = new DataSet();
                SqlConnection conn = new SqlConnection(strcon);
                //SqlCommand cmd = new SqlCommand("select * from mpassworddetails where username='" + Username + "'", conn);
                //SqlDataAdapter adp = new SqlDataAdapter(cmd);
                //adp.Fill(ds1);
                ds1 = GetDataSetTable1(Username);
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    userid = Convert.ToInt64(ds1.Tables[0].Rows[0]["userprofileid"].ToString());
                    HttpContext.Current.Session["upid"] = userid;
                    DataSet ds2 = new DataSet();
                    SqlConnection conn2 = new SqlConnection(strcon);
                    //SqlCommand cmd2 = new SqlCommand("select * from tsecurityqueanswer where userid=" + userid + "", conn);
                    //SqlDataAdapter adp2 = new SqlDataAdapter(cmd2);
                    //adp2.Fill(ds2);
                    ds2 = GetDataSetTable2(userid);
                    if (ds2.Tables[0].Rows.Count > 0)
                    {
                        result = "pass";
                    }
                    else
                    {
                        result = "nofound";
                    }
                }
                else
                {
                    result = "usernofound";
                }


            }
            catch (System.Exception ex)
            {
                Profile.ErrorHandling(ex, "SecirityQAForgotPassword.aspx", "WMGetUsername");
            }

            return result;
        }


        [WebMethod]
        public static List<contact> WMBindSercurtyQuestion(string uid)
        {
            CustomProfile profile = CustomProfile.GetProfile();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            List<contact> LocList = new List<contact>();
            try
            {
                SqlConnection conn = new SqlConnection(strcon);
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_BindDDLQuestionforuser";
                cmd.Connection = conn;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("userid", HttpContext.Current.Session["upid"].ToString());
                da.SelectCommand = cmd;
                da.Fill(ds);
                dt = ds.Tables[0];
                contact Loc = new contact();
                Loc.Name = "-Select Question-";
                Loc.Id = "0";
                LocList.Add(Loc);
                Loc = new contact();

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Loc.Id = dt.Rows[i]["Id"].ToString();
                        Loc.Name = dt.Rows[i]["Value"].ToString();
                        LocList.Add(Loc);
                        Loc = new contact();
                    }
                }
            }
            catch (Exception ex)
            {
                Profile.ErrorHandling(ex, "SecirityQAForgotPassword.aspx", "WMBindSercurtyQuestion");
            }
            return LocList;
        }

        public class contact
        {
            private string _name;
            public string Name
            {
                get { return _name; }
                set { _name = value; }
            }

            private string _id;
            public string Id
            {
                get { return _id; }
                set { _id = value; }
            }
        }

    }

}