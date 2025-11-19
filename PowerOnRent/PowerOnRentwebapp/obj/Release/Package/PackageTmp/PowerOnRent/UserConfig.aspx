<%@ Page Language="C#" AutoEventWireup="true" Theme="Blue" MasterPageFile="~/MasterPage/CRM.Master" CodeBehind="UserConfig.aspx.cs" Inherits="PowerOnRentwebapp.PowerOnRent.UserConfig" EnableEventValidation="false" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="obout" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Territory/UC_Territory.ascx" TagName="UC_Territory" TagPrefix="uc1" %>
<%@ Register Src="../Address/UCAddress.ascx" TagName="UCAddress" TagPrefix="uc7" %>
<%@ Register Src="../CommonControls/UC_Date.ascx" TagName="UC_Date" TagPrefix="uc2" %>
<%@ Register Src="../CommonControls/UCFormHeader.ascx" TagName="UCFormHeader" TagPrefix="uc4" %>
<%@ Register Src="../CommonControls/UCToolbar.ascx" TagName="UCToolbar" TagPrefix="uc5" %>


<asp:Content ID="Content2" ContentPlaceHolderID="CPHolder_FormHeader" runat="server">
    <uc5:UCToolbar ID="UCToolbar1" Visible="false" runat="server" />
    <table style="width: 100%">
        <tr>
            <td style="width: 100%; text-align: right">
                <asp:Button ID="btnsave" Text="Save" OnClientClick="return ChkValidation();" runat="server" OnClick="btnsave_Click" />
                <asp:Button ID="Button1" Text="Clear" OnClientClick="clearall()" runat="server"  />
            </td>
        </tr>
    </table>

    <uc4:UCFormHeader ID="UCFormHeader1" runat="server" />
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="CPHolder_Form" runat="server">

    <asp:ValidationSummary ID="validationsummary_Department" runat="server" ShowMessageBox="true"
        ShowSummary="false" DisplayMode="bulletlist" ValidationGroup="Save" />
    <asp:ValidationSummary ID="validationsummaryforsearch" runat="server" ShowMessageBox="true"
        ShowSummary="false" DisplayMode="bulletlist" ValidationGroup="search" />

    <center>
        <asp:TabContainer ID="TabContainerReasoncodeGV" runat="server" ActiveTabIndex="0">
            <asp:TabPanel ID="TabPanelReasonList" runat="server" HeaderText="Direct Import User Config">
                <ContentTemplate>
                    <center>
                        <table style="width: 100%">
                            <tr>

                                <td style="width: 10%; text-align: right">
                                    <asp:Label ID="lblcustsearch" runat="server" Text="Customer"></asp:Label>
                                </td>

                                <td style="width: 20%">
                                    <asp:DropDownList runat="server" ID="ddlCompany" Width="182px" DataTextField="Name"
                                        DataValueField="ID" ClientIDMode="Static" onchange="GetDept(this);">
                                    </asp:DropDownList>

                                </td>

                                <td style="width: 10%; text-align: right">
                                    <asp:Label ID="lbldeptsearch" runat="server" Text="Department"></asp:Label>
                                </td>

                                <td style="width: 20%">

                                    <asp:DropDownList runat="server" ID="ddlSites" Width="182px" DataTextField="Territory"
                                        DataValueField="ID" ClientIDMode="Static" onchange="BindGrid(this);">
                                    </asp:DropDownList>
                                </td>

                                <td style="width: 10%; text-align: right">
                                    <asp:Label ID="Label1" runat="server" Text="Select Requestor"></asp:Label>
                                </td>

                                <td style="width: 20%">
                                    <asp:TextBox ID="txtContact1" runat="server" Width="182px" Enabled="false" AccessKey="1"></asp:TextBox>
                                    <img id="imgSearch" src="../App_Themes/Grid/img/search.jpg" title="Search User"
                                        style="cursor: pointer;" onclick="openContactSearch('0')" />
                                </td>
                                <td style="width: 10%"></td>
                            </tr>
                        </table>
                        <table class="gridFrame" style="width: 100%">
                            <tr>
                                <td>
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="text-align: left;">
                                                <asp:Label ID="lblheader" CssClass="headerText" runat="server" Text="Direct Import User Config"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>

                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <obout:Grid ID="gvUserconfig" runat="server" AllowAddingRecords="False" AllowFiltering="True"
                                        Width="100%" AllowGrouping="True" AllowMultiRecordSelection="false" AutoGenerateColumns="False"
                                        OnRebind="gvUserconfig_Rebind" OnSelect="gvUserconfig_Select">
                                        <ScrollingSettings ScrollHeight="250" />
                                        <Columns>

                                            <obout:Column DataField="Id" Width="5%" HeaderText="Id" Visible="false" Index="4">
                                            </obout:Column>
                                            <obout:Column ID="Edit" HeaderText="Remove" Width="5%" TemplateId="GvTempEdit" Index="0">
                                                <TemplateSettings TemplateId="GvTempEdit" />
                                            </obout:Column>

                                            <obout:Column DataField="Customer" HeaderText="Customer" Width="10%" Index="1">
                                            </obout:Column>
                                            <obout:Column DataField="Territory" HeaderText="Department Name" Width="10%" Index="2">
                                            </obout:Column>
                                            <obout:Column DataField="username" HeaderText="User Name" Width="15%" Index="3">
                                            </obout:Column>


                                        </Columns>
                                        <Templates>

                                            <obout:GridTemplate ID="GvTempEdit" runat="server">
                                                <Template>
                                                    <asp:ImageButton ID="imgBtnEdit" CommandName="EditData" CausesValidation="false" runat="server" ImageUrl="../App_Themes/Grid/img/Remove16x16.png" />
                                                </Template>
                                            </obout:GridTemplate>
                                        </Templates>
                                    </obout:Grid>
                                </td>
                            </tr>

                        </table>
                    </center>
                </ContentTemplate>
            </asp:TabPanel>


        </asp:TabContainer>

    </center>



    <asp:HiddenField ID="hdnSelectedCompany" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hnduserID" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hndRoleSate" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnSelectedDept" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnSelectedCompanysearch" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnSelectedDeptvalue" runat="server" ClientIDMode="Static" />

    <asp:HiddenField ID="hdnReasoncode" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnReasondescription" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdntype" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnactive" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdndefautvalue" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnid" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnreasonarb" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnSelectedCompanynew" runat="server" ClientIDMode="Static" />

    <asp:HiddenField ID="hdnidno" runat="server" ClientIDMode="Static" />





    <script type="text/javascript">
        function CallConfirmBox() {
            var hdnSelectedCompany = document.getElementById("<%=hdnSelectedCompany.ClientID %>");;
            var hdnSelectedDept = document.getElementById("<%=hdnSelectedDept.ClientID %>");
            var hdnReasoncode = document.getElementById("<%=hdnReasoncode.ClientID %>");
            var hdnReasondescription = document.getElementById("<%=hdnReasondescription.ClientID %>");
            var hdntype = document.getElementById("<%=hdntype.ClientID %>");
            var hdnactive = document.getElementById("<%=hdnactive.ClientID %>");
            var hdndefautvalue = document.getElementById("<%=hdndefautvalue.ClientID %>");
            var hdnid = document.getElementById("<%=hdnid.ClientID %>");
            var hdnreasonarb = document.getElementById("<%=hdnreasonarb.ClientID %>");

            if (confirm("Already Default Reason Code Is Set Do You Want To Change")) {
                PageMethods.WMInsertDefalutvalue(hdnSelectedCompany.value, hdnSelectedDept.value, hdnReasoncode.value, hdnReasondescription.value, hdntype.value, hdnactive.value, hdndefautvalue.value, hdnid.value, hdnreasonarb.value, onSuccessInsertDefalutvalue, null)
            } else {
                //CANCEL – Do your stuff or call any callback method here..
                //alert("You pressed Cancel!");          
            }
        }

        function onSuccessInsertDefalutvalue(result) {
            if (result == 'Insert') {
                showAlert("Record save successfully", "info", "#");
                var tabContainer = document.getElementById('<%=TabContainerReasoncodeGV.ClientID%>');
                tabContainer.control.set_activeTabIndex(0);
            }
            else if (result == 'Update') {
                showAlert("Record update successfully", "info", "#");
            }

            Clear();
        }
    </script>

    <script type="text/javascript">

        function openContactSearch(seq) {
            var hdnselectedCompany = document.getElementById("<%=hdnSelectedCompany.ClientID %>");
            var hdnSelectedDept = document.getElementById("<%=hdnSelectedDept.ClientID %>");

            if (hdnselectedCompany.value == "0" || hdnselectedCompany.value == null || hdnselectedCompany.value == "") {
                showAlert("Select Customer.", "error", "#");
            }
            else if (hdnSelectedDept.value == "0" || hdnSelectedDept.value == null || hdnSelectedDept.value == "") {
                showAlert("Select Department.", "error", "#");
            }
            else {
                window.open('../PowerOnRent/UserList.aspx?Com=' + hdnselectedCompany.value + '  &Dept=' + hdnSelectedDept.value + '', null, 'height=1000px, width=825px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=300; screenY=50');
            }
        }

        function GetDeptnew() {
          <%-- // var ddlcompanynew = document.getElementById("<%=ddlcompanynew.ClientID %>");--%>
            var hdnSelectedCompanynew = document.getElementById("<%=hdnSelectedCompanynew.ClientID %>");
            hdnSelectedCompanynew.value = ddlcompanynew.value;
            var Cmpny = ddlcompanynew.value;
            PageMethods.WMGetDeptnew(Cmpny, OnSuccessCompanynew, null);
        }
        function OnSuccessCompanynew(result) {
       <%--    ////// ddldepartment = document.getElementById("<%=ddldepartment.ClientID %>");--%>
            ddldepartment.options.length = 0;
            var option0 = document.createElement("option");

            if (result.length > 0) {
                option0.text = '--Select--';
                option0.value = '0';
            }
            else {
                option0.text = 'N/A';
                option0.value = '0';
            }

            try {
                ddldepartment.add(option0, null);
            }
            catch (Error) {
                ddldepartment.add(option0);
            }

            for (var i = 0; i < result.length; i++) {
                var option1 = document.createElement("option");
                option1.text = result[i].Territory;
                option1.value = result[i].ID;
                try {
                    ddldepartment.add(option1, null);
                }
                catch (Error) {
                    ddldepartment.add(option1);
                }
            }
        }


        function Clear() {
          <%--  document.getElementById("<%=ddlcompanynew.ClientID %>").value = 0;
            document.getElementById("<%=ddldepartment.ClientID %>").value = 0;
            document.getElementById("<%=ddltype.ClientID %>").value = 0;
            document.getElementById("<%=txtreason.ClientID%>").value = "";
            document.getElementById("<%=txtdescription.ClientID%>").value = "";
            document.getElementById("<%=txtreasoninarb.ClientID%>").value = "";--%>
            return true;
        }

    </script>


    <script type="text/javascript">

        function GetDept() {
            var ddlCompany = document.getElementById("<%=ddlCompany.ClientID %>");
            var hdnselectedCompany = document.getElementById("<%=hdnSelectedCompany.ClientID %>");
            hdnselectedCompany.value = ddlCompany.value;
            var Cmpny = ddlCompany.value;
            PageMethods.WMGetDept(Cmpny, OnSuccessCompany, null);
        }
        function OnSuccessCompany(result) {
            ddlSites = document.getElementById("<%=ddlSites.ClientID %>");
            ddlSites.options.length = 0;
            var option0 = document.createElement("option");

            if (result.length > 0) {
                option0.text = '--Select--';
                option0.value = '0';
            }
            else {
                option0.text = 'N/A';
                option0.value = '0';
            }

            try {
                ddlSites.add(option0, null);
            }
            catch (Error) {
                ddlSites.add(option0);
            }

            for (var i = 0; i < result.length; i++) {
                var option1 = document.createElement("option");
                option1.text = result[i].Territory;
                option1.value = result[i].ID;
                try {
                    ddlSites.add(option1, null);
                }
                catch (Error) {
                    ddlSites.add(option1);
                }
            }
        }


        function BindGrid(id) {
            var ddlSites = document.getElementById("<%=ddlSites.ClientID %>");
            var hdnSelectedDept = document.getElementById("<%=hdnSelectedDept.ClientID %>");
            hdnSelectedDept.value = ddlSites.value;

        }

        function GetDepartment(id) {
           <%-- var ddldepartment = document.getElementById("<%=ddldepartment.ClientID %>");--%>
            var hdnSelectedDeptvalue = document.getElementById("<%=hdnSelectedDeptvalue.ClientID %>");
            hdnSelectedDeptvalue.value = ddldepartment.value;
        }


        function gvUserconfigval(value, Id) {
            var txtContact1 = document.getElementById("<%=txtContact1.ClientID %>");
            txtContact1.value = value;
            var hdnidno = document.getElementById("<%=hdnidno.ClientID %>");
            hdnidno.value = Id;
        }


        function ShowDetails(val) {
            alert(val);
        }

        function ChkValidation() {
            var hdnselectedCompany = document.getElementById("<%=hdnSelectedCompany.ClientID %>");
            var hdnSelectedDept = document.getElementById("<%=hdnSelectedDept.ClientID %>");
            var txtContact1 = document.getElementById("<%=txtContact1.ClientID %>");
            var hdnidno = document.getElementById("<%=hdnidno.ClientID %>");

            if (hdnselectedCompany.value == "" || hdnselectedCompany.value == null || hdnselectedCompany.value == "0") {
                showAlert("Select Customer.", "error", "#");
                return false;
            }
            else if (hdnSelectedDept.value == "" || hdnSelectedDept.value == null || hdnSelectedDept.value == "0") {

                showAlert("Select Department.", "error", "#");
                return false;
            }
            else if (txtContact1.value == "" || txtContact1.value == null || txtContact1.value == "0") {
                showAlert("Select Users.", "error", "#");
                return false;
            }
            else {
                PageMethods.SaveUserConfig(hdnselectedCompany.value, hdnSelectedDept.value, hdnidno.value, OnSuccessCompany1, null);
            }
        }

        function OnSuccessCompany1(val) {

            window.location.href = "../PowerOnRent/UserConfig.aspx"
            <%-- gvUserconfig.refresh();
            var hdnselectedCompany = document.getElementById("<%=hdnSelectedCompany.ClientID %>");
            hdnselectedCompany.value = "0";
            var hdnSelectedDept = document.getElementById("<%=hdnSelectedDept.ClientID %>");
            hdnSelectedDept.value = "0";
            var txtContact1 = document.getElementById("<%=txtContact1.ClientID %>");
            txtContact1.value = "";
            var hdnidno = document.getElementById("<%=hdnidno.ClientID %>");
            hdnidno.value = "0";

            setSelectedIndex(document.getElementById("<%=ddlCompany.ClientID %>"), 0);

            setSelectedIndex(document.getElementById("<%=ddlSites.ClientID %>"), 0);--%>
           
           
        }


        function setSelectedIndex(s, i) {

            s.options[i - 1].selected = true;

            return;

        }


        function clearall() {
             window.location.href = "../PowerOnRent/UserConfig.aspx"
            <%-- var hdnselectedCompany = document.getElementById("<%=hdnSelectedCompany.ClientID %>");
            hdnselectedCompany.value = "0";
            var hdnSelectedDept = document.getElementById("<%=hdnSelectedDept.ClientID %>");
            hdnSelectedDept.value = "0";
            var txtContact1 = document.getElementById("<%=txtContact1.ClientID %>");
            txtContact1.value = "";
            var hdnidno = document.getElementById("<%=hdnidno.ClientID %>");
            hdnidno.value = "0";

            setSelectedIndex(document.getElementById("<%=ddlCompany.ClientID %>"), 0);

            setSelectedIndex(document.getElementById("<%=ddlSites.ClientID %>"), 0);--%>
        }



    </script>


</asp:Content>
