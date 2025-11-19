<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VirtualProductReport.aspx.cs" Inherits="PowerOnRentwebapp.PowerOnRent.VirtualProductReport" %>


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
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
            <div id="divLoading" style="display: none; top: 10px; left: 50%; height: 30%; width: 20%;"
                class="modal" runat="server" clientidmode="Static">
                <%----%>
                <center>
                    <img src="../App_Themes/Blue/img/ajax-loader.gif" alt="" style="top: 0%; margin-top: 1%" />
                    <br />
                    <br />
                    <b>Please Wait...</b>
                </center>
            </div>

            <table id="tblRptMenu">
                <tr>
                    <td rowspan="2" id="imgadt1" runat="server">
                        <div id="prepaid"  runat="server">
                        <a>
                            <%--<img alt="" src="Img/Report4.png" />--%>
                             <img alt="" src="../POR/Img/prepaid_form.png" height="60%" width="60%" onclick="jsGetUserVirtualReport()"  />
                           
                        </a>
                            </div>
                    </td>
                    <td style="text-align: left" id="imgadt2" runat="server">
                        <input type="button" id="btnshowreport" runat="server" visible="false"  value="Prepaid Customer Form" onclick="jsGetUserVirtualReport()" />
                        <asp:HiddenField ID="hndprepaid" runat="server" ClientIDMode="Static" Value="prepaid" />
                    </td>
                    <td rowspan="2">
                          <div id="Broadband"  runat="server">
                        <a>
                            <%--<img alt="" src="Img/Report4.png" />--%>
                             <img alt="" src="../POR/Img/Postpaid_Icon.png" id="imgbroadband" height="60%" width="60%" onclick="jsPostPaidReport()"  />
                        </a>
                              </div>
                    </td>
                    <td style="text-align: left">

                        <input type="button" id="Button1" runat="server" visible="false" value="Broadband Customer Form" onclick="jsPostPaidReport()" />
                        <asp:HiddenField ID="hndpostpaid" runat="server" ClientIDMode="Static" Value="Broadband" />
                         <asp:HiddenField ID="hndcust" runat="server" ClientIDMode="Static" Value="CustomerSim" />
                    </td>
                    <%--<td rowspan="2">--%>
                        <%--<a>--%>
                            <%--<img alt="" src="Img/Report4.png" />--%>
                             <%--<img alt="" src="Img/Broadband_form.png" />--%>
                        <%--</a>--%>
                    <%--</td>--%>
                <%--    <td style="text-align: left">
                        <input type="button" id="Button2" runat="server" value="Sim Customer Form" onclick="jsCustomerSimReport()" />
                       
                    </td>--%>

                     <td rowspan="2">
                          <div id="FMS"  runat="server">
                        <a>
                            <%--<img alt="" src="Img/Report4.png" />--%>
                             <img alt="" src="../POR/Img/CFA.png" id="imgfms" height="37%" width="37%" onclick="jsFMSReport()"  />
                        </a>
                              </div>
                    </td>
                    <td style="text-align: left">

                        <input type="button" id="btnfms" runat="server" visible="false" value="FMS Customer Form" onclick="jsFMSReport()" />
                        <asp:HiddenField ID="hdnfms" runat="server" ClientIDMode="Static" Value="FMS" />
                         
                    </td>


                    <td rowspan="2">
                          <div id="Delivery"  runat="server">
                        <a>
                            <img alt="" src="../POR/Img/Delivery_form.png" height="60%" width="60%" onclick="jsDeliveryFormReport()" />
                        </a>
                              </div>
                    </td>
                    <td style="text-align: left">

                        <input type="button" id="Button3" runat="server" visible="false" value="Delivery Form" onclick="jsDeliveryFormReport()" />
                        <asp:HiddenField ID="hnddelivery" runat="server" ClientIDMode="Static" Value="DeliveryForm" />

                    </td>
                     <%-- <td style="text-align: left">
                           <input type="button" id="BtnAllocate" runat="server" value="Allocate Driver" onclick="AllocateDriver()" />
                      </td>--%>
                </tr>
                <tr>
                    <td>
                      
                    </td>
                </tr>
            </table>
           
        </div>
       
          <br />
          <br />
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

          <asp:HiddenField ID="hdnparameter" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnskucode" runat="server" ClientIDMode="Static" />
        
    </form>
</body>
</html>




<script type="text/javascript">
    //Prepaid
    function jsGetUserVirtualReport() {
        var hdnProductSelectedRec = document.getElementById("hndprepaid");
      <%--  var hdnskucode = document.getElementById("<%=hdnskucode.ClientID %>")--%>

        if (hdnProductSelectedRec.value != "" && hdnProductSelectedRec.value == "prepaid") {
            PageMethods.WMVirtualReport(document.getElementById("<%=hndprepaid.ClientID%>").value, jsGetReportDataOnSuccess, null)
        }
    }

    //Postpaid
    function jsPostPaidReport() {
        var hdnProductSelectedRec = document.getElementById("hndpostpaid");
         // <%-- var hdnskucode = document.getElementById("<%=hdnskucode.ClientID %>")--%>
        if (hdnProductSelectedRec.value != "" && hdnProductSelectedRec.value == "Broadband") {
            PageMethods.WMPostpaidReport(document.getElementById("<%=hndpostpaid.ClientID%>").value, jsGetReportDataOnSuccess, null)
        }
    }

    //Customer
    function jsCustomerSimReport() {
        var CustomerSim = document.getElementById("hndcust");
        if (CustomerSim.value != "" && CustomerSim.value == "CustomerSim") {
            PageMethods.WMVirtualReport(document.getElementById("<%=hndcust.ClientID%>").value, jsGetReportDataOnSuccess, null)
        }
    }

    //Delivery
    function jsDeliveryFormReport() {
        var hnddelivery = document.getElementById("hnddelivery");
        if (hnddelivery.value != "" && hnddelivery.value == "DeliveryForm") {
            PageMethods.WMDeliveryReport(document.getElementById("<%=hnddelivery.ClientID%>").value, jsGetReportDataOnSuccess, null)
        }
    }

    //FMS
     function jsFMSReport() {
        var hdnfms = document.getElementById("hdnfms");
        if (hdnfms.value != "" && hdnfms.value == "FMS") {
            PageMethods.WMFMSReport(document.getElementById("<%=hdnfms.ClientID%>").value, jsGetReportDataOnSuccess, null)
        }
    }


    function AllocateDriver() {
       var hdnparameter = document.getElementById("<%=hdnparameter.ClientID%>").value      
        window.open('../PowerOnRent/AllocateDriver.aspx?OID=' + hdnparameter + '&ST=DispatchstatusGrid&APL=0', null, 'height=550, width=700,status= 0, resizable= 1, scrollbars=0, toolbar=0,location=center,menubar=0, screenX=300; screenY=50');

    }


    function jsGetReportDataOnSuccess(result) {
        if (parseInt(result) > 0) {

            ShowReportOn();
        }
        else {
            showAlert("Data Not Available... ", "Error", "#");
            LoadingOff();
            ShowReportOff();
        }
    }
    function callMethod_Failure(errors, userContext, methodName) {
        alert(errors.get_Message());
    }
    function ShowReportOn() {
        // document.getElementById("iframeOMSRpt").src = "../PowerOnRent/VirtualProductReport.aspx";   // iframeOMSRpt   ReportViewer.aspx
        document.getElementById("iframeOMSRpt").src = "../POR/Reports/NewReportViewer.aspx";
        divPopUp.className = "divDetailExpandPopUpOn";
    }

    function ShowReportOff() {
        divPopUp.className = "divDetailExpandPopUpOff";
        LoadingOff();
    }
    function LoadingOn() {
        document.getElementById("divLoading").style.display = "block";
        var imgProcessing = document.getElementById("imgProcessing");
        if (imgProcessing != null) { imgProcessing.style.display = ""; }
    }
    function LoadingOff() {
        if (document.getElementById("divLoading") != null) {
            document.getElementById("divLoading").style.display = "none";
        }
        var imgProcessing = document.getElementById("imgProcessing");
        if (imgProcessing != null) { imgProcessing.style.display = "none"; }
    }
    function CloseShowReport() {
        LoadingOff();
        divPopUp.className = "divDetailExpandPopUpOff";
    }

</script>

