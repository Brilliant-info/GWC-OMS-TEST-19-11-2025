var CurrentPage = 1;
var RecordLimit = 10;
var searchfilter = 'ALL';
var searchvalue = 'none';
var glbdriverID = '';
var getUserId = 10956;
var getCompanyId = 10204;

//var getUserId = $('#hdnUserID').val();
//var getCompanyId = $('#hdnCompanyId').val();

$(document).ready(function () {
    $('#hdnS2SSelectedOrders').val('');
    S2SOrderList();
    $('#button2').click(function () {
        CancelselectOrder();
    });
    $("#txtcountchk").val('0');

});

function showDocumentPopup(refid, getstatus) {
    $("#wms-srv-document-popup").show();
    $("#wms-srv-document-popup-close").click(function () {
        $("#wms-srv-document-popup").off();
        $("#wms-srv-document-popup").hide();
    });
    S2Sdocumentlist(refid, getstatus);
}

function showViewPopup(gets2SOrderNo) {
    debugger;
    $("#wms-srv-ViewOrder-popup").show();
    $("#wms-srv-ViewOrder-popup-close").click(function () {
        $("#wms-srv-ViewOrder-popup").off();
        $("#wms-srv-ViewOrder-popup").hide();
    });
    clearData();
    viewS2Slist(gets2SOrderNo);
}

function showOrderHistoryPopup(gets2SOrderNo) {
    $("#wms-srv-OrderHistory-popup").show();
    $("#wms-srv-OrderHistory-popup-close").click(function () {
        $("#wms-srv-OrderHistory-popup").off();
        $("#wms-srv-OrderHistory-popup").hide();
    });
    S2SOrderhistoryList(gets2SOrderNo);
}

//function showdriverPopup() {
//    var getSelectedCheckBox = $('#gridS2SOrderList .messageCheckbox:checked');

//    if (getSelectedCheckBox.length > 0) {
//        if (isValidS2SSelection()) {
//            var firstCheckbox = getSelectedCheckBox.first();
//            var dataId = firstCheckbox.attr('data-id');
//            var status = firstCheckbox.attr('data-status');

//            $('#wms-srv-AssignDriverre-popup').show();
//            $('#txtDriverTruckNoS2S').val('');
//            $('#txtDriverList').val('');
//            $('#wms-srv-AssignDriverkj-popup-close').off('click').on('click', function () {
//                if (confirm("Selected checkboxes will be cleared. Do you want to close?")) {
//                    $('#gridS2SOrderList .messageCheckbox').prop('checked', false);
//                    $('#wms-srv-AssignDriverre-popup').hide();
//                    clearCheckboxAll();
//                }
               
//            });

//            $("#pnldriverlistAssign").html('');
//            S2SAssignDriverlist(dataId, status);
//        } else {
//            alert('Please select orders with status "Pending for driver allocation".');
//        }
//    } else {
//        alert("Please select at least one order.");
//    }
//}

function showdriverPopup() {
    var getSelectedCheckBox = $('#gridS2SOrderList .messageCheckbox:checked');

    if (getSelectedCheckBox.length > 0) {
        const validationResult = isValidS2SSelection();

        if (validationResult.valid) {
            var firstCheckbox = getSelectedCheckBox.first();
            var dataId = firstCheckbox.attr('data-id');
            var status = firstCheckbox.attr('data-status');

            $('#wms-srv-AssignDriverre-popup').show();
            $('#txtDriverTruckNoS2S').val('');
            $('#txtDriverList').val('');
            $('#wms-srv-AssignDriverkj-popup-close').off('click').on('click', function () {
                if (confirm("Selected checkboxes will be cleared. Do you want to close?")) {
                    $('#gridS2SOrderList .messageCheckbox').prop('checked', false);
                    $('#wms-srv-AssignDriverre-popup').hide();
                    clearCheckboxAll();
                }
            });

            $("#pnldriverlistAssign").html('');
            S2SAssignDriverlist(dataId, status);
        } else {
            alert(validationResult.message); // ✅ Show only one alert
        }
    } else {
        alert("Please select at least one order.");
    }
}



function openparameterPopup() {
    $('#wms-srv-Parameter-popup').show();
    $('#wms-srv-Parameter-popup-close').off();
    $('#wms-srv-Parameter-popup-close').click(function () {
        $('#wms-srv-Parameter-popup').hide();
    });
}
function showdriverlistPopup() {
    $("#wms-srv-list-Driverer-popup").show();
    $("#wms-srv-list-Driversd-popup-close").click(function () {
        $("#wms-srv-list-Driverer-popup").off();
        $("#wms-srv-list-Driverer-popup").hide();
    });

    S2SDriverlist();
    
}

//function clearCheckboxAll()
//{
//    $('#gridS2SOrderList .messageCheckbox').prop('checked', false);
//    $("#txtcountchk").val('0');
//}

function clearCheckboxAll()
{
    // Uncheck all checkboxes currently in DOM (current page)
    $('#gridS2SOrderList .messageCheckbox').prop('checked', false);

    // Clear global selection array and count
    arrSelectedS2SOrders = [];
    selectedS2SOrderCount = 0;

    // Reset any count display
    $("#txtcountchk").val('0');
}


function S2SOrderList() {
    debugger;
    var apiPath = S2SwmsApiPath + "S2SOMSOrder/GetOrderList";
    searchfilter = $("#ddls2sOrderColSearch").val();
    searchvalue = $("#ddls2sOrderColValue").val();
    if (searchfilter == '') {
        searchfilter = '';
        searchvalue = '';
    }

    var postData =
    {
        "currentPage": CurrentPage,
        "recordLimit": RecordLimit,
        "userId": getUserId, 
        "search": searchvalue,
        "filter": searchfilter
    };
    callHttpCoreUrl(apiPath, postData, function (data) {
        var isStatus = data.statuscode;
        var mygridList = data.s2SOrders;
        var TotalRecords = data.totalRecords;
        //var CurrentPage = result.Table[0].CurrentPage;
        //var mygridList = result.Table1;

        if (isStatus == 200) {
            $("#gridS2SOrderList").html('');
            var gridList = '';

            gridList = gridList + '<div class="wms-srv-grid-header" id="header-wrap">';
            gridList = gridList + '<div class="wms-srv-grid-cell"></div>';
            gridList = gridList + '<div class="wms-srv-grid-cell">S2S NO</div>';
            gridList = gridList + '<div class="wms-srv-grid-cell">WinCash Order No</div>';
            gridList = gridList + '<div class="wms-srv-grid-cell">Order Type</div>';
            gridList = gridList + '<div class="wms-srv-grid-cell">Source Store</div>';
            gridList = gridList + '<div class="wms-srv-grid-cell">Destination Store</div>';
            gridList = gridList + '<div class="wms-srv-grid-cell">Performed By</div>';
            gridList = gridList + '<div class="wms-srv-grid-cell">Received By</div>';
            gridList = gridList + '<div class="wms-srv-grid-cell">Status</div>';
            gridList = gridList + '<div class="wms-srv-grid-cell">Action</div>';
            gridList = gridList + '</div>';

            for (var i = 0; i < mygridList.length; i++) {
                var getId = mygridList[i].id;
                var gets2SOrderNo = mygridList[i].s2SOrderNo;
                var getwincashOrderNo = mygridList[i].wincashOrderNo;
                var getordertype = mygridList[i].ordertype;
                var getsourcestore = mygridList[i].sourcestore;
                var getdestinationStore = mygridList[i].destinationStore;
                var getperformedby = mygridList[i].performedby;
                var getreceivedby = mygridList[i].receivedby;
                var getstatus = mygridList[i].status;

                gridList = gridList + '<div class="wms-srv-grid-row">';
                gridList = gridList + '<div class="wms-srv-grid-cell"><input type="checkbox" class="messageCheckbox" data-id="' + getId + '" data-status="' + getstatus + '" ' + selectCheckboxOnListLoad(getId) +' onclick="pushToS2SSelection(this);"></div>';
                gridList = gridList + '<div class="wms-srv-grid-cell" style="text-align:center;">' + gets2SOrderNo + '</div>';
                gridList = gridList + '<div class="wms-srv-grid-cell" style="text-align:center;">' + getwincashOrderNo + '</div>';
                gridList = gridList + '<div class="wms-srv-grid-cell" style="text-align:center;">' + getordertype + '</div>';
                gridList = gridList + '<div class="wms-srv-grid-cell" style="text-align:center;">' + getsourcestore + '</div>';
                gridList = gridList + '<div class="wms-srv-grid-cell" style="text-align:center;">' + getdestinationStore + '</div>';
                gridList = gridList + '<div class="wms-srv-grid-cell" style="text-align:center;">' + getperformedby + '</div>';
                gridList = gridList + '<div class="wms-srv-grid-cell" style="text-align:center;">' + getreceivedby + '</div>';
                gridList = gridList + '<div class="wms-srv-grid-cell" style="text-align:center;">' + getstatus + '</div>';
                gridList = gridList + '<div class="wms-srv-grid-cell">';
                gridList = gridList + '<div class="wms-srv-grid-action">';
                gridList = gridList + '<i class="fas fa-eye iconView" title="View Order" onclick="showViewPopup(\'' + gets2SOrderNo + '\');"></i> ';
                gridList = gridList + '<div class="wms-srv-action-sep">|</div>';
                gridList = gridList + '<i class="fas fa-file-alt iconView"  title="Document" onclick="showDocumentPopup(' + getId + ',\'' + getstatus + '\');"></i>';
                gridList = gridList + '<div class="wms-srv-action-sep">|</div>';
                gridList = gridList + '<a href="#" title="" class="wms-srv-icononly" onclick="showOrderHistoryPopup(\'' + gets2SOrderNo + '\');"><i class="fas fa-tasks iconView" title="Order History"></i></a>';
                gridList = gridList + '</div>';
                gridList = gridList + '</div>';
                gridList = gridList + '</div>';
            }
            $("#gridS2SOrderList").append(gridList);
            setupGridPagingList('tblS2Slistpager', CurrentPage, TotalRecords, S2SOrderList);
        }
        else if (isStatus == 404)
        {
            //alert("Error Occured");
            alert("Order Not Found");
        }
        else
        {
            alert("Error Occured");
        }
    });
}

var divHeight = $('#header-wrap').height();
$('#gridS2SOrderList').css('margin-top', divHeight + 'px');

function searchReturnOrder() {
    var searchCol = $("#ddls2sOrderColSearch").val();
    var searchVal = $("#ddls2sOrderColValue").val();

    if (searchCol != '' && searchVal != '') {
        S2SOrderList();
    }
    else {
        alert("Please Select Proper Search Value..!!");
    }
}
function clearSearchFilterS2S() {
    $("#ddls2sOrderColValue").val('');
    $("#ddls2sOrderColSearch").val(0);
    S2SOrderList();
}
function searchS2SOrder() {
    var searchCol = $("#ddlReturnOrderColSearch").val();
    var searchVal = $("#ddlReturnFilterValue").val();

    if (searchCol != '' && searchVal != '') {
        S2SOrderList();
    }
    else {
        alert("Please Select Proper Search Value..!!");
    }
}

function S2SOrderhistoryList(gets2SOrderNo) {
    debugger;
    var apiPath = S2SwmsApiPath + "S2SOMSOrder/GetOrderHistory";

    var postData =
    {
        "s2SOrderNO": gets2SOrderNo
    };
    callHttpCoreUrl(apiPath, postData, function (data) {
        var isStatus = data.statuscode;
        var mygridList = data.s2SOrderHistories;
        //var TotalRecords = result.Table[0].TotalRecord;
        //var CurrentPage = result.Table[0].CurrentPage;
        //var mygridList = result.Table1;
        $("#srvgridOrderHistory").html('');
        var gridList = '';
        gridList = gridList + '<div class="wms-srv-grid-header">';
        //gridList = gridList + '<div class="wms-srv-grid-cell"></div>';
        gridList = gridList + '<div class="wms-srv-grid-cell">S2S Order No</div>';
        gridList = gridList + '<div class="wms-srv-grid-cell">Status</div>';
        gridList = gridList + '<div class="wms-srv-grid-cell">Date Time</div>';
        gridList = gridList + '<div class="wms-srv-grid-cell">Updated By</div>';
        //gridList = gridList + '<div class="wms-srv-grid-cell">Action</div>';
        gridList = gridList + '</div>';
        
        if (isStatus == 200)
        {
            for (var i = 0; i < mygridList.length; i++)
            {
                var gets2SOrderNo = mygridList[i].s2SOrderNo;
                var getstatus = mygridList[i].status;
                var getdate = mygridList[i].date;
                var getdatetime = getdate.replace("T", " ");
                var getupdatedby = mygridList[i].updatedby;

                gridList = gridList + '<div class="wms-srv-grid-row">';
                gridList = gridList + '<div class="wms-srv-grid-cell" style="text-align:center;">' + gets2SOrderNo + '</div>';
                gridList = gridList + '<div class="wms-srv-grid-cell" style="text-align:center;">' + getstatus + '</div>';
                gridList = gridList + '<div class="wms-srv-grid-cell" style="text-align:center;">' + getdatetime + '</div>';
                gridList = gridList + '<div class="wms-srv-grid-cell" style="text-align:center;">' + getupdatedby + '</div>';
                gridList = gridList + '</div>';
            }
            
        }
        else {
            // alert("Error Occured");
        }
        $("#srvgridOrderHistory").append(gridList);
    });
}

function S2SDriverlist() {
    debugger;
    var apiPath = S2SwmsApiPath + "S2SOMSOrder/GetDriverlist";
    var searchFName = $("#pnldriversearch").val();
    var postData =
    {
        "companyID": getCompanyId,
        "userID": getUserId,
        "searchFName": searchFName
    };
    callHttpCoreUrl(apiPath, postData, function (data) {
        var isStatus = data.statuscode;
        var mygridList = data.s2SOrderDriverlist;
        //var TotalRecords = result.Table[0].TotalRecord;
        //var CurrentPage = result.Table[0].CurrentPage;
        //var mygridList = result.Table1;
        $("#pnldriverlister").html('');

        if (isStatus == 200) {

            var gridList = '';

            gridList = gridList + '<div class="wms-srv-grid-header">';
            //gridList = gridList + '<div class="wms-srv-grid-cell"></div>';
            gridList = gridList + '<div class="wms-srv-grid-cell">Driver Name</div>';
            gridList = gridList + '<div class="wms-srv-grid-cell">Contact No</div>';
            gridList = gridList + '<div class="wms-srv-grid-cell">EmailID</div>';
            //gridList = gridList + '<div class="wms-srv-grid-cell">Updated By</div>';
            gridList = gridList + '<div class="wms-srv-grid-cell">Action</div>';
            gridList = gridList + '</div>';

            for (var i = 0; i < mygridList.length; i++) {
                var getdriverID = mygridList[i].driverID;
                //glbdriverID = getdriverID;
                var getdriverName = mygridList[i].driverName;
                var getcontactNo = mygridList[i].contactNo;
                var getemailID = mygridList[i].emailID;
                //var getupdatedby = mygridList[i].updatedby;

                gridList = gridList + '<div class="wms-srv-grid-row">';
                gridList = gridList + '<div class="wms-srv-grid-cell" style="text-align:center;">' + getdriverName + '</div>';
                gridList = gridList + '<div class="wms-srv-grid-cell" style="text-align:center;">' + getcontactNo + '</div>';
                gridList = gridList + '<div class="wms-srv-grid-cell" style="text-align:center;">' + getemailID + '</div>';
                //gridList = gridList + '<div class="wms-srv-grid-cell" style="text-align:center;">' + getupdatedby + '</div>';
                gridList = gridList + '<div class="wms-srv-grid-cell">';
                gridList = gridList + '<div class="wms-srv-grid-action">';
                gridList = gridList + '<i class="fas fa-check-circle" title="" onclick="binddriverName(' + getdriverID + ',\'' + getdriverName + '\');"></i>';
                gridList = gridList + '</div>';
                gridList = gridList + '</div>';
                gridList = gridList + '</div>';
            }
            $("#pnldriverlister").append(gridList);
        }
        else {
            alert("Driver Not found!!");
        }

    });
}

function binddriverName(getdriverID, getdriverName)
{
    debugger;
    $("#txtDriverList").val(getdriverName);
    //$("#txtDriverID23").val(getdriverID);
    glbdriverID = getdriverID;
    $("#wms-srv-list-Driverer-popup").hide();
}

function viewS2Slist(gets2SOrderNo) {
    debugger;
    var apiPath = S2SwmsApiPath + 'S2SOMSOrder/GetOrderview';
    var postData =
    {
        "omsorderNo": gets2SOrderNo
    };

    callHttpCoreUrl(apiPath, postData, function (data) {
        var getStatus = data.statuscode;
        //var popup=data.Result.Table;
        var getresult = data;
        //var GetGSTlistTb = getresult.Table;
        if (getStatus == 200) {
            var getwincashOrderNo = getresult.wincashOrderNo;
            var gets2SOrderNo = getresult.s2SOrderNo;
            var getsourcestore = getresult.sourcestore;
            var getordertype = getresult.ordertype;

            var getstatus = getresult.status;
            var getdesc = getresult.desc;
            var getdestinationStore = getresult.destinationStore;
            var getperformedby = getresult.performedby;
            var getreceivedby = getresult.receivedby;
            var getcreationDate = getresult.creationDate;


            $('#viewWinCashS2SOrderNo').val(getwincashOrderNo);
            $('#viewomsS2Sorderno').val(gets2SOrderNo);
            $('#viewsourcestoreS2S').val(getsourcestore);
            $('#viewWinCashS2SOrderNo').val(getwincashOrderNo);
            //$('#viewomsS2Sorderno').val(gets2SOrderNo);
            $('#viewsourcestoreS2S').val(getsourcestore);

            $('#views2sorderStatus').val(getstatus);
            //$('#viewomsS2Sorderno').val(getdesc);
            $('#viewDestinationStoreS2S').val(getdestinationStore);
            $('#viewperformedbyS2S').val(getperformedby);
            $('#viewreceivedByS2S').val(getreceivedby);
            $('#viewCreationDateS2S').val(getcreationDate);

            $("#viewS2SOrderGrid").html('');
            var mygridList = data.skulist;
            var gridList = '';

            gridList = gridList + '<div class="wms-srv-grid-header">';
            gridList = gridList + '<div class="wms-srv-grid-cell">Sku</div>';
            gridList = gridList + '<div class="wms-srv-grid-cell">Serial Number</div>';
            gridList = gridList + '<div class="wms-srv-grid-cell">Quantity</div>';
            gridList = gridList + '</div>';

            for (var i = 0; i < mygridList.length; i++) {
                var getsku = mygridList[i].sku;
                var getserialnumber = mygridList[i].serialnumber;
                var getquantity = mygridList[i].quantity;

                gridList = gridList + '<div class="wms-srv-grid-row">';
                gridList = gridList + '<div class="wms-srv-grid-cell" style="text-align:center;">' + getsku + '</div>';
                gridList = gridList + '<div class="wms-srv-grid-cell" style="text-align:center;">' + getserialnumber + '</div>';
                gridList = gridList + '<div class="wms-srv-grid-cell" style="text-align:center;">' + getquantity + '</div>';
                gridList = gridList + '</div>';
            }
            $("#viewS2SOrderGrid").append(gridList);
        }
        else if (getStatus == 400) {
            alert('OmsorderNo is required.');
        }
        else {
            alert("Error Occured");
        }
    });

}

//function selectOrder(obj)
//{
//    debugger;
//    var getSelectedCheckBoxSave = $('#gridS2SOrderList .messageCheckbox:checked');
//    var referenceID = $(getSelectedCheckBoxSave).attr('data-id');
//    var status = $(getSelectedCheckBoxSave).attr('data-status');
//    if (status == 'Pending for driver allocation')
//    {
//        var cnt = $('.messageCheckbox:checked').length;
//        if (cnt > 0) {
//            $(".messageCheckbox").each(function ()
//            {
//                var isChecked = $(this).prop('checked');
//                if (isChecked === true)
//                {
//                    //$('#wms-srv-AssignDriverre-popup').show();
//                    //showdriverPopup();
//                }
//                else
//                {
//                    $(this).prop('checked', false);
//                }
//            });
//        }
//        else {
//            alert("Please Select Record..!!");
//        }
//    }
//    else
//    {
//        alert("Cannot Select this Order..");
//        $(obj).prop('checked', false);
//    }
//}

function selectOrder(obj)
{
    //$('.messageCheckbox').prop('checked', false);
    $(obj).prop('checked', true);
}

function SaveDriver()
{
    debugger;
    if (validatedriver() != false)
    {
        var apiPath = S2SwmsApiPath + 'S2SOMSOrder/S2SAssignDriver';
        //var sourceElement = document.querySelector('.messageCheckbox');
        //var referenceID = sourceElement.getAttribute('data-id');
        var getvehicleDetail = $('#txtDriverTruckNoS2S').val();

        var getSelectedCheckBoxSave = $('#gridS2SOrderList .messageCheckbox:checked');
        //var referenceID = $(getSelectedCheckBoxSave).attr('data-id');
        var referenceID = GetS2SSelectedCheckboxIds();
        //alert(referenceID);
        var status = $(getSelectedCheckBoxSave).attr('data-status');

        //var DriverID = $("#txtDriverID23").val();



        var postData =
        {
            "objectName": "S2S",
            "referenceID": referenceID,
            "driverId": glbdriverID,
            "assignBy": getUserId,
            "vehicleDetail": getvehicleDetail
        };
        callHttpCoreUrl(apiPath, postData, function (data)
        {
            debugger;
            var isStatus = data.statuscode;
            var getresult = data.status;
            if (isStatus == 200)
            {
                var returnmsg = getresult;
                if (returnmsg == "Success")
                {
                    alert("Driver Assigned Successfully.");
                    //glbdriverID = '';
                    //$(".messageCheckbox").attr('data-id', '');
                    //$(".messageCheckbox").attr('data-status', '');
                    S2SAssignDriverlist(referenceID, 'S2S');
                    $("#wms-srv-AssignDriverre-popup").hide();
                    S2SOrderList();
                }
                else {
                    alert("Failed to Save.");
                }
            }
            else if (isStatus == 601) {
                alert("Order Already assigned.");
            }
            else if (isStatus == 602) {
                alert("Can Not Assign Driver for Pick and Drop Status.");
            }
            else {
                alert("Error Occured.");
            }
        });
    }
}

var arrSelectedS2SOrders = [];

function GetS2SSelectedCheckboxIds() {
    var getS2SIds = '';
    for (var i = 0; i < arrSelectedS2SOrders.length; i++) {
        if (getS2SIds == '') {
            getS2SIds = getS2SIds + arrSelectedS2SOrders[i];
        } else {
            getS2SIds = getS2SIds + ', ' + arrSelectedS2SOrders[i];
        }
    }
    /*
    $('#gridS2SOrderList input.messageCheckbox:checked').each(function () {
        if (getS2SIds == '') {
            getS2SIds = getS2SIds + $(this).attr('data-id');
        } else {
            getS2SIds = getS2SIds + ', ' + $(this).attr('data-id');
        }
    });
    */
    return getS2SIds;
}

//function pushToS2SSelection(chkObj) {
//    debugger;
//    var chkId = 0;
//    var isChecked = $(chkObj).is(':checked');
//    if (isChecked) {
//        chkId = $(chkObj).attr('data-id');
//        arrSelectedS2SOrders.push(chkId);
//    } else {
//        chkId = $(chkObj).attr('data-id');
//        arrSelectedS2SOrders = arrSelectedS2SOrders.filter(item => item !== chkId);
//    }
//}

var arrSelectedS2SOrders = []; // holds selected IDs
var selectedS2SOrderCount = 0; // holds count of selected checkboxes

function pushToS2SSelection(chkObj)
{
    debugger;
    $("#txtcountchk").val('0');
    var chkId = $(chkObj).attr('data-id');
    var isChecked = $(chkObj).is(':checked');

    if (isChecked) {
        // Add only if not already in array
        if (!arrSelectedS2SOrders.includes(chkId)) {
            arrSelectedS2SOrders.push(chkId);
        }
    } else {
        // Remove unchecked ID
        arrSelectedS2SOrders = arrSelectedS2SOrders.filter(item => item !== chkId);
    }

    // ✅ Update global count variable
    selectedS2SOrderCount = arrSelectedS2SOrders.length;
    $("#txtcountchk").val(selectedS2SOrderCount);
    
    //console.log('Selected IDs:', arrSelectedS2SOrders);
    //console.log('Total selected:', selectedS2SOrderCount);
    //alert(selectedS2SOrderCount);
}



function selectCheckboxOnListLoad(chkDataId)
{
    debugger
    var isChecked = '';
    if (arrSelectedS2SOrders.includes(chkDataId)) {
        isChecked = 'checked="checked"';
    }
    return isChecked;   
}

//function isValidS2SSelection() {
//    var isValid = true;
//    $('#gridS2SOrderList .messageCheckbox:checked').each(function () {
//        var status = $(this).attr('data-status').toLowerCase().trim();
//        if (status !== 'pending for driver allocation' && status !== 'driver scheduled') {
//            isValid = false;
//            return false; // exit loop early
//        }
//    });
//    return isValid;
//}

//function isValidS2SSelection() {
//    var statuses = new Set();
//    var isValid = true;

//    $('#gridS2SOrderList .messageCheckbox:checked').each(function () {
//        var status = $(this).attr('data-status').toLowerCase().trim();

//        if (status !== 'pending for driver allocation' && status !== 'driver scheduled') {
//            isValid = false;
//            return false; // early exit
//        }

//        statuses.add(status);
//    });

//    // Allow only one type of valid status
//    if (!isValid || statuses.size > 1) {
//        return false;
//    }

//    return true;
//}

function isValidS2SSelection() {
    var statuses = new Set();
    var selectedDriverScheduledCount = 0;

    let isValid = true;
    let message = '';

    $('#gridS2SOrderList .messageCheckbox:checked').each(function () {
        var status = $(this).attr('data-status').toLowerCase().trim();

        if (status !== 'pending for driver allocation' && status !== 'driver scheduled') {
            isValid = false;
            message = 'Only orders with status "Pending for Driver Allocation" or "Driver Scheduled" are allowed.';
            return false;
        }

        if (status === 'driver scheduled') {
            selectedDriverScheduledCount++;
        }

        statuses.add(status);
    });

    if (!isValid) return { valid: false, message };

    if (statuses.size > 1) {
        return { valid: false, message: "Please select only one status type: either 'Pending for Driver Allocation' or 'Driver Scheduled'." };
    }

    if (statuses.has('driver scheduled') && selectedDriverScheduledCount > 1) {
        return { valid: false, message: "Only one 'Driver Scheduled' order can be selected at a time." };
    }

    return { valid: true };
}



function validatedriver()
{
    debugger;
    var truckdetails = $("#txtDriverTruckNoS2S").val();
    var drivername = $("#txtDriverList").val();
    
    if (drivername == "") {
        alert("Please Fill the Driver Name..");
        return false;
    }
    else {
        return true;
    };
}
function S2SAssignDriverlist(dataId, status) {
    debugger;
    var apiPath = S2SwmsApiPath + "S2SOMSOrder/S2SAssignDriverlist";

    var postData =
    {
        "referenceID": dataId,
        "objectName": status
    };
    callHttpCoreUrl(apiPath, postData, function (data) {
        var isStatus = data.statuscode;
        var mygridList = data.s2SDriverlist;
        //var TotalRecords = result.Table[0].TotalRecord;
        //var CurrentPage = result.Table[0].CurrentPage;
        //var mygridList = result.Table1;
        
        if (isStatus == 200) {
            $("#pnldriverlistAssign").html('');
            var gridList = '';

            gridList = gridList + '<div class="wms-srv-grid-header">';
            //gridList = gridList + '<div class="wms-srv-grid-cell"></div>';
            gridList = gridList + '<div class="wms-srv-grid-cell">Driver Name</div>';
            gridList = gridList + '<div class="wms-srv-grid-cell">Track Details</div>';
            gridList = gridList + '<div class="wms-srv-grid-cell">Assign DateTime</div>';
            //gridList = gridList + '<div class="wms-srv-grid-cell">Updated By</div>';
            gridList = gridList + '<div class="wms-srv-grid-cell">Action</div>';
            gridList = gridList + '</div>';

            for (var i = 0; i < mygridList.length; i++) {
                var getassignDate = mygridList[i].assignDate;
                var getdriverName = mygridList[i].driverName;
                var gettrackDetails = mygridList[i].trackDetails;
                var getreferenceId = mygridList[i].referenceId;
                var getdriverId = mygridList[i].driverId;

                gridList = gridList + '<div class="wms-srv-grid-row">';
                gridList = gridList + '<div class="wms-srv-grid-cell" style="text-align:center;">' + getdriverName + '</div>';
                gridList = gridList + '<div class="wms-srv-grid-cell" style="text-align:center;">' + gettrackDetails + '</div>';
                gridList = gridList + '<div class="wms-srv-grid-cell" style="text-align:center;">' + getassignDate + '</div>';
                gridList = gridList + '<div class="wms-srv-grid-cell">';
                gridList = gridList + '<div class="wms-srv-grid-action">';
                gridList = gridList + '<a href="#" title="" class="wms-srv-icononly" onclick="RemoveDriver(' + getreferenceId + ',' + getdriverId + ',\'' + status + '\');"><i class="fas fa-times-circle" title=""></i></a>';
                gridList = gridList + '</div>';
                gridList = gridList + '</div>';
                gridList = gridList + '</div>';
            }
            $("#pnldriverlistAssign").append(gridList);
        }
        else if (isStatus == 404) {
            //alert("Driver Not found");
            $("#pnldriverlistAssign").html('');
            //$('.messageCheckbox').prop('checked', false);
        }
        else if (isStatus == 505) {
            alert("Something Went Wrong");
            $("#pnldriverlistAssign").html('');

        }
        else {
            alert("Error Occured.");
        }

    });
}

function RemoveDriver(getreferenceId, getdriverId, status) {
    debugger;
    var apiPath = S2SwmsApiPath + 'S2SOMSOrder/S2SRemoveAssignDriver';
    //var sourceElement = document.querySelector('.messageCheckbox');
    //var referenceID = sourceElement.getAttribute('data-id');
    //var getvehicleDetail = $('#txtDriverTruckNoS2S').val();

    var postData =
    {
        "objectName": "S2S",
        "referenceID": getreferenceId,
        "driverId": getdriverId
    }
    callHttpCoreUrl(apiPath, postData, function (data) {
        debugger;
        var isStatus = data.statuscode;
        var getresult = data.status;
        if (isStatus == 200) {
            var returnmsg = getresult;
            if (returnmsg == "Success") {
                alert("Assigned Driver Removed Successfully.");
                S2SAssignDriverlist(getreferenceId, status);
                S2SOrderList();
            }
            else {
                alert("Failed to Save.");
            }
        }
        else if (isStatus == 601) {
            alert("Order Not Assigned to Driver.");
        }
        else if (isStatus == 602) {
            alert("Selected Order Not Available.");
        }
        else if (isStatus == 603) {
            alert("Error Occured.");
        }
    });
}


function S2Sdocumentlist(refid, getstatus) {
    debugger;
    var apiPath = S2SwmsApiPath + "S2SOMSOrder/S2Sdocumentlist";

    var postData =
    {
        "referenceID": refid,
        "companyID": getCompanyId,
        "objectName": getstatus
    };
    callHttpCoreUrl(apiPath, postData, function (data) {
        var isStatus = data.statuscode;
        var mygridList = data.documentlist;
        //var TotalRecords = result.Table[0].TotalRecord;
        //var CurrentPage = result.Table[0].CurrentPage;
        //var mygridList = result.Table1;
        var gridList = '';
        $("#PnlDocumentListS2S").html('');
        gridList = gridList + '<div class="wms-srv-grid-header">';
        //gridList = gridList + '<div class="wms-srv-grid-cell"></div>';
        gridList = gridList + '<div class="wms-srv-grid-cell">Document Name</div>';
        gridList = gridList + '<div class="wms-srv-grid-cell">Description</div>';
        gridList = gridList + '<div class="wms-srv-grid-cell">Document DateTime</div>';
        gridList = gridList + '<div class="wms-srv-grid-cell">Document Type</div>';
        gridList = gridList + '<div class="wms-srv-grid-cell">file Type</div>';
        gridList = gridList + '<div class="wms-srv-grid-cell">Attached File</div>';
        gridList = gridList + '<div class="wms-srv-grid-cell">Action</div>';
        gridList = gridList + '</div>';
       
        if (isStatus == 200)
        {        
            for (var i = 0; i < mygridList.length; i++)
            {
                var getid = mygridList[i].id;
                var getdocumentName = mygridList[i].documentName;
                var getdescription = mygridList[i].description;
                var getordertype = mygridList[i].ordertype;
                var getdocumentType = mygridList[i].documentType;
                var getfileType = mygridList[i].fileType;
                var getFilePath = mygridList[i].attachedFile;
                var getRefId = mygridList[i].referenceID;
                var creationDate = mygridList[i].creationDate;
                var getcreationDate = creationDate.replace('T', ' ');

                gridList = gridList + '<div class="wms-srv-grid-row">';
                gridList = gridList + '<div class="wms-srv-grid-cell" style="text-align:center;">' + getdocumentName + '</div>';
                gridList = gridList + '<div class="wms-srv-grid-cell" style="text-align:center;">' + getdescription + '</div>';
                gridList = gridList + '<div class="wms-srv-grid-cell" style="text-align:center;">' + getcreationDate + '</div>';
                gridList = gridList + '<div class="wms-srv-grid-cell" style="text-align:center;">' + getdocumentType + '</div>';
                gridList = gridList + '<div class="wms-srv-grid-cell" style="text-align:center;">' + getfileType + '</div>';
                gridList = gridList + '<div class="wms-srv-grid-cell" style="text-align:center;">' + getDocumentAttachmentHtmlS2S(getFilePath, getRefId) + '</div>';
                gridList = gridList + '<div class="wms-srv-grid-cell">';
                gridList = gridList + '<div class="wms-srv-grid-action">';
                gridList = gridList + '<a href="#" download title="Download" class="wms-srv-save" onclick="downloadimage(\'' + getFilePath + '\');"><i class="fas fa-download"></i></a>';
                gridList = gridList + '</div>';
                gridList = gridList + '</div>';
                gridList = gridList + '</div>';
            }
            
        }
        
        else if (isStatus == 404) {
            //alert("Document Not found");
        }
        else if (isStatus == 505) {
            alert("Something Went Wrong");
        }
        else {
            alert("Error Occured.");
        }
        $("#PnlDocumentListS2S").append(gridList);
    });
}

function getDocumentAttachmentHtmlS2S(filePath, Refid) {

    var GetFileExtension = findDocumentFileExtensionS2S(filePath);
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
                '<a href="' + filePath + '" style="cursor: pointer" target="_blank">filePath</a>' +
                // '<img src="' + filePath + '" alt="image Loading.." height="50px" width="50px">' +

                '</span>' +
                '</span>';
        }
    }


    return strAttachment;
}

function findDocumentFileExtensionS2S(filePath) {
    debugger;
    var FileExtension = '';
    // fileName = document.querySelector('#wms-srv-cmDocument-file-upload').value;
    extension = filePath.split('.').pop();
    //  $('#txtCommonDocument_FileType').val(extension);
    //  $('#txtCommonDocument_Name').val(fileName);
    return FileExtension;
}


function downloadimage(getFilePath)
//document.getElementById("downloadButton").addEventListener("click", function() 
{
    debugger;
    const imageUrl = getFilePath;  // Get image URL
    const fileName = "downloaded_image.jpg";  // Set file name for download

    const a = document.createElement("a");  // Create a temporary <a> element
    a.href = imageUrl;  // Set the image URL as href
    a.download = fileName;  // Set the file name for the download
    document.body.appendChild(a);  // Append the link to the body
    a.click();  // Programmatically click the link to trigger the download
    document.body.removeChild(a);  // Remove the link after downloading
};


//function setupGridPagingList(gridObjId, strCurrentPage, strTotalRecords, callBackFunction) {
//    var global_max_record_count = 10;
//    var pageNo = Number(strCurrentPage);
//    var recordFrom = ((pageNo - 1) * 10) + 1;
//    var recordTo = recordFrom + 9;
//    var totalRecord = Number(strTotalRecords);
//    var pagerLinks = '';

//    if (totalRecord < recordTo) {
//        recordTo = totalRecord;
//    }

//    $('#' + gridObjId + ' .wms-srv-pager-records').html(recordFrom + '-' + recordTo + ' of ' + totalRecord + ' Records');
//    var lnkCounter = 1;
//    var currentCounter = global_max_record_count;
//    var pagingGroup = 1;
//    var pagingGroupCounter = 1;
//    var isFirstGroupLink = 'yes';
//    var lastPage = 0;
//    pagerLinks += recordFrom + '-' + recordTo + ' of ' + totalRecord + ' Records <span class="wms-srv-empty-space"></span>';
//    pagerLinks += '<b>Go to Page: </b> <select id="ddlGridPageNo" class="wms-srv-input ddlGridPageNo" style="width:100px;">';
//    while (currentCounter < totalRecord) {
//        if (lnkCounter == pageNo) {
//            pagerLinks += '<option selected="selected">' + lnkCounter + '</option>';
//        } else {
//            pagerLinks += '<option>' + lnkCounter + '</option>';
//            lastPage = lnkCounter;
//        }
//        global_last_page_no = lnkCounter;
//        currentCounter = currentCounter + global_max_record_count;
//        // Group Counter 
//        isFirstGroupLink = 'no';
//        pagingGroupCounter = pagingGroupCounter + 1;
//        // Group Counter
//        lnkCounter = lnkCounter + 1;
//    }

//    // Add Page link for remaining record 
//    if (totalRecord <= currentCounter) {
//        pagerLinks += '<option>' + lnkCounter + '</option>';
//        lastPage = lnkCounter;
//        this.global_last_page_no = lnkCounter;
//    }
//    pagerLinks += '</select>';
//    pagerLinks += '<a href="#" title="Jump To" class="wms-srv-ddlpager-go" style="font-size:16px;"><i class="fas fa-play-circle"></i></a>';

//    // Add Page link for remaining record
//    var pagerNavLinks = '';
//    pagerNavLinks += pagerLinks;

//    $('#' + gridObjId + '.wms-srv-grid-pager').html(pagerNavLinks);

//    $('#' + gridObjId + ' a.wms-srv-ddlpager-go').off();
//    $('#' + gridObjId + ' a.wms-srv-ddlpager-go').click(function () {
//        var getDataPage = $('#' + gridObjId + ' .ddlGridPageNo').val();
//        CurrentPage = getDataPage;
//        if (Number(getDataPage) < 1) {
//            alert('Please enter valid page number!!');
//        } else if (Number(getDataPage) > Number(lastPage)) {
//            alert('Page number should not be greater than ' + lastPage + ' !!');
//        } else {
//            //showHidePagingGroup(gridObjId);
//            if (callBackFunction != null) {
//                callBackFunction(getDataPage, searchfilter, searchvalue);
//            }
//        }
//        return false;
//    });
//    //showHidePagingGroup(gridObjId);
//}

function showHidePagingGroup(gridObjId) {
    var pagingGroup = $('#' + gridObjId + ' .wms-srv-pager-links .wms-paging-link[data-page="' + CurrentPage + '"]').attr('data-group');
    $('#' + gridObjId + ' .wms-srv-pager-links .wms-paging-link').hide();
    $('[data-group="' + pagingGroup + '"]').show();
    $('#' + gridObjId + ' .wms-srv-pager-links .wms-first-paging-yes').show();
}

function setupGridPagingList(gridObjId, strCurrentPage, strTotalRecords, callBackFunction, strMaxRecordCount) {
    var global_max_record_count = 10;
    if (strMaxRecordCount != null) {
        global_max_record_count = strMaxRecordCount;
    }
    var pageNo = Number(strCurrentPage);
    var recordFrom = ((pageNo - 1) * global_max_record_count) + 1;
    var recordTo = recordFrom + (global_max_record_count - 1);
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
            showHidePagingGroup(gridObjId);
            if (callBackFunction != null) {
                callBackFunction(getDataPage, searchfilter, searchvalue);
            }
        }
        return false;
    });
    showHidePagingGroup(gridObjId);
}


function clearData() {
    $('#viewWinCashS2SOrderNo').val('');
    $('#viewomsS2Sorderno').val('');
    $('#viewsourcestoreS2S').val('');
    $('#views2sorderStatus').val('');
    $('#viewCreationDateS2S').val('');
    $('#viewDestinationStoreS2S').val('');
    $('#viewperformedbyS2S').val('');
    $('#viewreceivedByS2S').val('');

    $('#viewS2SOrderGrid').html('');
}

function cancelOrder(orderID) {
    debugger;
    var apiPath = S2SwmsApiPath + "S2SOMSOrder/CancelS2SOrder";
    //var orderID = $(this).attr('data-id');
    var postData =
    {
        "orderID": orderID,
        //"userID": 21146
        "userID": getUserId
    };
    callHttpCoreUrl(apiPath, postData, function (data) {
        debugger;
        var isStatus = data.statuscode;
        var getresult = data.status;
        if (isStatus == 200) {
            alert("Removed Successfully.");
            S2SOrderList();
        }
        else if (isStatus == 201) {
            alert("Error Occured.");
        }
        else {
            alert("Error Occured.");
        }
    });
}


function CancelselectOrder() {
    debugger;
    var cnt = $('.messageCheckbox:checked').length;

    if (cnt > 0) {
        $(".messageCheckbox").each(function () {

            var isChecked = $(this).prop('checked'); //data - status
            if (isChecked === true) {
                var orderID = $(this).attr('data-id');
                var datastatus = $(this).attr('data-status');
                if (datastatus == 'Pending for driver allocation') {
                    alert("No Cancel");
                }
                else {
                    cancelOrder(orderID);

                }

            }
            else {
                $(this).prop('checked', false);
            }
        });
    }
    else {
        alert("Please Select Record..!!");
    }
}


function showOrderDetailPopup() {
    $("#wms-srv-Order-Detail-popup").show();
    $("#wms-srv-Order-Detail-popup-close").click(function () {
        $("#wms-srv-Order-Detail-popup-close").off();
        $("#wms-srv-Order-Detail-popup").hide();
    });
    // S2Sdocumentlist(refid);
    //$("#btnSearchReport").click(function () {
    //    getconsumptiondashboardList();
    //})
    $('#txtOrderNo').val('');
    $('#ddlOrderStatus').val('');

    $(".iconClose").click(function () {
        $("#dateSelecter" + obj).hide();
    });
    $('#txt_FromdatePicker').datepicker({
        dateFormat: 'yy/mm/dd',
        onSelect: function (date) {
            var getFromDate = $('#txt_FromdatePicker').val();
            var getToDate = $('#txt_TodatePicker').val();
            $('#txtFromToDate').val(getFromDate + " to " + getToDate);
            $("#hideFromDate").val(getFromDate);
        }
    });
    $('#txt_TodatePicker').datepicker({
        dateFormat: 'yy/mm/dd',
        onSelect: function (date) {
            var getFromDate = $('#txt_FromdatePicker').val();
            var getToDate = $('#txt_TodatePicker').val();
            $('#txtFromToDate').val(getFromDate + " to " + getToDate);
            $("#hideToDate").val(getToDate);
        }
    });

    globalDefaultFromDate = $("#hideFromDate").val();
    globalDefaultToDate = $("#hideToDate").val();
    var getFromDate = "";
    var getSearchFromDate = "2025-01-01";
    getSearchFromDate = getFromDate.substr(0, 10);
    getSearchToDate = getFromDate.substr(14, 24);
    loadCurrentDate();
    getOrderdetailReportlist();

}

function getSevenDateData(dateType) {
    debugger;
    var today = new Date();
    var todaydate = today.getDate();
    var dd = todaydate - dateType;
    var mm = today.getMonth() + 1;
    var yyyy = today.getFullYear();
    if (dd < 10) {
        dd = '0' + dd;
    }

    if (mm < 10) {
        mm = '0' + mm;
    }
    today = yyyy + '-' + mm + '-' + dd;

    //userSelectedDate = today.split("-").reverse().join("/");
    userSelectedDate = today.replaceAll("-", "/");
    $('#txtFromToDate').val(userSelectedDate + " To " + GetCurrentUserDate);
    //getAssetReportList(userSelectedDate);

}

function getThirtyDateData(dateType) {

    var today = new Date();
    var todaydate = today.getDate();
    var dd = dateType - todaydate;

    var mm = today.getMonth() + 1;
    var yyyy = today.getFullYear();
    if (dd < 10) {
        dd = '0' + dd;
    }
    //yyyy-mm-dd
    if (mm < 10) {
        mm = '0' + mm;
    }
    today = yyyy + '-' + mm + '-' + dd;

    //userSelectedDate = today.split("-").reverse().join("/");
    userSelectedDate = today.replaceAll("-", "/");
    $('#txtFromToDate').val(userSelectedDate + " To " + GetCurrentUserDate);
    //getAssetReportList(userSelectedDate);
}

function getPreviousdate(dateType) {

    var today = new Date();
    var todaydate = today.getDate();
    var dd = todaydate - dateType;
    //var getDateIndex = day.indexOf("-");
    // var dd ="";
    // if( getDateIndex == 0)
    // {
    //     dd= day.slice(0);

    // }   
    var mm = today.getMonth() + 1;
    var yyyy = today.getFullYear();
    if (dd < 10) {
        dd = '0' + dd;
    }
    //yyyy-mm-dd
    if (mm < 10) {
        mm = '0' + mm;
    }
    today = yyyy + '-' + mm + '-' + dd;

    //userSelectedDate = today.split("-").reverse().join("/");
    userSelectedDate = today.replaceAll("-", "/");
    $('#txtFromToDate').val(userSelectedDate + " To " + GetCurrentUserDate);
    //getAssetReportList(userSelectedDate);
}

function getcurrentFYData(dateType) {

    //     var today  = new Date();
    //     var todaydate = today.getDate();
    //     var dd = dateType - todaydate;

    //     var mm = today.getMonth()+1; 
    //     var yyyy = today.getFullYear();
    //     if(dd<10) 
    //     {
    //     dd='0'+dd;
    //     } 
    // //yyyy-mm-dd
    //     if(mm<10) 
    //     {
    //     mm='0'+mm;
    //     } 
    //     today = yyyy+'-'+mm+'-'+dd;

    //     //userSelectedDate = today.split("-").reverse().join("/");
    //     userSelectedDate = today.replaceAll("-","/");
    $('#txtFromToDate').val(currentfinacial + " To " + GetCurrentUserDate);
    //getAssetReportList(userSelectedDate);
}

function loadCurrentDate() {

    var today = new Date();
    var todaydate = today.getDate();
    var dd = todaydate
    var mm = today.getMonth() + 1;
    var yyyy = today.getFullYear();
    if (dd < 10) {
        dd = '0' + dd;
    }

    if (mm < 10) {
        mm = '0' + mm;
    }
    today = yyyy + '-' + mm + '-' + dd;
    GetCurrentUserDate = today.replaceAll("-", "/");
    $('#txtFromToDate').val(currentfinacial + " To " + GetCurrentUserDate);
}

function getDate() {
    $("#dateSelecter").show();

}
function openDatePopup() {
    $('#dateSelecter').hide();
}

var getSelectedDateCatg = "";
var GetCurrentUserDate = "";
var currentfinacial = "2025/05/14";
var getFromDateSelected = "";
var getToDateSelected = "";
var globalDefaultFromDate = "";
var globalDefaultToDate = "";
var newFromFormatted = '';

$("#wms-srv-objrexta-popup-close").click(function () {
    debugger;
    $('#wms-srv-objrexta-popup').hide();
});

function getOrderdetailReportlist() {
    debugger;
    var getSearchToDate = "";
    var apiPath = S2SwmsApiPath + "S2SOMSOrder/S2SOrderReport";

    var getOrderNo = $('#txtOrderNo').val();
    var getStatus = $('#ddlOrderStatus').val();

    var getFromDate = $("#txtFromToDate").val();
    var getSearchFromDate = "";
    var getSearchToDate = "";
    getSearchFromDate = getFromDate.substr(0, 10);
    getSearchToDate = getFromDate.substr(14, 24);

    //var getFromDate = $("#txtFromToDate").val();
    //var getSearchFromDate = "";
    //var getSearchToDate = "";

    //if (getFromDate && getFromDate.includes(" - ")) {
    //    var dateParts = getFromDate.split(" - ");
    //    getSearchFromDate = dateParts[0]; // "Apr 01, 2024"
    //    getSearchToDate = dateParts[1];   // "Apr 14, 2024"
    //}

    var fromFormatted = convertToYYYYMMDD(getSearchFromDate); // "2024-04-01"
    var toFormatted = convertToYYYYMMDD(getSearchToDate);

    // Convert to Date object
    var fromDate = new Date(fromFormatted);
    var ToDate = new Date(toFormatted);

    // Subtract 10 days
    ToDate.setDate(ToDate.getDate() - 40);

    newFromFormatted = convertToYYYYMMDDer(ToDate);

    var postData =
    {
        "orderNo": getOrderNo,
        "fromDate": fromFormatted,
        "toDate": toFormatted,
        "status": getStatus
    };


    callHttpCoreUrl(apiPath, postData, function (data) {
        var getStatus = data.statuscode;
        var getStatusCode = data.StatusCode;

        $("#gettopfiveorderobjrexta").html("");
        if (getStatus == 200) {
            var myGridList = data.s2SOrderReportlist;

            var myGrid = "";
            myGrid = myGrid + '<div class="wms-srv-grid-header reportHeaderRow" style="background-color: #1c8fca;" id="header-wrap">';
            myGrid = myGrid + '<div class="wms-srv-grid-cell reportHeaderCell"  style="text-align:center;color: #b3b3b3;font-weight: 500;"><b style="color:rgb(5, 15, 20);font-size: 13px;">Oms Order No</b></div>';
            myGrid = myGrid + '<div class="wms-srv-grid-cell reportHeaderCell" style="text-align:center;color: #b3b3b3;font-weight: 500;"><b style="color:rgb(5, 15, 20);font-size: 13px;">WinCash Order No</b></div>';
            myGrid = myGrid + '<div class="wms-srv-grid-cell reportHeaderCell" style="text-align:center;color: #b3b3b3;font-weight: 500;"><b style="color:rgb(5, 15, 20);font-size: 13px;">Status</b></div>';
            myGrid = myGrid + '<div class="wms-srv-grid-cell reportHeaderCell" style="text-align:center;color: #b3b3b3;font-weight: 500;"><b style="color:rgb(5, 15, 20);font-size: 13px;">DateTime</b></div>';
            myGrid = myGrid + '<div class="wms-srv-grid-cell reportHeaderCell" style="text-align:center;color: #b3b3b3;font-weight: 500;"><b style="color:rgb(5, 15, 20);font-size: 13px;">Source Store Name</b></div>';
            myGrid = myGrid + '<div class="wms-srv-grid-cell reportHeaderCell"  style="text-align:center;color: #b3b3b3;font-weight: 500;"><b style="color:rgb(5, 15, 20);font-size: 13px;">Dest Store Name</b></div>';
            myGrid = myGrid + '<div class="wms-srv-grid-cell reportHeaderCell"  style="text-align:center;color: #b3b3b3;font-weight: 500;"><b style="color:rgb(5, 15, 20);font-size: 13px;">Performed By</b></div>';
            myGrid = myGrid + '<div class="wms-srv-grid-cell reportHeaderCell"  style="text-align:center;color: #b3b3b3;font-weight: 500;"><b style="color:rgb(5, 15, 20);font-size: 13px;">Received By</b></div>';
            myGrid = myGrid + '<div class="wms-srv-grid-cell reportHeaderCell"  style="text-align:center;color: #b3b3b3;font-weight: 500;"><b style="color:rgb(5, 15, 20);font-size: 13px;">Sku</b></div>';
            myGrid = myGrid + '<div class="wms-srv-grid-cell reportHeaderCell"  style="text-align:center;color: #b3b3b3;font-weight: 500;"><b style="color:rgb(5, 15, 20);font-size: 13px;">Description</b></div>';
            myGrid = myGrid + '<div class="wms-srv-grid-cell reportHeaderCell"  style="text-align:center;color: #b3b3b3;font-weight: 500;"><b style="color:rgb(5, 15, 20);font-size: 13px;">SR.NO</b></div>';
            myGrid = myGrid + '<div class="wms-srv-grid-cell reportHeaderCell"  style="text-align:center;color: #b3b3b3;font-weight: 500;"><b style="color:rgb(5, 15, 20);font-size: 13px;">Quantity</b></div>';
            myGrid = myGrid + '</div>';

            for (var i = 0; i < myGridList.length; i++) {
                var getomsOrderNo = myGridList[i].omsOrderNo;
                var getwinCashOrderNumber = myGridList[i].winCashOrderNumber;
                var getstatus = myGridList[i].status;
                var getsourceStoreName = myGridList[i].sourceStoreName;
                var getdestinationStoreName = myGridList[i].destinationStoreName;
                var getperformedby = myGridList[i].performedby;
                var getreceivedby = myGridList[i].receivedby;
                var getsku = myGridList[i].sku;
                var getdescription = myGridList[i].description;
                var getserialnumber = myGridList[i].serialnumber;
                var getquantity = myGridList[i].quantity;
                var getCreationDate = myGridList[i].creationDate;

                myGrid = myGrid + '<div id="" class="wms-srv-grid-row reportRow">';
                myGrid = myGrid + '<div class="wms-srv-grid-cell reportRowCell" style="text-align:center;">' + getomsOrderNo + '</div>';
                myGrid = myGrid + '<div class="wms-srv-grid-cell reportRowCell" style="text-align:center;">' + getwinCashOrderNumber + '</div>';
                myGrid = myGrid + '<div class="wms-srv-grid-cell reportRowCell" style="text-align:center;">' + getstatus + '</div>';
                myGrid = myGrid + '<div class="wms-srv-grid-cell reportRowCell" style="text-align:center;">' + getCreationDate + '</div>';
                myGrid = myGrid + '<div class="wms-srv-grid-cell reportRowCell" style="text-align:center;">' + getsourceStoreName + '</div>';
                myGrid = myGrid + '<div class="wms-srv-grid-cell reportRowCell" style="text-align:center;">' + getdestinationStoreName + '</div>';
                myGrid = myGrid + '<div class="wms-srv-grid-cell reportRowCell" style="text-align:center;">' + getperformedby + '</div>';
                myGrid = myGrid + '<div class="wms-srv-grid-cell reportRowCell" style="text-align:center;">' + getreceivedby + '</div>';
                myGrid = myGrid + '<div class="wms-srv-grid-cell reportRowCell" style="text-align:center;">' + getsku + '</div>';
                myGrid = myGrid + '<div class="wms-srv-grid-cell reportRowCell" style="text-align:center;">' + getdescription + '</div>';
                myGrid = myGrid + '<div class="wms-srv-grid-cell reportRowCell" style="text-align:center;">' + getserialnumber + '</div>';
                myGrid = myGrid + '<div class="wms-srv-grid-cell reportRowCell" style="text-align:center;">' + getquantity + '</div>';
                myGrid = myGrid + '</div>';
            }
            $('#gettopfiveorderobjrexta').html(myGrid);
        }
    });
    $("#dateSelecter").hide();
}

function convertToYYYYMMDD(dateStr) {
    var date = new Date(dateStr);
    var yyyy = date.getFullYear();
    var mm = String(date.getMonth() + 1).padStart(2, '0');
    var dd = String(date.getDate()).padStart(2, '0');
    return `${yyyy}-${mm}-${dd}`;
}

function convertToYYYYMMDDer(date) {
    var yyyy = date.getFullYear();
    var mm = String(date.getMonth() + 1).padStart(2, '0');
    var dd = String(date.getDate()).padStart(2, '0');
    return `${yyyy}-${mm}-${dd}`;
}

function exportAtClient() {
    var getPageTitle = $('.previewPageTitle').text() || "Report";
    var finalData = [];

    // Try to get the header row
    var headerAdded = false;
    $('.previewTable .reportHeaderRow').each(function () {
        var rowData = [];
        $(this).find('.reportHeaderCell').each(function () {
            if (!$(this).hasClass('detailIconHeader')) {
                rowData.push($(this).text().trim());
            }
        });
        if (rowData.length > 0) {
            finalData.push(rowData);
            headerAdded = true;
        }
    });

    // Get data rows
    $('.previewTable .reportRow').each(function () {
        var rowData = [];
        $(this).find('.reportRowCell').each(function () {
            if (!$(this).hasClass('detailIconCell')) {
                rowData.push($(this).text().trim());
            }
        });
        if (rowData.length > 0) {
            finalData.push(rowData);
        }
    });

    // Fallback: If no header was found, create dummy headers
    if (!headerAdded && finalData.length > 0) {
        var dummyHeaders = [];
        for (var i = 0; i < finalData[0].length; i++) {
            dummyHeaders.push("Column " + (i + 1));
        }
        finalData.unshift(dummyHeaders);
    }

    // Export to XLSX
    var worksheet = XLSX.utils.aoa_to_sheet(finalData);
    var workbook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(workbook, worksheet, "Sheet1");
    XLSX.writeFile(workbook, getPageTitle + ".xlsx");
}
