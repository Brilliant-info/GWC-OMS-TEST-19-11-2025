<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/CRM.Master" AutoEventWireup="true"
    CodeBehind="InboxPOR.aspx.cs" Inherits="PowerOnRentwebapp.Inbox.InboxPOR" EnableEventValidation="false" %>


<%@ Register Src="../Approval/UC_Approval.ascx" TagName="UC_Approval" TagPrefix="uc5" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHolder_FormHeader" runat="server">
  <%--  <input type="button" value="submit" id="btn1" onclick="openwindowProductSearch()" />--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHolder_Form" runat="server">
    <style>
        .themeWmsPopup {
            position: fixed;
            top: 0px;
            left: 0px;
            width: 100%;
            height: 100%;
            z-index: 999999;
        }

        .themeWmsPopupContent {
            display: inline-block;
            position: relative;
            margin-left: auto;
            margin-right: auto;
            z-index: 2;
        }

        .themeWmsPopupBg {
            background-color: #000000;
            opacity: 0.5;
            position: fixed;
            top: 0px;
            left: 0px;
            width: 100%;
            height: 100%;
            z-index: 1;
        }

        .themeWmsPopupTitle {
            background: url(../images/texture_5.png), -moz-linear-gradient(top, #ffffff, #cccccc);
            background: url(../images/texture_5.png), -ms-linear-gradient(top, #ffffff, #cccccc);
            background: url(../images/texture_5.png), -o-linear-gradient(top, #ffffff, #cccccc);
            background: url(../images/texture_5.png), -webkit-gradient(linear, 0% 0%, 0% 100%, from(#ffffff), to(#cccccc));
            background: url(../images/texture_5.png), -webkit-linear-gradient(top, #ffffff, #cccccc);
            -pie-background: url(bg-image.png) no-repeat, linear-gradient(#ffffff, #cccccc);
            font-weight: bold;
            color: var(--wms-color-main-text);
            font-size: 14px;
            text-align: center;
            padding: 10px;
        }

        #btnCloseShortcutPopup {
            float: right;
            margin-top: 3px;
            cursor: pointer;
            display: inline-block !important;
            font-family: Arial;
            font-size: 12px;
            position: relative;
            margin-right: 5px;
            top: -3px;
        }

            #btnCloseShortcutPopup i {
                position: static;
                font-size: 20px;
            }

        .themeWmsPopupContentBody {
            background-color: #ffffff;
            border: solid 1px #ffffff;
            font-size: 12px;
            padding: 20px;
        }
    </style>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <%--<link href="../CSS/basic.css" rel="stylesheet" type="text/css" />
    <script src="../js/basic.js" type="text/javascript"></script>
    <script src="../js/jquery.js" type="text/javascript"></script>
    <script src="../js/jquery.simplemodal.js" type="text/javascript"></script>--%>
    <div id="divLeft" class="leftDiv_open1">
        <div id="divLeftChild" class="expand-image" onclick="expandLeftdiv()">
        </div>
        <div id="divLeftData" style="width: 100%;">
            <a class="aparent">
                <asp:Label ID="lblInbox" runat="server" Text="Inbox"></asp:Label></a>
            <br />
            <a id="at1" class="achild" onclick="LinkValueAssign(this, 'Todays Message');">-
               <%-- <asp:Label ID="lblTodaysMsg" runat="server" Text="- System Generated
                Message"></asp:Label>--%>
                <asp:Label ID="lblSystemGeneratedMessage" runat="server" Text="- System Generated
                Message"></asp:Label>
            </a>
            <br />
            <a id="at2" class="achild" onclick="LinkValueAssign(this, 'Yesterdays Message');">-
               <%-- <asp:Label ID="lblYestardayMsg" runat="server" Text="-
                Correspondance Message"></asp:Label>--%>
                <asp:Label ID="lblCorrespondanceMessage" runat="server" Text="-
                Correspondance Message"></asp:Label>
            </a>
            <br />
            <a id="at3" class="achild" onclick="LinkValueAssign(this, 'All Messages');">-
                <asp:Label ID="lblAllMsg" runat="server" Text="- All Messages"></asp:Label></a>
            <br />
            <a id="at4" class="achild" onclick="LinkValueAssign(this, 'Archive Messages');">-
                <asp:Label ID="lblArchiveMsg" runat="server" Text="- Archive
                Messages"></asp:Label></a>
            <br />
            <%-- 
            <a id="at5" class="achild" onclick="LinkValueAssign(this, 'TK');">- Unread Messages</a>--%>
        </div>
        <input id="hndLinkValue" type="hidden" value="" runat="server" clientidmode="Static" />
    </div>
    <div id="divMainright" style="width: 84%; float: right; overflow: auto;">
        <center>
            <%-- <div>--%>
            <table class="gridFrame" width="100%">
                <tr>
                    <td>
                        <table style="width: 100%">
                            <tr>
                                <td>
                                    <a style="color: white; font-size: 15px; font-weight: bold;">
                                        <asp:Label ID="lblInbox1" runat="server" Text="Inbox"></asp:Label></a>
                                </td>
                                <td>
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="text-align: right;">
                                                <input type="button" id="btnArchive" runat="server" onclick="SetToArchive();" value="Archive" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <obout:Grid ID="GVInbox" runat="server" AllowAddingRecords="false" AutoGenerateColumns="false" AllowFiltering="true"
                            AllowGrouping="true" CallbackMode="true" AllowRecordSelection="true" AllowColumnResizing="true"
                            AllowMultiRecordSelection="true" AllowColumnReordering="true"
                            OnRebind="GVInbox_OnRebind" Width="100%" PageSize="10">
                            <Columns>
                                <obout:Column DataField="Id" HeaderText="View" Width="4%" Align="center" HeaderAlign="center">
                                    <TemplateSettings TemplateId="TemplateEdit" />
                                </obout:Column>
                                <obout:Column DataField="OrderNo" HeaderText="Request No." Width="8%" Align="center"
                                    HeaderAlign="center">
                                </obout:Column>
                                <obout:Column DataField="Orderdate" HeaderText="Requested Date" Width="8%" DataFormatString="{0:dd-MMM-yy}">
                                </obout:Column>
                                <obout:Column DataField="StatusName" HeaderText="Status" Wrap="true" Width="10%">
                                </obout:Column>
                                <obout:Column DataField="MessageFromName" HeaderText="Message From" Width="8%">
                                    <TemplateSettings TemplateId="msgFrm" />
                                </obout:Column>
                                <obout:Column DataField="MessageTitle" HeaderText="Message Title" Width="8%">
                                </obout:Column>
                            </Columns>
                            <Templates>
                                <obout:GridTemplate runat="server" ID="msgFrm">
                                    <Template>
                                        <%# (Container.DataItem["MessageFromName"].ToString() == "" ? "System Generated" : Container.DataItem["MessageFromName"].ToString())%>
                                    </Template>
                                </obout:GridTemplate>
                                <obout:GridTemplate runat="server" ID="TemplateEdit">
                                    <Template>
                                        <asp:ImageButton ID="imgBtnView" runat="server" ImageUrl="../App_Themes/Blue/img/Search16.png"
                                            OnClick="imgBtnView_OnClick" OnClientClick="AddCorrespondanceVW(this);return false;" CausesValidation="false"
                                            ToolTip='<%# (Container.Value) %>' data-containerId="<%# (Container.Value) %>" />
                                    </Template>
                                </obout:GridTemplate>
                            </Templates>
                        </obout:Grid>
                    </td>
                </tr>
            </table>
            <%--</div>--%>
            <asp:HiddenField ID="hdnSelectedRec" runat="server" ClientIDMode="Static" />
        </center>
    </div>






    <style type="text/css">
        .details {
            color: Gray;
        }

        .UnRead {
            color: Black;
            font-weight: bold;
        }

        .Read {
            color: Black;
            font-weight: normal;
        }

        .UnRead:hover, Read:hover {
            cursor: pointer;
        }

        .aparent {
            font-weight: bold;
            margin: 2px 0px 2px 3px;
            color: Black;
            font-size: 17px;
        }

        .achild {
            font-weight: normal;
            margin: 3px 0px 2px 10px;
            word-wrap: break-word;
            font-size: 13px;
        }

        .achildselected {
            color: #000000;
            font-weight: bold;
            margin: 3px 0px 2px 10px;
            font-size: 14px;
        }

        .achild:hover {
            color: Black;
            cursor: pointer;
        }

        .expand-image {
            float: right;
            top: 0px;
            margin-right: -7px;
            margin-top: 15px;
            width: 7px;
            height: 23px;
            background: url('../App_Themes/Blue/img/sliderOpen.png') no-repeat;
        }

        .close-image {
            float: right;
            top: 0px;
            margin-right: -7px;
            margin-top: 15px;
            width: 7px;
            height: 23px;
            background: url('../App_Themes/Blue/img/sliderClose.png') no-repeat;
        }

            .expand-image:hover, .close-image:hover {
                cursor: pointer;
            }

        .leftDiv_open1 {
            position: relative;
            width: 15%;
            border-right: solid 2px silver;
            float: left;
        }
    </style>



    <%--<script src="//code.jquery.com/jquery-1.10.2.js"></script>--%>
    <script type="text/javascript">
        window.onload = function () {
            oboutGrid.prototype.restorePreviousSelectedRecord = function () {
                return;
            }
            oboutGrid.prototype.markRecordAsSelectedOld = oboutGrid.prototype.markRecordAsSelected;
            oboutGrid.prototype.markRecordAsSelected = function (row, param2, param3, param4, param5) {
                if (row.className != this.CSSRecordSelected) {
                    this.markRecordAsSelectedOld(row, param2, param3, param4, param5);

                } else {
                    var index = this.getRecordSelectionIndex(row);
                    if (index != -1) {
                        this.markRecordAsUnSelected(row, index);
                    }
                }
                SelectedRec();
            }
        }
        function SelectedRec() {
            var hdnSelectedRec = document.getElementById("hdnSelectedRec");
            hdnSelectedRec.value = "";

            for (var i = 0; i < GVInbox.PageSelectedRecords.length; i++) {
                var record = GVInbox.PageSelectedRecords[i];
                if (hdnSelectedRec.value != "") hdnSelectedRec.value += ',' + record.Id;
                if (hdnSelectedRec.value == "") hdnSelectedRec.value = record.Id;
            }
        }

        function SetToArchive() {
            var hdnSelectedRec = document.getElementById("hdnSelectedRec");
            //alert(hdnSelectedRec.value);
            if (hdnSelectedRec.value == "") {
                showAlert("Select At Leaset 1 Record", '', '#');
            }
            else {
                PageMethods.WMSetArchive(hdnSelectedRec.value, OnsuccessWMSetArchive, null);
            }
        }
        function OnsuccessWMSetArchive(result) {
            GVInbox.refresh();
        }
        function openwindowProductSearch() {
        window.open('../Product/ProductSearchWithSerial.aspx', null, 'height=700px, width=1000px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=300; screenY=50');
    }

        function AddCorrespondanceVW(viewObj) {
            var getObjId = $(viewObj).attr('data-containerId');
            // alert(getTitle);
            //  return false;
            window.open('../PowerOnRent/Correspondance.aspx?VW=' + getObjId, null, 'height=450px, width=825px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=300; screenY=50');
        }

        var expandcount = 1;
        function expandLeftdiv() {
            expandcount += 1;
            switch (expandcount) {
                case 1:
                    document.getElementById("divLeftData").style.display = "block";
                    document.getElementById("divLeft").style.width = "15%";
                    document.getElementById("divMainright").style.width = "84%";
                    break;
                case 2:
                    document.getElementById("divLeft").style.width = "20%";
                    document.getElementById("divMainright").style.width = "79%";
                    break;
                case 3:
                    document.getElementById("divLeft").style.width = "25%";
                    document.getElementById("divLeftChild").className = "close-image";
                    document.getElementById("divMainright").style.width = "74%";
                    break;
                case 4:
                    document.getElementById("divLeftData").style.display = "none";
                    document.getElementById("divLeft").style.width = "0%";
                    document.getElementById("divLeftChild").className = "expand-image";
                    document.getElementById("divMainright").style.width = "99%";
                    expandcount = 0;
                    break;
            }

        }
        var isFullScreen = false;
        var callcount = 0;

        window.onresize = chnageHeight1;
        chnageHeight1();
        function getPageDetails() {
            var scnWid, scnHei;
            if (self.innerHeight) // all except Explorer
            {
                scnWid = self.innerWidth;
                scnHei = self.innerHeight;
            }
            else if (document.documentElement && document.documentElement.clientHeight)
            // Explorer 6 Strict Mode
            {
                scnWid = document.documentElement.clientWidth;
                scnHei = document.documentElement.clientHeight;
            }
            else if (document.body) // other Explorers
            {
                scnWid = document.body.clientWidth;
                scnHei = document.body.clientHeight;
            }

            document.getElementById('divMainright').style.height = ((scnHei / 100) * 72).toString() + 'px';
            document.getElementById('divLeftData').style.height = ((scnHei / 100) * 72).toString() + 'px';

            // document.getElementById("divIFrame").style.height = ((scnHei / 100) * 92).toString() + 'px';
        }

        function chnageHeight1() {
            getPageDetails();
        }

        function LinkValueAssign(invoker, linkValue) {
            for (var i = 1; i <= 4; i++) {
                document.getElementById('at' + i.toString()).className = 'achild';
            }
            document.getElementById(invoker.id).className = 'achildselected';
            var v = document.getElementById('hndLinkValue');
            v.value = linkValue;
            GVInbox.refresh();
        }



        function selectedRecForView() {


            var hdnApprovalDetailIDs = document.getElementById("hdnApprovalDetailIDs");

            hdnApprovalDetailIDs.value = "";


            if (GVInbox.SelectedRecords.length > 0) {

                for (var i = 0; i < GVInbox.SelectedRecords.length; i++) {
                    var record = GVInbox.SelectedRecords[i];
                    if (hdnApprovalDetailIDs.value != "") hdnApprovalDetailIDs.value += ',' + record.TransID;
                    if (hdnApprovalDetailIDs.value == "") hdnApprovalDetailIDs.value = record.TransID;
                    break;
                }
            }
        }

        function closeSecurityQuestionPopup() {
            document.getElementById('popupSecurityQuestion').style.display = 'none';
        }
        function clearSecurityPopup() {

        }

    </script>
    <div class="themeWmsPopup" id="popupSecurityQuestion" clientidmode="Static" runat="server" style="display: none;">
        <div class="themeWmsPopupContent" style="margin-left: 350px; margin-top: 100px; width: 960px; height: 860px;">

            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="securityquestion"
                DisplayMode="BulletList" ShowMessageBox="true" ShowSummary="false" />
            <table class="gridFrame" style="margin: 3px; width: 100%;">
                <tr>
                    <td>
                        <span id="headerText">
                            <asp:Label ID="lblheader" CssClass="headerText" runat="server" Text="Security Questions"></asp:Label></span>
                    </td>
                    <td style="text-align:right">
                        <input type="button" value="Save" id="btnSaveChangePassword" runat="server" onclick="jsSaveData()" />
                                    <input type="button" value="Clear" id="btnClearChangePassword" runat="server" onclick="jsClearData()" />
                        
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table class="tableForm" style="background-color: White; width: 100%">
                            <tr>
                                <td style="text-align: left">
                                    <asp:Label ID="lblquestion1" runat="server" Text="Question1"></asp:Label>
                                </td>
                                <td style="text-align: left;">
                                    <asp:Label ID="lblanswer" runat="server" Text="Answer"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width:40%">
                                    <asp:DropDownList ID="ddlquestion1" Width="100%" runat="server" DataTextField="Value" DataValueField="ID">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="ddlquestion1"
                                        ErrorMessage="Select Security Question1" InitialValue="0" Display="None"
                                        ValidationGroup="securityquestion"></asp:RequiredFieldValidator>

                                </td>
                                <td style="text-align: left;width:60%">

                                    <asp:TextBox ID="txtanswer1" Width="100%" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtanswer1"
                                        ErrorMessage="Please Fill Answer Question." Display="None"
                                        ValidationGroup="securityquestion"></asp:RequiredFieldValidator>

                                </td>
                            </tr>


                            <tr>
                                <td style="text-align: left">
                                    <asp:Label ID="Label1" runat="server" Text="Question2"></asp:Label>
                                </td>
                                <td style="text-align: left;">
                                    <asp:Label ID="Label2" runat="server" Text="Answer"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:DropDownList ID="ddlquestion2" Width="100%" runat="server" DataTextField="Value" DataValueField="ID">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlquestion2"
                                        ErrorMessage="Select Security Question2" InitialValue="0" Display="None"
                                        ValidationGroup="securityquestion"></asp:RequiredFieldValidator>

                                </td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtanswer2" Width="100%" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtanswer2"
                                        ErrorMessage="Please Fill Answer Question." Display="None"
                                        ValidationGroup="securityquestion"></asp:RequiredFieldValidator>

                                </td>
                            </tr>

                            <tr>
                                <td style="text-align: left">
                                    <asp:Label ID="Label3" runat="server" Text="Question3"></asp:Label>
                                </td>
                                <td style="text-align: left;">
                                    <asp:Label ID="Label4" runat="server" Text="Answer"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <asp:DropDownList ID="ddlquestion3" Width="100%" runat="server" DataTextField="Value" DataValueField="ID">
                                    </asp:DropDownList>

                                </td>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtanswer3" Width="100%" runat="server"></asp:TextBox>

                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align:left">
                                    <br />  <br />
                                    </td>
                            </tr>
                            <tr>
                                  <td colspan="2" style="text-align:left">
                                    <asp:Label ID="Label6" runat="server" 
                                        Text="Note :- If you want to change password then click on setting icon and select option Change Password. First setup security questions answers.">
                                    </asp:Label>
                                      </td>
                            </tr>

                        </table>
                    </td>
                </tr>
                

            </table>

            

        </div>
        <div class="themeWmsPopupBg">
        </div>
    </div>

    <script type="text/javascript">

        function jsClearData() {
            var question1 = document.getElementById("<%= ddlquestion1.ClientID%>");
            var question2 = document.getElementById("<%= ddlquestion2.ClientID%>");
            var question3 = document.getElementById("<%= ddlquestion3.ClientID%>");
            var txtanswer1 = document.getElementById("<%= txtanswer1.ClientID%>");
            var txtanswer2 = document.getElementById("<%= txtanswer2.ClientID%>");
            var txtanswer3 = document.getElementById("<%= txtanswer3.ClientID%>");

            question1.selectedIndex = 0;
            question2.selectedIndex = 0;
            question3.selectedIndex = 0;
            //question1.setAttribute(0, "-Select-");
            //question2.setAttribute(0, "-Select-");
            //question3.setAttribute(0, "-Select-");
            txtanswer1.value = "";
            txtanswer2.value = "";
            txtanswer3.value = "";
        }

        function jsSaveData() {
            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate();
            }
            if (Page_IsValid) {

                var s1 = 0, s2 = 0, s3 = 0, sum = 0;
                var question1 = document.getElementById("<%= ddlquestion1.ClientID%>");
                var q1 = question1.value;
                var question2 = document.getElementById("<%= ddlquestion2.ClientID%>");
                var q2 = question2.value;
                var question3 = document.getElementById("<%= ddlquestion3.ClientID%>");
                var q3 = question3.value;
                var txtanswer1 = document.getElementById("<%= txtanswer1.ClientID%>");
                var txtanswer2 = document.getElementById("<%= txtanswer2.ClientID%>");
                var txtanswer3 = document.getElementById("<%= txtanswer3.ClientID%>");

                //if (q3 != "0") {
                //    if (txtanswer3.value == "") {
                //        showAlert("Please Fill Answer Question.", "Error", '#');
                //    }
                //    else {
                //        s1 = 1;
                //    }
                //}
                //else { s1 = 0; }

                if (q1 == "0" || txtanswer1.value == "") { s1 = 0; } else { s1 = 1; }
                if (q2 == "0" || txtanswer2.value == "") { s2 = 0; } else { s2 = 1; }
                if (q3 == "0" || txtanswer3.value == "") { s3 = 0; } else { s3 = 1; }

                //sum = s1 + s2 + s3;
                //if (sum >= 2) {
                if (q3 != "0") {
                    if (txtanswer3.value == "") {
                        alert('Please Fill Answer Question.');
                    }
                    else if (q1 == q2 || q1 == q3 || q2 == q3) {
                        // showAlert("Please select unique security question !!!", "Error", '#');
                        alert('Please select unique security question.');
                    }
                    else {
                        var obj1 = new Object();
                        obj1.question1 = q1;
                        obj1.question2 = q2;
                        obj1.question3 = q3;
                        obj1.txtanswer1 = txtanswer1.value;
                        obj1.txtanswer2 = txtanswer2.value;
                        obj1.txtanswer3 = txtanswer3.value;

                        PageMethods.WMSaveSecurityQuestion(obj1, OnSuccessWMSaveSecurityQuestion, null);
                    }
                }
                else {
                    if (q1 == q2 || q1 == q3 || q2 == q3) {
                        // showAlert("Please select unique security question !!!", "Error", '#');
                        alert('Please select unique security question.');
                    }
                    else {
                        var obj1 = new Object();
                        obj1.question1 = q1;
                        obj1.question2 = q2;
                        obj1.question3 = q3;
                        obj1.txtanswer1 = txtanswer1.value;
                        obj1.txtanswer2 = txtanswer2.value;
                        obj1.txtanswer3 = txtanswer3.value;

                        PageMethods.WMSaveSecurityQuestion(obj1, OnSuccessWMSaveSecurityQuestion, null);
                    }
                }
                //else {
                //    // showAlert("Please select at least 2 unique security question.", "Error", '#');
                //    alert('Please select at least 2 unique security question.');
                //}
            }

        }


        function OnSuccessWMSaveSecurityQuestion(result) {
            if (result > 0) {
                // showAlert(" Record save scuessfully.", "info", '../Inbox/InboxPOR.aspx');
                alert('Record save scuessfully.');
                //divsecurity.style.display = "block"; // Visible
                document.getElementById("<%= popupSecurityQuestion.ClientID%>").style.display = "none"; // Hidden
            }
            else {
                // showAlert("Error occurred.", "Error", "#");
                alert('Error occurred.');
            }
        }

    </script>

</asp:Content>
