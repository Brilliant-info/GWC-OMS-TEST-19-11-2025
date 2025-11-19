<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/CRM.Master" AutoEventWireup="true" CodeBehind="S2SORDERS.aspx.cs" Theme="Blue" Inherits="PowerOnRentwebapp.S2SORDER.S2SORDERS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../core-js/HttpLoader.js" language="javascript" type="text/javascript"></script>
    <link rel="stylesheet" href="Themes/bootstrap-4.0.0/dist/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous" type="text/javascript" />
    <link rel="Stylesheet" href="S2SORDERS.css" type="text/css" />
    <script src="Themes/bootstrap-4.0.0/dist/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl"
        crossorigin="anonymous" type="text/javascript"></script>
    <script src="ThemeHTML/js/wms-theme.js" type="text/javascript"></script>
    <script src="ThemeHTML/js/wms-app-navigator.js" type="text/javascript"></script>
    <script src="JAVASCRIPT/S2SORDER.js"></script>
    <script src="../images/opt-bg-selected2.png"></script>
      <script src="https://cdn.sheetjs.com/xlsx-latest/package/dist/xlsx.full.min.js"></script>

    <style>
        #main {
            height: auto !important;
        }

        #header-wrap {
    position: sticky;
    top: 0;
    height: auto;
    width: 100%;
    z-index: 100;
}

#header-wrap .wms-srv-grid-cell {
    background-color: #efefef !important;
    color: #000000 !important;
    font-weight: bold !important;
}

.reportHeader{
	display:table;
	width:100%;
	border:solid 1px #636363;
}

.reportHeaderRow{
	display:table-row;
}

.reportHeaderCell{
	display:table-cell;
	vertical-align:middle;
	padding:10px;
}
.reportHeaderAddress{
	display:inline-block;
	float:left;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHolder_FormHeader" runat="server">
    <asp:HiddenField ID="hdnUserID" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hdnCompanyId" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hdnUserType" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="hdnUserName" ClientIDMode="Static" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHolder_Form" runat="server">
    <div class="wms-srv-content-area pnlInbound" id="pnlAppointmentList">
        <div class="container-fluid wms-srv-dashboard-panel">
            <div class="row">
                <div class="col-md-4 nopadding">
                    <!-- Page Title -->
                    <div class="wms-srv-page-title">
                        <i class="fas fa-exchange-alt"></i><span class="wms-srv-data-title">S2S Order</span>
                    </div>
                    <!-- Page Title -->
                </div>
                <div class="col-md-8 nopadding">
                </div>
            </div>
        </div>
        <div class="gridHeadButton" style="float: right;">
            <button class="wms-srv-input  wms-srv-highlight-btn" title="Allocated Driver" onclick="showdriverPopup();" type="button" id="driverButton" data-action="action1"><i class="fas fa-plus"></i><span>&nbsp Allocated Driver</span></button>
            <button class="wms-srv-input  wms-srv-highlight-btn" title="Cancel" onclick="" style="display:none" type="button" id="button2" data-action="action2"><i class="fas fa-plus"></i><span>&nbsp Cancel</span></button>
            <button class="wms-srv-input  wms-srv-highlight-btn" title="Cancel" onclick="showOrderDetailPopup();" type="button" id="openPageBtn" data-action="action2" href="S2SORDERS.css"><i class="fas fa-plus"></i><span>&nbsp Order Detail Report</span></button>
        </div>
        <!-- Head Grid -->
        <div class="wms-srv-grid-holder pnlWmsHead" id="pnlWmsHead" style="display: none;">
            <div class="wms-srv-grid">
                <!-- Object Head Data Will be Here -->

            </div>
        </div>
        <br>
        <!-- Head Grid -->
        <div class="wms-srv-obj-main-grid pnlWmsDetail">
            <div class="wms-srv-grid-holder">
                <div class="wms-srv-grid-action-panel">
                    <div class="wms-srv-grid-common-action"><a href="#" class="wms-srv-icononly"><i class="fas fa-list"></i></a></div>
                    <label>Count Of Checkbox :</label>
                     <input type="text" value="" class="wms-srv-input wms-srv-simple-search" id="txtcountchk" style="width: 30px;">
                    <button class="wms-srv-input  wms-srv-highlight-btn" onclick="clearCheckboxAll();" type="button" id=""><span>Clear CheckBox</span></button>
                    <div class="wms-srv-search">
                        <label>Search Filter: </label>
                        <select type="text" class="wms-srv-input wms-srv-search-filter" id="ddls2sOrderColSearch">
                            <option value="0">-- Select --</option>
                            <option value="s2SOrderNo">S2S NO</option>
                            <option value="wincashOrderNo">Wincash OrderNo</option>
                        </select>
                        <input type="text" value="" class="wms-srv-input wms-srv-simple-search" id="ddls2sOrderColValue">
                        <a href="#" data-prefix="SE"><i class="fas fa-search" onclick="searchS2SOrder(); return false;" style="margin-right: 17px;"></i></a>
                        <a href="#" title="Cancel" class="wms-srv-cancel" data-prefix="CN"><i class="fas fa-times-circle" onclick="clearSearchFilterS2S();"></i></a>
                    </div>
                    
                    <div class="wms-srv-grid-title">
                        S2S Order List
                    </div>
                </div>
                <div class="wms-srv-grid-scroller" style="max-height: 300px;">                    
                    <div class="wms-srv-grid" id="gridS2SOrderList">
                    </div>
                </div>

                <div class="wms-srv-grid-pager" id="tblS2Slistpager">
                    1-0 of 0 Records <span class="wms-srv-empty-space"></span><b>Go to Page: </b>
                    <select id="ddlGridPageNo" class="wms-srv-input ddlGridPageNo" style="width: 100px;">
                        <option>1</option>
                    </select><a href="#" title="Jump To" class="wms-srv-ddlpager-go" style="font-size: 16px;"><i class="fas fa-play-circle"></i></a>

                </div>
            </div>
            <!-- Content Area End -->
        </div>

        <!-- Document Popup start-->
        <div class="wms-srv-popup-holder" id="wms-srv-document-popup" style="display: none;">
            <div class="wms-srv-popup-bg">
            </div>
            <div class="wms-srv-popup" style="width: 100%; height: 100%; margin-left: auto; margin-right: auto;">
                <div class="wms-srv-popup-content">
                    <!-- Popup Title -->
                    <div class="wms-srv-popup-title">
                        <div class="wms-srv-page-title">
                            <i class="fas fa-file-alt"></i><span>Document</span>
                        </div>
                        <a href="#" title="Close" id="wms-srv-document-popup-close" class="wms-srv-popup-close"><i class="fas fa-times-circle"></i></a>
                    </div>
                    <!-- Popup Title -->
                    <div class="wms-srv-popup-content-body" id="">
                        <div id="popupContentDocument">
                            <div id="pnlDocument">
                                <!-- Grid -->

                                <!-- Grid -->
                                <!-- Header Details -->
                                <!-- Grid -->
                                <div class="wms-srv-grid-holder pnlWmsDetail" id="pnlWmsDetail">
                                    <div class="wms-srv-grid-action-panel">
                                        <div class="wms-srv-grid-common-action"><a href="#" class="wms-srv-icononly"><i class="fas fa-list"></i></a></div>
                                        <div class="wms-srv-grid-title">
                                            Document
                                        </div>
                                    </div>
                                    <input type="hidden" id="hdnCommonDocument_RefId" value="0" />
                                    <input type="hidden" id="hdnCommonDocument_RefObject" value="0" />
                                    <div class="" style="height: auto;">
                                        <div class="wms-srv-grid" id="PnlDocumentListS2S" style="text-align: center; min-width: auto; width: 100%;">
                                        </div>
                                    </div>



                                    <%--View Only test--%>
                                    <%--   <div class="wms-srv-grid-row wms-srv-grid-add">
                                                            <div class="wms-srv-grid-cell">
                                                                 <lable> New Document</lable>
                                                            </div>
                                                            <div class="wms-srv-grid-cell">
                                                                <label> Test Description</label>
                                                            </div>
                                                            <!-- <div class="wms-srv-grid-cell">
                                                                <input type="text" data-for="KeyWords">
                                                                </div> -->
                                                          
                                                            <div class="wms-srv-grid-cell">
                                                                <label> Image</label>
                                                            </div>
                                                            <div class="wms-srv-grid-cell" id="txtCommonDocument_Name">  
                                                                <label> show image here</label></div>
                                                            <div class="wms-srv-grid-cell">
                                                                <div class="wms-srv-grid-action">
                                                                <div class="wms-srv-file-upload-holder">
                     
                                    <a href="#" title="Save" class="wms-srv-save"  onclick=""><i class="fas fa-solid fa-arrow-down"></i></a>
                                                             <div class="wms-srv-action-sep">|</div>
                                                                </div>
                                <a href="#" title="Save" class="wms-srv-save"  onclick=";"><i class="fas fa-times-circle"></i></a>
                                                                </div>
                                                            </div>
                                                        </div>--%>
                                </div>
                            </div>
                            <%--<div class="wms-srv-grid-pager">
                        <span class="wms-srv-pager-records">1-1 of 0 Records</span>
                        <span class="wms-srv-empty-space"></span>
                                                    <span class="wms-srv-pager-links"><a href="#" data-page="firstpage"><i class="fas fa-angle-double-left"></i></a><a href="#" data-page="previous"><i class="fas fa-angle-left"></i></a><a href="#" class="wms-srv-active" data-page="1">1</a><a href="#" data-page="next"><i class="fas fa-angle-right"></i></a><a href="#" data-page="lastpage"><i class="fas fa-angle-double-right"></i></a></span>
                                                    <span class="wms-srv-empty-space"></span>
                                                    <input type="text" name="txtGridPageNo" class="wms-srv-input" value="0" style="width: 80px;">
                                                    <a href="#" title="Jump To" class="wms-srv-pager-go"><i class="fas fa-play-circle"></i></a>
                                                    </div>--%>
                        </div>
                        <!-- Grid -->
                    </div>
                </div>
            </div>
        </div>
    </div>
    </div>

        <!-- Document Popup End -->

    <!-- Order History Popup -->
    <div class="wms-srv-popup-holder" id="wms-srv-OrderHistory-popup" style="display: none; width: 85%; height: 87%; margin-left: 5%; margin-right: auto;">
        <div class="wms-srv-popup-bg">
        </div>
        <div class="wms-srv-popup" style="width: 100%; height: 100%; margin-left: auto; margin-right: auto;">
            <div class="wms-srv-popup-content">
                <!-- Popup Title -->
                <div class="wms-srv-popup-title">
                    <div class="wms-srv-page-title">
                        <i class="fas fa-file-alt"></i><span>Order History</span>
                    </div>
                    <a href="#" title="Close" id="wms-srv-OrderHistory-popup-close" class="wms-srv-popup-close"><i class="fas fa-times-circle"></i></a>
                </div>
                <!-- Popup Title -->
                <div class="wms-srv-popup-content-body">

                    <!-- Grid -->
                    <div class="wms-srv-grid-holder pnlWmsHead">
                        <div class="wms-srv-grid" id="srvgridOrderHistory" style="text-align: center;">
                        </div>
                    </div>

                    <!-- Grid -->

                </div>
            </div>

        </div>
    </div>

    <!-- Order History Popup start-->

    <%--View Order List start --%>
    <div class="wms-srv-popup-holder" id="wms-srv-ViewOrder-popup" style="display: none;">
        <div class="wms-srv-popup-bg">
        </div>
        <div class="wms-srv-popup">
            <div class="wms-srv-popup-content">
                <!-- Popup Title -->
                <div class="wms-srv-popup-title">
                    <div class="wms-srv-page-title">
                        <i class="fas fa-file-alt"></i><span>View Order List</span>
                    </div>
                    <a href="#" title="Close" id="wms-srv-ViewOrder-popup-close" class="wms-srv-popup-close"><i class="fas fa-times-circle"></i></a>
                </div>
                <!-- Popup Title -->
                <div class="wms-srv-popup-content-body" id="wms-srv-popup-content-body" style="text-align: center;">
                    <div>
                        <div>
                            <!-- Grid -->
                            <div class="wms-srv-grid-holder pnlWmsHead" id="pnlHeadDocument1">
                                <div class="wms-srv-grid">
                                    <!-- Object Head Data Will be Here -->

                                    <div class="wms-srv-grid-cell">
                                        <div class="titleReturnOrder">Order List</div>
                                        <table class="tableForm" style="width: 100%;">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <%--  --%>
                                                        <span id="" class="lblReturnOrder">WinCash Order No :</span>

                                                    </td>
                                                    <td>

                                                        <input name="" type="text" maxlength="50" value="" id="viewWinCashS2SOrderNo" disabled class="inputTextReturnOrder" style="width: 200px;" />
                                                    </td>
                                                    <td>
                                                        <span id="" class="lblReturnOrder">OMS Order No:</span>
                                                    </td>
                                                    <td>
                                                        <input name="" type="text" maxlength="50" value="" id="viewomsS2Sorderno" disabled class="inputTextReturnOrder" style="width: 200px;" />

                                                    </td>
                                                    <td>
                                                        <span id="" class="lblReturnOrder">Source Store:</span>
                                                    </td>
                                                    <td>
                                                        <input name="" type="text" maxlength="50" value="" id="viewsourcestoreS2S" disabled class="inputTextReturnOrder" style="width: 200px;" />

                                                    </td>
                                                </tr>

                                                <%--second row--%>
                                                <tr>
                                                    <td>
                                                        <span id="" class="lblReturnOrder">Status :</span>

                                                    </td>
                                                    <td>
                                                        <input name="" type="text" maxlength="50" value="" id="views2sorderStatus" disabled class="inputTextReturnOrder" style="width: 200px;" />
                                                    </td>
                                                    <td>
                                                        <span id="" class="lblReturnOrder">Creation Date :</span>
                                                    </td>
                                                    <td>
                                                        <input name="" type="text" maxlength="50" value="09/10/2023" id="viewCreationDateS2S" disabled class="inputTextReturnOrder" style="width: 200px;" />
                                                    </td>
                                                    <td>
                                                        <span id="" class="lblReturnOrder">Destination Store :</span>
                                                    </td>
                                                    <td>
                                                        <input name="" type="text" maxlength="50" value="City Center" id="viewDestinationStoreS2S" disabled class="inputTextReturnOrder" style="width: 200px;" />
                                                    </td>
                                                </tr>

                                                <%--third row--%>
                                                <tr>
                                                    <td>

                                                        <span id="" class="lblReturnOrder">Performed By :</span>

                                                    </td>
                                                    <td>
                                                        <input name="" type="text" maxlength="50" value="Vodafone Commercial" id="viewperformedbyS2S" disabled class="inputTextReturnOrder" style="width: 200px;" />
                                                    </td>
                                                    <td>
                                                        <span id="" class="lblReturnOrder">Received By :</span>
                                                    </td>
                                                    <td>
                                                        <input name="" type="text" maxlength="50" value="VFQ VBS" id="viewreceivedByS2S" disabled class="inputTextReturnOrder" style="width: 200px;" />
                                                    </td>

                                                </tr>


                                            </tbody>
                                        </table>
                                        <br />
                                        <div class="wms-srv-obj-main-grid pnlWmsDetail">
                                            <div class="wms-srv-grid-holder">
                                                <div class="wms-srv-grid-action-panel">
                                                    <div class="wms-srv-grid-title titleReturnOrder">
                                                        Requested Sku List
                                                    </div>
                                                </div>
                                                <div class="wms-srv-grid-scroller" style="height: auto;">
                                                    <div class="wms-srv-grid" id="viewS2SOrderGrid" style="text-align: center; min-width: auto; width: 100%;">
                                                    </div>
                                                    <%--<div style="float: right;">Total Return Qty : <span id="lblViewTotalQty">1</span></div>--%>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%-- View Order List End --%>

    <!-- Driver Assign Popup start -->
    <div class="wms-srv-popup-holder" id="wms-srv-AssignDriverre-popup" style="display: none; width: 85%; height: 87%; margin-left: 5%; margin-right: auto;">
        <div class="wms-srv-popup-bg">
        </div>
        <div class="wms-srv-popup" style="width: 100%; height: 100%; margin-left: auto; margin-right: auto;">
            <div class="wms-srv-popup-content">
                <!-- Popup Title -->
                <div class="wms-srv-popup-title">
                    <div class="wms-srv-page-title">
                        <i class="fas fa-file-alt"></i><span>Assign Driver</span>
                    </div>
                    <a href="#" title="Close" id="wms-srv-AssignDriverkj-popup-close" class="wms-srv-popup-close"><i class="fas fa-times-circle"></i></a>
                </div>
                <div class="wms-srv-grid-action-panel" style="border-bottom: solid 1px #cccccc !important">
                   <div class="wms-srv-grid-title ">

                        <div style="float: left;">
                            <span id="" class="lblReturnOrder titleReturnOrder">Truck Detail :</span>
                            <input name="" type="text" maxlength="50" id="txtDriverTruckNoS2S" class="inputTextReturnOrder notranslate" style="margin-right: 53px;">
                        </div>
                        <div style="float: left;">
                            <span id="" class="lblReturnOrder titleReturnOrder">Driver Name:</span>
                            <input name="" type="hidden" maxlength="50" id="hdnDriverId" value="" class="inputTextReturnOrder notranslate">
                            <input name="" type="text" maxlength="50" id="txtDriverList" disabled="" value="" class="inputTextReturnOrder notranslate">
                            <i class="fas fa-search iconView" onclick="showdriverlistPopup();"></i>
                        </div>
                        <button class="wms-srv-input wms-srv-button wms-srv-highlight-btn wms-srv-add-order" style="margin-left: 200px;" title="Save" type="button" onclick="SaveDriver();"><i class="fas fa-plus"></i><span>Save</span></button>
                    </div>
                    <br />

                    <!-- Popup Title -->
                    <div class="wms-srv-popup-content-body">

                        <!-- Grid -->
                        <div class="wms-srv-grid-holder pnlWmsHead">
                            <div class="wms-srv-grid" id="pnldriverlistAssign" style="text-align: center;">
                                <%--<div class="wms-srv-grid-header" style="white-space: nowrap;">
                                        <div class="wms-srv-grid-cell">Driver Name</div>
                                        <div class="wms-srv-grid-cell">Contact No</div>
                                        <div class="wms-srv-grid-cell">Email Id</div>
                                        <div class="wms-srv-grid-cell" style="text-align: center; width: 50px;">Action </div>
                                    </div>
                                    <div class="wms-srv-grid-row">
                                        <div class="wms-srv-grid-cell">Mohamad Rafi</div>
                                        <div class="wms-srv-grid-cell">9933737373</div>
                                        <div class="wms-srv-grid-cell">Muhash.moharoof@gmail.com</div>
                                        <div class="wms-srv-grid-cell">
                                            <div class="wms-srv-grid-action">
                                                <a href="#" title="Save" class="wms-srv-save" onclick=""><i class="fas fa-check-circle iconView"></i></a>
                                            </div>
                                        </div>
                                    </div>--%>
                            </div>
                            <!-- Grid -->
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Driver list Popup start -->
    <div class="wms-srv-popup-holder" id="wms-srv-list-Driverer-popup" style="display: none; width: 85%; height: 87%; margin-left: 5%; margin-right: auto;">
        <div class="wms-srv-popup-bg">
        </div>
        <div class="wms-srv-popup" style="width: 100%; height: 100%; margin-left: auto; margin-right: auto;">
            <div class="wms-srv-popup-content">
                <!-- Popup Title -->
                <div class="wms-srv-popup-title">
                    <div class="wms-srv-page-title">
                        <i class="fas fa-file-alt"></i><span>Driver list</span>
                    </div>
                    <a href="#" title="Close" id="wms-srv-list-Driversd-popup-close" class="wms-srv-popup-close"><i class="fas fa-times-circle"></i></a>
                    
                </div>
                <div class="wms-srv-search" style="float:right;margin-right: 18px;margin-top: 12px;">
                        <input type="text" value="" placeholder="Enter To Search" id="pnldriversearch" class="wms-srv-input wms-srv-simple-search"><a href="#"><i class="fas fa-search" onclick="S2SDriverlist();"></i></a>
                    </div>
                <div class="wms-srv-grid-action-panel" style="border-bottom: solid 1px #cccccc !important">

                    <br />

                    <!-- Popup Title -->
                    <div class="wms-srv-popup-content-body">

                        <!-- Grid -->
                        <div class="wms-srv-grid-holder pnlWmsHead">
                             
                            <div class="wms-srv-grid" id="pnldriverlister" style="text-align: center;">
                                <%--<div class="wms-srv-grid-header" style="white-space: nowrap;">
                                        <div class="wms-srv-grid-cell">Driver Name</div>
                                        <div class="wms-srv-grid-cell">Contact No</div>
                                        <div class="wms-srv-grid-cell">Email Id</div>
                                        <div class="wms-srv-grid-cell" style="text-align: center; width: 50px;">Action </div>
                                    </div>
                                    <div class="wms-srv-grid-row">
                                        <div class="wms-srv-grid-cell">Mohamad Rafi</div>
                                        <div class="wms-srv-grid-cell">9933737373</div>
                                        <div class="wms-srv-grid-cell">Muhash.moharoof@gmail.com</div>
                                        <div class="wms-srv-grid-cell">
                                            <div class="wms-srv-grid-action">
                                                <a href="#" title="Save" class="wms-srv-save" onclick=""><i class="fas fa-check-circle iconView"></i></a>
                                            </div>
                                        </div>
                                    </div>--%>
                            
                               
                                 </div>
                            <!-- Grid -->
                        </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Driver Assign list Popup start -->
    <div class="wms-srv-popup-holder" id="wms-srv-Assignlist-Driver-popup" style="display: none; width: 85%; height: 87%; margin-left: 5%; margin-right: auto;">
        <div class="wms-srv-popup-bg">
        </div>
        <div class="wms-srv-popup" style="width: 100%; height: 100%; margin-left: auto; margin-right: auto;">
            <div class="wms-srv-popup-content">
                <!-- Popup Title -->
                <div class="wms-srv-popup-title">
                    <div class="wms-srv-page-title">
                        <i class="fas fa-file-alt"></i><span>Driver list</span>
                    </div>
                    <a href="#" title="Close" id="wms-srv-Assignlist-Driver-popup-close" class="wms-srv-popup-close"><i class="fas fa-times-circle"></i></a>
                </div>
                <div class="wms-srv-grid-action-panel" style="border-bottom: solid 1px #cccccc !important">

                    <br />

                    <!-- Popup Title -->
                    <div class="wms-srv-popup-content-body">

                        <!-- Grid -->
                        <div class="wms-srv-grid-holder pnlWmsHead">
                            <div class="wms-srv-grid" id="pnlAssigndriverlist" style="text-align: center;">
                                <%--<div class="wms-srv-grid-header" style="white-space: nowrap;">
                                        <div class="wms-srv-grid-cell">Driver Name</div>
                                        <div class="wms-srv-grid-cell">Contact No</div>
                                        <div class="wms-srv-grid-cell">Email Id</div>
                                        <div class="wms-srv-grid-cell" style="text-align: center; width: 50px;">Action </div>
                                    </div>
                                    <div class="wms-srv-grid-row">
                                        <div class="wms-srv-grid-cell">Mohamad Rafi</div>
                                        <div class="wms-srv-grid-cell">9933737373</div>
                                        <div class="wms-srv-grid-cell">Muhash.moharoof@gmail.com</div>
                                        <div class="wms-srv-grid-cell">
                                            <div class="wms-srv-grid-action">
                                                <a href="#" title="Save" class="wms-srv-save" onclick=""><i class="fas fa-check-circle iconView"></i></a>
                                            </div>
                                        </div>
                                    </div>--%>
                            </div>
                            <!-- Grid -->
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Driver Assign list Popup End -->

    <!-- Order Detail Popup start -->
    <div class="wms-srv-popup-holder" id="wms-srv-Order-Detail-popup" style="display: none; width: 85%; height: 87%; margin-left: 5%; margin-right: auto;">
        <div class="wms-srv-popup-bg">
        </div>
        <div class="wms-srv-popup" style="width: 100%; height: 100%; margin-left: auto; margin-right: auto;">
            <div class="wms-srv-popup-content">
                <!-- Popup Title -->
                <div class="wms-srv-popup-title">
                    <div class="wms-srv-page-title">
                        <i class="fas fa-file-alt"></i><span>S2S Order Detail Report</span>
                    </div>
                    <a href="#" title="Close" id="wms-srv-Order-Detail-popup-close" class="wms-srv-popup-close"><i class="fas fa-times-circle"></i></a>
                </div>
                <br />
                <!-- Popup Title -->
                <div class="container-fluid">
                    <div class="row" style="display: flex">
                        <div class="col-sm-2" style="margin-left: 40px;">
                            <div class="su-input-group su-static-label" style="text-align: center;height: 40px;background-color: white;border-radius: initial;">
                                <label class="su-input-label"><b>Oms Order No</b></label>
                                <div class="" >
                                    <input type="text" id="txtOrderNo" placeholder="Search Oms Order No">
                                    
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-2" style="margin-left: 42px;">
                            <div class="su-input-group su-static-label">
                                <label for="ddlOrderStatus" class="su-input-label"><b>Status</b></label>
                                <select id="ddlOrderStatus" class="form-control notranslate" style="text-align: center;height: 40px;background-color: white;width: 189px">
                                    <option value="">--Select--</option>
                                    <option value="Pending for driver allocation">Pending for driver allocation</option>
                                    <option value="Driver Scheduled">Driver Scheduled</option>
                                    <option value="Pickup">Pickup</option>
                                    <option value="Drop">Drop</option>
                                    <option value="Cancelled">Cancelled</option>
                                </select>
                            </div>
                        </div>

                        <div class="col-sm-3" style="margin-left: 83px;">
                            <div class="su-input-group su-static-label">
                                <label class="su-input-label"><b>Select Date</b></label>
                                <div class="">
                                    <input type="text" id="txtFromToDate" placeholder="eg: From To To"
                                        onclick="getDate();" autocomplete="off" autocorrect="off" maxlength="25"
                                        step="1" noerror="true" staticlabel="true" animate="true" label="Symbol" style="width: 235px;"
                                        rules="" class="">
                                    <inout type="hidden" id="hideFromDate" value="">
                                </inout>
                                    <inout type="hidden" id="hideToDate" value="">
                                </inout>
                                </div>
                            </div>
                            <div class="dateGridView" id="dateSelecter" style="display: none; margin-left: 400px">
                                <i class="fas fa-times-circle" onclick="openDatePopup();"
                                    style="margin-left: 471px;"></i>
                                <div>

                                    <span>
                                        <lable class="txtFixDates" onclick="getPreviousdate(1);">
                                        prev. day
                                        |
                                    </lable>
                                    </span><i class="fa-solid fa-circle-xmark iconClose" title="Cancel"></i>
                                    <span>
                                        <lable class="txtFixDates" onclick="getSevenDateData(7);">
                                        last 7
                                        days |
                                    </lable>
                                    </span>
                                    <span>
                                        <lable class="txtFixDates" onclick="getThirtyDateData(30);">
                                        last 30
                                        days 
                                    </lable>
                                    </span>
                                    <!-- <span>
                                    <lable class="txtFixDates" onclick="getcurrentFYData('2023/04/24');">
                                        current FY
                                    </lable>
                                </span> -->
                                    <hr class="hrLine1">
                                </div>
                                <table class="dateGrid" id="gridDatePicker">

                                    <tr>
                                        <td>

                                            <div class="text-center">
                                                <strong>From</strong>
                                            </div>


                                            <div id="txt_FromdatePicker" class="txt_FromdatePicker">
                                            </div>
                                        </td>
                                        <td>
                                            <div class="text-center2">
                                                <strong>To</strong>
                                            </div>

                                            <div id="txt_TodatePicker">
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>

                        <div class="col-sm-1" style="margin-right: 116px; margin-top: 21px;">
                            <button type="button" class="btn-blue"
                                id="" style="float: right;" onclick="getOrderdetailReportlist();">
                                <span class="icon icon-arrow-right">
                                    <i class="fas fa-solid fa-arrow-right searchIcon"></i>
                                </span>
                            </button>
                           <%-- <i class="fas fa-solid fa-arrow-right searchIcon" onclick="getOrderdetailReportlist();"></i>--%>
                        </div>
                        
                    </div>
                    <br />
                    <div style="margin-left: 955px;">                      
                            <div class="table-download text-small"><span class="icon icon-download"></span>
                                Download:
                                <a href="#" data-balloon="Download complete results as Excel" data-balloon-pos="up">
                                </a><a href="#" data-balloon="Download complete results as Excel" data-balloon-pos="up"
                                    onclick="exportAtClient();">Excel</a>
                            </div>
                      </div>
                    <div class="reportHeader">
                                <div class="reportHeaderRow">
                                    <div class="reportHeaderCell" style="width:207px">
                                         <!--  <img src="../../../Images/GWC-Logo.png" height="60" /> -->
                                         <img src="../images/GWC-Logo.png" height="60" />
                                    </div>
                                    <div class="reportHeaderCell">
                                        <div class="reportHeaderAddress">
                                            D Ring Road - Doha, Qatar, P O Box 24434<br />
                                            <b>Tel:</b> +974 4449 3000, <b>Fax:</b> +974 4449 3100<br />
                                            <b>E-mail:</b> info@gulfwarehousing.com, <b>Website:</b>
                                            www.gilfwarehousing.com<br />
                                        </div>
                                    </div>
                                </div>
                            </div>
                    <div class="wms-srv-obj-main-grid pnlWmsDetail">
                        <div class="wms-srv-grid-holder">
                            <div class="wms-srv-grid-action-panel">
                                <div class="wms-srv-grid-title titleReturnOrder">
                                    S2S Order Detail List
                                </div>
                            </div>
                               <div class="wms-srv-grid-scroller" style="max-height: 300px;">
                                <div class="wms-srv-grid previewTable" id="gettopfiveorderobjrexta" style="text-align: center";>
                                </div>
                              </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>

        <!-- Order Detail Popup End -->
    </div>
</asp:Content>
