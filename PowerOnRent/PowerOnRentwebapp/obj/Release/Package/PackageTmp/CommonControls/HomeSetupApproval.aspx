<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomeSetupApproval.aspx.cs" MasterPageFile="~/MasterPage/CRM.Master" Theme="Blue" Inherits="PowerOnRentwebapp.CommonControls.HomeSetupApproval" %>

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
                            
                        </td>
                        <td style="text-align: left">
                            &nbsp;</td>
                    </tr>
                    <tr id="row2" runat="server">
                        <td id="ctd3" runat="server" style="text-align: left; width: 350px;">
                            &nbsp;<a href="../PowerOnRent/ProjectTypeMaster.aspx"><asp:Label ID="lblprojecttype" runat="server" Text="Project Type Master"></asp:Label></a>
                              |
                             <a href="../PowerOnRent/SitecodeMaster.aspx">
                                 <asp:Label ID="lblsitemaster" runat="server" Text="Site Master"></asp:Label></a>

                        </td>
                        <td style="text-align: left;">
                            <a href="../UserManagement/RoleMaster.aspx">
                                <%--<asp:Label ID="lblrollmaster" runat="server" Text="Role Master" /></a> |--%> 
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