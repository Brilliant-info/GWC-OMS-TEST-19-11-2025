<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/CRM2.Master" AutoEventWireup="true" CodeBehind="NASerial.aspx.cs" Inherits="PowerOnRentwebapp.CommonControls.NASerial" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:TextBox runat="server" Width="700px" ID="txtnaserial" TextMode="MultiLine" ReadOnly="true" Text=""></asp:TextBox>
</asp:Content>
