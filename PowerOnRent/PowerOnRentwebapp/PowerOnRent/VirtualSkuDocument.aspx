<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VirtualSkuDocument.aspx.cs" Inherits="PowerOnRentwebapp.PowerOnRent.VirtualSkuDocument" Theme="Blue" %>

<%@ Register Src="../Document/UC_AttachDocument.ascx" TagName="UC_AttachDocument" TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" style="background-color: White;">
          <asp:ScriptManager ID="ScriptManager12" runat="server" EnablePageMethods="true" />
    <div>

        <asp:Panel ID="paneldoc" runat="server" Visible="true">
        <table>
            <tr>
                <td>
                     <div class="divDetailExpand" id="divDocumentDetails" runat="server" clientidmode="Static">
            <uc3:UC_AttachDocument ID="UC_AttachmentDocument1" runat="server" />
        </div>
                </td>
            </tr>
             <tr>
                <td style="text-align:right">                    
                        <%--<input type="button" runat="server" value="Document Upload Complete" id="btnDocUpload" onclick="DocumentUploadComplete();" />--%>
                    <asp:Button ID="btndocuplodcomplete" runat="server" Text="Document Upload Complete" OnClick="btndocuplodcomplete_Click" />
                    
                </td>
            </tr>
        </table>
            </asp:Panel>

        <asp:Panel ID="panelmsg" runat="server" Visible="false">

            <table width="100%">
                <tr>
                    <td width="15%"></td>
                     <td width="70%">
                         <br />
                             <table class="tableForm">
                  <tr>
                      <td colspan="2" style="text-align:center">
                             <asp:Label ID="lblmsg" runat="server"  ></asp:Label> 
                      </td>  
            </tr>
            <tr>    
                <td style="text-align:right">
                    <br />
                     <input type="button" id="btnCompleted" runat="server" value="Complete"  onclick="Completed()"/>
                </td>                
                <td style="text-align:left">
                     <br />
                       <input type="button" id="btncancel" runat="server" value="Cancel"  onclick="Cancel()"/>
                </td>
            </tr>
        </table>
                     </td>
                    <td width="15%"></td>
                </tr>
            </table>
          

            </asp:Panel>
   

   
    </div>
          <asp:HiddenField ID="hndSelectedRec" runat="server" ClientIDMode="Static" />
         <asp:HiddenField ID="hndSelectedRecSku" runat="server" ClientIDMode="Static" />
    </form>
     <script type="text/javascript">
         var OrderNo = hndSelectedRec.value;
         var Skucode = hndSelectedRecSku.value;
         //function DocumentUploadComplete() {             
         //    PageMethods.CheckPendingforAvtivation(OrderNo, Skucode, OnSuccessDocumentUploadComplete, null);
         //}

         //function OnSuccessDocumentUploadComplete(result) {
         //    if (result == "No") {
         //        showAlert('Documentation Upload Already Completed...', 'Error', '#');
         //        //self.close();
         //    }
         //    else {
         //        window.open('../PowerOnRent/DocUploadWindow.aspx?OrdNo=' + OrderNo + '&Skucode=' + Skucode + '&ST=24&APL=0', null, 'height=80px, width=595px,status= 0, resizable= 1, scrollbars=1, toolbar=0,location=center,menubar=0, screenX=300; screenY=50');
         //    }
         //}


         function Completed() {

             var hndSelectedRec = document.getElementById("<%= hndSelectedRec.ClientID %>");
             var hndSelectedRecSku = document.getElementById("<%= hndSelectedRecSku.ClientID %>");
             var OrderId = hndSelectedRec.value;
             var Skucode = hndSelectedRecSku.value;
             document.getElementById("btnCompleted").style.visibility = "hidden";
             PageMethods.CompletedDocUpload(OrderId, Skucode, OnSuccessCompletedDocUpload, null)
         }
         function OnSuccessCompletedDocUpload(result) {
             if (result == "true") {
                 self.close();
                 window.opener.GridDispatchstatus.refresh();
             }
         }

         function Cancel() {
             self.close();
         }

         var myTimer;
         function showAlert(msg, msytype, formpath) {
             var newdiv = document.createElement('div');
             newdiv.id = "divAlerts";
             newdiv.className = "alertBox0";

             var newdiv2 = document.createElement('div');
             msytype = msytype.toString().toLowerCase();
             switch (msytype) {
                 case "info":
                     newdiv2.style.borderTopColor = "green";
                     break;
                 case "error":
                     newdiv2.style.borderTopColor = "red";
                     break;
                 case "p": /*Process*/
                     newdiv2.style.borderTopColor = "navy";
                     break;
                 case "":
                     newdiv2.style.borderTopColor = "orange";
                     break;
             }


             var span1 = document.createElement('span');
             span1.id = 'alertmsg';
             newdiv2.appendChild(span1);
             newdiv.appendChild(newdiv2);
             //var strdiv = "<div id='divAlerts' class='alertBox0'><div><span id='alertmsg'></span></div></div>"
             document.body.appendChild(newdiv);
             document.getElementById('alertmsg').innerHTML = msg;
             document.getElementById('divAlerts').className = "alertBox1";
             myTimer = self.setInterval(function () { msgclock(formpath) }, 3500);
         }
         </script>

</body>
</html>
