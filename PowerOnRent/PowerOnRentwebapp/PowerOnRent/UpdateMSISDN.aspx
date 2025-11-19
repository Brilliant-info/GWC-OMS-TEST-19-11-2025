<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateMSISDN.aspx.cs" Inherits="PowerOnRentwebapp.PowerOnRent.UpdateMSISDN" Theme="Blue" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager12" runat="server" EnablePageMethods="true" />
        <div>

             <table width="100%">
                <tr>
                    <td width="20%"></td>
                     <td width="60%">
                         <br />
               
            <table  class="tableForm">
                <tr>
                     <td></td>                                              
                  
                    <td style="text-align:left">
                         <asp:Label ID="lblmsisdn" Text=" SIM Serial Number " runat="server" ></asp:Label> 
                    </td>
                    <td style="text-align:left">
                          <asp:TextBox runat="server" ID="txtMSISDN" Width="200px" ></asp:TextBox>
                    </td>

                    <td style="text-align:left">
                          <input type="button" id="btnMSISDN" runat="server" value="Add"  onclick="UpdateMSISDN()"/>
                    </td>
                        <td style=""></td>
                </tr>
            </table>

                         </td>
                     <td width="20%"></td>
                </tr>
            </table>

        </div>
        <asp:HiddenField ID="hdnselectvalue" runat="server" ClientIDMode="Static" />
          <asp:HiddenField ID="hdnskucode" runat="server" ClientIDMode="Static" />
       
    </form>

    <script type="text/javascript">

        function UpdateMSISDN() {

            var hdnselectvalue = document.getElementById("<%= hdnselectvalue.ClientID %>");
            var hdnskucode = document.getElementById("<%= hdnskucode.ClientID %>");
            var Id =  hdnselectvalue.value;
            var MSSIDN = document.getElementById("<%= txtMSISDN.ClientID %>").value;
            var Skucode = hdnskucode.value;
            PageMethods.WMUpdateMSISDN(Id,MSSIDN,Skucode, OnSuccessOperation, null);
        }
        function OnSuccessOperation(result) {
            if (result != "") {
                window.opener.GridDispatchstatus.refresh();
                self.close();
            }
        }

    </script>
</body>
</html>
