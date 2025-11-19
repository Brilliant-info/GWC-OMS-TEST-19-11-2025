<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SecirityQAForgotPassword.aspx.cs"
    Inherits="PowerOnRentwebapp.Login.SecirityQAForgotPassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<script src="../PopupMessages/Scripts/message.js" type="text/javascript"></script>
<link href="../App_Themes/Login.css" rel="stylesheet" type="text/css" />
<script src="../App_Themes/PIE.js" type="text/javascript"></script>
<script src="../App_Themes/PIE_uncompressed.js" type="text/javascript"></script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="ForgotPwHead1" runat="server">
    <title>GWC</title>
    <style>
        body {
            margin: 0px;
            padding: 0px;
        }

        .divLoginHolder table {
            width: 300px;
        }

        .divLoginHolder {
            width: 380px;
            border: solid 1px #ffffff;
            border-radius: 13px;
            box-shadow: 0px 0px 10px #636363;
            font-size: 14px;
            font-family: Arial;
            margin-bottom: 30px;
            overflow: hidden;
        }

            .divLoginHolder req {
                color: #4088BF;
                font-weight: bold;
            }

            .divLoginHolder input, .divLoginHolder select {
                font-size: 14px;
                font-family: Arial;
                padding: 10px 10px;
                border-radius: 13px;
                border: solid 1px #cccccc;
                width: 300px !important;
                color: #636363;
            }

            .divLoginHolder select {
                width: 324px !important;
            }

            .divLoginHolder input[type="button"], .divLoginHolder input[type="submit"], .divLoginHolder input[type="reset"] {
                width: 321px !important;
                color: #ffffff;
            }

        .divLoginBoxHeader {
            color: #ffffff;
            font-weight: bold;
            background-color: #4088BF;
            border: solid 1px #4088BF;
            padding: 10px 20px;
            border-radius: 13px;
            font-size: 24px;
            text-align: center;
            border-bottom-left-radius: 0px;
            border-bottom-right-radius: 0px;
        }

        .divLoginBoxContent {
            padding: 10px 20px 0px;
            background-image: url(../company/background/login-box-bg.png);
            padding-top: 0px;
        }

        .pnlLoginBg {
            background-color: #000000;
            position: fixed;
            top: 0px;
            left: 0px;
            width: 100%;
            height: 100%;
        }

            .pnlLoginBg img {
                opacity: 0.5;
            }

        .pnlLoginContent {
            position: absolute;
            z-index: 999;
            display: table;
            width: 100%;
            height: 100%;
        }

        .pnlLoginContentSub {
            display: table-cell;
            vertical-align: middle;
            text-align: center;
        }

        .loginFooter {
            background-color: #000000;
            position: absolute;
            bottom: 0px;
            left: 0px;
            opacity: 0.8 !important;
            width: 100%;
        }

            .loginFooter table:first-child {
                width: 90%;
            }

            .loginFooter td, .loginFooter div, .loginFooter a {
                color: #ffffff;
            }

        .tlbPoweredBy {
            width: auto !important;
        }

        .spnFailureMsg {
            margin-top: 10px;
            background-color: #990000;
            border: solid 1px #990000;
            border-radius: 7px;
            padding: 10px;
            display: inline-block;
            color: #ffffff;
            font-weight: bold;
            font-size: 12px;
        }
    </style>
</head>
<body>
    <div class="pnlLoginBg">
        <asp:Image ID="Image1" Width="100%" Height="100%" runat="server" ImageUrl="~/Company/Background/login-bg.jpg" />
    </div>
    <div class="pnlLoginContent">
        <div class="pnlLoginContentSub">
            <form runat="server">
                <asp:ScriptManager ID="ToolkitScriptManager1" runat="server" EnablePageMethods="true"
                    EnablePartialRendering="true">
                </asp:ScriptManager>
                <%-- <div style="position: relative; left: -20px; top: -7px;">
                <img src="../App_Themes/Blue/img/Background.jpg" />
            </div>--%>
                <div runat="server" clientidmode="Static" id="divsecurity">
                    <center>
                        <table style="margin: 0px 0 0 0;" border="0">

                            <tr>
                                <%-- <td>
                                <center>
                                    <table>
                                        <tr>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center; vertical-align: bottom;">
                                                <asp:Image ID="ElegantLogo1" runat="server" ImageUrl="~/MasterPage/Logo/GWCBrandNewLogo.png" />
                                            </td>
                                        </tr>
                                    </table>
                                </center>
                            </td>
                            <td>
                                <img src="../MasterPage/Logo/Partitionimg.png" />
                            </td>--%>
                                <td>
                                    <div class="divLoginHolder">
                                        <div class="divLoginBoxContent">
                                            <asp:ValidationSummary ID="validationsummary1" runat="server" ShowMessageBox="true"
                                                ShowSummary="false" DisplayMode="bulletlist" ValidationGroup="PasswordRecovery1" />
                                            <table class="tableForm" cellspacing="5" border="0">
                                                <tr>
                                                    <td align="center">
                                                        <asp:Image ID="ElegantLogo" runat="server" ImageUrl="~/MasterPage/Logo/GWCBrandNewLogo.png"
                                                            Style="position: relative; left: 5px; width: 150px; margin-bottom: 10px;" />
                                                    </td>
                                                </tr>
                                                <%-- <tr>
                                                        <td>
                                                            <req> User Name :</req>
                                                        </td>
                                                    </tr>--%>
                                                <tr>
                                                    <td style="text-align: center; font-size: 25px; padding: 0 0 0 15px;">Security Questions
                                                    </td>
                                                </tr>


                                               <%-- <tr>
                                                    <td style="text-align: left;">
                                                        <asp:Label ID="lblquestion1" runat="server" Text="Question1:"></asp:Label>
                                                    </td>
                                                </tr>--%>
                                                <tr>
                                                    <td style="text-align: left;">
                                                        <asp:DropDownList ID="ddlquestion1" Width="200px" runat="server"></asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" InitialValue="0" runat="server" ErrorMessage="Select Security Question1"
                                                            ControlToValidate="ddlquestion1" ValidationGroup="PasswordRecovery1" Display="None">
                                                        </asp:RequiredFieldValidator>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left;">

                                                        <asp:TextBox ID="txtanswer1" Width="200px" runat="server" ClientIDMode="Static"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Answer Question"
                                                            ControlToValidate="txtanswer1" ValidationGroup="PasswordRecovery1" Display="None">
                                                        </asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left;">
                                                        <%--  <asp:Button ID="SubmitButton" runat="server"  Text="Submit"  ValidationGroup="PasswordRecovery1" />--%>
                                                        <input type="button" value="Submit" id="btnsavequeans" runat="server" onclick="SaveQuestionAnswer()" />
                                                    </td>
                                                </tr>
                                                <%--<tr>
                                                    <td style="font-size: 12px; text-align: center;">Enter your User Name to receive your password
                                                    </td>
                                                </tr>--%>
                                            </table>

                                        </div>
                                    </div>
                                </td>
                            </tr>

                        </table>
                    </center>
                </div>


                <div runat="server" clientidmode="Static" id="divuserid">
                    <center>
                        <table style="margin: 0px 0 0 0;" border="0">

                            <tr>

                                <td>
                                    <div class="divLoginHolder">
                                        <div class="divLoginBoxContent">
                                            <asp:ValidationSummary ID="validationsummary2" runat="server" ShowMessageBox="true"
                                                ShowSummary="false" DisplayMode="bulletlist" ValidationGroup="PasswordRecovery2" />
                                            <table class="tableForm" cellspacing="5" border="0">
                                                <tr>
                                                    <td align="center">
                                                        <asp:Image ID="Image2" runat="server" ImageUrl="~/MasterPage/Logo/GWCBrandNewLogo.png"
                                                            Style="position: relative; left: 5px; width: 150px; margin-bottom: 10px;" />
                                                    </td>
                                                </tr>
                                                <%-- <tr>
                                                        <td>
                                                            <req> User Name :</req>
                                                        </td>
                                                    </tr>--%>
                                                <tr>
                                                    <td style="text-align: center; font-size: 25px; padding: 0 0 0 15px;">Security Questions
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td style="text-align: left;">
                                                        <asp:TextBox ID="UserName" runat="server" Width="200px" CssClass="inputElement" Placeholder="User Name"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                                            ErrorMessage="User Name is required." ToolTip="User Name is required." Display="None"
                                                            ValidationGroup="PasswordRecovery2"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td style="text-align: left;">
                                                        <%--  <asp:Button ID="SubmitButton" runat="server"  Text="Submit"  ValidationGroup="PasswordRecovery1" />--%>
                                                        <input type="button" value="Submit" id="btngetname" runat="server" onclick="GetUsername()" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 12px; text-align: center;">Enter your User Name to show your Security Questions
                                                    </td>
                                                </tr>

                                            </table>

                                        </div>
                                    </div>
                                </td>
                            </tr>

                        </table>
                    </center>
                </div>

             
                <asp:HiddenField ID="txtUserName" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnuid" runat="server" ClientIDMode="Static" />
            </form>
        </div>
    </div>

    <script type="text/javascript">

        function SaveQuestionAnswer() {

            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate();
            }
            if (Page_IsValid) {
                var s1 = 0, s2 = 0, s3 = 0, sum = 0;
                var question1 = document.getElementById("<%= ddlquestion1.ClientID%>");
                var q1 = question1.value;               
                var txtanswer1 = document.getElementById("<%= txtanswer1.ClientID%>");              
                var UserName = document.getElementById("<%= hdnuid.ClientID%>");
                document.getElementById('txtUserName').value = UserName.value;
                var obj1 = new Object();
                obj1.question1 = q1;              
                obj1.txtanswer1 = txtanswer1.value;              
                obj1.UserName = UserName.value;
                PageMethods.WMChkSecurityQuestion(obj1, OnSuccessWMChkSecurityQuestion, null);
            }
        }

        function OnSuccessWMChkSecurityQuestion(result) {
            if (result == "match") {
                var UserName = document.getElementById("<%= hdnuid.ClientID%>");
                PageMethods.WMGetUsernameWMEncryptstring(UserName.value, OnSuccessencryptstring, null);               
            }
            else if (result == "100") {
                alert('Please select unique security question.');
            }
            else if (result == "200") {
                alert('Error occurred.');
            }
            else if (result == "notmatch") {
                alert('Answer not match.');
            }
        }

        function OnSuccessencryptstring(result) {
             window.location.href = "../Login/ForgotPassword.aspx?uname=" + result;
        }

        function GetUsername() {
            //if (typeof (Page_ClientValidate) == 'function') {
            //    Page_ClientValidate();
            //}
            //if (Page_IsValid) {

            var UserName = document.getElementById("<%= UserName.ClientID%>");
            document.getElementById("<%= hdnuid.ClientID%>").value = UserName.value;
            if (UserName.value == "") {
                 alert('Please enter user name.');
            } else {
                PageMethods.WMGetUsername(UserName.value, OnSuccessWMGetUsername, null);
            }
            //  }
        }

        function OnSuccessWMGetUsername(result) {
            if (result == "usernofound") {
                alert('Please enter correct user name.');
            }
            else if (result == "nofound") {
                alert('Please contact to your admin for get password.');
            }
            else if (result == "pass") {
                var divsecurity = document.getElementById("<%= divsecurity.ClientID%>");
                 var divuserid = document.getElementById("<%= divuserid.ClientID%>");
                divsecurity.style.display = "block"; // Visible
                divuserid.style.display = "none"; // Hidden

                var hdnuid = document.getElementById("<%= hdnuid.ClientID%>").value;
                PageMethods.WMBindSercurtyQuestion(hdnuid, WMBindSercurtyQuestion_onSuccessed, null);
            }
        }

        var ddlquestion1 = document.getElementById("<%= ddlquestion1.ClientID%>");
    
        function WMBindSercurtyQuestion_onSuccessed(result) {
            ddlquestion1.options.length = 0;
         //   ddlquestion2.options.length = 0;
            for (var i in result) {
                AddOption(result[i].Name, result[i].Id);
            }
            //for (var i in result) {
            //    AddOption1(result[i].Name, result[i].Id);
            //}
        }
        function AddOption(text, value) {
            var option = document.createElement('option');
            option.value = value;
            option.innerHTML = text;
            ddlquestion1.options.add(option);
        }

        //function AddOption1(text, value) {
        //    var option = document.createElement('option');
        //    option.value = value;
        //    option.innerHTML = text;
        //    ddlquestion2.options.add(option);
        //}


    </script>

</body>
</html>
