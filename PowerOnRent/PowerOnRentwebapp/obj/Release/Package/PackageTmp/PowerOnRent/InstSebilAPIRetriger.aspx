<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/CRM.Master" AutoEventWireup="true" CodeBehind="InstSebilAPIRetriger.aspx.cs" Inherits="PowerOnRentwebapp.PowerOnRent.InstSebilAPIRetriger" Theme="Blue" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="obout" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Territory/UC_Territory.ascx" TagName="UC_Territory" TagPrefix="uc1" %>
<%@ Register Src="../Address/UCAddress.ascx" TagName="UCAddress" TagPrefix="uc7" %>
<%@ Register Src="../CommonControls/UC_Date.ascx" TagName="UC_Date" TagPrefix="uc2" %>
<%@ Register Src="../CommonControls/UCFormHeader.ascx" TagName="UCFormHeader" TagPrefix="uc4" %>
<%@ Register Src="../CommonControls/UCToolbar.ascx" TagName="UCToolbar" TagPrefix="uc5" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPHolder_FormHeader" runat="server">
     <uc4:UCFormHeader ID="UCFormHeader1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHolder_Form" runat="server">
     <asp:TabContainer ID="TabContainerUserCreation1" runat="server" ActiveTabIndex="0">
            <asp:TabPanel ID="TabPanelUsersList" runat="server" HeaderText="Siebel Re-Trigger">
                <ContentTemplate>
                    <center>
                        <table style="width: 100%">
                            <tr>
                                <td style="width: 10%"></td>
                                <td style="width: 80%">
                                    <table class="gridFrame" width="100%" style="margin: 3px 3px 3px 3px;">
                                        <tr>
                                            <td>
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td style="text-align: left;">
                                                            <asp:Label ID="lblheader" CssClass="headerText" runat="server" Text="Installation Order Siebel Re-Trigger "></asp:Label>
                                                        </td>
                                                        <td style="width: 50%; text-align: right">
                                                            <input type="button" id="btnretrigger" runat="server" value="Re-Trigger Service" onclick="ReTrigger()" />
                                                        </td>
                                                        <td style="text-align: right;">
                                                            <%--     <input type="button" runat="server" value="Submit" id="btnSubmitProductSearch1" onclick="selectedRec();" />--%>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <!-- oBout Grid Template -->
                                                <div class="wmsThemeMobileGridMain">
                                                    <div class="wmsThemeMobileGridHolder">
                                                        <div class="wmsThemeMobileGrid">
                                                            <!-- obout Grid -->
                                                            <obout:Grid runat="server" ID="GrdSebilReTrigger" AutoGenerateColumns="False" AllowAddingRecords="False"
                                                                AllowFiltering="True" AllowGrouping="True" AllowMultiRecordSelection="false"
                                                                AllowRecordSelection="true" Width="100%" AllowColumnResizing="true" AllowColumnReordering="true" OnRebind="GrdSebilReTrigger_Rebind">
                                                                <ScrollingSettings ScrollHeight="250" />
                                                                <Columns>
                                                                    <obout:Column DataField="ID" HeaderText="ID" HeaderAlign="center" Align="center" Width="5%" Wrap="true" ShowFilterCriterias="false"></obout:Column>
                                                                     <obout:Column DataField="ServiceName" HeaderText="Service Name" Align="left" HeaderAlign="left"
                                                                        Width="15%" AllowFilter="false" ParseHTML="true">
                                                                    </obout:Column>
                                                                    <obout:Column DataField="OnlineOrder" HeaderText="Online Order No." Align="left" HeaderAlign="left"
                                                                        Width="15%" AllowFilter="false" ParseHTML="true">
                                                                    </obout:Column>
                                                                    <obout:Column DataField="ReferenceID" HeaderText="ECom OrderID" Align="left" HeaderAlign="left"
                                                                        Width="15%" AllowFilter="false" ParseHTML="true">
                                                                    </obout:Column>
                                                                    <obout:Column DataField="RequestDate" HeaderText="Request Date" Align="left" HeaderAlign="left"
                                                                        Width="15%" AllowFilter="false" ParseHTML="true">
                                                                    </obout:Column>
                                                                    <obout:Column DataField="Responsestatus" HeaderText="Response Status" Align="left" HeaderAlign="left" Width="20%"
                                                                        AllowFilter="false" ParseHTML="true">
                                                                    </obout:Column>
                                                                    <obout:Column DataField="ResponceDescription" HeaderText="Response Description" Align="left" HeaderAlign="left"
                                                                        Width="25%" AllowFilter="false" ParseHTML="true">
                                                                    </obout:Column>
                                                                    <obout:Column DataField="AttempCount" HeaderText="Attempt Count" Align="left" HeaderAlign="left"
                                                                        Width="10%" AllowFilter="false" ParseHTML="true">
                                                                    </obout:Column>
                                                                       <obout:Column DataField="Retrigerflg" HeaderText="Retrigger Set" Align="left" HeaderAlign="left"
                                                                        Width="10%" AllowFilter="false" ParseHTML="true">
                                                                    </obout:Column>
                                                                </Columns>
                                                            </obout:Grid>
                                                            <!-- obout Grid -->
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                        </tr>
                                    </table>

                                </td>
                                <td style="width: 10%"></td>
                            </tr>
                        </table>
                    </center>
                </ContentTemplate>
            </asp:TabPanel>
        </asp:TabContainer>

    <asp:HiddenField ID="hdnPartSelectedRec" runat="server" ClientIDMode="Static" />

     <script type="text/javascript">

          window.onload = function ()
          {
            oboutGrid.prototype.restorePreviousSelectedRecord = function ()
            {
                return;
            }
            oboutGrid.prototype.markRecordAsSelectedOld = oboutGrid.prototype.markRecordAsSelected;
            oboutGrid.prototype.markRecordAsSelected = function (row, param2, param3, param4, param5) {
                if (row.className != this.CSSRecordSelected) {
                    this.markRecordAsSelectedOld(row, param2, param3, param4, param5);
                }
                else
                {
                    var index = this.getRecordSelectionIndex(row);
                    if (index != -1) {
                        this.markRecordAsUnSelected(row, index);
                    }
                }
                SelectedPrdRec();
            }
         }   

         function SelectedPrdRec()
        {
            hdnPartSelectedRec.value = "";
            var record = GrdSebilReTrigger.PageSelectedRecords[0];
            for (var i = 0; i < GrdSebilReTrigger.PageSelectedRecords.length; i++)
            {
                var record = GrdSebilReTrigger.PageSelectedRecords[i];
                hdnPartSelectedRec.value = record.ID;
            }
         }

         function ReTrigger() {
             debugger;
             var APILogID = hdnPartSelectedRec.value;
             PageMethods.ReTriggerSebilService(APILogID, OnSuccessupdatesrflag, null);
         }

         function OnSuccessupdatesrflag(result)
        {
            var Ids = document.getElementById("hdnPartSelectedRec").value;
            if (result == "Success")
            {
                showAlert("Retrigger set successfully", "info", "#");   
                GrdSebilReTrigger.refresh();
            }
            else
            {
                showAlert("Error occured.", "Error", "#");
            }
        }

        function gridrefresh()
        {
            
            showAlert("Order ReTrigger successfully", "info", "#");                   
        }

       </script>
</asp:Content>
