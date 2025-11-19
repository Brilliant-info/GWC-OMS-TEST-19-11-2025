<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CloseOrder.aspx.cs" Inherits="PowerOnRentwebapp.PowerOnRent.CloseOrder" Theme="Blue" %>



<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

           <asp:ScriptManager ID="ScriptManager12" runat="server" EnablePageMethods="true" />
    <div>
       <center>
             <br />
         <table class="tableForm" width="90%">
                  <tr>
                      <td colspan="2" style="width:100%;text-align:center">
                             <asp:Label ID="lblmsg" runat="server" ></asp:Label> 
                      </td>  
            </tr>
            <tr>    
                <td style="width:50%;text-align:center">
                     <input type="button" id="btnCompleted" runat="server" value="Completed"  onclick="Completed()"/>
                </td>                
                <td style="width:50%;text-align:center">
                       <input type="button" id="btncancel" runat="server" value="Cancel"  onclick="Cancel()"/>
                </td>
            </tr>
        </table>
              </center>

           <asp:HiddenField ID="hndSelectedRec" runat="server" ClientIDMode="Static" />
         <asp:HiddenField ID="hndSelectedRecSku" runat="server" ClientIDMode="Static" />
    </div>
    </form>

    <script type="text/javascript">
        function Completed() {
            var OrderId = hndSelectedRec.value;
            var Skucode = hndSelectedRecSku.value;
            PageMethods.CompletedOrder(OrderId, Skucode, OnSuccessCompletedDocUpload, null)
        }
        function OnSuccessCompletedDocUpload(result) {
            if (result == "true") {
                window.opener.AfterCompleteOrder.refresh();
                self.close();               
            }

            function Cancel() {
                self.close();
            }

        }
</script>
</body>
</html>
