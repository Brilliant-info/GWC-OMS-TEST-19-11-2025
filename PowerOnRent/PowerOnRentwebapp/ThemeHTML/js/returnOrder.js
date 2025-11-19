var addNewRowCounter = 0;
var CurrentPage = 1;
var RecordLimit = 10;
var RecordLimitSku = 20;
var wmsAppFiles = "";
var documentOrderId = '';
var documentObject = '';
var globalFileName = [];
var globalFilePath = [];
var searchfilter = 'ALL';
var searchvalue = 'none';
var DocObject = 'RMSDocument';
var getUserId = $('#hdnUserID').val();
var getLoginUserType = '';
var gloablUserName = '';
var abc = '';
//var wmsAppFiles = "http://localhost:50068/bwmsAppfiles/";

$(document).ready(function () {
    setGloablUserId();
   // returnOrderList();
    $("#ddlCustomerList").change(function () {
        //var checkOrderheadId = $("#setAddReturnOrderId").val();
        //if (checkOrderheadId == 'To be generated') {
        //    AddDepartmentListddl();
        //}
        AddDepartmentListddl();
    });
     
    $("#wms-srv-addtransfer-popup-close").click(function () {
        var checkOrderheadId = $("#setAddReturnOrderId").val();

        
         if (checkOrderheadId == '')
        {

             $("#wms-srv-addtransfer-popup").hide();
             clearAddNewOrderPopup();
         }  
         else if (checkOrderheadId != 'To be generated')
         {

            var confimrVal = confirm("If you close will clear entered order details, Do you want to close?");
             if (confimrVal == true)
             {

                removeCurrentOrder();
                 clearAddNewOrderPopup();
                 $("#wms-srv-addtransfer-popup").hide();
                $("#setTotalRetrnQtylbl").html('');
            }
            else {
                 alert("You can continue with this order");
                 //returnOrderList(checkOrderheadId);
             }
        
        }
         else {
             clearAddNewOrderPopup();
             $("#wms-srv-addtransfer-popup").hide();
         }
       

    });
    $("#wms-srv-ViewOrder-popup-close").click(function () {
     //   $('#wms-srv-addtransfer-popup .wms-srv-datepicker').datepicker('destroy');
        $("#wms-srv-ViewOrder-popup").off();
        $("#wms-srv-ViewOrder-popup").hide();
    });

    $("#wms-srv-AssignDriver-popup-close").click(function () {
        $("#wms-srv-AssignDriver-popup").off();
        clearAssignDriverPopup();
        returnOrderList();
        $("#wms-srv-AssignDriver-popup").hide();
    });
});

function setGloablUserId() {
    getUserId = $('#hdnUserID').val();
    getLoginUserType = $("#hdnUserType").val();
    gloablUserName = $("#hdnUserName").val();
    checkLoginUserSts();
}

function checkLoginUserSts() {
    //
    getUserId = $('#hdnUserID').val();
    var apiPath = wmsApiPath + 'checkLoginUser';
    var postData = {       
        "UserId": getUserId      
    };
    callHttpUrl(apiPath, postData, function (data) {
        var isStatus = data.Status;
        var result = data.Result;
        if (result != "Success") {
            $("#pnlAppointmentList").html('');
            alert("You don't have access of this Menu");
            window.location.href = "../Inbox/InboxPOR.aspx";

        }
        else {
            //$("#pnlAppointmentList").show();
            returnOrderList();
        }
    });
}

function searchReturnOrder() {
    var searchCol = $("#ddlReturnOrderColSearch").val();
    var searchVal = $("#ddlReturnFilterValue").val();

    if (searchCol != '' && searchVal !='') {
        returnOrderList();
    }
    else {
        alert("Please Select Proper Search Value..!!");
    }
}

function removeCurrentOrder() {
    var apiPath = wmsApiPath + 'removeUnsavedOrder';
    var gerReturnOrdID = $("#setAddReturnOrderId").val();
    if (gerReturnOrdID != 'To be generated' && gerReturnOrdID != '') {

        var postData =
        {
            "UserId": getUserId,
            "ReturnId": gerReturnOrdID
        }
        callHttpUrl(apiPath, postData, function (data) {
            var isStatus = data.Status;
            var checkSts = data.Result;
            if (checkSts == 'success') {
                $("#setTotalRetrnQtylbl").html('');
                $("#wms-srv-addtransfer-popup").hide();
                returnOrderList(checkOrderheadId);
            }
            else {
                alert("Faild To Remove Order");
            }
        });
    }
    else {
        $("#wms-srv-addtransfer-popup").hide();
    }
}

function clearSearchFilter() {
    $("#ddlReturnFilterValue").val('');
    $("#ddlReturnOrderColSearch").val(0);
    returnOrderList();
}

function returnOrderList() {
    var apiPath = wmsApiPath + 'GetReturnList';
    searchfilter = $("#ddlReturnOrderColSearch").val();
    searchvalue = $("#ddlReturnFilterValue").val();
    if (searchfilter == '')
    {
        searchfilter = '';
        searchvalue = '';
    }
  
    var postData = {
        "CurrentPage": CurrentPage,
        "RecordLimit": RecordLimit,
        "UserId": getUserId,
        "Filter": searchfilter,
        "Search": searchvalue
    };
    callHttpUrl(apiPath, postData, function (data) {
        var isStatus = data.Status;
        var result = data.Result;
        var TotalRecords = result.Table[0].TotalRecord;
        var CurrentPage = result.Table[0].CurrentPage;
        var mygridList = result.Table1;
        if (isStatus == 200) {

            $("#gridReturnOrderList").html('');
            var gridList = '';
            var RMSDocument = 'RMSDocument';
            gridList = gridList + '<div class="wms-srv-grid-header">';
            gridList = gridList + '<div class="wms-srv-grid-cell"></div>';
            gridList = gridList + '<div class="wms-srv-grid-cell">Return No</div>';
            gridList = gridList + '<div class="wms-srv-grid-cell">Department</div>';
            gridList = gridList + '<div class="wms-srv-grid-cell">Return Date</div>';
            gridList = gridList + '<div class="wms-srv-grid-cell">OMS Ref.Number</div>';
            gridList = gridList + '<div class="wms-srv-grid-cell">Priority</div>';
            gridList = gridList + '<div class="wms-srv-grid-cell">Return Category</div>';
            gridList = gridList + '<div class="wms-srv-grid-cell">Location Name</div>';
            gridList = gridList + '<div class="wms-srv-grid-cell">Order Status</div>';
            //gridList = gridList + '<div class="wms-srv-grid-cell" style="width: 100px;">Pending Driver</div>';
            gridList = gridList + '<div class="wms-srv-grid-cell" style="width: 100px;">Driver Allocation</div>';
            gridList = gridList + '<div class="wms-srv-grid-cell" style="width: 100px;">Collection In Progress</div>';
            gridList = gridList + '<div class="wms-srv-grid-cell" style="width: 100px;">Rec.In Warehouse</div>';
            gridList = gridList + '<div class="wms-srv-grid-cell">Action</div>';
            gridList = gridList + '</div>';

            for (var i = 0; i < mygridList.length;i++)
            {
                var getRownumber = mygridList[i].rownumber;
                var getReturnID = mygridList[i].ReturnID;
                var getDepartment = mygridList[i].Department;

                var getReturndate = mygridList[i].returndate;
                var getReferenceNo = mygridList[i].referenceNo;
                var getPriority = mygridList[i].Priority;
                var getReturncategory = mygridList[i].returncategory;
                var getReturncategoryName = mygridList[i].returncategoryName;
                var getLocationName = mygridList[i].LocationName;
                var getStatusid = mygridList[i].statusid;     
                var getStatus = mygridList[i].status;
                
            gridList = gridList + '<div class="wms-srv-grid-row">';
                //gridList = gridList + '<div class="wms-srv-grid-cell"><input type="checkbox"  name="Order" onclick="saveOrderId(' + getReturnID+')" class="messageCheckbox" / ></div > ';
                gridList = gridList + '<div class="wms-srv-grid-cell"><input type="checkbox"  name="Order"  class="messageCheckbox" id="chkbox1" data-id="' + getReturnID + '" /></div>';
                gridList = gridList + '<div class="wms-srv-grid-cell" style="text-align:center;">' + getReturnID+'</div>';
                gridList = gridList + '<div class="wms-srv-grid-cell" style="text-align:center;">' + getDepartment+'</div>';
                gridList = gridList + '<div class="wms-srv-grid-cell" style="text-align:center;">' + getReturndate+'</div>';
                gridList = gridList + '<div class="wms-srv-grid-cell" style="text-align:center;">' + getReferenceNo+'</div>';
                gridList = gridList + '<div class="wms-srv-grid-cell" style="text-align:center;">' + getPriority+'</div>';
                gridList = gridList + '<div class="wms-srv-grid-cell" style="text-align:center;">' + getReturncategoryName + '</div>';
                gridList = gridList + '<div class="wms-srv-grid-cell" style="text-align:center;">' + getLocationName + '</div>';
                gridList = gridList + '<div class="wms-srv-grid-cell" style="text-align:center;">' + getStatus+'</div>';
                gridList = gridList + ''+bindStatusControl(getReturnID, getStatus)+'';
            gridList = gridList + '<div class="wms-srv-grid-cell">';
            gridList = gridList + '<div class="wms-srv-grid-action">';
                gridList = gridList + '<i class="fas fa-eye iconView" title="View Order" onclick="ViewOrderList(' + getReturnID+');"></i> ';
            gridList = gridList + '<div class="wms-srv-action-sep">|</div>';
            //gridList = gridList + '<i class="fas fa-edit iconView" title="Edit Order" onclick="EditReturnOrder();"></i>';
            //gridList = gridList + '<div class="wms-srv-action-sep">|</div>';
                gridList = gridList + '<i class="fas fa-file-alt iconView"  title="Document" onclick="showDocumentPopup(\''+getReturnID+'\',\''+RMSDocument+'\');"></i>';
            gridList = gridList + '<div class="wms-srv-action-sep">|</div>';
                gridList = gridList + '<a href="#" title="" class="wms-srv-icononly" onclick="showOrderHistoryPopup(\'' + getReturnID +'\');"><i class="fas fa-tasks iconView" title="Order History"></i></a>';
            gridList = gridList + '</div>';
            gridList = gridList + '</div>';
            gridList = gridList + '</div>';
            }
            $("#gridReturnOrderList").append(gridList);
            setupGridPagingList('tbltranferlistpager', CurrentPage, TotalRecords, returnOrderList)
        }
    });
}

function bindStatusControl(orderId, strStatus)
{
    var htmlControl = '';
 
    htmlControl = htmlControl + '<div class="wms-srv-grid-cell wms-srv-status-progress" style="display:none";> <div class="wms-srv-dot wms-srv-' + getStatusColor('Pending for driver allocation', strStatus) + '" data-orderid="' + orderId + '" data-status="' + strStatus + '" title=""></div></div>';    

    htmlControl = htmlControl + '<div class="wms-srv-grid-cell wms-srv-status-progress"><div class="wms-srv-dot wms-srv-' + getStatusColor('Driver Scheduled', strStatus) + '" data-orderid="' + orderId + '" data-status="' + strStatus + '" title = ""></div></div >';


    htmlControl = htmlControl + '<div class="wms-srv-grid-cell wms-srv-status-progress"><div class="wms-srv-dot wms-srv-' + getStatusColor('Collection in Progress', strStatus) + '" data-orderid="' + orderId + '" data-status="' + strStatus + '" title = ""></div></div>';


    htmlControl = htmlControl + '<div class="wms-srv-grid-cell wms-srv-status-progress"><div class="wms-srv-dot wms-srv-' + getStatusColor('Received in Warehouse', strStatus) + '" data-orderid="' + orderId + '" data-status="' + strStatus + '" title = ""></div></div>';

    return htmlControl;
}

function getStatusColor(dataFor, currentStatus) {
  
    var myColor = 'gray';
    if (dataFor == 'Pending for driver allocation') {
        if (currentStatus == 'Pending for driver allocation' || currentStatus == 'Driver Scheduled' || currentStatus == 'Received in Warehouse') {
            myColor = 'red';
        }
        else if (currentStatus == 'Cancelled') {
            myColor = 'yellow';
        }
        
    }
    else if (dataFor == 'Driver Scheduled')
    {
        if (currentStatus == 'Pending for driver allocation') {
            myColor = 'red';
        }

        else if (currentStatus == 'Driver Scheduled' || currentStatus == 'Received in Warehouse') {
            myColor = 'green';
        }
        else if (currentStatus == 'Collection in Progress') {
            myColor = 'green';
        }
        else if (currentStatus == 'Cancelled') {
            myColor = 'yellow';
        }
    }

    else if (dataFor == 'Collection in Progress')
    {

            if (currentStatus == 'Collection in Progress' || currentStatus == 'Received in Warehouse') {
                myColor = 'green';
            }
            else if (currentStatus == 'Driver Scheduled') {
                myColor = 'red';
             }
            else if (currentStatus == 'Cancelled') {
                myColor = 'yellow';
            }           

        }

    else if (dataFor == 'Received in Warehouse')
    {
         if (currentStatus == 'Received in Warehouse') {
                myColor = 'green';
                 }
        else if (currentStatus == 'Collection in Progress') {
            myColor = 'red';
        }
        else if (currentStatus == 'Cancelled') {
            myColor = 'yellow';
        }
      }
        return myColor;
}

function addNewReturnOrder()
{

    //alert("Hello, world!");
    $("#wms-srv-addtransfer-popup .pnlWmsHead").show();
    $("#wms-srv-addtransfer-popup").show();
    $("#btnshwoHideSku").show();
    $('#wms-srv-addtransfer-popup .wms-srv-datepicker').datepicker({ dateFormat: 'yy-mm-dd' });
    $("#setTotalRetrnQtylbl").html(0);
    $("#ddlDeptWiseUserList").html('<option onclick="' + getUserId + '">' + gloablUserName + '</option>');
    $(locadd).hide();
    $(txtAddress).hide();
    
    setTodayDate();
    AddCustomerListddl();
    ddlReturnCatgOrCondtion();
    
}

function showSkuPopup() {
    $("#wms-srv-skusearch-popup").show();
    getSkuList();
    $("#wms-srv-skusearch-popup-close").click(function () {
        $("#wms-srv-skusearch-popup").off();
        $("#txtSearchSkuVal").val('');
        $("#wms-srv-skusearch-popup").hide();
    });
}

function showDocumentPopup(oid, ObjName) {
    documentOrderId = oid;
    documentObject = ObjName;
    getDocumentListByObject();
    $("#wms-srv-document-popup .pnlWmsDetail").show();
    $("#wms-srv-document-popup").show();
    $("#wms-srv-document-popup-close").click(function () {
        globalFileName = [];
        globalFilePath = [];
        $("#wms-srv-document-popup").off();
        $("#wms-srv-document-popup").hide();
    });
}

function showOrderHistoryPopup(obj) {
    OrderHistory(obj)
    $("#wms-srv-OrderHistory-popup .pnlWmsHead").show();
    $("#wms-srv-OrderHistory-popup").show();
    $("#wms-srv-OrderHistory-popup-close").click(function () {
        $("#wms-srv-OrderHistory-popup").off();
        $("#wms-srv-OrderHistory-popup").hide();
    });
}

function showExcelImport() {
    window.open("../RMS/RMSImport.aspx", "_self");
    /*$("#wms-srv-import-Excel").show();
    $("#wms-srv-import-Excel-Close").click(function () {
        $("#wms-srv-import-Excel").off();
        $("#wms-srv-import-Excel").hide();
    });*/
}

function showUploadFile() {
    $("#WmsPage_step_filetype").hide();
    $("#step_filetype").removeClass('activeStep');
    $("#step_validatefile").removeClass('activeStep');
    $("#stepsLable").removeClass('activeStep');
    $("#step_mapcolumns").addClass('activeStep');
    $("#WmsPage_step_mapcolumns").show();
}

function EditReturnOrder() {
    
    $("#wms-srv-EditReturnOrder-popup").show();
    $("#wms-srv-EditReturnOrder-popup-close").click(function () {
        $("#wms-srv-EditReturnOrder-popup").off();
        $("#wms-srv-EditReturnOrder-popup").hide();
    });
}

function showLocationList() {

    $("#wms-srv-LocationList-popup").show();
    $("#wms-srv-LocationList-popup-close").click(function () {
        $("#wms-srv-LocationList-popup").off();
        $("#txtLocationSrch").val('');
        $("#wms-srv-LocationList-popup").hide();
    });
}

function ViewOrderList(getRetrnID) {
    var apiPath = wmsApiPath + 'Getreturnorderhead';

    var postData = {
        "returnid": getRetrnID,
        "paramval": "Head"
    }
    callHttpUrl(apiPath, postData, function (data) {
        debugger;
        var isStatus = data.Status;

        if (isStatus == 200) {

            var getViewTitle = data.Result.Table[0].Title;
            var getViewreferenceNo = data.Result.Table[0].referenceNo;
            var getViewreturnID = data.Result.Table[0].returnID;
            var getViewstatusid = data.Result.Table[0].statusid;
            var getViewstatus = data.Result.Table[0].status;
            var getViewreturndate = data.Result.Table[0].returndate;
            var getViewexpdcollection = data.Result.Table[0].expdcollection;
            var getViewcompanyid = data.Result.Table[0].companyid;
            var getViewCompanyName = data.Result.Table[0].CompanyName;
            var getViewstoreid = data.Result.Table[0].storeid;
            var getViewTerritory = data.Result.Table[0].Territory;
            var getViewColumn1 = data.Result.Table[0].Column1;
            var getViewLocationCode = data.Result.Table[0].LocationCode;

            var getViewLocationName = data.Result.Table[0].LocationName;

            var getViewPriority = data.Result.Table[0].Priority;
            var getViewreturnby = data.Result.Table[0].returnby;
            var getViewRequestedby = data.Result.Table[0].Requestedby;
            var getViewRetuencategoryID = data.Result.Table[0].RetuencategoryID;
            var getViewReturnCategory = data.Result.Table[0].ReturnCategory;
            var getViewconditionid = data.Result.Table[0].conditionid;
            var getViewcondition = data.Result.Table[0].condition;
            var getViewRemark = data.Result.Table[0].Remark;
            var getViewTotalorderqty = data.Result.Table[0].Totalorderqty;
            var getViewTotalreturnqty = data.Result.Table[0].Totalreturnqty;
            var getViewTotalactualreturnqty = data.Result.Table[0].Totalactualreturnqty;
            var getViewLocAddress = data.Result.Table[0].locaddress;

            $("#viewReturnOrderTitle").val(getViewTitle);
            $("#viewReturnOrderRefNo").val(getViewreferenceNo);
            $("#viewReturnOrderId").val(getViewreturnID);
            $("#viewReturnOrdeStatus").val(getViewstatus);
            $("#viewReturnOrdeReturnDate").val(getViewreturndate);
            $("#viewReturnOrdeExpDate").val(getViewexpdcollection);
            $("#viewReturnOrdeCustomerName").val(getViewCompanyName);
            $("#viewReturnOrdeDepartmentName").val(getViewTerritory);
            $("#viewReturnOrdeLocCode").val(getViewLocationCode);

            $("#viewReturnOrdeLocName").val(getViewLocationName);
            
            $("#viewReturnOrdePriority").val(getViewPriority);
            $("#viewReturnOrdeRequestedBy").val(getViewRequestedby);
            $("#viewReturnOrdeReturnCategory").val(getViewReturnCategory);
            $("#viewReturnOrdeCondition").val(getViewcondition);
            $("#viewReturnOrdeRemark").val(getViewRemark);  

            if (getViewTitle == 'E-Commerce Order') {
                $(locadd).show();
               // $("#viewReturnOrdeAddress").show();
                $("#viewReturnOrdeAddress").hide();
                $("#viewReturnOrdeAddress").val(getViewLocAddress);
            } else {
                $(locadd).hide();
                $("#viewReturnOrdeAddress").hide();
                $(txtAddress).hide();
            }
            

            

            viewDetailReturnOrder(getRetrnID);
        }
    });
   
}

function viewDetailReturnOrder(RetnId) {

    var apiPath = wmsApiPath + 'Getreturnorderhead';

    var postData = {
        "returnid": RetnId,
        "paramval": "Detail"
    }
    callHttpUrl(apiPath, postData, function (data) {
        var isStatus = data.Status;

        if (isStatus == 200) {

            $("#viewReturnOrderGrid").html('');

            gridList = '';

            gridList = gridList + '<div class="wms-srv-grid-header" style="white-space:nowrap; text-align:center">';
            //gridList = gridList + '<div class="wms-srv-grid-cell" style="width:60px;">SR. No.</div>';
            gridList = gridList + '<div class="wms-srv-grid-cell">SKU Code</div>';
            gridList = gridList + '<div class="wms-srv-grid-cell">SKU Name</div>';
            gridList = gridList + '<div class="wms-srv-grid-cell">UOM</div>';
            gridList = gridList + '<div class="wms-srv-grid-cell">Order Quantity</div>';
            gridList = gridList + '<div class="wms-srv-grid-cell">Actual Return Qty</div>';
            gridList = gridList + '<div class="wms-srv-grid-cell">Return Quantity</div>';
            gridList = gridList + '</div>';

            var totalQty = 0;
            var mygridResult = data.Result.Table
            for (var i = 0; i < mygridResult.length;i++)
            {
                var getid = mygridResult[i].id;
                var getskuid = mygridResult[i].skuid
                var getskucode = mygridResult[i].skucode
                var getskuname = mygridResult[i].skuname
                var getdescription = mygridResult[i].description
                var getUOM = mygridResult[i].UOM
                var getorderqty = mygridResult[i].orderqty
                var getreturnqty = mygridResult[i].returnqty
                var getactualreturnqty = mygridResult[i].actualreturnqty               

                gridList = gridList + '<div class="wms-srv-grid-row">';
                //gridList = gridList + '<div class="wms-srv-grid-cell">' + getskuid+'</div>';
                gridList = gridList + '<div class="wms-srv-grid-cell">' + getskucode +'</div>';
                gridList = gridList + '<div class="wms-srv-grid-cell">' + getskuname +'</div>';
                gridList = gridList + '<div class="wms-srv-grid-cell">' + getUOM +'</div>';
                gridList = gridList + '<div class="wms-srv-grid-cell">' + getorderqty +'</div>';
                gridList = gridList + '<div class="wms-srv-grid-cell">' + getactualreturnqty +'</div>';
                gridList = gridList + '<div class="wms-srv-grid-cell">' + getreturnqty +'</div>';
                gridList = gridList + '</div>';


                totalQty += getreturnqty;
            }
            $("#viewReturnOrderGrid").append(gridList);
            $("#lblViewTotalQty").html(totalQty);
            $("#wms-srv-ViewOrder-popup .pnlWmsHead").show();
            $("#wms-srv-ViewOrder-popup").show();

        }
    });
}

function isNumber(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;
    return true;
}

function validateEmailAddress(emailVal, EmailID) {
    // alert(emailVal.value);


    var email = emailVal.value;
    var isEmailValid = true;
    var mailformat = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;

    if (email.match(mailformat)) {
        //isEmailValid = true;

    }
    else {
        // isEmailValid = false;
        alert("Enter correct email.");
        if (EmailID == "0") {
            $("#txtemail").val('');
            $("#txtemail").focus();
        }
        else {
            // $("#txtEditemailid").val('EmailID');
            $("#txtEditemailid").val('');
            // $("#txtEditemailid").focus();
        }
    }
    //return isEmailValid;
}

function checkForCustomer() {
    var checkOrderheadId = $("#setAddReturnOrderId").val();
    if (checkOrderheadId != 'To be generated') {
        var confimrVal = confirm("If you close will clear entered order details, Do you want to close?");
        if (confimrVal == true) {

            removeCurrentOrder();
            clearAddNewOrderPopup();
            //$("#wms-srv-addtransfer-popup").hide();
            //$("#setTotalRetrnQtylbl").html('');
            var getCurrentVal = $("#ddlCustomerList").val();
            $("#ddlCustomerList").attr('data-val', getCurrentVal);
        } else {
            alert("You can continue with this order");
            var getCurrentVal = $("#ddlCustomerList").attr('data-val');
            $("#ddlCustomerList").attr('onchange', 'return false;');
            $("#ddlCustomerList").val(getCurrentVal);
            $("#ddlCustomerList").attr('onchange', 'AddDepartmentListddl();');
            //returnOrderList(checkOrderheadId);
        }
    }
}

function AddCustomerListddl() {

    var apiPath = wmsApiPath + 'CustomerList';

    var postData = {
        "UserId": getUserId
    };
    callHttpUrl(apiPath, postData, function (data) {
        var isStatus = data.Status;
        if (isStatus == 200) {

            $("#ddlCustomerList").html('');
            $("#ddlCustomerList").html('<option value="0">-- Select Customer --</option>');
            var myGridList = data.Result;
            var CustomerList = myGridList.Table;
            if (CustomerList.length > 0) {
                for (var i = 0; i < CustomerList.length; i++)
                {
                    var Id = CustomerList[i].Id;
                    var CustomerName = CustomerList[i].Name;                  
                    $("#ddlCustomerList").append('<option value = "' + Id + '">' + CustomerName + '</option>');
                }
                if (getLoginUserType == "Super Admin") {
                    $("#ddlCustomerList").val(0);
                }
                else {

                $("#ddlCustomerList").val(Id);
                }
                var getCurrentVal = $("#ddlCustomerList").val();
                $("#ddlCustomerList").attr('data-val', getCurrentVal);
            }
            AddDepartmentListddl();

        }
    });
}

function AddDepartmentListddl()
{
    //var checkOrderheadId = $("#setAddReturnOrderId").val();
    //if (checkOrderheadId != 'To be generated') {
    //    var confimrVal = confirm("If you close will clear entered order details, Do you want to close?");
    //    if (confimrVal == true) {

    //        removeCurrentOrder();
    //        clearAddNewOrderPopup();
    //        //$("#wms-srv-addtransfer-popup").hide();
    //        //$("#setTotalRetrnQtylbl").html('');
    //        var getCurrentVal = $("#ddlCustomerList").val();
    //        $("#ddlCustomerList").attr('data-val', getCurrentVal);
    //    } else {
    //        alert("You can continue with this order");
    //        var getCurrentVal = $("#ddlCustomerList").attr('data-val');
    //        $("#ddlCustomerList").attr('onchange', 'return false;');
    //        $("#ddlCustomerList").val(getCurrentVal);
    //        $("#ddlCustomerList").attr('onchange', 'AddDepartmentListddl();');
    //        //returnOrderList(checkOrderheadId);
    //    }
    //} else {
    var apiPath = wmsApiPath + 'getDepartmentList';
    var getCustomerId = $("#ddlCustomerList").val();
    if (getCustomerId != "" && getCustomerId !=0) {

        var postData = {
            "UserId": getUserId,
            "CustomerId": getCustomerId
        }
        callHttpUrl(apiPath, postData, function (data) {
            var isStatus = data.Status;
            if (isStatus == 200) {
                $("#ddlDepartmentList").html('');
                $("#ddlDepartmentList").html('<option value="0">-- Select Department --</option>');
                var myGridList = data.Result;
                var DepartmentList = myGridList.Table;
                if (DepartmentList.length > 0) {
                    for (var i = 0; i < DepartmentList.length; i++) {
                        var Id = DepartmentList[i].id;
                        var DepartmentName = DepartmentList[i].DepartmentName;
                        $("#ddlDepartmentList").append('<option value = "' + Id + '">' + DepartmentName + '</option>');
                    }

                }
                if (getLoginUserType == "Super Admin") {
                    $("#ddlDepartmentList").val(0);
                }
                else {
                    $("#ddlDepartmentList").val(Id);
                }
                //bidnDepartmentWiseUserlist();
               // $('#wmsSkuDetailsSalesOrderGrid .wmSkuDetailsSalesOrderRow').html('');
              //  $('#wmsSkuDetailsSalesOrderGrid').html('');
               // $('#wmsSkuDetailsSalesOrderGrid .wms-srv-grid-row').html('');
               
            }
            
        });
    }
        else {
            $("#ddlDepartmentList").html('<option onclick="checkCustomerValidation();">-- Select Department --</option>');
            $("#ddlDeptWiseUserList").html('<option onclick="' + getUserId + '">' + gloablUserName + '</option>');
            //$("#ddlDepartmentList").click(function () {
            //    checkCustomerValidation();
            //});
            //$("#ddlDeptWiseUserList").click(function () {
            //    checkCustomerValidation();
            //});
            }
    //    }
    //}
  //  }
}

function checkOrderDeptList() {
    var checkSkuGridLgth = $("#wmsSkuDetailsSalesOrderGrid .wmsFrmAddRow_0").length;
    if (checkSkuGridLgth > 0) {
        var checkVal = confirm("You are Chaning Department All Sku Added will be Remove?");
        if (checkVal) {
            $('#wmsSkuDetailsSalesOrderGrid .wmSkuDetailsSalesOrderRow').html('');
            $('#wmsSkuDetailsSalesOrderGrid .wms-srv-grid-row').html('');
            getSkuList();
        }
    }
}

function bidnDepartmentWiseUserlist() {
    //alert('bidnDepartmentWiseUserlist');
    var checkOrderheadId = $("#setAddReturnOrderId").val();
    if (checkOrderheadId != 'To be generated') {
        removeCurrentOrder();
        clearAddNewOrderPopup();
        //var confimrVal = confirm("If you close will clear entered order details, Do you want to close?");
        //if (confimrVal == true) {

        //    removeCurrentOrder();
        //    clearAddNewOrderPopup();
        //    //$("#wms-srv-addtransfer-popup").hide();
        //    //$("#setTotalRetrnQtylbl").html('');
        //    var getCurrentVal = $("#ddlCustomerList").val();
        //    $("#ddlCustomerList").attr('data-val', getCurrentVal);
        //} else {
        //    //alert("You can continue with this order");
        //    //var getCurrentVal = $("#ddlCustomerList").attr('data-val');
        //    //$("#ddlCustomerList").attr('onchange', 'return false;');
        //    //$("#ddlCustomerList").val(getCurrentVal);
        //    //$("#ddlCustomerList").attr('onchange', 'AddDepartmentListddl();');
        //    //returnOrderList(checkOrderheadId);
        //}
    } else {
             $('#wmsSkuDetailsSalesOrderGrid .wms-srv-grid-row').html('');
            }
}
//function bidnDepartmentWiseUserlist() {
//    var apiPath = wmsApiPath + 'DeptUserList';
//    var getCustomerId = $("#ddlCustomerList").val();
//    var DepartmentId = $("#ddlDepartmentList").val();
//    if (getCustomerId != "") {

//        var postData = {
//            "UserId": getUserId,
//            "DepartmentId": DepartmentId
//        }
//        callHttpUrl(apiPath, postData, function (data) {
//            var isStatus = data.Status;
//            var dataLength = data.Result.Table[0];
//            if (dataLength != '') {
//                if (isStatus == 200) {
//                    $("#ddlDeptWiseUserList").html('');
//                    //$("#ddlDeptWiseUserList").html('<option >-- Select User --</option>');
//                    var myGridList = data.Result;
//                    var deptWiseUserList = myGridList.Table;
//                    if (deptWiseUserList.length > 0) {
//                        for (var i = 0; i < deptWiseUserList.length; i++) {
//                            var Id = deptWiseUserList[i].id;
//                            var Username = deptWiseUserList[i].Username;
//                            $("#ddlDeptWiseUserList").append('<option value = "' + Id + '">' + Username + '</option>');
//                        }
//                    }
//                }                
//            }
//        });
//    }
//    else {
//           $("#ddlDeptWiseUserList").html('<option >-- Select User --</option>');
//    }
//}

function checkCustomerValidation() {
    var getCustomerId = $("#ddlCustomerList").val();
    if (getCustomerId == "") {
        alert("Please Select Customer");
    }
}

function LocationList() {
   
    var apiPath = wmsApiPath + 'getLocationList';
    var getCustomerId = $("#ddlCustomerList").val();
    if (getCustomerId != 0) {
        var searchVal = ''
        searchVal = $("#txtLocationSrch").val();
        if (searchVal == "") {
            searchVal = "";
        }
        var postData = {
            "UserId": getUserId,
            "CustomerId": getCustomerId,
            "Searchval": searchVal
        }
        callHttpUrl(apiPath, postData, function (data) {
            var isStatus = data.Status;
            if (isStatus == 200) {
                $("#wms-srv-grid-LocationList").html('');

                var gridList = '';
                gridList = '<div class="wms-srv-grid-header">';
                gridList = gridList + '<div class="wms-srv-grid-cell wms-srv-align">Location Code</div>';
                gridList = gridList + '<div class="wms-srv-grid-cell wms-srv-align">Location Name</div>';
                gridList = gridList + '<div class="wms-srv-grid-cell wms-srv-align">Address</div>';
                gridList = gridList + '<div class="wms-srv-grid-cell wms-srv-align">Email Id</div>';
                gridList = gridList + '<div class="wms-srv-grid-cell wms-srv-align">Mobile No</div>';
                gridList = gridList + '<div class="wms-srv-grid-cell wms-srv-align">Action</div>';
                gridList = gridList + '</div >';

                var myGridList = data.Result;
                var DataList = myGridList.Table;
                if (DataList.length > 0) {
                    for (var i = 0; i < DataList.length; i++) {
                        var Id = DataList[i].Id;
                        var getAddressLine1 = DataList[i].AddressLine1;
                        var getLocationCode = DataList[i].LocationCode;
                        var getName = DataList[i].Name;
                        var getEmail = DataList[i].Email;
                        var getMobileNo = DataList[i].MobileNo;

                        gridList = gridList + '<div class="wms-srv-grid-row wms-srv-grid-add">';
                        gridList = gridList + '<div class="wms-srv-grid-cell">' + getLocationCode + '</div>';
                        gridList = gridList + '<div class="wms-srv-grid-cell">' + getName + '</div>';
                        gridList = gridList + '<div class="wms-srv-grid-cell">' + getAddressLine1 + '</div>';
                        gridList = gridList + '<div class="wms-srv-grid-cell">' + getEmail + '</div>';
                        gridList = gridList + '<div class="wms-srv-grid-cell">' + getMobileNo + '</div>';
                        gridList = gridList + '<div class="wms-srv-grid-cell">';
                        gridList = gridList + '<div class="wms-srv-grid-action"><a href="#" title="Save" class="wms-srv-save"><i class="fas fa-check-circle" onclick="setLocationCode(\'' + Id + '\',\'' + getLocationCode + '\',\'' + getName + '\');"></i></a></div>';
                        gridList = gridList + '</div>';
                        gridList = gridList + '</div>';
                    }
                    $("#wms-srv-grid-LocationList").append(gridList);
                }

            }
            showLocationList();
        });
    }
    else {
        alert("Please Select Customer");
    }
}

function setLocationCode(LocId, locCode, locName) {
    $("#hdnAddLocationId").val(LocId);
    $("#txtAddLocationCode").val(locCode);
    $("#txtAddLocationName").val(locName); //added by suraj khopade
    
    $("#txtLocationSrch").val('');
    $("#wms-srv-LocationList-popup").hide();
}

function ClearSearchSkuList() {
    $("#txtSearchSkuVal").val('');
    getSkuList();
}

function setTodayDate() {
    var currentDate = new Date();
    var year = currentDate.getFullYear();
    var month = (currentDate.getMonth() + 1).toString().padStart(2, '0'); // Months are 0-indexed
    var day = currentDate.getDate().toString().padStart(2, '0');

    var formattedDate = year + '-' + month + '-' + day;

    document.getElementById('txtAddReturnDate').value = formattedDate;
    setNextDate();
}

function setNextDate() {

    var dateInput = document.getElementById('txtAddExpDateDate');
    var currentDate = new Date();
    // Get the current date from the input field

    // Add one day to the current date
    currentDate.setDate(currentDate.getDate() + 1);

    // Format the next date to 'YYYY-MM-DD'
    var year = currentDate.getFullYear();
    var month = (currentDate.getMonth() + 1).toString().padStart(2, '0');
    var day = currentDate.getDate().toString().padStart(2, '0');
    var nextDate = year + '-' + month + '-' + day;
    dateInput.value = nextDate;
   
}

function expDateSetasRetrn() {
 
    var getreturnDate = $("#txtAddReturnDate").val();
    var dateInput = document.getElementById('txtAddExpDateDate');
    var currentDate = new Date(getreturnDate);

    var todaysDate = new Date();
    var year = todaysDate.getFullYear();
    var month = (todaysDate.getMonth() + 1).toString().padStart(2, '0');
    var day = todaysDate.getDate().toString().padStart(2, '0');
    var SelectedCurntDate = year + '-' + month + '-' + day;

    if (SelectedCurntDate <= getreturnDate)
    {
        currentDate.setDate(currentDate.getDate() + 1);
        var year = currentDate.getFullYear();
        var month = (currentDate.getMonth() + 1).toString().padStart(2, '0');
        var day = currentDate.getDate().toString().padStart(2, '0');
        var nextDate = year + '-' + month + '-' + day;
        dateInput.value = nextDate;
    }
    else {
        alert("Please Select Current Or Future Date");
        $("#txtAddReturnDate").val(SelectedCurntDate);
    }  
}

function expDateCollectionDate() {
 

    var dateInput = $('#txtAddExpDateDate').val();
    var getreturnDate = $("#txtAddReturnDate").val();

    var todaysDate = new Date();
    var year = todaysDate.getFullYear();
    var month = (todaysDate.getMonth() + 1).toString().padStart(2, '0');
    var day = todaysDate.getDate().toString().padStart(2, '0');
    var SelectedCurntDate = year + '-' + month + '-' + day;

    if (SelectedCurntDate <= dateInput && getreturnDate <= dateInput) {
        currentDate.setDate(currentDate.getDate() + 1);
        var year = currentDate.getFullYear();
        var month = (currentDate.getMonth() + 1).toString().padStart(2, '0');
        var day = currentDate.getDate().toString().padStart(2, '0');
        var nextDate = year + '-' + month + '-' + day;
        dateInput.value = nextDate;
    }
    else {
        alert("Please Select Current Or Future Date");
        $("#txtAddExpDateDate").val(getreturnDate);
    } 
}

function getSkuList() {
  
    var apiPath = wmsApiPath + 'getSkuList';
    var getDepartmentId = $("#ddlDepartmentList").val();
    var searchVal = ''
    searchVal = $("#txtSearchSkuVal").val();
    if (searchVal == "") {
        searchVal = "";
    }
    var postData = {
        "currentPage": CurrentPage,
        "recordLimit": RecordLimitSku,
        "UserId": getUserId,
        "DepartmentId": getDepartmentId,
        "SearchVal": searchVal
    }
    callHttpUrl(apiPath, postData, function (data) {
        var isStatus = data.Status;
        if (isStatus == 200) {

            var getSkuId = [];
            var getSkuCode = [];
            var getSkuName = [];
            var getDescription = [];
            var getUOMId = [];
            var getUOM = [];

            $("#srv-grid-SkuList").html('');

            
            $("#srv-grid-SkuList").append('<div class="wms-srv-grid-header">' +
                '<div class="wms-srv-grid-cell" style="text-align:center;">SKU Code</div>' +
                '<div class="wms-srv-grid-cell" style="text-align:center;">SKU Name</div>' +
                '<div class="wms-srv-grid-cell" style="text-align:center;">Description </div>' +
                '<div class="wms-srv-grid-cell" style="text-align:center;">Action </div>' +
                '</div>');

            var myGridList = data.Result;
            var TotalRecord = myGridList.Table[0].TotalRecord;
            var DataList = myGridList.Table1;
            for (var i = 0; i < DataList.length; i++)
            {
                var rowNum = i + 1;
                 getSkuId[i] = DataList[i].ProductId;
                getSkuCode[i] = DataList[i].SkuCode;
                getSkuName[i] = DataList[i].SkuName;
                getDescription[i] = DataList[i].Description;
                getUOMId[i] = DataList[i].UOMID;
                getUOM[i] = DataList[i].UomDescription;

                $("#srv-grid-SkuList").append('<div class="wms-srv-grid-row">' +
                    '<div class="wms-srv-grid-cell" style="text-align:center;">' + getSkuCode[i] + '</div>' +
                    '<div class="wms-srv-grid-cell" style="text-align:center;">' + getSkuName[i] + '</div>' +
                    '<div class="wms-srv-grid-cell" style="text-align:center;">' + getDescription[i] + '</div>' +
                    '<div class="wms-srv-grid-cell">' +
                    '<div class="wms-srv-grid-action">' +
                    '<a href="#" title="Save" class="wms-srv-save btnSelectProdFromList" data-pos="' + i + '" data-isselected="no"><i class="fas fa-check-circle iconView"></i></a>' +
                    '</div>' +
                    '</div>' +
                    '</div>');
            }
            
            $('.btnSelectProdFromList').click(function () {
                var isSelected = $(this).attr('data-isselected');
                if (isSelected == 'no') {
                    $(this).attr('data-isselected', 'yes');
                    $(this).find('i').attr('style', 'color:#78b421');
                } else {
                    $(this).attr('data-isselected', 'no');
                    $(this).find('i').attr('style', '');
                }
            });

            $("#btnAddSkuToOutwardOrder").off();
            $('#btnAddSkuToOutwardOrder').click(function () {
             
                //$('#wmsSkuDetailsSalesOrderGrid .wmsFrmAddRow').remove();
                $('.btnSelectProdFromList[data-isselected="yes"]').each(function () {
                    var listItemObj = $(this);
                    var k = Number($(this).attr('data-pos'));
                    addMultipleProduct(getSkuId[k], getSkuCode[k], getSkuName[k], getUOMId[k], getUOM[k]);
                    checkForAddNewSkuRow();
                    $("#wms-srv-skusearch-popup").hide();
                    //$('#wms-srv-sales-product-list-popup').hide();
                });
            });                       

        }
        setupGridPagingList('tblSkuListPage', CurrentPage, TotalRecord, getSkuList)
    });
}

function checkForAddNewSkuRow() {
    var rowLength = $('#wmsSkuDetailsSalesOrderGrid .wms-srv-grid-row').length;
    if (rowLength > 0) {
        $('#pnlNoSkuRecord').hide();
    } else {
        $('#pnlNoSkuRecord').show();
    }
}

function addMultipleProduct(getSkuId, getSkuCode, getSkuName, getUOMId, getUOM) {
    
    var addNewRowHtml = '<div class="wms-srv-grid-row wmsFrmAddRow_' + addNewRowCounter + '" >' +
        //'<div class="wms-srv-grid-cell"></div>' +
        '<div class="wms-srv-grid-cell" id="txtSo_SKUCode_' + addNewRowCounter + '" ></div>' +
        '<div class="wms-srv-grid-cell" id="txtSo_SKUName_' + addNewRowCounter + '"></div>' +
        '<div class="wms-srv-grid-cell" id="txtSo_UOM_' + addNewRowCounter + '"></div>' +
        '<div class="wms-srv-grid-cell"><input type="text" placeholder="0" id="txtSo_RequestedQty_' + addNewRowCounter + '" value="0" class="notranslate"></div>' +
        '<div class="wms-srv-grid-cell">0</div>' +
        '<div class="wms-srv-grid-cell"><input type="text" id="txtSo_ReturnQty_' + addNewRowCounter + '" value="" class="notranslate"></div>' +
        '<div class="wms-srv-grid-cell">' +
        '<div class="wms-srv-grid-action">' +
        '<button class="wms-srv-input wms-srv-button " type="button" title="Save" data-prefix="SV" onclick="saveReturnOrerSkuDetails(' + addNewRowCounter + ');"><i class="fas fa-check-circle"></i><span>Save</span></button>' +
        '<div class="wms-srv-action-sep" style="margin-right: 3px;margin-left: 3px;">|</div>' +
        '<button class="wms-srv-input wms-srv-button " type="button" title="Save" data-prefix="SV" onclick="removeAddNewRow(' + addNewRowCounter + ');"><i class="fas fa-times-circle"></i><span>Cancel</span></button>' +
        '</div>'+
        '</div>'+
        '</div>'
    $('#wmsSkuDetailsSalesOrderGrid .wms-srv-grid-header').after(addNewRowHtml);

    $('#txtSo_SKUCode_' + addNewRowCounter).html(getSkuCode);
    $('#txtSo_SKUCode_' + addNewRowCounter).attr('data-id', getSkuId);

    $('#txtSo_SKUName_' + addNewRowCounter).html(getSkuName);
    $('#txtSo_UOM_' + addNewRowCounter).html(getUOM);
    $('#txtSo_UOM_' + addNewRowCounter).attr('data-id', getUOMId);
    addNewRowCounter = addNewRowCounter + 1;
}

function removeAddNewRow(rowNum) {
    $('#wmsSkuDetailsSalesOrderGrid .wmsFrmAddRow_' + rowNum).remove();
    checkForAddNewSkuRow();
}

function ddlReturnCatgOrCondtion() {

    var apiPath = wmsApiPath + 'ddlCategOrConditionList';
  
    var postData = {
        "UserId": getUserId
    };
    callHttpUrl(apiPath, postData, function (data) {
        var isStatus = data.Status;
        if (isStatus == 200) {
            
            $("#ddlReturnCategory").html('');          
            $("#ddlReturnCategory").html('<option value="">-- Select Category --</option>');          

            var myGridList = data.Result;
            var ReturnCategory = myGridList.Table;
           

            if (ReturnCategory.length > 0) {
                for (var i = 0; i < ReturnCategory.length; i++)
                {
                    var Id = ReturnCategory[i].Id;
                    var ReturnVal = ReturnCategory[i].Value;
                    var getParameter = ReturnCategory[i].Object;
                    $("#ddlReturnCategory").append('<option value = "' + Id + '">' + ReturnVal + '</option>');
                }
            }

            $("#ddlReturnCondition").html('');
            $("#ddlReturnCondition").html('<option value="">-- Select Condition --</option>');
            var ReturnCondition = myGridList.Table1;
            if (ReturnCondition.length > 0)
            {
                for (var k = 0; k < ReturnCondition.length; k++) {
                    var getId = ReturnCondition[k].Id;
                    var ReturnConVal = ReturnCondition[k].Value;
                    var getPara = ReturnCondition[k].Object; 
                    $("#ddlReturnCondition").append('<option value = "' + getId + '">' + ReturnConVal + '</option>');
                }
            }

        }
    });
}

function OrderHistory(getReturnOID) {
    var apiPath = wmsApiPath + 'getOrderHistoryList';
    var postData = {
        "UserId": getUserId,
        "ReturnOrderId": getReturnOID
    };
    callHttpUrl(apiPath, postData, function (data) {
        var isStatus = data.Status;
        if (isStatus == 200) {

            $("#srvgridOrderHistory").html('');
            var gridlist = '';
            gridlist = gridlist + '<div class="wms-srv-grid-header" style="white-space: nowrap;">';
            gridlist = gridlist + '<div class="wms-srv-grid-cell">Order No</div>';
            gridlist = gridlist + '<div class="wms-srv-grid-cell">Status</div>';
            gridlist = gridlist + '<div class="wms-srv-grid-cell">Date</div>';
            gridlist = gridlist + '<div class="wms-srv-grid-cell">Updated By</div>';
            gridlist = gridlist + '</div>';

            var myGridList = data.Result;
            var dataSetList = myGridList.Table;

            for (var i = 0; i < dataSetList.length; i++)
            {
                var getId = dataSetList[i].id;
                var getobject = dataSetList[i].object;
                var getreferenceid = dataSetList[i].referenceid;
                var getStatusId = dataSetList[i].statusid;
                var getStatus = dataSetList[i].Status;
                var getdate = dataSetList[i].date;
                var getUserId = dataSetList[i].UserId;

                gridlist = gridlist + '<div class="wms-srv-grid-row">';
                gridlist = gridlist + '<div class="wms-srv-grid-cell">' + getreferenceid+'</div>';
                gridlist = gridlist + '<div class="wms-srv-grid-cell">' + getStatus+'</div>';
                gridlist = gridlist + '<div class="wms-srv-grid-cell">' + getdate+'</div>';
                gridlist = gridlist + '<div class="wms-srv-grid-cell">' + getUserId+'</div>';
                gridlist = gridlist + '</div>';
            }
            $("#srvgridOrderHistory").append(gridlist);
        }
    });
}

// Document Code
function getDocumentListByObject() {

    $('#hdnCommonDocument_RefId').val(documentOrderId);
    var ReturnOrderId = $('#hdnCommonDocument_RefId').val();
    var apiPath = wmsApiPath + "DocumentList";
    var postData = {     
        "UserId": getUserId,
        "RMSRetnOrderId": ReturnOrderId,
        "Object": DocObject
    };
    callHttpUrl(apiPath, postData, function (data) {
        var getStatus = data.Status;
        var getStatusCode = data.StatusCode;
        if (getStatusCode.toLocaleLowerCase() == 'success') {

            var getDocumentList = data.Result.Table;
            $("#wmsGridDocumentList").html('');
            gridList = '';
            gridList = gridList + '<div class="wms-srv-grid-header">';
            gridList = gridList + '<div class="wms-srv-grid-cell">Document Title <span class="requiredStar" style="color: red;">*</span></div>';
            gridList = gridList + '<div class="wms-srv-grid-cell">Description <span class="requiredStar" style="color: red;">*</span></div>';
            gridList = gridList + '<div class="wms-srv-grid-cell">Document Type <span class="requiredStar" style="color: red;">*</span></div>';
            gridList = gridList + '<div class="wms-srv-grid-cell">File Type <span class="requiredStar" style="color: red;">*</span></div>';
            //gridList = gridList + '<div class="wms-srv-grid-cell">Attachment <span class="requiredStar" style="color: red;">*</span></div>';
            gridList = gridList + '<div class="wms-srv-grid-cell">Action</div>';
            gridList = gridList + '</div>';
            gridList = gridList + '<div class="wms-srv-grid-row wms-srv-grid-add">';
            gridList = gridList + '<div class="wms-srv-grid-cell">';
            gridList = gridList + '<input name="" type="text" maxlength="50" id="txtCommonDocument_Title" class="inputTextReturnOrder notranslate" style="width: 200px; margin-left: auto; margin-right: auto;" />';
            gridList = gridList + '</div>';
            gridList = gridList + '<div class="wms-srv-grid-cell">';
            gridList = gridList + '<input name="" type="text" maxlength="50" id="txtCommonDocument_Description" class="inputTextReturnOrder notranslate" style="width: 200px; margin-left: auto; margin-right: auto;" />';
            gridList = gridList + '</div>';
            gridList = gridList + '<div class="wms-srv-grid-cell">';
            gridList = gridList + '<input name="" type="text" maxlength="50" id="txtCommonDocument_Type" class="inputTextReturnOrder notranslate" style="width: 200px; margin-left: auto; margin-right: auto;" />';
            gridList = gridList + '</div>';
            gridList = gridList + '<div class="wms-srv-grid-cell">';
            gridList = gridList + '<input type="text" data-for="FileType" id="txtCommonDocument_FileType" class="inputTextReturnOrder notranslate" disabled="disabled" style="width: 200px; margin-left: auto; margin-right: auto;">';
            gridList = gridList + '</div>';
            gridList = gridList + '<div class="wms-srv-grid-cell" style="display:none;"  id="txtCommonDocument_Name">';
            gridList = gridList + '</div> ';
            gridList = gridList + '<div class="wms-srv-grid-cell">';
            gridList = gridList + '<div class="wms-srv-grid-action">';
            gridList = gridList + '<div class="wms-srv-file-upload-holder">';
            gridList = gridList + '<label for="wms-srv-cmDocument-file-upload"><i class="fas fa-paperclip"></i><span class="wms-srv-badge" style="display: none;">0</span></label>';
            gridList = gridList + '<div class="wms-srv-action-sep">|</div>';
            gridList = gridList + '<input type="file" id="wms-srv-cmDocument-file-upload" onchange="uploadDocumentAttachment();" style="display: none;" />';
            gridList = gridList + ' </div>';
            gridList = gridList + '<a href="#" title="Save" class="wms-srv-save" onclick="saveDocumentObject();"><i class="fas fa-check-circle"></i></a>';
            gridList = gridList + '</div>';
            gridList = gridList + '</div>';
            gridList = gridList + '</div>';
            $('#wmsGridDocumentList').append(gridList);
            $('#txtCommonDocument_Total').html(getDocumentList.length);
            for (var i = 0; i < getDocumentList.length; i++) {
                var getDocumentId = getDocumentList[i].DocumentId;
                var getDocumentTitle = getDocumentList[i].DocumentTitle;
                var getDescription = getDocumentList[i].Description;
                var getFilePath = getDocumentList[i].DocumentDownloadPath;
                var getFileType = getDocumentList[i].DocumentType;
                var getSaveFilePath = getDocumentList[i].DocumentSavePath;
                var getRefId = getDocumentList[i].ReferenceID;
                var getFileName = getDocumentList[i].FileName;
                var getObj = getDocumentList[i].object;
                

               
                    //'<div class="wms-srv-grid-cell" >' + getFilePath + '</div>' +

                if (getObj == 'RMSDocument') {
                    var strDocumentRow = '<div class="wms-srv-grid-row wmsDocumentRow" id="wmsDocumentRow_' + getDocumentId + '">' +
                        '<div class="wms-srv-grid-cell">' + getDocumentTitle + '</div>' +
                        '<div class="wms-srv-grid-cell">' + getDescription + '</div>' +
                        '<div class="wms-srv-grid-cell">' + getFileType + '</div>' +
                       // '<div class="wms-srv-grid-cell">' + getDocumentAttachmentHtml('../Document/Attach_Document/' + getFilePath, getRefId) + '</div>' +
                        '<div class="wms-srv-grid-cell">' + getDocumentAttachmentHtml(getFilePath, getRefId) + '</div>' +
                        '<div class="wms-srv-grid-cell">' +
                        '<div class="wms-srv-grid-action">' +
                        //'<a href="download-file.ashx?DocId=' + getDocumentId + '" title="Download" class="wms-srv-save"><i class="fas fa-solid fa-arrow-down"></i></a>' +
                        '<a href="download-file.ashx?DocId=' + getDocumentId + '&obj=' + getObj+'" title="Download" class="wms-srv-save"><i class="fas fa-solid fa-arrow-down"></i></a>' +
                        '<div class="wms-srv-action-sep">|</div>' +
                        '<a href="#" title="Cancel" class="wms-srv-cancel" data-prefix="CN" onclick="removeDocumentObject(' + getDocumentId + ',' + getRefId + ');"><i class="fas fa-times-circle"></i></a>' +
                        '</div>' +
                        '</div>' +
                        '</div>';
                    }
                else {
                    var strDocumentRow = '<div class="wms-srv-grid-row wmsDocumentRow" id="wmsDocumentRow_' + getDocumentId + '">' +
                        '<div class="wms-srv-grid-cell">' + getDocumentTitle + '</div>' +
                        '<div class="wms-srv-grid-cell">' + getDescription + '</div>' +
                        '<div class="wms-srv-grid-cell">' + getFileType + '</div>' +
                        '<div class="wms-srv-grid-cell">' + getDocumentAttachmentHtml(getFilePath, getRefId) + '</div>' +
                        '<div class="wms-srv-grid-cell">' +
                        '<div class="wms-srv-grid-action">' +
                        //'<a href="download-file.ashx?DocId=' + getDocumentId + '" title="Download" class="wms-srv-save"><i class="fas fa-solid fa-arrow-down"></i></a>' +
                        //'<a href="download-file.ashx?DocId=' + getDocumentId + '&obj=' + getObj +'" title="Download" class="wms-srv-save"><i class="fas fa-solid fa-arrow-down"></i></a>' +
                        //'<a href="" title="Download" class="wms-srv-save" onclick="DownloadDocument(\'' + getFilePath +'\');"><i class="fas fa-solid fa-arrow-down"></i></a>' +
                        '<a href="' + getFilePath +'" title="Download" class="wms-srv-save" download><i class="fas fa-solid fa-arrow-down"></i></a>' +
                        '<div class="wms-srv-action-sep">|</div>' +
                        '<a href="#" title="Cancel" class="wms-srv-cancel" data-prefix="CN" onclick="removeDocumentObject(' + getDocumentId + ',' + getRefId + ');"><i class="fas fa-times-circle"></i></a>' +
                        '</div>' +
                        '</div>' +
                        '</div>';
                    }
                   

            $('#wmsGridDocumentList').append(strDocumentRow);

            }
        }
        else {
            alert("Error in document list");
        }
    });
}

function DownloadDocument(URL) {
    var url1 = URL;
    window.open(url1, 'self');
}

function getDocumentAttachmentHtml(filePath, Refid) {

    var GetFileExtension = findDocumentFileExtension(filePath);
    if (GetFileExtension != 'png' || GetFileExtension != 'jpg') {
        var strAttachment = '';
        if (filePath.trim() != '') {
            strAttachment = '<span class="wmsTicketAttachment1">' +
                //'<i class="fas fa-paperclip"></i> ' +
                '<span class="themeWMSImportHighlight">' +
                //'<a href="' + wmsAppFiles + '/' + filePath + '" style="cursor: pointer" target="_blank">filePath</a>' +         
                '<img src="' + filePath + '" alt="image Loading.." height="50px" width="50px">' +

                '</span>' +
                '</span>';
        }
    } else {
        var strAttachment = '';
        if (filePath.trim() != '') {
            strAttachment = '<span class="wmsTicketAttachment1">' +
                //'<i class="fas fa-paperclip"></i> ' +
                '<span class="themeWMSImportHighlight">' +
                '<a href="'  + filePath + '" style="cursor: pointer" target="_blank">filePath</a>' +         
               // '<img src="' + filePath + '" alt="image Loading.." height="50px" width="50px">' +

                '</span>' +
                '</span>';
        }
    }

   
    return strAttachment;
}

function findDocumentFileExtension(filePath) {
    debugger;
    var FileExtension = '';
   // fileName = document.querySelector('#wms-srv-cmDocument-file-upload').value;
    extension = filePath.split('.').pop();
  //  $('#txtCommonDocument_FileType').val(extension);
    //  $('#txtCommonDocument_Name').val(fileName);
    return FileExtension;
}

function downloadDocumentFile(fileUrl, fileName) {
    // Create an anchor element
    //var a = document.createElement("a");
    //a.href = fileUrl;
    //a.href = window.URL.createObjectURL(new Blob([fileUrl]));
    //a.download = fileUrl;
    //window.location.href = a.href;
    //a.click();
    window.open(fileUrl);
}

function uploadDocumentAttachment() {

    var file = document.getElementById('wms-srv-cmDocument-file-upload').files[0];
    //alert(file.name);
    var frmFile = new FormData();
    frmFile.append(file.name, file);
    var currentFileName = file.name;
    // showWMSThemeLoading();
    var filterFileName = 'document_' + currentFileName.replace(' ', '_');
    var apiPath = wmsApiPath + 'UploadDocument?OrderId=' + documentOrderId + '&uploadedfilename=' + filterFileName + '&objectname=' + documentObject;
    if (globalFileName.length < 1) {
        $.ajax({
            url: apiPath,
            type: "POST",
            contentType: false,
            processData: false,
            data: frmFile,
            success: function (result) {
                var jsonObj = JSON.parse(result);
                var getStatus = jsonObj["upload_result"][0].status;
                var getPath = jsonObj["upload_result"][0].path;
                if (getStatus == 'success') {
                    alert('File uploaded successfully!!');
                    globalFileName.push(currentFileName);
                    globalFilePath.push(getPath);
                    getDocumentFileExtension(getPath);
                    /* var fileListHtml = '';
                     for (var i = 0; i < globalFileName.length; i++) {
                         fileListHtml += '<div class="wms-srv-filename">' + globalFileName[i] + ' <i class="fas fa-times-circle"></i></div>';
                     }
 txtCommonDocument_FileType
 txtCommonDocument_Name
                     $('#wms-srv-attachment-files').html(fileListHtml);*/
                    //$('#txtCommonDocument_FileType').val(extension);
                    $('#txtCommonDocument_Name').html(getPath);
                    $('#txtCommonDocument_FileType').html(getPath);
                    $('.wms-srv-file-upload-holder .wms-srv-badge').html(globalFileName.length);
                    $('.wms-srv-file-upload-holder .wms-srv-badge').show();

                } else {
                    $('#wms-srv-cmDocument-file-upload').data('srvpath', '');
                }
                //alert(result);
                // hideWMSThemeLoading();
            },
            error: function (err) {
                showWmsAlert('Error!!', err.statusText, 'error');
                //alert(err.statusText);
            }
        });
    } else {
        alert('Maximum 1 attachment are allowed!!');
    }
}

function saveDocumentObject() {
   
     
        // $('.wmsDocumentRow').remove();
        var apiPath = wmsApiPath + "Savedocument";

        var getDocumentName = $('#txtCommonDocument_Title').val();
        var getDescription = $('#txtCommonDocument_Description').val();
        var getDocumentType = $('#txtCommonDocument_Type').val();
        var getFileType = $('#txtCommonDocument_FileType').val();
        var getFileAttachmentPath = $('#txtCommonDocument_Name').html().trim();
        var postData = {
            "ObjectName": documentObject,          
            "UserId": getUserId,
            "ReferenceID": documentOrderId,
            "DocumentName": getDocumentName,
            "Description": getDescription,
            "DocumentType": getDocumentType,
            "FileType": getFileType,
            "DocumentDownloadPath": getFileAttachmentPath,
            "DocumentSavePath": getFileAttachmentPath
        }
            ;
        callHttpUrl(apiPath, postData, function (data) {
            var getStatus = data.Status;
            var getStatusCode = data.StatusCode;
            if (getStatus.toLocaleLowerCase() == 'success')
            {
                var getMessage = data.Result.Message;
                alert("Document Saved Successfully");
                getDocumentListByObject();
                globalFileName = [];
                globalFilePath = [];
                cleartextboxes();
            }
                else {
                    alert("Something Went Wrong !!");
                }
        });
}

function getDocumentFileExtension(getPath) {
 
    fileName = document.querySelector('#wms-srv-cmDocument-file-upload').value;
    extension = fileName.split('.').pop();
    $('#txtCommonDocument_FileType').val(getPath);
    $("#txtCommonDocument_FileType").val(getPath);
  //  $('#txtCommonDocument_Name').val(fileName);
}

function removeDocumentObject(getDocumentId,RetnOrderID) {
  
    var apiPath = wmsApiPath + "RemoveDocument";
    var postData = {   
        "UserId": getUserId,
        "DocumentId": getDocumentId,
        "ReturnOrderId": RetnOrderID
    }
    callHttpUrl(apiPath, postData, function (data) {

        var getStatus = data.Status;
        var getStatusCode = data.StatusCode;
        if (getStatus.toLocaleLowerCase() == 'success') {     

            $('.wmsDocumentRow').remove();
                alert("Document Removed Successfully");
                cleartextboxes();       
                getDocumentListByObject();
            
        } else {
            alert("Failed To Remove Error");
        }
    });
}

function cleartextboxes() {
    $("#txtCommonDocument_Title").val('');
    $("#txtCommonDocument_Description").val('');
    $("#txtCommonDocument_Type").val('');
    $("#txtCommonDocument_FileType").val('');
    $("#wms-srv-cmDocument-file-upload").val(''); 
}

// Document Code End
var TotalRetrnqty = [];
//Order Save Code
function saveReturnOrerSkuDetails(rowId) {
    debugger;
    //if (validateSalesOrderForm(rowId, 'no')) {

    var getRequestNo = $('#setAddReturnOrderId').val();
    
    if (getRequestNo.trim() != 'To be generated' && getRequestNo != '') {

        if (validateAddNewSKURerntOrder(rowId, 'no')) {
            var apiPath = wmsApiPath + 'SaveReturnDetail';
            var orderRefNO = $("#txtAddOMSOrderRefNo").val();
            var getSkuId = $('#txtSo_SKUCode_' + rowId).attr('data-id');
            var getSkuName = $('#txtSo_SKUName_' + rowId).text();
            var getSkuCode = $('#txtSo_SKUCode_' + rowId).text();
            var getUOMId = $('#txtSo_UOM_' + rowId).attr('data-id');
            var getUOM = $('#txtSo_UOM_' + rowId).text();
            var getOrderQty = $('#txtSo_RequestedQty_' + rowId).val();
            //var getActualRetnQty = $('#txtSo_SKUName_' + rowId).text();
            var getReturnQty = $('#txtSo_ReturnQty_' + rowId).val();

            var postData = {
                "returnheadid": getRequestNo,
                "skuid": getSkuId,
                "skuname": getSkuName,
                "skucode": getSkuCode,
                "UOM": getUOMId,
                "orderqty": getOrderQty,
                "returnqty": getReturnQty
            };
            callHttpUrl(apiPath, postData, function (data) {
                //TotalRetrnqty.push(getReturnQty);

                var getStatus = data.Status;
                var getStatusCode = data.StatusCode;
                var AdditionOfSku = 0;

                var getResult = data.Result;
                if (getStatusCode == 200) {
                    alert("SKU details added successfully!!");
                    removeAddSKURow(rowId);
                    getSkuDetailsByReturnOrder();
                    var getskuCount = $("#setTotalRetrnQtylbl").text();
                    var TotalCount = parseInt(getskuCount) + parseInt(getReturnQty);
                    $("#setTotalRetrnQtylbl").html(TotalCount);
                    //    var noOfQty = TotalRetrnqty.map(Number);
                    //for (var i = 0; i < noOfQty.length; i++) {
                    //    AdditionOfSku += noOfQty[i];                       
                    //}
                    //$("#setTotalRetrnQtylbl").html(AdditionOfSku);

                }
                else {
                    alert("unable to connect server");
                }
            });
        }
        }
        else {
            saveReturnOrderHead(rowId, 'no');
        }

}


function validateAddNewSKURerntOrder(rowId, getObj) {

    var checkTitle = $("#txtAddTitle").val();
    var checkAddReturnDate = $("#txtAddReturnDate").val();
    var checkAddExpDateDate = $("#txtAddExpDateDate").val();
    var checkCustomerList = $("#ddlCustomerList").val();
    var checkDeptList = $("#ddlDepartmentList").val();
    var checkDeptWiseUserList = $("#ddlDeptWiseUserList").val();
    var checkHdnLocationId = $("#hdnAddLocationId").val();
    var checkLocationText = $("#txtAddLocationCode").val();
    var checkReturnCategory = $("#ddlReturnCategory").val();
    var checkReturnCondition = $("#ddlReturnCondition").val();
    var checkPriority = $("#txtAddPriority").val();

    var getOrderQty = $('#txtSo_RequestedQty_' + rowId).val();
    var getReturnQty = $('#txtSo_ReturnQty_' + rowId).val();


    if (getOrderQty == '') {
        alert('Please enter Order Qty !!');
        isValidForm = false;
    }
    else if (getReturnQty == '') {
        alert('Please enter Return Qty !!');
        isValidForm = false;
    } else if (getReturnQty == 0) {
        alert('Please enter Return quantity it should not be 0 !!');
        isValidForm = false;
    } else if (getOrderQty != 0) {
        if (Number.parseInt(getOrderQty) == Number.parseInt(getReturnQty)) {
            isValidForm = true;
            return true;
        } else
            //if (getOrderQty <= getReturnQty) {
            if (getOrderQty < getReturnQty) {
                alert('Please enter Return quantity can not be 0 or greater than Order Qty !!');
                isValidForm = false;
            } else {
                isValidForm = true;
                return true;
            }
        //isValidForm = true;
    }
    else {
        return true;
    }

}

function removeAddSKURow(rowId) {
      $('#wmsSkuDetailsSalesOrderGrid .wmsFrmAddRow_' + rowId).remove();
}

function saveReturnOrderHead(rowId, isFinalSave) {

    if (validateAddNewRerntOrder(rowId, 'no')) {
        var apiPath = wmsApiPath + "SaveReturnOrder";

        var getCompanyID = $("#ddlCustomerList").val();
        var getStoreId = $("#ddlDepartmentList").val();
        var getTitle = $("#txtAddTitle").val();
        var getOMSRefNo = $("#txtAddOMSOrderRefNo").val();

        var getReturnDate = $("#txtAddReturnDate").val();
        var getExpDateColn = $("#txtAddExpDateDate").val();
        var getLocationId = $("#hdnAddLocationId").val();
        var getPriority = $("#txtAddPriority").val();
        var getCondition = $("#ddlReturnCondition").val();
        var getReturnCategory = $("#ddlReturnCategory").val();
        var getRemark = $("#txtAddRemark").val();
        var StatusId = $("#txtAddStatusId").val();
        //var gettSoId = "0";
        //var getRequestNo = $('#txtSo_ReqNo').text();
        //if (getRequestNo.trim() != 'To be generated') {
        //    gettSoId = getRequestNo;
        //}

        var postData = {
            "Companyid": getCompanyID,
            "storeid": getStoreId,
            "returnby": getUserId,
            "Title": getTitle,
            "referenceNo": getOMSRefNo,
            "statusid": StatusId,
            "returndate": getReturnDate,
            "expdcollection": getExpDateColn,
            "locationid": getLocationId,
            "Priority": getPriority,
            "condition": getCondition,
            "returncategory": getReturnCategory,
            "Remark": getRemark
        }
        callHttpUrl(apiPath, postData, function (data) {
       
            var isStatus = data.StatusCode;
            var getResult = data.Result;
            if (isStatus == 200) {
                if (isFinalSave != 'yes') {
                    var checkResult = getResult.split("|");
                    var Result = checkResult[0];
                    var OrderId = checkResult[1];

                    $("#setAddReturnOrderId").val('To be generated');
                    $('#setAddReturnOrderId').val(OrderId);
                    saveReturnOrerSkuDetails(rowId);
                }
                else {
                    alert('Order saved successfully!!');
                    //closeCreateSalesOrderForm(true);
                }
            } 
            else if (isStatus == 300) {
               
                alert("Already added an order using this reference no");
            } 
            else {
                alert(getMessage);
            }
        });
    }
}

function getSkuDetailsByReturnOrder() {

    var apiPath = wmsApiPath + "Getreturnorderhead";
    var returnId = $("#setAddReturnOrderId").val();
    var postData = {
        "returnid": returnId,
        "paramval": "Detail"
    };
    callHttpUrl(apiPath, postData, function (data) {

        var isStatus = data.StatusCode;
        var getResult = data.Result;

        if (isStatus == 200 || isStatus == 'Success') {
            $('#wmsSkuDetailsSalesOrderGrid .wmSkuDetailsSalesOrderRow').remove();
            var getOrderList = getResult.Table;

            for (var i = 0; i < getOrderList.length; i++) {
                var getSkuRowId = getOrderList[i].id;
                var getSkuId = getOrderList[i].skuid;
                var getItemCode = getOrderList[i].skucode;
                var getItemName = getOrderList[i].skuname;
                var getDescription = getOrderList[i].description;
                var getUOM = getOrderList[i].UOM;
                var getOrderqty = getOrderList[i].orderqty;
                var getReturnqty = getOrderList[i].returnqty;
                var getActualreturnqty = getOrderList[i].actualreturnqty;

                var skuRow = '<div class="wms-srv-grid-row wmSkuDetailsSalesOrderRow">';
                skuRow = skuRow + '<div class="wms-srv-grid-cell">' + getItemName + '</div>';
                skuRow = skuRow + '<div class="wms-srv-grid-cell">' + getItemCode + '</div>';
                skuRow = skuRow + '<div class="wms-srv-grid-cell">' + getUOM + '</div>';
                skuRow = skuRow + '<div class="wms-srv-grid-cell">' + getOrderqty + '</div>';
                skuRow = skuRow + '<div class="wms-srv-grid-cell">' + getActualreturnqty + '</div>';
                skuRow = skuRow + '<div class="wms-srv-grid-cell">' + getReturnqty + '</div>';           
                
                skuRow = skuRow + '<div class="wms-srv-grid-cell">' +
                    '<div class="wms-srv-grid-action">' +
                    '<a href="#" title="Cart" data-prefix="RM" class="wms-srv-icononly" onclick="removeSkuDetailsBySalesOrder(\'' + getSkuRowId + '\');"><i class="fas fa-times-circle"></i></a>' +
                    '</div>' +
                    '</div>';
                skuRow = skuRow + '</div>';

                $('#wmsSkuDetailsSalesOrderGrid').append(skuRow);

            }

        }

    });
    
}

function removeSkuDetailsBySalesOrder(SkuId) {
    var apiPath = wmsApiPath + "RemoveSKUDetailsByID";
    var getRetnId = $("#setAddReturnOrderId").val();
    var postData = {
        "ReturnID": getRetnId,
        "DetailID": SkuId
    };
    callHttpUrl(apiPath, postData, function (data) {

        var getStatus = data.Status;
        var getStatusCode = data.StatusCode;
        if (getStatus.toLocaleLowerCase() == 'success') {
            getSkuDetailsByReturnOrder();
        } else {
            var getMessage = data.Result.Message;
            alert('Failed To Remove Record..!!');
        }
    });


}

function finalSaveReturnOrder() {
    debugger;
    var apiPath = wmsApiPath + "SaveStaggingdetails";
    var returnId = $("#setAddReturnOrderId").val();
    if (returnId =='') {
        alert("Please Fill All Feilds..!!");
    }
    else if (returnId =='To be generated')
    {
        alert("Please Fill All Feilds..!!");
    }
    else {

    var postData = {
        "ReturnID": returnId
    };
    callHttpUrl(apiPath, postData, function (data) {

        var isStatus = data.Status;
        var getResult = data.Result;

        if (isStatus == 'Success') {
            alert("You have Successfully Save the Data..!!");
            $("#setTotalRetrnQtylbl").html('');
            $("#wms-srv-addtransfer-popup").hide();
            clearAddNewOrderPopup();
            returnOrderList();
        }

    });
    }
}

function validateAddNewRerntOrder(rowId,getObj) {

    var checkTitle = $("#txtAddTitle").val();
    var checkAddReturnDate = $("#txtAddReturnDate").val();
    var checkAddExpDateDate = $("#txtAddExpDateDate").val();
    var checkCustomerList = $("#ddlCustomerList").val();
    var checkDeptList = $("#ddlDepartmentList").val();
    var checkDeptWiseUserList = $("#ddlDeptWiseUserList").val();
    var checkHdnLocationId = $("#hdnAddLocationId").val();
    var checkLocationText = $("#txtAddLocationCode").val();
    var checkReturnCategory = $("#ddlReturnCategory").val();
    var checkReturnCondition = $("#ddlReturnCondition").val();
    var checkPriority = $("#txtAddPriority").val();
                        
    var getOrderQty = $('#txtSo_RequestedQty_' + rowId).val();
    var getReturnQty = $('#txtSo_ReturnQty_' + rowId).val();
                         

    if (checkTitle == '') {
        alert("Please Enter Title");
        return false;
    }
    else if (checkPriority == '' && checkPriority == '0')
    {
        alert("Please Select Priority");
        return false;
    }
    else if (checkPriority == '0') {
        alert("Please Select Priority");
        return false;
    }
    else if (checkAddReturnDate == '') {
        alert("Please Select Return Date");
        return false;
    }
    else if (checkAddExpDateDate == '') {
        alert("Please Select Exp.Date of Collection");
        return false;
    }
    else if (checkCustomerList == '' && checkCustomerList == 0) {
        alert("Please Select Customer");
        return false;
    }
    else if (checkCustomerList == 0) {
        alert("Please Select Customer");
        return false;
    }
    else if (checkDeptWiseUserList == '') {
        alert("Please Select Department");
        return false;
    }
    else if (checkDeptList == '')
    {
        alert("Please Select Department");
        return false;
    }
    else if (checkDeptList == 0) {
        alert("Please Select Department");
        return false;
    }

    else if (checkHdnLocationId == '')
    {
        alert("Please Select Valid Location");
        return false;
    }
    else if (checkLocationText == '') {
        alert("Please Select Valid Location");
        return false;
    }
    else if (checkReturnCategory == '') {
        alert("Please Select Return Category");
        return false;
    }
    else if (checkReturnCondition == '') {
        alert("Please Select Return Condition");
        return false;
    }
    else if (getOrderQty == '') {
        alert('Please enter Order Qty !!');
        isValidForm = false;
    }
    else if (getReturnQty == '') {
        alert('Please enter Return Qty !!');
        isValidForm = false;
        //} else if (getReturnQty <= 0) {
        //    alert('Please enter Return quantity greater than 0 !!');
        //    isValidForm = false;
        //}
    }
    //} else if (0 == getReturnQty) {
    //    alert('Please enter Return quantity can not be 0 or greater than Order Qty !!');
    //    isValidForm = false;
    //} else if (getOrderQty < getReturnQty) {
    //    alert('Please enter Return quantity can not be 0 or greater than Order Qty !!');
    //    isValidForm = false;
    //}

    else if (getReturnQty == 0) {
        alert('Please enter Return quantity it should not be 0 !!');
        isValidForm = false;
    }  else  if (getOrderQty != 0) {
        if (Number.parseInt(getOrderQty) == Number.parseInt(getReturnQty)) {
            isValidForm = true;
            return true;
        } else
        if (getOrderQty < getReturnQty) {
            alert('Please enter Return quantity can not be 0 or greater than Order Qty !!');
            isValidForm = false;
        } else {
            isValidForm = true;
            return true;
        }
        //isValidForm = true;
    }
    else {
        return true;
    }

}

function clearAddNewOrderPopup()
{
       
    $("#txtAddTitle").val('');
    $("#txtAddPriority").val(0);
    $("#setAddReturnOrderId").val('To be generated');
    //$("#txtAddReturnDate").val('');
    //$("#txtAddExpDateDate").val('');
    $("#ddlCustomerList").val('');
    $("#ddlDepartmentList").val('');
    $("#txtAddOMSOrderRefNo").val('');
    $("#txtAddLocationCode").val('');
    $("#txtAddLocationName").val('');   //Added by suraj khopade
    $("#hdnAddLocationId").val('');
    //$("#ddlDeptWiseUserList").val('');
    $("#ddlReturnCategory").val('');
    $("#ddlReturnCondition").val('');
    $("#txtAddRemark").val('');
    $('#wmsSkuDetailsSalesOrderGrid .wmSkuDetailsSalesOrderRow').html('');
    $('#wmsSkuDetailsSalesOrderGrid .wms-srv-grid-row').html('');
    $('#wms-srv-addtransfer-popup .wms-srv-datepicker').datepicker('destroy');

}

function clearRefOrderno() {
    $("#txtAddTitle").val('');
    $("#txtAddPriority").val(0);
    $("#wmsSkuDetailsSalesOrderGrid .wms-srv-grid-row").hide();
}

function getRefOrderData() {
    clearRefOrderno();  
    debugger;
    var apiPath = wmsApiPath + "GetReferenceOrderDetails";
    var refNo = $("#txtAddOMSOrderRefNo").val();
    var deptId = $("#ddlDepartmentList").val();
    var postData = {
        "ReferenceNo": refNo,
        "storeid": deptId
    };
    callHttpUrl(apiPath, postData, function (data) {

        var isStatus = data.Status;
        var getResult = data.Result;
        var getStatusCode = data.StatusCode;
        var getMessage = getResult.Table[0].ResMessage;
        const myArray = "";
        getMessage = getMessage.split(",");
            //getMessage[0]; 
            //getMessage[1];
        var getMessage1 = '';
        var getTitle = '';

        getMessage1 = getMessage[0]; 
        getTitle = getMessage[1]; 
        
        //split getMessage

        if (isStatus == '200' && getStatusCode == 'Success') {

            if (getMessage1 == 'Refnonotavailable') {
                alert("Entered Ref. no. not avilable in the OMS system..");
                fetchClearOrderPopup();
            }
            else if (getMessage1 == 'statusnotcorrect'){
               // alert("Order status not for return..");
                

                if (getTitle == 'E-Commerce Order') {
                    alert('Please add OMS order reference number whose Status is Cancelled,Out for Delivery,Delivery to Hub. and Order Type is not SIM Only and Delivery Type is not HUB and it should not have 110056 and 110057 SIM SKU.', 'Error', '#');
                } else {
                    alert('Please add OMS order reference number whose Status is Cancelled,Out for Delivery,Dispatch,Return,Delivered.', 'Error', '#');
                }
                fetchClearOrderPopup();
            } 
            else {

                $("#btnshwoHideSku").hide();
                var getOrderId = getResult.Table[0].OrderID;
                var getReferenceID = getResult.Table[0].ReferenceID;
                var getTitle = getResult.Table[0].Title;
                var getStatus = getResult.Table[0].Status;
                var getStatusID = getResult.Table[0].StatusID;
                var getreturndate = getResult.Table[0].returndate;
                var getExpDtCollection = getResult.Table[0].ExpDtCollection;
                var getstoreid = getResult.Table[0].storeid;
                var getstorename = getResult.Table[0].storename;
                var getcustomerid = getResult.Table[0].customerid;
                var getcustomername = getResult.Table[0].customername;
                var getLocationName = getResult.Table[0].LocationName;
                var getLocationCode = getResult.Table[0].LocationCode; //added by suraj khopade
                var getLocationID = getResult.Table[0].LocationID;
                var getAddress = getResult.Table[0].Address;
                var getContactName = getResult.Table[0].ContactName;
                setTodayDate();
                $("#txtAddPriority").val(1);
                $("#txtAddTitle").val(getTitle);
                //$("#txtAddReturnDate").val(getreturndate);
                //$("#txtAddExpDateDate").val(getExpDtCollection);
                $("#ddlCustomerList").val(getcustomerid);
                $("#ddlDepartmentList").val(getstoreid);
               // $("#txtAddLocationCode").val(getLocationName); //tmp cmd by suraj khopade
                $("#txtAddLocationCode").val(getLocationCode);
                $("#txtAddLocationName").val(getLocationName);

                $("#hdnAddLocationId").val(getLocationID);
            
                for (var i = 0; i < getResult.Table1.length; i++) {
                    var fetachSkuId = getResult.Table1[i].SKUID;
                    var fetachSKUCode = getResult.Table1[i].SKUCode;
                    var fetachSKUName = getResult.Table1[i].SKUName;
                    var fetachSKUDescription = getResult.Table1[i].SKUDescription;
                    var fetachUOMID = getResult.Table1[i].UOMID;
                    var fetachUOM = getResult.Table1[i].UOM;
                    var fetachOrderQty = getResult.Table1[i].OrderQty;

                    var addNewRowHtml = '<div class="wms-srv-grid-row wmsFrmAddRow_' + addNewRowCounter + '" >' +
                        //'<div class="wms-srv-grid-cell"></div>' +
                        '<div class="wms-srv-grid-cell" id="txtSo_SKUCode_' + addNewRowCounter + '" ></div>' +
                        '<div class="wms-srv-grid-cell" id="txtSo_SKUName_' + addNewRowCounter + '"></div>' +
                        '<div class="wms-srv-grid-cell" id="txtSo_UOM_' + addNewRowCounter + '"></div>' +
                        '<div class="wms-srv-grid-cell"><input type="text" id="txtSo_RequestedQty_' + addNewRowCounter + '" value="' + fetachOrderQty + '" class="notranslate"></div>' +
                        '<div class="wms-srv-grid-cell">0</div>' +
                        '<div class="wms-srv-grid-cell"><input type="text" id="txtSo_ReturnQty_' + addNewRowCounter + '" value="" class="notranslate"></div>' +
                        '<div class="wms-srv-grid-cell">' +
                        '<div class="wms-srv-grid-action">' +
                        '<button class="wms-srv-input wms-srv-button " type="button" title="Save" data-prefix="SV" onclick="saveReturnOrerSkuDetails(' + addNewRowCounter + ');"><i class="fas fa-check-circle"></i><span>Save</span></button>' +
                        '<div class="wms-srv-action-sep" style="margin-right: 3px;margin-left: 3px;">|</div>' +
                        '<button class="wms-srv-input wms-srv-button " type="button" title="Save" data-prefix="SV" onclick="removeAddNewRow(' + addNewRowCounter + ');"><i class="fas fa-times-circle"></i><span>Cancel</span></button>' +
                        '</div>' +
                        '</div>' +
                        '</div>'
                    $('#wmsSkuDetailsSalesOrderGrid .wms-srv-grid-header').after(addNewRowHtml);

                    $('#txtSo_SKUCode_' + addNewRowCounter).html(fetachSKUCode);
                    $('#txtSo_SKUCode_' + addNewRowCounter).attr('data-id', fetachSkuId);

                    $('#txtSo_SKUName_' + addNewRowCounter).html(fetachSKUName);
                    $('#txtSo_UOM_' + addNewRowCounter).html(fetachUOM);
                    $('#txtSo_UOM_' + addNewRowCounter).attr('data-id', fetachUOMID);
                    addNewRowCounter = addNewRowCounter + 1;
                }
            }
        }
    });
}

function fetchClearOrderPopup() {

    $("#txtAddTitle").val('');
    $("#txtAddPriority").val(0);
    $("#setAddReturnOrderId").val('To be generated');
    //$("#txtAddReturnDate").val('');
    //$("#txtAddExpDateDate").val('');   
    $("#txtAddOMSOrderRefNo").val('');
    $("#txtAddLocationCode").val('');
    $("#hdnAddLocationId").val('');
    $("#ddlReturnCategory").val('');
    $("#ddlReturnCondition").val('');
    $("#txtAddRemark").val('');
    $('#wmsSkuDetailsSalesOrderGrid .wmSkuDetailsSalesOrderRow').html('');
    $('#wmsSkuDetailsSalesOrderGrid .wms-srv-grid-row').html('');

}

function AssginDriver() {

    $(".pnlWmsHead").hide();
    var radioOption = document.getElementsByClassName("messageCheckbox");
    var cnt = $('.messageCheckbox:checked').length;


    if (cnt > 0) {

        var myData = '';
        $('.messageCheckbox').each(function () {
            //  var isChecked = $(this).find('.messageCheckbox').prop('checked');
            var isChecked = $(this).prop('checked');
            if (isChecked) {
                //sequenceNo = sequenceNo + 1;
                var orderid = $(this).attr('data-id');
                var columnName = $(this).find('.messageCheckbox').parent().find('span').text().trim();
                if (myData.trim() == '') {
                    myData = orderid;
                }
                else {
                    myData = myData + ',' + orderid;
                }
                

            }
        });
        gloablRetrnId = myData;
      //  alert(myData);

        $("#wms-srv-AssignDriver-popup").show();
        ViewOrder();
    }
    else {
        alert("Please Select Record..!!");
    } 
    //if (radioOption.checked)
    //{

    //    $("#wms-srv-AssignDriver-popup").show();
    //    ViewOrder();
    //}
    //else {
    //    alert("Please Select Record..!!");
    //}
}

function saveOrderId(getRetnId) {
  
    var radioOption = document.getElementsByClassName("messageCheckbox");
        
    //if (radioOption.checked) {
    //    radioOption.checked = false; 
    //    $("#gridReturnOrderList .messageCheckbox").prop('checked', false);
        
    //} else {
    //    radioOption.checked = true; 
    //   // var myval = "";
    //   // myval = getImportDesignerFromRow();
    //   // alert(myval);
    //    gloablRetrnId = getRetnId;//com suraj khopade
    //}

    if (radioOption.checked) {
        radioOption.checked = false;
        $("#gridReturnOrderList .messageCheckbox").prop('checked', false);

    } else {
        radioOption.checked = true;
       // var myval = "";
       // myval = getImportDesignerFromRow();
       // alert(myval);
        // gloablRetrnId = getRetnId;//com suraj khopade
    }

   
}

function getImportDesignerFromRow() {
     var myData = '';
    var sequenceNo = -1;
    $('.gridReturnOrderList').each(function () {
    var isChecked = $(this).find('.messageCheckbox').prop('checked');
        if (isChecked) {
            //sequenceNo = sequenceNo + 1;
            var columnName = $(this).find('.messageCheckbox').parent().find('span').text().trim();
            myData = myData + ',' + columnName + ',';
            //var columnDataType = $(this).find('.dataType').val();
            //var isNull = $(this).find('.isNull').prop('checked');
           // var dataLength = $(this).find('.length').val();
            //if (myData.trim() == '') {
            //    myData = ':' + columnName + ':' + sequenceNo + ':' + columnDataType + ':' + isNull + ':';
            //} else {
            //    myData = myData + '|:' + columnName + ':' + sequenceNo + ':' + columnDataType + ':' + isNull + ':';
            //}

        }
   });
    //alert(myData);
    return myData
}

function cancelOrder() {
   

    var UserTypeC = $("#hdnUserType").val();

    if (UserTypeC == 'Super Admin') {
        var radioOption = document.getElementsByClassName("messageCheckbox");

        if (radioOption.checked) {
            var apiPath = wmsApiPath + 'CancelOrder';
            var postData = {
                "ReturnId": gloablRetrnId,
                "UserId": getUserId
            }
            callHttpUrl(apiPath, postData, function (data) {
                var getStatusCode = data.Result;
                var isStatus = data.StatusCode;
                if (isStatus == 200 && getStatusCode == 'success') {
                    alert("Order Cancel Successfully");
                    returnOrderList();
                }
                else if (getStatusCode == "NotCancelOrder") {
                    alert("You Can't cancel this order..!!");
                }
                else {
                    alert("Unable To connect With Server..!!");
                }
            })
        }
        else {
            alert("Please Select Record..!!");
        }

    } else {
        alert("Do not have access to cancel the order.");
    }

    
}

function DriverList() {

    $("#wms-srv-DriverGridList-popup").show();
    var apiPath = wmsApiPath + 'getUserList';
    var searchVal = ''
    searchVal = $("#txtDriverListSrch").val();
    if (searchVal == "") {
        searchVal = "";
    }
    var postData = {
        "UserId": getUserId,
        "CustomerId": 0,
        "SearchVal": searchVal
    }
    callHttpUrl(apiPath, postData, function (data) {
        var isStatus = data.Status;
        if (isStatus == 200) {

            $("#wms-srv-grid-AssignDriverList").html('');
            var gridlist = '';
            gridlist = gridlist + '<div class="wms-srv-grid-header" style="white-space: nowrap;">';
            gridlist = gridlist + '<div class="wms-srv-grid-cell">Driver Name</div>';
            gridlist = gridlist + '<div class="wms-srv-grid-cell">Contact No</div>';
            gridlist = gridlist + '<div class="wms-srv-grid-cell">Email Id</div>';
            gridlist = gridlist + '<div class="wms-srv-grid-cell" style="text-align:center;width:50px;">Action </div>';
            gridlist = gridlist + '</div>';

            var myGridList = data.Result;
            var DataList = myGridList.Table;
            for (var i = 0; i < DataList.length; i++) {
                var getUserId = DataList[i].UserID;
                var getFirstName = DataList[i].FirstName;
                var getLastName = DataList[i].LastName;
                var getEmployeeID = DataList[i].EmployeeID;
                var getUserType = DataList[i].UserType;
                var getUserEmailID = DataList[i].EmailID;
                var getContactNo = DataList[i].ContactNo;
                var driverName = getFirstName + ' ' + getLastName;
                gridlist = gridlist + '<div class="wms-srv-grid-row">';
                gridlist = gridlist + '<div class="wms-srv-grid-cell">' + driverName + '</div>';
                gridlist = gridlist + '<div class="wms-srv-grid-cell">' + getContactNo + '</div>';
                gridlist = gridlist + '<div class="wms-srv-grid-cell">' + getUserEmailID + '</div>';
                gridlist = gridlist + '<div class="wms-srv-grid-cell">';
                gridlist = gridlist + '<div class="wms-srv-grid-action">';
                gridlist = gridlist + '<a href="#" title="Save" class="wms-srv-save" onclick="saveDriverId(\'' + getUserId + '\',\'' + getFirstName + '\')"><i class="fas fa-check-circle iconView"></i></a>';
                //gridlist = gridlist + '<div class="wms-srv-action-sep">|</div>';
                //gridlist = gridlist + '<a href="#" title="Save" class="wms-srv-save" onclick=";"><i class="fas fa-times-circle iconView"></i></a>';
                gridlist = gridlist + '</div>';
                gridlist = gridlist + '</div>';
                gridlist = gridlist + '</div>';
            }
            $("#wms-srv-grid-AssignDriverList").append(gridlist);
            $("#wms-srv-DriverGridList-popup-close").click(function () {
                $("#wms-srv-DriverGridList-popup").off();
                $("#txtDriverListSrch").val('');
                $("#wms-srv-DriverGridList-popup").hide();
            });
        }
    });
}

function saveDriverId(getDriverId,driverName) {
    $("#hdnDriverId").val(getDriverId);
    $("#txtDriverList").val(driverName);
    $("#wms-srv-DriverGridList-popup").off();
    $("#txtDriverListSrch").val('');
    $("#wms-srv-DriverGridList-popup").hide();
}

function AssignDriver() {
    if (validateDriverAssign()) {


    var apiPath = wmsApiPath + 'AssignDriver';
    var DriverId = $("#hdnDriverId").val();
    getUserId = $('#hdnUserID').val();
    var TruckId = $("#txtDriverTruckNo").val();
        if (TruckId =="") {
            TruckId = "";
        }
    var postData = {
        "DriverId": DriverId,
        "returnId": gloablRetrnId,
        "TruckNo": TruckId,
        "UserId": getUserId
    };
    callHttpUrl(apiPath, postData, function (data) {
        var getStatusCode = data.Result;
        var isStatus = data.StatusCode;
        if (isStatus == 200 && getStatusCode == 'success') {
            alert("Driver assigned successfully");
            ViewOrder();
            clearAssignDriverPopup();
        }
        else if (isStatus == 300 && getStatusCode == 'failed')
        {
            alert("Driver already assigned to this order");
            clearAssignDriverPopup();
        } else if (isStatus == 300 && getStatusCode == 'OrderCancel') {
            alert("Can not assign driver as order status is cancelled.");
            clearAssignDriverPopup();
        } else if (isStatus == 300 && getStatusCode == 'asnnumber') {
          //  alert("This Order is ASN Number not updated, You can't Assign Driver");
            alert("Can not assign driver as ASN number not added to order.");
            clearAssignDriverPopup();
        }
        else {
            alert("Failed to assign driver..!!");
        }
    })
    }
}

function clearAssignDriverPopup() {
    $("#hdnDriverId").val('');
    $("#txtDriverTruckNo").val('');
    $("#txtDriverList").val('');
    $("#gridReturnOrderList .messageCheckbox").prop('checked', false);
}

function validateDriverAssign() {
    var DriverId = $("#hdnDriverId").val();
    var TruckId = $("#txtDriverTruckNo").val();
    var DriverName = $("#txtDriverList").val();
    if (DriverName == "") {
        alert("Please Select Valid Driver");
        return false;
    }
    else if (DriverId == "") {
        alert("Please Select Valid Driver");
        return false;
    }
    else {
        return true;
    }


}

function ViewOrder() {
    var apiPath = wmsApiPath + 'viewAssignDriver';
   
    var postData = {
        "ReturnId": gloablRetrnId
    };
    callHttpUrl(apiPath, postData, function (data) {
        var isStatus = data.Status;
        var viewRecordlength = data.Result.Table.length;
        if (isStatus == 200 && viewRecordlength > 0) {
            $(".pnlWmsHead").show();
            $("#wms-srv-grid-DriverListPopup").html('');
          
            var gridlist = '';
            gridlist = gridlist + '<div class="wms-srv-grid-header" style="white-space: nowrap;">';
            gridlist = gridlist + '<div class="wms-srv-grid-cell">Driver Name</div>';
            gridlist = gridlist + '<div class="wms-srv-grid-cell">Truck No</div>';
            gridlist = gridlist + '<div class="wms-srv-grid-cell">Email Id</div>';
            //gridlist = gridlist + '<div class="wms-srv-grid-cell" style="text-align:center;width:50px;">Action </div>';
            gridlist = gridlist + '</div>';
            var myGridListData = data.Result.Table;
            for (var i = 0; i < myGridListData.length; i++)
            {
                var getDriverId = myGridListData[i].DriverId;
                var getFirstName = myGridListData[i].FirstName;
                var getLastName = myGridListData[i].LastName;
                var getEmailid = myGridListData[i].Emailid;
                var getOrderId = myGridListData[i].OrderId;
                var getTruckDetail = myGridListData[i].TruckDetail;

        gridlist = gridlist + '<div class="wms-srv-grid-row">';
        gridlist = gridlist + '<div class="wms-srv-grid-cell">' + getFirstName+'</div>';
        gridlist = gridlist + '<div class="wms-srv-grid-cell">' + getTruckDetail +'</div>';
        gridlist = gridlist + '<div class="wms-srv-grid-cell">' + getEmailid+'</div>';
        //gridlist = gridlist + '<div class="wms-srv-grid-cell">';
        //gridlist = gridlist + '<div class="wms-srv-grid-action">';
        //gridlist = gridlist + '<a href="#" title="Save" class="wms-srv-save" onclick=""><i class="fas fa-check-circle iconView"></i></a>';
        //gridlist = gridlist + '<div class="wms-srv-action-sep">|</div>';
        //gridlist = gridlist + '<a href="#" title="Save" class="wms-srv-save" onclick=";"><i class="fas fa-times-circle iconView"></i></a>';
        //gridlist = gridlist + '</div>';
        //gridlist = gridlist + '</div>';
        gridlist = gridlist + '</div>';
            }
            $("#wms-srv-grid-DriverListPopup").append(gridlist);

            
        }
    });
}

function setupGridPagingList(gridObjId, strCurrentPage, strTotalRecords, callBackFunction) {
    var global_max_record_count = 10;
    var pageNo = Number(strCurrentPage);
    var recordFrom = ((pageNo - 1) * 10) + 1;
    var recordTo = recordFrom + 9;
    var totalRecord = Number(strTotalRecords);
    var pagerLinks = '';

    if (totalRecord < recordTo) {
        recordTo = totalRecord;
    }

    $('#' + gridObjId + ' .wms-srv-pager-records').html(recordFrom + '-' + recordTo + ' of ' + totalRecord + ' Records');
    var lnkCounter = 1;
    var currentCounter = global_max_record_count;
    var pagingGroup = 1;
    var pagingGroupCounter = 1;
    var isFirstGroupLink = 'yes';
    var lastPage = 0;
    pagerLinks += recordFrom + '-' + recordTo + ' of ' + totalRecord + ' Records <span class="wms-srv-empty-space"></span>';
    pagerLinks += '<b>Go to Page: </b> <select id="ddlGridPageNo" class="wms-srv-input ddlGridPageNo" style="width:100px;">';
    while (currentCounter < totalRecord) {
        if (lnkCounter == pageNo) {
            pagerLinks += '<option selected="selected">' + lnkCounter + '</option>';
        } else {
            pagerLinks += '<option>' + lnkCounter + '</option>';
            lastPage = lnkCounter;
        }
        global_last_page_no = lnkCounter;
        currentCounter = currentCounter + global_max_record_count;
        // Group Counter 
        isFirstGroupLink = 'no';
        pagingGroupCounter = pagingGroupCounter + 1;
        // Group Counter
        lnkCounter = lnkCounter + 1;
    }

    // Add Page link for remaining record 
    if (totalRecord <= currentCounter) {
        pagerLinks += '<option>' + lnkCounter + '</option>';
        lastPage = lnkCounter;
        this.global_last_page_no = lnkCounter;
    }
    pagerLinks += '</select>';
    pagerLinks += '<a href="#" title="Jump To" class="wms-srv-ddlpager-go" style="font-size:16px;"><i class="fas fa-play-circle"></i></a>';

    // Add Page link for remaining record
    var pagerNavLinks = '';
    pagerNavLinks += pagerLinks;

    $('#' + gridObjId + '.wms-srv-grid-pager').html(pagerNavLinks);

    $('#' + gridObjId + ' a.wms-srv-ddlpager-go').off();
    $('#' + gridObjId + ' a.wms-srv-ddlpager-go').click(function () {
        var getDataPage = $('#' + gridObjId + ' .ddlGridPageNo').val();
        CurrentPage = getDataPage;
        if (Number(getDataPage) < 1) {
            alert('Please enter valid page number!!');
        } else if (Number(getDataPage) > Number(lastPage)) {
            alert('Page number should not be greater than ' + lastPage + ' !!');
        } else {
            //showHidePagingGroup(gridObjId);
            if (callBackFunction != null) {
                callBackFunction(getDataPage, searchfilter, searchvalue);
            }
        }
        return false;
    });
    //showHidePagingGroup(gridObjId);
}
