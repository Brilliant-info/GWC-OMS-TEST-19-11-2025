

function showDocumentPopup()
{
    $("#wms-srv-document-popup").show();
    $("#wms-srv-document-popup-close").click(function () {
        $("#wms-srv-document-popup").off();
        $("#wms-srv-document-popup").hide();
    });
}

function showViewPopup()
{
    $("#wms-srv-ViewOrder-popup").show();
    $("#wms-srv-ViewOrder-popup-close").click(function () {
        $("#wms-srv-ViewOrder-popup").off();
        $("#wms-srv-ViewOrder-popup").hide();
    });
}

function showOrderHistoryPopup()
{
    $("#wms-srv-OrderHistory-popup").show();
    $("#wms-srv-OrderHistory-popup-close").click(function () {
        $("#wms-srv-OrderHistory-popup").off();
        $("#wms-srv-OrderHistory-popup").hide();
    });
}

