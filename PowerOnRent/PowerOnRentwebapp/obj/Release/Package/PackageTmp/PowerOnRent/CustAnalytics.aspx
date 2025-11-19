<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustAnalytics.aspx.cs" Inherits="PowerOnRentwebapp.PowerOnRent.CustAnalytics" Theme="Blue" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

        <asp:ScriptManager ID="ScriptManager12" runat="server" EnablePageMethods="true" />       
        <div>
         <table>
                    <tr>
                        <td>
                            <obout:Grid ID="GVCustAnalytics" runat="server" AllowAddingRecords="false" AutoGenerateColumns="false"
                                AllowColumnResizing="true" AllowFiltering="true" AllowManualPaging="true" AllowColumnReordering="true"
                                AllowMultiRecordSelection="false" AllowRecordSelection="true" AllowGrouping="true"
                                Width="100%" Serialize="true" CallbackMode="true"
                                PageSize="10" AllowPaging="true" AllowPageSizeSelection="true">
                             
                                <ClientSideEvents ExposeSender="true" />
                                <Columns>
                                    <obout:Column DataField="ID" HeaderAlign="center" Align="center" Visible="false" Width="0%"></obout:Column>
                                    <%--1--%>
                                    <obout:Column DataField="OrderNo"  HeaderText="Request No." HeaderAlign="center" Align="center" Width="15%"></obout:Column>
                                    <%--2--%>
                                    <obout:Column DataField="KahramaaID" Visible="false" HeaderText="Qatar ID" HeaderAlign="left" Align="left" Width="0%"></obout:Column>
                               <%--     <obout:Column DataField="Idt" HeaderText="Passport ID/Qatar ID" HeaderAlign="left" Align="left" Width="10%"></obout:Column>--%>

                                      <obout:Column DataField="Idtype" Visible="true" HeaderText="ID Type" HeaderAlign="left" Align="left" Width="10%"></obout:Column>

                                      <obout:Column DataField="Idt" HeaderText="ID" HeaderAlign="left" Align="left" Width="10%"></obout:Column>
                                    <%--3--%>
                                     <obout:Column DataField="CustNM" HeaderText="Customer Name" HeaderAlign="left" Align="left" Width="10%"></obout:Column>
                                  
                                    <%--4--%>
                                    <obout:Column DataField="OrderCreationDate" HeaderText="Request Date" HeaderAlign="left" Align="left" DataFormatString="{0:dd-MMM-yyyy}" Width="10%"></obout:Column>
                                    <%--5--%>
                                     <obout:Column DataField="Status" HeaderText="Order Status" HeaderAlign="left" Align="left" Width="10%"></obout:Column>

                                </Columns>
                             
                            </obout:Grid>
                        </td>
                        <tr>
                        <td style="text-align:right">
                             <input type="button" id="btncancel" runat="server" value="Close" style="float: right;" onclick="CancelWindow();"/>
                        </td>
                            </tr>
                    </tr>
                </table>
        </div>
    </form>
    <script type="text/javascript">
        function CancelWindow() {
            window.close();
        }
    </script>
</body>
</html>
