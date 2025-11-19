<%@ Page Title="Product Search" Language="C#" MasterPageFile="~/MasterPage/CRM2.Master"
    AutoEventWireup="true" Theme="Blue" CodeBehind="TransactionReportPopup.aspx.cs" Inherits="PowerOnRentwebapp.PowerOnRent.TransactionReportPopup" EnableEventValidation="false" %>

<%@ Register Assembly="obout_Flyout2_NET" Namespace="OboutInc.Flyout2" TagPrefix="obout" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body{
           /* background:none !important;
            background-color:#ffffff;
            background-image:none; */
        }
        .headerText{
            color:#483D8B !important;
        }
        .ob_gR {
	        background-color: #E5F4FF;
        }
        .ob_gRA {
	        background-color: #C7E4F8;
        }
        .ob_gHR th{
	        background-image: url(../App_Themes/Grid/styles/style_9/header.gif);
	        height: 31px;
	        font-family: Tahoma;
	        font-weight: bold;
	        color: #FFFFFF;
	        text-align: left;
	        cursor: pointer;
        }
        .pnlTransactionReportList{
            border:4px solid #5898C5;
        }
        .pnlTransactionReportList th,
        .pnlTransactionReportList td{
            padding:5px;
             font-size: 12px;
             text-align:center !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <!-- Header -->
    <div id="top_bar" style="margin-top: -10px;">
            <!-- BEGIN LOGO -->
            <div class="CompanyLogo" style="">
                <asp:Image ID="ClientLogo" runat="server" ImageUrl="#" ImageAlign="Middle"  Width="167px" Height="90px"/>
            </div> 
        </div>
    <!-- Header -->
        <div style="margin:20px;">
            <div style="text-align:center;">
                 <asp:Label ID="lblorderlist" CssClass="headerText" runat="server" Text="Transaction Report List" Font-Bold="True" Font-Size="XX-Large"  Height="30px"></asp:Label>
                     <br />
                 <br /> 
               <%-- <asp:Label ID="lblData" runat="server" Text="Label"></asp:Label>
                                     <br />
                 <asp:Label ID="lblErrorMessage" runat="server" Text="Label"></asp:Label>
                                     <br />
                 <asp:Label ID="lblStatusCode" runat="server" Text="Label"></asp:Label>
                                     <br />
                 <asp:Label ID="lblStatusDescription" runat="server" Text="Label"></asp:Label>
                                     <br />--%>
            </div>

            <div style="text-align:right;">
                 <asp:Button id="MyExportToExcel" Text="Export To Excel" runat="server" OnClick="ExporttoExcel_Click" BackColor="#236496" ForeColor="White" Height="35px"  />
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
             <asp:Button id="MyExportToPDF" Text="Export To PDF" runat="server" OnClick="ExporttoPDF_Click" BackColor="#236496" ForeColor="White" Height="35px"/>
            </div>
            <br />
            <div class="pnlTransactionReportList">
    
           <asp:GridView ID="GVTransactionReportInfo"  AutoGenerateColumns="false" CellPadding="0" ShowHeaderWhenEmpty="True" Width="100%"                runat="server" HeaderStyle-CssClass="ob_gHR" CssClass="ob_gBody" HeaderStyle-HorizontalAlign="Center" HeaderStyle-ForeColor="#075ADA" HeaderStyle-Font-Bold="true" RowStyle-CssClass="ob_gR" ForeColor="Black" AllowPaging="True" AllowSorting="True" EnableSortingAndPagingCallbacks="True">                <AlternatingRowStyle CssClass="ob_gRA" />            <Columns>            <asp:BoundField DataField="ID" HeaderText="ID" Visible="false"  />            <asp:BoundField DataField="sku" HeaderText="SKU"  ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center" >                     <ItemStyle HorizontalAlign="Center" /><ItemStyle Width="10%"></ItemStyle>                </asp:BoundField>            <asp:BoundField DataField="description" HeaderText="Description" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center"><ItemStyle Width="10%" HorizontalAlign="Center"></ItemStyle>                </asp:BoundField>            <asp:BoundField DataField="serial" HeaderText="Serial" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center"><ItemStyle Width="10%" HorizontalAlign="Center"></ItemStyle>                </asp:BoundField>            <asp:BoundField DataField="transactionType" HeaderText="Transaction Type" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center"><ItemStyle Width="10%" HorizontalAlign="Center"></ItemStyle>                </asp:BoundField>            <asp:BoundField DataField="referenceNo" HeaderText="Ref No" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center"><ItemStyle Width="10%" HorizontalAlign="Center"></ItemStyle>                </asp:BoundField>            <asp:BoundField DataField="transactionDate" HeaderText="Transaction Date" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center"><ItemStyle Width="10%" HorizontalAlign="Center"></ItemStyle>                </asp:BoundField>            <asp:BoundField DataField="category" HeaderText="Category" ItemStyle-Width="10%" HeaderStyle-HorizontalAlign="Center"><ItemStyle Width="10%" HorizontalAlign="Center"></ItemStyle>                </asp:BoundField>            </Columns>                <EmptyDataTemplate>                    <div style="text-align:center; Width:100% ">No records found.</div>                </EmptyDataTemplate>            </asp:GridView>
                 </div>
        </div>
        
    </asp:Content>