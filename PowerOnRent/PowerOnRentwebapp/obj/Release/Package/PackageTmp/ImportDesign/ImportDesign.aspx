<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/CRM.Master" AutoEventWireup="true" CodeBehind="ImportDesign.aspx.cs" Inherits="BrilliantWMS.ImportDesign.ImportDesign" EnableEventValidation="false" Theme="Blue" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>
    <%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="obout" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
    <%@ Register Src="../Territory/UC_Territory.ascx" TagName="UC_Territory" TagPrefix="uc1" %>
    <%@ Register Src="../Address/UCAddress.ascx" TagName="UCAddress" TagPrefix="uc7" %>
    <%@ Register Src="../CommonControls/UC_Date.ascx" TagName="UC_Date" TagPrefix="uc2" %>
    <%@ Register Src="../CommonControls/UCFormHeader.ascx" TagName="UCFormHeader" TagPrefix="uc4" %>
    <%@ Register Src="../CommonControls/UCToolbar.ascx" TagName="UCToolbar" TagPrefix="uc5" %>
    <%--<%@ Register TagPrefix="asp" Namespace="Saplin.Controls" Assembly="DropDownCheckBoxes" %>--%>
    <%@ Register Src="../CommonControls/UCFormHeader.ascx" TagName="UCFormHeader" TagPrefix="uc7" %>
    <%@ Register Src="../CommonControls/UCToolbar.ascx" TagName="UCToolbar" TagPrefix="uc8" %>
    <%@ Register Src="../CommonControls/Toolbar.ascx" TagName="Toolbar" TagPrefix="uc3" %>

     
    
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHolder_FormHeader" runat="server">
    <script type="text/javascript">
         var qry_vr = '<%= hdnfinalquery.ClientID %>';
         var hdnwhere_vr = '<%= hdnwhere.ClientID %>';
        var sessionqry = '<%= Session["qry"] %>';
        var ddlObject_vr = '<%= ddlobject.ClientID %>';
        var ddlcustomer_vr='<%= ddlcustomer.ClientID %>';
     </script>
    <uc3:Toolbar ID="Toolbar1" runat="server" />
    <uc8:UCToolbar ID="UCToolbar1" runat="server" />
    <uc4:UCFormHeader ID="UCFormHeader1" runat="server" />
    <link href="../WMSTheme/jquery-ui/jquery-ui.css" rel="stylesheet">
    <script src="../WMSTheme/jquery-ui/jquery-ui.js"></script>
    
        <link href="../WMSTheme/css/theme-wms-sky-blue.css" id="wmsThemeSet" rel="stylesheet">
    <link href="../WMSTheme/css/wms-style.css" rel="stylesheet">
     <script src="https://code.jquery.com/jquery-3.2.1.min.js"></script>
    <link href="../WMSTheme/jquery-ui/jquery-ui.css" rel="stylesheet">
    <script src="../WMSTheme/jquery-ui/jquery-ui.js"></script>
    <link href="../WMSTheme/fontawesome/css/all.css" rel="stylesheet"> 
        
     <link rel="stylesheet" href="../WMSTheme/bootstrap-4.0.0/dist/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js" integrity="sha384-ApNbgh9B+Y1QKtv3Rn7W3mgPxhU9K/ScQsAP7hUibX39j7fakFPskvXusvfa0b4Q" crossorigin="anonymous"></script>
    <script src="../WMSTheme/bootstrap-4.0.0/dist/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>
  <%--  <script src="../javascript/QueryBuilder.js"></script>--%>
    <script src="../javascript/ImportDesigner.js"></script>
   <%-- <link href="../css/QueryBuilder.css" rel="stylesheet" />--%>
        <link href="../css/ImportDesigner.css" rel="stylesheet" />
<%--    <script src="../WMSTheme/jquery-ui/jquery-ui.js"></script>--%>
     <style>
        #tabletoolbar{
            width:auto;
        }
        #tblRecordHolder select,
        #tblRecordHolder input{
            width:100%;
            displaÿ:block;
        }
        #tempDataHolder {
	        overflow-y: auto;
             overflow-x: auto;
	        width: 100%;
	        height: 400px;
        }
        #tempPaging a{
            display:inline-block;            
            border:solid 1px #cccccc;
            background-color:#ffffff;
            border-radius:25px;
            width: 30px;
            height: 30px;
            margin: 2px;
        }
            #tempPaging a span {
                display:table-cell;
                 width: 30px;
                height: 30px;
                text-align:center;
                vertical-align:middle;
            }
            #tempPaging a.activepaging {
                  border:solid 1px #cccccc;
                background-color:var(--wms-color-primary);
            }
            #tempPaging a.activepaging span{                
                color:#ffffff;
            }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHolder_Form" runat="server">
    <asp:HiddenField ID="hdnQueryId" ClientIDMode="Static" runat="server" Value="0" />
    <asp:HiddenField ID="hdnHtmlColumnsObj" ClientIDMode="Static" runat="server" Value="" />
    <asp:HiddenField ID="hdnHtmlWhereObj" ClientIDMode="Static" runat="server" Value="" />
     <asp:HiddenField ID="hdnfinalquery" ClientIDMode="Static" runat="server"/>
     <asp:HiddenField ID="hdnwhere" ClientIDMode="Static" runat="server"/>
     <asp:HiddenField ID="hdnImportID" ClientIDMode="Static" runat="server"/>
    <asp:HiddenField ID="hdncustomerid" ClientIDMode="Static" runat="server" />
    <asp:ValidationSummary ID="validationsummary_UserCreation" runat="server" ShowMessageBox="true"
        ShowSummary="false" DisplayMode="bulletlist" ValidationGroup="Save" />
    <center>
         <%--<asp:UpdatePanel ID="UpdatePanelTabPanelCompanyList" runat="server">
        <ContentTemplate>--%>
        <asp:TabContainer ID="TabContainerQueryBuilder" runat="server" ActiveTabIndex="1">
            <asp:TabPanel ID="tabQueryBuilderList" runat="server" TabIndex="1" HeaderText="<i class='fas fa-list'></i>My Import List" >
                <ContentTemplate>
                    <table class="gridFrame" width="100%">
                        <tr class="themeWMSGridHeaderRow" style="height: 40px">
                            <td>
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            <!-- <label id="lblskulist" style="font-size: 15px; font-weight: bold;" >SKU List</label>-->
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                 <div class="wmsThemeMobileGridMain">
                                    <div class="wmsThemeMobileGridHolder">
                                        <div class="wmsThemeMobileGrid">
                                            <obout:Grid ID="grvQueryList" runat="server" AutoGenerateColumns="False" AllowAddingRecords="False" CallbackMode="true" ShowLoadingMessage="true" Serialize="true"
                                                AllowFiltering="True" AllowGrouping="True" PageSize="-1" AllowMultiRecordSelection="False" OnRebind="grvQueryList_Rebind"
                                                AllowRecordSelection="true" Width="100%" AllowColumnResizing="true" AllowColumnReordering="true">
                                                <ScrollingSettings ScrollHeight="250" />
                                                <Columns>
                                                    <obout:Column HeaderText="Edit" DataField="ImportID" Width="10%" Align="center" HeaderAlign="center" AllowFilter="false">
                                                        <TemplateSettings TemplateId="TemplateEdit" />
                                                    </obout:Column>
                                                    <%-- <obout:Column HeaderText="ImportId" DataField="ImportID" Width="15%" ShowFilterCriterias="false"  />--%>
                                                    
                                                    <obout:Column HeaderText="Import Title" DataField="importTilte" Width="20%" ShowFilterCriterias="false" />
                                                    <obout:Column HeaderText="Description" DataField="Description" Width="20%" ShowFilterCriterias="false" />
                                                    <obout:Column HeaderText="Object" DataField="ObjectName" Width="15%" ShowFilterCriterias="false" />
                                                     <obout:Column HeaderText="Company" DataField="CompanyName" Width="15%" ShowFilterCriterias="false" />
                                                     <obout:Column HeaderText="Customer" DataField="CustomerName" Width="15%" ShowFilterCriterias="false" />
                                                    <obout:Column HeaderText="Creation Date" DataField="CreationDate" Width="15%" ShowFilterCriterias="false" DataFormatString="{0:dd-MM-yyyy}" />
                                                </Columns>
                                                <FilteringSettings FilterPosition="Top" InitialState="Hidden" />
                                                <Templates>   
                                                            <obout:GridTemplate runat="server" ID="TemplateEdit">
                                                        <Template>
                                                            <asp:ImageButton ID="imgBtnEdit1" runat="server" ImageUrl="../App_Themes/Blue/img/Edit16.png"
                                                                OnClick="imgBtnEdit_OnClick" ToolTip='<%# (Container.Value) %>' CausesValidation="false" />
                                                        </Template>
                                                    </obout:GridTemplate>
                                               

                                               
                                               
                                                </Templates>
                                            </obout:Grid>
                                        </div>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel ID="tabQueryBuilderEditor"  runat="server" TabIndex="2" HeaderText="<i class='fas fa-database'></i>Import Designer">
                <ContentTemplate>
                      <!-- Bootstrap Code Starts Here -->
                    <div class="container-fluid">
                        <!-- Row Start -->
                      <div class="row">
                        <div class="col-lg-3">
                            <!-- Cell Content -->
                                <div class="container-fluid">
	                                <div class="row">
		                                <div class="col-md-4">
			                                <!-- Label -->
                                             <%-- <div class="winQueryHeader">Object:</div>--%>
                                            Object:
			                                <!-- Label -->
		                                </div>
		                                <div class="col-md-8">
			                                <!-- Control -->
                                         <asp:DropDownList ID="ddlobject" runat="server" DataTextField="ObjectName" DataValueField="ObjectID" ClientIDMode="Static" onchange="CheckImportData(this.value);"></asp:DropDownList>
			                                <!-- Control -->
		                                </div>
	                                </div>
                                </div>
                            <!-- Cell Content -->
                        </div>
                        <div class="col-lg-4">
                            <!-- Cell Content -->
                                <div class="container-fluid">
	                                <div class="row">
		                                <div class="col-md-4">
			                                <!-- Label -->
                                             <%-- <div class="winQueryHeader">Customer:</div> --%>
                                            Customer:
			                                <!-- Label -->
		                                </div>
		                                <div class="col-md-8">
			                                <!-- Control -->
                                             <asp:DropDownList ID="ddlcustomer" runat="server"  DataTextField="Name" DataValueField="ID" onchange="CheckImportData(this.value);"></asp:DropDownList>
			                                <!-- Control -->
		                                </div>
	                                </div>
                                </div>
                            <!-- Cell Content -->
                        </div>
                        <div class="col-lg-4">
                            <!-- Cell Content -->
                                <div class="container-fluid">
	                                <div class="row">
		                                <div class="col-md-4">
			                                <!-- Label -->
                                            
			                                <!-- Label -->
		                                </div>
		                                <div class="col-md-8">
			                                <!-- Control -->
                                            
			                                <!-- Control -->
		                                </div>
	                                </div>
                                </div>
                            <!-- Cell Content -->
                        </div>
                      </div>
                     <!-- Row End -->
                    </div>
                    <!-- Bootstrap Code Ends Here -->
                    <%-- <!-- Bootstrap Code Starts Here -->
                    <div class="container-fluid">
                        <!-- Row Start -->
                      <div class="row">
                        <div class="col-lg-12">
                            <!-- Cell Content -->
                                <div class="container-fluid">
	                                <div class="row">
		                                <div class="col-md-1" >
			                                <!-- Label -->
                                            <%-- <div class="winQueryHeader">Object:</div>--%>
                                          
			                                <!-- Label -->
		                              <%--  </div>
		                                <div class="col-md-2">--%>
			                                <!-- Control -->
                                              <%--    <asp:DropDownList ID="ddlobject" runat="server"  DataTextField="Name" DataValueField="ID"></asp:DropDownList>--%>
			                                <!-- Control -->
		                               <%-- </div>
                                         <div class="col-md-9">--%>
			                                <!-- Control -->
                                                 
			                          <%--      <!-- Control -->
		                                </div>
	                                </div>
                                </div>--%>
                            <!-- Cell Content -->
                       <%-- </div>
                   
                      </div>
                     <!-- Row End -->
                    </div>--%>
                    <!-- Bootstrap Code Ends Here -->
                    <div class="container-fluid">
                        <div class="row">
                            <div class="col-md-3">
                                <!-- Section for Object List -->
                                <div class="winQueryBox">
                                    <div class="winQueryHeader">Name of View</div>
                                    <div class="winQueryContent">
                                        <div class="winQueryObjects">
                                            <ul id="dataObjectHolder" runat="server" clientidmode="Static">
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                                <asp:HiddenField ID="hdnObjectName" ClientIDMode="Static" runat="server" Value="" />
                                <!-- Section for Object List -->
                            </div>
                            <div class="col-md-8">
                                <!-- Section for Object List -->
                                <div class="winQueryBox">
                                    <%--<div class="winQueryHeader" id="winQueryHeader"><table width="100%"><tr><td style="width:30%">Column Name</td><td style="width:10%">Sequnce</td><td style="width:20%">Datatype</td><td style="width:10%">IsNull</td><td style="width:20%">DateFormate</td><td style="width:10%">Lenght</td></tr></table></div>--%>
                                    <asp:HiddenField ID="hdngetSaveColumnData" ClientIDMode="Static" runat="server" />
                                <div class="winQueryHeader" id="winQueryHeader"><table width="100%" style="text-align:left"><tr><td style="width:15%">Column Name</td><td style="width:10%"></td><td style="text-align:center; width:15%">Datatype</td><td style="width:5%">Is Null</td><td style="width:25%">DataType Length</td></tr></table></div>
                                    <div class="winQueryContent">
                                        <div class="winQueryColumnNames">
                                            <%--<div id="dataColumnHolder">--%>
                                              
                                                    <ul id="dataColumnHolder" runat="server" clientidmode="Static"></ul>
                                                
                                         <%--   </div>--%>
                                            <br />
                                        </div>
                                    </div>
                                </div>
                                <!-- Section for Object List -->
                            </div>
                          <%--  <div class="col-md-6">
                                <!-- Section for Object List -->
                                <div class="winQueryBox">
                                    <div class="winQueryHeader" id="winQueryConditionHeader">Condition</div>
                                    <div class="winQueryContent" style="height: 44px; overflow: hidden;">
                                        <div class="winQueryColumnNames">
                                            <div>
                                                <!-- Condition Row -->
                                                <div class="container-fluid">
                                                    <div class="row">
                                                        <div class="col-md-3" id="txtSqlConditionColumn">
                                                        </div>
                                                        <div class="col-md-3">
                                                            <select id="txtSqlCondition">
                                                                <option>=</option>
                                                                <option>!=</option>
                                                                <option><></option>
                                                                <option><</option>
                                                                <option><=</option>
                                                                <option>></option>
                                                                <option>>=</option>
                                                                <option>LIKE</option>
                                                                <option>NOT LIKE</option>
                                                                <%-- <option>NOT EQUAL TO</option> --%>
                                                          <%--  </select>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <input type="text" value="" id="txtSqlConditionValue" />
                                                        </div>
                                                        <div class="col-md-3">
                                                            <div class="themeWMS_ctrl_btn wms_show_ctrl">
                                                                <i class="fas fa-plus"></i>
                                                                <input type="button" value="Add" class="buttonON" style="height: 24px; width: 65px;" onclick="saveAndAddCondition();">
                                                            </div>
                                                            <!-- <a href="#"><i class="fas fa-pencil-alt"></i></a> | <a href="#" onclick="saveAndAddCondition();"><i class="fas fa-plus"></i></a> -->
                                                        </div>
                                                    </div>
                                                </div>
                                                <!-- Condition Row -->
                                            </div>
                                            <br />
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <!-- Section for Object List -->
                                <!-- Section for Query Rules -->
                                <div class="winQueryBox">
                                    <div class="winQueryHeader">
                                        Conditions 
                                    <div class="sqlGroupConditions">
                                        Group Bracket / Bracket Conditions: 
                                        <select id="txtSqlGroupBrackets">
                                            <option>-- Select --</option>
                                            <option>(</option>
                                            <option>)</option>
                                            <option>AND</option>
                                            <option>OR</option>
                                            <option>NOT</option>
                                            <option>LIKE</option>
                                            <option>=</option>
                                            <option>></option>
                                            <option>>=</option>
                                            <option><</option>
                                            <option><=</option>
                                        </select>
                                        <a href="#" onclick="addSqlGroupCondition();return false;">ADD</a>
                                    </div>
                                    </div>
                                    <div class="winQueryContent" style="height: 154px;">
                                        <div class="winQueryColumnNames">
                                            <div>
                                                <!-- Condition Row -->
                                                <div class="container-fluid" id="dataConditionHolder">
                                                </div>
                                                <!-- Condition Row -->
                                            </div>
                                            <br />
                                        </div>
                                    </div>
                                </div>
                                                               </div>--%>
                                <!-- Section for Query Rules -->
                           
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <asp:UpdatePanel ID="UpdatePanelTabPanelQueryBuilder" runat="server">
                                    <ContentTemplate>
                                        <asp:UpdateProgress ID="progress1" runat="server">
                                            <ProgressTemplate>
                                                <asp:Panel ID="pnlProgressBar" runat="server" CssClass="ProgressBarPanel">
                                                    <div class="themeWMSLoading" style="display: block;">
                                                        <div class="themeWMSModelHolder">
                                                            <div class="themeWMSModel">
                                                                <img src="../App_Themes/Blue/img/ajax-loader.gif" style="top: 50%;"><span class="themeWMSLoadingProcessing"><br />
                                                                    Please Wait. Processing...</span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                        <br />
                                       <%-- <asp:TabContainer ID="TabContainerQueryResult" runat="server">
                                            <asp:TabPanel ID="TabPanelUserInformation" runat="server" HeaderText="<i class='fas fa-code'></i>SQL Query Text" CssClass="winQuerySQLConsole">
                                                <ContentTemplate>--%>
                                                    <asp:HiddenField ID="hdnSQLQueryText" runat="server" ClientIDMode="Static" Value="" />
                                                    <%--   <input type="hidden" id="hdnSQLQueryText" name="hdnSQLQueryText" value="" />--%>
                                    <%--                <asp:Label ID="lblSQLQueryText" ClientIDMode="Static" runat="server" Text="SQL -> "></asp:Label>
                                                    <asp:Button ID="btnExecuteQuery" runat="server" CssClass="btnExecuteSqlQuery" Text="Execute Query" OnClientClick="return callAPItoGetQueryResultNew();" />--%>
                                                    <%--   PostBackUrl="~/QueryBuilder/QueryBuilderResult.aspx"--%>
                                                    <%-- onClick="executeSqlQuery"--%>
                                              <%--  </ContentTemplate>
                                            </asp:TabPanel>
                                            <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="<i class='fas fa-list'></i>SQL Result">
                                                <ContentTemplate>
                                                    <table class="gridFrame" style="width: 100%;display:none;">
                                                        <tr class="themeWMSGridHeaderRow">
                                                            <td>
                                                                <table style="width: 100%">
                                                                    <tr>
                                                                        <td style="text-align: left;">
                                                                            <!-- <label id="lblheader" class="headerText">User List</label>-->
                                                                        </td>
                                                                        <td align="right"></td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div class="wmsThemeMobileGridMain">
                                                                    <div class="wmsThemeMobileGridHolder">
                                                                        <div class="wmsThemeMobileGrid">
                                                                            <obout:Grid ID="grdSqlQueryResult" runat="server" ClientIDMode="Static" AllowAddingRecords="false" PageSize="-1" CallbackMode="false" Serialize="true" ShowLoadingMessage="true" AllowPaging="true" AllowPageSizeSelection="true" AllowManualPaging="true"
                                                                                AllowGrouping="True" AllowFiltering="True" Width="100%" AutoGenerateColumns="true" OnRebind="grdSqlQueryResult_Rebind">
                                                                                <ScrollingSettings ScrollWidth="100%" />
                                                                                <Columns>
                                                                                </Columns>
                                                                                <FilteringSettings InitialState="Hidden" FilterPosition="Top" />
                                                                                <Templates>
                                                                                    <obout:GridTemplate ID="GvTempEdit" runat="server">
                                                                                        <Template>
                                                                                            <asp:ImageButton ID="imgBtnEdit" CausesValidation="false" runat="server" ImageUrl="../App_Themes/Blue/img/Edit16.png" />
                                                                                        </Template>
                                                                                    </obout:GridTemplate>
                                                                                </Templates>
                                                                            </obout:Grid>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                     <asp:Panel ID="tempDataHolder" runat="server" ClientIDMode="Static">
        
                                                  </asp:Panel>
                                                </ContentTemplate>
                                            </asp:TabPanel>
                                        </asp:TabContainer>--%>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>

                </ContentTemplate>
            </asp:TabPanel>

        </asp:TabContainer>
           <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
     
        <div id="divSqlResult"></div>
        <%--<div class="themeWmsSqlQueryPopup" style="display: none;">
            <div class="themeWmsPopupContent">
                <div class="themeWmsPopupTitle">
                    SQL Query Result
                    <div class="themeWMS_ctrl_btn wms_show_ctrl" id="btnCloseSqlQueryPopup" onclick="closeSqlQueryPopup();"><i class="fas fa-times-circle"></i></div>
                </div>
                <div class="themeWmsPopupContentBody">
                    <iframe id="iframeSqlQueryResult" name="iframeSqlQueryResult" src="" style="width: 100%; height: 300px;" frameborder="0"></iframe>
                </div>
            </div>
            <div class="themeWmsPopupBg">
            </div>
        </div>--%>
    </center>
    <div class="themeWmsPopup" id="themeQueryBuilderTitlePopup" style="display: none;">
        <div class="themeWmsPopupContent" style="display: table; margin-top: 30px;">
            <div class="themeWmsPopupTitle">
                Query Builder
                <div class="themeWMS_ctrl_btn wms_show_ctrl" id="btnCloseSqlTitlePopup" onclick="closeSqlQueryTitlePopup();"><i class="fas fa-times-circle"></i></div>
            </div>
            <div class="themeWmsPopupContentBody">
                <span class="themeWMSPopupLabel"><b>Title</b></span><div class="themeWMS_ctrl wms_ctrl_title">
                    <i class="fas fa-tag"></i>
                    <asp:TextBox ID="txtShortcutTitle" ClientIDMode="Static" runat="server" Text="" CssClass="inputElement" Style="width: 300px"></asp:TextBox>
                </div>
                <br />
                <br />
                <span class="themeWMSPopupLabel"><b>&nbsp;</b></span>
                <div class="themeWMS_ctrl_btn wms_show_ctrl">
                    <i class="fas fa-plus" style="top: 11px; left: 12px;"></i>
                    <asp:Button ID="btnSaveQueryBuilder" runat="server" Text="Save Query" CssClass="buttonON"  />
                    <%-- <input type="button" value="Save Shortcut" class="buttonON" onclick="saveShortCut();" />--%>
                </div>

            </div>
        </div>
        <div class="themeWmsPopupBg">
        </div>
    </div>
    <div class="themeWmsPopup" id="themeQueryBuilderSetNotification" style="display: none;">
        <div class="themeWmsPopupContent" style="display: table; margin-top: 30px;">
            <div class="themeWmsPopupTitle">
                Query Builder Notification
                <div class="themeWMS_ctrl_btn wms_show_ctrl" id="btnCloseSqlNotificationPopup" onclick="closeSqlQueryNotificationPopup();"><i class="fas fa-times-circle"></i></div>
            </div>
            <div class="themeWmsPopupContentBody">
                <asp:HiddenField ID="hdnQueryIdForNotification" runat="server" ClientIDMode="Static" Value="0" />
                <asp:HiddenField ID="hdnNotificationId" runat="server" ClientIDMode="Static" Value="0" />
                <asp:HiddenField ID="hdnQueryInterval" runat="server" ClientIDMode="Static" Value="" />
                <asp:HiddenField ID="hdnQuerySendAt" runat="server" ClientIDMode="Static" Value="" />
                <asp:HiddenField ID="hdnQueryDayOfWeek" runat="server" ClientIDMode="Static" Value="" />
                <asp:HiddenField ID="hdnQueryDayOfMonth" runat="server" ClientIDMode="Static" Value="" />

               

                <div id="divQbNotificationFor" class="divQbFormRow">
                    <span class="themeWMSPopupLabel"><b>Notification To</b></span><div class="themeWMS_ctrl">
                        <i class="fas fa-address-book"></i>
                        <asp:TextBox ID="txtQueryNotificationTo" ClientIDMode="Static" runat="server" Text="abhijit@brilliantinfosys.com" CssClass="inputElement" Style="width: 300px"></asp:TextBox>
                    </div>
                </div>

                <div id="divQbInterval" class="divQbFormRow">
                    <span class="themeWMSPopupLabel"><b>Interval</b></span>
                    <div class="queryBuilderScheduleInterval" data-value="DAILY">
                        <div id="intDaily" data-value="DAILY" class="qbInteralSelected">DAILY</div>
                        <div id="intWeekly" data-value="WEEKLY">WEEKLY</div>
                        <div id="intMonthly" data-value="MONTHLY">MONTHLY</div>
                    </div>
                </div>

                <div id="divQbSentAt" class="divQbFormRow">
                    <span class="themeWMSPopupLabel"><b>Send At</b></span>

                    <asp:DropDownList ID="qbDDListHH" ClientIDMode="Static" runat="server">
                        <asp:ListItem>--HH--</asp:ListItem>
                        <asp:ListItem>01</asp:ListItem>
                        <asp:ListItem>02</asp:ListItem>
                        <asp:ListItem>03</asp:ListItem>
                        <asp:ListItem>04</asp:ListItem>
                        <asp:ListItem>05</asp:ListItem>
                        <asp:ListItem>06</asp:ListItem>
                        <asp:ListItem>07</asp:ListItem>
                        <asp:ListItem>08</asp:ListItem>
                        <asp:ListItem>09</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>11</asp:ListItem>
                        <asp:ListItem>12</asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="qbDDListMM" ClientIDMode="Static" runat="server">
                        <asp:ListItem>--MM--</asp:ListItem>
                        <asp:ListItem>00</asp:ListItem>
                        <asp:ListItem>15</asp:ListItem>
                        <asp:ListItem>30</asp:ListItem>
                        <asp:ListItem>45</asp:ListItem>

                        
                    </asp:DropDownList>
                    <asp:DropDownList ID="qbDDListAMPM" ClientIDMode="Static" runat="server">
                        <asp:ListItem>AM</asp:ListItem>
                        <asp:ListItem>PM</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div id="divDayOfWeek" class="divQbFormRow">
                    <span class="themeWMSPopupLabel"><b>Day of Week</b></span>
                    <div class="queryBuilderScheduleDay">
                        <div id="ntdMON" data-value="MON" data-isselected="no"><i class="far fa-check-square"></i>MON</div>
                        <div id="ntdTUE" data-value="TUE" data-isselected="no"><i class="far fa-check-square"></i>TUE</div>
                        <div id="ntdWED" data-value="WED" data-isselected="no"><i class="far fa-check-square"></i>WED</div>
                        <div id="ntdTHU" data-value="THU" data-isselected="no"><i class="far fa-check-square"></i>THU</div>
                        <div id="ntdFRI" data-value="FRI" data-isselected="no"><i class="far fa-check-square"></i>FRI</div>
                        <div id="ntdSAT" data-value="SAT" data-isselected="no"><i class="far fa-check-square"></i>SAT</div>
                        <div id="ntdSUN" data-value="SUN" data-isselected="no"><i class="far fa-check-square"></i>SUN</div>
                    </div>
                </div>
                <div id="divDayOfMonth" class="divQbFormRow">
                    <span class="themeWMSPopupLabel"><b>Day of Month</b></span>
                    <div class="queryBuilderScheduleWeek">
                        <div id="dmFirstDayOfMonth" data-value="FirstDayOfMonth" data-isselected="no"><i class="far fa-dot-circle"></i>First Day of Month</div>
                        <div id="dmLastDayOfMonth" data-value="LastDayOfMonth" data-isselected="no"><i class="far fa-circle"></i>Last Day of Month</div>
                        <div id="dmMidDayOfMonth" data-value="MidDayOfMonth" data-isselected="no"><i class="far fa-circle"></i>Specific Day of Month</div>
                    </div>
                </div>
                <div id="divQbCalendarDate" class="divQbFormRow">
                    <span class="themeWMSPopupLabel"><b>Select Day</b></span>

                    <asp:DropDownList ID="qbDDListDate" ClientIDMode="Static" runat="server">
                        <asp:ListItem>--Select Date--</asp:ListItem>
                        <asp:ListItem>01</asp:ListItem>
                        <asp:ListItem>02</asp:ListItem>
                        <asp:ListItem>03</asp:ListItem>
                        <asp:ListItem>04</asp:ListItem>
                        <asp:ListItem>05</asp:ListItem>
                        <asp:ListItem>06</asp:ListItem>
                        <asp:ListItem>07</asp:ListItem>
                        <asp:ListItem>08</asp:ListItem>
                        <asp:ListItem>09</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>11</asp:ListItem>
                        <asp:ListItem>12</asp:ListItem>
                        <asp:ListItem>13</asp:ListItem>
                        <asp:ListItem>14</asp:ListItem>
                        <asp:ListItem>15</asp:ListItem>
                        <asp:ListItem>16</asp:ListItem>
                        <asp:ListItem>17</asp:ListItem>
                        <asp:ListItem>18</asp:ListItem>
                        <asp:ListItem>19</asp:ListItem>
                        <asp:ListItem>20</asp:ListItem>
                        <asp:ListItem>21</asp:ListItem>
                        <asp:ListItem>22</asp:ListItem>
                        <asp:ListItem>23</asp:ListItem>
                        <asp:ListItem>24</asp:ListItem>
                        <asp:ListItem>25</asp:ListItem>
                        <asp:ListItem>26</asp:ListItem>
                        <asp:ListItem>27</asp:ListItem>
                        <asp:ListItem>28</asp:ListItem>
                        <asp:ListItem>29</asp:ListItem>
                        <asp:ListItem>30</asp:ListItem>
                        <asp:ListItem>31</asp:ListItem>
                    </asp:DropDownList>

                </div>
                <div id="divQbSaveNotification">
                    <span class="themeWMSPopupLabel"><b>&nbsp;</b></span>
                    <div class="themeWMS_ctrl_btn wms_show_ctrl" id="divBtnSaveNotification">
                        <i class="fas fa-plus" style="top: 11px; left: 12px;"></i>
                        <asp:Button ID="Button1" runat="server" Text="Save Notification" CssClass="buttonON"  />                        
                    </div>
                </div>
            </div>
        </div>
        <div class="themeWmsPopupBg">
        </div>
    </div>
</asp:Content>
