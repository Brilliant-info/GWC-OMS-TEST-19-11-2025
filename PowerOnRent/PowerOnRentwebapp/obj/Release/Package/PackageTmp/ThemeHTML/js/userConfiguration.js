
var recordlimit = 10;
var CurrentPage = 1;
var getConfigUserId = $('#hdnConfigUserID').val();
var searchfilter = 'ALL';
var searchvalue = 'none';

$(document).ready(function () {
    setGloablUserIdUserConfg();
    ddlCustomerList();
    //alert("Hello, world!");
    $("#ddluserCustomerList").change(function () {
        ddlDepartmentList()
    });
    UserConfiList();
});

function setGloablUserIdUserConfg() {
    getConfigUserId = $('#hdnConfigUserID').val();
}

function viewUserList() {
    UserConfigurationList();
    $("#wms-srv-UserList-popup").show();
    $("#wms-srv-UserList-popup-close").click(function () {
        $("#wms-srv-UserList-popup").off();
        $("#wms-srv-UserList-popup").hide();
        $("#txtUserSearch").val('');
    });
}


function ddlCustomerList() {
 
    var apiPath = wmsApiPath + 'CustomerList';

    var postData = {
        "UserId": getConfigUserId
    };
    callHttpUrl(apiPath, postData, function (data) {
        var isStatus = data.Status;
        if (isStatus == 200) {

            $("#ddluserCustomerList").html('');
            $("#ddluserCustomerList").html('<option value="">-- Select Customer --</option>');
            var myGridList = data.Result;
            var CustomerList = myGridList.Table;
            if (CustomerList.length > 0) {
                for (var i = 0; i < CustomerList.length; i++) {
                    var Id = CustomerList[i].Id;
                    var CustomerName = CustomerList[i].Name;
                    $("#ddluserCustomerList").append('<option value = "' + Id + '">' + CustomerName + '</option>');
                }
            }

        }
    });
}

function ddlDepartmentList() {
    var apiPath = wmsApiPath + 'getDepartmentList';
    var getCustomerId = $("#ddluserCustomerList").val();
    if (getCustomerId != "") {


        var postData = {
            "UserId": getConfigUserId,
            "CustomerId": getCustomerId
        }
        callHttpUrl(apiPath, postData, function (data) {
            var isStatus = data.Status;
            if (isStatus == 200) {
                $("#ddluserDepartment").html('');
                //$("#ddlDepartmentList").html('<option value="">-- Select Department --</option>');
                var myGridList = data.Result;
                var DepartmentList = myGridList.Table;
                if (DepartmentList.length > 0) {
                    for (var i = 0; i < DepartmentList.length; i++) {
                        var Id = DepartmentList[i].id;
                        var DepartmentName = DepartmentList[i].DepartmentName;
                        $("#ddluserDepartment").append('<option value = "' + Id + '">' + DepartmentName + '</option>');
                    }
                }
                if (getCustomerId == 0) {
                    $("#ddluserDepartment").html('<option value="">-- Select Department --</option>');
                }

            }
        });
    }
    else {
        clearDeptUserBox();
    }
}

function clearDeptUserBox() {
    $("#sethdnUserId").val("");
    $("#setUserName").val("");
    $("#ddluserDepartment").html('<option value="" onchange=validateCustomer();">-- Select Department --</option>');
}

function validateCustomer() {
    var CusId = $("#ddluserCustomerList").val();
    if (CusId =="") {
        alert("Please Select Customer");
    }
}

function clearHeadValues() {
    $("#ddluserCustomerList").val("");
    $("#ddluserDepartment").val("");
    $("#sethdnUserId").val("");
    $("#setUserName").val("");

    $("#ddluserDepartment").html('<option value="" onchange=validateCustomer();">-- Select Department --</option>');

}

function UserConfigurationList() {
    debugger;
    var apiPath = wmsApiPath + 'getUserConfiList';
    var searchVal = ''
    searchVal = $("#txtUserSearch").val();
    var getCustomerId = $("#ddluserCustomerList").val();
    var getDepartmentId = $("#ddluserDepartment").val();
    if (searchVal == "") {
        searchVal = "";
    }
    var postData = {
        "UserId": getConfigUserId,
        "CustomerId": getCustomerId,
        "DepartmentId": getDepartmentId,
        "SearchVal": searchVal
    }
    callHttpUrl(apiPath, postData, function (data) {
        var isStatus = data.Status;
        if (isStatus == 200) {
          
            $("#wms-srv-grid-UserListPopup").html('');
            var gridlist = '';
            gridlist = gridlist + '<div class="wms-srv-grid-header">';
            //gridlist = gridlist + '<div class="wms-srv-grid-cell wms-srv-align">Employee ID</div>';
            gridlist = gridlist + '<div class="wms-srv-grid-cell wms-srv-align">First Name</div>';
            gridlist = gridlist + '<div class="wms-srv-grid-cell wms-srv-align">Last Name</div>';
            gridlist = gridlist + '<div class="wms-srv-grid-cell wms-srv-align">User Type</div>';
            gridlist = gridlist + '<div class="wms-srv-grid-cell wms-srv-align">User Name</div>';
            //gridlist = gridlist + '<div class="wms-srv-grid-cell wms-srv-align">User Id</div>';
            gridlist = gridlist + '<div class="wms-srv-grid-cell wms-srv-align">Email Id</div>';
            gridlist = gridlist + '<div class="wms-srv-grid-cell wms-srv-align">Action</div>';
            gridlist = gridlist + '</div>';

            var myGridList = data.Result;
            var DataList = myGridList.Table;
            for (var i = 0; i < DataList.length; i++)
            {
                var getUId = DataList[i].UserID;
                var getUserName = DataList[i].UserName;
                var getFirstName = DataList[i].FirstName;
                var getLastName = DataList[i].LastName;
                var getEmployeeID = DataList[i].EmployeeID;
                var getUserType = DataList[i].UserType;
                var getUserEmailID = DataList[i].EmailID;
                
                gridlist = gridlist + '<div class="wms-srv-grid-row wms-srv-grid-add">';
                //gridlist = gridlist + '<div class="wms-srv-grid-cell">' + getEmployeeID+'</div>';
                gridlist = gridlist + '<div class="wms-srv-grid-cell">' + getFirstName + '</div>';
                gridlist = gridlist + '<div class="wms-srv-grid-cell">' + getLastName + '</div>';
                gridlist = gridlist + '<div class="wms-srv-grid-cell">' + getUserType+'</div>';
                gridlist = gridlist + '<div class="wms-srv-grid-cell">' + getUserName+'</div>';
                gridlist = gridlist + '<div class="wms-srv-grid-cell">' + getUserEmailID+'</div>';
                gridlist = gridlist + '<div class="wms-srv-grid-cell">';
                gridlist = gridlist + '<div class="wms-srv-grid-action"><a href="#" title="Save" class="wms-srv-save"><i class="fas fa-check-circle" onclick="setSelectedUser(\'' + getUId +'\',\'' + getFirstName+'\');"></i></a></div>';
                gridlist = gridlist + '</div>';
                gridlist = gridlist + '</div>';
            }
            $("#wms-srv-grid-UserListPopup").append(gridlist);

        }
    });
}

function setSelectedUser(userId, UserName) {

    $("#sethdnUserId").val(userId);
    $("#setUserName").val(UserName);
    $("#wms-srv-UserList-popup").hide();
    $("#txtUserSearch").val('');
}

function saveRMSUser() {
    if (validateSaveUser()) {

        var apiPath = wmsApiPath + 'SaveRMSUser';
        var getCustomerId = $("#ddluserCustomerList").val();
        var getDepartId = $("#ddluserDepartment").val();
        var SelectedUserId = $("#sethdnUserId").val();

        var postData = {
            "CustomerId": getCustomerId,
            "DepartmentId": getDepartId,
            "UserId": SelectedUserId,
            "CreatedBy": getConfigUserId
        }
        callHttpUrl(apiPath, postData, function (data) {
            var isStatus = data.StatusCode;
            var getResult = data.Result;
            if (isStatus == 200) {

                alert("User Successfully Registered");
                UserConfiList();
                $("#ddluserCustomerList").val('');
                $("#ddluserDepartment").val('');
                $("#sethdnUserId").val('');
                $("#setUserName").val('');
            }
            else if (getResult == 'UserAlreadyAvilable')
            {
                alert("User Already Available in the System..!!");
            }
            else {
                alert("Failed To Register..!!");
            }
        });
    }
}

function validateSaveUser() {
    var getUserCustList = $("#ddluserCustomerList").val();
    var getUserDeptList = $("#txtAddReturnDate").val();
    var checkhdnValiId = $("#sethdnUserId").val();
    var checkUserName = $("#setUserName").val();
    if (getUserCustList == '' && getUserCustList == 0) {
        alert("Please Select Valid Customer");
        return false;
    }
    else if (getUserDeptList == '' && getUserDeptList == 0) {
        alert("Please Select Valid Department");
        return false;
    }
    else if (checkUserName == '') {
        alert("Please Select Valid User");
        return false;
    }
    else if (checkhdnValiId == '') {
        alert("Please Select Valid User");
        return false;    }
    else {
        return true;
    }
 
}

function UserConfiList() {
    var apiPath = wmsApiPath + 'getGridUserList';
    var getCustomerId = $("#ddluserCustomerList").val();
    var getDepartId = $("#ddluserDepartment").val();
    var getSearchVal = $("#txtsearchRMSuser").val();
    if (getSearchVal == '')
    {
        getSearchVal = "";
    }

    var postData = {
        "UserId": getConfigUserId,
        "Search": getSearchVal,
        "recordlimit": recordlimit,
        "CurrentPage": CurrentPage

    }
    callHttpUrl(apiPath, postData, function (data) {
        var isStatus = data.Status;
        if (isStatus == 200) {
            $("#gridUserConfiList").html('');
            var myGridList = data.Result;
            var checkData = myGridList.Table1.length;
            var TotalRecords = myGridList.Table[0].TotalRecord;;
            if (checkData > 0) {

  
            var getRecords = myGridList.Table1;
            
            var gridList = '';
            gridList = gridList+'<div class="wms-srv-grid-header">';
            gridList = gridList + '<div class="wms-srv-grid-cell">Customer</div>';
            gridList = gridList + '<div class="wms-srv-grid-cell">Department Name</div>';
            gridList = gridList + '<div class="wms-srv-grid-cell">User Name</div>';
            gridList = gridList + '<div class="wms-srv-grid-cell">User Type</div>';
            gridList = gridList + '<div class="wms-srv-grid-cell">Active</div>';
            gridList = gridList + '<div class="wms-srv-grid-cell" style="width:14%;">Action</div>';
            gridList = gridList + '</div>';

            var DataList = myGridList.Table1;

            for (var i = 0; i < DataList.length;i++)
            {
                
                var getRmsUserId = DataList[i].RMSUserId;
                var getLoginUserId = DataList[i].UserId;
                var getrownumber = DataList[i].rownumber;
                var getCustomerId = DataList[i].CustomerId;
                var getCustomerName = DataList[i].CustomerName;
                var getDepatmentId = DataList[i].DepatmentId;
                var getDepartmentName = DataList[i].DepartmentName;
                var getUsername = DataList[i].Username;
                var getUserType = DataList[i].UserType;
                var getActive = DataList[i].Active;
                
            gridList = gridList + '<div class="wms-srv-grid-row">';
            gridList = gridList + '<div class="wms-srv-grid-cell" style="text-align:center;">' + getCustomerName+'</div>';
            gridList = gridList + '<div class="wms-srv-grid-cell" style="text-align:center;">' + getDepartmentName+'</div>';
            gridList = gridList + '<div class="wms-srv-grid-cell" style="text-align:center;">' + getUsername + '</div>';
            gridList = gridList + '<div class="wms-srv-grid-cell" style="text-align:center;">' + getUserType + '</div>';
                if (getActive == 'YES') {
                    gridList = gridList + '<div class="wms-srv-grid-cell" style="text-align:center;">' + getActive+'</div>';
                }
                else {
                    gridList = gridList + '<div class="wms-srv-grid-cell" style="text-align:center;">' + getActive+'</div>';
                }
            gridList = gridList + '<div class="wms-srv-grid-cell">';
                gridList = gridList + '<div class="wms-srv-grid-action" style="margin-bottom: -20px;">';
                if (getActive == 'YES')
                {
                    gridList = gridList + '<label class="switch"><input type="checkbox" checked onclick="activeUser(' + getRmsUserId + ',' + getCustomerId + ',' + getDepatmentId +');" id="checkUserActiveVal' + getRmsUserId + '"/><span class="slider round"></span></label>';
                }
                else {
                    gridList = gridList + '<label class="switch"><input type="checkbox" onclick="activeUser(' + getRmsUserId + ',' + getCustomerId + ',' + getDepatmentId+');" id="checkUserActiveVal' + getRmsUserId + '"/><span class="slider round"></span></label>';
                }
           
            gridList = gridList + '<div class="wms-srv-action-sep"> | </div>';
                gridList = gridList + '<i class="fas fa-times-circle iconView" onclick=RemoveRMSUser(' + getRmsUserId + ',' + getCustomerId + ',' + getDepatmentId+') title="Remove User"></i>';
            gridList = gridList + '</div>';
            gridList = gridList + '</div>';
            gridList = gridList + '</div>';
            }

            $("#gridUserConfiList").append(gridList);

            }
            setupGridPagingListUser('pgUserConfigUserList', CurrentPage, TotalRecords, UserConfiList)

        }
    });
}


function activeUser(getUID,CustId,DeptId) {
    var activeVal = $("#checkUserActiveVal" + getUID).prop('checked');
  
    if (activeVal == true) {
        activeVal = "YES"
    }
    else {
        activeVal = "NO"
    }

    var apiPath = wmsApiPath + 'activeUser';
    var postData = {
        "UserId": getUID,
        "CustomerId": CustId,
        "DepartmentId": DeptId,
        "Active": activeVal
    }
    callHttpUrl(apiPath, postData, function (data) {
        var getStatusCode = data.Result;
        var isStatus = data.StatusCode;
        if (isStatus == 200 && getStatusCode == 'Success') {
            var myGridList = data.Result;
            var getRecords = myGridList.Table;

            alert("User Successfully Activated");
            UserConfiList();
        }
        else if (isStatus == 200 && getStatusCode == 'update')
        {
            alert("User Successfully Deactivated");
            UserConfiList();

        }
        
        else {
            alert("unable to connect server..!!");
        }
    });
}

function RemoveRMSUser(UserId, compId, DeptId) {
    var apiPath = wmsApiPath + 'RemoveRMSUser';
    var postData = {
        "UserId": UserId,
        "CustomerId": compId,
        "DepartmentId": DeptId
    }
    callHttpUrl(apiPath, postData, function (data) {
        var getStatusCode = data.Result;
        var isStatus = data.StatusCode;
        if (isStatus == 200 && getStatusCode == 'Success') {
            alert("User Remove Successfully");
            UserConfiList();
        }
        else {
            alert("Unable To connect With Server..!!");
        }
    })
}



function setupGridPagingListUser(gridObjId, strCurrentPage, strTotalRecords, callBackFunction) {
    debugger;
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




