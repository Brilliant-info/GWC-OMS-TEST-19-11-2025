<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/CRM2.Master" CodeBehind="SiteMaster.aspx.cs" Inherits="PowerOnRentwebapp.PowerOnRent.SiteMaster" EnableEventValidation="false" Theme="Blue" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="obout" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>
<%@ Register Src="../CommonControls/UCFormHeader.ascx" TagName="UCFormHeader" TagPrefix="uc1" %>
<%@ Register Src="../CommonControls/UCToolbar.ascx" TagName="UCToolbar" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <center>
        <asp:UpdateProgress ID="UpdateProgress_Contact" runat="server" AssociatedUpdatePanelID="updPnl_Contact">
            <ProgressTemplate>
                <center>
                    <div class="modal">
                        <img src="../App_Themes/Blue/img/ajax-loader.gif" style="top: 50%;" />
                    </div>
                </center>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="updPnl_Contact" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="hdnConID" runat="server" />
                <asp:HiddenField ID="hdnstate" runat="server" ClientIDMode="Static" />
                <div id="divdata" runat="server" clientidmode="Static" visible="false">

                    <table class="tableForm" id="tblAddAdrs" runat="server">
                        <tr>
                            <td>
                                <req><asp:Label Id="lblprojecttype" runat="server" Text="Project Type"/></req>
                                :
                            </td>
                            <td style="text-align: left">
                                <asp:DropDownList ID="ddlprojecttype" runat="server" DataTextField="Name" DataValueField="ID">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <req><asp:Label Id="lblsitecode" runat="server" Text="Site Code"/></req>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="txtsitecode" Width="200px" runat="server" MaxLength="100"
                                    ClientIDMode="Static" onchange="CheckDuplicateSiteCode();"></asp:TextBox>
                            </td>
                            <td>
                                <req><asp:Label Id="lblsitename" runat="server" Text="Site Name"/></req>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="txtsitename" Width="200px" runat="server" MaxLength="100"
                                    ClientIDMode="Static"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <req><asp:Label Id="lblLatitude" runat="server" Text="Latitude"/></req>
                                :
                            </td>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtLatitude" Width="200px" runat="server" MaxLength="100"
                                    ClientIDMode="Static"></asp:TextBox>
                            </td>
                            <td>
                                <req><asp:Label Id="lblLongitude" runat="server" Text="Longitude"/></req>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="txtLongitude" Width="200px" runat="server" MaxLength="100"
                                    ClientIDMode="Static"></asp:TextBox>
                            </td>
                            <td>
                                <req><asp:Label Id="lblAccessRequirement" runat="server" Text="Access Requirement"/></req>
                                :
                            </td>
                            <td>
                                <asp:TextBox ID="txtAccessRequirement" Width="200px" runat="server" MaxLength="100"
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
                </div>
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
                        <td style="text-align: right;">
                            <input type="button" id="btnSubmit" title="Save" runat="server" value="Submit" onclick="GetContactPerson();" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <obout:Grid ID="gvContactPerson" runat="server" AutoGenerateColumns="False" AllowAddingRecords="False"
                                AllowFiltering="True" AllowGrouping="True" AllowMultiRecordSelection="False" OnRebind="gvContactPerson_Rebind"
                                AllowRecordSelection="true" Width="100%" AllowColumnResizing="true" AllowColumnReordering="true">
                                <Columns>
                                    <obout:Column HeaderText="Edit" Visible="false" DataField="ID" Width="5%" Align="center" HeaderAlign="center">
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


                                </Columns>
                                <Templates>
                                    <obout:GridTemplate runat="server" ID="TemplateEdit">
                                        <Template>
                                            <asp:ImageButton ID="imgBtnEdit1" runat="server" ImageUrl="../App_Themes/Blue/img/Edit16.png"
                                                ToolTip='<%# (Container.Value) %>' CausesValidation="false" />
                                            <%--OnClick="imgBtnEdit_OnClick"--%>
                                        </Template>
                                    </obout:GridTemplate>
                                </Templates>
                            </obout:Grid>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
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
            var ddlprojectype = document.getElementById("<%= ddlprojecttype.ClientID %>");
            var txtsitecode = document.getElementById("<%= txtsitecode.ClientID %>");
            var txtsitename = document.getElementById("<%= txtsitename.ClientID %>");
            var txtLatitude = document.getElementById("<%= txtLatitude.ClientID %>");
            var txtLongitude = document.getElementById("<%= txtLongitude.ClientID %>");
            var txtAccessRequirement = document.getElementById("<%= txtAccessRequirement.ClientID %>");

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

                var site = new Object();
                site.projectype = ddlprojectype.value;
                site.sitecode = txtsitecode.value.toString();
                site.sitename = txtsitename.value.toString();
                site.Longitude = txtLatitude.value.toString();
                site.Latitude = txtLongitude.value.toString();
                site.AccessRequirement = txtAccessRequirement.value.toString();
                site.Active = "Yes";
                PageMethods.WMSaveRequestHead(site, hdnstate.value, WMSaveContact_onSuccessed, WMSaveContact_onFailed);

            }

            function WMSaveContact_onSuccessed(result) {
                if (result == "Some error occurred" || result == "") { showAlert("Error occurred", "Error", '#'); }
                else if (result == "Site Details Saved Successfully") {
                    showAlert(result, "info");
                    txtsitecode.value = "";
                    txtsitename.value = "";
                    txtLatitude.value = "";
                    txtLongitude.value = "";
                    txtAccessRequirement.value = "";

                    txtsitecode.value = "";

                    gvContactPerson.refresh();
                    self.close();
                }
            }
            function WMSaveContact_onFailed() {
                showAlert("Error occurred", "Error", "#"); self.close();
            }
        }

        function GetContactPerson() {
            var hdnSelectedRec = document.getElementById('hdnSelectedRec');
            if (hdnSelectedRec.value == "") {
                showAlert("Select Atleast One Site...", "Error");
            } else {
                var SelCon = hdnSelectedRec.value;
                var count = (SelCon.match(/,/g) || []).length;
                console.log(count);
                if (count >= 1) {
                    showAlert("Select Only One Site", "Error", "#");
                } else {
                    var hdnsiteid = window.opener.document.getElementById("hdnsiteid");
                    var hdnsitecode1 = window.opener.document.getElementById("hdnsitecode1");
                    var hdnsitenm = window.opener.document.getElementById("hdnsitenm");
                    var hdnlatitude = window.opener.document.getElementById("hdnlatitude");
                    var hdnlogitude = window.opener.document.getElementById("hdnlogitude");
                    var hdnaccessspe = window.opener.document.getElementById("hdnaccessspe");


                    hdnsiteid.value = "";
                    hdnsitenm.value = "";
                    hdnlatitude.value = "";
                    hdnlogitude.value = "";
                    hdnaccessspe.value = "";

                    if (gvContactPerson.SelectedRecords.length > 0) {
                        for (var i = 0; i < gvContactPerson.SelectedRecords.length; i++) {
                            var record = gvContactPerson.SelectedRecords[i];
                            if (hdnsiteid.value != "") {
                                hdnsiteid.value += ',' + record.ID;
                                hdnsitecode1.value += ',' + record.SiteCode;
                                hdnsitenm.value += ',' + record.SiteName;
                                hdnlatitude.value += ',' + record.Latitude;
                                hdnlogitude.value += ',' + record.Longitude;
                                hdnaccessspe.value += ',' + record.AccessRequirement;
                            }
                            if (hdnsiteid.value == "") {
                                hdnsiteid.value = record.ID;
                                hdnsitecode1.value = record.SiteCode;
                                hdnsitenm.value = record.SiteName;
                                hdnlatitude.value = record.Latitude;
                                hdnlogitude.value = record.Longitude;
                                hdnaccessspe.value = record.AccessRequirement;
                                // hdnSearchAddress.value = record.AddressLine1;
                                //hdnSearchAddress.value = record.AddressLine1 + ', Contact Name:' + record.ContactName + ',Mobile:' + record.MobileNo + ', Email:' + record.ContactEmail;
                            }
                        }
                        window.opener.AfterSiteSelect(hdnsiteid.value, hdnsitecode1.value, hdnsitenm.value, hdnlatitude.value, hdnlogitude.value, hdnaccessspe.value);
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
