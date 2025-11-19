var CurrentPage = 1;
var RecordLimit = 10;
var searchfilter = 'ALL';
var searchvalue = 'none';
var glbdriverID = '';
var getUserId = $('#sethdnUserIdS2S').val();
var getCompanyId = 10204;

$(document).ready(function () {
    GetS2SUserConfigList();
});

function ClearUserList()
{
    $("#txtUserSearchS2S").val('');
    GetS2SUserList();
}

function showUserlist() {
    $("#wms-srv-UserList-popup").show();
    $("#wms-srv-UserList-popup-close").click(function () {
        $("#wms-srv-UserList-popup").off();
        $("#wms-srv-UserList-popup").hide();
    });
    $("#txtUserSearchS2S").val('');
    GetS2SUserList();
}

function GetS2SUserConfigList() {
    debugger;
    var apiPath = S2SwmsApiPath + "S2SUserConfig/GetS2SUserConfigList";
    searchfilter = $("#ddlS2SOrderColSearchUser").val();
    searchvalue = $("#ddlS2SFilterValueUser").val();
    if (searchfilter == '') {
        searchfilter = '';
        searchvalue = '';
    }

    var postData =
    {
        "currentPage": CurrentPage,
        "recordLimit": RecordLimit,
        "companyID": getCompanyId,
        "userID": getUserId,
        "search": searchvalue,
        "filter": searchfilter
    };
    callHttpCoreUrl(apiPath, postData, function (data) {
        var isStatus = data.statuscode;
        var mygridList = data.userGridList;
        var TotalRecords = data.totalRecords;
        //var CurrentPage = result.Table[0].CurrentPage;
        //var mygridList = result.Table1;
        $("#wms-srv-grid-S2SUserList").html('');

        if (isStatus == 200) {
            var gridList = '';

            gridList = gridList + '<div class="wms-srv-grid-header" id="header-wrap">';
            gridList = gridList + '<div class="wms-srv-grid-cell">Customer</div>';
            gridList = gridList + '<div class="wms-srv-grid-cell">User Name</div>';
            gridList = gridList + '<div class="wms-srv-grid-cell">User Type</div>';
            gridList = gridList + '<div class="wms-srv-grid-cell">Active</div>';
            gridList = gridList + '<div class="wms-srv-grid-cell">Action</div>';
            gridList = gridList + '</div>';

            for (var i = 0; i < mygridList.length; i++) {
                var getId = mygridList[i].id;
                var getcustomer = mygridList[i].customer;
                var getuserName = mygridList[i].userName;
                var getuserType = mygridList[i].userType;
                var getactive = mygridList[i].active;

                gridList = gridList + '<div class="wms-srv-grid-row">';
                gridList = gridList + '<div class="wms-srv-grid-cell" style="text-align:center;">' + getcustomer + '</div>';
                gridList = gridList + '<div class="wms-srv-grid-cell" style="text-align:center;">' + getuserName + '</div>';
                gridList = gridList + '<div class="wms-srv-grid-cell" style="text-align:center;">' + getuserType + '</div>';
                gridList = gridList + '<div class="wms-srv-grid-cell" style="text-align:center;">' + getactive + '</div>';
                gridList = gridList + '<div class="wms-srv-grid-cell">';
                gridList = gridList + '<div class="wms-srv-grid-action">';
                //gridList = gridList + '<label class="switch">';
                //gridList = gridList + '<input type="checkbox" checked="" onclick="">';
                //gridList = gridList + '<span class="slider round"></span>';
                //gridList = gridList + '</label>';
                //gridList = gridList + '<div class="wms-srv-action-sep">|</div>';
                gridList = gridList + '<i class="fas fa-times-circle"  title="Remove" onclick="RemoveUser(' + getId + ');"></i>';
                gridList = gridList + '</div>';
                gridList = gridList + '</div>';
                gridList = gridList + '</div>';

            }
            $("#wms-srv-grid-S2SUserList").append(gridList);
            setupGridPagingList('tblUserlistpager', CurrentPage, TotalRecords, GetS2SUserConfigList);
        }
        else if (isStatus == 404) {
            alert("Records Not Found");
        }
        else {
            alert("Error Occured");
        }

    });
}

var divHeight = $('#header-wrap').height();
$('#wms-srv-grid-S2SUserList').css('margin-top', divHeight + 'px');

function searchUserOrder() {
    var searchCol = $("#ddlS2SOrderColSearchUser").val();
    var searchVal = $("#ddlS2SFilterValueUser").val();

    if (searchCol != '' && searchVal != '') {
        GetS2SUserConfigList();
    }
    else {
        alert("Please Select Proper Search Value..!!");
    }
}
function clearSearchFilterUser() {
    $("#ddlS2SFilterValueUser").val('');
    $("#ddlS2SOrderColSearchUser").val(0);
    GetS2SUserConfigList();
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


function GetS2SUserList() {
    debugger;
    var apiPath = S2SwmsApiPath + "S2SUserConfig/GetS2SUserList";
    var searchFName = $("#txtUserSearchS2S").val();

    var postData =
    {
        "companyID": getCompanyId,
        "searchFName": searchFName
    };
    callHttpCoreUrl(apiPath, postData, function (data) {
        var isStatus = data.statuscode;
        var mygridList = data.userList;
        //var TotalRecords = data.totalRecords;
        //var CurrentPage = result.Table[0].CurrentPage;
        //var mygridList = result.Table1;

        if (isStatus == 200) {
            $("#pnlS2SUserList").html('');
            var gridList = '';

            gridList = gridList + '<div class="wms-srv-grid-header">';
            gridList = gridList + '<div class="wms-srv-grid-cell">First Name</div>';
            gridList = gridList + '<div class="wms-srv-grid-cell">Last Name</div>';
            gridList = gridList + '<div class="wms-srv-grid-cell">User Type</div>';
            gridList = gridList + '<div class="wms-srv-grid-cell">User Name</div>';
            gridList = gridList + '<div class="wms-srv-grid-cell">Email ID</div>';
            gridList = gridList + '<div class="wms-srv-grid-cell">Action</div>';
            gridList = gridList + '</div>';

            for (var i = 0; i < mygridList.length; i++) {
                var getId = mygridList[i].id;
                var getfirstName = mygridList[i].firstName;
                var getlastName = mygridList[i].lastName;
                var getuserType = mygridList[i].userType;
                var getuserName = mygridList[i].userName;
                var getemailID = mygridList[i].emailID;
                var getFullUserName = getfirstName + " " + getlastName;


                gridList = gridList + '<div class="wms-srv-grid-row">';
                gridList = gridList + '<div class="wms-srv-grid-cell" style="text-align:center;">' + getfirstName + '</div>';
                gridList = gridList + '<div class="wms-srv-grid-cell" style="text-align:center;">' + getlastName + '</div>';
                gridList = gridList + '<div class="wms-srv-grid-cell" style="text-align:center;">' + getuserType + '</div>';
                gridList = gridList + '<div class="wms-srv-grid-cell" style="text-align:center;">' + getuserName + '</div>';
                gridList = gridList + '<div class="wms-srv-grid-cell" style="text-align:center;">' + getemailID + '</div>';
                gridList = gridList + '<div class="wms-srv-grid-cell">';
                gridList = gridList + '<div class="wms-srv-grid-action">';
                gridList = gridList + '<i class="fas fa-check-circle"  title="Save" class="wms-srv-save" onclick="BindUser(\'' + getFullUserName + '\',\'' + getId + '\')"></i>';
                gridList = gridList + '</div>';
                gridList = gridList + '</div>';
                gridList = gridList + '</div>';

            }
            $("#pnlS2SUserList").append(gridList);
        }
        else {
            alert("Not Found");
        }

    });
}


function BindUser(getuserName, getId) {
    $("#plnUserNameS2S").val(getuserName);
    $('#sethdnUserIdS2S').val(getId);
    $("#wms-srv-UserList-popup").hide();
}



function SaveUser() {
    debugger;
    if (validateUser() != false) {
        var apiPath = S2SwmsApiPath + 'S2SUserConfig/SaveS2SUserConfig';
        var getUserId = $('#sethdnUserIdS2S').val();

        var postData =
        {
            "objectName": "S2SOrder",
            "userID": getUserId,
            "createdBy": getUserId,
            "companyID": getCompanyId
        }
        callHttpCoreUrl(apiPath, postData, function (data) {
            debugger;
            var isStatus = data.statuscode;
            var getresult = data.status;
            if (isStatus == 200) {
                alert("Saved Successfully.");
                $("#plnUserNameS2S").val('');
                $('#sethdnUserIdS2S').val('');
                GetS2SUserConfigList();
            }
            else if (isStatus == 201) {
                alert("Error Occured.");
            }
            else if (isStatus == 202) {
                alert(getresult);
            }
        });
    }
}

function RemoveUser(getId)
{
    var result = confirm("Do you want to remove this User?");
    if (result)
    {
        debugger;
        var apiPath = S2SwmsApiPath + 'S2SUserConfig/RemovedS2SUserConfig';

        var postData =
        {
            "objectName": "S2SOrder",
            "id": getId
        }
        callHttpCoreUrl(apiPath, postData, function (data) {
            debugger;
            var isStatus = data.statuscode;
            var getresult = data.status;
            if (isStatus == 200) {
                alert("User Removed Successfully.");
                //$("#plnUserNameS2S").val('');
                GetS2SUserConfigList();

            }
            else if (isStatus == 201) {
                alert("Error Occured.");
            }
        })
    }
    else
    {
        // User clicked Cancel
        //console.log("User Remove cancelled.");
    }
}



function validateUser() {
    var Username = $('#plnUserNameS2S').val();

    if (Username == '') {
        alert('Please Select Username!');
        return false;
    }
    else {
        return true;
    };
}