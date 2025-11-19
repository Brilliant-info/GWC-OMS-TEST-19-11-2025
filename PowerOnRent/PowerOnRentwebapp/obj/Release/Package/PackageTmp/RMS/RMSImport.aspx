<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/CRM.Master" AutoEventWireup="true" CodeBehind="RMSImport.aspx.cs" Inherits="PowerOnRentwebapp.RMS.RMSImport" Theme="Blue" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <script src="../core-js/HttpLoader.js" language="javascript" type="text/javascript"></script>
    <%--<link rel="stylesheet" href="Themes/bootstrap-4.0.0/dist/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous" type="text/javascript" />--%>

  <%--  <script src="Themes/bootstrap-4.0.0/dist/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl"
        crossorigin="anonymous" type="text/javascript"></script>--%>
    <script src="ThemeHTML/js/wms-theme.js" type="text/javascript"></script>
    <script src="ThemeHTML/js/wms-app-navigator.js" type="text/javascript"></script>
    <script src="../ThemeHTML/js/Import.js" type="text/javascript"></script>
    <style>
        #pnlImport *, #pnlImport  ::after, #pnlImport  ::before {
	        box-sizing: border-box;
        }
        #pnlImport .container-fluid {
	        width: 100%;
	        padding-right: 15px;
	        padding-left: 15px;
	        margin-right: auto;
	        margin-left: auto;
        }
        #pnlImport .col-md-4 {
	        -webkit-box-flex: 0;
	        -ms-flex: 0 0 33.333333%;
	        flex: 0 0 33.333333%;
	        max-width: 33.333333%;
        }
        #pnlImport .row {
	        display: -webkit-box;
	        display: -ms-flexbox;
	        display: flex;
	        -ms-flex-wrap: wrap;
	        flex-wrap: wrap;
	        margin-right: -15px;
	        margin-left: -15px;
        }
        #pnlImport .col-lg-6 {
	        -webkit-box-flex: 0;
	        -ms-flex: 0 0 50%;
	        flex: 0 0 50%;
	        max-width: 50%;
        }
        #pnlImport .col-lg-12 {
	        -webkit-box-flex: 0;
	        -ms-flex: 0 0 100%;
	        flex: 0 0 100%;
	        max-width: 100%;
        }
        #pnlImport.col, #pnlImport .col-1, #pnlImport .col-10, #pnlImport .col-11, #pnlImport .col-12, #pnlImport .col-2, #pnlImport .col-3, #pnlImport .col-4, #pnlImport .col-5, #pnlImport .col-6, #pnlImport .col-7, #pnlImport .col-8, #pnlImport .col-9, #pnlImport .col-auto, #pnlImport .col-lg, #pnlImport .col-lg-1, #pnlImport .col-lg-10, #pnlImport .col-lg-11, #pnlImport .col-lg-12, #pnlImport .col-lg-2, #pnlImport .col-lg-3, #pnlImport .col-lg-4, #pnlImport .col-lg-5, #pnlImport .col-lg-6, #pnlImport .col-lg-7, #pnlImport .col-lg-8, #pnlImport .col-lg-9, #pnlImport .col-lg-auto, #pnlImport .col-md, #pnlImport .col-md-1, #pnlImport .col-md-10, #pnlImport .col-md-11, #pnlImport .col-md-12, #pnlImport .col-md-2, #pnlImport .col-md-3, #pnlImport .col-md-4, #pnlImport .col-md-5, #pnlImport .col-md-6, #pnlImport .col-md-7, #pnlImport .col-md-8, #pnlImport .col-md-9, #pnlImport .col-md-auto, #pnlImport .col-sm, #pnlImport .col-sm-1, #pnlImport .col-sm-10, #pnlImport .col-sm-11, #pnlImport .col-sm-12, #pnlImport .col-sm-2, #pnlImport .col-sm-3, #pnlImport .col-sm-4, #pnlImport .col-sm-5, #pnlImport .col-sm-6, #pnlImport .col-sm-7, #pnlImport .col-sm-8, #pnlImport .col-sm-9, #pnlImport .col-sm-auto, #pnlImport .col-xl, #pnlImport .col-xl-1, #pnlImport .col-xl-10, #pnlImport .col-xl-11, #pnlImport .col-xl-12, #pnlImport .col-xl-2, #pnlImport .col-xl-3, #pnlImport .col-xl-4, #pnlImport .col-xl-5, #pnlImport .col-xl-6, #pnlImport .col-xl-7, #pnlImport .col-xl-8, #pnlImport .col-xl-9, #pnlImport .col-xl-auto{
	        position: relative;
	        width: 100%;
	        min-height: 1px;
	        padding-right: 15px;
	        padding-left: 15px;
        }
        #main{
            height:auto !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHolder_FormHeader" runat="server">
        <asp:HiddenField ID="hdnUserIDimp" ClientIDMode="Static" runat="server" />
        <asp:HiddenField ID="hdnCompanyIdimp" ClientIDMode="Static" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHolder_Form" runat="server">

    <div id="pnlImport" class="wmsMasterTypePage" style="display: block;">
    <input type="hidden" value="" id="hdnImportSessionId" />
    <div class="wms-srv-content-area pnlInbound" id="pnlAppointmentList">
        <div class="wms-srv-page-title">
            <i class="fas fa-file-import"></i> <span class="wms-srv-data-title">Import</span>
        </div>
        <!-- <hr style="border-bottom: 2px dashed #636363;"> -->
        <div class="container-fluid">
            <div class="row import_wizard" id="import_wizard_grid" style="display:none;">
                <div class="col-lg-6 import_wizard_left">
                    <div class="importMenuItems btnopenimportwizardsteps" data-obj="RMS Import" id="dvpoimport">
                        <a href="#"><i class="fas fa-list"></i> RMS Import</a>
                    </div>
                </div>
            </div>
        </div>
        <div class="WMSImportWizard" id="import_wizard_steps" style="display: none;">
            <hr class="WmsImport_Wizard_Steps_Line" />
            <div class="WmsImport_Wizard_Steps">
                <div id="step_filetype" class="WmsImport_Wizard_Steps_Holder activeStep">
                    <div class="stepsNumberHolder">
                        <span><i class="fas fa-file"></i>
                        </span>
                    </div>
                    <div class="stepsLable">File Type</div>
                </div>
                <div id="step_mapcolumns" class="WmsImport_Wizard_Steps_Holder">
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
            <!-- Page -->
            <div class="WMSImport_StepsPages" id="WmsPage_step_filetype">
                <div class="WmsImport_Title">
                    <div class="themeWMSAjaxTabPageTitle"><i class="fas fa-file"></i><span>Import Excel Wizard</span>
                    </div>
                </div>
                <!-- Bootstrap Code Starts Here -->
                <div class="container-fluid">
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
                                        <asp:DropDownList ID="ddlcompanymain" CssClass="inputElement"
                                            ClientIDMode="Static" runat="server" Width="187px" DataTextField="Name"
                                            DataValueField="ID" onchange="GetCustomer()">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ForeColor="Maroon"
                                            ControlToValidate="ddlcompanymain" InitialValue="0" ValidationGroup="Save"
                                            runat="server" ErrorMessage="Please Select Company" Display="None">
                                        </asp:RequiredFieldValidator>
                                        <!-- Control -->
                                    </div>
                                </div>
                            </div>
                            <!-- Cell Content -->
                        </div>
                    </div>
                </div>
                <!-- Bootstrap Code Ends Here -->
                <div class="WmsImport_TypeHolder">
                    <div class="container-fluid">
                        <div class="row">
                              <div class="col-md-4">                             
                               <%-- <div class="WmsImport_Type" data-type="CSV" style=" cursor:pointer;">
                                    <div class="WmsImport_Icon">
                                        <i class="fas fa-file-csv"></i>
                                    </div>
                                    <div class="WmsImport_Description">
                                        CSV
                                    </div>
                                </div>
                                
                                <div class="WmsImport_DownloadTemplate">
                                    <a href="#" onclick="downloadTemplate('csv');return false;"
                                        class="wmsWizNavNext wmsWizNavActive"><i class="fas fa-download"></i> Download
                                        Template</a>
                                </div>--%>
                            </div>
                            <div class="col-md-4">
                                <!-- Export to Excel -->
                                <div class="WmsImport_Type" data-type="Excel" style="opacity:1; cursor:pointer;">
                                    <div class="WmsImport_Icon">
                                        <i class="fas fa-file-excel"></i>
                                    </div>
                                    <div class="WmsImport_Description">
                                        Excel Import
                                    </div>
                                </div>
                                <!-- Export to Excel -->
                                <div class="WmsImport_DownloadTemplate" style="opacity:1;">
                                    <a href="#" class="wmsWizNavNext wmsWizNavActive" id="exceltemp"
                                        onclick="downloadTemplate('excels');return false;" ><i
                                            class="fas fa-download"></i>
                                        Download Template</a>
                                    <a href="#" class="wmsWizNavNext wmsWizNavActive" id="Instructions"                                        onclick="OpenInstruction();return false;" ><i></i>Instructions</a>
                                </div>
                            </div>
                          
                         <div class="col-md-4">
                             
                               <%-- <div class="WmsImport_Type" data-type="Text" style="opacity:1;cursor:pointer;">
                                    <div class="WmsImport_Icon">
                                        <i class="fas fa-file-alt"></i>
                                    </div>
                                    <div class="WmsImport_Description">
                                        Text
                                    </div>
                                </div>
                              
                                <div class="WmsImport_DownloadTemplate" style="opacity:1; ">
                                    <a href="#" class="wmsWizNavNext wmsWizNavActive"
                                        onclick="downloadTemplate('txt');return false;"><i class="fas fa-download"></i>
                                        Download
                                        Template</a>
                                </div>--%>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="WmsImport_NavHolder" style="display:none;">
                    <asp:LinkButton ID="wizSetupBack" runat="server" CssClass="wmsWizNavBack">
                        <i class="fas fa-arrow-alt-circle-left"></i>Back
                    </asp:LinkButton>
                    <asp:LinkButton ID="wizSetupNext" runat="server" CssClass="wmsWizNavNext wmsWizNavActive">
                        Next<i class="fas fa-arrow-alt-circle-right"></i>
                    </asp:LinkButton>
                </div>
            </div>
            <!-- Page -->
            <div class="WMSImport_StepsPages" id="WmsPage_step_mapcolumns">
                <div class="WmsImport_Title">
                    <div class="themeWMSAjaxTabPageTitle"><i class="fas fa-columns"></i><span>Select file / Map
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
                    <div class="WMSdragDropHolder" ondrop="drop(event)" ondragover="allowDrop(event)"
                        onmouseout="removeDrop(event)">
                        <div class="WMSdragDropPanel">
                            <div class="WMSdragDropLabel">
                                       Drag and drop files here to import
                            </div>
                        </div>
                    </div>
                    <div class="WMSDefaultFileCtrl">Or use default file uploader
                        <asp:FileUpload ID="txtFile" runat="server" ClientIDMode="Static" onchange="setFile(this);" />
                    </div>
                    <!-- Drag Drop Files -->
                </div>
                <div id="fileHeaderContainer" class="fileHeaderContainer" style="display: none;">
                    <div class="row">
                        <div class="col-lg-12">
                            <!-- <hr /> -->
                            <b>Sort or Remove column to adjust your header as per template header. <a href="#"
                                    id="lnkMapHeader" onclick="mapHeaderColumn();return false;"
                                    class="wmsWizNavNext wmsWizNavActive lnkMapHeader">Map Header</a> </b>
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
            <!-- Page -->
            <!-- Page -->
            <div class="WMSImport_StepsPages" id="WmsPage_step_validatefile">
                <div class="WmsImport_Title" id="WmsImport_Title_Validate">
                    <div class="themeWMSAjaxTabPageTitle"><i class="fas fa-clipboard-check"></i><span>Validate
                            File</span>
                    </div>
                </div>

                <div id="wmsImportCountHolder" class="wmsImportCountHolder wmsDashboardCountHolder">
                    <div class="wmsDashboardCountBoxHolder" id="divWmsCompletedCount">
                        <div class="wmsDashboardCountBox" style="cursor: pointer;">
                            <div class="wmsDashboardCountIcon" style="background-color:#548235;">
                                <i class="fas fa-check-circle" style="color:#ffffff"></i>
                            </div>
                            <div class="wmsDashboardCountContent">
                                <div class="wmsDashboardCount" id="wmsCompletedCount">0</div>
                                <div class="wmsDashboardCountLabel">Valid Records</div>
                            </div>
                        </div>
                    </div>&nbsp;
                    <div class="wmsDashboardCountBoxHolder" id="divWmsErrorCount">
                        <div class="wmsDashboardCountBox" style="cursor:pointer;">
                            <div class="wmsDashboardCountIcon" style="background-color:#c82a2a;">
                                <i class="fas fa-times-circle"></i>
                            </div>
                            <div class="wmsDashboardCountContent" style="cursor:pointer;">
                                <div class="wmsDashboardCount" id="wmsErrorCount">0</div>
                                <div class="wmsDashboardCountLabel">Total Error</div>
                            </div>
                        </div>
                    </div>&nbsp;
                    <div class="wmsDashboardCountBoxHolder" id="divWmsWarningCount">
                        <div class="wmsDashboardCountBox" style="cursor:pointer;">
                            <div class="wmsDashboardCountIcon" style="background-color:#ffc000;">
                                <i class="fas fa-equals"></i>
                            </div>
                            <div class="wmsDashboardCountContent">
                                <div class="wmsDashboardCount" id="wmsWarningCount">0</div>
                                <div class="wmsDashboardCountLabel">Total Row</div>
                            </div>
                        </div>
                    </div>&nbsp;
                </div>
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-md-12">
                            <!-- Error Panel -->
                            <div class="validationHolder" id="validationHolder">
                                <!-- Row Start -->
                                <div class="validationHolderRow">
                                    <div class="validationHolderCell">
                                        <div class="validationMessage" id="validationMessage">
                                            Your file contains 3 errors on first page. Kindly fix it before processing
                                            import.
                                        </div>
                                    </div>
                                    <div class="validationHolderCell" style="width: 100px;">
                                        <a href="#" id="lnkValidateCorrection"
                                            onclick="validateTableData();return false;"
                                            class="wmsWizNavNext wmsWizNavActive">
                                            <i class="fas fa-check-circle"></i>Validate
                                        </a>
                                        <a href="#" id="lnkUploadImportData" onclick="return false;"
                                            class="wmsWizNavNext wmsWizNavActive">
                                            <i class="fas fa-check-circle"></i>Upload
                                        </a>
                                    </div>

                                    <div class="validationHolderCell">
                                        <a href="#" id="lnkcancel" onclick="cancleImport();return false;"
                                            class="wmsWizNavNext wmsWizNavActive">
                                            <i class="far fa-times-circle"></i> cancel
                                        </a>
                                        <!-- <%--    <div id="btnfinish" style="position:absolute; top:12px; float:right">--%> -->
                                        <a href="#" id="lnkfinish" onclick="FinishImport();return false;"
                                            class="wmsWizNavNext wmsWizNavActive" style="visibility:hidden">
                                            <i class="far fa-times-circle"></i>Finish
                                        </a>
                                    </div>
                                </div>
                                <!-- Row End -->
                            </div>
                            <!-- Error Panel -->
                        </div>
                        <div class="col-md-3" style="display:none">
                            <div class="WMSFilterPanel">
                                Filter:
                                <select id="WMSFilterOption" class="inputElement" onchange="filterDataTable();">
                                    <option value="showAll">Show All</option>
                                    <option value="completedDataOnly">Show Completed/Valid Data Only</option>
                                    <option value="errorDataOnly">Show Error Data Only</option>
                                    <!-- <%--<option value="warningDataOnly">Show Warning Data Only</option>--%> -->
                                </select>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="WMSImportTableHolder" style="width:600px;height:500px;overflow:auto;">
                    <table border="1" class="WMSImportTable" id="WMSImportTable" width="100%">
                    </table>
                </div>
            </div>
            <!-- Page -->
            <!-- Page -->
            <div class="WMSImport_StepsPages" id="WmsPage_step_import">
                <div class="WmsImport_Title">
                    <div class="themeWMSAjaxTabPageTitle"><i class="fas fa-file-upload"></i><span>Import Data</span>
                    </div>
                </div>
                <div id="prgImportCountHolder" class="wmsImportCountHolder wmsDashboardCountHolder">
                    <div class="wmsDashboardCountBoxHolder" id="prgWmsCompletedCount">
                        <div class="wmsDashboardCountBox">
                            <div class="wmsDashboardCountIcon" style="background-color:#548235;">
                                <i class="fas fa-check-circle" style="color:#ffffff"></i>
                            </div>
                            <div class="wmsDashboardCountContent">
                                <div class="wmsDashboardCount" id="prgCompletedCount">0</div>
                                <div class="wmsDashboardCountLabel">Completed Record </div>
                            </div>
                        </div>
                    </div>&nbsp;
                    <div class="wmsDashboardCountBoxHolder" id="prgWmsErrorCount">
                        <div class="wmsDashboardCountBox">
                            <div class="wmsDashboardCountIcon" style="background-color:#c82a2a;">
                                <i class="fas fa-times-circle"></i>
                            </div>
                            <div class="wmsDashboardCountContent">
                                <div class="wmsDashboardCount" id="prgErrorCount">0</div>
                                <div class="wmsDashboardCountLabel">Total Error</div>
                            </div>
                        </div>
                    </div>&nbsp;
                    <div class="wmsDashboardCountBoxHolder" id="prgWmsWarningCount">
                        <div class="wmsDashboardCountBox">
                            <div class="wmsDashboardCountIcon" style="background-color:#ffc000;">
                                <i class="fas fa-equals"></i>
                            </div>
                            <div class="wmsDashboardCountContent">
                                <div class="wmsDashboardCount" id="prgWarningCount">0</div>
                                <div class="wmsDashboardCountLabel">Total Row</div>
                            </div>
                        </div>
                    </div>&nbsp;
                </div>
                <div class="WMSProgressLabel">
                    <span class="WMSPercentProgress">0% Done </span>
                    <span class="WMSProgressSubLabel">Wait while your data upload is in progress...</span>
                </div>
                <div class="WMSProgressBar">
                    <div id="WMSProgressBarStatus" class="WMSProgressBarStatus" style="width:0%;"></div>
                </div>

            </div>
            <!-- Page -->
        </div>
        <div class="WmsImportOptionPopup" id="WmsImportOptionPopup" style="display:none;">
            <div class="themeWmsPopupContent" style="width:470px;">
                <div class="themeWmsPopupTitle">
                    Import File <div class="themeWMS_ctrl_btn wms_show_ctrl" id="btnCloseImportPopup"><i
                            class="fas fa-times-circle"></i></div>
                </div>
                <div class="themeWmsPopupContentBody">
                    <!-- <%-- <span class="themeWMSPopupLabel"><input id="chkIsHeader" type="checkbox" name="chkIsHeader" checked="checked"> <b> My File Contains Header </b></span>--%> -->
                    <span class="themeWMSPopupLabel" style="margin-top:10px;"><b>Contains Header?</b></span><br />
                    <div>
                        <!-- <%--<select name="txtIsHeader" id="txtIsHeader" class="inputElement notranslate" onchange="setIsContainsHeader(this);">
                    <option value="yes">Contains Header</option>
                    <option value="no">No Header</option>
                </select>--%> -->
                        <select name="txtIsHeader" id="txtIsHeader" class="inputElement notranslate">
                            <option value="yes">Contains Header</option>
                            <option value="no">No Header</option>
                        </select>
                    </div>

                    <div id="pnlFieldSaparator">
                        <span class="themeWMSPopupLabel" style="margin-top:10px;"><b>Field Separator</b></span><br />
                        <select name="txtFieldSaparator" id="txtFieldSaparator" class="inputElement notranslate">
                            <option value="comma">Comma delimited (,)</option>
                            <option value="semicolon">Semi-Colon delimited (;)</option>
                            <option value="tab">Tab delimited (&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;)</option>
                        </select>
                    </div>
                    <div id="divTextPreviewHolder" style="display:none;">
                        <span class="themeWMSPopupLabel" style="margin-top:10px;"><b>Click on desired position to create
                                break
                                line.</b></span><br />
                        <div class="textPreview">
                            <div class="textPreviewPointer">

                            </div>
                            <div class="textPreviewScale">

                            </div>
                            <div class="textPreviewContent" id="textPreviewContent">
                            </div>
                        </div>
                    </div>
                    <div class="themeWMS_ctrl_btn wms_show_ctrl" style="margin-top:10px;"><i class="fas fa-file-upload"
                            style="top:14px;left:16px;"></i>
                        <input type="button" value="Import File" class="buttonON" onclick="saveFileOption();" />
                    </div>
                </div>
            </div>
            <div class="themeWmsPopupBg">
            </div>
        </div>

         <%--START Instruction Popup--%>          <div class="wms-srv-popup-holder" id="wms-srv-Instruction-popup" style="display:none;width: 85%; height: 87%; margin-left: 5%; margin-right: auto;">                                <div class="wms-srv-popup-bg">                                </div>                                <div class="wms-srv-popup" style="width: 100%; height: 100%; margin-left: auto; margin-right: auto;">                                    <div class="wms-srv-popup-content">                                        <!-- Popup Title -->                                        <div class="wms-srv-popup-title">                                        <div class="wms-srv-page-title">                                            <i class="fas fa-file-alt"></i><span>Instructions</span>                                        </div>                                        <a href="#" title="Close" id="wms-srv-Instruction-popup-close" class="wms-srv-popup-close"><i class="fas fa-times-circle"></i></a>                                        </div>                                 <div class="wms-srv-grid-action-panel" style="border-bottom: solid 1px #cccccc !important">                                                                                 <!-- Popup Title -->                                                <div class="wms-srv-popup-content-body">                                                                                     <!-- Grid -->                                                <div class="wms-srv-grid-holder pnlWmsHead">                                                                                         <div class="wms-srv-grid" id="wms-srv-grid-DriverListPopup" style="text-align: center;">                                     <div class="wms-srv-grid-header" style="white-space: nowrap;">                                                        <div class="wms-srv-grid-cell" style="text-align:center;width:50px;">Sr. No.</div>                                                        <div class="wms-srv-grid-cell" style="text-align:left;width:200px;">Instructions</div>                                                                                                                                                       </div>                                                    <div class="wms-srv-grid-row">                                                        <div class="wms-srv-grid-cell">1</div>                                                        <div class="wms-srv-grid-cell" style="text-align:left;">Use New Template every time.</div>                                                                                                          </div>                                                 <div class="wms-srv-grid-row">                                                        <div class="wms-srv-grid-cell">2</div>                                                        <div class="wms-srv-grid-cell" style="text-align:left;">Reference No. Is Mandatory & Unique for each Order.</div>                                                                                                         </div>                                                  <div class="wms-srv-grid-row">                                                        <div class="wms-srv-grid-cell">3</div>                                                        <div class="wms-srv-grid-cell" style="text-align:left;">If Error Occured to Particular Reference No. order row then again import Only that reference No. all rows.</div>                                                                                                         </div>                                                  <div class="wms-srv-grid-row">                                                        <div class="wms-srv-grid-cell">3</div>                                                        <div class="wms-srv-grid-cell" style="text-align:left;">UOM column value will be "Each" only.</div>                                                                                                         </div>                                                    </div>                                                                                                                                         <!-- Grid -->                                                                                                                                 </div>                                        </div>                                        </div>                                    </div>                                </div>                                </div>        <%--END Instruction Popup--%> 

    </div>
    <textarea id="txtCSVdata" style='width:1px;height:1px;position:absolute;overflow:hidden;'>
        </textarea>
        <div id="WMSDataCalendar" class="WMSDataCalendar"></div>
</div>

</asp:Content>
