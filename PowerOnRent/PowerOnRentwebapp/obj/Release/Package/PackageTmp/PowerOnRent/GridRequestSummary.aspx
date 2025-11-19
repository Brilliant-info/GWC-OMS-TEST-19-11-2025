<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GridRequestSummary.aspx.cs" Inherits="PowerOnRentwebapp.PowerOnRent.GridRequestSummary" %>


<%--EnableEventValidation="false"--%>
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
                        <table style="width: 100%">
                            <tr>
                                <td style="text-align: left;width:50%">
                                   
                             <%--    <input type="button" id="btnadvancesearch" title="Advance Search" value="Advance Search" runat="server" onclick="AdvanceSearchOrder()" />--%>
                                </td>
                                <td style="text-align: right;width:50%">
                                      <asp:Label ID="lblselectord" runat="server" CssClass="headerText" Text="Select Order"></asp:Label>   <%--CssClass="headerText"--%>
                                     <asp:DropDownList ID="ddlSelectOrderTypenew" runat="server" ClientIDMode="Static" onchange="updateOrderType(this);">   <%--  OnSelectedIndexChanged="ddlSelectOrderTypenew_SelectedIndexChanged" AutoPostBack="True"  --%>
                        <asp:ListItem Text="Normal Order" Value="Normal"></asp:ListItem>
                        <asp:ListItem Text="E-Commerce Order" Value="Ecommerce"></asp:ListItem>
                    </asp:DropDownList>
                                 </td>
                            </tr>
                    </table>
                    </tr>
                    <tr>
                        <td>
                            <obout:Grid ID="GVRequest" runat="server" AllowAddingRecords="false" AutoGenerateColumns="false"
                                AllowColumnResizing="true" AllowFiltering="true" AllowManualPaging="true" AllowColumnReordering="true"
                                AllowMultiRecordSelection="false" AllowRecordSelection="true" AllowGrouping="true"
                                Width="100%" Serialize="true" CallbackMode="true" OnRebind="GVRequest_OnRebind"
                                PageSize="10" AllowPaging="true" AllowPageSizeSelection="true"  >
                          <%--  <ScrollingSettings  ScrollWidth="1400"/>--%>
                                <%--  <ScrollingSettings  ScrollWidth="100%"/>--%>
                                
                                 <ScrollingSettings  ScrollHeight="100%" ScrollWidth="100%" />

                                <%-- AllowAddingRecords="false" AutoGenerateColumns="false"
                                AllowGrouping="true" Serialize="true" CallbackMode="true" AllowRecordSelection="true"
                                AllowMultiRecordSelection="true" AllowColumnReordering="true" AllowFiltering="true"
                                Width="100%" PageSize="10"--%>
                                <ClientSideEvents ExposeSender="true" />
                                <Columns>
                                    <%--  <obout:Column DataField="PRH_ID" Visible="false" Width="0px">
                                </obout:Column>--%>

                                    <%--0--%>
                                    <obout:Column DataField="SiteID" Visible="false" Width="0px">
                                    </obout:Column>
                                    <%--1--%>
                                    <obout:Column DataField="OrderNo" HeaderText="Request No." HeaderAlign="left" Align="left" Width="10%"></obout:Column>
                                    
                                    <%--2--%>
                                    <obout:Column DataField="EcomOrderNumber" HeaderText="Web Order No." HeaderAlign="center" Align="left" Width="12%"></obout:Column>

                                    <%--3--%>
                                    <obout:Column DataField="SiteName" HeaderText="Department" HeaderAlign="left" Align="left" Width="10%"></obout:Column>
                                    <%--4--%>
                                    <obout:Column DataField="ID" HeaderAlign="center" Align="center" Visible="false" Width="0%"></obout:Column>
                                    <%--5--%>
                                    <obout:Column DataField="Orderdate" HeaderText="Request Date" HeaderAlign="left" Align="left" DataFormatString="{0:dd-MMM-yyyy}" Width="12%"></obout:Column>
                                    <%--6--%>
                                    <obout:Column DataField="Deliverydate" HeaderText="Exp.Delivery Date" HeaderAlign="left" Align="left" DataFormatString="{0:dd-MMM-yyyy}" Width="12%"></obout:Column>
                                    <%--7--%>
                                    <obout:Column DataField="OrderType" HeaderText="Order Type" HeaderAlign="left" Align="left" Width="10%" Wrap="true"></obout:Column>
                                     <%--8--%>
                                    <obout:Column DataField="OrderCategory" HeaderText="Order Category" HeaderAlign="left" Align="left" Width="12%" Wrap="true"></obout:Column>                                   
                                    <%--9--%>
                                    <obout:Column DataField="MethodName" HeaderText="Payment Method" HeaderAlign="left" Align="left" Width="10%" Wrap="true"></obout:Column>
                                    <%--10--%>
                                    <obout:Column DataField="Title" HeaderText="Title" HeaderAlign="left" Align="left" Width="8%" Wrap="true"></obout:Column>
                                    <%--11--%>
                                    <obout:Column DataField="RequestByUserName" HeaderText="Request By" HeaderAlign="left" Align="left" Width="10%"></obout:Column>
                                    <%--12--%>
                                    <obout:Column DataField="AmounttobeCollected" HeaderText="Amt to be collected" HeaderAlign="left" Align="left" Width="8%"></obout:Column>
                                    <%--13--%>
                                    <obout:Column DataField="AmountPaid" HeaderText="Amt Paid" HeaderAlign="left" Align="left" Width="8%"></obout:Column>

                                    <%--14--%>
                                    <obout:Column DataField="CustomerFirstName" HeaderText="First Name" HeaderAlign="left" Align="left" Width="8%"></obout:Column>

                                    <%--15--%>
                                    <obout:Column DataField="CustomerLastName" HeaderText="Last Name" HeaderAlign="left" Align="left" Width="8%"></obout:Column>


                                    <%--16--%>
                                    <obout:Column DataField="DeliveryType" HeaderText="Delivery Type" HeaderAlign="left" Align="left" Width="8%" ></obout:Column>

                                        <%--17--%>
                                    <obout:Column DataField="VIPCustomer" HeaderText="VIP Customer" HeaderAlign="left" Align="left" Width="8%"  ></obout:Column>


                                    <%--18--%>
                                    <obout:Column DataField="RequestStatus" HeaderText="Request Status" HeaderAlign="left" Align="left" Width="8%" Wrap="true"></obout:Column>


                                    <%--19--%>
                                    <obout:Column DataField="ImgRequest" HeaderText="Request" HeaderAlign="center" Align="center" Width="8%">
                                        <TemplateSettings TemplateId="GTStatusGUIRequest" />
                                    </obout:Column>
                                    <%--20--%>
                                    <obout:Column DataField="ImgApproval" HeaderText="Approval" HeaderAlign="center"
                                        Align="center" Width="8%">
                                        <TemplateSettings TemplateId="GTStatusGUIApproval" />
                                    </obout:Column>
                                    <%--21--%>
                                    <obout:Column DataField="ImgIssue" HeaderText="Dispatch" HeaderAlign="center" Align="center"
                                        Width="8%">
                                        <TemplateSettings TemplateId="GTStatusGUIIssue" />
                                    </obout:Column>
                                    <%--other column--%>
                                    <obout:Column DataField="RequestBy" Visible="false" Width="0px"></obout:Column>
                                    <obout:Column DataField="Priority" HeaderText="Request Type" HeaderAlign="left" Visible="false" Align="left" Width="0%"></obout:Column>
                                    <obout:Column DataField="StatusID" HeaderText="" Width="0px" Visible="false"></obout:Column>

                                </Columns>
                                <Templates>
                                    <obout:GridTemplate ID="GTStatusGUIRequest" runat="server">
                                        <Template>
                                            <center>
                                                <div class='<%# ("POR" + Container.Value) %>' onclick="parent.RequestOpenEntryForm('Request','<%# Container.Value %>', '<%# Container.DataItem["ID"] %>')">
                                                </div>
                                            </center>
                                        </Template>
                                    </obout:GridTemplate>
                                    <obout:GridTemplate ID="GTStatusGUIApproval" runat="server">
                                        <Template>
                                            <center>
                                                <div class='<%# ("POR" + Container.Value) %>' onclick="parent.RequestOpenEntryForm('Approval','<%# Container.Value %>', '<%# Container.DataItem["ID"] %>')">
                                                </div>
                                            </center>
                                        </Template>
                                    </obout:GridTemplate>
                                    <obout:GridTemplate ID="GTStatusGUIIssue" runat="server">
                                        <Template>
                                            <center>
                                                <div class='<%# ("POR" + Container.Value) %>' onclick="parent.RequestOpenEntryForm('Request','<%# Container.Value %>', '<%# Container.DataItem["ID"] %>')">
                                                </div>
                                            </center>
                                        </Template>
                                    </obout:GridTemplate>
                                    <obout:GridTemplate ID="GTStatusGUIReceipt" runat="server">
                                        <Template>
                                            <center>
                                                <div class='<%# ("POR" + Container.Value) %>' onclick="parent.RequestOpenEntryForm('Receipt','<%# Container.Value %>', '<%# Container.DataItem["ID"] %>')">
                                                </div>
                                            </center>
                                        </Template>
                                    </obout:GridTemplate>
                                    <obout:GridTemplate ID="GTStatusGUIConsumption" runat="server">
                                        <Template>
                                            <center>
                                                <div class='<%# ("POR" + Container.Value) %>' onclick="parent.Requestparent('Consumption','<%# Container.Value %>', '<%# Container.DataItem["ID"] %>')">
                                                </div>
                                            </center>
                                        </Template>
                                    </obout:GridTemplate>
                                </Templates>
                            </obout:Grid>
                        </td>
                    </tr>
                </table>

                 

                <asp:HiddenField ID="hdnSelectedRec" runat="server" ClientIDMode="Static" />
                 <asp:HiddenField ID="hdnFrmdate" runat="server" ClientIDMode="Static" />
                 <asp:HiddenField ID="hdnTdate" runat="server" ClientIDMode="Static" />
                 <asp:HiddenField ID="hdnOrdcategory" runat="server" ClientIDMode="Static" />
                 <asp:HiddenField ID="hdnOrderno" runat="server" ClientIDMode="Static" />
                 <asp:HiddenField ID="hdnLocation" runat="server" ClientIDMode="Static" />
                 <asp:HiddenField ID="hdnPassportno" runat="server" ClientIDMode="Static" />
                 <asp:HiddenField ID="hdnOrdtype" runat="server" ClientIDMode="Static" />
                 <asp:HiddenField ID="hdnMisidn" runat="server" ClientIDMode="Static" />
                   <asp:HiddenField ID="hdnsearch" runat="server" ClientIDMode="Static" />
                 <asp:HiddenField ID="hdnvisibleornot" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdnpendingordertype" runat="server" ClientIDMode="Static" />
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
                    SelectedRec();
                }
            }
            function SelectedRec() {
               // alert('SelectedRec');
                var hdnSelectedRec = document.getElementById("hdnSelectedRec");
                hdnSelectedRec.value = "";
                if (GVRequest.PageSelectedRecords.length > 0) {
                    for (var i = 0; i < GVRequest.PageSelectedRecords.length; i++) {
                        var record = GVRequest.PageSelectedRecords[i];
                        if (hdnSelectedRec.value != "") hdnSelectedRec.value += ',' + record.ID;
                        if (hdnSelectedRec.value == "") hdnSelectedRec.value = record.ID;
                    }
                    // alert('SelectedRec hdnSelectedRec.value' + hdnSelectedRec.value);
                    parent.GetSelected(hdnSelectedRec.value);
                    //parent.ShowMsg('1');
                }
            }

            function GVRequest_Deselect(index)
            {
                SelectedRec();
            }
            function onDeselect(row)
            {
                SelectedRec();
            }
        </script>

        <script type="text/javascript">
        function RequestOpenEntryFormSearch(invoker, state, requestID) {
            if (state != "gray") {
                if (state == "red") { state = "Edit"; }
                else if (state != "red") { state = "View"; }
                PageMethods.WMSetSessionRequest(invoker, requestID, state, RequestOpenEntryFormSearchOnSuccess, null);
            }
            else
            {
                showAlert("Not Applicable", '', '#');
            }
        }


        function RequestOpenEntryFormSearchOnSuccess(result) {
            switch (result) {
                case "Request":
                    window.open('../PowerOnRent/PartRequestEntry.aspx', '_self', '');
                    break;
                case "Approval":
                    window.open('../PowerOnRent/PartRequestEntry.aspx', '_self', '');
                    break;
                case "Issue":
                    window.open('../PowerOnRent/PartIssueEntry.aspx?invoker=Request', '_self', '');
                    break;
                case "Receipt":
                    window.open('../PowerOnRent/PartReceiptEntry.aspx?invoker=Request', '_self', '');
                    break;
                case "Consumption":
                    window.open('../PowerOnRent/PartConsumptionEntry.aspx?invoker=' + getParameterByName("invoker"), '_self', '');
                    break;
                case "HQReceipt":
                    window.open('../PowerOnRent/HQGoodsReceiptEntry.aspx', '_self', '');
                    break;
                case "AccessDenied":
                    showAlert("Access Denied", '', '#');
                    break;
            }
        }


        //function AfterAdvanceSearchAllValue(Search,frmdatr, todate, ordercategory, Orderno, Location, Passportno, Ordtype, Misidno) {         
        //    alert('hi');
        //    var hdnFrmdate = document.getElementById("hdnFrmdate");
        //    var hdnTdate = document.getElementById("hdnTdate");
        //    var hdnOrdcategory = document.getElementById("hdnOrdcategory");
        //    var hdnOrderno =document.getElementById("hdnOrderno");
        //    var hdnLocation =document.getElementById("hdnLocation");
        //    var hdnPassportno =document.getElementById("hdnPassportno");
        //    var hdnOrdtype = document.getElementById("hdnOrdtype");
        //    var hdnMisidn = document.getElementById("hdnMisidn");
        //    var hdnsearch = document.getElementById("hdnsearch");
        //    hdnsearch = Search
        //    hdnFrmdate = frmdatr;
        //    hdnTdate = todate;
        //    hdnOrdcategory = ordercategory;
        //    hdnOrderno = Orderno;
        //    hdnLocation = Location;
        //    hdnPassportno =Passportno ;
        //    hdnOrdtype = Ordtype;
        //    hdnMisidn = Misidno;
        //    window.opener.GVRequest.refresh();
            //}

        function AdvanceSearchOrder(){
        var hdnvisibleornot = document.getElementById("<%= hdnvisibleornot.ClientID %>");
            if (hdnvisibleornot.value == "Yes") {
                if (getParameterByName("invoker").toString() == "Issue") {
                    window.open('../PowerOnRent/AdvanceSearch.aspx?invoker=Issue', null, 'height=200px, width=1500px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=500; screenY=200');
                }
                else if (getParameterByName("invoker").toString() == "Request") {
                    window.open('../PowerOnRent/AdvanceSearch.aspx?invoker=Request', null, 'height=200px, width=1500px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=500; screenY=200');
                }
                else {
                    window.open('../PowerOnRent/AdvanceSearch.aspx?invoker=Request', null, 'height=200px, width=1500px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=500; screenY=200');
                }
            }
            else {
                 var e = document.getElementById("<%= ddlSelectOrderTypenew.ClientID %>");
                var strUser = e.options[e.selectedIndex].value;

               <%-- var hdnadvancesearch = document.getElementById("<%= hdnadvancesearch.ClientID %>");
                var strUser = hdnadvancesearch.value;--%>
       
                if (strUser != "Normal") {
                    if (getParameterByName("invoker").toString() == "Issue") {
                        window.open('../PowerOnRent/AdvanceSearch.aspx?invoker=Issue', null, 'height=300px, width=1500px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=500; screenY=200');
                    }
                    else if (getParameterByName("invoker").toString() == "Request") {
                        window.open('../PowerOnRent/AdvanceSearch.aspx?invoker=Request', null, 'height=300px, width=1500px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=500; screenY=200');
                    }
                    else {
                        window.open('../PowerOnRent/AdvanceSearch.aspx?invoker=Request', null, 'height=300px, width=1500px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=500; screenY=200');
                    }
                }
                else {
                    showAlert("Not Applicable", '', '#');                  
                }
            }
        }


    </script>

        <%-- code for Pending approval Order--%>
        <script type="text/javascript">
           
            if (sessionStorage["gwcOrderType"] != null && sessionStorage["gwcOrderType"] != '') {
                try {
                     document.getElementById('ddlSelectOrderTypenew').value = sessionStorage["gwcOrderType"];
                } catch (ex) {
                    // Do nothing...
                }
               
            }

            function updateOrderType(obj) {
                parent.showWMSThemeLoading();
                    

                    //take hidden field and assign value to it
               
                    var chkordertype = document.getElementById('ddlSelectOrderTypenew').value;
                  
                    if (chkordertype == 'Normal') {
                        document.getElementById("<%= hdnpendingordertype.ClientID %>").value = chkordertype;
                    }
                //parent.SetPendingbuttonvalue();
                parent.showListByOrderType(obj.value);
                sessionStorage["gwcOrderType"] = document.getElementById('ddlSelectOrderTypenew').value;
                }
           
             parent.hideWMSThemeLoading();
        </script>

    </form>
</body>
</html>
