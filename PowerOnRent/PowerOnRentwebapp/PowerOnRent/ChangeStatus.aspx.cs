using PowerOnRentwebapp.Login;
using PowerOnRentwebapp.PORServicePartRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebMsgBox;
using System.Data;

namespace PowerOnRentwebapp.PowerOnRent
{
    public partial class ChangeStatus : System.Web.UI.Page
    {
        string OrderID = "";
        string ismoneyflag = "";
        string UserType = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                OrderID = Request.QueryString["Id"].ToString();
                UserType = Request.QueryString["UserType"].ToString();
                getismoneyflag();
                fillStatus();
                hndSelectedOid.Value = OrderID.ToString();
                if(UserType != "Super Admin")
                {
                    if (Session["ORDType"].ToString() == "Normal" && ismoneyflag == "No")
                    {

                        MsgBox.Show("Not Applicable For Normal Order");
                        Response.Write("<script>window.close();</" + "script>");
                        // Response.Write("<script>window.close();</" + "script>");
                        // ScriptManager.RegisterStartupScript(this, this.GetType(), "Not Applicable For Normal Order", "window.close()", true);
                    }
                }
                else
                {
                    if (Session["ORDType"].ToString() == "Normal")
                    {

                      
                    }
                }
            }
        }

        public void getismoneyflag()
        {
            CustomProfile profile = CustomProfile.GetProfile();
            iPartRequestClient objService = new iPartRequestClient();
            DataSet ds = new DataSet();
            try
            {
                long oid = 0;
                oid = Convert.ToInt64(OrderID);
                //ds = objService.getismoneydept(Convert.ToInt64(OrderID).ToString(), profile.DBConnection._constr);
                ds = objService.getismoneydept(oid, profile.DBConnection._constr);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ismoneyflag = ds.Tables[0].Rows[0]["Result"].ToString();

                }
            }
            catch (Exception ex) { }
            finally
            {

            }

        }

        public void fillStatus()
        {
            iPartRequestClient objService = new iPartRequestClient();
            CustomProfile profile = CustomProfile.GetProfile();
            ddlcngstatus.DataSource = objService.Getcngstatus(Convert.ToInt32(OrderID), UserType,profile.DBConnection._constr);
            ddlcngstatus.DataBind();
            ListItem lst = new ListItem();
            lst.Text = "-Select All-";
            lst.Value = "-Select All-";
            ddlcngstatus.Items.Insert(0, lst);

        }
        [WebMethod]
        public static string WMChangeStatus(string OrderID,string cngvalue)
        {
            string result = "";
            iPartRequestClient objService = new iPartRequestClient();
            try
            {
                DataSet ds = new DataSet();
                string ismoneyflag = "";
                CustomProfile profile = CustomProfile.GetProfile();
               
                string[] aa = OrderID.Split(',').ToArray();
                int cnt = Convert.ToInt32(aa.Length);
                for (int i = 0; i < cnt; i++)
                {
                    long UserID = profile.Personal.UserID;
                    string orderid = aa[i].ToString();
                    long Oid = Convert.ToInt64(orderid);
                    //get status for selected orders
                    long Ordstatus = objService.GetStatus(orderid, profile.DBConnection._constr);

                    // ds = objService.getismoneydept(Convert.ToInt64(OrderID).ToString(), profile.DBConnection._constr);
                    ds = objService.getismoneydept(Oid, profile.DBConnection._constr);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ismoneyflag = ds.Tables[0].Rows[0]["Result"].ToString();

                    }
                    if (ismoneyflag == "Yes")
                    {
                        if (cngvalue == "Delivered")
                        {
                            if (Ordstatus == 3)
                            {
                                // result = objServie.CancelSelectedOrder(Convert.ToInt64(orderid), reasoncode, UserID, profile.DBConnection._constr);
                                result = objService.ChangeStatus(orderid, cngvalue, profile.DBConnection._constr);
                                objService.InsertOrderStatuslog(Convert.ToInt64(orderid), 0, cngvalue, profile.Personal.UserID, profile.DBConnection._constr);
                            }
                        }
                        if (cngvalue == "Cancel")
                        {
                            if (Ordstatus == 3)
                            {
                                result = objService.ChangeStatus(orderid, cngvalue, profile.DBConnection._constr);
                                objService.InsertOrderStatuslog(Convert.ToInt64(orderid), 0, cngvalue, profile.Personal.UserID, profile.DBConnection._constr);
                            }
                        }
                        if (cngvalue == "Delivered to Hub")
                        {
                            if (Ordstatus == 25)
                            {
                                result = objService.ChangeStatus(orderid, cngvalue, profile.DBConnection._constr);
                                objService.InsertOrderStatuslog(Convert.ToInt64(orderid), 0, cngvalue, profile.Personal.UserID, profile.DBConnection._constr);
                            }
                        }
                    }
                    else
                    {
                        if (cngvalue == "Delivered")
                        {
                            // CR 2022 combine project 3 change allow Expired order to update status as Delivered by retail admin.
                            if (Ordstatus == 8 || Ordstatus == 10040 || Ordstatus == 10041)
                            {
                                // result = objServie.CancelSelectedOrder(Convert.ToInt64(orderid), reasoncode, UserID, profile.DBConnection._constr);
                                result = objService.ChangeStatus(orderid, cngvalue, profile.DBConnection._constr);
                                objService.InsertOrderStatuslog(Convert.ToInt64(orderid), 0, cngvalue, profile.Personal.UserID, profile.DBConnection._constr);
                            }
                        }
                        if (cngvalue == "Cancel")
                        {
                            if (Ordstatus == 8 || Ordstatus == 10041)
                            {
                                result = objService.ChangeStatus(orderid, cngvalue, profile.DBConnection._constr);
                                objService.InsertOrderStatuslog(Convert.ToInt64(orderid), 0, cngvalue, profile.Personal.UserID, profile.DBConnection._constr);
                                int Countmailcheck = 0;
                                Countmailcheck = objService.CheckRetailCancelMailsend(Convert.ToInt64(orderid), profile.DBConnection._constr);
                                if (Countmailcheck > 0)
                                {
                                    //string AccEmailID = objService.GetAccountEmail(Convert.ToInt64(orderid), profile.DBConnection._constr);
                                    string AccEmailID = objService.GetExpiredCancelEmail(profile.DBConnection._constr);
                                    objService.MailCompose(AccEmailID, Convert.ToInt64(orderid), profile.DBConnection._constr);
                                }
                            }
                        }
                        if (cngvalue == "Delivered to Hub")
                        {
                            // CR 2022 combine project 3 change allow Expired order to update status as Delivered by retail admin.
                            if (Ordstatus == 25)
                            {
                                // result = objServie.CancelSelectedOrder(Convert.ToInt64(orderid), reasoncode, UserID, profile.DBConnection._constr);
                                result = objService.ChangeStatus(orderid, cngvalue, profile.DBConnection._constr);
                                objService.InsertOrderStatuslog(Convert.ToInt64(orderid), 0, cngvalue, profile.Personal.UserID, profile.DBConnection._constr);
                            }
                        }

                    }

                }
                // result = objService.ChangeStatus(OrderID, cngvalue, profile.DBConnection._constr);
                result = "true";
               
            }
            catch(Exception ex)
            {

            }
            finally
            {
                objService.Close();
            }
            return result;
        }
    }
}