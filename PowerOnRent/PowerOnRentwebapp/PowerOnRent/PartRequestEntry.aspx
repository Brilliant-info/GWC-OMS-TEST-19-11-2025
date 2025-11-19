<%@ Page Title="GWC | Request" Language="C#" MasterPageFile="~/MasterPage/CRM.Master"
    AutoEventWireup="true" CodeBehind="PartRequestEntry.aspx.cs" Inherits="PowerOnRentwebapp.PowerOnRent.PartRequestEntry"
    EnableEventValidation="false" %>

<%@ Register Assembly="obout_Flyout2_NET" Namespace="OboutInc.Flyout2" TagPrefix="obout" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="obout" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>
<%@ Register Assembly="Obout.Ajax.UI" Namespace="Obout.Ajax.UI.HTMLEditor" TagPrefix="obout" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="obout" %>
<%@ Register Src="../CommonControls/UC_Date.ascx" TagName="UC_Date" TagPrefix="uc1" %>
<%@ Register Src="../Product/UCProductSearch.ascx" TagName="UCProductSearch" TagPrefix="uc1" %>
<%@ Register Src="../CommonControls/Toolbar.ascx" TagName="Toolbar" TagPrefix="uc2" %>
<%@ Register Src="../CommonControls/UCFormHeader.ascx" TagName="UCFormHeader" TagPrefix="uc2" %>
<%@ Register Src="../Document/UC_AttachDocument.ascx" TagName="UC_AttachDocument" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHolder_FormHeader" runat="server">

    <uc2:Toolbar ID="Toolbar1" runat="server" />
    <uc2:UCFormHeader ID="UCFormHeader1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHolder_Form" runat="server">
    <script>
        function openBomDetails(bomProdId, isgroup) {
            if (isgroup == "Yes") {
                var myBomWin = window.open('bomDetails.aspx?id=' + bomProdId, null, 'height=450px, width=825px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=300; screenY=50'); //'directories=no,titlebar=no,toolbar=no,location=no,status=no,menubar=no,scrollbars=no,resizable=no,width=900,height=650');                       
            }
        }

    </script>
    <style>
        .bomImgStyleYes img {
            opacity: 1;
        }

        .bomImgStyleNo img {
            opacity: 0.2;
        }

        .bomImgStyleNA img {
            opacity: 0.2;
        }

        .bomImgStyleNo a {
            cursor: default !important;
        }

        .bomImgStyleNA a {
            cursor: default !important;
        }

        .bomImgStyleYes a {
            cursor: pointer;
        }



        .disableCylender a,
        .disableCylender img {
            display: none !important;
        }

        .active {
            display: none;
        }

        .removeHeaderPadding {
            padding-left: 0px;
        }
    </style>
    <style id="styleHideSerialColumn" runat="server">
        .hideHeaderGridColumn {
            display: none !important;
        }
    </style>
    <center>
        <%-- <div id = "myDiv" style="visibility:hidden;"><img id = "myImage" src = "../App_Themes/Blue/img/ajax-loader.gif"></div>--%>
        <%--<div class="glow-alert" style="display: none;"></div>--%>
        <span id="imgProcessing" style="display: none;">Please wait... </span>
        <div id="divLoading" style="height: 110%; width: 100%; display: none; top: 0; left: 0;"
            class="modal" runat="server" clientidmode="Static">
            <center>
            </center>
        </div>


        <div class="divHead" id="divRequestHead" runat="server" clientidmode="Static">
            <h4>
                <asp:Label ID="lblrequest" runat="server" Text="Request"></asp:Label>
            </h4>
            <a onclick="javascript:divcollapsOpen('divRequestDetail',this)" id="linkRequest"
                runat="server">Collapse</a>
        </div>
        <div class="divDetailExpand" id="divRequestDetail" runat="server" clientidmode="Static">
            <div id="OrderHead" runat="server" clientidmode="Static">
                <table class="tableForm">

                    <tr runat="server" id="divtemplate" clientidmode="Static">
                        <td colspan="6">
                            <center>
                                <table>
                                    <tr>
                                        <td style="text-align: right;">
                                            <asp:Button ID="btnGenerateFromTemplate" runat="server" Text="Generate From Template" Style="cursor: pointer;" OnClientClick="OpenTelplateList();" />
                                            <asp:HiddenField ID="hdnSelTemplateID" runat="server" ClientIDMode="Static" ViewStateMode="Enabled" />
                                        </td>
                                        <td style="text-align: right;">
                                            <asp:Label ID="lblTemplateTitleNew" runat="server" Text="Template Title"></asp:Label>
                                            :
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:TextBox ID="txtTemplateTitleNew" runat="server" Width="180px">
                                            </asp:TextBox>
                                        </td>
                                        <td style="text-align: right;">
                                            <asp:Label ID="lblAccessTypeNew" runat="server" Text="Access Type"></asp:Label>
                                            :
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:DropDownList ID="ddlAccessTypeNew" runat="server" Width="182px">
                                                <asp:ListItem Selected="True" Text="-Select-" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Public" Value="Public"></asp:ListItem>
                                                <asp:ListItem Text="Private" Value="Private"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: right;">
                                            <asp:Button ID="btnSaveAsTemplateNew" runat="server" Text="Save As Template" Style="cursor: pointer;" OnClientClick="SubmitTemplate()" />
                                        </td>
                                    </tr>
                                </table>
                            </center>
                        </td>
                    </tr>
                    <tr runat="server" id="divborder" clientidmode="Static">
                        <td colspan="6" style="text-align: left;">
                            <hr style="border-color: #f5deb3; border-width: 1px;" />
                        </td>
                    </tr>
                    <tr runat="server" id="divtitle" clientidmode="Static">
                        <td style="text-align: right;">
                            <req><asp:Label ID="lblTitle" runat="server" Text="Title*"></asp:Label></req>
                            :
                        </td>
                        <td colspan="5" style="text-align: left;">
                            <asp:TextBox runat="server" ID="txtTitle" Width="99%" MaxLength="100" AccessKey="1">
                            </asp:TextBox>

                            <asp:Button ID="btnchangestatus" Visible="false" runat="server" Text="Change Status" Style="cursor: pointer;" OnClientClick="ChangeStatus()" />

                            <%--99--%>
                        </td>
                    </tr>
                    <tr runat="server" id="divcustorderno" clientidmode="Static">
                        <td style="text-align: right;">
                            <asp:Label ID="lblCustomerOrderRefNo" runat="server" Text="Customer Order Ref. No."></asp:Label>
                            :
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox runat="server" ID="txtCustOrderRefNo" Width="180px"></asp:TextBox>
                            <%--AccessKey="1"--%>
                        </td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblRequestNumber" runat="server" Text="Request No."></asp:Label>
                            :
                        </td>
                        <td style="text-align: left; font-weight: bold;">
                            <%--<asp:TextBox runat="server" ID="lblRequestNo" Width="176px" AccessKey="1"></asp:TextBox>--%>
                            <asp:Label runat="server" ID="lblRequestNo" Width="176px" AccessKey="1"></asp:Label>
                        </td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblStatus" runat="server" Text="Status"></asp:Label>
                            :
                        </td>
                        <td style="text-align: left;">
                            <asp:DropDownList runat="server" ID="ddlStatus" Width="182px" AccessKey="1" DataTextField="Status"
                                DataValueField="ID">
                            </asp:DropDownList><%--Enabled="false"--%>

                            <%--   <img id="imgchnagestatus" src="../App_Themes/Grid/img/search.jpg" title="Change Status" alt="" runat="server"
                                 onclick="ChangeStatus()" />--%>

                        </td>
                    </tr>
                    <tr runat="server" id="divcustname" clientidmode="Static">
                        <td style="text-align: right;">
                            <asp:Label ID="lblCustomerName" runat="server" Text="Customer Name"></asp:Label>
                            :
                        </td>
                        <td style="text-align: left;">
                            <asp:DropDownList runat="server" ID="ddlCompany" Width="182px" DataTextField="Name"
                                DataValueField="ID" ClientIDMode="Static" onchange="GetDept(this);">
                            </asp:DropDownList>
                        </td>



                        <td style="text-align: right;">
                            <asp:Label ID="lblDepartment" runat="server" Text="Department"></asp:Label>
                            :
                        </td>
                        <td style="text-align: left;">

                            <asp:DropDownList runat="server" ID="ddlSites" Width="182px" DataTextField="Territory"
                                DataValueField="ID" ClientIDMode="Static" onchange="GetContact1(this);GetAddress(this);GetDeptID(this);CheckParts();PaymentMethod();">
                            </asp:DropDownList>
                        </td>

                        <td style="text-align: right;">
                            <asp:Label ID="lblRequestedBy" runat="server" Text="Requested By"></asp:Label>
                            :
                        </td>
                        <td style="text-align: left;">
                            <asp:DropDownList runat="server" ID="ddlRequestByUserID" Width="182px" AccessKey="1" onchange="GetRequestor()"
                                DataTextField="userName" DataValueField="userID">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr runat="server" id="divreqtdate" clientidmode="Static">

                        <td style="text-align: right;">
                            <asp:Label ID="lblRequestDate" runat="server" Text="Request Date"></asp:Label>
                            :
                        </td>
                        <td style="text-align: left;" class="disableCylender">
                            <asp:TextBox ID="txtRequestDate" runat="server" Enabled="false" AccessKey="1" Width="82px"></asp:TextBox>
                            <uc1:UC_Date ID="UC_DateRequestDate" runat="server" AccessKey="1" Visible="false" />
                        </td>

                        <td style="text-align: right;">
                            <req><asp:Label ID="lblExpDeliveryDate" runat="server" Text="Exp. Delivery Date*"></asp:Label></req>
                            :
                        </td>
                        <td style="text-align: left;">
                            <asp:UpdatePanel ID="UpdExpDDate" runat="server">
                                <ContentTemplate>
                                    <uc1:UC_Date ID="UC_ExpDeliveryDate" runat="server" AccessKey="1" OnLoad="UC_ExpDeliveryDate_OnLoad" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>


                        <td style="text-align: right;">
                            <req><asp:Label ID="lblContact1" runat="server" Text="Contact 1*"></asp:Label></req>
                            :
                        </td>
                        <td style="text-align: left;">
                            <asp:DropDownList runat="server" ID="ddlContact1" Width="0px" DataTextField="Name"
                                DataValueField="ID" ClientIDMode="Static" onchange="GetContact2(this);" Visible="false">
                                <%--AccessKey="1"--%>
                            </asp:DropDownList>
                            <asp:TextBox ID="txtContact1" runat="server" Width="182px" Enabled="false" AccessKey="1"></asp:TextBox>
                            <img id="imgSearch" src="../App_Themes/Grid/img/search.jpg" title="Search SKU"
                                style="cursor: pointer;" onclick="openContactSearch('0')" />
                        </td>
                    </tr>
                    <tr runat="server" id="divcontact2" clientidmode="Static">
                        <td style="text-align: right;">
                            <asp:Label ID="lblContact2" runat="server" Text="Contact 2"></asp:Label>
                            :
                        </td>
                        <td style="text-align: left;">
                            <asp:DropDownList runat="server" ID="ddlContact2" Width="0px" DataTextField="Name"
                                DataValueField="ID" onchange="ddl2Selvalue(this);" Visible="false">
                            </asp:DropDownList>
                            <asp:TextBox ID="txtContact2" runat="server" Width="178px" Enabled="false"></asp:TextBox>
                            <img id="img1" src="../App_Themes/Grid/img/search.jpg" title="Search SKU"
                                style="cursor: pointer;" onclick="openContactSearch2('0')" />
                            <%--AccessKey="1"--%>
                        </td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblLocationID" runat="server" Text="Location ID"></asp:Label>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="txtLocation" runat="server" Width="182px" Enabled="false"></asp:TextBox>
                            <img id="img3" src="../App_Themes/Grid/img/search.jpg" title="Search Location" style="cursor: pointer;" onclick="openLocationSearch('0')" />
                        </td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblCustomerAddress" runat="server" Text="Delivery Address"></asp:Label>
                            :
                        </td>
                        <td style="text-align: left;">
                            <asp:DropDownList runat="server" ID="ddlAddress" Width="0px" DataTextField="AddressLine1"
                                DataValueField="ID" onchange="PrintAddress();" Visible="false">
                            </asp:DropDownList>
                            <asp:TextBox ID="txtAddress" runat="server" Width="182px" Enabled="false"></asp:TextBox>
                            <img id="img2" src="../App_Themes/Grid/img/search.jpg" title="Search Address"
                                style="cursor: pointer;" onclick="openAddressSearch('0')" />
                        </td>
                    </tr>
                    <tr runat="server" id="divlocn" clientidmode="Static">
                        <td style="text-align: right">
                            <asp:Label ID="lblLocationLabel" runat="server" Text="Location Details"></asp:Label>
                            :
                        </td>
                        <td colspan="2" style="text-align: left;">
                            <div style="width: 100%; height: 10px;">
                                <asp:Label ID="lblLocation" runat="server"></asp:Label>
                            </div>
                        </td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblAddressLabel" runat="server" Text="Address Details"></asp:Label>
                            :
                        </td>
                        <td colspan="2" style="text-align: left;">
                            <div style="width: 100%; height: 10px;">
                                <asp:Label ID="lblAddress" runat="server"></asp:Label>
                            </div>

                        </td>
                    </tr>

                    <tr runat="server" id="divsegmenttype" class="active" clientidmode="Static">
                        
                        <td style="text-align: right;">
                            <asp:Label ID="lblsegment" runat="server" Text="Enterprise Segment"></asp:Label>
                            :
                        </td>
                         <td style="text-align: left;">
                            <asp:TextBox ID="txtsengmenttype" runat="server" Width="182px" Enabled="false"></asp:TextBox>
                            <img id="img2" src="../App_Themes/Grid/img/search.jpg" title="Search Segment Type"
                                style="cursor: pointer;" onclick="openSegmentType('0')" />
                        </td>
                            
                    </tr>

                    <tr runat="server" id="divprojectsite" class="active" clientidmode="Static">


                        <td style="text-align: right;">
                            <asp:Label ID="lblprojecttype" runat="server" Text="Project Type"></asp:Label>
                            :
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtprojectype" runat="server" Width="182px" Enabled="false"></asp:TextBox>
                            <img id="img2" src="../App_Themes/Grid/img/search.jpg" title="Search Project Type"
                                style="cursor: pointer;" onclick="openProjectType('0')" />
                        </td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblsitecode" runat="server" Text="Site Code"></asp:Label>
                            :
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtsitecode" runat="server" Width="182px" Enabled="false"></asp:TextBox>
                            <img id="img2" src="../App_Themes/Grid/img/search.jpg" title="Search Site Code"
                                style="cursor: pointer;" onclick="openSiteMaster('0')" />
                        </td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblsitenm" runat="server" Text="Site Name"></asp:Label>
                            :
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtsitenm" runat="server" Width="182px" Enabled="false"></asp:TextBox>
                        </td>

                    </tr>
                    <tr runat="server" id="divlatitude" class="active" clientidmode="Static">
                        <td style="text-align: right;">
                            <asp:Label ID="lblLatitude" runat="server" Text="Latitude"></asp:Label>
                            :
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtLatitude" runat="server" Width="182px" Enabled="false"></asp:TextBox>

                        </td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblLongitude" runat="server" Text="Longitude"></asp:Label>
                            :
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtLongitude" runat="server" Width="182px" Enabled="false"></asp:TextBox>

                        </td>
                        <td style="text-align: right;">
                            <asp:Label ID="lblAccessRequirement" runat="server" Text="Access Requirement"></asp:Label>
                            :
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtAccessRequirement" runat="server" Width="182px" Enabled="false"></asp:TextBox>
                        </td>

                    </tr>



                    <tr runat="server" id="divremark" clientidmode="Static">
                        <td style="text-align: right;">
                            <asp:Label ID="lblRemark" runat="server" Text="Remark"></asp:Label>
                            :
                        </td>
                        <td colspan="5" style="text-align: left;">
                            <asp:TextBox runat="server" ID="txtRemark" Width="99%" onkeyup="Chkxmltag(this);" TextMode="MultiLine" Rows="1">
                            </asp:TextBox>
                            <%--AccessKey="1"--%>
                        </td>
                    </tr>
                    <tr runat="server" id="divpaymentm" clientidmode="Static">
                        <td style="text-align: right;">
                            <asp:Label ID="lblPaymentMethod" runat="server" Text="Payment Method"></asp:Label>
                            :
                        </td>
                        <td style="text-align: left;">
                            <asp:DropDownList ID="ddlPaymentMethod" runat="server" Width="182px" DataTextField="MethodName" onchange="divPMChng(); GetPaymentMethodID(); "
                                DataValueField="PMethodID" ClientIDMode="Static">
                            </asp:DropDownList>
                        </td>
                        <td colspan="4" style="text-align: left;">
                            <div id="dvLst" class="active">
                                <asp:UpdatePanel ID="UpdLst" runat="server">
                                    <ContentTemplate>
                                        <asp:ListView ID="LstStatutoryInfo" runat="server" GroupItemCount="3" Style="margin-bottom: 0px; width: 1000px" OnLoad="LstStatutoryInfo_OnLoad">
                                            <GroupTemplate>
                                                <tr id="itemPlaceholderContainer" runat="server">
                                                    <td id="itemPlaceholder" runat="server"></td>
                                                </tr>
                                            </GroupTemplate>
                                            <ItemTemplate>
                                                <center>
                                                    <td id="Td1" runat="server" style="">
                                                        <table width="100%">
                                                            <tr>
                                                                <td style="text-align: right;"><%--width: 150px;--%>
                                                                    <div class="lstLbl">
                                                                        <asp:Label ToolTip='<%# Eval("ID") %>' ID="lblname" runat="server" Text='<%# Eval("FieldName") %>' />
                                                                    </div>
                                                                </td>
                                                                <td style="text-align: left;" align="left">
                                                                    <div class="lstText">
                                                                        <asp:TextBox ID="textboxPM" CssClass="tbox" MaxLength="50" runat="server" Width="180px" Text='<%# Bind("StatutoryValue") %>'></asp:TextBox>
                                                                    </div>
                                                                    <%--Text='<%# Bind("StatutoryValue") %>'--%>                                                                                                      
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </center>
                                            </ItemTemplate>
                                            <LayoutTemplate>

                                                <table id="groupPlaceholderContainer" runat="server" width="100%" cellpadding="0"
                                                    cellspacing="0">
                                                    <%--class="tableForm"--%>
                                                    <tr id="groupPlaceholder" runat="server">
                                                    </tr>
                                                </table>
                                            </LayoutTemplate>
                                        </asp:ListView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div id="dvFOC" class="active" style="margin-left: 35px;">
                                <asp:Label ID="lblFOC" runat="server" Text="Charge To" />
                                :
                        <asp:DropDownList ID="ddlFOC" runat="server" Width="180px" DataValueField="ID" DataTextField="CenterName" ClientIDMode="Static"></asp:DropDownList>
                            </div>
                        </td>
                    </tr>


                </table>

            </div>
            <div id="OrderDetail" runat="server" clientidmode="Static">
                <table class="gridFrame" width="100%" id="tblCart">
                    <tr>
                        <td>
                            <table style="width: 100%">
                                <tr>
                                    <td style="text-align: left;">
                                        <asp:Label ID="lblrequestpartlist" CssClass="headerText" runat="server" Text="Request Part List"></asp:Label>
                                    </td>
                                    <td style="text-align: right;">
                                        <uc1:UCProductSearch ID="UCProductSearch1" runat="server" />

                                        <asp:Button ID="btnNewPrduct" runat="server" Text="Add New Product" Visible="false" />

                                        <asp:HiddenField ID="hdnprodID" runat="server" />

                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <obout:Grid ID="Grid1" runat="server" CallbackMode="true" Serialize="true" AllowColumnReordering="true"
                                AllowColumnResizing="true" AutoGenerateColumns="false" AllowPaging="false" ShowLoadingMessage="true"
                                AllowSorting="false" AllowManualPaging="false" AllowRecordSelection="false" ShowFooter="false"
                                Width="100%" PageSize="-1" OnRebind="Grid1_OnRebind" OnRowDataBound="Grid1_OnRowDataBound">
                                <%--RowEditTemplateId="Grid1_RowCommand"--%>
                                <ClientSideEvents ExposeSender="true" />
                                <Columns>
                                    <obout:Column DataField="Prod_ID" Visible="false">
                                    </obout:Column>
                                    <obout:Column DataField="Sequence" HeaderText="Sr.No." AllowEdit="false" Width="4%"
                                        Align="left" HeaderAlign="left">
                                        <TemplateSettings TemplateId="ItemTempRemove" />
                                    </obout:Column>
                                    <obout:Column DataField="Prod_Code" HeaderAlign="center" Align="center" HeaderText="SKU Code" Wrap="true" AllowEdit="false" Width="8%">
                                    </obout:Column>
                                    <obout:Column DataField="Prod_Name" Align="center" HeaderText="Product Name" Wrap="true" AllowEdit="false" Width="10%"
                                        HeaderAlign="center">
                                    </obout:Column>
                                    <obout:Column DataField="Prod_Description" Align="left" HeaderText="Description" Wrap="true" AllowEdit="false"
                                        Width="0%" HeaderAlign="left" Visible="false" >
                                    </obout:Column>
                                    <obout:Column DataField="moq" HeaderText="MOQ" AllowEdit="false" Width="3%" HeaderAlign="left" Align="center">
                                        <TemplateSettings TemplateId="PrdMOQ" />
                                    </obout:Column>
                                    <%--5--%>
                                    <obout:Column DataField="GroupSet" HeaderText="GroupSet" AllowEdit="false" Width="0%"
                                        HeaderAlign="left" Align="left" Visible="false">
                                    </obout:Column>
                                    <%--6 new--%>
                                    <%--5--%>
                                    <obout:Column DataField="CurrentStock" HeaderText="Current Stock" Wrap="true" AllowEdit="false"
                                        Width="5%" HeaderAlign="left" Align="left">
                                        <TemplateSettings TemplateId="GridTemplateRightAlign" />
                                    </obout:Column>
                                    <%--7 new--%>
                                    <%--10--%>
                                    <obout:Column DataField="ReserveQty" HeaderText="Reserve Qty" Wrap="true" AllowEdit="false" Width="5%"
                                        HeaderAlign="left" Align="left">
                                    </obout:Column>
                                    <%--8 new--%>
                                    <obout:Column DataField="RequestQty" Width="5%" Wrap="true" HeaderAlign="left" HeaderText="Request Quantity"
                                        Align="left" AllowEdit="false">
                                        <TemplateSettings TemplateId="PlainEditTemplate" />
                                    </obout:Column>
                                    <%--9 new--%>
                                    <%--6--%>
                                    <obout:Column DataField="UOM" HeaderText="UOM" AllowEdit="false" Width="8%" HeaderAlign="left" Wrap="true"
                                        Align="left">
                                        <%--10 new--%>
                                        <%--7--%>
                                        <TemplateSettings TemplateId="UOMEditTemplate" />
                                    </obout:Column>
                                    <obout:Column DataField="UOMID" AllowEdit="false" Width="0%" Visible="false">
                                    </obout:Column>
                                    <%--11 new--%>
                                    <%--8--%>
                                    <obout:Column DataField="OrderQuantity" Wrap="true" HeaderText="Order Quantity" AllowEdit="false"
                                        Width="5%" HeaderAlign="left" Align="left">
                                        <TemplateSettings TemplateId="GridTemplateTotal" />
                                    </obout:Column>
                                    <%--12 new--%>
                                    <%--9--%>
                                    <obout:Column DataField="Price" HeaderText="Price" Wrap="true" AllowEdit="false" Width="5%" HeaderAlign="left" Align="left">
                                        <TemplateSettings TemplateId="TemplatePrice" />
                                    </obout:Column>
                                    <%--13 new--%>

                                    <%----Test--%>
                                    
                                   <obout:Column DataField="VATPercent" HeaderText="VAT Percent" Wrap="true"   AllowEdit="false"   HeaderAlign="left" Align="left" Visible="false" Width="0%"  >                                  
                                    </obout:Column>
                                       <obout:Column DataField="VATAmount" HeaderText="VAT Amount" Wrap="true"  AllowEdit="false"    HeaderAlign="left" Align="left"  Visible="false" Width="0%" >                                    
                                    </obout:Column>
                                       
                                    

                                    <obout:Column DataField="Total" HeaderText="Total" Wrap="true" AllowEdit="false" Width="5%" HeaderAlign="left" Align="left">
                                        <TemplateSettings TemplateId="PrdPriceTotal" />
                                    </obout:Column>

                                     <obout:Column DataField="SKUTotalAmtExclVAT" HeaderText="SKUTotalExclVat" Wrap="true"  AllowEdit="false"   HeaderAlign="left" Align="left"  Visible="false" Width="0%" >                                    
                                    </obout:Column>
                                       
                                    <%--14 new--%>
                                    <obout:Column DataField="colBom" HeaderText="Group Set" Wrap="true" AllowEdit="false" Width="5%"
                                        HeaderAlign="left" Align="left">
                                        <TemplateSettings TemplateId="GridTemplateBOM" />
                                    </obout:Column>
                                    <%--15 new--%>

                                    <obout:Column DataField="IsEdit" HeaderText="Edit" AllowEdit="false" HeaderAlign="left" Align="left" Width="5%">
                                        <TemplateSettings TemplateId="ItemEdit" />
                                    </obout:Column>

                                    <obout:Column DataField="IsPriceChange" HeaderText="IsPriceChange" Wrap="true" AllowEdit="false" Width="0%" Visible="false"
                                        HeaderAlign="left" Align="left">
                                    </obout:Column>
                                    <obout:Column DataField="ISShowSerial" HeaderStyle-CssClass="hideHeaderGridColumn" ID="gridHeaderViewSerial" HeaderText="View Serial" Wrap="true" AllowEdit="false" HeaderAlign="left" Align="left" Width="5%">
                                        <TemplateSettings TemplateId="ShowSerial" />
                                    </obout:Column>
                                    <obout:Column DataField="AddShowSerial" HeaderStyle-CssClass="hideHeaderGridColumn" HeaderText="Add Serial No." Wrap="true" AllowEdit="false" HeaderAlign="left" Align="left" Width="5%">
                                        <TemplateSettings TemplateId="AddSerial" />
                                    </obout:Column>
                                    <obout:Column DataField="SerialFlag" HeaderText="SerialFlag" AllowEdit="false" Width="0%"
                                        HeaderAlign="left" Align="left" Visible="false">
                                    </obout:Column>
                                </Columns>
                                <Templates>
                                    <obout:GridTemplate ID="GridTemplateBOM">
                                        <Template>
                                            <div align="center" class="bomImgStyle<%# (Container.DataItem["GroupSet"].ToString()) %>">
                                                <a id="bomURL" href="#" onclick="openBomDetails(<%# (Container.DataItem["Prod_ID"].ToString()) %>,'<%# (Container.DataItem["GroupSet"].ToString()) %>');return false;">
                                                    <asp:Image ImageUrl="~/PowerOnRent/Img/bom.png" ID="BomImg" runat="server" Height="24" ToolTip='<%# (Container.Value) %>' /></a>
                                            </div>
                                        </Template>
                                    </obout:GridTemplate>
                                    <obout:GridTemplate ID="ItemEdit">
                                        <Template>
                                            <div id="dvItemEdit" align="left">
                                                <a id="EditPrdDetail" onclick="openEditProduct(<%# (Container.DataItem["Sequence"].ToString()) %>);return false;">
                                                    <asp:Image ID="imgBtnEdit" ImageUrl="../App_Themes/Blue/img/Edit16.png" runat="server" CausesValidation="false" /></a>
                                            </div>
                                        </Template>
                                    </obout:GridTemplate>

                                    <obout:GridTemplate ID="ShowSerial">
                                        <Template>
                                            <div id="dvItemEdit" align="left" class="bomImgStyle<%# (Container.DataItem["SerialFlag"].ToString()) %>">
                                                <a id="EditPrdDetail" onclick="openShowSerial(<%# (Container.DataItem["Prod_ID"].ToString()) %> ,'<%# (Container.DataItem["SerialFlag"].ToString()) %>');return false;">
                                                    <asp:Image ID="imgBtnEdit" ImageUrl="../App_Themes/Blue/img/info.jpg" runat="server" CausesValidation="false" /></a>
                                            </div>
                                        </Template>
                                    </obout:GridTemplate>
                                    <obout:GridTemplate ID="AddSerial">
                                        <Template>
                                            <div id="dvItemEdit" align="left" class="bomImgStyle<%# (Container.DataItem["SerialFlag"].ToString()) %>">
                                                <a id="EditPrdDetail" onclick="openaddShowSerial(<%# (Container.DataItem["Prod_ID"].ToString()) %>,'<%# (Container.DataItem["SerialFlag"].ToString()) %>');return false;">
                                                    <asp:Image ID="imgBtnEdit" ImageUrl="../App_Themes/Blue/img/add.png" runat="server" CausesValidation="false" /></a>
                                            </div>
                                        </Template>
                                    </obout:GridTemplate>


                                    <obout:GridTemplate ID="GridTemplateTotal">
                                        <Template>
                                            <div class="divrowQtyTotal">
                                                <asp:Label ID="rowQtyTotal" runat="server">1</asp:Label>
                                            </div>
                                        </Template>
                                    </obout:GridTemplate>
                                    <obout:GridTemplate ID="PrdPriceTotal">
                                        <Template>
                                            <div class="divrowpriceTotal">
                                                <asp:Label ID="rowPriceTotal" runat="server">0</asp:Label>
                                            </div>
                                        </Template>
                                    </obout:GridTemplate>
                                    <obout:GridTemplate ID="PrdMOQ">
                                        <Template>
                                            <span style="text-align: left; width: 130px; margin-right: 10px;">
                                                <%# Container.Value  %></span>
                                        </Template>
                                    </obout:GridTemplate>
                                    <obout:GridTemplate ID="HeaderTempRequiredQuantity">
                                        <Template>
                                        </Template>
                                    </obout:GridTemplate>
                                    <obout:GridTemplate ID="ItemTempRemove">
                                        <Template>
                                            <table>
                                                <tr>
                                                    <td style="width: 20px; text-align: center;">
                                                        <img id="imgbtnRemove" src="../App_Themes/Grid/img/Remove16x16.png" title="Remove"
                                                            onclick="removePartFromList('<%# (Container.DataItem["Sequence"].ToString()) %>','<%# (Container.DataItem["Prod_ID"].ToString()) %>','<%# (Container.DataItem["SerialFlag"].ToString()) %>');"
                                                            style="cursor: pointer;" />
                                                    </td>
                                                    <td style="width: 35px; text-align: left;">
                                                        <%# Convert.ToInt32(Container.PageRecordIndex) + 1 %>
                                                    </td>
                                                </tr>
                                            </table>
                                        </Template>
                                    </obout:GridTemplate>
                                    <obout:GridTemplate runat="server" ID="PlainEditTemplate">
                                        <Template>
                                            <div class="divTxtUserQty">
                                                <asp:TextBox ID="txtUsrQty" Width="90%" Style="text-align: right;" runat="server"
                                                    Text="<%# Container.Value %>" onfocus="markAsFocused(this)" onkeydown="AllowDecimal(this,event);"
                                                    onkeypress="AllowDecimal(this,event);" data-qty="0" data-searialcount="0" data-prodid='<%# (Container.DataItem["Prod_ID"].ToString()) %>' onblur="markAsBlured(this, '<%# Grid1.Columns[Container.ColumnIndex].DataField %>', <%# Container.PageRecordIndex %>);"></asp:TextBox>
                                            </div>
                                        </Template>
                                    </obout:GridTemplate>
                                    <obout:GridTemplate ID="GridTemplateRightAlign">
                                        <Template>
                                            <span style="text-align: right; width: 130px; margin-right: 10px;">
                                                <%# Container.Value  %></span>
                                        </Template>
                                    </obout:GridTemplate>
                                    <obout:GridTemplate ID="UOMEditTemplate" runat="server">
                                        <Template>
                                            <asp:DropDownList ID="ddlUOM" runat="server" Width="100px" Style="text-align: left;" data-index="" data-ddlprodid='<%# (Container.DataItem["Prod_ID"].ToString()) %>'>
                                            </asp:DropDownList>
                                            <asp:HiddenField ID="hdnMyUOM" Value="" runat="server" />
                                            <%--onchange="getselectedUOM(this,'<%# Grid1.Columns[Container.ColumnIndex].DataField %>',<%# Container.PageRecordIndex %>)" CssClass="ddlUOM" onchange="return GetSelValue(this);"--%>
                                        </Template>
                                    </obout:GridTemplate>
                                    <obout:GridTemplate ID="TemplatePrice" runat="server">
                                        <Template>
                                            <div class="divTxtPrice">
                                                <asp:TextBox ID="txtUsrPrice" Width="90%" Style="text-align: right;" runat="server" onkeydown="AllowDecimal(this,event);"
                                                    onkeypress="AllowDecimal(this,event);" Text="<%# Container.Value %>"></asp:TextBox>
                                            </div>
                                        </Template>
                                    </obout:GridTemplate>
                                </Templates>
                            </obout:Grid>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                            </table>
                        </td>
                        <td align="right"><%--align="right" style="margin-right: 50px;"--%>
                            <table style="text-align: left;">
                                <tr>
                                    
                                    <td align="left">
                                        <asp:Label ID="lblTQty" runat="server" Text="Total Quantity"></asp:Label>
                                        :
                                    <asp:TextBox ID="txtTotalQty" runat="server" Enabled="false" Width="60px"></asp:TextBox>
                                    </td>
                                    <td></td>
                                    
                                  <td align="left">
                                       <div id="tdvatamt" runat="server" style="display:none" clientidmode="Static" >
                                        <asp:Label ID="lblvatamt" runat="server" Text="TotalVATAmt"></asp:Label>
                                        :
                                            
                                    <asp:TextBox ID="txtvattotalamt" runat="server" Enabled="false" Width="60px"></asp:TextBox>
                                           </div>
                                    </td>
                                    <td></td>
                                    
                                <td align="left"  >
                                        <div id="tdvatexclamt" runat="server" style="display:none"  clientidmode="Static">
                                        <asp:Label ID="lblvatexclamt" runat="server" Text="VATExclTotalamt"></asp:Label>
                                        :
                                    <asp:TextBox ID="txtvatexclamt" runat="server" Enabled="false" Width="60px"></asp:TextBox>
                                            </div>
                                    </td>
                                    <td></td>
                                        
                                    <td>
                                        <asp:Label ID="lblGTotal" runat="server" Text="Grand Total"></asp:Label>
                                        :
                                    <asp:TextBox ID="txtGrandTotal" runat="server" Enabled="false" Width="125px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
       </div>
        <div class="divHead" id="divApprovalHead" runat="server" clientidmode="Static">
            <h4>
                <asp:Label ID="lblApproval" runat="server" Text="Approval"></asp:Label>
            </h4>
            <a onclick="javascript:divcollapsOpen('divApprovalDetail',this)" id="linkApprovalDetail"
                runat="server">Collapse</a>
        </div>
        <div class="divDetailExpand" id="divApprovalDetail" runat="server" clientidmode="Static">
            <br />
            <table class="gridFrame" width="100%" id="Table2">
                <tr>
                    <td>
                        <table style="width: 100%">
                            <tr>
                                <td style="text-align: left; width: 50%">
                                    <asp:Label ID="lblApprovalHistory" CssClass="headerText" runat="server" Text="Approval"></asp:Label>
                                </td>
                                <td style="text-align: right; width: 50%">
                                    <input type="button" id="btncustomeranalys" title="Customer Analytics" value="Customer Analytics" runat="server" onclick="CustomerAnalytics();" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <obout:Grid ID="gvApprovalRemark" runat="server" CallbackMode="true" Serialize="true"
                            AllowColumnReordering="true" AllowColumnResizing="true" AutoGenerateColumns="false"
                            AllowPaging="false" ShowLoadingMessage="true" AllowSorting="false" AllowManualPaging="false"
                            AllowRecordSelection="false" ShowFooter="false" Width="100%" PageSize="-1" OnRebind="gvApprovalRemark_OnRebind">
                            <Columns>
                                <obout:Column DataField="id" Visible="false">
                                </obout:Column>
                                <obout:Column DataField="OrderId" Visible="false">
                                </obout:Column>
                                <obout:Column DataField="ApprovalId" HeaderText="Level" AllowEdit="false" Width="4%"
                                    Align="center" HeaderAlign="center">
                                </obout:Column>
                                <obout:Column DataField="ApproverName" HeaderText="Approver Name" AllowEdit="false"
                                    Width="12%" Align="center" HeaderAlign="center">
                                </obout:Column>
                                <obout:Column DataField="Date" HeaderText="Approved Date" DataFormatString="{0:dd-MMM-yyyy HH:mm:ss}" AllowEdit="false"
                                    Width="10%" Align="center" HeaderAlign="center">
                                </obout:Column>
                                <obout:Column DataField="DeligateUser" HeaderText="Delegate User" AllowEdit="false"
                                    Width="12%" HeaderAlign="center" Align="center">
                                </obout:Column>
                                <obout:Column DataField="Remark" HeaderText="Remark" AllowEdit="false" Width="15%"
                                    HeaderAlign="left" Wrap="true">
                                </obout:Column>
                                <obout:Column DataField="StatusName" HeaderText="Status" AllowEdit="false" Width="10%"
                                    HeaderAlign="left" Wrap="true">
                                </obout:Column>
                                <obout:Column DataField="ImgApproval" HeaderText="Approve" HeaderAlign="center" Align="center"
                                    Width="7%">
                                    <TemplateSettings TemplateId="GTStatusGUIApproval" />
                                </obout:Column>
                                <obout:Column DataField="ImgReject" HeaderText="Reject" HeaderAlign="center" Align="center"
                                    Width="7%">
                                    <TemplateSettings TemplateId="GTStatusGUIReject" />
                                </obout:Column>
                                <obout:Column DataField="ImgApprovewithRevision" HeaderText="Approve With Revision"
                                    HeaderAlign="center" Align="center" Width="0%" Visible="false">
                                    <TemplateSettings TemplateId="GTStatusGUIApproveWithRevision" />
                                </obout:Column>
                            </Columns>
                            <Templates>
                                <obout:GridTemplate ID="GTStatusGUIApproval" runat="server">
                                    <Template>
                                        <center>
                                            <div class='<%# ("POR" + Container.Value) %>' onclick="RequestOpenEntryForm('Approval','<%# Container.Value %>', '<%# Container.DataItem["ApproverID"] %>','<%# Container.DataItem["DeligateTo"] %>','<%# Container.DataItem["OrderId"] %>','<%# Container.DataItem["ApprovalId"] %>')">
                                            </div>
                                        </center>
                                    </Template>
                                </obout:GridTemplate>
                                <obout:GridTemplate ID="GTStatusGUIReject" runat="server">
                                    <Template>
                                        <center>
                                            <div class='<%# ("POR" + Container.Value) %>' onclick="RequestOpenEntryFormReject('Approval','<%# Container.Value %>', '<%# Container.DataItem["ApproverID"] %>','<%# Container.DataItem["DeligateTo"] %>','<%# Container.DataItem["OrderId"] %>','<%# Container.DataItem["ApprovalId"] %>')">
                                            </div>
                                        </center>
                                    </Template>
                                </obout:GridTemplate>
                                <obout:GridTemplate ID="GTStatusGUIApproveWithRevision" runat="server">
                                    <Template>
                                        <center>
                                            <div class='<%# ("POR" + Container.Value) %>' onclick="RequestOpenEntryAppWithRevision('<%# Container.Value %>', '<%# Container.DataItem["id"] %>','<%# Container.DataItem["OrderId"] %>')">
                                            </div>
                                            <%--RequestOpenEntryFormReject('Approval','<%# Container.Value %>', '<%# Container.DataItem["id"] %>','<%# Container.DataItem["OrderId"] %>')--%>
                                        </center>
                                    </Template>
                                </obout:GridTemplate>
                            </Templates>
                        </obout:Grid>
                    </td>
                </tr>
            </table>
        </div>
        <div class="divHead" id="divIssueHead" runat="server" clientidmode="Static">
            <h4>
                <asp:Label ID="lblDispatch" runat="server" Text="Dispatch"></asp:Label>
            </h4>
            <a onclick="javascript:divcollapsOpen('divIssueDetail',this)" id="linkIssueDetail"
                runat="server">Collapse</a>
        </div>
        <div class="divDetailExpand" id="divIssueDetail" runat="server" clientidmode="Static">


            <div id="Dispatchstatus" runat="server" clientidmode="Static">
                <table class="gridFrame" width="100%" id="Table1">
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td style="text-align: left;">
                                        <asp:Label ID="Label2" CssClass="headerText" runat="server" Text="Dispatch Status"></asp:Label>
                                        <asp:HiddenField ID="hndUserId" runat="server" ClientIDMode="Static" />

                                    </td>

                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>


                            <obout:Grid ID="GridDispatchstatus" runat="server" CallbackMode="true" Serialize="true" AllowColumnReordering="true"
                                AllowColumnResizing="true" AutoGenerateColumns="false" AllowPaging="false" ShowLoadingMessage="true"
                                AllowSorting="false" AllowManualPaging="false" ShowFooter="false" Width="100%"
                                PageSize="-1" OnRebind="GridDispatchstatus_Rebind">
                                <%--OnSelect="GridDispatchstatus_Select"--%>

                                <ClientSideEvents ExposeSender="true" />
                                <Columns>

                                    <%--<obout:Column DataField="Id" Visible="false">
                                    </obout:Column>--%>
                                    <obout:Column DataField="OrderNo" Index="11" Visible="false">
                                    </obout:Column>
                                    <obout:Column DataField="PackageID" Index="12" Visible="false">
                                    </obout:Column>

                                    <obout:Column ID="Edit" DataField="ID" HeaderText="Edit" Width="5%" HeaderAlign="center" TemplateId="GvTempEdit" Index="0">
                                        <TemplateSettings TemplateId="GvEdit" />
                                    </obout:Column>

                                    <obout:Column DataField="SkuCode" Width="5%" HeaderText="Sku Code" HeaderAlign="left" Index="1">
                                    </obout:Column>

                                    <obout:Column DataField="SkuDescription" HeaderText="Sku Description" AllowEdit="false" Width="15%" Align="Left" Index="2"
                                        HeaderAlign="left">
                                    </obout:Column>

                                    <obout:Column DataField="DeliveryStatus" HeaderText="Delivery Status" AllowEdit="false" Visible="false" Align="Left" Index="10"
                                        HeaderAlign="left">
                                    </obout:Column>


                                    <obout:Column DataField="Status" HeaderText="Delivery Status" AllowEdit="false" Width="7%" Align="Left" Index="3"
                                        HeaderAlign="left">
                                    </obout:Column>
                                    <obout:Column DataField="MSSIDN" HeaderText="SIM Serial" AllowEdit="false" Width="13%" Align="Left" Index="4"
                                        HeaderAlign="left">
                                    </obout:Column>

                                    <obout:Column DataField="ReadyForActivation" HeaderText="Ready For Activation" Align="center" AllowEdit="false" Width="10%" HeaderAlign="center" Index="5">
                                        <TemplateSettings TemplateId="GVPendingforActivation" />
                                    </obout:Column>

                                    <obout:Column DataField="Activated" HeaderText="Activated" AllowEdit="false" HeaderAlign="left" Align="center" Width="5%" Index="6">
                                        <TemplateSettings TemplateId="GVPendingForDriverAllocation" />
                                    </obout:Column>

                                    <obout:Column DataField="OutForDelivery" HeaderText="Out For Delivery" AllowEdit="false" Align="center" HeaderAlign="center" Width="10%" Index="7">
                                        <TemplateSettings TemplateId="GVPendingForDocumentation" />
                                    </obout:Column>

                                    <obout:Column DataField="PendingForDocument" HeaderText="Pending For Documentation" AllowEdit="false" Align="center" HeaderAlign="left" Width="12%" Index="8">
                                        <TemplateSettings TemplateId="GVPendingForClosure" />
                                    </obout:Column>

                                    <obout:Column DataField="UploadDelivered" HeaderText="Upload Delivered" AllowEdit="false" Align="center" HeaderAlign="left" Width="8%" Index="9">
                                        <TemplateSettings TemplateId="GVUploadDelivered" />
                                    </obout:Column>




                                </Columns>
                                <Templates>
                                    <obout:GridTemplate ID="GvEdit" runat="server">
                                        <Template>
                                            <table>
                                                <tr>
                                                    <td style="width: 20px; text-align: center;"></td>
                                                    <td style="width: 35px; text-align: center;">
                                                        <%--<asp:ImageButton ID="imgBtnEditNEW" CausesValidation="false" commandargument="<%# Eval("OrderNo" )%>"  runat="server" OnClientClick="EditMSISDN(<%# Eval("OrderNo" )%>);" ImageUrl="../App_Themes/Blue/img/Edit16.png" />--%>
                                                        <img id="imgBtnEditNEW" src="../App_Themes/Blue/img/Edit16.png" alt="Remove" title="Remove" onclick="EditMSISDN('<%# (Container.DataItem["Id"].ToString()) %>','<%# Container.DataItem["SkuCode"] %>');" style="cursor: pointer;" />

                                                    </td>
                                                </tr>
                                            </table>

                                        </Template>
                                    </obout:GridTemplate>


                                    <obout:GridTemplate ID="GVPendingforActivation" runat="server">
                                        <Template>
                                            <center>
                                                <div class='<%# ("POR" + Container.Value) %>' onclick="PendingforActivation('Dispatch','<%# Container.DataItem["OrderNo"] %>','<%# Container.DataItem["ReadyForActivation"] %>','<%# Container.DataItem["SkuCode"] %>')">
                                                </div>
                                            </center>
                                        </Template>
                                    </obout:GridTemplate>

                                    <obout:GridTemplate ID="GVPendingForDriverAllocation" runat="server">
                                        <Template>
                                            <center>
                                                <div class='<%# ("POR" + Container.Value) %>' onclick="PendingForDriverAllocation('Dispatch','<%# Container.DataItem["OrderNo"] %>','<%# Container.DataItem["Activated"] %>')">
                                                </div>
                                            </center>
                                        </Template>
                                    </obout:GridTemplate>

                                    <obout:GridTemplate ID="GVPendingForDocumentation" runat="server">
                                        <Template>
                                            <center>
                                                <%--<div class='<%# ("POR" + Container.Value) %>' onclick="OutForDelivery('Dispatch','<%# Container.DataItem["OrderNo"] %>','<%# Container.DataItem["OutForDelivery"] %>','<%# Container.DataItem["SkuCode"] %>')">--%>
                                                <%--<div class='<%# ("POR" + Container.Value) %>' onclick="OutForDelivery('Dispatch','<%# Container.DataItem["OrderNo"] %>','<%# Container.DataItem["OutForDelivery"] %>','<%# Container.DataItem["SkuCode"] %>','<%# Container.DataItem["PackageID"] %>')">--%>
                                                <div class='<%# ("POR" + Container.Value) %>' onclick="OutForDelivery('Dispatch','<%# Container.DataItem["OrderNo"] %>','<%# Container.DataItem["OutForDelivery"] %>','<%# Container.DataItem["SkuCode"] %>','<%# Container.DataItem["PackageID"] %>','<%# Container.PageRecordIndex %>')">
                                                </div>
                                            </center>
                                        </Template>
                                    </obout:GridTemplate>

                                    <obout:GridTemplate ID="GVPendingForClosure" runat="server">
                                        <Template>
                                            <center>
                                                <div class='<%# ("POR" + Container.Value) %>' onclick="PendingForDocument('Dispatch','<%# Container.DataItem["OrderNo"] %>','<%# Container.DataItem["PendingForDocument"] %>','<%# Container.DataItem["SkuCode"] %>')">
                                                </div>
                                            </center>
                                        </Template>
                                    </obout:GridTemplate>

                                    <obout:GridTemplate ID="GVUploadDelivered" runat="server">
                                        <Template>
                                            <center>
                                                <div class='<%# ("POR" + Container.Value) %>' onclick="UploadDelivered('Dispatch','<%# Container.DataItem["OrderNo"] %>','<%# Container.DataItem["UploadDelivered"] %>','<%# Container.DataItem["SkuCode"] %>')">
                                                </div>
                                            </center>
                                        </Template>
                                    </obout:GridTemplate>

                                </Templates>
                            </obout:Grid>

                        </td>
                    </tr>



                </table>

            </div>

            <br />


            <table class="tableForm">
                <tr>
                    <%--<td>
                        <req><asp:Label ID="lblIssueNo1" runat="server" Text="Issue No"></asp:Label> </req>
                        :
                    </td>--%>
                    <%-- <td style="text-align: left; font-weight: bold;">
                        <asp:Label runat="server" ID="lblIssueNo" Width="180px" Text="459"></asp:Label>
                    </td>--%>
                    <%-- <td colspan="2" style="visibility: hidden;">
                        <a style="font-weight: bold;" href="../PowerOnRent/TransferFrmSite.aspx">
                            <asp:Label ID="lbltranfersite" runat="server" Text="Click here to Transfer From Site"></asp:Label>
                        </a>
                    </td>
                    <td colspan="3" style="visibility: hidden;">
                        <a style="font-weight: bold;" href="../PowerOnRent/HQGoodsReceiptEntry.aspx">
                            <asp:Label ID="lblcreatereceipt" runat="server" Text="Click here to Create Goods Receipt [HQ]"></asp:Label></a>
                    </td>--%>
                </tr>
                <tr>
                    <td colspan="7" align="left" style="text-align: left; font-size: 15px; color: gray;">
                        <asp:Label ID="lblDispatchDetails" runat="server" Text="Dispatch Details"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblshipeddate" runat="server" Text="Ready For Dispatch Date"></asp:Label>
                        <%--  Shipped Date :--%>
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox runat="server" ID="txtShippedDate" MaxLength="50" Width="200px" Enabled="false"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblReceivedDate" runat="server" Text="Dispatch Date"></asp:Label>
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox runat="server" ID="txtReceivedDate" MaxLength="50" Width="200px" Enabled="false"></asp:TextBox>
                    </td>
                    <%-- </tr>
                <tr>--%>
                    <td>
                        <asp:Label ID="lblCloseDate" runat="server" Text="Cancel Date"></asp:Label>
                    </td>
                    <td colspan="3" style="text-align: left">
                        <asp:TextBox runat="server" ID="txtCloseDate" MaxLength="50" Width="200px" Enabled="false"></asp:TextBox>
                    </td>


                    <td>
                        <asp:Label ID="lblrno" runat="server" Text="Recipt No"></asp:Label>
                    </td>
                    <td colspan="3" style="text-align: left">
                        <asp:TextBox runat="server" ID="txtreciptno" MaxLength="50" Width="200px" Enabled="false"></asp:TextBox>
                    </td>


                    <%-- </tr>
                <tr>--%>
                    <td>
                        <asp:Label ID="lblRemark1" runat="server" Text="Remark"></asp:Label>
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox runat="server" ID="txtDispatchRemark" Width="200px"
                            Enabled="false"></asp:TextBox>
                        <%--TextMode="MultiLine"--%>
                    </td>
                    <td>
                        <asp:Label ID="lblasnno" runat="server" Text="ASN No."></asp:Label>
                    </td>
                   <%--Start combine 2022 CR project 2 changes start --%>
                    <td  style="text-align: left">
                        <asp:TextBox runat="server" ID="txtasnno" MaxLength="50" Width="200px" Enabled="false"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblrefundcode" runat="server" Text="Refund Code"></asp:Label>
                    </td>
                    <td  style="text-align: left">
                        <asp:TextBox runat="server" ID="txtrefundcode" MaxLength="50" Width="200px" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td colspan="12" align="left">
                        <asp:Label ID="lblrefunddate" runat="server" Text="Refund Date"></asp:Label>
                    </td>
                    <td  style="text-align: left">
                        <asp:TextBox runat="server" ID="txtrefunddate" MaxLength="50" Width="200px" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <%--End combine 2022 CR project 2 changes--%>
            </table>
            <br />
            <table class="tableForm">
                <tr>
                    <td colspan="5" align="left" style="text-align: left; font-size: 15px; color: gray;">
                        <asp:Label ID="lblCustomerDetails" runat="server" Text="Customer Details"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCustName" runat="server" Text="Customer Name"></asp:Label>
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox runat="server" ID="txtCustName" MaxLength="100" Width="200px" Enabled="false"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblContactNo" runat="server" Text="Contact No."></asp:Label>
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox runat="server" ID="txtContactNo" MaxLength="100" Width="200px" Enabled="false"></asp:TextBox>
                    </td>
                    <%--  <td>
                        <asp:Label ID="lblPhotoID" runat="server" Text="Photo ID"></asp:Label>
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox runat="server" ID="txtPhotoID" MaxLength="100" Width="200px" Enabled="false"></asp:TextBox>
                    </td>--%>
                    <td>
                        <asp:Label ID="lblEmail" runat="server" Text="Email Id"></asp:Label>
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox runat="server" ID="txtEmail" MaxLength="100" Width="200px" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCustAddress" runat="server" Text="Address"></asp:Label>
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox runat="server" ID="txtCustAddress" TextMode="MultiLine" Width="200px" Enabled="false"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblLandmark" runat="server" Text="Landmark"></asp:Label>
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox runat="server" ID="txtLandmark" Width="200px" Enabled="false"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblZipCode" runat="server" Text="Zip Code"></asp:Label>
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox runat="server" ID="txtZipcode" Width="200px" Enabled="false"></asp:TextBox>
                    </td>
                    <%--<td colspan="2">
                        <asp:Image ID="imgPhotoID" runat="server" Width="200px" />
                    </td>--%>
                </tr>
            </table>
            <br />
            <table class="tableForm">
                <tr>
                    <td colspan="5" align="left" style="text-align: left; font-size: 15px; color: gray;">
                        <asp:Label ID="lblPaymentDetail" runat="server" Text="Payment Details"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblPaymentMode" runat="server" Text="Payment Mode"></asp:Label>
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox runat="server" ID="txtPaymentMode" Width="200px" Enabled="false"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblCardNo" runat="server" Text="Card No"></asp:Label>
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox runat="server" ID="txtCardNo" Width="200px" Enabled="false"></asp:TextBox>
                        <img alt="Verified" src="Img/tick_16.png" id="Verified" runat="server" visible="false" />
                        <img alt="Decline" src="Img/delete_16.png" id="Decline" runat="server" visible="false" />
                        <img alt="Pending" src="Img/Pending.png" id="Pending" runat="server" visible="false" />
                    </td>
                    <td>
                        <asp:Label ID="lblPaymentRemark" runat="server" Text="Remark"></asp:Label>
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox runat="server" ID="txtPaymentRemark" Width="200px" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblBankName" runat="server" Text="Bank Name"></asp:Label>
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox runat="server" ID="txtBankName" Width="200px" Enabled="false"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblPaymentDate" runat="server" Text="Payment Date"></asp:Label>
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox runat="server" ID="txtPaymentDate" Width="200px" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Label ID="lblVerified" runat="server" Text="Verified"></asp:Label>
                        <img alt="Verified" src="Img/tick_16.png" id="Img3" runat="server" />
                        <asp:Label ID="lblDecline" runat="server" Text="Decline"></asp:Label>
                        <img alt="Decline" src="Img/delete_16.png" id="Img4" runat="server" />
                        <asp:Label ID="lblPending" runat="server" Text="Pending"></asp:Label>
                        <img alt="Pending" src="Img/Pending.png" id="Img5" runat="server" />
                    </td>
                </tr>
            </table>
            <br />
            <table class="tableForm">
                <tr>
                    <td colspan="5" align="left" style="text-align: left; font-size: 15px; color: gray;">
                        <asp:Label ID="lblDriverDetails" runat="server" Text="Driver Details"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblDriverName" runat="server" Text=" Driver Name"></asp:Label>
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox runat="server" ID="txtDriverName" Width="200px" Enabled="false"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblDriverContactNo" runat="server" Text="Contact No."></asp:Label>
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox runat="server" ID="txtDriverContactNo" Width="200px" Enabled="false"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblDriverEmail" runat="server" Text="Email ID"></asp:Label>
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox runat="server" ID="txtDriverEmail" Width="200px" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblTruckDetail" runat="server" Text="Truck Detail"></asp:Label>
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox runat="server" ID="txtTruckDetails" Width="200px" Enabled="false"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblAssignDate" runat="server" Text="Assign Date"></asp:Label>
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox runat="server" ID="txtAssignDate" Width="200px" Enabled="false"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblDeliveryType" runat="server" Text="Delivery Type"></asp:Label>
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox runat="server" ID="txtDeliveryType" Width="200px" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <br />
            <%--<iframe id="iframeIssue" src="../PowerOnRent/GridIssueSummary.aspx?FillBy=RequestID"
                style="border: none; width: 100%"></iframe>--%>
        </div>
        <%-- <div class="divHead" id="divReceiptHead" runat="server" clientidmode="Static">
            <h4>
                Material Receipt Detail
            </h4>
            <a onclick="javascript:divcollapsOpen('divReceiptDetail',this)" id="linkReceiptDetail">
                Collapse</a>
        </div>
        <div class="divDetailExpand" id="divReceiptDetail" runat="server" clientidmode="Static">
            <iframe id="iframeReceipt" src="../PowerOnRent/GridReceiptSummary.aspx?FillBy=RequestID"
                style="border: none; width: 100%"></iframe>
        </div>--%>
        <div class="divHead" id="divCorrespondanceHead" runat="server" clientidmode="Static">
            <h4>
                <asp:Label ID="lblCorrespondance" runat="server" Text="Correspondance"></asp:Label>
            </h4>
            <a onclick="javascript:divcollapsOpen('divCorrespondanceDetails',this)" id="linkCorrespondanceDetail"
                runat="server">Collapse</a>
        </div>
        <div class="divDetailExpand" id="divCorrespondanceDetails" runat="server" clientidmode="Static">
            <center>
                <table class="gridFrame" width="80%">
                    <tr>
                        <td>
                            <table style="width: 100%">
                                <tr>
                                    <td style="text-align: left;">
                                        <asp:Label ID="lblinbox" runat="server" Style="color: white; font-size: 15px; font-weight: bold;"
                                            Text="Inbox"></asp:Label>
                                    </td>
                                    <td style="text-align: right;">
                                        <input type="button" id="btnAddNewCorrespondance" runat="server" value="Add New"
                                            onclick="AddCorrespondance();" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <obout:Grid ID="GVInboxPOR" runat="server" Width="100%" CallbackMode="true" Serialize="true"
                                AllowColumnReordering="true" AllowColumnResizing="true" AutoGenerateColumns="false"
                                AllowPaging="false" ShowLoadingMessage="true" AllowSorting="false" AllowManualPaging="false"
                                AllowRecordSelection="false" ShowFooter="false" OnRebind="GVInboxPOR_OnRebind">
                                <Columns>
                                    <obout:Column HeaderText="View" DataField="Id" Width="4%" Align="center" HeaderAlign="center">
                                        <TemplateSettings TemplateId="TemplateEdit" />
                                    </obout:Column>
                                    <obout:Column DataField="OrderHeadId" HeaderText="Request No." Width="8%" Align="center"
                                        HeaderAlign="center">
                                    </obout:Column>
                                    <obout:Column DataField="Orderdate" HeaderText="Requested Date" Width="8%" DataFormatString="{0:dd-MMM-yy}">
                                    </obout:Column>
                                    <obout:Column DataField="StatusName" HeaderText="Status" Wrap="true" Width="10%">
                                    </obout:Column>
                                    <obout:Column DataField="MessageFromName" HeaderText="Message From" Width="8%">
                                        <TemplateSettings TemplateId="msgFrm" />
                                    </obout:Column>
                                    <obout:Column DataField="MessageTitle" HeaderText="Message Title" Width="8%">
                                    </obout:Column>
                                    <%--<obout:Column DataField="Message" HeaderText="Message" Width="15%" Wrap="true">
                                        <TemplateSettings TemplateId="GetMessage" />
                                    </obout:Column>--%>
                                </Columns>
                                <Templates>
                                    <obout:GridTemplate runat="server" ID="msgFrm">
                                        <Template>
                                            <%# (Container.DataItem["MessageFromName"].ToString() == "" ? "System Generated" : Container.DataItem["MessageFromName"].ToString())%>
                                        </Template>
                                    </obout:GridTemplate>
                                    <obout:GridTemplate runat="server" ID="TemplateEdit">
                                        <Template>
                                            <asp:ImageButton ID="imgBtnView" runat="server" ImageUrl="../App_Themes/Blue/img/Search16.png"
                                                OnClick="imgBtnView_OnClick" OnClientClick="AddCorrespondanceVW(this);return false;"
                                                CausesValidation="false" ToolTip='<%# (Container.Value) %>' data-containerId="<%# (Container.Value) %>" />
                                            <%-- OnClick="imgBtnView_OnClick" OnClientClick="openCorrenpondance('<%# (Container.Value) %>')"--%>
                                        </Template>
                                    </obout:GridTemplate>
                                    <obout:GridTemplate runat="server" ID="GetMessage">
                                        <Template>
                                            <asp:Panel ID="pnlQuizPlainText" runat="server">
                                                <asp:Label ID="setTextQCss" runat="server"><span id='QHolder<%# Container.DataItem["Id"] %>'>
                                                    <asp:Label ID="Label1" Text='<%# Container.DataItem["Message"] %>' runat="server"></asp:Label>
                                                </span>
                                                    <script type="text/javascript">
                                                        //var getQHolderObj = document.getElementById('<%#Container.DataItem["Message"] %>');
                                                        var getQHolderObj = document.getElementById('This is Message');
                                                        // if (getQHolderObj != null) {
                                                        var getHtmlQuestionStr = getQHolderObj; //.innerHTML;
                                                        var filterHtmlQuestionStr = getHtmlQuestionStr.split('andBrSt;').join('<');
                                                        getHtmlQuestionStr = filterHtmlQuestionStr;
                                                        filterHtmlQuestionStr = getHtmlQuestionStr.split('andBrEn;').join('>');
                                                        getHtmlQuestionStr = filterHtmlQuestionStr;
                                                        getQHolderObj.innerHTML = getHtmlQuestionStr;
                                                        // }        
                                                    </script>
                                                </asp:Label>
                                            </asp:Panel>
                                        </Template>
                                    </obout:GridTemplate>
                                </Templates>
                            </obout:Grid>
                        </td>
                    </tr>
                </table>
            </center>
        </div>
        <div class="divHead" id="divDocumentHead" runat="server" clientidmode="Static">
            <h4>
                <asp:Label ID="lblDocument" runat="server" Text="Document"></asp:Label>
            </h4>
            <a onclick="javascript:divcollapsOpen('divDocumentDetails',this)" id="linkDocumentDetail" runat="server">Collapse</a>
        </div>
        <div class="divDetailExpand" id="divDocumentDetails" runat="server" clientidmode="Static">
            <uc3:UC_AttachDocument ID="UC_AttachmentDocument1" runat="server" />
        </div>


        <div class="divHead" id="divOrderparameter" runat="server" clientidmode="Static">
            <h4>
                <asp:Label ID="lblorderparameter" runat="server" Text="Order Parameter"></asp:Label>
            </h4>
            <a onclick="javascript:divcollapsOpen('divOrderparameterdetails',this)" id="linkorderparameterdetails" runat="server">Collapse</a>
        </div>

        <div class="divDetailExpand" id="divOrderparameterdetails" runat="server" clientidmode="Static">
            <center>
                <table class="gridFrame" width="100%">
                    <tr>
                        <td>
                            <table style="width: 100%">
                                <tr>
                                    <td style="text-align: left;">
                                        <asp:Label ID="Label4" runat="server" Style="color: white; font-size: 15px; font-weight: bold;"
                                            Text="Order Parameter"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <obout:Grid ID="GVOrderParameter" runat="server" Width="100%" CallbackMode="true" Serialize="true"
                                AllowColumnReordering="true" AllowColumnResizing="true" AutoGenerateColumns="false"
                                AllowPaging="false" ShowLoadingMessage="true" AllowSorting="false" AllowManualPaging="false"
                                AllowRecordSelection="false" ShowFooter="false">
                                <Columns>

                                    <obout:Column DataField="Sequnece" HeaderText="Sr.No." AllowEdit="false" Width="5%"
                                        Align="center" HeaderAlign="center">
                                        <TemplateSettings TemplateId="ItemTempRemove1" />
                                    </obout:Column>

                                    <obout:Column DataField="Name" HeaderText="Group Name" HeaderAlign="left" Width="30%">
                                    </obout:Column>

                                    <obout:Column DataField="Value" HeaderText="Value" HeaderAlign="left" Width="60%">
                                    </obout:Column>

                                </Columns>

                                <Templates>
                                    <obout:GridTemplate ID="ItemTempRemove1">
                                        <Template>
                                            <table>
                                                <tr>
                                                    <td style="width: 20px; text-align: center;"></td>
                                                    <td style="width: 35px; text-align: center;">
                                                        <asp:Label ID="lblsrid" runat="server" Text=" <%# Convert.ToInt32(Container.PageRecordIndex) + 1 %> "></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </Template>
                                    </obout:GridTemplate>
                                </Templates>

                            </obout:Grid>
                        </td>
                    </tr>
                </table>
            </center>
        </div>

         <%--START Ecommerce Installation Order Details --%>

        <div class="divHead" id="divEcomminstallment" runat="server" clientidmode="Static">
            <h4>
                <asp:Label ID="lblinstall" runat="server" Text="Installment Details"></asp:Label>
            </h4>
            <a onclick="javascript:divcollapsOpen('divinstallmentdetails',this)" id="lnkInstallOrder" runat="server">Collapse</a>
        </div>

        <div class="divDetailExpand" id="divinstallmentdetails" runat="server" clientidmode="Static">
            <center>
                <table class="gridFrame" width="100%">
                    <tr>
                        <td>
                            <table style="width: 100%">
                                <tr>
                                    <td style="text-align: left;">
                                        <asp:Label ID="lblEcomInstall" runat="server" Style="color: white; font-size: 15px; font-weight: bold;"
                                            Text="Installment Details"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <obout:Grid ID="grdinstallment" runat="server" Width="100%" CallbackMode="true" Serialize="true"
                                AllowColumnReordering="true" AllowColumnResizing="true" AutoGenerateColumns="false"
                                AllowPaging="false" ShowLoadingMessage="true" AllowSorting="false" AllowManualPaging="false"
                                AllowRecordSelection="false" ShowFooter="false">
                                <Columns>
                                    <obout:Column DataField="Sequnece" HeaderText="Sr.No." AllowEdit="false" Width="5%"
                                        Align="center" HeaderAlign="center">
                                        <TemplateSettings TemplateId="ItemTempRemove2" />
                                    </obout:Column>
                                    <obout:Column DataField="WebOrderNo" HeaderText="WebOrderNo" HeaderAlign="left" Width="10%">
                                    </obout:Column>
                                    <obout:Column DataField="InstallmentDetails" HeaderText="Details" HeaderAlign="left" Width="80%">
                                    </obout:Column>
                                    <%--                    <obout:Column DataField="Value" HeaderText="SKU Name" HeaderAlign="left" Width="60%">
        </obout:Column>
        <obout:Column DataField="Value" HeaderText="SKU Description" HeaderAlign="left" Width="60%">
        </obout:Column>
        <obout:Column DataField="Value" HeaderText="Installment Period" HeaderAlign="left" Width="60%">
        </obout:Column>
        <obout:Column DataField="Value" HeaderText="Duration UOM" HeaderAlign="left" Width="60%">
        </obout:Column>
        <obout:Column DataField="Value" HeaderText="Unit Price" HeaderAlign="left" Width="60%">
        </obout:Column>
        <obout:Column DataField="Value" HeaderText="Down Payment" HeaderAlign="left" Width="60%">
        </obout:Column>
        <obout:Column DataField="Value" HeaderText="Total Install Amount" HeaderAlign="left" Width="60%">
        </obout:Column>
        <obout:Column DataField="Value" HeaderText="1st Month Install Amount" HeaderAlign="left" Width="60%">
        </obout:Column>--%>
                                </Columns>
                                <Templates>
                                    <obout:GridTemplate ID="ItemTempRemove2">
                                        <Template>
                                            <table>
                                                <tr>
                                                    <td style="width: 20px; text-align: center;"></td>
                                                    <td style="width: 35px; text-align: center;">
                                                        <asp:Label ID="lblsrid" runat="server" Text=" <%# Convert.ToInt32(Container.PageRecordIndex) + 1 %> "></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </Template>
                                    </obout:GridTemplate>
                                </Templates>

                            </obout:Grid>
                        </td>
                    </tr>
                </table>
            </center>
        </div>

        <%--END Ecommerce Installation Order Details --%>

        <table class="tableForm" style="visibility: hidden;">
            <tr>
                <td style="font-size: 13px; font-weight: bold;">
                    <asp:Label ID="lblOperationApproval" runat="server" Text="Operation Approval *"></asp:Label>
                    :
                </td>
                <td style="vertical-align: top; font-size: 13px; font-weight: bold; text-align: left;">
                    <label class="label_check" id="lblApproved" for="CheckBoxApproved">
                        <asp:CheckBox ID="CheckBoxApproved" runat="server" ClientIDMode="Static" onclick="OnlyOneCheckedA('CheckBoxApproved','CheckBoxRejected');" />
                        <asp:Label ID="lblApproved1" runat="server" Text="Approved"></asp:Label>
                    </label>
                    <label class="label_check" id="lblRejected" for="CheckBoxRejected">
                        <asp:CheckBox ID="CheckBoxRejected" runat="server" ClientIDMode="Static" onclick="OnlyOneCheckedR('CheckBoxApproved','CheckBoxRejected');" />
                        <asp:Label ID="lblCancelled" runat="server" Text="Cancelled"></asp:Label>
                    </label>
                    <label class="label_check" id="lblRevision" for="CheckBoxRevision">
                        <asp:CheckBox ID="CheckBoxRevison" runat="server" ClientIDMode="Static" onclick="OnlyOneCheckedR('CheckBoxApproved','CheckBoxRejected');" />
                        <%--<asp:Label ID=" ApproveWithRevision" runat="server" Text=" Approve With Revision"></asp:Label>--%>
                        <asp:Label ID="lblapprovrevision" runat="server" Text="Approve With Revision"></asp:Label>
                    </label>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    <asp:Label ID="lblDate" runat="server" Text="Date"></asp:Label>
                    :
                </td>
                <td style="text-align: left;">
                    <asp:Label runat="server" ID="lblApprovalDate"></asp:Label>
                </td>
            </tr>
            <tr>
                <td id="tdApprovalRemark" style="text-align: right;">
                    <asp:Label ID="lblApprovRemark" runat="server" Text="Remark"></asp:Label>
                    :
                </td>
                <td>
                    <asp:TextBox runat="server" ID="txtApprovalRemark" onkeyup="TextBox_KeyUp(this,'CharactersCountertxtApprovalRemark','200');"
                        ClientIDMode="Static" Width="400px" TextMode="MultiLine">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td></td>
                <td style="text-align: left;">
                    <span class="watermark"><span id="CharactersCountertxtApprovalRemark">
                        <asp:Label ID="lbl200" runat="server" Text="200"></asp:Label></span><asp:Label ID="lbl2001"
                            runat="server" Text="/ 200"></asp:Label>
                    </span>
                    <input type="button" id="btnSaveApproval" value="Submit" style="float: right;" onclick="jsSaveApproval()"
                        runat="server" />
                </td>
            </tr>
        </table>



        <asp:HiddenField ID="hdnselectedCompany" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnselectedDept" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnselectedCont1" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnselectedCont2" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnSelAddress" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnSelectedQty" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnSearchContactID1" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnSearchContactName1" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnSearchContactID2" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnSearchContactName2" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnSearchAddressID" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnSearchAddress" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnSearchLocationID" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnSearchLocation" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnChngDept" runat="server" />
        <asp:HiddenField ID="hdnSelPaymentMethod" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnEnteredPrice" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnEnteredQty" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnSelectedUMO" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnMaxDeliveryDays" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="PMText" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="PMLable" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="PMReturn" runat="server" ClientIDMode="Static" Value="0" />
        <asp:HiddenField ID="hdnApprovalId" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnSelectedSequenceNo" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnChangePrdQtyPrice" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnNewOrderID" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnOrderStatus" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnPmethodChng" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnLocConID" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnLocConName" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnlocationname" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnlocationcode" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnselectedRequestor" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hdngridvalue" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnselectedorder" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnselectedorderMsidn" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hdncustanalya" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnUserType" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hdndeliverytype" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnsiteidnew" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnsitecode" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnsitecodeid" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnsitedescription" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnprojectid" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnprojetnm" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnselectedprojectid" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnselectedprojectnm" runat="server" ClientIDMode="Static" />
        
        <asp:HiddenField ID="hdnsgmntID" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnsgmnType" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnsegmentID" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnsegmenttype" runat="server" ClientIDMode="Static" />


        <asp:HiddenField ID="hdnsiteid" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnsitenm" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnlatitude" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnlogitude" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnaccessspe" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnsitecode1" runat="server" ClientIDMode="Static" />

        <asp:HiddenField ID="hdnselectedsiteid" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnIsState" runat="server" ClientIDMode="Static" />

        <asp:HiddenField ID="hdnISProjectSiteDetails" runat="server" ClientIDMode="Static" />


        <%--//serial number--%>
        <asp:HiddenField ID="hdnserialsku" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnOrderisEdit" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnshowserialsku" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnSerialSKUQty" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnchnageqtyskuid" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnchangeqtyvalue" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnnewskuqty" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnrowindex" runat="server" ClientIDMode="Static" />

        <asp:HiddenField ID="hdnordertype" runat="server" ClientIDMode="Static" />
        <asp:HiddenField ID="hdnErpAutoApproval" runat="server" ClientIDMode="Static" />
        

    </center>
    <script type="text/javascript">
        onload();
        function onload()
        {
            var v = document.getElementById("btnConvertTo");
            v.style.visibility = 'hidden';

            var exp = document.getElementById("btnExport");
            exp.style.visibility = 'hidden';

            var imp = document.getElementById("btnImport");
            imp.style.visibility = 'hidden';

            var ml = document.getElementById("btnMail");
            ml.style.visibility = 'hidden';

            var pt = document.getElementById("btnPrint");
            pt.style.visibility = 'hidden';
             showAlert("Not Applicable", "Error", "#");
            alert('on load call');
        }

        /*Navigation code*/
        function OpenEntryForm(invoker, state, referenceID, requestID) {
            if (state != "gray") {
                if (state == "red") { state = "Edit"; }
                else if (state != "red") { state = "View"; }
                pupUpLoading.style.display = "";
                PageMethods.WMSetSession(referenceID, requestID, state, OpenEntryFormOnSuccess, null);

            }
            else {
                showAlert("Not Applicable", "Error", "#");
            }
        }

    </script>
    <script type="text/javascript">
        /*Approval JS*/
        /*Approval checkbox Only one checked Approved or Rejected*/
        function OnlyOneCheckedA(chkA, chkR) {
            var findtd = document.getElementById('tdApprovalRemark');

            if (document.getElementById(chkA).checked == true) {
                findtd.innerHTML = "Remark / Reason : ";
                document.getElementById('txtApprovalRemark').accessKey = "";
                document.getElementById(chkR).checked = false;
                lblRejected.className = "label_check c_off";
            }
        }

        function OnlyOneCheckedR(chkA, chkR) {
            var findtd = document.getElementById('tdApprovalRemark');

            if (document.getElementById(chkR).checked == true) {
                findtd.innerHTML = "Remark / Reason * :";
                document.getElementById('txtApprovalRemark').accessKey = "1";
                document.getElementById(chkA).checked = false;
                lblApproved.className = "label_check c_off";
            }
        }

        function jsSaveApproval() {
            if (document.getElementById('CheckBoxApproved').checked == false && document.getElementById('CheckBoxRejected').checked == false) {
                showAlert("Approval status should not be left blank [ Approved or Rejected ]", "Error", "#");
            }
            else if (document.getElementById('CheckBoxRejected').checked == true && document.getElementById('txtApprovalRemark').value == "") {
                showAlert("Fill rejection reason", "Error", "#");
                document.getElementById('txtApprovalRemark').focus();
            }
            else {
                LoadingOn(true);
                debugger;
                PageMethods.WMSaveApproval(document.getElementById('CheckBoxApproved').checked, document.getElementById('txtApprovalRemark').value, jsSaveApprovalOnSuccess, jsSaveApprovalOnFail);
            }
        }

        function jsSaveApprovalOnSuccess(result) {
            if (result.toString().toLowerCase() == 'true') {
                showAlert("Approval status has been saved successfully", "info", "../PowerOnRent/Default.aspx?invoker=Request");
            }
            else {
                showAlert("Some error occurred", "error", "#");
            }
        }
        function jsSaveApprovalOnFail() { }

    </script>
    <script type="text/javascript">
        var txtTitle = document.getElementById("<%= txtTitle.ClientID %>");
        var ddlSites = document.getElementById("<%= ddlSites.ClientID %>");
        var lblRequestNo = document.getElementById("<%= lblRequestNo.ClientID %>");
        var ddlStatus = document.getElementById("<%= ddlStatus.ClientID %>");


        var ddlRequestByUserID = document.getElementById("<%= ddlRequestByUserID.ClientID %>");
        var txtRemark = document.getElementById("<%= txtRemark.ClientID %>");
        var ddlContact1 = document.getElementById("<%= ddlContact1.ClientID %>");
        var ddlContact2 = document.getElementById("<%= ddlContact2.ClientID %>");
        var ddlAddress = document.getElementById("<%= ddlAddress.ClientID %>");
        var txtCustOrderRefNo = document.getElementById("<%= txtCustOrderRefNo.ClientID %>");

        var txtTemplateTitleNew = document.getElementById("<%= txtTemplateTitleNew.ClientID %>");
        var ddlAccessTypeNew = document.getElementById("<%= ddlAccessTypeNew.ClientID %>")
        /*---------------*/

        function openProjectType()
        {
            window.open('../PowerOnRent/ProjectType.aspx', null, 'height=480px, width=800px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=300; screenY=50');

        }
        function openSiteMaster()
        {
            var hdnselectedprojectid = document.getElementById("hdnselectedprojectid");
            if (hdnselectedprojectid.value == "" || hdnselectedprojectid.value == null)
            {
                showAlert("Select Project Type", "Error", "#");
            }
            else
            {
                window.open('../PowerOnRent/SiteMaster.aspx?pid=' + hdnselectedprojectid.value, null, 'height=680px, width=990px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=300; screenY=50');
            }

        }


        /*Toolbar Code*/
        function jsAddNew() {
            PageMethods.WMpageAddNew(jsAddNewOnSuccess, null);
        }
        function jsAddNewOnSuccess() {
            //Grid1.refresh();
            //ClearMode('divRequestDetail');
            //jsFillStatusList('Add');
            window.open('../PowerOnRent/PartRequestEntry.aspx', '_self', '');
        }

        /*Add By Suresh */

        function OpenProductScreen() {
            window.open('../Product/ProductMaster.aspx', '_self', '');
        }

        /*Add By Suresh */

        //function jsSaveData() {
        function GetPaymentMethodDetails()
        {

            var validate = validateForm('divRequestDetail');
            var Con1ID = document.getElementById("hdnSearchContactID1");
            var Con2ID = document.getElementById("hdnSearchContactID2");
            var AdrsID = document.getElementById("hdnSearchAddressID");

            var AdrsSelID = document.getElementById("hdnSelAddress");
            var Con1Sel = document.getElementById("hdnselectedCont1");
            var Con2Sel = document.getElementById("hdnselectedCont2");
            var LocationID = document.getElementById("hdnSearchLocationID");
            //var PMD = GetPaymentMethodDetails();
            var PMReturn = document.getElementById('PMReturn');
            var PMD = PMReturn.value;

            var DTDIFF = GetDateDifference();
            var hdnMaxDeliveryDays = document.getElementById('hdnMaxDeliveryDays');

            var hdnselectedCompany = document.getElementById('hdnselectedCompany');
            var hdnselectedprojectid = document.getElementById("hdnselectedprojectid");
            var hdnselectedsiteid = document.getElementById("hdnselectedsiteid");
            var ddlCompany = document.getElementById("<%= ddlCompany.ClientID %>");
            var companyid = ddlCompany.options[ddlCompany.selectedIndex].value;
            var hdnISProjectSiteDetails = document.getElementById("hdnISProjectSiteDetails");
            
            var isdeptERP = document.getElementById("hdnErpAutoApproval");

            if (PMD == 0)
            {
                showAlert("Fill all mandatory fields of Payment Methods", "Error", "#");
            }
            else if (validate == false)
            {
                showAlert("Fill all mandatory fields", "Error", "#");
            }

            else if (DTDIFF == 0)
            {
                showAlert("Selected Exp. Delivery Date is must less than " + hdnMaxDeliveryDays.value + " Days", "Error", "#");
            }
            else if (DTDIFF == 2)
            {
                showAlert("Please Select Exp. Delivery Date", "Error", "#");
            }
            else if (isdeptERP.value == "True" && LocationID.value == "")
            {
                showAlert("Please Select Location", "Error", "#");
            }
            else if (hdnselectedDept.value == "10307" && hdnsegmentID.value == "")
            {
                 showAlert("Please Select Enterprise Segment", "Error", "#");
            }
            // else if (companyid == "10266")
            else if (hdnISProjectSiteDetails.value == "Yes")
            {
                if (hdnselectedprojectid.value == "")
                {
                    showAlert("Please Select Project Type", "Error", "#");
                }
                else if (hdnselectedsiteid.value == "")
                {
                    showAlert("Please Select Site", "Error", "#");
                }
                else {
                    // if (ddlStatus.options[ddlStatus.selectedIndex].value == 2 && Grid1.Rows.length == 0) {
                    if (Grid1.Rows.length == 0)
                    {
                        showAlert("Add atleast one part into the Request Part List", "error", "#");
                    }
                    else {
                        // Code to check all txtUserQty values
                        //var getLength = $('.divTxtUserQty input').length();
                        //alert(getLength);
                        var isContainsZero = 'no';
                        var matches = 0;
                        $(".divTxtUserQty input").each(function (i, val) {
                            if (($(this).val() == '0.00') || ($(this).val() == '0') || ($(this).val() == 0)) {
                                isContainsZero = 'yes';
                            }
                        });

                        var isContnZro = 'no';
                        var mtch = 0;
                        $(".divrowQtyTotal span").each(function (i, html) {
                            if (($(this).html() == '0') || ($(this).html() == '0.00') || ($(this).html() == 0) || ($(this).html() == 'NaN')) {
                                isContnZro = 'yes';
                            }
                        });

                        var isPriceZro = 'no';
                        var mth = 0;
                        $(".divTxtPrice input").each(function (i, val) {
                            if (($(this).val() == '0.00') || ($(this).val() == '0') || ($(this).val() == 0)) {
                                isPriceZro = 'yes';
                            }
                        });

                        if (isContainsZero != 'yes' && isContnZro != 'yes') {
                            LoadingOn(true);

                            var Deliverydate = getDateFromUC("<%= UC_ExpDeliveryDate.ClientID %>");
                            var obj1 = new Object();
                            obj1.StoreId = parseInt(ddlSites.options[ddlSites.selectedIndex].value);
                            // obj1.OrderNumber = lblRequestNo.innerHTML.toString();
                            obj1.OrderNumber = txtCustOrderRefNo.value.toString();
                            obj1.Priority = "Medium";// ddlRequestType.options[ddlRequestType.selectedIndex].value.toString();
                            obj1.Status = parseInt(ddlStatus.options[ddlStatus.selectedIndex].value);
                            obj1.Title = txtTitle.value.toString();

                            obj1.RequestBy = parseInt(ddlRequestByUserID.options[ddlRequestByUserID.selectedIndex].value);
                            obj1.Remark = txtRemark.value.toString();
                            obj1.Deliverydate = Deliverydate;
                            //obj1.ContactId1 = parseInt(ddlContact1.options[ddlContact1.selectedIndex].value);                        
                            if (Con1ID.value == "" && Con1Sel.value != "") { obj1.ContactId1 = parseInt(Con1Sel.value); }
                            else {
                                obj1.ContactId1 = parseInt(Con1ID.value);
                            }


                            if (obj1.ContactId1 == 0)
                            {
                                obj1.ContactId2 = 0;
                            } else
                            {
                                
                                if (Con2ID.value != "")
                                {
                                    obj1.ContactId2 = Con2ID.value;
                                } else { obj1.ContactId2 = 0; }
                            }
                           
                            if (AdrsID.value == "" && AdrsSelID.value != "") {
                                obj1.AddressId = parseInt(AdrsSelID.value);
                            } else if (AdrsID.value != "" && AdrsSelID.value == "") {
                                obj1.AddressId = parseInt(AdrsID.value);
                            } else {
                                obj1.AddressId = 0;
                            }

                            if (LocationID.value == "")
                            {
                                obj1.LocationID = 0
                            } else
                            {
                                obj1.LocationID = LocationID.value;
                            }
                            var pm = $('#ddlPaymentMethod').val();
                            obj1.PaymentID = pm;

                            var txtGrandTotal = document.getElementById("<%=txtGrandTotal.ClientID%>");
                            var txtTotalQty = document.getElementById("<%=txtTotalQty.ClientID%>");

                            obj1.TotalQty = txtTotalQty.value;
                            obj1.GrandTotal = txtGrandTotal.value;

                            if (ddlStatus.selectedIndex == 1) { obj1.IsSubmit = "false"; }
                            else { obj1.IsSubmit = "true"; }


                            //companyid == "10266"
                            if (hdnISProjectSiteDetails.value == "Yes")
                            {
                                if (hdnselectedprojectid.value == "") { hdnselectedprojectid.value = "0"; }
                                if (hdnselectedsiteid.value == "") { hdnselectedsiteid.value = "0"; }
                                obj1.ProjTypeID = hdnselectedprojectid.value;
                                obj1.SiteID = hdnselectedsiteid.value;
                            }
                            else
                            {
                                obj1.ProjTypeID = "0";
                                obj1.SiteID = "0";
                            }
                            if (hdnsegmentID.value == "")
                            {
                                obj1.SegmentID = "0";
                            }
                            else
                            {
                                 obj1.SegmentID = hdnsegmentID.value;
                            }
                           // showAlert("1", "error", "#");
                            // SavePaymentMethod();
                            //PageMethods.WMSaveRequestHead(obj1, WMSaveRequestHead_onSuccessed, WMSaveRequestHead_onFailed); // old
                            PageMethods.WMSaveRequestHead_New(obj1, WMSaveRequestHead_onSuccessed, WMSaveRequestHead_onFailed);//new

                        }
                        else {
                            showAlert("One or more request or order quantity is zero", "error", "#");
                        }                    //return false;
                        // Code to check all txtUserQty values
                    }
                }
            }

            else {
                // if (ddlStatus.options[ddlStatus.selectedIndex].value == 2 && Grid1.Rows.length == 0) {
                if (Grid1.Rows.length == 0) {
                    showAlert("Add atleast one part into the Request Part List", "error", "#");
                }
                else {
                    // Code to check all txtUserQty values
                    //var getLength = $('.divTxtUserQty input').length();
                    //alert(getLength);
                    var isContainsZero = 'no';
                    var matches = 0;
                    $(".divTxtUserQty input").each(function (i, val) {
                        if (($(this).val() == '0.00') || ($(this).val() == '0') || ($(this).val() == 0)) {
                            isContainsZero = 'yes';
                        }
                    });

                    var isContnZro = 'no';
                    var mtch = 0;
                    $(".divrowQtyTotal span").each(function (i, html) {
                        if (($(this).html() == '0') || ($(this).html() == '0.00') || ($(this).html() == 0) || ($(this).html() == 'NaN')) {
                            isContnZro = 'yes';
                        }
                    });

                    var isPriceZro = 'no';
                    var mth = 0;
                    $(".divTxtPrice input").each(function (i, val) {
                        if (($(this).val() == '0.00') || ($(this).val() == '0') || ($(this).val() == 0)) {
                            isPriceZro = 'yes';
                        }
                    });

                    if (isContainsZero != 'yes' && isContnZro != 'yes') {
                        LoadingOn(true);

                        var Deliverydate = getDateFromUC("<%= UC_ExpDeliveryDate.ClientID %>");
                        var obj1 = new Object();
                        obj1.StoreId = parseInt(ddlSites.options[ddlSites.selectedIndex].value);
                        // obj1.OrderNumber = lblRequestNo.innerHTML.toString();
                        obj1.OrderNumber = txtCustOrderRefNo.value.toString();
                        obj1.Priority = "Medium";// ddlRequestType.options[ddlRequestType.selectedIndex].value.toString();
                        obj1.Status = parseInt(ddlStatus.options[ddlStatus.selectedIndex].value);
                        obj1.Title = txtTitle.value.toString();

                        obj1.RequestBy = parseInt(ddlRequestByUserID.options[ddlRequestByUserID.selectedIndex].value);
                        obj1.Remark = txtRemark.value.toString();
                        obj1.Deliverydate = Deliverydate;
                        //obj1.ContactId1 = parseInt(ddlContact1.options[ddlContact1.selectedIndex].value);                        
                        if (Con1ID.value == "" && Con1Sel.value != "") { obj1.ContactId1 = parseInt(Con1Sel.value); }
                        else {
                            obj1.ContactId1 = parseInt(Con1ID.value);
                        }


                        if (obj1.ContactId1 == 0) {
                            obj1.ContactId2 = 0;
                        } else {
                            //obj1.ContactId2 = parseInt(ddlContact2.options[ddlContact2.selectedIndex].value);
                            //if (Con2ID.value == "" && Con2Sel.value!="0") {
                            //    obj1.ContactId2 = Con2Sel.value;
                            //} else if (Con2ID.value != "" && Con2Sel.value == "0") {
                            //    obj1.ContactId2 = Con2ID.value;
                            //} else {
                            //    obj1.ContactId2 = 0;
                            //}
                            if (Con2ID.value != "") {
                                obj1.ContactId2 = Con2ID.value;
                            } else { obj1.ContactId2 = 0; }
                        }
                        // obj1.AddressId = parseInt(ddlAddress.options[ddlAddress.selectedIndex].value);
                        if (AdrsID.value == "" && AdrsSelID.value != "") {
                            obj1.AddressId = parseInt(AdrsSelID.value);
                        } else if (AdrsID.value != "" && AdrsSelID.value == "") {
                            obj1.AddressId = parseInt(AdrsID.value);
                        } else {
                            obj1.AddressId = 0;
                        }

                        if (LocationID.value == "") {
                            obj1.LocationID = 0
                        } else {
                            obj1.LocationID = LocationID.value;
                        }
                        var pm = $('#ddlPaymentMethod').val();
                        obj1.PaymentID = pm;

                        var txtGrandTotal = document.getElementById("<%=txtGrandTotal.ClientID%>");
                        var txtTotalQty = document.getElementById("<%=txtTotalQty.ClientID%>");

                        obj1.TotalQty = txtTotalQty.value;
                        obj1.GrandTotal = txtGrandTotal.value;

                        if (ddlStatus.selectedIndex == 1) { obj1.IsSubmit = "false"; }
                        else { obj1.IsSubmit = "true"; }

                        obj1.ProjTypeID = "0";
                        obj1.SiteID = "0";
                        if (hdnsegmentID.value == "")
                            {
                                obj1.SegmentID = "0";
                            }
                            else
                            {
                                 obj1.SegmentID = hdnsegmentID.value;
                            }
                        // SavePaymentMethod();
                       //  showAlert("2", "error", "#");
                        //PageMethods.WMSaveRequestHead(obj1, WMSaveRequestHead_onSuccessed, WMSaveRequestHead_onFailed);  // old
                        PageMethods.WMSaveRequestHead_New(obj1, WMSaveRequestHead_onSuccessed, WMSaveRequestHead_onFailed);//new
                    }
                    else {
                        showAlert("One or more request or order quantity is zero", "error", "#");
                    }                    //return false;
                    // Code to check all txtUserQty values
                }
            }
        }

        function WMSaveRequestHead_onSuccessed(result) {
            var hdnNewOrderID = document.getElementById('hdnNewOrderID');
            if (result == 0 || result == 0) {
                showAlert("Order Generation Failed. Please Check the Order Input and Department Configurations...", "Error", '../PowerOnRent/Default.aspx?invoker=Request');
            }
            else if (result >= 1) {
                //"Request saved successfully"
                hdnNewOrderID.value = result;
                SavePaymentMethod();
                showAlert("Request saved successfully", "info", "../PowerOnRent/Default.aspx?invoker=Request");
            } else if (result == -5) {
                showAlert("Current Stock for One of the Sku is Changed in Warehouse. Order Can Not Be Process. Please Re-Enter The Order...", "Error", '../PowerOnRent/PartRequestEntry.aspx');
            }
            else if (result == -3) {
                showAlert("Request saved successfully. Email Notification Failed...", "info", "../PowerOnRent/Default.aspx?invoker=Request");
            }
            else {
                window.open('../PowerOnRent/Approval.aspx?REQID=' + result + '&ST=24&APL=0', null, 'height=180px, width=900px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=300; screenY=50');
                showAlert("Request Edited Successfully", "info", "../PowerOnRent/Default.aspx?invoker=Request");
            }
        }

        function WMSaveRequestHead_onFailed() { showAlert("Error occurred", "Error", "../PowerOnRent/Default.aspx?invoker=Request"); }

        function OnSuccessMandatory(result) {
            var SelCon = result
            var pm = $('#ddlPaymentMethod').val();
            var PMReturn = document.getElementById('PMReturn');
            //PMReturn.value = 0;
            var count = (SelCon.match(/,/g) || []).length;
            console.log(count);
            var res = result.split(",");
            // var PMText = document.getElementById('PMText');

            var PMText = document.getElementById("<%=PMText.ClientID%>")
            var txtvl = 0;
            $(".lstText input").each(function (i, val) {
                txtval = $(this).val();
                if (i == 0) { PMText.value = $(this).val(); }
                else { PMText.value = PMText.value + '|' + $(this).val(); }

            });

            var addmth = PMText.value.split("|");
            //for (var i = 0; i <= res.length - 1; i++) {
            //for (var i = 0; i <= addmth.length - 1; i++) {
            //    if (addmth[i] == "") {
            //        PMReturn.value = 0;
            //        i = (addmth.length);
            //    } else {
            //        PMReturn.value = 1;           
            //    }
            //}

            for (var i = 1; i <= addmth.length; i++) {
                if (i == result)
                {
                    if (addmth[i] == "") {
                        PMReturn.value = 0;
                        i = (addmth.length);
                    } else {
                        PMReturn.value = 1;
                    }
                }
                else
                {
                     if (addmth[i] == "") {
                        PMReturn.value = 0;
                        i = (addmth.length);
                    } else {
                        PMReturn.value = 1;
                    }
                }

            }


            GetPaymentMethodDetails();
            // alertGlow('Wait');

            // return PMReturn.value;
        }



        function jsSaveData()
        {
       
            var hdnIsState = document.getElementById("<%=hdnIsState.ClientID%>");
           // alert('jsSaveData'+hdnIsState.value)
            var hdnselectedprojectid = document.getElementById("<%=hdnselectedprojectid.ClientID%>");
           // alert('jsSaveData'+hdnselectedprojectid.value)
            var hdnselectedsiteid = document.getElementById("<%=hdnselectedsiteid.ClientID%>");
           // alert('jsSaveData'+hdnselectedsiteid.value)
            if (hdnIsState.value == "EditProjectsite")
            {

                if (hdnselectedprojectid.value == "")
                {
                    showAlert("Please Select Project Type", "Error", "#");
                }
                else if (hdnselectedsiteid.value == "")
                {
                    showAlert("Please Select Site", "Error", "#");
                }
                else
                {
                    LoadingOn(true);
                    PageMethods.WMSaveEditmodeData(hdnselectedprojectid.value, hdnselectedsiteid.value, WMSaveEditmodeDataonsuccess, null)
                }

            }
            else
            {

                if (document.getElementById("<%= txtLocation.ClientID %>").value == "")
                {
                    var StoreId = parseInt(ddlSites.options[ddlSites.selectedIndex].value);
                    PageMethods.WMcheckLocationForDepartment(StoreId, Locationonsuccess, null)
                }
                else
                {
                    CheckandinsertOrderWithandWithoutLocation();
                }
            }
        }

        function WMSaveEditmodeDataonsuccess(result)
        {
            if (result == "1") {
                showAlert("Request saved successfully", "info", "../PowerOnRent/Default.aspx?invoker=Request");
            }
            else {
                showAlert("Request saved failed", "error", "../PowerOnRent/Default.aspx?invoker=Request");
            }

        }

        function Locationonsuccess(value)
        {
            if (value == 1)
            {
                showAlert("Location  is Mandatory!", "error", "#");
            }
            else
            {
                CheckandinsertOrderWithandWithoutLocation();
            }
        }
        //function GetPaymentMethodDetails() {
        function CheckandinsertOrderWithandWithoutLocation()
        {

            var PMText = document.getElementById("<%=PMText.ClientID%>")
            var PMR = document.getElementById('PMReturn');
            // PMReturn.value = 0;
            var pm = $('#ddlPaymentMethod').val(); //alert(pm);
            if (pm <= 4 && pm > 1) {
                if (pm == 3) {
                    var txtvl = 0;
                    $(".lstText input").each(function (i, val) {
                        txtval = $(this).val();
                    })
                    var txtGrandTotal = document.getElementById("<%=txtGrandTotal.ClientID%>");
                    if (txtval == txtGrandTotal.value) {
                        PageMethods.WMGetMandatoryDetails(pm, OnSuccessMandatory, null);
                    }
                    else {
                        showAlert("Cash to be collected price must be Grandtotal price ...!!!", "Error", '#');

                    }

                }
                else if (pm == 2) {
                    var txtvl = 0;
                    $(".lstText input").each(function (i, val) {
                        txtval = $(this).val();
                        if (i == 0) { PMText.value = $(this).val(); }
                        else {
                            PMText.value = PMText.value + '|' + $(this).val();
                        }
                    })

                    var addmth = PMText.value.split("|");
                    var msisdn = addmth[1];
                    if (msisdn != "") {
                        if (msisdn.length == 8) {
                            PageMethods.WMGetMandatoryDetails(pm, OnSuccessMandatory, null);

                        }
                        else {
                            showAlert("MSISDN must be exactly 8 characters", "Error", '#');
                        }

                    }
                    else {
                        showAlert("Please Enter MSISDN", "Error", '#');
                    }

                }
                else {
                    PageMethods.WMGetMandatoryDetails(pm, OnSuccessMandatory, null);
                }
            }
            else if (pm == 5) {
                var dpm = $('#ddlFOC').val();
                if (dpm == 0) {
                    PMR.value = 0; GetPaymentMethodDetails();
                } else {
                    PMR.value = 1; GetPaymentMethodDetails();
                }
            }
            else if (pm == 1) {
                PMR.value = 1; GetPaymentMethodDetails();
            }
            else if (pm >= 6) {
                if (pm == 7 || pm==8) {
                    var txtvl = 0;
                    $(".lstText input").each(function (i, val) {
                        txtval = $(this).val();
                        if (i == 0) { PMText.value = $(this).val(); }
                        else {
                            PMText.value = PMText.value + '|' + $(this).val();
                        }
                    })

                    var addmth = PMText.value.split("|");
                    var CCFourDigits = addmth[0];
                    if (CCFourDigits != "") {
                        if (CCFourDigits.length == 4) {
                            PageMethods.WMGetMandatoryDetails(pm, OnSuccessMandatory, null);
                        }
                        else {
                            showAlert(" Enter Only 4 digits", "Error", '#');
                        }
                    }
                    else {
                        showAlert("Please Enter Last Four Digits of Credit Card Number", "Error", '#');
                    }
                }
                else {
                    PageMethods.WMGetMandatoryDetails(pm, OnSuccessMandatory, null);
                }
            }

            //document.title = "hi.." + PMR.value;
            // alert('wait');
            // return PMReturn.value;
        }
        //function alertGlow(msg) {
        //    $(".glow-alert").html(msg);
        //    $(".glow-alert").delay(2000).fadeIn().delay(4000).fadeOut();
        //};


        function SavePaymentMethod() {
            var hdnNewOrderID = document.getElementById('hdnNewOrderID');
            OrderID = hdnNewOrderID.value;
            var PMReturn = document.getElementById('PMReturn');
            var pm = $('#ddlPaymentMethod').val();
            if (pm <= 4 && pm > 1) {
                PageMethods.WMGetMandatoryDetails(pm, OnSuccessMandatorySave, null);
            } else if (pm == 5) {
                var dpm = $('#ddlFOC').val();
                PageMethods.WmGetPaymentMethodLabelText('Charge To', dpm, pm, 1, OrderID);
                //PMReturn.value = 1;
            } else if (pm == 1) {
                PageMethods.WMPaymentMethodNone(pm, OrderID);
            } else if (pm >= 6) {
                PageMethods.WMGetMandatoryDetails(pm, OnSuccessMandatorySave, null);
            }
        }
        function OnSuccessMandatorySave(result) {
            var hdnNewOrderID = document.getElementById('hdnNewOrderID');
            OrderID = hdnNewOrderID.value;
            var SelCon = result
            var pm = $('#ddlPaymentMethod').val();
            var PMReturn = document.getElementById('PMReturn');
            var count = (SelCon.match(/,/g) || []).length;
            console.log(count);
            var res = result.split(",");

            var PMText = document.getElementById('PMText');
            var txtvl = 0;
            $(".lstText input").each(function (i, val) {
                txtval = $(this).val();
                if (i == 0) { PMText.value = $(this).val(); }
                else { PMText.value = PMText.value + '|' + $(this).val(); }
            });

            var addmth = PMText.value.split("|");
            // for (var i = 0; i <= res.length - 1; i++) {
            for (var i = 0; i <= addmth.length - 1; i++) {
                var PMLable = document.getElementById('PMLable');
                var lblhtml = 0;
                $(".lstLbl span").each(function (i, html) {
                    lblhtml = $(this).html();
                    if (i == 0) { PMLable.value = $(this).html(); }
                    else { PMLable.value = PMLable.value + '|' + $(this).html(); }
                });
                var Seq = i + 1;
                var addmthLbl = PMLable.value.split("|");
                PageMethods.WmGetPaymentMethodLabelText(addmthLbl[i], addmth[i], pm, Seq, OrderID);
            }
        }

        function GetDateDifference() {
            var retValue = 0;
            var today = new Date();
            var d = today.getDate(); var m = today.getMonth() + 1; var y = today.getFullYear();
            var cdt = m + '/' + d + '/' + y; //Current Date
            var hdnMaxDeliveryDays = document.getElementById('hdnMaxDeliveryDays');
            // alert(hdnMaxDeliveryDays.value);
            if (getDateFromUC("<%= UC_ExpDeliveryDate.ClientID %>") == "") {
                // showAlert("Please Select Exp. Delivery Date", "Error", "#");
                retValue = 2;
            } else {
                var sdt = getDateFromUC("<%= UC_ExpDeliveryDate.ClientID %>");//Selected Date
                var getselmonth = sdt.split("-");
                var mmmmm = getMonth(getselmonth[1]);
                var selectedDate = mmmmm + '/' + getselmonth[0] + '/' + getselmonth[2]
                //alert(selectedDate);

                var date1 = new Date(selectedDate);
                var date2 = new Date(cdt);
                var timeDiff = Math.abs(date2.getTime() - date1.getTime());
                var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24));
                // alert(diffDays);
                if (diffDays > hdnMaxDeliveryDays.value) {
                    //alert('Not OK');
                    retValue = 0;
                }
                else if (diffDays <= hdnMaxDeliveryDays.value) {
                    //alert('OK');
                    retValue = 1;
                }
            }
            return retValue;
        }

        /*If Request Type is Machine Fuilure then Mandatory Expected Consuption Detials*/
        function ddlRequestType_onchange(ddl) {
            var TrReq1 = document.getElementById('TrReq1');
            var TrReq2 = document.getElementById('TrReq2');

            var allinput1 = TrReq1.getElementsByTagName('input');
            var allinput2 = TrReq2.getElementsByTagName('input');
            var alltd1 = TrReq1.getElementsByTagName('td');
            var alltd2 = TrReq2.getElementsByTagName('td');
            var allselect1 = TrReq1.getElementsByTagName('select');
            var allselect2 = TrReq1.getElementsByTagName('select');
            var alltextarea2 = TrReq2.getElementsByTagName('textarea');

            var i = 0;
            for (i = 0; i < alltextarea2.length; i++) {
                alltextarea2[i].accessKey = "";
            }

            for (i = 0; i < alltd1.length; i++) {
                //alltd1[i].className = "";
                alltd1[i].innerHTML = alltd1[i].innerHTML.toString().replace('*', '');
                i = i + 1;
            }
            for (i = 0; i < allinput1.length; i++) {
                allinput1[i].accessKey = "";
            }
            for (i = 0; i < allselect1.length; i++) {
                allselect1[i].accessKey = "";
            }


            for (i = 0; i < alltd2.length; i++) {
                //alltd2[i].className = "";
                alltd2[i].innerHTML = alltd2[i].innerHTML.toString().replace('*', '');
                i = i + 1;
            }
            for (i = 0; i < allinput2.length; i++) {
                allinput2[i].accessKey = "";
            }
            for (i = 0; i < allselect2.length; i++) {
                allselect2[i].accessKey = "";
            }

            if (ddl.selectedIndex > 1) {
                for (i = 0; i < alltextarea2.length; i++) {
                    alltextarea2[i].accessKey = "1";
                }

                for (i = 0; i < alltd1.length; i++) {
                    //alltd1[i].className = "req";
                    alltd1[i].innerHTML = alltd1[i].innerHTML.toString().replace(':', '* :');
                    i = i + 1;
                }
                for (i = 0; i < allinput1.length; i++) {
                    allinput1[i].accessKey = "1";
                }
                for (i = 0; i < allselect1.length; i++) {
                    allselect1[i].accessKey = "1";
                }


                for (i = 0; i < alltd2.length; i++) {
                    //alltd2[i].className = "req";
                    alltd2[i].innerHTML = alltd2[i].innerHTML.toString().replace(':', '* :');
                    i = i + 1;
                }
                for (i = 0; i < allinput2.length; i++) {
                    allinput2[i].accessKey = "1";
                }
                for (i = 0; i < allselect2.length; i++) {
                    allselect2[i].accessKey = "1";
                }
            }

        }
        /*End*/

        /*Request Part List*/
        function GetSelValue(sel) {
            alert("Hi");
        }

        function isQtyZero(getQty) {
            var isZero = false;
            if (((getQty == '0') || (getQty == '0.0') || (getQty == '0.00'))) {
                isZero = true;
            }
            return isZero;
        }

        function isSerialConditionPassed(getQty, getSerialCount) {
            var chkSerialConditionPassed = false;
            //alert(getQty + ":" + getSerialCount);
            if (getSerialCount != '0' && !isQtyZero(getQty)) {
                if (confirm("Do you want to remove existing serial numbers which you have added earlier?")) {
                    $('input[data-prodid="' + currentSelectedProduct + '"]').data('searialcount', '0');
                    chkSerialConditionPassed = true;
                }
            }
            return chkSerialConditionPassed;
        }

        function GetIndex(myDD, myHdnUMO, mySpnTotalQty, usrInputQty, crntStock, Index, Price, TotPrice, moq, txtprice, ddluom) {
            var getMyId = myDD.id;
            currentSelectedProduct = $('#' + getMyId).data('ddlprodid');
            // var myDDCurrentValue = $('#' + getMyId).data('index');
            // alert("Current Product: " + currentSelectedProduct);
            var getSerialCount = $('input[data-prodid="' + currentSelectedProduct + '"]').data('searialcount');
            var getQty = $('input[data-prodid="' + currentSelectedProduct + '"]').data('qty');

            var getMyHdnField = document.getElementById(myHdnUMO);
            // if (getMyHdnField != null) {
            if (getMyHdnField != null) {
                if (((isQtyZero(getQty)) || (getSerialCount == '0')) || (isSerialConditionPassed(getQty, getSerialCount))) {

                    var myUMOval = myDD.value;
                    // alert(myUMOval);
                    var myFilterVal = myUMOval.split(":");

                    var myFilteredUMO = myFilterVal[0];
                    var myFilteredUnit = myFilterVal[1];

                    var hdnSelectedQty = document.getElementById('hdnSelectedQty');
                    hdnSelectedQty.value = myFilteredUnit;
                    var hdnSelectedUMO = document.getElementById('hdnSelectedUMO');
                    hdnSelectedUMO.value = myFilteredUMO;
                    var hdnEnteredPrice = document.getElementById('hdnEnteredPrice');
                    var EntPrice = hdnEnteredPrice.value;
                    var hdnEnteredQty = document.getElementById('hdnEnteredQty');
                    var QtyEntered = hdnEnteredQty.value;

                    var getUserInputQtyObj = document.getElementById(usrInputQty);
                    var getTotalQtyObj = document.getElementById(mySpnTotalQty);
                    var txtprice = document.getElementById(txtprice);
                    var ddluom = document.getElementById(ddluom);
                    var ddluomvalue = ddluom.value;
                    var numTotalQty = Number(getUserInputQtyObj.value) * Number(myFilteredUnit);
                    getTotalQtyObj.innerHTML = numTotalQty;

                    getMyHdnField.value = myFilteredUMO;
                    var getMyVal = $('#' + getMyId).val();
                    $('#' + getMyId).data('index', getMyVal);

                    // var getPrdPrice = document.getElementById(Price);
                    if (hdnEnteredPrice.value == "")
                    {
                        /// var CalPrice = numTotalQty * Price;
                        var CalPrice = numTotalQty * Number(txtprice.value);
                    }
                    else
                    {
                        // var CalPrice = numTotalQty * EntPrice;
                        var CalPrice = numTotalQty * Number(txtprice.value);
                    }

                    var ShowTotPrice = document.getElementById(TotPrice);
                    ShowTotPrice.innerHTML = CalPrice;

                    if (crntStock < numTotalQty)
                    {
                        getUserInputQtyObj.value = "0";
                        getTotalQtyObj.innerHTML = "0";
                        ShowTotPrice.innerHTML = "0";
                        showAlert("Requested Quantity is greater than Current Stock...!!!", "Error", '#');
                        //alert("Requested quantity is greater than.... samjun ghya!!!");
                    }
                    else
                    {
                        var moqv = moq;
                        if (moqv > 0)
                        {
                            var rem = numTotalQty % moqv;
                            if (rem > 0)
                            {
                                getUserInputQtyObj.value = "0";
                                getTotalQtyObj.innerHTML = "0";
                                showAlert("Requested Quantity is not in range of MOQ ...!!!", "Error", '#');
                            }
                            else
                            {
                                var order = new Object();
                                order.Sequence = Index + 1;
                                order.RequestQty = numTotalQty;
                                order.UOMID = myFilteredUMO;   // myHdnUMO;  //myFilteredUnit;//  myFilteredUMO;
                                order.Total = CalPrice;
                                PageMethods.WMUpdRequestPartNew(order, UpdRequestPartNewOnSussess, null);
                                PageMethods.WMGetTotal(OnSuccessGetTotal, null);
                                // PageMethods.WMUpdRequestPart(order, null, null);
                                //PageMethods.WMGetTotal(OnSuccessGetTotal, null);
                            }
                        }
                        else if (moqv == 0)
                        {
                            var order = new Object();
                            order.Sequence = Index + 1;
                            order.RequestQty = numTotalQty;
                            order.UOMID = myFilteredUMO; // myHdnUMO;  //myFilteredUnit;//  myFilteredUMO;
                            order.Total = CalPrice;
                            PageMethods.WMUpdRequestPartNew(order, UpdRequestPartNewOnSussess, null);
                            PageMethods.WMGetTotal(OnSuccessGetTotal, null);
                            // PageMethods.WMUpdRequestPart(order, null, null);
                            // PageMethods.WMGetTotal(OnSuccessGetTotal, null);
                        }
                    }

                    // alert("My UMO: " + myFilteredUMO + "My Unit: " + myFilteredUnit + " My IndexNo: " + Index);
                } else {
                    var getMyOldVal = $('#' + getMyId).data('index');
                    $('#' + getMyId).val(getMyOldVal);
                }
            } else {
                var getMyOldVal = $('#' + getMyId).data('index');
                $('#' + getMyId).val(getMyOldVal);
            }
            /*} else {
                myDDCurrentValue = myDD.value;
            }*/
        }


        var myObjqtytext = null;

        function GetIndexQty(myDD, myHdnQty, myHdnUMO, mySpnTotalQty, usrInputQty, crntStock, Index, Price, TotPrice, moq, txtprice, ddluom) {
            var getMyId = myDD.id;
            currentSelectedProduct = $('#' + getMyId).data('prodid');
            var myOldQty = $('#' + getMyId).data('qty');
            var myNewVal = $('#' + getMyId).val();
            // $('*[data-prodid="' + currentSelectedProduct + '"]').data('searialcount', result);
            // var getCurrentQtyVal = $('*[data-prodid="' + currentSelectedProduct + '"]').val();
            //  $('*[data-prodid="'+ currentSelectedProduct +'"]').data('qty', getCurrentQtyVal);

            //  alert(myOldQty + ":" + myNewVal);
            if (myOldQty != myNewVal) {
                //  alert("Processing condition");
                var getSerialCount = $('input[data-prodid="' + currentSelectedProduct + '"]').data('searialcount');
                //  alert(getSerialCount);
                if ((getSerialCount == '0') || (confirm("Do you want to remove existing serial numbers which you have added earlier?"))) {



                    // $('*[data-prodid="' + currentSelectedProduct + '"]').data('searialcount', result);
                    // var getCurrentQtyVal = $('*[data-prodid="' + currentSelectedProduct + '"]').val();
                    // $('*[data-prodid="'+ currentSelectedProduct +'"]').data('qty', getCurrentQtyVal);

                    var hdnSelectedQty = document.getElementById('hdnSelectedQty');
                    var selUMOQty = hdnSelectedQty.value;
                    var hdnSelectedUMO = document.getElementById('hdnSelectedUMO');
                    var selUMOID = hdnSelectedUMO.value;
                    var hdnEnteredPrice = document.getElementById('hdnEnteredPrice');
                    var EntPrice = hdnEnteredPrice.value;
                    var hdnEnteredQty = document.getElementById('hdnEnteredQty');
                    var eq = hdnEnteredQty.value;
                    var enterQty = myDD.value;
                    hdnEnteredQty.value = myDD.value;

                    var getMyHdnField = myHdnQty;  //myHdnUMO;
                    var myUMOval = myHdnUMO.value;
                    var myFilteredUnit = 0;

                    myFilteredUnit = myHdnQty;
                    myObjqtytext = usrInputQty;
                    var getUserInputQtyObj = document.getElementById(usrInputQty);
                    var getTotalQtyObj = document.getElementById(mySpnTotalQty);
                    var txtprice = document.getElementById(txtprice);
                    var ddluom = document.getElementById(ddluom);
                    var ddluomvalue = ddluom.value;
                    var fields = ddluomvalue.split(':');
                    var uomid = fields[0];
                    var uomval = fields[1];


                    if (hdnEnteredQty.value == "") {

                        var numTotalQty = Number(getUserInputQtyObj.value) * Number(uomval);
                        // var numTotalQty = Number(getUserInputQtyObj.value) * Number(myHdnQty); //myHdnUMO       
                        getTotalQtyObj.innerHTML = numTotalQty;
                        getMyHdnField.value = myFilteredUnit;  // myFilteredUMO;
                        var getMyVal = $('select[data-ddlprodid="' + currentSelectedProduct + '"]').val();
                        $('select[data-ddlprodid="' + currentSelectedProduct + '"]').data('index', getMyVal);

                    } else {
                        if (selUMOQty == "") {
                            var numTotalQty = hdnEnteredQty.value * Number(uomval);
                            //  var numTotalQty = hdnEnteredQty.value * Number(myHdnQty);
                            getTotalQtyObj.innerHTML = numTotalQty;
                        }
                        else {
                            var numTotalQty = hdnEnteredQty.value * Number(uomval);
                            // var numTotalQty = hdnEnteredQty.value * selUMOQty;
                            // var numTotalQty = hdnEnteredQty.value * Number(myHdnQty);
                            getTotalQtyObj.innerHTML = numTotalQty;
                        }
                    }


                    if (hdnEnteredPrice.value == "") {
                        var updatedPrice = Number(txtprice.value)
                        // var CalPrice = numTotalQty * Price;
                        var CalPrice = numTotalQty * updatedPrice;
                        var ShowTotPrice = document.getElementById(TotPrice);
                        ShowTotPrice.innerHTML = CalPrice;
                    } else {
                        var updatedPrice = Number(txtprice.value)
                        var CalPrice = numTotalQty * updatedPrice;
                        // var CalPrice = numTotalQty * hdnEnteredPrice.value;
                        var ShowTotPrice = document.getElementById(TotPrice);
                        ShowTotPrice.innerHTML = CalPrice;
                    }

                    if (crntStock < numTotalQty) {
                        getUserInputQtyObj.value = "0";
                        getTotalQtyObj.innerHTML = "0";
                        ShowTotPrice.innerHTML = "0";
                        showAlert("Requested Quantity is greater than Current Stock...!!!", "Error", '#');
                    }
                    else {
                        var moqv = moq;
                        if (moqv > 0) {
                            var rem = numTotalQty % moqv;
                            if (rem > 0) {
                                getUserInputQtyObj.value = "0";
                                getTotalQtyObj.innerHTML = "0";
                                showAlert("Requested Quantity is not in range of MOQ ...!!!", "Error", '#');
                            } else {
                                var order = new Object();
                                order.Sequence = Index + 1;
                                order.RequestQty = numTotalQty;
                                if (selUMOID == "") {
                                    order.UOMID = myHdnUMO;
                                } else {
                                    order.UOMID = selUMOID; //myHdnUMO;  //myFilteredUnit;//  myFilteredUMO;
                                }
                                order.Total = CalPrice;
                                PageMethods.WMUpdRequestPartNew(order, UpdRequestPartNewOnSussess, null);
                                PageMethods.WMGetTotal(OnSuccessGetTotal, null);

                                //      PageMethods.WMUpdRequestPart(order, null, null);
                                // PageMethods.WMGetTotal(OnSuccessGetTotal, null);
                            }
                        } else if (moqv == 0) {
                            var order = new Object();
                            order.Sequence = Index + 1;
                            order.RequestQty = numTotalQty;
                            if (selUMOID == "") {
                                order.UOMID = myHdnUMO;
                            } else {
                                order.UOMID = selUMOID; //myHdnUMO;  //myFilteredUnit;//  myFilteredUMO;
                            }
                            order.Total = CalPrice;
                            PageMethods.WMUpdRequestPartNew(order, UpdRequestPartNewOnSussess, null);
                            PageMethods.WMGetTotal(OnSuccessGetTotal, null);

                            //   PageMethods.WMUpdRequestPart(order, null, null);
                            // PageMethods.WMGetTotal(OnSuccessGetTotal, null);
                        }
                    }
                } else {
                    // alert("Condition failed");
                    $('#' + getMyId).val(myOldQty);
                    // $(this).val(myOldQty);
                }

            }
        }



        function UpdRequestPartNewOnSussess(result) {
            var getCurrentQtyVal = $('input[data-prodid="' + currentSelectedProduct + '"]').val();
            $('input[data-prodid="' + currentSelectedProduct + '"]').data('qty', getCurrentQtyVal);
            // alert(currentSelectedProduct + ":" + $('input[data-prodid="' + currentSelectedProduct + '"]').data('qty'));
            if (result == "sameqty") {
                PageMethods.WMGetTotal(OnSuccessGetTotal, null);
            }
            else {
                // alert(result);
                var qty = result.split(',');
                var hdnnewskuqty = document.getElementById("<%=hdnnewskuqty.ClientID%>");
                //  alert(qty[1].toString());
                //  alert(qty[2].toString());
                hdnnewskuqty.value = qty[1].toString();
                var hdnchnageqtyskuid = document.getElementById("<%=hdnchnageqtyskuid.ClientID%>");
                hdnchnageqtyskuid.value = qty[2].toString();
                PageMethods.WMRemoveAssignSkuSerialNew("0", hdnchnageqtyskuid.value, qty[1].toString(), OnsuccessRemoveAssignSkuSerialNEw, null);
                //var r = confirm("Do you want to remove existing serial numbers which you have added earlier?")
                //if (r == true) {
                //    //remove assign serial number 
                //    PageMethods.WMRemoveAssignSkuSerialNew("0", hdnchnageqtyskuid.value, qty[1].toString(), OnsuccessRemoveAssignSkuSerialNEw, null);
                //}
                //else {
                //    myObjqtytext.value = qty[3].toString();
                //    PageMethods.WMGetTotal(OnSuccessGetTotal, null);
                //}
            }
        }


        function OnsuccessRemoveAssignSkuSerialNEw(result) {
            if (result == "1") {
                PageMethods.WMGetTotal(OnSuccessGetTotal, null);
            }

        }

        function OnSuccessGetTotal(result) {
            var txtGrandTotal = document.getElementById("<%=txtGrandTotal.ClientID%>");
            txtGrandTotal.value = result;
            PageMethods.WMGetTotalQty(OnsuccessTotalQty, null);
        }
        function OnsuccessTotalQty(result) {
            var txtTotalQty = document.getElementById("<%=txtTotalQty.ClientID%>");
            txtTotalQty.value = result;
        }

        function GetChangedPrice(myTxt, myHdnQty, myHdnUMO, mySpnTotalQty, usrInputQty, crntStock, Index, TotPrice, ProdID, txtprice, ddluom) {
            var ChangedPrice = myTxt.value;

            var hdnEnteredPrice = document.getElementById('hdnEnteredPrice');
            var CP = hdnEnteredPrice.value;
            hdnEnteredPrice.value = myTxt.value;
            var hdnSelectedQty = document.getElementById('hdnSelectedQty');
            var SelQty = hdnSelectedQty.value;
            var hdnSelectedUMO = document.getElementById('hdnSelectedUMO');
            var selUMOID = hdnSelectedUMO.value;
            var hdnEnteredQty = document.getElementById('hdnEnteredQty');
            var QtyEntered = hdnEnteredQty.value;


            var getMyHdnField = myHdnQty;
            var myUMOval = myHdnUMO.value;
            var myFilteredUnit = 0;
            myFilteredUnit = myHdnQty;

            var getUserInputQtyObj = document.getElementById(usrInputQty);
            var getTotalQtyObj = document.getElementById(mySpnTotalQty);
            var txtprice = document.getElementById(txtprice);

            var ddluom = document.getElementById(ddluom);
            var ddluomvalue = ddluom.value;

            var fields = ddluomvalue.split(':');
            var uomid = fields[0];
            var uomval = fields[1];

            if (hdnSelectedQty.value == "") {
                // var numTotalQty = Number(getUserInputQtyObj.value) * Number(myHdnQty);
                var numTotalQty = Number(getUserInputQtyObj.value) * Number(uomval);
                getTotalQtyObj.innerHTML = numTotalQty;
                getMyHdnField.value = myFilteredUnit;
                var getMyVal = $('select[data-ddlprodid="' + currentSelectedProduct + '"]').val();
                $('select[data-ddlprodid="' + currentSelectedProduct + '"]').data('index', getMyVal);
            } else {
                if (hdnEnteredQty.value == "") {

                    //  var numTotalQty = Number(getUserInputQtyObj.value) * Number(myHdnQty);
                    var numTotalQty = Number(getUserInputQtyObj.value) * Number(uomval);
                    //  var numTotalQty = Number(getUserInputQtyObj.value) * Number(hdnSelectedQty.value);
                    getTotalQtyObj.innerHTML = numTotalQty;
                }
                else {
                    // var numTotalQty = QtyEntered * SelQty;
                    var numTotalQty = QtyEntered * Number(uomval);
                    getTotalQtyObj.innerHTML = numTotalQty;
                }
            }

            var CalPrice = numTotalQty * ChangedPrice;
            var ShowTotPrice = document.getElementById(TotPrice);
            ShowTotPrice.innerHTML = CalPrice;

            if (crntStock < numTotalQty) {
                getUserInputQtyObj.value = "0";
                getTotalQtyObj.innerHTML = "0";
                ShowTotPrice.innerHTML = "0";
                showAlert("Requested Quantity is greater than Current Stock...!!!", "Error", '#');
            } else {
                var order = new Object();
                order.Sequence = Index + 1;
                order.RequestQty = numTotalQty;
                if (selUMOID == "") {
                    order.UOMID = myHdnUMO;
                } else {
                    order.UOMID = selUMOID; //myHdnUMO;  //myFilteredUnit;//  myFilteredUMO;
                }
                order.Total = CalPrice;
                order.Price = ChangedPrice;
                order.IsPriceChange = 1;
                PageMethods.WMUpdRequestPartPrice(order, ProdID, null, null);
                PageMethods.WMGetTotal(OnSuccessGetTotal, null);
            }

        }

        function getselectedUOM(dropdown, datafield, rowIndex) {
            var ddlvalue = dropdown.value;
            if (ddlvalue == "") ddlvalue = 0;
            if (Grid1.Rows[rowIndex].Cells[datafield].Value != ddlvalue.value) {
                Grid1.Rows[rowIndex].Cells[datafield].Value = ddlvalue.value;
                PageMethods.WmUpdateRequestPartUOM(getPartUOM(rowIndex), null, null);
            }
        }

        function getPartUOM(rowIndex) {
            /*Save Request Part UOM into TempData when changed*/
            var order = new Object();
            order.Sequence = Grid1.Rows[rowIndex].Cells['Sequence'].Value;
            order.UOMID = Grid1.Rows[rowIndex].Cells['UOMID'].Value;
            return order;
        }

        function markAsFocused(textbox) {
            textbox.className = 'excel-textbox-focused';
            textbox.select();
        }


        function markAsBlured(textbox, dataField, rowIndex) {
            textbox.className = 'excel-textbox';

            var txtvalue = textbox.value;
            if (txtvalue == "") txtvalue = 0;
            textbox.value = parseFloat(txtvalue).toFixed(2);
            if (Grid1.Rows[rowIndex].Cells[dataField].Value != textbox.value) {
                Grid1.Rows[rowIndex].Cells[dataField].Value = textbox.value;
                PageMethods.WMUpdateRequestQty(getOrderObject(rowIndex), null, null);
            }
        }

        var myObjtext = null;
        function markAsBlured123(textbox, dataField, rowIndex) {
            textbox.className = 'excel-textbox';
            alert(rowIndex);
            var txtvalue = textbox.value;
            var hdnchangeqtyvalue = document.getElementById("<%=hdnchangeqtyvalue.ClientID%>");
            hdnchangeqtyvalue.value = textbox.value;
            var hdnrowindex = document.getElementById("<%=hdnrowindex.ClientID%>");
            hdnrowindex.value = rowIndex;
            if (txtvalue == "") txtvalue = 0;
            textbox.value = parseFloat(txtvalue).toFixed(2);
            if (Grid1.Rows[rowIndex].Cells[dataField].Value != textbox.value) {
                Grid1.Rows[rowIndex].Cells[dataField].Value = textbox.value;
                myObjtext = textbox;
                PageMethods.WMUpdateRequestQty1(getOrderObject(rowIndex), UpdateRequestQtyOnSussess, null);
            }
        }


        function getOrderObject(rowIndex) {
            /*Save Request qty into TempData when changed*/
            var order = new Object();
            order.Sequence = Grid1.Rows[rowIndex].Cells['Sequence'].Value;
            order.RequestQty = Grid1.Rows[rowIndex].Cells['RequestQty'].Value;
            return order;
        }


        function UpdateRequestQtyOnSussess(result) {
            if (result == "sameqty") {

            }
            else {
                var qty = result.split(',')
                var hdnnewskuqty = document.getElementById("<%=hdnnewskuqty.ClientID%>");
                hdnnewskuqty.value = qty[1].toString();
                var hdnchnageqtyskuid = document.getElementById("<%=hdnchnageqtyskuid.ClientID%>");
                hdnchnageqtyskuid.value = "43285";  // qty[2].toString();
                var r = confirm("Do you want to remove existing serial numbers which you have added earlier?")
                if (r == true) {
                    //remove assign serial number                      

                    PageMethods.WMRemoveAssignSkuSerial("0", hdnchnageqtyskuid.value, "0", OnsuccessRemoveAssignSkuSerial1, null);
                }
                else {

                }
            }
        }



        function OnsuccessRemoveAssignSkuSerial1(result) {
            if (result == "1") {
                var hdnnewskuqty = document.getElementById("<%=hdnnewskuqty.ClientID%>");
                myObjtext.value = hdnnewskuqty.value;
                var hdnrowindex = document.getElementById("<%=hdnrowindex.ClientID%>");
                PageMethods.WMUpdateRequestQty(getOrderObject(hdnrowindex.value), null, null);
            }

        }

        function removePartFromList(sequence, skuid, serialflag) {
            /*Remove Part from list*/

            var hdnProductSearchSelectedRec = document.getElementById('hdnProductSearchSelectedRec');
            hdnProductSearchSelectedRec.value = "";
            PageMethods.WMRemovePartFromRequest(sequence, skuid, serialflag, removePartFromListOnSussess, null);
        }

        function removePartFromListOnSussess(result) {
            if (result == 0) {
                showAlert("Not Applicable.......", "error", "#");
            } else {
                Grid1.refresh();
            }
        }
        /*End Request Part List*/

        /*Exp*/

        /*Get Engine Details when Select Engine*/
        function jsGetEngineDetails(ddl) {
            if (ddl.selectedIndex > 0) {
                PageMethods.WMGetEngineDetails(ddl.options[ddl.selectedIndex].value, jsGetEngineDetailsOnSussess, null);
            }
            else {
                //  lblEngineModel.innerHTML = "";
                //  lblEngineSerial.innerHTML = "";
            }
        }

        function jsGetEngineDetailsOnSussess(result) {
            //  lblEngineModel.innerHTML = result.EngineModel;
            //  lblEngineSerial.innerHTML = result.EngineSerial;

        }
        /*End*/


        /*Fill DropDown*/

        function jsFillStatusList(state) {
            ddlStatus.options.length = 0;
            ddlLoadingOn(ddlStatus);
            PageMethods.WMFillStatus(jsFillStatusListOnSuccessed, jsFillStatusListOnFailed);
        }
        function jsFillStatusListOnSuccessed(result) {
            var ddlS = ddlStatus;
            for (var i = 0; i < result.length; i++) {
                var option1 = document.createElement("option");

                option1.text = result[i].Status;
                option1.value = result[i].ID;
                try {
                    ddlS.add(option1, null); //Standard 
                } catch (error) {
                    ddlS.add(option1); // IE only
                }
            }
            ddlLoadingOff(ddlS);
            divVisibility();

        }

        function jsFillStatusListOnFailed() {
            ddlLoadingOff(ddlStatus);
        }
        function jsFillUsersList() {

            ddlRequestByUserID.options.length = 0;
            if (ddlSites.selectedIndex > 0) {
                ddlLoadingOn(ddlRequestByUserID);
                PageMethods.WMFillUserList(ddlSites.options[ddlSites.selectedIndex].value, jsFillUsersListOnSuccess, jsFillUsersListOnFail);
            }
        }

        function jsFillUsersListOnSuccess(result) {
            var ddlR = ddlRequestByUserID;

            for (var i = 0; i < result.length; i++) {
                var option1 = document.createElement("option");
                option1.text = result[i].userName;
                option1.value = result[i].userID;
                try {
                    ddlR.add(option1, null); //Standard 
                } catch (error) {
                    ddlR.add(option1); // IE only
                }
            }
            ddlLoadingOff(ddlR);
        }

        function jsFillUsersListOnFail() {
            ddlLoadingOff(ddlRequestByUserID);
        }


        function jsFillEnginList() {
            // ddlContainer.options.length = 0;
            if (ddlSites.selectedIndex > 0) {
                //   ddlLoadingOn(ddlContainer);
                PageMethods.WMFillEnginList(ddlSites.options[ddlSites.selectedIndex].value, jsFillEnginListOnSuccess, jsFillEnginListOnFail);
            }
        }
        function jsFillEnginListOnSuccess(result) {
            // var ddlEng = ddlContainer;

            for (var i = 0; i < result.length; i++) {
                var optionE1 = document.createElement("option");
                optionE1.text = result[i].Container;
                optionE1.value = result[i].ID;
                try {
                    ddlEng.add(optionE1, null); //Standard 
                } catch (error) {
                    ddlEng.add(optionE1); // IE only
                }
            }
            ddlLoadingOff(ddlEng);
        }

        function jsFillEnginListOnFail() {
            // ddlLoadingOff(ddlContainer);
        }

        /*End*/

        function PrintAddress() {
            var ddlAddress = document.getElementById("<%=ddlAddress.ClientID %>");
            var hdnSelAddress = document.getElementById('hdnSelAddress');
            hdnSelAddress.value = ddlAddress.value;
            var Adrs = ddlAddress.options[ddlAddress.selectedIndex].text;

            var lblAddress = document.getElementById("<%=lblAddress.ClientID %>");
            lblAddress.innerHTML = Adrs;
        }

        function SubmitTemplate() {
            //alert('Submit Template');
            if (Grid1.Rows.length == 0) {
                showAlert("Add atleast one part into the Request Part List", "error", "#");
            }
            else {
                if (txtTemplateTitleNew.value == "") {
                    showAlert("Enter Template Title", "error", "#");
                }
                else if (ddlAccessTypeNew.options[ddlAccessTypeNew.selectedIndex].value.toString() == "0") {
                    showAlert("Select Access Type", "error", "#");
                }
                else {

                    

                    // LoadingOn(true);
                    var obj1 = new Object();
                    obj1.TemplateTitle = txtTemplateTitleNew.value.toString();
                    //chkTemplateTitle(obj1.TemplateTitle);
                    obj1.Accesstype = ddlAccessTypeNew.options[ddlAccessTypeNew.selectedIndex].value.toString();
                    obj1.StoreId = parseInt(ddlSites.options[ddlSites.selectedIndex].value);
                    //  obj1.Status = parseInt(ddlStatus.options[ddlStatus.selectedIndex].value);
                    //obj1.Title = txtTitle.value.toString();
                    obj1.Remark = txtRemark.value.toString();
                    //obj1.Deliverydate = Deliverydate;

                    if (ddlStatus.selectedIndex == 1) { obj1.IsSubmit = "false"; }
                    else { obj1.IsSubmit = "true"; }

                    PageMethods.WMSaveTemplateHead(obj1, WMSaveTemplateHead_onSuccessed, WMSaveTemplateHead_onFailed);
                    //}
                    //else {
                    //    showAlert("One or more request or order quantity is zero", "error", "#");
                    //}
                }
            }
        }

        function WMSaveTemplateHead_onSuccessed(result) {
            //LoadingOn(false);
            if (result == "Some error occurred" || result == "") {
                showAlert("Error occurred", "Error", "#");
            }
            else if (result == "Title Already Available") {
                showAlert("Template With This Title Already Available", "Error", "#");
                txtTemplateTitleNew.focus();
            }
            else if (result == "Template Saved Successfully") {
                showAlert(result, "info", "#");
                txtTemplateTitleNew.value = "";
                ddlAccessTypeNew.selectedIndex = 0;
            }
        }
        function WMSaveTemplateHead_onFailed() { showAlert("Error occurred", "Error", "#"); }

    </script>

    <script src="//code.jquery.com/jquery-1.10.2.js" type="text/javascript">
        $(function () {

            //loop the studentID dropdownlist in gridview
            $(".ddlUOM").each(function () {

                //get every studentID dropdownlist selected index and selected value
                alert($(this)[0].selectedIndex);
                //get every studentID dropdownlist selected value
                alert($(this).val());

            });

        })
    </script>
    <script src="//code.jquery.com/jquery-1.10.2.js" type="text/javascript">
                    $(".disableCylender input").prop('disabled', true);
                
    </script>

    <script type="text/javascript">
        /*sections[Collapsable] code*/

        function divVisibility()
        {
            divApproval(false);
            divIssue(false);
            divReceipt(false);
            divConsumption(false);
            for (var i = 0; i < ddlStatus.options.length; i++)
            {
                if (ddlStatus.options[i].text == 'Approved') { divApproval(true); }
                else if (ddlStatus.options[i].text == 'Issued') { divIssue(true); }
                else if (ddlStatus.options[i].text == 'Received') { divReceipt(true); }
                else if (ddlStatus.options[i].text == 'Consumed') { divConsumption(true); }
            }
        }

        function divApproval(val)
        {
            if (val == true) {

                linkApprovalDetail.innerHTML = "Expand";
                divApprovalDetail.className = "divDetailCollapse"
            }
            else if (val == false) {
                document.getElementById('divApprovalHead').style.display = "none";
                document.getElementById('divApprovalDetail').style.display = "none";
            }
        }

        function divIssue(val) {
            if (val == true) {
                document.getElementById('divIssueHead').style.display = "";
                document.getElementById('divIssueDetail').style.display = "";
            }
            else if (val == false) {
                document.getElementById('divIssueHead').style.display = "none";
                document.getElementById('divIssueDetail').style.display = "none";
            }
        }
        function divReceipt(val) {
            if (val == true) {
                document.getElementById('divReceiptHead').style.display = "";
                document.getElementById('divReceiptDetail').style.display = "";
            }
            else if (val == false) {
                document.getElementById('divReceiptHead').style.display = "none";
                document.getElementById('divReceiptDetail').style.display = "none";
            }
        }
        function divConsumption(val) {
            if (val == true) {
                document.getElementById('divConsumptionHead').style.display = "";
                document.getElementById('divConsumptionDetail').style.display = "";
            }
            else if (val == false) {
                document.getElementById('divConsumptionHead').style.display = "none";
                document.getElementById('divConsumptionDetail').style.display = "none";
            }
        }

        function OpenTelplateList() {
            window.open('../PowerOnRent/TemplateList.aspx', null, 'height=380px, width=810px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=300; screenY=50');
        }

        function GetDept() {
            var ddlCompany = document.getElementById("<%=ddlCompany.ClientID %>");
            var hdnselectedCompany = document.getElementById('hdnselectedCompany');
            hdnselectedCompany.value = ddlCompany.value;
            var Cmpny = ddlCompany.value;

            PageMethods.WMISProjectSiteDetails(Cmpny, OnSuccessISProjectSiteDetails, null);

        }

        function OnSuccessISProjectSiteDetails(result) {

            var ddlCompany = document.getElementById("<%=ddlCompany.ClientID %>");
            var hdnselectedCompany = document.getElementById('hdnselectedCompany');
            hdnselectedCompany.value = ddlCompany.value;
            var Cmpny = ddlCompany.value;
            var hdnISProjectSiteDetails = document.getElementById("hdnISProjectSiteDetails");
            hdnISProjectSiteDetails.value = result;
            //10266
            if (result == "Yes")
            {
                $('#divprojectsite').removeClass('active');
                $('#divlatitude').removeClass('active');
            }
            else
            {

                $('#divprojectsite').addClass('active');
                $('#divlatitude').addClass('active');
            }

            PageMethods.WMGetDept(Cmpny, OnSuccessCompany, null);

        }



        function OnSuccessCompany(result) {
            ddlSites = document.getElementById("<%=ddlSites.ClientID %>");
            ddlSites.options.length = 0;
            var option0 = document.createElement("option");

            if (result.length > 0) {
                option0.text = '--Select--';
                option0.value = '0';
            }
            else {
                option0.text = 'N/A';
                option0.value = '0';
            }

            try {
                ddlSites.add(option0, null);
            }
            catch (Error) {
                ddlSites.add(option0);
            }

            for (var i = 0; i < result.length; i++) {
                var option1 = document.createElement("option");
                option1.text = result[i].Territory;
                option1.value = result[i].ID;
                try {
                    ddlSites.add(option1, null);
                }
                catch (Error) {
                    ddlSites.add(option1);
                }
            }
        }

        function GetContact1() {
            ddlSites = document.getElementById("<%=ddlSites.ClientID %>");
            ddlCompany = document.getElementById("<%=ddlCompany.ClientID %>");
            var hdnselectedDept = document.getElementById('hdnselectedDept');
            hdnselectedDept.value = ddlSites.value;

            var Dept = ddlSites.value;
            var Company = ddlCompany.value;
            //PageMethods.WMGetContactPersonLst(Dept, OnSuccessContactPerson, null);
            PageMethods.GetDeptReqUsr(Dept, getLoc_onSuccessed);
            PageMethods.WMGetContactPersonLst(Company, OnSuccessContactPerson, null);

        }

        function getLoc_onSuccessed(result)
        {
            var ddlrequser = document.getElementById("<%=ddlRequestByUserID.ClientID %>");
            ddlrequser.options.length = 0;
            for (var i in result) {
                AddOptionuser(result[i].Name, result[i].Id);
            }
        }

        function AddOptionuser(text, value) {
            var ddlrequser = document.getElementById("<%=ddlRequestByUserID.ClientID %>");
            var option = document.createElement('option');
            option.value = value;
            option.innerHTML = text;
            ddlrequser.options.add(option);
        }






        function OnSuccessContactPerson(result) {
            ddlContact1 = document.getElementById("<%=ddlContact1.ClientID %>");
            ddlContact1.options.length = 0;
            var option0 = document.createElement("option");

            if (result.length > 0) {
                option0.text = '--Select--';
                option0.value = '0';
            }
            else {
                option0.text = 'N/A';
                option0.value = '0';
            }

            try {
                ddlContact1.add(option0, null);
            }
            catch (Error) {
                ddlContact1.add(option0);
            }

            for (var i = 0; i < result.length; i++) {
                var option1 = document.createElement("option");
                option1.text = result[i].Name;
                option1.value = result[i].ID;
                try {
                    ddlContact1.add(option1, null);
                }
                catch (Error) {
                    ddlContact1.add(option1);
                }
            }
        }

        function GetContact2() {
            var ddlContact1 = document.getElementById("<%=ddlContact1.ClientID %>");
            ddlSites = document.getElementById("<%=ddlSites.ClientID %>");
            ddlCompany = document.getElementById("<%=ddlCompany.ClientID %>");
            var hdnselectedCont1 = document.getElementById('hdnselectedCont1');
            hdnselectedCont1.value = ddlContact1.value;
            var Dept = ddlSites.value;
            var Company = ddlCompany.value;
            var Cont1 = ddlContact1.value;
            // PageMethods.WMGetContactPerson2Lst(Dept, Cont1, OnSuccessContactPerson2, null);
            PageMethods.WMGetContactPerson2Lst(Company, Cont1, OnSuccessContactPerson2, null);
        }
        function OnSuccessContactPerson2(result) {
            ddlContact2 = document.getElementById("<%=ddlContact2.ClientID %>");
            ddlContact2.options.length = 0;
            var option0 = document.createElement("option");

            if (result.length > 0) {
                option0.text = '--Select--';
                option0.value = '0';
            }
            else {
                option0.text = 'N/A';
                option0.value = '0';
            }

            try {
                ddlContact2.add(option0, null);
            }
            catch (Error) {
                ddlContact2.add(option0);
            }

            for (var i = 0; i < result.length; i++) {
                var option1 = document.createElement("option");
                option1.text = result[i].Name;
                option1.value = result[i].ID;
                try {
                    ddlContact2.add(option1, null);
                }
                catch (Error) {
                    ddlContact2.add(option1);
                }
            }
        }

        function ddl2Selvalue(selvalue) {
            var ddlContact2 = document.getElementById("<%=ddlContact2.ClientID %>");
            var hdnselectedCont2 = document.getElementById('hdnselectedCont2');
            hdnselectedCont2.value = ddlContact2.value;
        }
        function GetDeptID()
        {
            ddlSites = document.getElementById("<%=ddlSites.ClientID %>");
            var Dept = ddlSites.value;            
            if (Dept ==10307)
            {
                $('#divsegmenttype').removeClass('active');
            }
            else
            {
                  $('#divsegmenttype').addClass('active');
            }
            
            var hdnselectedDept = document.getElementById('hdnselectedDept');
            hdnselectedDept.value = ddlSites.value;
            PageMethods.WMGetDepartmentSession(Dept,onsuccessdept,null);
            var hdnMaxDeliveryDays = document.getElementById('hdnMaxDeliveryDays');
            PageMethods.WMGetMaxDeliveryDays(Dept, OnsuccessmaxDeliveryDays, null);
        }
        function onsuccessdept(result)
        {
            var hdnERPAutoapr = document.getElementById('hdnErpAutoApproval');
            hdnERPAutoapr.value = result;
        }
        function OnsuccessmaxDeliveryDays(result)
        {
            var hdnMaxDeliveryDays = document.getElementById('hdnMaxDeliveryDays');
            hdnMaxDeliveryDays.value = result;
            __doPostBack('<%=UpdExpDDate.ClientID %>', '');
        }

        function displaysegmenttype()
        {
             ddlSites = document.getElementById("<%=ddlSites.ClientID %>");
            var Dept = ddlSites.value;            
            if (Dept ==10307)
            {
                $('#divsegmenttype').removeClass('active');
            }
            else
            {
                  $('#divsegmenttype').addClass('active');
            }
        }
        function GetRequestor()
        {
            var RequestorID = document.getElementById("<%=ddlRequestByUserID.ClientID%>");
            var value = RequestorID.value;
            document.getElementById("<%=txtLocation.ClientID%>").value = "";
            document.getElementById("<%=lblLocation.ClientID%>").innerHTML = "";
            PageMethods.WMGetRequestorIDLoc(value, null, null);
        }

        function CheckParts()
        {
            ddlSites = document.getElementById("<%=ddlSites.ClientID %>");
            var Dept = ddlSites.value;
            var hdnChngDept = document.getElementById("<%=hdnChngDept.ClientID%>");
            if (Grid1.Rows.length == 0) { }
            else {
                showAlert("Order can be processed only for one department per request. Change in department selection will remove the Sku selected.", "Error");
                //    var r = confirm("Order can be processed only for one department per request. Change in department selection will remove the Sku selected. Do you want to change department?")
                //    if (r == true) {                   
                hdnChngDept.value = "0x00x0";
                Grid1.refresh();
                hdnChngDept.value = "1x1";
                //    } else {
                //       //ddlSites.disabled = true;
                //    }
            }
        }
        function AnotherFunction() {
            ddlSites = document.getElementById("<%=ddlSites.ClientID %>");
            var Dept = ddlSites.value;
            ddlSites.disabled = true;
        }
        function GetAddress() {
            ddlSites = document.getElementById("<%=ddlSites.ClientID %>");
            var hdnselectedDept = document.getElementById('hdnselectedDept');
            hdnselectedDept.value = ddlSites.value;
            ddlCompany = document.getElementById("<%=ddlCompany.ClientID %>");
            var Company = ddlCompany.value;

            var Dept = ddlSites.value;
            //PageMethods.WMGetDeptAddress(Dept, OnSuccessDeptAddress, null);
            PageMethods.WMGetDeptAddress(Company, OnSuccessDeptAddress, null);
        }
        function OnSuccessDeptAddress(result) {
            var ddlAddress = document.getElementById("<%=ddlAddress.ClientID %>");
            ddlAddress.options.length = 0;
            var option0 = document.createElement("option");

            if (result.length > 0) {
                option0.text = '--Select--';
                option0.value = '0';
            }
            else {
                option0.text = 'N/A';
                option0.value = '0';
            }

            try {
                ddlAddress.add(option0, null);
            }
            catch (Error) {
                ddlAddress.add(option0);
            }

            for (var i = 0; i < result.length; i++) {
                var option1 = document.createElement("option");
                option1.text = result[i].AddressLine1;
                option1.value = result[i].ID;
                try {
                    ddlAddress.add(option1, null);
                }
                catch (Error) {
                    ddlAddress.add(option1);
                }
            }
        }
        function AddCorrespondance() {
            window.open('../PowerOnRent/Correspondance.aspx?VW=', null, 'height=475px, width=825px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=300; screenY=50');
        }

        function AddCorrespondanceVW(viewObj) {
            var getObjId = $(viewObj).attr('data-containerId');
            // alert(getTitle);
            //  return false;
            window.open('../PowerOnRent/Correspondance.aspx?VW=' + getObjId, null, 'height=450px, width=825px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=300; screenY=50');
        }

        function openCorrenpondance(CorID) {
            window.open('../PowerOnRent/Correspondance.aspx?CORID=' + CorID + '', null, 'height=450px, width=825px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=300; screenY=50');
        }

        function RequestOpenEntryForm(invoker, state, AprvlID, DeligateID, requestID, ApprovalId) {
            if (state != "gray") {
                if (state == "red") {
                    var DelID = DeligateID;
                    if (DelID == "") DelID = "0";
                    var hdnApprovalId = document.getElementById("hdnApprovalId");
                    hdnApprovalId.value = ApprovalId;
                    PageMethods.WMGetApproverForApprove(AprvlID, requestID, DelID, OnSuccessApproverForApprove, null);
                    ////window.open('../PowerOnRent/Approval.aspx?APID='+ AprvlID +'&REQID='+ requestID +'&ST=3', null, 'height=180px, width=595px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=300; screenY=50');
                    // window.open('../PowerOnRent/Approval.aspx?REQID=' + requestID + '&ST=3', null, 'height=180px, width=595px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=300; screenY=50');
                }
                else {
                    showAlert("Not Applicable", '', '#');
                }
            }
            else {
                showAlert("Not Applicable", '', '#');
            }
        }
        function OnSuccessApproverForApprove(result) {
            if (result == "AccessDenied") {
                showAlert("Access Denied", '', '#');
            } else {
                var hdnApprovalId = document.getElementById("hdnApprovalId");
                window.open('../PowerOnRent/Approval.aspx?REQID=' + result + '&ST=3&APL=' + hdnApprovalId.value + '', null, 'height=180px, width=900px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=300; screenY=50');

                
                // window.open('../PowerOnRent/Approval.aspx?REQID=' + requestID + '&ST=3', null, 'height=180px, width=595px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=300; screenY=50');
            }
        }

        function RequestOpenEntryFormReject(invoker, state, AprvlID, DeligateID, requestID, ApprovalId) {
            if (state != "gray") {
                if (state == "red") {
                    var DelID = DeligateID;
                    if (DelID == "") DelID = "0";
                    var hdnApprovalId = document.getElementById("hdnApprovalId");
                    hdnApprovalId.value = ApprovalId;
                    //if (DeligateID == 0) {                       
                    //}
                    //else {
                    //}                                          
                    PageMethods.WMGetApproverForReject(AprvlID, requestID, DelID, OnSuccessApproverForReject, null);
                    //window.open('../PowerOnRent/Approval.aspx?REQID=' + requestID + '&ST=4', null, 'height=180px, width=595px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=300; screenY=50');
                }
                else {
                    showAlert("Not Applicable", '', '#');
                }
            }
            else {
                showAlert("Not Applicable", '', '#');
            }
        }
        function OnSuccessApproverForReject(result) {
            if (result == "AccessDenied") {
                showAlert("Access Denied", '', '#');
            } else {
                //Add by suraj for show in IE
                var hdnApprovalId = document.getElementById("hdnApprovalId");
                //Add by suraj for show in IE
                window.open('../PowerOnRent/Approval.aspx?REQID=' + result + '&ST=4&APL=' + hdnApprovalId.value + '', null, 'height=180px, width=900px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=300; screenY=50');


                // window.open('../PowerOnRent/Approval.aspx?REQID=' + requestID + '&ST=4', null, 'height=180px, width=595px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=300; screenY=50');
            }
        }
        function RequestOpenEntryAppWithRevision(state, AprvlID, requestID) {
            if (state != "gray") {
                if (state == "red") {
                    showAlert("OMS enabled request # " + requestID + " for editing. Please edit the request for your revision & save for Approval with revision.", '', '#');
                    jsEditData();
                }
                else {
                    showAlert("Not Applicable", '', '#');
                }
            }
            else {
                showAlert("Not Applicable", '', '#');
            }

        }

        function jsEditData() {
            // var txtTitle = document.getElementById("<%= txtTitle.ClientID %>");
            changemode(false, 'divRequestDetail');
            //document.getElementById(txtTitle).disabled = true;
        }

        function openContactSearch(sequence) {
            window.open('../PowerOnRent/AddEditSearchContact.aspx?Con=1', null, 'height=500px, width=825px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=300; screenY=50');
        }
        function openContactSearch2(sequence) {
            window.open('../PowerOnRent/AddEditSearchContact.aspx?Con=2', null, 'height=500px, width=825px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=300; screenY=50');
        }
        function openAddressSearch(sequence) {
            window.open('../PowerOnRent/AddEditSearchAddress.aspx', null, 'height=500px, width=825px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=300; screenY=50');
        }
        function openLocationSearch(sequence) {
            var requestorID = document.getElementById("<%=ddlRequestByUserID.ClientID %>");
            window.open('../PowerOnRent/GetLocation.aspx?RequestorID='+requestorID.value+'', null, 'height=500px, width=825px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=300; screenY=50');
        }

        function openContactSearchSiteCode(sequence) {
            window.open('../PowerOnRent/AddEditSitecode.aspx', null, 'height=500px, width=825px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=300; screenY=50');
        }
        function AfterSitecodeSelected(SiteID, SiteCode, Sitecodedescription) {


            var hdnsitecode = document.getElementById("hdnsitecode");
            var hdnsitecodeid = document.getElementById("hdnsitecodeid");
            var hdnsitedescription = document.getElementById("hdnsitedescription");

            hdnsitecode.value = SiteCode;
            hdnsitecodeid.value = SiteID;
            hdnsitedescription.value = Sitecodedescription;

           <%-- var txtsitecode = document.getElementById("<%= txtsitecode.ClientID %>");
            txtsitecode.value = SiteCode;
             var lblsiteDescription = document.getElementById("<%= lblsiteDescription.ClientID %>");
            lblsiteDescription.value = Sitecodedescription;--%>

        }
        function AfterContact1Selected(ConID, ConName) {
            var Con1ID = document.getElementById("hdnSearchContactID1");
            var Con1NM = document.getElementById("hdnSearchContactName1");
            Con1ID.value = ConID;
            Con1NM.value = ConName;
            var txtContact1 = document.getElementById("<%= txtContact1.ClientID %>");
            txtContact1.value = ConName;
        }
        function AfterContact2Selected(ConID, ConName) {
            var Con2ID = document.getElementById("hdnSearchContactID2");
            var Con2NM = document.getElementById("hdnSearchContactName2");
            Con2ID.value = ConID;
            Con2NM.value = ConName;
            var txtContact2 = document.getElementById("<%= txtContact2.ClientID %>");
            txtContact2.value = ConName;
        }

        function AfterAddressselected(AdrID, AdrName) {
            var AdrsID = document.getElementById("hdnSearchAddressID");
            var AdrsNm = document.getElementById("hdnSearchAddress");
            AdrsID.value = AdrID;
            AdrsNm.value = AdrName;
            var txtAddress = document.getElementById("<%= txtAddress.ClientID %>");
            var lblAddress = document.getElementById("<%= lblAddress.ClientID %>");
            txtAddress.value = AdrName;
            lblAddress.innerHTML = AdrName;
        }


        function AfterProjectTypeSelected(ProjectId, ProjectTypenm)
        {
            var hdnselectedprojectid = document.getElementById("hdnselectedprojectid");
            var hdnselectedprojectnm = document.getElementById("hdnselectedprojectnm");
            hdnselectedprojectid.value = ProjectId;
            hdnselectedprojectnm.value = ProjectTypenm;
            var txtprojectype = document.getElementById("<%= txtprojectype.ClientID %>");
            txtprojectype.value = ProjectTypenm;
            document.getElementById("hdnselectedsiteid").value = "";
            var txtsitecode = document.getElementById("<%= txtsitecode.ClientID %>");
            var txtsitenm = document.getElementById("<%= txtsitenm.ClientID %>");
            var txtLatitude = document.getElementById("<%= txtLatitude.ClientID %>");
            var txtLongitude = document.getElementById("<%= txtLongitude.ClientID %>");
            var txtAccessRequirement = document.getElementById("<%= txtAccessRequirement.ClientID %>");

            txtsitecode.value = "";
            txtsitenm.value = "";
            txtLatitude.value = "";
            txtLongitude.value = "";
            txtAccessRequirement.value = "";
        }

        function AfterSiteSelect(siteid, sitecode, sitenm, latitude, logitude, accessspe) {
            var txtsitecode = document.getElementById("<%= txtsitecode.ClientID %>");
            var txtsitenm = document.getElementById("<%= txtsitenm.ClientID %>");
            var txtLatitude = document.getElementById("<%= txtLatitude.ClientID %>");
            var txtLongitude = document.getElementById("<%= txtLongitude.ClientID %>");
            var txtAccessRequirement = document.getElementById("<%= txtAccessRequirement.ClientID %>");
            var hdnselectedsiteid = document.getElementById("hdnselectedsiteid");
            hdnselectedsiteid.value = siteid;
            txtsitecode.value = sitecode;
            txtsitenm.value = sitenm;
            txtLatitude.value = latitude;
            txtLongitude.value = logitude;
            txtAccessRequirement.value = accessspe;
        }

        function AfterLocationSelected(LocID, LocName, LocConID, LocConNM, LName, LocCode) {
            var LocationID = document.getElementById("hdnSearchLocationID");
            var LocationName = document.getElementById("hdnSearchLocation");
            LocationID.value = LocID;
            LocationName.value = LocName;
            var txtLocation = document.getElementById("<%= txtLocation.ClientID %>");
            //txtLocation.value = LocName;
            var LocationCodeName = LocCode + " " + LName;
            txtLocation.value = LocationCodeName;
            var lblLocation = document.getElementById("<%= lblLocation.ClientID %>");
            lblLocation.innerHTML = LocName;
            var LocContactID = document.getElementById("hdnLocConID");
            var LocConName = document.getElementById("hdnLocConName");
            LocContactID.value = LocConID;
            LocConName.value = LocConNM;
            var txtContact1 = document.getElementById("<%= txtContact1.ClientID %>");
            txtContact1.value = LocConNM;
            var Con1ID = document.getElementById("hdnSearchContactID1");
            Con1ID.value = LocConID;
        }

        function divPMChng() {
            var hdnPmethodChng = document.getElementById("hdnPmethodChng");
            hdnPmethodChng.value = "1";
        }

        function GetPaymentMethodID() {
            var hdnSelPaymentMethod = document.getElementById("hdnSelPaymentMethod");
            $selval = $('#ddlPaymentMethod').val();
            hdnSelPaymentMethod.value = $selval;
            if ($selval == 5) {
                $('#dvFOC').removeClass('active');
                $('#dvLst').addClass('active');
            }
            else {
                __doPostBack('<%=UpdLst.ClientID %>', '');
                $('#dvLst').removeClass('active');
                $('#dvFOC').addClass('active');
            }
        }


        function EbabledTrRow(enabled) {
            //var divtemplate=document.getElementById("divtemplate");
            //      divtemplate.disabled = false;
            $("#divlatitude").find("input,button,textarea").attr("disabled", true);
        }

        function EbabledTrRow1()
        {

            $("#divtemplate").find("input,button,textarea,select").attr("disabled", true);
            $("#divborder").find("input,button,textarea,select").attr("disabled", true);
            $("#divtitle").find("input,button,textarea,select").attr("disabled", true);
            $("#divcustorderno").find("input,button,textarea,select").attr("disabled", true);
            $("#divreqtdate").find("input,button,textarea,select").attr("disabled", true);
            $("#divcontact2").find("input,button,textarea,select").attr("disabled", true);
            $("#divlocn").find("input,button,textarea,select").attr("disabled", true);
            $("#divremark").find("input,button,textarea,select").attr("disabled", true);
            $("#divpaymentm").find("input,button,textarea,select").attr("disabled", true);
            $("#divcustname").find("input,button,textarea,select").attr("disabled", true);
            $("#divprojectsite").find("input,button,textarea,select").attr("disabled", true);
            $("#divlatitude").find("input,button,textarea,select").attr("disabled", true);

            // var divprojectsite=document.getElementById("divprojectsite");
            //var allimg = divprojectsite.getElementsByTagName("img");

            //for (var m = 0; m < allimg.length; m++) {

            //     allimg[m].style.visibility = "hidden"; 
            //}

            var divreqtdate = document.getElementById("divreqtdate")
            var allimg1 = divreqtdate.getElementsByTagName("img");
            for (var m = 0; m < allimg1.length; m++) {

                allimg1[m].style.visibility = "hidden";
            }

            var divcontact2 = document.getElementById("divcontact2")
            var allimg2 = divcontact2.getElementsByTagName("img");
            for (var m = 0; m < allimg2.length; m++) {

                allimg2[m].style.visibility = "hidden";
            }
        }


        function ShowDetails()
        {
            $('#divprojectsite').removeClass('active');
            $('#divlatitude').removeClass('active');

        }
        function NotShowDetails()
        {
            $('#divprojectsite').addClass('active');
            $('#divlatitude').addClass('active');
        }

        function OnSuccessISProjectSiteDetails1(result)
        {

            var hdnISProjectSiteDetails = document.getElementById("hdnISProjectSiteDetails");
            hdnISProjectSiteDetails.value = result;
            //10266
            if (result == "Yes") {
                $('#divprojectsite').removeClass('active');
                $('#divlatitude').removeClass('active');
            }
            else {

                $('#divprojectsite').addClass('active');
                $('#divlatitude').addClass('active');
            }



        }

        function GetProjecttype() {
            var hdnselectedCompany = document.getElementById('hdnselectedCompany');
            $selval = $('#ddlCompany').val();
            hdnselectedCompany.value = $selval;
            PageMethods.WMISProjectSiteDetails1($selval, OnSuccessISProjectSiteDetails1, null);
        }

        function PaymentMethod() {
            ddlSites = document.getElementById("<%=ddlSites.ClientID %>");
            var Dept = ddlSites.value;
            var hdnselectedDept = document.getElementById('hdnselectedDept');
            hdnselectedDept.value = ddlSites.value;
            PageMethods.WMGetPaymentMethod(Dept, onSuccessGetPaymentMEthod, null);
        }
        function onSuccessGetPaymentMEthod(result) {
            var ddlPaymentMethod = document.getElementById("<%=ddlPaymentMethod.ClientID %>");
            ddlPaymentMethod.options.length = 0;
            var option0 = document.createElement("option");

            if (result.length > 0) {
                //option0.text = '--Select--';
                //option0.value = '0';               
            }
            else {
                option0.text = 'N/A';
                option0.value = '0';
            }

            //try {
            //    ddlPaymentMethod.add(option0, null);
            //}
            //catch (Error) {
            //    ddlPaymentMethod.add(option0);
            //}

            for (var i = 0; i < result.length; i++) {
                var option1 = document.createElement("option");
                option1.text = result[i].MethodName;
                option1.value = result[i].PMethodID;
                try {
                    ddlPaymentMethod.add(option1, null);
                }
                catch (Error) {
                    ddlPaymentMethod.add(option1);
                }
            }
            ddlSites = document.getElementById("<%=ddlSites.ClientID %>");
            var Dept = ddlSites.value;
            var hdnselectedDept = document.getElementById('hdnselectedDept');
            hdnselectedDept.value = ddlSites.value;
            PageMethods.WMGetCostCenter(Dept, onSuccessGetCostCenter, null);
        }
        function onSuccessGetCostCenter(result) {
            var ddlFOC = document.getElementById("<%=ddlFOC.ClientID %>");
            ddlFOC.options.length = 0;
            var option0 = document.createElement("option");

            if (result.length > 0) {
                option0.text = '--Select--';
                option0.value = '0';
            }
            else {
                option0.text = 'N/A';
                option0.value = '0';
            }

            try {
                ddlFOC.add(option0, null);
            }
            catch (Error) {
                ddlFOC.add(option0);
            }

            for (var i = 0; i < result.length; i++) {
                var option1 = document.createElement("option");
                option1.text = result[i].CenterName;
                option1.value = result[i].ID;
                try {
                    ddlFOC.add(option1, null);
                }
                catch (Error) {
                    ddlFOC.add(option1);
                }
            }
        }
        function openShowSerial(skuid, isserial) {
            if (isserial == "Yes") {
                //  alert(skuid);
                var hdncustanalya = document.getElementById("<%= hdncustanalya.ClientID %>");
                if (hdncustanalya.value == "" || hdncustanalya.value == null) {
                    hdncustanalya.value = "0";
                }

                var hdnshowserialsku = document.getElementById("<%= hdnshowserialsku.ClientID %>");
                hdnshowserialsku.value = skuid;
                if (hdnshowserialsku.value == "" || hdnshowserialsku.value == null) {
                    hdnshowserialsku.value = "0";
                }
                PageMethods.WMChkSerialNoaddorNot(skuid, hdncustanalya.value, OnsuccessChkSerialNoaddorNot, null);
                //  window.open('../PowerOnRent/ShowOrderSerial.aspx?oid=' + hdncustanalya.value + '&sid=' + skuid, null, 'height=700px, width=1000px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=300; screenY=50');
            }
            else {
                showAlert("Not Applicable", '', '#');
            }

        }

        function OnsuccessChkSerialNoaddorNot(result) {
            if (result == "0") {
                showAlert("No serial number added.", '', '#');
            }
            else {
                var hdncustanalya = document.getElementById("<%= hdncustanalya.ClientID %>");
                var hdnshowserialsku = document.getElementById("<%= hdnshowserialsku.ClientID %>");
                window.open('../PowerOnRent/ShowOrderSerial.aspx?oid=' + hdncustanalya.value + '&sid=' + hdnshowserialsku.value, null, 'height=700px, width=1000px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=300; screenY=50');
            }
        }

        var currentSelectedProduct = '';
        function openaddShowSerial(skuid, isserial) {
            currentSelectedProduct = skuid;
            // alert(isserial);
            // alert(skuid);
            if (isserial == "Yes") {
                var hdncustanalya = document.getElementById("<%= hdncustanalya.ClientID %>");
                var hdnserialsku = document.getElementById("<%= hdnserialsku.ClientID %>");
                var hdnOrderisEdit = document.getElementById("<%= hdnOrderisEdit.ClientID %>");
                hdnserialsku.value = skuid;
                // alert(hdncustanalya.value);
                //  alert(hdnOrderisEdit.value);
                if (hdncustanalya.value == "0") {
                    //   alert(hdncustanalya.value);
                    PageMethods.WMGetSelectedSKUQty(skuid, hdncustanalya.value, OnsuccessopenaddShowSerial, null);
                }
                else if (hdnOrderisEdit.value == "Yes") {
                    //  alert(hdnOrderisEdit.value);
                    PageMethods.WMGetSelectedSKUQty(skuid, hdncustanalya.value, OnsuccessopenaddShowSerial, null);
                }
                else {
                    showAlert("Not Applicable", '', '#');
                }
            }
            else {
                showAlert("Not Applicable", '', '#');
            }
        }

        function updateSerialCountOnPopupClose(serialcnt) {
            // alert(serialcnt);
            $('input[data-prodid="' + currentSelectedProduct + '"]').data('searialcount', serialcnt);
        }

        function gotorequestpage(result) {
            window.open('../PowerOnRent/Default.aspx?invoker=Request', '_self', '');
        }

        function OnsuccessopenaddShowSerial(result) {

            var result1 = result.split(',');

            // alert(result);
            // var checkMyQty = $('*[data-prodid="' + currentSelectedProduct + '"]').val();
            /* alert("Result:- " + result1[0] + " | " + result1[1]);
             if (result1[1] == "1") {
                 $('input[data-prodid="' + currentSelectedProduct + '"]').data('searialcount', result);
             }*/

            //   var getCurrentQtyVal = $('*[data-prodid="' + currentSelectedProduct + '"]').val();
            //   $('*[data-prodid="'+ currentSelectedProduct +'"]').data('qty', getCurrentQtyVal);
            // alert("MyProductQty:- " + checkMyQty);
            if (result1[0].toString() == "0") {
                showAlert("Enter SKU Quantity.", '', '#');
            }
            else {
                var hdnSerialSKUQty = document.getElementById("<%= hdnSerialSKUQty.ClientID %>");
                hdnSerialSKUQty.value = result1[0];
                var hdncustanalya = document.getElementById("<%= hdncustanalya.ClientID %>");
                if (hdncustanalya.value == "" || hdncustanalya.value == null) {
                    hdncustanalya.value = "0";
                }
                var hdnserialsku = document.getElementById("<%= hdnserialsku.ClientID %>");
                if (hdnserialsku.value == "" || hdnserialsku.value == null) {
                    hdnserialsku.value = "0";
                }
                PageMethods.WMGetPreviousserialcnt(hdncustanalya.value, hdnserialsku.value, result1[0].toString(), OnsuccessGetPreviousserialcnt, null);
            }
        }


        function OnsuccessGetPreviousserialcnt(result) {
            $('input[data-prodid="' + currentSelectedProduct + '"]').data('searialcount', result);
            var hdnSerialSKUQty = document.getElementById("<%= hdnSerialSKUQty.ClientID %>");
            var hdncustanalya = document.getElementById("<%= hdncustanalya.ClientID %>");
            if (hdncustanalya.value == "" || hdncustanalya.value == null) {
                hdncustanalya.value = "0";
            }
            var hdnserialsku = document.getElementById("<%= hdnserialsku.ClientID %>");
            if (hdnserialsku.value == "" || hdnserialsku.value == null) {
                hdnserialsku.value = "0";
            }
            if (result != "0") {


                if (hdncustanalya.value != "0") {
                    var r = confirm("Do you want to remove existing serial numbers which you have added earlier?")
                    if (r == true) {
                        //remove assign serial number
                        PageMethods.WMRemoveAssignSkuSerial(hdncustanalya.value, hdnserialsku.value, result, OnsuccessRemoveAssignSkuSerial, null);
                    }
                    else {

                    }
                }
                else {
                    var r = confirm("Do you want to remove existing serial numbers which you have added earlier?")
                    if (r == true) {
                        //remove assign serial number
                        PageMethods.WMRemoveAssignSkuSerial(hdncustanalya.value, hdnserialsku.value, result, OnsuccessRemoveAssignSkuSerial, null);
                    }
                    else {

                    }
                }
            }
            else {
                window.open('../Product/ProductSearchWithSerial.aspx?oid=' + hdncustanalya.value + '&sid=' + hdnserialsku.value + '&qty=' + hdnSerialSKUQty.value, null, 'height=700px, width=1000px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=300; screenY=50');
            }
        }

        function OnsuccessRemoveAssignSkuSerial(result) {

            if (result == "1") {
                $('input[data-prodid="' + currentSelectedProduct + '"]').data('searialcount', '0');
                var hdnSerialSKUQty = document.getElementById("<%= hdnSerialSKUQty.ClientID %>");
                if (hdnSerialSKUQty.value == "" || hdnSerialSKUQty.value == null) {
                    hdnSerialSKUQty.value = "0";
                }
                var hdncustanalya = document.getElementById("<%= hdncustanalya.ClientID %>");
                if (hdncustanalya.value == "" || hdncustanalya.value == null) {
                    hdncustanalya.value = "0";
                }
                var hdnserialsku = document.getElementById("<%= hdnserialsku.ClientID %>");
                if (hdnserialsku.value == "" || hdnserialsku.value == null) {
                    hdnserialsku.value = "0";
                }

                if (hdnSerialSKUQty.value == "0") {
                    showAlert("Enter SKU Quantity.", '', '#');
                }
                else {
                    window.open('../Product/ProductSearchWithSerial.aspx?oid=' + hdncustanalya.value + '&sid=' + hdnserialsku.value + '&qty=' + hdnSerialSKUQty.value, null, 'height=700px, width=1000px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=300; screenY=50');
                }
            }
            else {
                showAlert("Not Applicable", '', '#');
            }

        }

        function openEditProduct(Seq) {
            var hdnSelectedSequenceNo = document.getElementById('hdnSelectedSequenceNo');
            hdnSelectedSequenceNo.value = Seq;
            var ddlStatus = document.getElementById("<%= ddlStatus.ClientID %>");
            var Status = ddlStatus.value;
            if (Status == 2 || Status == 21 || Status == 22 || Status == 31) {
                PageMethods.WMGetAccessOfProductChange(Seq, OnsuccessGetAccessProductChange, null);
            } else {
                showAlert("Not Applicable", '', '#');
            }
        }
        function OnsuccessGetAccessProductChange(result) {
            if (result == 0) {
                showAlert("Not Applicable", '', '#');
            } else {
                window.open('../PowerOnRent/ChangeOrderProduct.aspx', null, 'height=300px, width=925px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=300; screenY=50');
            }
        }

        function AfterProductQtyChange(PrdQtyChng) {
            PageMethods.WMGetTotalQtyGrandTotal(OnsuccessTotalQtyGrandTotal, null);
        }
        function OnsuccessTotalQtyGrandTotal(result) {
            var txtTotalQty = document.getElementById("<%= txtTotalQty.ClientID %>");
            var txtGrandTotal = document.getElementById("<%= txtGrandTotal.ClientID %>");
            txtTotalQty.value = result[0].TotalQty;
            txtGrandTotal.value = result[0].GrandTotal;
            Grid1.refresh();
        }
        function disableExpDeliveryDate() {
            $('#UC_ExpDeliveryDate').attr("disabled", "disabled");
        }

        //Add by suraj for Dispatch Grid
        var hdngridvalue = document.getElementById("<%= hdngridvalue.ClientID %>");
        var hdnselectedorder = document.getElementById("<%= hdnselectedorder.ClientID %>");
        var hdnUserType = document.getElementById("<%= hdnUserType.ClientID %>");

        var hdndeliverytype = document.getElementById("<%= hdndeliverytype.ClientID %>");

        function PendingforActivation(invoker, OrderNo, ReadyForActivation, Skucode) {
            var userType = hdnUserType.value;
            if (ReadyForActivation == "gray") {
                showAlert("Not Applicable..", '', '#');
            }
            else {

                if (ReadyForActivation != "green") {
                    if (ReadyForActivation == "red") {
                        var hdngridvalue = document.getElementById("<%= hdngridvalue.ClientID %>");
                        if (userType == "Fulfilment") {
                            window.open('../PowerOnRent/ActivationForm.aspx?OrdNo=' + OrderNo + '&Skucode=' + Skucode + '&ST=24&APL=0', null, 'height=140px, width=550px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=300; screenY=50');
                        }
                        else {
                            showAlert("Access Denied..", '', '#');
                        }
                    }
                    else {
                        showAlert("Already Activated..", '', '#');
                    }
                }
                else {
                    showAlert("Already Activated..", '', '#');
                }
            }
        }


        function PendingForDriverAllocation(invoker, OrderNo, PendingForDriverAllocation, Skucode) {
            if (PendingForDriverAllocation == "green") { showAlert("Already Driver Allocate..", '', '#'); }
            else {

                if (PendingForDriverAllocation == "gray") {
                    showAlert("Not Applicable..", '', '#');
                }
                else {

                    hdngridvalue.value = PendingForDriverAllocation;
                    hdnselectedorder.value = OrderNo;
                    var userType = hdnUserType.value;

                    PageMethods.WMCheckAllProductISActiveOrNot(OrderNo, OnsuccessProductISActiveOrNot, null);
                }
            }

        }
        function OnsuccessProductISActiveOrNot(result) {

            //if(result=="green")
            //{
            //    showAlert("Already driver assign for this order...", '', '#');
            //}
            //else 
            if (result == "red") {
                showAlert("Firstly activate the all product...", '', '#');
            }
            else {
                var PendingForDriverAllocation = hdngridvalue.value;
                var OrderNo = hdnselectedorder.value;
                var userType = hdnUserType.value;
                if (userType == "Super Admin" || userType == "Admin") {
                    //     window.open('../PowerOnRent/AllocateDriver.aspx?OID=' + OrderNo + '&ST=DispatchstatusGrid&APL=0', null, 'height=550, width=700,status= 0, resizable= 1, scrollbars=0, toolbar=0,location=center,menubar=0, screenX=300; screenY=50');
                    window.open('../PowerOnRent/AllocateDriver.aspx?OID=' + OrderNo + '&ST=DispatchstatusGrid&Default=No', null, 'height=550, width=700,status= 0, resizable= 1, scrollbars=0, toolbar=0,location=center,menubar=0, screenX=300; screenY=50');

                }
                else {
                    showAlert("Access Denied", '', '#');
                }
            }
        }

        function OutForDelivery(invoker, OrderNo, OutForDelivery, Skucode, PackageID, srno) {
            if (OutForDelivery == "gray") {
                showAlert("Not Applicable..", '', '#');
            }
            else {
                window.open('VirtualProductReport.aspx?id=' + OrderNo + '&Skucode=' + Skucode + '&PackageID=' + PackageID + '&srno=' + srno, null, 'height=1100px, width=1200px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=300; screenY=50');
            }
        }

        function PendingForDocument(invoker, OrderNo, PendingForDocument, Skucode) {
            if (PendingForDocument == "gray") {
                showAlert("Not Applicable..", '', '#');
            }
            else {
                var userType = hdnUserType.value;
                if (userType == "Admin" || userType == "GWC User" || userType == "Super Admin") {
                    var myBomWin = window.open('VirtualSkuDocument.aspx?Sequence=' + OrderNo + '&Skucode=' + Skucode, null, 'height=300px, width=825px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=300; screenY=50');
                }
                else {
                    showAlert("Access Denied", '', '#');
                }
            }
        }


        function UploadDelivered(invoker, OrderNo, UploadDelivered, Skucode) {
            if (UploadDelivered == "gray") {
                showAlert("Not Applicable..", '', '#');
            }
            else {
                if (UploadDelivered == "green") {
                    showAlert("Not Applicable..", '', '#');
                }
                else {
                    var userType = hdnUserType.value;
                    var DeliveryType = hdndeliverytype.value;
                    if (DeliveryType == "HUB") {
                        if (userType == "Retail User Admin") {
                            var myBomWin = window.open('DocDownloadWindow.aspx?Sequence=' + OrderNo + '&Skucode=' + Skucode, null, 'height=180spx, width=825px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=300; screenY=50');
                        }
                        else {
                            showAlert("Access Denied", '', '#');
                        }
                    }
                    else if (DeliveryType == "Home") {
                        if (userType == "Fulfilment") {
                            var myBomWin = window.open('DocDownloadWindow.aspx?Sequence=' + OrderNo + '&Skucode=' + Skucode, null, 'height=180spx, width=825px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=300; screenY=50');
                        }
                        else {
                            showAlert("Access Denied", '', '#');
                        }
                    }
                    else {
                        showAlert("Access Denied", '', '#');
                    }


                }
            }
        }

        //add by suraj for only fullfillment can edit the data
        function EditMSISDN(Id, Skucode) {
            var hdncustanalya = document.getElementById("<%= hdncustanalya.ClientID %>");

            var userType = hdnUserType.value;
            if (userType == "GWC User") {
                var myBomWin = window.open('UpdateMSISDN.aspx?Id=' + Id + '&Skucode=' + Skucode, null, 'height=140px, width=825px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=300; screenY=50');
            }
            else {
                showAlert("Access Denied", '', '#');
            }
        }

        function CustomerAnalytics() {
            var hdncustanalya = document.getElementById("<%= hdncustanalya.ClientID %>");
            var OrdNo = hdncustanalya.value;
            window.open('../PowerOnRent/CustAnalytics.aspx?Id=' + OrdNo, null, 'height=300px, width=1500px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=300; screenY=100');
        }

        //Add by suraj for Dispatch Grid


        //Add by suraj for change status for retail admin
        function ChangeStatus() {
            var hdncustanalya = document.getElementById("<%= hdncustanalya.ClientID %>");
            var OrdNo = hdncustanalya.value;
            window.open('../PowerOnRent/ChangeStatus.aspx?Id=' + OrdNo, null, 'height=170px, width=600px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=300; screenY=50');

        }

        function Chkxmltag() {
            var txtRemark = document.getElementById("<%= txtRemark.ClientID %>");
            var txtRemarkval = txtRemark.value;
            var result = txtRemarkval.replace(/<|>|&|'|"/g, '');

            txtRemark.value = result;
        }
         function openSegmentType()
        {
            window.open('../PowerOnRent/SegmentType.aspx', null, 'height=480px, width=800px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=300; screenY=50');

        }
        function AfterSegmentTypeSelected(ID, SegmentTypenm)
        {
            var segmentID = document.getElementById("hdnsegmentID");
            var segmentname = document.getElementById("hdnsegmenttype");
            segmentID.value = ID;
            segmentname.value = SegmentTypenm;
            var txtsegmenttype = document.getElementById("<%= txtsengmenttype.ClientID %>");
            txtsegmenttype.value = SegmentTypenm;
            
        }

    </script>
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
    <style type="text/css">
        /*POR Collapsable Div*/

        .PanelCaption {
            color: Black;
            font-size: 13px;
            font-weight: bold;
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
                color: #483D8B;
                margin: 3px 3px 3px 3px;
            }

            .divHead a {
                float: right;
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
    <script type="text/javascript">
        /*Checkbox js for css*/
        var d = document;
        var safari = (navigator.userAgent.toLowerCase().indexOf('safari') != -1) ? true : false;
        var gebtn = function (parEl, child) { return parEl.getElementsByTagName(child); };
        onload = function () {

            var body = gebtn(d, 'body')[0];
            body.className = body.className && body.className != '' ? body.className + ' has-js' : 'has-js';

            if (!d.getElementById || !d.createTextNode) return;
            var ls = gebtn(d, 'label');
            for (var i = 0; i < ls.length; i++) {
                var l = ls[i];
                if (l.className.indexOf('label_') == -1) continue;
                var inp = gebtn(l, 'input')[0];
                if (l.className == 'label_check') {
                    l.className = (safari && inp.checked == true || inp.checked) ? 'label_check c_on' : 'label_check c_off';
                    l.onclick = check_it;
                };
                if (l.className == 'label_radio') {
                    l.className = (safari && inp.checked == true || inp.checked) ? 'label_radio r_on' : 'label_radio r_off';
                    l.onclick = turn_radio;
                };
            };
        };
        var check_it = function () {
            var inp = gebtn(this, 'input')[0];
            if (this.className == 'label_check c_off' || (!safari && inp.checked)) {
                this.className = 'label_check c_on';
                if (safari) inp.click();
            } else {
                this.className = 'label_check c_off';
                if (safari) inp.click();
            };
        };
        var turn_radio = function () {
            var inp = gebtn(this, 'input')[0];
            if (this.className == 'label_radio r_off' || inp.checked) {
                var ls = gebtn(this.parentNode, 'label');
                for (var i = 0; i < ls.length; i++) {
                    var l = ls[i];
                    if (l.className.indexOf('label_radio') == -1) continue;
                    l.className = 'label_radio r_off';
                };
                this.className = 'label_radio r_on';
                if (safari) inp.click();
            } else {
                this.className = 'label_radio r_off';
                if (safari) inp.click();
            };
        };
        /*End*/

    </script>
    <style type="text/css">
        .has-js .label_check, .has-js .label_radio {
            padding-left: 25px;
            padding-bottom: 10px;
        }

        .has-js .label_radio {
            background: url(../App_Themes/Blue/img/radio-off.png) no-repeat;
        }

        .has-js .label_check {
            background: url("../App_Themes/Blue/img/check-off.png") no-repeat;
        }

        .has-js label.c_on {
            background: url("../App_Themes/Blue/img/check-on.png") no-repeat;
        }

        .has-js label.r_on {
            background: url(../App_Themes/Blue/img/radio-on.png) no-repeat;
        }

        .has-js .label_check input, .has-js .label_radio input {
            position: absolute;
            left: -9999px;
        }
    </style>
</asp:Content>
