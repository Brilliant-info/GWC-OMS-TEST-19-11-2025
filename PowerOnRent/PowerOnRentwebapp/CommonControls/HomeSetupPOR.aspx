<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/CRM.Master" AutoEventWireup="true"
    CodeBehind="HomeSetupPOR.aspx.cs" Inherits="PowerOnRentwebapp.CommonControls.HomeSetupPOR" %>

<%@ Register Src="UCToolbar.ascx" TagName="UCToolbar" TagPrefix="uc1" %>
<%@ Register Src="UCFormHeader.ascx" TagName="UCFormHeader" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHolder_FormHeader" runat="server">
    <uc2:UCFormHeader ID="UCFormHeader1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHolder_Form" runat="server">
    <center>
        <asp:UpdateProgress ID="UpdateProgress_HomeSetup" runat="server" AssociatedUpdatePanelID="updPnl_HomeSetup">
            <ProgressTemplate>
                <center>
                    <div class="modal">
                        <img src="../App_Themes/Blue/img/ajax-loader.gif" style="top: 50%;" />
                    </div>
                </center>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="updPnl_HomeSetup" runat="server">
            <ContentTemplate>
                <br />
                <br />
                <br />
                <table class="tableForm">
                    <tr id="row1" runat="server">
                        <td id="ctd1" runat="server" rowspan="2">
                            <img alt="" src="HomeSetupImg/companymgm.jpg" />
                        </td>
                        <td id="ctd2" runat="server" style="text-align: left">
                            <asp:LinkButton ID="lnkBtnCompanyMang" runat="server" CssClass="ParentGroup" Text="Customer Management"></asp:LinkButton>
                        </td>
                        <td rowspan="2">
                            <img alt="" src="HomeSetupImg/user_Management.png" />
                        </td>
                        <td style="text-align: left">
                            <asp:LinkButton ID="lnkBtnUserMang0" runat="server" CssClass="ParentGroup" Text="User Management"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr id="row2" runat="server">
                        <td id="ctd3" runat="server" style="text-align: left; width: 350px;">
                            <a href="../Account/AccountMaster.aspx">
                                <asp:Label ID="lblcustomermaster" runat="server" Text="Customer Master"></asp:Label></a> |
                            <a href="../Account/Location.aspx">
                                <asp:Label ID="Label1" runat="server" Text="Location Master"></asp:Label></a> |
                             <a href="../PowerOnRent/ReasonCodeMst.aspx">
                                 <asp:Label ID="lblrcode" runat="server" Text="Reason Code"></asp:Label></a>
                            |
                             <a href="../PowerOnRent/ProjectTypeMaster.aspx">
                                 <asp:Label ID="lblprojecttype" runat="server" Text="Project Type Master"></asp:Label></a>
                              |
                             <a href="../PowerOnRent/SitecodeMaster.aspx">
                                 <asp:Label ID="lblsitemaster" runat="server" Text="Site Master"></asp:Label></a>
                            |
                            <a href="../PowerOnRent/SegmentMaster.aspx">
                                 <asp:Label ID="Label11" runat="server" Text="Segment Master"></asp:Label></a>|
                            

                        </td>
                        <td style="text-align: left; width: 330px;">
                            <a href="../UserManagement/RoleMaster.aspx">
                                <%--<asp:Label ID="lblrollmaster" runat="server" Text="Role Master" /></a> |--%> <a href="../UserManagement/UserCreation.aspx">
                                    <asp:Label ID="lblusermaster" runat="server" Text="User Master" /></a> |
                            <a href="../Approval/ApprovalLevelMaster.aspx">
                                <asp:Label ID="lblapproval" runat="server" Text="Approval Master" /></a>
                            <a href="../RMS/UserConfiguration.aspx">
                                <asp:Label ID="lblrmsconfig" runat="server" Text="RMS User Configuration" /></a>|
							<a href="../S2SORDER/S2SUserConfiguration.aspx">
                                <asp:Label ID="Label14" runat="server" Text="S2S User Configuration" /></a> |
                                <a href="../PowerOnRent/DedicatedDriver.aspx" runat="server" id="a2311" >
                                <asp:Label ID="lblecom" runat="server" Text="Ecom Driver Configuration" /></a>
                             <asp:Label ID="Label7" runat="server" Text="|" />
                             <a href="../PowerOnRent/UserConfig.aspx" runat="server" id="a1" >
                                <asp:Label ID="Label8" runat="server" Text="Import User Configuration" /></a>
                        </td>
                    </tr>
                    <tr>
                    </tr>
                    <tr>
                        <td colspan="4"></td>
                    </tr>
                    <tr>
                        <td rowspan="2">
                            <img alt="" src="HomeSetupImg/productmanagement.jpg" />
                        </td>
                        <td style="text-align: left">
                            <asp:LinkButton ID="lnkBtnProductMang" runat="server" CssClass="ParentGroup" Text="SKU Management"></asp:LinkButton>
                        </td>
                        <td id="ctd4" runat="server" rowspan="2">
                            <img alt="" src="HomeSetupImg/inventory.jpg" />
                        </td>
                        <td id="ctd5" runat="server" style="text-align: left">
                            <asp:LinkButton ID="lnkbtninterface" runat="server" CssClass="ParentGroup" Text="Interface Management"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            <a href="../Product/ProductMaster.aspx">
                                <asp:Label ID="lblsku" runat="server" Text="SKU" /></a> |
                            <%--<a href="../Product/ImportPriceD.aspx">--%>
                                <a href="../WMSImport/WMSImport.aspx?obj=Product">
                                <asp:Label ID="lblimportprice" runat="server" Text="Import Price" /></a> |
                             <a href="../PowerOnRent/DirectImportD.aspx">
                                
                                 
                             

                                 <asp:Label ID="Label2" runat="server" Text="Direct Order" /></a> |
                            <a href="../Product/VSkuImport.aspx">
                                <asp:Label ID="lblvirtualsku" runat="server" Text="Import Virtual SKU" /></a> |
                             <a href="../Product/VSkuStockImport.aspx">
                                 <asp:Label ID="lblvirtualstock" runat="server" Text="Import Virtual Stock" /></a>
                            <asp:Label ID="Label4" runat="server" Text="|" />
                             <a href="../PowerOnRent/VTechImport.aspx" runat="server" id="a25">
                                 <asp:Label ID="Label5" runat="server" Text="Serial Direct Order" /></a>
                        </td>
                        <td id="ctd7" runat="server" style="width: 150px;"></td>
                        <td id="ctd6" runat="server" style="text-align: left">
                            <a href="../PowerOnRent/InterfaceDefination.aspx">
                                <asp:Label ID="lblinterdef" runat="server" Text="Interface Defination" /></a>
                            | <a href="../PowerOnRent/MessageDefination.aspx">
                                <asp:Label ID="lblmsgdef" runat="server" Text="Message Defination" />
                            </a>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4"></td>
                    </tr>
                    <tr>
                        <td rowspan="2">
                            <img alt="" src="HomeSetupImg/reports.jpg" />
                        </td>
                        <td style="text-align: left">
                            <asp:LinkButton ID="lnkBtnActivityMang" runat="server" CssClass="ParentGroup" Text="Utility"></asp:LinkButton>
                        </td>
                        <td rowspan="2">
                            <img alt="" src="HomeSetupImg/help_64.png" />
                        </td>
                        <td style="text-align: left">
                            <asp:LinkButton ID="lnkHelp" runat="server" CssClass="ParentGroup" Text="Help"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            <a href="../Product/EmailTemplate.aspx">
                                <asp:Label ID="lblemailconfig" runat="server" Text="Email Configuration" /></a>
                            | <a href="../PowerOnRent/RequestTemplate.aspx">
                                <asp:Label ID="lblrequsttemp" runat="server" Text="Request Template" /></a><br />
                            <a href="../Product/ImportDSo.aspx">
                                <asp:Label ID="lblimageimport" runat="server" Text="Image Import" /></a>
                            | <a href="../PowerOnRent/StockConfig.aspx">
                                <asp:Label ID="lblstockconfig" runat="server" Text="Database Setting" /></a>
                            | <a href="../PowerOnRent/SkuSearch.aspx">
                                <asp:Label ID="lblskusearch" runat="server" Text="Sku Search" /></a>
                              <asp:Label ID="Label6" runat="server" Text="|" />
                             
                         <%--   <asp:Label ID="Label9" runat="server" Text="|" />--%>
                             <a href="../PowerOnRent/ReTrigger.aspx" runat="server" id="a2" >
                                <asp:Label ID="Label10" runat="server" Text="ERP ReTrigger" /></a>
                            <asp:Label ID="Label12" runat="server" Text="|" />
                             <a href="../PowerOnRent/DirectImportVC.aspx" runat="server" id="a3" >
                                <asp:Label ID="Label13" runat="server" Text="BulkImport" /></a> |
                            <a href="../PowerOnRent/InstSebilAPIRetriger.aspx" runat="server" id="sebilretriger" >
                            <asp:Label ID="lblsebilretriger" runat="server" Text="Installation Siebel ReTrigger" /></a>
                        </td>
                        <td style="text-align: left">
                            <a href="User Manual/Admin_GWC OMS User Manual 3.0.pdf">
                                <asp:Label ID="lblHelp" runat="server" Text="Help" /></a>
                           <asp:Label ID="Label3" runat="server" Text="|" />   <a href="../Login/ForgetPasswordByAdmin.aspx" runat="server" id="a15">
                                <asp:Label ID="lblforgetpasswordSA" runat="server" Text="Recover Password By Superadmin" /></a>
                        </td>

                    </tr>
                    <tr>
                        <td colspan="4"></td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </center>
</asp:Content>
