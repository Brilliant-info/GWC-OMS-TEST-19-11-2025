<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/CRM.Master" EnableEventValidation="false"
    AutoEventWireup="true" CodeBehind="ProductMaster.aspx.cs" Inherits="PowerOnRentwebapp.Product.ProductMaster" %>

<%@ Register Assembly="obout_Flyout2_NET" Namespace="OboutInc.Flyout2" TagPrefix="obout" %>
<%@ Register Assembly="Obout.Ajax.UI" Namespace="Obout.Ajax.UI.FileUpload" TagPrefix="obout" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="obout" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../CommonControls/UCFormHeader.ascx" TagName="UCFormHeader" TagPrefix="uc1" %>
<%@ Register Src="../CommonControls/UCToolbar.ascx" TagName="UCToolbar" TagPrefix="uc2" %>
<%@ Register Src="../Document/UC_AttachDocument.ascx" TagName="UC_AttachDocument"
    TagPrefix="uc3" %>
<%@ Register Src="../CommonControls/UC_Date.ascx" TagName="UC_Date" TagPrefix="uc4" %>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHolder_FormHeader" runat="server">
    <uc2:UCToolbar ID="UCToolbar1" runat="server" />
    <uc1:UCFormHeader ID="UCFormHeader1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHolder_Form" runat="server">
    <asp:UpdatePanel ID="UpdatePanelTabPanelProductList" runat="server">
        <ContentTemplate>
            <asp:ValidationSummary ID="validationsummary_ProductMaster" runat="server" ShowMessageBox="true"
                ShowSummary="false" DisplayMode="bulletlist" ValidationGroup="Save" />
            <asp:HiddenField ID="hdnprodID" runat="server" />
            <asp:HiddenField ID="hdncompanyid" runat="server" />
            <asp:HiddenField ID="hdndeptid" runat="server" />
            <asp:HiddenField ID="hdnSelectedPrdId" runat="server" />
            <asp:HiddenField ID="hdnstate" runat="server" />
            <asp:HiddenField ID="hdnImgState" runat="server" />
            <asp:HiddenField ID="hdnProductSearchSelectedRec" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnproductsearchId" runat="server" ClientIDMode="Static" />
            <asp:HiddenField ID="hdnBomDetailId" runat="server" />
            <asp:HiddenField ID="hdnSelImage" runat="server" />
            <asp:TabContainer runat="server" ID="TabContainerProductMaster" ActiveTabIndex="4">
                <asp:TabPanel ID="TabPanelProductList" runat="server" HeaderText="SKU List">
                    <ContentTemplate>
                        <asp:UpdateProgress ID="UpdateGirdProductProcess" runat="server" AssociatedUpdatePanelID="Up_PnlGirdProduct">
                            <ProgressTemplate>
                                <center>
                                    <div class="modal">
                                        <img src="../App_Themes/Blue/img/ajax-loader.gif" style="top: 50%;" />
                                    </div>
                                </center>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        <asp:UpdatePanel ID="Up_PnlGirdProduct" runat="server">
                            <ContentTemplate>
                                <center>
                                    <table class="gridFrame" width="100%">
                                        <tr>
                                            <td>
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td style="width: 50%;text-align:left">
                                                            <asp:Label ID="lblskulist" Style="color: white; font-size: 15px; font-weight: bold;" runat="server" Text="SKU List"></asp:Label>
                                                        </td>
                                                         <td style="width: 50%;text-align:right">
                                                             <input type="button" id="btnupdate" runat="server" value="Update Serial Flag" onclick="Updateserialflag()" />
                                                         </td>
                                                    </tr>
                                                </table>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td>
                                                <obout:Grid ID="grvProduct" runat="server" AutoGenerateColumns="False" AllowAddingRecords="False" 
                                                    AllowFiltering="True" AllowGrouping="True" AllowMultiRecordSelection="false" 
                                                    AllowRecordSelection="true" Width="100%" AllowColumnResizing="true" AllowColumnReordering="true">
                                                    <ScrollingSettings ScrollHeight="250"/>
                                                    <Columns>
                                                        <obout:Column HeaderText="Edit" DataField="ID" Width="7%" Align="center" HeaderAlign="center">
                                                            <TemplateSettings TemplateId="TemplateEdit" />
                                                        </obout:Column>
                                                        <%--<obout:Column HeaderText="Type" DataField="ProductType" Width="20%" />--%>
                                                        <obout:Column HeaderText="WMS SKU Code" DataField="ProductCode" Width="15%" />
                                                        <obout:Column HeaderText="SKU Code" DataField="OMSSKUCode" Width="20%" />
                                                        <obout:Column HeaderText="SKU Name" DataField="Name" Width="18%" />
                                                        <obout:Column HeaderText="Description" DataField="description" Width="23%" />
                                                        <obout:Column HeaderText="Customer" DataField="CompanyName" Width="19%" />
                                                        <obout:Column HeaderText="Department" DataField="Territory" Width="18%" />
                                                        <obout:Column HeaderText="Group Set" DataField="GroupSet" Width="7%" Align="center" />
                                                        <%-- <obout:Column HeaderText="UOM" DataField="uom1" Width="15%" />--%>
                                                        <obout:Column HeaderText="Retail Price" DataField="PrincipalPrice" Width="12%" Align="right" HeaderAlign="center" />
                                                        <obout:Column HeaderText="MOQ" DataField="moq" Width="6%" Align="right" HeaderAlign="center" />
                                                          <obout:Column HeaderText="Virtual Quantity" DataField="VirtualQty" Width="10%" Align="right" HeaderAlign="center" />
                                                         <%--<obout:Column HeaderText="Available Virtual Qty" DataField="AvailVirtualQty" Width="12%" Align="right" HeaderAlign="center" />--%>
                                                        <obout:Column DataField="AvailableBalance" Width="14%" Align="right" HeaderAlign="center">
                                                            <%-- <TemplateSettings HeaderTemplateId="HeaderTempAmount" />--%>
                                                        </obout:Column>
                                                        <%--   <obout:Column HeaderText="Price"  DataField="PrincipalPrice" Width="30%" Align="right" />--%>
                                                        <obout:Column HeaderText="Active" HeaderAlign="center" DataField="active" Width="8%" Align="center">
                                                            <TemplateSettings TemplateId="tplActive" />
                                                        </obout:Column>
                                                    </Columns>
                                                    <Templates>
                                                        <obout:GridTemplate ID="HeaderTempAmount" runat="server">
                                                            <Template>
                                                                &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp Price
                                                            </Template>
                                                        </obout:GridTemplate>
                                                        <obout:GridTemplate runat="server" ID="tplActive">
                                                            <Template>
                                                                <%# (Container.DataItem["Active"].ToString() == "Y" ? "Yes" : "No")%>
                                                            </Template>
                                                        </obout:GridTemplate>
                                                        <obout:GridTemplate runat="server" ID="TemplateEdit">
                                                            <Template>
                                                                <asp:ImageButton ID="imgBtnEdit1" runat="server" ImageUrl="../App_Themes/Blue/img/Edit16.png"
                                                                    OnClick="imgBtnEdit_OnClick" ToolTip='<%# (Container.Value) %>' CausesValidation="false" />
                                                            </Template>
                                                        </obout:GridTemplate>
                                                    </Templates>
                                                </obout:Grid>
                                            </td>
                                        </tr>
                                    </table>
                                </center>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="tabProductDetails" runat="server" HeaderText="SKU Details">
                    <ContentTemplate>
                        <asp:UpdateProgress ID="UpdateProgress_ProductDetails" runat="server" AssociatedUpdatePanelID="Uppnl_productDetails">
                            <ProgressTemplate>
                                <center>
                                    <div class="modal">
                                        <img src="../App_Themes/Blue/img/ajax-loader.gif" style="top: 50%;" />
                                    </div>
                                </center>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        <asp:UpdatePanel ID="Uppnl_productDetails" runat="server">
                            <ContentTemplate>
                                <center>
                                    <table class="tableForm">
                                        <%--<tr>
                                            <td>
                                                <req>Product Type :</req>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlProductType" runat="server" DataTextField="ProductType"
                                                    DataValueField="ID" Width="200px" />
                                                <asp:RequiredFieldValidator ID="req_ddlProductType" runat="server" ErrorMessage="Select Product Type"
                                                    ControlToValidate="ddlProductType" InitialValue="0" ValidationGroup="Save" Display="None"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                <req>Category :</req>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlCategory" runat="server" DataTextField="ProductCategory"
                                                    onchange="printProductSubCategory();" ClientIDMode="Static" DataValueField="ID"
                                                    Width="200px">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlCategory" runat="server" ErrorMessage="Select Category"
                                                    DataValueField="ID" DataTextField="ProductCategory" ControlToValidate="ddlCategory"
                                                    InitialValue="0" ValidationGroup="Save" Display="None"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                <req> Sub Category :</req>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlSubCategory" runat="server" ClientIDMode="Static" Width="200px"
                                                    DataTextField="ProductSubCategory" DataValueField="ID">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Select Sub Category"
                                                    ControlToValidate="ddlSubCategory" InitialValue="0" ValidationGroup="Save" Display="None"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblskucode" runat="server" Text="WMS SKU code"></asp:Label>
                                                :
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txtProductCode" runat="server" MaxLength="50" Width="194px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <req> <asp:Label ID="lblskuname" runat="server" Text="SKU Name"></asp:Label> </req>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtProductName" runat="server" MaxLength="100" Width="194px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="reqtxtProductName" runat="server" ErrorMessage="Enter Product Name"
                                                    ControlToValidate="txtProductName" ValidationGroup="Save" Display="None"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                <req><asp:Label ID="lblCompany" runat="server" Text="Company"></asp:Label></req>
                                                :
                                            </td>
                                            <td style="text-align: left">
                                                <asp:DropDownList ID="ddlcompany" DataTextField="Name" DataValueField="ID" onchange="GetDepartment()"
                                                    Width="190px" runat="server">
                                                </asp:DropDownList>
                                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtdepartment"
                                                        Display="None" ErrorMessage="Please Enter Department Name" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td>
                                                <req><asp:Label ID="lbldepartment" runat="server" Text=" Department"></asp:Label></req>
                                                :
                                            </td>
                                            <td style="text-align: left">
                                                <asp:DropDownList ID="ddldepartment" DataTextField="Territory" DataValueField="ID" onchange="Getdeptid()"
                                                    Width="200px" runat="server">
                                                </asp:DropDownList>

                                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtdepartment"
                                                        Display="None" ErrorMessage="Please Enter Department Name" ValidationGroup="Save"></asp:RequiredFieldValidator>--%>
                                            </td>
                                            <td>
                                                <req><asp:Label ID="lblomsskucode" runat="server" Text="SKU Code"></asp:Label></req>
                                                :
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtomsskucode" runat="server" MaxLength="50" Width="194px"></asp:TextBox>
                                            </td>
                                            <td>
                                                <req><asp:Label ID="lblcost" runat="server" Text="Cost"></asp:Label></req>
                                                :
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txtcost" runat="server" MaxLength="15" Width="184px" Style="text-align: right;"
                                                    onkeydown="return AllowDecimal(this,event);" onkeypress="return AllowDecimal(this,event);"></asp:TextBox>

                                                <asp:DropDownList ID="ddlUOM" runat="server" DataTextField="Name" DataValueField="ID" Visible="false"
                                                    Width="187px">
                                                </asp:DropDownList>
                                                <%--<asp:RequiredFieldValidator ID="req_ddlUOM" runat="server" ErrorMessage="Please Select Unit"
                                                    ControlToValidate="ddlUOM" ValidationGroup="Save" Display="None" InitialValue="0"></asp:RequiredFieldValidator>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <req><asp:Label ID="lblpriciprice" runat="server" Text="Principal Price"></asp:Label></req>
                                                :
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txtPrincipalPrice" runat="server" MaxLength="15" Width="194px" Style="text-align: right;"
                                                    onkeydown="return AllowDecimal(this,event);" onkeypress="return AllowDecimal(this,event);"></asp:TextBox>

                                                <span style="position: absolute; top: 0px; left: 0px; visibility: hidden;"><a id="changePrice1" runat="server" style="visibility: hidden;"></a></span>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblgrpset" runat="server" Text="Group Set" />
                                                :
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlBom" runat="server"  ValidationGroup="Save" Width="200px" onchange="ShowHideMOQ()">
                                                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                    <asp:ListItem Selected="True" Text="No" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                             <td>
                                                  <req><asp:Label ID="lblMOQ" runat="server" Text="MOQ" />  </req>
                                                :
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txtMOQ" runat="server" Style="text-align: right" MaxLength="50"
                                                 Width="184px" onkeypress="return fnAllowDigitsOnly(event)"></asp:TextBox>
                                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter MOQ"
                                                    ControlToValidate="txtMOQ" ValidationGroup="Save" Display="None"></asp:RequiredFieldValidator>--%>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lbldescri" runat="server" Text="Description"></asp:Label>
                                                :
                                            </td>
                                            <td style="text-align: left" colspan="6">
                                                <asp:TextBox ID="txtdescription" runat="server" TextMode="MultiLine" Width="745px"></asp:TextBox>
                                            </td>

                                        </tr>
                                        <tr>
                                           <td>
                                                <req><asp:Label ID="lblactive" runat="server" Text="Active"/></req>
                                                :
                                            </td>
                                            <td style="text-align: left">
                                                <obout:OboutRadioButton ID="rbtYes" runat="server" Text="Yes  " GroupName="rbtActive"
                                                    Checked="True" FolderStyle="">
                                                </obout:OboutRadioButton>
                                                &nbsp;&nbsp;
                                                    <obout:OboutRadioButton ID="rbtNo" runat="server" Text="No"
                                                        GroupName="rbtActive" FolderStyle="">
                                                    </obout:OboutRadioButton>
                                            </td>
                                             <td>
                                                <asp:Label ID="lblskutype" runat="server" Text="SKU Type" /> :
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlskutype" runat="server" ValidationGroup="Save" Width="200px" onchange="ShowHideMOQ()">
                                                    <asp:ListItem Text="General SKU" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Virtual SKU" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                             <td>
                                                <req><asp:Label ID="Label1" runat="server" Text="Serial Flag"/></req>
                                                :
                                            </td>
                                            <td style="text-align: left">
                                                <obout:OboutRadioButton ID="rbtserialYes" runat="server" Text="Yes" GroupName="rbtActive"
                                                    FolderStyle="">
                                                </obout:OboutRadioButton>
                                                &nbsp;&nbsp;
                                                    <obout:OboutRadioButton ID="rbtserialNo" runat="server" Text="No"
                                                       Checked="True"  GroupName="rbtActive" FolderStyle="">
                                                    </obout:OboutRadioButton>
                                            </td>
                                           
                                        </tr>
                                        <%-- <tr>
                                            <td>
                                                Product Specification :
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:CheckBoxList ID="chkProductSpe" runat="server" RepeatDirection="Vertical" RepeatColumns="2">
                                                    <asp:ListItem>Installable</asp:ListItem>
                                                    <asp:ListItem>AMC</asp:ListItem>
                                                </asp:CheckBoxList>
                                            </td>
                                            <td>
                                                Warranty :
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txtWarrenyInDays" runat="server" Width="100px" MaxLength="5" Style="text-align: right;"
                                                    onkeydown="AllowDecimal(this,event);" onkeypress="AllowInt(this,event);"></asp:TextBox>
                                                <span class="watermark">( In Days )</span>
                                            </td>
                                            <td>
                                                Guarantee :
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txtGuaranteeInDays" runat="server" Width="100px" MaxLength="5" Style="text-align: right;"
                                                    onkeydown="AllowDecimal(this,event);" onkeypress="AllowInt(this,event);"></asp:TextBox>
                                                <span class="watermark">( In Days )</span>
                                            </td>
                                        </tr>--%>
                                    </table>
                                    <table class="gridFrame" width="69%">
                                        <tr>
                                            <td>
                                                <table style="width: 70%">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblspecifictn" CssClass="headerText" runat="server" Text="SKU UDF List" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <obout:Grid ID="Grid1" runat="server" AllowFiltering="True" AllowGrouping="True"
                                                    AllowColumnResizing="true" AutoGenerateColumns="False" Width="100%"
                                                    AllowAddingRecords="False" PageSize="6" OnInsertCommand="GVProductSpecification_InsertRecord" OnRebind="Grid1_RebindGrid">
                                                    <ScrollingSettings ScrollHeight="150" />
                                                    <Columns>
                                                        <%--<obout:Column ID="Column8" HeaderText="ID" DataField="ID" Width="10%" runat="server" Visible="false">
                                                          </obout:Column>--%>
                                                        <obout:Column ID="Column0" DataField="ID" HeaderText="Edit" Width="5%" AllowSorting="false" Index="2"
                                                            Align="center" HeaderAlign="center">
                                                            <TemplateSettings TemplateId="GridTemplate9" />
                                                        </obout:Column>
                                                        <obout:Column DataField="Sequence" HeaderText="Edit" Width="5%" AllowSorting="false" Visible="false"
                                                            Align="center" HeaderAlign="center">
                                                            <TemplateSettings TemplateId="GvTempEdit" />
                                                        </obout:Column>
                                                        <obout:Column ID="Column1" HeaderText="User Defined Field" DataField="SpecificationTitle" Width="30%"
                                                            runat="server" Align="center" HeaderAlign="Center">
                                                        </obout:Column>
                                                        <obout:Column ID="Column2" HeaderText="Value" DataField="SpecificationDescription"
                                                            Width="30%" runat="server" Align="center" HeaderAlign="Center">
                                                        </obout:Column>
                                                        <obout:Column ID="Column3" HeaderText="Active" DataField="Active" Width="30%" runat="server" Align="center" HeaderAlign="Center">
                                                        </obout:Column>
                                                    </Columns>
                                                    <Templates>
                                                        <obout:GridTemplate ID="GridTemplate9" runat="server">
                                                            <Template>
                                                                <img id="imgBtnEdit" src="../App_Themes/Blue/img/Edit16.png" title="Edit" onclick="openSpecificWindow('<%# (Container.DataItem["ID"].ToString()) %>');"
                                                                    style="cursor: pointer;" />
                                                            </Template>
                                                        </obout:GridTemplate>
                                                        <obout:GridTemplate ID="GridTemplate2" runat="server">
                                                            <Template>
                                                                <asp:ImageButton ID="imgBtnEdit" CausesValidation="false" runat="server" ImageUrl="../App_Themes/Blue/img/Edit16.png"
                                                                    ToolTip='<%# Container.DataItem["Sequence"].ToString() %>' OnClick="imgBtnEdit_Click" />
                                                            </Template>
                                                        </obout:GridTemplate>
                                                        <obout:GridTemplate runat="server" ID="GridTemplate3">
                                                            <Template>
                                                                <%# (Container.Value)%>
                                                            </Template>
                                                        </obout:GridTemplate>
                                                        <obout:GridTemplate runat="server" ID="GridTemplate4" ControlPropertyName="value">
                                                            <Template>
                                                                <asp:TextBox ID="txtTitle" runat="server" MaxLength="50" Width="200px" Text='<%# (Container.Value)%>'></asp:TextBox>
                                                                <asp:RequiredFieldValidator runat="server" ID="req_txtTitle" ControlToValidate="txtTitle"
                                                                    ErrorMessage="Enter Value" Display="None" />
                                                            </Template>
                                                        </obout:GridTemplate>
                                                        <obout:GridTemplate runat="server" ID="GridTemplate5">
                                                            <Template>
                                                                <%# (Container.Value)%>
                                                            </Template>
                                                        </obout:GridTemplate>
                                                        <obout:GridTemplate runat="server" ID="GridTemplate6" ControlPropertyName="value">
                                                            <Template>
                                                                <asp:TextBox ID="txtDescription" runat="server" MaxLength="500" Width="200px" Text='<%# (Container.Value)%>'></asp:TextBox>
                                                                <asp:RequiredFieldValidator runat="server" ID="reqtxtDescription" ControlToValidate="txtDescription"
                                                                    ErrorMessage="Enter Value" Display="None" />
                                                            </Template>
                                                        </obout:GridTemplate>
                                                        <obout:GridTemplate ID="GridTemplate7" runat="server">
                                                            <Template>
                                                                <%# (Container.DataItem["Active"].ToString() == "Y" ? "Yes" : "No")%>
                                                            </Template>
                                                        </obout:GridTemplate>
                                                        <obout:GridTemplate ID="GridTemplate8" runat="server" ControlPropertyName="value">
                                                            <Template>
                                                                <asp:CheckBox ID="chkSpecificationIsActive" runat="server" />
                                                            </Template>
                                                        </obout:GridTemplate>
                                                    </Templates>
                                                </obout:Grid>
                                            </td>
                                        </tr>
                                    </table>
                                    <obout:Flyout runat="server" ID="FlyoutChangeProdPrice" OpenEvent="ONCLICK" CloseEvent="NONE"
                                        Position="TOP_CENTER" AttachTo="changePrice1">
                                        <div id="flyoutDiv">
                                            <table class="tableForm" cellspacing="5" cellpadding="5" style="text-align: left; margin: 10px;">
                                                <tr>
                                                    <td>
                                                        <req>New Price :</req>
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <asp:TextBox ID="txtNewPrice" runat="server" MaxLength="15" Width="100px" ClientIDMode="Static"
                                                            Style="text-align: right;" onkeydown="return AllowDecimal(this,event);" onkeypress="return AllowDecimal(this,event);"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <req>Effective Date :</req>
                                                    </td>
                                                    <td>
                                                        <uc4:UC_Date ID="UC_EffectiveDateNewPrice" runat="server" />
                                                    </td>
                                                    <td>
                                                        <span class="remove_a" style="position: absolute; margin-top: -18px; margin-right: -10px;"
                                                            onclick='<%=FlyoutChangeProdPrice.getClientID()%>.Close();'>X</span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <req>Start Date :</req>
                                                    </td>
                                                    <td>
                                                        <uc4:UC_Date ID="UC_StartDateNewPrice" runat="server" />
                                                    </td>
                                                    <td>End Date :
                                                    </td>
                                                    <td>
                                                        <uc4:UC_Date ID="UC_EndDateNewPrice" runat="server" />
                                                    </td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td>
                                                        <input type="button" value="Submit" id="btnSaveNewPrice" onclick="SaveNewPrice()" />
                                                        <input type="button" value="Clear" id="btnClearNewPrice" />
                                                    </td>
                                                    <td></td>
                                                </tr>
                                            </table>
                                        </div>
                                        <script type="text/javascript">
                                            function SaveNewPrice() {
                                                alert("called");
                                                var txtEffectiveDtNP = getDateTextBoxFromUC("<%= UC_EffectiveDateNewPrice.ClientID %>");
                                                var txtStartDtNP = getDateTextBoxFromUC("<%= UC_StartDateNewPrice.ClientID %>");
                                                var txtEndDtNP = getDateTextBoxFromUC("<%= UC_EndDateNewPrice.ClientID %>");
                                                var txtNewPrice = document.getElementById("txtNewPrice");
                                                var validateNewPrice = validateNewRate();
                                                if (validateNewPrice != "") {
                                                    alert(validateNewPrice);
                                                }
                                                else {

                                                    hdnprodID = document.getElementById("<%= hdnprodID.ClientID %>");

                                                    if (hdnprodID.value == "") hdnprodID = "0";
                                                    if (hdnprodID.value != "0") {
                                                        var obj = new Object;
                                                        obj.ProdID = parseInt(hdnprodID.value);
                                                        obj.Rate = parseFloat(txtNewPrice.value);
                                                        obj.EffectiveDate = txtEffectiveDtNP.value;
                                                        obj.StartDate = txtStartDtNP.value;
                                                        obj.EndDate = txtEndDtNP.value;
                                                        PageMethods.PMSaveNewRates(obj, onSuccessSaveNewPrice, null);
                                                    }
                                                }
                                            }

                                            function onSuccessSaveNewPrice(result) {
                                                if (parseInt(result) > 0) {
                                                    GVRateHistory1.refresh();
                                                    alert("New Rate saved successfully");
                                                }
                                            }

                                            function validateNewRate() {
                                                var txtEffectiveDtNP = getDateTextBoxFromUC("<%= UC_EffectiveDateNewPrice.ClientID %>");
                                                var txtStartDtNP = getDateTextBoxFromUC("<%= UC_StartDateNewPrice.ClientID %>");
                                                var txtEndDtNP = getDateTextBoxFromUC("<%= UC_EndDateNewPrice.ClientID %>");
                                                var txtNewPrice = document.getElementById("txtNewPrice");
                                                var result = "";
                                                if (txtNewPrice.value == "") { result = "-Enter new price"; }
                                                if (txtEffectiveDtNP.value == "") {
                                                    if (result == "") { result = "-Enter effective date"; }
                                                    else { result += "\n-Enter effective date"; }
                                                }
                                                if (txtStartDtNP.value == "") {
                                                    if (result == "") { result = "-Enter start date"; }
                                                    else { result += "\n-Enter start date"; }
                                                }
                                                return result;
                                            }

                                            function clearNewPrice() {
                                                var txtEffectiveDtNP = getDateTextBoxFromUC("<%= UC_EffectiveDateNewPrice.ClientID %>");
                                                var txtStartDtNP = getDateTextBoxFromUC("<%= UC_StartDateNewPrice.ClientID %>");
                                                var txtEndDtNP = getDateTextBoxFromUC("<%= UC_EndDateNewPrice.ClientID %>");
                                                var txtNewPrice = document.getElementById("txtNewPrice");
                                                txtEffectiveDtNP.value = "";
                                                txtStartDtNP.value = "";
                                                txtEndDtNP.value = "";
                                                txtNewPrice.value = "";
                                            }
                                        </script>
                                    </obout:Flyout>
                                    <%--<table class="gridFrame" width="60%">
                                        <tr>
                                            <td>
                                                <a class="headerText">Price History</a>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <obout:Grid ID="GVRateHistory1" runat="server" AutoGenerateColumns="false" AllowAddingRecords="false"
                                                    Width="100%" OnRebind="GVRateHistory_OnRebind">
                                                    <Columns>
                                                        <obout:Column DataField="ID" Visible="false" Width="0%">
                                                        </obout:Column>
                                                        <obout:Column DataField="EffectiveDate" HeaderText="Effective Date" Width="10%" Align="center"
                                                            HeaderAlign="center" DataFormatString="{0:dd-MMM-yyyy}">
                                                        </obout:Column>
                                                        <obout:Column DataField="StartDate" HeaderText="Start Date" Width="10%" Align="center"
                                                            HeaderAlign="center" DataFormatString="{0:dd-MMM-yyyy}">
                                                        </obout:Column>
                                                        <obout:Column DataField="EndDate" HeaderText="End Date" Width="10%" Align="center"
                                                            HeaderAlign="center" DataFormatString="{0:dd-MMM-yyyy}">
                                                        </obout:Column>
                                                        <obout:Column DataField="Rate" HeaderText="Product Price" Width="15%" Align="right"
                                                            HeaderAlign="right">
                                                        </obout:Column>
                                                    </Columns>
                                                </obout:Grid>
                                            </td>
                                        </tr>
                                    </table>--%>
                                </center>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel runat="server" ID="tabSpecification" HeaderText="Specifications">
                    <ContentTemplate>
                        <asp:ValidationSummary ID="validationsummary1" runat="server" ShowMessageBox="true"
                            ShowSummary="false" DisplayMode="bulletlist" ValidationGroup="ProductSpe" />
                        <asp:UpdateProgress ID="UpdateProgress_Specification" runat="server" AssociatedUpdatePanelID="Up_pnl_Spefication">
                            <ProgressTemplate>
                                <center>
                                    <div class="modal">
                                        <img src="../App_Themes/Blue/img/ajax-loader.gif" style="top: 50%;" />
                                    </div>
                                </center>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        <asp:UpdatePanel ID="Up_pnl_Spefication" runat="server">
                            <ContentTemplate>
                                <center>
                                    <%--<table class="tableForm">
                                        <tr>
                                            <td>
                                                <req> Specification Title :</req>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtspecificationtitle" runat="server" MaxLength="50" Width="200px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="valRftxtspecificationtitle" runat="server" ErrorMessage="Please enter Specification Title"
                                                    ControlToValidate="txtspecificationtitle" ValidationGroup="ProductSpe" Display="None"></asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                <req> Specification Description :</req>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtSpecificationDesc" runat="server" TextMode="MultiLine" Width="200px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="valRftxtSpecificationDesc" runat="server" ErrorMessage="Please enter Specification Description"
                                                    ControlToValidate="txtSpecificationDesc" ValidationGroup="ProductSpe" Display="None"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" style="text-align: right;">
                                                <asp:Button runat="server" ID="BtnSubMitproductSp" ValidationGroup="ProductSpe" OnClick="BtnSubMitproductSp_Click"
                                                    Text="Submit" />
                                            </td>
                                        </tr>
                                    </table>--%>
                                    <table class="gridFrame" width="100%">
                                        <tr>
                                            <td>
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td>
                                                            <a class="headerText">SKU Specification List</a>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <obout:Grid ID="GVProductSpecification" runat="server" AllowFiltering="true" AllowGrouping="true"
                                                    AllowSorting="true" AllowColumnResizing="true" AutoGenerateColumns="false" Width="100%"
                                                    AllowAddingRecords="true" OnInsertCommand="GVProductSpecification_InsertRecord">
                                                    <Columns>
                                                        <obout:Column DataField="Sequence" HeaderText="Edit" Width="5%" AllowSorting="false"
                                                            Align="center" HeaderAlign="center">
                                                            <TemplateSettings TemplateId="GvTempEdit" />
                                                        </obout:Column>
                                                        <obout:Column HeaderText="Specification Title" DataField="SpecificationTitle" Width="25%"
                                                            runat="server">
                                                            <TemplateSettings TemplateId="TemplateProductSpecificationTitle" EditTemplateId="EditTemplateProductSpecificationTitle" />
                                                        </obout:Column>
                                                        <obout:Column HeaderText="Specification Description" DataField="SpecificationDescription"
                                                            Width="50%" runat="server">
                                                            <TemplateSettings TemplateId="TemplateDescription" EditTemplateId="EditTemplateDescription" />
                                                        </obout:Column>
                                                        <obout:Column HeaderText="Active" DataField="Active" Width="10%" runat="server">
                                                            <TemplateSettings TemplateId="TemplateProductSpecificationActive" EditTemplateId="EditTemplateProductSpecificationActive" />
                                                        </obout:Column>
                                                    </Columns>
                                                    <Templates>
                                                        <obout:GridTemplate ID="GvTempEdit" runat="server">
                                                            <Template>
                                                                <asp:ImageButton ID="imgBtnEdit" CausesValidation="false" runat="server" ImageUrl="../App_Themes/Blue/img/Edit16.png"
                                                                    ToolTip='<%# Container.DataItem["Sequence"].ToString() %>' OnClick="imgBtnEdit_Click" />
                                                            </Template>
                                                        </obout:GridTemplate>
                                                        <obout:GridTemplate runat="server" ID="TemplateProductSpecificationTitle">
                                                            <Template>
                                                                <%# (Container.Value)%>
                                                            </Template>
                                                        </obout:GridTemplate>
                                                        <obout:GridTemplate runat="server" ID="EditTemplateProductSpecificationTitle" ControlPropertyName="value">
                                                            <Template>
                                                                <asp:TextBox ID="txtTitle" runat="server" MaxLength="50" Width="200px" Text='<%# (Container.Value)%>'></asp:TextBox>
                                                                <asp:RequiredFieldValidator runat="server" ID="req_txtTitle" ControlToValidate="txtTitle"
                                                                    ErrorMessage="Enter Value" Display="None" />
                                                            </Template>
                                                        </obout:GridTemplate>
                                                        <obout:GridTemplate runat="server" ID="TemplateDescription">
                                                            <Template>
                                                                <%# (Container.Value)%>
                                                            </Template>
                                                        </obout:GridTemplate>
                                                        <obout:GridTemplate runat="server" ID="EditTemplateDescription" ControlPropertyName="value">
                                                            <Template>
                                                                <asp:TextBox ID="txtDescription" runat="server" MaxLength="500" Width="200px" Text='<%# (Container.Value)%>'></asp:TextBox>
                                                                <asp:RequiredFieldValidator runat="server" ID="reqtxtDescription" ControlToValidate="txtDescription"
                                                                    ErrorMessage="Enter Value" Display="None" />
                                                            </Template>
                                                        </obout:GridTemplate>
                                                        <obout:GridTemplate ID="TemplateProductSpecificationActive" runat="server">
                                                            <Template>
                                                                <%# (Container.DataItem["Active"].ToString() == "Y" ? "Yes" : "No")%>
                                                            </Template>
                                                        </obout:GridTemplate>
                                                        <obout:GridTemplate ID="EditTemplateProductSpecificationActive" runat="server" ControlPropertyName="value">
                                                            <Template>
                                                                <asp:CheckBox ID="chkSpecificationIsActive" runat="server" />
                                                            </Template>
                                                        </obout:GridTemplate>
                                                    </Templates>
                                                </obout:Grid>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:HiddenField ID="hndsequence" runat="server" />
                                    <asp:HiddenField ID="Hndstate" runat="server" />
                                    <asp:HiddenField ID="hndproductid" runat="server" />
                                </center>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel runat="server" ID="tabTaxSetup" HeaderText="Tax Setup" Visible="false">
                    <ContentTemplate>
                        <asp:UpdateProgress ID="UpdateProgress_TaxSetUp" runat="server" AssociatedUpdatePanelID="Up_pnl_TaxSetUp">
                            <ProgressTemplate>
                                <center>
                                    <div class="modal">
                                        <img src="../App_Themes/Blue/img/ajax-loader.gif" style="top: 50%;" />
                                    </div>
                                </center>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        <asp:UpdatePanel ID="Up_pnl_TaxSetUp" runat="server">
                            <ContentTemplate>
                                <center>
                                    <table class="gridFrame" width="70%">
                                        <tr>
                                            <td>
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td>
                                                            <a style="color: white; font-size: 15px; font-weight: bold;">Tax List</a>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <obout:Grid ID="GVTaxSetup" runat="server" ShowLoadingMessage="false" AutoGenerateColumns="false"
                                                    AllowGrouping="false" AllowColumnResizing="false" AllowAddingRecords="false"
                                                    AllowColumnReordering="true" AllowMultiRecordSelection="true" AllowPageSizeSelection="false"
                                                    AllowPaging="false" AllowRecordSelection="false" AllowSorting="false" FilterType="Normal"
                                                    AllowFiltering="false" Serialize="false" CallbackMode="true" Width="100%" ShowFooter="false"
                                                    PageSize="-1" ShowColumnsFooter="true" OnRebind="GVTaxSetup_OnRebind" OnLoad="GVTaxSetup_OnRebind">
                                                    <ClientSideEvents ExposeSender="true" />
                                                    <Columns>
                                                        <obout:Column DataField="ParentID" Visible="false">
                                                        </obout:Column>
                                                        <obout:Column DataField="ID" Visible="false">
                                                        </obout:Column>
                                                        <obout:Column DataField="Checked" Visible="false">
                                                        </obout:Column>
                                                        <obout:Column DataField="CheckBoxVisible" Width="10%" Align="center" HeaderAlign="center"
                                                            HeaderText="Select Tax">
                                                            <TemplateSettings TemplateId="ItemTempCheckBox" HeaderTemplateId="HeaderTempCheckBox" />
                                                        </obout:Column>
                                                        <obout:Column DataField="Name" HeaderText="Tax Name" Width="35%" HeaderAlign="left"
                                                            Align="left">
                                                        </obout:Column>
                                                        <obout:Column DataField="Description" HeaderText="Description" Width="35%" HeaderAlign="left"
                                                            Align="left">
                                                        </obout:Column>
                                                        <obout:Column DataField="Percent" HeaderText="Tax Rate [ % ]" Width="20%" HeaderAlign="right"
                                                            Align="right">
                                                            <TemplateSettings TemplateId="GridTemplatePercent" />
                                                        </obout:Column>
                                                    </Columns>
                                                    <Templates>
                                                        <obout:GridTemplate ID="ItemTempCheckBox" runat="server">
                                                            <Template>
                                                                <asp:CheckBox runat="server" ID="chkboxID" Visible='<%# Convert.ToBoolean(Container.Value) %>'
                                                                    Checked='<%# Convert.ToBoolean(Container.DataItem["Checked"]) %>' Style="cursor: hand;"
                                                                    ToolTip='<%# Container.DataItem["ID"].ToString() %>' onclick="ProductMasterTaxSetup(this)" />
                                                            </Template>
                                                        </obout:GridTemplate>
                                                        <obout:GridTemplate ID="GridTemplatePercent" runat="server">
                                                            <Template>
                                                                <span style="margin-right: 10px;">
                                                                    <%# Container.Value %></span>
                                                            </Template>
                                                        </obout:GridTemplate>
                                                    </Templates>
                                                </obout:Grid>
                                            </td>
                                        </tr>
                                    </table>
                                </center>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel runat="server" ID="tabImages" HeaderText="Images">
                    <ContentTemplate>
                        <center>
                            <asp:UpdateProgress ID="UpdateProgressProductMasterImages" runat="server" AssociatedUpdatePanelID="UpdatePanelProductMasterImages">
                                <ProgressTemplate>
                                    <center>
                                        <div class="modal">
                                            <img src="../App_Themes/Blue/img/ajax-loader.gif" style="top: 50%;" />
                                        </div>
                                    </center>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                            <asp:UpdatePanel ID="UpdatePanelProductMasterImages" runat="server">
                                <ContentTemplate>
                                    <table class="tableForm">
                                        <tr>
                                            <td>
                                                <req> <asp:Label ID="lblimagetitle" runat="server" Text="Image Title"/></req>
                                                :
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txtImageTitle" Width="200px" runat="server"></asp:TextBox>
                                            </td>
                                            <td>
                                                <req><asp:Label ID="lblimageselect" runat="server" Text="Select Image"/></req>
                                                :
                                            </td>
                                            <td rowspan="2">
                                                <asp:UpdatePanel ID="Updpnl" runat="server" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:PostBackTrigger ControlID="btnProductMasterUploadImg" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:FileUpload ID="FileUpload1" runat="server" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                </br>
                                                <span class="watermark">Only bmp | jpg | jpeg | jpe | gif | tiff | tif | png | pdf
                                                    <br />
                                        Max Limit 60 KB 
                                                </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblimagedescri" runat="server" Text="Image Description" />
                                                :
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txtImageDescription" Width="200px" TextMode="MultiLine" runat="server"></asp:TextBox>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td style="visibility:hidden;">
                                                <req> <asp:Label ID="lblactive1" runat="server" Text="Active"/></req>
                                                :
                                            </td>
                                            <td align="left" style="text-align: left; visibility :hidden;">
                                                <obout:OboutRadioButton ID="rbtnYes1" runat="server" Text="Yes" GroupName="rbtnActive"
                                                    Checked="True" FolderStyle="">
                                                </obout:OboutRadioButton>
                                                <obout:OboutRadioButton ID="rbtnNo1" runat="server" Text="No" GroupName="rbtnActive"
                                                    FolderStyle="">
                                                </obout:OboutRadioButton>
                                            </td>
                                            <td colspan="2">
                                                <asp:Button ID="btnProductMasterUploadImg" runat="server" Text="Upload Image" CausesValidation="false"
                                                    OnClick="btnProductMasterUploadImg_OnClick"></asp:Button>
                                            </td>
                                        </tr>
                                    </table>
                                    <table class="gridFrame" width="700px">
                                        <tr>
                                            <td>
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblimages" CssClass="headerText" runat="server" Text="Images" />
                                                            <%--<a class="headerText">Images</a>--%>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <obout:Grid ID="GVImages" runat="server" AutoGenerateColumns="false" EnableTypeValidation="true"
                                                    AllowAddingRecords="false" AllowFiltering="true" AllowGrouping="true" AllowSorting="true"
                                                    Width="100%" AllowRecordSelection="false">
                                                    <Columns>
                                                        <%-- <obout:Column DataField="ID" HeaderText="ID" Visible="false">
                                                        </obout:Column>--%>
                                                        <obout:Column HeaderText="Edit" DataField="ID" Width="10%" Align="center" HeaderAlign="center">
                                                            <TemplateSettings TemplateId="TemplateEditImg" />
                                                        </obout:Column>
                                                        <%-- <obout:Column DataField="ID1" ReadOnly="true" HeaderText="Sr.No." HeaderAlign="center" runat="server" Align="center"
                                                            ItemStyle-Height="30" Width="12%">
                                                            <TemplateSettings TemplateId="tplNumbering" />
                                                        </obout:Column>--%>
                                                        <obout:Column DataField="ImgeTitle" HeaderText="Image Title" Width="35%" ItemStyle-Height="30px">
                                                            <TemplateSettings TemplateId="TemplateImageName" />
                                                        </obout:Column>
                                                        <obout:Column DataField="ImageDesc" HeaderText="Image Description" Width="35%" ItemStyle-Height="30px">
                                                        </obout:Column>
                                                        <obout:Column DataField="Path" HeaderText="Image" Align="center" HeaderAlign="center"
                                                            ItemStyle-Height="30" Width="20%" AllowFilter="false" AllowGroupBy="false" Visible="false">
                                                            <TemplateSettings TemplateId="TemplateShowImage" />
                                                        </obout:Column>
                                                        <%-- <obout:Column ID="Download" DataField="Path" HeaderText="Download" Width="10%">
                                                            <TemplateSettings TemplateId="GvTempDownload" />
                                                        </obout:Column>--%>
                                                        <obout:Column DataField="SkuImage" HeaderText="Sku Image" Width="13%">
                                                            <TemplateSettings TemplateId="GetSkuImage" />
                                                        </obout:Column>
                                                        <obout:Column DataField="Active" HeaderText="Active" Width="10%" Visible="false">
                                                        </obout:Column>

                                                    </Columns>
                                                    <Templates>
                                                        <%--<obout:GridTemplate ID="GvTempDownload">
                                                            <Template>
                                                                <a href='<%# (Container.DataItem["Path"]) %>' target="_blank">
                                                                    <img src='<%# "../CommonControls/HomeSetupImg/download.png" %>' />
                                                                </a>
                                                            </Template>
                                                        </obout:GridTemplate>--%>
                                                        <obout:GridTemplate runat="server" ID="TemplateEditImg">
                                                            <Template>
                                                                <asp:ImageButton ID="imgBtnEdit1" runat="server" ImageUrl="../App_Themes/Blue/img/Edit16.png"
                                                                    OnClick="imgImageBtnEdit_OnClick" ToolTip='<%# (Container.Value) %>' CausesValidation="false" />
                                                            </Template>
                                                        </obout:GridTemplate>

                                                        <%--<obout:GridTemplate runat="server" ID="tplNumbering">
                                                            <Template>
                                                                <%# (Container.RecordIndex + 1) %>
                                                            </Template>
                                                        </obout:GridTemplate>--%>
                                                        <obout:GridTemplate runat="server" ID="TemplateImageName">
                                                            <Template>
                                                                <%# (Container.Value.ToString().Trim()) %>
                                                            </Template>
                                                        </obout:GridTemplate>
                                                        <obout:GridTemplate runat="server" ID="TemplateShowImage">
                                                            <Template>
                                                                <a target="_blank" href='<%# (Container.Value) %>' style="cursor: pointer;">
                                                                    <asp:Image ID="Image1" runat="server" ImageUrl='<%# (Container.Value) %>' Height="30px"
                                                                        Width="30px" Style="border: solid 2px gray;" ToolTip="Click here to Download" />
                                                                </a>
                                                            </Template>
                                                        </obout:GridTemplate>
                                                        <obout:GridTemplate runat="server" ID="GetSkuImage">
                                                            <Template>
                                                                <asp:Image ID="ImageDisplay" runat="server" ImageUrl='<%#"DisplaySkuImage.ashx?ID="+ hdnprodID.Value +"" %>' Height="50px" Width="50px" Style="border: solid 2px gray;" />
                                                            </Template>
                                                        </obout:GridTemplate>
                                                    </Templates>
                                                </obout:Grid>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnProductMasterUploadImg" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </center>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel runat="server" ID="tabDocuments" HeaderText="Documents">
                    <ContentTemplate>
                        <asp:UpdatePanel ID="UpdtPnlDoc" runat="server">
                            <ContentTemplate>
                                <uc3:UC_AttachDocument ID="UC_AttachDocument1" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel runat="server" ID="tabInventory" HeaderText="Inventory">
                    <ContentTemplate>
                        <center>
                            <%--<table class="tableForm" border="2" style="width: 700px;">--%>
                            <table class="tableForm" style="visibility: hidden;">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblopeningbal" runat="server" Text="Opening Balance"></asp:Label>
                                        :
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtOpbalance" Width="200px" runat="server" onkeydown="return AllowDecimal(this,event);" onkeypress="return AllowDecimal(this,event);"></asp:TextBox>
                                        <%-- <uc4:UC_Date ID="UC_EffectiveDateInventory" runat="server" /> --%>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblopbaldate" runat="server" Text="Date"></asp:Label>
                                        :
                                    </td>
                                    <td style="text-align: left;">
                                        <uc4:UC_Date ID="UC_EffectiveDateInventory" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td colspan="4" style="text-align: right;">
                                        <asp:Button ID="btninvsubmit" runat="server" Text="Submit" Visible="false" />
                                    </td>
                                </tr>
                            </table>
                            <table class="gridFrame" width="1250px">
                                <tr>
                                    <td>
                                        <asp:Label ID="lbldeptinventry" CssClass="headerText" runat="server" Text="Department wise Inventory" />
                                    </td>
                                    <td align="right">
                                          <asp:Button ID="btnvirtualQty" runat="server" OnClientClick="openvirtualQty()" Text=" Add Virtual Quantity" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <obout:Grid ID="GVInventory" runat="server" CallbackMode="true" Serialize="true"
                                            AllowColumnReordering="true" AllowColumnResizing="true" AutoGenerateColumns="false"
                                            AllowPaging="false" ShowLoadingMessage="true" AllowSorting="false" AllowManualPaging="false"
                                            AllowRecordSelection="false" ShowFooter="false" Width="111%" PageSize="-1" OnRebind="GVInventory_RebindGrid">
                                            <ClientSideEvents ExposeSender="true" />
                                            <Columns>
                                                <obout:Column DataField="SiteID" Visible="false" Width="10%">
                                                </obout:Column>
                                                <obout:Column DataField="ID" Visible="false" Width="10%">
                                                </obout:Column>
                                                <%-- <obout:Column DataField="Territory" HeaderText="Deartment" Width="15%">
                                                    <TemplateSettings TemplateId="GridTemplate1" />
                                                </obout:Column>--%>
                                                <obout:Column DataField="OpeningStock" HeaderText="Opening Quantity" Align="Center" HeaderAlign="center" Width="15%">
                                                </obout:Column>
                                                <obout:Column DataField="TotalReceiveQty" HeaderText="Receiving Quantity" Width="15%" Align="Center" HeaderAlign="center">
                                                </obout:Column>
                                                <obout:Column DataField="TotalDispatchQty" HeaderText="Dispatch Quantity" Width="15%" Align="Center" HeaderAlign="center">
                                                </obout:Column>
                                                <obout:Column DataField="ResurveQty" HeaderText="Reserve Quantity" Width="15%" Align="Center" HeaderAlign="center">
                                                </obout:Column>
                                                <obout:Column DataField="AvailableBalance" HeaderText="Current Balance" Width="15%" Align="Center" HeaderAlign="center">
                                                </obout:Column>
                                                 <obout:Column DataField="VirtualQty" HeaderText="Virtual Quantity" Width="15%" Align="Center" HeaderAlign="center">
                                                </obout:Column>
                                                <obout:Column DataField="VirtualReQty" HeaderText="Virtual ReOrder Quantity" Width="15%" Align="Center" HeaderAlign="center">
                                                </obout:Column>
                                                 <obout:Column DataField="AvailVirtualQty" HeaderText="Available Virtual Quantity" Width="18%" Align="Center" HeaderAlign="center">
                                                </obout:Column>
                                            </Columns>
                                            <Templates>
                                                <obout:GridTemplate runat="server" ID="GridTemplate1">
                                                    <Template>
                                                        <span style="font-weight: bold; margin-left: 5px;">
                                                            <%# Container.Value %></span>
                                                    </Template>
                                                </obout:GridTemplate>
                                                <obout:GridTemplate runat="server" ID="PlainEditTemplate">
                                                    <Template>
                                                        <input type="text" class="excel-textbox" maxlength="12" value="<%# Container.Value %>"
                                                            onfocus="markAsFocused(this)" onkeydown="AllowInt(this,event);" onkeypress="AllowInt(this,event);"
                                                            onblur="markAsBlured(this, '<%# GVInventory.Columns[Container.ColumnIndex].DataField %>', <%# Container.PageRecordIndex %>)" />
                                                    </Template>
                                                </obout:GridTemplate>
                                                <obout:GridTemplate ID="GridTemplateRightAlign">
                                                    <Template>
                                                        <span style="text-align: right; width: 130px; margin-right: 10px;">
                                                            <%# Container.Value  %></span>
                                                    </Template>
                                                </obout:GridTemplate>
                                            </Templates>
                                        </obout:Grid>
                                    </td>
                                </tr>
                            </table>
                        </center>
                    </ContentTemplate>
                </asp:TabPanel>

                <asp:TabPanel ID="TabBOM" runat="server" HeaderText="BOM">
                    <ContentTemplate>
                        <asp:UpdateProgress ID="UpdateProgressBOM" runat="server" AssociatedUpdatePanelID="Uppnl_BOMDetails">
                            <ProgressTemplate>
                                <center>
                                    <div class="modal">
                                        <img src="../App_Themes/Blue/img/ajax-loader.gif" style="top: 50%;" />
                                    </div>
                                </center>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        <asp:UpdatePanel ID="Uppnl_BOMDetails" runat="server">
                            <ContentTemplate>
                                <center>
                                    <table class="tableForm">
                                        <tr>
                                            <td>
                                                <req><asp:Label ID="lblbomsku" runat="server" Text="Select BOM SKU"/></req>
                                                :
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txtbomsku" Enabled="false" runat="server" MaxLength="50" Width="220px"></asp:TextBox>
                                                <img id="imgSearch" src="../App_Themes/Grid/img/search.jpg" title="Search SKU"
                                                    style="cursor: pointer;" onclick="openProductSearch('0')" />
                                            </td>
                                            <td>
                                                <req><asp:Label ID="lblquantity" runat="server" Text="Quantity"/></req>
                                                :
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txtQuantity" runat="server" MaxLength="50" Width="194px" onkeypress="return fnAllowDigitsOnly(event)" Style="text-align: right;"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <req><asp:Label ID="lblBomSequence" runat="server" Text="Sequance"/></req>
                                                :
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txtBOMSequence" runat="server" MaxLength="50" Width="194px" onkeypress="return fnAllowDigitsOnly(event)" Style="text-align: right;"></asp:TextBox>
                                            </td>

                                            <td>
                                                <asp:Label ID="lblrwmark" runat="server" Text="Remark" />
                                                :
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtremarkbom" runat="server" MaxLength="500" Width="511px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" align="right">
                                                <asp:Button ID="btnsubmit" runat="server" Text="Submit" OnClientClick="SaveBomDetail()" />
                                            </td>
                                        </tr>
                                    </table>
                                    <table class="gridFrame" width="70%">
                                        <tr>
                                            <td>
                                                <table style="width: 70%">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblbomskulist" CssClass="headerText" runat="server" Text="BOM SKU List" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <obout:Grid ID="grdaccessdele" runat="server" AllowFiltering="true" AllowGrouping="true"
                                                    AllowSorting="true" AllowColumnResizing="true" AutoGenerateColumns="false" Width="100%"
                                                    AllowAddingRecords="false" OnRebind="grdaccessdele_RebindGrid">
                                                    <Columns>
                                                        <%-- <obout:Column DataField="Id" HeaderText="Id" Visible="False" >
                                                   </obout:Column>--%>
                                                        <obout:Column DataField="Sequence" HeaderText="Remove" Width="5%" AllowSorting="false"
                                                            Align="Center" HeaderAlign="Center">
                                                            <TemplateSettings TemplateId="GvRemoveSku" />
                                                        </obout:Column>
                                                        <obout:Column DataField="Id" HeaderText="Edit" Width="5%" AllowSorting="false"
                                                            Align="Center" HeaderAlign="Center">
                                                            <TemplateSettings TemplateId="GvEditBOM" />
                                                        </obout:Column>
                                                        <obout:Column ID="Column4" HeaderText="SKU" DataField="ProductCode" Width="15%" runat="server" Align="Center" HeaderAlign="Center">
                                                        </obout:Column>
                                                        <obout:Column ID="Column7" HeaderText="SKU Name" DataField="Name" Width="15%" runat="server" Align="Center" HeaderAlign="Center">
                                                        </obout:Column>
                                                        <obout:Column ID="Column5" HeaderText="Quantity" DataField="Quantity" Width="15%" runat="server" Align="Center" HeaderAlign="Center">
                                                        </obout:Column>
                                                        <obout:Column ID="Column6" HeaderText="Remark" DataField="Remark" Width="15%" runat="server" Align="Center" HeaderAlign="Center">
                                                        </obout:Column>
                                                    </Columns>
                                                    <Templates>
                                                        <obout:GridTemplate ID="GvEditBOM" runat="server" ControlID="" ControlPropertyName="">
                                                            <Template>
                                                                <asp:ImageButton ID="imgBtnEdit1bom" runat="server" ImageUrl="../App_Themes/Blue/img/Edit16.png"
                                                                    OnClick="imgBtnEditbom_OnClick" ToolTip='<%# (Container.Value) %>' CausesValidation="false" />
                                                            </Template>
                                                        </obout:GridTemplate>
                                                        <obout:GridTemplate ID="GvRemoveSku" runat="server">
                                                            <Template>
                                                                <img id="imgbuttonremove" src="../App_Themes/Grid/img/Remove16x16.png" alt="Remove" title="Remove" onclick="RemoveSkuRecord('<%# (Container.DataItem["Id"].ToString()) %>');"
                                                                    style="cursor: pointer;" />
                                                                <%--RemoveSkuRecord--%>
                                                            </Template>
                                                        </obout:GridTemplate>
                                                    </Templates>
                                                </obout:Grid>
                                            </td>
                                        </tr>
                                    </table>
                                </center>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </ContentTemplate>
                </asp:TabPanel>

                <asp:TabPanel ID="Tabpack" runat="server" HeaderText="Pack">
                    <ContentTemplate>
                        <asp:UpdateProgress ID="UpdateProgressPack" runat="server" AssociatedUpdatePanelID="UpdatePack">
                            <ProgressTemplate>
                                <center>
                                    <div class="modal">
                                        <img src="../App_Themes/Blue/img/ajax-loader.gif" style="top: 50%;" />
                                    </div>
                                </center>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        <asp:UpdatePanel ID="UpdatePack" runat="server">
                            <ContentTemplate>
                                <center>
                                    <table class="gridFrame" width="100%">
                                        <tr>
                                            <td>
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td>
                                                            <a class="headerText"><asp:Label ID="lblpack" runat="server" Text="Pack"></asp:Label></a>
                                                        </td>
                                                        <td align="right">
                                                            <asp:Button ID="btnadd" runat="server" OnClientClick="openpackadd('0')" Text="  Add  " />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <obout:Grid ID="Grid2" runat="server" AllowFiltering="true" AllowGrouping="true"
                                                    AllowSorting="true" AllowColumnResizing="true" AutoGenerateColumns="false" Width="100%"
                                                    AllowAddingRecords="true" OnRebind="Grid2_RebindGrid">
                                                    <Columns>
                                                        <%-- <obout:Column HeaderText="Id" DataField="Id" Width="5%" runat="server" Visible="false" Index="1">
                                                        </obout:Column>--%>
                                                        <obout:Column DataField="Id" HeaderText="Edit" Width="5%" AllowSorting="false" Index="2"
                                                            Align="center" HeaderAlign="center">
                                                            <TemplateSettings TemplateId="GvTempEdit1" />
                                                        </obout:Column>
                                                        <%--<obout:Column ID="Column7" HeaderText="UOM" DataField="UOM" Width="7%" runat="server">
                                                        </obout:Column>--%>
                                                        <obout:Column HeaderText="Short Description" DataField="ShortDescri" Width="7%" runat="server" Index="3">
                                                        </obout:Column>
                                                        <obout:Column HeaderText="Description" DataField="Description" Width="10%" runat="server" Index="4">
                                                        </obout:Column>
                                                        <obout:Column HeaderText="Quantity" DataField="Quantity" Width="7%" runat="server" Index="5">
                                                        </obout:Column>
                                                        <obout:Column HeaderText="Sequence" DataField="Sequence" Width="7%" runat="server" Index="6">
                                                        </obout:Column>
                                                    </Columns>
                                                    <Templates>
                                                        <obout:GridTemplate ID="GvTempEdit1" runat="server">
                                                            <Template>
                                                                <img id="imgBtnEdit" src="../App_Themes/Blue/img/Edit16.png" title="Edit" onclick="openAddressWindow('<%# (Container.DataItem["Id"].ToString()) %>');"
                                                                    style="cursor: pointer;" />
                                                            </Template>
                                                        </obout:GridTemplate>
                                                    </Templates>
                                                </obout:Grid>
                                            </td>
                                        </tr>
                                    </table>
                                </center>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </ContentTemplate>
                </asp:TabPanel>

                <asp:TabPanel ID="tabVendor" runat="server" HeaderText="Vendor">
                    <ContentTemplate>
                        <center>
                            <table class="gridFrame" width="50%">
                                <tr>
                                    <td>
                                        <table style="width: 100%">
                                            <tr>
                                                <td>
                                                    <a style="color: white; font-size: 15px; font-weight: bold;">Vendor Details</a>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <obout:Grid ID="grvVendorDetails" runat="server" AllowAddingRecords="false" AllowRecordSelection="false"
                                            AutoGenerateColumns="false" AllowPaging="true" AllowFiltering="true" AllowGrouping="true"
                                            AllowSorting="true">
                                            <Columns>
                                                <obout:Column HeaderText="product ID" DataField="productID" Visible="false">
                                                </obout:Column>
                                                <obout:Column HeaderText="Vendor ID" DataField="vendorID" Visible="false">
                                                </obout:Column>
                                                <obout:Column HeaderText="Name" DataField="VendorName" Width="150px">
                                                </obout:Column>
                                                <obout:Column HeaderText="Address" DataField="VendorAddress" Width="150px">
                                                </obout:Column>
                                                <obout:Column HeaderText="Contact No" DataField="VendorContactNo" Width="140px">
                                                </obout:Column>
                                                <obout:Column HeaderText="Email ID" DataField="VendorEmailID" Width="140px">
                                                </obout:Column>
                                                <obout:Column HeaderText="Min. Order Qty" DataField="MinOrderQty" Width="160px">
                                                </obout:Column>
                                                <obout:Column HeaderText="Lead Time" DataField="LeadTime" Width="80px">
                                                </obout:Column>
                                                <obout:Column HeaderText="Active" DataField="VendorActive" Width="100px">
                                                </obout:Column>
                                            </Columns>
                                        </obout:Grid>
                                    </td>
                                </tr>
                            </table>
                        </center>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>
    <style type="text/css">
        .remove_a {
            color: Red;
            font-size: 14px;
            font-weight: bold;
        }

            .remove_a:hover {
                color: Red;
                font-size: 15px;
                font-weight: bold;
                cursor: pointer;
            }
    </style>
    <style type="text/css">
        /*Grid css*/
        .excel-textbox {
            background-color: transparent;
            border: 0px;
            padding: 0px;
            outline: 0;
            font: inherit;
            width: 91%;
            padding-top: 4px;
            padding-right: 2px;
            padding-bottom: 4px;
            text-align: right;
        }

        .excel-textbox-focused {
            background-color: #FFFFFF;
            border: 0px;
            padding: 0px;
            outline: 0;
            font: inherit;
            width: 91%;
            padding-top: 4px;
            padding-right: 2px;
            padding-bottom: 4px;
            text-align: right;
        }

        .excel-textbox-error {
            color: #FF0000;
        }

        .ob_gCc2 {
            padding-left: 3px !important;
        }

        .ob_gBCont {
            border-bottom: 1px solid #C3C9CE;
        }

        .excel-checkbox {
            height: 20px;
            line-height: 20px;
        }
    </style>

    <asp:HiddenField ID="hdnbomeditstate" runat="server" />
    <asp:HiddenField ID="hdnPartSelectedRec" runat="server" ClientIDMode="Static" />

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
                SelectedPrdRec();
            }
        }

          
        function SelectedPrdRec() {   
            var hdnPartSelectedRec = document.getElementById("hdnPartSelectedRec");
           hdnPartSelectedRec.value = "";
            for (var i = 0; i < grvProduct.PageSelectedRecords.length; i++) {
                var record = grvProduct.PageSelectedRecords[i];
                if (hdnPartSelectedRec.value != "") hdnPartSelectedRec.value += ',' + record.ID;
                if (hdnPartSelectedRec.value == "") hdnPartSelectedRec.value = record.ID;
            }
        }

        var ddlcompany = document.getElementById("<%=ddlcompany.ClientID %>");
        var ddldeptid = document.getElementById("<%=ddldepartment.ClientID %>");
        var hdncompanyid = document.getElementById("<%=hdncompanyid.ClientID %>");
        var hdnstate = document.getElementById("<%=hdnstate.ClientID %>");
        var hdnproductsearchId = document.getElementById("<%=hdnproductsearchId.ClientID %>");
        var Quantity = document.getElementById("<%=txtQuantity.ClientID %>");
        var BomRemark = document.getElementById("<%=txtremarkbom.ClientID %>");
        var TxtProduct = document.getElementById("<%=txtbomsku.ClientID %>");
        var hdnBomDetailId = document.getElementById("<%=hdnBomDetailId.ClientID %>");

        function GetDepartment() {
            var obj1 = new Object();
            obj1.ddlcompanyId = ddlcompany.value.toString();
            document.getElementById("<%=hdncompanyid.ClientID %>").value = ddlcompany.value.toString();
            PageMethods.GetDepartment(obj1, getLoc_onSuccessed);
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

        function Getdeptid() {
            document.getElementById("<%=hdndeptid.ClientID %>").value = ddldeptid.value.toString();
            var DepartmentId = ddldeptid.value.toString();
            var CompanyId = ddlcompany.value.toString();
            var skucode = document.getElementById("<%=txtProductCode.ClientID %>").value
            var OMSskuCode = skucode.concat("-", CompanyId, "-", DepartmentId);
            document.getElementById("<%=txtomsskucode.ClientID %>").value = OMSskuCode;
        }

        function openAddressWindow(Id) {
            var hdnAddressTargetObject = document.getElementById("hdnAddressTargetObject");
            //window.open('../Address/AddressInfo.aspx?Sequence=' + sequence + '&TargetObject=' + hdnAddressTargetObject.value + '', null, 'height=280, width=1000,status= 0, resizable= 1, scrollbars=0, toolbar=0,location=center,menubar=0, screenX=300; screenY=50');
            window.open('../Product/AddPacks.aspx?Id=' + Id + "", null, 'height=150px, width=850px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=300; screenY=50');
        }

        function openSpecificWindow(Id) {
            var hdnAddressTargetObject = document.getElementById("hdnAddressTargetObject");
            //window.open('../Address/AddressInfo.aspx?Sequence=' + sequence + '&TargetObject=' + hdnAddressTargetObject.value + '', null, 'height=280, width=1000,status= 0, resizable= 1, scrollbars=0, toolbar=0,location=center,menubar=0, screenX=300; screenY=50');
            window.open('../Product/UDF.aspx?Id=' + Id + "", null, 'height=150px, width=850px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=300; screenY=50');
        }


        function AfterProductSelected(Code, skuid) {
            var hdnval = document.getElementById("hdnProductSearchSelectedRec");
            var searchdkuid = document.getElementById("hdnproductsearchId");
            hdnval.value = Code;
            searchdkuid.value = skuid;
            var TxtProduct = document.getElementById("<%=txtbomsku.ClientID %>");
            TxtProduct.value = Code;
        }


        function SaveBomDetail() {
            var hdnbomeditstate = document.getElementById("<%=hdnbomeditstate.ClientID %>");
            var hdnstate = document.getElementById("<%=hdnstate.ClientID %>");
            var hdnproductsearchId = document.getElementById("<%=hdnproductsearchId.ClientID %>");
            var Quantity = document.getElementById("<%=txtQuantity.ClientID %>");
            var BomRemark = document.getElementById("<%=txtremarkbom.ClientID %>");
            var TxtProduct = document.getElementById("<%=txtbomsku.ClientID %>");
            var hdnBomDetailId = document.getElementById("<%=hdnBomDetailId.ClientID %>");
            var txtBOMSequence = document.getElementById("<%=txtBOMSequence.ClientID %>");
            var obj1 = new Object();
            obj1.hdnbomeditstate = hdnbomeditstate.value.toString();
            obj1.hdnproductsearchId = hdnproductsearchId.value.toString();
            obj1.BomDetailId = hdnBomDetailId.value;
            obj1.Quantity = Quantity.value.toString();
            obj1.BomRemark = BomRemark.value.toString();
            obj1.TxtProduct = TxtProduct.value.toString();
            obj1.hdnstate = hdnstate.value.toString();
            obj1.Sequence = txtBOMSequence.value.toString();
            PageMethods.SaveBomDetail(obj1, getSave_onSuccessed);
        }

        function getSave_onSuccessed(result) {
            grdaccessdele.refresh();
            //            Quantity.value = "";
            //            TxtProduct.value = "";
            //            BomRemark.value = "";
            //            hdnproductsearchId.value = "";
            document.getElementById("<%=txtQuantity.ClientID %>").value = "";
            document.getElementById("<%=txtremarkbom.ClientID %>").value = "";
            document.getElementById("<%=txtbomsku.ClientID %>").value = "";
            document.getElementById("<%=hdnproductsearchId.ClientID %>").value = "";
            document.getElementById("<%=txtBOMSequence.ClientID %>").value = "";
        }

        function fnAllowDigitsOnly(key) {
            var keycode = (key.which) ? key.which : key.keyCode;
            if ((keycode < 48 || keycode > 57) && (keycode != 8) && (keycode != 9) && (keycode != 11) && (keycode != 37) && (keycode != 38) && (keycode != 39) && (keycode != 40) && (keycode != 127)) {
                return false;
            }
        }


         function Updateserialflag() {           
           var r = confirm("Are You Sure to Update SKU Serial Flag?")
             if (r == true) {
                 var SKUIds = document.getElementById("hdnPartSelectedRec").value;
                   PageMethods.Updateserialflag(SKUIds, OnSuccessupdatesrflag, null);
             }            
        }

        function OnSuccessupdatesrflag(result) {
            if (result == "Success") {
                showAlert("Serial flag updated successfully.", "info", "#");
            }
            else {
                showAlert("Error occured.", "Error", "#");
            }
        }

    </script>

    <script type="text/javascript">
        function RemoveSkuRecord(Id) {
            var obj1 = new Object();
            var Detailid = Id;
            obj1.SkuDetailId = Detailid;
            PageMethods.RemoveSku(obj1, Removesku_onSuccess);
        }

        function Removesku_onSuccess(result) {
            grdaccessdele.refresh();
        }

        function fnAllowDigitsOnly(key) {
            var keycode = (key.which) ? key.which : key.keyCode;
            if ((keycode < 48 || keycode > 57) && (keycode != 8) && (keycode != 9) && (keycode != 11) && (keycode != 37) && (keycode != 38) && (keycode != 39) && (keycode != 40) && (keycode != 127)) {
                return false;
            }
        }

    </script>

    <script type="text/javascript">
        function openProductSearch(sequence) {
            var deptid = document.getElementById("<%=hdndeptid.ClientID %>").value
             window.open('../Product/SearchProduct.aspx?deptid=' + deptid + "", null, 'height=700px, width=825px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=300; screenY=50');
         }

         function openpackadd(sequence) {
             var Ids = "0";
             window.open('../Product/AddPacks.aspx?Id=' + Ids + "", null, 'height=150px, width=850px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=300; screenY=50');
         }

         function openvirtualQty() {
             var skuid = document.getElementById("<%= hdnprodID.ClientID %>").value;
             var Ids = "0";
             window.open('../Product/VirtualQty.aspx?skuid=' + skuid +"", null, 'height=150px, width=800px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=300; screenY=50');
         }

        function ShowHideMOQ()
        {
            var GroupSetValue = document.getElementById("<%= ddlBom.ClientID %>").value;
            if (GroupSetValue == 1) {
                document.getElementById("<%= ddlBom.ClientID %>").disabled = true;
            }
            else
            {
                document.getElementById("<%= ddlBom.ClientID %>").disabled = false;
            }
        }



    </script>
    <script type="text/javascript">

        function ProductMasterTaxSetup(invoker) {
            var getParent = invoker.parentElement;
            PageMethods.TempSaveTaxSetup(invoker.checked, getParent.title, null, null);
        }

        function checkDiscount() {
            if (txtDiscount.value != "") {
                if (checkbox.checked == true) {
                    if (parseFloat(txtDiscount.value) > 100.00) {
                        txtDiscount.value = "";
                        alert("Enter valid Discount in Percent(%)");
                    }
                }
            }
            if (parseFloat(txtDiscount.value) > parseFloat(txtPrice.value)) {
                if (checkbox.checked == false)
                { alert("Discount Price can not be greater than Principal Price"); txtDiscount.value = ""; }
            }
        }
    </script>
    <script type="text/javascript">

        function downloadClick() {
            var chk = eval(sender); if (chk.checked(true))
                alert("Checked True");
        }

        function onBeforeClientInsert() { return validate(); }

        function onBeforeClientUpdate() { return validate(); }

        function validate() {
            if (!Page_ClientValidate()) {
                alert('Error: Mandantory fields required');
                return false;
            }
            return true;
        }


        function printProductSubCategory() {

            PageMethods.PMprint_ProductSubCategory(document.getElementById("ddlCategory").value, onSuccessPMprint_ProductSubCategory, null)
        }


        function onSuccessPMprint_ProductSubCategory(result) {
            var ddlSubCategory = document.getElementById("ddlSubCategory")
            ddlSubCategory.options.length = 0;
            var option0 = document.createElement("option");


            if (result.length > 0) {
                option0.text = '-Select- ' + document.getElementById("ddlSubCategory").innerHTML;
                option0.value = "0"
            }
            else { option0.text = 'N/A'; option0.value = "0" }


            try {
                ddlSubCategory.add(option0, null); //Standard 
            } catch (error) {
                ddlSubCategory.add(option0); // IE only
            }


            for (var i = 0; i < result.length; i++) {
                var option1 = document.createElement("option");

                option1.text = result[i].ProductSubCategory;
                option1.value = result[i].ID;

                try {
                    ddlSubCategory.add(option1, null); //Standard 
                } catch (error) {
                    ddlSubCategory.add(option1); // IE only
                }
            }
            if (ddlSubCategory != "") {
                if (ddlSubCategory.length > 0) { ddlSubCategory.value = ddlSubCategory; ddlSubCategory = ""; }
            }
            LoadingOff();
        }

    </script>
    <%--Inventory Code--%>
    <script type="text/javascript">
        /*Issue Part List*/
        function markAsFocused(textbox) {
            textbox.className = 'excel-textbox-focused';
            textbox.select();
        }
        var RowIndex = 0;
        function markAsBlured(textbox, dataField, rowIndex) {
            textbox.className = 'excel-textbox';
            RowIndex = rowIndex;
            var txtvalue = textbox.value;
            if (txtvalue == "") txtvalue = 0;
            textbox.value = parseInt(txtvalue);
            if (GVInventory.Rows[rowIndex].Cells[dataField].Value != textbox.value) {
                GVInventory.Rows[rowIndex].Cells[dataField].Value = textbox.value;
                getOrderObject(rowIndex);
            }
        }

        function getOrderObject(rowIndex) {
            var order = new Object();
            order.SiteID = GVInventory.Rows[rowIndex].Cells['SiteID'].Value;
            order.OpeningStock = parseInt(GVInventory.Rows[rowIndex].Cells['OpeningStock'].Value);
            order.MaxStockLimit = parseInt(GVInventory.Rows[rowIndex].Cells['MaxStockLimit'].Value);
            order.ReorderQty = parseInt(GVInventory.Rows[rowIndex].Cells['ReorderQty'].Value);
            PageMethods.WMUpdateInventoryQty(order.SiteID, order.OpeningStock, order.MaxStockLimit, order.ReorderQty, WMUpdateInventoryQtyOnSuccess, null);

        }
        function WMUpdateInventoryQtyOnSuccess(result) {
            GVInventory.Rows[RowIndex].Cells["AvailableBalance"].Value = result;
            var body = GVInventory.GridBodyContainer.firstChild.firstChild.childNodes[1];
            var cell1 = body.childNodes[RowIndex].childNodes[5];
            cell1.firstChild.lastChild.innerHTML = result;
        }


        function CancelSelectedOrder() {
           
                var r = confirm("Are You Sure to Update SKU Serial Flag?")
                if (r == true) {
                  // PageMethods.WMCancelOrder(SelectedOrder.value, OnSuccessCancelOrder, null);
                }
            
        }

    </script>
    <%--End Inventory Code--%>
</asp:Content>
