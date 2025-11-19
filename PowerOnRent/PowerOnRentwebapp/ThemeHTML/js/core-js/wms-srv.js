(function ($) {
    $.fn.hasScrollBar = function () {
        return this.get(0).scrollHeight > this.height();
    }
})(jQuery);

$(document).ready(function () {

});

$(document).ready(function () {
    //adjustGrid();
    activateResponsiveGrid();
    bindControlEvent();
    init();
});
$(document).mouseup(function () {
    $('.wms-srv-grid-more-navbar').hide();
    $('#wmsSrvActionPopupMenu').hide();
});

function activateResponsiveGrid() {
    $(window).resize(function () {
        var popupGridSpace = 60;
        var getWidth = $(window).width();
        var getHeight = $(window).height();

        var adjWidth = 0;
        var adjheight = 0;
        var adjPopupWidth = 0;
        var adjPopupheight = 0;

        var finalGridWidth = '';
        var finalGridHeight = '';
        var finalPopupGridWidth = '';
        var finalPopupGridHeight = '';

        if (getWidth <= 767.98) {
            adjWidth = getWidth - 65;
            adjPopupWidth = adjWidth - 74;
        } else {
            adjWidth = getWidth - 100;
            adjPopupWidth = adjWidth - 39;
        }
        adjheight = getHeight - 30;

        finalGridWidth = adjWidth + 'px';
        finalGridHeight = adjheight + 'px';

        $('.wms-srv-grid-scroller').css("width", finalGridWidth);
        $('.wms-srv-grid-scroller').css("height", finalGridHeight);

        // FOR POPUP GRID


        finalPopupGridWidth = adjPopupWidth + 'px';
        $('.wms-srv-popup-holder .wms-srv-grid-scroller').css("width", finalPopupGridWidth);
        $('.wms-srv-popup-holder .wms-srv-grid-scroller').css("height", finalGridHeight);
        removeNotApplicableScroll();

    });
    $(window).resize();
}

function removeNotApplicableScroll() {
    $('.wms-srv-popup-holder .wms-srv-grid-scroller').each(function () {
        var checkIfScroll = $(this).hasScrollBar();
        if (!checkIfScroll) {
            $(this).css('height', 'auto');
        }
    });
}

$(window).resize(function () {
    //	adjustGrid();
});

function adjustGrid() {
    // $('.wms-srv-grid-scroller .wms-srv-grid').hide();
    // $('.wms-srv-grid-scroller').attr('style','width:auto;');
    // var getWidth = $('.wms-srv-grid-scroller').width();
    // $('.wms-srv-grid-scroller').attr('style', 'width:' + getWidth + 'px');
    // setTimeout(function(){
    // 	$('.wms-srv-grid-scroller .wms-srv-grid').show();
    // 	}, 1000); 
}
/*
function init(){
//	$.get( "content-template/nav.html", function( data ) {
//		$( "#themeWMSNavPanel" ).html( data );
        setupNavigation();
        bindControlEvent();
        setupControls();
        //gridbind();
        $(function () {
          $('[data-toggle="tooltip"]').tooltip()
        })
        bindWmsPageData("1","");
        loadPageWiseEvent();
    	
        // Temp popup code...
        showWmsSrvPopup('document');
    //});
}
*/

function bindControlEvent() {
    $('#wms-srv-popup-close').click(function () {
        $('#wms-srv-popup').hide();
    });
}

function showWmsSrvPopup(popTitle, popIcon, popTemplate, popObjFunction) {
    var popupTitleObj = $('#wms-srv-popup .wms-srv-popup-title span');
    var popupTitleIcon = $('#wms-srv-popup .wms-srv-page-title i');
    var popupContentTemplate = '';
    $(popupTitleObj).html('Document');
    $(popupTitleIcon).attr('class', 'fas fa-file-alt');
    popupContentTemplate = 'content-template/document-template.html';
    $.get(popupContentTemplate, function (data) {
        $("#popupContentDocument").html(data);
        $('#wms-srv-popup').show();
        let myGrid = new wmsDataGrid("pnlDocument", "223");
        myGrid.loadData("0", "");
    });
}

function showWmsSrvPopup_backup(popupType) {
    var popupTitleObj = $('#wms-srv-popup .wms-srv-popup-title span');
    var popupTitleIcon = $('#wms-srv-popup .wms-srv-page-title i');
    var popupContentTemplate = '';
    if (popupType == 'communication') {
        $(popupTitleObj).html('Correspondence');
        $(popupTitleIcon).attr('class', 'fas fa-envelope');
        popupContentTemplate = 'content-template/communication-template.html';
    } else if (popupType == 'document') {
        $(popupTitleObj).html('Document');
        $(popupTitleIcon).attr('class', 'fas fa-file-alt');
        popupContentTemplate = 'content-template/document-template.html';
    } else if (popupType == 'approval') {
        $(popupTitleObj).html('Approval');
        $(popupTitleIcon).attr('class', 'fas fa-user-check');
        popupContentTemplate = 'content-template/approval-template.html';
    } else if (popupType == 'customizegrid') {
        $(popupTitleObj).html('Customize Grid');
        $(popupTitleIcon).attr('class', 'fas fa-th-list');
        //popupContentTemplate = 'content-template/customize-grid.html';
        popupContentTemplate = 'content-template/calendar-template.html';
    }
    $.get(popupContentTemplate, function (data) {
        $("#popupContentDocument").html(data);
        $('#wms-srv-popup').show();
        if (popupType == 'customizegrid') {
            $("#popupContentDocument ul.droptrue").sortable({
                connectWith: "ul"
            });

            $("#popupContentDocument ul.dropfalse").sortable({
                connectWith: "ul"
            });
        } else {
            $('#popupContentDocument .wms-srv-more-btn-holder button').click(function () {
                $(this).parent().find('.wms-srv-grid-more-navbar').show();
            });
            $('#popupContentDocument .wms-srv-grid-more-navbar a').click(function () {
                $(this).parent().hide();
            });
        }
    });
    let myGrid = new wmsDataGrid("pnlDocument", "223");
    myGrid.loadData("0", "");
}

function setupNavigation() {
    $('.themeWMSNav li a').click(function () {
        $('.themeWMSNav  li').removeClass('themeWMSActiveNav');
        var getParent = $(this).parent();
        var getParentOfParent = $(this).parent().parent().parent();
        if (getParent[0].nodeName.toLowerCase() == "li") {
            $(this).parent().addClass("themeWMSActiveNav");
        }
        if (getParentOfParent[0].nodeName.toLowerCase() == "li") {
            $(this).parent().parent().parent().addClass("themeWMSActiveNav");
        }
        var getChildLength = $(this).parent().children("ul").length;
        //saveThemeNavStatus();
        if (getChildLength > 0) {
            var firstUl = $(this).parent().find("ul").find("li").length;
            if (firstUl > 0) {
                //showThemeWMSNav();
                event.stopImmediatePropagation();
                return false;
            } else {
                return true;
            }

        } else {
            return true;
        }
    });
}

//================== RESPONSIVE PopUP GRID HEAD===============================//
function prepareGrid(objId, marginVal) {
    var headArray = [];
    var headValueArray = [];
    $('#' + objId + ' .wms-adjust-grid .wms-srv-grid-header .wms-srv-grid-cell').each(function () {
        var getHeaderLabel = $(this);
        headArray.push(getHeaderLabel);
    });
    $('#' + objId + ' .wms-adjust-grid .wms-srv-grid-row .wms-srv-grid-cell').each(function () {
        var getHeaderValue = $(this);
        headValueArray.push(getHeaderValue);
    });
    $(window).resize(function () {
        reAdjustGrid(objId, headArray, headValueArray, marginVal);
    });
    reAdjustGrid(objId, headArray, headValueArray, marginVal);
}

function reAdjustGrid(objId, headArray, headValueArray, marginVal) {
    var constructHtml = '';
    var getWindowWidth = $(window).width();
    var getHeadGridWidth = getWindowWidth - (marginVal * 2);
    $('#' + objId + ' .wms-adjust-grid').css('min-width', getHeadGridWidth + 'px');
    $('#' + objId + ' .wms-adjust-grid').css('width', getHeadGridWidth + 'px');

    var createGridRow = [];
    var dRC = 0;
    if (getWindowWidth < 1025 && getWindowWidth > 768) {
        for (var i = 0; i < headArray.length; i++) {
            createGridRow[dRC] = $('<div class="wms-srv-grid-row"></div>');
            $(headArray[i]).attr('style', 'background-color: #efefef; !important;font-weight:bold;');
            $(createGridRow[dRC]).append(headArray[i]);
            $(createGridRow[dRC]).append(headValueArray[i]);
            i = i + 1;
            $(headArray[i]).attr('style', 'background-color: #efefef; !important;font-weight:bold;');
            $(createGridRow[dRC]).append(headArray[i]);
            $(createGridRow[dRC]).append(headValueArray[i]);
            i = i + 1;
            $(headArray[i]).attr('style', 'background-color: #efefef; !important;font-weight:bold;');
            $(createGridRow[dRC]).append(headArray[i]);
            $(createGridRow[dRC]).append(headValueArray[i]);
            $('#' + objId + ' .wms-adjust-grid').append(createGridRow[dRC]);
            dRC = dRC + 1;
        }
    } else if (getWindowWidth < 769 && getWindowWidth > 480) {
        for (var i = 0; i < headArray.length; i++) {
            createGridRow[dRC] = $('<div class="wms-srv-grid-row"></div>');
            $(headArray[i]).attr('style', 'background-color: #efefef; !important;font-weight:bold;');
            $(createGridRow[dRC]).append(headArray[i]);
            $(createGridRow[dRC]).append(headValueArray[i]);
            i = i + 1;
            $(headArray[i]).attr('style', 'background-color: #efefef; !important;font-weight:bold;');
            $(createGridRow[dRC]).append(headArray[i]);
            $(createGridRow[dRC]).append(headValueArray[i]);
            $('#' + objId + ' .wms-adjust-grid').append(createGridRow[dRC]);
            dRC = dRC + 1;
        }
    } else if (getWindowWidth < 481) {
        for (var i = 0; i < headArray.length; i++) {
            createGridRow[dRC] = $('<div class="wms-srv-grid-row"></div>');
            $(headArray[i]).attr('style', 'background-color: #efefef; !important;font-weight:bold;');
            $(createGridRow[dRC]).append(headArray[i]);
            $(createGridRow[dRC]).append(headValueArray[i]);
            $('#' + objId + ' .wms-adjust-grid').append(createGridRow[dRC]);
            dRC = dRC + 1;
        }
    } else {
        var createGridHeaderRow = $('<div class="wms-srv-grid-header"></div>');
        for (var i = 0; i < headArray.length; i++) {
            $(createGridHeaderRow).append(headArray[i]);
        }
        var createGridRow = $('<div class="wms-srv-grid-row"></div>');
        for (var i = 0; i < headArray.length; i++) {
            $(headArray[i]).attr('style', '');
            $(createGridRow).append(headValueArray[i]);
        }
        $('#' + objId + ' .wms-adjust-grid').append(createGridHeaderRow);
        $('#' + objId + ' .wms-adjust-grid').append(createGridRow);
    }
}
//================== RESPONSIVE PopUP GRID HEAD===============================//

//================== Dropdown Binding Function===============================//
function BindDropDown() {
    debugger;
    var getUserId = mBrillWmsSession.getUserId();
    var apiPath = wmsApiPath + 'Dashboard/GetDropdownList';
    var postData = {
        UserId: getUserId,
    };

    callHttpUrl(apiPath, postData, function (data) {
        debugger;
        var isStatus = data.Status;
        if (isStatus == 200) {
            var myGridList = data.Result;
            var customer = myGridList.Table;
            if (customer.length > 0) {
                for (var i = 0; i < customer.length; i++) {
                    var CustId = customer[i].ID;
                    var custname = customer[i].Name;
                    $("#ddlcustomer").append('<option value = "' + CustId + '">' + custname + '</option>');
                }
                $("#ddlcustomer").off();
                $("#ddlcustomer").change(function () {
                    var getName = $(this).find('option:selected').text();
                    var getNameId = $(this).val();
                    mBrillWmsSession.setCustomerName(getName);
                    mBrillWmsSession.setCustomerId(getNameId);
                    /*  if(wmsAppDefaultPageFunction != null){
                          wmsAppDefaultPageFunction();
                      } */
                   // wmsNavigatePage(wmsAppPage);
                   BindWarehouseList(getNameId);
                });

                var getName = $("#ddlcustomer").find('option:selected').text();
                    var getNameId = $("#ddlcustomer").val();
                    mBrillWmsSession.setCustomerName(getName);
                    mBrillWmsSession.setCustomerId(getNameId);
                    BindWarehouseList(getNameId);
               // var getDefaultCustomer = mBrillWmsSession.getCustomerId();
                //$("#ddlcustomer").val(getDefaultCustomer);
            }
            //BindWarehouseList();
            // var custId = $("#ddlcustomer").val();
            // if(custId!="")
            // {
            //     BindWarehouseList(custId);
            // }
			// var warehouse = myGridList.Table1;
			// if (warehouse.length > 0) {
			// 	for (var i = 0; i < warehouse.length; i++) {
			// 		var wrId = warehouse[i].ID;
			// 		var wrname = warehouse[i].WarehouseName;
			// 		$("#ddlwarehouse").append('<option value = "' + wrId + '">' + wrname + '</option>');
			// 	}
            //     $("#ddlwarehouse").off();
            //     $("#ddlwarehouse").change(function(){
            //         var getName = $(this).find('option:selected').text();
            //         var getNameId = $(this).val();
            //         mBrillWmsSession.setWarehouseName(getName);
            //         mBrillWmsSession.setWarehouseId(getNameId);
            //         /* if(wmsAppDefaultPageFunction != null){
            //             wmsAppDefaultPageFunction();
            //         }*/
            //         wmsNavigatePage(wmsAppPage);
            //     });
            //     var getDefaultWarehouse = mBrillWmsSession.getWarehouseId();
            //     $("#ddlwarehouse").val(getDefaultWarehouse);
			// }

            var warehouse = myGridList.Table1;
            if (warehouse.length > 0) {
                for (var i = 0; i < warehouse.length; i++) {
                    var wrId = warehouse[i].ID;
                    var wrname = warehouse[i].WarehouseName;
                    $("#ddlwarehouse").append('<option value = "' + wrId + '">' + wrname + '</option>');
                }
                $("#ddlwarehouse").off();
                $("#ddlwarehouse").change(function () {
                    var getName = $("#ddlwarehouse").find('option:selected').text();
                    var getNameId = $("#ddlwarehouse").val();
                    mBrillWmsSession.setWarehouseName(getName);
                    mBrillWmsSession.setWarehouseId(getNameId);
                    /* if(wmsAppDefaultPageFunction != null){
                        wmsAppDefaultPageFunction();
                    }*/
                    wmsNavigatePage(wmsAppPage);
                });
                var getDefaultWarehouse = mBrillWmsSession.getWarehouseId();
                $("#ddlwarehouse").val(getDefaultWarehouse);
            }

            var client = myGridList.Table2;
            $("#ddlclient").html('<option value = "0"> -- Select All -- </option>');
            if (client.length > 0) {
                for (var i = 0; i < client.length; i++) {
                    var clId = client[i].ID;
                    var clname = client[i].Name;
                    $("#ddlclient").append('<option value = "' + clId + '">' + clname + '</option>');
                }
                $("#ddlclient").off();
                $("#ddlclient").change(function () {
                    var getName = $(this).find('option:selected').text();
                    var getNameId = $(this).val();
                    mBrillWmsSession.setClientName(getName);
                    mBrillWmsSession.setClientId(getNameId);
                    /* if(wmsAppDefaultPageFunction != null){
                        wmsAppDefaultPageFunction();
                    } */
                    wmsNavigatePage(wmsAppPage);
                });
                var getDefaultClient = mBrillWmsSession.getClientId();
                //$("#ddlclient").val(getDefaultClient);
                $("#ddlclient").val('0');
                mBrillWmsSession.setClientName('');
                mBrillWmsSession.setClientId('0');
            }
        }
    });
}

function BindWarehouseList(custId)
{
    debugger;
    var getUserId = mBrillWmsSession.getUserId();
    var getDefaultCustomer = mBrillWmsSession.getCustomerId();

	var apiPath = wmsApiPath + 'Dashboard/warehouselist';
   // var apiPath = 'http://localhost:50068/api/staging/v1/Dashboard/warehouselist';
	
    var postData = {
		UserId: getUserId,        
        CustomerId:getDefaultCustomer
      
	};
    callHttpUrl(apiPath, postData, function (data) {
        debugger;
		var isStatus = data.Status;
		if (isStatus == 200) {
            $("#ddlwarehouse").html('');
			var myGridList = data.Result;
            var warehouse = myGridList.Table;
			if (warehouse.length > 0) {
				for (var i = 0; i < warehouse.length; i++) {
					var wrId = warehouse[i].WarehouseId;
					var wrname = warehouse[i].WarehouseName;
					$("#ddlwarehouse").append('<option value = "' + wrId + '">' + wrname + '</option>');
				}
                    mBrillWmsSession.setWarehouseName(wrname);
                    mBrillWmsSession.setWarehouseId(wrId);
                $("#ddlwarehouse").off();
                $("#ddlwarehouse").change(function(){
                    var getName = $(this).find('option:selected').text();
                    var getNameId = $(this).val();
                    mBrillWmsSession.setWarehouseName(getName);
                    mBrillWmsSession.setWarehouseId(getNameId);
                    /* if(wmsAppDefaultPageFunction != null){
                        wmsAppDefaultPageFunction();
                    }*/
                    wmsNavigatePage(wmsAppPage);
                });
                var getName = $("#ddlwarehouse").find('option:selected').text();
                    var getNameId = $("#ddlwarehouse").val();
                    mBrillWmsSession.setWarehouseName(getName);
                    mBrillWmsSession.setWarehouseId(getNameId);
                    wmsNavigatePage(wmsAppPage);


                //var getDefaultWarehouse = mBrillWmsSession.getWarehouseId();
                //alert(getName);
                 //$("#ddlwarehouse").val(getDefaultWarehouse);
                // $("#ddlwarehouse").html(getDefaultWarehouse);
			}
        }
    });
}



//================== Dropdown Binding Function===============================//


//================== E-mail and Phone Number Validation Function ============//
function validateEmailAddress(emailVal) {
    var isEmailValid = true;
    var mailformat = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;

    if (emailVal.match(mailformat)) {
        isEmailValid = true;
    }
    else {
        isEmailValid = false;
    }
    return isEmailValid;
}

function validatePhoneNo(phoneNoVal) {
    var phonePattern = /^\d{10}$/;
    if (phoneNoVal.match(phonePattern)) {
        return true;
    }
    else {
        return false;
    }
}

/*
 VALIDATE WITH COUNTRY PREFIX
 Example
    +XX-XXXX-XXXX
    +XX.XXXX.XXXX
    +XX XXXX XXXX
 */

function validatePhoneWithCountryPrefix(phoneNoVal) {
    var phonePattern = /^\+?([0-9]{2})\)?[-. ]?([0-9]{4})[-. ]?([0-9]{4})$/;
    if (phoneNoVal.match(phonePattern)) {
        return true;
    } else {
        return false;
    }
}

/*
 VALIDATE WITHOUT COUNTRY PREFIX
 Example
    XXX-XXX-XXXX
    XXX.XXX.XXXX
    XXX XXX XXXX
 */
function validatePhoneWithoutPrefix(phoneNoVal) {
    var phoneno = /^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$/;
    if (phoneNoVal.match(phoneno)) {
        return true;
    } else {
        alert("message");
        return false;
    }
}

function validateIsNumber(num) {
    var isNumber = true;
    var chkNum = Number.isNaN(Number(num) / 1);
    if (chkNum) {
        isNumber = false;
    } else {
        var roundNo = Math.round(num);
        var chkNo = Number(num);
        if (roundNo != chkNo) {
            isNumber = false;
        }
    }
    return isNumber;
}

function validateIsFloatNumber(num) {
    var isFloatNumber = true;
    var chkNum = Number.isNaN(Number(num) / 1);
    if (chkNum) {
        isFloatNumber = false;
    } else {
        var strNum = num.toString();
        if (strNum.indexOf('.') > -1) {
            var breakDot = strNum.split('.');
            if (breakDot.length == 2) {
                isFloatNumber = true;
            } else {
                isFloatNumber = false;
            }
        } else {
            isFloatNumber = false;
        }
    }
    return isFloatNumber;
}
//================== E-mail and Phone Number Validation Function ============//

function changePassword() {
    $('#Reset-Password-popup').show();
    $('#Reset-Password-popup-close').off();
    $('#Reset-Password-popup-close').click(function () {
        $('#Reset-Password-popup').hide();
    });
    $('#txtCurrentPassword').val('');
    $('#txtNewPassword').val('');
    $('#txtConfirmPassword').val('');
    $('#userProfileMenu').hide();
}
function logoutWms() {
    sessionStorage.clear();
    location.href = 'Public/Login.html';
}
function showHideProfileMenu() {
    $('#userProfileMenu').toggle();
    //$('#userProfileMenu a').off();
    /* $('#dropDownQuickLinks a').click(function(){
         $('#dropDownQuickLinks').toggle();
    }); */
}

function closeProfileMenu() {
    $('#userProfileMenu').hide();
}
function showHideQuickLinks() {
    $('#dropDownQuickLinks').toggle();
    //$('#dropDownQuickLinks a').off();
    /* $('#dropDownQuickLinks a').click(function(){
         $('#dropDownQuickLinks').toggle();
    }); */
}

function addQuickOrder() {
    $('#wms-srv-quickorder-popup').show();
    $('#wms-srv-quickorder-popup-close').off();
    $('#wms-srv-quickorder-popup-close').click(function () {
        $('#wms-srv-quickorder-popup').hide();
    });
    $('#dropDownQuickLinks').toggle();
}

function addQuickSoOrder() {
    $('#wms-srv-quickSoOrder-popup').show();
    $('#wms-srv-quickSoOrder-popup-close').off();
    $('#wms-srv-quickSoOrder-popup-close').click(function () {
        $('#wms-srv-quickSoOrder-popup').hide();
    });
    $('#dropDownQuickLinks').toggle();
}

$(document).ready(function () {
    $("#wmsThemeAppClientSearch").click(function () {
        $("#client_search").toggle();
    });
    $("#wmsThemeAppWarehouseSearch").click(function () {
        $("#warehouse_search").toggle();
    });
    $("#wmsThemeAppSelectedCustomer").click(function () {
        $("#customer_search").toggle();
    });
});

function showHidePagingGroup(gridObjId) {
    var pagingGroup = $('#' + gridObjId + ' .wms-srv-pager-links .wms-paging-link[data-page="' + CurrentPage + '"]').attr('data-group');
    $('#' + gridObjId + ' .wms-srv-pager-links .wms-paging-link').hide();
    $('[data-group="' + pagingGroup + '"]').show();
    $('#' + gridObjId + ' .wms-srv-pager-links .wms-first-paging-yes').show();
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
            showHidePagingGroup(gridObjId);
            if (callBackFunction != null) {
                callBackFunction(getDataPage, searchfilter, searchvalue);
            }
        }
        return false;
    });
    showHidePagingGroup(gridObjId);
}

function setupGridPagingList_old_backup(gridObjId, strCurrentPage, strTotalRecords, callBackFunction) { //Changed Respective ID 
    var global_max_record_count = 5;
    var pageNo = Number(strCurrentPage);
    var recordFrom = ((pageNo - 1) * 5) + 1;
    var recordTo = recordFrom + 4;
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
    while (currentCounter < totalRecord) {
        if (lnkCounter == pageNo) {
            pagerLinks += '<a href="#" class="wms-paging-link wms-first-paging-' + isFirstGroupLink + ' wms-srv-active" data-page="' + lnkCounter + '" data-group="PagingGroup_' + pagingGroup + '">' + lnkCounter + '</a>';
        } else {
            pagerLinks += '<a href="#" class="wms-paging-link wms-first-paging-' + isFirstGroupLink + '" data-page="' + lnkCounter + '" data-group="PagingGroup_' + pagingGroup + '">' + lnkCounter + '</a>';
            lastPage = lnkCounter;
        }
        global_last_page_no = lnkCounter;
        currentCounter = currentCounter + global_max_record_count;
        // Group Counter 
        isFirstGroupLink = 'no';
        pagingGroupCounter = pagingGroupCounter + 1;
        if (pagingGroupCounter > global_max_record_count) {
            pagingGroup = pagingGroup + 1;
            pagingGroupCounter = 1;
            isFirstGroupLink = 'yes';
            pagerLinks += '<a href="#" style="padding:0px 0px 0px 0px;  ">...</a>';
        }
        // Group Counter
        lnkCounter = lnkCounter + 1;
    }

    // Add Page link for remaining record 
    // if (currentCounter > totalRecord) {
    if (totalRecord <= currentCounter) {
        if (lnkCounter == pageNo) {
            pagerLinks += '<a href="#" class="wms-paging-link wms-first-paging-' + isFirstGroupLink + ' wms-srv-active" data-page="' + lnkCounter + '" data-group="PagingGroup_' + pagingGroup + '">' + lnkCounter + '</a>';
        } else {
            pagerLinks += '<a href="#" class="wms-paging-link wms-first-paging-' + isFirstGroupLink + '" data-page="' + lnkCounter + '" data-group="PagingGroup_' + pagingGroup + '">' + lnkCounter + '</a>';
            lastPage = lnkCounter;
        }
        this.global_last_page_no = lnkCounter;
    }
    // Add Page link for remaining record
    var pagerNavLinks = '';
    pagerNavLinks = '<a href="#" data-page="1"><i class="fas fa-angle-double-left"></i></a>';
    pagerNavLinks += pagerLinks;
    pagerNavLinks += '<a href="#" data-page="' + lastPage + '"><i class="fas fa-angle-double-right"></i></a>';

    $('#' + gridObjId + ' .wms-srv-pager-links').html(pagerNavLinks);

    $('#' + gridObjId + ' .wms-srv-pager-links a').off();
    $('#' + gridObjId + ' .wms-srv-pager-links a').click(function () {
        var getDataPage = $(this).attr('data-page');
        CurrentPage = getDataPage;
        showHidePagingGroup(gridObjId);
        if (callBackFunction != null) {
            callBackFunction(getDataPage, searchfilter, searchvalue);
        }
        return false;
    });

    $('#' + gridObjId + ' a.wms-srv-pager-go').off();
    $('#' + gridObjId + ' a.wms-srv-pager-go').click(function () {
        var getDataPage = $('#' + gridObjId + ' input[name="txtGridPageNo"]').val();
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

function ValidateReset() {
    debugger;
    var getCurrentPassword = $("#txtCurrentPassword").val();
    var getNewPassword = $("#txtNewPassword").val();
    var getConfirmPassword = $("#txtConfirmPassword").val();
    var passwordPattern = /^(?=.*[0-9])(?=.*[!@#$%^&*])[a-zA-Z0-9!@#$%^&*]{7,15}$/;

    if (getCurrentPassword == "") {
        alert("Current password should not be blank!");
    }
    else if (getNewPassword == "") {
        alert("New password should not be blank");
    }
    else if (getNewPassword.length < 6) {
        alert("Passwords must be at least 8 characters!");
    }
    else if (getNewPassword.length > 50) {
        alert("Password should not be more than 50 characters!");
    }
    else if (getNewPassword.match(passwordPattern) == null) {
        alert('Your password should contain at least one capital letter, one special character and one number. ');
        return true;
    }
    else if (getConfirmPassword == "") {
        alert("Confirm Password can't be blank!");
    }
    else if (getConfirmPassword != getConfirmPassword) {
        alert("Confirm Password Not Match!");
    }
    else {
        ResetPassword();
    }
}

function ResetPassword() {
    debugger;
    var apiPath = wmsApiPath + 'loginpage/ResetPassword';

    var getCurrentPassword = $("#txtCurrentPassword").val();
    var getNewPassword = $("#txtNewPassword").val();
    var getLoginUserName = mBrillWmsSession.getLoginUserName();
    var getUserId = mBrillWmsSession.getUserId();

    var postData =
    {
        "UserName": getLoginUserName,
        "OldPassword": getCurrentPassword,
        "NewPassword": getNewPassword,
        "UserID": getUserId
    }

    callHttpUrl(apiPath, postData, function (data) {
        var isSuccess = data.Status;
        var getpassresult = data.Result;
        var returnmsg = getpassresult.Message;
        if (isSuccess == 200) {
            $('#Reset-Password-popup').hide();
            alert('Password chnaged Successfully!');
        }
        else if (isSuccess == 300) {
            alert(returnmsg);
        }
        else {
            alert("Error Occured");
        }
    });

}


