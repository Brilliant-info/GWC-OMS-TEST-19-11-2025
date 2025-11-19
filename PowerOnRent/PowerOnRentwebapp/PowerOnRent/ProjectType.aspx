<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/CRM2.Master" CodeBehind="ProjectType.aspx.cs" Inherits="PowerOnRentwebapp.PowerOnRent.ProjectType" EnableEventValidation="false" Theme="Blue" %>

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
                                <asp:TextBox ID="txtprojectype" Width="200px" runat="server" MaxLength="100"
                                    ClientIDMode="Static" onchange="CheckDuplicateAddress();"></asp:TextBox>
                            </td>
                            <td>
                                <req><asp:Label Id="lblactive" runat="server" Text="Active"/></req>
                                :
                            </td>
                            <td>
                                <obout:OboutRadioButton ID="rbtnYes" runat="server" Text="Yes" GroupName="rbtnActive"
                                    Checked="True" FolderStyle="">
                                </obout:OboutRadioButton>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <obout:OboutRadioButton ID="rbtnNo" runat="server" Text="No" GroupName="rbtnActive"
                                        FolderStyle="">
                                    </obout:OboutRadioButton>
                            </td>
                        </tr>

                        <tr>
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
                <table class="gridFrame" width="80%">
                    <tr>
                        <td>
                            <table style="width: 100%">
                                <tr>
                                    <td style="text-align: left;">
                                        <a id="headerText">
                                            <asp:Label ID="lblAddressList" runat="server" Text="Project Type" /></a>
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
                                    <obout:Column HeaderText="Edit" DataField="ID" Width="3%" Visible="false" Align="center" HeaderAlign="center">
                                        <TemplateSettings TemplateId="TemplateEdit" />
                                    </obout:Column>
                                    <%-- <obout:Column DataField="ID" HeaderText="ID" Visible="false">
                                    </obout:Column>--%>
                                    <obout:Column DataField="Projecttype" HeaderText="Project Type" Width="15%">
                                    </obout:Column>
                                    <obout:Column DataField="Active" HeaderText="Active" Width="8%">
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
            var txtprojectype = document.getElementById("<%= txtprojectype.ClientID %>");
            if (txtprojectype.value == "") {
                showAlert("Enter Project Type", "Error");
                txtprojectype.focus();
            } else {
                var activeyesno = "Yes";
                var hdnstate = document.getElementById('hdnstate');
                //var AddressInfo = new Object();
                //AddressInfo.AddressLine1 = document.getElementById("txtAddress").value;
                //AddressInfo.State = document.getElementById("ddlState").value;
                //AddressInfo.City = txtcity.value.toString();
                if (document.getElementById('rbtnYes').checked) {
                    activeyesno = "Yes";
                }
                else {
                        activeyesno = "No";
                }


                PageMethods.WMSaveRequestHead(txtprojectype.value, activeyesno, hdnstate.value, WMSaveContact_onSuccessed, WMSaveContact_onFailed);

            }

            function WMSaveContact_onSuccessed(result) {
                if (result == "Some error occurred" || result == "") { showAlert("Error occurred", "Error", '#'); }
                else if (result == "Project Type Already Available") {
                    showAlert("Project Type Already Available", "Error", '#');
                    document.getElementById("<%= txtprojectype.ClientID %>").value = "";
                    txtprojectype.focus();
                }
                else if (result == "Project Type Saved Successfully") {
                    showAlert(result, "info");
                    txtprojectype.value = "";
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
