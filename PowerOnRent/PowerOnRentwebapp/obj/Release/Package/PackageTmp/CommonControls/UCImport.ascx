<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCImport.ascx.cs" Inherits="BrilliantWMS.CommonControls.UCImport" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="obout" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>

<%--<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>--%>

 <link href="../App_Themes/wms-style.css" rel="stylesheet" id="wmsMainThemeCss" runat="server"/>
<link href="../App_Themes/wms-color-theme.css" rel="stylesheet" id="wmsThemeSet" runat="server"/>
<link href="../App_Themes/wms-dashboard.css" rel="stylesheet"/>


<div id="divimport" title="Import" class="themeWMSImportSection">
    <asp:UpdateProgress ID="UpdateGirdProductProcess" runat="server" AssociatedUpdatePanelID="Up_PnlGirdProduct" style="height: 500px; width: 100%; position: absolute;">
        <%--AssociatedUpdatePanelID="Up_PnlGirdProduct"--%>
        <ProgressTemplate>
            <center>
                                    <div class="modal noProgressBorder" style="height:500px;width:100%;display:block;margin-top:40px;">
                                        <img src="../App_Themes/Blue/img/test-mentor-loading.gif" style="top: 50%;" />      <%--ajax-loader.gif--%>
                                    </div>
                                </center>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:UpdatePanel ID="Up_PnlGirdProduct" runat="server">

        <ContentTemplate>
            <asp:HiddenField runat="server" ID="hdncustomerID" ClientIDMode="Static" />
            <asp:HiddenField runat="server" ID="hdnexportobject" ClientIDMode="Static" />
            <asp:HiddenField runat="server" ID="hdncheckvalid" ClientIDMode="Static" />
            <asp:Panel ID="pnlImport" runat="server" Visible="false">
                <!-- Arrow Progress Set -->
                <div class="themeWMSImportProgressHolder">
                    <!-- Upload File -->
                    <div class="themeWMSImportProgressDone themeWMSProgressUploadTab">
                        <div class="themeWMSImportArrowLeft">&nbsp;</div>
                        <div class="themeWMSImportProgressTitle">Upload File</div>
                        <div class="themeWMSImportArrowRight">&nbsp;</div>
                    </div>
                    <!-- Upload File -->
                    <!-- Data Verification -->
                    <div class="themeWMSImportProgressPending themeWMSProgressVerifyTab">
                        <div class="themeWMSImportArrowLeft">&nbsp;</div>
                        <div class="themeWMSImportProgressTitle">Data Verification</div>
                        <div class="themeWMSImportArrowRight">&nbsp;</div>
                    </div>
                    <!-- Data Verification -->
                    <!-- Data Verification -->
                    <div class="themeWMSImportProgressPending themeWMSProgressCompleteTab">
                        <div class="themeWMSImportArrowLeft">&nbsp;</div>
                        <div class="themeWMSImportProgressTitle">Finished</div>
                        <div class="themeWMSImportArrowRight">&nbsp;</div>
                    </div>
                    <!-- Data Verification -->
                </div>
                <!-- Arrow Progress Set -->
                <%-- <div id="divimport">
                    <h4>
                        <asp:Label ID="lblimportdata" runat="server" Text="Import Data"></asp:Label>                       
                    </h4>
                </div>--%>
                <asp:HiddenField ID="hdnobject" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdntablename" runat="server" ClientIDMode="Static" />
                <center>
        <table cellpadding="0" cellspacing="20px" border="0" width="95%">
         <%--   <tr>
                <td style="float:right">
                    <table align="right" border="0">                       
                      <div class="divViewScheduleHolder"><div class="divCircleCount"><div><asp:Label ID="lblschedulecount" runat="server" Text=""></asp:Label></div></div>
                       <input id="btnschedule"  type="button" value="View Schedule"  class="button" onclick="ShowSchedule()" />
                       </div>
                    </table>
                </td>
            </tr>--%>
            </table>
                     <div class="themeWMSImportTitle">
                    Ready to Import?
                </div>
                <div class="themeWMSImportSubTitle" >
                    <asp:Label ID="lbltext1" runat="server" Text="Import will faciliated Order Data to be directly imported into WMS."></asp:Label><br />
                    For Downloading Order Import Data Template 
                     <span class="themeWMSImportHighlight"><a href="~/ASNImportTemplate.xls" id="downloadlink" runat="server" target="_blank"  class="themeWMSImportHighlight">Click Here.</a></span></div>
                      
                    <br /><br />
                    <div style="text-align:center;">
                        <div style="display:inline-block !important;">
                        <asp:Label ID="lblcustomer" runat="server" Text="Select Customer :"></asp:Label> <asp:DropDownList ID="ddlCustomer" DataValueField="ID" DataTextField="Name" runat="server" Width="150px" onchange="GetCustomer();"></asp:DropDownList>
                            <asp:HiddenField ID="hdnSelectedCustomer" runat="server" ClientIDMode="Static" />
                            <%----%>
                            </div>
                        <asp:Label ID="lblSelecFile" runat="server" Text="Select Import File"></asp:Label> : <asp:FileUpload ID="FileuploadPO" runat="server" />
                         <div class="themeWMS_ctrl_btn" style="display:inline-block !important;">
                                            <i class="fas fa-upload"></i>
                                                <asp:Button ID="btnFileUpload" Text="Upload" runat="server" OnClientClick="return ProgressBar()" OnClick="btnUploadPo_Click"  CommandName="ClientSideButton" />
                                            </div>
                                        <%-- <asp:ImageButton ID="btnupload" runat="Server" ImageUrl="../App_Themes/Blue/img/Upload2.jpg" Width="88px" Height="39px" OnClientClick="return ProgressBar()" OnClick="btnUploadPo_Click"  CommandName="ClientSideButton" />--%>
                                         <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="FileuploadPO" ForeColor="Red"
                                          ErrorMessage="Upload only excel file" ValidationExpression="^.+(.xls|.XLS|.xlsx|.XLSX)$">
                                          </asp:RegularExpressionValidator>
                        </div>
                    <br />  <br />
                     <div id="divUpload" style="display: none">
                       <%-- <b><i>Uploading file...</i></b><br />--%>
                            <span style="text-align:center; float:inherit"><asp:Label ID="lbluploading" runat="server" Font-Bold="true" Font-Italic="true" Text="Uploading file..."></asp:Label></span><br />
                            <asp:Image ID="uploadimg" runat="server" ImageUrl="~/images/upload-animation.gif" Width="380" Height="20" />
                             <asp:Panel ID="uploadMessage" runat="server" Font-Bold="true" style="text-align:center;">File uploaded successfully! Click On Next Button </asp:Panel>
                             
                            </div>
                    <asp:Label ID="lblmessagesuccess" runat="server" style="text-align:center" Font-Size="16px" ForeColor="#206295" Font-Bold="true"  Text="File uploaded successfully! Click On Next Button"></asp:Label>
                     <table align="right">
                        <tr>
                            <td>
                                <div class="themeWMS_ctrl_btn themeWMS_ctrl_btn_reverse" style="display:inline-block !important;">
                                <i class="fas fa-arrow-right"></i>
                                    <asp:Button ID="btnImportNext" runat="Server" CssClass="inputElement" style="padding-left:10px;padding-right:10px;padding-top:5px;padding-bottom:5px; margin-right:5px;" Text="Next" OnClick="btnimportNext_Click" CausesValidation="false" CommandName="ClientSideButton" />
                                </div>
                                <%--<asp:ImageButton ID="btnimpnext" runat="Server" ImageUrl="../App_Themes/Blue/img/next.jpg" Width="100px" Height="53px" OnClick="btnimportNext_Click" CausesValidation="false" CommandName="ClientSideButton"/>--%>
                                <%--<asp:Button ID="btnimportNext" runat="server" Text="  Next  " OnClick="btnimportNext_Click" CausesValidation="false"   />--%>     <%-- OnClientClick="return CheckDeptvalidations();"--%>
                            </td>
                            <td>
                                 <div class="themeWMS_ctrl_btn themeWMS_ctrl_btn_reverse" style="display:inline-block !important;">
                                <i class="fas fa-arrow-left"></i>
                                     <asp:Button ID="btnImportCancel" runat="server" CssClass="inputElement" style="padding-left:10px;padding-right:10px;padding-top:5px;padding-bottom:5px;" Text="Back" OnClick="btnimportcancel_Click" CausesValidation="false"  CommandName="ClientSideButton"  />
                                     </div>
                                <%-- <asp:ImageButton ID="ImageButton2" runat="Server" ImageUrl="../App_Themes/Blue/img/back.jpg" Width="100px" Height="51px" OnClick="btnimportcancel_Click" CausesValidation="false"  CommandName="ClientSideButton" />--%>
                                <%--<asp:Button ID="btnimportcancel" runat="server" Text="Cancel" OnClick="btnimportcancel_Click" CausesValidation="false"  />  --%>
                            </td>
                        </tr>
                    </table>
                    <br /><br />
    </center>
            </asp:Panel>
            <asp:Panel ID="pnlvalidate" runat="server" Visible="false">
                <!-- Arrow Progress Set -->
                <div class="themeWMSImportProgressHolder">
                    <!-- Upload File -->
                    <div class="themeWMSImportProgressDone themeWMSProgressUploadTab">
                        <div class="themeWMSImportArrowLeft">&nbsp;</div>
                        <div class="themeWMSImportProgressTitle">Upload File</div>
                        <div class="themeWMSImportArrowRight">&nbsp;</div>
                    </div>
                    <!-- Upload File -->
                    <!-- Data Verification -->
                    <div class="themeWMSImportProgressDone themeWMSProgressVerifyTab">
                        <div class="themeWMSImportArrowLeft">&nbsp;</div>
                        <div class="themeWMSImportProgressTitle">Data Verification</div>
                        <div class="themeWMSImportArrowRight">&nbsp;</div>
                    </div>
                    <!-- Data Verification -->
                    <!-- Data Verification -->
                    <div class="themeWMSImportProgressPending themeWMSProgressCompleteTab">
                        <div class="themeWMSImportArrowLeft">&nbsp;</div>
                        <div class="themeWMSImportProgressTitle">Finished</div>
                        <div class="themeWMSImportArrowRight">&nbsp;</div>
                    </div>
                    <!-- Data Verification -->
                </div>
                <!-- Arrow Progress Set -->
                <div class="themeWMSImportTitle">
                    Data verification 
                </div>
                <div class="themeWMSImportSubTitle">
                    <asp:Label ID="lblbackMessage" runat="server" ForeColor="Red" Text=""></asp:Label>
                    <asp:Label ID="lblOkMessage" runat="server" Text=""></asp:Label>
                    <asp:Label ID="lblscheduleQue" runat="server" Text=""></asp:Label>
                    <asp:Label ID="lblserialnotmatching" runat="server" ForeColor="Red" Text=""></asp:Label> 
                    <asp:linkbutton id="lnkbtnserial" onclientclick="OpenSerialPop()"  OnClick="LinkButton_Click"  runat="server">Click To View</asp:linkbutton>  <%----%>
                   
                    <asp:HiddenField ID="hdnnotavailserial" ClientIDMode="Static" runat="server" />
                </div>
                <div style="text-align: right;">
                    <div class="themeWMS_ctrl_btn themeWMS_ctrl_btn_reverse" style="display: inline-block;">
                        <i class="fas fa-arrow-right"></i>
                       <asp:Button ID="btnvalidate" runat="server" Text="Validate Serial" style="width:110px; padding-left:10px;padding-right:10px;padding-top:5px;padding-bottom:5px; margin-right:5px !important; display:inline-block;" font-size="14px" OnClick="Clickbtvalidate_Click"/>
                        <%-- <input id="btnvalidateinp" type="button" value="Validate Serial" style="width:85px;" font-size="14px" onclick="validateSerial()" />--%>
                    </div>  

                    <div class="themeWMS_ctrl_btn themeWMS_ctrl_btn_reverse" style="display: inline-block;">
                        <i class="fas fa-arrow-right"></i>
                        <asp:Button ID="btnnext" runat="server" Text="Next" style="width:85px; padding-left:10px;padding-right:10px;padding-top:5px;padding-bottom:5px; margin-right:5px !important;  display:inline-block;" font-size="14px" OnClick="btnnext_Click" />
                    </div>


                    <div class="themeWMS_ctrl_btn themeWMS_ctrl_btn_reverse" style="display: inline-block; float: right;">
                        <i class="fas fa-arrow-left"></i>
                        <asp:Button ID="btnback" runat="server"  Text="Back" style="width:85px; padding-left:10px;padding-right:10px;padding-top:5px;padding-bottom:5px; margin-right:5px !important;  display:inline-block;" font-size="14px" OnClick="btnback_Click" />
                    </div>

                    <div class="themeWMS_ctrl_btn themeWMS_ctrl_btn_reverse" style="display: inline-block; float: right;">
                        <i class="fas fa-file-export"></i>
                        <input id="btnexporthtml" type="button" runat="server" visible="false"  value="Export" style="display:none; width:85px; padding-left:10px;padding-right:10px;padding-top:5px;padding-bottom:5px; margin-right:5px !important;  display:inline-block;" font-size="14px" onclick="OpenExport()" />
                    </div>
                </div>

                <%-- <div id="divRequestHead">
                    <h4>
                        <asp:Label ID="lblHeading" runat="server" Text="Validate Data"></asp:Label>
                    </h4>
                </div>--%>
                <div id="div1" class="themeWMSImportGridHolder" style="overflow-y: auto;">
                    <table class="gridFrame" style="width: calc(100% - 50px);">
                        <tr class="themeWMSGridHeaderRow" style="height: 40px">
                            <td style="text-align: left;">
                                <asp:Label class="headerText" ID="lbladdresslist" runat="server" Text="Import List"></asp:Label>
                            </td>
                        
                        </tr>
                        <tr>
                            <td>
                               <%-- <div class="wmsThemeMobileGridMain">
                                    <div class="wmsThemeMobileGridHolder">
                                        <div class="wmsThemeMobileGrid">--%>
                                            <%-- OnRowDataBound="GVImportView_OnRowDataBound"--%>
                                            <obout:Grid ID="GVImportView" runat="server" AllowGrouping="false" ShowFooter="false" Serialize="false" CallbackMode="true" AllowRecordSelection="true" AllowPaging="false"
                                                AllowColumnReordering="true" AllowMultiRecordSelection="false" AllowColumnResizing="true" AllowFiltering="false" Width="100%" OnRowDataBound="GVImportView_OnRowDataBound"
                                                AllowAddingRecords="false">
                                                <%--   <ScrollingSettings  ScrollHeight="130" />--%>
                                                <%--<ScrollingSettings ScrollWidth="1310" />   --%>
                                            </obout:Grid>
                                       <%-- </div>
                                    </div>
                                </div>--%>
                            </td>                           
                        </tr>
                    </table>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlfinish" runat="server" Visible="false">
                <!-- Arrow Progress Set -->
                <div class="themeWMSImportProgressHolder">
                    <!-- Upload File -->
                    <div class="themeWMSImportProgressDone themeWMSProgressUploadTab">
                        <div class="themeWMSImportArrowLeft">&nbsp;</div>
                        <div class="themeWMSImportProgressTitle">Upload File</div>
                        <div class="themeWMSImportArrowRight">&nbsp;</div>
                    </div>
                    <!-- Upload File -->
                    <!-- Data Verification -->
                    <div class="themeWMSImportProgressDone themeWMSProgressVerifyTab">
                        <div class="themeWMSImportArrowLeft">&nbsp;</div>
                        <div class="themeWMSImportProgressTitle">Data Verification</div>
                        <div class="themeWMSImportArrowRight">&nbsp;</div>
                    </div>
                    <!-- Data Verification -->
                    <!-- Data Verification -->
                    <div class="themeWMSImportProgressDone themeWMSProgressCompleteTab">
                        <div class="themeWMSImportArrowLeft">&nbsp;</div>
                        <div class="themeWMSImportProgressTitle">Finished</div>
                        <div class="themeWMSImportArrowRight">&nbsp;</div>
                    </div>
                    <!-- Data Verification -->
                </div>
                <!-- Arrow Progress Set -->
                <div class="themeWMSImportTitle">
                    Finished… You have completed all steps…
                </div>
                <div class="themeWMSImportSubTitle">
                    <asp:Label ID="lblImportSuccess" runat="server" Text=""></asp:Label>
                    <asp:Label ID="lblQueSchedule" runat="server" Text=""></asp:Label>
                    <%--We have successfully imported <span class="themeWMSImportHighlight">2800 records.</span>--%>
                </div>
               <%-- <div class="themeWMSImportCount">
                    <i class="fas fa-flag-checkered"></i>
                    <div class="themeWMSImportCountSubLabel">Done!!!</div>
                </div>--%>

                <div style="text-align:center;padding-top:40px;">
                <div class="themeWMS_ctrl_btn">
                    <i class="fas fa-flag-checkered"></i>
                    <asp:Button ID="btnfinish" runat="server" Text="Finish" Style="width: 90px; padding-left:10px;padding-right:10px;padding-top:5px;padding-bottom:5px;" OnClick="btnfinish_Click" />
                </div>
                </div>


            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <%--<asp:PostBackTrigger ControlID="btnupload" />--%>
            <asp:PostBackTrigger ControlID="btnFileUpload" />

        </Triggers>
    </asp:UpdatePanel>
</div>
<style type="text/css">
    /*POR Collapsable Div*/
    .class1 {
        opacity: 0.4;
        filter: alpha(opacity=40);
        cursor: wait;
    }

    .class2 {
        opacity: 1;
        filter: alpha(opacity=100);
        cursor: pointer;
    }

    .PanelCaption {
        color: Black;
        font-size: 13px; /*font-weight: bold;*/
        margin-top: -22px;
        margin-left: -5px;
        position: absolute;
        background-color: White;
        padding: 0px 2px 0px 2px;
    }

    .divHead {
        border: solid 2px #F5DEB3;
        width: 99%;
        text-align: left;
    }

        .divHead h4 {
            /*color: #33CCFF;*/
            color: #483D8B; /*margin: 3px 3px 3px 3px;*/
        }

        .divHead a {
            float: left;
            margin-top: -15px;
            margin-right: 5px;
        }

            .divHead a:hover {
                cursor: pointer;
                color: Red;
            }

    .divDetailExpand {
        border: solid 2px #F5DEB3;
        border-top: none;
        width: 99%;
        padding: 5px 0 5px 0;
    }

    .divDetailCollapse {
        display: none;
    }
    /*End POR Collapsable Div*/
</style>
<style type="text/css">
    .cartStepTitle {
        font-size: 20px;
        color: #cccccc;
        padding: 0px 50px 20px 0px;
        display: inline-block;
        padding-right: 60px;
        font-weight: bold;
    }

    .divCartSymbol, .divCartCurrentSymbol {
        position: relative;
        top: 30px;
        left: -10px;
        display: inline-block;
        background-image: url(../images/opt-bg-normal1.png);
        background-repeat: no-repeat;
        background-position: center center;
        font-size: 30px;
        font-family: Trebuchet MS, Arial;
        color: #ffffff;
        padding: 20px;
        text-decoration: none;
        overflow: hidden;
        opacity: 0.7;
    }

    .tdCartStepHolder {
        padding-left: 10px;
    }

    .cartStepHolder {
        position: relative;
        top: -10px;
    }

    .divCartCurrentSymbol {
        background-image: url(../images/opt-bg-selected2.png);
        opacity: 1;
    }

    .cartStepCurrentTitle {
        color: #000000;
    }

    .style2 {
        height: 47px;
    }

    .btnCommonStyle {
        font-family: inherit;
        font-weight: bold;
        font-size: 20px;
        color: #ffffff;
        text-decoration: none !important;
        padding-left: 50px;
        padding-right: 50px;
        padding-top: 14px;
        padding-bottom: 14px;
        border-radius: 7px; /* fallback */
        background-color: #1a82f7;
        background: url(../images/btn-common-bg.jpg);
        background-repeat: repeat-x; /* Safari 4-5, Chrome 1-9 */
        background: -webkit-gradient(linear, 0% 0%, 0% 100%, from(#3FC3C3), to(#339E9E)); /* Safari 5.1, Chrome 10+ */
        background: -webkit-linear-gradient(top, #3FC3C3, #339E9E); /* Firefox 3.6+ */
        background: -moz-linear-gradient(top, #3FC3C3, #339E9E); /* IE 10 */
        background: -ms-linear-gradient(top, #3FC3C3, #339E9E); /* Opera 11.10+ */
        background: -o-linear-gradient(top, #3FC3C3, #339E9E);
    }

    .tblcls {
        border: solid 2px #F5DEB3;
    }
</style>
<style type="text/css">
    .button {
        background: url(../App_Themes/Blue/img/Scheduleicon.png) no-repeat !important;
        background-position: 5px center !important;
        cursor: pointer !important;
        background-color: #1A527D !important;
        border: #1A527D 1px solid !important;
        border-top-left-radius: 6px !important;
        border-top-right-radius: 6px !important;
        border-bottom-left-radius: 6px !important;
        border-bottom-right-radius: 6px !important;
        box-shadow: inset 0px 1px #81B2D7 !important;
        padding: 5px 10px 5px 35px !important;
        margin: 10px !important;
        font-weight: bold !important;
    }
</style>
<style>
    .divViewScheduleHolder {
        position: relative;
    }

        .divViewScheduleHolder .divCircleCount {
            position: absolute;
            background-color: #ff0000;
            border: solid 2px #ffffff;
            border-radius: 50%;
            width: 30px;
            height: 30px;
            overflow: hidden;
            right: -20px;
            top: -20px;
            box-shadow: 0px 0px 5px #636363;
        }

    .divCircleCount div {
        text-align: center;
    }

        .divCircleCount div span {
            color: #ffffff;
            padding-top: 6px;
            display: block;
            font-weight: bold;
            font-size: 14px;
        }
</style>


<%--***** Validation for serial No.**********--%>
<script type="text/javascript">

    function OpenSerial() {
        var Serialstring = document.getElementById("<%=hdnnotavailserial.ClientID%>").value;
        document.getElementById("btnopenserial").Visible = true;
         window.open('../CommonControls/NASerial.aspx?NAserial=' + Serialstring + "", null, 'height=250px, width=800px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=300; screenY=60');
    }

    function OpenSerialPop() {
        var Serialstring = document.getElementById("<%=hdnnotavailserial.ClientID%>").value;
         window.open('../CommonControls/NASerial.aspx?NAserial=' + Serialstring + "", null, 'height=250px, width=800px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=300; screenY=60');
    }

    

</script>


<%--********* Show schedule Report ******--%>
<script type="text/javascript">
    function ShowSchedule() {
        var Objectname = document.getElementById("<%=hdnobject.ClientID%>").value;
        window.open('../CommonControls/ImportSchedule.aspx?Objectname=' + Objectname + "", null, 'height=350px, width=1020px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=300; screenY=60');
    }

    function OpenExport() {
        var valid = document.getElementById("<%=hdncheckvalid.ClientID%>").value;
        if (valid == "") {
            var CustomerID = document.getElementById("<%=hdncustomerID.ClientID%>").value;
              var Objectname = document.getElementById("<%=hdnexportobject.ClientID%>").value;
            window.open('../CommonControls/popup.aspx?Objectname=' + Objectname + '&CustomerID=' + CustomerID + "", null, 'height=350px, width=1020px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=300; screenY=60');
            PageMethods.PMMoveAddressToArchive(Ids, IsArchive, null, null); 
        }
    }

    function GetCustomer() {
        document.getElementById("<%=hdncustomerID.ClientID%>").value = document.getElementById("<%=ddlCustomer.ClientID%>").value;
        alert(document.getElementById("<%=hdncustomerID.ClientID%>").value);
    }
</script>

<%--******Progress bar script*******--%>
<script type="text/javascript">
    function ProgressBar() {
        if (document.getElementById('<%=FileuploadPO.ClientID %>').value != "") {
                document.getElementById("divUpload").style.display = "block";
                var getFileUploadMessageObj = document.getElementById('<%=uploadMessage.ClientID %>');
            if (getFileUploadMessageObj != null) {
                getFileUploadMessageObj.style.display = 'none';
            }

            return true;
        }
        else {
            alert("Select a file to upload");
            return false;
        }

    }
</script>
