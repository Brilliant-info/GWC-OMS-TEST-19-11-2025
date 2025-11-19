<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/CRM.Master" AutoEventWireup="true" CodeBehind="WMSImport.aspx.cs" Inherits="BrilliantWMS.WMSImport.WMSImport" Theme="Blue" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../WMSTheme/css/theme-wms-sky-blue.css" id="wmsThemeSet" rel="stylesheet">
    <link href="../WMSTheme/css/wms-style.css" rel="stylesheet">
    <link href="../WMSTheme/css/wms-dashboard.css" rel="stylesheet">
     <script src="https://code.jquery.com/jquery-3.2.1.min.js"></script>
    <link href="../WMSTheme/jquery-ui/jquery-ui.css" rel="stylesheet">
    <script src="../WMSTheme/jquery-ui/jquery-ui.js"></script>
    <link href="../WMSTheme/fontawesome/css/all.css" rel="stylesheet"> 
    <script src="../javascript/WMSImport.js" type="text/javascript"></script>
    <link href="../css/WMSImport.css" rel="stylesheet">
    <link rel="stylesheet" href="../WMSTheme/bootstrap-4.0.0/dist/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js" integrity="sha384-ApNbgh9B+Y1QKtv3Rn7W3mgPxhU9K/ScQsAP7hUibX39j7fakFPskvXusvfa0b4Q" crossorigin="anonymous"></script>
    <script src="../WMSTheme/bootstrap-4.0.0/dist/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>
    <style>
        /**, ::after, ::before {
            box-sizing: unset;
        }*/
            #top_bar{
                box-sizing: unset;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHolder_FormHeader" runat="server">
   <%--<div class="WMSTopImportStrip">
       <b>SKU Import</b>
   </div>--%>
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHolder_Form" runat="server">
    <asp:HiddenField ID="hdnCompanyId" ClientIDMode="Static" runat="server" Value="" />
    <asp:HiddenField ID="hdnImportObject" ClientIDMode="Static" runat="server" Value="" />
    <asp:HiddenField ID="hdnUser" ClientIDMode="Static" runat="server" Value="" />
     <asp:HiddenField ID="hdnImportSessionId" ClientIDMode="Static" runat="server" Value="" /> 
    <div class="WMSImportWizard">
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
        <div class="WMSImport_StepsPages" id="WmsPage_step_filetype" style="">
            <div class="WmsImport_Title">
                <div class="themeWMSAjaxTabPageTitle"><i class="fas fa-file"></i><span>Import Excel Wizard</span></div>
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
                                    <asp:DropDownList ID="ddlcompanymain" CssClass="inputElement" ClientIDMode="Static" runat="server" Width="187px" DataTextField="Name" DataValueField="ID" onchange="GetCustomer()">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ForeColor="Maroon" ControlToValidate="ddlcompanymain"
                                        InitialValue="0" ValidationGroup="Save" runat="server" ErrorMessage="Please Select Company" Display="None"></asp:RequiredFieldValidator>
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
                                    <req>Customer</req>
                                    :
			                                    <!-- Label -->
                                </div>
                                <div class="col-md-8">
                                    <!-- Control -->
                                    <asp:DropDownList ID="ddlcustomer" ClientIDMode="Static" CssClass="inputElement" runat="server" ValidationGroup="Save" Width="187px"
                                      >
                                    </asp:DropDownList>
                                     <%-- DataTextField="Name" DataValueField="ID" onchange="Getdeptid()--%>
                                    <asp:RequiredFieldValidator ID="RFVddlCompany" runat="server" ErrorMessage="Please Select Customer"
                                        ControlToValidate="ddlcustomer" InitialValue="0" Display="None" ValidationGroup="Save"></asp:RequiredFieldValidator>
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
                                    <a href="#" class="wmsWizNavNext wmsWizNavActive"  onclick="downloadTemplate('excel');return false;" ><i class="fas fa-download"></i> Download Template</a>
                              </div>
                        </div>
                        <div class="col-md-4">
                            <%--<!-- Export to SVC -->
                            <div class="WmsImport_Type" data-type="CSV" style=" cursor:pointer;">
                                <div class="WmsImport_Icon">
                                    <i class="fas fa-file-csv"></i>
                                </div>
                                <div class="WmsImport_Description">
                                    CSV
                                </div>                                
                            </div>
                            <!-- Export to Excel -->
                            <div class="WmsImport_DownloadTemplate">
                                    <a href="#" onclick="downloadTemplate('csv');return false;" class="wmsWizNavNext wmsWizNavActive"><i class="fas fa-download"></i> Download Template</a>
                            </div>--%>
                        </div>
                        <div class="col-md-4">
                           <%-- <!-- Export to text -->
                            <div class="WmsImport_Type" data-type="Text" style="opacity:1;cursor:pointer;">
                                <div class="WmsImport_Icon">
                                    <i class="fas fa-file-alt"></i>
                                </div>
                                <div class="WmsImport_Description">
                                    Text
                                </div>
                            </div>
                            <!-- Export to Excel -->
                            <div class="WmsImport_DownloadTemplate" style="opacity:1; ">
                                    <a href="#" class="wmsWizNavNext wmsWizNavActive"  onclick="downloadTemplate('txt');return false;" ><i class="fas fa-download"></i> Download Template</a>
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
                <div class="themeWMSAjaxTabPageTitle"><i class="fas fa-columns"></i><span>Select file / Map Columns</span></div>
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
            <div class="WMSDefaultFileCtrl">Or use default file uploader <asp:FileUpload ID="txtFile" runat="server" ClientIDMode="Static" onchange="setFile(this);" /></div>
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
        <!-- Page -->
        <!-- Page -->
        <div class="WMSImport_StepsPages" id="WmsPage_step_validatefile">
            <div class="WmsImport_Title" id="WmsImport_Title_Validate">
                <div class="themeWMSAjaxTabPageTitle"><i class="fas fa-clipboard-check"></i><span>Validate File</span></div>
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
                                        Your file contains 3 errors on first page. Kindly fix it before processing import.
                                    </div>
                                </div>
                                <div class="validationHolderCell" style="width: 100px;">
                                    <a href="#" id="lnkValidateCorrection" onclick="validateTableData();return false;" class="wmsWizNavNext wmsWizNavActive">
                                        <i class="fas fa-check-circle"></i>Validate
                                    </a>
                                    <a href="#" id="lnkUploadImportData" onclick="return false;" class="wmsWizNavNext wmsWizNavActive">
                                        <i class="fas fa-check-circle"></i>Upload
                                    </a>
                                </div>
                              
            <div class="validationHolderCell">
                <a href="#" id="lnkcancel" onclick="cancleImport();return false;" class="wmsWizNavNext wmsWizNavActive">
                                       <i class="far fa-times-circle"></i> cancel
                                    </a>
                            <%--    <div id="btnfinish" style="position:absolute; top:12px; float:right">--%>
                                    <a href="#" id="lnkfinish" onclick="FinishImport();return false;" class="wmsWizNavNext wmsWizNavActive" style="visibility:hidden">
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
                                <%--<option value="warningDataOnly">Show Warning Data Only</option>--%>
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
                <div class="themeWMSAjaxTabPageTitle"><i class="fas fa-file-upload"></i><span>Import Data</span></div>
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
    <div id="WMSDataCalendar" class="WMSDataCalendar"></div>
<asp:TextBox ID="txtCSVdata" runat="server" ClientIDMode="Static" TextMode="MultiLine" style="display:none;"></asp:TextBox>
<asp:TextBox ID="txtTextdata" runat="server" ClientIDMode="Static" TextMode="MultiLine" style="display:none;"></asp:TextBox>
<!-- POPUP for file options -->
<style>
    .WmsImportOptionPopup {
	    position: fixed;
	    top: 0px;
	    left: 0px;
	    width: 100%;
	    height: 100%;
	    z-index: 9;
    }
    #btnCloseImportPopup {
	    float: right;
	    margin-top: 3px;
	    cursor: pointer;
    }
    #btnCloseImportPopup i {
	    position: static;
	    font-size: 20px;
    }
    .WmsImportOptionPopup .themeWMSPopupLabel{
        white-space:nowrap;
        width:100%;
    }
</style>
    <script>
        $(document).ready(function () {
            $('#btnCloseImportPopup').click(function () {
                $('#WmsImportOptionPopup').hide();
            });
        });
    </script>
    <div class="WmsImportOptionPopup" id="WmsImportOptionPopup" style="display:none;">
<div class="themeWmsPopupContent" style="width:470px;">
	<div class="themeWmsPopupTitle">
		Import File <div class="themeWMS_ctrl_btn wms_show_ctrl" id="btnCloseImportPopup"><i class="fas fa-times-circle"></i></div>
	</div>
	<div class="themeWmsPopupContentBody">
       <%-- <span class="themeWMSPopupLabel"><input id="chkIsHeader" type="checkbox" name="chkIsHeader" checked="checked"> <b> My File Contains Header </b></span>--%>
		<span class="themeWMSPopupLabel" style="margin-top:10px;"><b>Contains Header?</b></span><br />
         <div>
            <%--<select name="txtIsHeader" id="txtIsHeader" class="inputElement notranslate" onchange="setIsContainsHeader(this);">
                <option value="yes">Contains Header</option>
                <option value="no">No Header</option>
            </select>--%>
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
        <span class="themeWMSPopupLabel" style="margin-top:10px;"><b>Click on desired position to create break line.</b></span><br />
        <div class="textPreview">
            <div class="textPreviewPointer">

            </div>
            <div class="textPreviewScale">

            </div>
            <div class="textPreviewContent" id="textPreviewContent">
            </div>
        </div>
        </div>
		<div class="themeWMS_ctrl_btn wms_show_ctrl" style="margin-top:10px;"><i class="fas fa-file-upload" style="top:14px;left:16px;"></i>
			<input type="button" value="Import File" class="buttonON" onclick="saveFileOption();" />
		</div>
	</div>
</div>
<div class="themeWmsPopupBg">
</div>
</div>
<!-- POPUP for file options -->
</asp:Content>
