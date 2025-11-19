<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SitecodeMaster.aspx.cs" MasterPageFile="~/MasterPage/CRM.Master" Theme="Blue" EnableEventValidation="false" Inherits="PowerOnRentwebapp.PowerOnRent.SitecodeMaster" %>

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
     <asp:HiddenField ID="hdnSelectedRec" runat="server" ClientIDMode="Static" />
    <center>
        <asp:TabContainer ID="TabContainerReasoncodeGV" runat="server" ActiveTabIndex="0">
            <asp:TabPanel ID="TabPanelReasonList" runat="server" HeaderText="Site Master">
                <ContentTemplate>
                    <center>
                        <asp:HiddenField ID="HiddenField1" runat="server" />
                        <asp:HiddenField ID="HiddenField2" runat="server" ClientIDMode="Static" />
                        <br />
                        <div id="divLoading" style="display: none; top: 10px; left: 20%; height: 50%; width: 50%;"
                            class="modal" runat="server" clientidmode="Static">
                            <center>
                                <img src="../App_Themes/Blue/img/ajax-loader.gif" alt="" style="top: 0%; margin-top: 1%" />
                                <br />
                                <br />
                                <b>Please Wait...</b>
                            </center>
                            </div>
                        <table class="tableForm" id="tblAddAdrs" runat="server">
                            <tr>
                                <td>
                                    <req><asp:Label Id="lblprojecttype" runat="server" Text="Project Type"/></req>
                                    :
                                </td>
                                <td style="text-align: left">
                                    <asp:DropDownList ID="ddlprojecttype" Width="200px" runat="server" DataTextField="Name" DataValueField="ID">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <req><asp:Label Id="lblsitecode" runat="server" Text="Site Code"/></req>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtsitecode" Width="200px" runat="server" MaxLength="10"
                                        ClientIDMode="Static"></asp:TextBox>

                                    <%-- onchange="CheckDuplicateSiteCode();"--%>
                                </td>
                                <td>
                                    <req><asp:Label Id="lblsitename" runat="server" Text="Site Name"/></req>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtsitename" Width="200px" runat="server" MaxLength="25"
                                        ClientIDMode="Static"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <req><asp:Label Id="lblLatitude" runat="server" Text="Latitude"/></req>
                                    :
                                </td>
                                <td style="text-align: left">
                                    <asp:TextBox ID="txtLatitude" Width="200px" runat="server" MaxLength="4"
                                        ClientIDMode="Static"></asp:TextBox>
                                </td>
                                <td>
                                    <req><asp:Label Id="lblLongitude" runat="server" Text="Longitude"/></req>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtLongitude" Width="200px" runat="server" MaxLength="4"
                                        ClientIDMode="Static"></asp:TextBox>
                                </td>
                                <td>
                                    <req><asp:Label Id="lblAccessRequirement" runat="server" Text="Access Requirement"/></req>
                                    :
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAccessRequirement" Width="200px" runat="server" MaxLength="50"
                                        ClientIDMode="Static"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <req><asp:Label Id="Label1" runat="server" Text="Active"/></req>
                                    :</td>
                                <td style="text-align: left">
                                    <obout:OboutRadioButton ID="rbtnYes" runat="server" Text="Yes" GroupName="rbtnActive"
                                        Checked="True" FolderStyle="">
                                    </obout:OboutRadioButton>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <obout:OboutRadioButton ID="rbtnNo" runat="server" Text="No" GroupName="rbtnActive"
                                        FolderStyle="">
                                    </obout:OboutRadioButton>
                                </td>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td style="text-align: right">
                                    <input type="button" id="btnSave" title="Save" runat="server" value="Save" onclick="SaveContactDetail();" />
                                </td>
                            </tr>

                        </table>
                        <asp:HiddenField ID="hdnContactID" runat="server" />
                        <table class="gridFrame" width="90%">
                            <tr>
                                <td>
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="text-align: left;">
                                                <a id="headerText">
                                                    <asp:Label ID="lblAddressList" runat="server" Text="Site Details" /></a>
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
                                <%--<td style="text-align: right;">
                                    <input type="button" id="btnSubmit" title="Save" runat="server" value="Submit" onclick="GetContactPerson();" />
                                </td>--%>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <obout:Grid ID="gvContactPerson" runat="server" AutoGenerateColumns="False" AllowAddingRecords="False"
                                        AllowFiltering="True" AllowGrouping="True" AllowMultiRecordSelection="False" OnRebind="gvContactPerson_Rebind"
                                        AllowRecordSelection="true" Width="100%" AllowColumnResizing="true" AllowColumnReordering="true">
                                        <Columns>
                                            <obout:Column HeaderText="Edit" DataField="ID" Width="5%" Align="center" HeaderAlign="center">
                                                <TemplateSettings TemplateId="TemplateEdit" />
                                            </obout:Column>
                                            <%-- <obout:Column DataField="ID" HeaderText="ID" Visible="false">
                                    </obout:Column>--%>
                                            <obout:Column DataField="ProjectTypenm" HeaderText="Project Type" Width="15%">
                                            </obout:Column>
                                            <obout:Column DataField="SiteCode" HeaderText="Site Code" Width="15%">
                                            </obout:Column>
                                            <obout:Column DataField="SiteName" HeaderText="Site Name" Width="15%">
                                            </obout:Column>
                                            <obout:Column DataField="Latitude" HeaderText="Latitude" Width="7%">
                                            </obout:Column>
                                            <obout:Column DataField="Longitude" HeaderText="Longitude" Width="7%">
                                            </obout:Column>
                                            <obout:Column DataField="AccessRequirement" HeaderText="Access Requirement" Width="15%">
                                            </obout:Column>
                                            <obout:Column DataField="Active" HeaderText="Active" Width="10%">
                                            </obout:Column>


                                        </Columns>
                                        <Templates>
                                            <obout:GridTemplate runat="server" ID="TemplateEdit">
                                                <Template>
                                                    <asp:ImageButton ID="imgBtnEdit1" runat="server" ImageUrl="../App_Themes/Blue/img/Edit16.png"
                                                        ToolTip='<%# (Container.Value) %>' CausesValidation="false"  OnClick="imgBtnEdit1_Click"
                                                        />
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
            var ddlprojectype = document.getElementById("<%= ddlprojecttype.ClientID %>");
            var txtsitecode = document.getElementById("<%= txtsitecode.ClientID %>");
            var txtsitename = document.getElementById("<%= txtsitename.ClientID %>");
            var txtLatitude = document.getElementById("<%= txtLatitude.ClientID %>");
            var txtLongitude = document.getElementById("<%= txtLongitude.ClientID %>");
            var txtAccessRequirement = document.getElementById("<%= txtAccessRequirement.ClientID %>");
            var hdnstate = document.getElementById('hdnstate');
            if (ddlprojectype.value == "0" || ddlprojectype.value == "") {
                showAlert("Select Project Type", "Error");
                ddlprojectype.focus();
            }
            else if (txtsitecode.value == "") {
                showAlert("Enter Site Code", "Error");
                txtsitecode.focus();
            } else if (txtsitename.value == "") {
                showAlert("Enter Site Name", "Error");
                txtsitename.focus();
            }
            else if (txtLongitude.value == "") {
                showAlert("Enter Longitude", "Error");
                txtLongitude.focus();
            }
            else if (txtLatitude.value == "") {
                showAlert("Enter Latitude", "Error");
                txtLatitude.focus();
            }
            else if (txtAccessRequirement.value == "") {
                showAlert("Enter Access Requirement", "Error");
                txtAccessRequirement.focus();
            }
            else {

                var siteDetails = new Object();
                siteDetails.projectype = document.getElementById("<%= ddlprojecttype.ClientID %>").value;
                siteDetails.sitecode = txtsitecode.value.toString();
                siteDetails.sitename = txtsitename.value.toString();
                siteDetails.Longitude =txtLongitude.value.toString();
                siteDetails.Latitude = txtLatitude.value.toString();
                siteDetails.AccessRequirement = txtAccessRequirement.value.toString();
                var activeyesno = "Yes";
                if (document.getElementById("<%= rbtnYes.ClientID %>").checked) {
                    siteDetails.Active = "Yes";
                }
                else {
                    siteDetails.Active = "No";
                }
                LoadingOn(true);
              //  PageMethods.WMSaveRequestHead(siteDetails, hdnstate.value, WMSaveContact_onSuccessed, WMSaveContact_onFailed);
                 PageMethods.WMSaveRequestHead(siteDetails, hdnstate.value, WMSaveContact_onSuccessed, WMSaveContact_onFailed);
            }

            function WMSaveContact_onSuccessed(result) {
                if (result == "Some error occurred" || result == "") { LoadingOff(); showAlert("Error occurred", "Error", '#'); }
                else if (result == "Site Already Available") { LoadingOff(); showAlert("Site Already Available", "Error", '#'); }
                else if (result == "Site Details Saved Successfully") {
                    LoadingOff();                  
                  
                    txtsitecode.value = "";
                    txtsitename.value = "";
                    txtLatitude.value = "";
                    txtLongitude.value = "";
                    txtAccessRequirement.value = "";
                    document.getElementById("<%= ddlprojecttype.ClientID %>").value= "0";
                    txtsitecode.value = "";    
                    //document.getElementById("<%= rbtnYes.ClientID %>").checked = true;
                     showAlert(result, "info","../PowerOnRent/SitecodeMaster.aspx");
                    gvContactPerson.refresh();                   
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
                        window.opener.AfterProjectTypeSelect(hdnSearchAddressID.value, hdnprojetnm.value);
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

    </script>
</asp:Content>
