<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/CRM.Master" AutoEventWireup="true" CodeBehind="S2SUserConfiguration.aspx.cs" Theme="Blue" Inherits="PowerOnRentwebapp.S2SORDER.S2SUserConfiguration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script src="../core-js/HttpLoader.js" language="javascript" type="text/javascript"></script>
    <link rel="stylesheet" href="Themes/bootstrap-4.0.0/dist/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous" type="text/javascript" />

    <script src="Themes/bootstrap-4.0.0/dist/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl"
        crossorigin="anonymous" type="text/javascript"></script>
    <script src="ThemeHTML/js/wms-theme.js" type="text/javascript"></script>
    <script src="ThemeHTML/js/wms-app-navigator.js" type="text/javascript"></script>
    <script src="JAVASCRIPT/S2SUserConfiguration.js" type="text/javascript"></script>
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
                        <i class="fas fa-exchange-alt"></i><span class="wms-srv-data-title">S2S User Configuration</span>
                    </div>
                    <!-- Page Title -->
                </div>
                <div class="col-md-8 nopadding">
                </div>
            </div>
        </div>
        <div class="gridBtnNewUserConfi">
            <button class="wms-srv-input wms-srv-highlight-btn wms-srv-add-order" onclick="SaveUser();" style="margin-right: 10px;" title="save" type="button"><i class="fas fa-plus"></i>&nbsp<span>Save</span></button>
            <%--<button class="wms-srv-input  wms-srv-highlight-btn wms-srv-add-order" title="clear" onclick="clearHeadValues();" type="button"><i class="fas fa-times-circle"></i><span>Clear</span></button>--%>
        </div>
        <div class="userConfiGridAddNew" style="padding: 2px;">
            <table>
                <tbody>
                    <tr>
                        <td>
                            <span>Customer :</span>
                            <select id="ddluserCustomerListS2S" onclick="" style="width: 200px; height: 23px;">
                                <option value="Vodafone Commerical">Vodafone Commerical</option>
                            </select>
                        </td>

                        <%--<td>
                            <span class="userConfigInputBox">Department :</span>
                            <select id="ddluserDepartment" style="width: 200px; height: 23px;">
                                <option>VFQ online</option>
                            </select>
                        </td>--%>
                        <td>
                            <span class="userConfigInputBox">Select User :</span>
                            <sapn>
                            <input id="sethdnUserIdS2S" type="hidden" style="width:200px;height:20px;"/>
                            <input id="plnUserNameS2S" type="text"  style="width:200px;height:20px;"  disabled/></sapn>
                            <sapn><i class="fas fa-search iconView" onclick="showUserlist();"></i></sapn>
                        </td>
                        <%-- <td>
                           <span  class="userConfigInputBox">Active :</span>
                          <select style="width:200px;height:23px;">
                                   <option>YES</option>
                                    <option>NO</option>
                          
                           </select>
                       </td>--%>
                    </tr>


                </tbody>
            </table>
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
                        <select type="text" class="wms-srv-input wms-srv-search-filter" id="ddlS2SOrderColSearchUser">
                            <option value="0">-- Select --</option>
                           <%-- <option value="customer">Customer</option>--%>
                            <option value="userName">User Name</option>
                            <option value="userType">User Type</option>
                        </select>
                        <input type="text" value="" class="wms-srv-input wms-srv-simple-search" id="ddlS2SFilterValueUser">
                        <a href="#" data-prefix="SE"><i class="fas fa-search" onclick="searchUserOrder(); return false;" style="margin-right: 17px;"></i></a>
                        <a href="#" title="Cancel" class="wms-srv-cancel" data-prefix="CN"><i class="fas fa-times-circle" onclick="clearSearchFilterUser();"></i></a>
                    </div>
                    <div class="wms-srv-grid-title">
                        User List
                    </div>
                </div>
                <div class="wms-srv-grid-scroller" style="max-height: 300px;">
                    <div class="wms-srv-grid" id="wms-srv-grid-S2SUserList">
                      <%--  <div class="wms-srv-grid-header">
                            <div class="wms-srv-grid-cell">Customer</div>
                            <div class="wms-srv-grid-cell">Department Name</div>
                            <div class="wms-srv-grid-cell">User Name</div>
                            <div class="wms-srv-grid-cell">User Type</div>
                            <div class="wms-srv-grid-cell">Active</div>
                            <div class="wms-srv-grid-cell">Action</div>
                        </div>
                        <div class="wms-srv-grid-row">
                            <div class="wms-srv-grid-cell" style="text-align: center;">Vodaphone Commercial</div>
                            <div class="wms-srv-grid-cell" style="text-align: center;">Vodafone - Office it_CC-9926</div>
                            <div class="wms-srv-grid-cell" style="text-align: center;">Muhamad Khan</div>
                            <div class="wms-srv-grid-cell" style="text-align: center;">Requester for Approver</div>
                            <div class="wms-srv-grid-cell" style="text-align: center;">Yes</div>
                            <div class="wms-srv-grid-cell">
                                <div class="wms-srv-grid-action" style="text-align: center;">
                                    <label class="switch">
                                        <input type="checkbox" checked="" onclick="">
                                        <span class="slider round"></span>
                                    </label>
                                    <div class="wms-srv-action-sep">| </div>
                                    <a href="#" title="Rate History" onclick="" class="wms-srv-save"><i class="fas fa-times-circle"></i></a>
                                </div>
                            </div>
                        </div>--%>
                    </div>
                </div>

                <div class="wms-srv-grid-pager" id="tblUserlistpager">
                    1-0 of 0 Records <span class="wms-srv-empty-space"></span><b>Go to Page: </b>
                    <select id="ddlGridPageNo" class="wms-srv-input ddlGridPageNo" style="width: 100px;">
                        <option>1</option>
                    </select><a href="#" title="Jump To" class="wms-srv-ddlpager-go" style="font-size: 16px;"><i class="fas fa-play-circle"></i></a>

                </div>
            </div>
            <!-- Content Area End -->
        </div>

        <%--User List  Popup--%>
        <div class="wms-srv-popup-holder" id="wms-srv-UserList-popup" style="display: none; z-index: 9999">
            <div class="wms-srv-popup-bg">
            </div>
            <div class="wms-srv-popup" style="width: 64%; margin-left: 21%; height: auto;">
                <div class="wms-srv-popup-content">
                    <!-- Popup Title -->
                    <div class="wms-srv-popup-title">
                        <div class="wms-srv-page-title">
                            <i class="fas fa-file-alt"></i><span>User List</span>
                        </div>
                        <a href="#" id="wms-srv-UserList-popup-close" class="wms-srv-popup-close"><i class="fas fa-times-circle"></i></a>
                    </div>
                    <!-- Popup Title -->
                    <div class="wms-srv-popup-content-body" id="wms-srv-popup-content-body">
                        <!-- Grid 2-->
                        <div class="wms-srv-grid-holder pnlWmsDetail" id="pnlWmsDetail">
                            <div class="wms-srv-grid-action-panel">
                                <div class="wms-srv-grid-common-action"><a href="#" data-prefix="CG" class="wms-srv-icononly"><i class="fas fa-list"></i></a></div>
                                <div class="wms-srv-grid-title">
                                    User List
                                </div>
                                <div class="wms-srv-search">
                                    <input type="text" value="" placeholder="Enter To Search" id="txtUserSearchS2S" class="wms-srv-input wms-srv-simple-search"><a href="#"><i class="fas fa-search" onclick="GetS2SUserList();"></i>&nbsp<i class="fas fa-times-circle" onclick="ClearUserList();"></i></a>
                                </div>
                            </div>
                            <div class="wms-srv-grid-scroller">
                                <div class="wms-srv-grid" style="text-align: center; min-width: 400px;" id="pnlS2SUserList">
                                  <%--  <div class="wms-srv-grid-header">
                                        <div class="wms-srv-grid-cell  wms-srv-align">First Name</div>
                                        <div class="wms-srv-grid-cell  wms-srv-align">Last Name</div>
                                        <div class="wms-srv-grid-cell  wms-srv-align">User Type</div>
                                        <div class="wms-srv-grid-cell  wms-srv-align">User Name</div>
                                        <div class="wms-srv-grid-cell  wms-srv-align">Email Id</div>
                                        <div class="wms-srv-grid-cell  wms-srv-align">Action</div>
                                    </div>
                                    <div class="wms-srv-grid-row">
                                        <div class="wms-srv-grid-cell">Mohammad</div>
                                        <div class="wms-srv-grid-cell">Akram1</div>
                                        <div class="wms-srv-grid-cell">Requester and Approver</div>
                                        <div class="wms-srv-grid-cell">Mohammad Akram1</div>
                                        <div class="wms-srv-grid-cell">Mohammad.Akram@gmail.com</div>
                                        <div class="wms-srv-grid-cell">
                                            <div class="wms-srv-grid-action">
                                                <a href="#" title="Save" class="wms-srv-save" onclick="">
                                                    <i class="fas fa-check-circle"></i>
                                                </a>
                                            </div>
                                        </div>
                                    </div>--%>
                                </div>
                            </div>
                        </div>
                        <!-- Grid 2-->
                    </div>
                </div>
            </div>
        </div>
        <%--User List Popup--%>
    </div>
</asp:Content>
