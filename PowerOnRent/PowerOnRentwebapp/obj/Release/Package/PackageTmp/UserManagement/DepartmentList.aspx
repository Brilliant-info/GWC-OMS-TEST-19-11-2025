<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/CRM2.Master" AutoEventWireup="true" CodeBehind="DepartmentList.aspx.cs" Theme="Blue" Inherits="PowerOnRentwebapp.UserManagement.DepartmentList" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <table class="gridFrame" width="800px" style="margin: 3px 3px 3px 3px;">
        <tr>
            <td>
                <table style="width: 100%">
                    <tr>
                        <td style="text-align: left;">
                            <asp:Label ID="lblheader" CssClass="headerText" runat="server" Text="Department List"></asp:Label>
                        </td>
                        <td style="text-align: right;">
                            <table>
                                <tr>
                                    <td>
                                        <input type="text" id="txtProductSearch" onkeyup="SearchProduct();" style="font-size: 15px; padding: 2px; width: 450px;" />
                                        <asp:HiddenField runat="server" ID="hdnFilterText" />
                                    </td>
                                    <td>
                                        <img src="../App_Themes/Blue/img/Search24.png" onclick="SearchProduct()" />
                                    </td>
                                    <td style="text-align: right;">
                                        <%-- <input type="checkbox" id="chkWithBOM"  /> --%><%--onclick="SelectAllEngine(this);"--%>
                                        <%-- <asp:Label ID="lblwithbom" CssClass="headerText" runat="server" Text="With BOM"></asp:Label>    --%>                              
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="text-align: right;">
                            <input type="button" runat="server" value="Submit" id="btnSubmitProductSearch1" onclick="selectedRec();" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <obout:Grid ID="GridProductSearch" runat="server" AutoGenerateColumns="false" AllowFiltering="false" AllowMultiRecordSelection="false"
                    AllowGrouping="true" AllowColumnResizing="true" AllowAddingRecords="false" AllowColumnReordering="true"
                    CallbackMode="true" Width="100%" Serialize="false"
                    PageSize="25" AllowPageSizeSelection="false" AllowManualPaging="true" ShowTotalNumberOfPages="false"
                    KeepSelectedRecords="false">
                    <ClientSideEvents ExposeSender="true" />
                    <Columns>
                        <obout:Column DataField="ID" HeaderText="ID" Visible="true">
                        </obout:Column>
                        <obout:Column DataField="Company" HeaderText="Company" Align="left" HeaderAlign="left"
                            Width="10%" AllowFilter="false" ParseHTML="true">
                        </obout:Column>
                        <obout:Column DataField="Territory" HeaderText="Department" Align="left" HeaderAlign="left"
                            Width="18%" AllowFilter="false" ParseHTML="true">
                        </obout:Column>
                        <obout:Column DataField="StoreCode" HeaderText="Department Code" Align="left" HeaderAlign="left"
                            Width="10%" AllowFilter="false" ParseHTML="true">
                        </obout:Column>
                        <obout:Column DataField="OrderFormat" HeaderText="OrderFormat" Align="left" HeaderAlign="left"
                            Width="10%" AllowFilter="false" ParseHTML="true">
                        </obout:Column>
                        <obout:Column DataField="PriceChange" HeaderText="PriceChange" Align="left" HeaderAlign="left"
                            Width="10%" AllowFilter="false" ParseHTML="true">
                        </obout:Column>
                        <obout:Column DataField="AutoCancel" HeaderText="AutoCancel" Align="left" HeaderAlign="left"
                            Width="10%" AllowFilter="false" ParseHTML="true">
                        </obout:Column>
                        <obout:Column DataField="Active" HeaderText="Active" Align="left" HeaderAlign="left"
                            Width="10%" AllowFilter="false" ParseHTML="true">
                        </obout:Column>

                    </Columns>
                    <Templates>
                        <obout:GridTemplate runat="server" ID="TemplateFieldAmount">
                            <Template>
                                <span style="margin-right: 7px;">
                                    <%# Container.Value %></span>
                            </Template>
                        </obout:GridTemplate>
                    </Templates>
                </obout:Grid>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%">
                    <tr>
                        <td style="text-align: left;"></td>
                        <td style="text-align: right;">
                            <input type="button" runat="server" value="Submit" id="btnSubmitProductSearch2" onclick="selectedRec();" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    <asp:HiddenField ID="hndgrupByGrid" runat="server" />
    <asp:HiddenField ID="hdncompanyid" runat="server" />
    <asp:HiddenField ID="hdndeptid" runat="server" />
    <asp:HiddenField ID="hnduserID" runat="server" />

    <script type="text/javascript">
        window.onload = function () {
            oboutGrid.prototype.restorePreviousSelectedRecord = function () {
                return;
            }

            oboutGrid.prototype.markRecordAsSelectedOld = oboutGrid.prototype.markRecordAsSelected;
            oboutGrid.prototype.markRecordAsSelected = function (row, param2, param3, param4, param5) {
                if (row.className != this.CSSRecordSelected) {
                    this.markRecordAsSelectedOld(row, param2, param3, param4, param5);
                } else {
                    var index = this.getRecordSelectionIndex(row);
                    if (index != -1) {
                        this.markRecordAsUnSelected(row, index);
                    }
                }
            }
        }                                                                                            //for (var i = 0; i < GridProductSearch.SelectedRecords.length; i++)
        function selectedRec() {
            var UserID = document.getElementById("<%=hnduserID.ClientID%>").value;
            var departmentinfo = new Object();
            var Selectedrecord = "";
            var hdnProductSearchSelectedRec = window.opener.document.getElementById("hdnDepatmentName");
            var hdnproductsearchId = window.opener.document.getElementById("hdnDepatmentId");
            if (GridProductSearch.SelectedRecords.length > 0) {
                for (var i = 0; i < GridProductSearch.SelectedRecords.length; i++) {
                    var record = GridProductSearch.SelectedRecords[i];
                    if (Selectedrecord.value != "") Selectedrecord += ',' + record.ID;
                    if (Selectedrecord.value == "") {
                        //Selectedrecord.value = record.LocationCode;
                        Selectedrecord.value = record.ID;
                    }
                }

                var SelectedContactIds = "";
                departmentinfo.SelectedContactIds = Selectedrecord;

                PageMethods.PMSaveDepartment(Selectedrecord, UserID, Submitassigndept_onSuccess, Submitassigndept_onFail)

                // window.opener.AfterApproverSelect(hdnProductSearchSelectedRec.value, Selectedrecord);
                //self.close();
            }
            if (GridProductSearch.SelectedRecords.length == 0) {
                alert("Please Select Department");
            }
        }

        function Submitassigndept_onSuccess() {
            //LoadingOff();
            window.opener.Gvassigndept.refresh();
            self.close();
        }

        function Submitassigndept_onFail() { alert("error"); }

        var searchTimeout = null;
        function SearchProduct() {
            var hdnFilterText = document.getElementById("<%= hdnFilterText.ClientID %>");
            hdnFilterText.value = document.getElementById("txtProductSearch").value;
            if (searchTimeout != null) {
                window.clearTimeout(searchTimeout);
            }
            searchTimeout = window.setTimeout(performSearch, 700);
        }

        function performSearch() {
            GridProductSearch.refresh();
            searchTimeout = null;
            return false;
        }
    </script>
</asp:Content>
