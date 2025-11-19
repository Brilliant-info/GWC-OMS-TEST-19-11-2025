<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/CRM.Master" AutoEventWireup="true" CodeBehind="S2SOrder.aspx.cs" Theme="Blue" Inherits="PowerOnRentwebapp.RMS.S2SOrder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<script src="../core-js/HttpLoader.js" language="javascript" type="text/javascript"></script>
    <link rel="stylesheet" href="Themes/bootstrap-4.0.0/dist/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous" type="text/javascript" />

    <script src="Themes/bootstrap-4.0.0/dist/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl"
        crossorigin="anonymous" type="text/javascript"></script>
    <script src="ThemeHTML/js/wms-theme.js" type="text/javascript"></script>
    <script src="ThemeHTML/js/wms-app-navigator.js" type="text/javascript"></script>
    <script src="../ThemeHTML/js/S2SOrder.js" type="text/javascript"></script>
    <style>
       #main {
        height: auto !important;
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
	   <div class="gridHeadButton">
        <button class="wms-srv-input  wms-srv-highlight-btn wms-srv-add-order" title="Allocated Driver" onclick="AssginDriver();" type="button"><i class="fas fa-plus"></i><span> Allocated Driver</span></button>
        <div class="dotStatsHead">

        <div class="wms-srv-dot wms-srv-green sts-btn-green" title="Completed" onclick=""></div><label> Completed</label>
        <div class="wms-srv-dot wms-srv-red sts-btn-red" title="Pending" onclick=""></div><label> Pending</label>
        <div class="wms-srv-dot wms-srv-yellow sts-btn-Yellow" title="Cancel Order" onclick=""></div><label> Cancel</label>
        </div>

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
                <div class="wms-srv-search">
                    <label>Search Filter: </label>
                    <select type="text" class="wms-srv-input wms-srv-search-filter" id="ddlReturnOrderColSearch">
                        <option value="">-- Select --</option>
                        <option value="ReturnID">Return No</option>      
                    </select>
               <input type="text" value="" class="wms-srv-input wms-srv-simple-search" id="ddlReturnFilterValue">
               <a href="#" data-prefix="SE"><i class="fas fa-search" onclick="searchReturnOrder(); return false;" style="margin-right: 17px;"></i></a>
               <a href="#" title="Cancel" class="wms-srv-cancel" data-prefix="CN"><i class="fas fa-times-circle" onclick="clearSearchFilter();"></i></a>
              </div>
                
                <button class="wms-srv-input wms-srv-button wms-srv-highlight-btn wms-srv-add-order" onclick="addNewReturnOrder();" title="Add New" type="button">
                <i class="fas fa-plus"></i><span>Add New</span></button>
                            <div class="wms-srv-grid-title">
                                S2S Order List
                            </div>
            </div>
       <div class="wms-srv-grid-scroller" style="max-height:300px;">
        <div class="wms-srv-grid" id="gridReturnOrderList">
   
            <div class="wms-srv-grid-header">
              <div class="wms-srv-grid-cell"><input type="checkbox" onclick="" class="messageCheckbox"/></div>
              <div class="wms-srv-grid-cell">S2S NO.</div>
              <div class="wms-srv-grid-cell">Wincash Order No.</div>
			  <div class="wms-srv-grid-cell">Order Type</div>
			  <div class="wms-srv-grid-cell">Source Store</div>
			  <div class="wms-srv-grid-cell">Destination Store</div>
			  <div class="wms-srv-grid-cell">Performed By</div>
			  <div class="wms-srv-grid-cell">Received By</div>
			  <div class="wms-srv-grid-cell">Order Status</div>
			  <div class="wms-srv-grid-cell">Driver Allocation</div>
			  <div class="wms-srv-grid-cell">Collection</div>
			  <div class="wms-srv-grid-cell">Delivered</div>
              <div class="wms-srv-grid-cell">Action</div>
            </div>

            <div class="wms-srv-grid-row">
              <div class="wms-srv-grid-cell"><input type="checkbox" onclick="" class="messageCheckbox" /></div>
                  <div class="wms-srv-grid-cell" style="text-align:center;">S51712</div>
                  <div class="wms-srv-grid-cell" style="text-align:center;">WN35241</div>
                  <div class="wms-srv-grid-cell" style="text-align:center;">WinCash order</div>
                  <div class="wms-srv-grid-cell" style="text-align:center;">Vellagio Store</div>
                  <div class="wms-srv-grid-cell" style="text-align:center;">City Center</div>
                  <div class="wms-srv-grid-cell" style="text-align:center;">Javed Qureshi</div>
                  <div class="wms-srv-grid-cell" style="text-align:center;">Akram</div>
		          <div class="wms-srv-grid-cell" style="text-align:center;">Pending For Driver Allocation</div>
                  <div class="wms-srv-grid-cell wms-srv-status-progress">
                     <div class="wms-srv-dot wms-srv-red"  title="Driver Allocation Status" onclick=""></div>
                  </div>
                  <div class="wms-srv-grid-cell wms-srv-status-progress">
                     <div class="wms-srv-dot wms-srv-red" title="Collection Status" onclick=""></div>
                  </div>
                  <div class="wms-srv-grid-cell wms-srv-status-progress">
                     <div class="wms-srv-dot wms-srv-red" title="Delivered" onclick=""></div>
                  </div>
                <div class="wms-srv-grid-cell">
                <div class="wms-srv-grid-action">
                    <i class="fas fa-eye iconView" title="View Order" onclick="showViewPopup();"></i>   
                        <div class="wms-srv-action-sep">|</div>
                    <i class="fas fa-file-alt iconView"  title="Document" onclick="showDocumentPopup();"></i>
                        <div class="wms-srv-action-sep">|</div>
                    <a href="#" title="" class="wms-srv-icononly" onclick="showOrderHistoryPopup();"><i class="fas fa-tasks iconView" title="Order History"></i></a>                   
                </div>
                </div>
             </div>
   
             
      </div>
    </div>

      <div class="wms-srv-grid-pager" id="tbltranferlistpager"> 1-0 of 0 Records <span class="wms-srv-empty-space"></span><b>Go to Page: </b> 
          <select id="ddlGridPageNo" class="wms-srv-input ddlGridPageNo" style="width:100px;">
              <option>1</option></select><a href="#" title="Jump To" class="wms-srv-ddlpager-go" style="font-size:16px;"><i class="fas fa-play-circle"></i></a>

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
                                        <div class="wms-srv-grid" id="wmsGridDocumentList" style="text-align: center; min-width: auto; width: 100%;">
                                            <div class="wms-srv-grid-header">
                                                <div class="wms-srv-grid-cell">Document Title <span class="requiredStar" style="color: red;">*</span></div>
                                                <div class="wms-srv-grid-cell">Description <span class="requiredStar" style="color: red;">*</span></div>

                                                <div class="wms-srv-grid-cell">Document Type <span class="requiredStar" style="color: red;">*</span></div>
                                                <div class="wms-srv-grid-cell">File Type <span class="requiredStar" style="color: red;">*</span></div>
                                                <%-- <div class="wms-srv-grid-cell">Attachment Image <span class="requiredStar" style="color: red;">*</span></div>--%>
                                                <div class="wms-srv-grid-cell">Attachment <span class="requiredStar" style="color: red;">*</span></div>

                                                <div class="wms-srv-grid-cell">Action</div>
                                            </div>

                                            <%--input tag document--%>

                                            <div class="wms-srv-grid-row wms-srv-grid-add">
                                                <div class="wms-srv-grid-cell">
                                                    <input name="" type="text" maxlength="50" id="txtCommonDocument_Title" class="inputTextReturnOrder notranslate" style="width: 200px; margin-left: auto; margin-right: auto;" />
                                                </div>
                                                <div class="wms-srv-grid-cell">
                                                    <input name="" type="text" maxlength="50" id="txtCommonDocument_Description" class="inputTextReturnOrder notranslate" style="width: 200px; margin-left: auto; margin-right: auto;" />
                                                </div>
                                                <!-- <div class="wms-srv-grid-cell">
                                                     <input type="text" data-for="KeyWords"></div> -->

                                                <div class="wms-srv-grid-cell">
                                                    <input name="" type="text" maxlength="50" id="txtCommonDocument_Type" class="inputTextReturnOrder notranslate" style="width: 200px; margin-left: auto; margin-right: auto;" />
                                                </div>
                                                <div class="wms-srv-grid-cell">

                                                    <input type="text" data-for="FileType" id="txtCommonDocument_FileType" class="inputTextReturnOrder notranslate" disabled="disabled" style="width: 200px; margin-left: auto; margin-right: auto;">
                                                </div>
                                                <div class="wms-srv-grid-cell" id="txtCommonDocument_Name">
                                                </div>
                                                <div class="wms-srv-grid-cell">
                                                    <div class="wms-srv-grid-action">
                                                        <div class="wms-srv-file-upload-holder">
                                                            <label for="wms-srv-cmDocument-file-upload"><i class="fas fa-paperclip"></i><span class="wms-srv-badge" style="display: none;">0</span></label>
                                                            <div class="wms-srv-action-sep">|</div>
                                                            <input type="file" id="wms-srv-cmDocument-file-upload" onchange="uploadDocumentAttachment();" style="display: none;" />
                                                        </div>
                                                        <a href="#" title="Save" class="wms-srv-save" onclick="saveDocumentObject();"><i class="fas fa-check-circle"></i></a>
                                                    </div>
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
                            <div class="wms-srv-popup-holder" id="wms-srv-OrderHistory-popup" style="display:none;width: 85%; height: 87%; margin-left: 5%; margin-right: auto;">
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
                                        <div class="wms-srv-popup-content-body" >
                                     
                                         <!-- Grid -->
                                             <div class="wms-srv-grid-holder pnlWmsHead">                                            
                                               <div class="wms-srv-grid" id="srvgridOrderHistory" style="text-align: center;">
                                                    <div class="wms-srv-grid-header" style="white-space: nowrap;">
                                                        <div class="wms-srv-grid-cell">Order No</div>
                                                        <div class="wms-srv-grid-cell">Status</div>
                                                        <div class="wms-srv-grid-cell">Date</div>
                                                        <div class="wms-srv-grid-cell">Updated By</div>
                                                    </div>
                                                    <div class="wms-srv-grid-row">
                                                        <div class="wms-srv-grid-cell">25896</div>
                                                        <div class="wms-srv-grid-cell">In Process</div>
                                                        <div class="wms-srv-grid-cell">03-10-2023</div>
                                                        <div class="wms-srv-grid-cell">User 1</div>
                                                    </div>
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

                                                            <input name="" type="text" maxlength="50" value="Loc01" id="viewWinCashOrderNo" disabled class="inputTextReturnOrder" style="width: 200px;" />
                                                        </td>
                                                        <td>
                                                            <span id="" class="lblReturnOrder">OMS Order No:</span>
                                                        </td>
                                                        <td>
                                                            <input name="" type="text" maxlength="50" value="OCRREF1" id="viewomsorderno" disabled class="inputTextReturnOrder" style="width: 200px;" />

                                                        </td>
                                                        <td>
                                                            <span id="" class="lblReturnOrder">Source Store:</span>
                                                        </td>
                                                        <td>
                                                            <input name="" type="text" maxlength="50" value="01256" id="viewsourcestore" disabled class="inputTextReturnOrder" style="width: 200px;" />

                                                        </td>
                                                    </tr>

                                                    <%--second row--%>
                                                    <tr>
                                                        <td>
                                                            <span id="" class="lblReturnOrder">Status :</span>

                                                        </td>
                                                        <td>
                                                            <input name="" type="text" maxlength="50" value="In Transit" id="views2sorderStatus" disabled class="inputTextReturnOrder" style="width: 200px;" />
                                                        </td>
                                                        <td>
                                                            <span id="" class="lblReturnOrder">Creation Date :</span>
                                                        </td>
                                                        <td>
                                                            <input name="" type="text" maxlength="50" value="09/10/2023" id="viewCreationDate" disabled class="inputTextReturnOrder" style="width: 200px;" />
                                                        </td>
                                                        <td>
                                                            <span id="" class="lblReturnOrder">Destination Store :</span>
                                                        </td>
                                                        <td>
                                                            <input name="" type="text" maxlength="50" value="City Center" id="viewDestinationStore" disabled class="inputTextReturnOrder" style="width: 200px;" />
                                                        </td>
                                                    </tr>

                                                    <%--third row--%>
                                                    <tr>
                                                        <td>

                                                            <span id="" class="lblReturnOrder">Performed By :</span>

                                                        </td>
                                                        <td>
                                                            <input name="" type="text" maxlength="50" value="Vodafone Commercial" id="viewperformedby" disabled class="inputTextReturnOrder" style="width: 200px;" />
                                                        </td>
                                                        <td>
                                                            <span id="" class="lblReturnOrder">Received By :</span>
                                                        </td>
                                                        <td>
                                                            <input name="" type="text" maxlength="50" value="VFQ VBS" id="viewreceivedBy" disabled class="inputTextReturnOrder" style="width: 200px;" />
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
                                                        <div class="wms-srv-grid" id="viewReturnOrderGrid" style="text-align: center; min-width: auto; width: 100%;">
                                                            <div class="wms-srv-grid-header" style="white-space: nowrap; text-align: center">
                                                                <div class="wms-srv-grid-cell" style="width: 60px;">SR. No.</div>
                                                                <div class="wms-srv-grid-cell">SKU Code</div>
                                                                <div class="wms-srv-grid-cell">SKU Name</div>
                                                                <div class="wms-srv-grid-cell">Serial No</div>
                                                                <div class="wms-srv-grid-cell">Order Quantity</div>
                                                            </div>
                                                            <div class="wms-srv-grid-row">
                                                                <div class="wms-srv-grid-cell">1</div>
                                                                <div class="wms-srv-grid-cell">SKU001</div>
                                                                <div class="wms-srv-grid-cell">173773</div>
                                                                <div class="wms-srv-grid-cell">P1343222</div>
                                                                <div class="wms-srv-grid-cell">0</div>
                                                            </div>
                                                            <div class="wms-srv-grid-row">
                                                                <div class="wms-srv-grid-cell">2</div>
                                                                <div class="wms-srv-grid-cell">SKU002</div>
                                                                <div class="wms-srv-grid-cell">173773</div>
                                                                <div class="wms-srv-grid-cell">P1</div>
                                                                <div class="wms-srv-grid-cell">20</div>
                                                            </div>

                                                        </div>
                                                        <div style="float: right;">Total Return Qty : <span id="lblViewTotalQty">1</span></div>
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
        <%--View Order List End --%>

        <!-- Driver Assign Popup start -->
        <div class="wms-srv-popup-holder" id="wms-srv-AssignDriver-popup" style="display: none; width: 85%; height: 87%; margin-left: 5%; margin-right: auto;">
            <div class="wms-srv-popup-bg">
            </div>
            <div class="wms-srv-popup" style="width: 100%; height: 100%; margin-left: auto; margin-right: auto;">
                <div class="wms-srv-popup-content">
                    <!-- Popup Title -->
                    <div class="wms-srv-popup-title">
                        <div class="wms-srv-page-title">
                            <i class="fas fa-file-alt"></i><span>Assign Driver</span>
                        </div>
                        <a href="#" title="Close" id="wms-srv-AssignDriver-popup-close" class="wms-srv-popup-close"><i class="fas fa-times-circle"></i></a>
                    </div>
                    <div class="wms-srv-grid-action-panel" style="border-bottom: solid 1px #cccccc !important">
                        <div class="wms-srv-grid-title titleReturnOrder">

                            <div style="float: left;">
                                <span id="" class="lblReturnOrder">Truck Detail :</span>
                                <input name="" type="text" maxlength="50" id="txtDriverTruckNo" class="inputTextReturnOrder notranslate" style="margin-right: 53px;">
                            </div>
                            <div style="float: left;">
                                <span id="" class="lblReturnOrder">Driver Name:</span>
                                <input name="" type="hidden" maxlength="50" id="hdnDriverId" value="" class="inputTextReturnOrder notranslate">
                                <input name="" type="text" maxlength="50" id="txtDriverList" value="" class="inputTextReturnOrder notranslate">
                                <i class="fas fa-search iconView" onclick="DriverList();"></i>
                            </div>
                            <button class="wms-srv-input wms-srv-button wms-srv-highlight-btn wms-srv-add-order" style="margin-left: 200px;" title="Save" type="button" onclick="AssignDriver();"><i class="fas fa-plus"></i><span>Save</span></button>
                        </div>
                        <br />

                        <!-- Popup Title -->
                        <div class="wms-srv-popup-content-body">

                            <!-- Grid -->
                            <div class="wms-srv-grid-holder pnlWmsHead">
                                <div class="wms-srv-grid" id="wms-srv-grid-DriverListPopup" style="text-align: center;">
                                    <div class="wms-srv-grid-header" style="white-space: nowrap;">
                                        <div class="wms-srv-grid-cell">Driver Name</div>
                                        <div class="wms-srv-grid-cell">Contact No</div>
                                        <div class="wms-srv-grid-cell">Email Id</div>
                                        <div class="wms-srv-grid-cell" style="text-align: center; width: 50px;">Action </div>

                                    </div>
                                    <div class="wms-srv-grid-row">
                                        <div class="wms-srv-grid-cell"></div>
                                        <div class="wms-srv-grid-cell"></div>
                                        <div class="wms-srv-grid-cell"></div>
                                        <div class="wms-srv-grid-cell">
                                            <%--<div class="wms-srv-grid-action">


                              <a href="#" title="Save" class="wms-srv-save" onclick=""><i class="fas fa-check-circle iconView"></i></a>
                              <div class="wms-srv-action-sep">|</div>

                              <a href="#" title="Save" class="wms-srv-save" onclick=";"><i class="fas fa-times-circle iconView"></i></a>
                          </div>--%>
                                        </div>

                                    </div>
                                </div>


                                <!-- Grid -->

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

	</div>
</asp:Content>
