<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DedicatedDriverList.aspx.cs" MasterPageFile="~/MasterPage/CRM2.Master" Inherits="PowerOnRentwebapp.PowerOnRent.DedicatedDriverList"
    EnableEventValidation="false" Theme="Blue" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="obout" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>
<%@ Register Src="../CommonControls/UCFormHeader.ascx" TagName="UCFormHeader" TagPrefix="uc1" %>
<%@ Register Src="../CommonControls/UCToolbar.ascx" TagName="UCToolbar" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>
        <asp:UpdatePanel ID="updPnl_Contact" runat="server">
            <ContentTemplate>


                <table class="gridFrame" width="90%">
                    <tr>
                        <td colspan="2">
                            <obout:Grid ID="gvContactPerson" runat="server" AutoGenerateColumns="False" AllowAddingRecords="False"
                                AllowFiltering="True" AllowGrouping="True" AllowMultiRecordSelection="False"
                                AllowRecordSelection="true" Width="100%" AllowColumnResizing="true" AllowColumnReordering="true">
                                <Columns>
                                    <obout:Column DataField="ID" Visible="false">
                                    </obout:Column>
                                    <obout:Column DataField="Name" HeaderText="Driver Name" Width="10%" Align="center" HeaderAlign="center" Wrap="true"></obout:Column>
                                    <obout:Column DataField="MobileNo" HeaderText="Contact No" Width="10%" Align="center" HeaderAlign="center" Wrap="true"></obout:Column>
                                    <obout:Column DataField="EmailID" HeaderText="Email Id" Width="10%" Align="center" HeaderAlign="center" Wrap="true"></obout:Column>

                                </Columns>

                            </obout:Grid>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <table style="width: 100%">
                                <tr>
                                    <td style="text-align: left;"></td>
                                    <td style="text-align: right;">
                                        <input type="button" runat="server" value="Allocate" id="btnAllocateDriver" onclick="selectedRec()" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <br />
                        </td>
                    </tr>
                </table>
                 <asp:HiddenField ID="hdndrid" runat="server" ClientIDMode="Static" />
                <asp:HiddenField ID="hdndrnm" runat="server" ClientIDMode="Static" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </center>
    
    <script type="text/javascript">

        //function selectedRec() {            

        //     var hdnSelectedRecID = window.opener.document.getElementById("hdnSelectedRecID");
        //    var hdnSelectedRecNM = window.opener.document.getElementById("hdnSelectedRecNM");
        //    hdnSelectedRecID.value = "";
        //    hdnSelectedRecNM.value = "";

        //    if (gvContactPerson.SelectedRecords.length > 0) {
        //        for (var i = 0; i < gvContactPerson.PageSelectedRecords.length; i++) {
        //            var record = gvContactPerson.PageSelectedRecords[i];
        //            //if (hdnSelectedRec.value != "") hdnSelectedRec.value += ',' + record.ID; hdnSelectedRecNM.value += ',' + record.Name;
        //            if (hdnSelectedRecID.value == "") hdnSelectedRecID.value = record.ID; hdnSelectedRecNM.value =  record.Name;
        //        }

        //         window.opener.AfterDrselected(hdnSelectedRecID.value, hdnSelectedRecNM.value);
        //         self.close();
        //    }
        //    else {
        //        showAlert('Select One Driver', 'Error', '#');
        //    }
        //}





        function selectedRec() {            

             var hdnSelectedRecID = window.opener.document.getElementById("hdnSelectedRecID");
            var hdnSelectedRecNM = window.opener.document.getElementById("hdnSelectedRecNM");
            hdnSelectedRecID.value = "";
            hdnSelectedRecNM.value = "";

            var hdndrid = document.getElementById("<%= hdndrid.ClientID %>");
               var hdndrnm = document.getElementById("<%= hdndrnm.ClientID %>");

            if (gvContactPerson.SelectedRecords.length > 0) {
                for (var i = 0; i < gvContactPerson.PageSelectedRecords.length; i++) {
                    var record = gvContactPerson.PageSelectedRecords[i];
                    //if (hdnSelectedRec.value != "") hdnSelectedRec.value += ',' + record.ID; hdnSelectedRecNM.value += ',' + record.Name;
                    if (hdnSelectedRecID.value == "") hdnSelectedRecID.value = record.ID; hdnSelectedRecNM.value =  record.Name;
                }


                hdndrid.value = hdnSelectedRecID.value;
                hdndrnm.value = hdnSelectedRecNM.value;
                  PageMethods.WMChkDrAssignActiveOrd(hdnSelectedRecID.value, OnSuccessDispatchedOrder, null);


                 
            }
            else {
                showAlert('Select One Driver', 'Error', '#');
            }
        }

        function OnSuccessDispatchedOrder(result) {
              var hdndrid = document.getElementById("<%= hdndrid.ClientID %>");
               var hdndrnm = document.getElementById("<%= hdndrnm.ClientID %>");

            if (result == "1") {
                window.opener.AfterDrselected(hdndrid.value, hdndrnm.value);
                 self.close();
            }
            else if (result == "2")
            {
                showAlert('Some error occurred', 'Error', '#');
            }
            else {
                  showAlert('Please select driver whose not allocated single order.', 'Error', '#');
            }

        }
    </script>

</asp:Content>
