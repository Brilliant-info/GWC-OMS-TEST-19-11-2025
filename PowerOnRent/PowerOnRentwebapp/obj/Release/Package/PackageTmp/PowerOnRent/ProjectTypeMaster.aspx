<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectTypeMaster.aspx.cs" MasterPageFile="~/MasterPage/CRM.Master" Theme="Blue" EnableEventValidation="false" Inherits="PowerOnRentwebapp.PowerOnRent.ProjectTypeMaster" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="obout" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Territory/UC_Territory.ascx" TagName="UC_Territory" TagPrefix="uc1" %>
<%@ Register Src="../Address/UCAddress.ascx" TagName="UCAddress" TagPrefix="uc7" %>
<%@ Register Src="../CommonControls/UC_Date.ascx" TagName="UC_Date" TagPrefix="uc2" %>
<%@ Register Src="../CommonControls/UCFormHeader.ascx" TagName="UCFormHeader" TagPrefix="uc4" %>
<%@ Register Src="../CommonControls/UCToolbar.ascx" TagName="UCToolbar" TagPrefix="uc5" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPHolder_FormHeader" runat="server">
    <%--  <uc5:UCToolbar ID="UCToolbar1" runat="server" />
    <uc4:UCFormHeader ID="UCFormHeader1" runat="server" />--%>
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="CPHolder_Form" runat="server">

    <asp:ValidationSummary ID="validationsummary_Department" runat="server" ShowMessageBox="true"
        ShowSummary="false" DisplayMode="bulletlist" ValidationGroup="Save" />
    <asp:ValidationSummary ID="validationsummaryforsearch" runat="server" ShowMessageBox="true"
        ShowSummary="false" DisplayMode="bulletlist" ValidationGroup="search" />
    <asp:HiddenField ID="hdnConID" runat="server" />
    <asp:HiddenField ID="hdnstate" runat="server" ClientIDMode="Static" />
    <center>
        <asp:TabContainer ID="TabContainerReasoncodeGV" runat="server" ActiveTabIndex="0">
            <asp:TabPanel ID="TabPanelReasonList" runat="server" HeaderText="Project Type Master">
                <ContentTemplate>
                    <center>
                        <div id="divLoading" style="display: none; top: 10px; left: 40%; height: 50%; width: 50%;"
                            class="modal" runat="server" clientidmode="Static">
                            <center>
                                <img src="../App_Themes/Blue/img/ajax-loader.gif" alt="" style="top: 0%; margin-top: 1%" />
                                <br />
                                <br />
                                <b>Please Wait...</b>
                            </center>
                            </div>
                       
                        <table class="tableForm" style="width: 50%" id="tblAddAdrs" runat="server">
                            <tr runat="server">
                                <td runat="server">
                                    <req><asp:Label Id="lblprojecttype" runat="server" Text="Project Type"/>

</req>
                                    :
                                </td>
                                <td style="text-align: left" runat="server">
                                    <asp:TextBox ID="txtprojectype" Width="200px" runat="server" MaxLength="20"
                                        ClientIDMode="Static" onchange="CheckDuplicateAddress();"></asp:TextBox>
                                </td>
                                <td runat="server">
                                    <req><asp:Label Id="lblactive" runat="server" Text="Active"/>

</req>
                                    :
                                </td>
                                <td runat="server">
                                    <obout:OboutRadioButton ID="rbtnYes" runat="server" Text="Yes" GroupName="rbtnActive"
                                        Checked="True" FolderStyle="">
                                    </obout:OboutRadioButton>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <obout:OboutRadioButton ID="rbtnNo" runat="server" Text="No" GroupName="rbtnActive"
                                        FolderStyle="">
                                    </obout:OboutRadioButton>
                                </td>
                            </tr>

                            <tr runat="server">
                                <td runat="server"></td>
                                <td runat="server"></td>
                                <td runat="server"></td>
                                <td style="text-align: right" runat="server">
                                    <input type="button" id="btnSave" title="Save" runat="server" value="Save" onclick="SaveContactDetail();" />                                  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                            </tr>
                        </table>
                        <asp:HiddenField ID="hdnContactID" runat="server" />
                        <table class="gridFrame" width="80%">
                            <tr>
                                <td>
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="text-align: left;">
                                                <a id="headerText">
                                                    <asp:Label ID="lblAddressList" runat="server" Text="Project Type List" /></a>
                                            </td>
                                            <td style="text-align: right;">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <input type="text" id="txtProjecttypeSearch" onkeyup="SearchProduct();" style="font-size: 15px; padding: 2px; width: 350px;" />
                                                            <asp:HiddenField runat="server" ID="hdnFilterText" />
                                                        </td>
                                                        <td>
                                                            <img src="../App_Themes/Blue/img/Search24.png" onclick="SearchProduct()" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <obout:Grid ID="gvContactPerson" runat="server" AutoGenerateColumns="False" AllowAddingRecords="False"
                                        AllowFiltering="True" AllowGrouping="True" AllowMultiRecordSelection="False" OnRebind="gvContactPerson_Rebind" Width="100%" AllowColumnReordering="True">
                                        <Columns>
                                            <obout:Column HeaderText="Edit" DataField="ID" Width="3%" Align="center" HeaderAlign="center" Index="0" TemplateId="TemplateEdit">
                                                <TemplateSettings TemplateId="TemplateEdit" />
                                            </obout:Column>
                                            <obout:Column DataField="Projecttype" HeaderText="Project Type" Width="15%" Index="1">
                                            </obout:Column>
                                            <obout:Column DataField="Active" HeaderText="Active" Width="8%" Index="2">
                                            </obout:Column>

                                        </Columns>
                                        <Templates>
                                            <obout:GridTemplate runat="server" ID="TemplateEdit">
                                                <Template>
                                                    <asp:ImageButton ID="imgBtnEdit1" runat="server" ImageUrl="../App_Themes/Blue/img/Edit16.png" OnClick="imgBtnEdit1_Click"
                                                        ToolTip='<%# (Container.Value) %>' CausesValidation="false" />
                                                    <%--OnClick="imgBtnEdit_OnClick"--%>
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

    <asp:HiddenField ID="hdnSelectedRec" runat="server" ClientIDMode="Static" />
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
                SelectedPrdRec();
            }
        }

        function SelectedPrdRec() {
            var hdnSelectedRec = document.getElementById("hdnSelectedRec");
            hdnSelectedRec.value = "";

            for (var i = 0; i < gvContactPerson.PageSelectedRecords.length; i++) {
                var record = gvContactPerson.PageSelectedRecords[i];
                if (hdnSelectedRec.value != "") hdnSelectedRec.value += ',' + record.ID;
                if (hdnSelectedRec.value == "") hdnSelectedRec.value = record.ID;
            }
        }



        function SaveContactDetail() {
            var txtprojectype = document.getElementById("<%= txtprojectype.ClientID %>");
            if (txtprojectype.value == "") {
                showAlert("Enter Project Type", "Error");
                txtprojectype.focus();
            } else {
                var hdnstate = document.getElementById('hdnstate');
                //var AddressInfo = new Object();
                //AddressInfo.AddressLine1 = document.getElementById("txtAddress").value;
                //AddressInfo.State = document.getElementById("ddlState").value;
                //AddressInfo.City = txtcity.value.toString();
                var activeyesno = "Yes";
                if (document.getElementById("<%= rbtnYes.ClientID %>").checked) {
                    //if (document.getElementById('rbtnYes').checked) {
                    activeyesno = "Yes";
                }
                else {
                    activeyesno = "No";
                }
                 LoadingOn(true);
                PageMethods.WMSaveRequestHead(txtprojectype.value, activeyesno, hdnstate.value, WMSaveContact_onSuccessed, WMSaveContact_onFailed);

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

            function WMSaveContact_onSuccessed(result) {
                if (result == "Some error occurred" || result == "") { LoadingOff(); showAlert("Error occurred", "Error", '#'); }
                else if (result == "Project Type Already Available") {
                    LoadingOff();
                    showAlert("Project Type Already Available", "Error", '#');
                    document.getElementById("<%= txtprojectype.ClientID %>").value = "";
                    txtprojectype.focus();
                }
                else if (result == "Project Type Saved Successfully") {
                    LoadingOff();
                    showAlert(result, "info");
                    txtprojectype.value = "";
                   // $("#rbtnYes").prop("checked", true);
                   // document.getElementById("rbtnYes").checked = true;
                   // document.getElementById("rbtnYes").checked = true;
                    gvContactPerson.refresh();
                    //self.close();
                }
            }
            function WMSaveContact_onFailed() {
                showAlert("Error occurred", "Error", "#"); self.close();
            }
        }

        function GetContactPerson() {
            var hdnSelectedRec = document.getElementById('hdnSelectedRec');
            if (hdnSelectedRec.value == "") {
                showAlert("Select Atleast One Project Type...", "Error");
            } else {
                var SelCon = hdnSelectedRec.value;
                var count = (SelCon.match(/,/g) || []).length;
                console.log(count);
                if (count >= 1) {
                    showAlert("Select Only One Project Type", "Error", "#");
                } else {
                    var hdnSearchAddressID = window.opener.document.getElementById("hdnprojectid");
                    var hdnprojetnm = window.opener.document.getElementById("hdnprojetnm");

                    hdnSearchAddressID.value = "";
                    hdnprojetnm.value = "";
                    if (gvContactPerson.SelectedRecords.length > 0) {
                        for (var i = 0; i < gvContactPerson.SelectedRecords.length; i++) {
                            var record = gvContactPerson.SelectedRecords[i];
                            if (hdnSearchAddressID.value != "") { hdnSearchAddressID.value += ',' + record.ID; hdnprojetnm.value += ',' + record.Projecttype; }
                            if (hdnSearchAddressID.value == "") {
                                hdnSearchAddressID.value = record.ID;
                                hdnprojetnm.value = record.Projecttype;
                                // hdnSearchAddress.value = record.AddressLine1;
                                //hdnSearchAddress.value = record.AddressLine1 + ', Contact Name:' + record.ContactName + ',Mobile:' + record.MobileNo + ', Email:' + record.ContactEmail;
                            }
                        }
                        window.opener.AfterProjectTypeSelected(hdnSearchAddressID.value, hdnprojetnm.value);
                        self.close();
                    }
                }
            }
        }


        var searchTimeout = null;
        function SearchProduct() {
            var hdnFilterText = document.getElementById("<%= hdnFilterText.ClientID %>");
            hdnFilterText.value = document.getElementById("txtProjecttypeSearch").value;
            if (searchTimeout != null) {
                window.clearTimeout(searchTimeout);
            }
            searchTimeout = window.setTimeout(performSearch, 700);
        }

        function performSearch() {
            gvContactPerson.refresh();
            searchTimeout = null;
        }

    </script>
</asp:Content>
