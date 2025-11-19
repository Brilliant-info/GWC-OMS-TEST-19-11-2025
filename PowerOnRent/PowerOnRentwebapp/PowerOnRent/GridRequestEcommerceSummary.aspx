<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GridRequestEcommerceSummary.aspx.cs" Inherits="PowerOnRentwebapp.PowerOnRent.GridRequestEcommerceSummary" %>


<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
   <div>
            <center>
                <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
                </asp:ScriptManager>
                <table>
                    <tr>
                        <td>
                             <obout:Grid ID="GVEcomRequest" runat="server" AllowAddingRecords="false" AutoGenerateColumns="false"
                                                AllowColumnResizing="true" AllowFiltering="true" AllowManualPaging="true" AllowColumnReordering="true"
                                                AllowMultiRecordSelection="false" AllowRecordSelection="true" AllowGrouping="true"
                                                Width="100%" Serialize="true" CallbackMode="true"  OnRebind="GVEcomRequest_Rebind"
                                                PageSize="10" AllowPaging="true" AllowPageSizeSelection="true">
                               <%-- AllowAddingRecords="false" AutoGenerateColumns="false"
                                AllowGrouping="true" Serialize="true" CallbackMode="true" AllowRecordSelection="true"
                                AllowMultiRecordSelection="true" AllowColumnReordering="true" AllowFiltering="true"
                                Width="100%" PageSize="10"--%>
                                <ClientSideEvents ExposeSender="true" />
                                <Columns>
                                    <%--  <obout:Column DataField="PRH_ID" Visible="false" Width="0px">
                                </obout:Column>--%>
                                    <obout:Column DataField="SiteID" Visible="false" Width="0px">
                                    </obout:Column>
                                    <obout:Column DataField="OrderNo" HeaderText="Request No." HeaderAlign="center" Align="center" Width="15%"></obout:Column>

                                    <obout:Column DataField="SiteName" HeaderText="Department" HeaderAlign="left" Align="left"
                                        Width="10%">
                                    </obout:Column>
                                    <obout:Column DataField="ID"  HeaderAlign="center" Align="center" Visible="false"
                                        Width="0%">
                                    </obout:Column>
                                    <obout:Column DataField="Orderdate" HeaderText="Request Date" HeaderAlign="left"
                                        Align="left" DataFormatString="{0:dd-MMM-yyyy}" Width="10%">
                                    </obout:Column>
                                    <obout:Column DataField="Deliverydate" HeaderText="Exp.Delivery Date" HeaderAlign="left"
                                        Align="left" DataFormatString="{0:dd-MMM-yyyy}" Width="10%">
                                    </obout:Column>

                                    <obout:Column DataField="Title" HeaderText="Title" HeaderAlign="left" Align="left" Visible="false"
                                        Width="15%" Wrap="true">
                                    </obout:Column> 
                                    
                                    <obout:Column DataField="OrderType" HeaderText="Order Type" HeaderAlign="left" Align="left" 
                                        Width="10%" Wrap="true">
                                    </obout:Column>

                                      <obout:Column DataField="MethodName" HeaderText="Payment Method" HeaderAlign="left" Align="left" 
                                        Width="10%" Wrap="true">
                                    </obout:Column>

                                                                       
                                   
                                    <obout:Column DataField="RequestBy" Visible="false" Width="0px">
                                    </obout:Column>
                                    <obout:Column DataField="RequestByUserName" HeaderText="Request By" HeaderAlign="left" Visible="false"
                                        Align="left" Width="10%">
                                    </obout:Column>
                                    <obout:Column DataField="StatusID" HeaderText="" Width="0px" Visible="false">
                                    </obout:Column>
                                   
                                    <obout:Column DataField="DeliveryType" HeaderText="Delivery Type" HeaderAlign="left"
                                        Align="left"  Width="8%" Visible="false" >
                                    </obout:Column>

                                    <obout:Column DataField="CustomerFirstName" HeaderText="First Name" HeaderAlign="left"
                                        Align="left" Width="8%" Wrap="true">
                                    </obout:Column>

                                     <obout:Column DataField="CustomerLastName" HeaderText="Last Name" HeaderAlign="left"
                                        Align="left" Width="8%" Wrap="true">
                                    </obout:Column>

                                     <obout:Column DataField="RequestStatus" HeaderText="Order Status" HeaderAlign="left"
                                        Align="left" Width="8%" Wrap="true">
                                    </obout:Column>

                                    <obout:Column DataField="ImgRequest" HeaderText="Request" HeaderAlign="center" Align="center"
                                        Width="7%">
                                        <TemplateSettings TemplateId="GTStatusGUIRequest" />
                                    </obout:Column>
                                    <obout:Column DataField="ImgApproval" HeaderText="Approval" HeaderAlign="center"
                                        Align="center" Width="7%">
                                        <TemplateSettings TemplateId="GTStatusGUIApproval" />
                                    </obout:Column>
                                    <obout:Column DataField="ImgIssue" HeaderText="Dispatch" HeaderAlign="center" Align="center"
                                        Width="7%">
                                        <TemplateSettings TemplateId="GTStatusGUIIssue" />
                                    </obout:Column>
                                    <%--<obout:Column DataField="ImgReceipt" HeaderText="Receipt" HeaderAlign="center" Align="center"
                                    Width="6%">
                                    <TemplateSettings TemplateId="GTStatusGUIReceipt" />
                                </obout:Column>--%>
                                    <%-- <obout:Column DataField="ImgConsumption" HeaderText="Consumption" HeaderAlign="center"
                                    Align="center" Width="9%">
                                    <TemplateSettings TemplateId="GTStatusGUIConsumption" />
                                </obout:Column>--%>
                                </Columns>
                                <Templates>
                                    <obout:GridTemplate ID="GTStatusGUIRequest" runat="server">
                                        <Template>
                                            <center>
                                                <div class='<%# ("POR" + Container.Value) %>' onclick="parent.EcomRequestOpenEntryForm('Request','<%# Container.Value %>', '<%# Container.DataItem["ID"] %>')">
                                                </div>
                                            </center>
                                        </Template>
                                    </obout:GridTemplate>
                                    <obout:GridTemplate ID="GTStatusGUIApproval" runat="server">
                                        <Template>
                                            <center>
                                                <div class='<%# ("POR" + Container.Value) %>' onclick="parent.EcomRequestOpenEntryForm('Approval','<%# Container.Value %>', '<%# Container.DataItem["ID"] %>')">
                                                </div>
                                            </center>
                                        </Template>
                                    </obout:GridTemplate>
                                    <obout:GridTemplate ID="GTStatusGUIIssue" runat="server">
                                        <Template>
                                            <center>
                                                <div class='<%# ("POR" + Container.Value) %>' onclick="parent.EcomRequestOpenEntryForm('Request','<%# Container.Value %>', '<%# Container.DataItem["ID"] %>')">
                                                </div>
                                            </center>
                                        </Template>
                                    </obout:GridTemplate>
                                    <obout:GridTemplate ID="GTStatusGUIReceipt" runat="server">
                                        <Template>
                                            <center>
                                                <div class='<%# ("POR" + Container.Value) %>' onclick="parent.EcomRequestOpenEntryForm('Receipt','<%# Container.Value %>', '<%# Container.DataItem["ID"] %>')">
                                                </div>
                                            </center>
                                        </Template>
                                    </obout:GridTemplate>
                                    <obout:GridTemplate ID="GTStatusGUIConsumption" runat="server">
                                        <Template>
                                            <center>
                                                <div class='<%# ("POR" + Container.Value) %>' onclick="parent.EcomRequestparent('Consumption','<%# Container.Value %>', '<%# Container.DataItem["ID"] %>')">
                                                </div>
                                            </center>
                                        </Template>
                                    </obout:GridTemplate>
                                </Templates>
                            </obout:Grid>
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="hdnSelectedEcomRec" runat="server" ClientIDMode="Static" />
            </center>
        </div>
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
                     SelectedEcomRec();
                 }
             }
             function SelectedRec() {
                 var hdnSelectedRec = document.getElementById("hdnSelectedEcomRec");
                 hdnSelectedRec.value = "";
                 if (GVEcomRequest.PageSelectedRecords.length > 0) {
                     for (var i = 0; i < GVEcomRequest.PageSelectedRecords.length; i++) {
                         var record = GVEcomRequest.PageSelectedRecords[i];
                         if (hdnSelectedRec.value != "") hdnSelectedRec.value += ',' + record.ID;
                         if (hdnSelectedRec.value == "") hdnSelectedRec.value = record.ID;
                     }

                     parent.GetSelected(hdnSelectedEcomRec.value);
                 }
             }

             function GVEcomRequest_Deselect(index) {
                 SelectedEcomRec();
             }
             function onDeselect(row) {
                 SelectedEcomRec();
             }
        </script>
    </form>
</body>
</html>
