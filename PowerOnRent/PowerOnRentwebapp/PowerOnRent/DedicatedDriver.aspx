<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DedicatedDriver.aspx.cs" EnableEventValidation="false" MasterPageFile="~/MasterPage/CRM.Master" Theme="Blue" Inherits="PowerOnRentwebapp.PowerOnRent.DedicatedDriver" %>


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
    <asp:HiddenField ID="hdnConID" runat="server" />
    <asp:HiddenField ID="hdnstate" runat="server" ClientIDMode="Static" />
    <center>
        <asp:TabContainer ID="TabContainerReasoncodeGV" runat="server" ActiveTabIndex="0">
            <asp:TabPanel ID="TabPanelReasonList" runat="server" HeaderText="Dedicated Driver">
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

                        <table class="tableForm" style="width: 70%" id="tblAddAdrs" runat="server">
                            <tr runat="server">
                                <td runat="server">
                                    <req><asp:Label Id="lblprojecttype" runat="server" Text="Customer"/>
</req>
                                    :
                                </td>
                                <td style="text-align: left" runat="server">
                                    <asp:DropDownList runat="server" ID="ddlCompany1" Width="182px" DataTextField="Name"
                                        DataValueField="ID" ClientIDMode="Static" onchange="GetDept(this);">
                                    </asp:DropDownList>
                                </td>
                                <td runat="server">
                                    <req><asp:Label Id="lblactive" runat="server" Text="Department"/>

</req>
                                    :
                                </td>
                                <td runat="server">
                                    <asp:DropDownList runat="server" ID="ddlSites" Width="182px" DataTextField="Territory"
                                        DataValueField="ID" ClientIDMode="Static" onchange="GetDeptID(this);">
                                    </asp:DropDownList>
                                </td>
                                <td runat="server">
                                    <req><asp:Label Id="Label1" runat="server" Text="Select Driver"/></req>
                                    :
                                </td>

                                <td style="text-align: left" runat="server">
                                    <asp:TextBox ID="txtprojectype" Width="200px" runat="server" MaxLength="100" Enabled="false"
                                        ClientIDMode="Static" onchange="CheckDuplicateAddress();"></asp:TextBox>
                                    <img id="img3" src="../App_Themes/Grid/img/search.jpg" title="Search Location" style="cursor: pointer;" onclick="openDriver('0')" />
                                </td>


                            </tr>

                            <tr runat="server">
                                <td runat="server"></td>
                                <td runat="server"></td>
                                <td runat="server"></td>
                                <td runat="server"></td>
                                <td runat="server"></td>
                                <td runat="server"></td>
                                <td style="text-align: right" runat="server">
                                    <input type="button" id="btnSave" title="Save" runat="server" value="Save" onclick="SaveContactDetail();" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
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
                                                    <asp:Label ID="lblAddressList" runat="server" Text="Dedicated Driver List" /></a>
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
                                        AllowFiltering="True" AllowGrouping="True"  OnRebind="gvContactPerson_Rebind" AllowMultiRecordSelection="False" Width="100%" AllowColumnReordering="True">
                                        <%--OnRebind="gvContactPerson_Rebind"--%>

                                        <Columns>


                                            <obout:Column HeaderText="Edit" DataField="ID" Width="3%" Align="center" HeaderAlign="center" Index="0" TemplateId="TemplateEdit">
                                                <TemplateSettings TemplateId="TemplateEdit" />
                                            </obout:Column>
                                            
                                            <obout:Column DataField="Companynm" HeaderText="Customer" Width="15%" Index="1">
                                            </obout:Column>
                                            <obout:Column DataField="Department" HeaderText="Department" Width="15%" Index="2">
                                            </obout:Column>
                                            <obout:Column DataField="Drivernm" HeaderText="Driver Name" Width="15%" Index="3">
                                            </obout:Column>


                                        </Columns>
                                        <Templates>
                                            <obout:GridTemplate runat="server" ID="TemplateEdit">
                                                <Template> 
                                                    <asp:ImageButton ID="imgBtnEdit1" runat="server" ImageUrl="../App_Themes/Blue/img/Edit16.png"
                                                        CommandArgument='<%# (Container.Value) %>'   OnCommand="imgBtnEdit1_Command"
                                                        ToolTip='<%# (Container.Value) %>' CausesValidation="false" />
                                                    <%--OnClick="imgBtnEdit_OnClick"  '<%#Eval("Companynm")%>'   OnClick="imgBtnEdit1_Click" --%>
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
     <asp:HiddenField ID="hdneditid" runat="server" ClientIDMode="Static" />
     <asp:HiddenField ID="hdnstateNM" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnSelectedCompany" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnsiteid" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnSelectedRec" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnSelectedCompanynew" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnSelectedRecID" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnSelectedRecNM" runat="server" ClientIDMode="Static" />
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

        function AfterDrselected(Id, NM) {
            var RecID = document.getElementById("hdnSelectedRecID");
            var RecNm = document.getElementById("hdnSelectedRecNM");
            RecID.value = Id;
            RecNm.value = NM;
            var txtprojectype = document.getElementById("<%= txtprojectype.ClientID %>");
            txtprojectype.value = NM;
        }



        function openDriver(sequence) {
            var ddlCompany = document.getElementById("<%=ddlCompany1.ClientID %>");
            var hdnselectedCompany = document.getElementById("<%=hdnSelectedCompany.ClientID %>");
            hdnselectedCompany.value = ddlCompany.value;
           // alert(hdnselectedCompany.value);
            //  window.open('../PowerOnRent/DedicatedDriverList.aspx?cid =' + hdnselectedCompany.value, null, 'height=500px, width=825px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=300; screenY=50');
              //window.open('../PowerOnRent/Approval.aspx?REQID=' + result + '&ST=3&APL=' + hdnApprovalId.value + '', null, 'height=180px, width=900px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=300; screenY=50');
            window.open('../PowerOnRent/DedicatedDriverList.aspx?Id='+hdnselectedCompany.value + '&comid =0', null, 'height=500px, width=825px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=300; screenY=50');
            //   window.open('../PowerOnRent/ChangeStatus.aspx?Id=' + OrdNo, null, 'height=170px, width=600px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=300; screenY=50');
        }

        function GetDeptnew() {
            var ddlcompanynew = document.getElementById("<%=ddlCompany1.ClientID %>");
            var hdnSelectedCompanynew = document.getElementById("<%=hdnSelectedCompanynew.ClientID %>");
            hdnSelectedCompanynew.value = ddlcompanynew.value;
            var Cmpny = ddlcompanynew.value;
            PageMethods.WMGetDeptnew(Cmpny, OnSuccessCompanynew, null);
        }
        function OnSuccessCompanynew(result) {
            ddldepartment = document.getElementById("<%=ddlSites.ClientID %>");
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


        function GetDeptID() {
            var ddlSites = document.getElementById("<%=ddlSites.ClientID %>");
            var hdnsiteid = document.getElementById("<%=hdnsiteid.ClientID %>");
            hdnsiteid.value = ddlSites.value;
            var txtprojectype = document.getElementById("<%= txtprojectype.ClientID %>");
            txtprojectype.value = "";
        }

        function GetDept() {
            var ddlCompany = document.getElementById("<%=ddlCompany1.ClientID %>");
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

            var hdnselectedCompany = document.getElementById("<%=hdnSelectedCompany.ClientID %>");

            if (hdnselectedCompany.value == "" || hdnselectedCompany.value == "0") {
                showAlert("Select Customer", "Error");
            }
            else if (hdnsiteid.value == "" || hdnsiteid.value == "0") {
                showAlert("Select Department", "Error");
            }
            else if (txtprojectype.value == "") {
                showAlert("Select Dedicated Driver", "Error");
                txtprojectype.focus();
            }

            else {

                var RecID = document.getElementById("hdnSelectedRecID");
                var hdnstateNM = document.getElementById("<%=hdnstateNM.ClientID %>");
                if (hdnstateNM.value == "Edit") {                                        

                    PageMethods.WMSaveRequestHead("Edit",  RecID.value, hdnselectedCompany.value, hdnsiteid.value, WMSaveContact_onSuccessed, WMSaveContact_onFailed);
                }
                else {
                         PageMethods.WMSaveRequestHead("Save", RecID.value, hdnselectedCompany.value, hdnsiteid.value, WMSaveContact_onSuccessed, WMSaveContact_onFailed);
                }

                //LoadingOn(true);
              

            }
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

            if (result == "1") {

                showAlert("Driver Already Allocated For This Department.", "Error", '#');
            }
            else {
                showAlert("Driver Allocation Saved Successfully.", "info");
                txtprojectype.value = "";
                document.getElementById("ddlCompany1").selectedIndex = 0;
                var hdnselectedCompany = document.getElementById("<%=hdnSelectedCompany.ClientID %>");
                hdnselectedCompany.value = "0";
                PageMethods.WMGetDept(0, OnSuccessCompany, null);
                var hdnsiteid = document.getElementById("<%=hdnsiteid.ClientID %>");
                hdnsiteid.value = "0";
                document.getElementById('<%= this.ddlCompany1.ClientID %>').disabled = false;
                document.getElementById('<%= this.ddlSites.ClientID %>').disabled = false;
                var hdnstateNM = document.getElementById("<%=hdnstateNM.ClientID %>");
                hdnstateNM.value = "";
                gvContactPerson.refresh();
            }

                <%--if (result == "Some error occurred" || result == "") { LoadingOff(); showAlert("Error occurred", "Error", '#'); }
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
                }--%>
        }
        function WMSaveContact_onFailed() {
            showAlert("Error occurred", "Error", "#"); self.close();
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
