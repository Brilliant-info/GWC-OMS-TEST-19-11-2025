<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddNote.aspx.cs" Inherits="PowerOnRentwebapp.PowerOnRent.AddNote" Theme="Blue" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>
<script src="../MasterPage/JavaScripts/countries.js" type="text/javascript"></script>
<script src="../MasterPage/JavaScripts/crm.js" type="text/javascript"></script>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>Brilliant WMS</title>
    <script src="../MasterPage/JavaScripts/countries.js" type="text/javascript"></script>
    <script src="../MasterPage/JavaScripts/crm.js" type="text/javascript"></script>
    
    <link href="../css/ContactPersonInfo.css" rel="stylesheet" />
    <!-- WMS THEME -->
    <link href="../WMSTheme/fontawesome/css/all.css" rel="stylesheet">
    <link href="../WMSTheme/css/theme-wms-sky-blue.css" id="wmsThemeSet" runat="server" rel="stylesheet">
    <link href="../WMSTheme/css/wms-style.css" rel="stylesheet">
    <link href="../WMSTheme/css/wms-dashboard.css" rel="stylesheet">
    <link rel="stylesheet" href="../WMSTheme/bootstrap-4.0.0/dist/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous">
    <script src="https://code.jquery.com/jquery-3.2.1.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js" integrity="sha384-ApNbgh9B+Y1QKtv3Rn7W3mgPxhU9K/ScQsAP7hUibX39j7fakFPskvXusvfa0b4Q" crossorigin="anonymous"></script>
    <script src="../WMSTheme/bootstrap-4.0.0/dist/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>
    <script src="../WMSTheme/js/wms-theme.js"></script>
    <!-- WMS THEME -->
   
</head>
<body>
    <div class="themeWMSContentHolder" style="display: block;">
    <form id="ContactPerson_Form" runat="server">
        <asp:HiddenField ID="hdnLanguage" ClientIDMode="Static" Value="" runat="server" />
        <div>
            <asp:HiddenField ID="hdnstate" runat="server" />
            <asp:ScriptManager ID="ScriptmanagerContactPerson" runat="server" EnablePageMethods="true">
            </asp:ScriptManager>
            <center>
                <div id="divLoading" style="height: 275px; width: 920px; display: none" class="modal">
                    <center>
                        <br />
                        <span id="spanLoading" style="font-size: 17px; font-weight: bold; color: lightskyblue;">Processing
                        please wait...</span>
                    </center>
                </div>
                <asp:ValidationSummary ID="validationsummary_UcNoteInfo" runat="server"
                    ShowMessageBox="true" ShowSummary="false" DisplayMode="bulletlist" ValidationGroup="SubmitContactPerson" />
                 <table class="gridFrame" style="margin: 3px 3px 3px 3px; width: 100%;">
                    <tr class="themeWMSGridHeaderRow" style="height: 40px;">
                        <td style="text-align: left;">
                            <asp:Label class="headerText" ID="lblNoteFormHeader" runat="server" Text="Add Note"></asp:Label>
                        </td>
                        <td style="width: 20%">
                            <table style="float: right;">
                                <tr>
                                    <td>
                                         <div class="themeWMS_ctrl_btn" style="display: inline-block">
                                             <i class="fas fa-save"></i>
                                        <input type="button" id="btnNoteSubmit" runat="server" value="Submit"  onclick="SubmitNote()"
                                            style="width: 70px;" />
                                             </div>
                                    </td>
                                    <td>
                                        <div class="themeWMS_ctrl_btn" style="display: inline-block">
                                             <i class="fas fa-eraser"></i>
                                        <input type="button" id="btnNoteClear" runat="server" value="Clear" onclick="ClearNote()"
                                            style="width: 70px;" />
                                            </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="background-color: White; padding-top: 20px; padding-bottom: 20px" colspan="2">

                              <!-- Bootstrap Code Starts Here -->
                    <div class="container-fluid">
                        <!-- Row Start -->
                      <div class="row">
                      
                        <div class="col-lg-4">
                            <!-- Cell Content -->
                                <div class="container-fluid">
	                                <div class="row">
		                                <div class="col-md-4">
			                                <!-- Label -->
                                               <asp:Label ID="lblNote" runat="server" Text="Remark / Note :"></asp:Label> :
			                                <!-- Label -->
		                                </div>
		                                <div class="col-md-8">
			                                <!-- Control -->
                                             <asp:TextBox ID="txtNote" runat="server" TextMode="MultiLine"></asp:TextBox>
                                             <%-- <asp:TextBox ID="txtNote" runat="server"  MaxLength="15" Width="194px" Style="text-align: right;"></asp:TextBox>--%>
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
                           
                        </td>
                    </tr>
                    <tr class="themeWMSGridHeaderRow" style="height: 40px;">
                        <td style="text-align: left;"></td>
                            <td style="text-align: right;">
                                         <div class="themeWMS_ctrl_btn" style="display: inline-block">
                                             <i class="fas fa-save"></i>
                                        <input type="button" id="Button1"  runat="server" value="Submit" onclick="SubmitNote();"
                                            style="width: 70px;" />
                                             </div>
                    <div class="themeWMS_ctrl_btn" style="display: inline-block">
                                        <i class="fas fa-eraser"></i>
                                        <input type="button" id="Button2" runat="server" value="Clear"  onclick="ClearNote()"
                                            style="width: 70px;" />
                                             </div>
                                    </td>
                                </tr>
                            
                 
                </table>
            </center>
        </div>
        <script type="text/javascript">
           
            <%--function SubmitNote()
            {
                if (document.getElementById("<%=txtNote.ClientID%>").value == "")
                {
                    showAlert("Please Enter Note!", "error", "#");
                    document.getElementById("<%=txtNote.ClientID %>").focus();
                     return false;
                }
                //LoadingOn();
                var NoteInfo = new Object();
                NoteInfo.note = document.getElementById("txtNote").value;
                PageMethods.SaveReTrigger(NoteInfo, SubmitNote_onSuccess, SubmitNote_onFail)

                function SubmitNote_onFail()
                { alert("error"); }
                function SubmitNote_onSuccess(result) 
                {
                   // window.opener.gridrefresh();
                    

                    if (result == "Success")
                    {
                    window.opener.gridrefresh();
                    }
                        self.close();
                }
               
            }--%>

            //Change by ajit started

            function SubmitNote()
            {
                document.getElementById('btnNoteSubmit').disabled = true;
                document.getElementById('Button1').disabled = true;

                if (document.getElementById("<%=txtNote.ClientID%>").value == "")
                {
                    showAlert("Please Enter Note!", "error", "#");
                    document.getElementById('btnNoteSubmit').disabled = false;
                    document.getElementById('Button1').disabled = false;
                    document.getElementById("<%=txtNote.ClientID %>").focus();
                     return false;
                }
                //LoadingOn();
                var NoteInfo = new Object();
                NoteInfo.note = document.getElementById("txtNote").value;
                PageMethods.SaveReTrigger(NoteInfo, SubmitNote_onSuccess, SubmitNote_onFail)

                function SubmitNote_onFail()
                {
                    alert("error");
                    document.getElementById('btnNoteSubmit').disabled = false;
                    document.getElementById('Button1').disabled = false;
                }
                function SubmitNote_onSuccess(result) 
                {
                   // window.opener.gridrefresh();
                    

                    if (result == "Success")
                    {
                    window.opener.gridrefresh();
                    }
                        self.close();
                }
               
            }

            //Change by ajit End

            function ClearNote()
            {
                document.getElementById("txtNote").value = '';
            }
        </script>
    </form>
        </div>
</body>
</html>

