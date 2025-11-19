<%@ Page Language="C#" MasterPageFile="~/MasterPage/CRM2.Master" AutoEventWireup="true" Theme="Blue" CodeBehind="ProductSearchWithSerial.aspx.cs" Inherits="PowerOnRentwebapp.Product.ProductSearchWithSerial" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- <table class="gridFrame" width="800px" style="margin: 3px 3px 3px 3px;">--%>
    <table class="gridFrame" width="100%">
        <tr>
            <td>
                <table style="width: 100%">
                    <tr>
                        <td style="text-align: left;">
                            <asp:Label ID="lblheader" CssClass="headerText" runat="server" Text="SKU Serial List"></asp:Label>
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
                            <input type="button" runat="server" value="Submit" id="btnSubmitProductSearch1" onclick="selectedRecGrid();" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <obout:Grid ID="GridProductSearch" runat="server" AutoGenerateColumns="false" AllowFiltering="false"
                    AllowGrouping="true" AllowColumnResizing="true" AllowAddingRecords="false" AllowColumnReordering="true"
                    AllowMultiRecordSelection="false" CallbackMode="true" Width="100%" Serialize="true"
                    PageSize="25" AllowPageSizeSelection="false" AllowManualPaging="true" ShowTotalNumberOfPages="false"
                    KeepSelectedRecords="false">
                    <ClientSideEvents ExposeSender="true" />
                    <Columns>
                        <%--<obout:Column DataField="ID" Visible="false">
                        </obout:Column>--%>

                        <obout:Column DataField="value" HeaderText="Serial Numer" Align="left" HeaderAlign="left"
                            Width="20%" AllowFilter="false" ParseHTML="true">
                        </obout:Column>
                        <%--<obout:Column DataField="availableQty" HeaderText="Quantity" Align="left" HeaderAlign="left"
                            Width="20%" AllowFilter="false" ParseHTML="true">
                        </obout:Column>--%>

                    </Columns>


                </obout:Grid>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%">
                    <tr>
                        <td style="text-align: left;"></td>
                        <td style="text-align: right;">
                            <input type="button" runat="server" value="Submit" id="btnSubmitProductSearch2" onclick="selectedRecGrid();" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>



    <asp:HiddenField ID="hdnskuqty" runat="server" />
    <asp:HiddenField ID="hdnserialid" runat="server" />

    <asp:HiddenField ID="hndgrupByGrid" runat="server" />
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
        }
        function selectedRec() {
            var hdnskuqty = document.getElementById("<%= hdnskuqty.ClientID %>");
            var hdnserialid = document.getElementById("<%= hdnserialid.ClientID %>");
            hdnserialid.value = "";
            if (GridProductSearch.SelectedRecords.length > 0) {
                for (var i = 0; i < GridProductSearch.SelectedRecords.length; i++) {
                    var record = GridProductSearch.SelectedRecords[i];
                    if (hdnserialid.value != "") hdnserialid.value += ',' + record.value;
                    if (hdnserialid.value == "") {
                        hdnserialid.value = record.value;
                    }
                }
            }

        }


        function selectedRecGrid() {

             var hdnskuqty = document.getElementById("<%= hdnskuqty.ClientID %>");
            var hdnserialid = document.getElementById("<%= hdnserialid.ClientID %>");
            hdnserialid.value = "";

            if (GridProductSearch.SelectedRecords.length > 0) {
                for (var i = 0; i < GridProductSearch.SelectedRecords.length; i++) {
                    var record = GridProductSearch.SelectedRecords[i];
                    if (hdnserialid.value != "") hdnserialid.value += ',' + record.value;
                    if (hdnserialid.value == "") {
                        hdnserialid.value = record.value;
                    }
                }
            }
           // alert(hdnserialid.value);
            if(hdnserialid.value == "" || hdnserialid.value == null)
            { hdnserialid.value = 0; }

            if (hdnserialid.value == 0) {
                alert("Select SKU serial");
            }
            else {
                var skuqty = hdnskuqty.value;              
                var count = hdnserialid.value.split(",")
                if (count.length == skuqty) {
                    PageMethods.WMGetSKUSerial(hdnserialid.value, onSuccessWMGetSKUSerial, null);
                }
                else {
                    alert("Select " + skuqty + " SKU serial");
                }

            }
        }


        function onSuccessWMGetSKUSerial(result) {
            if (result != "0") {
                window.opener.updateSerialCountOnPopupClose(result);
                self.close();
            }

        }

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

