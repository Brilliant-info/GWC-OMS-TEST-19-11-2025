<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeliveryNote.aspx.cs" Inherits="PowerOnRentwebapp.PowerOnRent.DeliveryNote" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=14.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <style type="text/css">
        .divDetailExpandPopUpOn {
            border: solid 3px gray;
            width: 90%;
            max-height: 543px;
            padding: 10px;
            background-color: #FFFFFF;
            margin-top: 50px;
            top: 17%;
            left: 3%;
            position: absolute;
            z-index: 99999;
        }

        .divDetailExpandPopUpOff {
            display: none;
        }

        .popupClose {
            background: url(../App_Themes/Blue/img/icon_close.png) no-repeat;
            height: 32px;
            width: 32px;
            float: right;
            margin-top: -30px;
            margin-right: -25px;
        }

            .popupClose:hover {
                cursor: pointer;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
          <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
        <div>
             <div class="divDetailExpandPopUpOff" id="divPopUp">
            <center>
                <div class="popupClose" onclick="CloseShowReport()">
                </div>
                <div class="divDetailExpand" id="div1">
                    <iframe runat="server" id="iframeOMSRpt" clientidmode="Static" src="#" width="80%"
                        style="border: none; height: 450px;"></iframe>
                </div>
            </center>
        </div>


            <asp:Button ID="btn1" runat="server" OnClick="btn1_Click" Text="submit" />
            <input type="button" id="btn2" onclick="Getreport();"  value="submit" />

             <input type="button" id="btn3" onclick="Getreport1();"  value="submitcaf" />
             <input type="button" id="btn4" onclick="TODAYRPT();"  value="TODAYRPT" />



                       <input type="button" id="btn25" onclick="prepaid();"  value="prepaid" />
        </div>
    </form>
</body>
</html>

<script type="text/javascript">


    function Getreport() {
        PageMethods.WMFMSReport("rpt", jsGetReportDataOnSuccess, null);
    }

 function jsGetReportDataOnSuccess() {
        // document.getElementById("iframeOMSRpt").src = "../PowerOnRent/VirtualProductReport.aspx";   // iframeOMSRpt   ReportViewer.aspx
        document.getElementById("iframeOMSRpt").src = "../POR/Reports/NewReportViewer.aspx";
        divPopUp.className = "divDetailExpandPopUpOn";
    }


     function Getreport1() {
        PageMethods.WMFMSReport1("rpt", jsGetReportDataOnSuccess, null);
    }

    function TODAYRPT() {
        PageMethods.WMFMSReport2("rpt", jsGetReportDataOnSuccess, null);
    }

    
    function prepaid() {
        PageMethods.WMFMSprepaid("rpt", jsGetReportDataOnSuccess, null);
    }
</script>