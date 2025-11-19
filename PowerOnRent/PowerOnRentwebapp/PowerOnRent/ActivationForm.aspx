<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ActivationForm.aspx.cs" Inherits="PowerOnRentwebapp.PowerOnRent.Dispatch1" Theme="Blue" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head  runat="server">
    <title></title>
</head>
    <form id="form1" runat="server" style="background-color: White;">
    <asp:ScriptManager ID="ScriptManager12" runat="server" EnablePageMethods="true" />
        <div class="divDetailExpand" id="divApprovalDetail" runat="server" clientidmode="Static">
    <center>

        <div id="divLoading" style=" display: none; top: 10px; left: 50%; height: 30%; width: 20%;"
            class="modal" runat="server" clientidmode="Static"> <%----%>
            <center>
             <img src="../App_Themes/Blue/img/ajax-loader.gif" alt="" style="top: 0%; margin-top: 1%" />
                <br />
                <br />
                <b>Please Wait...</b>
            </center>
        </div>
        <br />   <br />
            <table class="tableForm">
                  <tr>
                      <td colspan="2" style="text-align:center">
                          <asp:Label ID="lblmsg"  runat="server" ></asp:Label> 
                      </td> 
            </tr>
           
            <tr>   <td style="text-align:right">
                    <br />
                     <input type="button" id="btnactivedispatch" runat="server" value="Activate"  onclick="jsActivate()"/>
                </td>
                
                <td style="width:50%;text-align:left">
                    <br />
                       <input type="button" id="btncanceldispatch" runat="server" value="Cancel"  onclick="jsActivateCancel()"/>
                </td>
                
            </tr>
        </table>
        <br />
        </center>
              </div>

         <script type="text/javascript">
             onload();
             function onload() {
                 document.getElementById("btnSaveApproval").style.visibility = "visible";
             }
             function getParameterByName(name) {
                 name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
                 var regexS = "[\\?&]" + name + "=([^&#]*)";
                 var regex = new RegExp(regexS);
                 var results = regex.exec(window.location.search);
                 if (results == null)
                     return "";
                 else
                     return decodeURIComponent(results[1].replace(/\+/g, " "));
             }

             /*Approval JS*/
             /*Approval checkbox Only one checked Approved or Rejected*/
             function OnlyOneCheckedA(chkA, chkR, chkRV) {
                 var findtd = document.getElementById('tdApprovalRemark');

                 if (document.getElementById(chkA).checked == true) {
                     findtd.innerHTML = "Remark / Reason : ";
                     document.getElementById('txtApprovalRemark').accessKey = "";
                     document.getElementById(chkR).checked = false;
                     document.getElementById(chkRV).checked = false;
                     lblRejected.className = "label_check c_off";
                 }
             }

             function OnlyOneCheckedR(chkA, chkR, chkRV) {
                 var findtd = document.getElementById('tdApprovalRemark');

                 if (document.getElementById(chkR).checked == true) {
                     findtd.innerHTML = "Remark / Reason * :";
                     document.getElementById('txtApprovalRemark').accessKey = "1";
                     document.getElementById(chkA).checked = false;
                     document.getElementById(chkRV).checked = false;
                     lblApproved.className = "label_check c_off";
                 }
             }

             function OnlyOneCheckedRV(chkA, chkR, chkRV) {
                 var findtd = document.getElementById('tdApprovalRemark');

                 if (document.getElementById(chkR).checked == true) {
                     findtd.innerHTML = "Remark / Reason * :";
                     document.getElementById('txtApprovalRemark').accessKey = "1";
                     document.getElementById(chkA).checked = false;
                     document.getElementById(chkR).checked = false;
                     lblApproved.className = "label_check c_off";
                 }
             }

             function jsActivate() {

                 document.getElementById("btnactivedispatch").style.visibility = "hidden";            
                 var OrdNo = getParameterByName("OrdNo").toString();
                 var Skucode = getParameterByName("Skucode").toString();
                 PageMethods.WMActivateSim(OrdNo, Skucode, jsSaveApprovalOnSuccess, jsSaveApprovalOnFail);
             }

             function jsActivateCancel() {

                 document.getElementById("btncanceldispatch").style.visibility = "hidden"; 
                 window.close('../PowerOnRent/Dispatch.aspx');
             }


           

             function jsSaveApprovalOnSuccess(result) {
                 if (result.toString().toLowerCase() == 'true')
                 {                   
                     LoadingOn(true);
                     window.opener.GridDispatchstatus.refresh();                                  
                     self.close();
                 }
                 else if (result.toString().toLowerCase() == 'truerev') {
                     self.close();
                 }
                 else if (result = 'Approved') {
                     showAlert("Already Approved", "error", "#");
                     self.close();
                 }
                 else {
                     showAlert("Some error occurred", "error", "#");
                 }
             }
             function jsSaveApprovalOnFail() { }

             function checkLength(sender, maxLength) {
                 if (sender.value.length > parseInt(maxLength)) {
                     sender.value = sender.value.substr(0, parseInt(maxLength));
                     showAlert("Character limit is exceed", 'error', '#');
                 }
             }

             function TextBox_KeyUp(sender, conunterspan, maxLength) {
                 checkLength(sender, maxLength);

                 var counterId = conunterspan;
                 if (counterId != "") document.getElementById(counterId).innerHTML = parseInt(maxLength) - sender.value.length;
             }

             var myTimer;
             function showAlert(msg, msytype, formpath) {
                 var newdiv = document.createElement('div');
                 newdiv.id = "divAlerts";
                 newdiv.className = "alertBox0";

                 var newdiv2 = document.createElement('div');
                 msytype = msytype.toString().toLowerCase();
                 switch (msytype) {
                     case "info":
                         newdiv2.style.borderTopColor = "green";
                         break;
                     case "error":
                         newdiv2.style.borderTopColor = "red";
                         break;
                     case "p": /*Process*/
                         newdiv2.style.borderTopColor = "navy";
                         break;
                     case "":
                         newdiv2.style.borderTopColor = "orange";
                         break;
                 }


                 var span1 = document.createElement('span');
                 span1.id = 'alertmsg';
                 newdiv2.appendChild(span1);
                 newdiv.appendChild(newdiv2);
                 //var strdiv = "<div id='divAlerts' class='alertBox0'><div><span id='alertmsg'></span></div></div>"
                 document.body.appendChild(newdiv);
                 document.getElementById('alertmsg').innerHTML = msg;
                 document.getElementById('divAlerts').className = "alertBox1";
                 myTimer = self.setInterval(function () { msgclock(formpath) }, 3500);
             }
             function msgclock(formpath) {
                 document.body.removeChild(document.getElementById("divAlerts"));
                 if (formpath != '#' && formpath != '') {
                     window.open(formpath, '_self', '');
                     LoadingOff();
                 }
                 clearInterval(myTimer);
             }

             /*Show Loading div*/
             function LoadingOn() {
                 document.getElementById("divLoading").style.display = "block";
                 var imgProcessing = document.getElementById("imgProcessing");
                 if (imgProcessing != null) { imgProcessing.style.display = ""; }
             }

             function LoadingOn(ShowWaitMsg) {
                 document.getElementById("divLoading").style.display = "block";
                 var imgProcessing = document.getElementById("imgProcessing");
                 if (imgProcessing != null) {
                     if (ShowWaitMsg == true) { imgProcessing.style.display = ""; }
                     if (ShowWaitMsg == false) { imgProcessing.style.display = "none"; }
                 }
             }

             function LoadingText(val) {
                 if (val == true) { document.getElementById("pupUpLoading").style.display = ""; }
                 else { document.getElementById("pupUpLoading").style.display = "none"; }
             }

             function LoadingOff() {
                 if (document.getElementById("divLoading") != null) {
                     document.getElementById("divLoading").style.display = "none";
                 }
                 var imgProcessing = document.getElementById("imgProcessing");
                 if (imgProcessing != null) { imgProcessing.style.display = "none"; }
             }
             /*End Show Loading div*/

    </script>
    <style type="text/css">
    .has-js .label_check, .has-js .label_radio
    {
        padding-left: 25px;
        padding-bottom: 10px;
    }
    .has-js .label_radio
    {
        background: url(../App_Themes/Blue/img/radio-off.png) no-repeat;
    }
    .has-js .label_check
    {
        background: url("../App_Themes/Blue/img/check-off.png") no-repeat;
    }
    .has-js label.c_on
    {
        background: url("../App_Themes/Blue/img/check-on.png") no-repeat;
    }
    .has-js label.r_on
    {
        background: url(../App_Themes/Blue/img/radio-on.png) no-repeat;
    }
    .has-js .label_check input, .has-js .label_radio input
    {
        position: absolute;
        left: -9999px;
    }
</style>
    </form>
    </html>
