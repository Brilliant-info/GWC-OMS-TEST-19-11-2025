var stringDataType = ['char', 'varchar', 'text', 'nchar', 'nvarchar', 'ntext', 'binary', 'varbinary', 'image'];
var numberDataType = ['bit', 'tinyint', 'smallint', 'int', 'bigint', 'decimal', 'numeric', 'float', 'smallmoney', 'money', 'real'];
var dateTimeDataType = ['datetime', 'datetime2', 'smalldatetime', 'date', 'time', 'datetimeoffset', 'timestamp'];
var queryNumericOperators = ['=', '<', '<=', '>', '>='];
var queryStringOperators = ['LIKE', 'NOT LIKE'];

var sqlTableName = '';
var sqlCurrentColumnName = '';
var sqlCurrentColumnType = '';
var sqlFinalQuery = '';
var sqlConditionGroupCounter = 0;
var columnCounter = 1;

function resetQueryVariables() {
    sqlTableName = '';
    sqlCurrentColumnName = '';
    sqlCurrentColumnType = '';
    sqlFinalQuery = '';
    sqlConditionGroupCounter = 0;
    resetQueryUI();
    hdnImportID = '';
}

function resetQueryUI() {
    $('#dataConditionHolder').empty();
    $('#winQueryConditionHeader').html('Condition: ');
    $('#txtSqlConditionColumn').empty();
    $('#dataColumnHolder').empty();
    $('#hdnSQLQueryText').val('');
    $('#lblSQLQueryText').html('');
    $('#txtSqlCondition').val('=');
    $('#txtSqlConditionValue').val('');
    $('#hdnObjectName').val('');
}

function loadQueryColumns(result, sqlTableName, cacheCurrentSQL) {
    var arrSqlColumns = result.split("|");
    for (rc = 0; rc <= (arrSqlColumns.length - 1); rc++) {
        var arrColumnDetails = arrSqlColumns[rc].split(":");
        var getColumnName = arrColumnDetails[0];
        var getDataType = arrColumnDetails[1];

        addQueryColumns(getColumnName, getDataType,rc);
    }
    attachColumnListEvent();
    if (cacheCurrentSQL != 'na') {
        extractSelectedDBColumnView(cacheCurrentSQL, sqlTableName);
    }
   // hideWMSThemeLoading();
}

function attachColumnListEvent() {
    $('#dataColumnHolder .winQueryColumnItem').click(function (e) {
        $('#dataColumnHolder .winQueryColumnItem').removeClass('sqlActiveQueryColumn');
        $(this).addClass('sqlActiveQueryColumn');
        sqlCurrentColumnName = $(this).data('label');
        sqlCurrentColumnType = $(this).data('type');
        $('#winQueryConditionHeader').html('Condition: ' + sqlCurrentColumnName);
        $('#txtSqlConditionColumn').html(sqlCurrentColumnName);
        $('#txtSqlCondition').val('=');
        $('#txtSqlConditionValue').val('');
        //   changeSqlControlType();
    });

    $("#dataColumnHolder").sortable({
        update: function (event, ui) {
            buildSQLQuery();
        }
    });
    $("#dataColumnHolder").disableSelection();
}

function loadSqlQueryResult(result) {
    $('#divSqlResult').html(result);
}

function callNotificationData(queryObj) {
    var queryId = $(queryObj).data('queryid');
    var notificationId = $(queryObj).data('notificationid');
    if (notificationId != '0') {
        $.ajax({
            type: "GET",
            url: "../querybuilder/getNotificationDetails.ashx?ntid=" + notificationId + "&qid=" + queryId,
            data: { 'notificationId': notificationId },
            beforeSend: function () {
                //showWMSThemeLoading();
            },
            success: function (data) {
                //    $('#themeQueryBuilderSetNotification').css('display', 'block');
                var obj = JSON.parse(data);
                var getID = obj.getID;
                var getTitle = obj.getTitle;
                var getMessage = obj.getMessage;
                var getNotificationTo = obj.getNotificationTo;
                var getInterval = obj.getInterval;
                var getSendAt = obj.getSendAt;
                var getNotiDayOfWeek = obj.getNotiDayOfWeek;
                var getNotiDayOfMonth = obj.getNotiDayOfMonth;
                var getUserID = obj.getUserID;
                var getObjectName = obj.getObjectName;
                var getQueryId = obj.getQueryId;
                var getCreatedBy = obj.getCreatedBy;
                var getCreationDate = obj.getCreationDate;

                $('#hdnQueryIdForNotification').val(queryId);
                $('#hdnNotificationId').val(notificationId);
                $('#hdnQueryInterval').val(getInterval);
                $('#hdnQuerySendAt').val(getSendAt);
                $('#hdnQueryDayOfWeek').val(getNotiDayOfWeek);
                $('#hdnQueryDayOfMonth').val(getNotiDayOfMonth);
                $('#txtQueryNotificationTitle').val(getTitle);
                $('#txtQueryNotificationMessage').val(getMessage);
                $('#txtQueryNotificationTo').val(getNotificationTo);

                $('.queryBuilderScheduleInterval div').removeClass('qbInteralSelected');
                $('#hdnQueryInterval').val(getInterval);

                if (getInterval == 'DAILY') {
                    $('#intDaily').addClass('qbInteralSelected');
                    $('#intDaily').parent().data('value', getInterval);
                    $('#divDayOfWeek').css('display', '');
                    // $('#divDayOfWeek').css('display', 'none');
                    $('#divDayOfMonth').css('display', 'none');
                    // $('.queryBuilderScheduleDay i').attr('class', 'fas fa-check-square');
                    // $('.queryBuilderScheduleDay div').data('isselected', 'yes');
                    $('.queryBuilderScheduleDay i').attr('class', 'far fa-check-square');
                    $('.queryBuilderScheduleDay div').data('isselected', 'no');
                    $('#divQbCalendarDate').css('display', 'none');
                    /* Select Only Specific Data */
                    var getNotiEachDay = getNotiDayOfWeek.split(":");
                    for (i = 0; i < (getNotiEachDay.length - 1); i++) {
                        // alert("Notification Day: " + getNotiEachDay[i]);
                        $('#ntd' + getNotiEachDay[i]).data('isselected', 'yes');
                        $('#ntd' + getNotiEachDay[i]).find('i').attr('class', 'fas fa-check-square');
                    }
                    /* Select Only Specific Data */

                } else if (getInterval == 'WEEKLY') {
                    $('#intWeekly').addClass('qbInteralSelected');
                    $('#intWeekly').parent().data('value', getInterval);
                    $('#divDayOfWeek').css('display', '');
                    $('#divDayOfMonth').css('display', 'none');
                    $('.queryBuilderScheduleDay i:first-child').attr('class', 'far fa-check-square');
                    $('.queryBuilderScheduleDay div').data('isselected', 'no');
                    $('#divQbCalendarDate').css('display', 'none');
                    /* Select Only Specific Data */
                    var getNotiEachDayWeekly = getNotiDayOfWeek.split(":");
                    for (i = 0; i < (getNotiEachDayWeekly.length - 1); i++) {
                        // alert("Notification Day: " + getNotiEachDay[i]);
                        $('#ntd' + getNotiEachDayWeekly[i]).data('isselected', 'yes');
                        $('#ntd' + getNotiEachDayWeekly[i]).find('i').attr('class', 'fas fa-check-square');
                    }
                    /* Select Only Specific Data */
                } else if (getInterval == 'MONTHLY') {
                    $('#intMonthly').addClass('qbInteralSelected');
                    $('#intMonthly').parent().data('value', getInterval);
                    $('#divDayOfWeek').css('display', 'none');
                    $('#divDayOfMonth').css('display', '');
                    $('.queryBuilderScheduleDay i').attr('class', 'far fa-check-square');
                    $('.queryBuilderScheduleDay div').data('isselected', 'no');
                    /* Select Monthly Data */
                    $('#dmFirstDayOfMonth').data('isselected', 'no');
                    $('#dmLastDayOfMonth').data('isselected', 'no');
                    $('#dmMidDayOfMonth').data('isselected', 'no');
                    $('#dmFirstDayOfMonth i').attr('class', 'far fa-circle');
                    $('#dmLastDayOfMonth i').attr('class', 'far fa-circle');
                    $('#dmMidDayOfMonth i').attr('class', 'far fa-circle');
                    $('#divQbCalendarDate').css('display', 'none');
                    // alert(getNotiDayOfMonth);
                    if (getNotiDayOfMonth == 'FirstDayOfMonth') {
                        $('#dmFirstDayOfMonth').data('isselected', 'yes');
                        $('#dmFirstDayOfMonth i').attr('class', 'far fa-dot-circle');
                        $('#qbDDListDate').val('--Select Date--');
                    } else if (getNotiDayOfMonth == 'LastDayOfMonth') {
                        $('#dmLastDayOfMonth').data('isselected', 'yes');
                        $('#dmLastDayOfMonth i').attr('class', 'far fa-dot-circle');
                        $('#qbDDListDate').val('--Select Date--');
                    } else {
                        $('#dmMidDayOfMonth').data('isselected', 'yes');
                        $('#dmMidDayOfMonth i').attr('class', 'far fa-dot-circle');
                        $('#qbDDListDate').val(getNotiDayOfMonth);
                        $('#divQbCalendarDate').css('display', '');
                        $('#hdnQueryDayOfMonth').val('MidDayOfMonth');
                    }
                    /* Select Monthly Data */
                }

                /* SEND AT TIME */
                var getHH = getSendAt.split(":");
                var getMM = '';
                //  alert(getHH[0]);
                $('#qbDDListHH').val(getHH[0]);

                if (getSendAt.indexOf('AM') > -1) {
                    $('#qbDDListAMPM').val('AM');
                    getMM = getHH[1].replace(' AM', '');
                } else if (getSendAt.indexOf('PM') > -1) {
                    $('#qbDDListAMPM').val('PM');
                    getMM = getHH[1].replace(' PM', '');
                }
                $('#qbDDListMM').val(getMM);
                $('#divBtnSaveNotification i').attr('class', 'fas fa-save');
                $('#divBtnSaveNotification input').val('Update Notification');
                $('#hdnQueryDayOfWeek').val(getSelectedDaysOfWeek());
                /* SEND AT TIME */

                // alert(obj.getMessage);
                openSqlQueryNotificationPopup(queryObj);
               // hideWMSThemeLoading();
                //if (data.trim() != '') {
                //    alert(data);
                //} else {
                //    hideWMSThemeLoading();
                //}
            }
        });
    } else {
        $('#divBtnSaveNotification i').attr('class', 'fas fa-plus');
        $('#divBtnSaveNotification input').val('Save Notification');
        openSqlQueryNotificationPopup(queryObj);
       // hideWMSThemeLoading();
    }
}

function callAPItoGetObjectColumn(sqlTableName, cacheCurrentSQL) {
    if (sqlTableName != '') {
        $('#hdnObjectName').val(sqlTableName);
      //  $('#winQueryHeader').html(sqlTableName);
        buildSQLQuery();
        $('#dataColumnHolder').empty();

        $.ajax({
            type: "POST",
            url: "../ImportDesign/GetobjectColumnImport.ashx",
            data: { 'objname': sqlTableName },
            beforeSend: function () {
               // showWMSThemeLoading();
            },
            success: function (data) {
                // alert(data);
                if (data.trim() != '') {
                    loadQueryColumns(data, sqlTableName, cacheCurrentSQL);
                } else {
                   // hideWMSThemeLoading();
                }
            }
        });
    } else {
       // hideWMSThemeLoading();
    }
}

function callAPItoGetQueryResult() {
    if (sqlFinalQuery != '') {
        $('#aspnetForm').attr('target', 'iframeSqlQueryResult');
        $('.themeWmsSqlQueryPopup').css('display', 'block');
       // showWMSThemeLoading();
    } else {
       // hideWMSThemeLoading();
    }
}

function callAPItoGetQueryResultNew() {
    grdSqlQueryResult.refresh();
}

function grdSqlQueryResult_OnSuccess() {
}


function loadQueryObjects() {
    // addQueryObjects('V_WMS_LocationObj');
    // addQueryObjects('V_WMS_UserObj');
    // addQueryObjects('V_WMS_SkuObj');
    // addQueryObjects('V_WMS_InboundObj');

    $('#dataObjectHolder li').click(function (e) {
        resetQueryVariables();
        sqlTableName = $(this).data('value');
        callAPItoGetObjectColumn(sqlTableName, 'na');
    });
    var checkData = $('#dataColumnHolder').val();

        var chkEditQueryId = $('#hdnQueryId').val();
        var cacheCurrentSQL = $('#hdnSQLQueryText').val();
        if (chkEditQueryId.trim() != '') {
            sqlTableName = $('#hdnObjectName').val();
           // callAPItoGetObjectColumn(sqlTableName, cacheCurrentSQL);
            attachColumnListEvent();
        }
   

}

function extractSelectedDBColumnView(getCacheCurrentSQL, getObjectName) {
    var splitSqlByFrom = getCacheCurrentSQL.split(' from ' + getObjectName);
    var removeSelectFromSql = splitSqlByFrom[0].replace('Select ', '');
    var splitSqlByComma = removeSelectFromSql.split(',');
    for (var i = 0; i < splitSqlByComma.length; i++) {
        var getCacheColumn = splitSqlByComma[i].trim();
        if (getCacheColumn.indexOf(' AS ') > -1) {
            var splitSqlColByAs = getCacheColumn.split(' AS ');
            $('#sqlCol-' + splitSqlColByAs[0] + ' input[type=text]').val(splitSqlColByAs[1]);
            $('#sqlCol-' + splitSqlColByAs[0] + ' input[type=checkbox]').prop("checked", true);
            $('#sqlCol-' + splitSqlColByAs[0]).addClass("editableSqlCol");

        } else {
            $('#sqlCol-' + getCacheColumn + ' input[type=text]').val(getCacheColumn);
            $('#sqlCol-' + getCacheColumn + ' input[type=checkbox]').prop("checked", true);
            $('#sqlCol-' + getCacheColumn).addClass("editableSqlCol");
        }
    }
    restoreWhereConditionOnEdit();
    //buildSQLQuery();
}

function addQueryObjects(dataLabel) {
    $('#dataObjectHolder').append('<li data-value="' + dataLabel + '"><i class="fas fa-caret-right"></i>' + dataLabel + '</li>');
}

function addQueryColumns(dataLabel, dataType,rc) {
    //var columnControl
    //$('#dataColumnHolder').append('<div id="sqlCol-' + dataLabel + '" data-type="' + dataType + '" data-label="' + dataLabel + '" class="winQueryColumnItem"><input type="checkbox" id="" value="' + dataLabel + '" onclick="addColumnToSqlQuery(\'' + dataLabel + '\', this);" />' + dataLabel + '<a href="#" class="editSqlColumnName"><i class="fas fa-pen"></i></a><div class="frmSqlColumnEdit">display as <input type="text" value="' + dataLabel + '" onkeyup="editSqlColumnName(\'' + dataLabel + '\',this);" /> < select id ="Datatype"  name = "Datatype" > <option value="String">String</option><option value="Datetime">Datetime</option><option value="Float">Float</option><option value="Number">Number</option></select>< input type = "checkbox" id = "IsNull" value = "IsNull' + dataLabel + '" />< input type = "Text" id = "Dateformate" value = "Dateformate" />< input type = "Text" id = "Length" value = "Length" /> </div ></div > ');

    //$('#dataColumnHolder').append('<div id="sqlCol-' + dataLabel + '" data-type="' + dataType + '" data-label="' + dataLabel + '" class="winQueryColumnItem"><input type="checkbox" id="" value="' + dataLabel + '" onclick="addColumnToSqlQuery(\'' + dataLabel + '\', this);" />' + dataLabel + '<a href="#" class="editSqlColumnName"><i class="fas fa-pen"></i></a><div class="frmSqlColumnEdit">display as <input type="text" value="' + dataLabel + '" onkeyup="editSqlColumnName(\'' + dataLabel + '\',this);" /></div></div>');

    $('#dataColumnHolder').append('<div class="winQueryColumnItem sqlCol" data-type="' + dataType + '" data-label="' + dataLabel + '" ><li><table class="dataTable" style="width:99%"><tr><td style="width:31%"><input type="checkbox" class="chkIsSelected" value="' + dataLabel + '"  />' + dataLabel + '</td><td style="width:20%" align="center" > <select name="dataType" class="dataType"><option value="Number">Number</option><option value="String">String</option> <option value="Float">Float</option> <option value="DD/MM/YYYY">DD/MM/YYYY</option> <option value="MM/DD/YYYY">MM/DD/YYYY</option> <option value="YYYY/MM/DD">YYYY/MM/DD</option></select ></td > <td style="width:10%"><input type="checkbox" class="isNull" value="Is Null" /></td> <td style="width:20%"><input type="Text" maxlength="4" size="4" class="length" value="" /></td></tr ></table ></li></div > ');


    $(function () {
        $("#dataColumnHolder").sortable();
        $("#dataColumnHolder").disableSelection();
    });
}
//function SaveColumnData() {

//    var myData = '';
//    var rowCounter = 0;

//    var hdnImportID = 0;


//    hdnImportID = document.getElementById('hdnImportID').value;
//    var isValid = checkIfValidValue();
//    if (isValid == true) {

//        $('#dataColumnHolder table').each(function () {

//            var hasDataType = $(this).has('.chkIsSelected');
//            if (hasDataType) {

//                var isCheckboxChecked = $(this).find('.chkIsSelected').prop('checked');
//                if (isCheckboxChecked) {


//                    var isNullSelected = $(this).find('.isNull').prop('checked');
//                    var Datalength = $(this).find('.length').val();
//                    if (Datalength == '') {
//                        Datalength = '';

//                    }
//                    if (rowCounter != 0) {
//                        myData += "|";
//                    }

//                    myData += ":" + $(this).find('.chkIsSelected').val();
//                    myData += ":" + rowCounter;
//                    myData += ":" + $(this).find('.dataType').val();
//                    myData += ":" + isNullSelected;
//                    // myData += ":" + Datalength;
//                    myData += ":" + $(this).find('.length').val();


//                }
//            }
//            rowCounter = rowCounter + 1;
//        });


//        //alert(myData);
//        if (document.getElementById(ddlObject_vr).value == "" || document.getElementById(ddlObject_vr).value == "0") {
//            showAlert("Please Enter Email Id!", "error", "#");
//            document.getElementById(txtemailid).focus();
//            return false;
//        }
//        else if (document.getElementById(ddlObject_vr).value == "" || document.getElementById(ddlObject_vr).value == "0") {
//            showAlert("Please Enter Email Id!", "error", "#");
//            document.getElementById(txtemailid).focus();
//            return false;
//        }
//        else {
//            var ddlObject = document.getElementById(ddlObject_vr).value;
//            var Customer = document.getElementById(ddlcustomer_vr).value;
//            //  var ViewName = document.getElementById(hdnObjectName).value;
//            var ViewName = $('#hdnObjectName').val();

//            var obj1 = new Object();
//            obj1.ColumnData = myData;
//            obj1.objectName = ddlObject;
//            obj1.viewName = ViewName;
//            obj1.Ipmoprtid = hdnImportID;
//            obj1.Customer = Customer;
//            // hdngetSaveColumnData.value = myData;
//            // return true; 
//            PageMethods.SaveColumnData(obj1, onSuccessSaveData, null);
//        }
        
//    }
//}

function jsSaveData()
{
    debugger;
    showAlert("You click on Import", "Error", "#");
    var myData = '';
    var rowCounter = 0;

    var hdnImportID = 0;


    hdnImportID = document.getElementById('hdnImportID').value;
    var isValid = checkIfValidValue();
    if (isValid == true) {
        var CountCheckbox = 0;
        $('#dataColumnHolder table').each(function () {

            var hasDataType = $(this).has('.chkIsSelected');
            if (hasDataType) {

                var isCheckboxChecked = $(this).find('.chkIsSelected').prop('checked');
                if (isCheckboxChecked) {
                    CountCheckbox = CountCheckbox + 1;

                    var isNullSelected = $(this).find('.isNull').prop('checked');
                    var Datalength = $(this).find('.length').val();
                    if (Datalength == '') {
                        Datalength = '';

                    }
                    if (rowCounter != 0) {
                        myData += "|";
                    }

                    myData += ":" + $(this).find('.chkIsSelected').val();
                    myData += ":" + rowCounter;
                    myData += ":" + $(this).find('.dataType').val();
                    myData += ":" + isNullSelected;
                    // myData += ":" + Datalength;
                    myData += ":" + $(this).find('.length').val();


                }
            }
            rowCounter = rowCounter + 1;
        });
        if (CountCheckbox == 0) {
            showAlert("Please select  Coloumn name !", "error", "#");
            //document.getElementById(CountCheckbox).focus();
            return false;
        }
        var Customer = document.getElementById(ddlcustomer_vr).value;
        var ddlObject = document.getElementById(ddlObject_vr).value; 
        if (ddlObject == '' || ddlObject == '0') {
            ddlObject = hdnObjectName.value;
        }
        var getddlObject = $('#ddlobject').val();
        //alert(myData);
       
         if (Customer == "" || Customer == "0") {
            showAlert("Please select Customer!", "error", "#");
            document.getElementById(ddlcustomer_vr).focus();
            return false;
        }
        else {
            var ViewName = $('#hdnObjectName').val();
             var obj1 = new Object();
             //var res = myData.charAt(0);
             //if (res == '|') {     
             //}
             var newMyData = myData.indexOf('|') == 0 ? myData.substring(1) : myData;
            // alert(newMyData);
             obj1.ColumnData = newMyData;
            obj1.objectName = ddlObject;
            obj1.viewName = ViewName;
             obj1.Ipmoprtid = hdnImportID;
         
            if (hdnImportID.value == 0) {
                if (ddlObject == "" || ddlObject == "0") {
                    showAlert("Please select  Object!", "error", "#");
                    document.getElementById(ddlObject_vr).focus();
                    return false;
                }
            }
            obj1.Customer = Customer;
            PageMethods.SaveColumnDatanew(obj1, onSuccessSaveDatanew, onFailSaveDatanew);
        }

    }
}
function onSuccessSaveDatanew(result) {
  //  alert(result);
    if (parseInt(result) == 0) {
        alert("Import Template already Generated for customer");
    }
    if (parseInt(result) == 1)
    {
        alert("Data save sucessfully");
        hdnImportID.value = "0";
        //ddlObject.value = 0;
        ddlObject_vr = 0;
        ddlcustomer_vr = 0;
       // window.location.href = "ImportDesign.aspx?importID=result&Result='sucess'";
    }
    if (parseInt(result) == 2) {
        alert("Data Updated sucessfully");
        hdnImportID.value = "0";
        ddlObject_vr = 0;
        ddlcustomer_vr = 0;
        // window.location.href = "ImportDesign.aspx?importID=result&Result='sucess'";
    }
    grvQueryList.refresh();
    window.location.href = '../ImportDesign/ImportDesign.aspx';
}
function onFailSaveDatanew() 
{
    showAlert("Error occurred", "Error", "../importdesign/ImportDesign.aspx");
}
function saveAndAddCondition() {
    var dataColumn = sqlCurrentColumnName;
    var dataCondition = $('#txtSqlCondition').val();
    var dataConditionValue = $('#txtSqlConditionValue').val();
    if (checkIsValidSqlCondition()) {
        sqlConditionGroupCounter = sqlConditionGroupCounter + 1;
        var dataConditionJoin = 'qb-' + sqlConditionGroupCounter;
        // addQueryCondition(dataColumn, dataCondition, dataConditionValue, dataConditionJoin, sqlCurrentColumnType);
        $('#dataConditionHolder').append('<div class="row qbRow' + sqlConditionGroupCounter + '" data-column="' + dataColumn + '" data-condition="' + dataCondition + '" data-conditionvalue="' + dataConditionValue + '" data-join="' + dataConditionJoin + '" data-columntype="' + sqlCurrentColumnType + '"><div class="col-md-3">' + dataColumn + '</div><div class="col-md-3">' + dataCondition + '</div><div class="col-md-3">' + dataConditionValue + '</div><div class="col-md-3 sqlBtnConditionAction"><a href="#"><i class="fas fa-pencil-alt"></i></a> | <a href="#" onclick="deleteSqlCondition(\'qbRow' + sqlConditionGroupCounter + '\', this);"><i class="fas fa-trash-alt"></i></a></div></div>');
        $("#dataConditionHolder").sortable({
            update: function (event, ui) {
                buildSQLQuery();
            }
        });
        $("#dataConditionHolder").disableSelection();
        buildSQLQuery();
    }
}

function checkIsValidSqlCondition() {
    var isValidCondition = true;
    var dataColumn = sqlCurrentColumnName;
    var dataCondition = $('#txtSqlCondition').val();
    var dataConditionValue = $('#txtSqlConditionValue').val();
    if (dataColumn.trim() == '') {
        showAlert('Please select object column first !!', 'Error', '#');
        isValidCondition = false;
    } else if (dataConditionValue.trim() == '') {
        showAlert('Please enter value for "' + dataCondition + '" condition  !!', 'Error', '#');
        isValidCondition = false;
    } else {
        isValidCondition = validateSqlDataInput(dataConditionValue);
    }
    return isValidCondition;
}

$(document).ready(function () {
    loadQueryObjects();
});

function restoreWhereConditionOnEdit() {
    var cacheWhereCondition = $('#hdnHtmlWhereObj').val();
    if (cacheWhereCondition.trim() != '') {
        var breakWhereConByRules = cacheWhereCondition.split('[R]');
        for (i = 0; i < breakWhereConByRules.length; i++) {
            var breakRuleBySep = breakWhereConByRules[i].split('[S]');
            var getDataColumn = breakRuleBySep[0];
            var getDataCondition = breakRuleBySep[1];
            var getDataConditionValue = breakRuleBySep[2];
            var getDataJoin = breakRuleBySep[3];
            var getDataColumnType = breakRuleBySep[4];

            sqlConditionGroupCounter = sqlConditionGroupCounter + 1;
            if (getDataColumn == 'QBuilderGrpCondition') {
                $('#dataConditionHolder').append('<div class="row qbRow' + sqlConditionGroupCounter + '"  data-column="' + getDataColumn + '" data-condition="' + getDataCondition + '" data-conditionvalue="' + getDataConditionValue + '" data-join="' + getDataJoin + '" data-columntype="' + getDataColumnType + '"><div class="col-md-9 sqlGrpBrCondition">' + getDataCondition + '</div><div class="col-md-3 sqlGrpBrCondition sqlBtnConditionAction"><a href="#"><i class="fas fa-pencil-alt"></i></a> | <a href="#" onclick="deleteSqlCondition(\'qbRow' + sqlConditionGroupCounter + '\', this);"><i class="fas fa-trash-alt"></i></a></div></div>');
            } else {
                $('#dataConditionHolder').append('<div class="row qbRow' + sqlConditionGroupCounter + '" data-column="' + getDataColumn + '" data-condition="' + getDataCondition + '" data-conditionvalue="' + getDataConditionValue + '" data-join="' + getDataJoin + '" data-columntype="' + getDataColumnType + '"><div class="col-md-3">' + getDataColumn + '</div><div class="col-md-3">' + getDataCondition + '</div><div class="col-md-3">' + getDataConditionValue + '</div><div class="col-md-3 sqlBtnConditionAction"><a href="#"><i class="fas fa-pencil-alt"></i></a> | <a href="#" onclick="deleteSqlCondition(\'qbRow' + sqlConditionGroupCounter + '\', this);"><i class="fas fa-trash-alt"></i></a></div></div>');
            }
        }
        $("#dataConditionHolder").sortable({
            update: function (event, ui) {
                buildSQLQuery();
            }
        });
        $("#dataConditionHolder").disableSelection();
    }
    buildSQLQuery();
}

function getSqlWhereCondition() {
    var currentWhereStatement = '';
    var sqlArrLen = $('#dataConditionHolder .row').length;
    // document.title = sqlArrLen;
    if (sqlArrLen > 0) {
        var qbCnt = 0;
        var getHdnWhereVal = '';
        $('#dataConditionHolder .row').each(function () {
            // data-column="QBuilderGrpCondition" data-condition="AND" data-conditionvalue="qb-1" data-join="qb-1" data-columntype="qb-operator"
            var getDataColumn = $(this).data('column');
            var getDataCondition = $(this).data('condition');
            var getDataConditionValue = $(this).data('conditionvalue');
            var getDataJoin = $(this).data('join');
            var getDataColumnType = $(this).data('columntype');
            var getHdnWhereObj = getDataColumn + '[S]' + getDataCondition + '[S]' + getDataConditionValue + '[S]' + getDataJoin + '[S]' + getDataColumnType;
            if (getHdnWhereVal.trim() == '') {
                getHdnWhereVal += getHdnWhereObj;
            } else {
                getHdnWhereVal += "[R]" + getHdnWhereObj;
            }
            //   alert(getHdnWhereVal);
            $('#hdnHtmlWhereObj').val(getHdnWhereVal);

            var currentSqlWhereArr = [getDataColumn, getDataCondition, getDataConditionValue, getDataJoin, getDataColumnType];
            var getColumnDataType = currentSqlWhereArr[4];
            var constructWhere = '';
            if (getColumnDataType != 'operator') {
                if (getColumnDataType == "int" || getColumnDataType == "bigint" || getColumnDataType == "tinyint" || getColumnDataType == "smallint" || getColumnDataType == "decimal" || getColumnDataType == "numeric") {
                    constructWhere = currentSqlWhereArr[0] + ' ' + currentSqlWhereArr[1] + ' ' + currentSqlWhereArr[2];
                } else {
                    var getQueryOperator = currentSqlWhereArr[1];
                    if (getQueryOperator == 'LIKE' || getQueryOperator == 'NOT LIKE') {
                        constructWhere = currentSqlWhereArr[0] + ' ' + currentSqlWhereArr[1] + ' \'%' + currentSqlWhereArr[2] + '%\'';
                    } else {
                        constructWhere = currentSqlWhereArr[0] + ' ' + currentSqlWhereArr[1] + ' \'' + currentSqlWhereArr[2] + '\'';
                    }
                }
            }
            // var constructWhere = currentSqlWhereArr[0] + ' ' + currentSqlWhereArr[1] + ' ' + currentSqlWhereArr[2];
            if (currentSqlWhereArr[0] == 'QBuilderGrpCondition') {
                constructWhere = currentSqlWhereArr[1];
            }
            if (qbCnt == 0) {
                currentWhereStatement += ' ' + 'WHERE' + constructWhere;
            } else {
                currentWhereStatement += ' ' + constructWhere;
            }
            qbCnt = qbCnt + 1;
        });
    }
    return currentWhereStatement;
}

function getSqlSelectedColumns() {
    var selectedColumns = '';
    var sqlArrLen = $('.winQueryColumnItem').length;
    var sqlSelectedColumn = $('#dataColumnHolder input[type=checkbox]').filter(":checked").length;
    if (sqlSelectedColumn > 0) {
        var qbCnt = 0;
        $('.winQueryColumnItem').each(function () {
            var getColumnId = $(this).attr('id');
            var isQueryCheckboxChecked = $('#' + getColumnId + ' input[type=checkbox]').prop('checked');
            if (isQueryCheckboxChecked) {
                var modifiedSqlColName = $('#' + getColumnId + ' input[type=text]').val();
                var originalSqlColName = $('#' + getColumnId + ' input[type=checkbox]').val();
                var finalSqlColName = originalSqlColName;

                if (modifiedSqlColName.trim() != originalSqlColName.trim()) {
                    finalSqlColName = originalSqlColName + ' AS ' + modifiedSqlColName;
                }

                if (qbCnt == 0) {
                    selectedColumns += finalSqlColName;
                } else {
                    selectedColumns += ', ' + finalSqlColName;
                }
                qbCnt = qbCnt + 1;
            }
        });
    } else {
        selectedColumns = '*';
    }
    return selectedColumns;
}

function buildSQLQuery() {
    sqlFinalQuery = "Select " + getSqlSelectedColumns() + " from " + sqlTableName + getSqlWhereCondition();
    $('#hdnSQLQueryText').val(sqlFinalQuery);
    $('#lblSQLQueryText').html(sqlFinalQuery);
}

function addColumnToSqlQuery(dataLabel, getCheckBox) {
    if (getCheckBox.checked) {
        $('#sqlCol-' + dataLabel).addClass('editableSqlCol');
        $('#sqlCol-' + dataLabel + ' input').val(dataLabel);
    } else {
        $('#sqlCol-' + dataLabel).removeClass('editableSqlCol');
    }
    buildSQLQuery();
}

function editSqlColumnName(dataLabel, txtdisplayAs) {
    // var findColumnIndex = sqlColumnNames.indexOf(dataLabel);
    // sqlColumnDisplayAs[findColumnIndex] = txtdisplayAs.value;
    buildSQLQuery();
}

function deleteSqlCondition(conditionToDelete) {
    $('#dataConditionHolder .' + conditionToDelete).remove();
    buildSQLQuery();
}

function fnQBKeepNumberOnly(inputObj) {
    var getObjNumber = inputObj.value;
    var chkIsNumber = getObjNumber.trim() / 1;
    while (chkIsNumber != getObjNumber.trim()) {
        var qbNewInputVal = inputObj.value;
        inputObj.value = qbNewInputVal.substring(0, (qbNewInputVal.length - 1));
        getObjNumber = inputObj.value;
        chkIsNumber = getObjNumber.trim() / 1;
    }
}

function fnQBCheckIfValidNumericInput(inputObj, isIntegerOnly) {
    var getObjNumber = inputObj.value;
    var chkIsNumber = getObjNumber.trim() / 1;
    if ((!isIntegerOnly) && (getObjNumber.trim() == '.')) {
        inputObj.value = "0.";
    } else if (isIntegerOnly && (getObjNumber.indexOf('.') > -1) && chkIsNumber == getObjNumber.trim()) {
        showAlert('Decimal input is not allowed', 'Error', '#');
        inputObj.value = getObjNumber.replace('.', '');
    } else if (chkIsNumber != getObjNumber.trim()) {
        showAlert('Enter valid numeric value', 'Error', '#');
        fnQBKeepNumberOnly(inputObj);
        //alert('Please enter number only');s
    }
    //alert(chkIsNumber);
}

function changeSqlControlType() {
    $("#txtSqlConditionValue").datepicker("destroy");
    if (sqlCurrentColumnType == "int" || sqlCurrentColumnType == "bigint" || sqlCurrentColumnType == "tinyint" || sqlCurrentColumnType == "smallint") {
        //  $('#txtSqlConditionValue').attr('type', 'number');
        $('#txtSqlConditionValue').attr('onkeyup', 'fnQBCheckIfValidNumericInput(this, true);');
        $('#txtSqlConditionValue').attr('onkeydown', 'return true;');
        $('#txtSqlConditionValue').attr('placeholder', '0-9 Only');
    } else if (sqlCurrentColumnType == "decimal" || sqlCurrentColumnType == "numeric") {
        //  $('#txtSqlConditionValue').attr('type', 'number');
        $('#txtSqlConditionValue').attr('onkeyup', 'fnQBCheckIfValidNumericInput(this, false);');
        $('#txtSqlConditionValue').attr('onkeydown', 'return true;');
        $('#txtSqlConditionValue').attr('placeholder', '0-9 and " . " Only');
    } else if (sqlCurrentColumnType == "date" || sqlCurrentColumnType == "datetime") {
        $('#txtSqlConditionValue').attr('placeholder', 'YYYY-MM-DD');
        $("#txtSqlConditionValue").datepicker({
            dateFormat: "yy-mm-dd"
        });
        $('#txtSqlConditionValue').attr('onkeyup', 'return false;');
        $('#txtSqlConditionValue').attr('onkeydown', 'return false;');
        //$('#txtSqlConditionValue').attr('type', 'date');
    } else if (sqlCurrentColumnType == "time") {
        $('#txtSqlConditionValue').attr('placeholder', '00:00');
        $('#txtSqlConditionValue').attr('onkeyup', 'return true;');
        $('#txtSqlConditionValue').attr('onkeydown', 'return true;');
        //   $('#txtSqlConditionValue').attr('type', 'time');
    } else {
        $('#txtSqlConditionValue').attr('placeholder', '');
        $('#txtSqlConditionValue').attr('onkeyup', 'return true;');
        $('#txtSqlConditionValue').attr('onkeydown', 'return true;');
        //   $('#txtSqlConditionValue').attr('type', 'text');
    }
}

function validateSqlDataInput(dataInput) {
    var isValidSqlDataInput = true;
    return isValidSqlDataInput;
}

function addSqlGroupCondition() {
    var getGroupBracketIndex = txtSqlGroupBrackets.selectedIndex;
    var getGroupBracketValue = txtSqlGroupBrackets.value;
    if (getGroupBracketIndex > 0) {
        sqlConditionGroupCounter = sqlConditionGroupCounter + 1;
        // addQueryCondition('QBuilderGrpCondition', getGroupBracketValue, 'qb-' + sqlConditionGroupCounter, 'qb-' + sqlConditionGroupCounter, 'qb-operator');
        $('#dataConditionHolder').append('<div class="row qbRow' + sqlConditionGroupCounter + '"  data-column="QBuilderGrpCondition" data-condition="' + getGroupBracketValue + '" data-conditionvalue="qb-' + sqlConditionGroupCounter + '" data-join="qb-' + sqlConditionGroupCounter + '" data-columntype="qb-operator"><div class="col-md-9 sqlGrpBrCondition">' + getGroupBracketValue + '</div><div class="col-md-3 sqlGrpBrCondition sqlBtnConditionAction"><a href="#"><i class="fas fa-pencil-alt"></i></a> | <a href="#" onclick="deleteSqlCondition(\'qbRow' + sqlConditionGroupCounter + '\', this);"><i class="fas fa-trash-alt"></i></a></div></div>');
        buildSQLQuery();
    } else {
        showAlert('Please select Group Bracket to insert', 'Error', '#');
    }
}

function closeSqlQueryPopup() {
    $('#iframeSqlQueryResult').attr('src', '');
    $('.themeWmsSqlQueryPopup').attr('style', 'display:none;');
    event.stopImmediatePropagation();
    return false;
}
function validateQueryTitle() {
    var getCurrentQueryTitle = $('#txtShortcutTitle').val();
    if (getCurrentQueryTitle != '') {
        return true;
    } else {
        showAlert('Please enter Query Title!!', 'Error', '#');
        return false;
    }
}

function openSqlQueryTitlePopup() {
    var getCurrentQuery = $('#hdnSQLQueryText').val();
    if (getCurrentQuery != '') {
        var getSqlId = $('#hdnQueryId').val();
        if (getSqlId.trim() == '' || getSqlId.trim() == '0') {
            $('#txtShortcutTitle').val('');
        }
        $('#themeQueryBuilderTitlePopup').css('display', 'block');
    } else {
        showAlert('Can\'t save empty query!!', 'Error', '#');
    }

    return false;
}
function closeSqlQueryTitlePopup() {
    $('#themeQueryBuilderTitlePopup').css('display', 'none');
    var getSqlId = $('#hdnQueryId').val();
    if (getSqlId.trim() == '' || getSqlId.trim() == '0') {
        $('#txtShortcutTitle').val('');
    }
}

function getSelectedDaysOfWeek() {
    var finalDaysOfWeek = '';
    $('.queryBuilderScheduleDay div').each(function () {
        var isCurrentItemSelected = $(this).data('isselected');
        if (isCurrentItemSelected == 'yes') {
            finalDaysOfWeek += $(this).data('value') + ':';
        }
    });
    return finalDaysOfWeek;
}

function resetQueryNotificationPopup(getQueryId, notificationId) {
    $('.queryBuilderScheduleDay div').unbind();
    $('.queryBuilderScheduleWeek div').unbind();
    $('.queryBuilderScheduleInterval div').unbind();

    $('.queryBuilderScheduleInterval div').removeClass('qbInteralSelected');
    $('.queryBuilderScheduleInterval div:first-child').addClass('qbInteralSelected');

    $('#qbDDListHH').val('--HH--');
    $('#qbDDListMM').val('--MM--');
    $('#qbDDListAMPM').val('AM');
    $('#qbDDListDate').val('--Select Date--');

    $('.queryBuilderScheduleWeek div i').attr('class', 'far fa-circle');
    $('.queryBuilderScheduleWeek div').data('isselected', 'no');
    $('.queryBuilderScheduleWeek div:first-child i').attr('class', 'far fa-dot-circle');
    $('.queryBuilderScheduleWeek div:first-child').data('isselected', 'yes');

    $('#divDayOfWeek').css('display', '');
    //$('#divDayOfWeek').css('display', 'none');
    $('#divDayOfMonth').css('display', 'none');
    $('#divQbCalendarDate').css('display', 'none');

    $('.queryBuilderScheduleDay i').attr('class', 'fas fa-check-square');
    $('.queryBuilderScheduleDay div').data('isselected', 'yes');

    $('#txtQueryNotificationTitle').val('');
    $('#txtQueryNotificationMessage').val('');
    $('#txtQueryNotificationTo').val('');
    $('#hdnQueryIdForNotification').val(getQueryId);
    $('#hdnNotificationId').val(notificationId);
    $('#hdnQueryInterval').val('DAILY');
    $('#hdnQuerySendAt').val('');
    $('#hdnQueryDayOfWeek').val(getSelectedDaysOfWeek());
    $('#hdnQueryDayOfMonth').val('');
}

function openSqlQueryNotificationPopup(queryObj) {
    var getQueryId = $(queryObj).data('queryid');
    var notificationId = $(queryObj).data('notificationid');
    //  alert('Notification ID: ' + notificationId);
    //var notificationId = $('#hdnNotificationId').val();
    if (notificationId == '0') {
        resetQueryNotificationPopup(getQueryId, notificationId);
        $('#hdnQueryIdForNotification').val(getQueryId);
    }

    $('#themeQueryBuilderSetNotification').css('display', 'block');
    $('.queryBuilderScheduleDay div').click(function (e) {
        var getInterval = $('#hdnQueryInterval').val();
        if (getInterval == 'WEEKLY') {
            $('.queryBuilderScheduleDay i').attr('class', 'far fa-check-square');
            $('.queryBuilderScheduleDay div').data('isselected', 'no');
        }
        var checkIsSelected = $(this).data('isselected');
        var getHdnQueryDayOfWeek = $('#hdnQueryDayOfWeek').val();
        // var updatedHdnQueryDayOfWeek = '';
        // var currentScheduleDayVal = $(this).data('value');
        if (checkIsSelected == 'yes') {
            $(this).data('isselected', 'no');
            $(this).find('i').attr('class', 'far fa-check-square');
            //if (getHdnQueryDayOfWeek.indexOf(currentScheduleDayVal) > -1) {
            //    updatedHdnQueryDayOfWeek = getHdnQueryDayOfWeek.replace(currentScheduleDayVal + ':', '');
            //    $('#hdnQueryDayOfWeek').val(updatedHdnQueryDayOfWeek);
            //}
        } else {
            $(this).data('isselected', 'yes');
            $(this).find('i').attr('class', 'fas fa-check-square');
            //if (getHdnQueryDayOfWeek.indexOf(currentScheduleDayVal) < 0) {
            //    updatedHdnQueryDayOfWeek = getHdnQueryDayOfWeek + currentScheduleDayVal + ':';
            //    $('#hdnQueryDayOfWeek').val(updatedHdnQueryDayOfWeek);
            //}
        }
        $('#hdnQueryDayOfWeek').val(getSelectedDaysOfWeek());
        // alert($('#hdnQueryDayOfWeek').val());
    });

    $('.queryBuilderScheduleWeek div').click(function (e) {
        var checkIsSelected = $(this).data('isselected');
        $('.queryBuilderScheduleWeek div i').attr('class', 'far fa-circle');
        $('.queryBuilderScheduleWeek div').data('isselected', 'no');
        $(this).data('isselected', 'yes');
        $(this).find('i').attr('class', 'far fa-dot-circle');
        var getDayOfMonth = $(this).data('value');
        $('#hdnQueryDayOfMonth').val(getDayOfMonth);
        if (getDayOfMonth == 'MidDayOfMonth') {
            $('#divQbCalendarDate').css('display', '');
        } else {
            $('#divQbCalendarDate').css('display', 'none');
        }
    });

    $('.queryBuilderScheduleInterval div').click(function (e) {
        $('.queryBuilderScheduleInterval div').removeClass('qbInteralSelected');
        $(this).addClass('qbInteralSelected');
        var getScheduleInterval = $(this).data('value');
        $('#hdnQueryInterval').val(getScheduleInterval);
        $(this).parent().data('value', getScheduleInterval);
        var getDayOfMonth = $('#hdnQueryDayOfMonth').val();
        // alert(getDayOfMonth);
        if (getScheduleInterval == 'DAILY') {
            $('#divDayOfWeek').css('display', '');
            // $('#divDayOfWeek').css('display', 'none');
            $('#divDayOfMonth').css('display', 'none');
            $('.queryBuilderScheduleDay i').attr('class', 'fas fa-check-square');
            $('.queryBuilderScheduleDay div').data('isselected', 'yes');
            $('#divQbCalendarDate').css('display', 'none');
            $('#hdnQueryDayOfWeek').val(getSelectedDaysOfWeek());
        } else if (getScheduleInterval == 'WEEKLY') {
            $('#divDayOfWeek').css('display', '');
            $('#divDayOfMonth').css('display', 'none');
            $('.queryBuilderScheduleDay i:first-child').attr('class', 'far fa-check-square');
            $('.queryBuilderScheduleDay div').data('isselected', 'no');
            $('#divQbCalendarDate').css('display', 'none');
            $('#hdnQueryDayOfWeek').val(getSelectedDaysOfWeek());
        } else if (getScheduleInterval == 'MONTHLY') {
            $('#divDayOfWeek').css('display', 'none');
            $('#divDayOfMonth').css('display', '');
            $('.queryBuilderScheduleDay i').attr('class', 'far fa-check-square');
            $('.queryBuilderScheduleDay div').data('isselected', 'no');
            if (getDayOfMonth == 'MidDayOfMonth') {
                $('#divQbCalendarDate').css('display', '');
            } else {
                $('#divQbCalendarDate').css('display', 'none');
            }
        }
    });
    return false;
}
function closeSqlQueryNotificationPopup() {
    $('#themeQueryBuilderSetNotification').css('display', 'none');
}

function findIfEmptyValue() {
    var isEmptyValue = false;
    $('.winQueryColumnItem').each(function () {
        // $('#dataColumnHolder input.length').each(function(){
        var isCheckBoxChecked = $(this).find('.chkIsSelected').prop('checked');
       
        if (isCheckBoxChecked) {
            $getLengthBox = $(this).find('.length');
            if ($getLengthBox.val().trim() == '') {
                isEmptyValue = true;
                return false;
            }
        }
    });
    if (isEmptyValue) {
        alert('Please enter value for length');
        return false;
    }
    //return isEmptyValue;
}

function checkIfValidValue() {
    //var isEmptyValue = false;
    var counterTotalEmpty = 0;
    var counterFilled = 0;
    var totalChecked = 0;
    var totalBoxLength = $('.winQueryColumnItem').length;
    $('.winQueryColumnItem').each(function () {
        var isCheckBoxChecked = $(this).find('.chkIsSelected').prop('checked');
        if (isCheckBoxChecked) {
            totalChecked + - 1;
            $getLengthBox = $(this).find('.length');
            if ($getLengthBox.val().trim() == '') {
                counterTotalEmpty += 1;
            } else {
                counterFilled += 1;
            }
        }
    });

    //if ((counterTotalEmpty > 0 && totalChecked != counterTotalEmpty) || (counterFilled > 0 && totalChecked != counterFilled)) {
    if ((totalChecked > 0 && counterTotalEmpty != totalChecked) || (totalChecked > 0 && counterFilled != totalChecked)) {
        alert('Please enter value for all length or leave all empty!!');
        return false;
    } else {
        return true;
    }
}
function CheckImportData(valueobj) {
    var Objectname = valueobj;
      var Customer = document.getElementById(ddlcustomer_vr).value;
    var ddlObject = document.getElementById(ddlObject_vr).value;
    PageMethods.CheckImportDataForObj(Customer, ddlObject, OnSucessCheckDuplicateImport,null)
}
function OnSucessCheckDuplicateImport(result) {
    if (result > 0) {
        alert("Import Template already Generated for customer");
        document.getElementById(ddlObject_vr).value = 0;
        document.getElementById(ddlcustomer_vr).value = 0;
    }
}

//function saveData() {
//    var isValid = checkIfValidValue();
//    if (isValid) {
//        alert('Great!! No empty record!!');
//    }
//}

