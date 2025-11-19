using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Data;
using PowerOnRentwebapp.Login;
using PowerOnRentwebapp.CompanySetupService;
using System.IO;
using System.Web.Services;
using PowerOnRentwebapp.PORServicePartRequest;
using iTextSharp.text.pdf;
using System.Data.SqlClient;

namespace PowerOnRentwebapp.PowerOnRent
{
    public partial class DeliveryNote : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn1_Click(object sender, EventArgs e)
        {
            int result = 0;
            DataSet dsDeliveryFormRpt = new DataSet();
            DataSet dsDeliveryFormRpt1 = new DataSet();
            iPartRequestClient objService = new iPartRequestClient();
            CustomProfile profile = CustomProfile.GetProfile();
            dsDeliveryFormRpt = objService.GetReportDataNew(profile.DBConnection._constr);
            dsDeliveryFormRpt.Tables[0].TableName = "dsDeliveryFormRptNew";
            if (dsDeliveryFormRpt.Tables[0].Rows.Count > 0)
            {

                dsDeliveryFormRpt1 = objService.GetReportDataNew1(profile.DBConnection._constr);

                HttpContext.Current.Session["ReportDS"] = dsDeliveryFormRpt;
                HttpContext.Current.Session["ReportDS1"] = dsDeliveryFormRpt1;
                HttpContext.Current.Session["SelObject"] = dsDeliveryFormRpt;
                result = Convert.ToInt16(dsDeliveryFormRpt.Tables[0].Rows.Count);
                //iframeOMSRpt.GetRouteUrl("../POR/Reports/NewReportViewer.aspx");
                iframeOMSRpt.Attributes.Add("src", "../POR/Reports/NewReportViewer.aspx");
            }
            else { result = 0; }

        }




        [WebMethod]
        public static int WMFMSReport(string report)
        {
            int result = 0;
            DataSet DataSet1 = new DataSet();
            DataSet DataSet2 = new DataSet();
            iPartRequestClient objService = new iPartRequestClient();
            CustomProfile profile = CustomProfile.GetProfile();
            DataSet1 = objService.GetReportDataNew(profile.DBConnection._constr);
            DataSet1.Tables[0].TableName = "DataSet1";
            if (DataSet1.Tables[0].Rows.Count > 0)
            {

                DataSet2 = objService.GetReportDataNew1(profile.DBConnection._constr);
                DataSet2.Tables[0].TableName = "DataSet2";

                HttpContext.Current.Session["ReportDS"] = DataSet1;
                HttpContext.Current.Session["ReportDS1"] = DataSet2;
                HttpContext.Current.Session["SelObject"] = "DataSet1";
                result = Convert.ToInt16(DataSet1.Tables[0].Rows.Count);
            }
            else { result = 0; }
            return result;
        }



        [WebMethod]
        public static int WMFMSReport1(string report)
        {
            int result = 0;
            DataSet DataSet1 = new DataSet();
            DataSet DataSet2 = new DataSet();
            DataSet DataSet3 = new DataSet();
            iPartRequestClient objService = new iPartRequestClient();
            CustomProfile profile = CustomProfile.GetProfile();
            DataSet1 = objService.GetReportDataNew(profile.DBConnection._constr);
            DataSet1.Tables[0].TableName = "DataSet1";
            if (DataSet1.Tables[0].Rows.Count > 0)
            {

                DataSet2 = objService.GetReportDataNew1(profile.DBConnection._constr);
                DataSet2.Tables[0].TableName = "DataSet2";

                DataSet3 = objService.GetReportDataNew3(profile.DBConnection._constr);
                DataSet3.Tables[0].TableName = "dsBoardband";

                HttpContext.Current.Session["ReportDS"] = DataSet1;
                HttpContext.Current.Session["ReportDS1"] = DataSet2;
                HttpContext.Current.Session["ReportDS3"] = DataSet3;
                HttpContext.Current.Session["SelObject"] = "DataSet1";
                result = Convert.ToInt16(DataSet1.Tables[0].Rows.Count);
            }
            else { result = 0; }
            return result;
        }


        [WebMethod]
        public static int WMFMSReport2(string report)
        {
            int result = 0;
            DataSet DataSet1 = new DataSet();
            DataSet DataSet2 = new DataSet();
            DataSet DataSet3 = new DataSet();
            iPartRequestClient objService = new iPartRequestClient();
            CustomProfile profile = CustomProfile.GetProfile();
            DataSet1 = objService.GetReportDataNew(profile.DBConnection._constr);
            DataSet1.Tables[0].TableName = "DataSet1";
            if (DataSet1.Tables[0].Rows.Count > 0)
            {

                DataSet2 = objService.GetReportDataNew1(profile.DBConnection._constr);
                DataSet2.Tables[0].TableName = "DataSet2";

                //DataSet3 = objService.GetReportDataNew3(profile.DBConnection._constr);
                //DataSet3.Tables[0].TableName = "dsBoardband";

                HttpContext.Current.Session["ReportDS"] = DataSet1;
                HttpContext.Current.Session["ReportDS2"] = DataSet2;
              //  HttpContext.Current.Session["ReportDS3"] = DataSet3;
                HttpContext.Current.Session["SelObject"] = "DataSet1";
                result = Convert.ToInt16(DataSet1.Tables[0].Rows.Count);
            }
            else { result = 0; }
            return result;
        }

        [WebMethod]
        public static int WMFMSprepaid(string report)
        {
            int result = 0;
            DataSet DataSet1 = new DataSet();
            DataSet DataSet2 = new DataSet();
            DataSet DataSet3 = new DataSet();
            iPartRequestClient objService = new iPartRequestClient();
            CustomProfile profile = CustomProfile.GetProfile();
            DataSet1 = objService.GetReportDataNew(profile.DBConnection._constr);
            DataSet1.Tables[0].TableName = "DataSet1";
            if (DataSet1.Tables[0].Rows.Count > 0)
            {

                DataSet2 = objService.GetReportDataNew1(profile.DBConnection._constr);
                DataSet2.Tables[0].TableName = "DataSet2";

                //DataSet3 = objService.GetReportDataNew3(profile.DBConnection._constr);
                //DataSet3.Tables[0].TableName = "dsBoardband";

                HttpContext.Current.Session["ReportDS"] = DataSet1;
                HttpContext.Current.Session["ReportDS2"] = DataSet2;
                //  HttpContext.Current.Session["ReportDS3"] = DataSet3;
                HttpContext.Current.Session["SelObject"] = "DataSet1";
                result = Convert.ToInt16(DataSet1.Tables[0].Rows.Count);
            }
            else { result = 0; }
            return result;
        }

        [WebMethod]
        public static int WMFMSReport3(string report)
        {

            int result = 0;
            DataSet DataSet1 = new DataSet();
            DataSet DataSet2 = new DataSet();
            DataSet DataSet3 = new DataSet();
            iPartRequestClient objService = new iPartRequestClient();
            CustomProfile profile = CustomProfile.GetProfile();
            DataSet1 = objService.GetReportDataNew(profile.DBConnection._constr);
            DataSet1.Tables[0].TableName = "DataSetPrepaid";
            if (DataSet1.Tables[0].Rows.Count > 0)
            {

                //DataSet2 = objService.GetReportDataNew1(profile.DBConnection._constr);
                //DataSet2.Tables[0].TableName = "DataSet2";

                //DataSet3 = objService.GetReportDataNew3(profile.DBConnection._constr);
                //DataSet3.Tables[0].TableName = "dsBoardband";

                HttpContext.Current.Session["ReportDS"] = DataSet1;
               // HttpContext.Current.Session["ReportDS2"] = DataSet2;
                //  HttpContext.Current.Session["ReportDS3"] = DataSet3;
                HttpContext.Current.Session["SelObject"] = "DataSetPrepaid";
                result = Convert.ToInt16(DataSet1.Tables[0].Rows.Count);
            }
            else { result = 0; }
            return result;
        }
    }
}