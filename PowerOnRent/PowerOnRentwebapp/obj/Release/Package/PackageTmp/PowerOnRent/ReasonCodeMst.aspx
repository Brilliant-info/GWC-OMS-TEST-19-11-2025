<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReasonCodeMst.aspx.cs" MasterPageFile="~/MasterPage/CRM.Master" Theme="Blue" EnableEventValidation="false"
    Inherits="PowerOnRentwebapp.PowerOnRent.ReasonCodeMst" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="obout" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Territory/UC_Territory.ascx" TagName="UC_Territory" TagPrefix="uc1" %>
<%@ Register Src="../Address/UCAddress.ascx" TagName="UCAddress" TagPrefix="uc7" %>
<%@ Register Src="../CommonControls/UC_Date.ascx" TagName="UC_Date" TagPrefix="uc2" %>
<%@ Register Src="../CommonControls/UCFormHeader.ascx" TagName="UCFormHeader" TagPrefix="uc4" %>
<%@ Register Src="../CommonControls/UCToolbar.ascx" TagName="UCToolbar" TagPrefix="uc5" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPHolder_FormHeader" runat="server">
    <uc5:UCToolbar ID="UCToolbar1" runat="server" />
    <uc4:UCFormHeader ID="UCFormHeader1" runat="server" />
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="CPHolder_Form" runat="server">

    <asp:ValidationSummary ID="validationsummary_Department" runat="server" ShowMessageBox="true"
        ShowSummary="false" DisplayMode="bulletlist" ValidationGroup="Save" />
    <asp:ValidationSummary ID="validationsummaryforsearch" runat="server" ShowMessageBox="true"
        ShowSummary="false" DisplayMode="bulletlist" ValidationGroup="search" />

    <center>
        <asp:TabContainer ID="TabContainerReasoncodeGV" runat="server" ActiveTabIndex="0">
            <asp:TabPanel ID="TabPanelReasonList" runat="server" HeaderText="Reason code List">
                <ContentTemplate>
                    <center>
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 10%"></td>
                                <td style="width: 10%; text-align: right">
                                    <asp:Label ID="lblcustsearch" runat="server" Text="Customer"></asp:Label>
                                </td>

                                <td style="width: 20%">
                                    <asp:DropDownList runat="server" ID="ddlCompany" Width="182px" DataTextField="Name"
                                        DataValueField="ID" ClientIDMode="Static" onchange="GetDept(this);">
                                    </asp:DropDownList>
                                    <%--<asp:DropDownList ID="ddlcustsearch" runat="server" Width="100%" ClientIDMode="Static" DataValueField="ID" DataTextField="Name" onchange="GetCompanySearch(this);BindDepartmentSearch()"></asp:DropDownList>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidatorddlcustsearch" runat="server" ErrorMessage="Select Customer"
                                        ControlToValidate="ddlcustsearch" InitialValue="0" ValidationGroup="search" Display="None"></asp:RequiredFieldValidator>
                                    --%>    

                                </td>

                                <td style="width: 10%; text-align: right">
                                    <asp:Label ID="lbldeptsearch" runat="server" Text="Department"></asp:Label>
                                </td>

                                <td style="width: 20%">

                                    <asp:DropDownList runat="server" ID="ddlSites" Width="182px" DataTextField="Territory"
                                        DataValueField="ID" ClientIDMode="Static" onchange="BindGrid(this);">
                                    </asp:DropDownList>


                                    <%--    <asp:DropDownList ID="ddldeptsearch" runat="server" Width="100%" ClientIDMode="Static" DataValueField="ID" DataTextField="Territory" onchange="GetDepartmentSearch(this);"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorddldeptsearch" runat="server" ErrorMessage="Select Department"
                                        ControlToValidate="ddldeptsearch" InitialValue="0" ValidationGroup="search" Display="None"></asp:RequiredFieldValidator>--%>
                                </td>

                                <td style="width: 20%">
                                    <%--<asp:Button ID="btnserach" ValidationGroup="search" runat="server" Text="Submit" OnClick="btnserach_Click" />--%>

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
                                                <asp:Label ID="lblheader" CssClass="headerText" runat="server" Text="Reason Code List"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>

                                </td>
                            </tr>

                            <tr>
                                <td>
                                    <obout:Grid ID="gvReasoncodelist" runat="server" AllowAddingRecords="False" AllowFiltering="True"
                                        Width="100%" AllowGrouping="True" AllowMultiRecordSelection="false" AutoGenerateColumns="False"
                                        OnRebind="gvReasoncodelist_Rebind" OnSelect="gvReasoncodelist_Select">
                                        <ScrollingSettings ScrollHeight="250" />
                                        <Columns>

                                            <obout:Column DataField="Id" HeaderText="Id" Visible="false" Index="10">
                                            </obout:Column>
                                            <obout:Column ID="Edit" HeaderText="Edit" Width="10%" TemplateId="GvTempEdit" Index="0">
                                                <TemplateSettings TemplateId="GvTempEdit" />
                                            </obout:Column>
                                            <obout:Column DataField="companyId" HeaderText="Company Id" Visible="false" Index="8">
                                            </obout:Column>
                                            <obout:Column DataField="DeptId" HeaderText="Dept Id" Visible="false" Index="9">
                                            </obout:Column>
                                            <obout:Column DataField="Name" HeaderText="Company Name" Width="10%" Index="1">
                                            </obout:Column>
                                            <obout:Column DataField="Territory" HeaderText="Department Name" Width="10%" Index="2">
                                            </obout:Column>
                                            <obout:Column DataField="ReasonCode" HeaderText="ReasonCode Name" Width="15%" Index="3">
                                            </obout:Column>
                                            <obout:Column DataField="ReasonDetails" HeaderText="Reason Code Description" Width="30%" Index="4">
                                            </obout:Column>
                                            <obout:Column DataField="Type" HeaderText="Type" Width="10%" Index="5">
                                            </obout:Column>
                                            <obout:Column DataField="Active" HeaderText="Active" Width="5%" Index="6">
                                            </obout:Column>
                                            <obout:Column DataField="DefaultValue" HeaderText="Default Value" Width="10%" Index="7">
                                            </obout:Column>
                                            <obout:Column DataField="reasoncodeArb" Visible="false" HeaderText="reasoncodeArb" Width="15%" Index="11">
                                            </obout:Column>

                                        </Columns>
                                        <Templates>

                                            <obout:GridTemplate ID="GvTempEdit" runat="server">
                                                <Template>
                                                    <asp:ImageButton ID="imgBtnEdit" CommandName="EditData" CausesValidation="false" runat="server" ImageUrl="../App_Themes/Blue/img/Edit16.png" />
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

            <asp:TabPanel ID="TabPanelReasonData" runat="server" HeaderText="Reason Code">
                <ContentTemplate>
                    <center>
                        <table class="tableForm" style="width: 100%">
                            <tr>
                                <td style="width: 10%"></td>


                                <td style="width: 10%; text-align: right">
                                    <req>   <asp:Label ID="lblCustomer" runat="server" Text="Customer"></asp:Label></req>
                                </td>


                                <td style="width: 15%">
                                    <asp:DropDownList ID="ddlcompanynew" runat="server" Width="100%" ClientIDMode="Static"
                                        DataValueField="ID" DataTextField="Name" onchange="GetDeptnew(this);">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RFVddlcompany" runat="server" ErrorMessage="Select Customer"
                                        ControlToValidate="ddlcompanynew" InitialValue="0" ValidationGroup="Save" Display="None"></asp:RequiredFieldValidator>

                                </td>


                                <td style="width: 10%; text-align: right">
                                    <req>  <asp:Label ID="lbldept" runat="server" Text="Department"></asp:Label></req>
                                </td>
                                <td style="width: 17%">
                                    <asp:DropDownList ID="ddldepartment" runat="server" Width="100%" ClientIDMode="Static" DataValueField="ID" DataTextField="Territory" onchange="GetDepartment(this);">
                                    </asp:DropDownList>

                                    <asp:RequiredFieldValidator ID="RFVddldepartment" runat="server" ErrorMessage="Select Department"
                                        ControlToValidate="ddldepartment" InitialValue="0" ValidationGroup="Save" Display="None"></asp:RequiredFieldValidator>

                                </td>
                                <td style="width: 10%; text-align: right">
                                    <req>    <asp:Label ID="lbltype" runat="server" Text="Type"></asp:Label><req>
                                </td>

                                <td style="width: 18%">
                                    <asp:DropDownList ID="ddltype" runat="server" Width="100%" ClientIDMode="Static">
                                        <asp:ListItem Text="-Select-" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Rejection" Value="Rejection"></asp:ListItem>
                                        <asp:ListItem Text="Cancellation" Value="Cancellation"></asp:ListItem>
                                    </asp:DropDownList>

                                    <asp:RequiredFieldValidator ID="RFVddltype" runat="server" ErrorMessage="Select Type"
                                        ControlToValidate="ddltype" InitialValue="0" ValidationGroup="Save" Display="None"></asp:RequiredFieldValidator>

                                </td>
                                <td style="width: 10%"></td>
                            </tr>


                            <tr>
                                <td style="width: 10%"></td>

                                <td style="width: 10%; text-align: right">
                                    <req>   <asp:Label ID="lblreason" runat="server" Text="Reason Code"></asp:Label><req>
                                </td>

                                <td style="width: 15%">
                                    <asp:TextBox ID="txtreason" runat="server" Width="100%" MaxLength="50"></asp:TextBox>

                                    <asp:RequiredFieldValidator ID="valRftxtreason" runat="server" ControlToValidate="txtreason"
                                        ErrorMessage="Enter Reason Code." ValidationGroup="Save" Display="None"></asp:RequiredFieldValidator>
                                </td>

                                <td style="width: 10%; text-align: right">
                                    <asp:Label ID="lblreasonarb" runat="server" Text="Reason code(Arabic)"></asp:Label>
                                </td>

                                <td style="width: 17%">
                                    <asp:TextBox ID="txtreasoninarb" runat="server" Width="100%" MaxLength="250"></asp:TextBox>

                                </td>


                                <td style="width: 10%; text-align: right">
                                    <req>   <asp:Label ID="lbldescription" runat="server" Text="Description"></asp:Label><req>
                                </td>

                                <td style="width: 18%">
                                    <asp:TextBox ID="txtdescription" runat="server" Width="100%" MaxLength="250"></asp:TextBox>

                                    <asp:RequiredFieldValidator ID="valRftxtdescription" runat="server" ControlToValidate="txtdescription"
                                        ErrorMessage="Enter Reason Code Description." ValidationGroup="Save" Display="None"></asp:RequiredFieldValidator>
                                </td>

                                <td style="width: 10%"></td>
                            </tr>

                            <tr>
                                <td style="width: 10%"></td>

                                <td style="width: 10%; text-align: right">
                                    <asp:Label ID="lblactive" runat="server" Text="Active"></asp:Label>
                                </td>

                                <td style="width: 15%; text-align: left">

                                    <obout:OboutRadioButton ID="rbtactYes" runat="server" Text="Yes" GroupName="rbtnActive"
                                        Checked="True" FolderStyle="">
                                    </obout:OboutRadioButton>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <obout:OboutRadioButton ID="rbtactNo" runat="server" Text="No" GroupName="rbtnActive"
                                        FolderStyle="">
                                    </obout:OboutRadioButton>
                                </td>


                                <td style="width: 10%; text-align: right">
                                    <asp:Label ID="lbldefault" runat="server" Text="Default"></asp:Label>
                                </td>

                                <td style="width: 17%; text-align: left">
                                    <obout:OboutRadioButton ID="rbtnYes" runat="server" Text="Yes" GroupName="rbtnDefault"
                                        FolderStyle="">
                                    </obout:OboutRadioButton>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <obout:OboutRadioButton ID="rbtnNo" runat="server" Text="No" GroupName="rbtnDefault"
                                        Checked="True" FolderStyle="">
                                    </obout:OboutRadioButton>

                                </td>


                                <td style="width: 10%; text-align: center"></td>

                                <td style="width: 18%"></td>

                                <td style="width: 10%"></td>
                            </tr>


                        </table>


                    </center>

                    <%--     <asp:Panel ID="panelmodel" runat="server" Visible="false">
                        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                        </asp:ToolkitScriptManager>
                        <ajax:modalpopupextender id="Modalreasoncode" runat="server" targetcontrolid="btnShowPopup" popupcontrolid="pnlpopup"
                            cancelcontrolid="btnNo" backgroundcssclass="modalBackground">
                        </ajax:modalpopupextender>
                        <asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="100px" Width="400px" Style="display: none">
                            <table width="100%" style="border: Solid 2px #D46900; width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                                <tr style="background-image: url(Images/header.gif)">
                                    <td style="height: 10%; color: White; font-weight: bold; padding: 3px; font-size: larger; font-family: Calibri" align="Left">Confirm Box</td>
                                    <td style="color: White; font-weight: bold; padding: 3px; font-size: larger" align="Right">
                                        <a href="javascript:void(0)" onclick="closepopup()">
                                            <img src="Images/Close.gif" style="border: 0px" align="right" /></a>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="left" style="padding: 5px; font-family: Calibri">
                                        <asp:Label ID="lblmodelmsg" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2"></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td align="right" style="padding-right: 15px">

                                        <asp:Button ID="btnyes" runat="server" Text="Yes" />
                                        <asp:Button ID="btnno" runat="server" Text="No" />

                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>

                    </asp:Panel>--%>
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

        function GetDeptnew() {
            var ddlcompanynew = document.getElementById("<%=ddlcompanynew.ClientID %>");
             var hdnSelectedCompanynew = document.getElementById("<%=hdnSelectedCompanynew.ClientID %>");
            hdnSelectedCompanynew.value = ddlcompanynew.value;
            var Cmpny = ddlcompanynew.value;
            PageMethods.WMGetDeptnew(Cmpny, OnSuccessCompanynew, null);
        }
        function OnSuccessCompanynew(result) {
            ddldepartment = document.getElementById("<%=ddldepartment.ClientID %>");
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
            document.getElementById("<%=ddlcompanynew.ClientID %>").value = 0;
            document.getElementById("<%=ddldepartment.ClientID %>").value = 0;
            document.getElementById("<%=ddltype.ClientID %>").value = 0;
            document.getElementById("<%=txtreason.ClientID%>").value = "";
            document.getElementById("<%=txtdescription.ClientID%>").value = "";
            document.getElementById("<%=txtreasoninarb.ClientID%>").value = "";
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
            gvReasoncodelist.refresh();
        }

        function GetDepartment(id) {
            var ddldepartment = document.getElementById("<%=ddldepartment.ClientID %>");
            var hdnSelectedDeptvalue = document.getElementById("<%=hdnSelectedDeptvalue.ClientID %>");
            hdnSelectedDeptvalue.value = ddldepartment.value;
        }


    </script>


</asp:Content>



