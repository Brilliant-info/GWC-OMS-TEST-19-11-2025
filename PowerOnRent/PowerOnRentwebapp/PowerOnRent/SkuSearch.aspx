<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/CRM.Master" EnableEventValidation="false" Theme="Blue" CodeBehind="SkuSearch.aspx.cs" Inherits="PowerOnRentwebapp.PowerOnRent.SkuSearch" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="obout" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../Territory/UC_Territory.ascx" TagName="UC_Territory" TagPrefix="uc1" %>
<%@ Register Src="../Address/UCAddress.ascx" TagName="UCAddress" TagPrefix="uc7" %>
<%@ Register Src="../CommonControls/UC_Date.ascx" TagName="UC_Date" TagPrefix="uc2" %>
<%@ Register Src="../CommonControls/UCFormHeader.ascx" TagName="UCFormHeader" TagPrefix="uc4" %>
<%@ Register Src="../CommonControls/UCToolbar.ascx" TagName="UCToolbar" TagPrefix="uc5" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CPHolder_FormHeader" runat="server">
    <%--<uc5:UCToolbar ID="UCToolbar1" runat="server" />--%>
    <uc4:UCFormHeader ID="UCFormHeader1" runat="server" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="CPHolder_Form" runat="server">
    <center>

        <asp:TabContainer ID="TabContainerUserCreation1" runat="server" ActiveTabIndex="0">
            <asp:TabPanel ID="TabPanelUsersList" runat="server" HeaderText="Sku Search">
                <ContentTemplate>
                    <center>
                        <table class="tableForm" style="width: 100%">
                            <tr>
                                <td style="width: 10%"></td>


                                <td style="width: 10%; text-align: right">
                                    <asp:Label ID="lblCustomer" runat="server" Text="Customer"></asp:Label>
                                </td>


                                <td style="width: 15%">
                                    <asp:DropDownList ID="ddlcompany" runat="server" Width="100%" ClientIDMode="Static"
                                        DataValueField="ID" DataTextField="Name" onchange="GetCompany(this);BindDepartment()">
                                    </asp:DropDownList>
                                </td>


                                <td style="width: 10%; text-align: right">
                                    <asp:Label ID="lbldept" runat="server" Text="Department"></asp:Label>
                                </td>
                                <td style="width: 17%">
                                    <asp:DropDownList ID="ddldepartment" runat="server" Width="100%" ClientIDMode="Static" DataValueField="ID" DataTextField="Territory"
                                        onchange="GetDepartmentId(this)">
                                        <%--OnSelectedIndexChanged="ddldepartment_SelectedIndexChanged" AutoPostBack="true"--%>
                                    </asp:DropDownList>
                                    <%--onchange="GetDepartment(this)"--%>
                                </td>

                                <td style="width: 10%; text-align: left">
                                     <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" OnClientClick="return Validate();" />
                                </td>
                                
                                <td style="width: 18%"></td>


                                <td style="width: 10%"></td>
                            </tr>
                        </table>
                        <table style="width:100%">
                            <tr>
                                <td  style="width: 10%"></td>
                                  <td  style="width: 80%">
                                         <table class="gridFrame" width="100%" style="margin: 3px 3px 3px 3px;">
                            <tr>
                                <td>
                                    <table style="width: 100%">
                                        <tr>
                                            <td style="text-align: left;">
                                                <asp:Label ID="lblheader" CssClass="headerText" runat="server" Text="SKU List"></asp:Label>
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
                                                        <%--<td style="text-align: right;">
                                        <input type="checkbox" id="chkWithBOM" />                                      
                                        <asp:Label ID="lblwithbom" CssClass="headerText" runat="server" Text="With BOM"></asp:Label>
                                    </td>--%>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="text-align: right;">
                                                <%--     <input type="button" runat="server" value="Submit" id="btnSubmitProductSearch1" onclick="selectedRec();" />--%>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <obout:Grid ID="GridProductSearch" runat="server" AllowAddingRecords="false"  AllowFiltering="true" Serialize="true"
                                        Width="100%"  AllowGrouping="true"   AllowMultiRecordSelection="true" AutoGenerateColumns="false"
                                         OnRebind="GridProductSearch_Rebind">
                                    <ClientSideEvents ExposeSender="true" />
                                        <Columns>
                                            <obout:Column DataField="ID" Visible="false">
                                            </obout:Column>
                                            <obout:Column HeaderText="Info" Visible="false" DataField="DDescription" Width="7%" Align="center" HeaderAlign="center" Wrap="true">
                                            </obout:Column>
                                            <obout:Column DataField="ProductType" Visible="false" HeaderText="Type" Width="0%"
                                                AllowFilter="false" ParseHTML="true">
                                            </obout:Column>
                                            <obout:Column DataField="ProductCode" HeaderText="Product Code" Align="left" HeaderAlign="left"
                                                Width="15%" AllowFilter="false" ParseHTML="true">
                                            </obout:Column>
                                            <obout:Column DataField="Name" HeaderText="Product Name" Align="left" HeaderAlign="left"
                                                Width="20%" AllowFilter="false" ParseHTML="true">
                                            </obout:Column>
                                            <obout:Column DataField="Description" HeaderText="Description" Align="left" HeaderAlign="left"
                                                Width="50%" AllowFilter="false" ParseHTML="true">
                                            </obout:Column>
                                            <obout:Column DataField="UOM" HeaderText="UOM" Align="left" HeaderAlign="left" Width="0%"
                                                AllowFilter="false" ParseHTML="true" Visible="false">
                                            </obout:Column>
                                            <obout:Column DataField="PrincipalPrice" Visible="false" HeaderText="Price" Align="right" HeaderAlign="right"
                                                Width="15%" AllowFilter="false" ParseHTML="true">
                                            </obout:Column>
                                            <obout:Column DataField="GroupSet" Visible="true" HeaderText="GroupSet" Width="13%"
                                                AllowFilter="false" ParseHTML="true" Align="center" HeaderAlign="center">
                                            </obout:Column>
                                            <obout:Column DataField="Path" HeaderText="Image" Align="center" HeaderAlign="center"
                                                ItemStyle-Height="30" Width="0%" AllowFilter="false" AllowGroupBy="false">
                                            </obout:Column>
                                            <obout:Column DataField="SkuImage" Visible="false" HeaderText="Sku Image" Width="13%">
                                            </obout:Column>
                                            <obout:Column DataField="AvailableBalance" Visible="true" HeaderText="AvailableBalance" Width="15%"
                                                AllowFilter="false" ParseHTML="true" Align="center" HeaderAlign="center">
                                            </obout:Column>
                                        </Columns>

                                    </obout:Grid>

                                </td>
                            </tr>
                            <tr>
                                <td>
                                   
                                </td>
                            </tr>
                        </table>

                                  </td>
                                  <td  style="width: 10%"></td>
                            </tr>
                        </table>

                     
                    </center>
                </ContentTemplate>
            </asp:TabPanel>
        </asp:TabContainer>
    </center>
    <asp:HiddenField ID="hdnSelectedCompany" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdnEditrecId" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdndeptid" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hndRoleSate" runat="server" />
      <asp:HiddenField ID="hndgrupByGrid" runat="server" />

    <script type="text/javascript">





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


        var ddlcompany = document.getElementById("<%=ddlcompany.ClientID %>");
        var ddldeptid = document.getElementById("<%=ddldepartment.ClientID %>");

        function GetCompany(obj) {
            var ddlcompany = document.getElementById("<%=ddlcompany.ClientID %>");
            var hdnSelectedCompany = document.getElementById("<%=hdnSelectedCompany.ClientID %>");
            hdnSelectedCompany.value = ddlcompany.value;
        }

        function BindDepartment() {
            var hdnSelectedCompany = document.getElementById('hdnSelectedCompany');            
            var obj1 = new Object();
            obj1.ddlcompanyId = document.getElementById("<%=ddlcompany.ClientID %>").value;
            hdnSelectedCompany.value=document.getElementById("<%=ddlcompany.ClientID %>").value;
            PageMethods.GetDepartment(obj1, getLoc_onSuccessed);
        }

        function GetDepartmentId(id) {
            var departmentId = document.getElementById("<%=ddldepartment.ClientID %>");
              var hdndepartmentId = document.getElementById("<%=hdndeptid.ClientID %>");
            hdndepartmentId.value = departmentId.value;
            //GridProductSearch.refresh();
        }

         function getLoc_onSuccessed(result) {

            ddldeptid.options.length = 0;
            for (var i in result) {
                AddOption(result[i].Name, result[i].Id);
            }
        }

        function AddOption(text, value) {

            var option = document.createElement('option');
            option.value = value;
            option.innerHTML = text;
            ddldeptid.options.add(option);
        }



        function Validate() {
            if (document.getElementById("<%=ddlcompany.ClientID%>").value == "0") {
                showAlert("Please Select Company ...", "Error", "#");
                document.getElementById("<%=ddlcompany.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=ddldepartment.ClientID%>").value == "0") {
                showAlert("Please Select Department ...", "Error", "#");
                document.getElementById("<%=ddldepartment.ClientID%>").focus();
                return false;
            }
        }
    </script>
</asp:Content>
