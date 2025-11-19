<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/CRM.Master" AutoEventWireup="true"
    CodeBehind="DashboardPOR.aspx.cs" Inherits="PowerOnRentwebapp.POR.DashboardPOR" %>

<%@ Register Src="../DashBoard/DashBoard.ascx" TagName="DashBoard" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHolder_FormHeader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHolder_Form" runat="server">
    <center>
        <div>
            <table>
                <tr>
                    <td rowspan="2">
                        <a href="../POR/DashboardPOR.aspx?invoker=site">
                            <img alt="" src="../commonControls/HomeSetupImg/dashboard.jpg" />
                        </a>
                    </td>
                    <td style="text-align: left">
                        <a href="../POR/DashboardPOR.aspx?invoker=site" class="ParentGroup">Site Wise </a>
                    </td>
                    <td rowspan="2">
                        <a href="../POR/DashboardPOR.aspx?invoker=product">
                            <img alt="" src="../commonControls/HomeSetupImg/dasbaord2.png" />
                        </a>
                    </td>
                    <td style="text-align: left">
                        <a href="../POR/DashboardPOR.aspx?invoker=product" class="ParentGroup">Part Wise</a>
                    </td>
                    <td rowspan="2">
                        <a href="../POR/DashboardPOR.aspx?invoker=engine">
                            <img alt="" src="../commonControls/HomeSetupImg/Dashboad1.png" /></a>
                    </td>
                    <td style="text-align: left">
                        <a href="../POR/DashboardPOR.aspx?invoker=engine" class="ParentGroup">Engine Wise</a>
                    </td>
                    <td rowspan="2">
                        <a href="../POR/Dashboard_Enginewise.aspx">
                            <img alt="" src="../CommonControls/HomeSetupImg/Dasboard_Eng.png" /></a>
                    </td>
                    <td style="text-align: left">
                        <a href="../POR/Dashboard_Enginewise.aspx" class="ParentGroup">Engine Wise Consumption</a>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <a href="../POR/DashboardPOR.aspx?invoker=site">Site Wise</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </br> Consumption All Site
                    </td>
                    <td style="text-align: left">
                        <a href="../POR/DashboardPOR.aspx?invoker=product">Part Wise</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </br> Partwise Consumption As on date
                    </td>
                    <td style="text-align: left">
                        <a href="../POR/DashboardPOR.aspx?invoker=engine">Engine Wise</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </br> Consumption for All Engine
                    </td>
                    <td style="text-align: left">
                        <a href="../POR/Dashboard_Enginewise.aspx">Engine Wise Consumption</a> </br> Engine
                        Wise Consumption of Part
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                    </td>
                </tr>
            </table>
        </div>
        <center>
            <div>
                <table>
                    <tr>
                        <td rowspan="3">
                            <%-- <uc1:DashBoard ID="DashBoard1" runat="server" />--%>
                            <uc1:DashBoard ID="DashBoard1" runat="server" />
                        </td>
                    </tr>
                </table>
            </div>
        </center>
        <%--  <uc1:DashBoard ID="DashBoard2" runat="server" />
        <uc1:DashBoard ID="DashBoard3" runat="server" />--%>
    </center>
    <script type="text/javascript">
    </script>
</asp:Content>
