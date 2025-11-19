<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewReportViewer.aspx.cs" Inherits="PowerOnRentwebapp.POR.Reports.NewReportViewer" EnableEventValidation="false" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=14.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title></title>
</head>
<body>
    <form id="form1" runat="server">
         <asp:ScriptManager runat="server"></asp:ScriptManager>
        <div>           
             <asp:Panel ID="pnlContents" runat="server">
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" Font-Names="Verdana" Font-Size="8pt" SizeToReportContent="true" EnableExternalImages="true" ShowPrintButton="true"   WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Height="100%" >
                           <serverreport reportserverurl="http://localhost:51490/PowerOnRent/" reportpath="POR/Reports/PartRequestList.rdlc" />        
            </rsweb:ReportViewer>
                 </asp:Panel>
        </div>
    </form>
</body>
</html>