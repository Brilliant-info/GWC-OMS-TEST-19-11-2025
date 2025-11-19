<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/CRM.Master" AutoEventWireup="true" CodeBehind="ReturnOrder.aspx.cs" Theme="Blue" Inherits="PowerOnRentwebapp.RMS.ReturnOrder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  
    <%--<script src="../MasterPage/JavaScripts/jquery-3.1.1.min.js" type="text/javascript"></script>--%>

    <script src="../core-js/HttpLoader.js" language="javascript" type="text/javascript"></script>
    <link rel="stylesheet" href="Themes/bootstrap-4.0.0/dist/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous" type="text/javascript" />

    <script src="Themes/bootstrap-4.0.0/dist/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl"
        crossorigin="anonymous" type="text/javascript"></script>
    <script src="ThemeHTML/js/wms-theme.js" type="text/javascript"></script>
    <script src="ThemeHTML/js/wms-app-navigator.js" type="text/javascript"></script>
     <script src="../ThemeHTML/js/returnOrder.js" type="text/javascript"></script>
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
        <!-- Content Area -->

        <div class="container-fluid wms-srv-dashboard-panel">
            <div class="row">
                <div class="col-md-4 nopadding">
                    <!-- Page Title -->
                    <div class="wms-srv-page-title">
                        <i class="fas fa-exchange-alt"></i><span class="wms-srv-data-title">Return Order</span>
                    </div>
                    <!-- Page Title -->
                </div>
                <div class="col-md-8 nopadding">
                </div>
            </div>
        </div>
        <div class="gridHeadButton">
            <button class="wms-srv-input wms-srv-highlight-btn wms-srv-add-order" onclick="cancelOrder();" title="Cancel Order" type="button"><i class="fas fa-times-circle"></i><span>Cancel</span></button>
            <button class="wms-srv-input wms-srv-highlight-btn wms-srv-add-order" onclick="showExcelImport();" title="Return Import" type="button"><i class="fas fa-plus"></i><span>Return Import</span></button>
            <button class="wms-srv-input  wms-srv-highlight-btn wms-srv-add-order" title="Allocated Driver" onclick="AssginDriver();" type="button"><i class="fas fa-plus"></i><span>Allocated Driver</span></button>
            <div class="dotStatsHead">

                <div class="wms-srv-dot wms-srv-green sts-btn-green" title="Completed" onclick=""></div>
                <label>Completed</label>
                <div class="wms-srv-dot wms-srv-red sts-btn-red" title="Pending" onclick=""></div>
                <label>Pending</label>
                <div class="wms-srv-dot wms-srv-yellow sts-btn-Yellow" title="Cancel Order" onclick=""></div>
                <label>Cancel</label>
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

        <!-- Grid -->
        <div class="wms-srv-obj-main-grid pnlWmsDetail">
            <div class="wms-srv-grid-holder">
                <div class="wms-srv-grid-action-panel">
                    <div class="wms-srv-grid-common-action"><a href="#" class="wms-srv-icononly"><i class="fas fa-list"></i></a></div>
                    <div class="wms-srv-search">
                        <label>Search Filter: </label>
                        <select type="text" class="wms-srv-input wms-srv-search-filter" id="ddlReturnOrderColSearch">
                            <option value="">-- Select --</option>
                            <option value="ReturnID">Return No</option>
                            <%--  <option value="storeid">Department</option>--%>
                            <option value="returndate">Return Date</option>
                            <option value="referenceNo">OMS Ref.Number</option>
                            <option value="Priority">Priority</option>
                            <%--    <option value="returnCategory">Return Category</option>--%>
                            <option value="status">Order Status</option>

                        </select>
                        <input type="text" value="" class="wms-srv-input wms-srv-simple-search" id="ddlReturnFilterValue">
                        <a href="#" data-prefix="SE"><i class="fas fa-search" onclick="searchReturnOrder(); return false;" style="margin-right: 17px;"></i></a>
                        <a href="#" title="Cancel" class="wms-srv-cancel" data-prefix="CN"><i class="fas fa-times-circle" onclick="clearSearchFilter();"></i></a>
                    </div>

                    <button class="wms-srv-input wms-srv-button wms-srv-highlight-btn wms-srv-add-order" onclick="addNewReturnOrder();" title="Add New" type="button">
                        <i class="fas fa-plus"></i><span>Add New</span></button>
                    <div class="wms-srv-grid-title">
                        Return Order List
                    </div>
                </div>
                <div class="wms-srv-grid-scroller" style="max-height: 300px;">
                    <div class="wms-srv-grid" id="gridReturnOrderList">

                        <div class="wms-srv-grid-header">
                            <div class="wms-srv-grid-cell">
                                <input type="checkbox" onclick="" class="messageCheckbox" /></div>
                            <div class="wms-srv-grid-cell">Return No</div>
                            <div class="wms-srv-grid-cell">Department</div>
                            <div class="wms-srv-grid-cell">Return Date</div>
                            <div class="wms-srv-grid-cell">OMS REf.Number</div>
                            <div class="wms-srv-grid-cell">Priority</div>
                            <div class="wms-srv-grid-cell">Return Category</div>
                            <div class="wms-srv-grid-cell">Order Status</div>
                            <div class="wms-srv-grid-cell" style="width: 100px;">Driver Allocation</div>
                            <div class="wms-srv-grid-cell" style="width: 100px;">Collection In Progress</div>
                            <div class="wms-srv-grid-cell" style="width: 100px;">Rec.In Warehouse</div>
                            <div class="wms-srv-grid-cell">Action</div>
                        </div>

                        <%-- <div class="wms-srv-grid-row">
           <div class="wms-srv-grid-cell"><input type="checkbox" onclick="" class="messageCheckbox" /></div>
                  <div class="wms-srv-grid-cell" style="text-align:center;">51712</div>
                  <div class="wms-srv-grid-cell" style="text-align:center;">Order Return</div>
                  <div class="wms-srv-grid-cell" style="text-align:center;">28/09/2023</div>
                  <div class="wms-srv-grid-cell" style="text-align:center;">15525712</div>
                  <div class="wms-srv-grid-cell" style="text-align:center;">Low</div>
                  <div class="wms-srv-grid-cell" style="text-align:center;">UserReturn</div>
                  <div class="wms-srv-grid-cell" style="text-align:center;">Return</div>
                  <div class="wms-srv-grid-cell wms-srv-status-progress">
                     <div class="wms-srv-dot wms-srv-red"  title="Driver Allocation Status" onclick=""></div>
                  </div>
                  <div class="wms-srv-grid-cell wms-srv-status-progress">
                     <div class="wms-srv-dot wms-srv-red" title="Collection Status" onclick=""></div>
                  </div>
                  <div class="wms-srv-grid-cell wms-srv-status-progress">
                     <div class="wms-srv-dot wms-srv-red" title="Rec.In Warehouse Status" onclick=""></div>
                  </div>
          <div class="wms-srv-grid-cell">
                <div class="wms-srv-grid-action">
                      <i class="fas fa-eye iconView" title="Edit Order" onclick="ViewOrderList();"></i>   
                     <div class="wms-srv-action-sep">|</div>
                     <i class="fas fa-edit iconView" title="Edit Order" onclick="EditReturnOrder();"></i>                    
                        <div class="wms-srv-action-sep">|</div>
          <i class="fas fa-file-alt iconView"  title="Document" onclick="showDocumentPopup(204510,'RMSDocument');"></i>
                    <div class="wms-srv-action-sep">|</div>
                    <a href="#" title="" class="wms-srv-icononly" onclick="showOrderHistoryPopup();"><i class="fas fa-tasks iconView" title="Order History"></i></a>                   
                </div>
                </div>
   </div>--%>
                    </div>
                </div>

                <div class="wms-srv-grid-pager" id="tbltranferlistpager">
                    1-0 of 0 Records <span class="wms-srv-empty-space"></span><b>Go to Page: </b>
                    <select id="ddlGridPageNo" class="wms-srv-input ddlGridPageNo" style="width: 100px;">
                        <option>1</option>
                    </select><a href="#" title="Jump To" class="wms-srv-ddlpager-go" style="font-size: 16px;"><i class="fas fa-play-circle"></i></a>

                </div>
            </div>
            <!-- Content Area End -->
        </div>

        <!-- Add Transfer Popup with Scan Start -->
        <div class="wms-srv-popup-holder" id="wms-srv-addtransfer-popup" style="display: none;">
            <div class="wms-srv-popup-bg">
            </div>
            <div class="wms-srv-popup">
                <div class="wms-srv-popup-content">
                    <!-- Popup Title -->
                    <div class="wms-srv-popup-title">
                        <div class="wms-srv-page-title">
                            <i class="fas fa-file-alt"></i><span>Return Order</span>
                        </div>
                        <a href="#" title="Close" id="wms-srv-addtransfer-popup-close" class="wms-srv-popup-close"><i class="fas fa-times-circle"></i></a>
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
                                            <div class="titleReturnOrder">Request</div>
                                            <div class="btnRetunrOrder">

                                                <%--<button class="wms-srv-input wms-srv-button wms-srv-highlight-btn wms-srv-add-order" title="Allocated Driver" type="button"><i class="fas fa-plus"></i><span> Add New</span></button>--%>
                                                <button class="wms-srv-input wms-srv-button wms-srv-highlight-btn wms-srv-add-order" title="Save" type="button" onclick="finalSaveReturnOrder();"><i class="fas fa-plus"></i><span>Save</span></button>
                                            </div>

                                            <table class="tableForm" style="width: 100%;">
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <req><span id="" class="lblReturnOrder">Title  :</span>  </req>

                                                        </td>
                                                        <td>
                                                            <input name="" type="text" maxlength="50" id="txtAddTitle" class="inputTextReturnOrder" style="width: 200px;" />
                                                        </td>
                                                        <td>
                                                            <req>
                        <span id="" class="lblReturnOrder">Priority :</span>
                    </req>

                                                        </td>
                                                        <td>
                                                            <select class="inputTextReturnOrder" id="txtAddPriority" style="width: 200px; height: 23px;">
                                                                <option value="0">-- Select Priority --</option>
                                                                <option value="1">1</option>
                                                                <option value="2">2</option>
                                                                <option value="3">3</option>
                                                            </select>

                                                        </td>
                                                        <td>
                                                            <span id="" class="lblReturnOrder">Return Id:</span>
                                                        </td>
                                                        <td>
                                                            <input name="" type="text" maxlength="50" value="To be generated" disabled id="setAddReturnOrderId" class="inputTextReturnOrder" style="width: 200px;" />
                                                        </td>
                                                    </tr>

                                                    <%--second row--%>
                                                    <tr>
                                                        <td>
                                                            <span id="" class="lblReturnOrder">Status :</span>

                                                        </td>
                                                        <td>
                                                            <input name="" type="hidden" id="txtAddStatusId" value="10042" />
                                                            <input name="" disabled type="text" maxlength="50" placeholder="Pending for driver allocation" class="inputTextReturnOrder" style="width: 200px;" />
                                                        </td>
                                                        <td>
                                                            <req> <span id="" class="lblReturnOrder">Return Date :</span> </req>
                                                        </td>
                                                        <td>
                                                            <input type="text" value="" onchange="expDateSetasRetrn();" class="wms-srv-grid-cell-input wms-srv-datepicker" style="width: 200px;" id="txtAddReturnDate" />
                                                        </td>
                                                        <td>
                                                            <req><span id="" class="lblReturnOrder">Exp.Date Of Collection :</span></req>
                                                        </td>
                                                        <td>
                                                            <input type="text" value="" onchange="expDateCollectionDate();" class="wms-srv-grid-cell-input wms-srv-datepicker" style="width: 200px;" id="txtAddExpDateDate" />
                                                        </td>
                                                    </tr>

                                                    <%--third row--%>
                                                    <tr>
                                                        <td>
                                                            <req>
                        <span id="" class="lblReturnOrder">Customer Name :</span>
                    </req>

                                                        </td>
                                                        <td>
                                                            <select onchange="AddDepartmentListddl();" style="width: 200px; height: 23px;" id="ddlCustomerList" class="">
                                                                <option>-- Select Customer --</option>

                                                            </select>
                                                        </td>
                                                        <td>
                                                            <req><span id="" class="lblReturnOrder">Department  :</span> </req>
                                                        </td>
                                                        <td>
                                                            <select style="width: 200px; height: 23px;" onclick="checkCustomerValidation();" onchange="bidnDepartmentWiseUserlist();" id="ddlDepartmentList" data-val="" class="">
                                                                <%--<option value="">-- Select Department --</option>--%>
                                                            </select>
                                                        </td>
                                                        <td>
                                                            <span id="" class="lblReturnOrder">OMS Order Ref.No:</span>
                                                        </td>
                                                        <td>
                                                            <input name="" type="text" maxlength="50" id="txtAddOMSOrderRefNo" class="inputTextReturnOrder" value="" style="width: 200px;" />
                                                            <a href="#"><i class="fas fa-search" onclick="getRefOrderData();"></i></a>
                                                        </td>
                                                    </tr>

                                                    <%--Fourth Row--%>
                                                    <tr>
                                                        <td>
                                                            <req><span id="" class="lblReturnOrder">Location Code :</span></req>
                                                        </td>
                                                        <td>
                                                            <input type="hidden" id="hdnAddLocationId" />
                                                            <input name="" type="text" maxlength="100" value="" id="txtAddLocationCode" readonly class="inputTextReturnOrder" style="width: 200px;" />
                                                            <a href="#"><i class="fas fa-search" onclick="LocationList();"></i></a>
                                                        </td>

                                                        <td>
                                                            <req><span id="" class="lblReturnOrder">Location Name :</span></req>
                                                        </td>
                                                        <td>
                                                            <%-- <input type="hidden" id="hdnAddLocationId" />--%>
                                                            <input name="" disabledtype="text" maxlength="100" value="" id="txtAddLocationName" readonly class="inputTextReturnOrder" style="width: 200px;" />
                                                            <%--  <a href="#"><i class="fas fa-search" onclick="LocationList();"></i></a>--%>
                                                        </td>


                                                        <td>
                                                            <req><span id="" class="lblReturnOrder">Requested By :</span> </req>
                                                        </td>
                                                        <td>
                                                            <select class="inputTextReturnOrder" onclick="checkCustomerValidation();" id="ddlDeptWiseUserList" style="width: 200px; height: 23px;">
                                                                <option value=""></option>
                                                            </select>

                                                        </td>
                                                        <%--<td>
                    <req><span  class="lblReturnOrder">Return Category :</span></req>
                </td>
                <td>
                        <select id="ddlReturnCategory" class="inputTextReturnOrder" style="width:200px;height:23px;">
                                <option>Product Not Good</option>
                                <option>Warranty Return</option>
                                <option>Product Wrong</option>
                    </select> 
                </td>--%>   <%--cmd by suraj khopade--%>
                                                    </tr>

                                                    <%--fifth Row--%>

                                                    <tr>
                                                        <td>
                                                            <req><span  class="lblReturnOrder">Return Category :</span></req>
                                                        </td>
                                                        <td>
                                                            <select id="ddlReturnCategory" class="inputTextReturnOrder" style="width: 200px; height: 23px;">
                                                                <option>Product Not Good</option>
                                                                <option>Warranty Return</option>
                                                                <option>Product Wrong</option>
                                                            </select>
                                                        </td>

                                                        <td>
                                                            <req><span  class="lblReturnOrder">Condition :</span> </req>
                                                        </td>
                                                        <td>
                                                            <select id="ddlReturnCondition" class="inputTextReturnOrder" style="width: 200px; height: 23px;">
                                                                <option>Sealed</option>
                                                                <option>Pack</option>
                                                                <option>Open</option>
                                                            </select>
                                                        </td>
                                                        <td>


                                                            <span id="" class="lblReturnOrder">Remark :</span>


                                                        </td>
                                                        <td>
                                                            <input name="" type="text" maxlength="50" id="txtAddRemark" class="inputTextReturnOrder" style="width: 300px;" />
                                                        </td>

                                                        <td>


                                                            <span id="locadd" class="lblReturnOrder" visible="false">Address :</span>


                                                        </td>
                                                        <td>
                                                            <input name="" type="text" maxlength="50" id="txtAddress" visible="false" class="inputTextReturnOrder" style="width: 300px;" />
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
                                                        <%--       <button class="wms-srv-input "><i class="fas fa-plus"></i><span>Add Item To List</span>
                                        </button> --%>
                                                        <button class="wms-srv-input wms-srv-button wms-srv-highlight-btn wms-srv-add-order" id="btnshwoHideSku" onclick="showSkuPopup();" title="Add SKU" type="button"><i class="fas fa-plus"></i>Add Item To List</button>
                                                    </div>
                                                    <div class="wms-srv-grid-scroller" style="height: auto;">
                                                        <div class="wms-srv-grid" id="wmsSkuDetailsSalesOrderGrid" style="text-align: center; min-width: auto; width: 100%;">
                                                            <div class="wms-srv-grid-header" style="white-space: nowrap; text-align: center">


                                                                <%--    <div class="wms-srv-grid-cell" style="width:60px;">SR. No.</div>--%>

                                                                <div class="wms-srv-grid-cell">SKU Name</div>
                                                                <div class="wms-srv-grid-cell">SKU Code</div>
                                                                <div class="wms-srv-grid-cell">UOM</div>
                                                                <div class="wms-srv-grid-cell">Order Quantity</div>
                                                                <div class="wms-srv-grid-cell">Actual Return Qty</div>
                                                                <div class="wms-srv-grid-cell">Return Quantity</div>
                                                                <div class="wms-srv-grid-cell" style="text-align: center;">Action</div>
                                                            </div>
                                                            <%--<div class="wms-srv-grid-row">
             
                    <div class="wms-srv-grid-cell">1</div>
                       <div class="wms-srv-grid-cell">SKU01</div>
                    <div class="wms-srv-grid-cell">173773</div>
                    <div class="wms-srv-grid-cell">P1</div>
                    <div class="wms-srv-grid-cell"><input type="text" value="50"/></div>
                     <div class="wms-srv-grid-cell">100</div>
                    <div class="wms-srv-grid-cell"><input type="text" value="5"/></div>
                    <div class="wms-srv-grid-cell">
                        <div class="wms-srv-grid-action">
                        
                            <a href="#" title="Cancel" class="wms-srv-cancel"><i class="fas fa-times-circle"></i></a>

                        </div>

                    </div>
         
                </div>--%>
                                                        </div>
                                                        <div class="lblTotalQty">Total Return Qty : <span id="setTotalRetrnQtylbl">0</span></div>
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

        <!-- Add Transfer Popup with Scan -->

        <%--Sku List Popup Start--%>

        <div class="wms-srv-popup-holder" id="wms-srv-skusearch-popup" style="display: none; width: 85%; height: 87%; margin-left: 5%; margin-right: auto;">
            <div class="wms-srv-popup-bg">
            </div>
            <div class="wms-srv-popup" style="width: 100%; height: 100%; margin-left: auto; margin-right: auto;">
                <div class="wms-srv-popup-content">
                    <!-- Popup Title -->
                    <div class="wms-srv-popup-title">
                        <div class="wms-srv-page-title">
                            <i class="fas fa-solid fa-box"></i><span>SKU Search</span>
                        </div>
                        <a href="#" title="Close" id="wms-srv-skusearch-popup-close" class="wms-srv-popup-close"><i class="fas fa-times-circle" onclick="CloseSkuSearchPopup();"></i></a>
                    </div>
                    <!-- Popup Title -->
                    <div class="wms-srv-popup-content-body" id="wms-srv-popup-content-body">
                        <!-- Grid 2-->
                        <div class="wms-srv-grid-holder pnlWmsDetail" id="pnlWmsDetail">
                            <div class="wms-srv-grid-action-panel">
                                <div class="wms-srv-grid-common-action"><a href="#" title="Customize Grid" class="wms-srv-icononly"><i class="fas fa-edit"></i></a></div>
                                <div class="wms-srv-search" style="margin-right: 100px;">
                                    <input type="text" value="" placeholder="Enter SKU Code" id="txtSearchSkuVal" class="wms-srv-input wms-srv-simple-search" />
                                    <a href="#"><i class="fas fa-search" style="padding-right: 25px;" onclick="getSkuList();"></i></a>
                                    <a href="#" title="Close" id="wms-srv-SkU-popup-close" onclick="ClearSearchSkuList();" class="wms-srv-popup-close" style="font-size: 15px;"><i class="fas fa-times-circle"></i></a>
                                </div>
                                <div style="text-align: center; margin-top: -27px; margin-left: 92%;">
                                    <button class="wms-srv-input wms-srv-button wms-srv-highlight-btn-secondary" type="button" title="Save" id="btnAddSkuToOutwardOrder">
                                        <span>Add SKU</span></button>
                                </div>
                                <div class="wms-srv-grid-title">
                                    SKU List
                                </div>
                                <%-- <button class="wms-srv-input wms-srv-button wms-srv-highlight-btn wms-srv-add-order" title="Allocated Driver" type="button"><i class="fas fa-plus"></i><span> Submit</span></button>--%>
                            </div>
                            <div class="wms-srv-grid-holder">
                                <div class="wms-srv-grid" id="srv-grid-SkuList">
                                    <div class="wms-srv-grid-header">

                                        <div class="wms-srv-grid-cell" style="text-align: center;">SKU Code</div>
                                        <div class="wms-srv-grid-cell" style="text-align: center;">SKU Name</div>
                                        <div class="wms-srv-grid-cell" style="text-align: center;">Description </div>
                                        <div class="wms-srv-grid-cell" style="text-align: center;">Action </div>
                                    </div>
                                    <%--<div class="wms-srv-grid-row">
                           
                         <div class="wms-srv-grid-cell" style="text-align:center;">RH001</div>
                         <div class="wms-srv-grid-cell" style="text-align:center;">RahulSku</div>
                         <div class="wms-srv-grid-cell" style="text-align:center;">This is test Sku</div>
                           <div class="wms-srv-grid-cell">
                          <div class="wms-srv-grid-action">


                              <a href="#" title="Save" class="wms-srv-save" onclick=""><i class="fas fa-check-circle iconView"></i></a>
                          <div class="wms-srv-action-sep">|</div>

                              <a href="#" title="Save" class="wms-srv-save" onclick=";"><i class="fas fa-times-circle iconView"></i></a>
                          </div>
                               </div>
                 </div>--%>
                                </div>

                            </div>
                            <div class="wms-srv-grid-pager" id="tblSkuListPage">
                                1-0 of 0 Records <span class="wms-srv-empty-space"></span><b>Go to Page: </b>
                                <select id="ddlGridPageNo" class="wms-srv-input ddlGridPageNo notranslate" style="width: 100px;">
                                    <option>1</option>
                                </select><a href="#" title="Jump To" class="wms-srv-ddlpager-go" style="font-size: 16px;"><i class="fas fa-play-circle"></i></a>

                            </div>
                        </div>
                        <!-- Grid 2-->
                    </div>
                </div>
            </div>
        </div>
        <%--Sku List Popup End --%>

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
        <input type="text" data-for="KeyWords">
        </div> -->

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

        <!-- Document Popup End-->

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

        <!-- Order Parameter Popup End-->
        <div class="wms-srv-popup-holder" id="wms-srv-OrderParameter-popup" style="display: none; width: 85%; height: 87%; margin-left: 5%; margin-right: auto;">
            <div class="wms-srv-popup-bg">
            </div>
            <div class="wms-srv-popup" style="width: 100%; height: 100%; margin-left: auto; margin-right: auto;">
                <div class="wms-srv-popup-content">
                    <!-- Popup Title -->
                    <div class="wms-srv-popup-title">
                        <div class="wms-srv-page-title">
                            <i class="fas fa-file-alt"></i><span>Order Parameter</span>
                        </div>
                        <a href="#" title="Close" id="wms-srv-OrderParameter-popup-close" class="wms-srv-popup-close"><i class="fas fa-times-circle"></i></a>
                    </div>
                    <!-- Popup Title -->
                    <div class="wms-srv-popup-content-body" id="">
                        <div id="">
                            <div id="">
                                <!-- Grid -->
                                <div class="wms-srv-grid-holder pnlWmsHead">
                                    <input type="hidden" id="" value="" />
                                    <input type="hidden" id="" value="" />
                                    <div class="wms-srv-grid" id="" style="text-align: center;">
                                        <div class="wms-srv-grid-header" style="white-space: nowrap;">
                                            <div class="wms-srv-grid-cell">Sr no</div>
                                            <div class="wms-srv-grid-cell">Group Name</div>
                                            <div class="wms-srv-grid-cell">Value</div>

                                        </div>
                                        <div class="wms-srv-grid-row">
                                            <div class="wms-srv-grid-cell">173805</div>
                                            <div class="wms-srv-grid-cell">03-10-2023</div>
                                            <div class="wms-srv-grid-cell">TESTG</div>

                                        </div>
                                    </div>
                                </div>
                                <br>
                                <!-- Grid -->
                                <!-- Header Details -->

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- Order Parameter Popup -->

        <!-- Edit Order Popup with Scan Start-->
        <div class="wms-srv-popup-holder" id="wms-srv-EditReturnOrder-popup" style="display: none; z-index: 999;">
            <div class="wms-srv-popup-bg">
            </div>
            <div class="wms-srv-popup">
                <div class="wms-srv-popup-content">
                    <!-- Popup Title -->
                    <div class="wms-srv-popup-title">
                        <div class="wms-srv-page-title">
                            <i class="fas fa-file-alt"></i><span>Edit Return Order</span>
                        </div>
                        <a href="#" title="Close" id="wms-srv-EditReturnOrder-popup-close" class="wms-srv-popup-close"><i class="fas fa-times-circle"></i></a>
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
                                            <div class="titleReturnOrder">Edit Order</div>
                                            <div class="btnRetunrOrder">

                                                <%--<button class="wms-srv-input wms-srv-button wms-srv-highlight-btn wms-srv-add-order" title="Allocated Driver" type="button"><i class="fas fa-plus"></i><span> Add New</span></button>--%>
                                                <button class="wms-srv-input wms-srv-button wms-srv-highlight-btn wms-srv-add-order" title="Allocated Driver" type="button"><i class="fas fa-plus"></i><span>Save</span></button>
                                            </div>
                                            <table class="tableForm">
                                                <tbody>
                                                    <tr>
                                                        <td>
                                                            <req>
                        <span id="" class="lblReturnOrder">Title  :</span>
                    </req>
                                                            :
                                                        </td>
                                                        <td>
                                                            <input name="" type="text" value="This Is Test Title" maxlength="50" id="" class="inputTextReturnOrder" style="width: 200px;" />
                                                        </td>
                                                        <td>
                                                            <span id="" class="lblReturnOrder">OMS Order Ref.No:</span>
                                                        </td>
                                                        <td>
                                                            <input name="" type="text" value="Order1201" maxlength="50" id="" class="inputTextReturnOrder" style="width: 200px;" />
                                                        </td>
                                                        <td>
                                                            <span id="" class="lblReturnOrder">Return Id:</span>
                                                        </td>
                                                        <td>
                                                            <input name="" type="text" value="259632" maxlength="50" disabled id="" class="inputTextReturnOrder" style="width: 200px;" />
                                                        </td>
                                                    </tr>

                                                    <%--second row--%>
                                                    <tr>
                                                        <td>
                                                            <req><span id="" class="lblReturnOrder">Status :</span></req>
                                                            :
                                                        </td>
                                                        <td>
                                                            <input name="" type="text" maxlength="50" id="" value="Return" class="inputTextReturnOrder" style="width: 200px;" />
                                                        </td>
                                                        <td>
                                                            <span id="" class="lblReturnOrder">Return Date :</span>
                                                        </td>
                                                        <td>
                                                            <input name="" type="date" maxlength="50" id="" value="06/10/2023" class="inputTextReturnOrder" style="width: 200px;" />
                                                        </td>
                                                        <td>
                                                            <req><span id="" class="lblReturnOrder">Exp.Date Of Collection :</span></req>
                                                        </td>
                                                        <td>
                                                            <input name="" type="date" maxlength="100" value="25/07/2024" id="" class="inputTextReturnOrder" style="width: 200px;" />
                                                        </td>
                                                    </tr>

                                                    <%--third row--%>
                                                    <tr>
                                                        <td>
                                                            <req>
                        <span id="" class="lblReturnOrder">Customer Name :</span>
                    </req>
                                                            :
                                                        </td>
                                                        <td>
                                                            <select style="width: 200px; height: 23px;" class="inputTextReturnOrder">
                                                                <option>Customer 1</option>
                                                                <option>Customer 2</option>
                                                                <option>Customer 3</option>
                                                            </select>
                                                        </td>
                                                        <td>
                                                            <span id="" class="lblReturnOrder">Department  :</span>
                                                        </td>
                                                        <td>
                                                            <select style="width: 200px; height: 23px;" class="inputTextReturnOrder">
                                                                <option>Department 1</option>
                                                                <option>Department 2</option>
                                                                <option>Department 3</option>
                                                            </select>
                                                        </td>
                                                        <td>
                                                            <req><span id="" class="lblReturnOrder">Location Code :</span></req>
                                                        </td>
                                                        <td>
                                                            <input name="" type="text" maxlength="100" value="25/07/2024" id="" class="inputTextReturnOrder" style="width: 200px;" />
                                                            <a href="#" data-prefix="SE"><i class="fas fa-search" onclick="showLocationList();"></i></a>
                                                        </td>
                                                    </tr>

                                                    <%--Fourth Row--%>
                                                    <tr>
                                                        <td>

                                                            <req>
                        <span id="" class="lblReturnOrder">Priority :</span>
                    </req>
                                                        </td>
                                                        <td>

                                                            <select class="inputTextReturnOrder" style="width: 200px; height: 23px;">
                                                                <option>High</option>
                                                                <option>Low</option>
                                                                <option>Medium</option>
                                                            </select>
                                                        </td>
                                                        <td>
                                                            <span id="" class="lblReturnOrder">Requested By :</span>
                                                        </td>
                                                        <td>
                                                            <select class="inputTextReturnOrder" style="width: 200px; height: 23px;">
                                                                <option>User 1</option>
                                                                <option>User 2</option>
                                                                <option>User 3</option>
                                                            </select>
                                                        </td>
                                                        <td>
                                                            <req><span id="" class="lblReturnOrder">Return Category :</span></req>
                                                        </td>
                                                        <td>
                                                            <select class="inputTextReturnOrder" style="width: 200px; height: 23px;">
                                                                <option>Product Not Good</option>
                                                                <option>Warranty Return</option>
                                                                <option>Product Wrong</option>
                                                            </select>
                                                        </td>
                                                    </tr>

                                                    <%--fifth Row--%>

                                                    <tr>

                                                        <td>
                                                            <span id="" class="lblReturnOrder">Condition :</span>
                                                        </td>
                                                        <td>
                                                            <select class="inputTextReturnOrder" style="width: 200px; height: 23px;">
                                                                <option>Sealed</option>
                                                                <option>Pack</option>
                                                                <option>Open</option>
                                                            </select>
                                                        </td>
                                                        <td>
                                                            <req>
                        <span id="" class="lblReturnOrder">Remark :</span>
                    </req>
                                                        </td>
                                                        <td>
                                                            <input name="" type="text" maxlength="50" value="high" id="" class="inputTextReturnOrder" style="width: 300px;" />
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                            <div class="wms-srv-obj-main-grid pnlWmsDetail">
                                                <div class="wms-srv-grid-holder">
                                                    <div class="wms-srv-grid-action-panel">
                                                        <div class="wms-srv-grid-title titleReturnOrder">
                                                            Requested Sku List
                                                        </div>

                                                        <button class="wms-srv-input wms-srv-button wms-srv-highlight-btn wms-srv-add-order" onclick="showSkuPopup();" title="Allocated Driver" type="button"><i class="fas fa-plus"></i>Add Item To List</button>
                                                    </div>
                                                    <div class="wms-srv-grid-scroller" style="height: auto;">
                                                        <div class="wms-srv-grid" style="text-align: center; min-width: auto; width: 100%;">
                                                            <div class="wms-srv-grid-header" style="white-space: nowrap; text-align: center">


                                                                <div class="wms-srv-grid-cell" style="width: 60px;">SR. No.</div>
                                                                <div class="wms-srv-grid-cell">SKU Code</div>
                                                                <div class="wms-srv-grid-cell">SKU Name</div>
                                                                <div class="wms-srv-grid-cell">UOM</div>
                                                                <div class="wms-srv-grid-cell">Order Quantity</div>
                                                                <div class="wms-srv-grid-cell">Actual Return Qty</div>
                                                                <div class="wms-srv-grid-cell">Return Quantity</div>
                                                                <div class="wms-srv-grid-cell" style="text-align: center;">Action</div>
                                                            </div>
                                                            <%--<div class="wms-srv-grid-row">
             
                    <div class="wms-srv-grid-cell">1</div>
                    <div class="wms-srv-grid-cell">SKU01</div>
                    <div class="wms-srv-grid-cell">173773</div>
                    <div class="wms-srv-grid-cell">P1</div>
                    <div class="wms-srv-grid-cell"><input type="text" value="50"/></div>
                     <div class="wms-srv-grid-cell">100</div>
                    <div class="wms-srv-grid-cell"><input type="text" value="5"/></div>
                    <div class="wms-srv-grid-cell">
                        <div class="wms-srv-grid-action">
                        
                            <a href="#" title="Cancel" class="wms-srv-cancel"><i class="fas fa-times-circle"></i></a>

                        </div>

                    </div>
         
                </div>--%>
                                                        </div>
                                                    </div>
                                                    <div style="float: right;">Total Return Qty : <span>25</span></div>
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
        <!-- Edit Order Popup with Scan End-->

        <%--Location List Popup Start--%>
        <div class="wms-srv-popup-holder" id="wms-srv-LocationList-popup" style="display: none; z-index: 99999;">
            <div class="wms-srv-popup-bg">
            </div>
            <div class="wms-srv-popup" style="width: 50%; margin-left: 25%; height: 65%;">
                <div class="wms-srv-popup-content">
                    <!-- Popup Title -->
                    <div class="wms-srv-popup-title">
                        <div class="wms-srv-page-title">
                            <i class="fas fa-file-alt"></i><span>Location List</span>
                        </div>
                        <a href="#" id="wms-srv-LocationList-popup-close" class="wms-srv-popup-close"><i class="fas fa-times-circle"></i></a>
                    </div>
                    <!-- Popup Title -->
                    <div class="wms-srv-popup-content-body" id="wms-srv-popup-content-body">
                        <!-- Grid 2-->
                        <div class="wms-srv-grid-holder pnlWmsDetail" id="pnlWmsDetail">
                            <div class="wms-srv-grid-action-panel">
                                <div class="wms-srv-grid-common-action"><a href="#" data-prefix="CG" class="wms-srv-icononly"><i class="fas fa-list"></i></a></div>
                                <div class="wms-srv-grid-title">
                                    Location List
                                </div>
                                <div class="wms-srv-search">
                                    <input type="text" value="" placeholder="Enter To Location" id="txtLocationSrch" class="wms-srv-input wms-srv-simple-search"><a href="#"><i class="fas fa-search" onclick="LocationList();"></i></a>
                                </div>
                            </div>
                            <div class="wms-srv-grid-scroller">
                                <div class="wms-srv-grid" style="text-align: center; min-width: 400px;" id="wms-srv-grid-LocationList">
                                    <div class="wms-srv-grid-header">
                                        <div class="wms-srv-grid-cell  wms-srv-align">Location Code</div>
                                        <div class="wms-srv-grid-cell  wms-srv-align">Location Name</div>
                                        <div class="wms-srv-grid-cell  wms-srv-align">Location</div>
                                        <div class="wms-srv-grid-cell  wms-srv-align">Name</div>
                                        <div class="wms-srv-grid-cell  wms-srv-align">Email Id</div>
                                        <div class="wms-srv-grid-cell  wms-srv-align">Mobile No</div>
                                        <div class="wms-srv-grid-cell  wms-srv-align">Action</div>
                                    </div>

                                </div>
                            </div>
                            <%--<div class="wms-srv-grid-pager" id="tbltranferlistpager"> 1-0 of 0 Records <span class="wms-srv-empty-space"></span><b>Go to Page: </b> 
          <select id="ddlGridPageNo" class="wms-srv-input ddlGridPageNo notranslate" style="width:100px;">
              <option>1</option></select><a href="#" title="Jump To" class="wms-srv-ddlpager-go" style="font-size:16px;"><i class="fas fa-play-circle"></i></a>

            </div>--%>
                        </div>
                        <!-- Grid 2-->
                    </div>
                </div>
            </div>
        </div>
        <%--Location List Popup End --%>

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
                                                            <span id="" class="lblReturnOrder">Title  :</span>


                                                        </td>
                                                        <td>

                                                            <input name="" type="text" maxlength="50" value="Loc01" id="viewReturnOrderTitle" disabled class="inputTextReturnOrder" style="width: 200px;" />
                                                        </td>
                                                        <td>
                                                            <span id="" class="lblReturnOrder">OMS Order Ref.No:</span>
                                                        </td>
                                                        <td>
                                                            <input name="" type="text" maxlength="50" value="OCRREF1" id="viewReturnOrderRefNo" disabled class="inputTextReturnOrder" style="width: 200px;" />

                                                        </td>
                                                        <td>
                                                            <span id="" class="lblReturnOrder">Return Id:</span>
                                                        </td>
                                                        <td>
                                                            <input name="" type="text" maxlength="50" value="01256" id="viewReturnOrderId" disabled class="inputTextReturnOrder" style="width: 200px;" />

                                                        </td>
                                                    </tr>

                                                    <%--second row--%>
                                                    <tr>
                                                        <td>
                                                            <span id="" class="lblReturnOrder">Status :</span>

                                                        </td>
                                                        <td>
                                                            <input name="" type="text" maxlength="50" value="Return" id="viewReturnOrdeStatus" disabled class="inputTextReturnOrder" style="width: 200px;" />
                                                        </td>
                                                        <td>
                                                            <span id="" class="lblReturnOrder">Return Date :</span>
                                                        </td>
                                                        <td>
                                                            <input name="" type="text" maxlength="50" value="09/10/2023" id="viewReturnOrdeReturnDate" disabled class="inputTextReturnOrder" style="width: 200px;" />
                                                        </td>
                                                        <td>
                                                            <span id="" class="lblReturnOrder">Exp.Date Of Collection :</span>
                                                        </td>
                                                        <td>
                                                            <input name="" type="text" maxlength="50" value="25/10/2023" id="viewReturnOrdeExpDate" disabled class="inputTextReturnOrder" style="width: 200px;" />
                                                        </td>
                                                    </tr>

                                                    <%--third row--%>
                                                    <tr>
                                                        <td>

                                                            <span id="" class="lblReturnOrder">Customer Name :</span>


                                                        </td>
                                                        <td>
                                                            <input name="" type="text" maxlength="50" value="" id="viewReturnOrdeCustomerName" disabled class="inputTextReturnOrder" style="width: 200px;" />
                                                        </td>
                                                        <td>
                                                            <span id="" class="lblReturnOrder">Department  :</span>
                                                        </td>
                                                        <td>
                                                            <input name="" type="text" maxlength="50" value="" id="viewReturnOrdeDepartmentName" disabled class="inputTextReturnOrder" style="width: 200px;" />
                                                        </td>
                                                        <td>
                                                            <span id="" class="lblReturnOrder">Location Code :</span>
                                                        </td>
                                                        <td>
                                                            <input name="" type="text" maxlength="50" value="Loc001" id="viewReturnOrdeLocCode" disabled class="inputTextReturnOrder" style="width: 200px;" />
                                                        </td>
                                                    </tr>

                                                    <%--Fourth Row--%>
                                                    <tr>
                                                        <td>
                                                            <span id="" class="lblReturnOrder">Location Name :</span>
                                                        </td>
                                                        <td>
                                                            <input name="" type="text" maxlength="50" value="Loc001" id="viewReturnOrdeLocName" disabled class="inputTextReturnOrder" style="width: 200px;" />
                                                        </td>


                                                        <td>

                                                            <span id="" class="lblReturnOrder">Priority :</span>


                                                        </td>
                                                        <td>
                                                            <input name="" type="text" maxlength="50" value="Loc001" id="viewReturnOrdePriority" disabled class="inputTextReturnOrder" style="width: 200px;" />

                                                        </td>
                                                        <td>
                                                            <span id="" class="lblReturnOrder">Requested By :</span>
                                                        </td>
                                                        <td>
                                                            <input name="" type="text" maxlength="50" value="Loc001" id="viewReturnOrdeRequestedBy" disabled class="inputTextReturnOrder" style="width: 200px;" />
                                                        </td>
                                                      
                                                    </tr>

                                                    <%--fifth Row--%>

                                                    <tr>
                                                        <td>
                                                            <span id="" class="lblReturnOrder">Return Category :</span>
                                                        </td>
                                                        <td>
                                                            <input name="" type="text" maxlength="50" value="Loc001" id="viewReturnOrdeReturnCategory" disabled class="inputTextReturnOrder" style="width: 200px;" />

                                                        </td>

                                                        <td>
                                                            <span id="" class="lblReturnOrder">Condition :</span>
                                                        </td>
                                                        <td>
                                                            <input name="" type="text" maxlength="50" value="Loc001" id="viewReturnOrdeCondition" disabled class="inputTextReturnOrder" style="width: 200px;" />
                                                        </td>
                                                        <td>


                                                            <span id="" class="lblReturnOrder">Remark :</span>


                                                        </td>
                                                        <td>
                                                            <input name="" type="text" maxlength="50" value="Loc001" id="viewReturnOrdeRemark" disabled class="inputTextReturnOrder" style="width: 200px;" />
                                                        </td>

                                                        <td>


                                                            <span id="locadd" class="lblReturnOrder" visible="false">Address :</span>


                                                        </td>
                                                        <td>
                                                            <input name="" type="text" maxlength="50" value="Loc001" id="viewReturnOrdeAddress" visible="false" disabled class="inputTextReturnOrder" style="width: 200px;" />
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
                                                                <div class="wms-srv-grid-cell">UOM</div>
                                                                <div class="wms-srv-grid-cell">Order Quantity</div>
                                                                <div class="wms-srv-grid-cell">Actual Return Qty</div>
                                                                <div class="wms-srv-grid-cell">Return Quantity</div>

                                                            </div>
                                                            <div class="wms-srv-grid-row">

                                                                <div class="wms-srv-grid-cell">1</div>
                                                                <div class="wms-srv-grid-cell">SKU001</div>
                                                                <div class="wms-srv-grid-cell">173773</div>
                                                                <div class="wms-srv-grid-cell">P1</div>
                                                                <div class="wms-srv-grid-cell">20</div>
                                                                <div class="wms-srv-grid-cell">50</div>
                                                                <div class="wms-srv-grid-cell">100</div>


                                                            </div>
                                                            <div class="wms-srv-grid-row">

                                                                <div class="wms-srv-grid-cell">2</div>
                                                                <div class="wms-srv-grid-cell">SKU002</div>
                                                                <div class="wms-srv-grid-cell">173773</div>
                                                                <div class="wms-srv-grid-cell">P1</div>
                                                                <div class="wms-srv-grid-cell">20</div>
                                                                <div class="wms-srv-grid-cell">50</div>
                                                                <div class="wms-srv-grid-cell">100</div>


                                                            </div>

                                                        </div>
                                                        <div style="float: right;">Total Return Qty : <span id="lblViewTotalQty">0</span></div>
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

        <!-- Driver Assign Popup End -->
        <%--Driver Grid List Popup Start--%>
        <div class="wms-srv-popup-holder" id="wms-srv-DriverGridList-popup" style="display: none; z-index: 99999;">
            <div class="wms-srv-popup-bg">
            </div>
            <div class="wms-srv-popup" style="width: 50%; margin-left: 25%;">
                <div class="wms-srv-popup-content">
                    <!-- Popup Title -->
                    <div class="wms-srv-popup-title">
                        <div class="wms-srv-page-title">
                            <i class="fas fa-file-alt"></i><span>Driver List</span>
                        </div>
                        <a href="#" id="wms-srv-DriverGridList-popup-close" class="wms-srv-popup-close"><i class="fas fa-times-circle"></i></a>
                    </div>
                    <!-- Popup Title -->
                    <div class="wms-srv-popup-content-body" id="wms-srv-popup-content-body">
                        <!-- Grid 2-->
                        <div class="wms-srv-grid-holder pnlWmsDetail" id="pnlWmsDetail">
                            <div class="wms-srv-grid-action-panel">
                                <div class="wms-srv-grid-common-action"><a href="#" data-prefix="CG" class="wms-srv-icononly"><i class="fas fa-list"></i></a></div>
                                <div class="wms-srv-grid-title">
                                    Driver List
                                </div>
                                <div class="wms-srv-search">
                                    <input type="text" value="" placeholder="Enter To Search" id="txtDriverListSrch" class="wms-srv-input wms-srv-simple-search"><a href="#"><i class="fas fa-search" onclick="DriverList();"></i></a>
                                </div>
                            </div>
                            <div class="wms-srv-grid-scroller">
                                <div class="wms-srv-grid" style="text-align: center; min-width: 400px;" id="wms-srv-grid-AssignDriverList">
                                    <%--<div class="wms-srv-grid-header">
                        <div class="wms-srv-grid-cell  wms-srv-align">Location Code</div>
                        <div class="wms-srv-grid-cell  wms-srv-align">Location Name</div>
                        <div class="wms-srv-grid-cell  wms-srv-align">Location</div>
                          <div class="wms-srv-grid-cell  wms-srv-align">Name</div>
                          <div class="wms-srv-grid-cell  wms-srv-align">Email Id</div>
                          <div class="wms-srv-grid-cell  wms-srv-align">Mobile No</div>
                          <div class="wms-srv-grid-cell  wms-srv-align">Action</div>
                     </div>--%>

                                    <%--<div class="wms-srv-grid-pager" id="tbltranferlistpager"> 1-0 of 0 Records <span class="wms-srv-empty-space"></span><b>Go to Page: </b> 
          <select id="ddlGridPageNo" class="wms-srv-input ddlGridPageNo notranslate" style="width:100px;">
              <option>1</option></select><a href="#" title="Jump To" class="wms-srv-ddlpager-go" style="font-size:16px;"><i class="fas fa-play-circle"></i></a>

            </div>--%>
                                </div>
                            </div>

                        </div>
                        <!-- Grid 2-->
                    </div>
                </div>
            </div>
        </div>
        <%--Driver Grid List Popup End --%>








        <%--Import Excel Data start --%>

        <div class="wms-srv-popup-holder" id="wms-srv-import-Excel" style="display: none; width: 85%; height: 87%; margin-left: 5%; margin-right: auto;">
            <div class="wms-srv-popup-bg"></div>
            <div class="wms-srv-popup" style="width: 100%; height: 100%; margin-left: auto; margin-right: auto;">
                <div class="wms-srv-popup-title">
                    <div class="wms-srv-page-title">
                        <i class="fas fa-file-alt"></i><span>Import Order</span>
                    </div>
                    <a href="#" title="Close" id="wms-srv-import-Excel-Close" class="wms-srv-popup-close"><i class="fas fa-times-circle"></i></a>
                </div>


                <hr class="WmsImport_Wizard_Steps_Line">
                <div class="WmsImport_Wizard_Steps">
                    <div id="step_filetype" class="WmsImport_Wizard_Steps_Holder activeStep">
                        <div class="stepsNumberHolder">
                            <span><i class="fas fa-file"></i>
                            </span>
                        </div>
                        <div class="stepsLable">File Type</div>
                    </div>
                    <div id="step_mapcolumns" class="WmsImport_Wizard_Steps_Holder ">
                        <div class="stepsNumberHolder">
                            <span><i class="fas fa-columns"></i></span>
                        </div>
                        <div class="stepsLable">Map Columns</div>
                    </div>
                    <div id="step_validatefile" class="WmsImport_Wizard_Steps_Holder">
                        <div class="stepsNumberHolder">
                            <span><i class="fas fa-clipboard-check"></i>
                            </span>
                        </div>
                        <div class="stepsLable">Validate File</div>
                    </div>
                    <div id="step_import" class="WmsImport_Wizard_Steps_Holder">
                        <div class="stepsNumberHolder">
                            <span><i class="fas fa-file-upload"></i>
                            </span>
                        </div>
                        <div class="stepsLable">Import</div>
                    </div>
                </div>


                <div class="WMSImport_StepsPages" id="WmsPage_step_filetype" style="">


                    <div class="WmsImport_Title">
                        <div class="themeWMSAjaxTabPageTitle">
                            <i class="fas fa-file"></i><span>Import Excel Wizard</span>
                        </div>
                    </div>
                    <!-- Bootstrap Code Starts Here -->
                    <%--  <div class="container-fluid">
                    <!-- Row Start -->
                    <div class="row">
                        <div class="col-lg-4" style="display:none;">
                            <!-- Cell Content -->
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="col-md-4">
                                        <!-- Label -->
                                        <req> Company </req>
                                        :
                                        <!-- Label -->
                                    </div>
                                    <div class="col-md-8">
                                        <!-- Control -->
                                        <asp:dropdownlist id="ddlcompanymain" cssclass="inputElement" clientidmode="Static" runat="server" width="187px" datatextfield="Name" datavaluefield="ID" onchange="GetCustomer()">
                                        </asp:dropdownlist>
                                        <asp:requiredfieldvalidator id="RequiredFieldValidator3" forecolor="Maroon" controltovalidate="ddlcompanymain" initialvalue="0" validationgroup="Save" runat="server" errormessage="Please Select Company" display="None">
                                        </asp:requiredfieldvalidator>
                                        <!-- Control -->
                                    </div>
                                </div>
                            </div>
                            <!-- Cell Content -->
                        </div>
                    </div>
                </div>--%>
                    <!-- Bootstrap Code Ends Here -->
                    <div class="WmsImport_TypeHolder">
                        <div class="container-fluid">
                            <div class="row">
                                <div class="col-md-4">

                                    <%--<div class="WmsImport_Type" data-type="CSV" style=" cursor:pointer;">
                                    <div class="WmsImport_Icon">
                                        <i class="fas fa-file-csv"></i>
                                    </div>
                                    <div class="WmsImport_Description">
                                        CSV
                                    </div>
                                </div>
                                <!-- Export to Excel -->
                                <div class="WmsImport_DownloadTemplate">
                                    <a href="#" onclick="downloadTemplate('csv');return false;" class="wmsWizNavNext wmsWizNavActive"><i class="fas fa-download"></i> Download
                                        Template</a>
                                </div>--%>
                                </div>
                                <div class="col-md-4">
                                    <!-- Export to Excel -->
                                    <div class="WmsImport_Type" data-type="Excel" style="opacity: 1; cursor: pointer; margin-left: 33%; margin-right: 38%;">
                                        <div class="WmsImport_Icon">
                                            <i class="fas fa-file-excel" onclick="showUploadFile();"></i>
                                        </div>
                                        <div class="WmsImport_Description" onclick="showUploadFile();">
                                            Excel Import
                                        </div>
                                    </div>
                                    <!-- Export to Excel -->
                                    <div class="WmsImport_DownloadTemplate" style="opacity: 1;">
                                        <a href="#" class="wmsWizNavNext wmsWizNavActive" style="margin-right: 32px;" id="exceltemp" onclick="downloadTemplate('excels');return false;"><i class="fas fa-download"></i>
                                            Download Template</a>
                                    </div>
                                    <!-- Export to Excel -->

                                </div>
                                <div class="col-md-4">
                                    <!-- Export to Excel -->
                                    <%--<div class="WmsImport_Type" data-type="Text" style="opacity:1;cursor:pointer;">
                                    <div class="WmsImport_Icon">
                                        <i class="fas fa-file-alt"></i>
                                    </div>
                                    <div class="WmsImport_Description">
                                        Text
                                    </div>
                                </div>
                                <!-- Export to Excel -->
                                <div class="WmsImport_DownloadTemplate" style="opacity:1; ">
                                    <a href="#" class="wmsWizNavNext wmsWizNavActive" onclick="downloadTemplate('txt');return false;"><i class="fas fa-download"></i>
                                        Download
                                        Template</a>
                                </div>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="WmsImport_NavHolder" style="display: none;">
                        <asp:LinkButton ID="wizSetupBack" runat="server" CssClass="wmsWizNavBack">
                        <i class="fas fa-arrow-alt-circle-left"></i>Back
                        </asp:LinkButton>
                        <asp:LinkButton ID="wizSetupNext" runat="server" CssClass="wmsWizNavNext wmsWizNavActive">
                        Next<i class="fas fa-arrow-alt-circle-right"></i>
                        </asp:LinkButton>
                    </div>
                </div>
                <%--Upload File Code--%>
                <div class="WMSImport_StepsPages" id="WmsPage_step_mapcolumns" style="display: none; z-index: 9999">
                    <div class="WmsImport_Title">
                        <div class="themeWMSAjaxTabPageTitle">
                            <i class="fas fa-columns"></i><span>Select file / Map
                            Columns</span>
                        </div>
                    </div>
                    <div class="WMSImport_FileForm" id="WMSImport_FileForm">
                        <!-- Bootstrap Code Starts Here -->
                        <div class="container-fluid">
                            <!-- Row Start -->
                            <div class="row" style="display: none;">
                                <div class="col-lg-4">
                                    Total File:
                                <div id="fileNum"></div>
                                </div>
                                <div class="col-lg-4">
                                    File Size:
                                <div id="fileSize"></div>
                                </div>
                                <div class="col-lg-4">
                                    File Output:
                                <div id="fileOutput"></div>
                                </div>
                            </div>
                            <!-- Row End -->
                        </div>
                        <!-- Bootstrap Code Ends Here -->
                        <!-- Drag Drop Files -->
                        <div class="WMSdragDropHolder" ondrop="drop(event)" ondragover="allowDrop(event)" onmouseout="removeDrop(event)">
                            <div class="WMSdragDropPanel">
                                <div class="WMSdragDropLabel">
                                    Drag and drop files here to import
                                </div>
                            </div>
                        </div>
                        <div class="WMSDefaultFileCtrl">
                            Or use default file uploader
                        <asp:FileUpload ID="txtFile" runat="server" ClientIDMode="Static" onchange="setFile(this);" accept="application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"></asp:FileUpload>
                            <%--  <input type="file" id="txtFile" onchange="setFile(this);">--%>
                        </div>
                        <!-- Drag Drop Files -->
                    </div>
                    <div id="fileHeaderContainer" class="fileHeaderContainer" style="display: none;">
                        <div class="row">
                            <div class="col-lg-12">
                                <!-- <hr /> -->
                                <b>Sort or Remove column to adjust your header as per template header. <a href="#" id="lnkMapHeader" onclick="mapHeaderColumn();return false;" class="wmsWizNavNext wmsWizNavActive lnkMapHeader">Map Header</a> </b>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-6 sortableHeaderHolder">
                                Template Header
                            </div>
                            <div class="col-lg-6 sortableHeaderHolder">
                                Your Data Header
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-6">
                                <!-- Column Mapping Table -->
                                <ul id="templateHeader">
                                </ul>
                                <!-- Column Mapping Table -->
                            </div>
                            <div class="col-lg-6">
                                <!-- Column Mapping Table -->
                                <ul id="sortable">
                                </ul>
                                <!-- Column Mapping Table -->
                            </div>
                        </div>
                    </div>

                </div>

                <%--Upload File Code--%>
            </div>
        </div>

    </div>


    <%--Import Excel Data End --%>
</asp:Content>
