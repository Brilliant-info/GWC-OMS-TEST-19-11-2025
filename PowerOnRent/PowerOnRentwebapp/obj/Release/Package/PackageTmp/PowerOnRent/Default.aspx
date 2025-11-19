<%@ Page Title="GWC" Language="C#" MasterPageFile="~/MasterPage/CRM.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="PowerOnRentwebapp.PowerOnRent.Default"
    EnableEventValidation="false" %>

<%@ Register Src="../CommonControls/UCFormHeader.ascx" TagName="UCFormHeader" TagPrefix="uc2" %>
<%@ Register Src="../CommonControls/Toolbar.ascx" TagName="Toolbar" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHolder_FormHeader" runat="server">
    <uc2:Toolbar ID="Toolbar1" runat="server" />
    <uc2:UCFormHeader ID="UCFormHeader1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHolder_Form" runat="server">

    <div class="divHead">
        <h4 id="h4DivHead" runat="server"></h4>
        <table style="float: right; font-size: 15px; font-weight: bold; color: Black; margin-top: -25px;">
            <tr>


                <%--<td style="padding-right: 10px; padding-bottom: 0px; padding-top: 0px;">
                    <asp:Label ID="lblselectord" Visible="false" runat="server" Text="Select Order"></asp:Label>
                </td>
                <td style="padding-right: 10px; padding-bottom: 0px; padding-top: 0px;">                

                 <asp:DropDownList ID="ddlSelectOrderType" Visible="false" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSelectOrderType_SelectedIndexChanged">  
                        <asp:ListItem Text="Normal Order" Value="Normal"></asp:ListItem>
                        <asp:ListItem Text="E-Commerce Order" Value="Ecommerce"></asp:ListItem>
                    </asp:DropDownList>                  
                </td>--%>
                <td style="padding-right: 10px; padding-bottom: 0px; padding-top: 0px;">
                    <input type="button" id="btnmarkasreturn" title="Mark As Return" value="Mark As Return" runat="server" visible="false" onclick="ChangeMarkAsReturnOrderStatus()" />
                </td>
                <td style="padding-right: 10px; padding-bottom: 0px; padding-top: 0px;">
                    <input type="button" id="btnrefund" title="btnpend" value="Customer Refund" runat="server" visible="false" onclick="ChangeRefundStatus()" />
                </td>
                <td style="padding-right: 10px; padding-bottom: 0px; padding-top: 0px;">
                    <input type="button" id="btnpending" title="btnpend" value="Pending for Approval" runat="server" style="display:none;" onclick="ShowApprovalPending()" />
                </td>
                <td style="padding-right: 10px; padding-bottom: 0px; padding-top: 0px;">
                    <input type="button" id="btnchangestatus" title="Change Status" value="Change Status" runat="server" onclick="ChangeStatus()" />
                </td>
                <td style="padding-right: 10px; padding-bottom: 0px; padding-top: 0px;">
                    <input type="button" id="btnadvancesearch" title="Advance Search" value="Advance Search" runat="server" onclick="AdvanceSearchOrder()" />
                </td>

                <td style="padding-right: 10px; padding-bottom: 0px; padding-top: 0px;">
                    <input type="button" id="btnCancelOrder" title="Cancel Order" value="Cancel Order" runat="server" visible="false" onclick="CancelSelectedOrder()" />
                    <input type="button" id="btnDriver" title="Allocate Driver" value="Allocate Driver" runat="server" visible="false" onclick="AllocateDriver()" />
                </td>
                <td>
                    <div class="PORgray">
                    </div>
                </td>
                <td>
                    <asp:Label ID="lblHeading" runat="server" Text="Not Applicable"></asp:Label>
                </td>
                <td>&nbsp;
                </td>
                <td>
                    <div class="PORgreen">
                    </div>
                </td>
                <td>
                    <asp:Label ID="lblCompleted" runat="server" Text="Completed"></asp:Label>
                </td>
                <td>&nbsp;
                </td>
                <td>
                    <div class="PORred">
                    </div>
                </td>
                <td>
                    <asp:Label ID="lblPending" runat="server" Text="Pending"></asp:Label>
                </td>
                <td>&nbsp;
                </td>
                <td>
                    <div class="PORorange">
                    </div>
                </td>
                <td>
                    <asp:Label ID="lblCancelled" runat="server" Text="Cancelled"></asp:Label>

                </td>
                <%--<td>
                    &nbsp;
                </td>
                <td>
                    <div class="PORgreenRed">
                    </div>
                </td>
                <td>
                    Partially Completed
                </td>--%>
            </tr>
        </table>
    </div>
    <div class="divDetailExpand" id="divlinkRequestsList">
        <center>
            <iframe runat="server" id="iframePOR" width="99%" style="border: none; min-height: 440px;">

            </iframe>
        </center>
    </div>
    <asp:HiddenField ID="SelectedOrder" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="SelectedOrderNew" runat="server" ClientIDMode="Static" />

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
    <asp:HiddenField ID="hdnUserType" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdndropdown" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnadvancesearch" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnemail" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnpaymenttype" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnSimserial" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnismoney" runat="server" ClientIDMode="Static" />
    <style type="text/css">
        /*POR Collapsable Div*/

        .PanelCaption 
        {
            color: Black;
            font-size: 13px;
            font-weight: bold;
            margin-top: -22px;
            margin-left: -5px;
            position: absolute;
            background-color: White;
            padding: 0px 2px 0px 2px;
        }

        .divHead {
            border: solid 2px #F5DEB3;
            width: 99%;
            text-align: left;
        }

            .divHead h4 {
                /*color: #33CCFF;*/
                color: #483D8B;
                margin: 3px 3px 3px 3px;
            }

            .divHead a {
                float: right;
                margin-top: -15px;
                margin-right: 5px;
            }

                .divHead a:hover {
                    cursor: pointer;
                    color: Red;
                }

        .divDetailExpand {
            border: solid 2px #F5DEB3;
            border-top: none;
            width: 99%;
            padding: 5px 0 5px 0;
            overflow: hidden;
            height: 92%;
        }

        .divDetailCollapse {
            display: none;
        }
        /*End POR Collapsable Div*/
    </style>
    <script type="text/javascript">
        // onload();
        //function onload() {
        //    var v = document.getElementById("btnConvertTo");
        //    v.style.visibility = 'hidden';

        //    var exp = document.getElementById("btnExport");
        //    exp.style.visibility = 'hidden';

        //    var imp = document.getElementById("btnImport");
        //    imp.style.visibility = 'hidden';

        //    var ml = document.getElementById("btnMail");
        //    ml.style.visibility = 'hidden';

        //    var pt = document.getElementById("btnPrint");
        //    pt.style.visibility = 'hidden';
        //}

        function jsAddNew() {
            if (getParameterByName("invoker") == "Request" || getParameterByName("invoker") == "Issue" || getParameterByName("invoker") == "Receipt" || getParameterByName("invoker") == "Consumption" || getParameterByName("invoker") == "HQReceipt") {
                PageMethods.WMSetSessionAddNew(getParameterByName("invoker"), "Add", jsAddNewOnSuccess, null);
            }
            else {
                showAlert("Invalid url", "error", "../PowerOnRent/Default.aspx?invoker=Request");
            }
        }
        function jsAddNewOnSuccess() {
            if (getParameterByName("invoker") == "Request") {
                window.open('../PowerOnRent/PartRequestEntry.aspx', '_self', '');
            }
            else if (getParameterByName("invoker") == "Issue") {
                window.open('../PowerOnRent/PartIssueEntry.aspx', '_self', '');
            }
            else if (getParameterByName("invoker") == "Receipt") {
                window.open('../PowerOnRent/PartReceiptEntry.aspx', '_self', '');
            }
            else if (getParameterByName("invoker") == "Consumption") {
                window.open('../PowerOnRent/PartConsumptionEntry.aspx', '_self', '');
            }
            else if (getParameterByName("invoker") == "HQReceipt") {
                window.open('../PowerOnRent/HQGoodsReceiptEntry.aspx', '_self', '');
            }
        }

        function OpenEntryForm(invoker, state, referenceID, requestID) {
            if (state != "gray") {
                if (state == "red") { state = "Edit"; }
                else if (state != "red") { state = "View"; }
                PageMethods.WMSetSession(invoker, referenceID, requestID, state, OpenEntryFormOnSuccess);
            }
            else {
                showAlert("Not Applicable", '', '#');
            }
        }

        function OpenEntryFormOnSuccess(result) {
            switch (result) {
                case "Request":
                    window.open('../PowerOnRent/PartRequestEntry.aspx', '_self', '');
                    break;
                case "Approval":
                    window.open('../PowerOnRent/PartRequestEntry.aspx', '_self', '');
                    break;
                case "Issue":
                    window.open('../PowerOnRent/PartIssueEntry.aspx', '_self', '');
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
            }
        }

        function ToolbarAccess() {

        }

        function AdvanceSearchOrder() {

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
                <%-- var e = document.getElementById("<%= ddlSelectOrderType.ClientID %>");
                var strUser = e.options[e.selectedIndex].value;--%>

                <%-- var hdnadvancesearch = document.getElementById("<%= hdnadvancesearch.ClientID %>");
                var strUser = hdnadvancesearch.value;
       
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
                }--%>

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
        }


    </script>
    <%--Code for Request Summary--%>
    <script type="text/javascript">
        function RequestOpenEntryForm(invoker, state, requestID) {
            if (state != "gray") {
                if (state == "red") { state = "Edit"; }
                else if (state != "red") { state = "View"; }
                PageMethods.WMSetSessionRequest(invoker, requestID, state, RequestOpenEntryFormOnSuccess, null);
            }
            else {
                showAlert("Not Applicable", '', '#');
            }
        }

        function RequestOpenEntryFormOnSuccess(result) {
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



        function AfterAdvanceSearchAllValue(Search, frmdatr, todate, ordercategory, Orderno, Location, Passportno, Ordtype, Misidno, Email, PaymentType, Simserial) {
            //  alert('hi');
            var hdnFrmdate = document.getElementById("hdnFrmdate");
            var hdnTdate = document.getElementById("hdnTdate");
            var hdnOrdcategory = document.getElementById("hdnOrdcategory");
            var hdnOrderno = document.getElementById("hdnOrderno");
            var hdnLocation = document.getElementById("hdnLocation");
            var hdnPassportno = document.getElementById("hdnPassportno");
            var hdnOrdtype = document.getElementById("hdnOrdtype");
            var hdnMisidn = document.getElementById("hdnMisidn");
            var hdnsearch = document.getElementById("hdnsearch");
            var hdnemail = document.getElementById("hdnemail");
            var hdnpaymenttype = document.getElementById("hdnpaymenttype");
            var hdnSimserial = document.getElementById("hdnSimserial");
            hdnsearch = Search
            hdnFrmdate = frmdatr;
            hdnTdate = todate;
            hdnOrdcategory = ordercategory;
            hdnOrderno = Orderno;
            hdnLocation = Location;
            hdnPassportno = Passportno;
            hdnOrdtype = Ordtype;
            hdnMisidn = Misidno;
            hdnemail = Email;
            hdnpaymenttype = PaymentType;
            hdnSimserial = Simserial;
            PageMethods.AssignValueToSession(hdnsearch, hdnFrmdate, hdnTdate, hdnOrdcategory, hdnOrderno, hdnLocation, hdnPassportno, hdnOrdtype, hdnMisidn, hdnemail, hdnpaymenttype, hdnSimserial, OnSuccessAdvanceSearch, null);

        }

        function OnSuccessAdvanceSearch(result) {
            if (result != 0) {
                var iframe = document.getElementById("<%= iframePOR.ClientID %>");
                if (getParameterByName("invoker").toString() == "Request") {
                    iframe.src = "../PowerOnRent/GridRequestSummary.aspx?FillBy=AdvanceSearch&Invoker=Request";
                }
                else if (getParameterByName("invoker").toString() == "Issue") {
                    iframe.src = "../PowerOnRent/GridRequestSummary.aspx?FillBy=AdvanceSearch&Invoker=Issue";
                }
                else {
                    iframe.src = "../PowerOnRent/GridRequestSummary.aspx?FillBy=AdvanceSearch&Invoker=Request";
                }
            }
        }



    </script>

    <%--End Code for Request Summary--%>
    <%--Code for Issue Summary--%>
    <script type="text/javascript">
        function IssueOpenEntryForm(invoker, state, issueID) {
            if (state != "gray") {
                if (state == "red") { state = "Edit"; }
                else if (state != "red") { state = "View"; }
                PageMethods.WMSetSessionIssue(invoker, issueID, state, IssueOpenEntryFormOnSuccess, null);
            }
            else {
                showAlert("Not Applicable", '', '#');
            }
        }

        function IssueOpenEntryFormOnSuccess(result) {
            switch (result) {
                case "Issue":
                    window.open('../PowerOnRent/PartIssueEntry.aspx?invoker=Issue', '_self', '');
                    break;
                case "Receipt":
                    window.open('../PowerOnRent/PartReceiptEntry.aspx?invoker=Issue', '_self', '');
                    break;
                case "Consumption":
                    window.open('../PowerOnRent/PartConsumptionEntry.aspx', 'self', '');
                    break;
                case "AccessDenied":
                    showAlert("Access Denied", '', '#');
                    break;
            }
        }
    </script>
    <%--End Code for Issue Summary--%>
    <%--Code for Receipt Summary--%>
    <script type="text/javascript">
        function ReceiptOpenEntryForm(invoker, state, receiptID) {
            debugger;
            if (state != "gray") {
                if (state == "red") { state = "Edit"; }
                else if (state != "red") { state = "View"; }
                PageMethods.WMSetSessionReceipt(invoker, receiptID, state, ReceiptOpenEntryFormOnSuccess, null);
            }
            else {
                showAlert("Not Applicable", '', '#');
            }
        }

        function ReceiptOpenEntryFormOnSuccess(result) {
            debugger;
            switch (result) {
                case "Receipt":
                    window.open('../PowerOnRent/PartReceiptEntry.aspx?invoker=Receipt', '_self', '');
                    break;
                case "Consumption":
                    window.open('../PowerOnRent/PartConsumptionEntry.aspx', '_self', '');
                    break;
                case "AccessDenied":
                    showAlert("Access Denied", '', '#');
                    break;
            }
        }
    </script>
    <%--End Code for Receipt Summary--%>
    <%--Code for Consumption Summary--%>
    <script type="text/javascript">
        function ConsumptionOpenEntryForm(invoker, state, ConsumptionID) {
            if (state != "gray") {
                if (state == "red") { state = "Edit"; }
                else if (state != "red") { state = "View"; }
                PageMethods.WMSetSessionConsumption(invoker, ConsumptionID, state, ConsumptionOpenEntryFormOnSuccess, null);
            }
            else {
                showAlert("Not Applicable", '', '#');
            }
        }

        function ConsumptionOpenEntryFormOnSuccess(result) {
            switch (result) {
                case "Consumption":
                    window.open('../PowerOnRent/PartConsumptionEntry.aspx', '_self', '');
                    break;
                case "AccessDenied":
                    showAlert("Access Denied", '', '#');
                    break;
            }
        }
    </script>
    <%--End Code for Consumption Summary--%>
    <%--Code for Consumption Summary--%>
    <script type="text/javascript">
        function HQReceiptOpenEntryForm(invoker, state, HQReceiptID) {
            if (state != "gray") {
                if (state == "red") { state = "Edit"; }
                else if (state != "red") { state = "View"; }
                PageMethods.WMSetSessionHQReceipt(invoker, HQReceiptID, state, HQReceiptOpenEntryFormOnSuccess, null);
            }
            else {
                showAlert("Not Applicable", '', '#');
            }
        }

        function HQReceiptOpenEntryFormOnSuccess(result) {
            switch (result) {
                case "HQReceipt":
                    window.open('../PowerOnRent/HQGoodsReceiptEntry.aspx', '_self', '');
                    break;
                case "AccessDenied":
                    showAlert("Access Denied", '', '#');
                    break;
            }
        }
    </script>
    <%--End Code for Consumption Summary--%>
    <%--Start of Allocate Driver--%>
    <script type="text/javascript">
       
        function GetSelected(hdnSelectedRec)
        {
            var SelectedOrder = document.getElementById("SelectedOrder");
            SelectedOrder.value = hdnSelectedRec;

            var SelectedOrderNew = document.getElementById("SelectedOrderNew");
            SelectedOrderNew.value = hdnSelectedRec;
            PageMethods.getismoneydept(SelectedOrder.value, OnSuccessSelOrder, null);
        }
        function OnSuccessSelOrder(result)
        {
             var ismoney = document.getElementById("hdnismoney");
           
            if (result == "Yes")
            {
                ismoney.value = "true";
            }
            else
            {
                 ismoney.value = "false";
            }
        }
        function AllocateDriver()
        {
            var SelectedOrder = document.getElementById("SelectedOrder");
            var hdnUserType = document.getElementById("<%= hdnUserType.ClientID %>");
            var userType = hdnUserType.value;
            if (userType == "Super Admin" || userType == "Admin")
            {
                if (SelectedOrder.value == "")
                {
                    showAlert('Please Select At Least One Order!!!', 'Error', '#');
                }
                else
                {
                    PageMethods.WMChkDispatchedOrder(SelectedOrder.value, OnSuccessDispatchedOrder, null);
                }
            }
            else
            {
                showAlert("Not Applicable", '', '#');
            }
        }
        function OnSuccessDispatchedOrder(result)
        {
            if (result == 1)
            {
                //  showAlert('One or More Selected Orders Are Already Dispatched. Please Select only Not Dispatched Orders!!!', 'Error', '#');
                showAlert('Please select orders whose status is Ready For Dispatch or Activated.', 'Error', '#');
            } else if (result == 2)
            {
                  showAlert('Please do not select orders whose comes from UDF5 as HUB.', 'Error', '#');
            }
            else if (result == 3)
            {
                showAlert('Only select Orders which are not for VIP customer.', 'Error', '#');
            }
            else
            {
                var SelectedOrder = document.getElementById("SelectedOrder");
                // alert(SelectedOrder.value);
                //    window.open('../PowerOnRent/AllocateDriver.aspx?OID=' + SelectedOrder.value + '', null, 'height=550, width=700,status= 0, resizable= 1, scrollbars=0, toolbar=0,location=center,menubar=0, screenX=300; screenY=50');
                window.open('../PowerOnRent/AllocateDriver.aspx?OID=' + SelectedOrder.value + '&Default=Yes', null, 'height=550, width=700,status= 0, resizable= 1, scrollbars=0, toolbar=0,location=center,menubar=0, screenX=300; screenY=50');

            }
        }
        function AfterAssignDriver() {
            location.reload();
        }
        function CancelSelectedOrder()
        {
            var SelectedOrder = document.getElementById("SelectedOrder");
            var SelOrdr = SelectedOrder.value;
            var count = (SelOrdr.match(/,/) || []).length;
            console.log(count);
            if (count >= 1)
            {
                showAlert("Select Only One Order", "Error", "#");
            }
            else if (SelectedOrder.value == "")
            {
                showAlert('Please Select One Order!!!', 'Error', '#');
            }
            else
            {
                var r = confirm("Are You Sure to Cancel This Order?")
                if (r == true)
                {
                    var myBomWin = window.open('CancelOrderConfirm.aspx?Id=' + SelOrdr, null, 'height=170px, width=600px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=300; screenY=50');
                    // PageMethods.WMCancelOrder(SelectedOrder.value, OnSuccessCancelOrder, null);
                }
            }
        }
        function OnSuccessCancelOrder(result)
        {
            if (result == 0) {
                showAlert("Not Applicable", '', '#');
            } else {
                showAlert('Selected Order Is Cancelled!!!', 'Error', '#');
                location.reload();
            }
        }
        function ChangeMarkAsReturnOrderStatus()
        {
            var hdnUserType = document.getElementById("<%= hdnUserType.ClientID %>");
            var userType = hdnUserType.value;
           // alert('userType'+userType);
            var SelectedOrderNew = document.getElementById("SelectedOrderNew");
            var SelOrdr1 = SelectedOrderNew.value;
             //alert('SelectedOrderNew'+SelOrdr1);
            var count = (SelOrdr1.match(/,/) || []).length;
            console.log(count);
             // alert('count'+count);
            //if (userType == "Super Admin")
            //{
                    if (SelectedOrderNew.value == "")
                    {
                        showAlert('Please Select One Order!!!', 'Error', '#');
                    }
                    else
                    {
                        PageMethods.WMChkChangeMarkAsReturnOrder(SelectedOrderNew.value, userType, OnSuccessMarkAsReturnOrder, null);
                    }
           // }

        }
         function OnSuccessMarkAsReturnOrder(result)
         {
           <%-- var hdnUserType = document.getElementById("<%= hdnUserType.ClientID %>");--%>
           
           /* if (result >= 1)
            {
                showAlert('Please select orders whose Status is Cancelled,Out for Delivery,Delivery to Hub. and Order Type is not SIM Only and Delivery Type is not HUB.', 'Error', '#');
            }
            else
            {
                showAlert('Return Order Success.', 'Error', '#');
             }*/



             if (result == 2)
            { 
                showAlert('Please select orders whose Status is Cancelled,Out for Delivery,Delivery to Hub. and Order Type is not SIM Only and Delivery Type is not HUB and it should not have 110056 and 110057 SIM SKU.', 'Error', '#');
            } else if (result == 3) {
                showAlert('Please select Only one Order', 'Error', '#');
             }
                 else if (result == 1001) {
                showAlert('Return Order Already Created for This Reference Number', 'Error', '#');
            }
            else
            {
                showAlert('Return Order Success.', '_self', '#');
            }
         }

        function ChangeStatus()
        {
            var hdnUserType = document.getElementById("<%= hdnUserType.ClientID %>");
            var userType = hdnUserType.value;
            //alert('ChangeStatus' + userType);
            
            var SelectedOrderNew = document.getElementById("SelectedOrderNew");
            var SelOrdr1 = SelectedOrderNew.value;
           // alert('SelectedOrderNew.value' + SelectedOrderNew.value);
            var count = (SelOrdr1.match(/,/) || []).length;
            console.log(count);

            if (userType == "Retail User Admin")
            {
                if (SelectedOrderNew.value == "")
                {
                    showAlert('Please Select One Order!!!', 'Error', '#');
                }
                else
                {
                    PageMethods.WMChkChangeOrderStatus(SelectedOrderNew.value, OnSuccessDispatchedOrdernew, null);
                  //   PageMethods.WMChkChangeOrderStatusfulfillment(SelectedOrderNew.value, OnSuccessDispatchedOrdernew, null);
                }

               // showAlert('Do not have access to change status as Delivered or Cancled!!!', 'Error', '#');
            }
            else if (userType == "Admin" && hdnismoney.value == "true" )
            {
                if (SelectedOrderNew.value == "")
                {
                    showAlert('Please Select One Order!!!', 'Error', '#');
                }
                else
                {
                    PageMethods.WMChkChangeOrderStatus(SelectedOrderNew.value, OnSuccessDispatchedOrdernew, null);
                  //   PageMethods.WMChkChangeOrderStatusfulfillment(SelectedOrderNew.value, OnSuccessDispatchedOrdernew, null);
                }
            }
            else
            {
                if (userType == "Fulfilment")
                {
                    if (SelectedOrderNew.value == "")
                    {
                        showAlert('Please Select One Order!!!', 'Error', '#');
                    }
                    else
                    {
                        PageMethods.WMChkChangeOrderStatusfulfillment(SelectedOrderNew.value, OnSuccessDispatchedOrdernew, null);
                    }
                }else if (userType == "Super Admin")
                {
                        if (SelectedOrderNew.value == "")
                        {
                            showAlert('Please Select One Order!!!', 'Error', '#');
                        }
                        else
                        {
                            PageMethods.WMChkChangeOrderStatusSuperAdmin(SelectedOrderNew.value, userType, OnSuccessDeliveredtoHubOrdernew, null);
                        }
                }
                else
                {
                    showAlert('Not applicable.', 'Error', '#');
                }
            }
        }
        

        function OnSuccessDispatchedOrdernew(result)
        {
          
            var hdnUserType = document.getElementById("<%= hdnUserType.ClientID %>");
           
            if (result >= 1)
            {
              
                var userType = hdnUserType.value;
                if (userType == "")
                {
                    showAlert('Please select orders whose ordercategory is Fixed Only,delivery type is Home and status is Dispatch.', 'Error', '#');
                }
                else
                {
                      //showAlert('Please select orders whose status is Dispatch.', 'Error', '#');
                    //showAlert('Please select orders whose status is Delivered to Hub Or it should not be created as Return Order', 'Error', '#');
                    showAlert('Please select orders whose status is Delivered to Hub', 'Error', '#');
                }
            }
            else
            {
                var SelectedOrder = document.getElementById("SelectedOrder");
              
                //  window.open('../PowerOnRent/ChangeStatus.aspx?Id=' + SelectedOrder.value, null, 'height=170px, width=600px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=300; screenY=50');
                window.open('../PowerOnRent/ChangeStatus.aspx?Id=' + SelectedOrder.value +"&UserType=" +hdnUserType.value+ "", null, 'height=170px, width=600px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=300; screenY=50');
                //'../PowerOnRent/UpdateRefundstatus.aspx?Id=' + SelectedOrder.value + "&userType=" + userType + "", null, 
            }
        }


         function OnSuccessDeliveredtoHubOrdernew(result)
         {
             // alert('OnSuccessDeliveredtoHubOrdernew'+result);
          
            var hdnUserType = document.getElementById("<%= hdnUserType.ClientID %>");
           
            if (result == 2)
            { 
                showAlert('Please select E-Commerce orders whose delivery type is HUB and status is Out For Delivery or Order Expired.', 'Error', '#');
            } else if (result == 3) {
                showAlert('Please select Only one Order', 'Error', '#');
            }
            else
            {
                var SelectedOrder = document.getElementById("SelectedOrder");
              
                //  window.open('../PowerOnRent/ChangeStatus.aspx?Id=' + SelectedOrder.value, null, 'height=170px, width=600px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=300; screenY=50');
                window.open('../PowerOnRent/ChangeStatus.aspx?Id=' + SelectedOrder.value +"&UserType=" +hdnUserType.value+ "", null, 'height=170px, width=600px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=300; screenY=50');
                //'../PowerOnRent/UpdateRefundstatus.aspx?Id=' + SelectedOrder.value + "&userType=" + userType + "", null, 
            }
        }

        
        
// For Parent...
        function refreshPage()
        {
	        location.reload();
        }

    </script>

       <%--Show Approval Pending orders on button click--%>
    <script type="text/javascript">

        sessionStorage["gwcOrderType"] = "Normal";

        function ShowApprovalPending()
        {
            var iframe = document.getElementById("<%= iframePOR.ClientID %>");           
            var button = document.getElementById("<%= btnpending.ClientID %>").value;
           // var OrderType = document.getElementById("<%= hdnadvancesearch.ClientID %>").value;
            var OrderType = '';
             if (sessionStorage["gwcOrderType"] != null && sessionStorage["gwcOrderType"] != '') {
                OrderType = sessionStorage["gwcOrderType"];
             }
            if (button === 'Pending for Approval') {

                if (getParameterByName("invoker").toString() == "Request") {
                    document.getElementById("<%= btnpending.ClientID %>").value = 'All Requests';
                    showWMSThemeLoading();
                    iframe.src = '../PowerOnRent/GridRequestSummary.aspx?FillBy=Pendingrequest&Invoker=Request&Ordertype=' + OrderType + '';
                    // hideWMSThemeLoading();

                }
                else if (getParameterByName("invoker").toString() == "Approval") {
                     document.getElementById("<%= btnpending.ClientID %>").value = 'All Requests';
                    showWMSThemeLoading();
                    iframe.src = '../PowerOnRent/GridRequestSummary.aspx?FillBy=Pendingrequest&Invoker=Request&Ordertype=' + OrderType + '';
                }
            } else {
                if (button === 'All Requests') {
                    document.getElementById("<%= btnpending.ClientID %>").value = 'Pending for Approval';
                    showWMSThemeLoading();
                    iframe.src = '../PowerOnRent/GridRequestSummary.aspx?FillBy=UserID&Invoker=Request&Ordertype=' + OrderType + '';
                  // hideWMSThemeLoading();
                }
            }
        }

        function showListByOrderType(OrderType) {
            showWMSThemeLoading();
            var iframe = document.getElementById("<%= iframePOR.ClientID %>");
            
            var button = document.getElementById("<%= btnpending.ClientID %>").value;
            var SessionOrderType =  sessionStorage["gwcOrderType"];
            if (button === 'Pending for Approval') {
                if (SessionOrderType == OrderType) {
                    iframe.src = '../PowerOnRent/GridRequestSummary.aspx?FillBy=Pendingrequest&Invoker=Request&Ordertype=' + OrderType + '';
                }
                else {
                     iframe.src = '../PowerOnRent/GridRequestSummary.aspx?FillBy=UserID&Invoker=Request&Ordertype=' + OrderType + '';
                }
            } else if (button === 'All Requests') {
                iframe.src = '../PowerOnRent/GridRequestSummary.aspx?FillBy=UserID&Invoker=Request&Ordertype=' + OrderType + '';
            }
            SetPendingbuttonvalue();
        }

        function SetPendingbuttonvalue() {
             document.getElementById("<%= btnpending.ClientID %>").value = 'Pending for Approval';
        }
    </script>

     <%--2022 combine CR Project 3 Cancel, Return & expire Changes--%>
    <script type ="text/javascript">
        function ChangeRefundStatus()
        {
            var hdnUserType = document.getElementById("<%= hdnUserType.ClientID %>");
            var userType = hdnUserType.value;
            var SelectedOrderNew = document.getElementById("SelectedOrderNew");
            var SelOrdr1 = SelectedOrderNew.value;
           // var count = (SelOrdr1.match(/,/) || []).length;
             var count = SelOrdr1.split(',').length;

            console.log(count);
            if (count == 1) {
                if (userType == "Account") {
                    if (SelectedOrderNew.value == "") {
                        showAlert('Please Select One Order!!!', 'Error', '#');
                    }
                    else {
                        PageMethods.checkrefundorderstatus(SelectedOrderNew.value,userType, OnSuccessRefundstatus, null);                     
                    }
                }
                else if (userType == "Super Admin") {
                    if (SelectedOrderNew.value == "") {
                        showAlert('Please Select One Order!!!', 'Error', '#');
                    }
                    else {
                        PageMethods.checkrefundorderstatus(SelectedOrderNew.value,userType, OnSuccessRefundstatus, null);
                    }
                }
                else {
                       showAlert('Not applicable.', 'Error', '#');
                     }
            }
            else if (count > 1) {
                 showAlert('Please select only one order at a time', 'Error', '#');
            }
            else if (count < 1) {
                 showAlert('Please select order', 'Error', '#');
            }
        }


        function OnSuccessRefundstatus(result) {
            if (result == 1) {              
                showAlert('Please select order whose status is Pending for Finance activity.', 'Error', '#');
            }
            else if (result == 2)
            {
                showAlert('Please select orders whose status is Pending for Finance activity.', 'Error', '#');
                //showAlert('Please select orders whose status is Return,Order Expire,Cancelled,Dispatch,Delivered, Pending for Finance activity.', 'Error', '#');
            }
            else {
                var hdnUserType = document.getElementById("<%= hdnUserType.ClientID %>");
                var userType = hdnUserType.value;
                var SelectedOrder = document.getElementById("SelectedOrder");
                window.open('../PowerOnRent/UpdateRefundstatus.aspx?Id=' + SelectedOrder.value + "&userType=" + userType + "", null, 'height=170px, width=900px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=300; screenY=50');
            }
        }
        

    </script>

</asp:Content>
