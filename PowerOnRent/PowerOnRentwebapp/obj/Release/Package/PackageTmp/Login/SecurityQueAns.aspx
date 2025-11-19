<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SecurityQueAns.aspx.cs" MasterPageFile="~/MasterPage/CRM.Master" EnableEventValidation="false"
    Theme="Blue" Inherits="PowerOnRentwebapp.Login.SecurityQueAns" %>

<%@ Register Assembly="obout_Flyout2_NET" Namespace="OboutInc.Flyout2" TagPrefix="obout" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="obout" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>
<%@ Register Assembly="Obout.Ajax.UI" Namespace="Obout.Ajax.UI.HTMLEditor" TagPrefix="obout" %>
<%@ Register Src="../CommonControls/UC_Date.ascx" TagName="UC_Date" TagPrefix="uc1" %>
<%@ Register Src="../CommonControls/Toolbar.ascx" TagName="Toolbar" TagPrefix="uc2" %>
<%@ Register Src="../CommonControls/UCFormHeader.ascx" TagName="UCFormHeader" TagPrefix="uc2" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHolder_FormHeader" runat="server">

    <table style="width: 100%">
        <tr>
            <td style="text-align: right">
                <input type="button" style="width: 5%; height: 20%" value="Save" onclick="jsSaveData()" />
                <input type="button" style="width: 5%; height: 20%" value="Clear" onclick="jsClearData()" />
            </td>
        </tr>
    </table>



    <%-- <uc2:Toolbar ID="Toolbar1" runat="server" />
    <uc2:UCFormHeader ID="UCFormHeader1" runat="server" />--%>
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="CPHolder_Form" runat="server">
    <center>
        <table class="tableForm" border="0">
            <tr>
                <td style="text-align: left; width: 40%">Question 1:
                </td>
                <td style="text-align: left; width: 60%">Answer:
                </td>
            </tr>
            <tr>
                <td style="text-align: left; width: 40%">
                    <asp:DropDownList ID="ddlquestion1" Width="100%" runat="server" DataTextField="Value" DataValueField="ID">
                    </asp:DropDownList>
                </td>
                <td style="text-align: left; width: 60%">
                    <asp:TextBox ID="txtanswer1" Width="100%" runat="server"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td style="text-align: left; width: 40%">Question 2:
                </td>
                <td style="text-align: left; width: 60%">Answer:
                </td>
            </tr>
            <tr>
                <td style="text-align: left; width: 40%">
                    <asp:DropDownList ID="ddlquestion2" Width="100%" runat="server" DataTextField="Value" DataValueField="ID">
                    </asp:DropDownList>
                </td>
                <td style="text-align: left; width: 60%">
                    <asp:TextBox ID="txtanswer2" Width="100%" runat="server"></asp:TextBox>
                </td>
            </tr>


            <tr>
                <td style="text-align: left; width: 40%">Question 3:
                </td>
                <td style="text-align: left; width: 60%">Answer:
                </td>
            </tr>
            <tr>
                <td style="text-align: left; width: 40%">
                    <asp:DropDownList ID="ddlquestion3" Width="100%" runat="server" DataTextField="Value" DataValueField="ID">
                    </asp:DropDownList>
                </td>
                <td style="text-align: left; width: 60%">
                    <asp:TextBox ID="txtanswer3" Width="100%" runat="server"></asp:TextBox>
                </td>
            </tr>


        </table>
    </center>

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

            if (q1 == "0" || txtanswer1.value == "") { s1 = 0; } else { s1 = 1; }
            if (q2 == "0" || txtanswer2.value == "") { s2 = 0; } else { s2 = 1; }
            if (q3 == "0" || txtanswer3.value == "") { s3 = 0; } else { s3 = 1; }
            sum = s1 + s2 + s3;
            if (q3 != "0") {
                if (txtanswer3.value == "") {
                    showAlert("Please Fill Answer Question.", "Error", '#');
                }
                else {
                    if (sum >= 2) {
                        if (q1 == q2 || q1 == q3 || q2 == q3) {
                            showAlert("Please select unique security question.", "Error", '#');
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
                        showAlert("Please select at least 2 unique security question.", "Error", '#');
                    }
                }
            }
            else {
                if (sum >= 2) {
                    if (q1 == q2 || q1 == q3 || q2 == q3) {
                        showAlert("Please select unique security question.", "Error", '#');
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
                    showAlert("Please select at least 2 unique security question.", "Error", '#');
                }
            }
        }

        function OnSuccessWMSaveSecurityQuestion(result) {
            if (result > 0) {
                showAlert(" Record save scuessfully.", "info", '../Inbox/InboxPOR.aspx');
            }
            else {
                showAlert("Error occurred.", "Error", "#");
            }
        }

    </script>
</asp:Content>
