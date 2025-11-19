<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/CRM.Master" AutoEventWireup="true" CodeBehind="UserConfiguration.aspx.cs"  Theme="Blue" Inherits="PowerOnRentwebapp.UserConfiguration.UserConfiguration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <style>
        #main {
                height: auto !important;
              }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHolder_FormHeader" runat="server">
         <asp:HiddenField ID="hdnConfigUserID" ClientIDMode="Static" runat="server" />
        <asp:HiddenField ID="hdnConfigCompanyId" ClientIDMode="Static" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHolder_Form" runat="server">
<div class="wms-srv-content-area pnlInbound" id="pnlAppointmentList">
    <!-- Content Area -->

    <div class="container-fluid wms-srv-dashboard-panel">
        <div class="row">
            <div class="col-md-4 nopadding">
                <!-- Page Title -->
                <div class="wms-srv-page-title">
                    <i class="fas fa-solid fa-user"></i><span class="wms-srv-data-title">RMS User Configuration</span>
                </div>
                <!-- Page Title -->
            </div>
            <div class="col-md-8 nopadding">
            </div>
        </div>
    </div>
      <div class="gridBtnNewUserConfi">
           <button class="wms-srv-input wms-srv-highlight-btn wms-srv-add-order" onclick="saveRMSUser();" style="margin-right: 10px;"  title="save" type="button"><i class="fas fa-plus"></i><span> Save</span></button>
        <button class="wms-srv-input  wms-srv-highlight-btn wms-srv-add-order" title="clear" onclick="clearHeadValues();" type="button"><i class="fas fa-times-circle"></i><span> Clear</span></button>
      </div>
    <div class="userConfiGridAddNew" style="padding:2px;">
            <table>
                <tbody>
                   <tr>
                       <td>
                           <span>Customer :</span>
                         <select id="ddluserCustomerList" onclick="" style="width:200px;height:23px;" >
                              <option>-- Select Customer --</option>
                         </select>
                       </td>
                 
                       <td>
                           <span class="userConfigInputBox">Department :</span>
                         <select id="ddluserDepartment" style="width:200px;height:23px;" >
                           <option>-- Select Department --</option>
                         </select>
                       </td>
                          <td>
                           <span  class="userConfigInputBox">Select User :</span>
                        <sapn>
                            <input id="sethdnUserId" type="hidden" style="width:200px;height:20px;"/>
                            <input id="setUserName" type="text" style="width:200px;height:20px;"/></sapn> 
                          <sapn><i class="fas fa-search iconView" onclick="viewUserList();"></i></sapn> 
                      
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

    <!-- Grid -->
    <div class="wms-srv-obj-main-grid pnlWmsDetail">
        <div class="wms-srv-grid-holder">
            <div class="wms-srv-grid-action-panel">

                            <div class="wms-srv-grid-title">
                     <a href="#" class="wms-srv-icononly"><i class="fas fa-users"></i></a> User List
                            </div>
                <div class="wms-srv-search">
              <input type="text" value="" placeholder="Enter To Search" id="txtsearchRMSuser" class="wms-srv-input wms-srv-simple-search"><a href="#" ><i class="fas fa-search" onclick="UserConfiList();"></i></a>                                   
                        </div>
            </div>
       <div class="wms-srv-grid-scroller" style="max-height:300px;">
        <div class="wms-srv-grid" id="gridUserConfiList">
   
            <%--<div class="wms-srv-grid-header">   
                <div class="wms-srv-grid-cell">Customer</div>
                <div class="wms-srv-grid-cell">Department Name</div>
                <div class="wms-srv-grid-cell">User Name</div>
                <div class="wms-srv-grid-cell">Active</div>
                <div class="wms-srv-grid-cell" style="width:14%;">Action</div>
            </div>

              <div class="wms-srv-grid-row">      
                          <div class="wms-srv-grid-cell" style="text-align:center;">Customer1</div>
                          <div class="wms-srv-grid-cell" style="text-align:center;">Packing</div>
                          <div class="wms-srv-grid-cell" style="text-align:center;">User001</div>
                          <div class="wms-srv-grid-cell" style="text-align:center;">YES</div>               
                           <div class="wms-srv-grid-cell">
                                <div class="wms-srv-grid-action" style="margin-bottom: -20px;">  
                                    <label class="switch">
                                         <input type="checkbox" checked id="checkUserActiveVal"/>
                                              <span class="slider round"></span>
                                            </label>
                                    <div class="wms-srv-action-sep"> | </div>
                                        <i class="fas fa-times-circle iconView" title="Edit Order"></i>                               
                                </div>
                         </div>
           </div>      
        --%>
     </div>
</div>
           

     <%--   <div class="wms-srv-grid-pager" id="pgUserConfigUserList">1-0 of 0 Records <span class="wms-srv-empty-space"></span><b>Go to Page: </b> <select id="ddlGridPageNo" class="wms-srv-input ddlGridPageNo" style="width:100px;"><option>1</option></select><a href="#" title="Jump To" class="wms-srv-ddlpager-go" style="font-size:16px;"><i class="fas fa-play-circle"></i></a>

            </div>--%>
            <div class="wms-srv-grid-pager" id="pgUserConfigUserList"> 1-0 of 0 Records <span class="wms-srv-empty-space"></span><b>Go to Page: </b> 
          <select id="ddlGridPageNo" class="wms-srv-input ddlGridPageNo" style="width:100px;">
              <option>1</option></select><a href="#" title="Jump To" class="wms-srv-ddlpager-go" style="font-size:16px;"><i class="fas fa-play-circle"></i></a>

            </div>
        </div>
    <!-- Content Area -->
</div>
     </div>

    <%--User List  Popup--%>
 <div class="wms-srv-popup-holder" id="wms-srv-UserList-popup" style="display:none ;z-index:9999">
                   <div class="wms-srv-popup-bg">
                   </div>
   <div class="wms-srv-popup" style="width: 64%;margin-left: 21%; height: auto;">
      <div class="wms-srv-popup-content">
         <!-- Popup Title -->
         <div class="wms-srv-popup-title">
            <div class="wms-srv-page-title">
               <i class="fas fa-file-alt"></i><span>User List </span>
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
                            <input type="text" value="" placeholder="Enter To Search" id="txtUserSearch" class="wms-srv-input wms-srv-simple-search"><a href="#" ><i class="fas fa-search" onclick="UserConfigurationList();"></i></a>                                   
                        </div>
               </div>
               <div class="wms-srv-grid-scroller">
       <div class="wms-srv-grid" style="text-align: center; min-width: 400px;" id="wms-srv-grid-UserListPopup">
                     <div class="wms-srv-grid-header">
                        <div class="wms-srv-grid-cell  wms-srv-align">Employee ID</div>              
                          <div class="wms-srv-grid-cell  wms-srv-align">Name</div>
                          <div class="wms-srv-grid-cell  wms-srv-align">User Type</div>
                         <div class="wms-srv-grid-cell  wms-srv-align">User Id</div>
                          <div class="wms-srv-grid-cell  wms-srv-align">Email Id</div>
                          <div class="wms-srv-grid-cell  wms-srv-align">Action</div>
                     </div>
                    
                     
                      </div>
               </div>
            </div>
            <!-- Grid 2-->
         </div>
      </div>
   </div>
</div>
    <%--User List Popup--%>
</asp:Content>
