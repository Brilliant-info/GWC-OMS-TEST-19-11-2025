<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/CRM.Master" AutoEventWireup="true" CodeBehind="ImportTemplate.aspx.cs" Inherits="BrilliantWMS.ImportDesign.ImportTemplate" Theme="Blue"  EnableEventValidation="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHolder_FormHeader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHolder_Form" runat="server">
    <asp:FileUpload ID="FileuploadPO" runat="server" ClientIDMode="Static" />
    <asp:Button runat="server" ID="btnImport" OnClick="btnImport_Click" Text="Download template" />
    <asp:Button runat="server" ID="btnimportTxt" Text="Download txt template" OnClick="btnimportTxt_Click" />
    <asp:Button runat="server" ID="btnimportCsv" Text="Download csv template" OnClick="btnimportCsv_Click" />
    <asp:Button runat="server" ID="btnImportExcel" Text="Download Excel template" OnClick="btnImportExcel_Click" /> 
    <asp:Button runat="server" ID="btnExcel" Text="Download Excel template" OnClick="btnExcel_Click" /> 
    <asp:Button runat="server" id="btnExcelToCsv" text="ExcelToCsv" OnClick="btnExcelToCsv_Click"  />
    <asp:TextBox TextMode="MultiLine" ID="txtCSVdata" runat="server"></asp:TextBox>
</asp:Content>
